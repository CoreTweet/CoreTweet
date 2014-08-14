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
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using CoreTweet.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

#if WIN_RT
using System.Threading.Tasks;
#endif

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

        /// <summary>
        /// Initializes a new instance of the <see cref="CoreTweet.ParsingException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="json">The JSON that couldn't be parsed.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (<c>Nothing</c> in Visual Basic) if no inner exception is specified.</param>
        public ParsingException(string message, string json, Exception innerException) : base(message, innerException)
        {
            Json = json;
        }
    }

    /// <summary>
    /// Exception throwed by Twitter.
    /// </summary>
    public class TwitterException : Exception, ITwitterResponse
    {
        private TwitterException(HttpStatusCode status, Error[] errors, RateLimit rateLimit, string json, WebException innerException)
            : base(errors[0].Message, innerException)
        {
            this.Status = status;
            this.Errors = errors;
            this.RateLimit = rateLimit;
            this.Json = json;
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
        /// Gets or sets the rate limit of the response.
        /// </summary>
        public RateLimit RateLimit { get; set; }

        /// <summary>
        /// Gets or sets the JSON of the response
        /// </summary>
        public string Json { get; set; }

        private static Error[] ParseErrors(string json)
        {
            try
            {
                var obj = JObject.Parse(json);
                var errors = obj["errors"] ?? obj["error"];
                if(errors != null)
                {
                    switch(errors.Type)
                    {
                        case JTokenType.Array:
                            return errors.Select(x => x.ToObject<Error>()).ToArray();
                        case JTokenType.String:
                            return errors.ToString().Replace("\\n", "\n").Split('\n').Select(x => new Error { Message = x }).ToArray();
                    }
                }
            }
            catch(JsonException) { }

            var match = Regex.Match(json, @"(Reason:\n<pre>\s+?(?<reason>[^<]+)<\/pre>|<h1>(?<reason>[^<]+)<\/h1>|<error>(?<reason>[^<]+)<\/error>)");
            if(match.Success)
                return new[] { new Error { Message = match.Groups["reason"].Value } };

            return new[] { new Error { Message = json } };
        }

        private static TwitterException Create(string json, HttpStatusCode statusCode, WebException ex, RateLimit rateLimit)
        {
            return new TwitterException(statusCode, ParseErrors(json), rateLimit, json, ex);
        }

        /// <summary>
        /// Create a <see cref="CoreTweet.TwitterException"/> instance from the <see cref="System.Net.WebException"/>.
        /// </summary>
        /// <param name="ex">The thrown <see cref="System.Net.WebException"/>.</param>
        /// <returns><see cref="CoreTweet.TwitterException"/> instance or null.</returns>
        public static TwitterException Create(WebException ex)
        {
            try
            {
                var response = (HttpWebResponse)ex.Response;
                using(var sr = new StreamReader(response.GetResponseStream()))
                    return Create(sr.ReadToEnd(), response.StatusCode, ex, InternalUtils.ReadRateLimit(response));
            }
            catch
            {
                return null;
            }
        }

#if WIN_RT
        /// <summary>
        /// Create a <see cref="CoreTweet.TwitterException"/> instance from the <see cref="CoreTweet.AsyncResponse"/>.
        /// </summary>
        /// <returns><see cref="CoreTweet.TwitterException"/> instance or null.</returns>
        public static async Task<TwitterException> Create(AsyncResponse response)
        {
            try
            {
                using(var sr = new StreamReader(await response.GetResponseStreamAsync().ConfigureAwait(false)))
                    return Create(
                        await sr.ReadToEndAsync().ConfigureAwait(false),
                        (HttpStatusCode)response.StatusCode,
                        null,
                        InternalUtils.ReadRateLimit(response)
                    );
            }
            catch
            {
                return null;
            }
        }
#endif
    }
}
