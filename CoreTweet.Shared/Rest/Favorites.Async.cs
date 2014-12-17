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
    partial class Favorites
    {
        //GET Methods

        /// <summary>
        /// <para>Returns the 20 most recent Tweets favorited by the authenticating or specified user as an asynchronous operation.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> id (optional)</para>
        /// <para>- <c>string</c> screen_name (optonal)</para>
        /// <para>- <c>int</c> count (optional)</para>
        /// <para>- <c>long</c> since_id (optional)</para>
        /// <para>- <c>long</c> max_id (optional)</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the statuses.</para>
        /// </returns>
        public Task<ListedResponse<Status>> ListAsync(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiArrayAsync<Status>(MethodType.Get, "favorites/list", parameters);
        }

        /// <summary>
        /// <para>Returns the 20 most recent Tweets favorited by the authenticating or specified user as an asynchronous operation.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> id (optional)</para>
        /// <para>- <c>string</c> screen_name (optonal)</para>
        /// <para>- <c>int</c> count (optional)</para>
        /// <para>- <c>long</c> since_id (optional)</para>
        /// <para>- <c>long</c> max_id (optional)</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the statuses.</para>
        /// </returns>
        public Task<ListedResponse<Status>> ListAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiArrayAsync<Status>(MethodType.Get, "favorites/list", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Returns the 20 most recent Tweets favorited by the authenticating or specified user as an asynchronous operation.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> id (optional)</para>
        /// <para>- <c>string</c> screen_name (optonal)</para>
        /// <para>- <c>int</c> count (optional)</para>
        /// <para>- <c>long</c> since_id (optional)</para>
        /// <para>- <c>long</c> max_id (optional)</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the statuses.</para>
        /// </returns>
        public Task<ListedResponse<Status>> ListAsync<T>(T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiArrayAsync<Status, T>(MethodType.Get, "favorites/list", parameters, cancellationToken);
        }

        //POST Methods

        /// <summary>
        /// <para>Favorites the status specified in the ID parameter as the authenticating user as an asynchronous operation.</para>
        /// <para>Returns the favorite status when successful.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> id (required)</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the favorited status.</para>
        /// </returns>
        public Task<StatusResponse> CreateAsync(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiAsync<StatusResponse>(MethodType.Post, "favorites/create", parameters);
        }

        /// <summary>
        /// <para>Favorites the status specified in the ID parameter as the authenticating user as an asynchronous operation.</para>
        /// <para>Returns the favorite status when successful.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> id (required)</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the favorited status.</para>
        /// </returns>
        public Task<StatusResponse> CreateAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<StatusResponse>(MethodType.Post, "favorites/create", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Favorites the status specified in the ID parameter as the authenticating user as an asynchronous operation.</para>
        /// <para>Returns the favorite status when successful.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> id (required)</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the favorited status.</para>
        /// </returns>
        public Task<StatusResponse> CreateAsync<T>(T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<StatusResponse, T>(MethodType.Post, "favorites/create", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Un-favorites the status specified in the ID parameter as the authenticating user as an asynchronous operation.</para>
        /// <para>Returns the un-favorited status in the requested format when successful.</para>
        /// <para>This process invoked by this method is asynchronous.</para>
        /// <para>The immediately returned status may not indicate the resultant favorited status of the tweet.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> id (required)</para>
        /// <para>- <c>bool</c> include_entities (ooptional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the destroied status.</para>
        /// </returns>
        public Task<StatusResponse> DestroyAsync(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiAsync<StatusResponse>(MethodType.Post, "favorites/destroy", parameters);
        }

        /// <summary>
        /// <para>Un-favorites the status specified in the ID parameter as the authenticating user as an asynchronous operation.</para>
        /// <para>Returns the un-favorited status in the requested format when successful.</para>
        /// <para>This process invoked by this method is asynchronous.</para>
        /// <para>The immediately returned status may not indicate the resultant favorited status of the tweet.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> id (required)</para>
        /// <para>- <c>bool</c> include_entities (ooptional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the destroied status.</para>
        /// </returns>
        public Task<StatusResponse> DestroyAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<StatusResponse>(MethodType.Post, "favorites/destroy", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Un-favorites the status specified in the ID parameter as the authenticating user as an asynchronous operation.</para>
        /// <para>Returns the un-favorited status in the requested format when successful.</para>
        /// <para>This process invoked by this method is asynchronous.</para>
        /// <para>The immediately returned status may not indicate the resultant favorited status of the tweet.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> id (required)</para>
        /// <para>- <c>bool</c> include_entities (ooptional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the destroied status.</para>
        /// </returns>
        public Task<StatusResponse> DestroyAsync<T>(T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<StatusResponse, T>(MethodType.Post, "favorites/destroy", parameters, cancellationToken);
        }
    }
}
#endif
