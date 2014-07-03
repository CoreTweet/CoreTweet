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

namespace CoreTweet.Rest
{
    partial class Followers
    {
        //GET Methods

        /// <summary>
        /// <para>Returns a cursored collection of user IDs for every user following the specified user as an asynchronous operation.</para>
        /// <para>At this time, results are ordered with the most recent following first; however this ordering is subject to unannounced change and eventual consistency issues.</para>
        /// <para>Results are given in groups of 5,000 user IDs and multiple "pages" of results can be navigated through using the next_cursor value in subsequent requests.</para>
        /// <para>This method is especially powerful when used in conjunction with GET users/lookup, a method that allows you to convert user IDs into full user objects in bulk.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> user_id (optional)</para>
        /// <para>- <c>string</c> screen_name (optional)</para>
        /// <para>- <c>long</c> cursor (semi-optional)</para>
        /// <para>- <c>int</c> count (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the IDs.</para>
        /// </returns>
        public Task<Cursored<long>> IdsAsync(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiAsync<Cursored<long>>(MethodType.Get, "followers/ids", parameters);
        }

        /// <summary>
        /// <para>Returns a cursored collection of user IDs for every user following the specified user as an asynchronous operation.</para>
        /// <para>At this time, results are ordered with the most recent following first; however this ordering is subject to unannounced change and eventual consistency issues.</para>
        /// <para>Results are given in groups of 5,000 user IDs and multiple "pages" of results can be navigated through using the next_cursor value in subsequent requests.</para>
        /// <para>This method is especially powerful when used in conjunction with GET users/lookup, a method that allows you to convert user IDs into full user objects in bulk.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> user_id (optional)</para>
        /// <para>- <c>string</c> screen_name (optional)</para>
        /// <para>- <c>long</c> cursor (semi-optional)</para>
        /// <para>- <c>int</c> count (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the IDs.</para>
        /// </returns>
        public Task<Cursored<long>> IdsAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<Cursored<long>>(MethodType.Get, "followers/ids", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Returns a cursored collection of user IDs for every user following the specified user as an asynchronous operation.</para>
        /// <para>At this time, results are ordered with the most recent following first; however this ordering is subject to unannounced change and eventual consistency issues.</para>
        /// <para>Results are given in groups of 5,000 user IDs and multiple "pages" of results can be navigated through using the next_cursor value in subsequent requests.</para>
        /// <para>This method is especially powerful when used in conjunction with GET users/lookup, a method that allows you to convert user IDs into full user objects in bulk.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> user_id (optional)</para>
        /// <para>- <c>string</c> screen_name (optional)</para>
        /// <para>- <c>long</c> cursor (semi-optional)</para>
        /// <para>- <c>int</c> count (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the IDs.</para>
        /// </returns>
        public Task<Cursored<long>> IdsAsync<T>(T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<Cursored<long>, T>(MethodType.Get, "followers/ids", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Returns a cursored collection of user objects for users following the specified user as an asynchronous operation.</para>
        /// <para>At this time, results are ordered with the most recent following first; however, this ordering is subject to unannounced change and eventual consistency issues.</para>
        /// <para>Results are given in groups of 20 users and multiple "pages" of results can be navigated through using the next_cursor value in subsequent requests.</para>
        /// <para>Note: Either a screen_name or a user_id should be provided.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> id (optional)</para>
        /// <para>- <c>string</c> screen_name (optional)</para>
        /// <para>- <c>long</c> cursor (semi-optional)</para>
        /// <para>- <c>bool</c> skip_status (optional)</para>
        /// <para>- <c>bool</c> include_user_entities</para>
        /// </summary>
        /// <param name="parameters">The parameters</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the users.</para>
        /// </returns>
        public Task<Cursored<User>> ListAsync(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiAsync<Cursored<User>>(MethodType.Get, "followers/list", parameters);
        }

        /// <summary>
        /// <para>Returns a cursored collection of user objects for users following the specified user as an asynchronous operation.</para>
        /// <para>At this time, results are ordered with the most recent following first; however, this ordering is subject to unannounced change and eventual consistency issues.</para>
        /// <para>Results are given in groups of 20 users and multiple "pages" of results can be navigated through using the next_cursor value in subsequent requests.</para>
        /// <para>Note: Either a screen_name or a user_id should be provided.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> id (optional)</para>
        /// <para>- <c>string</c> screen_name (optional)</para>
        /// <para>- <c>long</c> cursor (semi-optional)</para>
        /// <para>- <c>bool</c> skip_status (optional)</para>
        /// <para>- <c>bool</c> include_user_entities</para>
        /// </summary>
        /// <param name="parameters">The parameters</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the users.</para>
        /// </returns>
        public Task<Cursored<User>> ListAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<Cursored<User>>(MethodType.Get, "followers/list", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Returns a cursored collection of user objects for users following the specified user as an asynchronous operation.</para>
        /// <para>At this time, results are ordered with the most recent following first; however, this ordering is subject to unannounced change and eventual consistency issues.</para>
        /// <para>Results are given in groups of 20 users and multiple "pages" of results can be navigated through using the next_cursor value in subsequent requests.</para>
        /// <para>Note: Either a screen_name or a user_id should be provided.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> id (optional)</para>
        /// <para>- <c>string</c> screen_name (optional)</para>
        /// <para>- <c>long</c> cursor (semi-optional)</para>
        /// <para>- <c>bool</c> skip_status (optional)</para>
        /// <para>- <c>bool</c> include_user_entities</para>
        /// </summary>
        /// <param name="parameters">The parameters</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the users.</para>
        /// </returns>
        public Task<Cursored<User>> ListAsync<T>(T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<Cursored<User>, T>(MethodType.Get, "followers/list", parameters, cancellationToken);
        }
    }
}
