// The MIT License (MIT)
//
// CoreTweet - A .NET Twitter Library supporting Twitter API 1.1
// Copyright (c) 2014 lambdalice
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
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using CoreTweet.Core;
using Newtonsoft.Json.Linq;

namespace CoreTweet.Rest
{
    partial class Media
    {
        //POST methods

        private Task<AsyncResponse> AccessUploadApiAsync(IEnumerable<KeyValuePair<string, object>> parameters, CancellationToken cancellationToken)
        {
            return this.Tokens.SendRequestAsyncImpl(MethodType.Post, InternalUtils.GetUrl(Tokens.ConnectionOptions, Tokens.ConnectionOptions.UploadUrl, true, "media/upload.json"), parameters, cancellationToken);
        }

        private Task<MediaUploadResult> UploadAsyncImpl(IEnumerable<KeyValuePair<string, object>> parameters, CancellationToken cancellationToken)
        {
            return this.AccessUploadApiAsync(parameters, cancellationToken)
                .ContinueWith(
                    t => InternalUtils.ReadResponse(t, s => CoreBase.Convert<MediaUploadResult>(s), cancellationToken),
                    cancellationToken
                )
                .Unwrap();
        }

        /// <summary>
        /// <para>Uploads an image and gets the media_id attached with a status as an asynchronous operation.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>Stream</c> / <c>IEnumerable&lt;byte&gt;</c> / <c>FileInfo</c> media (required)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the result for the uploaded media.</para>
        /// </returns>
        public Task<MediaUploadResult> UploadAsync(params Expression<Func<string, object>>[] parameters)
        {
            return this.UploadAsyncImpl(InternalUtils.ExpressionsToDictionary(parameters), CancellationToken.None);
        }

        /// <summary>
        /// <para>Uploads an image and gets the media_id attached with a status as an asynchronous operation.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>Stream</c> / <c>IEnumerable&lt;byte&gt;</c> / <c>FileInfo</c> media (required)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the result for the uploaded media.</para>
        /// </returns>
        public Task<MediaUploadResult> UploadAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.UploadAsyncImpl(parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Uploads an image and gets the media_id attached with a status as an asynchronous operation.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>Stream</c> / <c>IEnumerable&lt;byte&gt;</c> / <c>FileInfo</c> media (required)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the result for the uploaded media.</para>
        /// </returns>
        public Task<MediaUploadResult> UploadAsync<T>(T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.UploadAsyncImpl(InternalUtils.ResolveObject(parameters), cancellationToken);
        }

        private Task AppendData(string mediaId, Stream media, int remainingBytes, int segmentIndex, CancellationToken cancellationToken)
        {
            const int maxChunkSize = 5 * 1000 * 1000;
            var chunkSize = remainingBytes < maxChunkSize ? remainingBytes : maxChunkSize;
            var chunk = new byte[chunkSize];
            var readCount = media.Read(chunk, 0, chunkSize);
            if(readCount == 0) return InternalUtils.CompletedTask;
            if(chunkSize != readCount)
            {
                var newChunk = new byte[readCount];
                Buffer.BlockCopy(chunk, 0, newChunk, 0, readCount);
                chunk = newChunk;
            }
            return this.AccessUploadApiAsync(
                new Dictionary<string, object>()
                {
                    { "command", "APPEND" },
                    { "media_id", mediaId },
                    { "segment_index", segmentIndex },
                    { "media", chunk }
                }, cancellationToken)
                .ContinueWith(t =>
                {
                    if(t.IsFaulted)
                        t.Exception.InnerException.Rethrow();

                    t.Result.Dispose();

                    var rest = remainingBytes - readCount;
                    return rest > 0
                        ? this.AppendData(mediaId, media, rest, segmentIndex + 1, cancellationToken)
                        : InternalUtils.CompletedTask;
                }, cancellationToken)
                .Unwrap();
        }

        private Task<MediaUploadResult> UploadChunkedAsyncImpl(Stream media, int totalBytes, UploadMediaType mediaType, IEnumerable<KeyValuePair<string, object>> parameters, CancellationToken cancellationToken)
        {
            return this.AccessUploadApiAsync(
                new Dictionary<string, object>()
                {
                    { "command", "INIT" },
                    { "total_bytes", totalBytes },
                    { "media_type", GetMediaTypeString(mediaType) }
                }.Concat(parameters), cancellationToken)
                .ContinueWith(
                    t => InternalUtils.ReadResponse(t, s => (string)JObject.Parse(s)["media_id_string"], cancellationToken),
                    cancellationToken
                )
                .Unwrap()
                .ContinueWith(t =>
                {
                    if(t.IsFaulted)
                        t.Exception.InnerException.Rethrow();

                    var mediaId = t.Result;
                    return this.AppendData(mediaId, media, totalBytes, 0, cancellationToken)
                        .ContinueWith(x =>
                        {
                            if(x.IsFaulted)
                                x.Exception.InnerException.Rethrow();

                            return AccessUploadApiAsync(
                                new Dictionary<string, object>()
                                {
                                    { "command", "FINALIZE" },
                                    { "media_id", mediaId }
                                }, cancellationToken)
                                .ContinueWith(
                                    y => InternalUtils.ReadResponse(y, s => CoreBase.Convert<MediaUploadResult>(s), cancellationToken),
                                    cancellationToken
                                )
                                .Unwrap();
                        }, cancellationToken
                        )
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
        public Task<MediaUploadResult> UploadChunkedAsync(Stream media, int totalBytes, UploadMediaType mediaType, params Expression<Func<string, object>>[] parameters)
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
        public Task<MediaUploadResult> UploadChunkedAsync(Stream media, int totalBytes, UploadMediaType mediaType, IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
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
        public Task<MediaUploadResult> UploadChunkedAsync<T>(Stream media, int totalBytes, UploadMediaType mediaType, T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.UploadChunkedAsyncImpl(media, totalBytes, mediaType, InternalUtils.ResolveObject(parameters), cancellationToken);
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
            return this.UploadChunkedAsync(media, checked((int)media.Length), mediaType, parameters);
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
            return this.UploadChunkedAsync(media, checked((int)media.Length), mediaType, parameters, cancellationToken);
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
        public Task<MediaUploadResult> UploadChunkedAsync<T>(Stream media, UploadMediaType mediaType, T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.UploadChunkedAsync(media, checked((int)media.Length), mediaType, parameters, cancellationToken);
        }
    }
}
#endif
