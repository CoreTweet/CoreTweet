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

#if ASYNC
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CoreTweet.Core;

namespace CoreTweet.Rest
{
    partial class MediaMetadata
    {
        private Task CreateAsyncImpl(IEnumerable<KeyValuePair<string, object>> parameters, string[] jsonMap, CancellationToken cancellationToken, string urlPrefix, string urlSuffix)
        {
            var options = this.Tokens.ConnectionOptions ?? ConnectionOptions.Default;

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

            return this.Tokens
                .SendJsonRequestAsync(InternalUtils.GetUrl(options, options.UploadUrl, true, "media/metadata/create.json"), parameters, jsonMap, cancellationToken)
                .Done(res => res.Dispose(), CancellationToken.None);
        }
    }
}
#endif
