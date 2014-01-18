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
using CoreTweet.Core;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CoreTweet.Rest
{

    ///<summary>
    /// GET/POST direct_messages
    /// These endpoints require an access token with RWD permissions.
    /// </summary>
    public class DirectMessages : TokenIncluded
    {
        internal DirectMessages(Tokens e) : base(e) { }
            
            
        //GET Methods
            
        /// <summary>
        /// <para>Returns the 20 most recent direct messages sent by the authenticating user. Includes detailed information about the sender and recipient user. You can request up to 200 direct messages per call, up to a maximum of 800 outgoing DMs.</para>
        /// <para>This method requires an access token with RWD (read, write and direct message) permissions.</para>
        /// <para>Avaliable parameters: </para>
        /// <para><paramref name="long since_id (optional)"/> : Returns results with an ID greater than (that is, more recent than) the specified ID. There are limits to the number of Tweets which can be accessed through the API. If the limit of Tweets has occured since the since_id, the since_id will be forced to the oldest ID available.</para>
        /// <para><paramref name="long max_id (optional)"/> : Returns results with an ID less than (that is, older than) or equal to the specified ID.</para>
        /// <para><paramref name="int count (optional)"/> : Specifies the number of records to retrieve. Must be less than or equal to 200.<\para>
        /// <para><paramref name="int page (optional)"/> : Specifies the page of results to retrieve.</para>
        /// <para><paramref name="bool include_entities (optional)"/> : The entities node will not be included when set to false.</para>
        /// </summary>
        /// <returns>Direct messages.</returns>
        /// <param name='tokens'>
        /// Tokens.
        /// </param>
        /// <param name='parameters'>
        /// Parameters.
        /// </param>
        public IEnumerable<DirectMessage> Sent(params Expression<Func<string,object>>[] parameters)
        {
            return this.Tokens.AccessApiArray<DirectMessage>(MethodType.Get, "direct_messages/sent", parameters);
        }
            
        /// <summary>
        /// <para>Returns a single direct message, specified by an id parameter. Like the /1.1/direct_messages.format request, this method will include the user objects of the sender and recipient.</para>
        /// <para>This method requires an access token with RWD (read, write and direct message) permissions.</para>
        /// <para>Avaliable parameters: </para>
        /// <para><paramref name="long id (required)"/> : The ID of the direct message.</para>
        /// </summary>
        /// <returns>Direct messages.</returns>
        /// <param name='tokens'>
        /// Tokens.
        /// </param>
        /// <param name='parameters'>
        /// Parameters.
        /// </param>
        public IEnumerable<DirectMessage> Show(params Expression<Func<string,object>>[] parameters)
        {
            return this.Tokens.AccessApiArray<DirectMessage>(MethodType.Get, "direct_messages/show", parameters);
        }
            
        //POST Methods
            
        /// <summary>
        /// <para>Sends a new direct message to the specified user from the authenticating user. Requires both the user and text parameters and must be a POST. Returns the sent message in the requested format if successful.</para>
        /// <para>This method requires an access token with RWD (read, write and direct message) permissions.</para>
        /// <para>Note: One of user_id or screen_name are required.</para>
        /// <para>Avaliable parameters: </para>
        /// <para><paramref name="long user_id (optional)"/> : The ID of the user who should receive the direct message. Helpful for disambiguating when a valid user ID is also a valid screen name.</para>
        /// <para><paramref name="string screen_name (optional)"/> : The screen name of the user who should receive the direct message. Helpful for disambiguating when a valid screen name is also a user ID.</para>
        /// <para><paramref name="string text (required)"/> : The text of your direct message. Be sure to URL encode as necessary, and keep the message under 140 characters.</para>
        /// </summary>
        /// <returns>The sent direct message.</returns>
        /// <param name='tokens'>
        /// Tokens.
        /// </param>
        /// <param name='parameters'>
        /// Parameters.
        /// </param>
        public DirectMessage New(params Expression<Func<string,object>>[] parameters)
        {
            return this.Tokens.AccessApi<DirectMessage>(MethodType.Post, "direct_messages/new", parameters);
        }
            
        /// <summary>
        /// <para>Destroys the direct message specified in the required ID parameter. The authenticating user must be the recipient of the specified direct message.</para>
        /// <para>This method requires an access token with RWD (read, write and direct message) permissions.</para>
        /// <para>Avaliable parameters: </para>
        /// <para><paramref name="long id (required)"/> : The ID of the direct message to delete.</para>
        /// <para><paramref name="bool include_entities"/> : The entities node will not be included when set to false.</para>
        /// </summary>
        /// <returns>The direct message.</returns>
        /// <param name='tokens'>
        /// Tokens.
        /// </param>
        /// <param name='parameters'>
        /// Parameters.
        /// </param>
        public DirectMessage Destroy(params Expression<Func<string,object>>[] parameters)
        {
            return this.Tokens.AccessApi<DirectMessage>(MethodType.Post, "direct_messages/destroy", parameters);
        }
    }
}
