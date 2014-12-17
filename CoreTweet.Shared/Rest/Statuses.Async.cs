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
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using CoreTweet.Core;

namespace CoreTweet.Rest
{
    partial class Statuses
    {
        //GET Methods

        /// <summary>
        /// <para>Returns the 20 most recent mentions (tweets containing a users's &#64;screen_name) for the authenticating user as an asynchronous operation.</para>
        /// <para>The timeline returned is the equivalent of the one seen when you view your mentions on twitter.com.</para>
        /// <para>This method can only return up to 800 tweets.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>int</c> count (optional)</para>
        /// <para>- <c>long</c> since_id(optional)</para>
        /// <para>- <c>long</c> max_id (optional)</para>
        /// <para>- <c>bool</c> trim_user (optional)</para>
        /// <para>- <c>bool</c> contributor_details (optional)</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the statuses.</para>
        /// </returns>
        public Task<ListedResponse<Status>> MentionsTimelineAsync(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiArrayAsync<Status>(MethodType.Get, "statuses/mentions_timeline", parameters);
        }

        /// <summary>
        /// <para>Returns the 20 most recent mentions (tweets containing a users's &#64;screen_name) for the authenticating user as an asynchronous operation.</para>
        /// <para>The timeline returned is the equivalent of the one seen when you view your mentions on twitter.com.</para>
        /// <para>This method can only return up to 800 tweets.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>int</c> count (optional)</para>
        /// <para>- <c>long</c> since_id(optional)</para>
        /// <para>- <c>long</c> max_id (optional)</para>
        /// <para>- <c>bool</c> trim_user (optional)</para>
        /// <para>- <c>bool</c> contributor_details (optional)</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the statuses.</para>
        /// </returns>
        public Task<ListedResponse<Status>> MentionsTimelineAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiArrayAsync<Status>(MethodType.Get, "statuses/mentions_timeline", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Returns the 20 most recent mentions (tweets containing a users's &#64;screen_name) for the authenticating user as an asynchronous operation.</para>
        /// <para>The timeline returned is the equivalent of the one seen when you view your mentions on twitter.com.</para>
        /// <para>This method can only return up to 800 tweets.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>int</c> count (optional)</para>
        /// <para>- <c>long</c> since_id(optional)</para>
        /// <para>- <c>long</c> max_id (optional)</para>
        /// <para>- <c>bool</c> trim_user (optional)</para>
        /// <para>- <c>bool</c> contributor_details (optional)</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the statuses.</para>
        /// </returns>
        public Task<ListedResponse<Status>> MentionsTimelineAsync<T>(T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiArrayAsync<Status, T>(MethodType.Get, "statuses/mentions_timeline", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Returns a collection of the most recent Tweets posted by the user indicated by the screen_name or user_id parameters as an asynchronous operation.</para>
        /// <para>User timelines belonging to protected users may only be requested when the authenticated user either "owns" the timeline or is an approved follower of the owner.</para>
        /// <para>The timeline returned is the equivalent of the one seen when you view a user's profile on twitter.com.</para>
        /// <para>This method can only return up to 3,200 of a user's most recent Tweets.</para>
        /// <para>Native retweets of other statuses by the user is included in this total, regardless of whether include_rts is set to false when requesting this resource.</para>
        /// <para>Available parameters: </para>
        /// <para>- <c>long</c> user_id (optional)</para>
        /// <para>- <c>string</c> screen_name (optional)</para>
        /// <para>- <c>int</c> count (optional)</para>
        /// <para>- <c>long</c> since_id(optional)</para>
        /// <para>- <c>long</c> max_id (optional)</para>
        /// <para>- <c>bool</c> trim_user (optional)</para>
        /// <para>- <c>bool</c> contributor_details (optional)</para>
        /// <para>- <c>bool</c> include_rts (optional)</para>
        /// <para>- <c>bool</c> exclude_replies (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the statuses.</para>
        /// </returns>
        public Task<ListedResponse<Status>> UserTimelineAsync(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiArrayAsync<Status>(MethodType.Get, "statuses/user_timeline", parameters);
        }

        /// <summary>
        /// <para>Returns a collection of the most recent Tweets posted by the user indicated by the screen_name or user_id parameters as an asynchronous operation.</para>
        /// <para>User timelines belonging to protected users may only be requested when the authenticated user either "owns" the timeline or is an approved follower of the owner.</para>
        /// <para>The timeline returned is the equivalent of the one seen when you view a user's profile on twitter.com.</para>
        /// <para>This method can only return up to 3,200 of a user's most recent Tweets.</para>
        /// <para>Native retweets of other statuses by the user is included in this total, regardless of whether include_rts is set to false when requesting this resource.</para>
        /// <para>Available parameters: </para>
        /// <para>- <c>long</c> user_id (optional)</para>
        /// <para>- <c>string</c> screen_name (optional)</para>
        /// <para>- <c>int</c> count (optional)</para>
        /// <para>- <c>long</c> since_id(optional)</para>
        /// <para>- <c>long</c> max_id (optional)</para>
        /// <para>- <c>bool</c> trim_user (optional)</para>
        /// <para>- <c>bool</c> contributor_details (optional)</para>
        /// <para>- <c>bool</c> include_rts (optional)</para>
        /// <para>- <c>bool</c> exclude_replies (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the statuses.</para>
        /// </returns>
        public Task<ListedResponse<Status>> UserTimelineAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiArrayAsync<Status>(MethodType.Get, "statuses/user_timeline", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Returns a collection of the most recent Tweets posted by the user indicated by the screen_name or user_id parameters as an asynchronous operation.</para>
        /// <para>User timelines belonging to protected users may only be requested when the authenticated user either "owns" the timeline or is an approved follower of the owner.</para>
        /// <para>The timeline returned is the equivalent of the one seen when you view a user's profile on twitter.com.</para>
        /// <para>This method can only return up to 3,200 of a user's most recent Tweets.</para>
        /// <para>Native retweets of other statuses by the user is included in this total, regardless of whether include_rts is set to false when requesting this resource.</para>
        /// <para>Available parameters: </para>
        /// <para>- <c>long</c> user_id (optional)</para>
        /// <para>- <c>string</c> screen_name (optional)</para>
        /// <para>- <c>int</c> count (optional)</para>
        /// <para>- <c>long</c> since_id(optional)</para>
        /// <para>- <c>long</c> max_id (optional)</para>
        /// <para>- <c>bool</c> trim_user (optional)</para>
        /// <para>- <c>bool</c> contributor_details (optional)</para>
        /// <para>- <c>bool</c> include_rts (optional)</para>
        /// <para>- <c>bool</c> exclude_replies (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the statuses.</para>
        /// </returns>
        public Task<ListedResponse<Status>> UserTimelineAsync<T>(T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiArrayAsync<Status, T>(MethodType.Get, "statuses/user_timeline", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Returns a collection of the most recent Tweets and retweets posted by the authenticating user and the users they follow as an asynchronous operation.</para>
        /// <para>The home timeline is central to how most users interact with the Twitter service.</para>
        /// <para>Up to 800 Tweets are obtainable on the home timeline.</para>
        /// <para>It is more volatile for users that follow many users or follow users who tweet frequently.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>int</c> count (optional)</para>
        /// <para>- <c>long</c> since_id(optional)</para>
        /// <para>- <c>long</c> max_id (optional)</para>
        /// <para>- <c>bool</c> trim_user (optional)</para>
        /// <para>- <c>bool</c> contributor_details (optional)</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// <para>- <c>bool</c> exclude_replies (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the statuses.</para>
        /// </returns>
        public Task<ListedResponse<Status>> HomeTimelineAsync(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiArrayAsync<Status>(MethodType.Get, "statuses/home_timeline", parameters);
        }

        /// <summary>
        /// <para>Returns a collection of the most recent Tweets and retweets posted by the authenticating user and the users they follow as an asynchronous operation.</para>
        /// <para>The home timeline is central to how most users interact with the Twitter service.</para>
        /// <para>Up to 800 Tweets are obtainable on the home timeline.</para>
        /// <para>It is more volatile for users that follow many users or follow users who tweet frequently.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>int</c> count (optional)</para>
        /// <para>- <c>long</c> since_id(optional)</para>
        /// <para>- <c>long</c> max_id (optional)</para>
        /// <para>- <c>bool</c> trim_user (optional)</para>
        /// <para>- <c>bool</c> contributor_details (optional)</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// <para>- <c>bool</c> exclude_replies (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the statuses.</para>
        /// </returns>
        public Task<ListedResponse<Status>> HomeTimelineAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiArrayAsync<Status>(MethodType.Get, "statuses/home_timeline", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Returns a collection of the most recent Tweets and retweets posted by the authenticating user and the users they follow as an asynchronous operation.</para>
        /// <para>The home timeline is central to how most users interact with the Twitter service.</para>
        /// <para>Up to 800 Tweets are obtainable on the home timeline.</para>
        /// <para>It is more volatile for users that follow many users or follow users who tweet frequently.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>int</c> count (optional)</para>
        /// <para>- <c>long</c> since_id(optional)</para>
        /// <para>- <c>long</c> max_id (optional)</para>
        /// <para>- <c>bool</c> trim_user (optional)</para>
        /// <para>- <c>bool</c> contributor_details (optional)</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// <para>- <c>bool</c> exclude_replies (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the statuses.</para>
        /// </returns>
        public Task<ListedResponse<Status>> HomeTimelineAsync<T>(T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiArrayAsync<Status, T>(MethodType.Get, "statuses/home_timeline", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Returns the most recent tweets authored by the authenticating user that have recently been retweeted by others as an asynchronous operation.</para>
        /// <para>This timeline is a subset of the user's GET statuses/user_timeline.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>int</c> count (optional)</para>
        /// <para>- <c>long</c> since_id(optional)</para>
        /// <para>- <c>long</c> max_id (optional)</para>
        /// <para>- <c>bool</c> trim_user (optional)</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// <para>- <c>bool</c> include_user_entities (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the statuses.</para>
        /// </returns>
        public Task<ListedResponse<Status>> RetweetsOfMeAsync(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiArrayAsync<Status>(MethodType.Get, "statuses/retweets_of_me", parameters);
        }

        /// <summary>
        /// <para>Returns the most recent tweets authored by the authenticating user that have recently been retweeted by others as an asynchronous operation.</para>
        /// <para>This timeline is a subset of the user's GET statuses/user_timeline.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>int</c> count (optional)</para>
        /// <para>- <c>long</c> since_id(optional)</para>
        /// <para>- <c>long</c> max_id (optional)</para>
        /// <para>- <c>bool</c> trim_user (optional)</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// <para>- <c>bool</c> include_user_entities (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the statuses.</para>
        /// </returns>
        public Task<ListedResponse<Status>> RetweetsOfMeAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiArrayAsync<Status>(MethodType.Get, "statuses/retweets_of_me", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Returns the most recent tweets authored by the authenticating user that have recently been retweeted by others as an asynchronous operation.</para>
        /// <para>This timeline is a subset of the user's GET statuses/user_timeline.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>int</c> count (optional)</para>
        /// <para>- <c>long</c> since_id(optional)</para>
        /// <para>- <c>long</c> max_id (optional)</para>
        /// <para>- <c>bool</c> trim_user (optional)</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// <para>- <c>bool</c> include_user_entities (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the statuses.</para>
        /// </returns>
        public Task<ListedResponse<Status>> RetweetsOfMeAsync<T>(T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiArrayAsync<Status, T>(MethodType.Get, "statuses/retweets_of_me", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Returns information allowing the creation of an embedded representation of a Tweet on third party sitesas an asynchronous operation.</para>
        /// <para>While this endpoint allows a bit of customization for the final appearance of the embedded Tweet, be aware that the appearance of the rendered Tweet may change over time to be consistent with Twitter's Display Requirements.</para>
        /// <para>Do not rely on any class or id parameters to stay constant in the returned markup.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> id (required)</para>
        /// <para>- <c>int</c> maxwidth (optional)</para>
        /// <para>- <c>bool</c> hide_media (optional)</para>
        /// <para>- <c>bool</c> hide_thread (optional)</para>
        /// <para>- <c>bool</c> omit_script (optional)</para>
        /// <para>- <c>string</c> align (optional)</para>
        /// <para>- <c>string</c> related (optional)</para>
        /// <para>- <c>string</c> lang (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the HTML code and more.</para>
        /// </returns>
        public Task<Embed> OembedAsync(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiAsync<Embed>(MethodType.Get, "statuses/oembed", parameters);
        }

        /// <summary>
        /// <para>Returns information allowing the creation of an embedded representation of a Tweet on third party sites as an asynchronous operation.</para>
        /// <para>While this endpoint allows a bit of customization for the final appearance of the embedded Tweet, be aware that the appearance of the rendered Tweet may change over time to be consistent with Twitter's Display Requirements.</para>
        /// <para>Do not rely on any class or id parameters to stay constant in the returned markup.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> id (required)</para>
        /// <para>- <c>int</c> maxwidth (optional)</para>
        /// <para>- <c>bool</c> hide_media (optional)</para>
        /// <para>- <c>bool</c> hide_thread (optional)</para>
        /// <para>- <c>bool</c> omit_script (optional)</para>
        /// <para>- <c>string</c> align (optional)</para>
        /// <para>- <c>string</c> related (optional)</para>
        /// <para>- <c>string</c> lang (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the HTML code and more.</para>
        /// </returns>
        public Task<Embed> OembedAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<Embed>(MethodType.Get, "statuses/oembed", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Returns information allowing the creation of an embedded representation of a Tweet on third party sites as an asynchronous operation.</para>
        /// <para>While this endpoint allows a bit of customization for the final appearance of the embedded Tweet, be aware that the appearance of the rendered Tweet may change over time to be consistent with Twitter's Display Requirements.</para>
        /// <para>Do not rely on any class or id parameters to stay constant in the returned markup.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> id (required)</para>
        /// <para>- <c>int</c> maxwidth (optional)</para>
        /// <para>- <c>bool</c> hide_media (optional)</para>
        /// <para>- <c>bool</c> hide_thread (optional)</para>
        /// <para>- <c>bool</c> omit_script (optional)</para>
        /// <para>- <c>string</c> align (optional)</para>
        /// <para>- <c>string</c> related (optional)</para>
        /// <para>- <c>string</c> lang (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the HTML code and more.</para>
        /// </returns>
        public Task<Embed> OembedAsync<T>(T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<Embed, T>(MethodType.Get, "statuses/oembed", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Returns a single Tweet, specified by the id parameter as an asynchronous operation.</para>
        /// <para>The Tweet's author will also be embedded within the tweet.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> id (required)</para>
        /// <para>- <c>bool</c> trim_user (optional)</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the status.</para>
        /// </returns>
        public Task<StatusResponse> ShowAsync(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessParameterReservedApiAsync<StatusResponse>(MethodType.Get, "statuses/show/{id}", "id", InternalUtils.ExpressionsToDictionary(parameters), CancellationToken.None);
        }

        /// <summary>
        /// <para>Returns a single Tweet, specified by the id parameter as an asynchronous operation.</para>
        /// <para>The Tweet's author will also be embedded within the tweet.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> id (required)</para>
        /// <para>- <c>bool</c> trim_user (optional)</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the status.</para>
        /// </returns>
        public Task<StatusResponse> ShowAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessParameterReservedApiAsync<StatusResponse>(MethodType.Get, "statuses/show/{id}", "id", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Returns a single Tweet, specified by the id parameter as an asynchronous operation.</para>
        /// <para>The Tweet's author will also be embedded within the tweet.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> id (required)</para>
        /// <para>- <c>bool</c> trim_user (optional)</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the status.</para>
        /// </returns>
        public Task<StatusResponse> ShowAsync<T>(T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessParameterReservedApiAsync<StatusResponse>(MethodType.Get, "statuses/show/{id}", "id", InternalUtils.ResolveObject(parameters), cancellationToken);
        }

        /// <summary>
        /// <para>Returns up to 100 of the first retweets of a given tweet as an asynchronous operation.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> id (required)</para>
        /// <para>- <c>bool</c> trim_user (optional)</para>
        /// <para>- <c>int</c> count (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the statuses.</para>
        /// </returns>
        public Task<ListedResponse<Status>> RetweetsAsync(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessParameterReservedApiArrayAsync<Status>(MethodType.Get, "statuses/retweets/{id}", "id", InternalUtils.ExpressionsToDictionary(parameters), CancellationToken.None);
        }

        /// <summary>
        /// <para>Returns up to 100 of the first retweets of a given tweet as an asynchronous operation.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> id (required)</para>
        /// <para>- <c>bool</c> trim_user (optional)</para>
        /// <para>- <c>int</c> count (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the statuses.</para>
        /// </returns>
        public Task<ListedResponse<Status>> RetweetsAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessParameterReservedApiArrayAsync<Status>(MethodType.Get, "statuses/retweets/{id}", "id", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Returns up to 100 of the first retweets of a given tweet as an asynchronous operation.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> id (required)</para>
        /// <para>- <c>bool</c> trim_user (optional)</para>
        /// <para>- <c>int</c> count (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the statuses.</para>
        /// </returns>
        public Task<ListedResponse<Status>> RetweetsAsync<T>(T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessParameterReservedApiArrayAsync<Status>(MethodType.Get, "statuses/retweets/{id}", "id", InternalUtils.ResolveObject(parameters), cancellationToken);
        }

        /// <summary>
        /// <para>Returns fully-hydrated tweet objects for up to 100 tweets per request, as specified by comma-separated values passed to the id parameter as an asynchronous operation.</para>
        /// <para>This method is especially useful to get the details (hydrate) a collection of Tweet IDs.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> / <c>IEnumerbale&lt;long&gt;</c> id (required)</para>
        /// <para><example>Example Values: 20, 432656548536401920</example></para>
        /// <para>- <c>bool</c> trim_user (optional)</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the statuses.</para>
        /// </returns>
        public Task<ListedResponse<Status>> LookupAsync(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiArrayAsync<Status>(MethodType.Post, "statuses/lookup", parameters);
        }

        /// <summary>
        /// <para>Returns fully-hydrated tweet objects for up to 100 tweets per request, as specified by comma-separated values passed to the id parameter as an asynchronous operation.</para>
        /// <para>This method is especially useful to get the details (hydrate) a collection of Tweet IDs.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> / <c>IEnumerbale&lt;long&gt;</c> id (required)</para>
        /// <para><example>Example Values: 20, 432656548536401920</example></para>
        /// <para>- <c>bool</c> trim_user (optional)</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the statuses.</para>
        /// </returns>
        public Task<ListedResponse<Status>> LookupAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiArrayAsync<Status>(MethodType.Post, "statuses/lookup", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Returns fully-hydrated tweet objects for up to 100 tweets per request, as specified by comma-separated values passed to the id parameter as an asynchronous operation.</para>
        /// <para>This method is especially useful to get the details (hydrate) a collection of Tweet IDs.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> / <c>IEnumerbale&lt;long&gt;</c> id (required)</para>
        /// <para><example>Example Values: 20, 432656548536401920</example></para>
        /// <para>- <c>bool</c> trim_user (optional)</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the statuses.</para>
        /// </returns>
        public Task<ListedResponse<Status>> LookupAsync<T>(T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiArrayAsync<Status, T>(MethodType.Post, "statuses/lookup", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Returns a collection of up to 100 user IDs belonging to users who have retweeted the tweet specified by the id parameter as an asynchronous operation.</para>
        /// <para>This method offers similar data to GET statuses/retweets/:id and replaces API v1's GET statuses/:id/retweeted_by/ids method.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> id (required)</para>
        /// <para>- <c>long</c> cursor (semi-optional)</para>
        /// <para>Don't use stringify_ids</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the IDs.</para>
        /// </returns>
        public Task<Cursored<long>> RetweetersIdsAsync(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiAsync<Cursored<long>>(MethodType.Get, "statuses/retweeters/ids", parameters);
        }

        /// <summary>
        /// <para>Returns a collection of up to 100 user IDs belonging to users who have retweeted the tweet specified by the id parameter as an asynchronous operation.</para>
        /// <para>This method offers similar data to GET statuses/retweets/:id and replaces API v1's GET statuses/:id/retweeted_by/ids method.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> id (required)</para>
        /// <para>- <c>long</c> cursor (semi-optional)</para>
        /// <para>Don't use stringify_ids</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the IDs.</para>
        /// </returns>
        public Task<Cursored<long>> RetweetersIdsAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<Cursored<long>>(MethodType.Get, "statuses/retweeters/ids", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Returns a collection of up to 100 user IDs belonging to users who have retweeted the tweet specified by the id parameter as an asynchronous operation.</para>
        /// <para>This method offers similar data to GET statuses/retweets/:id and replaces API v1's GET statuses/:id/retweeted_by/ids method.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> id (required)</para>
        /// <para>- <c>long</c> cursor (semi-optional)</para>
        /// <para>Don't use stringify_ids</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the IDs.</para>
        /// </returns>
        public Task<Cursored<long>> RetweetersIdsAsync<T>(T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<Cursored<long>, T>(MethodType.Get, "statuses/retweeters/ids", parameters, cancellationToken);
        }

        //POST Methods

        /// <summary>
        /// <para>Updates the authenticating user's current status, also known as tweeting as an asynchronous operation.</para>
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
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the updated status.</para>
        /// </returns>
        public Task<StatusResponse> UpdateAsync(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiAsync<StatusResponse>(MethodType.Post, "statuses/update", parameters);
        }

        /// <summary>
        /// <para>Updates the authenticating user's current status, also known as tweeting as an asynchronous operation.</para>
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
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the updated status.</para>
        /// </returns>
        public Task<StatusResponse> UpdateAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<StatusResponse>(MethodType.Post, "statuses/update", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Updates the authenticating user's current status, also known as tweeting as an asynchronous operation.</para>
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
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the updated status.</para>
        /// </returns>
        public Task<StatusResponse> UpdateAsync<T>(T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<StatusResponse, T>(MethodType.Post, "statuses/update", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Updates the authenticating user's current status, uploading an image as an asynchronous operation.</para>
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
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the updated status.</para>
        /// </returns>
        [Obsolete("Use Media.UploadAsync and Statuses.UpdateAsync.")]
        public Task<StatusResponse> UpdateWithMediaAsync(params Expression<Func<string, object>>[] parameters)
        {
            return this.UpdateWithMediaAsyncImpl(InternalUtils.ExpressionsToDictionary(parameters), CancellationToken.None);
        }

        /// <summary>
        /// <para>Updates the authenticating user's current status, uploading an image as an asynchronous operation.</para>
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
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the updated status.</para>
        /// </returns>
        [Obsolete("Use Media.UploadAsync and Statuses.UpdateAsync.")]
        public Task<StatusResponse> UpdateWithMediaAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.UpdateWithMediaAsyncImpl(parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Updates the authenticating user's current status, uploading an image as an asynchronous operation.</para>
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
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the updated status.</para>
        /// </returns>
        [Obsolete("Use Media.UploadAsync and Statuses.UpdateAsync.")]
        public Task<StatusResponse> UpdateWithMediaAsync<T>(T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.UpdateWithMediaAsyncImpl(InternalUtils.ResolveObject(parameters), cancellationToken);
        }

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

        /// <summary>
        /// <para>Destroys the status specified by the required ID parameter as an asynchronous operation.</para>
        /// <para>The authenticating user must be the author of the specified status.</para>
        /// <para>Returns the destroyed status if successful.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> id (required)</para>
        /// <para>- <c>bool</c> trim_user (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the destroied status.</para>
        /// </returns>
        public Task<StatusResponse> DestroyAsync(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessParameterReservedApiAsync<StatusResponse>(MethodType.Post, "statuses/destroy/{id}", "id", InternalUtils.ExpressionsToDictionary(parameters), CancellationToken.None);
        }

        /// <summary>
        /// <para>Destroys the status specified by the required ID parameter as an asynchronous operation.</para>
        /// <para>The authenticating user must be the author of the specified status.</para>
        /// <para>Returns the destroyed status if successful.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> id (required)</para>
        /// <para>- <c>bool</c> trim_user (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the destroied status.</para>
        /// </returns>
        public Task<StatusResponse> DestroyAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessParameterReservedApiAsync<StatusResponse>(MethodType.Post, "statuses/destroy/{id}", "id", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Destroys the status specified by the required ID parameter as an asynchronous operation.</para>
        /// <para>The authenticating user must be the author of the specified status.</para>
        /// <para>Returns the destroyed status if successful.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> id (required)</para>
        /// <para>- <c>bool</c> trim_user (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the destroied status.</para>
        /// </returns>
        public Task<StatusResponse> DestroyAsync<T>(T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessParameterReservedApiAsync<StatusResponse>(MethodType.Post, "statuses/destroy/{id}", "id", InternalUtils.ResolveObject(parameters), cancellationToken);
        }

        /// <summary>
        /// <para>Retweets a tweet. Returns the original tweet with retweet details embedded as an asynchronous operation.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> id (required)</para>
        /// <para>- <c>bool</c> trim_user (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the retweeted status.</para>
        /// </returns>
        public Task<StatusResponse> RetweetAsync(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessParameterReservedApiAsync<StatusResponse>(MethodType.Post, "statuses/retweet/{id}", "id", InternalUtils.ExpressionsToDictionary(parameters), CancellationToken.None);
        }

        /// <summary>
        /// <para>Retweets a tweet. Returns the original tweet with retweet details embedded as an asynchronous operation.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> id (required)</para>
        /// <para>- <c>bool</c> trim_user (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the retweeted status.</para>
        /// </returns>
        public Task<StatusResponse> RetweetAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessParameterReservedApiAsync<StatusResponse>(MethodType.Post, "statuses/retweet/{id}", "id", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Retweets a tweet. Returns the original tweet with retweet details embedded as an asynchronous operation.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> id (required)</para>
        /// <para>- <c>bool</c> trim_user (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the retweeted status.</para>
        /// </returns>
        public Task<StatusResponse> RetweetAsync<T>(T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessParameterReservedApiAsync<StatusResponse>(MethodType.Post, "statuses/retweet/{id}", "id", InternalUtils.ResolveObject(parameters), cancellationToken);
        }
    }
}
#endif
