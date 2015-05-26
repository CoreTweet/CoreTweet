
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
using System.Linq;
using System.Linq.Expressions;
using CoreTweet.Core;
#if !NET35
using System.Threading;
using System.Threading.Tasks;
#endif

namespace CoreTweet.Rest
{

    /// <summary>
    /// This contains several types of api for testing.
    /// </summary>
    public partial class RestTest : ApiProviderBase
    {
        internal RestTest (TokensBase e) : base(e) { }

        #if !(PCL || WIN_RT || WP)
        /// <summary>
        /// <para>Updates the authenticating user's current status, also known as tweeting.</para>
        /// <para>To upload an image to accompany the tweet, use POST statuses/update_with_media.</para>
        /// <para>For each parameters attempt, the parameters text is compared with the authenticating user's recent tweets.</para>
        /// <para>Any attempt that would result in duplication will be blocked, resulting in a 403 error.</para>
        /// <para>Therefore, a user cannot submit the same status twice in a row.</para>
        /// <para>While not rate limited by the API a user is limited in the number of tweets they can create at a time.</para>
        /// <para>If the number of updates posted by the user reaches the current allowed limit this method will return an HTTP 403 error.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> status (required)</para>
        /// <para>- <c>long</c> in_reply_to_status_id (optional)</para>
        /// <para>- <c>double</c> lat (optional)</para>
        /// <para>- <c>double</c> long (optional)</para>
        /// <para>- <c>string</c> place_id (optional)</para>
        /// <para>- <c>bool</c> display_coordinates (optional)</para>
        /// <para>- <c>bool</c> trim_user (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The updated status.</returns>
        public StatusResponse Update(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApi<StatusResponse>(MethodType.Get, "statuses/update", parameters);
        }

        /// <summary>
        /// <para>Updates the authenticating user's current status, also known as tweeting.</para>
        /// <para>To upload an image to accompany the tweet, use POST statuses/update_with_media.</para>
        /// <para>For each parameters attempt, the parameters text is compared with the authenticating user's recent tweets.</para>
        /// <para>Any attempt that would result in duplication will be blocked, resulting in a 403 error.</para>
        /// <para>Therefore, a user cannot submit the same status twice in a row.</para>
        /// <para>While not rate limited by the API a user is limited in the number of tweets they can create at a time.</para>
        /// <para>If the number of updates posted by the user reaches the current allowed limit this method will return an HTTP 403 error.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> status (required)</para>
        /// <para>- <c>long</c> in_reply_to_status_id (optional)</para>
        /// <para>- <c>double</c> lat (optional)</para>
        /// <para>- <c>double</c> long (optional)</para>
        /// <para>- <c>string</c> place_id (optional)</para>
        /// <para>- <c>bool</c> display_coordinates (optional)</para>
        /// <para>- <c>bool</c> trim_user (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The updated status.</returns>
        public StatusResponse Update(IDictionary<string, object> parameters)
        {
            return this.Tokens.AccessApi<StatusResponse>(MethodType.Get, "statuses/update", parameters);
        }

        /// <summary>
        /// <para>Updates the authenticating user's current status, also known as tweeting.</para>
        /// <para>To upload an image to accompany the tweet, use POST statuses/update_with_media.</para>
        /// <para>For each parameters attempt, the parameters text is compared with the authenticating user's recent tweets.</para>
        /// <para>Any attempt that would result in duplication will be blocked, resulting in a 403 error.</para>
        /// <para>Therefore, a user cannot submit the same status twice in a row.</para>
        /// <para>While not rate limited by the API a user is limited in the number of tweets they can create at a time.</para>
        /// <para>If the number of updates posted by the user reaches the current allowed limit this method will return an HTTP 403 error.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> status (required)</para>
        /// <para>- <c>long</c> in_reply_to_status_id (optional)</para>
        /// <para>- <c>double</c> lat (optional)</para>
        /// <para>- <c>double</c> long (optional)</para>
        /// <para>- <c>string</c> place_id (optional)</para>
        /// <para>- <c>bool</c> display_coordinates (optional)</para>
        /// <para>- <c>bool</c> trim_user (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The updated status.</returns>
        public StatusResponse Update<T>(T parameters)
        {
            return this.Tokens.AccessApi<StatusResponse, T>(MethodType.Get, "statuses/update", parameters);
        }

        #endif
        #if !NET35

        /// <summary>
        /// <para>Updates the authenticating user's current status, also known as tweeting.</para>
        /// <para>To upload an image to accompany the tweet, use POST statuses/update_with_media.</para>
        /// <para>For each parameters attempt, the parameters text is compared with the authenticating user's recent tweets.</para>
        /// <para>Any attempt that would result in duplication will be blocked, resulting in a 403 error.</para>
        /// <para>Therefore, a user cannot submit the same status twice in a row.</para>
        /// <para>While not rate limited by the API a user is limited in the number of tweets they can create at a time.</para>
        /// <para>If the number of updates posted by the user reaches the current allowed limit this method will return an HTTP 403 error.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> status (required)</para>
        /// <para>- <c>long</c> in_reply_to_status_id (optional)</para>
        /// <para>- <c>double</c> lat (optional)</para>
        /// <para>- <c>double</c> long (optional)</para>
        /// <para>- <c>string</c> place_id (optional)</para>
        /// <para>- <c>bool</c> display_coordinates (optional)</para>
        /// <para>- <c>bool</c> trim_user (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The updated status.</returns>
        public Task<StatusResponse> Update(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiAsync<StatusResponse>(MethodType.Get, "statuses/update", parameters);
        }

        /// <summary>
        /// <para>Updates the authenticating user's current status, also known as tweeting.</para>
        /// <para>To upload an image to accompany the tweet, use POST statuses/update_with_media.</para>
        /// <para>For each parameters attempt, the parameters text is compared with the authenticating user's recent tweets.</para>
        /// <para>Any attempt that would result in duplication will be blocked, resulting in a 403 error.</para>
        /// <para>Therefore, a user cannot submit the same status twice in a row.</para>
        /// <para>While not rate limited by the API a user is limited in the number of tweets they can create at a time.</para>
        /// <para>If the number of updates posted by the user reaches the current allowed limit this method will return an HTTP 403 error.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> status (required)</para>
        /// <para>- <c>long</c> in_reply_to_status_id (optional)</para>
        /// <para>- <c>double</c> lat (optional)</para>
        /// <para>- <c>double</c> long (optional)</para>
        /// <para>- <c>string</c> place_id (optional)</para>
        /// <para>- <c>bool</c> display_coordinates (optional)</para>
        /// <para>- <c>bool</c> trim_user (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The updated status.</returns>
        public Task<StatusResponse> Update(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<StatusResponse>(MethodType.Get, "statuses/update", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Updates the authenticating user's current status, also known as tweeting.</para>
        /// <para>To upload an image to accompany the tweet, use POST statuses/update_with_media.</para>
        /// <para>For each parameters attempt, the parameters text is compared with the authenticating user's recent tweets.</para>
        /// <para>Any attempt that would result in duplication will be blocked, resulting in a 403 error.</para>
        /// <para>Therefore, a user cannot submit the same status twice in a row.</para>
        /// <para>While not rate limited by the API a user is limited in the number of tweets they can create at a time.</para>
        /// <para>If the number of updates posted by the user reaches the current allowed limit this method will return an HTTP 403 error.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> status (required)</para>
        /// <para>- <c>long</c> in_reply_to_status_id (optional)</para>
        /// <para>- <c>double</c> lat (optional)</para>
        /// <para>- <c>double</c> long (optional)</para>
        /// <para>- <c>string</c> place_id (optional)</para>
        /// <para>- <c>bool</c> display_coordinates (optional)</para>
        /// <para>- <c>bool</c> trim_user (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The updated status.</returns>
        public Task<StatusResponse> Update<T>(T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<StatusResponse, T>(MethodType.Get, "statuses/update", parameters, cancellationToken);
        }

        #endif

        #if !(PCL || WIN_RT || WP)
        /// <summary>
        /// <para>Returns a single Tweet, specified by the id parameter.</para>
        /// <para>The Tweet's author will also be embedded within the tweet.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>int</c> id (requires)</para>
        /// <para>- <c>bool</c> trim_user (optional)</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The status.</returns>
        public StatusResponse Show(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessParameterReservedApi<StatusResponse>(MethodType.Get, "statuses/show/{id}", "id", InternalUtils.ExpressionsToDictionary(parameters));
        }

        /// <summary>
        /// <para>Returns a single Tweet, specified by the id parameter.</para>
        /// <para>The Tweet's author will also be embedded within the tweet.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>int</c> id (requires)</para>
        /// <para>- <c>bool</c> trim_user (optional)</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The status.</returns>
        public StatusResponse Show(IDictionary<string, object> parameters)
        {
            return this.Tokens.AccessParameterReservedApi<StatusResponse>(MethodType.Get, "statuses/show/{id}", "id", parameters);
        }

        /// <summary>
        /// <para>Returns a single Tweet, specified by the id parameter.</para>
        /// <para>The Tweet's author will also be embedded within the tweet.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>int</c> id (requires)</para>
        /// <para>- <c>bool</c> trim_user (optional)</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The status.</returns>
        public StatusResponse Show<T>(T parameters)
        {
            return this.Tokens.AccessParameterReservedApi<StatusResponse>(MethodType.Get, "statuses/show/{id}", "id", InternalUtils.ResolveObject(parameters));
        }

        #endif
        #if !NET35

        /// <summary>
        /// <para>Returns a single Tweet, specified by the id parameter.</para>
        /// <para>The Tweet's author will also be embedded within the tweet.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>int</c> id (requires)</para>
        /// <para>- <c>bool</c> trim_user (optional)</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The status.</returns>
        public Task<StatusResponse> Show(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessParameterReservedApiAsync<StatusResponse>(MethodType.Get, "statuses/show/{id}", "id", InternalUtils.ExpressionsToDictionary(parameters), CancellationToken.None);
        }

        /// <summary>
        /// <para>Returns a single Tweet, specified by the id parameter.</para>
        /// <para>The Tweet's author will also be embedded within the tweet.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>int</c> id (requires)</para>
        /// <para>- <c>bool</c> trim_user (optional)</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The status.</returns>
        public Task<StatusResponse> Show(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessParameterReservedApiAsync<StatusResponse>(MethodType.Get, "statuses/show/{id}", "id", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Returns a single Tweet, specified by the id parameter.</para>
        /// <para>The Tweet's author will also be embedded within the tweet.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>int</c> id (requires)</para>
        /// <para>- <c>bool</c> trim_user (optional)</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The status.</returns>
        public Task<StatusResponse> Show<T>(T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessParameterReservedApiAsync<StatusResponse>(MethodType.Get, "statuses/show/{id}", "id", InternalUtils.ResolveObject(parameters), cancellationToken);
        }

        #endif

        #if !(PCL || WIN_RT || WP)
        /// <summary>
        /// <para>Returns the 20 most recent mentions (tweets containing a users's &#64;screen_name) for the authenticating user.</para>
        /// <para>The timeline returned is the equivalent of the one seen when you view your mentions on twitter.com.</para>
        /// <para>This method can only return up to 800 tweets.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>int</c> count (optional)</para>
        /// <para>- <c>int</c> since_id (optional)</para>
        /// <para>- <c>int</c> max_id (optional)</para>
        /// <para>- <c>bool</c> trim_user (optional)</para>
        /// <para>- <c>bool</c> contributor_details (optional)</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The statuses.</returns>
        public ListedResponse<Status> MentionsTimeline(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiArray<Status>(MethodType.Get, "statuses/mentions_timeline", parameters);
        }

        /// <summary>
        /// <para>Returns the 20 most recent mentions (tweets containing a users's &#64;screen_name) for the authenticating user.</para>
        /// <para>The timeline returned is the equivalent of the one seen when you view your mentions on twitter.com.</para>
        /// <para>This method can only return up to 800 tweets.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>int</c> count (optional)</para>
        /// <para>- <c>int</c> since_id (optional)</para>
        /// <para>- <c>int</c> max_id (optional)</para>
        /// <para>- <c>bool</c> trim_user (optional)</para>
        /// <para>- <c>bool</c> contributor_details (optional)</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The statuses.</returns>
        public ListedResponse<Status> MentionsTimeline(IDictionary<string, object> parameters)
        {
            return this.Tokens.AccessApiArray<Status>(MethodType.Get, "statuses/mentions_timeline", parameters);
        }

        /// <summary>
        /// <para>Returns the 20 most recent mentions (tweets containing a users's &#64;screen_name) for the authenticating user.</para>
        /// <para>The timeline returned is the equivalent of the one seen when you view your mentions on twitter.com.</para>
        /// <para>This method can only return up to 800 tweets.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>int</c> count (optional)</para>
        /// <para>- <c>int</c> since_id (optional)</para>
        /// <para>- <c>int</c> max_id (optional)</para>
        /// <para>- <c>bool</c> trim_user (optional)</para>
        /// <para>- <c>bool</c> contributor_details (optional)</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The statuses.</returns>
        public ListedResponse<Status> MentionsTimeline<T>(T parameters)
        {
            return this.Tokens.AccessApiArray<Status, T>(MethodType.Get, "statuses/mentions_timeline", parameters);
        }

        #endif
        #if !NET35

        /// <summary>
        /// <para>Returns the 20 most recent mentions (tweets containing a users's &#64;screen_name) for the authenticating user.</para>
        /// <para>The timeline returned is the equivalent of the one seen when you view your mentions on twitter.com.</para>
        /// <para>This method can only return up to 800 tweets.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>int</c> count (optional)</para>
        /// <para>- <c>int</c> since_id (optional)</para>
        /// <para>- <c>int</c> max_id (optional)</para>
        /// <para>- <c>bool</c> trim_user (optional)</para>
        /// <para>- <c>bool</c> contributor_details (optional)</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The statuses.</returns>
        public Task<ListedResponse<Status>> MentionsTimeline(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiArrayAsync<Status>(MethodType.Get, "statuses/mentions_timeline", parameters);
        }

        /// <summary>
        /// <para>Returns the 20 most recent mentions (tweets containing a users's &#64;screen_name) for the authenticating user.</para>
        /// <para>The timeline returned is the equivalent of the one seen when you view your mentions on twitter.com.</para>
        /// <para>This method can only return up to 800 tweets.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>int</c> count (optional)</para>
        /// <para>- <c>int</c> since_id (optional)</para>
        /// <para>- <c>int</c> max_id (optional)</para>
        /// <para>- <c>bool</c> trim_user (optional)</para>
        /// <para>- <c>bool</c> contributor_details (optional)</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The statuses.</returns>
        public Task<ListedResponse<Status>> MentionsTimeline(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiArrayAsync<Status>(MethodType.Get, "statuses/mentions_timeline", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Returns the 20 most recent mentions (tweets containing a users's &#64;screen_name) for the authenticating user.</para>
        /// <para>The timeline returned is the equivalent of the one seen when you view your mentions on twitter.com.</para>
        /// <para>This method can only return up to 800 tweets.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>int</c> count (optional)</para>
        /// <para>- <c>int</c> since_id (optional)</para>
        /// <para>- <c>int</c> max_id (optional)</para>
        /// <para>- <c>bool</c> trim_user (optional)</para>
        /// <para>- <c>bool</c> contributor_details (optional)</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The statuses.</returns>
        public Task<ListedResponse<Status>> MentionsTimeline<T>(T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiArrayAsync<Status, T>(MethodType.Get, "statuses/mentions_timeline", parameters, cancellationToken);
        }

        #endif

        #if !(PCL || WIN_RT || WP)
        /// <summary>
        /// <para>Returns up to 100 of the first retweets of a given tweet.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>int</c> id (required)</para>
        /// <para>- <c>bool</c> trim_user (optional)</para>
        /// <para>- <c>bool</c> count (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The statuses.</returns>
        public ListedResponse<Status> Retweets(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessParameterReservedApiArray<Status>(MethodType.Get, "statuses/retweets/{id}", "id", InternalUtils.ExpressionsToDictionary(parameters));
        }

        /// <summary>
        /// <para>Returns up to 100 of the first retweets of a given tweet.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>int</c> id (required)</para>
        /// <para>- <c>bool</c> trim_user (optional)</para>
        /// <para>- <c>bool</c> count (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The statuses.</returns>
        public ListedResponse<Status> Retweets(IDictionary<string, object> parameters)
        {
            return this.Tokens.AccessParameterReservedApiArray<Status>(MethodType.Get, "statuses/retweets/{id}", "id", parameters);
        }

        /// <summary>
        /// <para>Returns up to 100 of the first retweets of a given tweet.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>int</c> id (required)</para>
        /// <para>- <c>bool</c> trim_user (optional)</para>
        /// <para>- <c>bool</c> count (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The statuses.</returns>
        public ListedResponse<Status> Retweets<T>(T parameters)
        {
            return this.Tokens.AccessParameterReservedApiArray<Status>(MethodType.Get, "statuses/retweets/{id}", "id", InternalUtils.ResolveObject(parameters));
        }

        #endif
        #if !NET35

        /// <summary>
        /// <para>Returns up to 100 of the first retweets of a given tweet.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>int</c> id (required)</para>
        /// <para>- <c>bool</c> trim_user (optional)</para>
        /// <para>- <c>bool</c> count (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The statuses.</returns>
        public Task<ListedResponse<Status>> Retweets(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessParameterReservedApiArrayAsync<Status>(MethodType.Get, "statuses/retweets/{id}", "id", InternalUtils.ExpressionsToDictionary(parameters), CancellationToken.None);
        }

        /// <summary>
        /// <para>Returns up to 100 of the first retweets of a given tweet.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>int</c> id (required)</para>
        /// <para>- <c>bool</c> trim_user (optional)</para>
        /// <para>- <c>bool</c> count (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The statuses.</returns>
        public Task<ListedResponse<Status>> Retweets(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessParameterReservedApiArrayAsync<Status>(MethodType.Get, "statuses/retweets/{id}", "id", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Returns up to 100 of the first retweets of a given tweet.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>int</c> id (required)</para>
        /// <para>- <c>bool</c> trim_user (optional)</para>
        /// <para>- <c>bool</c> count (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The statuses.</returns>
        public Task<ListedResponse<Status>> Retweets<T>(T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessParameterReservedApiArrayAsync<Status>(MethodType.Get, "statuses/retweets/{id}", "id", InternalUtils.ResolveObject(parameters), cancellationToken);
        }

        #endif

        #if !(PCL || WIN_RT || WP)
        /// <summary>
        /// <para>Returns a collection of numeric IDs for every user who has a pending request to follow the authenticating user.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> cursor (semi-optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The IDs.</returns>
        public Cursored<long> Incoming(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApi<Cursored<long>>(MethodType.Get, "friendships/incoming", parameters);
        }

        /// <summary>
        /// <para>Returns a collection of numeric IDs for every user who has a pending request to follow the authenticating user.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> cursor (semi-optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The IDs.</returns>
        public Cursored<long> Incoming(IDictionary<string, object> parameters)
        {
            return this.Tokens.AccessApi<Cursored<long>>(MethodType.Get, "friendships/incoming", parameters);
        }

        /// <summary>
        /// <para>Returns a collection of numeric IDs for every user who has a pending request to follow the authenticating user.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> cursor (semi-optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The IDs.</returns>
        public Cursored<long> Incoming<T>(T parameters)
        {
            return this.Tokens.AccessApi<Cursored<long>, T>(MethodType.Get, "friendships/incoming", parameters);
        }

        /// <summary>
        /// <para>Returns a collection of numeric IDs for every user who has a pending request to follow the authenticating user.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> cursor (semi-optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The IDs.</returns>
        Cursored<long> EnumerateIncoming(EnumerateMode mode, params Expression<Func<string, object>>[] parameters)
        {
            return Cursored<long>.Enumerate(this.Tokens, "friendships/incoming", mode, parameters);
        }

        /// <summary>
        /// <para>Returns a collection of numeric IDs for every user who has a pending request to follow the authenticating user.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> cursor (semi-optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The IDs.</returns>
        Cursored<long> EnumerateIncoming(EnumerateMode mode, params IDictionary<string, object> parameters)
        {
            return Cursored<long>.Enumerate(this.Tokens, "friendships/incoming", mode, parameters);
        }

        /// <summary>
        /// <para>Returns a collection of numeric IDs for every user who has a pending request to follow the authenticating user.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> cursor (semi-optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The IDs.</returns>
        Cursored<long> EnumerateIncoming<T>(EnumerateMode mode, T parameters)
        {
            return Cursored<long>.Enumerate(this.Tokens, "friendships/incoming", mode, parameters);
        }

        #endif
        #if !NET35

        /// <summary>
        /// <para>Returns a collection of numeric IDs for every user who has a pending request to follow the authenticating user.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> cursor (semi-optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The IDs.</returns>
        public Task<Cursored<long>> Incoming(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiAsync<Cursored<long>>(MethodType.Get, "friendships/incoming", parameters);
        }

        /// <summary>
        /// <para>Returns a collection of numeric IDs for every user who has a pending request to follow the authenticating user.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> cursor (semi-optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The IDs.</returns>
        public Task<Cursored<long>> Incoming(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<Cursored<long>>(MethodType.Get, "friendships/incoming", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Returns a collection of numeric IDs for every user who has a pending request to follow the authenticating user.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> cursor (semi-optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The IDs.</returns>
        public Task<Cursored<long>> Incoming<T>(T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<Cursored<long>, T>(MethodType.Get, "friendships/incoming", parameters, cancellationToken);
        }

        #endif

        #if !(PCL || WIN_RT || WP)
        /// <summary>
        /// <para>Updates the authenticating user's current status, uploading an image.</para>
        /// <para>For each parameters attempt, the parameters text is compared with the authenticating user's recent tweets.</para>
        /// <para>Any attempt that would result in duplication will be blocked, resulting in a 403 error.</para>
        /// <para>Therefore, a user cannot submit the same status twice in a row.</para>
        /// <para>While not rate limited by the API a user is limited in the number of tweets they can create at a time.</para>
        /// <para>If the number of updates posted by the user reaches the current allowed limit this method will return an HTTP 403 error.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> status (required)</para>
        /// <para>- <c>Stream</c> / <c>IEnumerable&lt;byte&gt;</c> / <c>FileInfo</c> media (required)</para>
        /// <para>- <c>bool</c> possibly_sensitive (optional)</para>
        /// <para>- <c>long</c> in_reply_to_status_id (optional)</para>
        /// <para>- <c>double</c> lat (optional)</para>
        /// <para>- <c>double</c> long (optional)</para>
        /// <para>- <c>string</c> place_id (optional)</para>
        /// <para>- <c>bool</c> display_coordinates (optional)</para>
        /// <para>- <c>bool</c> trim_user (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The updated status.</returns>
        [Obsolete("Use Media.Upload and Statuses.Update.")]
        public StatusResponse UpdateWithMedia(params Expression<Func<string, object>>[] parameters)
        {
            return this.UpdateWithMediaImpl(InternalUtils.ExpressionsToDictionary(parameters));
        }
        
        /// <summary>
        /// <para>Updates the authenticating user's current status, uploading an image.</para>
        /// <para>For each parameters attempt, the parameters text is compared with the authenticating user's recent tweets.</para>
        /// <para>Any attempt that would result in duplication will be blocked, resulting in a 403 error.</para>
        /// <para>Therefore, a user cannot submit the same status twice in a row.</para>
        /// <para>While not rate limited by the API a user is limited in the number of tweets they can create at a time.</para>
        /// <para>If the number of updates posted by the user reaches the current allowed limit this method will return an HTTP 403 error.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> status (required)</para>
        /// <para>- <c>Stream</c> / <c>IEnumerable&lt;byte&gt;</c> / <c>FileInfo</c> media (required)</para>
        /// <para>- <c>bool</c> possibly_sensitive (optional)</para>
        /// <para>- <c>long</c> in_reply_to_status_id (optional)</para>
        /// <para>- <c>double</c> lat (optional)</para>
        /// <para>- <c>double</c> long (optional)</para>
        /// <para>- <c>string</c> place_id (optional)</para>
        /// <para>- <c>bool</c> display_coordinates (optional)</para>
        /// <para>- <c>bool</c> trim_user (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The updated status.</returns>
        [Obsolete("Use Media.Upload and Statuses.Update.")]
        public StatusResponse UpdateWithMedia(IDictionary<string, object> parameters)
        {
            return this.UpdateWithMediaImpl(parameters);
        }
        
        /// <summary>
        /// <para>Updates the authenticating user's current status, uploading an image.</para>
        /// <para>For each parameters attempt, the parameters text is compared with the authenticating user's recent tweets.</para>
        /// <para>Any attempt that would result in duplication will be blocked, resulting in a 403 error.</para>
        /// <para>Therefore, a user cannot submit the same status twice in a row.</para>
        /// <para>While not rate limited by the API a user is limited in the number of tweets they can create at a time.</para>
        /// <para>If the number of updates posted by the user reaches the current allowed limit this method will return an HTTP 403 error.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> status (required)</para>
        /// <para>- <c>Stream</c> / <c>IEnumerable&lt;byte&gt;</c> / <c>FileInfo</c> media (required)</para>
        /// <para>- <c>bool</c> possibly_sensitive (optional)</para>
        /// <para>- <c>long</c> in_reply_to_status_id (optional)</para>
        /// <para>- <c>double</c> lat (optional)</para>
        /// <para>- <c>double</c> long (optional)</para>
        /// <para>- <c>string</c> place_id (optional)</para>
        /// <para>- <c>bool</c> display_coordinates (optional)</para>
        /// <para>- <c>bool</c> trim_user (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The updated status.</returns>
        [Obsolete("Use Media.Upload and Statuses.Update.")]
        public StatusResponse UpdateWithMedia<T>(T parameters)
        {
            return this.UpdateWithMediaImpl(InternalUtils.ResolveObject(parameters));
        }
        
        private StatusResponse UpdateWithMediaImpl(IEnumerable<KeyValuePair<string, object>> parameters)
        {
            if(parameters == null) throw new ArgumentNullException("parameters");
            var list = parameters.ToList();
            list.Where(kvp => kvp.Key == "media").ToArray().ForEach(kvp =>
            {
                list.Remove(kvp);
                list.Add(new KeyValuePair<string, object>("media[]", kvp.Value));
            });
            return this.Tokens.AccessApiImpl<StatusResponse>(MethodType.Post, "statuses/update_with_media", list, "");
        }
        #endif

    }
}