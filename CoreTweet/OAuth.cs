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
    public static class OAuth
    {
        public class OAuthSession
        {
            /// <summary>
            /// Gets or sets the consumer key.
            /// </summary>
            /// <value>The consumer key.</value>
            public string ConsumerKey { get; set; }

            /// <summary>
            /// Gets or sets the consumer secret.
            /// </summary>
            /// <value>The consumer secret.</value>
            public string ConsumerSecret { get; set; }

            /// <summary>
            /// Gets or sets the request token.
            /// </summary>
            /// <value>The request token.</value>
            public string RequestToken { get; set; }

            /// <summary>
            /// Gets or sets the request token secret.
            /// </summary>
            /// <value>The request token secret.</value>
            public string RequestTokenSecret { get; set; }

#if !PCL
            /// <summary>
            /// Gets or sets proxy information for the request.
            /// </summary>
            public IWebProxy Proxy { get; set; }
#endif

            /// <summary>
            /// Gets the authorize URL.
            /// </summary>
            /// <value>The authorize URL.</value>
            public Uri AuthorizeUri
            {
                get
                {
                    return new Uri(AuthorizeUrl + "?oauth_token=" + RequestToken);
                }
            }
        }

        /// <summary>
        /// The request token URL.
        /// </summary>
        static readonly string RequestTokenUrl = "https://api.twitter.com/oauth/request_token";
        /// <summary>
        /// The access token URL.
        /// </summary>
        static readonly string AccessTokenUrl = "https://api.twitter.com/oauth/access_token";
        /// <summary>
        /// The authorize URL.
        /// </summary>
        static readonly string AuthorizeUrl = "https://api.twitter.com/oauth/authorize";

#if !PCL
        /// <summary>
        ///     Generates the authorize URI.
        ///     Then call GetTokens(string) after get the pin code.
        /// </summary>
        /// <returns>
        ///     The authorize URI.
        /// </returns>
        /// <param name="consumerKey">
        ///     Consumer key.
        /// </param>
        /// <param name="consumerSecret">
        ///     Consumer secret.
        /// </param>
        /// <param name="oauthCallback">
        ///     <para>For OAuth 1.0a compliance this parameter is required. The value you specify here will be used as the URL a user is redirected to should they approve your application's access to their account. Set this to oob for out-of-band pin mode. This is also how you specify custom callbacks for use in desktop/mobile applications.</para>
        ///     <para>Always send an oauth_callback on this step, regardless of a pre-registered callback.</para>
        /// </param>
        /// <param name="proxy">
        ///     Proxy information for the request.
        /// </param>
        public static OAuthSession Authorize(string consumerKey, string consumerSecret, string oauthCallback = null, IWebProxy proxy = null)
        {
            // Note: Twitter says,
            // "If you're using HTTP-header based OAuth, you shouldn't include oauth_* parameters in the POST body or querystring."
            var prm = new Dictionary<string,object>();
            if (!string.IsNullOrEmpty(oauthCallback))
                prm.Add("oauth_callback", oauthCallback);
            var header = Tokens.Create(consumerKey, consumerSecret, null, null)
                .CreateAuthorizationHeader(MethodType.Get, RequestTokenUrl, prm);
            var dic = from x in Request.HttpGet(RequestTokenUrl, prm, header, "CoreTweet", proxy).Use()
                      from y in new StreamReader(x).Use()
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
                Proxy = proxy
            };
        }

        /// <summary>
        ///     Gets the OAuth tokens.
        ///     Be sure to call GenerateAuthUri(string,string) before call this.
        /// </summary>
        /// <param name='pin'>
        ///     Pin code.
        /// </param>
        /// <param name='session'>
        ///     OAuth session.
        /// </para>
        /// <returns>
        ///     The tokens.
        /// </returns>
        public static Tokens GetTokens(this OAuthSession session, string pin)
        {
            var prm = new Dictionary<string,object>() { { "oauth_verifier", pin } };
            var header = Tokens.Create(session.ConsumerKey, session.ConsumerSecret, session.RequestToken, session.RequestTokenSecret)
                .CreateAuthorizationHeader(MethodType.Get, AccessTokenUrl, prm);
            var dic = from x in Request.HttpGet(AccessTokenUrl, prm, header, "CoreTweet", session.Proxy).Use()
                      from y in new StreamReader(x).Use()
                      select y.ReadToEnd()
                              .Split('&')
                              .Where(z => z.Contains('='))
                              .Select(z => z.Split('='))
                              .ToDictionary(z => z[0], z => z[1]);
            var t = Tokens.Create(session.ConsumerKey, session.ConsumerSecret,
                dic["oauth_token"], dic["oauth_token_secret"], long.Parse(dic["user_id"]), dic["screen_name"]);
            t.Proxy = session.Proxy;
            return t;
        }
#endif
    }

    public static class OAuth2
    {
        /// <summary>
        /// The access token URL.
        /// </summary>
        static readonly string AccessTokenUrl = "https://api.twitter.com/oauth2/token";
        /// <summary>
        /// The URL to revoke a OAuth2 Bearer Token.
        /// </summary>
        static readonly string InvalidateTokenUrl = "https://api.twitter.com/oauth2/invalidate_token";

        private static string CreateCredentials(string consumerKey, string consumerSecret)
        {
            return "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes(consumerKey + ":" + consumerSecret));
        }

#if !PCL
        /// <summary>
        /// Gets the OAuth 2 Bearer Token.
        /// </summary>
        /// <param name="consumerKey">Consumer key.</param>
        /// <param name="consumerSecret">Consumer secret.</param>
        /// <param name="proxy">Proxy information for the request.</param>
        /// <returns>The tokens.</returns>
        public static OAuth2Token GetToken(string consumerKey, string consumerSecret, IWebProxy proxy = null)
        {
            var token = from x in Request.HttpPost(
                            AccessTokenUrl,
                            new Dictionary<string,object>() { { "grant_type", "client_credentials" } }, //  At this time, only client_credentials is allowed.
                            CreateCredentials(consumerKey, consumerSecret),
                            "CoreTweet",
                            proxy,
                            true).Use()
                        from y in new StreamReader(x).Use()
                        select (string)JObject.Parse(y.ReadToEnd())["access_token"];
            var t = OAuth2Token.Create(consumerKey, consumerSecret, token);
            t.Proxy = proxy;
            return t;
        }

        /// <summary>
        /// Invalidates the OAuth 2 Bearer Token.
        /// </summary>
        /// <param name="tokens">An instance of OAuth2Tokens.</param>
        /// <returns>The invalidated token.</returns>
        public static string InvalidateToken(this OAuth2Token tokens)
        {
            return from x in Request.HttpPost(
                       InvalidateTokenUrl,
                       new Dictionary<string,object>() { { "access_token", Uri.UnescapeDataString(tokens.BearerToken) } },
                       CreateCredentials(tokens.ConsumerKey, tokens.ConsumerSecret),
                       tokens.UserAgent,
                       tokens.Proxy,
                       true).Use()
                   from y in new StreamReader(x).Use()
                   select (string)JObject.Parse(y.ReadToEnd())["access_token"];
        }
#endif
    }
}

