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
    /// Provides a set of methods for the wrapper of GET/POST favorites.
    /// </summary>
    public partial class Favorites : ApiProviderBase
    {
        internal Favorites(TokensBase e) : base(e) { }

#if !(PCL || WIN_RT || WP)
        //GET Methods

        /// <summary>
        /// <para>Returns the 20 most recent Tweets favorited by the authenticating or specified user.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> id (optional)</para>
        /// <para>- <c>string</c> screen_name (optonal)</para>
        /// <para>- <c>int</c> count (optional)</para>
        /// <para>- <c>long</c> since_id (optional)</para>
        /// <para>- <c>long</c> max_id (optional)</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The statuses.</returns>
        public ListedResponse<Status> List(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiArray<Status>(MethodType.Get, "favorites/list", parameters);
        }

        /// <summary>
        /// <para>Returns the 20 most recent Tweets favorited by the authenticating or specified user.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> id (optional)</para>
        /// <para>- <c>string</c> screen_name (optonal)</para>
        /// <para>- <c>int</c> count (optional)</para>
        /// <para>- <c>long</c> since_id (optional)</para>
        /// <para>- <c>long</c> max_id (optional)</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The statuses.</returns>
        public ListedResponse<Status> List(IDictionary<string, object> parameters)
        {
            return this.Tokens.AccessApiArray<Status>(MethodType.Get, "favorites/list", parameters);
        }

        /// <summary>
        /// <para>Returns the 20 most recent Tweets favorited by the authenticating or specified user.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> id (optional)</para>
        /// <para>- <c>string</c> screen_name (optonal)</para>
        /// <para>- <c>int</c> count (optional)</para>
        /// <para>- <c>long</c> since_id (optional)</para>
        /// <para>- <c>long</c> max_id (optional)</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The statuses.</returns>
        public ListedResponse<Status> List<T>(T parameters)
        {
            return this.Tokens.AccessApiArray<Status, T>(MethodType.Get, "favorites/list", parameters);
        }

        //POST Methods

        /// <summary>
        /// <para>Favorites the status specified in the ID parameter as the authenticating user.</para>
        /// <para>Returns the favorite status when successful.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> id (required)</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The favorited status.</returns>
        public StatusResponse Create(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApi<StatusResponse>(MethodType.Post, "favorites/create", parameters);
        }

        /// <summary>
        /// <para>Favorites the status specified in the ID parameter as the authenticating user.</para>
        /// <para>Returns the favorite status when successful.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> id (required)</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The favorited status.</returns>
        public StatusResponse Create(IDictionary<string, object> parameters)
        {
            return this.Tokens.AccessApi<StatusResponse>(MethodType.Post, "favorites/create", parameters);
        }

        /// <summary>
        /// <para>Favorites the status specified in the ID parameter as the authenticating user.</para>
        /// <para>Returns the favorite status when successful.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> id (required)</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The favorited status.</returns>
        public StatusResponse Create<T>(T parameters)
        {
            return this.Tokens.AccessApi<StatusResponse, T>(MethodType.Post, "favorites/create", parameters);
        }

        /// <summary>
        /// <para>Un-favorites the status specified in the ID parameter as the authenticating user.</para>
        /// <para>Returns the un-favorited status in the requested format when successful.</para>
        /// <para>This process invoked by this method is asynchronous.</para>
        /// <para>The immediately returned status may not indicate the resultant favorited status of the tweet.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> id (required)</para>
        /// <para>- <c>bool</c> include_entities (ooptional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The destroied status.</returns>
        public StatusResponse Destroy(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApi<StatusResponse>(MethodType.Post, "favorites/destroy", parameters);
        }

        /// <summary>
        /// <para>Un-favorites the status specified in the ID parameter as the authenticating user.</para>
        /// <para>Returns the un-favorited status in the requested format when successful.</para>
        /// <para>This process invoked by this method is asynchronous.</para>
        /// <para>The immediately returned status may not indicate the resultant favorited status of the tweet.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> id (required)</para>
        /// <para>- <c>bool</c> include_entities (ooptional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The destroied status.</returns>
        public StatusResponse Destroy(IDictionary<string, object> parameters)
        {
            return this.Tokens.AccessApi<StatusResponse>(MethodType.Post, "favorites/destroy", parameters);
        }

        /// <summary>
        /// <para>Un-favorites the status specified in the ID parameter as the authenticating user.</para>
        /// <para>Returns the un-favorited status in the requested format when successful.</para>
        /// <para>This process invoked by this method is asynchronous.</para>
        /// <para>The immediately returned status may not indicate the resultant favorited status of the tweet.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> id (required)</para>
        /// <para>- <c>bool</c> include_entities (ooptional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The destroied status.</returns>
        public StatusResponse Destroy<T>(T parameters)
        {
            return this.Tokens.AccessApi<StatusResponse, T>(MethodType.Post, "favorites/destroy", parameters);
        }
#endif
    }
}
