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
using System.IO;
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
        public StatusResponse Update(string status, long? in_reply_to_status_id, double? lat, double? @long, string place_id, bool? display_coordinates, bool? trim_user)
        {
            var parameters = new Dictionary<string, object>();
            if(status != null) parameters.Add("status", status);
            if(in_reply_to_status_id != null) parameters.Add("in_reply_to_status_id", in_reply_to_status_id);
            if(lat != null) parameters.Add("lat", lat);
            if(@long != null) parameters.Add("long", @long);
            if(place_id != null) parameters.Add("place_id", place_id);
            if(display_coordinates != null) parameters.Add("display_coordinates", display_coordinates);
            if(trim_user != null) parameters.Add("trim_user", trim_user);
            return this.Tokens.AccessApi<StatusResponse>(MethodType.Get, "statuses/update", parameters);
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
        public Task<StatusResponse> UpdateAsync(params Expression<Func<string, object>>[] parameters)
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
        public Task<StatusResponse> UpdateAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
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
        public Task<StatusResponse> UpdateAsync<T>(T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<StatusResponse, T>(MethodType.Get, "statuses/update", parameters, cancellationToken);
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
        public Task<StatusResponse> UpdateAsync(string status, long? in_reply_to_status_id, double? lat, double? @long, string place_id, bool? display_coordinates, bool? trim_user, CancellationToken cancellationToken = default(CancellationToken))
        {
            var parameters = new Dictionary<string, object>();
            if(status != null) parameters.Add("status", status);
            if(in_reply_to_status_id != null) parameters.Add("in_reply_to_status_id", in_reply_to_status_id);
            if(lat != null) parameters.Add("lat", lat);
            if(@long != null) parameters.Add("long", @long);
            if(place_id != null) parameters.Add("place_id", place_id);
            if(display_coordinates != null) parameters.Add("display_coordinates", display_coordinates);
            if(trim_user != null) parameters.Add("trim_user", trim_user);
            return this.Tokens.AccessApiAsync<StatusResponse>(MethodType.Get, "statuses/update", parameters, cancellationToken);
        }

        #endif

        #if !(PCL || WIN_RT || WP)
        /// <summary>
        /// <para>Returns a single Tweet, specified by the id parameter.</para>
        /// <para>The Tweet's author will also be embedded within the tweet.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>int</c> id (required)</para>
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
        /// <para>- <c>int</c> id (required)</para>
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
        /// <para>- <c>int</c> id (required)</para>
        /// <para>- <c>bool</c> trim_user (optional)</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The status.</returns>
        public StatusResponse Show<T>(T parameters)
        {
            return this.Tokens.AccessParameterReservedApi<StatusResponse>(MethodType.Get, "statuses/show/{id}", "id", InternalUtils.ResolveObject(parameters));
        }

        /// <summary>
        /// <para>Returns a single Tweet, specified by the id parameter.</para>
        /// <para>The Tweet's author will also be embedded within the tweet.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>int</c> id (required)</para>
        /// <para>- <c>bool</c> trim_user (optional)</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The status.</returns>
        public StatusResponse Show(int id, bool? trim_user, bool? include_entities)
        {
            var parameters = new Dictionary<string, object>();
            parameters.Add("id", id);
            if(trim_user != null) parameters.Add("trim_user", trim_user);
            if(include_entities != null) parameters.Add("include_entities", include_entities);
            return this.Tokens.AccessParameterReservedApi<StatusResponse>(MethodType.Get, "statuses/show/{id}", "id", parameters);
        }

        #endif
        #if !NET35

        /// <summary>
        /// <para>Returns a single Tweet, specified by the id parameter.</para>
        /// <para>The Tweet's author will also be embedded within the tweet.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>int</c> id (required)</para>
        /// <para>- <c>bool</c> trim_user (optional)</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The status.</returns>
        public Task<StatusResponse> ShowAsync(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessParameterReservedApiAsync<StatusResponse>(MethodType.Get, "statuses/show/{id}", "id", InternalUtils.ExpressionsToDictionary(parameters), CancellationToken.None);
        }

        /// <summary>
        /// <para>Returns a single Tweet, specified by the id parameter.</para>
        /// <para>The Tweet's author will also be embedded within the tweet.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>int</c> id (required)</para>
        /// <para>- <c>bool</c> trim_user (optional)</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The status.</returns>
        public Task<StatusResponse> ShowAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessParameterReservedApiAsync<StatusResponse>(MethodType.Get, "statuses/show/{id}", "id", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Returns a single Tweet, specified by the id parameter.</para>
        /// <para>The Tweet's author will also be embedded within the tweet.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>int</c> id (required)</para>
        /// <para>- <c>bool</c> trim_user (optional)</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The status.</returns>
        public Task<StatusResponse> ShowAsync<T>(T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessParameterReservedApiAsync<StatusResponse>(MethodType.Get, "statuses/show/{id}", "id", InternalUtils.ResolveObject(parameters), cancellationToken);
        }

        /// <summary>
        /// <para>Returns a single Tweet, specified by the id parameter.</para>
        /// <para>The Tweet's author will also be embedded within the tweet.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>int</c> id (required)</para>
        /// <para>- <c>bool</c> trim_user (optional)</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The status.</returns>
        public Task<StatusResponse> ShowAsync(int id, bool? trim_user, bool? include_entities, CancellationToken cancellationToken = default(CancellationToken))
        {
            var parameters = new Dictionary<string, object>();
            parameters.Add("id", id);
            if(trim_user != null) parameters.Add("trim_user", trim_user);
            if(include_entities != null) parameters.Add("include_entities", include_entities);
            return this.Tokens.AccessParameterReservedApiAsync<StatusResponse>(MethodType.Get, "statuses/show/{id}", "id", parameters, cancellationToken);
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
        public ListedResponse<Status> MentionsTimeline(int? count, int? since_id, int? max_id, bool? trim_user, bool? contributor_details, bool? include_entities)
        {
            var parameters = new Dictionary<string, object>();
            if(count != null) parameters.Add("count", count);
            if(since_id != null) parameters.Add("since_id", since_id);
            if(max_id != null) parameters.Add("max_id", max_id);
            if(trim_user != null) parameters.Add("trim_user", trim_user);
            if(contributor_details != null) parameters.Add("contributor_details", contributor_details);
            if(include_entities != null) parameters.Add("include_entities", include_entities);
            return this.Tokens.AccessApiArray<Status>(MethodType.Get, "statuses/mentions_timeline", parameters);
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
        public Task<ListedResponse<Status>> MentionsTimelineAsync(params Expression<Func<string, object>>[] parameters)
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
        public Task<ListedResponse<Status>> MentionsTimelineAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
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
        public Task<ListedResponse<Status>> MentionsTimelineAsync<T>(T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiArrayAsync<Status, T>(MethodType.Get, "statuses/mentions_timeline", parameters, cancellationToken);
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
        public Task<ListedResponse<Status>> MentionsTimelineAsync(int? count, int? since_id, int? max_id, bool? trim_user, bool? contributor_details, bool? include_entities, CancellationToken cancellationToken = default(CancellationToken))
        {
            var parameters = new Dictionary<string, object>();
            if(count != null) parameters.Add("count", count);
            if(since_id != null) parameters.Add("since_id", since_id);
            if(max_id != null) parameters.Add("max_id", max_id);
            if(trim_user != null) parameters.Add("trim_user", trim_user);
            if(contributor_details != null) parameters.Add("contributor_details", contributor_details);
            if(include_entities != null) parameters.Add("include_entities", include_entities);
            return this.Tokens.AccessApiArrayAsync<Status>(MethodType.Get, "statuses/mentions_timeline", parameters, cancellationToken);
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

        /// <summary>
        /// <para>Returns up to 100 of the first retweets of a given tweet.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>int</c> id (required)</para>
        /// <para>- <c>bool</c> trim_user (optional)</para>
        /// <para>- <c>bool</c> count (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The statuses.</returns>
        public ListedResponse<Status> Retweets(int id, bool? trim_user, bool? count)
        {
            var parameters = new Dictionary<string, object>();
            parameters.Add("id", id);
            if(trim_user != null) parameters.Add("trim_user", trim_user);
            if(count != null) parameters.Add("count", count);
            return this.Tokens.AccessParameterReservedApiArray<Status>(MethodType.Get, "statuses/retweets/{id}", "id", parameters);
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
        public Task<ListedResponse<Status>> RetweetsAsync(params Expression<Func<string, object>>[] parameters)
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
        public Task<ListedResponse<Status>> RetweetsAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
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
        public Task<ListedResponse<Status>> RetweetsAsync<T>(T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessParameterReservedApiArrayAsync<Status>(MethodType.Get, "statuses/retweets/{id}", "id", InternalUtils.ResolveObject(parameters), cancellationToken);
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
        public Task<ListedResponse<Status>> RetweetsAsync(int id, bool? trim_user, bool? count, CancellationToken cancellationToken = default(CancellationToken))
        {
            var parameters = new Dictionary<string, object>();
            parameters.Add("id", id);
            if(trim_user != null) parameters.Add("trim_user", trim_user);
            if(count != null) parameters.Add("count", count);
            return this.Tokens.AccessParameterReservedApiArrayAsync<Status>(MethodType.Get, "statuses/retweets/{id}", "id", parameters, cancellationToken);
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
        IEnumerable<long> EnumerateIncoming(EnumerateMode mode, long? cursor)
        {
            var parameters = new Dictionary<string, object>();
            if(cursor != null) parameters.Add("cursor", cursor);
            return Cursored<long>.Enumerate(this.Tokens, "friendships/incoming", mode, parameters);
        }

        /// <summary>
        /// <para>Returns a collection of numeric IDs for every user who has a pending request to follow the authenticating user.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> cursor (semi-optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The IDs.</returns>
        public Cursored<long> Incoming(long? cursor)
        {
            var parameters = new Dictionary<string, object>();
            if(cursor != null) parameters.Add("cursor", cursor);
            return this.Tokens.AccessApi<Cursored<long>>(MethodType.Get, "friendships/incoming", parameters);
        }

        /// <summary>
        /// <para>Returns a collection of numeric IDs for every user who has a pending request to follow the authenticating user.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> cursor (semi-optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The IDs.</returns>
        IEnumerable<long> EnumerateIncoming(EnumerateMode mode, params Expression<Func<string, object>>[] parameters)
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
        IEnumerable<long> EnumerateIncoming(EnumerateMode mode, params IDictionary<string, object> parameters)
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
        IEnumerable<long> EnumerateIncoming<T>(EnumerateMode mode, T parameters)
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
        public Task<Cursored<long>> IncomingAsync(params Expression<Func<string, object>>[] parameters)
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
        public Task<Cursored<long>> IncomingAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
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
        public Task<Cursored<long>> IncomingAsync<T>(T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<Cursored<long>, T>(MethodType.Get, "friendships/incoming", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Returns a collection of numeric IDs for every user who has a pending request to follow the authenticating user.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> cursor (semi-optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The IDs.</returns>
        public Task<Cursored<long>> IncomingAsync(long? cursor, CancellationToken cancellationToken = default(CancellationToken))
        {
            var parameters = new Dictionary<string, object>();
            if(cursor != null) parameters.Add("cursor", cursor);
            return this.Tokens.AccessApiAsync<Cursored<long>>(MethodType.Get, "friendships/incoming", parameters, cancellationToken);
        }

        #endif

        #if !(PCL || WIN_RT || WP)
        /// <summary>
        /// <para>Returns a variety of information about the user specified by the required user_id or screen_name parameter.</para>
        /// <para>The author's most recent Tweet will be returned inline when possible.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> user_id (any one is required)</para>
        /// <para>- <c>string</c> screen_name (any one is required)</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The user.</returns>
        public UserResponse Show(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApi<UserResponse>(MethodType.Get, "users/show", parameters);
        }

        /// <summary>
        /// <para>Returns a variety of information about the user specified by the required user_id or screen_name parameter.</para>
        /// <para>The author's most recent Tweet will be returned inline when possible.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> user_id (any one is required)</para>
        /// <para>- <c>string</c> screen_name (any one is required)</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The user.</returns>
        public UserResponse Show(IDictionary<string, object> parameters)
        {
            return this.Tokens.AccessApi<UserResponse>(MethodType.Get, "users/show", parameters);
        }

        /// <summary>
        /// <para>Returns a variety of information about the user specified by the required user_id or screen_name parameter.</para>
        /// <para>The author's most recent Tweet will be returned inline when possible.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> user_id (any one is required)</para>
        /// <para>- <c>string</c> screen_name (any one is required)</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The user.</returns>
        public UserResponse Show<T>(T parameters)
        {
            return this.Tokens.AccessApi<UserResponse, T>(MethodType.Get, "users/show", parameters);
        }

        /// <summary>
        /// <para>Returns a variety of information about the user specified by the required user_id or screen_name parameter.</para>
        /// <para>The author's most recent Tweet will be returned inline when possible.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> user_id (any one is required)</para>
        /// <para>- <c>string</c> screen_name (any one is required)</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The user.</returns>
        public UserResponse Show(long user_id, bool? include_entities)
        {
            var parameters = new Dictionary<string, object>();
            parameters.Add("user_id", user_id);
            if(include_entities != null) parameters.Add("include_entities", include_entities);
            return this.Tokens.AccessApi<UserResponse>(MethodType.Get, "users/show", parameters);
        }

        /// <summary>
        /// <para>Returns a variety of information about the user specified by the required user_id or screen_name parameter.</para>
        /// <para>The author's most recent Tweet will be returned inline when possible.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> user_id (any one is required)</para>
        /// <para>- <c>string</c> screen_name (any one is required)</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The user.</returns>
        public UserResponse Show(string screen_name, bool? include_entities)
        {
            var parameters = new Dictionary<string, object>();
            if(screen_name == null) throw new ArgumentNullException("A required argument 'screen_name' must not be null");
            else parameters.Add("screen_name", screen_name);
            if(include_entities != null) parameters.Add("include_entities", include_entities);
            return this.Tokens.AccessApi<UserResponse>(MethodType.Get, "users/show", parameters);
        }

        #endif
        #if !NET35

        /// <summary>
        /// <para>Returns a variety of information about the user specified by the required user_id or screen_name parameter.</para>
        /// <para>The author's most recent Tweet will be returned inline when possible.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> user_id (any one is required)</para>
        /// <para>- <c>string</c> screen_name (any one is required)</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The user.</returns>
        public Task<UserResponse> ShowAsync(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiAsync<UserResponse>(MethodType.Get, "users/show", parameters);
        }

        /// <summary>
        /// <para>Returns a variety of information about the user specified by the required user_id or screen_name parameter.</para>
        /// <para>The author's most recent Tweet will be returned inline when possible.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> user_id (any one is required)</para>
        /// <para>- <c>string</c> screen_name (any one is required)</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The user.</returns>
        public Task<UserResponse> ShowAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<UserResponse>(MethodType.Get, "users/show", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Returns a variety of information about the user specified by the required user_id or screen_name parameter.</para>
        /// <para>The author's most recent Tweet will be returned inline when possible.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> user_id (any one is required)</para>
        /// <para>- <c>string</c> screen_name (any one is required)</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The user.</returns>
        public Task<UserResponse> ShowAsync<T>(T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<UserResponse, T>(MethodType.Get, "users/show", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Returns a variety of information about the user specified by the required user_id or screen_name parameter.</para>
        /// <para>The author's most recent Tweet will be returned inline when possible.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> user_id (any one is required)</para>
        /// <para>- <c>string</c> screen_name (any one is required)</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The user.</returns>
        public Task<UserResponse> ShowAsync(long user_id, bool? include_entities, CancellationToken cancellationToken = default(CancellationToken))
        {
            var parameters = new Dictionary<string, object>();
            parameters.Add("user_id", user_id);
            if(include_entities != null) parameters.Add("include_entities", include_entities);
            return this.Tokens.AccessApiAsync<UserResponse>(MethodType.Get, "users/show", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Returns a variety of information about the user specified by the required user_id or screen_name parameter.</para>
        /// <para>The author's most recent Tweet will be returned inline when possible.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> user_id (any one is required)</para>
        /// <para>- <c>string</c> screen_name (any one is required)</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The user.</returns>
        public Task<UserResponse> ShowAsync(string screen_name, bool? include_entities, CancellationToken cancellationToken = default(CancellationToken))
        {
            var parameters = new Dictionary<string, object>();
            if(screen_name == null) throw new ArgumentNullException("A required argument 'screen_name' must not be null");
            else parameters.Add("screen_name", screen_name);
            if(include_entities != null) parameters.Add("include_entities", include_entities);
            return this.Tokens.AccessApiAsync<UserResponse>(MethodType.Get, "users/show", parameters, cancellationToken);
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
        /// <para>- <c>Stream</c> media (any one is required)</para>
        /// <para>- <c>IEnumerable&lt;byte&gt;</c> media (any one is required)</para>
        /// <para>- <c>FileInfo</c> media (any one is required)</para>
        /// <para>- <c>bool</c> possibly_sensitive (optional)</para>
        /// <para>- <c>long</c> in_reply_to_status_id (optional)</para>
        /// <para>- <c>double</c> lat (optional)</para>
        /// <para>- <c>double</c> long (optional)</para>
        /// <para>- <c>string</c> place_id (optional)</para>
        /// <para>- <c>bool</c> display_coordinates (optional)</para>
        /// <para>- <c>bool</c> trim_user (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
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
        /// <para>- <c>Stream</c> media (any one is required)</para>
        /// <para>- <c>IEnumerable&lt;byte&gt;</c> media (any one is required)</para>
        /// <para>- <c>FileInfo</c> media (any one is required)</para>
        /// <para>- <c>bool</c> possibly_sensitive (optional)</para>
        /// <para>- <c>long</c> in_reply_to_status_id (optional)</para>
        /// <para>- <c>double</c> lat (optional)</para>
        /// <para>- <c>double</c> long (optional)</para>
        /// <para>- <c>string</c> place_id (optional)</para>
        /// <para>- <c>bool</c> display_coordinates (optional)</para>
        /// <para>- <c>bool</c> trim_user (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
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
        /// <para>- <c>Stream</c> media (any one is required)</para>
        /// <para>- <c>IEnumerable&lt;byte&gt;</c> media (any one is required)</para>
        /// <para>- <c>FileInfo</c> media (any one is required)</para>
        /// <para>- <c>bool</c> possibly_sensitive (optional)</para>
        /// <para>- <c>long</c> in_reply_to_status_id (optional)</para>
        /// <para>- <c>double</c> lat (optional)</para>
        /// <para>- <c>double</c> long (optional)</para>
        /// <para>- <c>string</c> place_id (optional)</para>
        /// <para>- <c>bool</c> display_coordinates (optional)</para>
        /// <para>- <c>bool</c> trim_user (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        [Obsolete("Use Media.Upload and Statuses.Update.")]
        public StatusResponse UpdateWithMedia<T>(T parameters)
        {
            return this.UpdateWithMediaImpl(InternalUtils.ResolveObject(parameters));
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
        /// <para>- <c>Stream</c> media (any one is required)</para>
        /// <para>- <c>IEnumerable&lt;byte&gt;</c> media (any one is required)</para>
        /// <para>- <c>FileInfo</c> media (any one is required)</para>
        /// <para>- <c>bool</c> possibly_sensitive (optional)</para>
        /// <para>- <c>long</c> in_reply_to_status_id (optional)</para>
        /// <para>- <c>double</c> lat (optional)</para>
        /// <para>- <c>double</c> long (optional)</para>
        /// <para>- <c>string</c> place_id (optional)</para>
        /// <para>- <c>bool</c> display_coordinates (optional)</para>
        /// <para>- <c>bool</c> trim_user (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        [Obsolete("Use Media.Upload and Statuses.Update.")]
        public StatusResponse UpdateWithMedia(Stream media, string status, bool? possibly_sensitive, long? in_reply_to_status_id, double? lat, double? @long, string place_id, bool? display_coordinates, bool? trim_user)
        {
            var parameters = new Dictionary<string, object>();
            if(media == null) throw new ArgumentNullException("A required argument 'media' must not be null");
            else parameters.Add("media", media);
            if(status == null) throw new ArgumentNullException("A required argument 'status' must not be null");
            else parameters.Add("status", status);
            if(possibly_sensitive != null) parameters.Add("possibly_sensitive", possibly_sensitive);
            if(in_reply_to_status_id != null) parameters.Add("in_reply_to_status_id", in_reply_to_status_id);
            if(lat != null) parameters.Add("lat", lat);
            if(@long != null) parameters.Add("long", @long);
            if(place_id != null) parameters.Add("place_id", place_id);
            if(display_coordinates != null) parameters.Add("display_coordinates", display_coordinates);
            if(trim_user != null) parameters.Add("trim_user", trim_user);
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
        /// <para>- <c>Stream</c> media (any one is required)</para>
        /// <para>- <c>IEnumerable&lt;byte&gt;</c> media (any one is required)</para>
        /// <para>- <c>FileInfo</c> media (any one is required)</para>
        /// <para>- <c>bool</c> possibly_sensitive (optional)</para>
        /// <para>- <c>long</c> in_reply_to_status_id (optional)</para>
        /// <para>- <c>double</c> lat (optional)</para>
        /// <para>- <c>double</c> long (optional)</para>
        /// <para>- <c>string</c> place_id (optional)</para>
        /// <para>- <c>bool</c> display_coordinates (optional)</para>
        /// <para>- <c>bool</c> trim_user (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        [Obsolete("Use Media.Upload and Statuses.Update.")]
        public StatusResponse UpdateWithMedia(IEnumerable<byte> media, string status, bool? possibly_sensitive, long? in_reply_to_status_id, double? lat, double? @long, string place_id, bool? display_coordinates, bool? trim_user)
        {
            var parameters = new Dictionary<string, object>();
            if(media == null) throw new ArgumentNullException("A required argument 'media' must not be null");
            else parameters.Add("media", media);
            if(status == null) throw new ArgumentNullException("A required argument 'status' must not be null");
            else parameters.Add("status", status);
            if(possibly_sensitive != null) parameters.Add("possibly_sensitive", possibly_sensitive);
            if(in_reply_to_status_id != null) parameters.Add("in_reply_to_status_id", in_reply_to_status_id);
            if(lat != null) parameters.Add("lat", lat);
            if(@long != null) parameters.Add("long", @long);
            if(place_id != null) parameters.Add("place_id", place_id);
            if(display_coordinates != null) parameters.Add("display_coordinates", display_coordinates);
            if(trim_user != null) parameters.Add("trim_user", trim_user);
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
        /// <para>- <c>Stream</c> media (any one is required)</para>
        /// <para>- <c>IEnumerable&lt;byte&gt;</c> media (any one is required)</para>
        /// <para>- <c>FileInfo</c> media (any one is required)</para>
        /// <para>- <c>bool</c> possibly_sensitive (optional)</para>
        /// <para>- <c>long</c> in_reply_to_status_id (optional)</para>
        /// <para>- <c>double</c> lat (optional)</para>
        /// <para>- <c>double</c> long (optional)</para>
        /// <para>- <c>string</c> place_id (optional)</para>
        /// <para>- <c>bool</c> display_coordinates (optional)</para>
        /// <para>- <c>bool</c> trim_user (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        [Obsolete("Use Media.Upload and Statuses.Update.")]
        public StatusResponse UpdateWithMedia(FileInfo media, string status, bool? possibly_sensitive, long? in_reply_to_status_id, double? lat, double? @long, string place_id, bool? display_coordinates, bool? trim_user)
        {
            var parameters = new Dictionary<string, object>();
            if(media == null) throw new ArgumentNullException("A required argument 'media' must not be null");
            else parameters.Add("media", media);
            if(status == null) throw new ArgumentNullException("A required argument 'status' must not be null");
            else parameters.Add("status", status);
            if(possibly_sensitive != null) parameters.Add("possibly_sensitive", possibly_sensitive);
            if(in_reply_to_status_id != null) parameters.Add("in_reply_to_status_id", in_reply_to_status_id);
            if(lat != null) parameters.Add("lat", lat);
            if(@long != null) parameters.Add("long", @long);
            if(place_id != null) parameters.Add("place_id", place_id);
            if(display_coordinates != null) parameters.Add("display_coordinates", display_coordinates);
            if(trim_user != null) parameters.Add("trim_user", trim_user);
            return this.UpdateWithMediaImpl(parameters);
        }

        #endif
        #if !NET35

        /// <summary>
        /// <para>Updates the authenticating user's current status, uploading an image.</para>
        /// <para>For each parameters attempt, the parameters text is compared with the authenticating user's recent tweets.</para>
        /// <para>Any attempt that would result in duplication will be blocked, resulting in a 403 error.</para>
        /// <para>Therefore, a user cannot submit the same status twice in a row.</para>
        /// <para>While not rate limited by the API a user is limited in the number of tweets they can create at a time.</para>
        /// <para>If the number of updates posted by the user reaches the current allowed limit this method will return an HTTP 403 error.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> status (required)</para>
        /// <para>- <c>Stream</c> media (any one is required)</para>
        /// <para>- <c>IEnumerable&lt;byte&gt;</c> media (any one is required)</para>
        /// <para>- <c>FileInfo</c> media (any one is required)</para>
        /// <para>- <c>bool</c> possibly_sensitive (optional)</para>
        /// <para>- <c>long</c> in_reply_to_status_id (optional)</para>
        /// <para>- <c>double</c> lat (optional)</para>
        /// <para>- <c>double</c> long (optional)</para>
        /// <para>- <c>string</c> place_id (optional)</para>
        /// <para>- <c>bool</c> display_coordinates (optional)</para>
        /// <para>- <c>bool</c> trim_user (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public Task<StatusResponse> UpdateWithMediaAsync(params Expression<Func<string, object>>[] parameters)
        {
            return this.UpdateWithMediaAsyncImpl(InternalUtils.ExpressionsToDictionary(parameters), CancellationToken.None);
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
        /// <para>- <c>Stream</c> media (any one is required)</para>
        /// <para>- <c>IEnumerable&lt;byte&gt;</c> media (any one is required)</para>
        /// <para>- <c>FileInfo</c> media (any one is required)</para>
        /// <para>- <c>bool</c> possibly_sensitive (optional)</para>
        /// <para>- <c>long</c> in_reply_to_status_id (optional)</para>
        /// <para>- <c>double</c> lat (optional)</para>
        /// <para>- <c>double</c> long (optional)</para>
        /// <para>- <c>string</c> place_id (optional)</para>
        /// <para>- <c>bool</c> display_coordinates (optional)</para>
        /// <para>- <c>bool</c> trim_user (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public Task<StatusResponse> UpdateWithMediaAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.UpdateWithMediaAsyncImpl(parameters, cancellationToken);
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
        /// <para>- <c>Stream</c> media (any one is required)</para>
        /// <para>- <c>IEnumerable&lt;byte&gt;</c> media (any one is required)</para>
        /// <para>- <c>FileInfo</c> media (any one is required)</para>
        /// <para>- <c>bool</c> possibly_sensitive (optional)</para>
        /// <para>- <c>long</c> in_reply_to_status_id (optional)</para>
        /// <para>- <c>double</c> lat (optional)</para>
        /// <para>- <c>double</c> long (optional)</para>
        /// <para>- <c>string</c> place_id (optional)</para>
        /// <para>- <c>bool</c> display_coordinates (optional)</para>
        /// <para>- <c>bool</c> trim_user (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public Task<StatusResponse> UpdateWithMediaAsync<T>(T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.UpdateWithMediaAsyncImpl(InternalUtils.ResolveObject(parameters), cancellationToken);
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
        /// <para>- <c>Stream</c> media (any one is required)</para>
        /// <para>- <c>IEnumerable&lt;byte&gt;</c> media (any one is required)</para>
        /// <para>- <c>FileInfo</c> media (any one is required)</para>
        /// <para>- <c>bool</c> possibly_sensitive (optional)</para>
        /// <para>- <c>long</c> in_reply_to_status_id (optional)</para>
        /// <para>- <c>double</c> lat (optional)</para>
        /// <para>- <c>double</c> long (optional)</para>
        /// <para>- <c>string</c> place_id (optional)</para>
        /// <para>- <c>bool</c> display_coordinates (optional)</para>
        /// <para>- <c>bool</c> trim_user (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public Task<StatusResponse> UpdateWithMediaAsync(Stream media, string status, bool? possibly_sensitive, long? in_reply_to_status_id, double? lat, double? @long, string place_id, bool? display_coordinates, bool? trim_user, CancellationToken cancellationToken = default(CancellationToken))
        {
            var parameters = new Dictionary<string, object>();
            if(media == null) throw new ArgumentNullException("A required argument 'media' must not be null");
            else parameters.Add("media", media);
            if(status == null) throw new ArgumentNullException("A required argument 'status' must not be null");
            else parameters.Add("status", status);
            if(possibly_sensitive != null) parameters.Add("possibly_sensitive", possibly_sensitive);
            if(in_reply_to_status_id != null) parameters.Add("in_reply_to_status_id", in_reply_to_status_id);
            if(lat != null) parameters.Add("lat", lat);
            if(@long != null) parameters.Add("long", @long);
            if(place_id != null) parameters.Add("place_id", place_id);
            if(display_coordinates != null) parameters.Add("display_coordinates", display_coordinates);
            if(trim_user != null) parameters.Add("trim_user", trim_user);
            return this.UpdateWithMediaAsyncImpl(parameters, cancellationToken);
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
        /// <para>- <c>Stream</c> media (any one is required)</para>
        /// <para>- <c>IEnumerable&lt;byte&gt;</c> media (any one is required)</para>
        /// <para>- <c>FileInfo</c> media (any one is required)</para>
        /// <para>- <c>bool</c> possibly_sensitive (optional)</para>
        /// <para>- <c>long</c> in_reply_to_status_id (optional)</para>
        /// <para>- <c>double</c> lat (optional)</para>
        /// <para>- <c>double</c> long (optional)</para>
        /// <para>- <c>string</c> place_id (optional)</para>
        /// <para>- <c>bool</c> display_coordinates (optional)</para>
        /// <para>- <c>bool</c> trim_user (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public Task<StatusResponse> UpdateWithMediaAsync(IEnumerable<byte> media, string status, bool? possibly_sensitive, long? in_reply_to_status_id, double? lat, double? @long, string place_id, bool? display_coordinates, bool? trim_user, CancellationToken cancellationToken = default(CancellationToken))
        {
            var parameters = new Dictionary<string, object>();
            if(media == null) throw new ArgumentNullException("A required argument 'media' must not be null");
            else parameters.Add("media", media);
            if(status == null) throw new ArgumentNullException("A required argument 'status' must not be null");
            else parameters.Add("status", status);
            if(possibly_sensitive != null) parameters.Add("possibly_sensitive", possibly_sensitive);
            if(in_reply_to_status_id != null) parameters.Add("in_reply_to_status_id", in_reply_to_status_id);
            if(lat != null) parameters.Add("lat", lat);
            if(@long != null) parameters.Add("long", @long);
            if(place_id != null) parameters.Add("place_id", place_id);
            if(display_coordinates != null) parameters.Add("display_coordinates", display_coordinates);
            if(trim_user != null) parameters.Add("trim_user", trim_user);
            return this.UpdateWithMediaAsyncImpl(parameters, cancellationToken);
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
        /// <para>- <c>Stream</c> media (any one is required)</para>
        /// <para>- <c>IEnumerable&lt;byte&gt;</c> media (any one is required)</para>
        /// <para>- <c>FileInfo</c> media (any one is required)</para>
        /// <para>- <c>bool</c> possibly_sensitive (optional)</para>
        /// <para>- <c>long</c> in_reply_to_status_id (optional)</para>
        /// <para>- <c>double</c> lat (optional)</para>
        /// <para>- <c>double</c> long (optional)</para>
        /// <para>- <c>string</c> place_id (optional)</para>
        /// <para>- <c>bool</c> display_coordinates (optional)</para>
        /// <para>- <c>bool</c> trim_user (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public Task<StatusResponse> UpdateWithMediaAsync(FileInfo media, string status, bool? possibly_sensitive, long? in_reply_to_status_id, double? lat, double? @long, string place_id, bool? display_coordinates, bool? trim_user, CancellationToken cancellationToken = default(CancellationToken))
        {
            var parameters = new Dictionary<string, object>();
            if(media == null) throw new ArgumentNullException("A required argument 'media' must not be null");
            else parameters.Add("media", media);
            if(status == null) throw new ArgumentNullException("A required argument 'status' must not be null");
            else parameters.Add("status", status);
            if(possibly_sensitive != null) parameters.Add("possibly_sensitive", possibly_sensitive);
            if(in_reply_to_status_id != null) parameters.Add("in_reply_to_status_id", in_reply_to_status_id);
            if(lat != null) parameters.Add("lat", lat);
            if(@long != null) parameters.Add("long", @long);
            if(place_id != null) parameters.Add("place_id", place_id);
            if(display_coordinates != null) parameters.Add("display_coordinates", display_coordinates);
            if(trim_user != null) parameters.Add("trim_user", trim_user);
            return this.UpdateWithMediaAsyncImpl(parameters, cancellationToken);
        }

        #endif

        #if !(PCL || WIN_RT || WP)
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
        #if !NET35
        private Task<StatusResponse> UpdateWithMediaAsyncImpl(IEnumerable<KeyValuePair<string, object>> parameters, CancellationToken cancellationToken)
        {
            if(parameters == null) throw new ArgumentNullException("parameters");
            var list = parameters.ToList();
            list.Where(kvp => kvp.Key == "media").ToArray().ForEach(kvp =>
            {
                list.Remove(kvp);
                list.Add(new KeyValuePair<string, object>("media[]", kvp.Value));
            });
            return this.Tokens.AccessApiAsyncImpl<StatusResponse>(MethodType.Post, "statuses/update_with_media", list, cancellationToken, "");
        }
        #endif

    }
}