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
    /// Provides a set of methods for the wrapper of GET trends.
    /// </summary>
    public partial class Trends : ApiProviderBase
    {
        internal Trends(TokensBase e) : base(e) { }

#if !(PCL || WIN_RT || WP)
        //GET Methods

        /// <summary>
        /// <para>Returns the locations that Twitter has trending topic information for.</para>
        /// <para>The response is an array of "locations" that encode the location's id and some other human-readable information such as a canonical name and country the location belongs in.</para>
        /// <para>A id is a Yahoo! Where On Earth ID.</para>
        /// <para>See also: http://developer.yahoo.com/geo/geoplanet/</para>
        /// <para>Available parameters: Nothing.</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The locations.</returns>
        public ListedResponse<TrendLocation> Available(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiArray<TrendLocation>(MethodType.Get, "trends/available", parameters);
        }

        /// <summary>
        /// <para>Returns the locations that Twitter has trending topic information for.</para>
        /// <para>The response is an array of "locations" that encode the location's id and some other human-readable information such as a canonical name and country the location belongs in.</para>
        /// <para>A id is a Yahoo! Where On Earth ID.</para>
        /// <para>See also: http://developer.yahoo.com/geo/geoplanet/</para>
        /// <para>Available parameters: Nothing.</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The locations.</returns>
        public ListedResponse<TrendLocation> Available(IDictionary<string, object> parameters)
        {
            return this.Tokens.AccessApiArray<TrendLocation>(MethodType.Get, "trends/available", parameters);
        }

        /// <summary>
        /// <para>Returns the locations that Twitter has trending topic information for.</para>
        /// <para>The response is an array of "locations" that encode the location's id and some other human-readable information such as a canonical name and country the location belongs in.</para>
        /// <para>A id is a Yahoo! Where On Earth ID.</para>
        /// <para>See also: http://developer.yahoo.com/geo/geoplanet/</para>
        /// <para>Available parameters: Nothing.</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The locations.</returns>
        public ListedResponse<TrendLocation> Available<T>(T parameters)
        {
            return this.Tokens.AccessApiArray<TrendLocation, T>(MethodType.Get, "trends/available", parameters);
        }

        /// <summary>
        /// <para>Returns the locations that Twitter has trending topic information for, closest to a specified location.</para>
        /// <para>The response is an array of "locations" that encode the location's ID and some other human-readable information such as a canonical name and country the location belongs in.</para>
        /// <para>A id is a Yahoo! Where On Earth ID.</para>
        /// <para>See also: http://developer.yahoo.com/geo/geoplanet/</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>double</c> lat (required)</para>
        /// <para>- <c>double</c> long (required)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The locations.</returns>
        public ListedResponse<TrendLocation> Closest(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiArray<TrendLocation>(MethodType.Get, "trends/closest", parameters);
        }

        /// <summary>
        /// <para>Returns the locations that Twitter has trending topic information for, closest to a specified location.</para>
        /// <para>The response is an array of "locations" that encode the location's ID and some other human-readable information such as a canonical name and country the location belongs in.</para>
        /// <para>A id is a Yahoo! Where On Earth ID.</para>
        /// <para>See also: http://developer.yahoo.com/geo/geoplanet/</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>double</c> lat (required)</para>
        /// <para>- <c>double</c> long (required)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The locations.</returns>
        public ListedResponse<TrendLocation> Closest(IDictionary<string, object> parameters)
        {
            return this.Tokens.AccessApiArray<TrendLocation>(MethodType.Get, "trends/closest", parameters);
        }

        /// <summary>
        /// <para>Returns the locations that Twitter has trending topic information for, closest to a specified location.</para>
        /// <para>The response is an array of "locations" that encode the location's ID and some other human-readable information such as a canonical name and country the location belongs in.</para>
        /// <para>A id is a Yahoo! Where On Earth ID.</para>
        /// <para>See also: http://developer.yahoo.com/geo/geoplanet/</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>double</c> lat (required)</para>
        /// <para>- <c>double</c> long (required)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The locations.</returns>
        public ListedResponse<TrendLocation> Closest<T>(T parameters)
        {
            return this.Tokens.AccessApiArray<TrendLocation, T>(MethodType.Get, "trends/closest", parameters);
        }

        /// <summary>
        /// <para>Returns the top 10 trending topics for a specific id, if trending information is available for it.</para>
        /// <para>The response is an array of "trend" objects that encode the name of the trending topic, the query parameter that can be used to search for the topic on Twitter Search, and the Twitter Search URL.</para>
        /// <para>This information is cached for 5 minutes.</para>
        /// <para>Requesting more frequently than that will not return any more data, and will count against your rate limit usage.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> id (required)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The queries.</returns>
        public ListedResponse<TrendsResult> Place(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiArray<TrendsResult>(MethodType.Get, "trends/place", parameters);
        }

        /// <summary>
        /// <para>Returns the top 10 trending topics for a specific id, if trending information is available for it.</para>
        /// <para>The response is an array of "trend" objects that encode the name of the trending topic, the query parameter that can be used to search for the topic on Twitter Search, and the Twitter Search URL.</para>
        /// <para>This information is cached for 5 minutes.</para>
        /// <para>Requesting more frequently than that will not return any more data, and will count against your rate limit usage.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> id (required)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The queries.</returns>
        public ListedResponse<TrendsResult> Place(IDictionary<string, object> parameters)
        {
            return this.Tokens.AccessApiArray<TrendsResult>(MethodType.Get, "trends/place", parameters);
        }

        /// <summary>
        /// <para>Returns the top 10 trending topics for a specific id, if trending information is available for it.</para>
        /// <para>The response is an array of "trend" objects that encode the name of the trending topic, the query parameter that can be used to search for the topic on Twitter Search, and the Twitter Search URL.</para>
        /// <para>This information is cached for 5 minutes.</para>
        /// <para>Requesting more frequently than that will not return any more data, and will count against your rate limit usage.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> id (required)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The queries.</returns>
        public ListedResponse<TrendsResult> Place<T>(T parameters)
        {
            return this.Tokens.AccessApiArray<TrendsResult, T>(MethodType.Get, "trends/place", parameters);
        }
#endif
    }
}
