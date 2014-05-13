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

namespace CoreTweet.Rest
{
    partial class MutesUsers
    {
        //GET Methods

        /// <summary>
        /// <para>Returns an array of numeric user ids the authenticating user has muted.</para>
        /// <seealso cref="https://dev.twitter.com/docs/api/1.1/get/mutes/users/ids"/>
        /// <para>Avaliable parameters: </para>
        /// <para><paramref name="long cursor (optional)"/> : Causes the list of IDs to be broken into pages of no more than 5000 IDs at a time. The number of IDs returned is not guaranteed to be 5000 as suspended users are filtered out. If no cursor is provided, a value of -1 will be assumed, which is the first "page."</para>
        /// </summary>
        /// <param name='parameters'>Parameters.</param>
        /// <returns>IDs.</returns>
        public Task<Cursored<long>> IdsAsync(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiAsync<Cursored<long>>(MethodType.Get, "mutes/users/ids", parameters);
        }
        public Task<Cursored<long>> IdsAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<Cursored<long>>(MethodType.Get, "mutes/users/ids", parameters, cancellationToken);
        }
        public Task<Cursored<long>> IdsAsync<T>(T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<Cursored<long>, T>(MethodType.Get, "mutes/users/ids", parameters, cancellationToken);
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
        public Task<Cursored<User>> ListAsync(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiAsync<Cursored<User>>(MethodType.Get, "mutes/users/list", parameters);
        }
        public Task<Cursored<User>> ListAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<Cursored<User>>(MethodType.Get, "mutes/users/list", parameters, cancellationToken);
        }
        public Task<Cursored<User>> ListAsync<T>(T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<Cursored<User>, T>(MethodType.Get, "mutes/users/list", parameters, cancellationToken);
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
        public Task<User> CreateAsync(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiAsync<User>(MethodType.Post, "mutes/users/create", parameters);
        }
        public Task<User> CreateAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<User>(MethodType.Post, "mutes/users/create", parameters, cancellationToken);
        }
        public Task<User> CreateAsync<T>(T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<User, T>(MethodType.Post, "mutes/users/create", parameters, cancellationToken);
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
        public Task<User> DestroyAsync(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiAsync<User>(MethodType.Post, "mutes/users/destroy", parameters);
        }
        public Task<User> DestroyAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<User>(MethodType.Post, "mutes/users/destroy", parameters, cancellationToken);
        }
        public Task<User> DestroyAsync<T>(T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<User, T>(MethodType.Post, "mutes/users/destroy", parameters, cancellationToken);
        }
    }
}
