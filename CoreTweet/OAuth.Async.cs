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
using System.Threading;
using System.Threading.Tasks;
using CoreTweet.Core;

namespace CoreTweet
{
    partial class OAuth
    {
        /// <summary>
        /// <para>Generates the authorize URI as an asynchronous operation.</para>
        /// <para>Then call <see cref="CoreTweet.OAuth.GetTokensAsync"/> after get the pin code.</para>
        /// </summary>
        /// <param name="consumerKey">The consumer key.</param>
        /// <param name="consumerSecret">The consumer secret.</param>
        /// <param name="oauthCallback">
        /// <para>For OAuth 1.0a compliance this parameter is required.</para>
        /// <para>The value you specify here will be used as the URL a user is redirected to should they approve your application's access to their account.</para>
        /// <para>Set this to oob for out-of-band pin mode.</para>
        /// <para>This is also how you specify custom callbacks for use in desktop/mobile applications.</para>
        /// <para>Always send an oauth_callback on this step, regardless of a pre-registered callback.</para>
        /// </param>
        /// <param name="options">The connection options for the request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the authorize URI.</para>
        /// </returns>
        public static Task<OAuthSession> AuthorizeAsync(string consumerKey, string consumerSecret, string oauthCallback = "oob", ConnectionOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            var prm = new Dictionary<string, object>();
            if(!string.IsNullOrEmpty(oauthCallback))
                prm.Add("oauth_callback", oauthCallback);
            var header = Tokens.Create(consumerKey, consumerSecret, null, null)
                .CreateAuthorizationHeader(MethodType.Get, RequestTokenUrl, prm);
            return Request.HttpGetAsync(RequestTokenUrl, prm, header, options, cancellationToken)
                .ContinueWith(new Func<Task<AsyncResponse>, Task<AsyncResponse>>(InternalUtils.ResponseCallback), cancellationToken)
                .Unwrap()
                .ContinueWith(
                    t => InternalUtils.ReadResponse(t, s =>
                    {
                        var dic = s.Split('&')
                            .Where(z => z.Contains("="))
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
                    }, cancellationToken),
                    cancellationToken
                ).Unwrap().CheckCanceled(cancellationToken);
        }

        /// <summary>
        /// <para>Gets the OAuth tokens as an asynchronous operation.</para>
        /// <para>Be sure to call <see cref="CoreTweet.OAuth.AuthorizeAsync"/> before call this method.</para>
        /// </summary>
        /// <param name="session">The OAuth session.</param>
        /// <param name="pin">The pin code.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the tokens.</para>
        /// </returns>
        public static Task<Tokens> GetTokensAsync(this OAuthSession session, string pin, CancellationToken cancellationToken = default(CancellationToken))
        {
            var prm = new Dictionary<string, object>() { { "oauth_verifier", pin } };
            var header = Tokens.Create(session.ConsumerKey, session.ConsumerSecret, session.RequestToken, session.RequestTokenSecret)
                .CreateAuthorizationHeader(MethodType.Get, AccessTokenUrl, prm);
            return Request.HttpGetAsync(AccessTokenUrl, prm, header, session.ConnectionOptions, cancellationToken)
                .ContinueWith(new Func<Task<AsyncResponse>, Task<AsyncResponse>>(InternalUtils.ResponseCallback), cancellationToken)
                .Unwrap()
                .ContinueWith(
                    t => InternalUtils.ReadResponse(t, s =>
                    {
                        var dic = s.Split('&')
                            .Where(z => z.Contains("="))
                            .Select(z => z.Split('='))
                            .ToDictionary(z => z[0], z => z[1]);
                        var token = Tokens.Create(session.ConsumerKey, session.ConsumerSecret,
                            dic["oauth_token"], dic["oauth_token_secret"], long.Parse(dic["user_id"]), dic["screen_name"]);
                        token.ConnectionOptions = session.ConnectionOptions;
                        return token;
                    }, cancellationToken),
                    cancellationToken
                ).Unwrap().CheckCanceled(cancellationToken);
        }
    }

    partial class OAuth2
    {
        /// <summary>
        /// Gets the OAuth 2 Bearer Token as an asynchronous operation.
        /// </summary>
        /// <param name="consumerKey">The consumer key.</param>
        /// <param name="consumerSecret">The consumer secret.</param>
        /// <param name="options">The connection options for the request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the tokens.</para>
        /// </returns>
        public static Task<OAuth2Token> GetTokenAsync(string consumerKey, string consumerSecret, ConnectionOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            return
                Request.HttpPostAsync(
                    AccessTokenUrl,
                    new Dictionary<string, object>() { { "grant_type", "client_credentials" } },
                    CreateCredentials(consumerKey, consumerSecret),
                    options,
                    cancellationToken
                )
                .ContinueWith(new Func<Task<AsyncResponse>, Task<AsyncResponse>>(InternalUtils.ResponseCallback), cancellationToken)
                .Unwrap()
                .ContinueWith(
                    t => InternalUtils.ReadResponse(t, s =>
                    {
                        var token = OAuth2Token.Create(
                            consumerKey, consumerSecret,
                            (string)JObject.Parse(s)["access_token"]
                        );
                        token.ConnectionOptions = options;
                        return token;
                    }, cancellationToken),
                    cancellationToken
                ).Unwrap().CheckCanceled(cancellationToken);
        }

        /// <summary>
        /// Invalidates the OAuth 2 Bearer Token as an asynchronous operation.
        /// </summary>
        /// <param name="tokens">An instance of OAuth2Tokens.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the invalidated token.</para>
        /// </returns>
        public static Task<string> InvalidateTokenAsync(this OAuth2Token tokens, CancellationToken cancellationToken = default(CancellationToken))
        {
            return
                Request.HttpPostAsync(
                    InvalidateTokenUrl,
                    new Dictionary<string, object>() { { "access_token", Uri.UnescapeDataString(tokens.BearerToken) } },
                    CreateCredentials(tokens.ConsumerKey, tokens.ConsumerSecret),
                    tokens.ConnectionOptions,
                    cancellationToken
                )
                .ContinueWith(new Func<Task<AsyncResponse>, Task<AsyncResponse>>(InternalUtils.ResponseCallback), cancellationToken)
                .Unwrap()
                .ContinueWith(
                    t => InternalUtils.ReadResponse(t, s => (string)JObject.Parse(s)["access_token"], cancellationToken),
                    cancellationToken
                ).Unwrap().CheckCanceled(cancellationToken);
        }
    }
}
