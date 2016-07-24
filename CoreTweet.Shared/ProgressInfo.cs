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
namespace CoreTweet
{
    /// <summary>
    /// Contains status information on the progress of a POST request.
    /// </summary>
    public struct UploadProgressInfo
    {
        /// <summary>
        /// Gets the total number of bytes sent (not accurate).
        /// If the number is unknown, this value is <c>0</c>.
        /// </summary>
        public long BytesSent { get; }

        /// <summary>
        /// Gets the total number of data bytes to send (not accurate).
        /// If the number is unknown, this value is <c>null</c>.
        /// </summary>
        public long? TotalBytesToSend { get; }

        public UploadProgressInfo(long bytesSent, long? totalBytesToSend)
        {
            this.BytesSent = bytesSent;
            this.TotalBytesToSend = totalBytesToSend;
        }
    }

    /// <summary>
    /// Contains status information on the progress of <see cref="Rest.Media.UploadChunkedAsync(System.IO.Stream, UploadMediaType, object, System.Threading.CancellationToken, System.IProgress{UploadChunkedProgressInfo})"/>.
    /// </summary>
    public struct UploadChunkedProgressInfo
    {
        /// <summary>
        /// Gets or sets the step in the progress
        /// </summary>
        public UploadChunkedProgressStage Stage { get; set; }

        /// <summary>
        /// Gets or sets the total number of bytes sent.
        /// </summary>
        public long BytesSent { get; set; }

        /// <summary>
        /// Gets or sets the total number of data bytes to send.
        /// </summary>
        public long TotalBytesToSend { get; set; }

        public int ProcessingProgressPercent { get; set; }

        public UploadChunkedProgressInfo(UploadChunkedProgressStage stage, long bytesSent, long totalBytesToSend, int processingProgressPercent)
        {
            this.Stage = stage;
            this.BytesSent = bytesSent;
            this.TotalBytesToSend = totalBytesToSend;
            this.ProcessingProgressPercent = processingProgressPercent;
        }
    }

    /// <summary>
    /// Indicates the step in the progress for <see cref="Rest.Media.UploadChunkedAsync(System.IO.Stream, UploadMediaType, object, System.Threading.CancellationToken, System.IProgress{UploadChunkedProgressInfo})"/>.
    /// </summary>
    public enum UploadChunkedProgressStage
    {
        /// <summary>A default value that should not be encountered.</summary>
        None,
        /// <summary>Uploading the media.</summary>
        SendingContent,
        /// <summary>Waiting for Twitter to process the media.</summary>
        Pending,
        /// <summary>Twitter is processing the media.</summary>
        InProgress
    }
}
#endif
