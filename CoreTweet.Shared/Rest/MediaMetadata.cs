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

using System;
using System.Linq.Expressions;
using CoreTweet.Core;

namespace CoreTweet.Rest
{
    /// <summary>
    /// Provides a set of methods for the wrapper of POST media/metadata.
    /// </summary>
    public partial class MediaMetadata : ApiProviderBase
    {
        internal MediaMetadata(TokensBase e) : base(e) { }

#if SYNC
        // POST methods

        /// <summary>
        /// <para>Provides additional information about the uploaded media_id.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> media_id</para>
        /// <para>- JSON-Object alt_text</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        public void Create(object parameters)
        {
            var options = Tokens.ConnectionOptions ?? ConnectionOptions.Default;
            this.Tokens.PostContent(
                InternalUtils.GetUrl(options, options.UploadUrl, true, "media/metadata/create.json"),
                "application/json; charset=UTF-8",
                InternalUtils.ParametersToJson(parameters)
            ).Close();
        }

        /// <summary>
        /// <para>Provides additional information about the uploaded media_id.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> media_id</para>
        /// <para>- JSON-Object alt_text</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        public void Create(params Expression<Func<string, object>>[] parameters)
        {
            this.Create(InternalUtils.ExpressionsToDictionary(parameters));
        }
#endif
    }
}
