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

    ///<summary>GET/POST favorites</summary>
    public class Favorites : TokenIncluded
    {
        internal Favorites(Tokens e) : base(e) { }
            
            
        //GET Method
            
        /// <summary>
        /// <para>Returns the 20 most recent Tweets favorited by the authenticating or specified user.</para>
        /// <para>Avaliable parameters: </para>
        /// <para><paramref name="long id (optional)"/> : The ID of the user for whom to return results for.</para>
        /// <para><paramref name="string screen_name (optonal)"/> : The screen name of the user for whom to return results for.</para>
        /// <para><paramref name="int count (optional)"/> : Specifies the number of records to retrieve. Must be less than or equal to 200. Defaults to 20.</para>
        /// <para><paramref name="int since_id (optional)"/> : Returns results with an ID greater than (that is, more recent than) the specified ID. There are limits to the number of Tweets which can be accessed through the API. If the limit of Tweets has occured since the since_id, the since_id will be forced to the oldest ID available.</para>
        /// <para><paramref name="int max_id (optional)"/> : Returns results with an ID less than (that is, older than) or equal to the specified ID.</para>
        /// <para><paramref name="bool include_entities (optional)"/> : The entities node will be omitted when set to false.</para>
        /// </summary>
        /// <returns>The statuses.</returns>
        /// <param name='parameters'>
        /// Parameters.
        /// </param>
        public IEnumerable<Status> List(params Expression<Func<string,object>>[] parameters)
        {
            return this.Tokens.AccessApiArray<Status>(MethodType.Get, "favorites/list", parameters);
        }  
            
        //POST Methods
            
        /// <summary>
        /// <para>Favorites the status specified in the ID parameter as the authenticating user. Returns the favorite status when successful.</para>
        /// <para>Avaliable parameters: </para>
        /// <para><paramref name="long id (required)"/> : The numerical ID of the desired status.</para>
        /// <para><paramref name="bool include_entities (optional)"/> : The entities node will be omitted when set to false.</para>
        /// </summary>
        /// <returns>The favorited status.</returns>
        /// <param name='parameters'>
        /// Parameters.
        /// </param>
        public Status Create(params Expression<Func<string,object>>[] parameters)
        {
            return this.Tokens.AccessApi<Status>(MethodType.Post, "favorites/create", parameters);
        }
            
        /// <summary>
        /// <para>Un-favorites the status specified in the ID parameter as the authenticating user. Returns the un-favorited status in the requested format when successful.</para>
        /// <para>This process invoked by this method is asynchronous. The immediately returned status may not indicate the resultant favorited status of the tweet. A 200 OK response from this method will indicate whether the intended action was successful or not.</para>
        /// <para>Avaliable parameters: </para>
        /// <para><paramref name="long id (required)"/> : The numerical ID of the desired status.</para>
        /// <para><paramref name="bool include_entities (ooptional)"/> : The entities node will be omitted when set to false.</para>
        /// </summary>
        /// <returns>The destroied status.</returns>

        /// <param name='parameters'>
        /// Parameters.
        /// </param>
        public Status Destroy(params Expression<Func<string,object>>[] parameters)
        {
            return this.Tokens.AccessApi<Status>(MethodType.Post, "favorites/destroy", parameters);
        }
    }
}
