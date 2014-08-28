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
using System.Linq;
using CoreTweet.Core;

namespace CoreTweet
{
    /// <summary>
    /// Represents the OAuth tokens.
    /// </summary>
    public class Tokens : TokensBase
    {
        /// <summary>
        /// Gets or sets the access token.
        /// </summary>
        public string AccessToken { get; set; }
        /// <summary>
        /// Gets or sets the access token secret.
        /// </summary>
        public string AccessTokenSecret { get; set; }
        /// <summary>
        /// Gets or sets the user ID.
        /// </summary>
        public long UserId { get; set; }
        /// <summary>
        /// Gets or sets the screen name.
        /// </summary>
        public string ScreenName { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CoreTweet.Tokens"/> class.
        /// </summary>
        public Tokens() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CoreTweet.Tokens"/> class with a specified token.
        /// </summary>
        /// <param name="e">The token.</param>
        public Tokens(Tokens e) : this()
        {
            this.ConsumerKey = e.ConsumerKey;
            this.ConsumerSecret = e.ConsumerSecret;
            this.AccessToken = e.AccessToken;
            this.AccessTokenSecret = e.AccessTokenSecret;
            this.UserId = e.UserId;
            this.ScreenName = e.ScreenName;
        }

        /// <summary>
        /// Creates a string for Authorization header for OAuth 1.0A.
        /// </summary>
        /// <param name="type">The Type of HTTP request.</param>
        /// <param name="url">The URL.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>A string for Authorization header.</returns>
        public override string CreateAuthorizationHeader(MethodType type, string url, IEnumerable<KeyValuePair<string, object>> parameters)
        {
            var prms = Request.GenerateParameters(this.ConsumerKey, this.AccessToken);
            var sigPrms = parameters != null
                ? prms.Concat(parameters.Select(p => new KeyValuePair<string, string>(p.Key, p.Value.ToString())))
                : prms;
            var sgn = Request.GenerateSignature(this, type == MethodType.Get ? "GET" : "POST", url, sigPrms);
            prms.Add("oauth_signature", sgn);
            return "OAuth " + prms.Select(p => String.Format(@"{0}=""{1}""", Request.UrlEncode(p.Key), Request.UrlEncode(p.Value))).JoinToString(",");
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
        /// Makes an instance of Tokens.
        /// </summary>
        /// <param name="consumerKey">The consumer key.</param>
        /// <param name="consumerSecret">The consumer secret.</param>
        /// <param name="accessToken">The access token.</param>
        /// <param name="accessSecret">The access secret.</param>
        /// <param name="userID">The user's ID.</param>
        /// <param name="screenName">The user's screen name.</param>
        /// <returns>The tokens.</returns>
        public static Tokens Create(string consumerKey, string consumerSecret, string accessToken, string accessSecret, long userID = 0, string screenName = null)
        {
            return new Tokens()
            {
                ConsumerKey  = consumerKey,
                ConsumerSecret = consumerSecret,
                AccessToken = accessToken,
                AccessTokenSecret = accessSecret,
                UserId = userID,
                ScreenName = screenName
            };
        }
    }
}
