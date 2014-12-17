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

namespace CoreTweet.Rest
{
    partial class MutesUsers
    {
        //GET Methods

        /// <summary>
        /// <para>Returns an array of numeric user ids the authenticating user has muted as an asynchronous operation.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> cursor (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the IDs.</para>
        /// </returns>
        public Task<Cursored<long>> IdsAsync(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiAsync<Cursored<long>>(MethodType.Get, "mutes/users/ids", parameters);
        }

        /// <summary>
        /// <para>Returns an array of numeric user ids the authenticating user has muted as an asynchronous operation.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> cursor (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the IDs.</para>
        /// </returns>
        public Task<Cursored<long>> IdsAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<Cursored<long>>(MethodType.Get, "mutes/users/ids", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Returns an array of numeric user ids the authenticating user has muted as an asynchronous operation.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> cursor (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the IDs.</para>
        /// </returns>
        public Task<Cursored<long>> IdsAsync<T>(T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<Cursored<long>, T>(MethodType.Get, "mutes/users/ids", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Returns an array of user objects the authenticating user has muted as an asynchronous operation.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> cursor (optional)</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// <para>- <c>bool</c> skip_status (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the users.</para>
        /// </returns>
        public Task<Cursored<User>> ListAsync(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiAsync<Cursored<User>>(MethodType.Get, "mutes/users/list", parameters);
        }

        /// <summary>
        /// <para>Returns an array of user objects the authenticating user has muted as an asynchronous operation.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> cursor (optional)</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// <para>- <c>bool</c> skip_status (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the users.</para>
        /// </returns>
        public Task<Cursored<User>> ListAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<Cursored<User>>(MethodType.Get, "mutes/users/list", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Returns an array of user objects the authenticating user has muted as an asynchronous operation.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> cursor (optional)</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// <para>- <c>bool</c> skip_status (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the users.</para>
        /// </returns>
        public Task<Cursored<User>> ListAsync<T>(T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<Cursored<User>, T>(MethodType.Get, "mutes/users/list", parameters, cancellationToken);
        }

        //POST Methods

        /// <summary>
        /// <para>Mutes the user specified in the ID parameter for the authenticating user as an asynchronous operation.</para>
        /// <para>Returns the muted user in the requested format when successful.</para>
        /// <para>Returns a string describing the failure condition when unsuccessful.</para>
        /// <para>Actions taken in this method are asynchronous and changes will be eventually consistent.</para>
        /// <para>Note: Providing either screen_name or user_id is required.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> screen_name (optional)</para>
        /// <para>- <c>long</c> user_id (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the user.</para>
        /// </returns>
        public Task<UserResponse> CreateAsync(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiAsync<UserResponse>(MethodType.Post, "mutes/users/create", parameters);
        }

        /// <summary>
        /// <para>Mutes the user specified in the ID parameter for the authenticating user as an asynchronous operation.</para>
        /// <para>Returns the muted user in the requested format when successful.</para>
        /// <para>Returns a string describing the failure condition when unsuccessful.</para>
        /// <para>Actions taken in this method are asynchronous and changes will be eventually consistent.</para>
        /// <para>Note: Providing either screen_name or user_id is required.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> screen_name (optional)</para>
        /// <para>- <c>long</c> user_id (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the user.</para>
        /// </returns>
        public Task<UserResponse> CreateAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<UserResponse>(MethodType.Post, "mutes/users/create", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Mutes the user specified in the ID parameter for the authenticating user as an asynchronous operation.</para>
        /// <para>Returns the muted user in the requested format when successful.</para>
        /// <para>Returns a string describing the failure condition when unsuccessful.</para>
        /// <para>Actions taken in this method are asynchronous and changes will be eventually consistent.</para>
        /// <para>Note: Providing either screen_name or user_id is required.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> screen_name (optional)</para>
        /// <para>- <c>long</c> user_id (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the user.</para>
        /// </returns>
        public Task<UserResponse> CreateAsync<T>(T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<UserResponse, T>(MethodType.Post, "mutes/users/create", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Un-mutes the user specified in the ID parameter for the authenticating user as an asynchronous operation.</para>
        /// <para>Returns the unmuted user in the requested format when successful.</para>
        /// <para>Returns a string describing the failure condition when unsuccessful.</para>
        /// <para>Actions taken in this method are asynchronous and changes will be eventually consistent.</para>
        /// <para>Note: Providing either screen_name or user_id is required.</para>
        /// <para>Available parameters: </para>
        /// <para>- <c>string</c> screen_name (optional)</para>
        /// <para>- <c>long</c> user_id (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the user.</para>
        /// </returns>
        public Task<UserResponse> DestroyAsync(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiAsync<UserResponse>(MethodType.Post, "mutes/users/destroy", parameters);
        }

        /// <summary>
        /// <para>Un-mutes the user specified in the ID parameter for the authenticating user as an asynchronous operation.</para>
        /// <para>Returns the unmuted user in the requested format when successful.</para>
        /// <para>Returns a string describing the failure condition when unsuccessful.</para>
        /// <para>Actions taken in this method are asynchronous and changes will be eventually consistent.</para>
        /// <para>Note: Providing either screen_name or user_id is required.</para>
        /// <para>Available parameters: </para>
        /// <para>- <c>string</c> screen_name (optional)</para>
        /// <para>- <c>long</c> user_id (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the user.</para>
        /// </returns>
        public Task<UserResponse> DestroyAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<UserResponse>(MethodType.Post, "mutes/users/destroy", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Un-mutes the user specified in the ID parameter for the authenticating user as an asynchronous operation.</para>
        /// <para>Returns the unmuted user in the requested format when successful.</para>
        /// <para>Returns a string describing the failure condition when unsuccessful.</para>
        /// <para>Actions taken in this method are asynchronous and changes will be eventually consistent.</para>
        /// <para>Note: Providing either screen_name or user_id is required.</para>
        /// <para>Available parameters: </para>
        /// <para>- <c>string</c> screen_name (optional)</para>
        /// <para>- <c>long</c> user_id (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the user.</para>
        /// </returns>
        public Task<UserResponse> DestroyAsync<T>(T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<UserResponse, T>(MethodType.Post, "mutes/users/destroy", parameters, cancellationToken);
        }
    }
}
#endif
