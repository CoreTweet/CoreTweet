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
    /// Provides a set of methods for the wrapper of GET/POST saved_searches.
    /// </summary>
    public partial class SavedSearches : ApiProviderBase
    {
        internal SavedSearches(TokensBase e) : base(e) { }

#if !(PCL || WIN_RT || WP)
        //GET Methods

        /// <summary>
        /// <para>Returns the authenticated user's saved search queries.</para>
        /// <para>Available parameters: Nothing.</para>
        /// </summary>
        /// <param name="parameters">The Parameters.</param>
        /// <returns>The saved searches.</returns>
        public ListedResponse<SearchQuery> List(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiArray<SearchQuery>(MethodType.Get, "saved_searches/list", parameters);
        }

        /// <summary>
        /// <para>Returns the authenticated user's saved search queries.</para>
        /// <para>Available parameters: Nothing.</para>
        /// </summary>
        /// <param name="parameters">The Parameters.</param>
        /// <returns>The saved searches.</returns>
        public ListedResponse<SearchQuery> List(IDictionary<string, object> parameters)
        {
            return this.Tokens.AccessApiArray<SearchQuery>(MethodType.Get, "saved_searches/list", parameters);
        }

        /// <summary>
        /// <para>Returns the authenticated user's saved search queries.</para>
        /// <para>Available parameters: Nothing.</para>
        /// </summary>
        /// <param name="parameters">The Parameters.</param>
        /// <returns>The saved searches.</returns>
        public ListedResponse<SearchQuery> List<T>(T parameters)
        {
            return this.Tokens.AccessApiArray<SearchQuery, T>(MethodType.Get, "saved_searches/list", parameters);
        }

        /// <summary>
        /// <para>Retrieve the information for the saved search represented by the given id.</para>
        /// <para>The authenticating user must be the owner of saved search ID being requested.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> id (required)</para>
        /// </summary>
        /// <param name="parameters">The Parameters.</param>
        /// <returns>The saved search.</returns>
        public SearchQueryResponse Show(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessParameterReservedApi<SearchQueryResponse>(MethodType.Get, "saved_searches/show/{id}", "id", InternalUtils.ExpressionsToDictionary(parameters));
        }

        /// <summary>
        /// <para>Retrieve the information for the saved search represented by the given id.</para>
        /// <para>The authenticating user must be the owner of saved search ID being requested.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> id (required)</para>
        /// </summary>
        /// <param name="parameters">The Parameters.</param>
        /// <returns>The saved search.</returns>
        public SearchQueryResponse Show(IDictionary<string, object> parameters)
        {
            return this.Tokens.AccessParameterReservedApi<SearchQueryResponse>(MethodType.Get, "saved_searches/show/{id}", "id", parameters);
        }

        /// <summary>
        /// <para>Retrieve the information for the saved search represented by the given id.</para>
        /// <para>The authenticating user must be the owner of saved search ID being requested.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> id (required)</para>
        /// </summary>
        /// <param name="parameters">The Parameters.</param>
        /// <returns>The saved search.</returns>
        public SearchQueryResponse Show<T>(T parameters)
        {
            return this.Tokens.AccessParameterReservedApi<SearchQueryResponse>(MethodType.Get, "saved_searches/show/{id}", "id", InternalUtils.ResolveObject(parameters));
        }

        //POST Methods

        /// <summary>
        /// <para>Create a new saved search for the authenticated user.</para>
        /// <para>A user may only have 25 saved searches.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> query (required)</para>
        /// </summary>
        /// <param name="parameters">The Parameters.</param>
        /// <returns>The saved search.</returns>
        public SearchQueryResponse Create(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApi<SearchQueryResponse>(MethodType.Post, "saved_searches/create", parameters);
        }

        /// <summary>
        /// <para>Create a new saved search for the authenticated user.</para>
        /// <para>A user may only have 25 saved searches.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> query (required)</para>
        /// </summary>
        /// <param name="parameters">The Parameters.</param>
        /// <returns>The saved search.</returns>
        public SearchQueryResponse Create(IDictionary<string, object> parameters)
        {
            return this.Tokens.AccessApi<SearchQueryResponse>(MethodType.Post, "saved_searches/create", parameters);
        }

        /// <summary>
        /// <para>Create a new saved search for the authenticated user.</para>
        /// <para>A user may only have 25 saved searches.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> query (required)</para>
        /// </summary>
        /// <param name="parameters">The Parameters.</param>
        /// <returns>The saved search.</returns>
        public SearchQueryResponse Create<T>(T parameters)
        {
            return this.Tokens.AccessApi<SearchQueryResponse, T>(MethodType.Post, "saved_searches/create", parameters);
        }

        /// <summary>
        /// <para>Destroys a saved search for the authenticating user.</para>
        /// <para>The authenticating user must be the owner of saved search id being destroyed.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> id (required)</para>
        /// </summary>
        /// <param name="parameters">The Parameters.</param>
        /// <returns>The saved search.</returns>
        public SearchQueryResponse Destroy(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessParameterReservedApi<SearchQueryResponse>(MethodType.Get, "saved_searches/destroy/{0}", "id", InternalUtils.ExpressionsToDictionary(parameters));
        }

        /// <summary>
        /// <para>Destroys a saved search for the authenticating user.</para>
        /// <para>The authenticating user must be the owner of saved search id being destroyed.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> id (required)</para>
        /// </summary>
        /// <param name="parameters">The Parameters.</param>
        /// <returns>The saved search.</returns>
        public SearchQueryResponse Destroy(IDictionary<string, object> parameters)
        {
            return this.Tokens.AccessParameterReservedApi<SearchQueryResponse>(MethodType.Get, "saved_searches/destroy/{0}", "id", parameters);
        }

        /// <summary>
        /// <para>Destroys a saved search for the authenticating user.</para>
        /// <para>The authenticating user must be the owner of saved search id being destroyed.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> id (required)</para>
        /// </summary>
        /// <param name="parameters">The Parameters.</param>
        /// <returns>The saved search.</returns>
        public SearchQueryResponse Destroy<T>(T parameters)
        {
            return this.Tokens.AccessParameterReservedApi<SearchQueryResponse>(MethodType.Get, "saved_searches/destroy/{0}", "id", InternalUtils.ResolveObject(parameters));
        }
#endif
    }
}
