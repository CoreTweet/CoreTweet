﻿// The MIT License (MIT)
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

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Net;
using CoreTweet.Core;

namespace CoreTweet.Rest
{
    /// <summary>
    /// Provides a set of methods for the wrapper of POST media.
    /// </summary>
    public partial class Media : ApiProviderBase
    {
        internal static string GetMediaTypeString(UploadMediaType mediaType)
        {
            return mediaType == UploadMediaType.Video ? "video/mp4" : "application/octet-stream";
        }

#if !(PCL || WIN_RT || WP)
        //POST methods

        internal HttpWebResponse AccessUploadApi(IEnumerable<KeyValuePair<string, object>> parameters)
        {
            var options = Tokens.ConnectionOptions ?? new ConnectionOptions();
            return this.Tokens.SendRequestImpl(MethodType.Post, InternalUtils.GetUrl(options, options.UploadUrl, true, "media/upload.json"), parameters);
        }

        private MediaUploadResult UploadImpl(IEnumerable<KeyValuePair<string, object>> parameters)
        {
            using (var res = this.AccessUploadApi(parameters))
                return InternalUtils.ReadResponse<MediaUploadResult>(res, "");
        }

        private HttpWebResponse Command(string command, IEnumerable<KeyValuePair<string, object>> parameters)
        {
            return this.AccessUploadApi(parameters.EndWith(new KeyValuePair<string, object>("command", command)));
        }

        private T Command<T>(string command, IEnumerable<KeyValuePair<string, object>> parameters)
        {
            using (var res = this.Command(command, parameters))
                return InternalUtils.ReadResponse<T>(res, "");
        }

        private UploadInitCommandResult UploadInitCommandImpl(IEnumerable<KeyValuePair<string, object>> parameters)
        {
            return this.Command<UploadInitCommandResult>("INIT", parameters);
        }

        private void UploadAppendCommandImpl(IEnumerable<KeyValuePair<string, object>> parameters)
        {
            this.Command("APPEND", parameters).Close();
        }

        private MediaUploadResult UploadFinalizeCommandImpl(IEnumerable<KeyValuePair<string, object>> parameters)
        {
            return this.Command<MediaUploadResult>("FINALIZE", parameters);
        }

        private MediaUploadResult UploadChunkedImpl(Stream media, long totalBytes, UploadMediaType mediaType, IEnumerable<KeyValuePair<string, object>> parameters)
        {
            var mediaId = this.UploadInitCommandImpl(
                parameters.EndWith(
                    new KeyValuePair<string, object>("total_bytes", totalBytes),
                    new KeyValuePair<string, object>("media_type", mediaType)
                ))
                .MediaId;

            const int maxChunkSize = 5 * 1000 * 1000;
            var chunk = new byte[maxChunkSize];
            var remainingBytes = totalBytes;
            for(var segmentIndex = 0; remainingBytes > 0; segmentIndex++)
            {
                var readCount = media.Read(chunk, 0, (int)Math.Min(remainingBytes, maxChunkSize));
                if(readCount == 0) break;
                this.UploadAppendCommand(new Dictionary<string, object>
                {
                    { "media_id", mediaId },
                    { "segment_index", segmentIndex },
                    { "media", new ArraySegment<byte>(chunk, 0, readCount) }
                });
                remainingBytes -= readCount;
            }

            return this.UploadFinalizeCommand(mediaId);
        }

        /// <summary>
        /// <para>Uploads videos or chunked images to Twitter for use in a Tweet or Twitter-hosted Card.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>IEnumerbale&lt;long&gt;</c> additional_owners (optional)</para>
        /// </summary>
        /// <param name="media">The raw binary file content being uploaded.</param>
        /// <param name="totalBytes">The size of the media being uploaded in bytes.</param>
        /// <param name="mediaType">The type of the media being uploaded.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The result for the uploaded media.</returns>
        public MediaUploadResult UploadChunked(Stream media, long totalBytes, UploadMediaType mediaType, params Expression<Func<string, object>>[] parameters)
        {
            return this.UploadChunkedImpl(media, totalBytes, mediaType, InternalUtils.ExpressionsToDictionary(parameters));
        }

        /// <summary>
        /// <para>Uploads videos or chunked images to Twitter for use in a Tweet or Twitter-hosted Card.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>IEnumerbale&lt;long&gt;</c> additional_owners (optional)</para>
        /// </summary>
        /// <param name="media">The raw binary file content being uploaded.</param>
        /// <param name="totalBytes">The size of the media being uploaded in bytes.</param>
        /// <param name="mediaType">The type of the media being uploaded.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The result for the uploaded media.</returns>
        public MediaUploadResult UploadChunked(Stream media, long totalBytes, UploadMediaType mediaType, IDictionary<string, object> parameters)
        {
            return this.UploadChunkedImpl(media, totalBytes, mediaType, parameters);
        }

        /// <summary>
        /// <para>Uploads videos or chunked images to Twitter for use in a Tweet or Twitter-hosted Card.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>IEnumerbale&lt;long&gt;</c> additional_owners (optional)</para>
        /// </summary>
        /// <param name="media">The raw binary file content being uploaded.</param>
        /// <param name="totalBytes">The size of the media being uploaded in bytes.</param>
        /// <param name="mediaType">The type of the media being uploaded.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The result for the uploaded media.</returns>
        public MediaUploadResult UploadChunked(Stream media, long totalBytes, UploadMediaType mediaType, object parameters)
        {
            return this.UploadChunkedImpl(media, totalBytes, mediaType, InternalUtils.ResolveObject(parameters));
        }

        /// <summary>
        /// Uploads videos or chunked images to Twitter for use in a Tweet or Twitter-hosted Card.
        /// </summary>
        /// <param name="media">The raw binary file content being uploaded.</param>
        /// <param name="totalBytes">The size of the media being uploaded in bytes.</param>
        /// <param name="mediaType">The type of the media being uploaded.</param>
        /// <param name="additional_owners">A comma-separated string of user IDs to set as additional owners who are allowed to use the returned media_id in Tweets or Cards.</param>
        /// <returns>The result for the uploaded media.</returns>
        public MediaUploadResult UploadChunked(Stream media, long totalBytes, UploadMediaType mediaType, IEnumerable<long> additional_owners = null)
        {
            var parameters = new Dictionary<string, object>();
            if (additional_owners != null) parameters.Add(nameof(additional_owners), additional_owners);
            return this.UploadChunkedImpl(media, totalBytes, mediaType, parameters);
        }

        /// <summary>
        /// <para>Uploads videos or chunked images to Twitter for use in a Tweet or Twitter-hosted Card.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>IEnumerbale&lt;long&gt;</c> additional_owners (optional)</para>
        /// </summary>
        /// <param name="media">The raw binary file content being uploaded.</param>
        /// <param name="mediaType">The type of the media being uploaded.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The result for the uploaded media.</returns>
        public MediaUploadResult UploadChunked(Stream media, UploadMediaType mediaType, params Expression<Func<string, object>>[] parameters)
        {
            return this.UploadChunked(media, media.Length, mediaType, parameters);
        }

        /// <summary>
        /// <para>Uploads videos or chunked images to Twitter for use in a Tweet or Twitter-hosted Card.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>IEnumerbale&lt;long&gt;</c> additional_owners (optional)</para>
        /// </summary>
        /// <param name="media">The raw binary file content being uploaded.</param>
        /// <param name="mediaType">The type of the media being uploaded.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The result for the uploaded media.</returns>
        public MediaUploadResult UploadChunked(Stream media, UploadMediaType mediaType, IDictionary<string, object> parameters)
        {
            return this.UploadChunked(media, media.Length, mediaType, parameters);
        }

        /// <summary>
        /// <para>Uploads videos or chunked images to Twitter for use in a Tweet or Twitter-hosted Card.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>IEnumerbale&lt;long&gt;</c> additional_owners (optional)</para>
        /// </summary>
        /// <param name="media">The raw binary file content being uploaded.</param>
        /// <param name="mediaType">The type of the media being uploaded.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The result for the uploaded media.</returns>
        public MediaUploadResult UploadChunked(Stream media, UploadMediaType mediaType, object parameters)
        {
            return this.UploadChunked(media, media.Length, mediaType, parameters);
        }

        /// <summary>
        /// Uploads videos or chunked images to Twitter for use in a Tweet or Twitter-hosted Card.
        /// </summary>
        /// <param name="media">The raw binary file content being uploaded.</param>
        /// <param name="mediaType">The type of the media being uploaded.</param>
        /// <param name="additional_owners">A comma-separated string of user IDs to set as additional owners who are allowed to use the returned media_id in Tweets or Cards.</param>
        /// <returns>The result for the uploaded media.</returns>
        public MediaUploadResult UploadChunked(Stream media, UploadMediaType mediaType, IEnumerable<long> additional_owners = null)
        {
            return this.UploadChunked(media, media.Length, mediaType, additional_owners);
        }
#endif
    }
}
