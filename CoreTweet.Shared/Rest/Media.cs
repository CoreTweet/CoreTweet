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

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using CoreTweet.Core;
using Newtonsoft.Json.Linq;

namespace CoreTweet.Rest
{
    /// <summary>
    /// Provides a set of methods for the wrapper of POST media.
    /// </summary>
    public partial class Media : ApiProviderBase
    {
        internal Media(TokensBase e) : base(e) { }

        private static string GetMediaTypeString(UploadMediaType mediaType)
        {
            return mediaType == UploadMediaType.Video ? "video/mp4" : "application/octet-stream";
        }

#if !(PCL || WIN_RT || WP)
        //POST methods

        private HttpWebResponse AccessUploadApi(IEnumerable<KeyValuePair<string, object>> parameters)
        {
            return this.Tokens.SendRequestImpl(MethodType.Post, InternalUtils.GetUrl(Tokens.ConnectionOptions, Tokens.ConnectionOptions.UploadUrl, true, "media/upload.json"), parameters);
        }

        private MediaUploadResult UploadImpl(IEnumerable<KeyValuePair<string, object>> parameters)
        {
            using(var sr = new StreamReader(this.AccessUploadApi(parameters).GetResponseStream()))
            {
                var json = sr.ReadToEnd();
                var result = CoreBase.Convert<MediaUploadResult>(json);
                result.Json = json;
                return result;
            }
        }

        /// <summary>
        /// <para>Uploads an image and gets the media_id attached with a status.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>Stream</c> / <c>IEnumerable&lt;byte&gt;</c> / <c>FileInfo</c> media (required)</para>
        /// <para>- <c>string</c> media_data (required)</para>
        /// <para>- <c>IEnumerbale&lt;long&gt;</c> additional_owners (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The result for the uploaded media.</returns>
        public MediaUploadResult Upload(params Expression<Func<string, object>>[] parameters)
        {
            return this.UploadImpl(InternalUtils.ExpressionsToDictionary(parameters));
        }

        /// <summary>
        /// <para>Uploads an image and gets the media_id attached with a status.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>Stream</c> / <c>IEnumerable&lt;byte&gt;</c> / <c>FileInfo</c> media (required)</para>
        /// <para>- <c>string</c> media_data (required)</para>
        /// <para>- <c>IEnumerbale&lt;long&gt;</c> additional_owners (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The result for the uploaded media.</returns>
        public MediaUploadResult Upload(IDictionary<string, object> parameters)
        {
            return this.UploadImpl(parameters);
        }

        /// <summary>
        /// <para>Uploads an image and gets the media_id attached with a status.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>Stream</c> / <c>IEnumerable&lt;byte&gt;</c> / <c>FileInfo</c> media (required)</para>
        /// <para>- <c>string</c> media_data (required)</para>
        /// <para>- <c>IEnumerbale&lt;long&gt;</c> additional_owners (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The result for the uploaded media.</returns>
        public MediaUploadResult Upload<T>(T parameters)
        {
            return this.UploadImpl(InternalUtils.ResolveObject(parameters));
        }

        private MediaUploadResult UploadChunkedImpl(Stream media, int totalBytes, UploadMediaType mediaType, IEnumerable<KeyValuePair<string, object>> parameters)
        {
            string mediaId;
            using(var res = AccessUploadApi(new Dictionary<string, object>()
            {
                { "command", "INIT" },
                { "total_bytes", totalBytes },
                { "media_type", GetMediaTypeString(mediaType) }
            }.Concat(parameters)))
            using(var sr = new StreamReader(res.GetResponseStream()))
                mediaId = (string)JObject.Parse(sr.ReadToEnd())["media_id_string"];

            const int maxChunkSize = 5 * 1000 * 1000;
            byte[] chunk = null;
            var sentBytes = 0;
            for(var segmentIndex = 0; sentBytes <= totalBytes; segmentIndex++)
            {
                var rest = totalBytes - sentBytes;
                var chunkSize = rest < maxChunkSize ? rest : maxChunkSize;
                if(chunk == null || chunk.Length != chunkSize)
                    chunk = new byte[chunkSize];
                var readCount = media.Read(chunk, 0, chunkSize);
                if(readCount == 0) break;
                if(chunkSize != readCount)
                {
                    var newChunk = new byte[readCount];
                    Buffer.BlockCopy(chunk, 0, newChunk, 0, readCount);
                    chunk = newChunk;
                }
                this.AccessUploadApi(new Dictionary<string, object>()
                {
                    { "command", "APPEND" },
                    { "media_id", mediaId },
                    { "segment_index", segmentIndex },
                    { "media", chunk }
                }).Close();
                sentBytes += readCount;
            }

            using(var res = AccessUploadApi(new Dictionary<string, object>()
            {
                { "command", "FINALIZE" },
                { "media_id", mediaId }
            }))
            using(var sr = new StreamReader(res.GetResponseStream()))
            {
                var json = sr.ReadToEnd();
                var result = CoreBase.Convert<MediaUploadResult>(json);
                result.Json = json;
                return result;
            }
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
        public MediaUploadResult UploadChunked(Stream media, int totalBytes, UploadMediaType mediaType, params Expression<Func<string, object>>[] parameters)
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
        public MediaUploadResult UploadChunked(Stream media, int totalBytes, UploadMediaType mediaType, IDictionary<string, object> parameters)
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
        public MediaUploadResult UploadChunked<T>(Stream media, int totalBytes, UploadMediaType mediaType, T parameters)
        {
            return this.UploadChunkedImpl(media, totalBytes, mediaType, InternalUtils.ResolveObject(parameters));
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
            return this.UploadChunked(media, checked((int)media.Length), mediaType, parameters);
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
            return this.UploadChunked(media, checked((int)media.Length), mediaType, parameters);
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
        public MediaUploadResult UploadChunked<T>(Stream media, UploadMediaType mediaType, T parameters)
        {
            return this.UploadChunked(media, checked((int)media.Length), mediaType, parameters);
        }
#endif
    }
}
