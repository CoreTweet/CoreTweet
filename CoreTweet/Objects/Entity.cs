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

namespace CoreTweet
{
    public class Entity : CoreBase
    {     
        /// <summary>
        ///     Represents hashtags which have been parsed out of the Tweet text.
        /// </summary>
        public HashTag[] HashTags { get; set; }

        /// <summary>
        ///     Represents media elements uploaded with the Tweet.
        /// </summary>
        public Media Media { get; set; }

        /// <summary>
        ///     Represents URLs included in the text of a Tweet or within textual fields of a user object.
        /// </summary>
        public Url[] Urls { get; set; }

        /// <summary>
        ///     Represents other Twitter users mentioned in the text of the Tweet.
        /// </summary>
        public UserMention[] UserMentions { get; set; }
  
        public Entity(Tokens tokens) : base(tokens) { }
        
        internal override void ConvertBase(dynamic e)
        {
            HashTags = e.IsDefined("hashtags") ? CoreBase.ConvertArray<HashTag>(this.Tokens, e.hashtags) : null;
            Media = e.IsDefined("media") ? CoreBase.Convert<Media>(this.Tokens, e.media) : null;
            Urls = e.IsDefined("urls") ? CoreBase.ConvertArray<Url>(this.Tokens, e.urls) : null;
            UserMentions = e.IsDefined("user_memtions") ? CoreBase.ConvertArray<UserMention>(this.Tokens, e.user_mentions) : null;
        }
    }

    public class HashTag : CoreBase
    {
        /// <summary>
        ///     Name of the hashtag, minus the leading '#' character.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        ///     An array of integers indicating the offsets within the Tweet text where the hashtag begins and ends. The first integer represents the location of the # character in the Tweet text string. The second integer represents the location of the first character after the hashtag. Therefore the difference between the two numbers will be the length of the hashtag name plus one (for the '#' character).
        /// </summary>
        public int[] Indices { get; set; }
  
        public HashTag(Tokens tokens) : base(tokens) { }
        
        internal override void ConvertBase(dynamic e)
        {
            Indices = new[] { (int)e.indices[0], (int)e.indices[1] };
            Text = (string)e.text;
        }
    }

    public class Media : CoreBase
    {
        /// <summary>
        ///     URL of the media to display to clients.
        /// </summary>
        public string DisplayUrl { get; set; }

        /// <summary>
        ///     An expanded version of display_url. Links to the media display page.
        /// </summary>
        public string ExpandedUrl { get; set; }

        /// <summary>
        ///     ID of the media expressed as a 64-bit integer.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        ///     An array of integers indicating the offsets within the Tweet text where the URL begins and ends. The first integer represents the location of the first character of the URL in the Tweet text. The second integer represents the location of the first non-URL character occurring after the URL (or the end of the string if the URL is the last part of the Tweet text).
        /// </summary>
        public int[] Indices { get; set; }

        /// <summary>
        ///     An http:// URL pointing directly to the uploaded media file.
        /// </summary>
        public Uri MediaUrl { get; set; }

        /// <summary>
        ///     An https:// URL pointing directly to the uploaded media file, for embedding on https pages.
        /// </summary>
        public Uri MediaUrlHttps { get; set; }

        /// <summary>
        ///     An object showing available sizes for the media file.
        /// </summary>
        public Sizes Sizes { get; set; }

        /// <summary>
        ///     For Tweets containing media that was originally associated with a different tweet, this ID points to the original Tweet.
        /// </summary>
        public long SourceStatusId { get; set; }

        /// <summary>
        ///     Type of uploaded media.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        ///     Wrapped URL for the media link. This corresponds with the URL embedded directly into the raw Tweet text, and the values for the indices parameter.
        /// </summary>
        public Uri Url { get; set; }
  
        public Media(Tokens tokens) : base(tokens) { }
        
        internal override void ConvertBase(dynamic e)
        {
            DisplayUrl = e.display_url;
            ExpandedUrl = e.expanded_url;
            Id = e.id;
            Indices = new int[]{e.indices[0],e.indices[1]};
            MediaUrl = new Uri(e.media_url);
            MediaUrlHttps = new Uri(e.media_url_https);
            Sizes = CoreBase.Convert<Sizes>(this.Tokens, e.sizes);
            SourceStatusId = e.source_status_id;
            Type = e.type;
            Url = new Uri(e.url);
        }
    }

    public class Size : CoreBase
    {
        /// <summary>
        ///     Height in pixels of this size.
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        ///     Resizing method used to obtain this size. A value of fit means that the media was resized to fit one dimension, keeping its native aspect ratio. A value of crop means that the media was cropped in order to fit a specific resolution.
        /// </summary>
        public string Resize { get; set; }

        /// <summary>
        ///     Width in pixels of this size.
        /// </summary>
        public int Width { get; set; }
  
        public Size(Tokens tokens) : base(tokens) { }
        
        internal override void ConvertBase(dynamic e)
        {
            Height = e.IsDefined("height") ? e.height : e.h;
            Resize = e.resize;
            Width = e.IsDefined("width") ? e.width : e.w;
        }
    }

    public class Sizes : CoreBase
    {
        /// <summary>
        ///     Information for a large-sized version of the media.
        /// </summary>
        public Size Large { get; set; }

        /// <summary>
        ///     Information for a medium-sized version of the media.
        /// </summary>
        public Size Medium { get; set; }

        /// <summary>
        ///     Information for a small-sized version of the media.
        /// </summary>
        public Size Small { get; set; }

        /// <summary>
        ///     Information for a thumbnail-sized version of the media.
        /// </summary>
        public Size Thumb { get; set; }

        public Sizes(Tokens tokens) : base(tokens) { }
        
        internal override void ConvertBase(dynamic e)
        {
            Large = CoreBase.Convert<Size>(this.Tokens, e.large);
            Medium = CoreBase.Convert<Size>(this.Tokens, e.medium);
            Small = CoreBase.Convert<Size>(this.Tokens, e.small);
            Thumb = CoreBase.Convert<Size>(this.Tokens, e.thumb);
        }
    }

    public class Url : CoreBase
    {
        /// <summary>
        ///     Version of the URL to display to clients.
        /// </summary>
        public string DisplayUrl { get; set; }

        /// <summary>
        ///     Expanded version of display_url.
        /// </summary>
        public string ExpandedUrl { get; set; }

        /// <summary>
        ///     An array of integers representing offsets within the Tweet text where the URL begins and ends. The first integer represents the location of the first character of the URL in the Tweet text. The second integer represents the location of the first non-URL character after the end of the URL.
        /// </summary>
        public int[] Indices { get; set; }

        /// <summary>
        ///     Wrapped URL, corresponding to the value embedded directly into the raw Tweet text, and the values for the indices parameter.
        /// </summary>
        public Uri Uri { get; set; }
  
        public Url(Tokens tokens) : base(tokens) { }
        
        internal override void ConvertBase(dynamic e)
        {
            DisplayUrl = e.display_url;
            ExpandedUrl = e.expanded_url;
            Indices = new int[]{e.indices[0],e.indices[1]};
            Uri = new Uri(e.uri);
        }
    }

    public class UserMention : CoreBase
    {
        /// <summary>
        ///     ID of the mentioned user, as an integer.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        ///     An array of integers representing the offsets within the Tweet text where the user reference begins and ends. The first integer represents the location of the '@' character of the user mention. The second integer represents the location of the first non-screenname character following the user mention.
        /// </summary>
        public int[] Indices { get; set; }

        /// <summary>
        ///     Display name of the referenced user.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Screen name of the referenced user.
        /// </summary>
        public string ScreenName { get; set; }
  
        public UserMention(Tokens tokens) : base(tokens) { }
        
        internal override void ConvertBase(dynamic e)
        {
            Id = e.id;
            Indices = new int[]{e.indices[0],e.indices[1]};
            Name = e.name;
            ScreenName = e.screen_name;
        }
    }
}