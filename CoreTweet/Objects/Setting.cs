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
        public bool AlwaysUseHttps { get; set; }

        /// <summary>
        /// When true, your friend can discover you by your email address.
        /// </summary>
        public bool DiscoverableByEmail{ get; set; }

        /// <summary>
        ///     When true, indicates that the user has enabled the possibility of geotagging their Tweets. This field must be true for the current user to attach geographic data when using POST statuses/update.
        /// </summary>
        public bool GeoEnabled{ get; set; }

        /// <summary>
        ///     The BCP 47 code for the user's self-declared user interface language. May or may not have anything to do with the content of their Tweets.
        /// </summary>
        public string Language{ get; set; }
  
        /// <summary>
        ///     When true, indicates that this user has chosen to protect their Tweets.
        /// </summary>
        public bool Protected{ get; set; }
  
        /// <summary>
        ///     The screen name, handle, or alias that this user identifies themselves with. screen_names are unique but subject to change. Use id_str as a user identifier whenever possible. Typically a maximum of 15 characters long, but some historical accounts may exist with longer names.
        /// </summary>
        public string ScreenName{ get; set; }
  
        /// <summary>
        ///     Indicates that the user would like to see media inline. Somewhat disused.
        /// </summary>
        public bool? ShowAllInlineMedia{ get; set; }
        
        /// <summary>
        /// <para>Gets or sets the sleep time.</para>
        /// <para>Values: </para>
        /// <para>bool Enabled</para>
        /// <para>int EndTime</para>
        /// <para>int StartTime</para>
        /// </summary>
        public Dictionary<string,object> SleepTime{ get; set; }
        
        /// <summary>
        /// <para>Gets or sets the time zone.</para>
        /// <para>Values: </para>
        /// <para>string Name</para>
        /// <para>string TzinfoName</para>
        /// <para>int UtcOffset</para>
        /// </summary>
        public Dictionary<string,object> TimeZone{ get; set; }
        
        /// <summary>
        /// <para>Gets or sets the trend locaion.</para>
        /// <para>Values: </para>
        /// <para>string Country</para>
        /// <para>string CountryCode</para>
        /// <para>string Name</para>
        /// <para>long ParentId</para>
        /// <para>Dictionary(string,object) PlaceType [ int Code, string Name ]</para>
        /// </summary>
        public Dictionary<string,object> TrendLocaion{ get; set; }
        
        public bool UseCookiePersonalization { get; set; }
        
        public Setting(Tokens tokens) : base(tokens) { }
        
        internal override void ConvertBase(dynamic e)
        {
            AlwaysUseHttps = e.always_use_https;
            DiscoverableByEmail = e.discoverable_by_email;
            GeoEnabled = e.geo_enabled;
            Language = e.language;
            Protected = e.@protected;
            ScreenName = e.screen_name;
            ShowAllInlineMedia = e.IsDefined("show_all_inline_media") ? (bool?)e.show_all_inline_media : null;
            SleepTime = new Dictionary<string, object>()
            {
                {"Enabled", e.sleep_time.enabled},
                {"EndTime", e.sleep_time.enabled ? e.sleet_time.end_time : -1},
                {"StartTime", e.sleep_time.enabled ? e.sleet_time.start_time : -1},
            };
            TimeZone = new Dictionary<string, object>()
            {
                {"Name", e.time_zone.name},
                {"TzinfoName", e.time_zone.tzinfo_name},
                {"UtcOffset", e.time_zone.utc_offset}
            };
            TrendLocaion = new Dictionary<string, object>()
            {
                {"Country", e.IsDefined("trend_location.country") ? e.trend_location.country : null},
                {"CountryCode", e.IsDefined("trend_location.country_code") ? e.trend_location.country_code : null},
                {"Name", e.IsDefined("trend_location.country_code") ? e.trend_location.name : null},
                {"ParentId", e.IsDefined("trend_location.parent_id") ? e.trend_location.parent_id : null},
                {"PlaceType",e.IsDefined("trend_location.place_type") ? new Dictionary<string,object>(){ {"Code",e.trend_location.place_type.code},{"Name",e.trend_location.place_type.name} } : null},
            };
            UseCookiePersonalization = e.use_cookie_personalization;
        }
    }
}

