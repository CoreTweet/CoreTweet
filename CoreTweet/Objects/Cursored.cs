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
using Newtonsoft.Json;

namespace CoreTweet
{
    /// <summary>
    /// The cursored message object.
    /// </summary>
    [JsonObject]
    public class Cursored<T> : CoreBase, IEnumerable<T>
    {
        /// <summary>
        /// Results.
        /// </summary>
        /// <value>Result.</value>
        public IEnumerable<T> Result
        {
            get
            {
                return _ids ?? _users ?? _lists;
            }
        }

        /// <summary>
        /// The next cursor.
        /// </summary>
        /// <value>The next cursor.</value>
        [JsonProperty("next_cursor")]
        public long NextCursor{ get; set; }

        /// <summary>
        /// The previous cursor.
        /// </summary>
        /// <value>The previous cursor.</value>
        [JsonProperty("previous_cursor")]
        public long PreviousCursor{ get; set; }

        [JsonProperty("users")]
        T[] _users { get; set; }

        [JsonProperty("lists")]
        T[] _lists { get; set; }

        [JsonProperty("ids")]
        T[] _ids { get; set; }


        /// <summary>
        /// IE\<T\> implementation
        /// </summary>
        public IEnumerator<T> GetEnumerator()
        {
            return (Result as IEnumerable<T>).GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return Result.GetEnumerator();
        }

        internal static IEnumerable<T> Enumerate(TokensBase tokens, string apiName, EnumerateMode mode, params Expression<Func<string,object>>[] parameters)
        {
            var p = InternalUtils.ExpressionsToDictionary(parameters);
            return Enumerate(tokens, apiName, mode, p);
        }

        internal static IEnumerable<T> Enumerate(TokensBase tokens, string apiName, EnumerateMode mode, IDictionary<string, object> parameters)
        {
            var r = tokens.AccessApi<Cursored<T>>(MethodType.Get, apiName, parameters);
            while(true)
            {
                foreach(var i in r)
                    yield return i;
                var next = mode == EnumerateMode.Next ? r.NextCursor : r.PreviousCursor;
                if(next == 0)
                    break;
                parameters["cursor"] = next;
                r = tokens.AccessApi<Cursored<T>>(MethodType.Get, apiName, parameters);
            }
        }

        internal static IEnumerable<T> Enumerate<TV>(TokensBase tokens, string apiName, EnumerateMode mode, TV parameters)
        {
            var p = InternalUtils.ResolveObject(parameters);
            return Enumerate(tokens, apiName, mode, p);
        }
    }

    public enum EnumerateMode
    {
        Next, Previous
    }
}

