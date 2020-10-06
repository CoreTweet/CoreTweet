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
using System.Runtime.Serialization;
using System.Text;
using CoreTweet.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CoreTweet.V2
{
    public class Media : CoreBase
    {
        /// <summary>
        /// Unique identifiers for this expanded media content. This is returned using the same media key format as returned by the Media Library.
        /// </summary>
        /// <value></value>
        [JsonProperty("media_key")]
        public string MediaKey { get; set; }

        /// <summary>
        /// Height of this content in pixels.
        /// </summary>
        [JsonProperty("height")]
        public int Height { get; set; }

        /// <summary>
        /// Non-public engagement metrics for the media content at the time of the request. Requires user context authentication.
        /// </summary>
        [JsonProperty("non_public_metrics")]
        public MediaNonPublicMetrics NonPublicMetrics { get; set; }

        /// <summary>
        /// Engagement metrics for the media content, tracked in an organic context, at the time of the request. Requires user context authentication.
        /// </summary>
        [JsonProperty("organic_metrics")]
        public MediaGroupingMetrics OrganicMetrics { get; set; }

        /// <summary>
        /// URL to the static placeholder preview of this content.
        /// </summary>
        [JsonProperty("preview_image_url")]
        public string PreviewImageUrl { get; set; }

        /// <summary>
        /// Engagement metrics for the media content, tracked in a promoted context, at the time of the request. Requires user context authentication.
        /// </summary>
        [JsonProperty("promoted_metrics")]
        public MediaGroupingMetrics PromotedMetrics { get; set; }

        /// <summary>
        /// Non-public engagement metrics for the media content at the time of the request. Requires user context authentication.
        /// </summary>
        [JsonProperty("public_metrics")]
        public MediaPublicMetrics PublicMetrics { get; set; }

        /// <summary>
        /// Width of this content in pixels.
        /// </summary>
        [JsonProperty("width")]
        public int Width { get; set; }

        /// <summary>
        /// Available when <see cref="Media.Type"/> is <see cref="MediaType.Video"/>. Duration in milliseconds of the video.
        /// </summary>
        [JsonProperty("duration_ms")]
        public int? DurationMs { get; set; }

        /// <summary>
        /// Type of content.
        /// </summary>
        [JsonProperty("type")]
        public MediaType Type { get; set; }

        /// <summary>
        /// Undocumented property.
        /// </summary>
        [JsonProperty("url")]
        public string Url { get; set; }
    }

    public interface IMediaNonPublicMetrics
    {
        /// <summary>
        /// The number of users who played through to each quartile in a video. This reflects the number of quartile views across all Tweets in which the given video has been posted.
        /// <para>This is a private, or non-public, metric and always requires OAuth 1.0a User Context authentication.</para>
        /// <para>Use expansion for media objects, <see cref="TweetExpansions.AttachmentsMediaKeys"/></para>
        /// <para>Using the <see cref="TweetFields.NonPublicMetrics"/> field, this returns the total count of video view quartiles from both organic and paid contexts.</para>
        /// <para>Using the <see cref="TweetFields.OrganicMetrics"/> field, this returns the count of video view quartiles from organic contexts.</para>
        /// <para>Using the <see cref="TweetFields.PromotedMetrics"/> field, this returns the count of video view quartiles from promoted contexts.</para>
        /// </summary>
        int Playback0Count { get; set; }

        /// <summary>
        /// The number of users who played through to each quartile in a video. This reflects the number of quartile views across all Tweets in which the given video has been posted.
        /// <para>This is a private, or non-public, metric and always requires OAuth 1.0a User Context authentication.</para>
        /// <para>Use expansion for media objects, <see cref="TweetExpansions.AttachmentsMediaKeys"/></para>
        /// <para>Using the <see cref="TweetFields.NonPublicMetrics"/> field, this returns the total count of video view quartiles from both organic and paid contexts.</para>
        /// <para>Using the <see cref="TweetFields.OrganicMetrics"/> field, this returns the count of video view quartiles from organic contexts.</para>
        /// <para>Using the <see cref="TweetFields.PromotedMetrics"/> field, this returns the count of video view quartiles from promoted contexts.</para>
        /// </summary>
        int Playback25Count { get; set; }

        /// <summary>
        /// The number of users who played through to each quartile in a video. This reflects the number of quartile views across all Tweets in which the given video has been posted.
        /// <para>This is a private, or non-public, metric and always requires OAuth 1.0a User Context authentication.</para>
        /// <para>Use expansion for media objects, <see cref="TweetExpansions.AttachmentsMediaKeys"/></para>
        /// <para>Using the <see cref="TweetFields.NonPublicMetrics"/> field, this returns the total count of video view quartiles from both organic and paid contexts.</para>
        /// <para>Using the <see cref="TweetFields.OrganicMetrics"/> field, this returns the count of video view quartiles from organic contexts.</para>
        /// <para>Using the <see cref="TweetFields.PromotedMetrics"/> field, this returns the count of video view quartiles from promoted contexts.</para>
        /// </summary>
        int Playback50Count { get; set; }

        /// <summary>
        /// The number of users who played through to each quartile in a video. This reflects the number of quartile views across all Tweets in which the given video has been posted.
        /// <para>This is a private, or non-public, metric and always requires OAuth 1.0a User Context authentication.</para>
        /// <para>Use expansion for media objects, <see cref="TweetExpansions.AttachmentsMediaKeys"/></para>
        /// <para>Using the <see cref="TweetFields.NonPublicMetrics"/> field, this returns the total count of video view quartiles from both organic and paid contexts.</para>
        /// <para>Using the <see cref="TweetFields.OrganicMetrics"/> field, this returns the count of video view quartiles from organic contexts.</para>
        /// <para>Using the <see cref="TweetFields.PromotedMetrics"/> field, this returns the count of video view quartiles from promoted contexts.</para>
        /// </summary>
        int Playback75Count { get; set; }

        /// <summary>
        /// The number of users who played through to each quartile in a video. This reflects the number of quartile views across all Tweets in which the given video has been posted.
        /// <para>This is a private, or non-public, metric and always requires OAuth 1.0a User Context authentication.</para>
        /// <para>Use expansion for media objects, <see cref="TweetExpansions.AttachmentsMediaKeys"/></para>
        /// <para>Using the <see cref="TweetFields.NonPublicMetrics"/> field, this returns the total count of video view quartiles from both organic and paid contexts.</para>
        /// <para>Using the <see cref="TweetFields.OrganicMetrics"/> field, this returns the count of video view quartiles from organic contexts.</para>
        /// <para>Using the <see cref="TweetFields.PromotedMetrics"/> field, this returns the count of video view quartiles from promoted contexts.</para>
        /// </summary>
        int Playback100Count { get; set; }
    }

    public interface IMediaPublicMetrics
    {
        /// <summary>
        /// A count of how many times the video included in the Tweet has been viewed.
        /// <para>This is the number of video views aggregated across all Tweets in which the given video has been posted. That means that the metric includes the combined views from any instance where the video has been Retweeted or reposted in separate Tweets.</para>
        /// <para>Use expansion for media objects, <see cref="TweetExpansions.AttachmentsMediaKeys"/></para>
        /// <para>Using the <see cref="TweetFields.PublicMetrics"/> field, this returns the total count of video views from both organic and paid contexts, in order to maintain consistency with the counts shown publicly on Twitter.</para>
        /// <para>Using the <see cref="TweetFields.OrganicMetrics"/> field, this returns the total count of video views from organic contexts.</para>
        /// <para>Using the <see cref="TweetFields.PromotedMetrics"/> field, this returns the total count of video views from paid contexts.</para>
        /// </summary>
        int ViewCount { get; set; }
    }

    public class MediaNonPublicMetrics : CoreBase, IMediaNonPublicMetrics
    {
        /// <summary>
        /// The number of users who made it to less than 25% of the video. This reflects the number of quartile views across all Tweets in which the given video has been posted. This is the total count of video view quartiles from both organic and paid contexts.
        /// </summary>
        [JsonProperty("playback_0_count")]
        public int Playback0Count { get; set; }

        /// <summary>
        /// The number of users who made it to 25% of the video. This reflects the number of quartile views across all Tweets in which the given video has been posted. This is the total count of video view quartiles from both organic and paid contexts.
        /// </summary>
        [JsonProperty("playback_25_count")]
        public int Playback25Count { get; set; }

        /// <summary>
        /// The number of users who made it to 50% of the video. This reflects the number of quartile views across all Tweets in which the given video has been posted. This is the total count of video view quartiles from both organic and paid contexts.
        /// </summary>
        [JsonProperty("playback_50_count")]
        public int Playback50Count { get; set; }

        /// <summary>
        /// The number of users who made it to 75% of the video. This reflects the number of quartile views across all Tweets in which the given video has been posted. This is the total count of video view quartiles from both organic and paid contexts.
        /// </summary>
        [JsonProperty("playback_75_count")]
        public int Playback75Count { get; set; }

        /// <summary>
        /// The number of users who made it to 100% of the video. This reflects the number of quartile views across all Tweets in which the given video has been posted. This is the total count of video view quartiles from both organic and paid contexts.
        /// </summary>
        [JsonProperty("playback_100_count")]
        public int Playback100Count { get; set; }
    }

    public class MediaPublicMetrics : CoreBase, IMediaPublicMetrics
    {
        /// <summary>
        /// The count of how many times the video attached to this Tweet has been viewed. This is the number of video views aggregated across all Tweets in which the given video has been posted. That means that the metric includes the combined views from any instance where the video has been Retweeted or reposted in separate Tweets. This returns the total count of video views from both organic and paid contexts, in order to maintain consistency with the counts shown publicly on Twitter.
        /// </summary>
        [JsonProperty("view_count")]
        public int ViewCount { get; set; }
    }

    public class MediaGroupingMetrics : CoreBase, IMediaPublicMetrics, IMediaNonPublicMetrics
    {
        /// <summary>
        /// The number of users who made it to less than 25% of the video. This reflects the number of quartile views across all Tweets in which the given video has been posted. This is the total count of video view quartiles from both organic and paid contexts.
        /// </summary>
        [JsonProperty("playback_0_count")]
        public int Playback0Count { get; set; }

        /// <summary>
        /// The number of users who made it to 25% of the video. This reflects the number of quartile views across all Tweets in which the given video has been posted. This is the total count of video view quartiles from both organic and paid contexts.
        /// </summary>
        [JsonProperty("playback_25_count")]
        public int Playback25Count { get; set; }

        /// <summary>
        /// The number of users who made it to 50% of the video. This reflects the number of quartile views across all Tweets in which the given video has been posted. This is the total count of video view quartiles from both organic and paid contexts.
        /// </summary>
        [JsonProperty("playback_50_count")]
        public int Playback50Count { get; set; }

        /// <summary>
        /// The number of users who made it to 75% of the video. This reflects the number of quartile views across all Tweets in which the given video has been posted. This is the total count of video view quartiles from both organic and paid contexts.
        /// </summary>
        [JsonProperty("playback_75_count")]
        public int Playback75Count { get; set; }

        /// <summary>
        /// The number of users who made it to 100% of the video. This reflects the number of quartile views across all Tweets in which the given video has been posted. This is the total count of video view quartiles from both organic and paid contexts.
        /// </summary>
        [JsonProperty("playback_100_count")]
        public int Playback100Count { get; set; }

        /// <summary>
        /// The count of how many times the video attached to this Tweet has been viewed. This is the number of video views aggregated across all Tweets in which the given video has been posted. That means that the metric includes the combined views from any instance where the video has been Retweeted or reposted in separate Tweets. This returns the total count of video views from both organic and paid contexts, in order to maintain consistency with the counts shown publicly on Twitter.
        /// </summary>
        [JsonProperty("view_count")]
        public int ViewCount { get; set; }
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum MediaType
    {
        [EnumMember(Value = "animated_gif")]
        AnimatedGif,
        [EnumMember(Value = "photo")]
        Photo,
        [EnumMember(Value = "video")]
        Video,
    }

    /// <summary>
    /// List of media fields to return in the Tweet media object. The response will contain the selected fields only if a Tweet contains media attachments.
    /// </summary>
    [Flags]
    public enum MediaFields
    {
        None             = 0x00000000,
        DurationMs       = 0x00000001,
        Height           = 0x00000002,
        MediaKey         = 0x00000004,
        PreviewImageUrl  = 0x00000008,
        Type             = 0x00000010,
        Url              = 0x00000020,
        Width            = 0x00000040,
        PublicMetrics    = 0x00000080,
        NonPublicMetrics = 0x00000100,
        OrganicMetrics   = 0x00000200,
        PromotedMetrics  = 0x00000400,
        All              = 0x000007ff,
    }

    public static class MediaFieldsExtensions
    {
        public static string ToQueryString(this MediaFields value)
        {
            if (value == MediaFields.None)
                return "";

            var builder = new StringBuilder();

            if ((value & MediaFields.DurationMs) != 0)
                builder.Append("duration_ms,");
            if ((value & MediaFields.Height) != 0)
                builder.Append("height,");
            if ((value & MediaFields.MediaKey) != 0)
                builder.Append("media_key,");
            if ((value & MediaFields.PreviewImageUrl) != 0)
                builder.Append("preview_image_url,");
            if ((value & MediaFields.Type) != 0)
                builder.Append("type,");
            if ((value & MediaFields.Url) != 0)
                builder.Append("url,");
            if ((value & MediaFields.Width) != 0)
                builder.Append("width,");
            if ((value & MediaFields.PublicMetrics) != 0)
                builder.Append("public_metrics,");
            if ((value & MediaFields.NonPublicMetrics) != 0)
                builder.Append("non_public_metrics,");
            if ((value & MediaFields.OrganicMetrics) != 0)
                builder.Append("organic_metrics,");
            if ((value & MediaFields.PromotedMetrics) != 0)
                builder.Append("promoted_metrics,");

            return builder.ToString(0, builder.Length - 1);
        }
    }
}
