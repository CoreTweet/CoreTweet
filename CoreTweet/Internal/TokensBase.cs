﻿// The MIT License (MIT)
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
using System.IO;
using System.Linq.Expressions;
using System.Net;
using System.Threading;

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

        /// <summary>
        /// Gets the API provider for Twitter v1.1 APIs.
        /// </summary>
        public V1.V1Api V1 => new V1.V1Api(this);

        /// <summary>
        /// Gets the API provider for Twitter v2 APIs.
        /// </summary>
        public V2.V2Api V2 => new V2.V2Api(this);

        /// <summary>
        /// Gets or sets the options of the connection.
        /// </summary>
        public ConnectionOptions ConnectionOptions { get; set; }

        #if !NETSTANDARD1_3
        internal T AccessApi<T>(MethodType type, string url, IDictionary<string,object> parameters, string jsonPath = "", string urlPrefix = null, string urlSuffix = null)
        {
            return this.AccessApiImpl<T>(type, url, parameters, jsonPath, urlPrefix, urlSuffix);
        }

        internal T AccessApiImpl<T>(MethodType type, string url, IEnumerable<KeyValuePair<string, object>> parameters, string jsonPath, string urlPrefix, string urlSuffix)
        {
            var connectionOptions = this.ConnectionOptions;

            if (urlPrefix != null || urlSuffix != null)
            {
                connectionOptions = this.ConnectionOptions.Clone();

                if (urlPrefix != null)
                {
                    connectionOptions.UrlPrefix = urlPrefix;
                }

                if (urlSuffix != null)
                {
                    connectionOptions.UrlSuffix = urlSuffix;
                }
            }

            using (var response = this.SendRequestImpl(type, InternalUtils.GetUrl(connectionOptions, url), parameters))
                return InternalUtils.ReadResponse<T>(response, jsonPath);
        }

        internal ListedResponse<T> AccessApiArray<T>(MethodType type, string url, IDictionary<string, object> parameters, string jsonPath = "", string urlPrefix = null, string urlSuffix = null)
        {
            return this.AccessApiArrayImpl<T>(type, url, parameters, jsonPath, urlPrefix, urlSuffix);
        }

        internal ListedResponse<T> AccessApiArrayImpl<T>(MethodType type, string url, IEnumerable<KeyValuePair<string, object>> parameters, string jsonPath, string urlPrefix, string urlSuffix)
        {
            var connectionOptions = this.ConnectionOptions;

            if (urlPrefix != null || urlSuffix != null)
            {
                connectionOptions = this.ConnectionOptions.Clone();

                if (urlPrefix != null)
                {
                    connectionOptions.UrlPrefix = urlPrefix;
                }

                if (urlSuffix != null)
                {
                    connectionOptions.UrlSuffix = urlSuffix;
                }
            }

            using (var response = this.SendRequestImpl(type, InternalUtils.GetUrl(connectionOptions, url), parameters))
            using (var sr = new StreamReader(response.GetResponseStream()))
            {
                var json = sr.ReadToEnd();
                var list = CoreBase.ConvertArray<T>(json, jsonPath);
                return new ListedResponse<T>(list, InternalUtils.ReadRateLimit(response), json);
            }
        }

        internal DictionaryResponse<TKey, TValue> AccessApiDictionary<TKey, TValue>(MethodType type, string url, IDictionary<string, object> parameters, string jsonPath = "", string urlPrefix = null, string urlSuffix = null)
        {
            return this.AccessApiDictionaryImpl<TKey, TValue>(type, url, parameters, jsonPath, urlPrefix, urlSuffix);
        }

        internal DictionaryResponse<TKey, TValue> AccessApiDictionaryImpl<TKey, TValue>(MethodType type, string url, IEnumerable<KeyValuePair<string, object>> parameters, string jsonPath, string urlPrefix, string urlSuffix)
        {
            var connectionOptions = this.ConnectionOptions;

            if (urlPrefix != null || urlSuffix != null)
            {
                connectionOptions = this.ConnectionOptions.Clone();

                if (urlPrefix != null)
                {
                    connectionOptions.UrlPrefix = urlPrefix;
                }

                if (urlSuffix != null)
                {
                    connectionOptions.UrlSuffix = urlSuffix;
                }
            }

            using (var response = this.SendRequestImpl(type, InternalUtils.GetUrl(connectionOptions, url), parameters))
            using (var sr = new StreamReader(response.GetResponseStream()))
            {
                var json = sr.ReadToEnd();
                var dic = CoreBase.Convert<Dictionary<TKey, TValue>>(json, jsonPath);
                return new DictionaryResponse<TKey, TValue>(dic, InternalUtils.ReadRateLimit(response), json);
            }
        }

        internal void AccessApiNoResponse(MethodType type, string url, IDictionary<string,object> parameters, string urlPrefix = null, string urlSuffix = null)
        {
            this.AccessApiNoResponseImpl(type, url, parameters, urlPrefix, urlSuffix);
        }

        internal void AccessApiNoResponseImpl(MethodType type, string url, IEnumerable<KeyValuePair<string, object>> parameters, string urlPrefix, string urlSuffix)
        {
            var connectionOptions = this.ConnectionOptions;

            if (urlPrefix != null || urlSuffix != null)
            {
                connectionOptions = this.ConnectionOptions.Clone();

                if (urlPrefix != null)
                {
                    connectionOptions.UrlPrefix = urlPrefix;
                }

                if (urlSuffix != null)
                {
                    connectionOptions.UrlSuffix = urlSuffix;
                }
            }

            this.SendRequestImpl(type, InternalUtils.GetUrl(connectionOptions, url), parameters).Close();
        }

        internal T AccessJsonParameteredApi<T>(string url, IDictionary<string, object> parameters, string[] jsonMap, string jsonPath = "", string urlPrefix = null, string urlSuffix = null)
        {
            return this.AccessJsonParameteredApiImpl<T>(url, parameters, jsonMap, jsonPath, urlPrefix, urlSuffix);
        }

        internal T AccessJsonParameteredApiImpl<T>(string url, IEnumerable<KeyValuePair<string, object>> parameters, string[] jsonMap, string jsonPath, string urlPrefix, string urlSuffix)
        {
            var connectionOptions = this.ConnectionOptions;

            if (urlPrefix != null || urlSuffix != null)
            {
                connectionOptions = this.ConnectionOptions.Clone();

                if (urlPrefix != null)
                {
                    connectionOptions.UrlPrefix = urlPrefix;
                }

                if (urlSuffix != null)
                {
                    connectionOptions.UrlSuffix = urlSuffix;
                }
            }

            using (var response = this.SendJsonRequest(InternalUtils.GetUrl(connectionOptions, url), parameters, jsonMap))
                return InternalUtils.ReadResponse<T>(response, jsonPath);
        }

        internal ListedResponse<T> AccessJsonParameteredApiArray<T>(string url, IDictionary<string, object> parameters, string[] jsonMap, string jsonPath = "", string urlPrefix = null, string urlSuffix = null)
        {
            return this.AccessJsonParameteredApiArrayImpl<T>(url, parameters, jsonMap, jsonPath, urlPrefix, urlSuffix);
        }

        internal ListedResponse<T> AccessJsonParameteredApiArrayImpl<T>(string url, IEnumerable<KeyValuePair<string, object>> parameters, string[] jsonMap, string jsonPath, string urlPrefix, string urlSuffix)
        {
            var connectionOptions = this.ConnectionOptions;

            if (urlPrefix != null || urlSuffix != null)
            {
                connectionOptions = this.ConnectionOptions.Clone();

                if (urlPrefix != null)
                {
                    connectionOptions.UrlPrefix = urlPrefix;
                }

                if (urlSuffix != null)
                {
                    connectionOptions.UrlSuffix = urlSuffix;
                }
            }

            using (var response = this.SendJsonRequest(InternalUtils.GetUrl(connectionOptions, url), parameters, jsonMap))
            using (var sr = new StreamReader(response.GetResponseStream()))
            {
                var json = sr.ReadToEnd();
                var list = CoreBase.ConvertArray<T>(json, jsonPath);
                return new ListedResponse<T>(list, InternalUtils.ReadRateLimit(response), json);
            }
        }

        internal HttpWebResponse SendJsonRequest(string fullUrl, IEnumerable<KeyValuePair<string, object>> parameters, string[] jsonMap)
        {
            return this.PostContent(fullUrl, JsonContentType, InternalUtils.MapDictToJson(parameters, jsonMap));
        }
        #endif

        internal const string JsonContentType = "application/json; charset=UTF-8";

        /// <summary>
        /// When overridden in a descendant class, creates a string for Authorization header.
        /// </summary>
        /// <param name="type">Type of HTTP request.</param>
        /// <param name="url">The URL.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>A string for Authorization header.</returns>
        public abstract string CreateAuthorizationHeader(MethodType type, Uri url, IEnumerable<KeyValuePair<string, object>> parameters);

        private static Uri CreateUri(MethodType type, string url, IEnumerable<KeyValuePair<string, object>> formattedParameters)
        {
            var ub = new UriBuilder(url);
            if (type != MethodType.Post)
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
            return Array.Exists(parameters, x =>
            {
                var v = x.Value;

                if (v is string) return false;

                return v is Stream || v is IEnumerable<byte> || v is ArraySegment<byte> || v is FileInfo
                ;
            });
        }

        #if !NETSTANDARD1_3
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
            var options = this.ConnectionOptions != null ? this.ConnectionOptions.Clone() : new ConnectionOptions();
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
                var prmArray = InternalUtils.FormatParameters(parameters);
                var uri = CreateUri(type, url, prmArray);

                if(type == MethodType.Post && ContainsBinaryData(prmArray))
                {
                    return Request.HttpPostWithMultipartFormData(uri, prmArray,
                        CreateAuthorizationHeader(type, uri, null), options);
                }

                return type == MethodType.Post
                    ? Request.HttpPost(uri, prmArray, CreateAuthorizationHeader(type, uri, prmArray), options)
                    : Request.HttpNoBody(type, uri, CreateAuthorizationHeader(type, uri, null), options);
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
