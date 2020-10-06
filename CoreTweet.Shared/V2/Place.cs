// The MIT License (MIT)
//
// CoreTweet - A .NET Twitter Library supporting Twitter API 1.1
// Copyright (c) 2013-2018 CoreTweet Development Team
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
using System.Text;
using System.Runtime.Serialization;
using CoreTweet.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CoreTweet.V2
{
    public class Place : CoreBase
    {
        /// <summary>
        /// The unique identifier of the place, if this is a point of interest tagged in the Tweet.
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// The short name of this place, for example <c>"San Francisco"</c>.
        /// </summary>
        /// <value></value>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// A longer-form detailed place name, for example <c>"San Francisco, CA"</c>.
        /// </summary>
        [JsonProperty("full_name")]
        public string FullName { get; set; }

        /// <summary>
        /// Specified the particular type of information represented by this place information, such as a city name, or a point of interest.
        /// </summary>
        [JsonProperty("place_type")]
        public PlaceType PlaceType { get; set; }

        /// <summary>
        /// The ISO Alpha-2 country code this place belongs to.
        /// </summary>
        [JsonProperty("country_code")]
        public string CountryCode { get; set; }

        /// <summary>
        /// The full-length name of the country this place belongs to.
        /// </summary>
        [JsonProperty("country")]
        public string Country { get; set; }

        /// <summary>
        /// Returns the identifiers of known places that contain the referenced place.
        /// </summary>
        [JsonProperty("contained_within")]
        public Place[] ContainedWithin { get; set; }

        /// <summary>
        /// Contains place details in GeoJSON format.
        /// </summary>
        [JsonProperty("geo")]
        public GeoJsonFeature<GeoJsonPoint> Geo { get; set; }
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum PlaceType
    {
        [EnumMember(Value = "city")]
        City,
        [EnumMember(Value = "unknown")]
        Unknown,
        [EnumMember(Value = "country")]
        Country,
        [EnumMember(Value = "admin")]
        Admin,
        [EnumMember(Value = "neighborhood")]
        Neighborhood,
        [EnumMember(Value = "poi")]
        Poi,
        [EnumMember(Value = "zip_code")]
        ZipCode,
        [EnumMember(Value = "metro")]
        Metro,
        [EnumMember(Value = "admin0")]
        Admin0,
        [EnumMember(Value = "admin1")]
        Admin1,
    }

    /// <summary>
    /// List of location fields to return. The response will contain the selected fields only if location data is present in any of the response objects.
    /// </summary>
    [Flags]
    public enum PlaceFields
    {
        None            = 0x00000000,
        ContainedWithin = 0x00000001,
        Country         = 0x00000002,
        CountryCode     = 0x00000004,
        FullName        = 0x00000008,
        Geo             = 0x00000010,
        Id              = 0x00000020,
        Name            = 0x00000040,
        PlaceType       = 0x00000080,
        All             = 0x000000ff,
    }

    public static class PlaceFieldsExtensions
    {
        public static string ToQueryString(this PlaceFields value)
        {
            if (value == PlaceFields.None)
                return "";

            var builder = new StringBuilder();

            if ((value & PlaceFields.ContainedWithin) != 0)
                builder.Append("contained_within,");
            if ((value & PlaceFields.Country) != 0)
                builder.Append("country,");
            if ((value & PlaceFields.CountryCode) != 0)
                builder.Append("country_code,");
            if ((value & PlaceFields.FullName) != 0)
                builder.Append("full_name,");
            if ((value & PlaceFields.Geo) != 0)
                builder.Append("geo,");
            if ((value & PlaceFields.Id) != 0)
                builder.Append("id,");
            if ((value & PlaceFields.Name) != 0)
                builder.Append("name,");
            if ((value & PlaceFields.PlaceType) != 0)
                builder.Append("place_type,");

            return builder.ToString(0, builder.Length - 1);
        }
    }
}
