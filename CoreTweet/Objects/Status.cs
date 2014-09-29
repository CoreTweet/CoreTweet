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
using System.Collections.Generic;
using CoreTweet.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CoreTweet
{
    /// <summary>
    /// <para>Represents the Tweets, which are the basic atomic building block of all things Twitter.</para>
    /// <para>Users tweet Tweets, also known more generically as "status updates."</para>
    /// <para>Tweets can be embedded, replied to, favorited, unfavorited and deleted.</para>
    /// </summary>
    public class Status : CoreBase
    {
        /// <summary>
        /// <para>Gets or sets the integer representation of the unique identifier for this Tweet.</para>
        /// <para>See also: https://dev.twitter.com/docs/twitter-ids-json-and-snowflake</para>
        /// </summary>
        [JsonProperty("id")]
        public long Id { get; set; }

        /// <summary>
        /// <para>Gets or sets the collection of brief user objects (usually only one) indicating users who contributed to the authorship of the tweet, on behalf of the official tweet author.</para>
        /// <para>Nullable.</para>
        /// </summary>
        [JsonProperty("contributors")]
        public Contributors[] Contributors { get; set; }

        /// <summary>
        /// <para>Gets or sets the value represents the geographic location of the Tweet as reported by the user or client application.</para>
        /// <para>Nullable.</para>
        /// </summary>
        [JsonProperty("coordinates")]
        public Coordinates Coordinates { get; set; }

        /// <summary>
        /// Gets or sets the time when the Tweet was created.
        /// </summary>
        [JsonProperty("created_at")]
        [JsonConverter(typeof(DateTimeOffsetConverter))]
        public DateTimeOffset CreatedAt { get; set; }

        [JsonProperty("current_user_retweet")]
        private IDictionary<string, object> currentUserRetweetDic;

        /// <summary>
        /// <para>Gets or sets the Tweet ID of the user's own retweet of this Tweet, if exists.</para>
        /// <para>Only surfaces on methods supporting the include_my_retweet parameter, when set to true.</para>
        /// </summary>
        [JsonIgnore]
        public long? CurrentUserRetweet
        {
            get
            {
                return currentUserRetweetDic != null
                    ? (long?)currentUserRetweetDic["id"]
                    : null;
            }
            set
            {
                if (value.HasValue)
                    currentUserRetweetDic = new Dictionary<string, object>()
                    {
                        { "id", value.Value },
                        { "id_str", value.Value.ToString() }
                    };
                else
                    currentUserRetweetDic = null;
            }
        }

        /// <summary>
        /// Gets or sets the entities which have been parsed out of the text of the Tweet.
        /// </summary>
        [JsonProperty("entities")]
        public Entities Entities { get; set; }

        /// <summary>
        /// Gets or sets the extended entities which may have multiple entities data.
        /// </summary>
        [JsonProperty("extended_entities")]
        public Entities ExtendedEntities { get; set; }

        /// <summary>
        /// <para>Gets or sets a number of approximately how many times the Tweet has been favorited by Twitter users.</para>
        /// <para>Nullable.</para>
        /// </summary>
        [JsonProperty("favorite_count")]
        public int? FavoriteCount { get; set; }

        /// <summary>
        /// <para>Gets or sets a value that determines if the Tweet has been favorited by the authenticating user.</para>
        /// <para>Nullable.</para>
        /// </summary>
        [JsonProperty("favorited")]
        public bool? IsFavorited { get; set; }

        /// <summary>
        /// <para>Gets or sets a value that indicates the maximum value of the filter_level parameter which may be used and still stream this Tweet.</para>
        /// <para>Streaming only.</para>
        /// </summary>
        [JsonProperty("filter_level")]
        [JsonConverter(typeof(StringEnumConverter))]
        public FilterLevel? FilterLevel { get; set; }

        /// <summary>
        /// <para>Gets or sets the screen name of the original Tweet's author if the represented Tweet is a reply.</para>
        /// <para>Nullable.</para>
        /// </summary>
        [JsonProperty("in_reply_to_screen_name")]
        public string InReplyToScreenName { get; set; }

        /// <summary>
        /// <para>Gets or sets the integer representation of the original Tweet's ID if the represented Tweet is a reply.</para>
        /// <para>Nullable.</para>
        /// </summary>
        [JsonProperty("in_reply_to_status_id")]
        public long? InReplyToStatusId { get; set; }

        /// <summary>
        /// <para>Gets or sets the integer representation of the original Tweet's author ID if the represented Tweet is a reply.</para>
        /// <para>Nullable.</para>
        /// </summary>
        [JsonProperty("in_reply_to_user_id")]
        public long? InReplyToUserId { get; set; }

        /// <summary>
        /// <para>Gets or sets the BCP 47 language identifier.</para>
        /// <para>Nullable.</para>
        /// </summary>
        /// <value>A BCP 47 language identifier corresponding to the machine-detected language of the Tweet text, or <c>"und"</c> if no language could be detected.</value>
        [JsonProperty("lang")]
        public string Language { get; set; }

        /// <summary>
        /// <para>Gets or sets the place that the tweet is associated.</para>
        /// <para>Nullable.</para>
        /// <para>See also: https://dev.twitter.com/docs/platform-objects/places</para>
        /// </summary>
        [JsonProperty("place")]
        public Place Place { get; set; }

        /// <summary>
        /// <para>Gets or sets a value that determines if the URL contained in the tweet may contain content or media identified as sensitive content.</para>
        /// <para>This field only surfaces when a tweet contains a link and the meaning of the filed doesn't pertain to the Tweet content itself.</para>
        /// <para>Nullable.</para>
        /// </summary>
        [JsonProperty("possibly_sensitive")]
        public bool? PossiblySensitive { get; set; }

        /// <summary>
        /// <para>Gets or sets a set of key-value pairs indicatse the intended contextual delivery of the containing Tweet.</para>
        /// <para>Currently used by Twitter's Promoted Products.</para>
        /// </summary>
        [JsonProperty("scopes")]
        public Dictionary<string,object> Scopes { get; set; }

        /// <summary>
        /// <para>Gets or sets a number of approximately how many times the Tweet has been retweeted by Twitter users.</para>
        /// <para></para>
        /// </summary>
        [JsonProperty("retweet_count")]
        public int? RetweetCount { get; set; }

        /// <summary>
        /// <para>Gets or sets a value that determines if the Tweet has been retweeted by the authenticating user.</para>
        /// <para>Nullable.</para>
        /// </summary>
        [JsonProperty("retweeted")]
        public bool? IsRetweeted { get; set; }

        /// <summary>
        /// <para>Gets or sets the original Tweet if the status is a retweet.</para>
        /// <para>Users can amplify the broadcast of tweets authored by other users by retweeting.</para>
        /// <para>Retweets can be distinguished from typical Tweets by the existence of a retweeted_status attribute.</para>
        /// <para>This attribute contains a representation of the original Tweet that was retweeted.</para>
        /// <para>Note that retweets of retweets do not show representations of the intermediary retweet, but only the original tweet.</para>
        /// <para>(Users can also unretweet a retweet they created by deleting their retweet.) </para>
        /// </summary>
        [JsonProperty("retweeted_status")]
        public Status RetweetedStatus { get; set; }

        /// <summary>
        /// <para>Gets or sets the utility used to post the Tweet, as an HTML-formatted string.</para>
        /// <para>A tweet from the Twitter website has a value of "web" (case-insensitive).</para>
        /// </summary>
        [JsonProperty("source")]
        public string Source { get; set; }

        /// <summary>
        /// <para>Gets or sets the actual text of the status update.</para>
        /// <para>See also: https://github.com/twitter/twitter-text-rb/blob/master/lib/twitter-text/regex.rb</para>
        /// </summary>
        [JsonProperty("text")]
        public string Text { get; set; }

        /// <summary>
        /// <para>Gets or sets a value that determines if the value of the text parameter was truncated, for example, as a result of a retweet exceeding the 140 character Tweet length.</para>
        /// <para>Truncated text will end in ellipsis, like this ...</para>
        /// </summary>
        [Obsolete("Existed, but no longer used. (Since Twitter now rejects long Tweets vs truncating them, the large majority of Tweets will have this set to false.)")]
        [JsonProperty("truncated")]
        public bool? IsTruncated { get; set; }

        /// <summary>
        /// <para>Gets or sets the user who posted the Tweet.</para>
        /// <para>Perspectival attributes embedded within this object are unreliable.</para>
        /// <para>Seealso: https://dev.twitter.com/docs/platform-objects/users</para>
        /// </summary>
        [JsonProperty("user")]
        public User User { get; set; }

        /// <summary>
        /// <para>Gets or sets a value that determines if this piece of content has been withheld due to a DMCA complaint.</para>
        /// <para>See also: http://en.wikipedia.org/wiki/Digital_Millennium_Copyright_Act</para>
        /// </summary>
        [JsonProperty("withheld_copyright")]
        public bool? WithheldCopyright { get; set; }

        /// <summary>
        /// <para>Gets or sets a list of uppercase two-letter country codes this content is withheld from.</para>
        /// <para>See also: https://dev.twitter.com/blog/new-withheld-content-fields-api-responses</para>
        /// </summary>
        [JsonProperty("withheld_in_countries")]
        public string WithheldInCountries { get; set; }

        /// <summary>
        /// <para>Gets or sets the content being withheld is the "status" or a "user."</para>
        /// <para>See also: https://dev.twitter.com/blog/new-withheld-content-fields-api-responses</para>
        /// </summary>
        [JsonProperty("withheld_scope")]
        public string WithheldScope { get; set; }

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
    /// <para>Represents the Tweet with rate limit.</para>
    /// <para>Tweets are the basic atomic building block of all things Twitter.</para>
    /// <para>Users tweet Tweets, also known more generically as "status updates."</para>
    /// <para>Tweets can be embedded, replied to, favorited, unfavorited and deleted.</para>
    /// </summary>
    public class StatusResponse : Status, ITwitterResponse
    {
        /// <summary>
        /// Gets or sets the rate limit of the response.
        /// </summary>
        public RateLimit RateLimit { get; set; }

        /// <summary>
        /// Gets or sets the JSON of the response
        /// </summary>
        public string Json { get; set; }
    }

    /// <summary>
    /// <para>Represents the contributors, a collection of brief user objects (usually only one) indicating users who contributed to the authorship of the tweet, on behalf of the official tweet author.</para>
    /// </summary>
    [JsonConverter(typeof(ContributorsConverter))]
    public class Contributors : CoreBase
    {
        /// <summary>
        /// Gets or sets the integer representation of the ID of the user who contributed to the Tweet.
        /// </summary>
        [JsonProperty("id")]
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the screen name of the user who contributed to the Tweet.
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

    /// <summary>
    /// Represents the coordinates that the geographic location with longitude and latitude points.
    /// </summary>
    public class Coordinates : CoreBase
    {
        /// <summary>
        /// Gets or sets the longtitude of the location.
        /// </summary>
        public double Longtitude
        {
            get
            {
                return _coordinates[0];
            }
        }

        /// <summary>
        /// Gets or sets the latitude of the location.
        /// </summary>
        public double Latitude
        {
            get
            {
                return _coordinates[1];
            }
        }

        [JsonProperty("coordinates")]
        double[] _coordinates { get; set; }

        /// <summary>
        /// <para>Gets or sets the type of data encoded in the coordinates property.</para>
        /// <para>This will be "Point" for Tweet coordinates fields.</para>
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CoreTweet.Coordinates"/> class
        /// </summary>
        public Coordinates() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CoreTweet.Coordinates"/> class
        /// </summary>
        /// <param name="longtitude">The longtitude.</param>
        /// <param name="latitude">The latitude.</param>
        public Coordinates(double longtitude, double latitude)
            : this()
        {
            _coordinates = new double[2];
            _coordinates[0] = longtitude;
            _coordinates[1] = latitude;
            Type = "Polygon";
        }
    }

    /// <summary>
    /// Values of filter_level parameter.
    /// </summary>
    public enum FilterLevel
    {
        None, Low, Medium
    }

    /// <summary>
    /// Represents a direct message.
    /// </summary>
    public class DirectMessage : CoreBase
    {
        /// <summary>
        /// Gets or sets the integer representation of the unique identifier for the Direct message.
        /// </summary>
        [JsonProperty("id")]
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the sender of the Direct message.
        /// </summary>
        [JsonProperty("sender")]
        public User Sender{ get; set; }

        /// <summary>
        /// Gets or sets the Recipient of the Direct message.
        /// </summary>
        [JsonProperty("recipient")]
        public User Recipient{ get; set; }

        /// <summary>
        /// Gets or sets the time when the Direct message was created.
        /// </summary>
        [JsonProperty("created_at")]
        [JsonConverter(typeof(DateTimeOffsetConverter))]
        public DateTimeOffset CreatedAt{ get; set; }

        /// <summary>
        /// Gets or sets the entities which have been parsed out of the text of the Direct message.
        /// </summary>
        [JsonProperty("entities")]
        public Entities Entities{ get; set; }

        /// <summary>
        /// Gets or sets the actual text of the Direct message.
        /// </summary>
        [JsonProperty("text")]
        public string Text { get; set; }

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
    /// Represents a direct message with rate limit.
    /// </summary>
    public class DirectMessageResponse : DirectMessage, ITwitterResponse
    {
        /// <summary>
        /// Gets or sets the rate limit of the response.
        /// </summary>
        public RateLimit RateLimit { get; set; }

        /// <summary>
        /// Gets or sets the JSON of the response
        /// </summary>
        public string Json { get; set; }
    }

    /// <summary>
    /// Represents a collection of Tweets.
    /// </summary>
    [JsonObject]
    public class SearchResult : CoreBase, IEnumerable<Status>, ITwitterResponse
#if NET45 || WIN_RT || WP
    , IReadOnlyList<Status>
#endif
    {
        [JsonProperty("statuses")]
        private List<Status> statuses { get; set; }

        /// <summary>
        /// Gets or sets the metadata of the search.
        /// </summary>
        [JsonProperty("search_metadata")]
        public SearchMetadata SearchMetadata { get; set; }

        /// <summary>
        /// Gets or sets the rate limit of the response.
        /// </summary>
        public RateLimit RateLimit { get; set; }

        /// <summary>
        /// Gets or sets the JSON of the response
        /// </summary>
        public string Json { get; set; }

        /// <summary>
        /// Gets the element at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the element to get or set.</param>
        /// <returns>The element at the specified index.</returns>
        public Status this[int index]
        {
            get
            {
                return this.statuses[index];
            }
        }

        /// <summary>
        /// Gets the number of elements actually contained in the <see cref="SearchResult"/>.
        /// </summary>
        public int Count
        {
            get
            {
                return this.statuses.Count;
            }
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>An IEnumerator object that can be used to iterate through the collection.</returns>
        public IEnumerator<Status> GetEnumerator()
        {
            return this.statuses.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }

    /// <summary>
    /// Represents a metadata of the search.
    /// </summary>
    public class SearchMetadata : CoreBase
    {
        [JsonProperty("completed_in")]
        public double CompletedIn { get; set; }

        [JsonProperty("max_id")]
        public long MaxId { get; set; }

        [JsonProperty("since_id")]
        public long SinceId { get; set; }

        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("query")]
        public string Query { get; set; }

        [JsonProperty("next_results")]
        public string NextResults { get; set; }

        [JsonProperty("refresh_url")]
        public string RefreshUrl { get; set; }
    }
}