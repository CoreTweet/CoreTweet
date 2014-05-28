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
    /// The OAuth tokens
    /// </summary>
    public abstract partial class TokensBase
    {
        public TokensBase()
        {
            this.ConnectionOptions = new ConnectionOptions();
        }

        /// <summary>
        /// The consumer key.
        /// </summary>
        public string ConsumerKey { get; set; }
        /// <summary>
        /// The consumer secret.
        /// </summary>
        public string ConsumerSecret { get; set; }

        #region Endpoints for Twitter API

        /// <summary>
        /// Rest/Account
        /// </summary>
        public Account Account { get { return new Account(this); } }
        /// <summary>
        /// Rest/Blocks
        /// </summary>
        public Blocks Blocks { get { return new Blocks(this); } }
        /// <summary>
        /// Rest/Direct messages.
        /// </summary>
        public DirectMessages DirectMessages { get { return new DirectMessages(this); } }
        /// <summary>
        /// Rest/Favorites.
        /// </summary>
        public Favorites Favorites { get { return new Favorites(this); } }
        /// <summary>
        /// Rest/Friends.
        /// </summary>
        public Friends Friends { get { return new Friends(this); } }
        /// <summary>
        /// Rest/Followers
        /// </summary>
        public Followers Followers { get { return new Followers(this); } }
        /// <summary>
        /// Rest/Friendships.
        /// </summary>
        public Friendships Friendships { get { return new Friendships(this); } }
        /// <summary>
        /// Rest/Geo.
        /// </summary>
        public Geo Geo { get { return new Geo(this); } }
        /// <summary>
        /// Rest/Help.
        /// </summary>
        public Help Help { get { return new Help(this); } }
        /// <summary>
        /// Rest/Lists.
        /// </summary>
        public Lists Lists { get { return new Lists(this); } }
        /// <summary>
        /// Rest/Media.
        /// </summary>
        public Media Media { get { return new Media(this); } }
        /// <summary>
        /// Rest/Mutes.
        /// </summary>
        public Mutes Mutes { get { return new Mutes(this); } }
        /// <summary>
        /// Rest/Search.
        /// </summary>
        public Search Search { get { return new Search(this); } }
        /// <summary>
        /// Rest/Saved searches.
        /// </summary>
        public SavedSearches SavedSearches { get { return new SavedSearches(this); } }
        /// <summary>
        /// Rest/Statuses.
        /// </summary>
        public Statuses Statuses { get { return new Statuses(this); } }
        /// <summary>
        /// Rest/Trends.
        /// </summary>
        public Trends Trends { get { return new Trends(this); } }
        /// <summary>
        /// Rest/Users.
        /// </summary>
        public Users Users { get { return new Users(this); } }
        /// <summary>
        /// Streaming API.
        /// </summary>
        public StreamingApi Streaming { get { return new StreamingApi(this); } }

        #endregion

        /// <summary>
        /// Gets or sets options of connection.
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
                var result = CoreBase.Convert<T>(this, sr.ReadToEnd(), jsonPath);
                var twitterResponse = result as ITwitterResponse;
                if(twitterResponse != null)
                    twitterResponse.RateLimit = InternalUtils.ReadRateLimit(response);
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
                var list = CoreBase.ConvertArray<T>(this, sr.ReadToEnd(), jsonPath);
                return new ListedResponse<T>(list, InternalUtils.ReadRateLimit(response));
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
        /// When overridden in a descendant class, creates string for Authorization header.
        /// </summary>
        /// <param name="type">Type of HTTP request.</param>
        /// <param name="url">URL.</param>
        /// <param name="parameters">Parameters.</param>
        /// <returns>String for Authorization header.</returns>
        public abstract string CreateAuthorizationHeader(MethodType type, string url, IEnumerable<KeyValuePair<string, object>> parameters);

        private static KeyValuePair<string, object>[] CollectionToCommaSeparatedString(IEnumerable<KeyValuePair<string, object>> parameters)
        {
            return parameters != null ? parameters.Select(kvp =>
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
            ).ToArray() : new KeyValuePair<string, object>[] { };
        }

#if !(PCL || WIN_RT || WP)
        /// <summary>
        /// Sends a request to the specified url with the specified parameters.
        /// </summary>
        /// <returns>
        /// The stream.
        /// </returns>
        /// <param name='type'>
        /// Type of HTTP request.
        /// </param>
        /// <param name='url'>
        /// URL.
        /// </param>
        /// <param name='parameters'>
        /// Parameters.
        /// </param>
        public HttpWebResponse SendRequest(MethodType type, string url, params Expression<Func<string,object>>[] parameters)
        {
            return this.SendRequestImpl(type, url, InternalUtils.ExpressionsToDictionary(parameters), this.ConnectionOptions);
        }

        /// <summary>
        /// Sends a request to the specified url with the specified parameters.
        /// </summary>
        /// <returns>
        /// The stream.
        /// </returns>
        /// <param name='type'>
        /// Type of HTTP request.
        /// </param>
        /// <param name='url'>
        /// URL.
        /// </param>
        /// <param name='parameters'>
        /// Parameters.
        /// </param>
        public HttpWebResponse SendRequest<T>(MethodType type, string url, T parameters)
        {
            return this.SendRequestImpl(type, url, InternalUtils.ResolveObject(parameters), this.ConnectionOptions);
        }

        /// <summary>
        /// Sends a request to the specified url with the specified parameters.
        /// </summary>
        /// <returns>
        /// The stream.
        /// </returns>
        /// <param name='type'>
        /// Type of HTTP request.
        /// </param>
        /// <param name='url'>
        /// URL.
        /// </param>
        /// <param name='parameters'>
        /// Parameters.
        /// </param>
        public HttpWebResponse SendRequest(MethodType type, string url, IDictionary<string, object> parameters)
        {
            return this.SendRequestImpl(type, url, parameters, this.ConnectionOptions);
        }

        public HttpWebResponse SendStreamingRequest(MethodType type, string url, IEnumerable<KeyValuePair<string, object>> parameters)
        {
            var options = this.ConnectionOptions != null ? (ConnectionOptions)this.ConnectionOptions.Clone() : new ConnectionOptions();
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
