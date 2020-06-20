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

using CoreTweet.Core;
using Newtonsoft.Json;

namespace CoreTweet.Labs.V1
{
    public class Metrics : CoreBase
    {
        /// <summary>
        /// Unique identifier of this Tweet.
        /// </summary>
        [JsonProperty("tweet_id")]
        public long TweetId { get; set; }

        /// <summary>
        /// Contains detailed engagement metrics for this Tweet.
        /// </summary>
        [JsonProperty("tweet")]
        public TweetMetrics Tweet { get; set; }

        /// <summary>
        /// Contains detailed engagement metrics for any video attached to this Tweet.
        /// </summary>
        [JsonProperty("video")]
        public VideoMetrics Video { get; set; }
    }

    public class TweetMetrics : CoreBase
    {
        /// <summary>
        /// The count of how many times this Tweet has been liked. This is the total count of likes from both organic and paid contexts, in order to maintain consistency with the counts shown publicly on Twitter.
        /// </summary>
        [JsonProperty("like_count")]
        public int LikeCount { get; set; }

        /// <summary>
        /// The count of how many times this Tweet has been Retweeted. This does not include Quote Tweets ("Retweets with comment"). This is the total count of Retweets from both organic and paid contexts, in order to maintain consistency with the counts shown publicly on Twitter.
        /// </summary>
        [JsonProperty("retweet_count")]
        public int RetweetCount { get; set; }

        /// <summary>
        /// The count of how many times this Tweet has been Retweeted with a new comment (message). This does not include Retweets. This is the total count of Quote Tweets from both organic and paid contexts.
        /// </summary>
        [JsonProperty("quote_count")]
        public int QuoteCount { get; set; }

        /// <summary>
        /// The count of how many times this Tweet has been replied to. This returns the total count of replies from both organic and paid contexts, in order to maintain consistency with the counts shown publicly on Twitter.
        /// </summary>
        [JsonProperty("reply_count")]
        public int ReplyCount { get; set; }

        /// <summary>
        /// The count of how many times this Tweet has been viewed (not unique by user). This is the total count of impressions from both organic and paid contexts.
        /// </summary>
        [JsonProperty("impression_count")]
        public int ImpressionCount { get; set; }
    }

    public class VideoMetrics : CoreBase
    {
        /// <summary>
        /// Unique identifier of the media attached to this Tweet.
        /// </summary>
        [JsonProperty("media_key")]
        public string MediaKey { get; set; }

        /// <summary>
        /// The count of how many times the video attached to this Tweet has been viewed. This is the number of video views aggregated across all Tweets in which the given video has been posted. That means that the metric includes the combined views from any instance where the video has been Retweeted or reposted in separate Tweets. This returns the total count of video views from both organic and paid contexts, in order to maintain consistency with the counts shown publicly on Twitter.
        /// </summary>
        [JsonProperty("view_count")]
        public int ViewCount { get; set; }

        /// <summary>
        /// The number of users who made it to less than 25% of the video. This reflects the number of quartile views across all Tweets in which the given video has been posted. This is the total count of video view quartiles from both organic and paid contexts and is only returned when the requester is also the author of the media.
        /// </summary>
        [JsonProperty("playback_0_count")]
        public int Playback0Count { get; set; }

        /// <summary>
        /// The number of users who made it to 25% of the video. This reflects the number of quartile views across all Tweets in which the given video has been posted. This is the total count of video view quartiles from both organic and paid contexts and is only returned when the requester is also the author of the media.
        /// </summary>
        [JsonProperty("playback_25_count")]
        public int Playback25Count { get; set; }

        /// <summary>
        /// The number of users who made it to 50% of the video. This reflects the number of quartile views across all Tweets in which the given video has been posted. This is the total count of video view quartiles from both organic and paid contexts and is only returned when the requester is also the author of the media.
        /// </summary>
        [JsonProperty("playback_50_count")]
        public int Playback50Count { get; set; }

        /// <summary>
        /// The number of users who made it to 75% of the video. This reflects the number of quartile views across all Tweets in which the given video has been posted. This is the total count of video view quartiles from both organic and paid contexts and is only returned when the requester is also the author of the media.
        /// </summary>
        [JsonProperty("playback_75_count")]
        public int Playback75Count { get; set; }

        /// <summary>
        /// The number of users who made it to 100% of the video. This reflects the number of quartile views across all Tweets in which the given video has been posted. This is the total count of video view quartiles from both organic and paid contexts and is only returned when the requester is also the author of the media.
        /// </summary>
        [JsonProperty("playback_100_count")]
        public int Playback100Count { get; set; }
    }

    public class MetricsResponse : ResponseBase
    {
        /// <summary>
        /// List of requested Tweets and attached videos with associated engagement metrics.
        /// </summary>
        [JsonProperty("data")]
        public Metrics[] Data { get; set; }
    }
}
