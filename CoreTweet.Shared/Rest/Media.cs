// The MIT License (MIT)
//
// CoreTweet - A .NET Twitter Library supporting Twitter API 1.1
// Copyright (c) 2013-2018 CoreTweet Development Team
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
using System.Threading;
using CoreTweet.Core;

namespace CoreTweet.Rest
{
    public partial class Media
    {
        /// <summary>
        /// Gets the wrapper of media/metadata.
        /// </summary>
        public MediaMetadata Metadata => new MediaMetadata(this.Tokens);

        internal static string GetMediaTypeString(UploadMediaType mediaType)
        {
            return mediaType == UploadMediaType.Video ? "video/mp4" : "application/octet-stream";
        }

#if SYNC
        //POST methods

        internal HttpWebResponse AccessUploadApi(IEnumerable<KeyValuePair<string, object>> parameters, string urlPrefix, string urlSuffix)
        {
            var options = Tokens.ConnectionOptions ?? ConnectionOptions.Default;

            if (urlPrefix != null || urlSuffix != null)
            {
                options = options.Clone();

                if (urlPrefix != null)
                {
                    options.UrlPrefix = urlPrefix;
                }

                if (urlSuffix != null)
                {
                    options.UrlSuffix = urlSuffix;
                }
            }

            return this.Tokens.SendRequestImpl(MethodType.Post, InternalUtils.GetUrl(options, options.UploadUrl, true, "media/upload.json"), parameters);
        }

        private MediaUploadResult UploadImpl(IEnumerable<KeyValuePair<string, object>> parameters, string urlPrefix, string urlSuffix)
        {
            using (var res = this.AccessUploadApi(parameters, urlPrefix, urlSuffix))
                return InternalUtils.ReadResponse<MediaUploadResult>(res, "");
        }

        private HttpWebResponse Command(string command, IEnumerable<KeyValuePair<string, object>> parameters, string urlPrefix, string urlSuffix)
        {
            return this.AccessUploadApi(parameters.EndWith(new KeyValuePair<string, object>("command", command)), urlPrefix, urlSuffix);
        }

        private T Command<T>(string command, IEnumerable<KeyValuePair<string, object>> parameters, string urlPrefix, string urlSuffix)
        {
            using (var res = this.Command(command, parameters, urlPrefix, urlSuffix))
                return InternalUtils.ReadResponse<T>(res, "");
        }

        private UploadInitCommandResult UploadInitCommandImpl(IEnumerable<KeyValuePair<string, object>> parameters, string urlPrefix, string urlSuffix)
        {
            return this.Command<UploadInitCommandResult>("INIT", parameters, urlPrefix, urlSuffix);
        }

        private void UploadAppendCommandImpl(IEnumerable<KeyValuePair<string, object>> parameters, string urlPrefix, string urlSuffix)
        {
            this.Command("APPEND", parameters, urlPrefix, urlSuffix).Close();
        }

        private UploadFinalizeCommandResult UploadFinalizeCommandImpl(IEnumerable<KeyValuePair<string, object>> parameters, string urlPrefix, string urlSuffix)
        {
            return this.Command<UploadFinalizeCommandResult>("FINALIZE", parameters, urlPrefix, urlSuffix);
        }

        private UploadFinalizeCommandResult UploadStatusCommandImpl(IEnumerable<KeyValuePair<string, object>> parameters, string urlPrefix, string urlSuffix)
        {
            var options = Tokens.ConnectionOptions ?? ConnectionOptions.Default;

            if (urlPrefix != null || urlSuffix != null)
            {
                options = options.Clone();

                if (urlPrefix != null)
                {
                    options.UrlPrefix = urlPrefix;
                }

                if (urlSuffix != null)
                {
                    options.UrlSuffix = urlSuffix;
                }
            }

            var res = this.Tokens.SendRequestImpl(MethodType.Get, InternalUtils.GetUrl(options, options.UploadUrl, true, "media/upload.json"),
                parameters.EndWith(new KeyValuePair<string, object>("command", "STATUS")));
            using (res)
                return InternalUtils.ReadResponse<UploadFinalizeCommandResult>(res, "");
        }

        private MediaUploadResult UploadChunkedImpl(Stream media, long totalBytes, UploadMediaType mediaType, IEnumerable<KeyValuePair<string, object>> parameters, string urlPrefix, string urlSuffix)
        {
            var mediaId = this.UploadInitCommandImpl(
                parameters.EndWith(
                    new KeyValuePair<string, object>("total_bytes", totalBytes),
                    new KeyValuePair<string, object>("media_type", mediaType)
                ), urlPrefix, urlSuffix)
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

            var result = this.UploadFinalizeCommand(mediaId);
            while (result.ProcessingInfo?.CheckAfterSecs != null)
            {
                Thread.Sleep(result.ProcessingInfo.CheckAfterSecs.Value * 1000);
                result = this.UploadStatusCommand(mediaId);
            }

            if (result.ProcessingInfo?.State == "failed")
                throw new MediaProcessingException(result);

            return result;
        }

        /// <summary>
        /// <para>Uploads videos or chunked images to Twitter for use in a Tweet or Twitter-hosted Card.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> media_category (optional)</para>
        /// <para>- <c>IEnumerbale&lt;long&gt;</c> additional_owners (optional)</para>
        /// </summary>
        /// <param name="media">The raw binary file content being uploaded.</param>
        /// <param name="totalBytes">The size of the media being uploaded in bytes.</param>
        /// <param name="mediaType">The type of the media being uploaded.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The result for the uploaded media.</returns>
        public MediaUploadResult UploadChunked(Stream media, long totalBytes, UploadMediaType mediaType, params Expression<Func<string, object>>[] parameters)
        {
            return this.UploadChunkedImpl(media, totalBytes, mediaType, InternalUtils.ExpressionsToDictionary(parameters), null, null);
        }

        /// <summary>
        /// <para>Uploads videos or chunked images to Twitter for use in a Tweet or Twitter-hosted Card.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> media_category (optional)</para>
        /// <para>- <c>IEnumerbale&lt;long&gt;</c> additional_owners (optional)</para>
        /// </summary>
        /// <param name="media">The raw binary file content being uploaded.</param>
        /// <param name="totalBytes">The size of the media being uploaded in bytes.</param>
        /// <param name="mediaType">The type of the media being uploaded.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The result for the uploaded media.</returns>
        public MediaUploadResult UploadChunked(Stream media, long totalBytes, UploadMediaType mediaType, IDictionary<string, object> parameters)
        {
            return this.UploadChunkedImpl(media, totalBytes, mediaType, parameters, null, null);
        }

        /// <summary>
        /// <para>Uploads videos or chunked images to Twitter for use in a Tweet or Twitter-hosted Card.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> media_category (optional)</para>
        /// <para>- <c>IEnumerbale&lt;long&gt;</c> additional_owners (optional)</para>
        /// </summary>
        /// <param name="media">The raw binary file content being uploaded.</param>
        /// <param name="totalBytes">The size of the media being uploaded in bytes.</param>
        /// <param name="mediaType">The type of the media being uploaded.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The result for the uploaded media.</returns>
        public MediaUploadResult UploadChunked(Stream media, long totalBytes, UploadMediaType mediaType, object parameters)
        {
            return this.UploadChunkedImpl(media, totalBytes, mediaType, InternalUtils.ResolveObject(parameters), null, null);
        }

        /// <summary>
        /// Uploads videos or chunked images to Twitter for use in a Tweet or Twitter-hosted Card.
        /// </summary>
        /// <param name="media">The raw binary file content being uploaded.</param>
        /// <param name="totalBytes">The size of the media being uploaded in bytes.</param>
        /// <param name="mediaType">The type of the media being uploaded.</param>
        /// <param name="media_category">A string enum value which identifies a media usecase.</param>
        /// <param name="additional_owners">A comma-separated string of user IDs to set as additional owners who are allowed to use the returned media_id in Tweets or Cards.</param>
        /// <returns>The result for the uploaded media.</returns>
        public MediaUploadResult UploadChunked(Stream media, long totalBytes, UploadMediaType mediaType, string media_category = null, IEnumerable<long> additional_owners = null)
        {
            var parameters = new Dictionary<string, object>();
            if (media_category != null) parameters.Add(nameof(media_category), media_category);
            if (additional_owners != null) parameters.Add(nameof(additional_owners), additional_owners);
            return this.UploadChunkedImpl(media, totalBytes, mediaType, parameters, null, null);
        }

        /// <summary>
        /// <para>Uploads videos or chunked images to Twitter for use in a Tweet or Twitter-hosted Card.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> media_category (optional)</para>
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
        /// <para>- <c>string</c> media_category (optional)</para>
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
        /// <para>- <c>string</c> media_category (optional)</para>
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
        /// <param name="media_category">A string enum value which identifies a media usecase.</param>
        /// <param name="additional_owners">A comma-separated string of user IDs to set as additional owners who are allowed to use the returned media_id in Tweets or Cards.</param>
        /// <returns>The result for the uploaded media.</returns>
        public MediaUploadResult UploadChunked(Stream media, UploadMediaType mediaType, string media_category = null, IEnumerable<long> additional_owners = null)
        {
            return this.UploadChunked(media, media.Length, mediaType, media_category, additional_owners);
        }
#endif
    }
}
