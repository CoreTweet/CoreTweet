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
using System.Linq;
using System.Linq.Expressions;
using CoreTweet.Core;

namespace CoreTweet.Rest
{
    /// <summary>
    /// Provides a set of methods for the wrapper of GET/POST account.
    /// </summary>
    public partial class Account : ApiProviderBase
    {
        internal Account(TokensBase e) : base(e) { }

#if !(PCL || WIN_RT || WP)
        //GET Methods

        /// <summary>
        /// <para>Returns a representation of the requesting user if authentication was successful.</para>
        /// <para>Use this method to test if supplied user credentials are valid.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// <para>- <c>bool</c> skip_status (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The user object.</returns>
        public UserResponse VerifyCredentials(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApi<UserResponse>(MethodType.Get, "account/verify_credentials", parameters);
        }

        /// <summary>
        /// <para>Returns a representation of the requesting user if authentication was successful.</para>
        /// <para>Use this method to test if supplied user credentials are valid.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// <para>- <c>bool</c> skip_status (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The user object.</returns>
        public UserResponse VerifyCredentials(IDictionary<string, object> parameters)
        {
            return this.Tokens.AccessApi<UserResponse>(MethodType.Get, "account/verify_credentials", parameters);
        }

        /// <summary>
        /// <para>Returns a representation of the requesting user if authentication was successful.</para>
        /// <para>Use this method to test if supplied user credentials are valid.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// <para>- <c>bool</c> skip_status (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The user object.</returns>
        public UserResponse VerifyCredentials<T>(T parameters)
        {
            return this.Tokens.AccessApi<UserResponse, T>(MethodType.Get, "account/verify_credentials", parameters);
        }

        //GET & POST Methods

        /// <summary>
        /// <para>Returns settings (including current trend, geo and sleep time information) for the authenticating user or updates the authenticating user's settings.</para>
        /// <para>Available parameters: </para>
        /// <para>- <c>int</c> trend_location_woeid (optional)</para>
        /// <para>- <c>bool</c> sleep_time_enabled (optional)</para>
        /// <para>- <c>int</c> start_sleep_time (optional)</para>
        /// <para>- <c>int</c> end_sleep_time (optional)</para>
        /// <para>- <c>string</c> time_zone (optional)</para>
        /// <para>- <c>string</c> lang (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The settings of the user.</returns>
        public Setting Settings(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApi<Setting>(parameters.Length == 0 ? MethodType.Get : MethodType.Post, "account/settings", parameters);
        }

        /// <summary>
        /// <para>Returns settings (including current trend, geo and sleep time information) for the authenticating user or updates the authenticating user's settings.</para>
        /// <para>Available parameters: </para>
        /// <para>- <c>int</c> trend_location_woeid (optional)</para>
        /// <para>- <c>bool</c> sleep_time_enabled (optional)</para>
        /// <para>- <c>int</c> start_sleep_time (optional)</para>
        /// <para>- <c>int</c> end_sleep_time (optional)</para>
        /// <para>- <c>string</c> time_zone (optional)</para>
        /// <para>- <c>string</c> lang (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The settings of the user.</returns>
        public Setting Settings(IDictionary<string, object> parameters)
        {
            return this.Tokens.AccessApi<Setting>(parameters.Count == 0 ? MethodType.Get : MethodType.Post, "account/settings", parameters);
        }

        /// <summary>
        /// <para>Returns settings (including current trend, geo and sleep time information) for the authenticating user or updates the authenticating user's settings.</para>
        /// <para>Available parameters: </para>
        /// <para>- <c>int</c> trend_location_woeid (optional)</para>
        /// <para>- <c>bool</c> sleep_time_enabled (optional)</para>
        /// <para>- <c>int</c> start_sleep_time (optional)</para>
        /// <para>- <c>int</c> end_sleep_time (optional)</para>
        /// <para>- <c>string</c> time_zone (optional)</para>
        /// <para>- <c>string</c> lang (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The settings of the user.</returns>
        public Setting Settings<T>(T parameters)
        {
            var param = InternalUtils.ResolveObject(parameters).ToArray();
            return this.Tokens.AccessApiImpl<Setting>(param.Any() ? MethodType.Post : MethodType.Get, "account/settings", param, "");
        }


        //POST Methods

        /// <summary>
        /// <para>Sets which device Twitter delivers updates to for the authenticating user.</para>
        /// <para>Sending none as the device parameter will disable SMS updates.</para>
        /// <para>Available parameters: </para>
        /// <para>- <c>string</c> device (required)</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        public void UpdateDeliveryService(params Expression<Func<string, object>>[] parameters)
        {
            this.Tokens.AccessApiNoResponse("account/update_delivery_service", parameters);
        }

        /// <summary>
        /// <para>Sets which device Twitter delivers updates to for the authenticating user.</para>
        /// <para>Sending none as the device parameter will disable SMS updates.</para>
        /// <para>Available parameters: </para>
        /// <para>- <c>string</c> device (required)</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        public void UpdateDeliveryService(IDictionary<string, object> parameters)
        {
            this.Tokens.AccessApiNoResponse("account/update_delivery_service", parameters);
        }

        /// <summary>
        /// <para>Sets which device Twitter delivers updates to for the authenticating user.</para>
        /// <para>Sending none as the device parameter will disable SMS updates.</para>
        /// <para>Available parameters: </para>
        /// <para>- <c>string</c> device (required)</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        public void UpdateDeliveryService<T>(T parameters)
        {
            this.Tokens.AccessApiNoResponse("account/update_delivery_service", parameters);
        }

        /// <summary>
        /// <para>Sets values that users are able to set under the "Account" tab of their settings page.</para>
        /// <para>Only the parameters specified will be updated.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> name (optional)</para>
        /// <para>- <c>string</c> url (optional)</para>
        /// <para>- <c>string</c> location (optional)</para>
        /// <para>- <c>string</c> description (optional)</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// <para>- <c>bool</c> skip_status (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The user object.</returns>
        public UserResponse UpdateProfile(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApi<UserResponse>(MethodType.Post, "account/update_profile", parameters);
        }

        /// <summary>
        /// <para>Sets values that users are able to set under the "Account" tab of their settings page.</para>
        /// <para>Only the parameters specified will be updated.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> name (optional)</para>
        /// <para>- <c>string</c> url (optional)</para>
        /// <para>- <c>string</c> location (optional)</para>
        /// <para>- <c>string</c> description (optional)</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// <para>- <c>bool</c> skip_status (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The user object.</returns>
        public UserResponse UpdateProfile(IDictionary<string, object> parameters)
        {
            return this.Tokens.AccessApi<UserResponse>(MethodType.Post, "account/update_profile", parameters);
        }

        /// <summary>
        /// <para>Sets values that users are able to set under the "Account" tab of their settings page.</para>
        /// <para>Only the parameters specified will be updated.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> name (optional)</para>
        /// <para>- <c>string</c> url (optional)</para>
        /// <para>- <c>string</c> location (optional)</para>
        /// <para>- <c>string</c> description (optional)</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// <para>- <c>bool</c> skip_status (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The user object.</returns>
        public UserResponse UpdateProfile<T>(T parameters)
        {
            return this.Tokens.AccessApi<UserResponse, T>(MethodType.Post, "account/update_profile", parameters);
        }

        /// <summary>
        /// <para>Updates the authenticating user's profile background image.</para>
        /// <para>This method can also be used to enable or disable the profile background image.</para>
        /// <para>Although each parameter is marked as optional, at least one of image, tile or use must be provided when making this request.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>Stream</c> / <c>IEnumerable&lt;byte&gt;</c> / <c>FileInfo</c> image (optional)</para>
        /// <para>- <c>bool</c> tile (optional)</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// <para>- <c>bool</c> skip_status (optional)</para>
        /// <para>- <c>bool</c> use (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The user object.</returns>
        public UserResponse UpdateProfileBackgroundImage(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApi<UserResponse>(MethodType.Post, "account/update_profile_background_image", parameters);
        }

        /// <summary>
        /// <para>Updates the authenticating user's profile background image.</para>
        /// <para>This method can also be used to enable or disable the profile background image.</para>
        /// <para>Although each parameter is marked as optional, at least one of image, tile or use must be provided when making this request.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>Stream</c> / <c>IEnumerable&lt;byte&gt;</c> / <c>FileInfo</c> image (optional)</para>
        /// <para>- <c>bool</c> tile (optional)</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// <para>- <c>bool</c> skip_status (optional)</para>
        /// <para>- <c>bool</c> use (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The user object.</returns>
        public UserResponse UpdateProfileBackgroundImage(IDictionary<string, object> parameters)
        {
            return this.Tokens.AccessApi<UserResponse>(MethodType.Post, "account/update_profile_background_image", parameters);
        }

        /// <summary>
        /// <para>Updates the authenticating user's profile background image.</para>
        /// <para>This method can also be used to enable or disable the profile background image.</para>
        /// <para>Although each parameter is marked as optional, at least one of image, tile or use must be provided when making this request.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>Stream</c> / <c>IEnumerable&lt;byte&gt;</c> / <c>FileInfo</c> image (optional)</para>
        /// <para>- <c>bool</c> tile (optional)</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// <para>- <c>bool</c> skip_status (optional)</para>
        /// <para>- <c>bool</c> use (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The user object.</returns>
        public UserResponse UpdateProfileBackgroundImage<T>(T parameters)
        {
            return this.Tokens.AccessApi<UserResponse, T>(MethodType.Post, "account/update_profile_background_image", parameters);
        }

        /// <summary>
        /// <para>Uploads a profile banner on behalf of the authenticating user.</para>
        /// <para>For best results, upload an &lt;3MB image that is exactly 1500px by 500px.</para>
        /// <para>Images will be resized for a number of display options. Users with an uploaded profile banner will have a profile_banner_url node in their Users objects.</para>
        /// <para>More information about sizing variations can be found in https://dev.twitter.com/docs/user-profile-images-and-banners.</para>
        /// <para>Profile banner images are processed asynchronously.</para>
        /// <para>The profile_banner_url and its variant sizes will not necessary be available directly after upload.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>Stream</c> / <c>IEnumerable&lt;byte&gt;</c> / <c>FileInfo</c> banner (required)</para>
        /// <para>- <c>string</c> width (optional)</para>
        /// <para>- <c>string</c> height (optional)</para>
        /// <para>- <c>string</c> offset_left (optional)</para>
        /// <para>- <c>string</c> offset_top (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        public void UpdateProfileBanner(params Expression<Func<string, object>>[] parameters)
        {
            this.Tokens.AccessApiNoResponse("account/update_profile_banner", parameters);
        }

        /// <summary>
        /// <para>Uploads a profile banner on behalf of the authenticating user.</para>
        /// <para>For best results, upload an &lt;3MB image that is exactly 1500px by 500px.</para>
        /// <para>Images will be resized for a number of display options. Users with an uploaded profile banner will have a profile_banner_url node in their Users objects.</para>
        /// <para>More information about sizing variations can be found in https://dev.twitter.com/docs/user-profile-images-and-banners.</para>
        /// <para>Profile banner images are processed asynchronously.</para>
        /// <para>The profile_banner_url and its variant sizes will not necessary be available directly after upload.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>Stream</c> / <c>IEnumerable&lt;byte&gt;</c> / <c>FileInfo</c> banner (required)</para>
        /// <para>- <c>string</c> width (optional)</para>
        /// <para>- <c>string</c> height (optional)</para>
        /// <para>- <c>string</c> offset_left (optional)</para>
        /// <para>- <c>string</c> offset_top (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        public void UpdateProfileBanner(IDictionary<string, object> parameters)
        {
            this.Tokens.AccessApiNoResponse("account/update_profile_banner", parameters);
        }

        /// <summary>
        /// <para>Uploads a profile banner on behalf of the authenticating user.</para>
        /// <para>For best results, upload an &lt;3MB image that is exactly 1500px by 500px.</para>
        /// <para>Images will be resized for a number of display options. Users with an uploaded profile banner will have a profile_banner_url node in their Users objects.</para>
        /// <para>More information about sizing variations can be found in https://dev.twitter.com/docs/user-profile-images-and-banners.</para>
        /// <para>Profile banner images are processed asynchronously.</para>
        /// <para>The profile_banner_url and its variant sizes will not necessary be available directly after upload.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>Stream</c> / <c>IEnumerable&lt;byte&gt;</c> / <c>FileInfo</c> banner (required)</para>
        /// <para>- <c>string</c> width (optional)</para>
        /// <para>- <c>string</c> height (optional)</para>
        /// <para>- <c>string</c> offset_left (optional)</para>
        /// <para>- <c>string</c> offset_top (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        public void UpdateProfileBanner<T>(T parameters)
        {
            this.Tokens.AccessApiNoResponse("account/update_profile_banner", parameters);
        }

        /// <summary>
        /// <para>Removes the uploaded profile banner for the authenticating user.</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        public void RemoveProfileBanner(params Expression<Func<string, object>>[] parameters)
        {
            this.Tokens.AccessApiNoResponse("account/remove_profile_banner", parameters);
        }

        /// <summary>
        /// <para>Removes the uploaded profile banner for the authenticating user.</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        public void RemoveProfileBanner(IDictionary<string, object> parameters)
        {
            this.Tokens.AccessApiNoResponse("account/remove_profile_banner", parameters);
        }

        /// <summary>
        /// <para>Removes the uploaded profile banner for the authenticating user.</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        public void RemoveProfileBanner<T>(T parameters)
        {
            this.Tokens.AccessApiNoResponse("account/remove_profile_banner", parameters);
        }

        /// <summary>
        /// <para>Sets one or more hex values that control the color scheme of the authenticating user's profile page on twitter.com.</para>
        /// <para>Each parameter's value must be a valid hexidecimal value, and may be either three or six characters (ex: &#35;fff or &#35;ffffff).</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> profile_background_color (optional)</para>
        /// <para>- <c>string</c> profile_link_color (optional)</para>
        /// <para>- <c>string</c> profile_sidebar_border_color (optional)</para>
        /// <para>- <c>string</c> profile_sidebar_fill_color (optional)</para>
        /// <para>- <c>string</c> profile_text_color (optional)</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// <para>- <c>bool</c> skip_status (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The user object.</returns>
        public UserResponse UpdateProfileColors(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApi<UserResponse>(MethodType.Post, "account/update_profile_colors", parameters);
        }

        /// <summary>
        /// <para>Sets one or more hex values that control the color scheme of the authenticating user's profile page on twitter.com.</para>
        /// <para>Each parameter's value must be a valid hexidecimal value, and may be either three or six characters (ex: &#35;fff or &#35;ffffff).</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> profile_background_color (optional)</para>
        /// <para>- <c>string</c> profile_link_color (optional)</para>
        /// <para>- <c>string</c> profile_sidebar_border_color (optional)</para>
        /// <para>- <c>string</c> profile_sidebar_fill_color (optional)</para>
        /// <para>- <c>string</c> profile_text_color (optional)</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// <para>- <c>bool</c> skip_status (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The user object.</returns>
        public UserResponse UpdateProfileColors(IDictionary<string, object> parameters)
        {
            return this.Tokens.AccessApi<UserResponse>(MethodType.Post, "account/update_profile_colors", parameters);
        }

        /// <summary>
        /// <para>Sets one or more hex values that control the color scheme of the authenticating user's profile page on twitter.com.</para>
        /// <para>Each parameter's value must be a valid hexidecimal value, and may be either three or six characters (ex: &#35;fff or &#35;ffffff).</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> profile_background_color (optional)</para>
        /// <para>- <c>string</c> profile_link_color (optional)</para>
        /// <para>- <c>string</c> profile_sidebar_border_color (optional)</para>
        /// <para>- <c>string</c> profile_sidebar_fill_color (optional)</para>
        /// <para>- <c>string</c> profile_text_color (optional)</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// <para>- <c>bool</c> skip_status (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The user object.</returns>
        public UserResponse UpdateProfileColors<T>(T parameters)
        {
            return this.Tokens.AccessApi<UserResponse, T>(MethodType.Post, "account/update_profile_colors", parameters);
        }

        /// <summary>
        /// <para>Updates the authenticating user's profile image.</para>
        /// <para>Note that this method expects raw multipart data, not a URL to an image.</para>
        /// <para>This method asynchronously processes the uploaded file before updating the user's profile image URL.</para>
        /// <para>You can either update your local cache the next time you request the user's information, or, at least 5 seconds after uploading the image, ask for the updated URL using GET users/show.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>Stream</c> / <c>IEnumerable&lt;byte&gt;</c> / <c>FileInfo</c> image (required)</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// <para>- <c>bool</c> skip_status (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The user object.</returns>
        public UserResponse UpdateProfileImage(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApi<UserResponse>(MethodType.Post, "account/update_profile_image", parameters);
        }

        /// <summary>
        /// <para>Updates the authenticating user's profile image.</para>
        /// <para>Note that this method expects raw multipart data, not a URL to an image.</para>
        /// <para>This method asynchronously processes the uploaded file before updating the user's profile image URL.</para>
        /// <para>You can either update your local cache the next time you request the user's information, or, at least 5 seconds after uploading the image, ask for the updated URL using GET users/show.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>Stream</c> / <c>IEnumerable&lt;byte&gt;</c> / <c>FileInfo</c> image (required)</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// <para>- <c>bool</c> skip_status (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The user object.</returns>
        public UserResponse UpdateProfileImage(IDictionary<string, object> parameters)
        {
            return this.Tokens.AccessApi<UserResponse>(MethodType.Post, "account/update_profile_image", parameters);
        }

        /// <summary>
        /// <para>Updates the authenticating user's profile image.</para>
        /// <para>Note that this method expects raw multipart data, not a URL to an image.</para>
        /// <para>This method asynchronously processes the uploaded file before updating the user's profile image URL.</para>
        /// <para>You can either update your local cache the next time you request the user's information, or, at least 5 seconds after uploading the image, ask for the updated URL using GET users/show.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>Stream</c> / <c>IEnumerable&lt;byte&gt;</c> / <c>FileInfo</c> image (required)</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// <para>- <c>bool</c> skip_status (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The user object.</returns>
        public UserResponse UpdateProfileImage<T>(T parameters)
        {
            return this.Tokens.AccessApi<UserResponse, T>(MethodType.Post, "account/update_profile_image", parameters);
        }
#endif
    }
}
