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
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using CoreTweet.Core;

namespace CoreTweet.Rest
{

    
    /// <summary>
    ///  GET/POST saved_searches
    /// </summary>
    public class SavedSearches : TokenIncluded
    {
        internal SavedSearches(Tokens e) : base(e) { }
            
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
        public IEnumerable<SearchQuery> List(params Expression<Func<string,object>>[] parameters)
        {
            return this.Tokens.AccessApiArray<SearchQuery>(MethodType.Get, "saved_searches/list", parameters);
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
        public SearchQuery Show(params Expression<Func<string,object>>[] parameters)
        {
            return this.Tokens.AccessApi<SearchQuery>(MethodType.Get, string.Format("saved_searches/show/{0}", 
                    parameters.First(x => x.Parameters[0].Name == "id").Compile()("").ToString()), 
                         parameters.Where(x => x.Parameters[0].Name != "id").ToArray());
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
        public SearchQuery Create(params Expression<Func<string,object>>[] parameters)
        {
            return this.Tokens.AccessApi<SearchQuery>(MethodType.Post, "saved_searches/create", parameters);
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
        public SearchQuery Destroy(params Expression<Func<string,object>>[] parameters)
        {
            return this.Tokens.AccessApi<SearchQuery>(MethodType.Post, string.Format("saved_searches/destroy/{0}", 
                    parameters.First(x => x.Parameters[0].Name == "id").Compile()("").ToString()),
                         parameters.Where(x => x.Parameters[0].Name != "id").ToArray());
        }
    }    
}
