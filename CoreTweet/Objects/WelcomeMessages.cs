// The MIT License (MIT)
//
// CoreTweet - A .NET Twitter Library supporting Twitter API 1.1
// Copyright (c) 2013-2018 CoreTweet Development Team
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
using System.Collections.Generic;
using CoreTweet.Core;
using Newtonsoft.Json;

namespace CoreTweet
{
    public class WelcomeMessage : CoreBase
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("created_timestamp"), JsonConverter(typeof(TimestampConverter))]
        public DateTimeOffset CreatedTimestamp { get; set; }

        [JsonProperty("message_data")]
        public MessageData MessageData { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("source_app_id")]
        public string SourceAppId { get; set; }

        /// <summary>
        /// Returns the ID of this instance.
        /// </summary>
        /// <returns>The ID of this instance.</returns>
        public override string ToString() => this.Id;
    }

    public class WelcomeMessageResponse : CoreBase, ITwitterResponse
    {
        [JsonProperty("welcome_message")]
        public WelcomeMessage WelcomeMessage { get; set; }

        [JsonProperty("apps")]
        public IDictionary<string, MessageSourceApp> Apps { get; set; }

        /// <summary>
        /// Gets or sets the rate limit of the response.
        /// </summary>
        /// <remarks>
        /// This property will always be null when obtained from (most of) the POST endpoints, unless the rate is explicitly stated in the Twitter official documentation.
        /// </remarks>
        public RateLimit RateLimit { get; set; }

        /// <summary>
        /// Gets or sets the JSON of the response.
        /// </summary>
        public string Json { get; set; }
    }

    [JsonObject]
    public class CursoredWelcomeMessages
        : CoreBase, ITwitterResponse, IEnumerable<WelcomeMessage>, ICursorForwardable
    {
        [JsonProperty("welcome_messages")]
        public WelcomeMessage[] WelcomeMessages { get; set; }

        [JsonProperty("apps")]
        public IDictionary<string, MessageSourceApp> Apps { get; set; }

        /// <summary>
        /// Gets or sets the next cursor.
        /// </summary>
        [JsonProperty("next_cursor")]
        public string NextCursor { get; set; }

        /// <summary>
        /// Gets or sets the rate limit of the response.
        /// </summary>
        /// <remarks>
        /// This property will always be null when obtained from (most of) the POST endpoints, unless the rate is explicitly stated in the Twitter official documentation.
        /// </remarks>
        public RateLimit RateLimit { get; set; }

        /// <summary>
        /// Gets or sets the JSON of the response.
        /// </summary>
        public string Json { get; set; }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>An IEnumerator object that can be used to iterate through the collection.</returns>
        public IEnumerator<WelcomeMessage> GetEnumerator()
        {
            return WelcomeMessages?.AsEnumerable().GetEnumerator()
                ?? Enumerable.Empty<WelcomeMessage>().GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }

    public class WelcomeMessageRule : CoreBase
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("created_timestamp"), JsonConverter(typeof(TimestampConverter))]
        public DateTimeOffset CreatedTimestamp { get; set; }

        [JsonProperty("welcome_message_id")]
        public string WelcomeMessageId { get; set; }

        /// <summary>
        /// Returns the ID of this instance.
        /// </summary>
        /// <returns>The ID of this instance.</returns>
        public override string ToString() => this.Id;
    }

    public class WelcomeMessageRuleResponse : WelcomeMessageRule, ITwitterResponse
    {
        /// <summary>
        /// Gets or sets the rate limit of the response.
        /// </summary>
        /// <remarks>
        /// This property will always be null when obtained from (most of) the POST endpoints, unless the rate is explicitly stated in the Twitter official documentation.
        /// </remarks>
        public RateLimit RateLimit { get; set; }

        /// <summary>
        /// Gets or sets the JSON of the response.
        /// </summary>
        public string Json { get; set; }
    }

    [JsonObject]
    public class CursoredWelcomeMessageRules
        : CoreBase, ITwitterResponse, IEnumerable<WelcomeMessageRule>, ICursorForwardable
    {
        [JsonProperty("welcome_message_rules")]
        public WelcomeMessageRule[] WelcomeMessageRules { get; set; }

        /// <summary>
        /// Gets or sets the next cursor.
        /// </summary>
        [JsonProperty("next_cursor")]
        public string NextCursor { get; set; }

        /// <summary>
        /// Gets or sets the rate limit of the response.
        /// </summary>
        /// <remarks>
        /// This property will always be null when obtained from (most of) the POST endpoints, unless the rate is explicitly stated in the Twitter official documentation.
        /// </remarks>
        public RateLimit RateLimit { get; set; }

        /// <summary>
        /// Gets or sets the JSON of the response.
        /// </summary>
        public string Json { get; set; }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>An IEnumerator object that can be used to iterate through the collection.</returns>
        public IEnumerator<WelcomeMessageRule> GetEnumerator()
        {
            // WelcomeMessageRules may be null if the user has no rules.
            return WelcomeMessageRules?.AsEnumerable().GetEnumerator()
                ?? Enumerable.Empty<WelcomeMessageRule>().GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
