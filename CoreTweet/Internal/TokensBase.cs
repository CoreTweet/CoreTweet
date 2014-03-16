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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using CoreTweet.Core;
using CoreTweet.Rest;
using CoreTweet.Streaming;

namespace CoreTweet.Core
{
    /// <summary>
    /// The OAuth tokens
    /// </summary>
    public abstract class TokensBase
    {
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

        internal T AccessApi<T>(MethodType type, string url, Expression<Func<string,object>>[] parameters, string jsonPath = "")
        {
            return this.AccessApi<T>(type, url, InternalUtils.ExpressionsToDictionary(parameters), jsonPath);
        }

        internal T AccessApi<T, TV>(MethodType type, string url, TV parameters, string jsonPath = "")
        {
            return this.AccessApi<T>(type, url, InternalUtils.ResolveObject(parameters), jsonPath);
        }

        internal T AccessApi<T>(MethodType type, string url, IDictionary<string,object> parameters, string jsonPath = "")
        {
            using(var s = this.SendRequest(type, InternalUtils.GetUrl(url), parameters))
            using(var sr = new StreamReader(s))
                return CoreBase.Convert<T>(this, sr.ReadToEnd(), jsonPath);
        }

        internal IEnumerable<T> AccessApiArray<T>(MethodType type, string url, Expression<Func<string,object>>[] parameters, string jsonPath = "")
        {
            return this.AccessApiArray<T>(type, url, InternalUtils.ExpressionsToDictionary(parameters), jsonPath);
        }

        internal IEnumerable<T> AccessApiArray<T, TV>(MethodType type, string url, TV parameters, string jsonPath = "")
        {
            return this.AccessApiArray<T>(type, url, InternalUtils.ResolveObject(parameters), jsonPath);
        }

        internal IEnumerable<T> AccessApiArray<T>(MethodType type, string url, IDictionary<string,object> parameters, string jsonPath = "")
        {
            using(var s = this.SendRequest(type, InternalUtils.GetUrl(url), parameters))
            using(var sr = new StreamReader(s))
                return CoreBase.ConvertArray<T>(this, sr.ReadToEnd(), jsonPath);
        }

        internal abstract string CreateAuthorizationHeader(MethodType type, string url, IDictionary<string,object> parameters); 

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
        public Stream SendRequest(MethodType type, string url, params Expression<Func<string,object>>[] parameters)
        {
            return this.SendRequest(type, url, parameters.ToDictionary(e => e.Parameters[0].Name, e => e.Compile()("")));
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
        public Stream SendRequest(MethodType type, string url, IDictionary<string,object> parameters)
        {
            try
            {
                if(type != MethodType.Get && parameters.Values.Any(x => x is Stream || x is IEnumerable<byte> || x is FileInfo))
                {
                    return Request.HttpPostWithMultipartFormData(url, parameters,
                        CreateAuthorizationHeader(type, url, null), type == MethodType.Post);
                }
                else
                {
                    var header = CreateAuthorizationHeader(type, url, parameters);
                    return type == MethodType.Get ? Request.HttpGet(url, parameters, header) :
                        type == MethodType.Post ? Request.HttpPost(url, parameters, header, true) : Request.HttpPost(url, parameters, header, false);
                }
            }
            catch(WebException ex)
            {
                var tex = TwitterException.Create(this, ex);
                if(tex != null)
                    throw tex;
                else
                    throw;
            }
        }
    }
}
