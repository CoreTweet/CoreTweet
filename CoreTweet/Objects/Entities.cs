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
    /// Represents the metadata and additional contextual information about content posted on Twitter.
    /// </summary>
    public class Entities : CoreBase
    {
        /// <summary>
        /// Gets or sets the hashtags which have been parsed out of the Tweet text.
        /// </summary>
        [JsonProperty("hashtags")]
        public SymbolEntity[] HashTags { get; set; }

        /// <summary>
        /// Gets or sets the media elements uploaded with the Tweet.
        /// </summary>
        [JsonProperty("media")]
        public MediaEntity[] Media { get; set; }

        /// <summary>
        /// Gets or sets the URLs included in the text of a Tweet or within textual fields of a user object.
        /// </summary>
        [JsonProperty("urls")]
        public UrlEntity[] Urls { get; set; }

        /// <summary>
        /// Gets or sets the other Twitter users mentioned in the text of the Tweet.
        /// </summary>
        [JsonProperty("user_mentions")]
        public UserMentionEntity[] UserMentions { get; set; }

        /// <summary>
        /// Gets or sets the symbols which have been parsed out of the Tweet text.
        /// </summary>
        [JsonProperty("symbols")]
        public SymbolEntity[] Symbols { get; set; }
    }

    /// <summary>
    /// Represents an entity object in the content posted on Twitter. This is an <c>abstract</c> class.
    /// </summary>
    public abstract class Entity : CoreBase
    {
        /// <summary>
        /// <para>Gets or sets an array of integers indicating the offsets within the Tweet text where the URL begins and ends.</para>
        /// <para>The first integer represents the location of the first character of the URL in the Tweet text.</para>
        /// <para>The second integer represents the location of the first non-URL character occurring after the URL (or the end of the string if the URL is the last part of the Tweet text).</para>
        /// </summary>
        [JsonProperty("indices")]
        public int[] Indices { get; set; }
    }

    /// <summary>
    /// Represents a symbol object that contains a symbol in the content posted on Twitter.
    /// </summary>
    public class SymbolEntity : Entity
    {
        /// <summary>
        /// Gets or sets the name of the hashtag, minus the leading '#' or '$' character.
        /// </summary>
        [JsonProperty("text")]
        public string Text { get; set; }
    }

    /// <summary>
    /// Represents a media object that contains the URLs, sizes and type of the media.
    /// </summary>
    public class MediaEntity : UrlEntity
    {
        /// <summary>
        /// Gets or sets the ID of the media expressed as a 64-bit integer.
        /// </summary>
        [JsonProperty("id")]
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the URL pointing directly to the uploaded media file.
        /// </summary>
        [JsonProperty("media_url")]
        [JsonConverter(typeof(UriConverter))]
        public Uri MediaUrl { get; set; }

        /// <summary>
        /// Gets or sets the URL pointing directly to the uploaded media file, for embedding on https pages.
        /// </summary>
        [JsonProperty("media_url_https")]
        [JsonConverter(typeof(UriConverter))]
        public Uri MediaUrlHttps { get; set; }

        /// <summary>
        /// Gets or sets the object showing available sizes for the media file.
        /// </summary>
        [JsonProperty("sizes")]
        public MediaSizes Sizes { get; set; }

        /// <summary>
        /// <para>Gets or sets the ID  points to the original Tweet.</para>
        /// <para>(Only for Tweets containing media that was originally associated with a different tweet.)</para>
        /// </summary>
        [JsonProperty("source_status_id")]
        public long? SourceStatusId { get; set; }

        /// <summary>
        /// Gets or sets the of uploaded media.
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

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
    /// Represents the size of the <see cref="CoreTweet.MediaSizes"/>.
    /// </summary>
    public class MediaSize : CoreBase
    {
        /// <summary>
        /// Gets or sets the height in pixels of the size.
        /// </summary>
        [JsonProperty("h")]
        public int Height { get; set; }

        /// <summary>
        /// <para>Gets or sets the resizing method used to obtain the size.</para>
        /// <para>A value of fit means that the media was resized to fit one dimension, keeping its native aspect ratio.</para>
        /// <para>A value of crop means that the media was cropped in order to fit a specific resolution.</para>
        /// </summary>
        [JsonProperty("resize")]
        public string Resize { get; set; }

        /// <summary>
        /// Gets or sets the width in pixels of the size.
        /// </summary>
        [JsonProperty("w")]
        public int Width { get; set; }
    }

    /// <summary>
    /// Represents the variations of the media.
    /// </summary>
    public class MediaSizes : CoreBase
    {
        /// <summary>
        /// Gets or sets the information for a large-sized version of the media.
        /// </summary>
        [JsonProperty("large")]
        public MediaSize Large { get; set; }

        /// <summary>
        /// Gets or sets the information for a medium-sized version of the media.
        /// </summary>
        [JsonProperty("medium")]
        public MediaSize Medium { get; set; }

        /// <summary>
        /// Gets or sets the information for a small-sized version of the media.
        /// </summary>
        [JsonProperty("small")]
        public MediaSize Small { get; set; }

        /// <summary>
        /// Gets or sets the information for a thumbnail-sized version of the media.
        /// </summary>
        [JsonProperty("thumb")]
        public MediaSize Thumb { get; set; }
    }

    /// <summary>
    /// Represents a URL object that contains the string for display and the raw URL.
    /// </summary>
    public class UrlEntity : Entity
    {
        /// <summary>
        /// Gets or sets the URL to display on clients.
        /// </summary>
        [JsonProperty("display_url")]
        public string DisplayUrl { get; set; }

        /// <summary>
        /// Gets or sets the expanded version of <see cref="CoreTweet.UrlEntity.DisplayUrl"/>.
        /// </summary>
        [JsonProperty("expanded_url")]
        [JsonConverter(typeof(UriConverter))]
        public Uri ExpandedUrl { get; set; }

        /// <summary>
        /// Gets or sets the wrapped URL, corresponding to the value embedded directly into the raw Tweet text, and the values for the indices parameter.
        /// </summary>
        [JsonProperty("url")]
        [JsonConverter(typeof(UriConverter))]
        public Uri Url { get; set; }
    }

    /// <summary>
    /// Represents a mention object that contains the user information.
    /// </summary>
    public class UserMentionEntity : Entity
    {
        /// <summary>
        /// Gets or sets the ID of the mentioned user.
        /// </summary>
        [JsonProperty("id")]
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets display name of the referenced user.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets screen name of the referenced user.
        /// </summary>
        [JsonProperty("screen_name")]
        public string ScreenName { get; set; }

        /// <summary>
        /// Returns the ID of this instance.
        /// </summary>
        /// <returns>The ID of this instance.</returns>
        public override string ToString()
        {
            return this.Id.ToString("D");
        }
    }
}