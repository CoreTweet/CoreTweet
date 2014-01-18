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

    ///<summary>GET/POST blocks</summary>
    public class Blocks : TokenIncluded
    {
        internal Blocks(Tokens e) : base(e)
        {
        }
            
            
        //GET Methods
            
        /// <summary>
        /// <para>Returns an array of numeric user ids the authenticating user is blocking.</para>
        /// <para>Avaliable parameters: </para>
        /// <para><paramref name="long cursor (semi-optional)"/> : Causes the list of IDs to be broken into pages of no more than 5000 IDs at a time. The number of IDs returned is not guaranteed to be 5000 as suspended users are filtered out after connections are queried. If no cursor is provided, a value of -1 will be assumed, which is the first "page." The response from the API will include a previous_cursor and next_cursor to allow paging back and forth. See Using cursors to navigate collections for more information.</para>
        /// </summary>
        /// <returns>IDs.</returns>
        /// <param name='parameters'>
        /// Parameters.
        /// </param>
        /// <see cref="https://dev.twitter.com/docs/misc/cursoring"/>
        public Cursored<long> IDs(params Expression<Func<string,object>>[] parameters)
        {
            return this.Tokens.AccessApi<Cursored<long>>(MethodType.Get, "blocks/ids", parameters);
        }
         
        /// <summary>
        /// <para>Enumerates numeric user ids the authenticating user is blocking.</para>
        /// <para>Avaliable parameters: </para>
        /// <para><paramref name="long cursor (optional)"/> : The first cursor. If not be specified, enumerating starts from the first page.</para>
        /// </summary>
        /// <returns>
        /// IDs.
        /// </returns>
        /// <see cref="https://dev.twitter.com/docs/misc/cursoring"/>
        /// <param name='mode'>
        /// <para> Specify whether enumerating goes to the next page or the previous.</para>
        /// </param>
        /// <param name='parameters'>
        /// Parameters.
        /// </param>
        public IEnumerable<long> EnumerateIDs(EnumerateMode mode, params Expression<Func<string,object>>[] parameters)
        {
            return Cursored<long>.Enumerate(this.Tokens, "blocks/ids", mode, parameters);
        }
            
        /// <summary>
        /// <para>Returns a collection of user objects that the authenticating user is blocking.</para>
        /// <para>Avaliable parameters: </para>
        /// <para><paramref name="bool include_entities (optional)"/> : The entities node will not be included when set to false.</para>
        /// <para><paramref name="bool skip_status (optional)"/> : When set to true, statuses will not be included in the returned user objects.</para>
        /// <para><paramref name="long cursor (semi-optional)"/> : Causes the list of blocked users to be broken into pages of no more than 5000 IDs at a time. The number of IDs returned is not guaranteed to be 5000 as suspended users are filtered out after connections are queried. If no cursor is provided, a value of -1 will be assumed, which is the first "page." The response from the API will include a previous_cursor and next_cursor to allow paging back and forth. See Using cursors to navigate collections for more information.</para>
        /// </summary>
        /// <returns>Users.</returns>
        /// <param name='parameters'>
        /// Parameters.
        /// </param>
        /// <see cref="https://dev.twitter.com/docs/misc/cursoring"/>
        public Cursored<User> List(params Expression<Func<string,object>>[] parameters)
        {
            return this.Tokens.AccessApi<Cursored<User>>(MethodType.Get, "blocks/list", parameters);
        }

        /// <summary>
        /// <para>Enumerates numeric user objects the authenticating user is blocking.</para>
        /// <para>Avaliable parameters: </para>
        /// <para><paramref name="bool include_entities (optional)"/> : The entities node will not be included when set to false.</para>
        /// <para><paramref name="bool skip_status (optional)"/> : When set to true, statuses will not be included in the returned user objects.</para>
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
        public IEnumerable<User> EnumerateList(EnumerateMode mode, params Expression<Func<string,object>>[] parameters)
        {
            return Cursored<User>.Enumerate(this.Tokens, "blocks/list", mode, parameters);
        }
            
        //POST Methods
            
        /// <summary>
        /// <para>Blocks the specified user from following the authenticating user. In addition the blocked user will not show in the authenticating users mentions or timeline (unless retweeted by another user). If a follow or friend relationship exists it is destroyed.</para>
        /// <para>Note: Either screen_name or user_id must be provided.</para>
        /// <para>Avaliable parameters: </para>
        /// <para><paramref name="string screen_name (optional)"/> : The screen name of the potentially blocked user. Helpful for disambiguating when a valid screen name is also a user ID.</para>
        /// <para><paramref name="long user_id (optional)"/> : The ID of the potentially blocked user. Helpful for disambiguating when a valid user ID is also a valid screen name.</para>
        /// <para><paramref name="bool include_entities (optional)"/> : The entities node will not be included when set to false.</para>
        /// <para><paramref name="bool skip_status (optional)"/> : When set to true, statuses will not be included in the returned user objects.</para>
        /// </summary>
        /// <returns>The user.</returns>
        /// <param name='parameters'>
        /// Parameters.
        /// </param>
        public User Create(params Expression<Func<string,object>>[] parameters)
        {
            return this.Tokens.AccessApi<User>(MethodType.Post, "blocks/create", parameters);
        }
            
        /// <summary>
        /// <para>Un-blocks the user specified in the ID parameter for the authenticating user. Returns the un-blocked user in the requested format when successful. If relationships existed before the block was instated, they will not be restored.</para>
        /// <para>Note: Either screen_name or user_id must be provided.</para>
        /// <para>Avaliable parameters: </para>
        /// <para><paramref name="string screen_name (optional)"/> : The screen name of the potentially blocked user. Helpful for disambiguating when a valid screen name is also a user ID.</para>
        /// <para><paramref name="long user_id (optional)"/> : The ID of the potentially blocked user. Helpful for disambiguating when a valid user ID is also a valid screen name.</para>
        /// <para><paramref name="bool include_entities (optional)"/> : The entities node will not be included when set to false.</para>
        /// <para><paramref name="bool skip_status (optional)"/> : When set to either true, t or 1 statuses will not be included in the returned user objects.</para>
        /// </summary>
        /// <returns>The user.</returns>
        /// <param name='parameters'>
        /// Parameters.
        /// </param>
        public User Destroy(params Expression<Func<string,object>>[] parameters)
        {
            return this.Tokens.AccessApi<User>(MethodType.Post, "blocks/destroy", parameters);
        }
    }
}