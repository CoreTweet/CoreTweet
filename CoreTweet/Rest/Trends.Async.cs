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
    partial class Trends
    {
        //GET Methods

        /// <summary>
        /// <para>Returns the locations that Twitter has trending topic information for as an asynchronous operation.</para>
        /// <para>The response is an array of "locations" that encode the location's id and some other human-readable information such as a canonical name and country the location belongs in.</para>
        /// <para>A id is a Yahoo! Where On Earth ID.</para>
        /// <para>See also: http://developer.yahoo.com/geo/geoplanet/</para>
        /// <para>Available parameters: Nothing.</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the locations.</para>
        /// </returns>
        public Task<ListedResponse<TrendLocation>> AvailableAsync(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiArrayAsync<TrendLocation>(MethodType.Get, "trends/available", parameters);
        }

        /// <summary>
        /// <para>Returns the locations that Twitter has trending topic information for as an asynchronous operation.</para>
        /// <para>The response is an array of "locations" that encode the location's id and some other human-readable information such as a canonical name and country the location belongs in.</para>
        /// <para>A id is a Yahoo! Where On Earth ID.</para>
        /// <para>See also: http://developer.yahoo.com/geo/geoplanet/</para>
        /// <para>Available parameters: Nothing.</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the locations.</para>
        /// </returns>
        public Task<ListedResponse<TrendLocation>> AvailableAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiArrayAsync<TrendLocation>(MethodType.Get, "trends/available", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Returns the locations that Twitter has trending topic information for as an asynchronous operation.</para>
        /// <para>The response is an array of "locations" that encode the location's id and some other human-readable information such as a canonical name and country the location belongs in.</para>
        /// <para>A id is a Yahoo! Where On Earth ID.</para>
        /// <para>See also: http://developer.yahoo.com/geo/geoplanet/</para>
        /// <para>Available parameters: Nothing.</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the locations.</para>
        /// </returns>
        public Task<ListedResponse<TrendLocation>> AvailableAsync<T>(T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiArrayAsync<TrendLocation, T>(MethodType.Get, "trends/available", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Returns the locations that Twitter has trending topic information for, closest to a specified location as an asynchronous operation.</para>
        /// <para>The response is an array of "locations" that encode the location's ID and some other human-readable information such as a canonical name and country the location belongs in.</para>
        /// <para>A id is a Yahoo! Where On Earth ID.</para>
        /// <para>See also: http://developer.yahoo.com/geo/geoplanet/</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>double</c> lat (required)</para>
        /// <para>- <c>double</c> long (required)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the locations.</para>
        /// </returns>
        public Task<ListedResponse<TrendLocation>> ClosestAsync(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiArrayAsync<TrendLocation>(MethodType.Get, "trends/closest", parameters);
        }

        /// <summary>
        /// <para>Returns the locations that Twitter has trending topic information for, closest to a specified location as an asynchronous operation.</para>
        /// <para>The response is an array of "locations" that encode the location's ID and some other human-readable information such as a canonical name and country the location belongs in.</para>
        /// <para>A id is a Yahoo! Where On Earth ID.</para>
        /// <para>See also: http://developer.yahoo.com/geo/geoplanet/</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>double</c> lat (required)</para>
        /// <para>- <c>double</c> long (required)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the locations.</para>
        /// </returns>
        public Task<ListedResponse<TrendLocation>> ClosestAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiArrayAsync<TrendLocation>(MethodType.Get, "trends/closest", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Returns the locations that Twitter has trending topic information for, closest to a specified location as an asynchronous operation.</para>
        /// <para>The response is an array of "locations" that encode the location's ID and some other human-readable information such as a canonical name and country the location belongs in.</para>
        /// <para>A id is a Yahoo! Where On Earth ID.</para>
        /// <para>See also: http://developer.yahoo.com/geo/geoplanet/</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>double</c> lat (required)</para>
        /// <para>- <c>double</c> long (required)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the locations.</para>
        /// </returns>
        public Task<ListedResponse<TrendLocation>> ClosestAsync<T>(T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiArrayAsync<TrendLocation, T>(MethodType.Get, "trends/closest", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Returns the top 10 trending topics for a specific id, if trending information is available for it as an asynchronous operation.</para>
        /// <para>The response is an array of "trend" objects that encode the name of the trending topic, the query parameter that can be used to search for the topic on Twitter Search, and the Twitter Search URL.</para>
        /// <para>This information is cached for 5 minutes.</para>
        /// <para>Requesting more frequently than that will not return any more data, and will count against your rate limit usage.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> id (required)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the queries.</para>
        /// </returns>
        public Task<ListedResponse<TrendsResult>> PlaceAsync(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiArrayAsync<TrendsResult>(MethodType.Get, "trends/place", parameters);
        }

        /// <summary>
        /// <para>Returns the top 10 trending topics for a specific id, if trending information is available for it as an asynchronous operation.</para>
        /// <para>The response is an array of "trend" objects that encode the name of the trending topic, the query parameter that can be used to search for the topic on Twitter Search, and the Twitter Search URL.</para>
        /// <para>This information is cached for 5 minutes.</para>
        /// <para>Requesting more frequently than that will not return any more data, and will count against your rate limit usage.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> id (required)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the queries.</para>
        /// </returns>
        public Task<ListedResponse<TrendsResult>> PlaceAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiArrayAsync<TrendsResult>(MethodType.Get, "trends/place", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Returns the top 10 trending topics for a specific id, if trending information is available for it as an asynchronous operation.</para>
        /// <para>The response is an array of "trend" objects that encode the name of the trending topic, the query parameter that can be used to search for the topic on Twitter Search, and the Twitter Search URL.</para>
        /// <para>This information is cached for 5 minutes.</para>
        /// <para>Requesting more frequently than that will not return any more data, and will count against your rate limit usage.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> id (required)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the queries.</para>
        /// </returns>
        public Task<ListedResponse<TrendsResult>> PlaceAsync<T>(T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiArrayAsync<TrendsResult, T>(MethodType.Get, "trends/place", parameters, cancellationToken);
        }
    }
}
