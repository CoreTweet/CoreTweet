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
        /// <para>Returns all lists the authenticating or specified user subscribes to, including their own as an asynchronous operation.</para>
        /// <para>The user is specified using the user_id or screen_name parameters.</para>
        /// <para>If no user is given, the authenticating user is used.</para>
        /// <para>This method used to be GET lists in version 1.0 of the API and has been renamed for consistency with other call.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> user_id (optional)</para>
        /// <para>- <c>string</c> screen_name (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the lists.</para>
        /// </returns>
        public Task<ListedResponse<CoreTweet.List>> ListAsync(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiArrayAsync<CoreTweet.List>(MethodType.Get, "lists/list", parameters);
        }

        /// <summary>
        /// <para>Returns all lists the authenticating or specified user subscribes to, including their own as an asynchronous operation.</para>
        /// <para>The user is specified using the user_id or screen_name parameters.</para>
        /// <para>If no user is given, the authenticating user is used.</para>
        /// <para>This method used to be GET lists in version 1.0 of the API and has been renamed for consistency with other call.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> user_id (optional)</para>
        /// <para>- <c>string</c> screen_name (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the lists.</para>
        /// </returns>
        public Task<ListedResponse<CoreTweet.List>> ListAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiArrayAsync<CoreTweet.List>(MethodType.Get, "lists/list", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Returns all lists the authenticating or specified user subscribes to, including their own as an asynchronous operation.</para>
        /// <para>The user is specified using the user_id or screen_name parameters.</para>
        /// <para>If no user is given, the authenticating user is used.</para>
        /// <para>This method used to be GET lists in version 1.0 of the API and has been renamed for consistency with other call.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> user_id (optional)</para>
        /// <para>- <c>string</c> screen_name (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the lists.</para>
        /// </returns>
        public Task<ListedResponse<CoreTweet.List>> ListAsync<T>(T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiArrayAsync<CoreTweet.List, T>(MethodType.Get, "lists/list", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Returns the lists the specified user has been added to as an asynchronous operation.</para>
        /// <para>If user_id or screen_name are not provided the memberships for the authenticating user are returned.</para>
        /// <para>Available parameters: </para>
        /// <para>- <c>string</c> sereen_name (optional)</para>
        /// <para>- <c>long</c> user_id (optional)</para>
        /// <para>- <c>long</c> cursor (semi-optional)</para>
        /// <para>- <c>bool</c> filter_to_owned_lists (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the lists.</para>
        /// </returns>
        public Task<Cursored<CoreTweet.List>> MembershipsAsync(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiAsync<Cursored<CoreTweet.List>>(MethodType.Get, "lists/memberships", parameters);
        }

        /// <summary>
        /// <para>Returns the lists the specified user has been added to as an asynchronous operation.</para>
        /// <para>If user_id or screen_name are not provided the memberships for the authenticating user are returned.</para>
        /// <para>Available parameters: </para>
        /// <para>- <c>string</c> sereen_name (optional)</para>
        /// <para>- <c>long</c> user_id (optional)</para>
        /// <para>- <c>long</c> cursor (semi-optional)</para>
        /// <para>- <c>bool</c> filter_to_owned_lists (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the lists.</para>
        /// </returns>
        public Task<Cursored<CoreTweet.List>> MembershipsAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<Cursored<CoreTweet.List>>(MethodType.Get, "lists/memberships", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Returns the lists the specified user has been added to as an asynchronous operation.</para>
        /// <para>If user_id or screen_name are not provided the memberships for the authenticating user are returned.</para>
        /// <para>Available parameters: </para>
        /// <para>- <c>string</c> sereen_name (optional)</para>
        /// <para>- <c>long</c> user_id (optional)</para>
        /// <para>- <c>long</c> cursor (semi-optional)</para>
        /// <para>- <c>bool</c> filter_to_owned_lists (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the lists.</para>
        /// </returns>
        public Task<Cursored<CoreTweet.List>> MembershipsAsync<T>(T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<Cursored<CoreTweet.List>, T>(MethodType.Get, "lists/memberships", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Returns the lists owned by the specified Twitter user as an asynchronous operation. Private lists will only be shown if the authenticated user is also the owner of the lists.</para>
        /// <para>A user_id or screen_name must be provided.</para>
        /// <para>Available parameters: </para>
        /// <para>- <c>long</c> user_id (optional)</para>
        /// <para>- <c>string</c> sereen_name (optional)</para>
        /// <para>- <c>int</c> count (optional)</para>
        /// <para>- <c>long</c> cursor (semi-optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the lists.</para>
        /// </returns>
        public Task<Cursored<CoreTweet.List>> OwnershipsAsync(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiAsync<Cursored<CoreTweet.List>>(MethodType.Get, "lists/ownerships", parameters);
        }

        /// <summary>
        /// <para>Returns the lists owned by the specified Twitter user as an asynchronous operation. Private lists will only be shown if the authenticated user is also the owner of the lists.</para>
        /// <para>A user_id or screen_name must be provided.</para>
        /// <para>Available parameters: </para>
        /// <para>- <c>long</c> user_id (optional)</para>
        /// <para>- <c>string</c> sereen_name (optional)</para>
        /// <para>- <c>int</c> count (optional)</para>
        /// <para>- <c>long</c> cursor (semi-optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the lists.</para>
        /// </returns>
        public Task<Cursored<CoreTweet.List>> OwnershipsAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<Cursored<CoreTweet.List>>(MethodType.Get, "lists/ownerships", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Returns the lists owned by the specified Twitter user as an asynchronous operation. Private lists will only be shown if the authenticated user is also the owner of the lists.</para>
        /// <para>A user_id or screen_name must be provided.</para>
        /// <para>Available parameters: </para>
        /// <para>- <c>long</c> user_id (optional)</para>
        /// <para>- <c>string</c> sereen_name (optional)</para>
        /// <para>- <c>int</c> count (optional)</para>
        /// <para>- <c>long</c> cursor (semi-optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the lists.</para>
        /// </returns>
        public Task<Cursored<CoreTweet.List>> OwnershipsAsync<T>(T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<Cursored<CoreTweet.List>, T>(MethodType.Get, "lists/ownerships", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Returns the specified list as an asynchronous operation.</para>
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
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the lists.</para>
        /// </returns>
        public Task<ListResponse> ShowAsync(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiAsync<ListResponse>(MethodType.Get, "lists/show", parameters);
        }

        /// <summary>
        /// <para>Returns the specified list as an asynchronous operation.</para>
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
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the lists.</para>
        /// </returns>
        public Task<ListResponse> ShowAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<ListResponse>(MethodType.Get, "lists/show", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Returns the specified list as an asynchronous operation.</para>
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
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the lists.</para>
        /// </returns>
        public Task<ListResponse> ShowAsync<T>(T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<ListResponse, T>(MethodType.Get, "lists/show", parameters, cancellationToken);
        }


        /// <summary>
        /// <para>Obtain a collection of the lists the specified user is subscribed to, 20 lists per page by default as an asynchronous operation.</para>
        /// <para>Does not include the user's own lists.</para>
        /// <para>Note: A user_id or screen_name must be provided.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> user_id (optional)</para>
        /// <para>- <c>string</c> screen_name (optional)</para>
        /// <para>- <c>int</c> count (optional)</para>
        /// <para>- <c>long</c> cursor (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the lists.</para>
        /// </returns>
        public Task<Cursored<CoreTweet.List>> SubscriptionsAsync(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiAsync<Cursored<CoreTweet.List>>(MethodType.Get, "lists/subscriptions", parameters);
        }

        /// <summary>
        /// <para>Obtain a collection of the lists the specified user is subscribed to, 20 lists per page by default as an asynchronous operation.</para>
        /// <para>Does not include the user's own lists.</para>
        /// <para>Note: A user_id or screen_name must be provided.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> user_id (optional)</para>
        /// <para>- <c>string</c> screen_name (optional)</para>
        /// <para>- <c>int</c> count (optional)</para>
        /// <para>- <c>long</c> cursor (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the lists.</para>
        /// </returns>
        public Task<Cursored<CoreTweet.List>> SubscriptionsAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<Cursored<CoreTweet.List>>(MethodType.Get, "lists/subscriptions", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Obtain a collection of the lists the specified user is subscribed to, 20 lists per page by default as an asynchronous operation.</para>
        /// <para>Does not include the user's own lists.</para>
        /// <para>Note: A user_id or screen_name must be provided.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> user_id (optional)</para>
        /// <para>- <c>string</c> screen_name (optional)</para>
        /// <para>- <c>int</c> count (optional)</para>
        /// <para>- <c>long</c> cursor (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the lists.</para>
        /// </returns>
        public Task<Cursored<CoreTweet.List>> SubscriptionsAsync<T>(T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<Cursored<CoreTweet.List>, T>(MethodType.Get, "lists/subscriptions", parameters, cancellationToken);
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
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the statuses.</para>
        /// </returns>
        public Task<ListedResponse<Status>> StatusesAsync(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiArrayAsync<Status>(MethodType.Get, "lists/statuses", parameters);
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
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the statuses.</para>
        /// </returns>
        public Task<ListedResponse<Status>> StatusesAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiArrayAsync<Status>(MethodType.Get, "lists/statuses", parameters, cancellationToken);
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
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the statuses.</para>
        /// </returns>
        public Task<ListedResponse<Status>> StatusesAsync<T>(T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiArrayAsync<Status, T>(MethodType.Get, "lists/statuses", parameters, cancellationToken);
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
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the list.</para>
        /// </returns>
        public Task<ListResponse> CreateAsync(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiAsync<ListResponse>(MethodType.Post, "lists/create", parameters);
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
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the list.</para>
        /// </returns>
        public Task<ListResponse> CreateAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<ListResponse>(MethodType.Post, "lists/create", parameters, cancellationToken);
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
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the list.</para>
        /// </returns>
        public Task<ListResponse> CreateAsync<T>(T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<ListResponse, T>(MethodType.Post, "lists/create", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Deletes the specified list. The authenticated user must own the list to be able to destroy it.</para>
        /// <para>Note: Either a list_id or a slug is required.</para>
        /// <para>If providing a list_slug, an owner_screen_name or owner_id is also required.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> owner_screen_name (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the destroied list.</para>
        /// </returns>
        public Task<ListResponse> DestroyAsync(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiAsync<ListResponse>(MethodType.Post, "lists/destroy", parameters);
        }

        /// <summary>
        /// <para>Deletes the specified list. The authenticated user must own the list to be able to destroy it.</para>
        /// <para>Note: Either a list_id or a slug is required.</para>
        /// <para>If providing a list_slug, an owner_screen_name or owner_id is also required.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> owner_screen_name (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the destroied list.</para>
        /// </returns>
        public Task<ListResponse> DestroyAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<ListResponse>(MethodType.Post, "lists/destroy", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Deletes the specified list. The authenticated user must own the list to be able to destroy it.</para>
        /// <para>Note: Either a list_id or a slug is required.</para>
        /// <para>If providing a list_slug, an owner_screen_name or owner_id is also required.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> owner_screen_name (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the destroied list.</para>
        /// </returns>
        public Task<ListResponse> DestroyAsync<T>(T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<ListResponse, T>(MethodType.Post, "lists/destroy", parameters, cancellationToken);
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
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the list.</para>
        /// </returns>
        public Task<ListResponse> UpdateAsync(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiAsync<ListResponse>(MethodType.Post, "lists/update", parameters);
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
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the list.</para>
        /// </returns>
        public Task<ListResponse> UpdateAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<ListResponse>(MethodType.Post, "lists/update", parameters, cancellationToken);
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
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the list.</para>
        /// </returns>
        public Task<ListResponse> UpdateAsync<T>(T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<ListResponse, T>(MethodType.Post, "lists/update", parameters, cancellationToken);
        }
    }

    partial class Members
    {
        //GET Methods

        /// <summary>
        /// <para>Returns the members of the specified list as an asynchronous operation.</para>
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
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the lists.</para>
        /// </returns>
        public Task<Cursored<User>> ListAsync(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiAsync<Cursored<User>>(MethodType.Get, "lists/members", parameters);
        }

        /// <summary>
        /// <para>Returns the members of the specified list as an asynchronous operation.</para>
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
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the lists.</para>
        /// </returns>
        public Task<Cursored<User>> ListAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<Cursored<User>>(MethodType.Get, "lists/members", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Returns the members of the specified list as an asynchronous operation.</para>
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
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the lists.</para>
        /// </returns>
        public Task<Cursored<User>> ListAsync<T>(T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<Cursored<User>, T>(MethodType.Get, "lists/members", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Check if the specified user is a member of the specified list as an asynchronous operation.</para>
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
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the user.</para>
        /// </returns>
        public Task<UserResponse> ShowAsync(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiAsync<UserResponse>(MethodType.Get, "lists/members/show", parameters);
        }

        /// <summary>
        /// <para>Check if the specified user is a member of the specified list as an asynchronous operation.</para>
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
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the user.</para>
        /// </returns>
        public Task<UserResponse> ShowAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<UserResponse>(MethodType.Get, "lists/members/show", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Check if the specified user is a member of the specified list as an asynchronous operation.</para>
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
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the user.</para>
        /// </returns>
        public Task<UserResponse> ShowAsync<T>(T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<UserResponse, T>(MethodType.Get, "lists/members/show", parameters, cancellationToken);
        }

        //POST Methods

        /// <summary>
        /// <para>Add a member to a list as an asynchronous operation.</para>
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
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the list.</para>
        /// </returns>
        public Task<ListResponse> CreateAsync(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiAsync<ListResponse>(MethodType.Post, "lists/members/create", parameters);
        }

        /// <summary>
        /// <para>Add a member to a list as an asynchronous operation.</para>
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
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the list.</para>
        /// </returns>
        public Task<ListResponse> CreateAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<ListResponse>(MethodType.Post, "lists/members/create", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Add a member to a list as an asynchronous operation.</para>
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
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the list.</para>
        /// </returns>
        public Task<ListResponse> CreateAsync<T>(T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<ListResponse, T>(MethodType.Post, "lists/members/create", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Adds multiple members to a list, by specifying a comma-separated list of member ids or screen names as an asynchronous operation.</para>
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
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the list.</para>
        /// </returns>
        public Task<ListResponse> CreateAllAsync(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiAsync<ListResponse>(MethodType.Post, "lists/members/create_all", parameters);
        }

        /// <summary>
        /// <para>Adds multiple members to a list, by specifying a comma-separated list of member ids or screen names as an asynchronous operation.</para>
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
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the list.</para>
        /// </returns>
        public Task<ListResponse> CreateAllAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<ListResponse>(MethodType.Post, "lists/members/create_all", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Adds multiple members to a list, by specifying a comma-separated list of member ids or screen names as an asynchronous operation.</para>
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
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the list.</para>
        /// </returns>
        public Task<ListResponse> CreateAllAsync<T>(T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<ListResponse, T>(MethodType.Post, "lists/members/create_all", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Removes the specified member from the list as an asynchronous operation.</para>
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
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the list.</para>
        /// </returns>
        public Task<ListResponse> DeleteAsync(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiAsync<ListResponse>(MethodType.Post, "lists/members/delete", parameters);
        }

        /// <summary>
        /// <para>Removes the specified member from the list as an asynchronous operation.</para>
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
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the list.</para>
        /// </returns>
        public Task<ListResponse> DeleteAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<ListResponse>(MethodType.Post, "lists/members/delete", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Removes the specified member from the list as an asynchronous operation.</para>
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
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the list.</para>
        /// </returns>
        public Task<ListResponse> DeleteAsync<T>(T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<ListResponse, T>(MethodType.Post, "lists/members/delete", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Removes multiple members from a list, by specifying a comma-separated list of member ids or screen names as an asynchronous operation.</para>
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
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the list.</para>
        /// </returns>
        public Task<ListResponse> DeleteAllAsync(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiAsync<ListResponse>(MethodType.Post, "lists/members/delete_all", parameters);
        }

        /// <summary>
        /// <para>Removes multiple members from a list, by specifying a comma-separated list of member ids or screen names as an asynchronous operation.</para>
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
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the list.</para>
        /// </returns>
        public Task<ListResponse> DeleteAllAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<ListResponse>(MethodType.Post, "lists/members/delete_all", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Removes multiple members from a list, by specifying a comma-separated list of member ids or screen names as an asynchronous operation.</para>
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
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the list.</para>
        /// </returns>
        public Task<ListResponse> DeleteAllAsync<T>(T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<ListResponse, T>(MethodType.Post, "lists/members/delete_all", parameters, cancellationToken);
        }
    }

    partial class Subscribers
    {
        //GET Method

        /// <summary>
        /// <para>Check if the specified user is a subscriber of the specified list as an asynchronous operation.</para>
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
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the user.</para>
        /// </returns>
        public Task<UserResponse> ShowAsync(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiAsync<UserResponse>(MethodType.Get, "lists/subscribers/show", parameters);
        }

        /// <summary>
        /// <para>Check if the specified user is a subscriber of the specified list as an asynchronous operation.</para>
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
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the user.</para>
        /// </returns>
        public Task<UserResponse> ShowAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<UserResponse>(MethodType.Get, "lists/subscribers/show", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Check if the specified user is a subscriber of the specified list as an asynchronous operation.</para>
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
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the user.</para>
        /// </returns>
        public Task<UserResponse> ShowAsync<T>(T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<UserResponse, T>(MethodType.Get, "lists/subscribers/show", parameters, cancellationToken);
        }

        //POST Methods

        /// <summary>
        /// <para>Subscribes the authenticated user to the specified list as an asynchronous operation.</para>
        /// <para>Note: Either a list_id or a slug is required.</para>
        /// <para>If providing a list_slug, an owner_screen_name or owner_id is also required.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> list_id (required)</para>
        /// <para>- <c>string</c> slug (required)</para>
        /// <para>- <c>string</c> owner_screen_name (optional)</para>
        /// <para>- <c>long</c> owner_id (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the list.</para>
        /// </returns>
        public Task<ListResponse> CreateAsync(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiAsync<ListResponse>(MethodType.Post, "lists/subscribers/create", parameters);
        }

        /// <summary>
        /// <para>Subscribes the authenticated user to the specified list as an asynchronous operation.</para>
        /// <para>Note: Either a list_id or a slug is required.</para>
        /// <para>If providing a list_slug, an owner_screen_name or owner_id is also required.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> list_id (required)</para>
        /// <para>- <c>string</c> slug (required)</para>
        /// <para>- <c>string</c> owner_screen_name (optional)</para>
        /// <para>- <c>long</c> owner_id (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the list.</para>
        /// </returns>
        public Task<ListResponse> CreateAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<ListResponse>(MethodType.Post, "lists/subscribers/create", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Subscribes the authenticated user to the specified list as an asynchronous operation.</para>
        /// <para>Note: Either a list_id or a slug is required.</para>
        /// <para>If providing a list_slug, an owner_screen_name or owner_id is also required.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> list_id (required)</para>
        /// <para>- <c>string</c> slug (required)</para>
        /// <para>- <c>string</c> owner_screen_name (optional)</para>
        /// <para>- <c>long</c> owner_id (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the list.</para>
        /// </returns>
        public Task<ListResponse> CreateAsync<T>(T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<ListResponse, T>(MethodType.Post, "lists/subscribers/create", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Unsubscribes the authenticated user from the specified list as an asynchronous operation.</para>
        /// <para>Note: Either a list_id or a slug is required.</para>
        /// <para>If providing a list_slug, an owner_screen_name or owner_id is also required.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> list_id (required)</para>
        /// <para>- <c>string</c> slug (required)</para>
        /// <para>- <c>string</c> owner_screen_name (optional)</para>
        /// <para>- <c>long</c> owner_id (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the list.</para>
        /// </returns>
        public Task<ListResponse> DeleteAsync(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiAsync<ListResponse>(MethodType.Post, "lists/subscribers/delete", parameters);
        }

        /// <summary>
        /// <para>Unsubscribes the authenticated user from the specified list as an asynchronous operation.</para>
        /// <para>Note: Either a list_id or a slug is required.</para>
        /// <para>If providing a list_slug, an owner_screen_name or owner_id is also required.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> list_id (required)</para>
        /// <para>- <c>string</c> slug (required)</para>
        /// <para>- <c>string</c> owner_screen_name (optional)</para>
        /// <para>- <c>long</c> owner_id (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the list.</para>
        /// </returns>
        public Task<ListResponse> DeleteAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<ListResponse>(MethodType.Post, "lists/subscribers/delete", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Unsubscribes the authenticated user from the specified list as an asynchronous operation.</para>
        /// <para>Note: Either a list_id or a slug is required.</para>
        /// <para>If providing a list_slug, an owner_screen_name or owner_id is also required.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> list_id (required)</para>
        /// <para>- <c>string</c> slug (required)</para>
        /// <para>- <c>string</c> owner_screen_name (optional)</para>
        /// <para>- <c>long</c> owner_id (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the list.</para>
        /// </returns>
        public Task<ListResponse> DeleteAsync<T>(T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<ListResponse, T>(MethodType.Post, "lists/subscribers/delete", parameters, cancellationToken);
        }
    }
}
#endif
