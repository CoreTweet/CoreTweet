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
        /// The access token.
        /// </summary>
        public string BearerToken { get; set; }

        public OAuth2Token() { }

        public OAuth2Token(OAuth2Token e)
            : this()
        {
            this.ConsumerKey = e.ConsumerKey;
            this.ConsumerSecret = e.ConsumerSecret;
            this.BearerToken = e.BearerToken;
        }

        internal override string CreateAuthorizationHeader(MethodType type, string url, IEnumerable<KeyValuePair<string, object>> parameters)
        {
            return "Bearer " + this.BearerToken;
        }

        /// <summary>
        /// Make an instance of OAuth2Tokens.
        /// </summary>
        /// <param name="consumerKey">Consumer key.</param>
        /// <param name="consumerSecret">Consumer secret.</param>
        /// <param name="bearer">Bearer token</param>
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
