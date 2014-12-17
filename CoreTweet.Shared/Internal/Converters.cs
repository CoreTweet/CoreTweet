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
using Newtonsoft.Json;

namespace CoreTweet.Core
{
    /// <summary>
    /// Provides the <see cref="System.Uri"/> converter of the <see cref="Newtonsoft.Json.JsonSerializer"/>.
    /// </summary>
    public class UriConverter : JsonConverter
    {
        /// <summary>
        /// Returns whether this converter can convert the object to the specified type.
        /// </summary>
        /// <param name="objectType">A <see cref="System.Type"/> that represents the type you want to convert to.</param>
        /// <returns>
        /// <c>true</c> if this converter can perform the conversion; otherwise, <c>false</c>.
        /// </returns>
        public override bool CanConvert(Type objectType)
        {
            return objectType.Equals(typeof(Uri));
        }

        /// <summary>
        /// Reads the JSON representation of the object.
        /// </summary>
        /// <param name="reader">The <see cref="Newtonsoft.Json.JsonReader"/> to read from.</param>
        /// <param name="objectType">The <see cref="System.Type"/> of the object.</param>
        /// <param name="existingValue">The existing value of object being read.</param>
        /// <param name="serializer">The calling serializer.</param>
        /// <returns>The object value.</returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            switch(reader.TokenType)
            {
                case JsonToken.String:
                    return new Uri(reader.Value as String);
                case JsonToken.Null:
                    return null;
            }

            throw new InvalidOperationException("This object is not a Uri");
        }

        /// <summary>
        /// Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer">The <see cref="Newtonsoft.Json.JsonWriter"/> to write to.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The calling serializer.</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if(null == value)
                writer.WriteNull();
            else if(value is Uri)
                writer.WriteValue(((Uri)value).OriginalString);
            else
                throw new InvalidOperationException("This object is not a Uri");
        }
    }

    /// <summary>
    /// Provides the <see cref="System.DateTimeOffset"/> converter of the <see cref="Newtonsoft.Json.JsonSerializer"/>.
    /// </summary>
    public class DateTimeOffsetConverter : JsonConverter
    {
        /// <summary>
        /// Returns whether this converter can convert the object to the specified type.
        /// </summary>
        /// <param name="type">A <see cref="System.Type"/> that represents the type you want to convert to.</param>
        /// <returns>
        /// <c>true</c> if this converter can perform the conversion; otherwise, <c>false</c>.
        /// </returns>
        public override bool CanConvert(Type type)
        {
            return type.Equals(typeof(DateTimeOffset));
        }

        /// <summary>
        /// Reads the JSON representation of the object.
        /// </summary>
        /// <param name="reader">The <see cref="Newtonsoft.Json.JsonReader"/> to read from.</param>
        /// <param name="objectType">The <see cref="System.Type"/> of the object.</param>
        /// <param name="existingValue">The existing value of object being read.</param>
        /// <param name="serializer">The calling serializer.</param>
        /// <returns>The object value.</returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            switch(reader.TokenType)
            {
                case JsonToken.String:
                    return DateTimeOffset.ParseExact(reader.Value as string, "ddd MMM dd HH:mm:ss K yyyy",
                                                  System.Globalization.DateTimeFormatInfo.InvariantInfo,
                                                  System.Globalization.DateTimeStyles.AllowWhiteSpaces);
                case JsonToken.Date:
                    if (reader.Value is DateTimeOffset)
                        return (DateTimeOffset)reader.Value;
                    else
                        return new DateTimeOffset(((DateTime)reader.Value).ToUniversalTime(), TimeSpan.Zero);
                case JsonToken.Integer:
                    return InternalUtils.GetUnixTime((long)reader.Value);

                case JsonToken.Null:
                    return DateTimeOffset.Now;
            }

            throw new InvalidOperationException("This object is not a DateTimeOffset");
        }

        /// <summary>
        /// Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer">The <see cref="Newtonsoft.Json.JsonWriter"/> to write to.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The calling serializer.</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if(value is DateTimeOffset)
                writer.WriteValue((DateTimeOffset)value);
            else
                throw new InvalidOperationException("This object is not a DateTimeOffset");
        }
    }

    /// <summary>
    /// Provides the <see cref="CoreTweet.Contributors"/> converter of the <see cref="Newtonsoft.Json.JsonSerializer"/>.
    /// </summary>
    public class ContributorsConverter : JsonConverter
    {
        /// <summary>
        /// Returns whether this converter can convert the object to the specified type.
        /// </summary>
        /// <param name="objectType">A <see cref="System.Type"/> that represents the type you want to convert to.</param>
        /// <returns>
        /// <c>true</c> if this converter can perform the conversion; otherwise, <c>false</c>.
        /// </returns>
        public override bool CanConvert(Type objectType)
        {
            return objectType.Equals(typeof(Contributors));
        }

        /// <summary>
        /// Reads the JSON representation of the object.
        /// </summary>
        /// <param name="reader">The <see cref="Newtonsoft.Json.JsonReader"/> to read from.</param>
        /// <param name="objectType">The <see cref="System.Type"/> of the object.</param>
        /// <param name="existingValue">The existing value of object being read.</param>
        /// <param name="serializer">The calling serializer.</param>
        /// <returns>The object value.</returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            switch(reader.TokenType)
            {
                case JsonToken.Integer:
                    return new Contributors { Id = (long)reader.Value };
                case JsonToken.StartObject:
                    reader.Read();
                    var value = new Contributors();
                    while(reader.TokenType != JsonToken.EndObject)
                    {
                        if(reader.TokenType != JsonToken.PropertyName)
                            throw new FormatException("The format of this object is wrong");

                        switch((string)reader.Value)
                        {
                            case "id":
                                value.Id = (long)reader.ReadAsDecimal();
                                break;
                            case "screen_name":
                                value.ScreenName = reader.ReadAsString();
                                break;
                            default:
                                reader.Read();
                                break;
                        }
                        reader.Read();
                    }
                    return value;
            }

            throw new InvalidOperationException("This object is not a Contributors");
        }

        /// <summary>
        /// Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer">The <see cref="Newtonsoft.Json.JsonWriter"/> to write to.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The calling serializer.</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Provides the <see cref="System.DateTimeOffset"/> converter of the <see cref="Newtonsoft.Json.JsonSerializer"/>.
    /// </summary>
    public class TimestampConverter : JsonConverter
    {
        /// <summary>
        /// Returns whether this converter can convert the object to the specified type.
        /// </summary>
        /// <param name="objectType">A <see cref="System.Type"/> that represents the type you want to convert to.</param>
        /// <returns>
        /// <c>true</c> if this converter can perform the conversion; otherwise, <c>false</c>.
        /// </returns>
        public override bool CanConvert(Type objectType)
        {
            return objectType.Equals(typeof(DateTimeOffset));
        }

        /// <summary>
        /// Reads the JSON representation of the object.
        /// </summary>
        /// <param name="reader">The <see cref="Newtonsoft.Json.JsonReader"/> to read from.</param>
        /// <param name="objectType">The <see cref="System.Type"/> of the object.</param>
        /// <param name="existingValue">The existing value of object being read.</param>
        /// <param name="serializer">The calling serializer.</param>
        /// <returns>The object value.</returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            long ms;
            switch(reader.TokenType)
            {
                case JsonToken.String:
                    ms = long.Parse(reader.Value.ToString());
                    break;
                case JsonToken.Integer:
                    ms = (long)reader.Value;
                    break;
                default:
                    throw new InvalidOperationException("This object is not a timestamp");
            }
            return InternalUtils.GetUnixTimeMs(ms);
        }

        /// <summary>
        /// Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer">The <see cref="Newtonsoft.Json.JsonWriter"/> to write to.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The calling serializer.</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if(value is DateTimeOffset)
                writer.WriteValue(((((DateTimeOffset)value).Ticks - InternalUtils.unixEpoch.Ticks) / 10000).ToString("D"));
            else
                throw new InvalidOperationException("This object is not a DateTimeOffset");
        }
    }
}

