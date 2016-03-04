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

#if !NET35
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using CoreTweet.Core;

namespace CoreTweet.Rest
{
    partial class Media
    {
        //POST methods

        internal Task<AsyncResponse> AccessUploadApiAsync(IEnumerable<KeyValuePair<string, object>> parameters, CancellationToken cancellationToken
#if !(NET40 || PCL)
            , IProgress<UploadProgressInfo> progress = null
#endif
        )
        {
            var options = Tokens.ConnectionOptions ?? new ConnectionOptions();
            return this.Tokens.SendRequestAsyncImpl(MethodType.Post, InternalUtils.GetUrl(options, options.UploadUrl, true, "media/upload.json"), parameters, cancellationToken
#if !(NET40 || PCL)
                , progress
#endif
            );
        }

        private Task<MediaUploadResult> UploadAsyncImpl(IEnumerable<KeyValuePair<string, object>> parameters, CancellationToken cancellationToken
#if !(NET40 || PCL)
            , IProgress<UploadProgressInfo> progress = null
#endif
        )
        {
            return this.AccessUploadApiAsync(parameters, cancellationToken
#if !(NET40 || PCL)
                , progress
#endif
            ).ReadResponse(s => CoreBase.Convert<MediaUploadResult>(s), cancellationToken);
        }

        #region UploadAsync with progress parameter
#if !(NET40 || PCL)
        /// <summary>
        /// <para>Upload media (images) to Twitter for use in a Tweet or Twitter-hosted Card.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>Stream</c> media (any one is required)</para>
        /// <para>- <c>IEnumerable&lt;byte&gt;</c> media (any one is required)</para>
        /// <para>- <c>FileInfo</c> media (any one is required)</para>
        /// <para>- <c>string</c> media_data (any one is required)</para>
        /// <para>- <c>IEnumerable&lt;long&gt;</c> additional_owners (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The result for the uploaded media.</returns>
        public Task<MediaUploadResult> UploadAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken), IProgress<UploadProgressInfo> progress = null)
        {
            return this.UploadAsyncImpl(parameters, cancellationToken, progress);
        }

        /// <summary>
        /// <para>Upload media (images) to Twitter for use in a Tweet or Twitter-hosted Card.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>Stream</c> media (any one is required)</para>
        /// <para>- <c>IEnumerable&lt;byte&gt;</c> media (any one is required)</para>
        /// <para>- <c>FileInfo</c> media (any one is required)</para>
        /// <para>- <c>string</c> media_data (any one is required)</para>
        /// <para>- <c>IEnumerable&lt;long&gt;</c> additional_owners (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The result for the uploaded media.</returns>
        public Task<MediaUploadResult> UploadAsync(object parameters, CancellationToken cancellationToken = default(CancellationToken), IProgress<UploadProgressInfo> progress = null)
        {
            return this.UploadAsyncImpl(InternalUtils.ResolveObject(parameters), cancellationToken, progress);
        }

        /// <summary>
        /// <para>Upload media (images) to Twitter for use in a Tweet or Twitter-hosted Card.</para>
        /// </summary>
        /// <param name="media">any one is required.</param>
        /// <param name="additional_owners">optional.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The result for the uploaded media.</returns>
        public Task<MediaUploadResult> UploadAsync(Stream media, IEnumerable<long> additional_owners = null, CancellationToken cancellationToken = default(CancellationToken), IProgress<UploadProgressInfo> progress = null)
        {
            var parameters = new Dictionary<string, object>();
            if (media == null) throw new ArgumentNullException(nameof(media));
            parameters.Add("media", media);
            if (additional_owners != null) parameters.Add("additional_owners", additional_owners);
            return this.UploadAsyncImpl(parameters, cancellationToken, progress);
        }

        /// <summary>
        /// <para>Upload media (images) to Twitter for use in a Tweet or Twitter-hosted Card.</para>
        /// </summary>
        /// <param name="media">any one is required.</param>
        /// <param name="additional_owners">optional.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The result for the uploaded media.</returns>
        public Task<MediaUploadResult> UploadAsync(IEnumerable<byte> media, IEnumerable<long> additional_owners = null, CancellationToken cancellationToken = default(CancellationToken), IProgress<UploadProgressInfo> progress = null)
        {
            var parameters = new Dictionary<string, object>();
            if (media == null) throw new ArgumentNullException(nameof(media));
            parameters.Add("media", media);
            if (additional_owners != null) parameters.Add("additional_owners", additional_owners);
            return this.UploadAsyncImpl(parameters, cancellationToken, progress);
        }

#if !WIN_RT
        /// <summary>
        /// <para>Upload media (images) to Twitter for use in a Tweet or Twitter-hosted Card.</para>
        /// </summary>
        /// <param name="media">any one is required.</param>
        /// <param name="additional_owners">optional.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The result for the uploaded media.</returns>
        public Task<MediaUploadResult> UploadAsync(FileInfo media, IEnumerable<long> additional_owners = null, CancellationToken cancellationToken = default(CancellationToken), IProgress<UploadProgressInfo> progress = null)
        {
            var parameters = new Dictionary<string, object>();
            if (media == null) throw new ArgumentNullException(nameof(media));
            parameters.Add("media", media);
            if (additional_owners != null) parameters.Add("additional_owners", additional_owners);
            return this.UploadAsyncImpl(parameters, cancellationToken, progress);
        }
#endif
#endif
        #endregion

        private Task<AsyncResponse> CommandAsync(string command, IEnumerable<KeyValuePair<string, object>> parameters, CancellationToken cancellationToken
#if !(NET40 || PCL)
            , IProgress<UploadProgressInfo> progress = null
#endif
        )
        {
            return this.AccessUploadApiAsync(parameters.EndWith(new KeyValuePair<string, object>("command", command)), cancellationToken
#if !(NET40 || PCL)
                , progress
#endif
            );
        }

        private Task<T> CommandAsync<T>(string command, IEnumerable<KeyValuePair<string, object>> parameters, CancellationToken cancellationToken)
        {
            return this.CommandAsync(command, parameters, cancellationToken)
                .ReadResponse(s => CoreBase.Convert<T>(s), cancellationToken);
        }

        private Task<UploadInitCommandResult> UploadInitCommandAsyncImpl(IEnumerable<KeyValuePair<string, object>> parameters, CancellationToken cancellationToken)
        {
            return this.CommandAsync<UploadInitCommandResult>("INIT", parameters, cancellationToken);
        }

        private Task UploadAppendCommandAsyncImpl(IEnumerable<KeyValuePair<string, object>> parameters, CancellationToken cancellationToken
#if !(NET40 || PCL)
            , IProgress<UploadProgressInfo> progress = null
#endif
        )
        {
            return this.CommandAsync("APPEND", parameters, cancellationToken
#if !(NET40 || PCL)
                , progress
#endif
            ).Done(res => res.Dispose(), cancellationToken);
        }

        #region UploadAppendCommand with progress parameter
#if !(NET40 || PCL)
        /// <summary>
        /// <para>Upload(s) of chunked data.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> media_id (required)</para>
        /// <para>- <c>int</c> segment_index (required)</para>
        /// <para>- <c>Stream</c> media (any one is required)</para>
        /// <para>- <c>IEnumerable&lt;byte&gt;</c> media (any one is required)</para>
        /// <para>- <c>FileInfo</c> media (any one is required)</para>
        /// <para>- <c>string</c> media_data (any one is required)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public Task UploadAppendCommandAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken), IProgress<UploadProgressInfo> progress = null)
        {
            return this.UploadAppendCommandAsyncImpl(parameters, cancellationToken, progress);
        }

        /// <summary>
        /// <para>Upload(s) of chunked data.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> media_id (required)</para>
        /// <para>- <c>int</c> segment_index (required)</para>
        /// <para>- <c>Stream</c> media (any one is required)</para>
        /// <para>- <c>IEnumerable&lt;byte&gt;</c> media (any one is required)</para>
        /// <para>- <c>FileInfo</c> media (any one is required)</para>
        /// <para>- <c>string</c> media_data (any one is required)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public Task UploadAppendCommandAsync(object parameters, CancellationToken cancellationToken = default(CancellationToken), IProgress<UploadProgressInfo> progress = null)
        {
            return this.UploadAppendCommandAsyncImpl(InternalUtils.ResolveObject(parameters), cancellationToken, progress);
        }

        /// <summary>
        /// <para>Upload(s) of chunked data.</para>
        /// </summary>
        /// <param name="media_id">required.</param>
        /// <param name="segment_index">required.</param>
        /// <param name="media">any one is required.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public Task UploadAppendCommandAsync(long media_id, int segment_index, Stream media, CancellationToken cancellationToken = default(CancellationToken), IProgress<UploadProgressInfo> progress = null)
        {
            var parameters = new Dictionary<string, object>();
            parameters.Add("media_id", media_id);
            parameters.Add("segment_index", segment_index);
            if (media == null) throw new ArgumentNullException(nameof(media));
            parameters.Add("media", media);
            return this.UploadAppendCommandAsyncImpl(parameters, cancellationToken, progress);
        }

        /// <summary>
        /// <para>Upload(s) of chunked data.</para>
        /// </summary>
        /// <param name="media_id">required.</param>
        /// <param name="segment_index">required.</param>
        /// <param name="media">any one is required.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public Task UploadAppendCommandAsync(long media_id, int segment_index, IEnumerable<byte> media, CancellationToken cancellationToken = default(CancellationToken), IProgress<UploadProgressInfo> progress = null)
        {
            var parameters = new Dictionary<string, object>();
            parameters.Add("media_id", media_id);
            parameters.Add("segment_index", segment_index);
            if (media == null) throw new ArgumentNullException(nameof(media));
            parameters.Add("media", media);
            return this.UploadAppendCommandAsyncImpl(parameters, cancellationToken, progress);
        }

#if !WIN_RT
        /// <summary>
        /// <para>Upload(s) of chunked data.</para>
        /// </summary>
        /// <param name="media_id">required.</param>
        /// <param name="segment_index">required.</param>
        /// <param name="media">any one is required.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public Task UploadAppendCommandAsync(long media_id, int segment_index, FileInfo media, CancellationToken cancellationToken = default(CancellationToken), IProgress<UploadProgressInfo> progress = null)
        {
            var parameters = new Dictionary<string, object>();
            parameters.Add("media_id", media_id);
            parameters.Add("segment_index", segment_index);
            if (media == null) throw new ArgumentNullException(nameof(media));
            parameters.Add("media", media);
            return this.UploadAppendCommandAsyncImpl(parameters, cancellationToken, progress);
        }
#endif
#endif
        #endregion

        private Task<MediaUploadResult> UploadFinalizeCommandAsyncImpl(IEnumerable<KeyValuePair<string, object>> parameters, CancellationToken cancellationToken)
        {
            return this.CommandAsync<MediaUploadResult>("FINALIZE", parameters, cancellationToken);
        }

        private static Task WhenAll(List<Task> tasks)
        {
#if NET40
            var tcs = new TaskCompletionSource<Unit>();
            var count = tasks.Count;
            var exceptions = new List<Exception>();
            foreach (var task in tasks)
            {
                task.ContinueWith(t =>
                {
                    var i = Interlocked.Decrement(ref count);
                    if (t.IsCanceled)
                        tcs.TrySetCanceled();
                    else
                    {
                        if (t.IsFaulted)
                            exceptions.AddRange(t.Exception.InnerExceptions);
                        if (i <= 0)
                        {
                            if (exceptions.Count > 0)
                                tcs.TrySetException(exceptions);
                            else
                                tcs.TrySetResult(Unit.Default);
                        }
                    }
                });
            }
            return tcs.Task;
#else
            return Task.WhenAll(tasks);
#endif
        }

#if !(NET40 || PCL)
        private class DeltaReporter : IProgress<UploadProgressInfo>
        {
            private readonly Action<long> _handler;
            private Action<long?> _totalHandler;
            private long prevBytesSent = 0;

            public DeltaReporter(Action<long> handler, Action<long?> totalHandler)
            {
                this._handler = handler;
                this._totalHandler = totalHandler;
            }

            public void Report(UploadProgressInfo value)
            {
                var h = this._totalHandler;
                if (h != null)
                {
                    this._totalHandler = null;
                    h(value.TotalBytesToSend);
                }

                var delta = value.BytesSent - this.prevBytesSent;
                if (delta > 0)
                {
                    this.prevBytesSent = value.BytesSent;
                    this._handler(delta);
                }
            }
        }
#endif

        private Task<MediaUploadResult> UploadChunkedAsyncImpl(Stream media, long totalBytes, UploadMediaType mediaType, IEnumerable<KeyValuePair<string, object>> parameters, CancellationToken cancellationToken
#if !(NET40 || PCL)
            , IProgress<UploadProgressInfo> progress = null
#endif
        )
        {
            return this.UploadInitCommandAsyncImpl(
                parameters.EndWith(
                    new KeyValuePair<string, object>("total_bytes", totalBytes),
                    new KeyValuePair<string, object>("media_type", mediaType)
                ), cancellationToken)
                .Done(result =>
                {
                    var tasks = new List<Task>(4);
                    const int maxChunkSize = 5 * 1000 * 1000;
                    var remainingBytes = totalBytes;
#if !(NET40 || PCL)
                    var lockObj = new object();
                    long? totalBytesToSend = 0;
                    long bytesSent = 0;
#endif

                    for (var segmentIndex = 0; remainingBytes > 0; segmentIndex++)
                    {
                        var chunkSize = (int)Math.Min(remainingBytes, maxChunkSize);
                        var chunk = new byte[chunkSize];
                        var readCount = media.Read(chunk, 0, chunkSize);
                        if (readCount == 0) break;
                        remainingBytes -= readCount;
#if !(NET40 || PCL)
                        var progressHandler = progress == null ? null
                            : new DeltaReporter(
                                x =>
                                {
                                    var b = Interlocked.Add(ref bytesSent, x);
                                    progress.Report(new UploadProgressInfo(b, totalBytesToSend));
                                },
                                x =>
                                {
                                    lock (lockObj)
                                        totalBytesToSend += x;
                                });
#endif
                        var task = this.UploadAppendCommandAsyncImpl(
                            new Dictionary<string, object>
                            {
                                { "media_id", result.MediaId },
                                { "segment_index", segmentIndex },
                                { "media", new ArraySegment<byte>(chunk, 0, readCount) }
                            }, cancellationToken
#if !(NET40 || PCL)
                            , progressHandler
#endif
                        );
                        tasks.Add(task);
                    }

                    return WhenAll(tasks)
                        .Done(() => this.UploadFinalizeCommandAsync(result.MediaId, cancellationToken), cancellationToken)
                        .Unwrap();
                }, cancellationToken)
                .Unwrap();
        }

        /// <summary>
        /// <para>Uploads videos or chunked images to Twitter for use in a Tweet or Twitter-hosted Card as an asynchronous operation.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>IEnumerbale&lt;long&gt;</c> additional_owners (optional)</para>
        /// </summary>
        /// <param name="media">The raw binary file content being uploaded.</param>
        /// <param name="totalBytes">The size of the media being uploaded in bytes.</param>
        /// <param name="mediaType">The type of the media being uploaded.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the result for the uploaded media.</para>
        /// </returns>
        public Task<MediaUploadResult> UploadChunkedAsync(Stream media, long totalBytes, UploadMediaType mediaType, params Expression<Func<string, object>>[] parameters)
        {
            return this.UploadChunkedAsyncImpl(media, totalBytes, mediaType, InternalUtils.ExpressionsToDictionary(parameters), CancellationToken.None);
        }

        /// <summary>
        /// <para>Uploads videos or chunked images to Twitter for use in a Tweet or Twitter-hosted Card as an asynchronous operation.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>IEnumerbale&lt;long&gt;</c> additional_owners (optional)</para>
        /// </summary>
        /// <param name="media">The raw binary file content being uploaded.</param>
        /// <param name="totalBytes">The size of the media being uploaded in bytes.</param>
        /// <param name="mediaType">The type of the media being uploaded.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the result for the uploaded media.</para>
        /// </returns>
        public Task<MediaUploadResult> UploadChunkedAsync(Stream media, long totalBytes, UploadMediaType mediaType, IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.UploadChunkedAsyncImpl(media, totalBytes, mediaType, parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Uploads videos or chunked images to Twitter for use in a Tweet or Twitter-hosted Card as an asynchronous operation.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>IEnumerbale&lt;long&gt;</c> additional_owners (optional)</para>
        /// </summary>
        /// <param name="media">The raw binary file content being uploaded.</param>
        /// <param name="totalBytes">The size of the media being uploaded in bytes.</param>
        /// <param name="mediaType">The type of the media being uploaded.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the result for the uploaded media.</para>
        /// </returns>
        public Task<MediaUploadResult> UploadChunkedAsync(Stream media, long totalBytes, UploadMediaType mediaType, object parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.UploadChunkedAsyncImpl(media, totalBytes, mediaType, InternalUtils.ResolveObject(parameters), cancellationToken);
        }

        /// <summary>
        /// Uploads videos or chunked images to Twitter for use in a Tweet or Twitter-hosted Card as an asynchronous operation.
        /// </summary>
        /// <param name="media">The raw binary file content being uploaded.</param>
        /// <param name="totalBytes">The size of the media being uploaded in bytes.</param>
        /// <param name="mediaType">The type of the media being uploaded.</param>
        /// <param name="additional_owners">A comma-separated string of user IDs to set as additional owners who are allowed to use the returned media_id in Tweets or Cards.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The result for the uploaded media.</returns>
        public Task<MediaUploadResult> UploadChunkedAsync(Stream media, long totalBytes, UploadMediaType mediaType, IEnumerable<long> additional_owners = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            var parameters = new Dictionary<string, object>();
            if (additional_owners != null) parameters.Add(nameof(additional_owners), additional_owners);
            return this.UploadChunkedAsyncImpl(media, totalBytes, mediaType, parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Uploads videos or chunked images to Twitter for use in a Tweet or Twitter-hosted Card as an asynchronous operation.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>IEnumerbale&lt;long&gt;</c> additional_owners (optional)</para>
        /// </summary>
        /// <param name="media">The raw binary file content being uploaded.</param>
        /// <param name="mediaType">The type of the media being uploaded.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the result for the uploaded media.</para>
        /// </returns>
        public Task<MediaUploadResult> UploadChunkedAsync(Stream media, UploadMediaType mediaType, params Expression<Func<string, object>>[] parameters)
        {
            return this.UploadChunkedAsync(media, media.Length, mediaType, parameters);
        }

        /// <summary>
        /// <para>Uploads videos or chunked images to Twitter for use in a Tweet or Twitter-hosted Card as an asynchronous operation.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>IEnumerbale&lt;long&gt;</c> additional_owners (optional)</para>
        /// </summary>
        /// <param name="media">The raw binary file content being uploaded.</param>
        /// <param name="mediaType">The type of the media being uploaded.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the result for the uploaded media.</para>
        /// </returns>
        public Task<MediaUploadResult> UploadChunkedAsync(Stream media, UploadMediaType mediaType, IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.UploadChunkedAsync(media, media.Length, mediaType, parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Uploads videos or chunked images to Twitter for use in a Tweet or Twitter-hosted Card as an asynchronous operation.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>IEnumerbale&lt;long&gt;</c> additional_owners (optional)</para>
        /// </summary>
        /// <param name="media">The raw binary file content being uploaded.</param>
        /// <param name="mediaType">The type of the media being uploaded.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the result for the uploaded media.</para>
        /// </returns>
        public Task<MediaUploadResult> UploadChunkedAsync(Stream media, UploadMediaType mediaType, object parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.UploadChunkedAsync(media, media.Length, mediaType, parameters, cancellationToken);
        }

        /// <summary>
        /// Uploads videos or chunked images to Twitter for use in a Tweet or Twitter-hosted Card as an asynchronous operation.
        /// </summary>
        /// <param name="media">The raw binary file content being uploaded.</param>
        /// <param name="mediaType">The type of the media being uploaded.</param>
        /// <param name="additional_owners">A comma-separated string of user IDs to set as additional owners who are allowed to use the returned media_id in Tweets or Cards.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The result for the uploaded media.</returns>
        public Task<MediaUploadResult> UploadChunkedAsync(Stream media, UploadMediaType mediaType, IEnumerable<long> additional_owners = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.UploadChunkedAsync(media, media.Length, mediaType, additional_owners, cancellationToken);
        }

#if !(NET40 || PCL)
        /// <summary>
        /// <para>Uploads videos or chunked images to Twitter for use in a Tweet or Twitter-hosted Card as an asynchronous operation.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>IEnumerbale&lt;long&gt;</c> additional_owners (optional)</para>
        /// </summary>
        /// <param name="media">The raw binary file content being uploaded.</param>
        /// <param name="totalBytes">The size of the media being uploaded in bytes.</param>
        /// <param name="mediaType">The type of the media being uploaded.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the result for the uploaded media.</para>
        /// </returns>
        public Task<MediaUploadResult> UploadChunkedAsync(Stream media, long totalBytes, UploadMediaType mediaType, IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken), IProgress<UploadProgressInfo> progress = null)
        {
            return this.UploadChunkedAsyncImpl(media, totalBytes, mediaType, parameters, cancellationToken, progress);
        }

        /// <summary>
        /// <para>Uploads videos or chunked images to Twitter for use in a Tweet or Twitter-hosted Card as an asynchronous operation.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>IEnumerbale&lt;long&gt;</c> additional_owners (optional)</para>
        /// </summary>
        /// <param name="media">The raw binary file content being uploaded.</param>
        /// <param name="totalBytes">The size of the media being uploaded in bytes.</param>
        /// <param name="mediaType">The type of the media being uploaded.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the result for the uploaded media.</para>
        /// </returns>
        public Task<MediaUploadResult> UploadChunkedAsync(Stream media, long totalBytes, UploadMediaType mediaType, object parameters, CancellationToken cancellationToken = default(CancellationToken), IProgress<UploadProgressInfo> progress = null)
        {
            return this.UploadChunkedAsyncImpl(media, totalBytes, mediaType, InternalUtils.ResolveObject(parameters), cancellationToken, progress);
        }

        /// <summary>
        /// Uploads videos or chunked images to Twitter for use in a Tweet or Twitter-hosted Card as an asynchronous operation.
        /// </summary>
        /// <param name="media">The raw binary file content being uploaded.</param>
        /// <param name="totalBytes">The size of the media being uploaded in bytes.</param>
        /// <param name="mediaType">The type of the media being uploaded.</param>
        /// <param name="additional_owners">A comma-separated string of user IDs to set as additional owners who are allowed to use the returned media_id in Tweets or Cards.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The result for the uploaded media.</returns>
        public Task<MediaUploadResult> UploadChunkedAsync(Stream media, long totalBytes, UploadMediaType mediaType, IEnumerable<long> additional_owners = null, CancellationToken cancellationToken = default(CancellationToken), IProgress<UploadProgressInfo> progress = null)
        {
            var parameters = new Dictionary<string, object>();
            if (additional_owners != null) parameters.Add(nameof(additional_owners), additional_owners);
            return this.UploadChunkedAsyncImpl(media, totalBytes, mediaType, parameters, cancellationToken, progress);
        }

        /// <summary>
        /// <para>Uploads videos or chunked images to Twitter for use in a Tweet or Twitter-hosted Card as an asynchronous operation.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>IEnumerbale&lt;long&gt;</c> additional_owners (optional)</para>
        /// </summary>
        /// <param name="media">The raw binary file content being uploaded.</param>
        /// <param name="mediaType">The type of the media being uploaded.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the result for the uploaded media.</para>
        /// </returns>
        public Task<MediaUploadResult> UploadChunkedAsync(Stream media, UploadMediaType mediaType, IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken), IProgress<UploadProgressInfo> progress = null)
        {
            return this.UploadChunkedAsync(media, media.Length, mediaType, parameters, cancellationToken, progress);
        }

        /// <summary>
        /// <para>Uploads videos or chunked images to Twitter for use in a Tweet or Twitter-hosted Card as an asynchronous operation.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>IEnumerbale&lt;long&gt;</c> additional_owners (optional)</para>
        /// </summary>
        /// <param name="media">The raw binary file content being uploaded.</param>
        /// <param name="mediaType">The type of the media being uploaded.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the result for the uploaded media.</para>
        /// </returns>
        public Task<MediaUploadResult> UploadChunkedAsync(Stream media, UploadMediaType mediaType, object parameters, CancellationToken cancellationToken = default(CancellationToken), IProgress<UploadProgressInfo> progress = null)
        {
            return this.UploadChunkedAsync(media, media.Length, mediaType, parameters, cancellationToken, progress);
        }

        /// <summary>
        /// Uploads videos or chunked images to Twitter for use in a Tweet or Twitter-hosted Card as an asynchronous operation.
        /// </summary>
        /// <param name="media">The raw binary file content being uploaded.</param>
        /// <param name="mediaType">The type of the media being uploaded.</param>
        /// <param name="additional_owners">A comma-separated string of user IDs to set as additional owners who are allowed to use the returned media_id in Tweets or Cards.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The result for the uploaded media.</returns>
        public Task<MediaUploadResult> UploadChunkedAsync(Stream media, UploadMediaType mediaType, IEnumerable<long> additional_owners = null, CancellationToken cancellationToken = default(CancellationToken), IProgress<UploadProgressInfo> progress = null)
        {
            return this.UploadChunkedAsync(media, media.Length, mediaType, additional_owners, cancellationToken, progress);
        }
#endif
    }
}
#endif
