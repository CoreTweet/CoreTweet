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
    /// Provides a set of methods for the wrapper of GET help.
    /// </summary>
    public partial class Help : ApiProviderBase
    {
        internal Help(TokensBase e) : base(e) { }

#if !(PCL || WIN_RT || WP)
        //GET Methods

        /// <summary>
        /// <para>Returns the current configuration used by Twitter including twitter.com slugs which are not usernames, maximum photo resolutions, and t.co URL lengths.</para>
        /// <para>It is recommended applications request this endpoint when they are loaded, but no more than once a day.</para>
        /// <para>Available parameters: Nothing.</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>Configurations.</returns>
        public Configurations Configuration(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApi<Configurations>(MethodType.Get, "help/configuration", parameters);
        }

        /// <summary>
        /// <para>Returns the current configuration used by Twitter including twitter.com slugs which are not usernames, maximum photo resolutions, and t.co URL lengths.</para>
        /// <para>It is recommended applications request this endpoint when they are loaded, but no more than once a day.</para>
        /// <para>Available parameters: Nothing.</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>Configurations.</returns>
        public Configurations Configuration(IDictionary<string, object> parameters)
        {
            return this.Tokens.AccessApi<Configurations>(MethodType.Get, "help/configuration", parameters);
        }

        /// <summary>
        /// <para>Returns the current configuration used by Twitter including twitter.com slugs which are not usernames, maximum photo resolutions, and t.co URL lengths.</para>
        /// <para>It is recommended applications request this endpoint when they are loaded, but no more than once a day.</para>
        /// <para>Available parameters: Nothing.</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>Configurations.</returns>
        public Configurations Configuration<T>(T parameters)
        {
            return this.Tokens.AccessApi<Configurations, T>(MethodType.Get, "help/configuration", parameters);
        }

        /// <summary>
        /// <para>Returns the list of languages supported by Twitter along with their ISO 639-1 code.</para>
        /// <para>The ISO 639-1 code is the two letter value to use if you include lang with any of your requests.</para>
        /// <para>Available parameters: Nothing.</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>Languages.</returns>
        public ListedResponse<Language> Languages(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiArray<Language>(MethodType.Get, "help/languages", parameters);
        }

        /// <summary>
        /// <para>Returns the list of languages supported by Twitter along with their ISO 639-1 code.</para>
        /// <para>The ISO 639-1 code is the two letter value to use if you include lang with any of your requests.</para>
        /// <para>Available parameters: Nothing.</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>Languages.</returns>
        public ListedResponse<Language> Languages(IDictionary<string, object> parameters)
        {
            return this.Tokens.AccessApiArray<Language>(MethodType.Get, "help/languages", parameters);
        }

        /// <summary>
        /// <para>Returns the list of languages supported by Twitter along with their ISO 639-1 code.</para>
        /// <para>The ISO 639-1 code is the two letter value to use if you include lang with any of your requests.</para>
        /// <para>Available parameters: Nothing.</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>Languages.</returns>
        public ListedResponse<Language> Languages<T>(T parameters)
        {
            return this.Tokens.AccessApiArray<Language, T>(MethodType.Get, "help/languages", parameters);
        }

        /// <summary>
        /// <para>Returns Twitter's Privacy Policy.</para>
        /// <para>Available parameters: Nothing.</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The sentense.</returns>
        public StringResponse Privacy(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApi<StringResponse>(MethodType.Get, "help/privacy", parameters);
        }

        /// <summary>
        /// <para>Returns Twitter's Privacy Policy.</para>
        /// <para>Available parameters: Nothing.</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The sentense.</returns>
        public StringResponse Privacy(IDictionary<string, object> parameters)
        {
            return this.Tokens.AccessApi<StringResponse>(MethodType.Get, "help/privacy", parameters);
        }

        /// <summary>
        /// <para>Returns Twitter's Privacy Policy.</para>
        /// <para>Available parameters: Nothing.</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The sentense.</returns>
        public StringResponse Privacy<T>(T parameters)
        {
            return this.Tokens.AccessApi<StringResponse, T>(MethodType.Get, "help/privacy", parameters);
        }

        /// <summary>
        /// <para>Returns the Twitter Terms of Service in the requested format.</para>
        /// <para>These are not the same as the Developer Rules of the Road.</para>
        /// <para>Available parameters: Nothing.</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The sentense.</returns>
        public StringResponse Tos(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApi<StringResponse>(MethodType.Get, "help/tos", parameters);
        }

        /// <summary>
        /// <para>Returns the Twitter Terms of Service in the requested format.</para>
        /// <para>These are not the same as the Developer Rules of the Road.</para>
        /// <para>Available parameters: Nothing.</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The sentense.</returns>
        public StringResponse Tos(IDictionary<string, object> parameters)
        {
            return this.Tokens.AccessApi<StringResponse>(MethodType.Get, "help/tos", parameters);
        }

        /// <summary>
        /// <para>Returns the Twitter Terms of Service in the requested format.</para>
        /// <para>These are not the same as the Developer Rules of the Road.</para>
        /// <para>Available parameters: Nothing.</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The sentense.</returns>
        public StringResponse Tos<T>(T parameters)
        {
            return this.Tokens.AccessApi<StringResponse, T>(MethodType.Get, "help/tos", parameters);
        }

        /// <summary>
        /// <para>Returns the current rate limits for methods belonging to the specified resource families.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c>string / <c>IEnumerable&lt;string&gt;</c> resources (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The dictionary.</returns>
        [Obsolete("Use Application.RateLimitStatus.")]
        public DictionaryResponse<string, Dictionary<string, RateLimit>> RateLimitStatus(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.Application.RateLimitStatus(parameters);
        }

        /// <summary>
        /// <para>Returns the current rate limits for methods belonging to the specified resource families.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c>string / <c>IEnumerable&lt;string&gt;</c> resources (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The dictionary.</returns>
        [Obsolete("Use Application.RateLimitStatus.")]
        public DictionaryResponse<string, Dictionary<string, RateLimit>> RateLimitStatus(IDictionary<string, object> parameters)
        {
            return this.Tokens.Application.RateLimitStatus(parameters);
        }

        /// <summary>
        /// <para>Returns the current rate limits for methods belonging to the specified resource families.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c>string / <c>IEnumerable&lt;string&gt;</c> resources (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The dictionary.</returns>
        [Obsolete("Use Application.RateLimitStatus.")]
        public DictionaryResponse<string, Dictionary<string, RateLimit>> RateLimitStatus<T>(T parameters)
        {
            return this.Tokens.Application.RateLimitStatus(parameters);
        }
#endif
    }
}
