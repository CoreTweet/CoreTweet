﻿// The MIT License (MIT)
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
using System.IO;
using System.Linq;
using System.Net;
using CoreTweet.Core;
using Newtonsoft.Json.Linq;

namespace CoreTweet
{
    /// <summary>
    /// Exception when parsing.
    /// </summary>
    public class ParsingException : Exception
    {
        /// <summary>
        /// The JSON which causes an exception.
        /// </summary>
        /// <value>
        /// The json.
        /// </value>
        public string Json { get; private set; }

        internal ParsingException(string message, string data, Exception innerException) : base(message, innerException)
        {
            Json = data;
        }
    }

    /// <summary>
    /// Exception throwed by Twitter.
    /// </summary>
    public class TwitterException : Exception
    {
        private TwitterException(HttpStatusCode status, Error[] errors, WebException innerException)
            : base(errors[0].Message, innerException)
        {
            Status = status;
            Errors = errors;
        }

        /// <summary>
        ///     The status of the response.
        /// </summary>
        public HttpStatusCode Status { get; private set; }

        /// <summary>
        ///     The error messages.
        /// </summary>
        public Error[] Errors { get; private set; }

        /// <summary>
        /// Create a <see cref="CoreTweet.TwitterException"/> instance from the <see cref="System.Net.WebException"/>.
        /// </summary>
        /// <param name="tokens">OAuth tokens.</param>
        /// <param name="ex">The thrown <see cref="System.Net.WebException"/>.</param>
        /// <returns><see cref="CoreTweet.TwitterException"/> instance or null.</returns>
        public static TwitterException Create(TokensBase tokens, WebException ex)
        {
            try
            {
                var response = (HttpWebResponse)ex.Response;
                using(var sr = new StreamReader(response.GetResponseStream()))
                {
                    var errors = JObject.Parse(sr.ReadToEnd())["errors"];
                    switch(errors.Type)
                    {
                        case JTokenType.Array:
                            return new TwitterException(response.StatusCode, errors.Select(x => x.ToObject<Error>()).ToArray(), ex);
                        case JTokenType.String:
                            return new TwitterException(response.StatusCode, new[] { new Error { Message = (string)errors } }, ex);
                        default:
                            return null;
                    }
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
