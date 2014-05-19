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
    partial class Lists
    {
        //GET Methods

        /// <summary>
        /// <para>Returns all lists the authenticating or specified user subscribes to, including their own. The user is specified using the user_id or screen_name parameters. If no user is given, the authenticating user is used.</para>
        /// <para>This method used to be GET lists in version 1.0 of the API and has been renamed for consistency with other call.</para>
        /// <para>Avaliable parameters: </para>
        /// <para><paramref name="long user_id (optional)"/> : The ID of the user for whom to return results for. Helpful for disambiguating when a valid user ID is also a valid screen name.</para>
        /// <para><paramref name="string screen_name (optional)"/> : The screen name of the user for whom to return results for. Helpful for disambiguating when a valid screen name is also a user ID.</para>
        /// </summary>
        /// <returns>Lists.</returns>
        /// <param name='parameters'>
        /// Parameters.
        /// </param>
        public Task<ListedResponse<CoreTweet.List>> ListAsync(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiArrayAsync<CoreTweet.List>(MethodType.Get, "lists/list", parameters);
        }
        public Task<ListedResponse<CoreTweet.List>> ListAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiArrayAsync<CoreTweet.List>(MethodType.Get, "lists/list", parameters, cancellationToken);
        }
        public Task<ListedResponse<CoreTweet.List>> ListAsync<T>(T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiArrayAsync<CoreTweet.List, T>(MethodType.Get, "lists/list", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Returns the lists the specified user has been added to. If user_id or screen_name are not provided the memberships for the authenticating user are returned.</para>
        /// <see cref="https://dev.twitter.com/docs/misc/cursoring"/>
        /// <para>Avaliable parameters: </para>
        /// <para><paramref name="string sereen_name (optional)"/> : The screen name of the user for whom to return results for. Helpful for disambiguating when a valid screen name is also a user ID.</para>
        /// <para><paramref name="long user_id (optional)"/> : The ID of the user for whom to return results for. Helpful for disambiguating when a valid user ID is also a valid screen name.</para>
        /// <para><paramref name="long cursor (semi-optional)"/> : Breaks the results into pages. A single page contains 20 lists. Provide a value of -1 to begin paging. Provide values as returned in the response body's next_cursor and previous_cursor attributes to page back and forth in the list.</para>
        /// <para><paramref name="bool filter_to_owned_lists (optional)"/> : When set to true, will return just lists the authenticating user owns, and the user represented by user_id or screen_name is a member of.</para>
        /// </summary>
        /// <returns>Users.</returns>
        /// <param name='parameters'>
        /// Parameters.
        /// </param>
        public Task<Cursored<User>> MembershipsAsync(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiAsync<Cursored<User>>(MethodType.Get, "lists/memberships", parameters);
        }
        public Task<Cursored<User>> MembershipsAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<Cursored<User>>(MethodType.Get, "lists/memberships", parameters, cancellationToken);
        }
        public Task<Cursored<User>> MembershipsAsync<T>(T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<Cursored<User>, T>(MethodType.Get, "lists/memberships", parameters, cancellationToken);
        }
        
        /// <summary>
        /// <para>Returns the specified list. Private lists will only be shown if the authenticated user owns the specified list.</para>
        /// <para>Note: Either a list_id or a slug is required. If providing a list_slug, an owner_screen_name or owner_id is also required.</para>
        /// <para>Avaliable parameters: </para>
        /// <para><paramref name="long list_id (required)"/> : The numerical id of the list.</para>
        /// <para><paramref name="string slug (required)"/> : You can identify a list by its slug instead of its numerical id. If you decide to do so, note that you'll also have to specify the list owner using the owner_id or owner_screen_name parameters.</para>
        /// <para><paramref name="string owner_screen_name (optional)"/> : The screen name of the user who owns the list being requested by a slug.</para>
        /// <para><paramref name="long owner_id (optional)"/> : The user ID of the user who owns the list being requested by a slug.</para>
        /// </summary>
        /// <returns>A list.</returns>
        /// <param name='parameters'>
        /// Parameters.
        /// </param>
        public Task<ListResponse> ShowAsync(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiAsync<ListResponse>(MethodType.Get, "lists/show", parameters);
        }
        public Task<ListResponse> ShowAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<ListResponse>(MethodType.Get, "lists/show", parameters, cancellationToken);
        }
        public Task<ListResponse> ShowAsync<T>(T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<ListResponse, T>(MethodType.Get, "lists/show", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Obtain a collection of the lists the specified user is subscribed to, 20 lists per page by default. Does not include the user's own lists.</para>
        /// <para>Note: A user_id or screen_name must be provided.</para>
        /// <para>Avaliable parameters: </para>
        /// <para><paramref name="long user_id (optional)"/> : The ID of the user for whom to return results for. Helpful for disambiguating when a valid user ID is also a valid screen name.</para>
        /// <para><paramref name="string screen_name (optional)"/> : The screen name of the user for whom to return results for. Helpful for disambiguating when a valid screen name is also a user ID.</para>
        /// <para><paramref name="int count (optional)"/> : The amount of results to return per page. Defaults to 20. Maximum of 1,000 when using cursors.</para>
        /// <para><paramref name="long cursor (optional)"/> : Breaks the results into pages. A single page contains 20 lists. Provide a value of -1 to begin paging. Provide values as returned in the response body's next_cursor and previous_cursor attributes to page back and forth in the list. It is recommended to always use cursors when the method supports them.</para>
        /// </summary>
        /// <returns>Lists.</returns>
        /// <param name='parameters'>
        /// Parameters.
        /// </param>
        public Task<Cursored<CoreTweet.List>> SubscriptionsAsync(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiAsync<Cursored<CoreTweet.List>>(MethodType.Get, "lists/subscriptions", parameters);
        }
        public Task<Cursored<CoreTweet.List>> SubscriptionsAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<Cursored<CoreTweet.List>>(MethodType.Get, "lists/subscriptions", parameters, cancellationToken);
        }
        public Task<Cursored<CoreTweet.List>> SubscriptionsAsync<T>(T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<Cursored<CoreTweet.List>, T>(MethodType.Get, "lists/subscriptions", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Returns tweet timeline for members of the specified list. Retweets are included by default. You can use the include_rts=false parameter to omit retweet objects.</para>
        /// <para>Note: Either a list_id or a slug is required. If providing a list_slug, an owner_screen_name or owner_id is also required.</para>
        /// <para>Avaliable parameters: </para>
        /// <para><paramref name="long list_id (required)"/> : The numerical id of the list.</para>
        /// <para><paramref name="string slug (required)"/> : You can identify a list by its slug instead of its numerical id. If you decide to do so, note that you'll also have to specify the list owner using the owner_id or owner_screen_name parameters.</para>
        /// <para><paramref name="string owner_screen_name (optional)"/> : The screen name of the user who owns the list being requested by a slug.</para>
        /// <para><paramref name="long owner_id (optional)"/> : The user ID of the user who owns the list being requested by a slug.</para>
        /// <para><paramref name="long since_id (optional)"/> : Returns results with an ID greater than (that is, more recent than) the specified ID. There are limits to the number of Tweets which can be accessed through the API. If the limit of Tweets has occured since the since_id, the since_id will be forced to the oldest ID available.</para>
        /// <para><paramref name="long max_id (optional)"/> : Returns results with an ID less than (that is, older than) or equal to the specified ID.</para>
        /// <para><paramref name="int count (optional)"/> : Specifies the number of results to retrieve per page.</para>
        /// <para><paramref name="bool include_entities (optional)"/> : Entities are ON by default in API 1.1, each tweet includes a node called entities. This node offers a variety of metadata about the tweet in a discreet structure, including: user_mentions, urls, and hashtags. You can omit entities from the result by using include_entities=false.<\para>
        /// <para><paramref name="bool include_rts (optional)"/> : When set to true, the list timeline will contain native retweets (if they exist) in addition to the standard stream of tweets. The output format of retweeted tweets is identical to the representation you see in home_timeline.</para>
        /// </summary>
        /// <returns>Statuses.</returns>
        /// <param name='parameters'>
        /// Parameters.
        /// </param>
        public Task<ListedResponse<Status>> StatusesAsync(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiArrayAsync<Status>(MethodType.Get, "lists/statuses", parameters);
        }
        public Task<ListedResponse<Status>> StatusesAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiArrayAsync<Status>(MethodType.Get, "lists/statuses", parameters, cancellationToken);
        }
        public Task<ListedResponse<Status>> StatusesAsync<T>(T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiArrayAsync<Status, T>(MethodType.Get, "lists/statuses", parameters, cancellationToken);
        }

        // POST Methods

        /// <summary>
        /// <para>Creates a new list for the authenticated user. Note that you can't create more than 20 lists per account.</para>
        /// <para>Avaliable parameters: </para>
        /// <para><paramref name="string name (required)"/> : The name for the list.A list's name must start with a letter and can consist only of 25 or fewer letters, numbers, "-", or "_" characters.</para>
        /// <para><paramref name="string mode (optional)"/> : Whether your list is public or private. Values can be public or private. If no mode is specified the list will be public.</para>
        /// <para><paramref name="string description (optional)"/> : The description to give the list.</para>
        /// </summary>
        /// <returns>The list.</returns>
        /// <param name='parameters'>
        /// Parameters.
        /// </param>
        public Task<CoreTweet.List> CreateAsync(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiAsync<CoreTweet.List>(MethodType.Post, "lists/create", parameters);
        }
        public Task<CoreTweet.List> CreateAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<CoreTweet.List>(MethodType.Post, "lists/create", parameters, cancellationToken);
        }
        public Task<CoreTweet.List> CreateAsync<T>(T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<CoreTweet.List, T>(MethodType.Post, "lists/create", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Deletes the specified list. The authenticated user must own the list to be able to destroy it.</para>
        /// <para>Note: Either a list_id or a slug is required. If providing a list_slug, an owner_screen_name or owner_id is also required.</para>
        /// <para>Avaliable parameters: </para>
        /// <para><paramref name="string owner_screen_name (optional)"/> : The screen name of the user who owns the list being requested by a slug.</para>
        /// </summary>
        /// <returns>The destroied list.</returns>
        /// <param name='parameters'>
        /// Parameters.
        /// </param>
        public Task<CoreTweet.List> DestroyAsync(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiAsync<CoreTweet.List>(MethodType.Post, "lists/destroy", parameters);
        }
        public Task<CoreTweet.List> DestroyAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<CoreTweet.List>(MethodType.Post, "lists/destroy", parameters, cancellationToken);
        }
        public Task<CoreTweet.List> DestroyAsync<T>(T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<CoreTweet.List, T>(MethodType.Post, "lists/destroy", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Updates the specified list. The authenticated user must own the list to be able to update it.</para>
        /// <para>Note: Either a list_id or a slug is required. If providing a list_slug, an owner_screen_name or owner_id is also required.</para>
        /// <para>Avaliable parameters: </para>
        /// <para><paramref name="long list_id (required)"/> : The numerical id of the list.</para>
        /// <para><paramref name="string slug (required)"/> : You can identify a list by its slug instead of its numerical id. If you decide to do so, note that you'll also have to specify the list owner using the owner_id or owner_screen_name parameters.</para>
        /// <para><paramref name="string name (optional)"/> : The name for the list.</para>
        /// <para><paramref name="string mode (optional)"/> : Whether your list is public or private. Values can be public or private. If no mode is specified the list will be public.</para>
        /// <para><paramref name="string description (optional)"/> : The description to give the list.</para>
        /// <para><paramref name="string owner_screen_name (optional)"/> : The screen name of the user who owns the list being requested by a slug.</para>
        /// <para><paramref name="long owner_id (optional)"/> : The user ID of the user who owns the list being requested by a slug.</para>
        /// </summary>
        /// <returns>The list.</returns>
        /// <param name='parameters'>
        /// Parameters.
        /// </param>
        public Task<CoreTweet.List> UpdateAsync(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiAsync<CoreTweet.List>(MethodType.Post, "lists/update", parameters);
        }
        public Task<CoreTweet.List> UpdateAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<CoreTweet.List>(MethodType.Post, "lists/update", parameters, cancellationToken);
        }
        public Task<CoreTweet.List> UpdateAsync<T>(T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<CoreTweet.List, T>(MethodType.Post, "lists/update", parameters, cancellationToken);
        }
    }

    partial class Members
    {
        //GET Methods

        /// <summary>
        /// <para>Returns the members of the specified list. Private list members will only be shown if the authenticated user owns the specified list.</para>
        /// <para>Note: Either a list_id or a slug is required. If providing a list_slug, an owner_screen_name or owner_id is also required.</para>
        /// <para>The response from the API will include a previous_cursor and next_cursor to allow paging back and forth. See Using cursors to navigate collections for more information.</para>
        /// <see cref="https://dev.twitter.com/docs/misc/cursoring"/>
        /// <para>Avaliable parameters: </para>
        /// <para><paramref name="long list_id (required)"/> : The numerical id of the list.</para>
        /// <para><paramref name="string slug (required)"/> : You can identify a list by its slug instead of its numerical id. If you decide to do so, note that you'll also have to specify the list owner using the owner_id or owner_screen_name parameters.</para>
        /// <para><paramref name="string owner_sereen_name (optional)"/> : The screen name of the user who owns the list being requested by a slug.</para>
        /// <para><paramref name="long owner_id (optional)"/> : The user ID of the user who owns the list being requested by a slug.</para>
        /// <para><paramref name="long cursor (semi-optional)"/> : Causes the collection of list members to be broken into "pages" of somewhat consistent size. If no cursor is provided, a value of -1 will be assumed, which is the first "page".</para>
        /// </summary>
        /// <returns>Users.</returns>
        /// <param name='parameters'>
        /// Parameters.
        /// </param>
        public Task<Cursored<User>> ListAsync(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiAsync<Cursored<User>>(MethodType.Get, "lists/members", parameters);
        }
        public Task<Cursored<User>> ListAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<Cursored<User>>(MethodType.Get, "lists/members", parameters, cancellationToken);
        }
        public Task<Cursored<User>> ListAsync<T>(T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<Cursored<User>, T>(MethodType.Get, "lists/members", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Check if the specified user is a member of the specified list.</para>
        /// <para>Note: Either a list_id or a slug is required. If providing a list_slug, an owner_screen_name or owner_id is also required.</para>
        /// <para>Avaliable parameters: </para>
        /// <para><paramref name="long list_id (required)"/> : The numerical id of the list.</para>
        /// <para><paramref name="string slug (required)"/> : You can identify a list by its slug instead of its numerical id. If you decide to do so, note that you'll also have to specify the list owner using the owner_id or owner_screen_name parameters.</para>
        /// <para><paramref name="string sereen_name (required)"/> : The screen name of the user for whom to return results for. Helpful for disambiguating when a valid screen name is also a user ID.</para>
        /// <para><paramref name="long user_id (required)"/> : The ID of the user for whom to return results for. Helpful for disambiguating when a valid user ID is also a valid screen name.</para>
        /// <para><paramref name="string owner_screen_name (optional)"/> : The screen name of the user who owns the list being requested by a slug.</para>
        /// <para><paramref name="long owner_id (optional)"/> : The user ID of the user who owns the list being requested by a slug.</para>
        /// </summary>
        /// <returns>The user.</returns>
        /// <param name='parameters'>
        /// Parameters.
        /// </param>
        public Task<UserResponse> ShowAsync(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiAsync<UserResponse>(MethodType.Get, "lists/members/show", parameters);
        }
        public Task<UserResponse> ShowAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<UserResponse>(MethodType.Get, "lists/members/show", parameters, cancellationToken);
        }
        public Task<UserResponse> ShowAsync<T>(T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<UserResponse, T>(MethodType.Get, "lists/members/show", parameters, cancellationToken);
        }

        //POST Methods

        /// <summary>
        /// <para>Add a member to a list. The authenticated user must own the list to be able to add members to it. Note that lists can't have more than 500 members.</para>
        /// <para>Note: Either a list_id or a slug is required. If providing a list_slug, an owner_screen_name or owner_id is also required.</para>
        /// <para>Avaliable parameters: </para><para> </para>
        /// <para><paramref name="long list_id (required)"/> : The numerical id of the list.</para>
        /// <para><paramref name="string slug (required)"/> : You can identify a list by its slug instead of its numerical id. If you decide to do so, note that you'll also have to specify the list owner using the owner_id or owner_screen_name parameters.</para>
        /// <para><paramref name="long user_id (required)"/> : The ID of the user for whom to return results for. Helpful for disambiguating when a valid user ID is also a valid screen name.</para>
        /// <para><paramref name="string screen_name (required)"/> : The screen name of the user for whom to return results for. Helpful for disambiguating when a valid screen name is also a user ID.</para>
        /// <para><paramref name="string owner_screen_name (optional)"/> : The screen name of the user who owns the list being requested by a slug.</para>
        /// <para><paramref name="long owner_id (optional)"/> : The user ID of the user who owns the list being requested by a slug.</para>
        /// </summary>
        /// <returns>The list.</returns>
        /// <param name='parameters'>
        /// Parameters.
        /// </param>
        public Task<List> CreateAsync(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiAsync<List>(MethodType.Post, "lists/members/create", parameters);
        }
        public Task<List> CreateAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<List>(MethodType.Post, "lists/members/create", parameters, cancellationToken);
        }
        public Task<List> CreateAsync<T>(T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<List, T>(MethodType.Post, "lists/members/create", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Adds multiple members to a list, by specifying a comma-separated list of member ids or screen names. The authenticated user must own the list to be able to add members to it. Note that lists can't have more than 500 members, and you are limited to adding up to 100 members to a list at a time with this method.</para>
        /// <para>Please note that there can be issues with lists that rapidly remove and add memberships. Take care when using these methods such that you are not too rapidly switching between removals and adds on the same list.</para>
        /// <para>Note: Either a list_id or a slug is required. If providing a list_slug, an owner_screen_name or owner_id is also required.</para>
        /// <para>Avaliable parameters: </para><para> </para>
        /// <para><paramref name="long list_id (required)"/> : The numerical id of the list.</para>
        /// <para><paramref name="string slug (required)"/> : You can identify a list by its slug instead of its numerical id. If you decide to do so, note that you'll also have to specify the list owner using the owner_id or owner_screen_name parameters.</para>
        /// <para><paramref name="string, IEnumerable<long> user_id (optional)"/> : A list of user IDs or comma separated string of ones, up to 100 are allowed in a single request.</para>
        /// <para><paramref name="string, IEnumerable<string> screen_name (optional)"/> : A list of screen names or comma separated string of ones, up to 100 are allowed in a single request.</para>
        /// <para><paramref name="string owner_screen_name (optional)"/> : The screen name of the user who owns the list being requested by a slug.</para>
        /// <para><paramref name="long owner_id (optional)"/> : The user ID of the user who owns the list being requested by a slug.</para>
        /// </summary>
        /// <returns>The list.</returns>
        /// <param name='parameters'>
        /// Parameters.
        /// </param>
        public Task<List> CreateAllAsync(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiAsync<List>(MethodType.Post, "lists/members/create_all", parameters);
        }
        public Task<List> CreateAllAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<List>(MethodType.Post, "lists/members/create_all", parameters, cancellationToken);
        }
        public Task<List> CreateAllAsync<T>(T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<List, T>(MethodType.Post, "lists/members/create_all", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Removes the specified member from the list. The authenticated user must be the list's owner to remove members from the list.</para>
        /// <para>Note: Either a list_id or a slug is required. If providing a list_slug, an owner_screen_name or owner_id is also required.</para>
        /// <para>Avaliable parameters: </para><para> </para>
        /// <para><paramref name="long list_id (required)"/> : The numerical id of the list.</para>
        /// <para><paramref name="string slug (required)"/> : You can identify a list by its slug instead of its numerical id. If you decide to do so, note that you'll also have to specify the list owner using the owner_id or owner_screen_name parameters.</para>
        /// <para><paramref name="long user_id (required)"/> : The ID of the user to remove from the list. Helpful for disambiguating when a valid user ID is also a valid screen name.</para>
        /// <para><paramref name="string screen_name (required)"/> : The screen name of the user for whom to remove from the list. Helpful for disambiguating when a valid screen name is also a user ID.</para>
        /// <para><paramref name="string owner_screen_name (optional)"/> : The screen name of the user who owns the list being requested by a slug.</para>
        /// <para><paramref name="long owner_id (optional)"/> : The user ID of the user who owns the list being requested by a slug.</para>
        /// </summary>
        /// <returns>The list.</returns>
        /// <param name='parameters'>
        /// Parameters.
        /// </param>
        public Task<List> DeleteAsync(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiAsync<List>(MethodType.Post, "lists/members/delete", parameters);
        }
        public Task<List> DeleteAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<List>(MethodType.Post, "lists/members/delete", parameters, cancellationToken);
        }
        public Task<List> DeleteAsync<T>(T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<List, T>(MethodType.Post, "lists/members/delete", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Removes multiple members from a list, by specifying a comma-separated list of member ids or screen names. The authenticated user must own the list to be able to remove members from it. Note that lists can't have more than 500 members, and you are limited to removing up to 100 members to a list at a time with this method.</para>
        /// <para>Please note that there can be issues with lists that rapidly remove and add memberships. Take care when using these methods such that you are not too rapidly switching between removals and adds on the same list.</para>
        /// <para>Note: Either a list_id or a slug is required. If providing a list_slug, an owner_screen_name or owner_id is also required.</para>
        /// <para>Avaliable parameters: </para><para> </para>
        /// <para><paramref name="long list_id (required)"/> : The numerical id of the list.</para>
        /// <para><paramref name="string slug (required)"/> : You can identify a list by its slug instead of its numerical id. If you decide to do so, note that you'll also have to specify the list owner using the owner_id or owner_screen_name parameters.</para>
        /// <para><paramref name="string, IEnumerable<long> user_id (optional)"/> : A list of user IDs or comma separated string of ones, up to 100 are allowed in a single request.</para>
        /// <para><paramref name="string, IEnumerable<string> screen_name (optional)"/> : A list of screen names or comma separated string of ones, up to 100 are allowed in a single request.</para>
        /// <para><paramref name="string owner_screen_name (optional)"/> : The screen name of the user who owns the list being requested by a slug.</para>
        /// <para><paramref name="long owner_id (optional)"/> : The user ID of the user who owns the list being requested by a slug.</para>
        /// </summary>
        /// <returns>The list.</returns>
        /// <param name='parameters'>
        /// Parameters.
        /// </param>
        public Task<List> DeleteAllAsync(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiAsync<List>(MethodType.Post, "lists/members/delete_all", parameters);
        }
        public Task<List> DeleteAllAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<List>(MethodType.Post, "lists/members/delete_all", parameters, cancellationToken);
        }
        public Task<List> DeleteAllAsync<T>(T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<List, T>(MethodType.Post, "lists/members/delete_all", parameters, cancellationToken);
        }
    }

    partial class Subscribers
    {
        //GET Method

        /// <summary>
        /// <para>Check if the specified user is a subscriber of the specified list. Returns the user if they are subscriber.</para>
        /// <para>Note: Either a list_id or a slug is required. If providing a list_slug, an owner_screen_name or owner_id is also required.</para>
        /// <para>Avaliable parameters: </para>
        /// <para><paramref name="string owner_screen_name (optional)"/> : The screen name of the user who owns the list being requested by a slug.</para>
        /// <para><paramref name="long owner_id (optional)"/> : The user ID of the user who owns the list being requested by a slug.</para>
        /// <para><paramref name="long list_id (required)"/> : The numerical id of the list.</para>
        /// <para><paramref name="string slug (required)"/> : You can identify a list by its slug instead of its numerical id. If you decide to do so, note that you'll also have to specify the list owner using the owner_id or owner_screen_name parameters.</para>
        /// <para><paramref name="long user_id (required)"/> : The ID of the user for whom to return results for. Helpful for disambiguating when a valid user ID is also a valid screen name.</para>
        /// <para><paramref name="string screen_name (required)"/> : The screen name of the user for whom to return results for. Helpful for disambiguating when a valid screen name is also a user ID.</para>
        /// <para><paramref name="bool include_entities"/> : When set to true, each tweet will include a node called "entities". This node offers a variety of metadata about the tweet in a discreet structure, including: user_mentions, urls, and hashtags. While entities are opt-in on timelines at present, they will be made a default component of output in the future. See Tweet Entities for more details.</para>
        /// <para><paramref name="bool skip_status"/> : When set to true, statuses will not be included in the returned user objects.</para>
        /// </summary>
        /// <returns>The user.</returns>
        /// <param name='parameters'>
        /// Parameters.
        /// </param>
        public Task<UserResponse> ShowAsync(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiAsync<UserResponse>(MethodType.Get, "lists/subscribers/show", parameters);
        }
        public Task<UserResponse> ShowAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<UserResponse>(MethodType.Get, "lists/subscribers/show", parameters, cancellationToken);
        }
        public Task<UserResponse> ShowAsync<T>(T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<UserResponse, T>(MethodType.Get, "lists/subscribers/show", parameters, cancellationToken);
        }

        //POST Method

        /// <summary>
        /// <para>Subscribes the authenticated user to the specified list.</para>
        /// <para>Note: Either a list_id or a slug is required. If providing a list_slug, an owner_screen_name or owner_id is also required.</para>
        /// <para>Avaliable parameters: </para><para> </para>
        /// <para><paramref name="long list_id (required)"/> : The numerical id of the list.</para>
        /// <para><paramref name="string slug (required)"/> : You can identify a list by its slug instead of its numerical id. If you decide to do so, note that you'll also have to specify the list owner using the owner_id or owner_screen_name parameters.</para>
        /// <para><paramref name="string owner_screen_name (optional)"/> : The screen name of the user who owns the list being requested by a slug.</para>
        /// <para><paramref name="long owner_id (optional)"/> : The user ID of the user who owns the list being requested by a slug.</para>
        /// </summary>
        /// <returns>The list.</returns>
        /// <param name='parameters'>
        /// Parameters.
        /// </param>
        public Task<List> CreateAsync(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiAsync<List>(MethodType.Post, "lists/subscribers/create", parameters);
        }
        public Task<List> CreateAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<List>(MethodType.Post, "lists/subscribers/create", parameters, cancellationToken);
        }
        public Task<List> CreateAsync<T>(T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<List, T>(MethodType.Post, "lists/subscribers/create", parameters, cancellationToken);
        }

        /// <summary>
        /// <paraUnsubscribes the authenticated user from the specified list.</para>
        /// <para>Note: Either a list_id or a slug is required. If providing a list_slug, an owner_screen_name or owner_id is also required.</para>
        /// <para>Avaliable parameters: </para><para> </para>
        /// <para><paramref name="long list_id (required)"/> : The numerical id of the list.</para>
        /// <para><paramref name="string slug (required)"/> : You can identify a list by its slug instead of its numerical id. If you decide to do so, note that you'll also have to specify the list owner using the owner_id or owner_screen_name parameters.</para>
        /// <para><paramref name="string owner_screen_name (optional)"/> : The screen name of the user who owns the list being requested by a slug.</para>
        /// <para><paramref name="long owner_id (optional)"/> : The user ID of the user who owns the list being requested by a slug.</para>
        /// </summary>
        /// <returns>The list.</returns>
        /// <param name='parameters'>
        /// Parameters.
        /// </param>
        public Task<List> DeleteAsync(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiAsync<List>(MethodType.Post, "lists/subscribers/delete", parameters);
        }
        public Task<List> DeleteAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<List>(MethodType.Post, "lists/subscribers/delete", parameters, cancellationToken);
        }
        public Task<List> DeleteAsync<T>(T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<List, T>(MethodType.Post, "lists/subscribers/delete", parameters, cancellationToken);
        }
    }
}
