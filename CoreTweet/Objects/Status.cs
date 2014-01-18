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
using System.Collections.Generic;
using CoreTweet.Core;
using CoreTweet.Streaming;
using Newtonsoft.Json;

namespace CoreTweet
{
    public class Status : CoreBase
    {
        /// <summary>
        ///     The integer representation of the unique identifier for this Tweet.
        /// </summary>
        /// <seealso cref="https://dev.twitter.com/docs/twitter-ids-json-and-snowflake" />
        [JsonProperty("id")]
        public long ID { get; set; }

        /// <summary>
        ///     Nullable. An collection of brief user objects (usually only one) indicating users who contributed to the authorship of the tweet, on behalf of the official tweet author.
        /// </summary>
        [JsonProperty("contributors")]
        public Contributors[] Contributors { get; set; }

        /// <summary>
        ///     Nullable. Represents the geographic location of this Tweet as reported by the user or client application.
        /// </summary>
        [JsonProperty("coordinates")]
        public Coordinates Coordinates { get; set; }

        /// <summary>
        ///     Time when this Tweet was created.
        /// </summary>
        [JsonProperty("created_at")]
        [JsonConverter(typeof(DateTimeOffsetConverter))]
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// <para>
        ///     Details the Tweet ID of the user's own retweet (if existent) of this Tweet.
        /// </para>
        /// <para>
        ///     Only surfaces on methods supporting the include_my_retweet parameter, when set to true.
        /// </para>
        /// </summary>
        [JsonProperty("current_user_retweet")]
        public long CurrentUserRetweet { get; set; }

        /// <summary>
        ///     Entities which have been parsed out of the text of the Tweet.
        /// </summary>
        [JsonProperty("entities")]
        public Entity Entities { get; set; }

        /// <summary>
        ///     Nullable. Perspectival. Indicates whether this Tweet has been favorited by the authenticating user.
        /// </summary>
        [JsonProperty("favorited")]
        public bool? IsFavorited { get; set; }

        /// <summary>
        ///     Nullable. If the represented Tweet is a reply, this field will contain the screen name of the original Tweet's author.
        /// </summary>
        [JsonProperty("in_reply_to_screen_name")]
        public string InReplyToScreenName { get; set; }

        /// <summary>
        ///     Nullable. If the represented Tweet is a reply, this field will contain the integer representation of the original Tweet's ID.
        /// </summary>
        [JsonProperty("in_reply_to_status_id")]
        public long? InReplyToStatusID { get; set; }

        /// <summary>
        ///     Nullable. If the represented Tweet is a reply, this field will contain the integer representation of the original Tweet's author ID.
        /// </summary>
        [JsonProperty("in_reply_to_user_id")]
        public long? InReplyToUserID { get; set; }

        /// <summary>
        ///     Nullable. When present, indicates that the tweet is associated (but not necessarily originating from) a Place.
        /// </summary>
        /// <seealso cref="https://dev.twitter.com/docs/platform-objects/places" />
        [JsonProperty("place")]
        public Place Place { get; set; }

        /// <summary>
        ///     Nullable. This field only surfaces when a tweet contains a link.
        ///     The meaning of the field doesn't pertain to the tweet content itself, but instead it is an indicator
        ///     that the URL contained in the tweet may contain content or media identified as sensitive content.
        /// </summary>
        [JsonProperty("possibly_sensitive")]
        public bool? PossiblySensitive { get; set; }

        /// <summary>
        ///     A set of key-value pairs indicating the intended contextual delivery of the containing Tweet. Currently used by Twitter's Promoted Products.
        /// </summary>
        [JsonProperty("scopes")]
        public Dictionary<string, object> Scopes { get; set; }

        /// <summary>
        ///     Number of times this Tweet has been retweeted.
        /// </summary>
        [JsonProperty("retweet_count")]
        public int RetweetCount { get; set; }

        /// <summary>
        ///     Perspectival. Indicates whether this Tweet has been retweeted by the authenticating user.
        /// </summary>
        [JsonProperty("retweeted")]
        public bool? IsRetweeted { get; set; }

        /// <summary>
        /// <para> Users can amplify the broadcast of tweets authored by other users by retweeting. </para> 
        /// <para> Retweets can be distinguished from typical Tweets by the existence of a retweeted_status attribute. </para>
        /// <para> This attribute contains a representation of the original Tweet that was retweeted. </para> 
        /// <para> Note that retweets of retweets do not show representations of the intermediary retweet, but only the original tweet. (Users can also unretweet a retweet they created by deleting their retweet.) </para> 
        /// </summary>
        /// <value>
        /// The retweeted status.
        /// </value>
        [JsonProperty("retweeted_status")]
        public Status RetweetedStatus { get; set; }

        /// <summary>
        ///     Utility used to post the Tweet, as an HTML-formatted string. Tweets from the Twitter website have a source value of web.
        /// </summary>
        [JsonProperty("source")]
        public string Source { get; set; }

        /// <summary>
        ///     The actual UTF-8 text of the status update.
        /// </summary>
        /// <seealso cref="https://github.com/twitter/twitter-text-rb/blob/master/lib/twitter-text/regex.rb" />
        [JsonProperty("text")]
        public string Text { get; set; }

        /// <summary>
        ///     Indicates whether the value of the text parameter was truncated, for example, as a result of a retweet exceeding the 140 character Tweet length.
        ///     Truncated text will end in ellipsis, like this ...
        /// </summary>
        [Obsolete(
            "Existed, but no longer used.(Since Twitter now rejects long Tweets vs truncating them, the large majority of Tweets will have this set to false.)"
            )]
        [JsonProperty("truncated")]
        public bool? IsTruncated { get; set; }

        /// <summary>
        ///     The user who posted this Tweet. Perspectival attributes embedded within this object are unreliable.
        /// </summary>
        /// <seealso cref="https://dev.twitter.com/docs/platform-objects/users" />
        [JsonProperty("user")]
        public User User { get; set; }

        /// <summary>
        ///     When present and set to "true", it indicates that this piece of content has been withheld due to a DMCA complaint.
        /// </summary>
        /// <seealso cref="http://en.wikipedia.org/wiki/Digital_Millennium_Copyright_Act" />
        [JsonProperty("withheld_copyright")]
        public bool? WithheldCopyright { get; set; }

        /// <summary>
        ///     When present, indicates a list of uppercase two-letter country codes this content is withheld from.
        /// </summary>
        /// <see cref="https://dev.twitter.com/blog/new-withheld-content-fields-api-responses" />
        [JsonProperty("withheld_in_countries")]
        public string WithheldInCountries { get; set; }

        /// <summary>
        ///     When present, indicates whether the content being withheld is the "status" or a "user".
        /// </summary>
        /// <see cref="https://dev.twitter.com/blog/new-withheld-content-fields-api-responses" />
        [JsonProperty("withheld_scope")]
        public string WithheldScope { get; set; }
    }

    public class Contributors : CoreBase
    {
        /// <summary>
        ///     The integer representation of the ID of the user who contributed to this Tweet.
        /// </summary>
        [JsonProperty("id")]
        public long ID { get; set; }

        /// <summary>
        ///     The screen name of the user who contributed to this Tweet.
        /// </summary>
        [JsonProperty("screen_name")]
        public string ScreenName { get; set; }
    }

    public class Coordinates : CoreBase
    {
        /// <summary>
        ///     The longtitude of the Tweet's location.
        /// </summary>
        public double Longtitude
        {
            get
            {
                return _coordinates[0];
            }
        }

        /// <summary>
        ///     The latitude of the Tweet's location.
        /// </summary>
        public double Latitude
        {
            get
            {
                return _coordinates[1];
            }
        }

        /// <summary>
        /// The array of coordinates.
        /// Used internally.
        /// </summary>
        /// <value>
        /// Coordinates.
        /// </value>
        [JsonProperty("coordinates")]
        double[] _coordinates { get; set; }

        /// <summary>
        ///     The type of data encoded in the coordinates property. This will be "Point" for Tweet coordinates fields.
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }
    }

    public class DirectMessage : CoreBase
    {
        /// <summary>
        /// The sender of this Direct message.
        /// </summary>
        [JsonProperty("sender")]
        public User Sender{ get; set; }

        /// <summary>
        /// The Recipient of this Direct message.
        /// </summary>
        [JsonProperty("recipient")]
        public User Recipient{ get; set; }
        
        /// <summary>
        ///     Time when this Direct message was created.
        /// </summary>
        [JsonProperty("created_at")]
        [JsonConverter(typeof(DateTimeOffsetConverter))]
        public DateTimeOffset CreatedAt{ get; set; }
  
        /// <summary>
        ///     Entities which have been parsed out of the text of the Direct message.
        /// </summary>
        [JsonProperty("entities")]
        public Entity Entities{ get; set; }

        /// <summary>
        /// The actual UTF-8 text of the status update.
        /// </summary>
        /// <value>
        /// The text.
        /// </value>
        [JsonProperty("text")]
        public string Text { get; set; }
    }
    
}