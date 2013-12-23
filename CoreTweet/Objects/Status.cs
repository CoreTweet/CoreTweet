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
using System.Data.Linq;
using CoreTweet.Core;

namespace CoreTweet
{
    public class Status : CoreBase
    {
        /// <summary>
        ///     The integer representation of the unique identifier for this Tweet.
        /// </summary>
        /// <seealso cref="https://dev.twitter.com/docs/twitter-ids-json-and-snowflake" />
        public long? Id { get; set; }

        /// <summary>
        ///     Nullable. An collection of brief user objects (usually only one) indicating users who contributed to the authorship of the tweet, on behalf of the official tweet author.
        /// </summary>
        public Contributors[] Contributors { get; set; }

        /// <summary>
        ///     Nullable. Represents the geographic location of this Tweet as reported by the user or client application.
        /// </summary>
        public Coordinates Coordinates { get; set; }

        /// <summary>
        ///     Time when this Tweet was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        ///     Details the Tweet ID of the user's own retweet (if existent) of this Tweet.
        /// </summary>
        /// 
        ///     Only surfaces on methods supporting the include_my_retweet parameter, when set to true.
        /// 
        public long CurrentUserRetweet { get; set; }

        /// <summary>
        ///     Entities which have been parsed out of the text of the Tweet.
        /// </summary>
        public Entity[] Entities { get; set; }

        /// <summary>
        ///     Nullable. Perspectival. Indicates whether this Tweet has been favorited by the authenticating user.
        /// </summary>
        public bool? IsFavorited { get; set; }

        /// <summary>
        ///     Nullable. If the represented Tweet is a reply, this field will contain the screen name of the original Tweet's author.
        /// </summary>
        public string InReplyToScreenName { get; set; }

        /// <summary>
        ///     Nullable. If the represented Tweet is a reply, this field will contain the integer representation of the original Tweet's ID.
        /// </summary>
        public long? InReplyToStatusId { get; set; }

        /// <summary>
        ///     Nullable. If the represented Tweet is a reply, this field will contain the integer representation of the original Tweet's author ID.
        /// </summary>
        public long? InReplyToUserId { get; set; }

        /// <summary>
        ///     Nullable. When present, indicates that the tweet is associated (but not necessarily originating from) a Place.
        /// </summary>
        /// <seealso cref="https://dev.twitter.com/docs/platform-objects/places" />
        public Place Place { get; set; }

        /// <summary>
        ///     Nullable. This field only surfaces when a tweet contains a link.
        ///     The meaning of the field doesn't pertain to the tweet content itself, but instead it is an indicator
        ///     that the URL contained in the tweet may contain content or media identified as sensitive content.
        /// </summary>
        public bool? PossiblySensitive { get; set; }

        /// <summary>
        ///     A set of key-value pairs indicating the intended contextual delivery of the containing Tweet. Currently used by Twitter's Promoted Products.
        /// </summary>
        public Dictionary<string, object> Scopes { get; set; }

        /// <summary>
        ///     Number of times this Tweet has been retweeted.
        /// </summary>
        public int RetweetCount { get; set; }

        /// <summary>
        ///     Perspectival. Indicates whether this Tweet has been retweeted by the authenticating user.
        /// </summary>
        public bool? IsRetweeted { get; set; }

        /// <summary>
        ///     Utility used to post the Tweet, as an HTML-formatted string. Tweets from the Twitter website have a source value of web.
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        ///     The actual UTF-8 text of the status update.
        /// </summary>
        /// <seealso cref="https://github.com/twitter/twitter-text-rb/blob/master/lib/twitter-text/regex.rb" />
        public string Text { get; set; }

        /// <summary>
        ///     Indicates whether the value of the text parameter was truncated, for example, as a result of a retweet exceeding the 140 character Tweet length.
        ///     Truncated text will end in ellipsis, like this ...
        /// </summary>
        [Obsolete(
            "Existed, but no longer used.(Since Twitter now rejects long Tweets vs truncating them, the large majority of Tweets will have this set to false.)"
            )]
        public bool? Truncated { get; set; }

        /// <summary>
        ///     The user who posted this Tweet. Perspectival attributes embedded within this object are unreliable.
        /// </summary>
        /// <seealso cref="https://dev.twitter.com/docs/platform-objects/users" />
        public User User { get; set; }

        /// <summary>
        ///     When present and set to "true", it indicates that this piece of content has been withheld due to a DMCA complaint.
        /// </summary>
        /// <seealso cref="http://en.wikipedia.org/wiki/Digital_Millennium_Copyright_Act" />
        public bool? WithheldCopyright { get; set; }

        /// <summary>
        ///     When present, indicates a list of uppercase two-letter country codes this content is withheld from.
        /// </summary>
        /// <see cref="https://dev.twitter.com/blog/new-withheld-content-fields-api-responses" />
        public string WithheldInCountries { get; set; }

        /// <summary>
        ///     When present, indicates whether the content being withheld is the "status" or a "user".
        /// </summary>
        /// <see cref="https://dev.twitter.com/blog/new-withheld-content-fields-api-responses" />
        public string WithheldScope { get; set; }
  
        public Status(Tokens tokens) : base(tokens)
        {
            Tokens = tokens;
        }
        
        internal override void ConvertBase(dynamic e)
        {
            Id = e.IsDefined("id") ? (long?)e.id : null;
            Contributors = e.IsDefined("contributors") ? CoreBase.ConvertArray<Contributors>(this.Tokens, e.contributors) : null;
			Coordinates = e.IsDefined ("coordinates") ? CoreBase.Convert<Coordinates> (this.Tokens, e.coordinates) : null;
            CreatedAt = DateTimeOffset.ParseExact(e.created_at, "ddd MMM dd HH:mm:ss K yyyy",
                                                  System.Globalization.DateTimeFormatInfo.InvariantInfo, 
			                                      System.Globalization.DateTimeStyles.AllowWhiteSpaces);
            CurrentUserRetweet = e.IsDefined("current_user_retweet") ? e.current_user_retweet.id : -1;
            Entities = e.IsDefined("entities") ? CoreBase.ConvertArray<Entity>(this.Tokens, e.entities) : null;
            IsFavorited = e.IsDefined("favorited") ? e.favorited : null;
			InReplyToScreenName = e.IsDefined("in_reply_to_screen_name") ? e.in_reply_to_screen_name : null;
            InReplyToStatusId = (long?)e.in_reply_to_status_id;
            InReplyToUserId = (long?)e.in_reply_to_user_id;
            Place = e.place != null ? CoreBase.Convert<Place>(this.Tokens, e.place) : null;
            PossiblySensitive = e.IsDefined("possibly_sensitive") ? e.possibly_sensitive : null;
            //UNDONE: Parse scopes
            RetweetCount = (int)e.retweet_count;
            IsRetweeted = e.retweeted;
            Source = e.source;
            Text = e.text;
            Truncated = e.IsDefined("truncated") ? e.truncated : null;
            User = e.IsDefined("user") ? CoreBase.Convert<User>(this.Tokens, e.user) : null;
            WithheldCopyright = e.IsDefined("withheld_copyright") ? (bool?)e.withheld_copyright : null;
            WithheldInCountries = e.IsDefined("withheld_in_countries") ? e.withheld_in_countries : null;
            WithheldScope = e.IsDefined("withheld_scope") ? e.withheld_scope : null;
        }
    }

    public class Contributors : CoreBase
    {
        /// <summary>
        ///     The integer representation of the ID of the user who contributed to this Tweet.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        ///     The screen name of the user who contributed to this Tweet.
        /// </summary>
        public string ScreenName { get; set; }
  
        public Contributors(Tokens tokens) : base(tokens) { }
        
        internal override void ConvertBase(dynamic e)
        {
            Id = (long)e.id;
            ScreenName = e.screen_name;
        }
    }

    public class Coordinates : CoreBase
    {
        /// <summary>
        ///     The longtitude of the Tweet's location.
        /// </summary>
        public double Longtitude { get; set; }

        /// <summary>
        ///     The latitude of the Tweet's location.
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        ///     The type of data encoded in the coordinates property. This will be "Point" for Tweet coordinates fields.
        /// </summary>
        public string Type { get; set; }
  
        public Coordinates(Tokens tokens) : base(tokens) { }
        
        internal override void ConvertBase(dynamic e)
        {
            if(e != null)
            {
                Longtitude = e.coordinates[0];
                Latitude = e.coordinates[1];
                Type = e.type;
            }
        }
    }

    public class DirectMessage : CoreBase
    {
        /// <summary>
        /// The sender of this Direct message.
        /// </summary>
        public User Sender{ get; set; }

        /// <summary>
        /// The Recipient of this Direct message.
        /// </summary>
        public User Recipient{ get; set; }
        
        /// <summary>
        ///     Time when this Direct message was created.
        /// </summary>
        public DateTimeOffset CreatedAt{ get; set; }
  
        /// <summary>
        ///     Entities which have been parsed out of the text of the Direct message.
        /// </summary>
        public Entity Entities{ get; set; }
        
        public DirectMessage(Tokens tokens) : base(tokens) { }
        
        internal override void ConvertBase(dynamic e)
        {
            Sender = CoreBase.Convert<User>(this.Tokens, e.sender);
            Recipient = CoreBase.Convert<User>(this.Tokens, e.recipient);
			CreatedAt = DateTimeOffset.ParseExact(e.created_at, "ddd MMM dd HH:mm:ss K yyyy",
			                                      System.Globalization.DateTimeFormatInfo.InvariantInfo, 
			                                      System.Globalization.DateTimeStyles.AllowWhiteSpaces);
			Entities = CoreBase.Convert<Entity>(this.Tokens, e.entities);
        }
    }
    
}