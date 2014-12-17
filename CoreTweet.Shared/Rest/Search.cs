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
using CoreTweet.Core;

namespace CoreTweet.Rest
{
    /// <summary>
    /// Provides a set of methods for the wrapper of GET search.
    /// </summary>
    public partial class Search : ApiProviderBase
    {
        internal Search(TokensBase e) : base(e) { }

#if !(PCL || WIN_RT || WP)
        //GET Method

        /// <summary>
        /// <para>Returns a collection of relevant Tweets matching a specified query.</para>
        /// <para>Please note that Twitter's search service and, by extension, the Search API is not meant to be an exhaustive source of Tweets.</para>
        /// <para>Not all Tweets will be indexed or made available via the search interface.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> q (required)</para>
        /// <para>- <c>string</c> geocode (optional)</para>
        /// <para>- <c>string</c> lang (optional)</para>
        /// <para>- <c>string</c> locale (optional)</para>
        /// <para>- <c>string</c> result_type (optional)</para>
        /// <para>- <c>int</c> count (optional)</para>
        /// <para>- <c>string</c> until (optional)</para>
        /// <para>- <c>long</c> since_id (optional)</para>
        /// <para>- <c>long</c> max_id (optional)</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The statuses.</returns>
        public SearchResult Tweets(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApi<SearchResult>(MethodType.Get, "search/tweets", parameters);
        }

        /// <summary>
        /// <para>Returns a collection of relevant Tweets matching a specified query.</para>
        /// <para>Please note that Twitter's search service and, by extension, the Search API is not meant to be an exhaustive source of Tweets.</para>
        /// <para>Not all Tweets will be indexed or made available via the search interface.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> q (required)</para>
        /// <para>- <c>string</c> geocode (optional)</para>
        /// <para>- <c>string</c> lang (optional)</para>
        /// <para>- <c>string</c> locale (optional)</para>
        /// <para>- <c>string</c> result_type (optional)</para>
        /// <para>- <c>int</c> count (optional)</para>
        /// <para>- <c>string</c> until (optional)</para>
        /// <para>- <c>long</c> since_id (optional)</para>
        /// <para>- <c>long</c> max_id (optional)</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The statuses.</returns>
        public SearchResult Tweets(IDictionary<string, object> parameters)
        {
            return this.Tokens.AccessApi<SearchResult>(MethodType.Get, "search/tweets", parameters);
        }

        /// <summary>
        /// <para>Returns a collection of relevant Tweets matching a specified query.</para>
        /// <para>Please note that Twitter's search service and, by extension, the Search API is not meant to be an exhaustive source of Tweets.</para>
        /// <para>Not all Tweets will be indexed or made available via the search interface.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> q (required)</para>
        /// <para>- <c>string</c> geocode (optional)</para>
        /// <para>- <c>string</c> lang (optional)</para>
        /// <para>- <c>string</c> locale (optional)</para>
        /// <para>- <c>string</c> result_type (optional)</para>
        /// <para>- <c>int</c> count (optional)</para>
        /// <para>- <c>string</c> until (optional)</para>
        /// <para>- <c>long</c> since_id (optional)</para>
        /// <para>- <c>long</c> max_id (optional)</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The statuses.</returns>
        public SearchResult Tweets<T>(T parameters)
        {
            return this.Tokens.AccessApi<SearchResult, T>(MethodType.Get, "search/tweets", parameters);
        }
#endif
    }
}
