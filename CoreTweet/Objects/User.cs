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
using CoreTweet.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Converters;

namespace CoreTweet
{
    /// <summary>
    /// A user.
    /// </summary>
    public class User : CoreBase
    {
        // This is also a development example of 
        // changing over the JSON parser from DynamicJson to Newtonsoft.Json.
        // Parsing this class with Newtonsoft.Json is tested and perfectly works.
        // See also Objects/Converters.cs.

        /// <summary>
        ///     Indicates that the user has an account with "contributor mode" enabled, allowing for Tweets issued by the user to be co-authored by another account. Rarely true.
        /// </summary>
        [JsonProperty("contributors_enabled")]
        public bool IsContributorsEnabled { get; set; }

        /// <summary>
        ///     The UTC datetime that the user account was created on Twitter.
        /// </summary>
        [JsonProperty("created_at")]
        [JsonConverter(typeof(DateTimeOffsetConverter))]
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        ///     When true, indicates that the user has not altered the theme or background of their user profile.
        /// </summary>
        [JsonProperty("default_profile")]
        public bool IsDefaultProfile { get; set; }

        /// <summary>
        ///     When true, indicates that the user has not uploaded their own avatar and a default egg avatar is used instead.
        /// </summary>
        [JsonProperty("default_profile_image")]
        public bool IsDefaultProfileImage { get; set; }

        /// <summary>
        ///     Nullable. The user-defined UTF-8 string describing their account.
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        ///     Entities which have been parsed out of the url or description fields defined by the user.
        /// </summary>
        [JsonProperty("entities")]
        public Entity Entities { get; set; }

        /// <summary>
        ///     The number of tweets this user has favorited in the account's lifetime. British spelling used in the field name for historical reasons.
        /// </summary>
        [JsonProperty("favourites_count")]
        public int FavouritesCount { get; set; }

        /// <summary>
        ///     Nullable. Perspectival. When true, indicates that the authenticating user has issued a follow request to this protected user account.
        /// </summary>
        [JsonProperty("follow_request_sent")]
        public bool? IsFollowRequestSent { get; set; }

        /// <summary>
        ///     The number of followers this account currently has. Under certain conditions of duress, this field will temporarily indicate "0."
        /// </summary>
        [JsonProperty("followers_count")]
        public int FollowersCount { get; set; }

        /// <summary>
        ///     The number of users this account is following (AKA their "followings"). Under certain conditions of duress, this field will temporarily indicate "0."
        /// </summary>
        [JsonProperty("friends_count")]
        public int FriendsCount { get; set; }

        /// <summary>
        ///     When true, indicates that the user has enabled the possibility of geotagging their Tweets. This field must be true for the current user to attach geographic data when using POST statuses/update.
        /// </summary>
        [JsonProperty("geo_enabled")]
        public bool IsGeoEnabled { get; set; }
        
        /// <summary>
        ///     The integer representation of the unique identifier for this User. This number is greater than 53 bits and some programming languages may have difficulty/silent defects in interpreting it. Using a signed 64 bit integer for storing this identifier is safe. Use id_str for fetching the identifier to stay on the safe side.
        /// </summary>
        [JsonProperty("id")]
        public long? ID { get; set; }

        /// <summary>
        ///     When true, indicates that the user is a participant in Twitter's translator community.
        /// </summary>
        [JsonProperty("is_translator")]
        public bool IsTranslator { get; set; }

        /// <summary>
        ///     The BCP 47 code for the user's self-declared user interface language. May or may not have anything to do with the content of their Tweets.
        /// </summary>
        [JsonProperty("lang")]
        public string Language { get; set; }

        /// <summary>
        ///     The number of public lists that this user is a member of.
        /// </summary>
        [JsonProperty("listed_count")]
        public int ListedCount { get; set; }

        /// <summary>
        ///     Nullable. The user-defined location for this account's profile. Not necessarily a location nor parseable. This field will occasionally be fuzzily interpreted by the Search service.
        /// </summary>
        [JsonProperty("location")]
        public string Location { get; set; }

        /// <summary>
        ///     The name of the user, as they've defined it. Not necessarily a person's name. Typically capped at 20 characters, but subject to change.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        ///     The hexadecimal color chosen by the user for their background.
        /// </summary>
        [JsonProperty("profile_background_color")]
        public string ProfileBackgroundColor { get; set; }

        /// <summary>
        ///     A HTTP-based URL pointing to the background image the user has uploaded for their profile.
        /// </summary>
        [JsonProperty("profile_background_image_url")]
        [JsonConverter(typeof(UriConverter))]
        public Uri ProfileBackgroundImageUrl { get; set; }

        /// <summary>
        ///     A HTTPS-based URL pointing to the background image the user has uploaded for their profile.
        /// </summary>
        [JsonProperty("profile_background_image_url_https")]
        [JsonConverter(typeof(UriConverter))]
        public Uri ProfileBackgroundImageUrlHttps { get; set; }

        /// <summary>
        ///     When true, indicates that the user's profile_background_image_url should be tiled when displayed.
        /// </summary>
        [JsonProperty("profile_background_tile")]
        public bool IsProfileBackgroundTile { get; set; }

        /// <summary>
        ///     The HTTPS-based URL pointing to the standard web representation of the user's uploaded profile banner. By adding a final path element of the URL, you can obtain different image sizes optimized for specific displays. In the future, an API method will be provided to serve these URLs so that you need not modify the original URL. For size variations, please see User Profile Images and Banners.
        /// </summary>
        [JsonProperty("profile_banner_url")]
        [JsonConverter(typeof(UriConverter))]
        public Uri ProfileBannerUrl { get; set; }

        /// <summary>
        ///     A HTTP-based URL pointing to the user's avatar image. See User Profile Images and Banners.
        /// </summary>
        [JsonProperty("profile_image_url")]
        [JsonConverter(typeof(UriConverter))]
        public Uri ProfileImageUrl { get; set; }

        /// <summary>
        ///     A HTTPS-based URL pointing to the user's avatar image.
        /// </summary>
        [JsonProperty("profile_image_url_https")]
        [JsonConverter(typeof(UriConverter))]
        public Uri ProfileImageUrlHttps { get; set; }

        /// <summary>
        ///     The hexadecimal color the user has chosen to display links with in their Twitter UI.
        /// </summary>
        [JsonProperty("profile_link_color")]
        public string ProfileLinkColor { get; set; }

        /// <summary>
        ///     The hexadecimal color the user has chosen to display sidebar borders with in their Twitter UI.
        /// </summary>
        [JsonProperty("profile_sidebar_border_color")]
        public string ProfileSidebarBorderColor { get; set; }

        /// <summary>
        ///     The hexadecimal color the user has chosen to display sidebar backgrounds with in their Twitter UI.
        /// </summary>
        [JsonProperty("profile_sidebar_fill_color")]
        public string ProfileSidebarFillColor { get; set; }

        /// <summary>
        ///     The hexadecimal color the user has chosen to display text with in their Twitter UI.
        /// </summary>
        [JsonProperty("profile_text_color")]
        public string ProfileTextColor { get; set; }

        /// <summary>
        ///     When true, indicates the user wants their uploaded background image to be used.
        /// </summary>
        [JsonProperty("profile_use_background_image")]
        public bool IsProfileUseBackgroundImage { get; set; }

        /// <summary>
        ///     When true, indicates that this user has chosen to protect their Tweets.
        /// </summary>
        [JsonProperty("protected")]
        public bool IsProtected { get; set; }

        /// <summary>
        ///     The screen name, handle, or alias that this user identifies themselves with. screen_names are unique but subject to change. Use id_str as a user identifier whenever possible. Typically a maximum of 15 characters long, but some historical accounts may exist with longer names.
        /// </summary>
        [JsonProperty("screen_name")]
        public string ScreenName { get; set; }

        /// <summary>
        ///     Indicates that the user would like to see media inline. Somewhat disused.
        /// </summary>
        [JsonProperty("show_all_inline_media")]
        public bool? IsShowAllInlineMedia { get; set; }

        /// <summary>
        ///     Nullable. If possible, the user's most recent tweet or retweet. In some circumstances, this data cannot be provided and this field will be omitted, null, or empty. Perspectival attributes within tweets embedded within users cannot always be relied upon.
        /// </summary>
        [JsonProperty("status")]
        public Status Status { get; set; }

        /// <summary>
        ///     The number of tweets (including retweets) issued by the user.
        /// </summary>
        [JsonProperty("statuses_count")]
        public int StatusesCount { get; set; }

        /// <summary>
        ///     Nullable. A string describing the Time Zone this user declares themselves within.
        /// </summary>
        [JsonProperty("time_zone")]
        public string TimeZone { get; set; }

        /// <summary>
        ///     Nullable. A URL provided by the user in association with their profile.
        /// </summary>
        [JsonProperty("url")]
        [JsonConverter(typeof(UriConverter))]
        public Uri Url { get; set; }

        /// <summary>
        ///     Nullable. The offset from GMT/UTC in seconds.
        /// </summary>
        [JsonProperty("utc_offset")]
        public int? UtcOffset { get; set; }

        /// <summary>
        ///     When true, indicates that the user has a verified account.
        /// </summary>
        [JsonProperty("verified")]
        public bool IsVerified { get; set; }

        /// <summary>
        ///     When present, indicates a textual representation of the two-letter country codes this user is withheld from.
        /// </summary>
        [JsonProperty("withheld_in_countries")]
        public string WithheldInCountries { get; set; }

        /// <summary>
        ///     When present, indicates whether the content being withheld is the "status" or a "user."
        /// </summary>
        [JsonProperty("withheld_scope")]
        public string WithheldScope { get; set; }
    }
    
    public class RelationShip : CoreBase
    {
        /// <summary>
        /// The target of the relationship.
        /// </summary>
        /// <value>
        /// The target.
        /// </value>
        [JsonProperty("target")]
        public Friendship Target{ get; set; }

        /// <summary>
        /// The source of the relationship.
        /// </summary>
        /// <value>
        /// The source.
        /// </value>
        [JsonProperty("source")]
        public Friendship Source{ get; set; }
    }
    
    public class Friendship : CoreBase
    {
        /// <summary>
        /// The ID of the user.
        /// </summary>
        /// <value>
        /// The id.
        /// </value>
        [JsonProperty("id")]
        public long ID{ get; set; }

        /// <summary>
        /// The screen name of the user.
        /// </summary>
        /// <value>
        /// The screen name.
        /// </value>
        [JsonProperty("screen_name")]
        public string ScreenName{ get; set; }

        /// <summary>
        /// Gets whether you are following the user or not.
        /// </summary>
        /// <value>
        /// The bool value.
        /// </value>
        [JsonProperty("following")]
        public bool? IsFollowing{ get; set; }

        /// <summary>
        /// Gets whether the user is following you or not.
        /// </summary>
        /// <value>
        /// The bool value.
        /// </value>
        [JsonProperty("followed_by")]
        public bool? IsFollowedBy{ get; set; }

        /// <summary>
        /// When true, you can send a direct message to the user.
        /// </summary>
        /// <value>
        /// <c>true</c> if you can send direct message; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty("can_dm")]
        public bool? CanDm { get; set; }

        /// <summary>
        /// Gets or sets all replies.
        /// </summary>
        /// <value>
        /// All replies.
        /// </value>
        [JsonProperty("all_replies")]
        public bool? AllReplies{ get; set; }

        /// <summary>
        /// Gets whether the user wants retweets or not.
        /// </summary>
        /// <value>
        /// The bool value.
        /// </value>
        [JsonProperty("want_retweets")]
        public bool? WantsRetweets{ get; set; }

        /// <summary>
        /// Gets whether you are blocking the user or not.
        /// </summary>
        /// <value>
        /// The bool value.
        /// </value>
        [JsonProperty("blocking")]
        public bool? IsBlocking{ get; set; }

        /// <summary>
        /// Gets whether you marked the user as spam.
        /// </summary>
        /// <value>
        /// The bool value.
        /// </value>
        [JsonProperty("marked_spam")]
        public bool? IsMarkedSpam{ get; set; }

        /// <summary>
        /// Gets whether the notifications enabled or not.
        /// </summary>
        /// <value>
        /// The bool value.
        /// </value>
        [JsonProperty("notifications_enabled")]
        public bool? IsNotificationsEnabled{ get; set; }

        /// <summary>
        /// Gets the connections.
        /// </summary>
        /// <value>
        /// The connections.
        /// </value>
        [JsonProperty("connections")]
        public string[] Connections{ get; set; }
    }
    
    public class Category : CoreBase
    {
        /// <summary>
        /// The name of the category.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// The slug of the category.
        /// </summary>
        /// <value>
        /// The slug.
        /// </value>
        [JsonProperty("slug")]
        public string Slug{ get; set; }

        /// <summary>
        /// The size of the category.
        /// </summary>
        /// <value>
        /// The size.
        /// </value>
        [JsonProperty("size")]
        public int Size{ get; set; }
    }
}