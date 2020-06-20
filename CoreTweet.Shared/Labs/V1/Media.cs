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

namespace CoreTweet.Labs.V1
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
        /// URL to the static placeholder preview of this content.
        /// </summary>
        [JsonProperty("preview_image_url")]
        public string PreviewImageUrl { get; set; }

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

        /// <remarks>
        /// Undocumented property.
        /// </remarks>
        [JsonProperty("url")]
        public string Url { get; set; }
    }

    public class MediaNonPublicMetrics : CoreBase
    {
        /// <summary>
        /// The number of users who made it to less than 25% of the video. This reflects the number of quartile views across all Tweets in which the given video has been posted. This is the total count of video view quartiles from both organic and paid contexts.
        /// </summary>
        [JsonProperty("playback_0_count")]
        public int Playback0Count { get; set; }
        // TODO: is it ok to be `int`?

        /// <summary>
        /// The number of users who made it to 25% of the video. This reflects the number of quartile views across all Tweets in which the given video has been posted. This is the total count of video view quartiles from both organic and paid contexts.
        /// </summary>
        [JsonProperty("playback_25_count")]
        public int Playback25Count { get; set; }
        // TODO: is it ok to be `int`?

        /// <summary>
        /// The number of users who made it to 50% of the video. This reflects the number of quartile views across all Tweets in which the given video has been posted. This is the total count of video view quartiles from both organic and paid contexts.
        /// </summary>
        [JsonProperty("playback_50_count")]
        public int Playback50Count { get; set; }
        // TODO: is it ok to be `int`?

        /// <summary>
        /// The number of users who made it to 75% of the video. This reflects the number of quartile views across all Tweets in which the given video has been posted. This is the total count of video view quartiles from both organic and paid contexts.
        /// </summary>
        [JsonProperty("playback_75_count")]
        public int Playback75Count { get; set; }
        // TODO: is it ok to be `int`?

        /// <summary>
        /// The number of users who made it to 100% of the video. This reflects the number of quartile views across all Tweets in which the given video has been posted. This is the total count of video view quartiles from both organic and paid contexts.
        /// </summary>
        [JsonProperty("playback_100_count")]
        public int Playback100Count { get; set; }
        // TODO: is it ok to be `int`?
    }

    public class MediaPublicMetrics : CoreBase
    {
        /// <summary>
        /// The count of how many times the video attached to this Tweet has been viewed. This is the number of video views aggregated across all Tweets in which the given video has been posted. That means that the metric includes the combined views from any instance where the video has been Retweeted or reposted in separate Tweets. This returns the total count of video views from both organic and paid contexts, in order to maintain consistency with the counts shown publicly on Twitter.
        /// </summary>
        [JsonProperty("view_count")]
        public int ViewCount { get; set; }
        // TODO: is it ok to be `int`?
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
        NonPublicMetrics = 0x00000008,
        PreviewImageUrl  = 0x00000010,
        PublicMetrics    = 0x00000020,
        Type             = 0x00000040,
        Url              = 0x00000080,
        Width            = 0x00000100,
        All              = 0x000001ff,
        AllPublic        = All - NonPublicMetrics,
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
            if ((value & MediaFields.NonPublicMetrics) != 0)
                builder.Append("non_public_metrics,");
            if ((value & MediaFields.PreviewImageUrl) != 0)
                builder.Append("preview_image_url,");
            if ((value & MediaFields.PublicMetrics) != 0)
                builder.Append("public_metrics,");
            if ((value & MediaFields.Type) != 0)
                builder.Append("type,");
            if ((value & MediaFields.Url) != 0)
                builder.Append("url,");
            if ((value & MediaFields.Width) != 0)
                builder.Append("width,");

            return builder.ToString(0, builder.Length - 1);
        }
    }
}
