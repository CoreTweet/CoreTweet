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
    partial class Application
    {
        //GET Methods

        /// <summary>
        /// <para>Returns the current rate limits for methods belonging to the specified resource families as an asynchronous operation.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c>string / <c>IEnumerable&lt;string&gt;</c> resources (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the dictionary.</para>
        /// </returns>
        public Task<DictionaryResponse<string, Dictionary<string, RateLimit>>> RateLimitStatusAsync(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiDictionaryAsync<string, Dictionary<string, RateLimit>>(MethodType.Get, "application/rate_limit_status", parameters, "resources");
        }

        /// <summary>
        /// <para>Returns the current rate limits for methods belonging to the specified resource families as an asynchronous operation.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c>string / <c>IEnumerable&lt;string&gt;</c> resources (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the dictionary.</para>
        /// </returns>
        public Task<DictionaryResponse<string, Dictionary<string, RateLimit>>> RateLimitStatusAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiDictionaryAsync<string, Dictionary<string, RateLimit>>(MethodType.Get, "application/rate_limit_status", parameters, cancellationToken, "resources");
        }

        /// <summary>
        /// <para>Returns the current rate limits for methods belonging to the specified resource families as an asynchronous operation.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c>string / <c>IEnumerable&lt;string&gt;</c> resources (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the dictionary.</para>
        /// </returns>
        public Task<DictionaryResponse<string, Dictionary<string, RateLimit>>> RateLimitStatusAsync<T>(T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiDictionaryAsync<string, Dictionary<string, RateLimit>, T>(MethodType.Get, "application/rate_limit_status", parameters, cancellationToken, "resources");
        }
    }
}
