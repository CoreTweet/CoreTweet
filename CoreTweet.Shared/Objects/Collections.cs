// The MIT License (MIT)
//
// CoreTweet - A .NET Twitter Library supporting Twitter API 1.1
// Copyright (c) 2013-2016 CoreTweet Development Team
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

using System.Collections.Generic;
using System.Globalization;
using CoreTweet.Core;
using Newtonsoft.Json;

namespace CoreTweet
{
    internal class CollectionsApiResult : CoreBase, ITwitterResponse
    {
        [JsonProperty("objects")]
        public CollectionObjects Objects { get; set; }

        [JsonProperty("response")]
        public CollectionResponse Response { get; set; }

        public string Json { get; set; }

        public RateLimit RateLimit { get; set; }
    }

    internal class CollectionObjects : CoreBase
    {
        [JsonProperty("users")]
        public IDictionary<string, User> Users { get; set; }

        [JsonProperty("tweets")]
        public IDictionary<string, Status> Tweets { get; set; }

        [JsonProperty("timelines")]
        public IDictionary<string, TimelineResponse> Timelines { get; set; }
    }

    internal class TimelineInfo : CoreBase
    {
        [JsonProperty("timeline_id")]
        public string TimelineId { get; set; }
    }

    internal class CollectionResponse : CoreBase
    {
        [JsonProperty("timeline_id")]
        public string TimelineId { get; set; }

        [JsonProperty("results")]
        public TimelineInfo[] Results { get; set; }

        [JsonProperty("position")]
        public CollectionEntriesPosition Position { get; set; }

        [JsonProperty("cursors")]
        public CollectionCursors Cursors { get; set; }

        [JsonProperty("timeline")]
        public InternalTimelineItem[] Timeline { get; set; }
    }

    internal class InternalTimelineItem : CoreBase
    {
        [JsonProperty("feature_context")]
        public string FeatureContext { get; set; }

        [JsonProperty("tweet")]
        public InternalTimelineTweet Tweet { get; set; }
    }

    internal class InternalTimelineTweet : CoreBase
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("sort_index")]
        public string SortIndex { get; set; }
    }

    public class Timeline : CoreBase
    {
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("user_id")]
        public string UserId { get; set; }

        public User User { get; set; }

        [JsonProperty("collection_url")]
        public string CollectionUrl { get; set; }

        [JsonProperty("custom_timeline_url")]
        public string CustomTimelineUrl { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("visibility")]
        public string Visibility { get; set; }

        [JsonProperty("timeline_order")]
        public string TimelineOrder { get; set; }

        [JsonProperty("collection_type")]
        public string CollectionType { get; set; }

        [JsonProperty("custom_timeline_type")]
        public string CustomTimelineType { get; set; }
    }

    public class TimelineResponse : Timeline, ITwitterResponse
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

    public class CollectionEntriesPosition : CoreBase
    {
        [JsonProperty("max_position")]
        private string maxPosition;

        public long MaxPosition
        {
            get
            {
                return this.maxPosition == null ? 0 : long.Parse(this.maxPosition, NumberFormatInfo.InvariantInfo);
            }
            set
            {
                this.maxPosition = value.ToString("D");
            }
        }

        [JsonProperty("min_position")]
        private string minPosition;

        public long MinPosition
        {
            get
            {
                return this.minPosition == null ? 0 : long.Parse(this.minPosition, NumberFormatInfo.InvariantInfo);
            }
            set
            {
                this.minPosition = value.ToString("D");
            }
        }

        [JsonProperty("was_truncated")]
        public bool WasTruncated { get; set; }
    }

    public class CollectionCursors : CoreBase
    {
        [JsonProperty("next_cursor")]
        public string NextCursor { get; set; }

        [JsonProperty("previous_cursor")]
        public string PreviousCursor { get; set; }
    }

    public class TimelineEntry : CoreBase
    {
        public string FeatureContext { get; set; }

        public Status Tweet { get; set; }

        public long SortIndex { get; set; }
    }

    public class CollectionsListResult : CoreBase, ITwitterResponse
    {
        public Timeline[] Results { get; set; }

        public CollectionCursors Cursors { get; set; }

        /// <summary>
        /// Gets or sets the rate limit of the response.
        /// </summary>
        public RateLimit RateLimit { get; set; }

        /// <summary>
        /// Gets or sets the JSON of the response.
        /// </summary>
        public string Json { get; set; }
    }

    public class CollectionEntriesResult : CoreBase, ITwitterResponse
    {
        public TimelineEntry[] Entries { get; set; }

        public Timeline Timeline { get; set; }

        public CollectionEntriesPosition Position { get; set; }

        /// <summary>
        /// Gets or sets the rate limit of the response.
        /// </summary>
        public RateLimit RateLimit { get; set; }

        /// <summary>
        /// Gets or sets the JSON of the response.
        /// </summary>
        public string Json { get; set; }
    }

    public class CollectionDestroyResult : CoreBase, ITwitterResponse
    {
        [JsonProperty("destroyed")]
        public bool Destroyed { get; set; }

        /// <summary>
        /// Gets or sets the rate limit of the response.
        /// </summary>
        public RateLimit RateLimit { get; set; }

        /// <summary>
        /// Gets or sets the JSON of the response.
        /// </summary>
        public string Json { get; set; }
    }

    public class CollectionEntryChange : CoreBase
    {
        [JsonProperty("op")]
        public string Op { get; set; }

        [JsonProperty("tweet_id")]
        private string tweetId;

        [JsonIgnore]
        public long TweetId
        {
            get
            {
                return long.Parse(this.tweetId, NumberFormatInfo.InvariantInfo);
            }
            set
            {
                this.tweetId = value.ToString("D");
            }
        }
    }

    public class CollectionEntryOperationError : CoreBase
    {
        [JsonProperty("change")]
        public CollectionEntryChange Change { get; set; }

        [JsonProperty("reason")]
        public string Reason { get; set; }
    }
}
