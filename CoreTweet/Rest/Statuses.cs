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

namespace CoreTweet.Rest
{
    /// <summary>
    /// Provides a set of methods for the wrapper of GET/POST statuses.
    /// </summary>
    public partial class Statuses : ApiProviderBase
    {
        internal Statuses(TokensBase e) : base(e) { }

#if !(PCL || WIN_RT || WP)
        //GET Methods

        /// <summary>
        /// <para>Returns the 20 most recent mentions (tweets containing a users's &#64;screen_name) for the authenticating user.</para>
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
        /// <para>- <c>long</c> since_id(optional)</para>
        /// <para>- <c>long</c> max_id (optional)</para>
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
        /// <para>- <c>long</c> since_id(optional)</para>
        /// <para>- <c>long</c> max_id (optional)</para>
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
        /// <para>Returns a collection of the most recent Tweets posted by the user indicated by the screen_name or user_id parameters.</para>
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
        /// <returns>The statuses.</returns>
        public ListedResponse<Status> UserTimeline(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiArray<Status>(MethodType.Get, "statuses/user_timeline", parameters);
        }

        /// <summary>
        /// <para>Returns a collection of the most recent Tweets posted by the user indicated by the screen_name or user_id parameters.</para>
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
        /// <returns>The statuses.</returns>
        public ListedResponse<Status> UserTimeline(IDictionary<string, object> parameters)
        {
            return this.Tokens.AccessApiArray<Status>(MethodType.Get, "statuses/user_timeline", parameters);
        }

        /// <summary>
        /// <para>Returns a collection of the most recent Tweets posted by the user indicated by the screen_name or user_id parameters.</para>
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
        /// <returns>The statuses.</returns>
        public ListedResponse<Status> UserTimeline<T>(T parameters)
        {
            return this.Tokens.AccessApiArray<Status, T>(MethodType.Get, "statuses/user_timeline", parameters);
        }

        /// <summary>
        /// <para>Returns a collection of the most recent Tweets and retweets posted by the authenticating user and the users they follow.</para>
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
        /// <returns>The statuses.</returns>
        public ListedResponse<Status> HomeTimeline(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiArray<Status>(MethodType.Get, "statuses/home_timeline", parameters);
        }

        /// <summary>
        /// <para>Returns a collection of the most recent Tweets and retweets posted by the authenticating user and the users they follow.</para>
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
        /// <returns>The statuses.</returns>
        public ListedResponse<Status> HomeTimeline(IDictionary<string, object> parameters)
        {
            return this.Tokens.AccessApiArray<Status>(MethodType.Get, "statuses/home_timeline", parameters);
        }

        /// <summary>
        /// <para>Returns a collection of the most recent Tweets and retweets posted by the authenticating user and the users they follow.</para>
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
        /// <returns>The statuses.</returns>
        public ListedResponse<Status> HomeTimeline<T>(T parameters)
        {
            return this.Tokens.AccessApiArray<Status, T>(MethodType.Get, "statuses/home_timeline", parameters);
        }

        /// <summary>
        /// <para>Returns the most recent tweets authored by the authenticating user that have recently been retweeted by others.</para>
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
        /// <returns>The statuses.</returns>
        public ListedResponse<Status> RetweetsOfMe(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiArray<Status>(MethodType.Get, "statuses/retweets_of_me", parameters);
        }

        /// <summary>
        /// <para>Returns the most recent tweets authored by the authenticating user that have recently been retweeted by others.</para>
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
        /// <returns>The statuses.</returns>
        public ListedResponse<Status> RetweetsOfMe(IDictionary<string, object> parameters)
        {
            return this.Tokens.AccessApiArray<Status>(MethodType.Get, "statuses/retweets_of_me", parameters);
        }

        /// <summary>
        /// <para>Returns the most recent tweets authored by the authenticating user that have recently been retweeted by others.</para>
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
        /// <returns>The statuses.</returns>
        public ListedResponse<Status> RetweetsOfMe<T>(T parameters)
        {
            return this.Tokens.AccessApiArray<Status, T>(MethodType.Get, "statuses/retweets_of_me", parameters);
        }

        /// <summary>
        /// <para>Returns information allowing the creation of an embedded representation of a Tweet on third party sites.</para>
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
        /// <returns>The HTML code and more.</returns>
        public Embed Oembed(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApi<Embed>(MethodType.Get, "statuses/oembed", parameters);
        }

        /// <summary>
        /// <para>Returns information allowing the creation of an embedded representation of a Tweet on third party sites.</para>
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
        /// <returns>The HTML code and more.</returns>
        public Embed Oembed(IDictionary<string, object> parameters)
        {
            return this.Tokens.AccessApi<Embed>(MethodType.Get, "statuses/oembed", parameters);
        }

        /// <summary>
        /// <para>Returns information allowing the creation of an embedded representation of a Tweet on third party sites.</para>
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
        /// <returns>The HTML code and more.</returns>
        public Embed Oembed<T>(T parameters)
        {
            return this.Tokens.AccessApi<Embed, T>(MethodType.Get, "statuses/oembed", parameters);
        }

        /// <summary>
        /// <para>Returns a single Tweet, specified by the id parameter.</para>
        /// <para>The Tweet's author will also be embedded within the tweet.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> id (required)</para>
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
        /// <para>- <c>long</c> id (required)</para>
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
        /// <para>- <c>long</c> id (required)</para>
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
        /// <para>Returns up to 100 of the first retweets of a given tweet.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> id (required)</para>
        /// <para>- <c>bool</c> trim_user (optional)</para>
        /// <para>- <c>int</c> count (optional)</para>
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
        /// <para>- <c>long</c> id (required)</para>
        /// <para>- <c>bool</c> trim_user (optional)</para>
        /// <para>- <c>int</c> count (optional)</para>
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
        /// <para>- <c>long</c> id (required)</para>
        /// <para>- <c>bool</c> trim_user (optional)</para>
        /// <para>- <c>int</c> count (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The statuses.</returns>
        public ListedResponse<Status> Retweets<T>(T parameters)
        {
            return this.Tokens.AccessParameterReservedApiArray<Status>(MethodType.Get, "statuses/retweets/{id}", "id", InternalUtils.ResolveObject(parameters));
        }

        /// <summary>
        /// <para>Returns fully-hydrated tweet objects for up to 100 tweets per request, as specified by comma-separated values passed to the id parameter.</para>
        /// <para>This method is especially useful to get the details (hydrate) a collection of Tweet IDs.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> / <c>IEnumerbale&lt;long&gt;</c> id (required)</para>
        /// <para><example>Example Values: 20, 432656548536401920</example></para>
        /// <para>- <c>bool</c> trim_user (optional)</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The statuses.</returns>
        public ListedResponse<Status> Lookup(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiArray<Status>(MethodType.Post, "statuses/lookup", parameters);
        }

        /// <summary>
        /// <para>Returns fully-hydrated tweet objects for up to 100 tweets per request, as specified by comma-separated values passed to the id parameter.</para>
        /// <para>This method is especially useful to get the details (hydrate) a collection of Tweet IDs.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> / <c>IEnumerbale&lt;long&gt;</c> id (required)</para>
        /// <para><example>Example Values: 20, 432656548536401920</example></para>
        /// <para>- <c>bool</c> trim_user (optional)</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The statuses.</returns>
        public ListedResponse<Status> Lookup(IDictionary<string, object> parameters)
        {
            return this.Tokens.AccessApiArray<Status>(MethodType.Post, "statuses/lookup", parameters);
        }

        /// <summary>
        /// <para>Returns fully-hydrated tweet objects for up to 100 tweets per request, as specified by comma-separated values passed to the id parameter.</para>
        /// <para>This method is especially useful to get the details (hydrate) a collection of Tweet IDs.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> / <c>IEnumerbale&lt;long&gt;</c> id (required)</para>
        /// <para><example>Example Values: 20, 432656548536401920</example></para>
        /// <para>- <c>bool</c> trim_user (optional)</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The statuses.</returns>
        public ListedResponse<Status> Lookup<T>(T parameters)
        {
            return this.Tokens.AccessApiArray<Status, T>(MethodType.Post, "statuses/lookup", parameters);
        }

        /// <summary>
        /// <para>Returns a collection of up to 100 user IDs belonging to users who have retweeted the tweet specified by the id parameter.</para>
        /// <para>This method offers similar data to GET statuses/retweets/:id and replaces API v1's GET statuses/:id/retweeted_by/ids method.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> id (required)</para>
        /// <para>- <c>long</c> cursor (semi-optional)</para>
        /// <para>Don't use stringify_ids</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The IDs.</returns>
        public Cursored<long> RetweetersIds(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApi<Cursored<long>>(MethodType.Get, "statuses/retweeters/ids", parameters);
        }

        /// <summary>
        /// <para>Returns a collection of up to 100 user IDs belonging to users who have retweeted the tweet specified by the id parameter.</para>
        /// <para>This method offers similar data to GET statuses/retweets/:id and replaces API v1's GET statuses/:id/retweeted_by/ids method.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> id (required)</para>
        /// <para>- <c>long</c> cursor (semi-optional)</para>
        /// <para>Don't use stringify_ids</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The IDs.</returns>
        public Cursored<long> RetweetersIds(IDictionary<string, object> parameters)
        {
            return this.Tokens.AccessApi<Cursored<long>>(MethodType.Get, "statuses/retweeters/ids", parameters);
        }

        /// <summary>
        /// <para>Returns a collection of up to 100 user IDs belonging to users who have retweeted the tweet specified by the id parameter.</para>
        /// <para>This method offers similar data to GET statuses/retweets/:id and replaces API v1's GET statuses/:id/retweeted_by/ids method.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> id (required)</para>
        /// <para>- <c>long</c> cursor (semi-optional)</para>
        /// <para>Don't use stringify_ids</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The IDs.</returns>
        public Cursored<long> RetweetersIds<T>(T parameters)
        {
            return this.Tokens.AccessApi<Cursored<long>, T>(MethodType.Get, "statuses/retweeters/ids", parameters);
        }

        /// <summary>
        /// <para>Enumerates a collection of up to 100 user IDs belonging to users who have retweeted the tweet specified by the id parameter.</para>
        /// <para>This method offers similar data to GET statuses/retweets/:id and replaces API v1's GET statuses/:id/retweeted_by/ids method.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> id (required)</para>
        /// <para>- <c>long</c> cursor (semi-optional)</para>
        /// <para>Don't use stringify_ids</para>
        /// </summary>
        /// <param name="mode">Specify whether enumerating goes to the next page or the previous.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The IDs.</returns>
        public IEnumerable<long> EnumerateRetweetersIds(EnumerateMode mode, params Expression<Func<string, object>>[] parameters)
        {
            return Cursored<long>.Enumerate(this.Tokens, "statuses/retweeters/ids", mode, parameters);
        }

        /// <summary>
        /// <para>Enumerates a collection of up to 100 user IDs belonging to users who have retweeted the tweet specified by the id parameter.</para>
        /// <para>This method offers similar data to GET statuses/retweets/:id and replaces API v1's GET statuses/:id/retweeted_by/ids method.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> id (required)</para>
        /// <para>- <c>long</c> cursor (semi-optional)</para>
        /// <para>Don't use stringify_ids</para>
        /// </summary>
        /// <param name="mode">Specify whether enumerating goes to the next page or the previous.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The IDs.</returns>
        public IEnumerable<long> EnumerateRetweetersIds(EnumerateMode mode, IDictionary<string, object> parameters)
        {
            return Cursored<long>.Enumerate(this.Tokens, "statuses/retweeters/ids", mode, parameters);
        }

        /// <summary>
        /// <para>Enumerates a collection of up to 100 user IDs belonging to users who have retweeted the tweet specified by the id parameter.</para>
        /// <para>This method offers similar data to GET statuses/retweets/:id and replaces API v1's GET statuses/:id/retweeted_by/ids method.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> id (required)</para>
        /// <para>- <c>long</c> cursor (semi-optional)</para>
        /// <para>Don't use stringify_ids</para>
        /// </summary>
        /// <param name="mode">Specify whether enumerating goes to the next page or the previous.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The IDs.</returns>
        public IEnumerable<long> EnumerateRetweetersIds<T>(EnumerateMode mode, T parameters)
        {
            return Cursored<long>.Enumerate<T>(this.Tokens, "statuses/retweeters/ids", mode, parameters);
        }

        //POST Methods

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
            return this.Tokens.AccessApi<StatusResponse>(MethodType.Post, "statuses/update", parameters);
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
            return this.Tokens.AccessApi<StatusResponse>(MethodType.Post, "statuses/update", parameters);
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
            return this.Tokens.AccessApi<StatusResponse, T>(MethodType.Post, "statuses/update", parameters);
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

        /// <summary>
        /// <para>Destroys the status specified by the required ID parameter.</para>
        /// <para>The authenticating user must be the author of the specified status.</para>
        /// <para>Returns the destroyed status if successful.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> id (required)</para>
        /// <para>- <c>bool</c> trim_user (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The destroied status.</returns>
        public StatusResponse Destroy(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessParameterReservedApi<StatusResponse>(MethodType.Post, "statuses/destroy/{id}", "id", InternalUtils.ExpressionsToDictionary(parameters));
        }

        /// <summary>
        /// <para>Destroys the status specified by the required ID parameter.</para>
        /// <para>The authenticating user must be the author of the specified status.</para>
        /// <para>Returns the destroyed status if successful.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> id (required)</para>
        /// <para>- <c>bool</c> trim_user (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The destroied status.</returns>
        public StatusResponse Destroy(IDictionary<string, object> parameters)
        {
            return this.Tokens.AccessParameterReservedApi<StatusResponse>(MethodType.Post, "statuses/destroy/{id}", "id", parameters);
        }

        /// <summary>
        /// <para>Destroys the status specified by the required ID parameter.</para>
        /// <para>The authenticating user must be the author of the specified status.</para>
        /// <para>Returns the destroyed status if successful.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> id (required)</para>
        /// <para>- <c>bool</c> trim_user (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The destroied status.</returns>
        public StatusResponse Destroy<T>(T parameters)
        {
            return this.Tokens.AccessParameterReservedApi<StatusResponse>(MethodType.Post, "statuses/destroy/{id}", "id", InternalUtils.ResolveObject(parameters));
        }

        /// <summary>
        /// <para>Retweets a tweet. Returns the original tweet with retweet details embedded.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> id (required)</para>
        /// <para>- <c>bool</c> trim_user (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The retweeted status.</returns>
        public StatusResponse Retweet(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessParameterReservedApi<StatusResponse>(MethodType.Post, "statuses/retweet/{id}", "id", InternalUtils.ExpressionsToDictionary(parameters));
        }

        /// <summary>
        /// <para>Retweets a tweet. Returns the original tweet with retweet details embedded.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> id (required)</para>
        /// <para>- <c>bool</c> trim_user (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The retweeted status.</returns>
        public StatusResponse Retweet(IDictionary<string, object> parameters)
        {
            return this.Tokens.AccessParameterReservedApi<StatusResponse>(MethodType.Post, "statuses/retweet/{id}", "id", parameters);
        }

        /// <summary>
        /// <para>Retweets a tweet. Returns the original tweet with retweet details embedded.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> id (required)</para>
        /// <para>- <c>bool</c> trim_user (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The retweeted status.</returns>
        public StatusResponse Retweet<T>(T parameters)
        {
            return this.Tokens.AccessParameterReservedApi<StatusResponse>(MethodType.Post, "statuses/retweet/{id}", "id", InternalUtils.ResolveObject(parameters));
        }
#endif
    }
}
