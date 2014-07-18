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
    partial class Blocks
    {
        //GET Methods

        /// <summary>
        /// <para>Returns an array of numeric user ids the authenticating user is blocking as an asynchronous operation.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> cursor (semi-optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the IDs.</para>
        /// </returns>
        public Task<Cursored<long>> IdsAsync(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiAsync<Cursored<long>>(MethodType.Get, "blocks/ids", parameters);
        }

        /// <summary>
        /// <para>Returns an array of numeric user ids the authenticating user is blocking as an asynchronous operation.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> cursor (semi-optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the IDs.</para>
        /// </returns>
        public Task<Cursored<long>> IdsAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<Cursored<long>>(MethodType.Get, "blocks/ids", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Returns an array of numeric user ids the authenticating user is blocking as an asynchronous operation.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> cursor (semi-optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the IDs.</para>
        /// </returns>
        public Task<Cursored<long>> IdsAsync<T>(T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<Cursored<long>, T>(MethodType.Get, "blocks/ids", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Returns a collection of user objects that the authenticating user is blocking as an asynchronous operation.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// <para>- <c>bool</c> skip_status (optional)</para>
        /// <para>- <c>long</c> cursor (semi-optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the users.</para>
        /// </returns>
        public Task<Cursored<User>> ListAsync(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiAsync<Cursored<User>>(MethodType.Get, "blocks/list", parameters);
        }

        /// <summary>
        /// <para>Returns a collection of user objects that the authenticating user is blocking as an asynchronous operation.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// <para>- <c>bool</c> skip_status (optional)</para>
        /// <para>- <c>long</c> cursor (semi-optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the users.</para>
        /// </returns>
        public Task<Cursored<User>> ListAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<Cursored<User>>(MethodType.Get, "blocks/list", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Returns a collection of user objects that the authenticating user is blocking as an asynchronous operation.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// <para>- <c>bool</c> skip_status (optional)</para>
        /// <para>- <c>long</c> cursor (semi-optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the users.</para>
        /// </returns>
        public Task<Cursored<User>> ListAsync<T>(T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<Cursored<User>, T>(MethodType.Get, "blocks/list", parameters, cancellationToken);
        }

        //POST Methods

        /// <summary>
        /// <para>Blocks the specified user from following the authenticating user as an asynchronous operation.</para>
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
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the users.</para>
        /// </returns>
        public Task<UserResponse> CreateAsync(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiAsync<UserResponse>(MethodType.Post, "blocks/create", parameters);
        }

        /// <summary>
        /// <para>Blocks the specified user from following the authenticating user as an asynchronous operation.</para>
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
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the users.</para>
        /// </returns>
        public Task<UserResponse> CreateAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<UserResponse>(MethodType.Post, "blocks/create", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Blocks the specified user from following the authenticating user as an asynchronous operation.</para>
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
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the users.</para>
        /// </returns>
        public Task<UserResponse> CreateAsync<T>(T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<UserResponse, T>(MethodType.Post, "blocks/create", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Un-blocks the user specified in the ID parameter for the authenticating user as an asynchronous operation.</para>
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
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the users.</para>
        /// </returns>
        public Task<UserResponse> DestroyAsync(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiAsync<UserResponse>(MethodType.Post, "blocks/destroy", parameters);
        }

        /// <summary>
        /// <para>Un-blocks the user specified in the ID parameter for the authenticating user as an asynchronous operation.</para>
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
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the users.</para>
        /// </returns>
        public Task<UserResponse> DestroyAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<UserResponse>(MethodType.Post, "blocks/destroy", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Un-blocks the user specified in the ID parameter for the authenticating user as an asynchronous operation.</para>
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
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the users.</para>
        /// </returns>
        public Task<UserResponse> DestroyAsync<T>(T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<UserResponse, T>(MethodType.Post, "blocks/destroy", parameters, cancellationToken);
        }
    }
}
