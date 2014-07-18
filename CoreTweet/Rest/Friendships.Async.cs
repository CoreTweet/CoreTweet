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
using CoreTweet.Core;

namespace CoreTweet.Rest
{
    partial class Friendships
    {
        //GET Methods

        /// <summary>
        /// <para>Returns a collection of user_ids that the currently authenticated user does not want to receive retweets from as an asynchronous operation.</para>
        /// <para>Use POST friendships/update to set the "no retweets" status for a given user account on behalf of the current user.</para>
        /// <para>Available parameters: Nothing.</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the IDs.</para>
        /// </returns>
        public Task<ListedResponse<long>> NoRetweetsIdsAsync(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiArrayAsync<long>(MethodType.Get, "friendships/no_retweets/ids", parameters);
        }

        /// <summary>
        /// <para>Returns a collection of user_ids that the currently authenticated user does not want to receive retweets from as an asynchronous operation.</para>
        /// <para>Use POST friendships/update to set the "no retweets" status for a given user account on behalf of the current user.</para>
        /// <para>Available parameters: Nothing.</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the IDs.</para>
        /// </returns>
        public Task<ListedResponse<long>> NoRetweetsIdsAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiArrayAsync<long>(MethodType.Get, "friendships/no_retweets/ids", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Returns a collection of user_ids that the currently authenticated user does not want to receive retweets from as an asynchronous operation.</para>
        /// <para>Use POST friendships/update to set the "no retweets" status for a given user account on behalf of the current user.</para>
        /// <para>Available parameters: Nothing.</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the IDs.</para>
        /// </returns>
        public Task<ListedResponse<long>> NoRetweetsIdsAsync<T>(T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiArrayAsync<long, T>(MethodType.Get, "friendships/no_retweets/ids", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Returns a collection of numeric IDs for every user who has a pending request to follow the authenticating user as an asynchronous operation.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> cursor (semi-optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the IDs.</para>
        /// </returns>
        public Task<Cursored<long>> IncomingAsync(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiAsync<Cursored<long>>(MethodType.Get, "friendships/incoming", parameters);
        }

        /// <summary>
        /// <para>Returns a collection of numeric IDs for every user who has a pending request to follow the authenticating user as an asynchronous operation.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> cursor (semi-optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the IDs.</para>
        /// </returns>
        public Task<Cursored<long>> IncomingAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<Cursored<long>>(MethodType.Get, "friendships/incoming", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Returns a collection of numeric IDs for every user who has a pending request to follow the authenticating user as an asynchronous operation.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> cursor (semi-optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the IDs.</para>
        /// </returns>
        public Task<Cursored<long>> IncomingAsync<T>(T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<Cursored<long>, T>(MethodType.Get, "friendships/incoming", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Returns a collection of numeric IDs for every protected user for whom the authenticating user has a pending follow request as an asynchronous operation.</para>
        /// <para>Available parameters: </para>
        /// <para>- <c>long</c> cursor (semi-optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the IDs.</para>
        /// </returns>
        public Task<Cursored<long>> OutgoingAsync(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiAsync<Cursored<long>>(MethodType.Get, "friendships/outgoing", parameters);
        }

        /// <summary>
        /// <para>Returns a collection of numeric IDs for every protected user for whom the authenticating user has a pending follow request as an asynchronous operation.</para>
        /// <para>Available parameters: </para>
        /// <para>- <c>long</c> cursor (semi-optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the IDs.</para>
        /// </returns>
        public Task<Cursored<long>> OutgoingAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<Cursored<long>>(MethodType.Get, "friendships/outgoing", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Returns a collection of numeric IDs for every protected user for whom the authenticating user has a pending follow request as an asynchronous operation.</para>
        /// <para>Available parameters: </para>
        /// <para>- <c>long</c> cursor (semi-optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the IDs.</para>
        /// </returns>
        public Task<Cursored<long>> OutgoingAsync<T>(T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<Cursored<long>, T>(MethodType.Get, "friendships/outgoing", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Returns the relationships of the authenticating user to the comma-separated list of up to 100 screen_names or user_ids provided as an asynchronous operation.</para>
        /// <para>Values for connections can be: following, following_requested, followed_by, none.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> / IEnumerable&lt;string&gt; screen_name (optional)</para>
        /// <para>- <c>string</c> / IEnumerable&lt;long&gt; user_id (ooptional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the Friendships.</para>
        /// </returns>
        public Task<ListedResponse<Friendship>> LookupAsync(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiArrayAsync<Friendship>(MethodType.Get, "friendships/lookup", parameters);
        }

        /// <summary>
        /// <para>Returns the relationships of the authenticating user to the comma-separated list of up to 100 screen_names or user_ids provided as an asynchronous operation.</para>
        /// <para>Values for connections can be: following, following_requested, followed_by, none.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> / IEnumerable&lt;string&gt; screen_name (optional)</para>
        /// <para>- <c>string</c> / IEnumerable&lt;long&gt; user_id (ooptional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the Friendships.</para>
        /// </returns>
        public Task<ListedResponse<Friendship>> LookupAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiArrayAsync<Friendship>(MethodType.Get, "friendships/lookup", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Returns the relationships of the authenticating user to the comma-separated list of up to 100 screen_names or user_ids provided as an asynchronous operation.</para>
        /// <para>Values for connections can be: following, following_requested, followed_by, none.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> / IEnumerable&lt;string&gt; screen_name (optional)</para>
        /// <para>- <c>string</c> / IEnumerable&lt;long&gt; user_id (ooptional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the Friendships.</para>
        /// </returns>
        public Task<ListedResponse<Friendship>> LookupAsync<T>(T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiArrayAsync<Friendship, T>(MethodType.Get, "friendships/lookup", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Returns detailed information about the relationship between two arbitrary users as an asynchronous operation.</para>
        /// <para>Note: At least one source and one target, whether specified by IDs or screen_names, should be provided to this method.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> source_id (optional)</para>
        /// <para>- <c>string</c> source_screen_name (optional)</para>
        /// <para>- <c>long</c> target_id (optional)</para>
        /// <para>- <c>string</c> target_screen_name (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the Friendships.</para>
        /// </returns>
        public Task<RelationShipResponse> ShowAsync(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiAsync<RelationShipResponse>(MethodType.Get, "friendships/show", parameters, "relationship");
        }

        /// <summary>
        /// <para>Returns detailed information about the relationship between two arbitrary users as an asynchronous operation.</para>
        /// <para>Note: At least one source and one target, whether specified by IDs or screen_names, should be provided to this method.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> source_id (optional)</para>
        /// <para>- <c>string</c> source_screen_name (optional)</para>
        /// <para>- <c>long</c> target_id (optional)</para>
        /// <para>- <c>string</c> target_screen_name (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the Friendships.</para>
        /// </returns>
        public Task<RelationShipResponse> ShowAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<RelationShipResponse>(MethodType.Get, "friendships/show", parameters, cancellationToken, "relationship");
        }

        /// <summary>
        /// <para>Returns detailed information about the relationship between two arbitrary users as an asynchronous operation.</para>
        /// <para>Note: At least one source and one target, whether specified by IDs or screen_names, should be provided to this method.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> source_id (optional)</para>
        /// <para>- <c>string</c> source_screen_name (optional)</para>
        /// <para>- <c>long</c> target_id (optional)</para>
        /// <para>- <c>string</c> target_screen_name (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the Friendships.</para>
        /// </returns>
        public Task<RelationShipResponse> ShowAsync<T>(T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<RelationShipResponse, T>(MethodType.Get, "friendships/show", parameters, cancellationToken, "relationship");
        }

        //POST Methods

        /// <summary>
        /// <para>Allows the authenticating users to follow the user specified in the ID parameter as an asynchronous operation.</para>
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
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the user.</para>
        /// </returns>
        public Task<UserResponse> CreateAsync(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiAsync<UserResponse>(MethodType.Post, "friendships/create", parameters);
        }

        /// <summary>
        /// <para>Allows the authenticating users to follow the user specified in the ID parameter as an asynchronous operation.</para>
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
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the user.</para>
        /// </returns>
        public Task<UserResponse> CreateAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<UserResponse>(MethodType.Post, "friendships/create", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Allows the authenticating users to follow the user specified in the ID parameter as an asynchronous operation.</para>
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
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the user.</para>
        /// </returns>
        public Task<UserResponse> CreateAsync<T>(T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<UserResponse, T>(MethodType.Post, "friendships/create", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Allows the authenticating user to unfollow the user specified in the ID parameter as an asynchronous operation.</para>
        /// <para>Returns the unfollowed user in the requested format when successful.</para>
        /// <para>Returns a string describing the failure condition when unsuccessful.</para>
        /// <para>Actions taken in this method are asynchronous and changes will be eventually consistent.</para>
        /// <para>Note: Providing either screen_name or user_id is required.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> screen_name (optional)</para>
        /// <para>- <c>long</c> user_id (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the user.</para>
        /// </returns>
        public Task<UserResponse> DestroyAsync(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiAsync<UserResponse>(MethodType.Post, "friendships/destroy", parameters);
        }

        /// <summary>
        /// <para>Allows the authenticating user to unfollow the user specified in the ID parameter as an asynchronous operation.</para>
        /// <para>Returns the unfollowed user in the requested format when successful.</para>
        /// <para>Returns a string describing the failure condition when unsuccessful.</para>
        /// <para>Actions taken in this method are asynchronous and changes will be eventually consistent.</para>
        /// <para>Note: Providing either screen_name or user_id is required.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> screen_name (optional)</para>
        /// <para>- <c>long</c> user_id (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the user.</para>
        /// </returns>
        public Task<UserResponse> DestroyAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<UserResponse>(MethodType.Post, "friendships/destroy", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Allows the authenticating user to unfollow the user specified in the ID parameter as an asynchronous operation.</para>
        /// <para>Returns the unfollowed user in the requested format when successful.</para>
        /// <para>Returns a string describing the failure condition when unsuccessful.</para>
        /// <para>Actions taken in this method are asynchronous and changes will be eventually consistent.</para>
        /// <para>Note: Providing either screen_name or user_id is required.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> screen_name (optional)</para>
        /// <para>- <c>long</c> user_id (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the user.</para>
        /// </returns>
        public Task<UserResponse> DestroyAsync<T>(T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<UserResponse, T>(MethodType.Post, "friendships/destroy", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Allows one to enable or disable retweets and device notifications from the specified user as an asynchronous operation.</para>
        /// <para>Note: Providing either screen_name or user_id is required.</para>
        /// <para>Available parameters: </para>
        /// <para>- <c>string</c> screen_name (optional)</para>
        /// <para>- <c>long</c> user_id (optional)</para>
        /// <para>- <c>bool</c> device (optional)</para>
        /// <para>- <c>bool</c> retweets (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the Friendships.</para>
        /// </returns>
        public Task<RelationShipResponse> UpdateAsync(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiAsync<RelationShipResponse>(MethodType.Post, "friendships/update", parameters, "relationship");
        }

        /// <summary>
        /// <para>Allows one to enable or disable retweets and device notifications from the specified user as an asynchronous operation.</para>
        /// <para>Note: Providing either screen_name or user_id is required.</para>
        /// <para>Available parameters: </para>
        /// <para>- <c>string</c> screen_name (optional)</para>
        /// <para>- <c>long</c> user_id (optional)</para>
        /// <para>- <c>bool</c> device (optional)</para>
        /// <para>- <c>bool</c> retweets (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the Friendships.</para>
        /// </returns>
        public Task<RelationShipResponse> UpdateAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<RelationShipResponse>(MethodType.Post, "friendships/update", parameters, cancellationToken, "relationship");
        }

        /// <summary>
        /// <para>Allows one to enable or disable retweets and device notifications from the specified user as an asynchronous operation.</para>
        /// <para>Note: Providing either screen_name or user_id is required.</para>
        /// <para>Available parameters: </para>
        /// <para>- <c>string</c> screen_name (optional)</para>
        /// <para>- <c>long</c> user_id (optional)</para>
        /// <para>- <c>bool</c> device (optional)</para>
        /// <para>- <c>bool</c> retweets (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the Friendships.</para>
        /// </returns>
        public Task<RelationShipResponse> UpdateAsync<T>(T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<RelationShipResponse, T>(MethodType.Post, "friendships/update", parameters, cancellationToken, "relationship");
        }
    }
}
