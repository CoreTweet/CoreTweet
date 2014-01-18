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
using System.Linq;
using CoreTweet.Core;
using Newtonsoft.Json;

namespace CoreTweet
{
    public class Entity : CoreBase
    {     
        /// <summary>
        ///     Represents hashtags which have been parsed out of the Tweet text.
        /// </summary>
        [JsonProperty("hashtags")]
        public HashTag[] HashTags { get; set; }

        /// <summary>
        ///     Represents media elements uploaded with the Tweet.
        /// </summary>
        [JsonProperty("media")]
        public Media[] Media { get; set; }

        /// <summary>
        ///     Represents URLs included in the text of a Tweet or within textual fields of a user object.
        /// </summary>
        [JsonProperty("urls")]
        public Url[] Urls { get; set; }

        /// <summary>
        ///     Represents other Twitter users mentioned in the text of the Tweet.
        /// </summary>
        [JsonProperty("user_mentions")]
        public UserMention[] UserMentions { get; set; }
    }

    public class HashTag : CoreBase
    {
        /// <summary>
        ///     Name of the hashtag, minus the leading '#' character.
        /// </summary>
        [JsonProperty("text")]
        public string Text { get; set; }

        /// <summary>
        ///     An array of integers indicating the offsets within the Tweet text where the hashtag begins and ends. The first integer represents the location of the # character in the Tweet text string. The second integer represents the location of the first character after the hashtag. Therefore the difference between the two numbers will be the length of the hashtag name plus one (for the '#' character).
        /// </summary>
        [JsonProperty("indices")]
        public int[] Indices { get; set; }
    }

    public class Media : CoreBase
    {
        /// <summary>
        ///     URL of the media to display to clients.
        /// </summary>
        [JsonProperty("display_url")]
        //[JsonConverter(typeof(UriConverter))]
        public string DisplayUrl { get; set; }

        /// <summary>
        ///     An expanded version of display_url. Links to the media display page.
        /// </summary>
        [JsonProperty("expanded_url")]
        [JsonConverter(typeof(UriConverter))]
        public Uri ExpandedUrl { get; set; }

        /// <summary>
        ///     ID of the media expressed as a 64-bit integer.
        /// </summary>
        [JsonProperty("id")]
        public long ID { get; set; }

        /// <summary>
        ///     An array of integers indicating the offsets within the Tweet text where the URL begins and ends. The first integer represents the location of the first character of the URL in the Tweet text. The second integer represents the location of the first non-URL character occurring after the URL (or the end of the string if the URL is the last part of the Tweet text).
        /// </summary>
        [JsonProperty("indices")]
        public int[] Indices { get; set; }

        /// <summary>
        ///     An http:// URL pointing directly to the uploaded media file.
        /// </summary>
        [JsonProperty("media_url")]
        [JsonConverter(typeof(UriConverter))]
        public Uri MediaUrl { get; set; }

        /// <summary>
        ///     An https:// URL pointing directly to the uploaded media file, for embedding on https pages.
        /// </summary>
        [JsonProperty("media_url_https")]
        [JsonConverter(typeof(UriConverter))]
        public Uri MediaUrlHttps { get; set; }

        /// <summary>
        ///     An object showing available sizes for the media file.
        /// </summary>
        [JsonProperty("sizes")]
        public Sizes Sizes { get; set; }

        /// <summary>
        ///     For Tweets containing media that was originally associated with a different tweet, this ID points to the original Tweet.
        /// </summary>
        [JsonProperty("source_status_id")]
        public long SourceStatusID { get; set; }

        /// <summary>
        ///     Type of uploaded media.
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        ///     Wrapped URL for the media link. This corresponds with the URL embedded directly into the raw Tweet text, and the values for the indices parameter.
        /// </summary>
        [JsonProperty("url")]
        [JsonConverter(typeof(UriConverter))]
        public Uri Url { get; set; }
    }

    public class Size : CoreBase
    {
        /// <summary>
        ///     Height in pixels of this size.
        /// </summary>
        [JsonProperty("height")]
        public int Height
        {
            get { return h; }
            set { h = value;}
        }

        [JsonProperty("h")]
        int h { get; set; }

        /// <summary>
        ///     Resizing method used to obtain this size. A value of fit means that the media was resized to fit one dimension, keeping its native aspect ratio. A value of crop means that the media was cropped in order to fit a specific resolution.
        /// </summary>
        [JsonProperty("resize")]
        public string Resize { get; set; }

        /// <summary>
        ///     Width in pixels of this size.
        /// </summary>
        [JsonProperty("width")]
        public int Width
        {
            get { return w; }
            set { w = value;}
        }

        [JsonProperty("w")]
        int w { get; set; }
    }

    public class Sizes : CoreBase
    {
        /// <summary>
        ///     Information for a large-sized version of the media.
        /// </summary>
        [JsonProperty("large")]
        public Size Large { get; set; }

        /// <summary>
        ///     Information for a medium-sized version of the media.
        /// </summary>
        [JsonProperty("medium")]
        public Size Medium { get; set; }

        /// <summary>
        ///     Information for a small-sized version of the media.
        /// </summary>
        [JsonProperty("small")]
        public Size Small { get; set; }

        /// <summary>
        ///     Information for a thumbnail-sized version of the media.
        /// </summary>
        [JsonProperty("thumb")]
        public Size Thumb { get; set; }
    }

    public class Url : CoreBase
    {
        /// <summary>
        ///     Version of the URL to display to clients.
        /// </summary>
        [JsonProperty("display_url")]
        //[JsonConverter(typeof(UriConverter))]
        public string DisplayUrl { get; set; }

        /// <summary>
        ///     Expanded version of display_url.
        /// </summary>
        [JsonProperty("expanded_url")]
        [JsonConverter(typeof(UriConverter))]
        public Uri ExpandedUrl { get; set; }

        /// <summary>
        ///     An array of integers representing offsets within the Tweet text where the URL begins and ends. The first integer represents the location of the first character of the URL in the Tweet text. The second integer represents the location of the first non-URL character after the end of the URL.
        /// </summary>
        [JsonProperty("indices")]
        public int[] Indices { get; set; }

        /// <summary>
        ///     Wrapped URL, corresponding to the value embedded directly into the raw Tweet text, and the values for the indices parameter.
        /// </summary>
        [JsonProperty("url")]
        [JsonConverter(typeof(UriConverter))]
        public Uri Uri { get; set; }
    }

    public class UserMention : CoreBase
    {
        /// <summary>
        ///     ID of the mentioned user, as an integer.
        /// </summary>
        [JsonProperty("id")]
        public long ID { get; set; }

        /// <summary>
        ///     An array of integers representing the offsets within the Tweet text where the user reference begins and ends. The first integer represents the location of the '@' character of the user mention. The second integer represents the location of the first non-screenname character following the user mention.
        /// </summary>
        [JsonProperty("indices")]
        public int[] Indices { get; set; }

        /// <summary>
        ///     Display name of the referenced user.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        ///     Screen name of the referenced user.
        /// </summary>
        [JsonProperty("screen_name")]
        public string ScreenName { get; set; }
    }
}