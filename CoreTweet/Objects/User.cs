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

namespace CoreTweet
{
    public class User : CoreBase
    {
        /// <summary>
        ///     Indicates that the user has an account with "contributor mode" enabled, allowing for Tweets issued by the user to be co-authored by another account. Rarely true.
        /// </summary>
        public bool IsContributorsEnabled { get; set; }

        /// <summary>
        ///     The UTC datetime that the user account was created on Twitter.
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        ///     When true, indicates that the user has not altered the theme or background of their user profile.
        /// </summary>
        public bool IsDefaultProfile { get; set; }

        /// <summary>
        ///     When true, indicates that the user has not uploaded their own avatar and a default egg avatar is used instead.
        /// </summary>
        public bool IsDefaultProfileImage { get; set; }

        /// <summary>
        ///     Nullable. The user-defined UTF-8 string describing their account.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     Entities which have been parsed out of the url or description fields defined by the user.
        /// </summary>
        public Entity Entities { get; set; }

        /// <summary>
        ///     The number of tweets this user has favorited in the account's lifetime. British spelling used in the field name for historical reasons.
        /// </summary>
        public int FavouritesCount { get; set; }

        /// <summary>
        ///     Nullable. Perspectival. When true, indicates that the authenticating user has issued a follow request to this protected user account.
        /// </summary>
        public bool? IsFollowRequestSent { get; set; }

        /// <summary>
        ///     The number of followers this account currently has. Under certain conditions of duress, this field will temporarily indicate "0."
        /// </summary>
        public int FollowersCount { get; set; }

        /// <summary>
        ///     The number of users this account is following (AKA their "followings"). Under certain conditions of duress, this field will temporarily indicate "0."
        /// </summary>
        public int FriendsCount { get; set; }

        /// <summary>
        ///     When true, indicates that the user has enabled the possibility of geotagging their Tweets. This field must be true for the current user to attach geographic data when using POST statuses/update.
        /// </summary>
        public bool IsGeoEnabled { get; set; }

        /// <summary>
        ///     The integer representation of the unique identifier for this User. This number is greater than 53 bits and some programming languages may have difficulty/silent defects in interpreting it. Using a signed 64 bit integer for storing this identifier is safe. Use id_str for fetching the identifier to stay on the safe side.
        /// </summary>
        public long? Id { get; set; }

        /// <summary>
        ///     When true, indicates that the user is a participant in Twitter's translator community.
        /// </summary>
        public bool IsTranslator { get; set; }

        /// <summary>
        ///     The BCP 47 code for the user's self-declared user interface language. May or may not have anything to do with the content of their Tweets.
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        ///     The number of public lists that this user is a member of.
        /// </summary>
        public int ListedCount { get; set; }

        /// <summary>
        ///     Nullable. The user-defined location for this account's profile. Not necessarily a location nor parseable. This field will occasionally be fuzzily interpreted by the Search service.
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        ///     The name of the user, as they've defined it. Not necessarily a person's name. Typically capped at 20 characters, but subject to change.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     The hexadecimal color chosen by the user for their background.
        /// </summary>
        public string ProfileBackgroundColor { get; set; }

        /// <summary>
        ///     A HTTP-based URL pointing to the background image the user has uploaded for their profile.
        /// </summary>
        public Uri ProfileBackgroundImageUrl { get; set; }

        /// <summary>
        ///     A HTTPS-based URL pointing to the background image the user has uploaded for their profile.
        /// </summary>
        public Uri ProfileBackgroundImageUrlHttps { get; set; }

        /// <summary>
        ///     When true, indicates that the user's profile_background_image_url should be tiled when displayed.
        /// </summary>
        public bool IsProfileBackgroundTile { get; set; }

        /// <summary>
        ///     The HTTPS-based URL pointing to the standard web representation of the user's uploaded profile banner. By adding a final path element of the URL, you can obtain different image sizes optimized for specific displays. In the future, an API method will be provided to serve these URLs so that you need not modify the original URL. For size variations, please see User Profile Images and Banners.
        /// </summary>
        public Uri ProfileBannerUrl { get; set; }

        /// <summary>
        ///     A HTTP-based URL pointing to the user's avatar image. See User Profile Images and Banners.
        /// </summary>
        public Uri ProfileImageUrl { get; set; }

        /// <summary>
        ///     A HTTPS-based URL pointing to the user's avatar image.
        /// </summary>
        public Uri ProfileImageUrlHttps { get; set; }

        /// <summary>
        ///     The hexadecimal color the user has chosen to display links with in their Twitter UI.
        /// </summary>
        public string ProfileLinkColor { get; set; }

        /// <summary>
        ///     The hexadecimal color the user has chosen to display sidebar borders with in their Twitter UI.
        /// </summary>
        public string ProfileSidebarBorderColor { get; set; }

        /// <summary>
        ///     The hexadecimal color the user has chosen to display sidebar backgrounds with in their Twitter UI.
        /// </summary>
        public string ProfileSidebarFillColor { get; set; }

        /// <summary>
        ///     The hexadecimal color the user has chosen to display text with in their Twitter UI.
        /// </summary>
        public string ProfileTextColor { get; set; }

        /// <summary>
        ///     When true, indicates the user wants their uploaded background image to be used.
        /// </summary>
        public bool IsProfileUseBackgroundImage { get; set; }

        /// <summary>
        ///     When true, indicates that this user has chosen to protect their Tweets.
        /// </summary>
        public bool IsProtected { get; set; }

        /// <summary>
        ///     The screen name, handle, or alias that this user identifies themselves with. screen_names are unique but subject to change. Use id_str as a user identifier whenever possible. Typically a maximum of 15 characters long, but some historical accounts may exist with longer names.
        /// </summary>
        public string ScreenName { get; set; }

        /// <summary>
        ///     Indicates that the user would like to see media inline. Somewhat disused.
        /// </summary>
        public bool? IsShowAllInlineMedia { get; set; }

        /// <summary>
        ///     Nullable. If possible, the user's most recent tweet or retweet. In some circumstances, this data cannot be provided and this field will be omitted, null, or empty. Perspectival attributes within tweets embedded within users cannot always be relied upon.
        /// </summary>
        public Status Status { get; set; }

        /// <summary>
        ///     The number of tweets (including retweets) issued by the user.
        /// </summary>
        public int StatusesCount { get; set; }

        /// <summary>
        ///     Nullable. A string describing the Time Zone this user declares themselves within.
        /// </summary>
        public string TimeZone { get; set; }

        /// <summary>
        ///     Nullable. A URL provided by the user in association with their profile.
        /// </summary>
        public Uri Url { get; set; }

        /// <summary>
        ///     Nullable. The offset from GMT/UTC in seconds.
        /// </summary>
        public DateTimeOffset UtcOffset { get; set; }

        /// <summary>
        ///     When true, indicates that the user has a verified account.
        /// </summary>
        public bool IsVerified { get; set; }

        /// <summary>
        ///     When present, indicates a textual representation of the two-letter country codes this user is withheld from.
        /// </summary>
        public string WithheldInCountries { get; set; }

        /// <summary>
        ///     When present, indicates whether the content being withheld is the "status" or a "user."
        /// </summary>
        public string WithheldScope { get; set; }
        
        public User(Tokens tokens) : base(tokens) { }
        
        internal override void ConvertBase(dynamic e)
        {
			IsContributorsEnabled = (bool)e.contributors_enabled;
			CreatedAt = DateTimeOffset.ParseExact(e.created_at, "ddd MMM dd HH:mm:ss K yyyy",
			                                      System.Globalization.DateTimeFormatInfo.InvariantInfo, 
			                                      System.Globalization.DateTimeStyles.AllowWhiteSpaces);
			IsDefaultProfile = (bool)e.default_profile;
            IsDefaultProfileImage = (bool)e.default_profile_image;
            Description = (string)e.description;
            Entities = e.IsDefined("entities") ? CoreBase.Convert<Entity>(this.Tokens, e.entities) : null;
            FavouritesCount = (int)e.favourites_count;
            IsFollowRequestSent = (bool?)e.follow_request_sent;
            FollowersCount = (int)e.followers_count;
            FriendsCount = (int)e.friends_count;
            IsGeoEnabled = (bool)e.geo_enabled;
            Id = (long?)e.id;
            IsTranslator = (bool)e.is_translator;
            Language = (string)e.lang;
            ListedCount = (int)e.listed_count;
            Location = (string)e.location;
            Name = (string)e.name;
            ProfileBackgroundImageUrl = new Uri((string)e.profile_background_image_url);
            ProfileBackgroundImageUrlHttps = new Uri((string)e.profile_background_image_url_https);
            IsProfileBackgroundTile = (bool)e.profile_background_tile;
            ProfileBannerUrl = e.IsDefined("profile_banner_url") ? new Uri((string)e.profile_banner_url) : null;
            ProfileImageUrl = new Uri((string)e.profile_image_url);
            ProfileImageUrlHttps = new Uri((string)e.profile_image_url_https);
            ProfileLinkColor = (string)e.profile_link_color;
            ProfileSidebarBorderColor = (string)e.profile_sidebar_border_color;
            ProfileSidebarFillColor = (string)e.profile_sidebar_fill_color;
            ProfileTextColor = (string)e.profile_text_color;
            IsProfileUseBackgroundImage = (bool)e.profile_use_background_image;
            IsProtected = (bool)e.@protected;
            ScreenName = (string)e.screen_name;
            IsShowAllInlineMedia = e.IsDefined("show_all_inline_media") ? (bool?)e.show_all_inline_media : null;
            Status = e.IsDefined("status") ? CoreBase.Convert<Status>(this.Tokens, e.status) : null;
            StatusesCount = (int)e.statuses_count;
            TimeZone = (string)e.time_zone;
            Url = e.url == null ? null : new Uri((string)e.url);
            UtcOffset = DateTimeOffset.FromFileTime((long)e.utc_offset);
            IsVerified = (bool)e.verified;
            WithheldInCountries = e.IsDefined("withheld_in_countries") ? e.withheld_in_countries : null;
            WithheldScope = e.IsDefined("withheld_scope") ? e.withheld_scope : null;
        }
    }
    
    public class RelationShip : CoreBase
    {
        public Friendship Target{ get; set; }

        public Friendship Source{ get; set; }
  
        public RelationShip(Tokens tokens) : base(tokens) { }
        
        internal override void ConvertBase(dynamic e)
        {
            Target = CoreBase.Convert<Friendship>(this.Tokens, e.target);
            Source = CoreBase.Convert<Friendship>(this.Tokens, e.source);
        }
    }
    
    public class Friendship : CoreBase
    {
        public long Id{ get; set; }

        public string ScreenName{ get; set; }

        public bool? Following{ get; set; }

        public bool? FollowedBy{ get; set; }

        public bool? CanDm { get; set; }

        public bool? AllReplies{ get; set; }

        public bool? WantRetweets{ get; set; }

        public bool? Blocking{ get; set; }

        public bool? MarkedSpam{ get; set; }

        public bool? NotificationsEnabled{ get; set; }
        
        public string[] Connections{ get; set; }
        
        public Friendship(Tokens tokens) : base(tokens) { }
        
        internal override void ConvertBase(dynamic e)
        {
            Id = (long)e.id;
            ScreenName = e.screen_name;
            Following = (bool?)e.following;
            FollowedBy = (bool?)e.followed_by;
            CanDm = e.IsDefined("can_dm") ? (bool?)e.can_dm : null;
            AllReplies = e.IsDefined("all_replies") ? (bool?)e.all_replies : null;
            WantRetweets = e.IsDefined("want_retweets") ? (bool?)e.want_retweets : null;
            Blocking = e.IsDefined("blocking") ? (bool?)e.blocking : null;
            MarkedSpam = e.IsDefined("marked_spam") ? (bool?)e.marked_spam : null;
            NotificationsEnabled = e.IsDefined("notifications_enabled") ? (bool?)e.notifications_enabled : null;
            Connections = e.IsDefined("connections") ? (string[])e.connections : null;
        }
        
    }
    
    public class Category : CoreBase
    {
        public string Name { get; set; }

        public string Slug{ get; set; }

        public int Size{ get; set; }
        
        public Category(Tokens tokens) : base(tokens) { }
        
        internal override void ConvertBase(dynamic e)
        {
            Name = e.name;
            Slug = e.slug;
            Size = e.size;
        }
    }
}