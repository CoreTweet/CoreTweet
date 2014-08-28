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

using CoreTweet.Core;
using Newtonsoft.Json;

namespace CoreTweet
{
    /// <summary>
    /// Represents the settings including current trend, geo and sleep time information.
    /// </summary>
    public class Setting : CoreBase, ITwitterResponse
    {
        /// <summary>
        /// Gets or sets a value that determines if the connections always use https.
        /// </summary>
        [JsonProperty("always_use_https")]
        public bool AlwaysUseHttps { get; set; }

        /// <summary>
        /// Gets or sets a value that determines if your friends can discover you by your email address.
        /// </summary>
        [JsonProperty("discoverable_by_email")]
        public bool IsDiscoverableByEmail{ get; set; }

        /// <summary>
        /// <para>Gets or sets a value that determines if the user has enabled the possibility of geotagging their Tweets.</para>
        /// <para>This field must be true for the current user to attach geographic data when using POST statuses/update.</para>
        /// </summary>
        [JsonProperty("geo_enabled")]
        public bool GeoEnabled{ get; set; }

        /// <summary>
        /// <para>Gets or sets the BCP 47 code for the user's self-declared user interface language.</para>
        /// <para>May or may not have anything to do with the content of their Tweets.</para>
        /// </summary>
        [JsonProperty("language")]
        public string Language{ get; set; }

        /// <summary>
        /// Gets or sets a value that determines if the user has chosen to protect their Tweets.
        /// </summary>
        [JsonProperty("protected")]
        public bool IsProtected{ get; set; }

        /// <summary>
        /// <para>Gets or sets the screen name, handle, or alias that this user identifies themselves with.</para>
        /// <para>screen_names are unique but subject to change.</para>
        /// <para>Use id_str as a user identifier whenever possible.</para>
        /// <para>Typically a maximum of 15 characters long, but some historical accounts may exist with longer names.</para>
        /// </summary>
        [JsonProperty("screen_name")]
        public string ScreenName{ get; set; }

        /// <summary>
        /// Gets or sets a value that determines if the user would like to see media inline. Somewhat disused.
        /// </summary>
        [JsonProperty("show_all_inline_media")]
        public bool? ShowAllInlineMedia{ get; set; }

        /// <summary>
        /// Gets or sets the sleep time.
        /// </summary>
        [JsonProperty("sleep_time")]
        public SleepTime SleepTime{ get; set; }

        /// <summary>
        /// Gets or sets the time zone.
        /// </summary>
        [JsonProperty("time_zone")]
        public TimeZone TimeZone{ get; set; }

        /// <summary>
        /// Gets or sets the trend locaions.
        /// </summary>
        [JsonProperty("trend_location")]
        public Place[] TrendLocaion{ get; set; }

        /// <summary>
        /// Gets or sets the value that determines if the user has enabled the cookie personalization.
        /// </summary>
        [JsonProperty("use_cookie_personalization")]
        public bool UseCookiePersonalization { get; set; }

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
    /// Represents the sleep time of the user's location.
    /// </summary>
    public class SleepTime : CoreBase
    {
        /// <summary>
        /// Gets or sets a value that determines if the sleep time is enabled.
        /// </summary>
        [JsonProperty("enabled")]
        public bool IsEnabled { get; set; }

        /// <summary>
        /// Gets or sets the end time.
        /// </summary>
        [JsonProperty("end_time")]
        public int? EndTime { get; set; }

        /// <summary>
        /// Gets or sets the start time.
        /// </summary>
        [JsonProperty("start_time")]
        public int? StartTime { get; set; }
    }

    /// <summary>
    /// Gets or sets the timezone of the user.
    /// </summary>
    public class TimeZone : CoreBase
    {
        /// <summary>
        /// Gets or sets the name of the timezone.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the Rails TimeZone of the user's timezone.
        /// </summary>
        [JsonProperty("tzinfo_name")]
        public string InfoName { get; set; }

        /// <summary>
        /// Gets or sets the UTC offset from the user's timezone.
        /// </summary>
        [JsonProperty("utc_offset")]
        public int UtcOffset { get; set; }
    }
}

