// The MIT License (MIT)
//
// CoreTweet - A .NET Twitter Library supporting Twitter API 1.1
// Copyright (c) 2013-2015 CoreTweet Development Team
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
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Reflection;
using System.Threading;
using CoreTweet.Rest;
using CoreTweet.Streaming;

namespace CoreTweet.Core
{
    /// <summary>
    /// Represents an OAuth token. This is an <c>abstract</c> class.
    /// </summary>
    public abstract partial class TokensBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CoreTweet.Core.TokensBase"/> class.
        /// </summary>
        public TokensBase()
        {
            this.ConnectionOptions = new ConnectionOptions();
        }

        /// <summary>
        /// Gets or sets the consumer key.
        /// </summary>
        public string ConsumerKey { get; set; }
        /// <summary>
        /// Gets or sets the consumer secret.
        /// </summary>
        public string ConsumerSecret { get; set; }

        #region Endpoints for Twitter API

        /// <summary>
        /// Gets the wrapper of account.
        /// </summary>
        public Account Account => new Account(this);
        /// <summary>
        /// Gets the wrapper of application.
        /// </summary>
        public Application Application => new Application(this);
        /// <summary>
        /// Gets the wrapper of blocks.
        /// </summary>
        public Blocks Blocks => new Blocks(this);
        /// <summary>
        /// Gets the wrapper of direct_messages.
        /// </summary>
        public DirectMessages DirectMessages => new DirectMessages(this);
        /// <summary>
        /// Gets the wrapper of favorites.
        /// </summary>
        public Favorites Favorites => new Favorites(this);
        /// <summary>
        /// Gets the wrapper of friends.
        /// </summary>
        public Friends Friends => new Friends(this);
        /// <summary>
        /// Gets the wrapper of followers.
        /// </summary>
        public Followers Followers => new Followers(this);
        /// <summary>
        /// Gets the wrapper of friendships.
        /// </summary>
        public Friendships Friendships => new Friendships(this);
        /// <summary>
        /// Gets the wrapper of geo.
        /// </summary>
        public Geo Geo => new Geo(this);
        /// <summary>
        /// Gets the wrapper of help.
        /// </summary>
        public Help Help => new Help(this);
        /// <summary>
        /// Gets the wrapper of lists.
        /// </summary>
        public Lists Lists => new Lists(this);
        /// <summary>
        /// Gets the wrapper of media.
        /// </summary>
        public Media Media => new Media(this);
        /// <summary>
        /// Gets the wrapper of mutes.
        /// </summary>
        public Mutes Mutes => new Mutes(this);
        /// <summary>
        /// Gets the wrapper of search.
        /// </summary>
        public Search Search => new Search(this);
        /// <summary>
        /// Gets the wrapper of saved_searches.
        /// </summary>
        public SavedSearches SavedSearches => new SavedSearches(this);
        /// <summary>
        /// Gets the wrapper of statuses.
        /// </summary>
        public Statuses Statuses => new Statuses(this);
        /// <summary>
        /// Gets the wrapper of trends.
        /// </summary>
        public Trends Trends => new Trends(this);
        /// <summary>
        /// Gets the wrapper of users.
        /// </summary>
        public Users Users => new Users(this);
        /// <summary>
        /// Gets the wrapper of the Streaming API.
        /// </summary>
        public StreamingApi Streaming => new StreamingApi(this);
        #endregion

        /// <summary>
        /// Gets or sets the options of the connection.
        /// </summary>
        public ConnectionOptions ConnectionOptions { get; set; }

#if !(PCL || WIN_RT || WP)
        internal T AccessApi<T>(MethodType type, string url, Expression<Func<string,object>>[] parameters, string jsonPath = "")
        {
            return this.AccessApiImpl<T>(type, url, InternalUtils.ExpressionsToDictionary(parameters), jsonPath);
        }

        internal T AccessApi<T>(MethodType type, string url, object parameters, string jsonPath = "")
        {
            return this.AccessApiImpl<T>(type, url, InternalUtils.ResolveObject(parameters), jsonPath);
        }

        internal T AccessApi<T>(MethodType type, string url, IDictionary<string,object> parameters, string jsonPath = "")
        {
            return this.AccessApiImpl<T>(type, url, parameters, jsonPath);
        }

        internal T AccessApiImpl<T>(MethodType type, string url, IEnumerable<KeyValuePair<string, object>> parameters, string jsonPath)
        {
            using(var response = this.SendRequestImpl(type, InternalUtils.GetUrl(this.ConnectionOptions, url), parameters))
            using(var sr = new StreamReader(response.GetResponseStream()))
            {
                var json = sr.ReadToEnd();
                var result = CoreBase.Convert<T>(json, jsonPath);
                var twitterResponse = result as ITwitterResponse;
                if(twitterResponse != null)
                {
                    twitterResponse.RateLimit = InternalUtils.ReadRateLimit(response);
                    twitterResponse.Json = json;
                }
                return result;
            }
        }

        internal ListedResponse<T> AccessApiArray<T>(MethodType type, string url, Expression<Func<string, object>>[] parameters, string jsonPath = "")
        {
            return this.AccessApiArrayImpl<T>(type, url, InternalUtils.ExpressionsToDictionary(parameters), jsonPath);
        }

        internal ListedResponse<T> AccessApiArray<T>(MethodType type, string url, object parameters, string jsonPath = "")
        {
            return this.AccessApiArrayImpl<T>(type, url, InternalUtils.ResolveObject(parameters), jsonPath);
        }

        internal ListedResponse<T> AccessApiArray<T>(MethodType type, string url, IDictionary<string, object> parameters, string jsonPath = "")
        {
            return this.AccessApiArrayImpl<T>(type, url, parameters, jsonPath);
        }

        internal ListedResponse<T> AccessApiArrayImpl<T>(MethodType type, string url, IEnumerable<KeyValuePair<string, object>> parameters, string jsonPath)
        {
            using(var response = this.SendRequestImpl(type, InternalUtils.GetUrl(this.ConnectionOptions, url), parameters))
            using(var sr = new StreamReader(response.GetResponseStream()))
            {
                var json = sr.ReadToEnd();
                var list = CoreBase.ConvertArray<T>(json, jsonPath);
                return new ListedResponse<T>(list, InternalUtils.ReadRateLimit(response), json);
            }
        }

        internal DictionaryResponse<TKey, TValue> AccessApiDictionary<TKey, TValue>(MethodType type, string url, Expression<Func<string, object>>[] parameters, string jsonPath = "")
        {
            return this.AccessApiDictionaryImpl<TKey, TValue>(type, url, InternalUtils.ExpressionsToDictionary(parameters), jsonPath);
        }

        internal DictionaryResponse<TKey, TValue> AccessApiDictionary<TKey, TValue>(MethodType type, string url, object parameters, string jsonPath = "")
        {
            return this.AccessApiDictionaryImpl<TKey, TValue>(type, url, InternalUtils.ResolveObject(parameters), jsonPath);
        }

        internal DictionaryResponse<TKey, TValue> AccessApiDictionary<TKey, TValue>(MethodType type, string url, IDictionary<string, object> parameters, string jsonPath = "")
        {
            return this.AccessApiDictionaryImpl<TKey, TValue>(type, url, parameters, jsonPath);
        }

        internal DictionaryResponse<TKey, TValue> AccessApiDictionaryImpl<TKey, TValue>(MethodType type, string url, IEnumerable<KeyValuePair<string, object>> parameters, string jsonPath)
        {
            using(var response = this.SendRequestImpl(type, InternalUtils.GetUrl(this.ConnectionOptions, url), parameters))
            using(var sr = new StreamReader(response.GetResponseStream()))
            {
                var json = sr.ReadToEnd();
                var dic = CoreBase.Convert<Dictionary<TKey, TValue>>(json, jsonPath);
                return new DictionaryResponse<TKey, TValue>(dic, InternalUtils.ReadRateLimit(response), json);
            }
        }

        internal void AccessApiNoResponse(string url, Expression<Func<string,object>>[] parameters)
        {
            this.AccessApiNoResponseImpl(url, InternalUtils.ExpressionsToDictionary(parameters));
        }

        internal void AccessApiNoResponse(string url, object parameters)
        {
            this.AccessApiNoResponseImpl(url, InternalUtils.ResolveObject(parameters));
        }

        internal void AccessApiNoResponse(string url, IDictionary<string,object> parameters)
        {
            this.AccessApiNoResponseImpl(url, parameters);
        }

        internal void AccessApiNoResponseImpl(string url, IEnumerable<KeyValuePair<string, object>> parameters)
        {
            this.SendRequestImpl(MethodType.Post, InternalUtils.GetUrl(this.ConnectionOptions, url), parameters).Close();
        }
#endif

        /// <summary>
        /// When overridden in a descendant class, creates a string for Authorization header.
        /// </summary>
        /// <param name="type">Type of HTTP request.</param>
        /// <param name="url">The URL.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>A string for Authorization header.</returns>
        public abstract string CreateAuthorizationHeader(MethodType type, string url, IEnumerable<KeyValuePair<string, object>> parameters);

        private static object FormatObject(object x)
        {
            if (x is string) return x;
            if (x is int)
                return ((int)x).ToString("D", CultureInfo.InvariantCulture);
            if (x is long)
                return ((long)x).ToString("D", CultureInfo.InvariantCulture);
            if (x is double)
            {
                var s = ((double)x).ToString("F99", CultureInfo.InvariantCulture).TrimEnd('0');
                if (s[s.Length - 1] == '.') s += '0';
                return s;
            }
            if (x is float)
            {
                var s = ((float)x).ToString("F99", CultureInfo.InvariantCulture).TrimEnd('0');
                if (s[s.Length - 1] == '.') s += '0';
                return s;
            }
            if (x is uint)
                return ((uint)x).ToString("D", CultureInfo.InvariantCulture);
            if (x is ulong)
                return ((ulong)x).ToString("D", CultureInfo.InvariantCulture);
            if (x is short)
                return ((short)x).ToString("D", CultureInfo.InvariantCulture);
            if (x is ushort)
                return ((ushort)x).ToString("D", CultureInfo.InvariantCulture);
            if (x is decimal)
                return ((decimal)x).ToString(CultureInfo.InvariantCulture);
            if (x is byte)
                return ((byte)x).ToString("D", CultureInfo.InvariantCulture);
            if (x is sbyte)
                return ((sbyte)x).ToString("D", CultureInfo.InvariantCulture);

            if (x is IEnumerable<string>
                || x is IEnumerable<int>
                || x is IEnumerable<long>
                || x is IEnumerable<double>
                || x is IEnumerable<float>
                || x is IEnumerable<uint>
                || x is IEnumerable<ulong>
                || x is IEnumerable<short>
                || x is IEnumerable<ushort>
                || x is IEnumerable<decimal>)
            {
                return ((System.Collections.IEnumerable)x).Cast<object>().Select(FormatObject).JoinToString(",");
            }

            var type = x.GetType();
            if (type.Name == "FSharpOption`1")
            {
                return FormatObject(
#if WIN_RT || PCL
                    type.GetRuntimeProperty("Value").GetValue(x)
#else
                    type.GetProperty("Value").GetValue(x, null)
#endif
);
            }

            return x;
        }

        private static KeyValuePair<string, object>[] FormatParameters(IEnumerable<KeyValuePair<string, object>> parameters)
        {
            return parameters != null
                ? parameters.Where(kvp => kvp.Key != null && kvp.Value != null)
                    .Select(kvp => new KeyValuePair<string, object>(kvp.Key, FormatObject(kvp.Value)))
                    .ToArray()
                : new KeyValuePair<string, object>[0];
        }

#if !(PCL || WIN_RT || WP)
        /// <summary>
        /// Sends a request to the specified url with the specified parameters.
        /// </summary>
        /// <param name="type">The type of HTTP request.</param>
        /// <param name="url">The URL.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>A stream.</returns>
        public HttpWebResponse SendRequest(MethodType type, string url, params Expression<Func<string, object>>[] parameters)
        {
            return this.SendRequestImpl(type, url, InternalUtils.ExpressionsToDictionary(parameters), this.ConnectionOptions);
        }

        /// <summary>
        /// Sends a request to the specified url with the specified parameters.
        /// </summary>
        /// <param name="type">The type of HTTP request.</param>
        /// <param name="url">The URL.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>A stream.</returns>
        public HttpWebResponse SendRequest(MethodType type, string url, object parameters)
        {
            return this.SendRequestImpl(type, url, InternalUtils.ResolveObject(parameters), this.ConnectionOptions);
        }

        /// <summary>
        /// Sends a request to the specified url with the specified parameters.
        /// </summary>
        /// <param name="type">The type of HTTP request.</param>
        /// <param name="url">The URL.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>A stream.</returns>
        public HttpWebResponse SendRequest(MethodType type, string url, IDictionary<string, object> parameters)
        {
            return this.SendRequestImpl(type, url, parameters, this.ConnectionOptions);
        }

        /// <summary>
        /// Sends a streaming request to the specified url with the specified parameters.
        /// </summary>
        /// <param name="type">The type of HTTP request.</param>
        /// <param name="url">The URL.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>A stream.</returns>
        public HttpWebResponse SendStreamingRequest(MethodType type, string url, IEnumerable<KeyValuePair<string, object>> parameters)
        {
            var options = this.ConnectionOptions != null ? (ConnectionOptions)this.ConnectionOptions.Clone() : new ConnectionOptions();
            options.UseCompression = options.UseCompressionOnStreaming;
            options.ReadWriteTimeout = Timeout.Infinite;
            return this.SendRequestImpl(type, url, parameters, options);
        }

        internal HttpWebResponse SendRequestImpl(MethodType type, string url, IEnumerable<KeyValuePair<string, object>> parameters)
        {
            return this.SendRequestImpl(type, url, parameters, this.ConnectionOptions);
        }

        private HttpWebResponse SendRequestImpl(MethodType type, string url, IEnumerable<KeyValuePair<string, object>> parameters, ConnectionOptions options)
        {
            try
            {
                var prmArray = FormatParameters(parameters);
                if(type != MethodType.Get && prmArray.Any(x => x.Value is Stream || x.Value is IEnumerable<byte> || x.Value is FileInfo))
                {
                    return Request.HttpPostWithMultipartFormData(url, prmArray,
                        CreateAuthorizationHeader(type, url, null), options);
                }

                var header = CreateAuthorizationHeader(type, url, prmArray);
                return type == MethodType.Get ? Request.HttpGet(url, prmArray, header, options) :
                    Request.HttpPost(url, prmArray, header, options);
            }
            catch(WebException ex)
            {
                var tex = TwitterException.Create(ex);
                if(tex != null) throw tex;
                throw;
            }
        }
#endif
    }
}
