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
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Alice.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace CoreTweet.Core
{
    /// <summary>
    /// The Uri converter for the JsonSerializer.
    /// </summary>
    public class UriConverter : JsonConverter
    {
        /// <summary>
        /// Determines whether this instance can convert the specified type.
        /// </summary>
        /// <returns>
        /// <c>true</c> if this instance can convert the specified type; otherwise, <c>false</c>.
        /// </returns>
        /// <param name='type'>
        /// If set to <c>true</c> type.
        /// </param>
        public override bool CanConvert(Type type)
        {
            return type.Equals(typeof(Uri));
        }

        /// <summary>
        /// Reads and parses the json.
        /// </summary>
        /// <returns>
        /// The json.
        /// </returns>
        /// <param name='jr'>
        /// The instance of JsonReader.
        /// </param>
        /// <param name='_'>
        /// Unused.
        /// </param>
        /// <param name='__'>
        /// Unused.
        /// </param>
        /// <param name='___'>
        /// Unused.
        /// </param>
        public override object ReadJson(JsonReader jr, Type _, object __, JsonSerializer ___)
        {
            switch (jr.TokenType)
            {
                case JsonToken.String:
                    return new Uri(jr.Value as String);
                case JsonToken.Null:
                    return null;
            }

            throw new InvalidOperationException("This object is not a Uri");
        }

        /// <summary>
        /// Writes the object to the json.
        /// </summary>
        /// <param name='jw'>
        /// The instance of JsonReader.
        /// </param>
        /// <param name='value'>
        /// The object you want to serialize.
        /// </param>
        /// <param name='_'>
        /// Unused.
        /// </param>
        public override void WriteJson(JsonWriter jw, object value, JsonSerializer _)
        {
            if (null == value)
                jw.WriteNull();
            else if (value is Uri)
                jw.WriteValue(((Uri)value).OriginalString);
            else
                throw new InvalidOperationException("This object is not a Uri");
        }
    }

    /// <summary>
    /// The DateTimeOffset converter for the JsonSerializer.
    /// </summary>
    public class DateTimeOffsetConverter : JsonConverter
    {
        /// <summary>
        /// Determines whether this instance can convert the specified type.
        /// </summary>
        /// <returns>
        /// <c>true</c> if this instance can convert the specified type; otherwise, <c>false</c>.
        /// </returns>
        /// <param name='type'>
        /// If set to <c>true</c> type.
        /// </param>
        public override bool CanConvert(Type type)
        {
            return type.Equals(typeof(DateTimeOffset));
        }

        /// <summary>
        /// Reads and parses the json.
        /// </summary>
        /// <returns>
        /// The json.
        /// </returns>
        /// <param name='jr'>
        /// The instance of JsonReader.
        /// </param>
        /// <param name='_'>
        /// Unused.
        /// </param>
        /// <param name='__'>
        /// Unused.
        /// </param>
        /// <param name='___'>
        /// Unused.
        /// </param>
        public override object ReadJson(JsonReader jr, Type _, object __, JsonSerializer ___)
        {
            switch (jr.TokenType)
            {
                case JsonToken.String:
                    return DateTimeOffset.ParseExact(jr.Value as string, "ddd MMM dd HH:mm:ss K yyyy",
                                                  System.Globalization.DateTimeFormatInfo.InvariantInfo, 
                                                  System.Globalization.DateTimeStyles.AllowWhiteSpaces);
                case JsonToken.Integer:
                    return new DateTimeOffset(new DateTime((long)jr.Value));
                
                case JsonToken.Null:
                    return DateTimeOffset.Now;
            }

            throw new InvalidOperationException("This object is not a DateTimeOffset");
        }

        /// <summary>
        /// Writes the object to the json.
        /// </summary>
        /// <param name='jw'>
        /// The instance of JsonReader.
        /// </param>
        /// <param name='value'>
        /// The object you want to serialize.
        /// </param>
        /// <param name='_'>
        /// Unused.
        /// </param>
        public override void WriteJson(JsonWriter jw, object value, JsonSerializer _)
        {
            if (value is Uri)
                jw.WriteValue((DateTimeOffset)value);
            else
                throw new InvalidOperationException("This object is not a DateTimeOffset");
        }
    }
}

