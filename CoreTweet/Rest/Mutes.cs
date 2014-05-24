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
    /// <summary>GET/POST mutes</summary>
    public class Mutes : ApiProviderBase
    {
        internal Mutes(TokensBase e) : base(e) { }

        public MutesUsers Users { get { return new MutesUsers(this.Tokens); } }
    }

    /// <summary>GET/POST mutes/users</summary>
    public partial class MutesUsers : ApiProviderBase
    {
        internal MutesUsers(TokensBase e) : base(e) { }

#if !(PCL || WIN_RT || WP)
        //GET Methods

        /// <summary>
        /// <para>Returns an array of numeric user ids the authenticating user has muted.</para>
        /// <seealso cref="https://dev.twitter.com/docs/api/1.1/get/mutes/users/ids"/>
        /// <para>Avaliable parameters: </para>
        /// <para><paramref name="long cursor (optional)"/> : Causes the list of IDs to be broken into pages of no more than 5000 IDs at a time. The number of IDs returned is not guaranteed to be 5000 as suspended users are filtered out. If no cursor is provided, a value of -1 will be assumed, which is the first "page."</para>
        /// </summary>
        /// <param name='parameters'>Parameters.</param>
        /// <returns>IDs.</returns>
        public Cursored<long> Ids(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApi<Cursored<long>>(MethodType.Get, "mutes/users/ids", parameters);
        }
        public Cursored<long> Ids(IDictionary<string, object> parameters)
        {
            return this.Tokens.AccessApi<Cursored<long>>(MethodType.Get, "mutes/users/ids", parameters);
        }
        public Cursored<long> Ids<T>(T parameters)
        {
            return this.Tokens.AccessApi<Cursored<long>, T>(MethodType.Get, "mutes/users/ids", parameters);
        }

        /// <summary>
        /// <para>Enumerates numeric user ids the authenticating user has muted.</para>
        /// <seealso cref="https://dev.twitter.com/docs/api/1.1/get/mutes/users/ids"/>
        /// <para>Avaliable parameters: </para>
        /// <para><paramref name="long cursor (optional)"/> : Causes the list of IDs to be broken into pages of no more than 5000 IDs at a time. The number of IDs returned is not guaranteed to be 5000 as suspended users are filtered out. If no cursor is provided, a value of -1 will be assumed, which is the first "page."</para>
        /// </summary>
        /// <param name='parameters'>Parameters.</param>
        /// <returns>IDs.</returns>
        public IEnumerable<long> EnumerateIds(EnumerateMode mode, params Expression<Func<string, object>>[] parameters)
        {
            return Cursored<long>.Enumerate(this.Tokens, "mutes/users/ids", mode, parameters);
        }
        public IEnumerable<long> EnumerateIds(EnumerateMode mode, IDictionary<string, object> parameters)
        {
            return Cursored<long>.Enumerate(this.Tokens, "mutes/users/ids", mode, parameters);
        }
        public IEnumerable<long> EnumerateIds<T>(EnumerateMode mode, T parameters)
        {
            return Cursored<long>.Enumerate<T>(this.Tokens, "mutes/users/ids", mode, parameters);
        }

        /// <summary>
        /// <para>Returns an array of user objects the authenticating user has muted.</para>
        /// <seealso cref="https://dev.twitter.com/docs/api/1.1/get/mutes/users/list"/>
        /// <para>Avaliable parameters: </para>
        /// <para><paramref name="long cursor (optional)"/> : Causes the list of IDs to be broken into pages of no more than 5000 IDs at a time. The number of IDs returned is not guaranteed to be 5000 as suspended users are filtered out after connections are queried. If no cursor is provided, a value of -1 will be assumed, which is the first "page."</para>
        /// <para><paramref name="bool include_entities (optional)"/> : The entities node will not be included when set to false.</para>
        /// <para><paramref name="bool skip_status (optional)"/> : When set to either true, t or 1 statuses will not be included in the returned user objects.</para>
        /// </summary>
        /// <param name='parameters'>Parameters.</param>
        /// <returns>Users.</returns>
        public Cursored<User> List(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApi<Cursored<User>>(MethodType.Get, "mutes/users/list", parameters);
        }
        public Cursored<User> List(IDictionary<string, object> parameters)
        {
            return this.Tokens.AccessApi<Cursored<User>>(MethodType.Get, "mutes/users/list", parameters);
        }
        public Cursored<User> List<T>(T parameters)
        {
            return this.Tokens.AccessApi<Cursored<User>, T>(MethodType.Get, "mutes/users/list", parameters);
        }

        /// <summary>
        /// <para>Enumerates user objects the authenticating user has muted.</para>
        /// <seealso cref="https://dev.twitter.com/docs/api/1.1/get/mutes/users/list"/>
        /// <para>Avaliable parameters: </para>
        /// <para><paramref name="long cursor (optional)"/> : Causes the list of IDs to be broken into pages of no more than 5000 IDs at a time. The number of IDs returned is not guaranteed to be 5000 as suspended users are filtered out after connections are queried. If no cursor is provided, a value of -1 will be assumed, which is the first "page."</para>
        /// <para><paramref name="bool include_entities (optional)"/> : The entities node will not be included when set to false.</para>
        /// <para><paramref name="bool skip_status (optional)"/> : When set to either true, t or 1 statuses will not be included in the returned user objects.</para>
        /// </summary>
        /// <param name='parameters'>Parameters.</param>
        /// <returns>Users.</returns>
        public IEnumerable<User> EnumerateList(EnumerateMode mode, params Expression<Func<string, object>>[] parameters)
        {
            return Cursored<User>.Enumerate(this.Tokens, "mutes/users/list", mode, parameters);
        }
        public IEnumerable<User> EnumerateList(EnumerateMode mode, IDictionary<string, object> parameters)
        {
            return Cursored<User>.Enumerate(this.Tokens, "mutes/users/list", mode, parameters);
        }
        public IEnumerable<User> EnumerateList<T>(EnumerateMode mode, T parameters)
        {
            return Cursored<User>.Enumerate<T>(this.Tokens, "mutes/users/list", mode, parameters);
        }

        //POST Methods

        /// <summary>
        /// <para>Mutes the user specified in the ID parameter for the authenticating user.</para>
        /// <para>Returns the muted user in the requested format when successful. Returns a string describing the failure condition when unsuccessful.</para>
        /// <para>Actions taken in this method are asynchronous and changes will be eventually consistent.</para>
        /// <para>Note: Providing either screen_name or user_id is required.</para>
        /// <para>Avaliable parameters: </para>
        /// <para><paramref name="string screen_name (optional)"/> : The screen name of the potentially muted user. Helpful for disambiguating when a valid screen name is also a user ID.</para>
        /// <para><paramref name="long user_id (optional)"/> : The ID of the potentially muted user. Helpful for disambiguating when a valid user ID is also a valid screen name.</para>
        /// </summary>
        /// <returns>The user.</returns>
        /// <param name='parameters'>
        /// Parameters.
        /// </param>
        public User Create(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApi<User>(MethodType.Post, "mutes/users/create", parameters);
        }
        public User Create(IDictionary<string, object> parameters)
        {
            return this.Tokens.AccessApi<User>(MethodType.Post, "mutes/users/create", parameters);
        }
        public User Create<T>(T parameters)
        {
            return this.Tokens.AccessApi<User, T>(MethodType.Post, "mutes/users/create", parameters);
        }

        /// <summary>
        /// <para>Un-mutes the user specified in the ID parameter for the authenticating user.</para>
        /// <para>Returns the unmuted user in the requested format when successful. Returns a string describing the failure condition when unsuccessful.</para>
        /// <para>Actions taken in this method are asynchronous and changes will be eventually consistent.</para>
        /// <para>Note: Providing either screen_name or user_id is required.</para>
        /// <para>Avaliable parameters: </para>
        /// <para><paramref name="string screen_name (optional)"/> : The screen name of the potentially muted user. Helpful for disambiguating when a valid screen name is also a user ID.</para>
        /// <para><paramref name="long user_id (optional)"/> : The ID of the potentially muted user. Helpful for disambiguating when a valid user ID is also a valid screen name.</para>
        /// </summary>
        /// <returns>The user.</returns>
        /// <param name='parameters'>
        /// Parameters.
        /// </param>
        public User Destroy(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApi<User>(MethodType.Post, "mutes/users/destroy", parameters);
        }
        public User Destroy(IDictionary<string, object> parameters)
        {
            return this.Tokens.AccessApi<User>(MethodType.Post, "mutes/users/destroy", parameters);
        }
        public User Destroy<T>(T parameters)
        {
            return this.Tokens.AccessApi<User, T>(MethodType.Post, "mutes/users/destroy", parameters);
        }
#endif
    }
}
