// The MIT License (MIT)
//
// CoreTweet - A .NET Twitter Library supporting Twitter API 1.1
// Copyright (c) 2013 lambdalice
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

    /// <summary>GET/POST lists</summary>
    public class Lists : TokenIncluded
    {
             
        
        internal Lists(Tokens e) : base(e) { }
        
        public Members Members { get { return new Members(this.Tokens); } }
        
        public Subscribers Subscribers { get { return new Subscribers(this.Tokens); } }
        
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
        public IEnumerable<CoreTweet.List> List(params Expression<Func<string,object>>[] parameters)
        {
            return this.Tokens.AccessApiArray<CoreTweet.List>(MethodType.Get, "lists/list", parameters);
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
        public Cursored<User> Memberships(params Expression<Func<string,object>>[] parameters)
        {
            return this.Tokens.AccessApi<Cursored<User>>(MethodType.Get, "lists/memberships", parameters);
        }

        /// <summary>
        /// <para>Returns the lists the specified user has been added to. If user_id or screen_name are not provided the memberships for the authenticating user are returned.</para>
        /// <see cref="https://dev.twitter.com/docs/misc/cursoring"/>
        /// <para>Avaliable parameters: </para>
        /// <para><paramref name="string sereen_name (optional)"/> : The screen name of the user for whom to return results for. Helpful for disambiguating when a valid screen name is also a user ID.</para>
        /// <para><paramref name="long user_id (optional)"/> : The ID of the user for whom to return results for. Helpful for disambiguating when a valid user ID is also a valid screen name.</para>
        /// <para><paramref name="bool filter_to_owned_lists (optional)"/> : When set to true, will return just lists the authenticating user owns, and the user represented by user_id or screen_name is a member of.</para>
        /// <para><paramref name="long cursor (optional)"/> : The first cursor. If not be specified, enumerating starts from the first page.</para>
        /// </summary>
        /// <returns>
        /// Users.
        /// </returns>
        /// <see cref="https://dev.twitter.com/docs/misc/cursoring"/>
        /// <param name='mode'>
        /// <para> Specify whether enumerating goes to the next page or the previous.</para>
        /// </param>
        /// <param name='parameters'>
        /// Parameters.
        /// </param>
        public IEnumerable<User> EnumerateMemberships(EnumerateMode mode, params Expression<Func<string,object>>[] parameters)
        {
            return Cursored<User>.Enumerate(this.Tokens, "lists/memberships", mode, parameters);
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
        public CoreTweet.List Show(params Expression<Func<string,object>>[] parameters)
        {
            return this.Tokens.AccessApi<CoreTweet.List>(MethodType.Get, "lists/show", parameters);
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
        public Cursored<CoreTweet.List> Subscriptions(params Expression<Func<string,object>>[] parameters)
        {
            return this.Tokens.AccessApi<Cursored<CoreTweet.List>>(MethodType.Get, "lists/subscriptions", parameters);
        }

        /// <summary>
        /// <para>Enumerate lists the specified user is subscribed to, 20 lists per page by default. Does not include the user's own lists.</para>
        /// <para>Note: A user_id or screen_name must be provided.</para>
        /// <para>Avaliable parameters: </para>
        /// <para><paramref name="long user_id (optional)"/> : The ID of the user for whom to return results for. Helpful for disambiguating when a valid user ID is also a valid screen name.</para>
        /// <para><paramref name="string screen_name (optional)"/> : The screen name of the user for whom to return results for. Helpful for disambiguating when a valid screen name is also a user ID.</para>
        /// <para><paramref name="int count (optional)"/> : The amount of results to return per page. Defaults to 20. Maximum of 1,000 when using cursors.</para>
        /// <para><paramref name="long cursor (optional)"/> : The first cursor. If not be specified, enumerating starts from the first page.</para>
        /// </summary>
        /// <returns>
        /// Users.
        /// </returns>
        /// <see cref="https://dev.twitter.com/docs/misc/cursoring"/>
        /// <param name='mode'>
        /// <para> Specify whether enumerating goes to the next page or the previous.</para>
        /// </param>
        /// <param name='parameters'>
        /// Parameters.
        /// </param>
        public IEnumerable<User> EnumerateSubscriptions(EnumerateMode mode, params Expression<Func<string,object>>[] parameters)
        {
            return Cursored<User>.Enumerate(this.Tokens, "lists/subscriptions", mode, parameters);
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
        public IEnumerable<Status> Statuses(params Expression<Func<string,object>>[] parameters)
        {
            return this.Tokens.AccessApiArray<Status>(MethodType.Get, "lists/statuses", parameters);
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
        public CoreTweet.List Create(params Expression<Func<string,object>>[] parameters)
        {
            return this.Tokens.AccessApi<CoreTweet.List>(MethodType.Post, "lists/create", parameters);
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
        public CoreTweet.List Destroy(params Expression<Func<string,object>>[] parameters)
        {
            return this.Tokens.AccessApi<CoreTweet.List>(MethodType.Post, "lists/destroy", parameters);
        }

        //FIXME: The format of the response is not known.
        //UNDONE: Write a document comment in this endpoint 
        /// <summary>
        /// <para></para>
        /// <para>Avaliable parameters: </para>
        /// </summary>
        /// <returns>The list.</returns>
        /// <param name='tokens'>
        /// OAuth Tokens.
        /// </param>
        /// <param name='parameters'>
        /// Parameters.
        /// </param>
        public CoreTweet.List Update(Expression<Func<string,object>> parameters)
        {
            return this.Tokens.AccessApi<CoreTweet.List>(MethodType.Post, "lists/update", parameters);
        }
    }

    public class Members : TokenIncluded
    {
        internal Members(Tokens tokens): base(tokens) { }
       
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
        public Cursored<User> This(params Expression<Func<string,object>>[] parameters)
        {
            return this.Tokens.AccessApi<Cursored<User>>(MethodType.Get, "lists/members", parameters);
        }

        /// <summary>
        /// <para>Returns the members of the specified list. Private list members will only be shown if the authenticated user owns the specified list.</para>
        /// <para>Note: Either a list_id or a slug is required. If providing a list_slug, an owner_screen_name or owner_id is also required.</para>
        /// <para>The response from the API will include a previous_cursor and next_cursor to allow paging back and forth. See Using cursors to navigate collections for more information.</para>
        /// <para>Avaliable parameters: </para>
        /// <para><paramref name="long list_id (required)"/> : The numerical id of the list.</para>
        /// <para><paramref name="string slug (required)"/> : You can identify a list by its slug instead of its numerical id. If you decide to do so, note that you'll also have to specify the list owner using the owner_id or owner_screen_name parameters.</para>
        /// <para><paramref name="string owner_sereen_name (optional)"/> : The screen name of the user who owns the list being requested by a slug.</para>
        /// <para><paramref name="long owner_id (optional)"/> : The user ID of the user who owns the list being requested by a slug.</para>
        /// <para><paramref name="long cursor (optional)"/> : The first cursor. If not be specified, enumerating starts from the first page.</para>
        /// </summary>
        /// <returns>
        /// Users.
        /// </returns>
        /// <see cref="https://dev.twitter.com/docs/misc/cursoring"/>
        /// <param name='mode'>
        /// <para> Specify whether enumerating goes to the next page or the previous.</para>
        /// </param>
        /// <param name='parameters'>
        /// Parameters.
        /// </param>
        public IEnumerable<User> Enumerate(EnumerateMode mode, params Expression<Func<string,object>>[] parameters)
        {
            return Cursored<User>.Enumerate(this.Tokens, "lists/members", mode, parameters);
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
        public User Show(params Expression<Func<string,object>>[] parameters)
        {
            return this.Tokens.AccessApi<User>(MethodType.Get, "lists/members/show", parameters);
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
        public List Create(params Expression<Func<string,object>>[] parameters)
        {
            return this.Tokens.AccessApi<List>(MethodType.Post, "lists/members/create", parameters);
        }
        
        /// <summary>
        /// <para>Adds multiple members to a list, by specifying a comma-separated list of member ids or screen names. The authenticated user must own the list to be able to add members to it. Note that lists can't have more than 500 members, and you are limited to adding up to 100 members to a list at a time with this method.</para>
        /// <para>Please note that there can be issues with lists that rapidly remove and add memberships. Take care when using these methods such that you are not too rapidly switching between removals and adds on the same list.</para>
        /// <para>Note: Either a list_id or a slug is required. If providing a list_slug, an owner_screen_name or owner_id is also required.</para>
        /// <para>Avaliable parameters: </para><para> </para>
        /// <para><paramref name="long list_id (required)"/> : The numerical id of the list.</para>
        /// <para><paramref name="string slug (required)"/> : You can identify a list by its slug instead of its numerical id. If you decide to do so, note that you'll also have to specify the list owner using the owner_id or owner_screen_name parameters.</para>
        /// <para><paramref name="string user_id (optional)"/> : A comma separated list of user IDs, up to 100 are allowed in a single request.</para>
        /// <para><paramref name="string screen_name (optional)"/> : A comma separated list of screen names, up to 100 are allowed in a single request.</para>
        /// <para><paramref name="string owner_screen_name (optional)"/> : The screen name of the user who owns the list being requested by a slug.</para>
        /// <para><paramref name="long owner_id (optional)"/> : The user ID of the user who owns the list being requested by a slug.</para>
        /// </summary>
        /// <returns>The list.</returns>
        /// <param name='parameters'>
        /// Parameters.
        /// </param>
        public List CreateAll(params Expression<Func<string,object>>[] parameters)
        {
            return this.Tokens.AccessApi<List>(MethodType.Post, "list/members/create_all", parameters);
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
        public List Delete(params Expression<Func<string,object>>[] parameters)
        {
            return this.Tokens.AccessApi<List>(MethodType.Post, "lists/members/delete", parameters);
        }
        
        /// <summary>
        /// <para>Removes multiple members from a list, by specifying a comma-separated list of member ids or screen names. The authenticated user must own the list to be able to remove members from it. Note that lists can't have more than 500 members, and you are limited to removing up to 100 members to a list at a time with this method.</para>
        /// <para>Please note that there can be issues with lists that rapidly remove and add memberships. Take care when using these methods such that you are not too rapidly switching between removals and adds on the same list.</para>
        /// <para>Note: Either a list_id or a slug is required. If providing a list_slug, an owner_screen_name or owner_id is also required.</para>
        /// <para>Avaliable parameters: </para><para> </para>
        /// <para><paramref name="long list_id (required)"/> : The numerical id of the list.</para>
        /// <para><paramref name="string slug (required)"/> : You can identify a list by its slug instead of its numerical id. If you decide to do so, note that you'll also have to specify the list owner using the owner_id or owner_screen_name parameters.</para>
        /// <para><paramref name="string user_id (optional)"/> : A comma separated list of user IDs, up to 100 are allowed in a single request.</para>
        /// <para><paramref name="string screen_name (optional)"/> : A comma separated list of screen names, up to 100 are allowed in a single request.</para>
        /// <para><paramref name="string owner_screen_name (optional)"/> : The screen name of the user who owns the list being requested by a slug.</para>
        /// <para><paramref name="long owner_id (optional)"/> : The user ID of the user who owns the list being requested by a slug.</para>
        /// </summary>
        /// <returns>The list.</returns>
        /// <param name='parameters'>
        /// Parameters.
        /// </param>
        public List DeleteAll(params Expression<Func<string,object>>[] parameters)
        {
            return this.Tokens.AccessApi<List>(MethodType.Post, "list/members/delete_all", parameters);
        }
        
    }
        
    public class Subscribers : TokenIncluded
    {
        internal Subscribers(Tokens tokens): base(tokens) { }
        
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
        /// </pasram>
        public User Show(params Expression<Func<string,object>>[] parameters)
        {
            return this.Tokens.AccessApi<User>(MethodType.Get, "lists/subscribers/show", parameters);
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
        public List Create(params Expression<Func<string,object>>[] parameters)
        {
            return this.Tokens.AccessApi<List>(MethodType.Post, "lists/subscribers/create", parameters);
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
        public List Delete(params Expression<Func<string,object>>[] parameters)
        {
            return this.Tokens.AccessApi<List>(MethodType.Post, "lists/subscribers/delete", parameters);
        }
    }
}