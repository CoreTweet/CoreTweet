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
using Newtonsoft.Json;

namespace CoreTweet
{
    public class Setting : CoreBase
    {
        /// <summary>
        /// When true, always use https.
        /// </summary>
        /// <value>
        /// <c>true</c> if always use https; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty("always_use_https")]
        public bool AlwaysUseHttps { get; set; }

        /// <summary>
        /// When true, your friend can discover you by your email address.
        /// </summary>
        [JsonProperty("discoverable_by_email")]
        public bool IsDiscoverableByEmail{ get; set; }

        /// <summary>
        ///     When true, indicates that the user has enabled the possibility of geotagging their Tweets. This field must be true for the current user to attach geographic data when using POST statuses/update.
        /// </summary>
        [JsonProperty("geo_enabled")]
        public bool GeoEnabled{ get; set; }

        /// <summary>
        ///     The BCP 47 code for the user's self-declared user interface language. May or may not have anything to do with the content of their Tweets.
        /// </summary>
        [JsonProperty("language")]
        public string Language{ get; set; }
  
        /// <summary>
        ///     When true, indicates that this user has chosen to protect their Tweets.
        /// </summary>
        [JsonProperty("protected")]
        public bool IsProtected{ get; set; }
  
        /// <summary>
        ///     The screen name, handle, or alias that this user identifies themselves with. screen_names are unique but subject to change. Use id_str as a user identifier whenever possible. Typically a maximum of 15 characters long, but some historical accounts may exist with longer names.
        /// </summary>
        [JsonProperty("screen_name")]
        public string ScreenName{ get; set; }
  
        /// <summary>
        ///     Indicates that the user would like to see media inline. Somewhat disused.
        /// </summary>
        [JsonProperty("show_all_inline_media")]
        public bool? ShowAllInlineMedia{ get; set; }
        
        /// <summary>
        /// <para>Gets or sets the sleep time.</para>
        /// </summary>
        [JsonProperty("sleep_time")]
        public SleepTime SleepTime{ get; set; }
        
        /// <summary>
        /// <para>Gets or sets the time zone.</para>
        /// </summary>
        [JsonProperty("time_zone")]
        public TimeZone TimeZone{ get; set; }
        
        /// <summary>
        /// <para>Gets or sets the trend locaion.</para>
        /// </summary>
        [JsonProperty("trend_location")]
        public Place TrendLocaion{ get; set; }

        [JsonProperty("use_cookie_personalization")]
        public bool UseCookiePersonalization { get; set; }
    }

    public class SleepTime : CoreBase
    {
        [JsonProperty("enabled")]
        public bool IsEnabled { get; set; }

        [JsonProperty("end_time")]
        public int? EndTime { get; set; }

        [JsonProperty("start_time")]
        public int? StartTime { get; set; }
    }

    public class TimeZone : CoreBase
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("tzinfo_name")]
        public string InfoName { get; set; }

        [JsonProperty("utc_offset")]
        public int UtcOffset { get; set; }
    }
}

