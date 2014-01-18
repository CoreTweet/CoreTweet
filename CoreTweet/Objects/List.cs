// The MIT License (MIT)
//
// CoreTweet - A .NET Twitter Library supporting Twitter API 1.1
// Copyright (c) 2013 lambdalice
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
    public class List : CoreBase
    {
        /// <summary>
        ///     The string that become unique representation by combining an owner_id or owner_screen_name.
        /// </summary>
        [JsonProperty("slug")]
        public string Slug{ get; set; }

        /// <summary>
        ///     The name of thi List.
        /// </summary>
        [JsonProperty("name")]
        public string Name{ get; set; }

        /// <summary>
        ///     Time when this List was created.
        /// </summary>
        [JsonProperty("created_at")]
        [JsonConverter(typeof(DateTimeOffsetConverter))]
        public DateTimeOffset CreatedAt{ get; set; }

        /// <summary>
        ///     Uri of this List. Usage: string.Format("https://twitter.com{0}", uri)
        /// </summary>
        [JsonProperty("uri")]
        [JsonConverter(typeof(UriConverter))]
        public Uri Uri{ get; set; }

        /// <summary>
        ///     Number of users following this List.
        /// </summary>
        [JsonProperty("subscriber_count")]
        public int SubscriberCount { get; set; }

        /// <summary>
        ///     Number of members in this List .
        /// </summary>
        [JsonProperty("member_count")]
        public int MemberCount{ get; set; }

        /// <summary>
        ///     The integer representation of the unique identifier for this List.
        /// </summary>
        [JsonProperty("id")]
        public long ID{ get; set; }

        /// <summary>
        ///     Indicates whether this List has been published by the owner.
        /// </summary>
        [JsonProperty("mode")]
        public string Mode{ get; set; }

        /// <summary>
        ///     The full name of this List.
        /// </summary>
        [JsonProperty("full_name")]
        public string FullName{ get; set; }

        /// <summary>
        ///     Nullable. The user-defined UTF-8 string describing this List.
        /// </summary>
        [JsonProperty("description")]
        public string Description{ get; set; }

        /// <summary>
        ///     The user of this List's owner. 
        /// </summary>
        [JsonProperty("user")]
        public User User{ get; set; }

        /// <summary>
        ///     Indicates whether this List has been followed by the authenticating user.
        /// </summary>
        [JsonProperty("following")]
        public bool IsFollowing{ get; set; }
        
    }
}
