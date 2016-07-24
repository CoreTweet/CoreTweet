// The MIT License (MIT)
//
// CoreTweet - A .NET Twitter Library supporting Twitter API 1.1
// Copyright (c) 2013-2016 CoreTweet Development Team
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

#if SYNC
using System;
using System.Collections.Generic;
using System.IO;

namespace CoreTweet.Core
{
    internal abstract class MultipartItem
    {
        public string Key { get; }

        protected MultipartItem(string key)
        {
            this.Key = key;
        }

        public abstract void WriteTo(Stream stream);

        public static MultipartItem Create(string key, object value)
        {
            var valueStream = value as Stream;
            if (valueStream != null)
                return new StreamMultipartItem(key, valueStream);

            if (value is ArraySegment<byte>)
                return new ArraySegmentMultipartItem(key, (ArraySegment<byte>)value);

            var valueByteArray = value as byte[];
            if (valueByteArray != null)
                return new ByteArrayMultipartItem(key, valueByteArray);

            var valueBytes = value as IEnumerable<byte>;
            if (valueBytes != null)
                return new ByteEnumerableMultipartItem(key, valueBytes);

#if FILEINFO
            var valueFile = value as FileInfo;
            if (valueFile != null)
                return new FileInfoMultipartItem(key, valueFile);
#endif

            return new StringMultipartItem(key, value.ToString());
        }
    }

    internal class StringMultipartItem : MultipartItem
    {
        public string Value { get; }

        public StringMultipartItem(string key, string value)
            : base(key)
        {
            this.Value = value;
        }

        public override void WriteTo(Stream stream)
        {
            stream.WriteString($"Content-Disposition: form-data; name=\"{this.Key}\"\r\n\r\n");
            stream.WriteString(this.Value);
        }
    }

    internal abstract class FileMultipartItem : MultipartItem
    {
        protected FileMultipartItem(string key) : base(key) { }

        public abstract long? Length { get; }
        public virtual string FileName => null;

        protected abstract void WriteContent(Stream stream);

        public override void WriteTo(Stream stream)
        {
            stream.WriteString("Content-Type: application/octet-stream\r\n");
            stream.WriteString(string.Format("Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\n\r\n",
                this.Key,
                this.FileName != null
                    ? this.FileName.Replace("\n", "%0A").Replace("\r", "%0D").Replace("\"", "%22")
                    : "file"
            ));
            this.WriteContent(stream);
        }
    }

    internal class StreamMultipartItem : FileMultipartItem
    {
        public Stream Content { get; }

        public StreamMultipartItem(string key, Stream content)
            : base(key)
        {
            this.Content = content;
        }

        public override long? Length => this.Content.CanSeek ? (long?)this.Content.Length : null;

        protected override void WriteContent(Stream stream)
        {
            const int bufferSize = 81920;
            var buffer = new byte[bufferSize];
            int count;
            while ((count = this.Content.Read(buffer, 0, bufferSize)) > 0)
            {
                stream.Write(buffer, 0, count);
            }
        }
    }

    internal class ArraySegmentMultipartItem : FileMultipartItem
    {
        public ArraySegment<byte> Content { get; }

        public ArraySegmentMultipartItem(string key, ArraySegment<byte> content)
            : base(key)
        {
            this.Content = content;
        }

        public override long? Length => this.Content.Count;

        protected override void WriteContent(Stream stream)
        {
            stream.Write(this.Content.Array, this.Content.Offset, this.Content.Count);
        }
    }

    internal class ByteArrayMultipartItem : FileMultipartItem
    {
        public byte[] Content { get; }

        public ByteArrayMultipartItem(string key, byte[] content)
            : base(key)
        {
            this.Content = content;
        }

        public override long? Length => this.Content.Length;

        protected override void WriteContent(Stream stream)
        {
            stream.Write(this.Content, 0, this.Content.Length);
        }
    }

    internal class ByteEnumerableMultipartItem : FileMultipartItem
    {
        public IEnumerable<byte> Content { get; }

        public ByteEnumerableMultipartItem(string key, IEnumerable<byte> content)
            : base(key)
        {
            this.Content = content;
        }

        public override long? Length => (this.Content as ICollection<byte>)?.Count;

        protected override void WriteContent(Stream stream)
        {
            const int bufferSize = 81920;
            var buffer = new byte[bufferSize];
            var i = 0;
            foreach (var b in this.Content)
            {
                buffer[i++] = b;
                if (i == bufferSize)
                {
                    stream.Write(buffer, 0, bufferSize);
                    i = 0;
                }
            }
            if (i > 0)
            {
                stream.Write(buffer, 0, i);
            }
        }
    }

#if FILEINFO
    internal class FileInfoMultipartItem : StreamMultipartItem
    {
        private readonly long _length;

        public FileInfoMultipartItem(string key, FileInfo content)
            : base(key, content.OpenRead())
        {
            this._length = content.Length;
        }

        public override long? Length => this._length;

        protected override void WriteContent(Stream stream)
        {
            try
            {
                base.WriteContent(stream);
            }
            finally
            {
                this.Content.Close();
            }
        }
    }
#endif
}
#endif
