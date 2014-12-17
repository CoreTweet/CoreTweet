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
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
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
        public Account Account { get { return new Account(this); } }
        /// <summary>
        /// Gets the wrapper of application.
        /// </summary>
        public Application Application { get { return new Application(this); } }
        /// <summary>
        /// Gets the wrapper of blocks.
        /// </summary>
        public Blocks Blocks { get { return new Blocks(this); } }
        /// <summary>
        /// Gets the wrapper of direct_messages.
        /// </summary>
        public DirectMessages DirectMessages { get { return new DirectMessages(this); } }
        /// <summary>
        /// Gets the wrapper of favorites.
        /// </summary>
        public Favorites Favorites { get { return new Favorites(this); } }
        /// <summary>
        /// Gets the wrapper of friends.
        /// </summary>
        public Friends Friends { get { return new Friends(this); } }
        /// <summary>
        /// Gets the wrapper of followers.
        /// </summary>
        public Followers Followers { get { return new Followers(this); } }
        /// <summary>
        /// Gets the wrapper of friendships.
        /// </summary>
        public Friendships Friendships { get { return new Friendships(this); } }
        /// <summary>
        /// Gets the wrapper of geo.
        /// </summary>
        public Geo Geo { get { return new Geo(this); } }
        /// <summary>
        /// Gets the wrapper of help.
        /// </summary>
        public Help Help { get { return new Help(this); } }
        /// <summary>
        /// Gets the wrapper of lists.
        /// </summary>
        public Lists Lists { get { return new Lists(this); } }
        /// <summary>
        /// Gets the wrapper of media.
        /// </summary>
        public Media Media { get { return new Media(this); } }
        /// <summary>
        /// Gets the wrapper of mutes.
        /// </summary>
        public Mutes Mutes { get { return new Mutes(this); } }
        /// <summary>
        /// Gets the wrapper of search.
        /// </summary>
        public Search Search { get { return new Search(this); } }
        /// <summary>
        /// Gets the wrapper of saved_searches.
        /// </summary>
        public SavedSearches SavedSearches { get { return new SavedSearches(this); } }
        /// <summary>
        /// Gets the wrapper of statuses.
        /// </summary>
        public Statuses Statuses { get { return new Statuses(this); } }
        /// <summary>
        /// Gets the wrapper of trends.
        /// </summary>
        public Trends Trends { get { return new Trends(this); } }
        /// <summary>
        /// Gets the wrapper of users.
        /// </summary>
        public Users Users { get { return new Users(this); } }
        /// <summary>
        /// Gets the wrapper of the Streaming API.
        /// </summary>
        public StreamingApi Streaming { get { return new StreamingApi(this); } }

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

        internal T AccessApi<T, TV>(MethodType type, string url, TV parameters, string jsonPath = "")
        {
            return this.AccessApiImpl<T>(type, url, InternalUtils.ResolveObject(parameters), jsonPath);
        }

        internal T AccessApi<T>(MethodType type, string url, IDictionary<string,object> parameters, string jsonPath = "")
        {
            return this.AccessApiImpl<T>(type, url, parameters, jsonPath);
        }

        internal T AccessApiImpl<T>(MethodType type, string url, IEnumerable<KeyValuePair<string, object>> parameters, string jsonPath)
        {
            using(var response = this.SendRequestImpl(type, InternalUtils.GetUrl(url), parameters))
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

        internal ListedResponse<T> AccessApiArray<T, TV>(MethodType type, string url, TV parameters, string jsonPath = "")
        {
            return this.AccessApiArrayImpl<T>(type, url, InternalUtils.ResolveObject(parameters), jsonPath);
        }

        internal ListedResponse<T> AccessApiArray<T>(MethodType type, string url, IDictionary<string, object> parameters, string jsonPath = "")
        {
            return this.AccessApiArrayImpl<T>(type, url, parameters, jsonPath);
        }

        internal ListedResponse<T> AccessApiArrayImpl<T>(MethodType type, string url, IEnumerable<KeyValuePair<string, object>> parameters, string jsonPath)
        {
            using(var response = this.SendRequestImpl(type, InternalUtils.GetUrl(url), parameters))
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

        internal DictionaryResponse<TKey, TValue> AccessApiDictionary<TKey, TValue, TV>(MethodType type, string url, TV parameters, string jsonPath = "")
        {
            return this.AccessApiDictionaryImpl<TKey, TValue>(type, url, InternalUtils.ResolveObject(parameters), jsonPath);
        }

        internal DictionaryResponse<TKey, TValue> AccessApiDictionary<TKey, TValue>(MethodType type, string url, IDictionary<string, object> parameters, string jsonPath = "")
        {
            return this.AccessApiDictionaryImpl<TKey, TValue>(type, url, parameters, jsonPath);
        }

        internal DictionaryResponse<TKey, TValue> AccessApiDictionaryImpl<TKey, TValue>(MethodType type, string url, IEnumerable<KeyValuePair<string, object>> parameters, string jsonPath)
        {
            using (var response = this.SendRequestImpl(type, InternalUtils.GetUrl(url), parameters))
            using (var sr = new StreamReader(response.GetResponseStream()))
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

        internal void AccessApiNoResponse<TV>(string url, TV parameters)
        {
            this.AccessApiNoResponseImpl(url, InternalUtils.ResolveObject(parameters));
        }

        internal void AccessApiNoResponse(string url, IDictionary<string,object> parameters)
        {
            this.AccessApiNoResponseImpl(url, parameters);
        }

        internal void AccessApiNoResponseImpl(string url, IEnumerable<KeyValuePair<string, object>> parameters)
        {
            this.SendRequestImpl(MethodType.Post, InternalUtils.GetUrl(url), parameters).Close();
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

        private static KeyValuePair<string, object>[] CollectionToCommaSeparatedString(IEnumerable<KeyValuePair<string, object>> parameters)
        {
            return parameters != null ? parameters.Where(kvp => kvp.Key != null && kvp.Value != null).Select(kvp =>
                kvp.Value is IEnumerable<string>
                    || kvp.Value is IEnumerable<int>
                    || kvp.Value is IEnumerable<uint>
                    || kvp.Value is IEnumerable<long>
                    || kvp.Value is IEnumerable<ulong>
                    || kvp.Value is IEnumerable<decimal>
                    || kvp.Value is IEnumerable<float>
                    || kvp.Value is IEnumerable<double>
                ? new KeyValuePair<string, object>(
                    kvp.Key,
                    ((System.Collections.IEnumerable)kvp.Value)
                        .Cast<object>().Select(x => x.ToString())
                        .JoinToString(","))
                : kvp
            ).ToArray() : new KeyValuePair<string, object>[0];
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
        public HttpWebResponse SendRequest<T>(MethodType type, string url, T parameters)
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
                var prmArray = CollectionToCommaSeparatedString(parameters);
                if(type != MethodType.Get && prmArray.Any(x => x.Value is Stream || x.Value is IEnumerable<byte> || x.Value is FileInfo))
                {
                    return Request.HttpPostWithMultipartFormData(url, prmArray,
                        CreateAuthorizationHeader(type, url, null), options);
                }
                else
                {
                    var header = CreateAuthorizationHeader(type, url, prmArray);
                    return type == MethodType.Get ? Request.HttpGet(url, prmArray, header, options) :
                        Request.HttpPost(url, prmArray, header, options);
                }
            }
            catch(WebException ex)
            {
                var tex = TwitterException.Create(ex);
                if(tex != null)
                    throw tex;
                else
                    throw;
            }
        }
#endif
    }
}
