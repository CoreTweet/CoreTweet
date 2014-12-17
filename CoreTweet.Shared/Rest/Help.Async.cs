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

#if !NET35
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using CoreTweet.Core;

namespace CoreTweet.Rest
{
    partial class Help
    {
        //GET Methods

        /// <summary>
        /// <para>Returns the current configuration used by Twitter including twitter.com slugs which are not usernames, maximum photo resolutions, and t.co URL lengths as an asynchronous operation.</para>
        /// <para>It is recommended applications request this endpoint when they are loaded, but no more than once a day.</para>
        /// <para>Available parameters: Nothing.</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the configurations.</para>
        /// </returns>
        public Task<Configurations> ConfigurationAsync(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiAsync<Configurations>(MethodType.Get, "help/configuration", parameters);
        }

        /// <summary>
        /// <para>Returns the current configuration used by Twitter including twitter.com slugs which are not usernames, maximum photo resolutions, and t.co URL lengths as an asynchronous operation.</para>
        /// <para>It is recommended applications request this endpoint when they are loaded, but no more than once a day.</para>
        /// <para>Available parameters: Nothing.</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the configurations.</para>
        /// </returns>
        public Task<Configurations> ConfigurationAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<Configurations>(MethodType.Get, "help/configuration", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Returns the current configuration used by Twitter including twitter.com slugs which are not usernames, maximum photo resolutions, and t.co URL lengths as an asynchronous operation.</para>
        /// <para>It is recommended applications request this endpoint when they are loaded, but no more than once a day.</para>
        /// <para>Available parameters: Nothing.</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the configurations.</para>
        /// </returns>
        public Task<Configurations> ConfigurationAsync<T>(T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<Configurations, T>(MethodType.Get, "help/configuration", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Returns the list of languages supported by Twitter along with their ISO 639-1 code as an asynchronous operation.</para>
        /// <para>The ISO 639-1 code is the two letter value to use if you include lang with any of your requests.</para>
        /// <para>Available parameters: Nothing.</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the languages.</para>
        /// </returns>
        public Task<ListedResponse<Language>> LanguagesAsync(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiArrayAsync<Language>(MethodType.Get, "help/languages", parameters);
        }

        /// <summary>
        /// <para>Returns the list of languages supported by Twitter along with their ISO 639-1 code as an asynchronous operation.</para>
        /// <para>The ISO 639-1 code is the two letter value to use if you include lang with any of your requests.</para>
        /// <para>Available parameters: Nothing.</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the languages.</para>
        /// </returns>
        public Task<ListedResponse<Language>> LanguagesAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiArrayAsync<Language>(MethodType.Get, "help/languages", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Returns the list of languages supported by Twitter along with their ISO 639-1 code as an asynchronous operation.</para>
        /// <para>The ISO 639-1 code is the two letter value to use if you include lang with any of your requests.</para>
        /// <para>Available parameters: Nothing.</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the languages.</para>
        /// </returns>
        public Task<ListedResponse<Language>> LanguagesAsync<T>(T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiArrayAsync<Language, T>(MethodType.Get, "help/languages", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Returns Twitter's Privacy Policy as an asynchronous operation.</para>
        /// <para>Available parameters: Nothing.</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the sentense.</para>
        /// </returns>
        public Task<StringResponse> PrivacyAsync(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiAsync<StringResponse>(MethodType.Get, "help/privacy", parameters);
        }

        /// <summary>
        /// <para>Returns Twitter's Privacy Policy as an asynchronous operation.</para>
        /// <para>Available parameters: Nothing.</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the sentense.</para>
        /// </returns>
        public Task<StringResponse> PrivacyAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<StringResponse>(MethodType.Get, "help/privacy", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Returns Twitter's Privacy Policy as an asynchronous operation.</para>
        /// <para>Available parameters: Nothing.</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the sentense.</para>
        /// </returns>
        public Task<StringResponse> PrivacyAsync<T>(T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<StringResponse, T>(MethodType.Get, "help/privacy", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Returns the Twitter Terms of Service in the requested format as an asynchronous operation.</para>
        /// <para>These are not the same as the Developer Rules of the Road.</para>
        /// <para>Available parameters: Nothing.</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the sentense.</para>
        /// </returns>
        public Task<StringResponse> TosAsync(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiAsync<StringResponse>(MethodType.Get, "help/tos", parameters);
        }

        /// <summary>
        /// <para>Returns the Twitter Terms of Service in the requested format as an asynchronous operation.</para>
        /// <para>These are not the same as the Developer Rules of the Road.</para>
        /// <para>Available parameters: Nothing.</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the sentense.</para>
        /// </returns>
        public Task<StringResponse> TosAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<StringResponse>(MethodType.Get, "help/tos", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Returns the Twitter Terms of Service in the requested format as an asynchronous operation.</para>
        /// <para>These are not the same as the Developer Rules of the Road.</para>
        /// <para>Available parameters: Nothing.</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the sentense.</para>
        /// </returns>
        public Task<StringResponse> TosAsync<T>(T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<StringResponse, T>(MethodType.Get, "help/tos", parameters, cancellationToken);
        }

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
        [Obsolete("Use Application.RateLimitStatusAsync.")]
        public Task<DictionaryResponse<string, Dictionary<string, RateLimit>>> RateLimitStatusAsync(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.Application.RateLimitStatusAsync(parameters);
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
        [Obsolete("Use Application.RateLimitStatusAsync.")]
        public Task<DictionaryResponse<string, Dictionary<string, RateLimit>>> RateLimitStatusAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.Application.RateLimitStatusAsync(parameters, cancellationToken);
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
        [Obsolete("Use Application.RateLimitStatusAsync.")]
        public Task<DictionaryResponse<string, Dictionary<string, RateLimit>>> RateLimitStatusAsync<T>(T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.Application.RateLimitStatusAsync(parameters, cancellationToken);
        }
    }
}
#endif
