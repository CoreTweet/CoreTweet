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
    /// Provides a set of methods for the wrapper of GET/POST geo.
    /// </summary>
    public partial class Geo : ApiProviderBase
    {
        internal Geo(TokensBase e) : base(e) { }
        //FIXME: The format of "attribute:street_address" isn't known. Needed to check the format by "OAuth tool".

#if !(PCL || WIN_RT || WP)
        //GET Methods

        /// <summary>
        /// <para>Returns all the information about a known place.</para>
        /// <para>Available parameters: </para>
        /// <para>- <c>string</c> place_id (required)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The geo.</returns>
        public PlaceResponse Id(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessParameterReservedApi<PlaceResponse>(MethodType.Get, "geo/id/{place_id}", "place_id", InternalUtils.ExpressionsToDictionary(parameters));
        }

        /// <summary>
        /// <para>Returns all the information about a known place.</para>
        /// <para>Available parameters: </para>
        /// <para>- <c>string</c> place_id (required)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The geo.</returns>
        public PlaceResponse Id(IDictionary<string, object> parameters)
        {
            return this.Tokens.AccessParameterReservedApi<PlaceResponse>(MethodType.Get, "geo/id/{place_id}", "place_id", parameters);
        }

        /// <summary>
        /// <para>Returns all the information about a known place.</para>
        /// <para>Available parameters: </para>
        /// <para>- <c>string</c> place_id (required)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The geo.</returns>
        public PlaceResponse Id<T>(T parameters)
        {
            return this.Tokens.AccessParameterReservedApi<PlaceResponse>(MethodType.Get, "geo/id/{place_id}", "place_id", InternalUtils.ResolveObject(parameters));
        }

        /// <summary>
        /// <para>Locates places near the given coordinates which are similar in name.</para>
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
        /// <returns>Places and the token.</returns>
        public GeoResult SimilarPlaces(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApi<GeoResult>(MethodType.Get, "geo/similar_places", parameters, "result");
        }

        /// <summary>
        /// <para>Locates places near the given coordinates which are similar in name.</para>
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
        /// <returns>Places and the token.</returns>
        public GeoResult SimilarPlaces(IDictionary<string, object> parameters)
        {
            return this.Tokens.AccessApi<GeoResult>(MethodType.Get, "geo/similar_places", parameters, "result");
        }

        /// <summary>
        /// <para>Locates places near the given coordinates which are similar in name.</para>
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
        /// <returns>Places and the token.</returns>
        public GeoResult SimilarPlaces<T>(T parameters)
        {
            return this.Tokens.AccessApi<GeoResult, T>(MethodType.Get, "geo/similar_places", parameters, "result");
        }

        /// <summary>
        /// <para>Search for places that can be attached to a statuses/update.</para>
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
        /// <returns>Places.</returns>
        public GeoResult Search(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApi<GeoResult>(MethodType.Get, "geo/search", parameters, "result");
        }

        /// <summary>
        /// <para>Search for places that can be attached to a statuses/update.</para>
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
        /// <returns>Places.</returns>
        public GeoResult Search(IDictionary<string, object> parameters)
        {
            return this.Tokens.AccessApi<GeoResult>(MethodType.Get, "geo/search", parameters, "result");
        }

        /// <summary>
        /// <para>Search for places that can be attached to a statuses/update.</para>
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
        /// <returns>Places.</returns>
        public GeoResult Search<T>(T parameters)
        {
            return this.Tokens.AccessApi<GeoResult, T>(MethodType.Get, "geo/search", parameters, "result");
        }

        /// <summary>
        /// <para>Given a latitude and a longitude, searches for up to 20 places that can be used as a place_id when updating a status.</para>
        /// <para>This request is an informative call and will deliver generalized results about geography.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>double</c> lat (required)</para>
        /// <para>- <c>double</c> long (required)</para>
        /// <para>- <c>string</c> accuracy (optional)</para>
        /// <para>- <c>string</c> granularity (optional)</para>
        /// <para>- <c>int</c> max_results (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>Places.</returns>
        public GeoResult ReverseGeocode(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApi<GeoResult>(MethodType.Get, "geo/reverse_geocode", parameters, "result");
        }

        /// <summary>
        /// <para>Given a latitude and a longitude, searches for up to 20 places that can be used as a place_id when updating a status.</para>
        /// <para>This request is an informative call and will deliver generalized results about geography.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>double</c> lat (required)</para>
        /// <para>- <c>double</c> long (required)</para>
        /// <para>- <c>string</c> accuracy (optional)</para>
        /// <para>- <c>string</c> granularity (optional)</para>
        /// <para>- <c>int</c> max_results (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>Places.</returns>
        public GeoResult ReverseGeocode(IDictionary<string, object> parameters)
        {
            return this.Tokens.AccessApi<GeoResult>(MethodType.Get, "geo/reverse_geocode", parameters, "result");
        }

        /// <summary>
        /// <para>Given a latitude and a longitude, searches for up to 20 places that can be used as a place_id when updating a status.</para>
        /// <para>This request is an informative call and will deliver generalized results about geography.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>double</c> lat (required)</para>
        /// <para>- <c>double</c> long (required)</para>
        /// <para>- <c>string</c> accuracy (optional)</para>
        /// <para>- <c>string</c> granularity (optional)</para>
        /// <para>- <c>int</c> max_results (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>Places.</returns>
        public GeoResult ReverseGeocode<T>(T parameters)
        {
            return this.Tokens.AccessApi<GeoResult, T>(MethodType.Get, "geo/reverse_geocode", parameters, "result");
        }
#endif
    }
}
