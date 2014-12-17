using System.Collections.Generic;
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
using CoreTweet.Core;

namespace CoreTweet
{
    /// <summary>
    /// The OAuth2 token, which is usually used for Application-only authentication.
    /// </summary>
    public class OAuth2Token : TokensBase
    {
        /// <summary>
        /// Gets or sets the access token.
        /// </summary>
        public string BearerToken { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CoreTweet.OAuth2Token"/> class.
        /// </summary>
        public OAuth2Token() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CoreTweet.OAuth2Token"/> class with a specified token.
        /// </summary>
        /// <param name="e">The token.</param>
        public OAuth2Token(OAuth2Token e)
            : this()
        {
            this.ConsumerKey = e.ConsumerKey;
            this.ConsumerSecret = e.ConsumerSecret;
            this.BearerToken = e.BearerToken;
        }

        /// <summary>
        /// Creates a string for Authorization header including bearer token.
        /// </summary>
        /// <param name="type">The type of the HTTP request.</param>
        /// <param name="url">The URL.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>A string for Authorization header.</returns>
        public override string CreateAuthorizationHeader(MethodType type, string url, IEnumerable<KeyValuePair<string, object>> parameters)
        {
            return "Bearer " + this.BearerToken;
        }

        /// <summary>
        /// Makes an instance of OAuth2Tokens.
        /// </summary>
        /// <param name="consumerKey">The consumer key.</param>
        /// <param name="consumerSecret">The consumer secret.</param>
        /// <param name="bearer">The bearer token.</param>
        public static OAuth2Token Create(string consumerKey, string consumerSecret, string bearer)
        {
            return new OAuth2Token()
            {
                ConsumerKey = consumerKey,
                ConsumerSecret = consumerSecret,
                BearerToken = bearer
            };
        }
    }
}
