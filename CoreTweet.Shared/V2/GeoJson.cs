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
using System.Collections.Generic;
using CoreTweet.Core;
using Newtonsoft.Json;

namespace CoreTweet.V2
{
    public abstract class GeoJsonObject : CoreBase
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        /// The bounding box coordinates for this place (latitude_start, longitude_start, latitude_end, longitude_end).
        /// </summary>
        [JsonProperty("bbox")]
        public GeoJsonBoundingBox Bbox { get; set; }

        /// <summary>
        /// A dictionary containing additional properties, as defined by the GeoJSON specification. Can be empty.
        /// </summary>
        [JsonProperty("properties")]
        public IDictionary<string, object> Properties { get; set; }
    }

    public class GeoJsonPoint : GeoJsonObject
    {
        /// <summary>
        /// A pair of decimal values representing the precise location of the user (latitude, longitude). This value be <c>null</c> unless the user explicitly shared their precise location.
        /// </summary>
        [JsonProperty("coordinates")]
        public GeoJsonPointCoordinates? Coordinates { get; set; }
    }

    public class GeoJsonFeature<T> : GeoJsonObject
        where T : GeoJsonObject
    {
        /// <summary>
        /// Contains GeoJSON point information for this place, if available.
        /// </summary>
        [JsonProperty("geometry")]
        public T Geometry { get; set; }
    }

    [JsonConverter(typeof(GeoJsonBoundingBoxJsonConverter))]
    public struct GeoJsonBoundingBox
    {
        public double West { get; set; }

        public double South { get; set; }

        public double East { get; set; }

        public double North { get; set; }
    }

    [JsonConverter(typeof(GeoJsonPointCoordinatesJsonConverter))]
    public struct GeoJsonPointCoordinates
    {
        public double Longitude { get; set; }

        public double Latitude { get; set; }
    }

    public class GeoJsonBoundingBoxJsonConverter : JsonConverter<GeoJsonBoundingBox?>
    {
        public override GeoJsonBoundingBox? ReadJson(JsonReader reader, Type objectType, GeoJsonBoundingBox? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.TokenType != JsonToken.StartArray)
                return null;

            var west = reader.ReadAsDouble();

            if (west == null)
                return null;

            var south = reader.ReadAsDouble();

            if (south == null)
                return null;

            var east = reader.ReadAsDouble();

            if (east == null)
                return null;

            var north = reader.ReadAsDouble();

            if (north == null)
                return null;

            if (!reader.Read() || reader.TokenType != JsonToken.EndArray)
                return null;

            return new GeoJsonBoundingBox()
            {
                West = west.Value,
                South = south.Value,
                East = east.Value,
                North = north.Value,
            };
        }

        public override void WriteJson(JsonWriter writer, GeoJsonBoundingBox? value, JsonSerializer serializer)
        {
            if (value.HasValue)
            {
                writer.WriteStartArray();
                writer.WriteValue(value.Value.West);
                writer.WriteValue(value.Value.South);
                writer.WriteValue(value.Value.East);
                writer.WriteValue(value.Value.North);
                writer.WriteEndArray();
            }
            else
            {
                writer.WriteNull();
            }
        }
    }

    public class GeoJsonPointCoordinatesJsonConverter : JsonConverter<GeoJsonPointCoordinates?>
    {
        public override GeoJsonPointCoordinates? ReadJson(JsonReader reader, Type objectType, GeoJsonPointCoordinates? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.TokenType != JsonToken.StartArray)
                return null;

            var longtitude = reader.ReadAsDouble();

            if (longtitude == null)
                return null;

            var latitude = reader.ReadAsDouble();

            if (latitude == null)
                return null;

            if (!reader.Read() || reader.TokenType != JsonToken.EndArray)
                return null;

            return new GeoJsonPointCoordinates()
            {
                Longitude = longtitude.Value,
                Latitude = latitude.Value,
            };
        }

        public override void WriteJson(JsonWriter writer, GeoJsonPointCoordinates? value, JsonSerializer serializer)
        {
            if (value.HasValue)
            {
                writer.WriteStartArray();
                writer.WriteValue(value.Value.Longitude);
                writer.WriteValue(value.Value.Latitude);
                writer.WriteEndArray();
            }
            else
            {
                writer.WriteNull();
            }
        }
    }
}
