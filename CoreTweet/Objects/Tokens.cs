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
using System.Linq.Expressions;
using System.Collections.Generic;
using System.IO;
using System.Net;
using CoreTweet.Core;
using CoreTweet.Rest;
using CoreTweet.Streaming;

namespace CoreTweet
{
    /// <summary>
    /// The OAuth tokens.
    /// </summary>
    public class Tokens
    {
        /// <summary>
        /// The consumer key.
        /// </summary>
        public string ConsumerKey { get; set; }
        /// <summary>
        /// The consumer secret.
        /// </summary>
        public string ConsumerSecret { get; set; }
        /// <summary>
        /// The access token.
        /// </summary>
        public string AccessToken { get; set; }
        /// <summary>
        /// The access token secret.
        /// </summary> 
        public string AccessTokenSecret { get; set; }
        
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
        public StreamingApi Streaming{ get { return new StreamingApi(this); } }
        
        #endregion
        
        public Tokens() { }
        
        public Tokens(Tokens e) : this()
        {
            this.ConsumerKey = e.ConsumerKey;
            this.ConsumerSecret = e.ConsumerSecret;
            this.AccessToken = e.AccessToken;
            this.AccessTokenSecret = e.AccessTokenSecret;
        }
        
        internal T AccessApi<T>(MethodType type, string url, params Expression<Func<string,object>>[] parameters)
            where T : CoreBase
        {
            return this.AccessApi<T>(type, url, parameters.ToDictionary(e => e.Parameters[0].Name, e => e.Compile()("")));
        }
        
        internal T AccessApi<T>(MethodType type, string url, IDictionary<string,object> parameters)
            where T : CoreBase
        {
            using(var s = this.SendRequest(type, Url(url), parameters))
            using(var sr = new StreamReader(s))
                return CoreBase.Convert<T>(this, sr.ReadToEnd());
        }
        
        internal IEnumerable<T> AccessApiArray<T>(MethodType type, string url, params Expression<Func<string,object>>[] parameters)
            where T : CoreBase
        {
            return this.AccessApiArray<T>(type, url, parameters.ToDictionary(e => e.Parameters[0].Name, e => e.Compile()("")));
        }
        
        internal IEnumerable<T> AccessApiArray<T>(MethodType type, string url, IDictionary<string,object> parameters)
            where T : CoreBase
        {
            using(var s = this.SendRequest(type, Url(url), parameters))
            using(var sr = new StreamReader(s))
                return CoreBase.ConvertArray<T>(this, sr.ReadToEnd());
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
                    var prms = Request.GenerateParameters(this.ConsumerKey, this.AccessToken);
                    var sgn = Request.GenerateSignature(this, "POST", url, prms);
                    prms.Add("oauth_signature", sgn);
                    return Request.HttpPostWithMultipartFormData(url, parameters, prms, type == MethodType.Post);
                }
                else
                {
                    var prms = Request.GenerateParameters(this.ConsumerKey, this.AccessToken);
                    foreach(var p in parameters)
                        prms.Add(p.Key, Request.UrlEncode(p.Value.ToString()));
                    var sgn = Request.GenerateSignature(this,
                        type == MethodType.Get ? "GET" : "POST", url, prms);
                    prms.Add("oauth_signature", Request.UrlEncode(sgn));
                    return type == MethodType.Get ? Request.HttpGet(url, prms) :
                        type == MethodType.Post ? Request.HttpPost(url, prms, true) : Request.HttpPost(url, prms, false);
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
        
        /// <summary>
        /// Returns a <see cref="System.String"/> that represents the current <see cref="CoreTweet.Tokens"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents the current <see cref="CoreTweet.Tokens"/>.
        /// </returns>
        public override string ToString()
        {
            return string.Format("oauth_token={0}&oauth_token_secret={1}&oauth_consumer_key={2}&oauth_consumer_secret={3}", 
                                 this.AccessToken, this.AccessTokenSecret, this.ConsumerKey, this.ConsumerSecret);
        }
        
        /// <summary>
        /// Make an instance of Tokens.
        /// </summary>
        /// <param name='consumer_key'>
        /// Consumer key.
        /// </param>
        /// <param name='consumer_secret'>
        /// Consumer secret.
        /// </param>
        /// <param name='access_token'>
        /// Access token.
        /// </param>
        /// <param name='access_secret'>
        /// Access secret.
        /// </param>
        public static Tokens Create(string consumerKey, string consumerSecret, string accessToken, string accessSecret)
        {
            return new Tokens()
            {
                ConsumerKey  = consumerKey,
                ConsumerSecret = consumerSecret,
                AccessToken = accessToken,
                AccessTokenSecret = accessSecret
            };
        }
        
        /// <summary>
        /// Gets the url of the specified api's name.
        /// </summary>
        /// <returns>The url.</returns>
        internal static string Url(string apiName)
        {
            return string.Format("https://api.twitter.com/{0}/{1}.json", Property.ApiVersion, apiName);
        }
    }
}
