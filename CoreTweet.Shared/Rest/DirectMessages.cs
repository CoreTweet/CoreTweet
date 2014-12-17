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
    /// Provides a set of methods for the wrapper of GET/POST direct_messages.
    /// </summary>
    public partial class DirectMessages : ApiProviderBase
    {
        internal DirectMessages(TokensBase e) : base(e) { }

#if !(PCL || WIN_RT || WP)
        //GET Methods

        /// <summary>
        /// <para>Returns the 20 most recent direct messages sent to the authenticating user.</para>
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
        /// <returns>The direct messages.</returns>
        public ListedResponse<DirectMessage> Received(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiArray<DirectMessage>(MethodType.Get, "direct_messages", parameters);
        }

        /// <summary>
        /// <para>Returns the 20 most recent direct messages sent to the authenticating user.</para>
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
        /// <returns>The direct messages.</returns>
        public ListedResponse<DirectMessage> Received(IDictionary<string, object> parameters)
        {
            return this.Tokens.AccessApiArray<DirectMessage>(MethodType.Get, "direct_messages", parameters);
        }

        /// <summary>
        /// <para>Returns the 20 most recent direct messages sent to the authenticating user.</para>
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
        /// <returns>The direct messages.</returns>
        public ListedResponse<DirectMessage> Received<T>(T parameters)
        {
            return this.Tokens.AccessApiArray<DirectMessage, T>(MethodType.Get, "direct_messages", parameters);
        }

        /// <summary>
        /// <para>Returns the 20 most recent direct messages sent by the authenticating user.</para>
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
        /// <returns>The direct messages.</returns>
        public ListedResponse<DirectMessage> Sent(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiArray<DirectMessage>(MethodType.Get, "direct_messages/sent", parameters);
        }

        /// <summary>
        /// <para>Returns the 20 most recent direct messages sent by the authenticating user.</para>
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
        /// <returns>The direct messages.</returns>
        public ListedResponse<DirectMessage> Sent(IDictionary<string, object> parameters)
        {
            return this.Tokens.AccessApiArray<DirectMessage>(MethodType.Get, "direct_messages/sent", parameters);
        }

        /// <summary>
        /// <para>Returns the 20 most recent direct messages sent by the authenticating user.</para>
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
        /// <returns>The direct messages.</returns>
        public ListedResponse<DirectMessage> Sent<T>(T parameters)
        {
            return this.Tokens.AccessApiArray<DirectMessage, T>(MethodType.Get, "direct_messages/sent", parameters);
        }

        /// <summary>
        /// <para>Returns a single direct message, specified by an id parameter.</para>
        /// <para>Like the /1.1/direct_messages.format request, this method will include the user objects of the sender and recipient.</para>
        /// <para>This method requires an access token with RWD (read, write and direct message) permissions.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> id (required)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The direct message.</returns>
        public DirectMessageResponse Show(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApi<DirectMessageResponse>(MethodType.Get, "direct_messages/show", parameters);
        }

        /// <summary>
        /// <para>Returns a single direct message, specified by an id parameter.</para>
        /// <para>Like the /1.1/direct_messages.format request, this method will include the user objects of the sender and recipient.</para>
        /// <para>This method requires an access token with RWD (read, write and direct message) permissions.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> id (required)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The direct message.</returns>
        public DirectMessageResponse Show(IDictionary<string, object> parameters)
        {
            return this.Tokens.AccessApi<DirectMessageResponse>(MethodType.Get, "direct_messages/show", parameters);
        }

        /// <summary>
        /// <para>Returns a single direct message, specified by an id parameter.</para>
        /// <para>Like the /1.1/direct_messages.format request, this method will include the user objects of the sender and recipient.</para>
        /// <para>This method requires an access token with RWD (read, write and direct message) permissions.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> id (required)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The direct message.</returns>
        public DirectMessageResponse Show<T>(T parameters)
        {
            return this.Tokens.AccessApi<DirectMessageResponse, T>(MethodType.Get, "direct_messages/show", parameters);
        }

        //POST Methods

        /// <summary>
        /// <para>Sends a new direct message to the specified user from the authenticating user.</para>
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
        /// <returns>The sent direct message.</returns>
        public DirectMessageResponse New(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApi<DirectMessageResponse>(MethodType.Post, "direct_messages/new", parameters);
        }

        /// <summary>
        /// <para>Sends a new direct message to the specified user from the authenticating user.</para>
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
        /// <returns>The sent direct message.</returns>
        public DirectMessageResponse New(IDictionary<string, object> parameters)
        {
            return this.Tokens.AccessApi<DirectMessageResponse>(MethodType.Post, "direct_messages/new", parameters);
        }

        /// <summary>
        /// <para>Sends a new direct message to the specified user from the authenticating user.</para>
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
        /// <returns>The sent direct message.</returns>
        public DirectMessageResponse New<T>(T parameters)
        {
            return this.Tokens.AccessApi<DirectMessageResponse, T>(MethodType.Post, "direct_messages/new", parameters);
        }

        /// <summary>
        /// <para>Destroys the direct message specified in the required ID parameter.</para>
        /// <para>The authenticating user must be the recipient of the specified direct message.</para>
        /// <para>This method requires an access token with RWD (read, write and direct message) permissions.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> id (required)</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The direct message.</returns>
        public DirectMessageResponse Destroy(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApi<DirectMessageResponse>(MethodType.Post, "direct_messages/destroy", parameters);
        }

        /// <summary>
        /// <para>Destroys the direct message specified in the required ID parameter.</para>
        /// <para>The authenticating user must be the recipient of the specified direct message.</para>
        /// <para>This method requires an access token with RWD (read, write and direct message) permissions.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> id (required)</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The direct message.</returns>
        public DirectMessageResponse Destroy(IDictionary<string, object> parameters)
        {
            return this.Tokens.AccessApi<DirectMessageResponse>(MethodType.Post, "direct_messages/destroy", parameters);
        }

        /// <summary>
        /// <para>Destroys the direct message specified in the required ID parameter.</para>
        /// <para>The authenticating user must be the recipient of the specified direct message.</para>
        /// <para>This method requires an access token with RWD (read, write and direct message) permissions.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> id (required)</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The direct message.</returns>
        public DirectMessageResponse Destroy<T>(T parameters)
        {
            return this.Tokens.AccessApi<DirectMessageResponse, T>(MethodType.Post, "direct_messages/destroy", parameters);
        }
#endif
    }
}
