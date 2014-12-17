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
using Newtonsoft.Json.Linq;

namespace CoreTweet.Core
{
    /// <summary>
    /// Represents a Twitter object. This is an <c>abstract</c> class.
    /// </summary>
    public abstract class CoreBase
    {
        /// <summary>
        /// Converts the JSON to a twitter object of the specified type.
        /// </summary>
        /// <remarks>
        /// <para>This method is used internally in CoreTweet.</para>
        /// <para>You can use this method for debugging.</para>
        /// </remarks>
        /// <param name="json">The json message.</param>
        /// <param name="jsonPath">JSONPath of object to be deserialize.</param>
        /// <typeparam name="T">The type of a twitter object.</typeparam>
        /// <returns>The twitter object.</returns>
        public static T Convert<T>(string json, string jsonPath = "")
        {
            return ConvertBase<T>(json, jsonPath);
        }

        static T ConvertBase<T>(string json, string jsonPath)
        {
            try
            {
                return JToken.Parse(json).SelectToken(JsonPathPrefix + jsonPath).ToObject<T>();
            }
            catch(Exception ex)
            {
                throw new ParsingException("on a REST api, cannot parse the json", json, ex);
            }
        }

        /// <summary>
        /// <para>Converts the json to a twitter object of the specified type.</para>
        /// <para>This is used for APIs that return an array.</para>
        /// </summary>
        /// <remarks>
        /// <para>This method is used internally in CoreTweet.</para>
        /// <para>You can use this method for debugging.</para>
        /// </remarks>
        /// <param name="json">The json message.</param>
        /// <param name="jsonPath">JSONPath of object to be deserialize.</param>
        /// <typeparam name="T">The type of a twitter object.</typeparam>
        /// <returns>Twitter objects.</returns>
        public static List<T> ConvertArray<T>(string json, string jsonPath)
        {
            return ConvertBase<List<T>>(json, jsonPath);
        }

        internal static readonly string JsonPathPrefix = "";
    }

}