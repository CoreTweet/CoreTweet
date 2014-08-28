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
using Newtonsoft.Json.Serialization;

namespace CoreTweet
{
    /// <summary>
    /// Represents a user.
    /// </summary>
    public class User : CoreBase
    {
        /// <summary>
        /// Gets or sets a value that determines if the user has an account with "contributor mode" enabled, allowing for Tweets issued by the user to be co-authored by another account. Rarely true.
        /// </summary>
        [JsonProperty("contributors_enabled")]
        public bool IsContributorsEnabled { get; set; }

        /// <summary>
        /// Gets or sets the UTC datetime that the user account was created on Twitter.
        /// </summary>
        [JsonProperty("created_at")]
        [JsonConverter(typeof(DateTimeOffsetConverter))]
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets a value that determines if the user has not altered the theme or background of its user profile.
        /// </summary>
        [JsonProperty("default_profile")]
        public bool IsDefaultProfile { get; set; }

        /// <summary>
        /// Gets or sets a value that determines if the user has not uploaded their own avatar and a default egg avatar is used instead.
        /// </summary>
        [JsonProperty("default_profile_image")]
        public bool IsDefaultProfileImage { get; set; }

        /// <summary>
        /// <para>Gets or sets the user-defined string describing their account.</para>
        /// <para>Nullable.</para>
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the entities which have been parsed out of the URL or description fields defined by the user.
        /// </summary>
        [JsonProperty("entities")]
        public UserEntities Entities { get; set; }

        /// <summary>
        /// <para>Gets or sets the number of tweets this user has favorited in the account's lifetime.</para>
        /// <para>British spelling used in the field name for historical reasons.</para>
        /// </summary>
        [JsonProperty("favourites_count")]
        public int FavouritesCount { get; set; }

        /// <summary>
        /// <para>Gets or sets a value that determines if the authenticating user has issued a follow request to this protected user account.</para>
        /// <para>Nullable.</para>
        /// </summary>
        [JsonProperty("follow_request_sent")]
        public bool? IsFollowRequestSent { get; set; }

        /// <summary>
        /// <para>Gets or sets the number of followers this account currently has.</para>
        /// <para>Under certain conditions of duress, the field will temporarily indicates 0.</para>
        /// </summary>
        [JsonProperty("followers_count")]
        public int FollowersCount { get; set; }

        /// <summary>
        /// <para>Gets or sets the number of the account is following (AKA its followings).</para>
        /// <para>Under certain conditions of duress, the field will temporarily indicates 0.</para>
        /// </summary>
        [JsonProperty("friends_count")]
        public int FriendsCount { get; set; }

        /// <summary>
        /// <para>Gets or sets a value that determines if the user has enabled the possibility of geotagging their Tweets.</para>
        /// <para>This field must be true for the current user to attach geographic data when using statuses/update.</para>
        /// </summary>
        [JsonProperty("geo_enabled")]
        public bool IsGeoEnabled { get; set; }

        /// <summary>
        /// Gets or sets the integer representation of the unique identifier for this User.
        /// </summary>
        [JsonProperty("id")]
        public long? Id { get; set; }

        /// <summary>
        /// Gets or sets a value that determines if the user is a participant in Twitter's translator community.
        /// </summary>
        [JsonProperty("is_translator")]
        public bool IsTranslator { get; set; }

        /// <summary>
        /// <para>Gets or sets the BCP 47 code for the user's self-declared user interface language.</para>
        /// <para>May or may not have anything to do with the content of their Tweets.</para>
        /// </summary>
        [JsonProperty("lang")]
        public string Language { get; set; }

        /// <summary>
        /// Gets or sets the number of public lists that the user is a member of.
        /// </summary>
        [JsonProperty("listed_count")]
        public int? ListedCount { get; set; }

        /// <summary>
        /// <para>Gets or sets the user-defined location for this account's profile.</para>
        /// <para>Not necessarily a location nor parsable.</para>
        /// <para>This field will occasionally be fuzzily interpreted by the Search service.</para>
        /// <para>Nullable.</para>
        /// </summary>
        [JsonProperty("location")]
        public string Location { get; set; }

        /// <summary>
        /// <para>Gets or sets the name of the user, as they've defined it.</para>
        /// <para>Not necessarily a person's name.</para>
        /// <para>Typically capped at 20 characters, but subject to be changed.</para>
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the hexadecimal color chosen by the user for their background.
        /// </summary>
        [JsonProperty("profile_background_color")]
        public string ProfileBackgroundColor { get; set; }

        /// <summary>
        /// Gets or sets a HTTP-based URL pointing to the background image the user has uploaded for their profile.
        /// </summary>
        [JsonProperty("profile_background_image_url")]
        [JsonConverter(typeof(UriConverter))]
        public Uri ProfileBackgroundImageUrl { get; set; }

        /// <summary>
        /// Gets or sets a HTTPS-based URL pointing to the background image the user has uploaded for their profile.
        /// </summary>
        [JsonProperty("profile_background_image_url_https")]
        [JsonConverter(typeof(UriConverter))]
        public Uri ProfileBackgroundImageUrlHttps { get; set; }

        /// <summary>
        /// Gets or sets a value that determines if the user's <see cref="CoreTweet.User.ProfileBackgroundImageUrl"/> should be tiled when displayed.
        /// </summary>
        [JsonProperty("profile_background_tile")]
        public bool IsProfileBackgroundTile { get; set; }

        /// <summary>
        /// Gets or sets a HTTPS-based URL pointing to the standard web representation of the user's uploaded profile banner. By adding a final path element of the URL, you can obtain different image sizes optimized for specific displays. In the future, an API method will be provided to serve these URLs so that you need not modify the original URL. For size variations, please see User Profile Images and Banners.
        /// </summary>
        [JsonProperty("profile_banner_url")]
        [JsonConverter(typeof(UriConverter))]
        public Uri ProfileBannerUrl { get; set; }

        /// <summary>
        /// Gets or sets a HTTP-based URL pointing to the user's avatar image. See User Profile Images and Banners.
        /// </summary>
        [JsonProperty("profile_image_url")]
        [JsonConverter(typeof(UriConverter))]
        public Uri ProfileImageUrl { get; set; }

        /// <summary>
        /// Gets or sets a HTTPS-based URL pointing to the user's avatar image.
        /// </summary>
        [JsonProperty("profile_image_url_https")]
        [JsonConverter(typeof(UriConverter))]
        public Uri ProfileImageUrlHttps { get; set; }

        /// <summary>
        /// Gets or sets the hexadecimal color the user has chosen to display links with in their Twitter UI.
        /// </summary>
        [JsonProperty("profile_link_color")]
        public string ProfileLinkColor { get; set; }

        /// <summary>
        /// Gets or sets the hexadecimal color the user has chosen to display sidebar borders with in their Twitter UI.
        /// </summary>
        [JsonProperty("profile_sidebar_border_color")]
        public string ProfileSidebarBorderColor { get; set; }

        /// <summary>
        /// Gets or sets the hexadecimal color the user has chosen to display sidebar backgrounds with in their Twitter UI.
        /// </summary>
        [JsonProperty("profile_sidebar_fill_color")]
        public string ProfileSidebarFillColor { get; set; }

        /// <summary>
        /// Gets or sets the hexadecimal color the user has chosen to display text with in their Twitter UI.
        /// </summary>
        [JsonProperty("profile_text_color")]
        public string ProfileTextColor { get; set; }

        /// <summary>
        /// Gets or sets a value that determines if the user wants their uploaded background image to be used.
        /// </summary>
        [JsonProperty("profile_use_background_image")]
        public bool IsProfileUseBackgroundImage { get; set; }

        /// <summary>
        /// Gets or sets a value that determines if the user has chosen to protect their Tweets.
        /// </summary>
        [JsonProperty("protected")]
        public bool IsProtected { get; set; }

        /// <summary>
        /// <para>Gets or sets the screen name, handle, or alias that this user identifies themselves with.</para>
        /// <para><see cref="CoreTweet.User.ScreenName"/> are unique but subject to be changed.</para>
        /// <para>Use <see cref="CoreTweet.User.Id"/> as a user identifier whenever possible.</para>
        /// <para>Typically a maximum of 15 characters long, but some historical accounts may exist with longer names.</para>
        /// </summary>
        [JsonProperty("screen_name")]
        public string ScreenName { get; set; }

        /// <summary>
        /// Gets or sets a value that determines if the user would like to see media inline. Somewhat disused.
        /// </summary>
        [JsonProperty("show_all_inline_media")]
        public bool? IsShowAllInlineMedia { get; set; }

        /// <summary>
        /// <para>Gets or sets the user's most recent tweet or retweet.</para>
        /// <para>In some circumstances, this data cannot be provided and this field will be omitted, null, or empty.</para>
        /// <para>Perspectival attributes within tweets embedded within users cannot always be relied upon.</para>
        /// </summary>
        [JsonProperty("status")]
        public Status Status { get; set; }

        /// <summary>
        /// Gets or sets the number of tweets (including retweets) issued by the user.
        /// </summary>
        [JsonProperty("statuses_count")]
        public int StatusesCount { get; set; }

        /// <summary>
        /// <para>Gets or sets the string describes the time zone the user declares themselves within.</para>
        /// <para>Nullable.</para>
        /// </summary>
        [JsonProperty("time_zone")]
        public string TimeZone { get; set; }

        /// <summary>
        /// <para>Gets or sets the URL provided by the user in association with their profile.</para>
        /// <para>Nullable.</para>
        /// </summary>
        [JsonProperty("url")]
        [JsonConverter(typeof(UriConverter))]
        public Uri Url { get; set; }

        /// <summary>
        /// <para>Gets or sets the offset from GMT/UTC in seconds.</para>
        /// <para>Nullable.</para>
        /// </summary>
        [JsonProperty("utc_offset")]
        public int? UtcOffset { get; set; }

        /// <summary>
        /// Gets or sets a value that determines if the user has a verified account.
        /// </summary>
        [JsonProperty("verified")]
        public bool IsVerified { get; set; }

        /// <summary>
        /// Gets or sets a textual representation of the two-letter country codes this user is withheld from.
        /// </summary>
        [JsonProperty("withheld_in_countries")]
        public string WithheldInCountries { get; set; }

        /// <summary>
        /// Gets or sets the content being withheld is the "status" or a "user."
        /// </summary>
        [JsonProperty("withheld_scope")]
        public string WithheldScope { get; set; }

        /// <summary>
        /// Gets or sets a value that determines if the user is muted by authenticating user.
        /// </summary>
        [JsonProperty("muting")]
        public bool? IsMuting { get; set; }

        /// <summary>
        /// Returns the ID of this instance.
        /// </summary>
        /// <returns>The ID of this instance.</returns>
        public override string ToString()
        {
            return this.Id.HasValue ? this.Id.Value.ToString("D") : "";
        }
    }

    /// <summary>
    /// Represents a user response with rate limit.
    /// </summary>
    public class UserResponse : User, ITwitterResponse
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
    /// Represents a relationship with aother user.
    /// </summary>
    public class RelationShip : CoreBase
    {
        /// <summary>
        /// Gets or sets the target of the relationship.
        /// </summary>
        [JsonProperty("target")]
        public Friendship Target{ get; set; }

        /// <summary>
        /// Gets or sets the source of the relationship.
        /// </summary>
        [JsonProperty("source")]
        public Friendship Source{ get; set; }
    }

    /// <summary>
    /// Represents a relationship with aother user response with rate limit.
    /// </summary>
    public class RelationShipResponse : RelationShip, ITwitterResponse
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
    /// Represents a frienship.
    /// </summary>
    public class Friendship : CoreBase
    {
        /// <summary>
        /// Gets or sets the ID of the user.
        /// </summary>
        [JsonProperty("id")]
        public long Id{ get; set; }

        /// <summary>
        /// Gets or sets the screen name of the user.
        /// </summary>
        [JsonProperty("screen_name")]
        public string ScreenName{ get; set; }

        /// <summary>
        /// Gets or sets a value that determines if you are following the user.
        /// </summary>
        [JsonProperty("following")]
        public bool? IsFollowing{ get; set; }

        /// <summary>
        /// Gets or sets a value that determines if the user is following you.
        /// </summary>
        [JsonProperty("followed_by")]
        public bool? IsFollowedBy{ get; set; }

        /// <summary>
        /// Gets or sets a value that determines if you can send a direct message to the user.
        /// </summary>
        [JsonProperty("can_dm")]
        public bool? CanDM { get; set; }

        /// <summary>
        /// Gets or sets a value that determines if you get all replies.
        /// </summary>
        [JsonProperty("all_replies")]
        public bool? AllReplies{ get; set; }

        /// <summary>
        /// Gets or sets a value that determines if you want retweets or not.
        /// </summary>
        [JsonProperty("want_retweets")]
        public bool? WantsRetweets{ get; set; }

        /// <summary>
        /// Gets or sets a value that determines if you are blocking the user.
        /// </summary>
        [JsonProperty("blocking")]
        public bool? IsBlocking{ get; set; }

        /// <summary>
        /// Gets or sets a value that determines if you marked the user as spam.
        /// </summary>
        [JsonProperty("marked_spam")]
        public bool? IsMarkedSpam{ get; set; }

        /// <summary>
        /// Gets or sets a value that determines if the notifications of the user enabled or not.
        /// </summary>
        [JsonProperty("notifications_enabled")]
        public bool? IsNotificationsEnabled{ get; set; }

        /// <summary>
        /// Gets or sets the connections.
        /// </summary>
        [JsonProperty("connections")]
        public string[] Connections{ get; set; }

        /// <summary>
        /// Gets or sets a value that determines if you are muting the user.
        /// </summary>
        [JsonProperty("muting")]
        public bool? IsMuting { get; set; }
    }

    /// <summary>
    /// Represents a category.
    /// </summary>
    public class Category : CoreBase
    {
        /// <summary>
        /// Gets or sets the name of the category.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the slug of the category.
        /// </summary>
        [JsonProperty("slug")]
        public string Slug{ get; set; }

        /// <summary>
        /// Gets or sets the size of the category.
        /// </summary>
        [JsonProperty("size")]
        public int Size{ get; set; }

        /// <summary>
        /// Gets or sets the users in this category.
        /// </summary>
        [JsonProperty("users")]
        public User[] Users { get; set; }
    }

    /// <summary>
    /// Represents a category response with rate limit.
    /// </summary>
    public class CategoryResponse : Category, ITwitterResponse
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
    /// Represents the variations of a size of a profile banner.
    /// </summary>
    public class ProfileBannerSizes : CoreBase, ITwitterResponse
    {
        /// <summary>
        /// Gets or sets the size for Web.
        /// </summary>
        [JsonProperty("web")]
        public ProfileBannerSize Web { get; set; }

        /// <summary>
        /// Gets or sets the size for Web with high resolution devices.
        /// </summary>
        [JsonProperty("web_retina")]
        public ProfileBannerSize WebRetina { get; set; }

        /// <summary>
        /// Gets or sets the size for Apple iPad.
        /// </summary>
        [JsonProperty("ipad")]
        public ProfileBannerSize IPad { get; set; }

        /// <summary>
        /// Gets or sets the size for Apple iPad with high resolution.
        /// </summary>
        [JsonProperty("ipad_retina")]
        public ProfileBannerSize IPadRetina { get; set; }

        /// <summary>
        /// Gets or sets the size for mobile devices.
        /// </summary>
        [JsonProperty("mobile")]
        public ProfileBannerSize Mobile { get; set; }

        /// <summary>
        /// Gets or sets the size for mobile devices with high resolution devices.
        /// </summary>
        [JsonProperty("mobile_retina")]
        public ProfileBannerSize MobileRetina { get; set; }

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
    /// Represents a size of a profile banner.
    /// </summary>
    public class ProfileBannerSize : CoreBase
    {
        /// <summary>
        /// Gets or sets the width in pixels of the size.
        /// </summary>
        [JsonProperty("w")]
        public int Width { get; set; }

        /// <summary>
        /// Gets or sets the height in pixels of the size.
        /// </summary>
        [JsonProperty("h")]
        public int Height { get; set; }

        /// <summary>
        /// Gets or sets the URL of the size.
        /// </summary>
        [JsonProperty("url")]
        [JsonConverter(typeof(UriConverter))]
        public Uri Url { get; set; }
    }

    /// <summary>
    /// Represents an entity object for user.
    /// </summary>
    public class UserEntities : CoreBase
    {
        /// <summary>
        /// Gets or sets the entities for <see cref="CoreTweet.User.Url"/> field.
        /// </summary>
        [JsonProperty("url")]
        public Entities Url { get; set; }

        /// <summary>
        /// Gets or sets the entities for <see cref="CoreTweet.User.Description"/> field.
        /// </summary>
        [JsonProperty("description")]
        public Entities Description { get; set; }
    }
}