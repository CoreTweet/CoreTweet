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
using CoreTweet.Core;
using Newtonsoft.Json;

namespace CoreTweet
{
    /// <summary>
    /// Represents a collections of tweets, culled from a curated list of Twitter users.
    /// </summary>
    public class List : CoreBase
    {
        /// <summary>
        /// Gets or sets the string that becomes unique representation by combining an owner_id or owner_screen_name.
        /// </summary>
        [JsonProperty("slug")]
        public string Slug{ get; set; }

        /// <summary>
        /// Gets or sets the name of the List.
        /// </summary>
        [JsonProperty("name")]
        public string Name{ get; set; }

        /// <summary>
        /// Gets or sets the when the List was created.
        /// </summary>
        [JsonProperty("created_at")]
        [JsonConverter(typeof(DateTimeOffsetConverter))]
        public DateTimeOffset CreatedAt{ get; set; }

        /// <summary>
        /// <para>Gets or sets the URL of the List.</para>
        /// <para>Usage: string.Format("https://twitter.com{0}", uri);</para>
        /// </summary>
        [JsonProperty("uri")]
        public string Uri{ get; set; }

        /// <summary>
        /// Gets or sets the number of users following the List.
        /// </summary>
        [JsonProperty("subscriber_count")]
        public int SubscriberCount { get; set; }

        /// <summary>
        /// Gets or sets the number of members in the List.
        /// </summary>
        [JsonProperty("member_count")]
        public int MemberCount{ get; set; }

        /// <summary>
        /// Gets or sets the integer representation of the unique identifier for the List.
        /// </summary>
        [JsonProperty("id")]
        public long Id{ get; set; }

        /// <summary>
        /// Gets or sets a value that indicates whether the List has been published by the owner.
        /// </summary>
        [JsonProperty("mode")]
        public string Mode{ get; set; }

        /// <summary>
        /// Gets or sets the full name of the List.
        /// </summary>
        [JsonProperty("full_name")]
        public string FullName{ get; set; }

        /// <summary>
        /// Gets or sets the user-defined string describes the List. Nullable.
        /// </summary>
        [JsonProperty("description")]
        public string Description{ get; set; }

        /// <summary>
        /// Gets or sets the user of the owner of the List.
        /// </summary>
        [JsonProperty("user")]
        public User User{ get; set; }

        /// <summary>
        /// Gets or sets a value that determines if the List has been followed by the authenticating user.
        /// </summary>
        [JsonProperty("following")]
        public bool IsFollowing{ get; set; }

        /// <summary>
        /// Returns the ID of this instance.
        /// </summary>
        /// <returns>The ID of this instance.</returns>
        public override string ToString()
        {
            return this.Id.ToString("D");
        }
    }

    /// <summary>
    /// Represents a collections of tweets, culled from a curated list of Twitter users with the rate limit.
    /// </summary>
    public class ListResponse : List, ITwitterResponse
    {
        /// <summary>
        /// Gets or sets the rate limit of the response.
        /// </summary>
        public RateLimit RateLimit { get; set; }

        /// <summary>
        /// Gets or sets the JSON of the response
        /// </summary>
        public string Json { get; set; }
    }
}
