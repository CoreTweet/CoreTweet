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
    partial class DirectMessages
    {
        //GET Methods

        /// <summary>
        /// <para>Returns the 20 most recent direct messages sent to the authenticating user as an asynchronous operation.</para>
        /// <para>Includes detailed information about the sender and recipient user.</para>
        /// <para>You can request up to 200 direct messages per call, up to a maximum of 800 incoming DMs.</para>
        /// <para>This method requires an access token with RWD (read, write and direct message) permissions.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> since_id (optional)</para>
        /// <para>- <c>long</c> max_id (optional)</para>
        /// <para>- <c>int</c> count (optional)</para>
        /// <para>- <c>int</c> page (optional)</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the direct messages.</para>
        /// </returns>
        public Task<ListedResponse<DirectMessage>> ReceivedAsync(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiArrayAsync<DirectMessage>(MethodType.Get, "direct_messages", parameters);
        }

        /// <summary>
        /// <para>Returns the 20 most recent direct messages sent to the authenticating user as an asynchronous operation.</para>
        /// <para>Includes detailed information about the sender and recipient user.</para>
        /// <para>You can request up to 200 direct messages per call, up to a maximum of 800 incoming DMs.</para>
        /// <para>This method requires an access token with RWD (read, write and direct message) permissions.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> since_id (optional)</para>
        /// <para>- <c>long</c> max_id (optional)</para>
        /// <para>- <c>int</c> count (optional)</para>
        /// <para>- <c>int</c> page (optional)</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the direct messages.</para>
        /// </returns>
        public Task<ListedResponse<DirectMessage>> ReceivedAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiArrayAsync<DirectMessage>(MethodType.Get, "direct_messages", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Returns the 20 most recent direct messages sent to the authenticating user as an asynchronous operation.</para>
        /// <para>Includes detailed information about the sender and recipient user.</para>
        /// <para>You can request up to 200 direct messages per call, up to a maximum of 800 incoming DMs.</para>
        /// <para>This method requires an access token with RWD (read, write and direct message) permissions.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> since_id (optional)</para>
        /// <para>- <c>long</c> max_id (optional)</para>
        /// <para>- <c>int</c> count (optional)</para>
        /// <para>- <c>int</c> page (optional)</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the direct messages.</para>
        /// </returns>
        public Task<ListedResponse<DirectMessage>> ReceivedAsync<T>(T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiArrayAsync<DirectMessage, T>(MethodType.Get, "direct_messages", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Returns the 20 most recent direct messages sent by the authenticating user as an asynchronous operation.</para>
        /// <para>Includes detailed information about the sender and recipient user.</para>
        /// <para>You can request up to 200 direct messages per call, up to a maximum of 800 outgoing DMs.</para>
        /// <para>This method requires an access token with RWD (read, write and direct message) permissions.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> since_id (optional)</para>
        /// <para>- <c>long</c> max_id (optional)</para>
        /// <para>- <c>int</c> count (optional)</para>
        /// <para>- <c>int</c> page (optional)</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the direct messages.</para>
        /// </returns>
        public Task<ListedResponse<DirectMessage>> SentAsync(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiArrayAsync<DirectMessage>(MethodType.Get, "direct_messages/sent", parameters);
        }

        /// <summary>
        /// <para>Returns the 20 most recent direct messages sent by the authenticating user as an asynchronous operation.</para>
        /// <para>Includes detailed information about the sender and recipient user.</para>
        /// <para>You can request up to 200 direct messages per call, up to a maximum of 800 outgoing DMs.</para>
        /// <para>This method requires an access token with RWD (read, write and direct message) permissions.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> since_id (optional)</para>
        /// <para>- <c>long</c> max_id (optional)</para>
        /// <para>- <c>int</c> count (optional)</para>
        /// <para>- <c>int</c> page (optional)</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the direct messages.</para>
        /// </returns>
        public Task<ListedResponse<DirectMessage>> SentAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiArrayAsync<DirectMessage>(MethodType.Get, "direct_messages/sent", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Returns the 20 most recent direct messages sent by the authenticating user as an asynchronous operation.</para>
        /// <para>Includes detailed information about the sender and recipient user.</para>
        /// <para>You can request up to 200 direct messages per call, up to a maximum of 800 outgoing DMs.</para>
        /// <para>This method requires an access token with RWD (read, write and direct message) permissions.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> since_id (optional)</para>
        /// <para>- <c>long</c> max_id (optional)</para>
        /// <para>- <c>int</c> count (optional)</para>
        /// <para>- <c>int</c> page (optional)</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the direct messages.</para>
        /// </returns>
        public Task<ListedResponse<DirectMessage>> SentAsync<T>(T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiArrayAsync<DirectMessage, T>(MethodType.Get, "direct_messages/sent", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Returns a single direct message, specified by an id parameter as an asynchronous operation.</para>
        /// <para>Like the /1.1/direct_messages.format request, this method will include the user objects of the sender and recipient.</para>
        /// <para>This method requires an access token with RWD (read, write and direct message) permissions.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> id (required)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the direct messages.</para>
        /// </returns>
        public Task<DirectMessageResponse> ShowAsync(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiAsync<DirectMessageResponse>(MethodType.Get, "direct_messages/show", parameters);
        }

        /// <summary>
        /// <para>Returns a single direct message, specified by an id parameter as an asynchronous operation.</para>
        /// <para>Like the /1.1/direct_messages.format request, this method will include the user objects of the sender and recipient.</para>
        /// <para>This method requires an access token with RWD (read, write and direct message) permissions.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> id (required)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the direct messages.</para>
        /// </returns>
        public Task<DirectMessageResponse> ShowAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<DirectMessageResponse>(MethodType.Get, "direct_messages/show", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Returns a single direct message, specified by an id parameter as an asynchronous operation.</para>
        /// <para>Like the /1.1/direct_messages.format request, this method will include the user objects of the sender and recipient.</para>
        /// <para>This method requires an access token with RWD (read, write and direct message) permissions.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> id (required)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the direct messages.</para>
        /// </returns>
        public Task<DirectMessageResponse> ShowAsync<T>(T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<DirectMessageResponse, T>(MethodType.Get, "direct_messages/show", parameters, cancellationToken);
        }

        //POST Methods


        /// <summary>
        /// <para>Sends a new direct message to the specified user from the authenticating user as an asynchronous operation.</para>
        /// <para>Requires both the user and text parameters and must be a POST.</para>
        /// <para>Returns the sent message in the requested format if successful.</para>
        /// <para>This method requires an access token with RWD (read, write and direct message) permissions.</para>
        /// <para>Note: Either user_id or screen_name is required.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> user_id (optional)</para>
        /// <para>- <c>string</c> screen_name (optional)</para>
        /// <para>- <c>string</c> text (required)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the direct message.</para>
        /// </returns>
        public Task<DirectMessageResponse> NewAsync(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiAsync<DirectMessageResponse>(MethodType.Post, "direct_messages/new", parameters);
        }

        /// <summary>
        /// <para>Sends a new direct message to the specified user from the authenticating user as an asynchronous operation.</para>
        /// <para>Requires both the user and text parameters and must be a POST.</para>
        /// <para>Returns the sent message in the requested format if successful.</para>
        /// <para>This method requires an access token with RWD (read, write and direct message) permissions.</para>
        /// <para>Note: Either user_id or screen_name is required.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> user_id (optional)</para>
        /// <para>- <c>string</c> screen_name (optional)</para>
        /// <para>- <c>string</c> text (required)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the direct message.</para>
        /// </returns>
        public Task<DirectMessageResponse> NewAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<DirectMessageResponse>(MethodType.Post, "direct_messages/new", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Sends a new direct message to the specified user from the authenticating user as an asynchronous operation.</para>
        /// <para>Requires both the user and text parameters and must be a POST.</para>
        /// <para>Returns the sent message in the requested format if successful.</para>
        /// <para>This method requires an access token with RWD (read, write and direct message) permissions.</para>
        /// <para>Note: Either user_id or screen_name is required.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> user_id (optional)</para>
        /// <para>- <c>string</c> screen_name (optional)</para>
        /// <para>- <c>string</c> text (required)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the direct message.</para>
        /// </returns>
        public Task<DirectMessageResponse> NewAsync<T>(T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<DirectMessageResponse, T>(MethodType.Post, "direct_messages/new", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Destroys the direct message specified in the required ID parameter as an asynchronous operation.</para>
        /// <para>The authenticating user must be the recipient of the specified direct message.</para>
        /// <para>This method requires an access token with RWD (read, write and direct message) permissions.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> id (required)</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the direct message.</para>
        /// </returns>
        public Task<DirectMessageResponse> DestroyAsync(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiAsync<DirectMessageResponse>(MethodType.Post, "direct_messages/destroy", parameters);
        }

        /// <summary>
        /// <para>Destroys the direct message specified in the required ID parameter as an asynchronous operation.</para>
        /// <para>The authenticating user must be the recipient of the specified direct message.</para>
        /// <para>This method requires an access token with RWD (read, write and direct message) permissions.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> id (required)</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the direct message.</para>
        /// </returns>
        public Task<DirectMessageResponse> DestroyAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<DirectMessageResponse>(MethodType.Post, "direct_messages/destroy", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Destroys the direct message specified in the required ID parameter as an asynchronous operation.</para>
        /// <para>The authenticating user must be the recipient of the specified direct message.</para>
        /// <para>This method requires an access token with RWD (read, write and direct message) permissions.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> id (required)</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the direct message.</para>
        /// </returns>
        public Task<DirectMessageResponse> DestroyAsync<T>(T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<DirectMessageResponse, T>(MethodType.Post, "direct_messages/destroy", parameters, cancellationToken);
        }
    }
}
