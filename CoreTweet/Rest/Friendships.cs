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
    /// Provides a set of methods for the wrapper of GET/POST friendships.
    /// </summary>
    public partial class Friendships : ApiProviderBase
    {
        internal Friendships(TokensBase e) : base(e) { }

#if !(PCL || WIN_RT || WP)
        //GET Methods

        /// <summary>
        /// <para>Returns a collection of user_ids that the currently authenticated user does not want to receive retweets from.</para>
        /// <para>Use POST friendships/update to set the "no retweets" status for a given user account on behalf of the current user.</para>
        /// <para>Available parameters: Nothing.</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The Ids.</returns>
        public ListedResponse<long> NoRetweetsIds(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiArray<long>(MethodType.Get, "friendships/no_retweets/ids", parameters);
        }

        /// <summary>
        /// <para>Returns a collection of user_ids that the currently authenticated user does not want to receive retweets from.</para>
        /// <para>Use POST friendships/update to set the "no retweets" status for a given user account on behalf of the current user.</para>
        /// <para>Available parameters: Nothing.</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The Ids.</returns>
        public ListedResponse<long> NoRetweetsIds(IDictionary<string, object> parameters)
        {
            return this.Tokens.AccessApiArray<long>(MethodType.Get, "friendships/no_retweets/ids", parameters);
        }

        /// <summary>
        /// <para>Returns a collection of user_ids that the currently authenticated user does not want to receive retweets from.</para>
        /// <para>Use POST friendships/update to set the "no retweets" status for a given user account on behalf of the current user.</para>
        /// <para>Available parameters: Nothing.</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The Ids.</returns>
        public ListedResponse<long> NoRetweetsIds<T>(T parameters)
        {
            return this.Tokens.AccessApiArray<long, T>(MethodType.Get, "friendships/no_retweets/ids", parameters);
        }

        /// <summary>
        /// <para>Returns a collection of numeric IDs for every user who has a pending request to follow the authenticating user.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> cursor (semi-optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The Ids.</returns>
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
        /// <returns>The Ids.</returns>
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
        /// <returns>The Ids.</returns>
        public Cursored<long> Incoming<T>(T parameters)
        {
            return this.Tokens.AccessApi<Cursored<long>, T>(MethodType.Get, "friendships/incoming", parameters);
        }

        /// <summary>
        /// <para>Enumerate numeric IDs for every user who has a pending request to follow the authenticating user.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> cursor (semi-optional)</para>
        /// </summary>
        /// <param name="mode">Specify whether enumerating goes to the next page or the previous.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The IDs.</returns>
        public IEnumerable<long> EnumerateIncoming(EnumerateMode mode, params Expression<Func<string, object>>[] parameters)
        {
            return Cursored<long>.Enumerate(this.Tokens, "friendships/incoming", mode, parameters);
        }

        /// <summary>
        /// <para>Enumerate numeric IDs for every user who has a pending request to follow the authenticating user.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> cursor (semi-optional)</para>
        /// </summary>
        /// <param name="mode">Specify whether enumerating goes to the next page or the previous.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The IDs.</returns>
        public IEnumerable<long> EnumerateIncoming(EnumerateMode mode, IDictionary<string, object> parameters)
        {
            return Cursored<long>.Enumerate(this.Tokens, "friendships/incoming", mode, parameters);
        }

        /// <summary>
        /// <para>Enumerate numeric IDs for every user who has a pending request to follow the authenticating user.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> cursor (semi-optional)</para>
        /// </summary>
        /// <param name="mode">Specify whether enumerating goes to the next page or the previous.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The IDs.</returns>
        public IEnumerable<long> EnumerateIncoming<T>(EnumerateMode mode, T parameters)
        {
            return Cursored<long>.Enumerate<T>(this.Tokens, "friendships/incoming", mode, parameters);
        }

        /// <summary>
        /// <para>Returns a collection of numeric IDs for every protected user for whom the authenticating user has a pending follow request.</para>
        /// <para>Available parameters: </para>
        /// <para>- <c>long</c> cursor (semi-optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The IDs.</returns>
        public Cursored<long> Outgoing(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApi<Cursored<long>>(MethodType.Get, "friendships/outgoing", parameters);
        }

        /// <summary>
        /// <para>Returns a collection of numeric IDs for every protected user for whom the authenticating user has a pending follow request.</para>
        /// <para>Available parameters: </para>
        /// <para>- <c>long</c> cursor (semi-optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The IDs.</returns>
        public Cursored<long> Outgoing(IDictionary<string, object> parameters)
        {
            return this.Tokens.AccessApi<Cursored<long>>(MethodType.Get, "friendships/outgoing", parameters);
        }

        /// <summary>
        /// <para>Returns a collection of numeric IDs for every protected user for whom the authenticating user has a pending follow request.</para>
        /// <para>Available parameters: </para>
        /// <para>- <c>long</c> cursor (semi-optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The IDs.</returns>
        public Cursored<long> Outgoing<T>(T parameters)
        {
            return this.Tokens.AccessApi<Cursored<long>, T>(MethodType.Get, "friendships/outgoing", parameters);
        }

        /// <summary>
        /// <para>Enumerate numeric IDs for every protected user for whom the authenticating user has a pending follow request.</para>
        /// <para>Available parameters: </para>
        /// <para>- <c>long</c> cursor (semi-optional)</para>
        /// </summary>
        /// <param name="mode">Specify whether enumerating goes to the next page or the previous.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The IDs.</returns>
        public IEnumerable<long> EnumerateOutgoing(EnumerateMode mode, params Expression<Func<string, object>>[] parameters)
        {
            return Cursored<long>.Enumerate(this.Tokens, "friendships/outgoing", mode, parameters);
        }

        /// <summary>
        /// <para>Enumerate numeric IDs for every protected user for whom the authenticating user has a pending follow request.</para>
        /// <para>Available parameters: </para>
        /// <para>- <c>long</c> cursor (semi-optional)</para>
        /// </summary>
        /// <param name="mode">Specify whether enumerating goes to the next page or the previous.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The IDs.</returns>
        public IEnumerable<long> EnumerateOutgoing(EnumerateMode mode, IDictionary<string, object> parameters)
        {
            return Cursored<long>.Enumerate(this.Tokens, "friendships/outgoing", mode, parameters);
        }

        /// <summary>
        /// <para>Enumerate numeric IDs for every protected user for whom the authenticating user has a pending follow request.</para>
        /// <para>Available parameters: </para>
        /// <para>- <c>long</c> cursor (semi-optional)</para>
        /// </summary>
        /// <param name="mode">Specify whether enumerating goes to the next page or the previous.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The IDs.</returns>
        public IEnumerable<long> EnumerateOutgoing<T>(EnumerateMode mode, T parameters)
        {
            return Cursored<long>.Enumerate<T>(this.Tokens, "friendships/outgoing", mode, parameters);
        }

        /// <summary>
        /// <para>Returns the relationships of the authenticating user to the comma-separated list of up to 100 screen_names or user_ids provided.</para>
        /// <para>Values for connections can be: following, following_requested, followed_by, none.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> / IEnumerable&lt;string&gt; screen_name (optional)</para>
        /// <para>- <c>string</c> / IEnumerable&lt;long&gt; user_id (ooptional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The Friendships.</returns>
        public ListedResponse<Friendship> Lookup(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiArray<Friendship>(MethodType.Get, "friendships/lookup", parameters);
        }

        /// <summary>
        /// <para>Returns the relationships of the authenticating user to the comma-separated list of up to 100 screen_names or user_ids provided.</para>
        /// <para>Values for connections can be: following, following_requested, followed_by, none.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> / IEnumerable&lt;string&gt; screen_name (optional)</para>
        /// <para>- <c>string</c> / IEnumerable&lt;long&gt; user_id (ooptional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The Friendships.</returns>
        public ListedResponse<Friendship> Lookup(IDictionary<string, object> parameters)
        {
            return this.Tokens.AccessApiArray<Friendship>(MethodType.Get, "friendships/lookup", parameters);
        }

        /// <summary>
        /// <para>Returns the relationships of the authenticating user to the comma-separated list of up to 100 screen_names or user_ids provided.</para>
        /// <para>Values for connections can be: following, following_requested, followed_by, none.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> / IEnumerable&lt;string&gt; screen_name (optional)</para>
        /// <para>- <c>string</c> / IEnumerable&lt;long&gt; user_id (ooptional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The Friendships.</returns>
        public ListedResponse<Friendship> Lookup<T>(T parameters)
        {
            return this.Tokens.AccessApiArray<Friendship, T>(MethodType.Get, "friendships/lookup", parameters);
        }

        /// <summary>
        /// <para>Returns detailed information about the relationship between two arbitrary users.</para>
        /// <para>Note: At least one source and one target, whether specified by IDs or screen_names, should be provided to this method.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> source_id (optional)</para>
        /// <para>- <c>string</c> source_screen_name (optional)</para>
        /// <para>- <c>long</c> target_id (optional)</para>
        /// <para>- <c>string</c> target_screen_name (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The relationship.</returns>
        public RelationShipResponse Show(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApi<RelationShipResponse>(MethodType.Get, "friendships/show", parameters, "relationship");
        }

        /// <summary>
        /// <para>Returns detailed information about the relationship between two arbitrary users.</para>
        /// <para>Note: At least one source and one target, whether specified by IDs or screen_names, should be provided to this method.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> source_id (optional)</para>
        /// <para>- <c>string</c> source_screen_name (optional)</para>
        /// <para>- <c>long</c> target_id (optional)</para>
        /// <para>- <c>string</c> target_screen_name (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The relationship.</returns>
        public RelationShipResponse Show(IDictionary<string, object> parameters)
        {
            return this.Tokens.AccessApi<RelationShipResponse>(MethodType.Get, "friendships/show", parameters, "relationship");
        }

        /// <summary>
        /// <para>Returns detailed information about the relationship between two arbitrary users.</para>
        /// <para>Note: At least one source and one target, whether specified by IDs or screen_names, should be provided to this method.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> source_id (optional)</para>
        /// <para>- <c>string</c> source_screen_name (optional)</para>
        /// <para>- <c>long</c> target_id (optional)</para>
        /// <para>- <c>string</c> target_screen_name (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The relationship.</returns>
        public RelationShipResponse Show<T>(T parameters)
        {
            return this.Tokens.AccessApi<RelationShipResponse, T>(MethodType.Get, "friendships/show", parameters, "relationship");
        }

        //POST Methods

        /// <summary>
        /// <para>Allows the authenticating users to follow the user specified in the ID parameter.</para>
        /// <para>Returns the befriended user in the requested format when successful.</para>
        /// <para>Returns a string describing the failure condition when unsuccessful.</para>
        /// <para>Actions taken in this method are asynchronous and changes will be eventually consistent.</para>
        /// <para>Note: Providing either screen_name or user_id is required.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> screen_name (optional)</para>
        /// <para>- <c>long</c> user_id (optional)</para>
        /// <para>- <c>bool</c> follow (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The user.</returns>
        public UserResponse Create(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApi<UserResponse>(MethodType.Post, "friendships/create", parameters);
        }

        /// <summary>
        /// <para>Allows the authenticating users to follow the user specified in the ID parameter.</para>
        /// <para>Returns the befriended user in the requested format when successful.</para>
        /// <para>Returns a string describing the failure condition when unsuccessful.</para>
        /// <para>Actions taken in this method are asynchronous and changes will be eventually consistent.</para>
        /// <para>Note: Providing either screen_name or user_id is required.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> screen_name (optional)</para>
        /// <para>- <c>long</c> user_id (optional)</para>
        /// <para>- <c>bool</c> follow (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The user.</returns>
        public UserResponse Create(IDictionary<string, object> parameters)
        {
            return this.Tokens.AccessApi<UserResponse>(MethodType.Post, "friendships/create", parameters);
        }

        /// <summary>
        /// <para>Allows the authenticating users to follow the user specified in the ID parameter.</para>
        /// <para>Returns the befriended user in the requested format when successful.</para>
        /// <para>Returns a string describing the failure condition when unsuccessful.</para>
        /// <para>Actions taken in this method are asynchronous and changes will be eventually consistent.</para>
        /// <para>Note: Providing either screen_name or user_id is required.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> screen_name (optional)</para>
        /// <para>- <c>long</c> user_id (optional)</para>
        /// <para>- <c>bool</c> follow (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The user.</returns>
        public UserResponse Create<T>(T parameters)
        {
            return this.Tokens.AccessApi<UserResponse, T>(MethodType.Post, "friendships/create", parameters);
        }

        /// <summary>
        /// <para>Allows the authenticating user to unfollow the user specified in the ID parameter.</para>
        /// <para>Returns the unfollowed user in the requested format when successful.</para>
        /// <para>Returns a string describing the failure condition when unsuccessful.</para>
        /// <para>Actions taken in this method are asynchronous and changes will be eventually consistent.</para>
        /// <para>Note: Providing either screen_name or user_id is required.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> screen_name (optional)</para>
        /// <para>- <c>long</c> user_id (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The user.</returns>
        public UserResponse Destroy(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApi<UserResponse>(MethodType.Post, "friendships/destroy", parameters);
        }

        /// <summary>
        /// <para>Allows the authenticating user to unfollow the user specified in the ID parameter.</para>
        /// <para>Returns the unfollowed user in the requested format when successful.</para>
        /// <para>Returns a string describing the failure condition when unsuccessful.</para>
        /// <para>Actions taken in this method are asynchronous and changes will be eventually consistent.</para>
        /// <para>Note: Providing either screen_name or user_id is required.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> screen_name (optional)</para>
        /// <para>- <c>long</c> user_id (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The user.</returns>
        public UserResponse Destroy(IDictionary<string, object> parameters)
        {
            return this.Tokens.AccessApi<UserResponse>(MethodType.Post, "friendships/destroy", parameters);
        }

        /// <summary>
        /// <para>Allows the authenticating user to unfollow the user specified in the ID parameter.</para>
        /// <para>Returns the unfollowed user in the requested format when successful.</para>
        /// <para>Returns a string describing the failure condition when unsuccessful.</para>
        /// <para>Actions taken in this method are asynchronous and changes will be eventually consistent.</para>
        /// <para>Note: Providing either screen_name or user_id is required.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> screen_name (optional)</para>
        /// <para>- <c>long</c> user_id (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The user.</returns>
        public UserResponse Destroy<T>(T parameters)
        {
            return this.Tokens.AccessApi<UserResponse, T>(MethodType.Post, "friendships/destroy", parameters);
        }

        /// <summary>
        /// <para>Allows one to enable or disable retweets and device notifications from the specified user.</para>
        /// <para>Note: Providing either screen_name or user_id is required.</para>
        /// <para>Available parameters: </para>
        /// <para>- <c>string</c> screen_name (optional)</para>
        /// <para>- <c>long</c> user_id (optional)</para>
        /// <para>- <c>bool</c> device (optional)</para>
        /// <para>- <c>bool</c> retweets (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The relationship.</returns>
        public RelationShipResponse Update(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApi<RelationShipResponse>(MethodType.Post, "friendships/update", parameters, "relationship");
        }

        /// <summary>
        /// <para>Allows one to enable or disable retweets and device notifications from the specified user.</para>
        /// <para>Note: Providing either screen_name or user_id is required.</para>
        /// <para>Available parameters: </para>
        /// <para>- <c>string</c> screen_name (optional)</para>
        /// <para>- <c>long</c> user_id (optional)</para>
        /// <para>- <c>bool</c> device (optional)</para>
        /// <para>- <c>bool</c> retweets (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The relationship.</returns>
        public RelationShipResponse Update(IDictionary<string, object> parameters)
        {
            return this.Tokens.AccessApi<RelationShipResponse>(MethodType.Post, "friendships/update", parameters, "relationship");
        }

        /// <summary>
        /// <para>Allows one to enable or disable retweets and device notifications from the specified user.</para>
        /// <para>Note: Providing either screen_name or user_id is required.</para>
        /// <para>Available parameters: </para>
        /// <para>- <c>string</c> screen_name (optional)</para>
        /// <para>- <c>long</c> user_id (optional)</para>
        /// <para>- <c>bool</c> device (optional)</para>
        /// <para>- <c>bool</c> retweets (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The relationship.</returns>
        public RelationShipResponse Update<T>(T parameters)
        {
            return this.Tokens.AccessApi<RelationShipResponse, T>(MethodType.Post, "friendships/update", parameters, "relationship");
        }
#endif
    }
}
