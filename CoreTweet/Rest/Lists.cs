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
    /// Provides a set of methods for the wrapper of GET/POST lists.
    /// </summary>
    public partial class Lists : ApiProviderBase
    {
        internal Lists(TokensBase e) : base(e) { }

        /// <summary>
        /// Gets the wrapper of lists/members
        /// </summary>
        public Members Members { get { return new Members(this.Tokens); } }

        /// <summary>
        /// Gets the wrapper of lists/subscribers
        /// </summary>
        public Subscribers Subscribers { get { return new Subscribers(this.Tokens); } }

#if !(PCL || WIN_RT || WP)
        //GET Methods

        /// <summary>
        /// <para>Returns all lists the authenticating or specified user subscribes to, including their own.</para>
        /// <para>The user is specified using the user_id or screen_name parameters.</para>
        /// <para>If no user is given, the authenticating user is used.</para>
        /// <para>This method used to be GET lists in version 1.0 of the API and has been renamed for consistency with other call.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> user_id (optional)</para>
        /// <para>- <c>string</c> screen_name (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The lists.</returns>
        public ListedResponse<CoreTweet.List> List(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiArray<CoreTweet.List>(MethodType.Get, "lists/list", parameters);
        }

        /// <summary>
        /// <para>Returns all lists the authenticating or specified user subscribes to, including their own.</para>
        /// <para>The user is specified using the user_id or screen_name parameters.</para>
        /// <para>If no user is given, the authenticating user is used.</para>
        /// <para>This method used to be GET lists in version 1.0 of the API and has been renamed for consistency with other call.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> user_id (optional)</para>
        /// <para>- <c>string</c> screen_name (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The lists.</returns>
        public ListedResponse<CoreTweet.List> List(IDictionary<string, object> parameters)
        {
            return this.Tokens.AccessApiArray<CoreTweet.List>(MethodType.Get, "lists/list", parameters);
        }

        /// <summary>
        /// <para>Returns all lists the authenticating or specified user subscribes to, including their own.</para>
        /// <para>The user is specified using the user_id or screen_name parameters.</para>
        /// <para>If no user is given, the authenticating user is used.</para>
        /// <para>This method used to be GET lists in version 1.0 of the API and has been renamed for consistency with other call.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> user_id (optional)</para>
        /// <para>- <c>string</c> screen_name (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The lists.</returns>
        public ListedResponse<CoreTweet.List> List<T>(T parameters)
        {
            return this.Tokens.AccessApiArray<CoreTweet.List, T>(MethodType.Get, "lists/list", parameters);
        }

        /// <summary>
        /// <para>Returns the lists the specified user has been added to.</para>
        /// <para>If user_id or screen_name are not provided the memberships for the authenticating user are returned.</para>
        /// <para>Available parameters: </para>
        /// <para>- <c>string</c> sereen_name (optional)</para>
        /// <para>- <c>long</c> user_id (optional)</para>
        /// <para>- <c>long</c> cursor (semi-optional)</para>
        /// <para>- <c>bool</c> filter_to_owned_lists (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The lists.</returns>
        public Cursored<CoreTweet.List> Memberships(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApi<Cursored<CoreTweet.List>>(MethodType.Get, "lists/memberships", parameters);
        }

        /// <summary>
        /// <para>Returns the lists the specified user has been added to.</para>
        /// <para>If user_id or screen_name are not provided the memberships for the authenticating user are returned.</para>
        /// <para>Available parameters: </para>
        /// <para>- <c>string</c> sereen_name (optional)</para>
        /// <para>- <c>long</c> user_id (optional)</para>
        /// <para>- <c>long</c> cursor (semi-optional)</para>
        /// <para>- <c>bool</c> filter_to_owned_lists (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The lists.</returns>
        public Cursored<CoreTweet.List> Memberships(IDictionary<string, object> parameters)
        {
            return this.Tokens.AccessApi<Cursored<CoreTweet.List>>(MethodType.Get, "lists/memberships", parameters);
        }

        /// <summary>
        /// <para>Returns the lists the specified user has been added to.</para>
        /// <para>If user_id or screen_name are not provided the memberships for the authenticating user are returned.</para>
        /// <para>Available parameters: </para>
        /// <para>- <c>string</c> sereen_name (optional)</para>
        /// <para>- <c>long</c> user_id (optional)</para>
        /// <para>- <c>long</c> cursor (semi-optional)</para>
        /// <para>- <c>bool</c> filter_to_owned_lists (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The lists.</returns>
        public Cursored<CoreTweet.List> Memberships<T>(T parameters)
        {
            return this.Tokens.AccessApi<Cursored<CoreTweet.List>, T>(MethodType.Get, "lists/memberships", parameters);
        }

        /// <summary>
        /// <para>Enumerates the lists the specified user has been added to.</para>
        /// <para>If user_id or screen_name are not provided the memberships for the authenticating user are returned.</para>
        /// <para>Available parameters: </para>
        /// <para>- <c>string</c> sereen_name (optional)</para>
        /// <para>- <c>long</c> user_id (optional)</para>
        /// <para>- <c>long</c> cursor (semi-optional)</para>
        /// <para>- <c>bool</c> filter_to_owned_lists (optional)</para>
        /// </summary>
        /// <param name="mode">Specify whether enumerating goes to the next page or the previous.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The lists.</returns>
        public IEnumerable<CoreTweet.List> EnumerateMemberships(EnumerateMode mode, params Expression<Func<string, object>>[] parameters)
        {
            return Cursored<CoreTweet.List>.Enumerate(this.Tokens, "lists/memberships", mode, parameters);
        }

        /// <summary>
        /// <para>Enumerates the lists the specified user has been added to.</para>
        /// <para>If user_id or screen_name are not provided the memberships for the authenticating user are returned.</para>
        /// <para>Available parameters: </para>
        /// <para>- <c>string</c> sereen_name (optional)</para>
        /// <para>- <c>long</c> user_id (optional)</para>
        /// <para>- <c>long</c> cursor (semi-optional)</para>
        /// <para>- <c>bool</c> filter_to_owned_lists (optional)</para>
        /// </summary>
        /// <param name="mode">Specify whether enumerating goes to the next page or the previous.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The lists.</returns>
        public IEnumerable<CoreTweet.List> EnumerateMemberships(EnumerateMode mode, IDictionary<string, object> parameters)
        {
            return Cursored<CoreTweet.List>.Enumerate(this.Tokens, "lists/memberships", mode, parameters);
        }

        /// <summary>
        /// <para>Enumerates the lists the specified user has been added to.</para>
        /// <para>If user_id or screen_name are not provided the memberships for the authenticating user are returned.</para>
        /// <para>Available parameters: </para>
        /// <para>- <c>string</c> sereen_name (optional)</para>
        /// <para>- <c>long</c> user_id (optional)</para>
        /// <para>- <c>long</c> cursor (semi-optional)</para>
        /// <para>- <c>bool</c> filter_to_owned_lists (optional)</para>
        /// </summary>
        /// <param name="mode">Specify whether enumerating goes to the next page or the previous.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The lists.</returns>
        public IEnumerable<CoreTweet.List> EnumerateMemberships<T>(EnumerateMode mode, T parameters)
        {
            return Cursored<CoreTweet.List>.Enumerate<T>(this.Tokens, "lists/memberships", mode, parameters);
        }

        /// <summary>
        /// <para>Returns the lists owned by the specified Twitter user. Private lists will only be shown if the authenticated user is also the owner of the lists.</para>
        /// <para>A user_id or screen_name must be provided.</para>
        /// <para>Available parameters: </para>
        /// <para>- <c>long</c> user_id (optional)</para>
        /// <para>- <c>string</c> sereen_name (optional)</para>
        /// <para>- <c>int</c> count (optional)</para>
        /// <para>- <c>long</c> cursor (semi-optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The lists.</returns>
        public Cursored<CoreTweet.List> Ownerships(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApi<Cursored<CoreTweet.List>>(MethodType.Get, "lists/ownerships", parameters);
        }

        /// <summary>
        /// <para>Returns the lists owned by the specified Twitter user. Private lists will only be shown if the authenticated user is also the owner of the lists.</para>
        /// <para>A user_id or screen_name must be provided.</para>
        /// <para>Available parameters: </para>
        /// <para>- <c>long</c> user_id (optional)</para>
        /// <para>- <c>string</c> sereen_name (optional)</para>
        /// <para>- <c>int</c> count (optional)</para>
        /// <para>- <c>long</c> cursor (semi-optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The lists.</returns>
        public Cursored<CoreTweet.List> Ownerships(IDictionary<string, object> parameters)
        {
            return this.Tokens.AccessApi<Cursored<CoreTweet.List>>(MethodType.Get, "lists/ownerships", parameters);
        }

        /// <summary>
        /// <para>Returns the lists owned by the specified Twitter user. Private lists will only be shown if the authenticated user is also the owner of the lists.</para>
        /// <para>A user_id or screen_name must be provided.</para>
        /// <para>Available parameters: </para>
        /// <para>- <c>long</c> user_id (optional)</para>
        /// <para>- <c>string</c> sereen_name (optional)</para>
        /// <para>- <c>int</c> count (optional)</para>
        /// <para>- <c>long</c> cursor (semi-optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The lists.</returns>
        public Cursored<CoreTweet.List> Ownerships<T>(T parameters)
        {
            return this.Tokens.AccessApi<Cursored<CoreTweet.List>, T>(MethodType.Get, "lists/ownerships", parameters);
        }

        /// <summary>
        /// <para>Enumerates the lists owned by the specified Twitter user. Private lists will only be shown if the authenticated user is also the owner of the lists.</para>
        /// <para>A user_id or screen_name must be provided.</para>
        /// <para>Available parameters: </para>
        /// <para>- <c>long</c> user_id (optional)</para>
        /// <para>- <c>string</c> sereen_name (optional)</para>
        /// <para>- <c>int</c> count (optional)</para>
        /// <para>- <c>long</c> cursor (semi-optional)</para>
        /// </summary>
        /// <param name="mode">Specify whether enumerating goes to the next page or the previous.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The lists.</returns>
        public IEnumerable<CoreTweet.List> EnumerateOwnerships(EnumerateMode mode, params Expression<Func<string, object>>[] parameters)
        {
            return Cursored<CoreTweet.List>.Enumerate(this.Tokens, "lists/ownerships", mode, parameters);
        }

        /// <summary>
        /// <para>Enumerates the lists owned by the specified Twitter user. Private lists will only be shown if the authenticated user is also the owner of the lists.</para>
        /// <para>A user_id or screen_name must be provided.</para>
        /// <para>Available parameters: </para>
        /// <para>- <c>long</c> user_id (optional)</para>
        /// <para>- <c>string</c> sereen_name (optional)</para>
        /// <para>- <c>int</c> count (optional)</para>
        /// <para>- <c>long</c> cursor (semi-optional)</para>
        /// </summary>
        /// <param name="mode">Specify whether enumerating goes to the next page or the previous.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The lists.</returns>
        public IEnumerable<CoreTweet.List> EnumerateOwnerships(EnumerateMode mode, IDictionary<string, object> parameters)
        {
            return Cursored<CoreTweet.List>.Enumerate(this.Tokens, "lists/ownerships", mode, parameters);
        }

        /// <summary>
        /// <para>Enumerates the lists owned by the specified Twitter user. Private lists will only be shown if the authenticated user is also the owner of the lists.</para>
        /// <para>A user_id or screen_name must be provided.</para>
        /// <para>Available parameters: </para>
        /// <para>- <c>long</c> user_id (optional)</para>
        /// <para>- <c>string</c> sereen_name (optional)</para>
        /// <para>- <c>int</c> count (optional)</para>
        /// <para>- <c>long</c> cursor (semi-optional)</para>
        /// </summary>
        /// <param name="mode">Specify whether enumerating goes to the next page or the previous.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The lists.</returns>
        public IEnumerable<CoreTweet.List> EnumerateOwnerships<T>(EnumerateMode mode, T parameters)
        {
            return Cursored<CoreTweet.List>.Enumerate<T>(this.Tokens, "lists/ownerships", mode, parameters);
        }

        /// <summary>
        /// <para>Returns the specified list.</para>
        /// <para>Private lists will only be shown if the authenticated user owns the specified list.</para>
        /// <para>Note: Either a list_id or a slug is required.</para>
        /// <para>If providing a list_slug, an owner_screen_name or owner_id is also required.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> list_id (required)</para>
        /// <para>- <c>string</c> slug (required)</para>
        /// <para>- <c>string</c> owner_screen_name (optional)</para>
        /// <para>- <c>long</c> owner_id (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The list.</returns>
        public ListResponse Show(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApi<ListResponse>(MethodType.Get, "lists/show", parameters);
        }

        /// <summary>
        /// <para>Returns the specified list.</para>
        /// <para>Private lists will only be shown if the authenticated user owns the specified list.</para>
        /// <para>Note: Either a list_id or a slug is required.</para>
        /// <para>If providing a list_slug, an owner_screen_name or owner_id is also required.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> list_id (required)</para>
        /// <para>- <c>string</c> slug (required)</para>
        /// <para>- <c>string</c> owner_screen_name (optional)</para>
        /// <para>- <c>long</c> owner_id (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The list.</returns>
        public ListResponse Show(IDictionary<string, object> parameters)
        {
            return this.Tokens.AccessApi<ListResponse>(MethodType.Get, "lists/show", parameters);
        }

        /// <summary>
        /// <para>Returns the specified list.</para>
        /// <para>Private lists will only be shown if the authenticated user owns the specified list.</para>
        /// <para>Note: Either a list_id or a slug is required.</para>
        /// <para>If providing a list_slug, an owner_screen_name or owner_id is also required.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> list_id (required)</para>
        /// <para>- <c>string</c> slug (required)</para>
        /// <para>- <c>string</c> owner_screen_name (optional)</para>
        /// <para>- <c>long</c> owner_id (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The list.</returns>
        public ListResponse Show<T>(T parameters)
        {
            return this.Tokens.AccessApi<ListResponse, T>(MethodType.Get, "lists/show", parameters);
        }

        /// <summary>
        /// <para>Obtain a collection of the lists the specified user is subscribed to, 20 lists per page by default.</para>
        /// <para>Does not include the user's own lists.</para>
        /// <para>Note: A user_id or screen_name must be provided.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> user_id (optional)</para>
        /// <para>- <c>string</c> screen_name (optional)</para>
        /// <para>- <c>int</c> count (optional)</para>
        /// <para>- <c>long</c> cursor (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The lists.</returns>
        public Cursored<CoreTweet.List> Subscriptions(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApi<Cursored<CoreTweet.List>>(MethodType.Get, "lists/subscriptions", parameters);
        }

        /// <summary>
        /// <para>Obtain a collection of the lists the specified user is subscribed to, 20 lists per page by default.</para>
        /// <para>Does not include the user's own lists.</para>
        /// <para>Note: A user_id or screen_name must be provided.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> user_id (optional)</para>
        /// <para>- <c>string</c> screen_name (optional)</para>
        /// <para>- <c>int</c> count (optional)</para>
        /// <para>- <c>long</c> cursor (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The lists.</returns>
        public Cursored<CoreTweet.List> Subscriptions(IDictionary<string, object> parameters)
        {
            return this.Tokens.AccessApi<Cursored<CoreTweet.List>>(MethodType.Get, "lists/subscriptions", parameters);
        }

        /// <summary>
        /// <para>Obtain a collection of the lists the specified user is subscribed to, 20 lists per page by default.</para>
        /// <para>Does not include the user's own lists.</para>
        /// <para>Note: A user_id or screen_name must be provided.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> user_id (optional)</para>
        /// <para>- <c>string</c> screen_name (optional)</para>
        /// <para>- <c>int</c> count (optional)</para>
        /// <para>- <c>long</c> cursor (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The lists.</returns>
        public Cursored<CoreTweet.List> Subscriptions<T>(T parameters)
        {
            return this.Tokens.AccessApi<Cursored<CoreTweet.List>, T>(MethodType.Get, "lists/subscriptions", parameters);
        }

        /// <summary>
        /// <para>Enumerate lists the specified user is subscribed to, 20 lists per page by default.</para>
        /// <para>Does not include the user's own lists.</para>
        /// <para>Note: A user_id or screen_name must be provided.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> user_id (optional)</para>
        /// <para>- <c>string</c> screen_name (optional)</para>
        /// <para>- <c>int</c> count (optional)</para>
        /// <para>- <c>long</c> cursor (optional)</para>
        /// </summary>
        /// <param name="mode">Specify whether enumerating goes to the next page or the previous.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The lists.</returns>
        public IEnumerable<CoreTweet.List> EnumerateSubscriptions(EnumerateMode mode, params Expression<Func<string, object>>[] parameters)
        {
            return Cursored<CoreTweet.List>.Enumerate(this.Tokens, "lists/subscriptions", mode, parameters);
        }

        /// <summary>
        /// <para>Enumerate lists the specified user is subscribed to, 20 lists per page by default.</para>
        /// <para>Does not include the user's own lists.</para>
        /// <para>Note: A user_id or screen_name must be provided.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> user_id (optional)</para>
        /// <para>- <c>string</c> screen_name (optional)</para>
        /// <para>- <c>int</c> count (optional)</para>
        /// <para>- <c>long</c> cursor (optional)</para>
        /// </summary>
        /// <param name="mode">Specify whether enumerating goes to the next page or the previous.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The lists.</returns>
        public IEnumerable<CoreTweet.List> EnumerateSubscriptions(EnumerateMode mode, IDictionary<string, object> parameters)
        {
            return Cursored<CoreTweet.List>.Enumerate(this.Tokens, "lists/subscriptions", mode, parameters);
        }

        /// <summary>
        /// <para>Enumerate lists the specified user is subscribed to, 20 lists per page by default.</para>
        /// <para>Does not include the user's own lists.</para>
        /// <para>Note: A user_id or screen_name must be provided.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> user_id (optional)</para>
        /// <para>- <c>string</c> screen_name (optional)</para>
        /// <para>- <c>int</c> count (optional)</para>
        /// <para>- <c>long</c> cursor (optional)</para>
        /// </summary>
        /// <param name="mode">Specify whether enumerating goes to the next page or the previous.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The lists.</returns>
        public IEnumerable<CoreTweet.List> EnumerateSubscriptions<T>(EnumerateMode mode, T parameters)
        {
            return Cursored<CoreTweet.List>.Enumerate<T>(this.Tokens, "lists/subscriptions", mode, parameters);
        }

        /// <summary>
        /// <para>Returns tweet timeline for members of the specified list.</para>
        /// <para>Retweets are included by default.</para>
        /// <para>You can use the include_rts=false parameter to omit retweet objects.</para>
        /// <para>Note: Either a list_id or a slug is required.</para>
        /// <para>If providing a list_slug, an owner_screen_name or owner_id is also required.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> list_id (required)</para>
        /// <para>- <c>string</c> slug (required)</para>
        /// <para>- <c>string</c> owner_screen_name (optional)</para>
        /// <para>- <c>long</c> owner_id (optional)</para>
        /// <para>- <c>long</c> since_id (optional)</para>
        /// <para>- <c>long</c> max_id (optional)</para>
        /// <para>- <c>int</c> count (optional)</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// <para>- <c>bool</c> include_rts (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The statuses.</returns>
        public ListedResponse<Status> Statuses(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiArray<Status>(MethodType.Get, "lists/statuses", parameters);
        }

        /// <summary>
        /// <para>Returns tweet timeline for members of the specified list.</para>
        /// <para>Retweets are included by default.</para>
        /// <para>You can use the include_rts=false parameter to omit retweet objects.</para>
        /// <para>Note: Either a list_id or a slug is required.</para>
        /// <para>If providing a list_slug, an owner_screen_name or owner_id is also required.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> list_id (required)</para>
        /// <para>- <c>string</c> slug (required)</para>
        /// <para>- <c>string</c> owner_screen_name (optional)</para>
        /// <para>- <c>long</c> owner_id (optional)</para>
        /// <para>- <c>long</c> since_id (optional)</para>
        /// <para>- <c>long</c> max_id (optional)</para>
        /// <para>- <c>int</c> count (optional)</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// <para>- <c>bool</c> include_rts (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The statuses.</returns>
        public ListedResponse<Status> Statuses(IDictionary<string, object> parameters)
        {
            return this.Tokens.AccessApiArray<Status>(MethodType.Get, "lists/statuses", parameters);
        }

        /// <summary>
        /// <para>Returns tweet timeline for members of the specified list.</para>
        /// <para>Retweets are included by default.</para>
        /// <para>You can use the include_rts=false parameter to omit retweet objects.</para>
        /// <para>Note: Either a list_id or a slug is required.</para>
        /// <para>If providing a list_slug, an owner_screen_name or owner_id is also required.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> list_id (required)</para>
        /// <para>- <c>string</c> slug (required)</para>
        /// <para>- <c>string</c> owner_screen_name (optional)</para>
        /// <para>- <c>long</c> owner_id (optional)</para>
        /// <para>- <c>long</c> since_id (optional)</para>
        /// <para>- <c>long</c> max_id (optional)</para>
        /// <para>- <c>int</c> count (optional)</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// <para>- <c>bool</c> include_rts (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The statuses.</returns>
        public ListedResponse<Status> Statuses<T>(T parameters)
        {
            return this.Tokens.AccessApiArray<Status, T>(MethodType.Get, "lists/statuses", parameters);
        }

        // POST Methods

        /// <summary>
        /// <para>Creates a new list for the authenticated user.</para>
        /// <para>Note that you can't create more than 20 lists per account.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> name (required)</para>
        /// <para>- <c>string</c> mode (optional)</para>
        /// <para>- <c>string</c> description (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The list.</returns>
        public ListResponse Create(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApi<ListResponse>(MethodType.Post, "lists/create", parameters);
        }

        /// <summary>
        /// <para>Creates a new list for the authenticated user.</para>
        /// <para>Note that you can't create more than 20 lists per account.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> name (required)</para>
        /// <para>- <c>string</c> mode (optional)</para>
        /// <para>- <c>string</c> description (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The list.</returns>
        public ListResponse Create(IDictionary<string, object> parameters)
        {
            return this.Tokens.AccessApi<ListResponse>(MethodType.Post, "lists/create", parameters);
        }

        /// <summary>
        /// <para>Creates a new list for the authenticated user.</para>
        /// <para>Note that you can't create more than 20 lists per account.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> name (required)</para>
        /// <para>- <c>string</c> mode (optional)</para>
        /// <para>- <c>string</c> description (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The list.</returns>
        public ListResponse Create<T>(T parameters)
        {
            return this.Tokens.AccessApi<ListResponse, T>(MethodType.Post, "lists/create", parameters);
        }

        /// <summary>
        /// <para>Deletes the specified list. The authenticated user must own the list to be able to destroy it.</para>
        /// <para>Note: Either a list_id or a slug is required.</para>
        /// <para>If providing a list_slug, an owner_screen_name or owner_id is also required.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> owner_screen_name (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The destroied list.</returns>
        public ListResponse Destroy(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApi<ListResponse>(MethodType.Post, "lists/destroy", parameters);
        }

        /// <summary>
        /// <para>Deletes the specified list. The authenticated user must own the list to be able to destroy it.</para>
        /// <para>Note: Either a list_id or a slug is required.</para>
        /// <para>If providing a list_slug, an owner_screen_name or owner_id is also required.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> owner_screen_name (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The destroied list.</returns>
        public ListResponse Destroy(IDictionary<string, object> parameters)
        {
            return this.Tokens.AccessApi<ListResponse>(MethodType.Post, "lists/destroy", parameters);
        }

        /// <summary>
        /// <para>Deletes the specified list. The authenticated user must own the list to be able to destroy it.</para>
        /// <para>Note: Either a list_id or a slug is required.</para>
        /// <para>If providing a list_slug, an owner_screen_name or owner_id is also required.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> owner_screen_name (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The destroied list.</returns>
        public ListResponse Destroy<T>(T parameters)
        {
            return this.Tokens.AccessApi<ListResponse, T>(MethodType.Post, "lists/destroy", parameters);
        }

        /// <summary>
        /// <para>Updates the specified list.</para>
        /// <para>The authenticated user must own the list to be able to update it.</para>
        /// <para>Note: Either a list_id or a slug is required.</para>
        /// <para>If providing a list_slug, an owner_screen_name or owner_id is also required.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> list_id (required)</para>
        /// <para>- <c>string</c> slug (required)</para>
        /// <para>- <c>string</c> name (optional)</para>
        /// <para>- <c>string</c> mode (optional)</para>
        /// <para>- <c>string</c> description (optional)</para>
        /// <para>- <c>string</c> owner_screen_name (optional)</para>
        /// <para>- <c>long</c> owner_id (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The list.</returns>
        public ListResponse Update(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApi<ListResponse>(MethodType.Post, "lists/update", parameters);
        }

        /// <summary>
        /// <para>Updates the specified list.</para>
        /// <para>The authenticated user must own the list to be able to update it.</para>
        /// <para>Note: Either a list_id or a slug is required.</para>
        /// <para>If providing a list_slug, an owner_screen_name or owner_id is also required.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> list_id (required)</para>
        /// <para>- <c>string</c> slug (required)</para>
        /// <para>- <c>string</c> name (optional)</para>
        /// <para>- <c>string</c> mode (optional)</para>
        /// <para>- <c>string</c> description (optional)</para>
        /// <para>- <c>string</c> owner_screen_name (optional)</para>
        /// <para>- <c>long</c> owner_id (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The list.</returns>
        public ListResponse Update(IDictionary<string, object> parameters)
        {
            return this.Tokens.AccessApi<ListResponse>(MethodType.Post, "lists/update", parameters);
        }

        /// <summary>
        /// <para>Updates the specified list.</para>
        /// <para>The authenticated user must own the list to be able to update it.</para>
        /// <para>Note: Either a list_id or a slug is required.</para>
        /// <para>If providing a list_slug, an owner_screen_name or owner_id is also required.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> list_id (required)</para>
        /// <para>- <c>string</c> slug (required)</para>
        /// <para>- <c>string</c> name (optional)</para>
        /// <para>- <c>string</c> mode (optional)</para>
        /// <para>- <c>string</c> description (optional)</para>
        /// <para>- <c>string</c> owner_screen_name (optional)</para>
        /// <para>- <c>long</c> owner_id (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The list.</returns>
        public ListResponse Update<T>(T parameters)
        {
            return this.Tokens.AccessApi<ListResponse, T>(MethodType.Post, "lists/update", parameters);
        }
#endif
    }

    /// <summary>
    /// Provides a set of methods for the wrapper of GET/POST lists/members.
    /// </summary>
    public partial class Members : ApiProviderBase
    {
        internal Members(TokensBase tokens) : base(tokens) { }

#if !(PCL || WIN_RT || WP)
        //GET Methods

        /// <summary>
        /// <para>Returns the members of the specified list.</para>
        /// <para>Private list members will only be shown if the authenticated user owns the specified list.</para>
        /// <para>Note: Either a list_id or a slug is required.</para>
        /// <para>If providing a list_slug, an owner_screen_name or owner_id is also required.</para>
        /// <para>The response from the API will include a previous_cursor and next_cursor to allow paging back and forth.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> list_id (required)</para>
        /// <para>- <c>string</c> slug (required)</para>
        /// <para>- <c>string</c> owner_sereen_name (optional)</para>
        /// <para>- <c>long</c> owner_id (optional)</para>
        /// <para>- <c>long</c> cursor (semi-optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The users.</returns>
        public Cursored<User> List(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApi<Cursored<User>>(MethodType.Get, "lists/members", parameters);
        }

        /// <summary>
        /// <para>Returns the members of the specified list.</para>
        /// <para>Private list members will only be shown if the authenticated user owns the specified list.</para>
        /// <para>Note: Either a list_id or a slug is required.</para>
        /// <para>If providing a list_slug, an owner_screen_name or owner_id is also required.</para>
        /// <para>The response from the API will include a previous_cursor and next_cursor to allow paging back and forth.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> list_id (required)</para>
        /// <para>- <c>string</c> slug (required)</para>
        /// <para>- <c>string</c> owner_sereen_name (optional)</para>
        /// <para>- <c>long</c> owner_id (optional)</para>
        /// <para>- <c>long</c> cursor (semi-optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The users.</returns>
        public Cursored<User> List(IDictionary<string, object> parameters)
        {
            return this.Tokens.AccessApi<Cursored<User>>(MethodType.Get, "lists/members", parameters);
        }

        /// <summary>
        /// <para>Returns the members of the specified list.</para>
        /// <para>Private list members will only be shown if the authenticated user owns the specified list.</para>
        /// <para>Note: Either a list_id or a slug is required.</para>
        /// <para>If providing a list_slug, an owner_screen_name or owner_id is also required.</para>
        /// <para>The response from the API will include a previous_cursor and next_cursor to allow paging back and forth.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> list_id (required)</para>
        /// <para>- <c>string</c> slug (required)</para>
        /// <para>- <c>string</c> owner_sereen_name (optional)</para>
        /// <para>- <c>long</c> owner_id (optional)</para>
        /// <para>- <c>long</c> cursor (semi-optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The users.</returns>
        public Cursored<User> List<T>(T parameters)
        {
            return this.Tokens.AccessApi<Cursored<User>, T>(MethodType.Get, "lists/members", parameters);
        }

        /// <summary>
        /// <para>Enumeates the members of the specified list.</para>
        /// <para>Private list members will only be shown if the authenticated user owns the specified list.</para>
        /// <para>Note: Either a list_id or a slug is required.</para>
        /// <para>If providing a list_slug, an owner_screen_name or owner_id is also required.</para>
        /// <para>The response from the API will include a previous_cursor and next_cursor to allow paging back and forth.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> list_id (required)</para>
        /// <para>- <c>string</c> slug (required)</para>
        /// <para>- <c>string</c> owner_sereen_name (optional)</para>
        /// <para>- <c>long</c> owner_id (optional)</para>
        /// <para>- <c>long</c> cursor (optional)</para>
        /// </summary>
        /// <param name="mode">Specify whether enumerating goes to the next page or the previous.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The users.</returns>
        public IEnumerable<User> Enumerate(EnumerateMode mode, params Expression<Func<string, object>>[] parameters)
        {
            return Cursored<User>.Enumerate(this.Tokens, "lists/members", mode, parameters);
        }

        /// <summary>
        /// <para>Enumeates the members of the specified list.</para>
        /// <para>Private list members will only be shown if the authenticated user owns the specified list.</para>
        /// <para>Note: Either a list_id or a slug is required.</para>
        /// <para>If providing a list_slug, an owner_screen_name or owner_id is also required.</para>
        /// <para>The response from the API will include a previous_cursor and next_cursor to allow paging back and forth.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> list_id (required)</para>
        /// <para>- <c>string</c> slug (required)</para>
        /// <para>- <c>string</c> owner_sereen_name (optional)</para>
        /// <para>- <c>long</c> owner_id (optional)</para>
        /// <para>- <c>long</c> cursor (optional)</para>
        /// </summary>
        /// <param name="mode">Specify whether enumerating goes to the next page or the previous.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The users.</returns>
        public IEnumerable<User> Enumerate(EnumerateMode mode, IDictionary<string, object> parameters)
        {
            return Cursored<User>.Enumerate(this.Tokens, "lists/members", mode, parameters);
        }

        /// <summary>
        /// <para>Enumeates the members of the specified list.</para>
        /// <para>Private list members will only be shown if the authenticated user owns the specified list.</para>
        /// <para>Note: Either a list_id or a slug is required.</para>
        /// <para>If providing a list_slug, an owner_screen_name or owner_id is also required.</para>
        /// <para>The response from the API will include a previous_cursor and next_cursor to allow paging back and forth.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> list_id (required)</para>
        /// <para>- <c>string</c> slug (required)</para>
        /// <para>- <c>string</c> owner_sereen_name (optional)</para>
        /// <para>- <c>long</c> owner_id (optional)</para>
        /// <para>- <c>long</c> cursor (optional)</para>
        /// </summary>
        /// <param name="mode">Specify whether enumerating goes to the next page or the previous.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The users.</returns>
        public IEnumerable<User> Enumerate<T>(EnumerateMode mode, T parameters)
        {
            return Cursored<User>.Enumerate<T>(this.Tokens, "lists/members", mode, parameters);
        }

        /// <summary>
        /// <para>Check if the specified user is a member of the specified list.</para>
        /// <para>Note: Either a list_id or a slug is required.</para>
        /// <para>If providing a list_slug, an owner_screen_name or owner_id is also required.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> list_id (required)</para>
        /// <para>- <c>string</c> slug (required)</para>
        /// <para>- <c>string</c> sereen_name (required)</para>
        /// <para>- <c>long</c> user_id (required)</para>
        /// <para>- <c>string</c> owner_screen_name (optional)</para>
        /// <para>- <c>long</c> owner_id (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The user.</returns>
        public UserResponse Show(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApi<UserResponse>(MethodType.Get, "lists/members/show", parameters);
        }

        /// <summary>
        /// <para>Check if the specified user is a member of the specified list.</para>
        /// <para>Note: Either a list_id or a slug is required.</para>
        /// <para>If providing a list_slug, an owner_screen_name or owner_id is also required.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> list_id (required)</para>
        /// <para>- <c>string</c> slug (required)</para>
        /// <para>- <c>string</c> sereen_name (required)</para>
        /// <para>- <c>long</c> user_id (required)</para>
        /// <para>- <c>string</c> owner_screen_name (optional)</para>
        /// <para>- <c>long</c> owner_id (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The user.</returns>
        public UserResponse Show(IDictionary<string, object> parameters)
        {
            return this.Tokens.AccessApi<UserResponse>(MethodType.Get, "lists/members/show", parameters);
        }

        /// <summary>
        /// <para>Check if the specified user is a member of the specified list.</para>
        /// <para>Note: Either a list_id or a slug is required.</para>
        /// <para>If providing a list_slug, an owner_screen_name or owner_id is also required.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> list_id (required)</para>
        /// <para>- <c>string</c> slug (required)</para>
        /// <para>- <c>string</c> sereen_name (required)</para>
        /// <para>- <c>long</c> user_id (required)</para>
        /// <para>- <c>string</c> owner_screen_name (optional)</para>
        /// <para>- <c>long</c> owner_id (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The user.</returns>
        public UserResponse Show<T>(T parameters)
        {
            return this.Tokens.AccessApi<UserResponse, T>(MethodType.Get, "lists/members/show", parameters);
        }

        //POST Methods

        /// <summary>
        /// <para>Add a member to a list.</para>
        /// <para>The authenticated user must own the list to be able to add members to it.</para>
        /// <para>Note that lists can't have more than 500 members.</para>
        /// <para>Note: Either a list_id or a slug is required.</para>
        /// <para>If providing a list_slug, an owner_screen_name or owner_id is also required.</para>
        /// <para>Available parameters:</para><para> </para>
        /// <para>- <c>long</c> list_id (required)</para>
        /// <para>- <c>string</c> slug (required)</para>
        /// <para>- <c>long</c> user_id (required)</para>
        /// <para>- <c>string</c> screen_name (required)</para>
        /// <para>- <c>string</c> owner_screen_name (optional)</para>
        /// <para>- <c>long</c> owner_id (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The list.</returns>
        public ListResponse Create(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApi<ListResponse>(MethodType.Post, "lists/members/create", parameters);
        }

        /// <summary>
        /// <para>Add a member to a list.</para>
        /// <para>The authenticated user must own the list to be able to add members to it.</para>
        /// <para>Note that lists can't have more than 500 members.</para>
        /// <para>Note: Either a list_id or a slug is required.</para>
        /// <para>If providing a list_slug, an owner_screen_name or owner_id is also required.</para>
        /// <para>Available parameters:</para><para> </para>
        /// <para>- <c>long</c> list_id (required)</para>
        /// <para>- <c>string</c> slug (required)</para>
        /// <para>- <c>long</c> user_id (required)</para>
        /// <para>- <c>string</c> screen_name (required)</para>
        /// <para>- <c>string</c> owner_screen_name (optional)</para>
        /// <para>- <c>long</c> owner_id (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The list.</returns>
        public ListResponse Create(IDictionary<string, object> parameters)
        {
            return this.Tokens.AccessApi<ListResponse>(MethodType.Post, "lists/members/create", parameters);
        }

        /// <summary>
        /// <para>Add a member to a list.</para>
        /// <para>The authenticated user must own the list to be able to add members to it.</para>
        /// <para>Note that lists can't have more than 500 members.</para>
        /// <para>Note: Either a list_id or a slug is required.</para>
        /// <para>If providing a list_slug, an owner_screen_name or owner_id is also required.</para>
        /// <para>Available parameters:</para><para> </para>
        /// <para>- <c>long</c> list_id (required)</para>
        /// <para>- <c>string</c> slug (required)</para>
        /// <para>- <c>long</c> user_id (required)</para>
        /// <para>- <c>string</c> screen_name (required)</para>
        /// <para>- <c>string</c> owner_screen_name (optional)</para>
        /// <para>- <c>long</c> owner_id (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The list.</returns>
        public ListResponse Create<T>(T parameters)
        {
            return this.Tokens.AccessApi<ListResponse, T>(MethodType.Post, "lists/members/create", parameters);
        }

        /// <summary>
        /// <para>Adds multiple members to a list, by specifying a comma-separated list of member ids or screen names.</para>
        /// <para>The authenticated user must own the list to be able to add members to it.</para>
        /// <para>Note that lists can't have more than 500 members, and you are limited to adding up to 100 members to a list at a time with this method.</para>
        /// <para>Please note that there can be issues with lists that rapidly remove and add memberships.</para>
        /// <para>Take care when using these methods such that you are not too rapidly switching between removals and adds on the same list.</para>
        /// <para>Note: Either a list_id or a slug is required.</para>
        /// <para>If providing a list_slug, an owner_screen_name or owner_id is also required.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> list_id (required)</para>
        /// <para>- <c>string</c> slug (required)</para>
        /// <para>- <c>string</c> / <c>IEnumerable&lt;long&gt;</c> user_id (optional)</para>
        /// <para>- <c>string</c> / <c>IEnumerable&lt;string&gt;</c> screen_name (optional)</para>
        /// <para>- <c>string</c> owner_screen_name (optional)</para>
        /// <para>- <c>long</c> owner_id (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The list.</returns>
        public ListResponse CreateAll(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApi<ListResponse>(MethodType.Post, "lists/members/create_all", parameters);
        }

        /// <summary>
        /// <para>Adds multiple members to a list, by specifying a comma-separated list of member ids or screen names.</para>
        /// <para>The authenticated user must own the list to be able to add members to it.</para>
        /// <para>Note that lists can't have more than 500 members, and you are limited to adding up to 100 members to a list at a time with this method.</para>
        /// <para>Please note that there can be issues with lists that rapidly remove and add memberships.</para>
        /// <para>Take care when using these methods such that you are not too rapidly switching between removals and adds on the same list.</para>
        /// <para>Note: Either a list_id or a slug is required.</para>
        /// <para>If providing a list_slug, an owner_screen_name or owner_id is also required.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> list_id (required)</para>
        /// <para>- <c>string</c> slug (required)</para>
        /// <para>- <c>string</c> / <c>IEnumerable&lt;long&gt;</c> user_id (optional)</para>
        /// <para>- <c>string</c> / <c>IEnumerable&lt;string&gt;</c> screen_name (optional)</para>
        /// <para>- <c>string</c> owner_screen_name (optional)</para>
        /// <para>- <c>long</c> owner_id (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The list.</returns>
        public ListResponse CreateAll(IDictionary<string, object> parameters)
        {
            return this.Tokens.AccessApi<ListResponse>(MethodType.Post, "lists/members/create_all", parameters);
        }

        /// <summary>
        /// <para>Adds multiple members to a list, by specifying a comma-separated list of member ids or screen names.</para>
        /// <para>The authenticated user must own the list to be able to add members to it.</para>
        /// <para>Note that lists can't have more than 500 members, and you are limited to adding up to 100 members to a list at a time with this method.</para>
        /// <para>Please note that there can be issues with lists that rapidly remove and add memberships.</para>
        /// <para>Take care when using these methods such that you are not too rapidly switching between removals and adds on the same list.</para>
        /// <para>Note: Either a list_id or a slug is required.</para>
        /// <para>If providing a list_slug, an owner_screen_name or owner_id is also required.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> list_id (required)</para>
        /// <para>- <c>string</c> slug (required)</para>
        /// <para>- <c>string</c> / <c>IEnumerable&lt;long&gt;</c> user_id (optional)</para>
        /// <para>- <c>string</c> / <c>IEnumerable&lt;string&gt;</c> screen_name (optional)</para>
        /// <para>- <c>string</c> owner_screen_name (optional)</para>
        /// <para>- <c>long</c> owner_id (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The list.</returns>
        public ListResponse CreateAll<T>(T parameters)
        {
            return this.Tokens.AccessApi<ListResponse, T>(MethodType.Post, "lists/members/create_all", parameters);
        }

        /// <summary>
        /// <para>Removes the specified member from the list.</para>
        /// <para>The authenticated user must be the list's owner to remove members from the list.</para>
        /// <para>Note: Either a list_id or a slug is required.</para>
        /// <para>If providing a list_slug, an owner_screen_name or owner_id is also required.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> list_id (required)</para>
        /// <para>- <c>string</c> slug (required)</para>
        /// <para>- <c>long</c> user_id (required)</para>
        /// <para>- <c>string</c> screen_name (required)</para>
        /// <para>- <c>string</c> owner_screen_name (optional)</para>
        /// <para>- <c>long</c> owner_id (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The list.</returns>
        public ListResponse Delete(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApi<ListResponse>(MethodType.Post, "lists/members/delete", parameters);
        }

        /// <summary>
        /// <para>Removes the specified member from the list.</para>
        /// <para>The authenticated user must be the list's owner to remove members from the list.</para>
        /// <para>Note: Either a list_id or a slug is required.</para>
        /// <para>If providing a list_slug, an owner_screen_name or owner_id is also required.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> list_id (required)</para>
        /// <para>- <c>string</c> slug (required)</para>
        /// <para>- <c>long</c> user_id (required)</para>
        /// <para>- <c>string</c> screen_name (required)</para>
        /// <para>- <c>string</c> owner_screen_name (optional)</para>
        /// <para>- <c>long</c> owner_id (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The list.</returns>
        public ListResponse Delete(IDictionary<string, object> parameters)
        {
            return this.Tokens.AccessApi<ListResponse>(MethodType.Post, "lists/members/delete", parameters);
        }

        /// <summary>
        /// <para>Removes the specified member from the list.</para>
        /// <para>The authenticated user must be the list's owner to remove members from the list.</para>
        /// <para>Note: Either a list_id or a slug is required.</para>
        /// <para>If providing a list_slug, an owner_screen_name or owner_id is also required.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> list_id (required)</para>
        /// <para>- <c>string</c> slug (required)</para>
        /// <para>- <c>long</c> user_id (required)</para>
        /// <para>- <c>string</c> screen_name (required)</para>
        /// <para>- <c>string</c> owner_screen_name (optional)</para>
        /// <para>- <c>long</c> owner_id (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The list.</returns>
        public ListResponse Delete<T>(T parameters)
        {
            return this.Tokens.AccessApi<ListResponse, T>(MethodType.Post, "lists/members/delete", parameters);
        }

        /// <summary>
        /// <para>Removes multiple members from a list, by specifying a comma-separated list of member ids or screen names.</para>
        /// <para>The authenticated user must own the list to be able to remove members from it.</para>
        /// <para>Note that lists can't have more than 500 members, and you are limited to removing up to 100 members to a list at a time with this method.</para>
        /// <para>Please note that there can be issues with lists that rapidly remove and add memberships.</para>
        /// <para>Take care when using these methods such that you are not too rapidly switching between removals and adds on the same list.</para>
        /// <para>Note: Either a list_id or a slug is required.</para>
        /// <para>If providing a list_slug, an owner_screen_name or owner_id is also required.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> list_id (required)</para>
        /// <para>- <c>string</c> slug (required)</para>
        /// <para>- <c>string</c> / IEnumerable&lt;long&gt; user_id (optional)</para>
        /// <para>- <c>string</c> / IEnumerable&lt;string&gt; screen_name (optional)</para>
        /// <para>- <c>string</c> owner_screen_name (optional)</para>
        /// <para>- <c>long</c> owner_id (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The list.</returns>
        public ListResponse DeleteAll(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApi<ListResponse>(MethodType.Post, "lists/members/delete_all", parameters);
        }

        /// <summary>
        /// <para>Removes multiple members from a list, by specifying a comma-separated list of member ids or screen names.</para>
        /// <para>The authenticated user must own the list to be able to remove members from it.</para>
        /// <para>Note that lists can't have more than 500 members, and you are limited to removing up to 100 members to a list at a time with this method.</para>
        /// <para>Please note that there can be issues with lists that rapidly remove and add memberships.</para>
        /// <para>Take care when using these methods such that you are not too rapidly switching between removals and adds on the same list.</para>
        /// <para>Note: Either a list_id or a slug is required.</para>
        /// <para>If providing a list_slug, an owner_screen_name or owner_id is also required.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> list_id (required)</para>
        /// <para>- <c>string</c> slug (required)</para>
        /// <para>- <c>string</c> / IEnumerable&lt;long&gt; user_id (optional)</para>
        /// <para>- <c>string</c> / IEnumerable&lt;string&gt; screen_name (optional)</para>
        /// <para>- <c>string</c> owner_screen_name (optional)</para>
        /// <para>- <c>long</c> owner_id (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The list.</returns>
        public ListResponse DeleteAll(IDictionary<string, object> parameters)
        {
            return this.Tokens.AccessApi<ListResponse>(MethodType.Post, "lists/members/delete_all", parameters);
        }

        /// <summary>
        /// <para>Removes multiple members from a list, by specifying a comma-separated list of member ids or screen names.</para>
        /// <para>The authenticated user must own the list to be able to remove members from it.</para>
        /// <para>Note that lists can't have more than 500 members, and you are limited to removing up to 100 members to a list at a time with this method.</para>
        /// <para>Please note that there can be issues with lists that rapidly remove and add memberships.</para>
        /// <para>Take care when using these methods such that you are not too rapidly switching between removals and adds on the same list.</para>
        /// <para>Note: Either a list_id or a slug is required.</para>
        /// <para>If providing a list_slug, an owner_screen_name or owner_id is also required.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> list_id (required)</para>
        /// <para>- <c>string</c> slug (required)</para>
        /// <para>- <c>string</c> / IEnumerable&lt;long&gt; user_id (optional)</para>
        /// <para>- <c>string</c> / IEnumerable&lt;string&gt; screen_name (optional)</para>
        /// <para>- <c>string</c> owner_screen_name (optional)</para>
        /// <para>- <c>long</c> owner_id (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The list.</returns>
        public ListResponse DeleteAll<T>(T parameters)
        {
            return this.Tokens.AccessApi<ListResponse, T>(MethodType.Post, "lists/members/delete_all", parameters);
        }
#endif
    }

    /// <summary>
    /// Provides a set of methods for the wrapper of GET/POST lists/subscribers.
    /// </summary>
    public partial class Subscribers : ApiProviderBase
    {
        internal Subscribers(TokensBase tokens) : base(tokens) { }

#if !(PCL || WIN_RT || WP)
        //GET Method

        /// <summary>
        /// <para>Check if the specified user is a subscriber of the specified list.</para>
        /// <para>Returns the user if they are subscriber.</para>
        /// <para>Note: Either a list_id or a slug is required.</para>
        /// <para>If providing a list_slug, an owner_screen_name or owner_id is also required.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> owner_screen_name (optional)</para>
        /// <para>- <c>long</c> owner_id (optional)</para>
        /// <para>- <c>long</c> list_id (required)</para>
        /// <para>- <c>string</c> slug (required)</para>
        /// <para>- <c>long</c> user_id (required)</para>
        /// <para>- <c>string</c> screen_name (required)</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// <para>- <c>bool</c> skip_status (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The user.</returns>
        public UserResponse Show(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApi<UserResponse>(MethodType.Get, "lists/subscribers/show", parameters);
        }

        /// <summary>
        /// <para>Check if the specified user is a subscriber of the specified list.</para>
        /// <para>Returns the user if they are subscriber.</para>
        /// <para>Note: Either a list_id or a slug is required.</para>
        /// <para>If providing a list_slug, an owner_screen_name or owner_id is also required.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> owner_screen_name (optional)</para>
        /// <para>- <c>long</c> owner_id (optional)</para>
        /// <para>- <c>long</c> list_id (required)</para>
        /// <para>- <c>string</c> slug (required)</para>
        /// <para>- <c>long</c> user_id (required)</para>
        /// <para>- <c>string</c> screen_name (required)</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// <para>- <c>bool</c> skip_status (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The user.</returns>
        public UserResponse Show(IDictionary<string, object> parameters)
        {
            return this.Tokens.AccessApi<UserResponse>(MethodType.Get, "lists/subscribers/show", parameters);
        }

        /// <summary>
        /// <para>Check if the specified user is a subscriber of the specified list.</para>
        /// <para>Returns the user if they are subscriber.</para>
        /// <para>Note: Either a list_id or a slug is required.</para>
        /// <para>If providing a list_slug, an owner_screen_name or owner_id is also required.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> owner_screen_name (optional)</para>
        /// <para>- <c>long</c> owner_id (optional)</para>
        /// <para>- <c>long</c> list_id (required)</para>
        /// <para>- <c>string</c> slug (required)</para>
        /// <para>- <c>long</c> user_id (required)</para>
        /// <para>- <c>string</c> screen_name (required)</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// <para>- <c>bool</c> skip_status (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The user.</returns>
        public UserResponse Show<T>(T parameters)
        {
            return this.Tokens.AccessApi<UserResponse, T>(MethodType.Get, "lists/subscribers/show", parameters);
        }

        //POST Methods

        /// <summary>
        /// <para>Subscribes the authenticated user to the specified list.</para>
        /// <para>Note: Either a list_id or a slug is required.</para>
        /// <para>If providing a list_slug, an owner_screen_name or owner_id is also required.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> list_id (required)</para>
        /// <para>- <c>string</c> slug (required)</para>
        /// <para>- <c>string</c> owner_screen_name (optional)</para>
        /// <para>- <c>long</c> owner_id (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The list.</returns>
        public ListResponse Create(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApi<ListResponse>(MethodType.Post, "lists/subscribers/create", parameters);
        }

        /// <summary>
        /// <para>Subscribes the authenticated user to the specified list.</para>
        /// <para>Note: Either a list_id or a slug is required.</para>
        /// <para>If providing a list_slug, an owner_screen_name or owner_id is also required.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> list_id (required)</para>
        /// <para>- <c>string</c> slug (required)</para>
        /// <para>- <c>string</c> owner_screen_name (optional)</para>
        /// <para>- <c>long</c> owner_id (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The list.</returns>
        public ListResponse Create(IDictionary<string, object> parameters)
        {
            return this.Tokens.AccessApi<ListResponse>(MethodType.Post, "lists/subscribers/create", parameters);
        }

        /// <summary>
        /// <para>Subscribes the authenticated user to the specified list.</para>
        /// <para>Note: Either a list_id or a slug is required.</para>
        /// <para>If providing a list_slug, an owner_screen_name or owner_id is also required.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> list_id (required)</para>
        /// <para>- <c>string</c> slug (required)</para>
        /// <para>- <c>string</c> owner_screen_name (optional)</para>
        /// <para>- <c>long</c> owner_id (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The list.</returns>
        public ListResponse Create<T>(T parameters)
        {
            return this.Tokens.AccessApi<ListResponse, T>(MethodType.Post, "lists/subscribers/create", parameters);
        }

        /// <summary>
        /// <para>Unsubscribes the authenticated user from the specified list.</para>
        /// <para>Note: Either a list_id or a slug is required.</para>
        /// <para>If providing a list_slug, an owner_screen_name or owner_id is also required.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> list_id (required)</para>
        /// <para>- <c>string</c> slug (required)</para>
        /// <para>- <c>string</c> owner_screen_name (optional)</para>
        /// <para>- <c>long</c> owner_id (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The list.</returns>
        public ListResponse Delete(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApi<ListResponse>(MethodType.Post, "lists/subscribers/delete", parameters);
        }

        /// <summary>
        /// <para>Unsubscribes the authenticated user from the specified list.</para>
        /// <para>Note: Either a list_id or a slug is required.</para>
        /// <para>If providing a list_slug, an owner_screen_name or owner_id is also required.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> list_id (required)</para>
        /// <para>- <c>string</c> slug (required)</para>
        /// <para>- <c>string</c> owner_screen_name (optional)</para>
        /// <para>- <c>long</c> owner_id (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The list.</returns>
        public ListResponse Delete(IDictionary<string, object> parameters)
        {
            return this.Tokens.AccessApi<ListResponse>(MethodType.Post, "lists/subscribers/delete", parameters);
        }

        /// <summary>
        /// <para>Unsubscribes the authenticated user from the specified list.</para>
        /// <para>Note: Either a list_id or a slug is required.</para>
        /// <para>If providing a list_slug, an owner_screen_name or owner_id is also required.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> list_id (required)</para>
        /// <para>- <c>string</c> slug (required)</para>
        /// <para>- <c>string</c> owner_screen_name (optional)</para>
        /// <para>- <c>long</c> owner_id (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The list.</returns>
        public ListResponse Delete<T>(T parameters)
        {
            return this.Tokens.AccessApi<ListResponse, T>(MethodType.Post, "lists/subscribers/delete", parameters);
        }
#endif
    }
}
