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
using System.Collections.Generic;
using System.Linq;
using CoreTweet.Core;
using Newtonsoft.Json;

namespace CoreTweet
{
    public class MessageCreateEvent : CoreBase
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("created_timestamp"), JsonConverter(typeof(TimestampConverter))]
        public DateTimeOffset CreatedTimestamp { get; set; }

        [JsonProperty("initiated_via")]
        public InitiatedVia InitiatedVia { get; set; }

        [JsonProperty("message_create")]
        public MessageCreate MessageCreate { get; set; }

        /// <summary>
        /// Returns the ID of this instance.
        /// </summary>
        /// <returns>The ID of this instance.</returns>
        public override string ToString() => this.Id;
    }

    public class MessageCreateEventResponse : CoreBase, ITwitterResponse
    {
        [JsonProperty("event")]
        public MessageCreateEvent Event { get; set; }

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

    public class MessageCreate : CoreBase
    {
        [JsonProperty("target")]
        public MessageTarget Target { get; set; }

        [JsonProperty("sender_id")]
        public long SenderId { get; set; }

        [JsonProperty("source_app_id")]
        public string SourceAppId { get; set; }

        [JsonProperty("message_data")]
        public MessageData MessageData { get; set; }
    }

    public class MessageTarget : CoreBase
    {
        [JsonProperty("recipient_id")]
        public long RecipientId { get; set; }
    }

    public class MessageData : CoreBase
    {
        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("entities")]
        public Entities Entities { get; set; }

        [JsonProperty("attachment")]
        public MessageAttachment Attachment { get; set; }

        [JsonProperty("quick_reply")]
        public QuickReply QuickReply { get; set; }

        [JsonProperty("quick_reply_response")]
        public QuickReplyResponse QuickReplyResponse { get; set; }

        [JsonProperty("ctas")]
        public MessageCallToActionResponse[] Ctas { get; set; }
    }

    public class MessageAttachment : CoreBase
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("media")]
        public MediaEntity Media { get; set; }
    }

    public class QuickReply : CoreBase
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("options")]
        public QuickReplyOption[] Options { get; set; }
    }

    public class QuickReplyOption : CoreBase
    {
        [JsonProperty("label")]
        public string Label { get; set; }

        [JsonProperty("metadata")]
        public string Metadata { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }

    public class QuickReplyResponse : CoreBase
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("metadata")]
        public string Metadata { get; set; }
    }

    [JsonObject]
    public class CursoredMessageCreateEvents
        : CoreBase, ITwitterResponse, IEnumerable<MessageCreateEvent>, ICursorForwardable
    {
        [JsonProperty("events")]
        public MessageCreateEvent[] Events { get; set; }

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
        public IEnumerator<MessageCreateEvent> GetEnumerator()
        {
            return Events?.AsEnumerable().GetEnumerator()
                ?? Enumerable.Empty<MessageCreateEvent>().GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }

    public class InitiatedVia : CoreBase
    {
        [JsonProperty("tweet_id")]
        public string TweetId { get; set; }

        [JsonProperty("welcome_message_id")]
        public string WelcomeMessageId { get; set; }
    }

    public class MessageSourceApp : CoreBase
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }

    public class MessageCallToAction : CoreBase
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }

    public class MessageCallToActionResponse : MessageCallToAction
    {
        [JsonProperty("tco_url")]
        public string TcoUrl { get; set; }
    }
}
