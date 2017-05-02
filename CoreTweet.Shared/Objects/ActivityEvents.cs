// The MIT License (MIT)
//
// CoreTweet - A .NET Twitter Library supporting Twitter API 1.1
// Copyright (c) 2013-2017 CoreTweet Development Team
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
using Newtonsoft.Json;

namespace CoreTweet
{
    // abstract にしないで、 message_create とかをこれに詰め込む？
    public abstract class ActivityEvent : CoreBase
    {
        // enum?
        [JsonProperty("type")]
        public string Type { get; set; }

        // long?
        [JsonProperty("id")]
        public string Id { get; set; }

        // DateTimeOffset?
        [JsonProperty("created_timestamp")]
        public string CreatedTimestamp { get; set; }
    }

    public class MessageCreateEvent : ActivityEvent
    {
        [JsonProperty("message_create")]
        public MessageCreate MessageCreate { get; set; }
    }

    public class MessageCreateEventResponse : MessageCreateEvent, ITwitterResponse
    {
        /// <summary>
        /// Gets or sets the rate limit of the response.
        /// </summary>
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

        // long?
        [JsonProperty("sender_id")]
        public string SenderId { get; set; }

        [JsonProperty("message_data")]
        public MessageData MessageData { get; set; }
    }

    public class MessageTarget : CoreBase
    {
        // long?
        [JsonProperty("recipient_id")]
        public string RecipientId { get; set; }
    }

    public class MessageData : CoreBase
    {
        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("entities")]
        public Entities Entities { get; set; }

        [JsonProperty("attachment")]
        public AttachmentBase Attachment { get; set; }
    }

    public abstract class AttachmentBase : CoreBase
    {
        // enum?
        [JsonProperty("type")]
        public string Type { get; set; }
    }

    public class MediaAttachment : AttachmentBase
    {
        [JsonProperty("media")]
        public MediaEntity Media { get; set; }
    }
}
