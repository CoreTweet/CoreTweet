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
using System.Linq;
using CoreTweet.Core;
using Newtonsoft.Json;

namespace CoreTweet
{
    /// <summary>
    /// Places are specific, named locations with corresponding geo coordinates. 
    /// They can be attached to Tweets by specifying a place_id when tweeting. 
    /// Tweets associated with places are not necessarily issued from that location but could also potentially be about that location. 
    /// Places can be searched for. Tweets can also be found by place_id. 
    /// See About Geo Place Attributes for more information.
    /// </summary>
    public class Place : CoreBase
    {
        /// <summary>
        ///     Contains a hash of variant information about the place. 
        /// </summary>
        /// <see cref="https://dev.twitter.com/docs/about-geo-place-attributes"/>
        [JsonProperty("attributes")]
        public GeoAttributes Attributes { get; set; }

        /// <summary>
        ///     A bounding box of coordinates which encloses this place.
        /// </summary>
        [JsonProperty("bounding_box")]
        public BoundingBox BoundingBox { get; set; }

        /// <summary>
        ///     Name of the country containing this place.The country.
        /// </summary>
        [JsonProperty("country")]
        public string Country { get; set; }

        /// <summary>
        ///     Shortened country code representing the country containing this place.
        /// </summary>
        [JsonProperty("country_code")]
        public string CountryCode { get; set; }

        /// <summary>
        ///     Full human-readable representation of the place's name.
        /// </summary>
        [JsonProperty("full_name")]
        public string FullName { get; set; }

        /// <summary>
        ///     ID representing this place. Note that this is represented as a string, not an integer.
        ///     In trends/avaliable or trends/closest, ID is a Yahoo! Where On Earth ID.
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        ///     Short human-readable representation of the place's name.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        ///     The type of location represented by this place.
        /// </summary>
        [JsonProperty("place_type")]
        public string PlaceType { get; set; }

        /// <summary>
        ///     URL representing the location of additional place metadata for this place.
        /// </summary>
        [JsonProperty("url")]
        [JsonConverter(typeof(UriConverter))]
        public Uri Url { get; set; }

        /// <summary>
        ///     The array of Places contained within this Place.
        /// </summary>
        [JsonProperty("contained_within")]
        public Place[] ContainedWithin { get; set; }
    }

    /// <summary>
    /// Bounding box.
    /// This class can easily be converted to a JSON with JsonConvert.SerializeObject.
    /// </summary>
    [JsonObject]
    public class BoundingBox : CoreBase, IEnumerable<Coordinates>
    {
        /// <summary>
        /// A series of longitude and latitude points, defining a box which will contain the Place entity this bounding box is related to. Each point is an array in the form of [longitude, latitude]. Points are grouped into an array per bounding box. Bounding box arrays are wrapped in one additional array to be compatible with the polygon notation.
        /// </summary>
        [JsonProperty("coordinates")]
        public double[][][] Coordinates { get; set; }

        IEnumerable<Coordinates> GetCoordinates()
        {
            return Coordinates[0].Select(x => new Coordinates(x[0], x[1]));
        }

        /// <summary>
        /// The type of data encoded in the coordinates property. This will be "Polygon" for bounding boxes.
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetCoordinates().ToArray().GetEnumerator();
        }

        public IEnumerator<Coordinates> GetEnumerator()
        {
            return GetCoordinates().GetEnumerator();
        }

        public Coordinates this[int index]
        {
            get
            {
                return GetCoordinates().ToArray()[index];
            }
            set
            {
                Coordinates[0][index][0] = value.Longtitude;
                Coordinates[0][index][1] = value.Latitude;
            }
        }
    }


    /// <summary>
    ///     Locates places near the given coordinates which are similar in name.
    /// </summary>
    [JsonObject]
    public class GeoResult : CoreBase, IEnumerable<Place>
    {
        /// <summary>
        ///     Places.
        /// </summary>
        [JsonProperty("places")]
        public Place[] Places { get; set; }

        /// <summary>
        ///     The token needed to be able to create a new place.
        /// </summary>
        [JsonProperty("token")]
        public string Token { get; set; }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return Places.GetEnumerator();
        }

        public IEnumerator<Place> GetEnumerator()
        {
            return (Places as IEnumerable<Place>).GetEnumerator();
        }
    }

    /// <summary>
    ///      Trending topics for a specific WOEID
    /// </summary>
    [JsonObject]
    public class TrendsResult : CoreBase, IEnumerable<Trend>
    {
        /// <summary>
        ///     The UTC datetime that the trends are valid as of.
        /// </summary>
        [JsonProperty("as_of")]
        [JsonConverter(typeof(DateTimeOffsetConverter))]
        public DateTimeOffset AsOf { get; set; }

        /// <summary>
        ///     The UTC datetime that this result was created at.
        /// </summary>
        [JsonProperty("created_at")]
        [JsonConverter(typeof(DateTimeOffsetConverter))]
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        ///     Locations of trending topics.
        /// </summary>
        [JsonProperty("locations")]
        public Location[] Locations { get; set; }

        /// <summary>
        ///     The queried trends.
        /// </summary>
        [JsonProperty("trends")]
        public Trend[] Trends { get; set; }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return Trends.GetEnumerator();
        }

        public IEnumerator<Trend> GetEnumerator()
        {
            return (Trends as IEnumerable<Trend>).GetEnumerator();
        }
    }

    /// <summary>
    ///     The metadata about places. 
    /// </summary>
    public class GeoAttributes : CoreBase
    {
        /// <summary>
        ///     The address of street.
        /// </summary>
        [JsonProperty("street_address")]
        public string StreetAddress { get; set; }

        /// <summary>
        ///     The city the place is in.
        /// </summary>
        [JsonProperty("locality")]
        public string Locality { get; set; }

        /// <summary>
        ///     The administrative region the place is in.
        /// </summary>
        [JsonProperty("region")]
        public string Region { get; set; }

        /// <summary>
        ///     The country code.
        /// </summary>
        [JsonProperty("iso3")]
        public string Iso3CountryCode { get; set; }

        /// <summary>
        ///     In the preferred local format for the place.
        /// </summary>
        [JsonProperty("postal_code")]
        public string PostalCode { get; set; }

        /// <summary>
        ///     In the preferred local format for the place, include long distance code.
        /// </summary>
        [JsonProperty("phone")]
        public string Phone { get; set; }
    }

    public class Location : CoreBase
    {
        /// <summary>
        ///     The name of this location.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        ///     The WOEID of this location.
        /// </summary>
        [JsonProperty("woeid")]
        public string WoeId { get; set; }
    }

    public class Trend : CoreBase
    {
        /// <summary>
        ///     The name of this trend.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        ///     URL to search this trend.
        /// </summary>
        [JsonProperty("url")]
        [JsonConverter(typeof(UriConverter))]
        public Uri Url { get; set; }

        /// <summary>
        ///     The query string for search.
        /// </summary>
        [JsonProperty("query")]
        public string Query { get; set; }
    }
}