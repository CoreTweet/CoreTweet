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
using System.Linq.Expressions;
using CoreTweet.Core;

namespace CoreTweet.Rest
{
    /// <summary>
    /// Provides a set of methods for the wrapper of POST media.
    /// </summary>
    public partial class Media : ApiProviderBase
    {
        internal Media(TokensBase e) : base(e) { }

#if !(PCL || WIN_RT || WP)
        //POST methods

        private MediaUploadResult UploadImpl(IEnumerable<KeyValuePair<string, object>> parameters)
        {
            using(var sr = new StreamReader(this.Tokens.SendRequestImpl(
                MethodType.Post, string.Format("https://upload.twitter.com/{0}/media/upload.json", Property.ApiVersion), parameters)
                .GetResponseStream()))
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
        /// <para>- <c>Stream</c> / <c>IEnumerable&lt;byte&gt;</c> / FileInfo media (required)</para>
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
        /// <para>- <c>Stream</c> / <c>IEnumerable&lt;byte&gt;</c> / FileInfo media (required)</para>
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
        /// <para>- <c>Stream</c> / <c>IEnumerable&lt;byte&gt;</c> / FileInfo media (required)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The result for the uploaded media.</returns>
        public MediaUploadResult Upload<T>(T parameters)
        {
            return this.UploadImpl(InternalUtils.ResolveObject(parameters));
        }
#endif
    }
}
