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
using CoreTweet.Core;

namespace CoreTweet.Rest
{
    partial class Geo
    {
        //GET Methods

        /// <summary>
        /// <para>Returns all the information about a known place as an asynchronous operation.</para>
        /// <para>Available parameters: </para>
        /// <para>- <c>string</c> place_id (required)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the geo.</para>
        /// </returns>
        public Task<PlaceResponse> IdAsync(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessParameterReservedApiAsync<PlaceResponse>(MethodType.Get, "geo/id/{place_id}", "place_id", InternalUtils.ExpressionsToDictionary(parameters), CancellationToken.None);
        }

        /// <summary>
        /// <para>Returns all the information about a known place as an asynchronous operation.</para>
        /// <para>Available parameters: </para>
        /// <para>- <c>string</c> place_id (required)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the geo.</para>
        /// </returns>
        public Task<PlaceResponse> IdAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessParameterReservedApiAsync<PlaceResponse>(MethodType.Get, "geo/id/{place_id}", "place_id", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Returns all the information about a known place as an asynchronous operation.</para>
        /// <para>Available parameters: </para>
        /// <para>- <c>string</c> place_id (required)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the geo.</para>
        /// </returns>
        public Task<PlaceResponse> IdAsync<T>(T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessParameterReservedApiAsync<PlaceResponse>(MethodType.Get, "geo/id/{place_id}", "place_id", InternalUtils.ResolveObject(parameters), cancellationToken);
        }

        /// <summary>
        /// <para>Locates places near the given coordinates which are similar in name as an asynchronous operation.</para>
        /// <para>Conceptually you would use this method to get a list of known places to choose from first.</para>
        /// <para>Then, if the desired place doesn't exist, make a request to POST geo/place to create a new one.</para>
        /// <para>The token contained in the response is the token needed to be able to create a new place.</para>
        /// <para>Available parameters: </para>
        /// <para>- <c>double</c> lat (required)</para>
        /// <para>- <c>double</c> long (required)</para>
        /// <para>- <c>string</c> contained_within (optional)</para>
        /// <para>- <c>string</c> street_address (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the places and the token.</para>
        /// </returns>
        public Task<GeoResult> SimilarPlacesAsync(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiAsync<GeoResult>(MethodType.Get, "geo/similar_places", parameters, "result");
        }

        /// <summary>
        /// <para>Locates places near the given coordinates which are similar in name as an asynchronous operation.</para>
        /// <para>Conceptually you would use this method to get a list of known places to choose from first.</para>
        /// <para>Then, if the desired place doesn't exist, make a request to POST geo/place to create a new one.</para>
        /// <para>The token contained in the response is the token needed to be able to create a new place.</para>
        /// <para>Available parameters: </para>
        /// <para>- <c>double</c> lat (required)</para>
        /// <para>- <c>double</c> long (required)</para>
        /// <para>- <c>string</c> contained_within (optional)</para>
        /// <para>- <c>string</c> street_address (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the places and the token.</para>
        /// </returns>
        public Task<GeoResult> SimilarPlacesAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<GeoResult>(MethodType.Get, "geo/similar_places", parameters, cancellationToken, "result");
        }

        /// <summary>
        /// <para>Locates places near the given coordinates which are similar in name as an asynchronous operation.</para>
        /// <para>Conceptually you would use this method to get a list of known places to choose from first.</para>
        /// <para>Then, if the desired place doesn't exist, make a request to POST geo/place to create a new one.</para>
        /// <para>The token contained in the response is the token needed to be able to create a new place.</para>
        /// <para>Available parameters: </para>
        /// <para>- <c>double</c> lat (required)</para>
        /// <para>- <c>double</c> long (required)</para>
        /// <para>- <c>string</c> contained_within (optional)</para>
        /// <para>- <c>string</c> street_address (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the places and the token.</para>
        /// </returns>
        public Task<GeoResult> SimilarPlacesAsync<T>(T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<GeoResult, T>(MethodType.Get, "geo/similar_places", parameters, cancellationToken, "result");
        }

        /// <summary>
        /// <para>Search for places that can be attached to a statuses/update as an asynchronous operation.</para>
        /// <para>Given a latitude and a longitude pair, an IP address, or a name, this request will return a list of all the valid places that can be used as the place_id when updating a status.</para>
        /// <para>Conceptually, a query can be made from the user's location, retrieve a list of places, have the user validate the location he or she is at, and then send the ID of this location with a call to POST statuses/update.</para>
        /// <para>This is the recommended method to use find places that can be attached to statuses/update.</para>
        /// <para>Unlike GET geo/reverse_geocode which provides raw data access, this endpoint can potentially re-order places with regards to the user who is authenticated.</para>
        /// <para>This approach is also preferred for interactive place matching with the user.</para>
        /// <para>Note: At least one of the following parameters must be provided to this resource: lat, long, ip, or query</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>double</c> lat (optional)</para>
        /// <para>- <c>double</c> long (optional)</para>
        /// <para>- <c>string</c> query (optional)</para>
        /// <para>- <c>string</c> ip (optional)</para>
        /// <para>- <c>string</c> granularity (optional)</para>
        /// <para>- <c>string</c> accuracy (optional)</para>
        /// <para>- <c>int</c> max_results (optional)</para>
        /// <para>- <c>string</c> contained_within (optional)</para>
        /// <para>- <c>string</c> attribute:street_address (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the places.</para>
        /// </returns>
        public Task<GeoResult> SearchAsync(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiAsync<GeoResult>(MethodType.Get, "geo/search", parameters, "result");
        }

        /// <summary>
        /// <para>Search for places that can be attached to a statuses/update as an asynchronous operation.</para>
        /// <para>Given a latitude and a longitude pair, an IP address, or a name, this request will return a list of all the valid places that can be used as the place_id when updating a status.</para>
        /// <para>Conceptually, a query can be made from the user's location, retrieve a list of places, have the user validate the location he or she is at, and then send the ID of this location with a call to POST statuses/update.</para>
        /// <para>This is the recommended method to use find places that can be attached to statuses/update.</para>
        /// <para>Unlike GET geo/reverse_geocode which provides raw data access, this endpoint can potentially re-order places with regards to the user who is authenticated.</para>
        /// <para>This approach is also preferred for interactive place matching with the user.</para>
        /// <para>Note: At least one of the following parameters must be provided to this resource: lat, long, ip, or query</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>double</c> lat (optional)</para>
        /// <para>- <c>double</c> long (optional)</para>
        /// <para>- <c>string</c> query (optional)</para>
        /// <para>- <c>string</c> ip (optional)</para>
        /// <para>- <c>string</c> granularity (optional)</para>
        /// <para>- <c>string</c> accuracy (optional)</para>
        /// <para>- <c>int</c> max_results (optional)</para>
        /// <para>- <c>string</c> contained_within (optional)</para>
        /// <para>- <c>string</c> attribute:street_address (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the places.</para>
        /// </returns>
        public Task<GeoResult> SearchAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<GeoResult>(MethodType.Get, "geo/search", parameters, cancellationToken, "result");
        }

        /// <summary>
        /// <para>Search for places that can be attached to a statuses/update as an asynchronous operation.</para>
        /// <para>Given a latitude and a longitude pair, an IP address, or a name, this request will return a list of all the valid places that can be used as the place_id when updating a status.</para>
        /// <para>Conceptually, a query can be made from the user's location, retrieve a list of places, have the user validate the location he or she is at, and then send the ID of this location with a call to POST statuses/update.</para>
        /// <para>This is the recommended method to use find places that can be attached to statuses/update.</para>
        /// <para>Unlike GET geo/reverse_geocode which provides raw data access, this endpoint can potentially re-order places with regards to the user who is authenticated.</para>
        /// <para>This approach is also preferred for interactive place matching with the user.</para>
        /// <para>Note: At least one of the following parameters must be provided to this resource: lat, long, ip, or query</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>double</c> lat (optional)</para>
        /// <para>- <c>double</c> long (optional)</para>
        /// <para>- <c>string</c> query (optional)</para>
        /// <para>- <c>string</c> ip (optional)</para>
        /// <para>- <c>string</c> granularity (optional)</para>
        /// <para>- <c>string</c> accuracy (optional)</para>
        /// <para>- <c>int</c> max_results (optional)</para>
        /// <para>- <c>string</c> contained_within (optional)</para>
        /// <para>- <c>string</c> attribute:street_address (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the places.</para>
        /// </returns>
        public Task<GeoResult> SearchAsync<T>(T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<GeoResult, T>(MethodType.Get, "geo/search", parameters, cancellationToken, "result");
        }

        /// <summary>
        /// <para>Given a latitude and a longitude, searches for up to 20 places that can be used as a place_id when updating a status as an asynchronous operation.</para>
        /// <para>This request is an informative call and will deliver generalized results about geography.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>double</c> lat (required)</para>
        /// <para>- <c>double</c> long (required)</para>
        /// <para>- <c>string</c> accuracy (optional)</para>
        /// <para>- <c>string</c> granularity (optional)</para>
        /// <para>- <c>int</c> max_results (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the places.</para>
        /// </returns>
        public Task<GeoResult> ReverseGeocodeAsync(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiAsync<GeoResult>(MethodType.Get, "geo/reverse_geocode", parameters, "result");
        }

        /// <summary>
        /// <para>Given a latitude and a longitude, searches for up to 20 places that can be used as a place_id when updating a status as an asynchronous operation.</para>
        /// <para>This request is an informative call and will deliver generalized results about geography.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>double</c> lat (required)</para>
        /// <para>- <c>double</c> long (required)</para>
        /// <para>- <c>string</c> accuracy (optional)</para>
        /// <para>- <c>string</c> granularity (optional)</para>
        /// <para>- <c>int</c> max_results (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the places.</para>
        /// </returns>
        public Task<GeoResult> ReverseGeocodeAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<GeoResult>(MethodType.Get, "geo/reverse_geocode", parameters, cancellationToken, "result");
        }

        /// <summary>
        /// <para>Given a latitude and a longitude, searches for up to 20 places that can be used as a place_id when updating a status as an asynchronous operation.</para>
        /// <para>This request is an informative call and will deliver generalized results about geography.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>double</c> lat (required)</para>
        /// <para>- <c>double</c> long (required)</para>
        /// <para>- <c>string</c> accuracy (optional)</para>
        /// <para>- <c>string</c> granularity (optional)</para>
        /// <para>- <c>int</c> max_results (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the places.</para>
        /// </returns>
        public Task<GeoResult> ReverseGeocodeAsync<T>(T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<GeoResult, T>(MethodType.Get, "geo/reverse_geocode", parameters, cancellationToken, "result");
        }
    }
}
#endif
