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
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using CoreTweet.Core;

namespace CoreTweet.Rest
{
    partial class Media
    {
        //POST methods

        private Task<MediaUploadResult> UploadAsyncImpl(IEnumerable<KeyValuePair<string, object>> parameters, CancellationToken cancellationToken)
        {
            return this.Tokens.SendRequestAsyncImpl(MethodType.Post, string.Format("https://upload.twitter.com/{0}/media/upload.json", Property.ApiVersion), parameters, cancellationToken)
                .ContinueWith(
                    t => InternalUtils.ReadResponse(t, s => CoreBase.Convert<MediaUploadResult>(this.Tokens, s), cancellationToken),
                    cancellationToken
                )
                .Unwrap()
                .CheckCanceled(cancellationToken);
        }
        //TODO: Write documents
        public Task<MediaUploadResult> UploadAsync(params Expression<Func<string, object>>[] parameters)
        {
            return this.UploadAsyncImpl(InternalUtils.ExpressionsToDictionary(parameters), CancellationToken.None);
        }
        public Task<MediaUploadResult> UploadAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.UploadAsyncImpl(parameters, cancellationToken);
        }
        public Task<MediaUploadResult> UploadAsync<T>(T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.UploadAsyncImpl(InternalUtils.ResolveObject(parameters), cancellationToken);
        }
    }
}
