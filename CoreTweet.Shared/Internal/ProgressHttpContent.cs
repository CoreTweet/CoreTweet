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

#if ASYNC
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace CoreTweet.Core
{
    internal sealed class ProgressHttpContent : HttpContent
    {
        private readonly HttpContent innerContent;
        private readonly long? totalBytesToSend;
        private readonly IProgress<UploadProgressInfo> progress;

        public ProgressHttpContent(HttpContent innerContent, long? totalBytesToSend, IProgress<UploadProgressInfo> progress)
        {
            this.innerContent = innerContent;
            this.totalBytesToSend = totalBytesToSend;
            this.progress = progress;

            this.Headers.Clear();
            foreach (var x in innerContent.Headers)
                this.Headers.TryAddWithoutValidation(x.Key, x.Value);

            this.Headers.ContentLength = totalBytesToSend;
        }

        protected override Task SerializeToStreamAsync(Stream stream, TransportContext context)
        {
            var ps = new ProgressStream(stream, x => this.progress.Report(new UploadProgressInfo(x, this.totalBytesToSend)));
            var task = this.innerContent.CopyToAsync(ps, context);

            if (!this.totalBytesToSend.HasValue)
                task = task.Done(
                    () => this.progress.Report(new UploadProgressInfo(ps.BytesWritten, ps.BytesWritten)),
                    CancellationToken.None);

            return task;
        }

        protected override bool TryComputeLength(out long length)
        {
            length = this.totalBytesToSend.GetValueOrDefault();
            return this.totalBytesToSend.HasValue;
        }

        private sealed class ProgressStream : Stream
        {
            private readonly Stream innerStream;
            private readonly Action<long> reportAction;
            public long BytesWritten { get; private set; }

            public ProgressStream(Stream innerStream, Action<long> report)
            {
                this.innerStream = innerStream;
                this.reportAction = report;
            }

            public override bool CanRead => this.innerStream.CanRead;
            public override bool CanSeek => this.innerStream.CanSeek;
            public override bool CanWrite => this.innerStream.CanWrite;
            public override long Length => this.innerStream.Length;
            public override long Position
            {
                get { return this.innerStream.Position; }
                set { this.innerStream.Position = value; }
            }
            public override void Flush() => this.innerStream.Flush();
            public override Task FlushAsync(CancellationToken cancellationToken) => this.innerStream.FlushAsync(cancellationToken);
            public override int Read(byte[] buffer, int offset, int count) => this.innerStream.Read(buffer, offset, count);
            public override long Seek(long offset, SeekOrigin origin) => this.innerStream.Seek(offset, origin);
            public override void SetLength(long value) => this.innerStream.SetLength(value);
            public override int ReadTimeout
            {
                get { return this.innerStream.ReadTimeout; }
                set { this.innerStream.ReadTimeout = value; }
            }
            public override int WriteTimeout
            {
                get { return this.innerStream.WriteTimeout; }
                set { this.innerStream.WriteTimeout = value; }
            }

            protected override void Dispose(bool disposing)
            {
                if (disposing)
                    this.innerStream.Dispose();
            }

            private void Report(int bytes)
            {
                this.BytesWritten += bytes;
                reportAction(this.BytesWritten);
            }

            public override void Write(byte[] buffer, int offset, int count)
            {
                this.innerStream.Write(buffer, offset, count);
                this.Report(count);
            }

            public override void WriteByte(byte value)
            {
                this.innerStream.WriteByte(value);
                this.Report(1);
            }

#if !NETCORE
            private sealed class ProgressStreamAsyncResult : IAsyncResult
            {
                public IAsyncResult InnerAsyncResult { get; set; }
                public int ByteCount { get; set; }

                public object AsyncState => this.InnerAsyncResult.AsyncState;
                public WaitHandle AsyncWaitHandle => this.InnerAsyncResult.AsyncWaitHandle;
                public bool CompletedSynchronously => this.InnerAsyncResult.CompletedSynchronously;
                public bool IsCompleted => this.InnerAsyncResult.IsCompleted;
            }

            public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
            {
                var ar = new ProgressStreamAsyncResult() { ByteCount = count };
                ar.InnerAsyncResult = this.innerStream.BeginWrite(buffer, offset, count, x =>
                {
                    ar.InnerAsyncResult = x;
                    callback(ar);
                }, state);
                return ar;
            }

            public override void EndWrite(IAsyncResult asyncResult)
            {
                var ar = asyncResult as ProgressStreamAsyncResult;
                if (ar == null) throw new ArgumentException();

                this.innerStream.EndWrite(ar.InnerAsyncResult);
                this.Report(ar.ByteCount);
            }
#endif

            public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
            {
                return this.innerStream.WriteAsync(buffer, offset, count, cancellationToken)
                    .Done(() => this.Report(count), CancellationToken.None);
                // Specify CancellationToken.None to be sure to run Report.
            }
        }
    }
}
#endif
