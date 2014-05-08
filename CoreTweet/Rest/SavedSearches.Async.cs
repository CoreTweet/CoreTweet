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
    partial class SavedSearches
    {
        //GET Methods

        /// <summary>
        /// <para>Returns the authenticated user's saved search queries.</para>
        /// <para>Avaliable parameters: Nothing. </para>
        /// </summary>
        /// <returns>Saved searches.</returns>
        /// <param name='tokens'>
        /// Tokens.
        /// </param>
        /// <param name='parameters'>
        /// Parameters.
        /// </param>
        public Task<ListedResponse<SearchQuery>> ListAsync(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiArrayAsync<SearchQuery>(MethodType.Get, "saved_searches/list", parameters);
        }
        public Task<ListedResponse<SearchQuery>> ListAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiArrayAsync<SearchQuery>(MethodType.Get, "saved_searches/list", parameters, cancellationToken);
        }
        public Task<ListedResponse<SearchQuery>> ListAsync<T>(T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiArrayAsync<SearchQuery, T>(MethodType.Get, "saved_searches/list", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Retrieve the information for the saved search represented by the given id. The authenticating user must be the owner of saved search ID being requested.</para>
        /// <para>Avaliable parameters: </para>
        /// <para><paramref name="long id (required)"/> : The ID of the saved search.</para>
        /// </summary>
        /// <returns>The saved search.</returns>
        /// <param name='tokens'>
        /// Tokens.
        /// </param>
        /// <param name='parameters'>
        /// Parameters.
        /// </param>
        public Task<SearchQueryResponse> ShowAsync(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessParameterReservedApiAsync<SearchQueryResponse>(MethodType.Get, "saved_searches/destroy/{0}", "id", InternalUtils.ExpressionsToDictionary(parameters), CancellationToken.None);
        }
        public Task<SearchQueryResponse> ShowAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessParameterReservedApiAsync<SearchQueryResponse>(MethodType.Get, "saved_searches/show/{id}", "id", parameters, cancellationToken);
        }
        public Task<SearchQueryResponse> ShowAsync<T>(T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessParameterReservedApiAsync<SearchQueryResponse>(MethodType.Get, "saved_searches/destroy/{0}", "id", InternalUtils.ResolveObject(parameters), cancellationToken);
        }

        //POST Methods

        /// <summary>
        /// <para>Create a new saved search for the authenticated user. A user may only have 25 saved searches.</para>
        /// <para>Avaliable parameters: </para>
        /// <para><paramref name="string query (required)"/> : The query of the search the user would like to save.</para>
        /// </summary>
        /// <returns>The saved search.</returns>
        /// <param name='tokens'>
        /// Tokens.
        /// </param>
        /// <param name='parameters'>
        /// Parameters.
        /// </param>
        public Task<SearchQuery> CreateAsync(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiAsync<SearchQuery>(MethodType.Post, "saved_searches/create", parameters);
        }
        public Task<SearchQuery> CreateAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<SearchQuery>(MethodType.Post, "saved_searches/create", parameters, cancellationToken);
        }
        public Task<SearchQuery> CreateAsync<T>(T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<SearchQuery, T>(MethodType.Post, "saved_searches/create", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Destroys a saved search for the authenticating user. The authenticating user must be the owner of saved search id being destroyed.</para>
        /// <para>Avaliable parameters: </para>
        /// <para><paramref name="long id (required)"/> : The ID of the saved search.</para>
        /// </summary>
        /// <returns>The saved search.</returns>
        /// <param name='tokens'>
        /// Tokens.
        /// </param>
        /// <param name='parameters'>
        /// Parameters.
        /// </param>
        public Task<SearchQuery> DestroyAsync(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessParameterReservedApiAsync<SearchQuery>(MethodType.Get, "saved_searches/destroy/{0}", "id", InternalUtils.ExpressionsToDictionary(parameters), CancellationToken.None);
        }
        public Task<SearchQuery> DestroyAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessParameterReservedApiAsync<SearchQuery>(MethodType.Get, "saved_searches/destroy/{0}", "id", parameters, cancellationToken);
        }
        public Task<SearchQuery> DestroyAsync<T>(T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessParameterReservedApiAsync<SearchQuery>(MethodType.Get, "saved_searches/destroy/{0}", "id", InternalUtils.ResolveObject(parameters), cancellationToken);
        }
    }
}
