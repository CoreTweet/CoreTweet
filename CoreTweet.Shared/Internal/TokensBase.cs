// The MIT License (MIT)
//
// CoreTweet - A .NET Twitter Library supporting Twitter API 1.1
// Copyright (c) 2013-2016 CoreTweet Development Team
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

#if WIN_RT
using Windows.Storage;
using Windows.Storage.Streams;
#endif

namespace CoreTweet.Core
{
    /// <summary>
    /// Represents an OAuth token. This is an <c>abstract</c> class.
    /// </summary>
    public abstract partial class TokensBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TokensBase"/> class.
        /// </summary>
        protected TokensBase()
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
        /// Gets the wrapper of collections.
        /// </summary>
        public Collections Collections => new Collections(this);
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

#if SYNC
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
                return InternalUtils.ReadResponse<T>(response, jsonPath);
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
        public abstract string CreateAuthorizationHeader(MethodType type, Uri url, IEnumerable<KeyValuePair<string, object>> parameters);

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

            if (x is UploadMediaType)
                return Media.GetMediaTypeString((UploadMediaType)x);

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
#if NETCORE
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

        private static Uri CreateUri(MethodType type, string url, IEnumerable<KeyValuePair<string, object>> formattedParameters)
        {
            var ub = new UriBuilder(url);
            if (type == MethodType.Get)
            {
                var old = ub.Query;
                var s = Request.CreateQueryString(formattedParameters);
                ub.Query = !string.IsNullOrEmpty(old)
                    ? old.TrimStart('?') + "&" + s
                    : s;
            }
            // Windows.Web.Http.HttpClient reads Uri.OriginalString, so we have to re-construct an Uri instance.
            return new Uri(ub.Uri.AbsoluteUri);
        }

        private static bool ContainsBinaryData(KeyValuePair<string, object>[] parameters)
        {
            return Array.Exists(parameters, x => x.Value is Stream || x.Value is IEnumerable<byte> || x.Value is ArraySegment<byte>
#if FILEINFO
                || x.Value is FileInfo
#else
                || x.Value.GetType().FullName == "System.IO.FileInfo"
#endif
#if WIN_RT
                || x.Value is IInputStream || x.Value is IBuffer || x.Value is IInputStreamReference || x.Value is IStorageItem
#endif
            );
        }

#if SYNC
        /// <summary>
        /// Sends a request to the specified url with the specified parameters.
        /// </summary>
        /// <param name="type">The type of HTTP request.</param>
        /// <param name="url">The URL.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>A <see cref="HttpWebResponse"/>.</returns>
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
        /// <returns>A <see cref="HttpWebResponse"/>.</returns>
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
        /// <returns>A <see cref="HttpWebResponse"/>.</returns>
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
        /// <returns>A <see cref="HttpWebResponse"/>.</returns>
        public HttpWebResponse SendStreamingRequest(MethodType type, string url, IEnumerable<KeyValuePair<string, object>> parameters)
        {
            var options = this.ConnectionOptions != null ? (ConnectionOptions)this.ConnectionOptions.Clone() : new ConnectionOptions();
            options.UseCompression = options.UseCompressionOnStreaming;
            options.ReadWriteTimeout = Timeout.Infinite;
            return this.SendRequestImpl(type, url, parameters, options);
        }

        /// <summary>
        /// Sends a request to the specified url with the specified content.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="contentType">The Content-Type header.</param>
        /// <param name="content">The content.</param>
        /// <returns>A <see cref="HttpWebResponse"/>.</returns>
        public HttpWebResponse PostContent(string url, string contentType, byte[] content)
        {
            if (string.IsNullOrEmpty(url))
                throw new ArgumentNullException(nameof(url));
            if (string.IsNullOrEmpty(contentType) != (content == null))
                throw new ArgumentException("Both contentType and content must be null or not null.");
            if (contentType.StartsWith("application/x-www-form-urlencoded", StringComparison.OrdinalIgnoreCase))
                throw new ArgumentException("Use SendRequest method to send the content in application/x-www-form-urlencoded.");

            try
            {
                var uri = new Uri(url);
                return Request.HttpPost(uri, contentType, content, CreateAuthorizationHeader(MethodType.Post, uri, null), this.ConnectionOptions);
            }
            catch (WebException ex)
            {
                var tex = TwitterException.Create(ex);
                if (tex != null) throw tex;
                throw;
            }
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
                var uri = CreateUri(type, url, prmArray);

                if(type != MethodType.Get && ContainsBinaryData(prmArray))
                {
                    return Request.HttpPostWithMultipartFormData(uri, prmArray,
                        CreateAuthorizationHeader(type, uri, null), options);
                }

                return type == MethodType.Get
                    ? Request.HttpGet(uri, CreateAuthorizationHeader(type, uri, null), options) :
                    Request.HttpPost(uri, prmArray, CreateAuthorizationHeader(type, uri, prmArray), options);
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
