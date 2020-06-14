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
    partial class Collections
    {
        private Task<CollectionsApiResult> AccessApiAsync(MethodType type, string apiName, IEnumerable<KeyValuePair<string, object>> parameters, CancellationToken cancellationToken, string urlPrefix, string urlSuffix)
        {
            return this.Tokens.AccessApiAsyncImpl<CollectionsApiResult>(type, "collections/" + apiName, parameters, cancellationToken, "", urlPrefix, urlSuffix);
        }

        private Task<CollectionsListResult> ListAsyncImpl(IEnumerable<KeyValuePair<string, object>> parameters, CancellationToken cancellationToken, string urlPrefix, string urlSuffix)
        {
            return this.AccessApiAsync(MethodType.Get, "list", parameters, cancellationToken, urlPrefix, urlSuffix)
                .Done(ToCollectionsListResult, cancellationToken);
        }

        private Task<TimelineResponse> ShowAsyncImpl(IEnumerable<KeyValuePair<string, object>> parameters, CancellationToken cancellationToken, string urlPrefix, string urlSuffix)
        {
            return this.AccessApiAsync(MethodType.Get, "show", parameters, cancellationToken, urlPrefix, urlSuffix)
                .Done(ToTimelineResponse, cancellationToken);
        }

        private Task<CollectionEntriesResult> EntriesAsyncImpl(IEnumerable<KeyValuePair<string, object>> parameters, CancellationToken cancellationToken, string urlPrefix, string urlSuffix)
        {
            return this.AccessApiAsync(MethodType.Get, "entries", parameters, cancellationToken, urlPrefix, urlSuffix)
                .Done(ToCollectionEntriesResult, cancellationToken);
        }

        private Task<TimelineResponse> CreateAsyncImpl(IEnumerable<KeyValuePair<string, object>> parameters, CancellationToken cancellationToken, string urlPrefix, string urlSuffix)
        {
            return this.AccessApiAsync(MethodType.Post, "create", parameters, cancellationToken, urlPrefix, urlSuffix)
                .Done(ToTimelineResponse, cancellationToken);
        }

        private Task<TimelineResponse> UpdateAsyncImpl(IEnumerable<KeyValuePair<string, object>> parameters, CancellationToken cancellationToken, string urlPrefix, string urlSuffix)
        {
            return this.AccessApiAsync(MethodType.Post, "update", parameters, cancellationToken, urlPrefix, urlSuffix)
                .Done(ToTimelineResponse, cancellationToken);
        }
    }
}
#endif
