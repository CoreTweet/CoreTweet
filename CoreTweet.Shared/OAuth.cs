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
using System.Net;
using System.Text;
using Newtonsoft.Json.Linq;

namespace CoreTweet
{
    /// <summary>
    /// Provides a set of static (Shared in Visual Basic) methods for OAuth authentication.
    /// </summary>
    public static partial class OAuth
    {
        /// <summary>
        /// Represents an OAuth session.
        /// </summary>
        public class OAuthSession
        {
            /// <summary>
            /// Gets or sets the consumer key.
            /// </summary>
            public string ConsumerKey { get; set; }

            /// <summary>
            /// Gets or sets the consumer secret.
            /// </summary>
            public string ConsumerSecret { get; set; }

            /// <summary>
            /// Gets or sets the request token.
            /// </summary>
            public string RequestToken { get; set; }

            /// <summary>
            /// Gets or sets the request token secret.
            /// </summary>
            public string RequestTokenSecret { get; set; }

            /// <summary>
            /// Gets or sets the options of the connection.
            /// </summary>
            public ConnectionOptions ConnectionOptions { get; set; }

            /// <summary>
            /// Gets the authorize URL.
            /// </summary>
            public Uri AuthorizeUri
            {
                get
                {
                    return new Uri(AuthorizeUrl + "?oauth_token=" + RequestToken);
                }
            }
        }

        static readonly string RequestTokenUrl = "https://api.twitter.com/oauth/request_token";
        static readonly string AccessTokenUrl = "https://api.twitter.com/oauth/access_token";
        static readonly string AuthorizeUrl = "https://api.twitter.com/oauth/authorize";

#if !(PCL || WIN_RT || WP)
        /// <summary>
        /// <para>Generates the authorize URI.</para>
        /// <para>Then call <see cref="CoreTweet.OAuth.GetTokens"/> after get the pin code.</para>
        /// </summary>
        /// <param name="consumerKey">The Consumer key.</param>
        /// <param name="consumerSecret">The Consumer secret.</param>
        /// <param name="oauthCallback">
        /// <para>For OAuth 1.0a compliance this parameter is required.</para>
        /// <para>The value you specify here will be used as the URL a user is redirected to should they approve your application's access to their account.</para>
        /// <para>Set this to oob for out-of-band pin mode.</para>
        /// <para>This is also how you specify custom callbacks for use in desktop/mobile applications.</para>
        /// <para>Always send an oauth_callback on this step, regardless of a pre-registered callback.</para>
        /// </param>
        /// <param name="options">The connection options for the request.</param>
        /// <returns>The authorize URI.</returns>
        public static OAuthSession Authorize(string consumerKey, string consumerSecret, string oauthCallback = "oob", ConnectionOptions options = null)
        {
            // Note: Twitter says,
            // "If you're using HTTP-header based OAuth, you shouldn't include oauth_* parameters in the POST body or querystring."
            var prm = new Dictionary<string,object>();
            if(!string.IsNullOrEmpty(oauthCallback))
                prm.Add("oauth_callback", oauthCallback);
            var header = Tokens.Create(consumerKey, consumerSecret, null, null)
                .CreateAuthorizationHeader(MethodType.Get, RequestTokenUrl, prm);
            try
            {
                var dic = from x in Request.HttpGet(RequestTokenUrl, prm, header, options).Use()
                          from y in new StreamReader(x.GetResponseStream()).Use()
                          select y.ReadToEnd()
                                  .Split('&')
                                  .Where(z => z.Contains('='))
                                  .Select(z => z.Split('='))
                                  .ToDictionary(z => z[0], z => z[1]);
                return new OAuthSession()
                {
                    RequestToken = dic["oauth_token"],
                    RequestTokenSecret = dic["oauth_token_secret"],
                    ConsumerKey = consumerKey,
                    ConsumerSecret = consumerSecret,
                    ConnectionOptions = options
                };
            }
            catch(WebException ex)
            {
                var tex = TwitterException.Create(ex);
                if(tex != null)
                    throw tex;
                throw;
            }
        }

        /// <summary>
        /// <para>Gets the OAuth tokens.</para>
        /// <para>Be sure to call <see cref="CoreTweet.OAuth.Authorize"/> before call this method.</para>
        /// </summary>
        /// <param name="session">The OAuth session.</param>
        /// <param name="pin">The pin code.</param>
        /// <returns>The tokens.</returns>
        public static Tokens GetTokens(this OAuthSession session, string pin)
        {
            var prm = new Dictionary<string,object>() { { "oauth_verifier", pin } };
            var header = Tokens.Create(session.ConsumerKey, session.ConsumerSecret, session.RequestToken, session.RequestTokenSecret)
                .CreateAuthorizationHeader(MethodType.Get, AccessTokenUrl, prm);
            try
            {
                var dic = from x in Request.HttpGet(AccessTokenUrl, prm, header, session.ConnectionOptions).Use()
                          from y in new StreamReader(x.GetResponseStream()).Use()
                          select y.ReadToEnd()
                                  .Split('&')
                                  .Where(z => z.Contains('='))
                                  .Select(z => z.Split('='))
                                  .ToDictionary(z => z[0], z => z[1]);
                var t = Tokens.Create(session.ConsumerKey, session.ConsumerSecret,
                    dic["oauth_token"], dic["oauth_token_secret"], long.Parse(dic["user_id"]), dic["screen_name"]);
                t.ConnectionOptions = session.ConnectionOptions;
                return t;
            }
            catch(WebException ex)
            {
                var tex = TwitterException.Create(ex);
                if(tex != null)
                    throw tex;
                throw;
            }
        }
#endif
    }

    /// <summary>
    /// Provides a set of static (Shared in Visual Basic) methods for OAuth 2 Authentication.
    /// </summary>
    public static partial class OAuth2
    {
        static readonly string AccessTokenUrl = "https://api.twitter.com/oauth2/token";
        static readonly string InvalidateTokenUrl = "https://api.twitter.com/oauth2/invalidate_token";

        private static string CreateCredentials(string consumerKey, string consumerSecret)
        {
            return "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes(consumerKey + ":" + consumerSecret));
        }

#if !(PCL || WIN_RT || WP)
        /// <summary>
        /// Gets the OAuth 2 Bearer Token.
        /// </summary>
        /// <param name="consumerKey">The consumer key.</param>
        /// <param name="consumerSecret">The consumer secret.</param>
        /// <param name="options">The connection options for the request.</param>
        /// <returns>The tokens.</returns>
        public static OAuth2Token GetToken(string consumerKey, string consumerSecret, ConnectionOptions options = null)
        {
            try
            {
                var token = from x in Request.HttpPost(
                                AccessTokenUrl,
                                new Dictionary<string, object>() { { "grant_type", "client_credentials" } }, //  At this time, only client_credentials is allowed.
                                CreateCredentials(consumerKey, consumerSecret),
                                options).Use()
                            from y in new StreamReader(x.GetResponseStream()).Use()
                            select (string)JObject.Parse(y.ReadToEnd())["access_token"];
                var t = OAuth2Token.Create(consumerKey, consumerSecret, token);
                t.ConnectionOptions = options;
                return t;
            }
            catch(WebException ex)
            {
                var tex = TwitterException.Create(ex);
                if(tex != null)
                    throw tex;
                throw;
            }
        }

        /// <summary>
        /// Invalidates the OAuth 2 Bearer Token.
        /// </summary>
        /// <param name="tokens">An instance of <see cref="CoreTweet.OAuth2Token"/>.</param>
        /// <returns>The invalidated token.</returns>
        public static string InvalidateToken(this OAuth2Token tokens)
        {
            try
            {
                return from x in Request.HttpPost(
                           InvalidateTokenUrl,
                           new Dictionary<string, object>() { { "access_token", Uri.UnescapeDataString(tokens.BearerToken) } },
                           CreateCredentials(tokens.ConsumerKey, tokens.ConsumerSecret),
                           tokens.ConnectionOptions).Use()
                       from y in new StreamReader(x.GetResponseStream()).Use()
                       select (string)JObject.Parse(y.ReadToEnd())["access_token"];
            }
            catch(WebException ex)
            {
                var tex = TwitterException.Create(ex);
                if(tex != null)
                    throw tex;
                throw;
            }
        }
#endif
    }
}

