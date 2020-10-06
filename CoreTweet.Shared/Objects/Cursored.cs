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
using System.Linq;
using System.Linq.Expressions;
using CoreTweet.Core;
using Newtonsoft.Json;

namespace CoreTweet
{
    public interface ICursorForwardable
    {
        string NextCursor { get; }
    }

    public interface ICursorBackwardable
    {
        string PreviousCursor { get; }
    }

    public interface ICursored : ICursorForwardable, ICursorBackwardable
    {
    }

    /// <summary>
    /// Represents a cursored message object.
    /// </summary>
    [JsonObject]
    public class Cursored<T> : CoreBase, IEnumerable<T>, ITwitterResponse, ICursored
#if !(NET35 || NET40)
    , IReadOnlyList<T>
#endif
    {
        /// <summary>
        /// Gets the results.
        /// </summary>
        public T[] Result
        {
            get
            {
                return _ids ?? _users ?? _lists;
            }
        }

        /// <summary>
        /// Gets or sets the next cursor.
        /// </summary>
        [JsonProperty("next_cursor")]
        public long NextCursor{ get; set; }

        /// <summary>
        /// Gets or sets the previous cursor.
        /// </summary>
        [JsonProperty("previous_cursor")]
        public long PreviousCursor{ get; set; }

        [JsonProperty("users")]
        T[] _users { get; set; }

        [JsonProperty("lists")]
        T[] _lists { get; set; }

        [JsonProperty("ids")]
        T[] _ids { get; set; }

        /// <summary>
        /// Gets the element at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the element to get or set.</param>
        /// <returns>The element at the specified index.</returns>
        public T this[int index] => this.Result[index];

        /// <summary>
        /// Gets the number of elements actually contained in the <see cref="Cursored&lt;T&gt;"/>.
        /// </summary>
        public int Count => this.Result.Length;

        /// <summary>
        /// Gets or sets the rate limit of the response.
        /// </summary>
        /// <remarks>
        /// This property will always be null when obtained from (most of) the POST endpoints, unless the rate is explicitly stated in the Twitter official documentation.
        /// </remarks>
        public RateLimit RateLimit { get; set; }

        /// <summary>
        /// Gets or sets the JSON of the response.
        /// </summary>
        public string Json { get; set; }

        string ICursorForwardable.NextCursor => this.NextCursor.ToString("D");

        string ICursorBackwardable.PreviousCursor => this.PreviousCursor.ToString("D");

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>An IEnumerator object that can be used to iterate through the collection.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            return (Result as IEnumerable<T>).GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return Result.GetEnumerator();
        }

    }

    #if SYNC
    internal static class Cursored
    {
        internal static IEnumerable<T> Enumerate<T>(TokensBase tokens, string apiName, EnumerateMode mode, Expression<Func<string,object>>[] parameters, string urlPrefix = null, string urlSuffix = null)
        {
            var p = InternalUtils.ExpressionsToDictionary(parameters);
            return EnumerateImpl<T>(tokens, apiName, mode, p, urlPrefix, urlSuffix);
        }

        internal static IEnumerable<T> Enumerate<T>(TokensBase tokens, string apiName, EnumerateMode mode, IDictionary<string, object> parameters, string urlPrefix = null, string urlSuffix = null)
        {
            return EnumerateImpl<T>(tokens, apiName, mode, parameters, urlPrefix, urlSuffix);
        }

        internal static IEnumerable<T> Enumerate<T>(TokensBase tokens, string apiName, EnumerateMode mode, object parameters, string urlPrefix = null, string urlSuffix = null)
        {
            var p = InternalUtils.ResolveObject(parameters);
            return EnumerateImpl<T>(tokens, apiName, mode, p, urlPrefix, urlSuffix);
        }

        internal static IEnumerable<T> EnumerateImpl<T>(TokensBase tokens, string apiName, EnumerateMode mode, IEnumerable<KeyValuePair<string, object>> parameters, string urlPrefix, string urlSuffix)
        {
            if(mode == EnumerateMode.Next)
                return EnumerateForwardImpl<Cursored<T>, T>(tokens, apiName, parameters, urlPrefix, urlSuffix);
            else
                return EnumerateBackwardImpl<Cursored<T>, T>(tokens, apiName, parameters, urlPrefix, urlSuffix);
        }

        internal static IEnumerable<U> EnumerateForward<T, U>(TokensBase tokens, string apiName, Expression<Func<string, object>>[] parameters, string urlPrefix = null, string urlSuffix = null)
            where T : CoreBase, ICursorForwardable, IEnumerable<U>
        {
            var p = InternalUtils.ExpressionsToDictionary(parameters);
            return EnumerateForwardImpl<T, U>(tokens, apiName, p, urlPrefix, urlSuffix);
        }

        internal static IEnumerable<U> EnumerateForward<T, U>(TokensBase tokens, string apiName, IDictionary<string, object> parameters, string urlPrefix = null, string urlSuffix = null)
            where T : CoreBase, ICursorForwardable, IEnumerable<U>
        {
            return EnumerateForwardImpl<T, U>(tokens, apiName, parameters, urlPrefix, urlSuffix);
        }

        internal static IEnumerable<U> EnumerateForward<T, U>(TokensBase tokens, string apiName, object parameters, string urlPrefix = null, string urlSuffix = null)
            where T : CoreBase, ICursorForwardable, IEnumerable<U>
        {
            var p = InternalUtils.ResolveObject(parameters);
            return EnumerateForwardImpl<T, U>(tokens, apiName, p, urlPrefix, urlSuffix);
        }

        internal static IEnumerable<U> EnumerateForwardImpl<T, U>(TokensBase tokens, string apiName, IEnumerable<KeyValuePair<string, object>> parameters, string urlPrefix, string urlSuffix)
            where T : CoreBase, ICursorForwardable, IEnumerable<U>
        {
            var prmList = parameters.ToList();
            while(true)
            {
                var r = tokens.AccessApiImpl<T>(MethodType.Get, apiName, prmList, "", urlPrefix, urlSuffix);
                foreach(var i in r)
                    yield return i;
                var next = r.NextCursor;
                if(string.IsNullOrEmpty(next) || next == "0")
                    break;
                prmList.RemoveAll(kvp => kvp.Key == "cursor");
                prmList.Add(new KeyValuePair<string, object>("cursor", next));
            }
        }

        internal static IEnumerable<U> EnumerateBackwardImpl<T, U>(TokensBase tokens, string apiName, IEnumerable<KeyValuePair<string, object>> parameters, string urlPrefix, string urlSuffix)
            where T : CoreBase, ICursorBackwardable, IEnumerable<U>
        {
            var prmList = parameters.ToList();
            while(true)
            {
                var r = tokens.AccessApiImpl<T>(MethodType.Get, apiName, prmList, "", urlPrefix, urlSuffix);
                foreach(var i in r)
                    yield return i;
                var next = r.PreviousCursor;
                if(string.IsNullOrEmpty(next) || next == "0")
                    break;
                prmList.RemoveAll(kvp => kvp.Key == "cursor");
                prmList.Add(new KeyValuePair<string, object>("cursor", next));
            }
        }
    }
    #endif

    /// <summary>
    /// Provides a mode of enumeration.
    /// </summary>
    public enum EnumerateMode
    {
        /// <summary>
        /// The enumeration mode is next.
        /// </summary>
        Next,
        /// <summary>
        /// The enumeration mode is previous.
        /// </summary>
        Previous
    }
}

