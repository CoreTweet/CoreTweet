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
    /// Provides a set of methods for the wrapper of GET/POST blocks.
    /// </summary>
    public partial class Blocks : ApiProviderBase
    {
        internal Blocks(TokensBase e) : base(e) { }

#if !(PCL || WIN_RT || WP)
        //GET Methods

        /// <summary>
        /// <para>Returns an array of numeric user ids the authenticating user is blocking.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> cursor (semi-optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>IDs.</returns>
        public Cursored<long> Ids(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApi<Cursored<long>>(MethodType.Get, "blocks/ids", parameters);
        }

        /// <summary>
        /// <para>Returns an array of numeric user ids the authenticating user is blocking.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> cursor (semi-optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>IDs.</returns>
        public Cursored<long> Ids(IDictionary<string, object> parameters)
        {
            return this.Tokens.AccessApi<Cursored<long>>(MethodType.Get, "blocks/ids", parameters);
        }

        /// <summary>
        /// <para>Returns an array of numeric user ids the authenticating user is blocking.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> cursor (semi-optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>IDs.</returns>
        public Cursored<long> Ids<T>(T parameters)
        {
            return this.Tokens.AccessApi<Cursored<long>, T>(MethodType.Get, "blocks/ids", parameters);
        }

        /// <summary>
        /// <para>Enumerates numeric user ids the authenticating user is blocking.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> cursor (semi-optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="mode">Specify whether enumerating goes to the next page or the previous.</param>
        /// <returns>IDs.</returns>
        public IEnumerable<long> EnumerateIds(EnumerateMode mode, params Expression<Func<string, object>>[] parameters)
        {
            return Cursored<long>.Enumerate(this.Tokens, "blocks/ids", mode, parameters);
        }

        /// <summary>
        /// <para>Enumerates numeric user ids the authenticating user is blocking.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> cursor (semi-optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="mode">Specify whether enumerating goes to the next page or the previous.</param>
        /// <returns>IDs.</returns>
        public IEnumerable<long> EnumerateIds(EnumerateMode mode, IDictionary<string, object> parameters)
        {
            return Cursored<long>.Enumerate(this.Tokens, "blocks/ids", mode, parameters);
        }

        /// <summary>
        /// <para>Enumerates numeric user ids the authenticating user is blocking.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> cursor (semi-optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="mode">Specify whether enumerating goes to the next page or the previous.</param>
        /// <returns>IDs.</returns>
        public IEnumerable<long> EnumerateIds<T>(EnumerateMode mode, T parameters)
        {
            return Cursored<long>.Enumerate<T>(this.Tokens, "blocks/ids", mode, parameters);
        }

        /// <summary>
        /// <para>Returns a collection of user objects that the authenticating user is blocking.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// <para>- <c>bool</c> skip_status (optional)</para>
        /// <para>- <c>long</c> cursor (semi-optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The users.</returns>
        public Cursored<User> List(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApi<Cursored<User>>(MethodType.Get, "blocks/list", parameters);
        }

        /// <summary>
        /// <para>Returns a collection of user objects that the authenticating user is blocking.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// <para>- <c>bool</c> skip_status (optional)</para>
        /// <para>- <c>long</c> cursor (semi-optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The users.</returns>
        public Cursored<User> List(IDictionary<string, object> parameters)
        {
            return this.Tokens.AccessApi<Cursored<User>>(MethodType.Get, "blocks/list", parameters);
        }

        /// <summary>
        /// <para>Returns a collection of user objects that the authenticating user is blocking.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// <para>- <c>bool</c> skip_status (optional)</para>
        /// <para>- <c>long</c> cursor (semi-optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The users.</returns>
        public Cursored<User> List<T>(T parameters)
        {
            return this.Tokens.AccessApi<Cursored<User>, T>(MethodType.Get, "blocks/list", parameters);
        }

        /// <summary>
        /// <para>Enumerates numeric user objects the authenticating user is blocking.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// <para>- <c>bool</c> skip_status (optional)</para>
        /// <para>- <c>long</c> cursor (semi-optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="mode">Specify whether enumerating goes to the next page or the previous.</param>
        /// <returns>The users.</returns>
        public IEnumerable<User> EnumerateList(EnumerateMode mode, params Expression<Func<string, object>>[] parameters)
        {
            return Cursored<User>.Enumerate(this.Tokens, "blocks/list", mode, parameters);
        }

        /// <summary>
        /// <para>Enumerates numeric user objects the authenticating user is blocking.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// <para>- <c>bool</c> skip_status (optional)</para>
        /// <para>- <c>long</c> cursor (semi-optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="mode">Specify whether enumerating goes to the next page or the previous.</param>
        /// <returns>The users.</returns>
        public IEnumerable<User> EnumerateList(EnumerateMode mode, IDictionary<string, object> parameters)
        {
            return Cursored<User>.Enumerate(this.Tokens, "blocks/list", mode, parameters);
        }

        /// <summary>
        /// <para>Enumerates numeric user objects the authenticating user is blocking.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// <para>- <c>bool</c> skip_status (optional)</para>
        /// <para>- <c>long</c> cursor (semi-optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="mode">Specify whether enumerating goes to the next page or the previous.</param>
        /// <returns>The users.</returns>
        public IEnumerable<User> EnumerateList<T>(EnumerateMode mode, T parameters)
        {
            return Cursored<User>.Enumerate<T>(this.Tokens, "blocks/list", mode, parameters);
        }

        //POST Methods

        /// <summary>
        /// <para>Blocks the specified user from following the authenticating user.</para>
        /// <para>In addition the blocked user will not show in the authenticating users mentions or timeline.</para>
        /// <para>If a follow or friend relationship exists it is destroyed.</para>
        /// <para>Note: Either screen_name or user_id must be provided.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> screen_name (optional)</para>
        /// <para>- <c>long</c> user_id (optional)</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// <para>- <c>bool</c> skip_status (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The user object.</returns>
        public UserResponse Create(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApi<UserResponse>(MethodType.Post, "blocks/create", parameters);
        }

        /// <summary>
        /// <para>Blocks the specified user from following the authenticating user.</para>
        /// <para>In addition the blocked user will not show in the authenticating users mentions or timeline.</para>
        /// <para>If a follow or friend relationship exists it is destroyed.</para>
        /// <para>Note: Either screen_name or user_id must be provided.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> screen_name (optional)</para>
        /// <para>- <c>long</c> user_id (optional)</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// <para>- <c>bool</c> skip_status (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The user object.</returns>
        public UserResponse Create(IDictionary<string, object> parameters)
        {
            return this.Tokens.AccessApi<UserResponse>(MethodType.Post, "blocks/create", parameters);
        }

        /// <summary>
        /// <para>Blocks the specified user from following the authenticating user.</para>
        /// <para>In addition the blocked user will not show in the authenticating users mentions or timeline.</para>
        /// <para>If a follow or friend relationship exists it is destroyed.</para>
        /// <para>Note: Either screen_name or user_id must be provided.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> screen_name (optional)</para>
        /// <para>- <c>long</c> user_id (optional)</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// <para>- <c>bool</c> skip_status (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The user object.</returns>
        public UserResponse Create<T>(T parameters)
        {
            return this.Tokens.AccessApi<UserResponse, T>(MethodType.Post, "blocks/create", parameters);
        }

        /// <summary>
        /// <para>Un-blocks the user specified in the ID parameter for the authenticating user.</para>
        /// <para>Returns the un-blocked user in the requested format when successful.</para>
        /// <para>If relationships existed before the block was instated, they will not be restored.</para>
        /// <para>Note: Either screen_name or user_id must be provided.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> screen_name (optional)</para>
        /// <para>- <c>long</c> user_id (optional)</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// <para>- <c>bool</c> skip_status (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The user object.</returns>
        public UserResponse Destroy(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApi<UserResponse>(MethodType.Post, "blocks/destroy", parameters);
        }

        /// <summary>
        /// <para>Un-blocks the user specified in the ID parameter for the authenticating user.</para>
        /// <para>Returns the un-blocked user in the requested format when successful.</para>
        /// <para>If relationships existed before the block was instated, they will not be restored.</para>
        /// <para>Note: Either screen_name or user_id must be provided.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> screen_name (optional)</para>
        /// <para>- <c>long</c> user_id (optional)</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// <para>- <c>bool</c> skip_status (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The user object.</returns>
        public UserResponse Destroy(IDictionary<string, object> parameters)
        {
            return this.Tokens.AccessApi<UserResponse>(MethodType.Post, "blocks/destroy", parameters);
        }

        /// <summary>
        /// <para>Un-blocks the user specified in the ID parameter for the authenticating user.</para>
        /// <para>Returns the un-blocked user in the requested format when successful.</para>
        /// <para>If relationships existed before the block was instated, they will not be restored.</para>
        /// <para>Note: Either screen_name or user_id must be provided.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> screen_name (optional)</para>
        /// <para>- <c>long</c> user_id (optional)</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// <para>- <c>bool</c> skip_status (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The user object.</returns>
        public UserResponse Destroy<T>(T parameters)
        {
            return this.Tokens.AccessApi<UserResponse, T>(MethodType.Post, "blocks/destroy", parameters);
        }
#endif
    }
}