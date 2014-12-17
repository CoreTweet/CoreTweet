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
    /// Provides a set of methods for the wrapper of GET application.
    /// </summary>
    public partial class Application : ApiProviderBase
    {
        internal Application(TokensBase e) : base(e) { }

#if !(PCL || WIN_RT || WP)
        //GET Methods

        /// <summary>
        /// <para>Returns the current rate limits for methods belonging to the specified resource families.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c>string / <c>IEnumerable&lt;string&gt;</c> resources (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The dictionary.</returns>
        public DictionaryResponse<string, Dictionary<string, RateLimit>> RateLimitStatus(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiDictionary<string, Dictionary<string, RateLimit>>(MethodType.Get, "application/rate_limit_status", parameters, "resources");
        }

        /// <summary>
        /// <para>Returns the current rate limits for methods belonging to the specified resource families.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c>string / <c>IEnumerable&lt;string&gt;</c> resources (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The dictionary.</returns>
        public DictionaryResponse<string, Dictionary<string, RateLimit>> RateLimitStatus(IDictionary<string, object> parameters)
        {
            return this.Tokens.AccessApiDictionary<string, Dictionary<string, RateLimit>>(MethodType.Get, "application/rate_limit_status", parameters, "resources");
        }

        /// <summary>
        /// <para>Returns the current rate limits for methods belonging to the specified resource families.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c>string / <c>IEnumerable&lt;string&gt;</c> resources (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The dictionary.</returns>
        public DictionaryResponse<string, Dictionary<string, RateLimit>> RateLimitStatus<T>(T parameters)
        {
            return this.Tokens.AccessApiDictionary<string, Dictionary<string, RateLimit>, T>(MethodType.Get, "application/rate_limit_status", parameters, "resources");
        }
#endif
    }
}
