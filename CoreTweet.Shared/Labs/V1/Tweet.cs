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
    public class Tweet : CoreBase
    {
        /// <summary>
        /// Unique identifier of this Tweet.
        /// </summary>
        [JsonProperty("id")]
        public long Id { get; set; }

        /// <summary>
        /// Creation time of the Tweet.
        /// </summary>
        /// <remarks>
        /// To return this field, add <see cref="TweetFields.CreatedAt"/> in the request's query parameter.
        /// </remarks>
        [JsonProperty("created_at")]
        [JsonConverter(typeof(DateTimeOffsetConverter))]
        public DateTimeOffset? CreatedAt { get; set; }

        /// <summary>
        /// The content of the Tweet.
        /// </summary>
        /// <remarks>
        /// To return this field, add <see cref="TweetFields.Text"/> in the request's query parameter.
        /// </remarks>
        [JsonProperty("text")]
        public string Text { get; set; }

        /// <summary>
        /// Unique identifier of this User.
        /// </summary>
        /// <remarks>
        /// You can obtain the expanded object in <see cref="TweetResponseIncludes.Users"/> by adding <see cref="TweetExpansions.AuthorId"/> in the request's query parameter.
        /// </remarks>
        [JsonProperty("author_id")]
        public long? AuthorId { get; set; }

        /// <summary>
        /// If this Tweet is a Reply, indicates the User ID of the parent Tweet's author.
        /// </summary>
        /// <remarks>
        /// You can obtain the expanded object in <see cref="TweetResponseIncludes.Users"/> by adding <see cref="TweetExpansions.InReplyToUserId"/> in the request's query parameter.
        /// </remarks>
        [JsonProperty("in_reply_to_user_id")]
        public long? InReplyToUserId { get; set; }

        /// <summary>
        /// A list of Tweets this Tweet refers to. For example, if the parent Tweet is a Retweet, a Retweet with comment (also known as Quoted Tweet) or a Reply, it will include the related Tweet referenced to by its parent.
        /// </summary>
        /// <remarks>
        /// To return this field, add <see cref="TweetFields.ReferencedTweets"/> in the request's query parameter.
        /// </remarks>
        [JsonProperty("referenced_tweets")]
        public TweetReferencedTweet[] ReferencedTweets { get; set; }

        /// <summary>
        /// Specifies the type of attachments (if any) present in this Tweet.
        /// </summary>
        [JsonProperty("attachments")]
        public TweetAttachments Attachments { get; set; }

        /// <summary>
        /// Contains details about the location tagged by the user in this Tweet, if they specified one.
        /// </summary>
        /// <remarks>
        /// To return this field, add <see cref="TweetFields.Geo"/> in the request's query parameter.
        /// </remarks>
        [JsonProperty("geo")]
        public TweetGeo Geo { get; set; }

        /// <summary>
        /// Contains context annotations for the Tweet.
        /// </summary>
        /// <remarks>
        /// To return this field, add <see cref="TweetFields.ContextAnnotations"/> in the request's query parameter.
        /// </remarks>
        [JsonProperty("context_annotations")]
        public TweetContextAnnotation[] ContextAnnotations { get; set; }

        /// <summary>
        /// Contains details about text that has a special meaning in a Tweet.
        /// </summary>
        /// <remarks>
        /// To return this field, add <see cref="TweetFields.Entities"/> in the request's query parameter.
        /// </remarks>
        [JsonProperty("entities")]
        public Entities Entities { get; set; }

        /// <summary>
        /// Contains withholding details for withheld content.
        /// </summary>
        /// <remarks>
        /// To return this field, add <see cref="TweetFields.Withheld"/> in the request's query parameter.
        /// </remarks>
        [JsonProperty("withheld")]
        public Withheld Withheld { get; set; }

        /// <summary>
        /// Engagement metrics for the Tweet at the time of the request.
        /// </summary>
        /// <remarks>
        /// To return this field, add <see cref="TweetFields.PublicMetrics"/> in the request's query parameter.
        /// </remarks>
        [JsonProperty("public_metrics")]
        public TweetPublicMetrics PublicMetrics { get; set; }

        /// <summary>
        /// Non-public engagement metrics for the Tweet at the time of the request. Requires user context authentication.
        /// </summary>
        /// <remarks>
        /// To return this field, add <see cref="TweetFields.NonPublicMetrics"/> in the request's query parameter.
        /// </remarks>
        [JsonProperty("non_public_metrics")]
        public TweetNonPublicMetrics NonPublicMetrics { get; set; }

        /// <summary>
        /// Indicates if this Tweet contains URLs marked as sensitive, for example content suitable for mature audiences.
        /// </summary>
        /// <remarks>
        /// To return this field, add <see cref="TweetFields.PossiblySensitive"/> in the request's query parameter.
        /// </remarks>
        [JsonProperty("possibly_sensitive")]
        public bool? PossiblySensitive { get; set; }

        /// <summary>
        /// Language of the Tweet, if detected by Twitter. Returned as a BCP47 language tag.
        /// </summary>
        /// <remarks>
        /// To return this field, add <see cref="TweetFields.Lang"/> in the request's query parameter.
        /// </remarks>
        [JsonProperty("lang")]
        public string Lang { get; set; }

        /// <summary>
        /// The name of the app the user Tweeted from.
        /// </summary>
        /// <remarks>
        /// To return this field, add <see cref="TweetFields.Source"/> in the request's query parameter.
        /// </remarks>
        [JsonProperty("source")]
        public string Source { get; set; }

        /// <summary>
        /// Indicates the format returned for this object, as requested in the <c>format</c> or <c>tweet.format</c> query parameters.
        /// </summary>
        [JsonProperty("format")]
        public Format? Format { get; set; }
    }

    public class TweetReferencedTweet : CoreBase
    {
        /// <summary>
        /// Indicates the type of relationship between this Tweet and the Tweet returned in the response: <see cref="TweetReferencedTweetType.Retweeted"/> (this Tweet is a Retweet), <see cref="TweetReferencedTweetType.Quoted"/> (a Retweet with comment, also known as Quoted Tweet), or <see cref="TweetReferencedTweetType.RepliedTo"/> (this Tweet is a reply).
        /// </summary>
        [JsonProperty("type")]
        public TweetReferencedTweetType Type { get; set; }

        /// <summary>
        /// The unique identifier of the referenced Tweet.
        /// </summary>
        /// <remarks>
        /// You can obtain the expanded object in <see cref="TweetResponseIncludes.Tweets"/> by adding <see cref="TweetExpansions.ReferencedTweetsId"/> in the request's query parameter.
        /// </remarks>
        [JsonProperty("id")]
        public long Id { get; set; }

        // /// <summary>
        // /// The unique identifier of the author of the referenced Tweet.
        // /// </summary>
        // /// <remarks>
        // /// You can obtain the expanded object in <see cref="TweetResponseIncludes.Users"/> by adding <see cref="TweetExpansions.ReferencedTweetsIdAuthorId"/> in the request's query parameter.
        // /// </remarks>
        // TODO: what is `referenced_tweets.id.author_id`? how to create a string typed property in string?
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum TweetReferencedTweetType
    {
        [EnumMember(Value = "retweeted")]
        Retweeted,

        [EnumMember(Value = "quoted")]
        Quoted,

        [EnumMember(Value = "replied_to")]
        RepliedTo,
    }

    public class TweetAttachments : CoreBase
    {
        /// <summary>
        /// List of unique identifiers of media attached to this Tweet. These identifiers use the same media key format as those returned by the Media Library.
        /// </summary>
        /// <remarks>
        /// You can obtain the expanded object in <see cref="TweetResponseIncludes.Media"/> by adding <see cref="TweetExpansions.AttachmentsMediaKeys"/> in the request's query parameter.
        /// </remarks>
        [JsonProperty("media_keys")]
        public string[] MediaKeys { get; set; }

        /// <summary>
        /// List of unique identifiers of polls present in the Tweets returned.
        /// </summary>
        /// <remarks>
        /// You can obtain the expanded object in <see cref="TweetResponseIncludes.Polls"/> by adding <see cref="TweetExpansions.AttachmentsPollIds"/> in the request's query parameter.
        /// </remarks>
        [JsonProperty("poll_ids")]
        public long[] PollIds { get; set; }
    }

    public class TweetGeo : CoreBase
    {
        [JsonProperty("coordinates")]
        public GeoJsonPoint Coordinates { get; set; }

        [JsonProperty("place_id")]
        public string PlaceId { get; set; }
    }

    public class TweetContextAnnotation : CoreBase
    {
        /// <summary>
        /// Contains elements which identify detailed information regarding the domain classification based on Tweet text.
        /// </summary>
        [JsonProperty("domain")]
        public TweetContextAnnotationDomain Domain { get; set; }

        /// <summary>
        /// Contains elements which identify detailed information regarding the domain classification bases on Tweet text.
        /// </summary>
        [JsonProperty("entity")]
        public TweetContextAnnotationEntity Entity { get; set; }
    }

    public class TweetContextAnnotationDomain : CoreBase
    {
        /// <summary>
        /// Contains the numeric value of the domain.
        /// </summary>
        [JsonProperty("id")]
        public long Id { get; set; }

        /// <summary>
        /// Domain name based on the Tweet text.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Long form description of domain classification.
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }
    }

    public class TweetContextAnnotationEntity : CoreBase
    {
        /// <summary>
        /// Unique value which correlates to an explicitly mentioned Person, Place, Product or Organization
        /// </summary>
        [JsonProperty("id")]
        public long Id { get; set; }

        /// <summary>
        /// Name or reference of entity referenced in the Tweet.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Additional information regarding referenced entity.
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }
    }

    public class TweetPublicMetrics : CoreBase
    {
        /// <summary>
        /// Number of times this Tweet has been Retweeted.
        /// </summary>
        [JsonProperty("retweet_count")]
        public int RetweetCount { get; set; }

        /// <summary>
        /// Number of Replies of this Tweet.
        /// </summary>
        [JsonProperty("reply_count")]
        public int ReplyCount { get; set; }

        /// <summary>
        /// Number of Likes of this Tweet.
        /// </summary>
        [JsonProperty("like_count")]
        public int LikeCount { get; set; }

        /// <summary>
        /// Number of times this Tweet has been Retweeted with a comment (also known as Quote).
        /// </summary>
        [JsonProperty("quote_count")]
        public int QuoteCount { get; set; }
    }

    public class TweetNonPublicMetrics : CoreBase
    {
        /// <summary>
        /// Number of times the Tweet has been viewed.
        /// </summary>
        [JsonProperty("impression_count")]
        public int InpressionCount { get; set; }
    }

    public class TweetResponseIncludes : CoreBase
    {
        /// <summary>
        /// For referenced Tweets, this is a list of objects with the same structure as the one described in this page.
        /// </summary>
        [JsonProperty("tweets")]
        public Tweet[] Tweets { get; set; }

        [JsonProperty("users")]
        public User[] Users { get; set; }

        [JsonProperty("places")]
        public Place[] Places { get; set; }

        /// <summary>
        /// For referenced media attachments, this is a list of objects describing media content.
        /// </summary>
        [JsonProperty("media")]
        public Media[] Media { get; set; }

        /// <summary>
        /// For referenced polls, this is a list of objects describing polls.
        /// </summary>
        [JsonProperty("polls")]
        public Poll[] Polls { get; set; }
    }

    public class TweetResponse : ResponseBase
    {
        [JsonProperty("data")]
        public Tweet Data { get; set; }

        /// <summary>
        /// Returns the requested <see cref="TweetExpansions"/>, if available.
        /// </summary>
        [JsonProperty("includes")]
        public TweetResponseIncludes Includes { get; set; }
    }

    public class TweetsResponse : ResponseBase
    {
        [JsonProperty("data")]
        public Tweet[] Data { get; set; }

        /// <summary>
        /// Returns the requested <see cref="TweetExpansions"/>, if available.
        /// </summary>
        [JsonProperty("includes")]
        public TweetResponseIncludes Includes { get; set; }
    }

    /// <summary>
    /// List of fields to return in the Tweet object. By default, the endpoint only returns <see cref="TweetFields.Id"/> and <see cref="TweetFields.Text"/>.
    /// </summary>
    [Flags]
    public enum TweetFields
    {
        None               = 0x00000000,
        Attachments        = 0x00000001,
        AuthorId           = 0x00000002,
        ContextAnnotations = 0x00000004,
        CreatedAt          = 0x00000008,
        Entities           = 0x00000010,
        Geo                = 0x00000020,
        Id                 = 0x00000040,
        InReplyToUserId    = 0x00000080,
        Lang               = 0x00000100,
        NonPublicMetrics   = 0x00000200,
        PossiblySensitive  = 0x00000400,
        ReferencedTweets   = 0x00000800,
        Source             = 0x00001000,
        PublicMetrics      = 0x00002000,
        Text               = 0x00004000,
        Withheld           = 0x00008000,
        All                = 0x0000ffff,
        AllPublic          = All - NonPublicMetrics,
    }

    public static class TweetFieldsExtensions
    {
        public static string ToQueryString(this TweetFields value)
        {
            if (value == TweetFields.None)
                return "";

            var builder = new StringBuilder();

            if ((value & TweetFields.Attachments) != 0)
                builder.Append("attachments,");
            if ((value & TweetFields.AuthorId) != 0)
                builder.Append("author_id,");
            if ((value & TweetFields.ContextAnnotations) != 0)
                builder.Append("context_annotations,");
            if ((value & TweetFields.CreatedAt) != 0)
                builder.Append("created_at,");
            if ((value & TweetFields.Entities) != 0)
                builder.Append("entities,");
            if ((value & TweetFields.Geo) != 0)
                builder.Append("geo,");
            if ((value & TweetFields.Id) != 0)
                builder.Append("id,");
            if ((value & TweetFields.InReplyToUserId) != 0)
                builder.Append("in_reply_to_user_id,");
            if ((value & TweetFields.Lang) != 0)
                builder.Append("lang,");
            if ((value & TweetFields.NonPublicMetrics) != 0)
                builder.Append("non_public_metrics,");
            if ((value & TweetFields.PossiblySensitive) != 0)
                builder.Append("possibly_sensitive,");
            if ((value & TweetFields.ReferencedTweets) != 0)
                builder.Append("referenced_tweets,");
            if ((value & TweetFields.Source) != 0)
                builder.Append("source,");
            if ((value & TweetFields.PublicMetrics) != 0)
                builder.Append("public_metrics,");
            if ((value & TweetFields.Text) != 0)
                builder.Append("text,");
            if ((value & TweetFields.Withheld) != 0)
                builder.Append("withheld,");

            return builder.ToString(0, builder.Length - 1);
        }
    }

    /// <summary>
    /// List of fields to return in the Tweet poll object. The response will contain the selected fields only if a Tweet contains a poll.
    /// </summary>
    [Flags]
    public enum TweetExpansions
    {
        None                       = 0x00000000,
        AttachmentsPollIds         = 0x00000001,
        AttachmentsMediaKeys       = 0x00000002,
        AuthorId                   = 0x00000004,
        EntitiesMentionsUsername   = 0x00000008,
        GeoPlaceId                 = 0x00000010,
        InReplyToUserId            = 0x00000020,
        ReferencedTweetsId         = 0x00000040,
        ReferencedTweetsIdAuthorId = 0x00000080,
        All                        = 0x000000ff,
        AllPublic                  = All,
    }

    public static class TweetExpansionsExtensions
    {
        public static string ToQueryString(this TweetExpansions value)
        {
            if (value == TweetExpansions.None)
                return "";

            var builder = new StringBuilder();

            if ((value & TweetExpansions.AttachmentsPollIds) != 0)
                builder.Append("attachments.poll_ids,");
            if ((value & TweetExpansions.AttachmentsMediaKeys) != 0)
                builder.Append("attachments.media_keys,");
            if ((value & TweetExpansions.AuthorId) != 0)
                builder.Append("author_id,");
            if ((value & TweetExpansions.EntitiesMentionsUsername) != 0)
                builder.Append("entities.mentions.username,");
            if ((value & TweetExpansions.GeoPlaceId) != 0)
                builder.Append("geo.place_id,");
            if ((value & TweetExpansions.InReplyToUserId) != 0)
                builder.Append("in_reply_to_user_id,");
            if ((value & TweetExpansions.ReferencedTweetsId) != 0)
                builder.Append("referenced_tweets.id,");
            if ((value & TweetExpansions.ReferencedTweetsIdAuthorId) != 0)
                builder.Append("referenced_tweets.id.author_id,");

            return builder.ToString(0, builder.Length - 1);
        }
    }
}
