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
using System.Linq;
using System.Linq.Expressions;
using CoreTweet.Core;

namespace CoreTweet.Rest
{       
    /// <summary>GET/POST account</summary>
    public class Account : TokenIncluded
    {
        internal Account(Tokens e) : base(e) { }
            
       
        //GET Methods
        
        /// <summary>
        ///     <para>
        ///     Returns an HTTP 200 OK response code and a representation of the requesting user if authentication was successful; returns a 401 status code and an error message if not. Use this method to test if supplied user credentials are valid.
        ///     </para>           
        ///     <para>
        ///     Available parameters:
        ///     </para><para> </para>
        ///     <para>
        ///     <paramref name="bool include_entities (optional)"/> : The entities node will not be included when set to false.
        ///     </para>
        ///     <para>
        ///     <paramref name="bool skip_status (optional)"/> : When set to true, statuses will not be included in the returned user objects.
        ///     </para></summary>
        /// <returns>
        /// The user data of you.
        /// </returns>
        /// <param name='parameters'>
        /// Parameters.
        /// </param>
        public User VerifyCredentials(params Expression<Func<string,object>>[] parameters)
        {
            return this.Tokens.AccessApi<User>(MethodType.Get, "account/verify_credentials", parameters);
        }
        

        //UNDONE: Implement Account.RateLimitStatus
        public IDictionary<string,IDictionary<string,RateLimit>> RateLimitStatus(params Expression<Func<string,object>>[] parameters)
        {
            throw new NotImplementedException ("Sorry, this API must be implemented before stable release...");
        }
        
        //GET & POST Methods
        
        /// <summary>
        /// <para>Returns settings (including current trend, geo and sleep time information) for the authenticating user or updates the authenticating user's settings.</para>
        /// <para>Avaliable parameters: </para>
        /// <para><paramref name="int trend_location_woeid (optional)"/> : The Yahoo! Where On Earth ID to use as the user's default trend location. Global information is available by using 1 as the WOEID. The woeid must be one of the locations returned by GET trends/available.</para>
        /// <para><paramref name="bool sleep_time_enabled (optional)"/> : When set to true, will enable sleep time for the user. Sleep time is the time when push or SMS notifications should not be sent to the user.</para>
        /// <para><paramref name="int start_sleep_time (optional)"/> : The hour that sleep time should begin if it is enabled. The value for this parameter should be provided in ISO8601 format (i.e. 00-23). The time is considered to be in the same timezone as the user's time_zone setting.</para>
        /// <para><paramref name="int end_sleep_time (optional)"/> : The hour that sleep time should end if it is enabled. The value for this parameter should be provided in ISO8601 format (i.e. 00-23). The time is considered to be in the same timezone as the user's time_zone setting.</para>
        /// <para><paramref name="string time_zone (optional)"/> : The timezone dates and times should be displayed in for the user. The timezone must be one of the Rails TimeZone names.</para>
        /// <para><paramref name="string lang (optional)"/> : The language which Twitter should render in for this user. The language must be specified by the appropriate two letter ISO 639-1 representation. Currently supported languages are provided by GET help/languages.</para>
        /// </summary>
        /// <returns>
        /// Settings.
        /// </returns>
        /// <param name='parameters'>
        /// Parameters.
        /// </param>
        public Setting Settings(params Expression<Func<string,object>>[] parameters)
        {
            return this.Tokens.AccessApi<Setting>(parameters.Length == 0 ? MethodType.Get : MethodType.Post, "account/settings", parameters);
        }
        
        //POST Methods
        
        /// <summary>
        /// <para>Sets which device Twitter delivers updates to for the authenticating user. Sending none as the device parameter will disable SMS updates.</para>
        /// <para>Avaliable parameters: </para>
        /// <para><paramref name="string device (required)"/> : Must be one of: sms, none.</para>
        /// <para><paramref name="bool include_entities (optional)"/> : When set to true, each tweet will include a node called "entities,". This node offers a variety of metadata about the tweet in a discreet structure, including: user_mentions, urls, and hashtags. While entities are opt-in on timelines at present, they will be made a default component of output in the future. See Tweet Entities for more detail on entities.</para>
        /// </summary>
        /// <param name='parameters'>
        /// Parameters.
        /// </param>
        public void UpdateDeliveryService(params Expression<Func<string,object>>[] parameters)
        {
            this.Tokens.SendRequest(MethodType.PostNoResponse, "account/update_delivery_service", parameters).Dispose();
        }
        
        /// <summary>
        /// <para>Sets values that users are able to set under the "Account" tab of their settings page. Only the parameters specified will be updated.</para>
        /// <para>Avaliable parameters: </para>
        /// <para><paramref name="string name (optional)"/> : Full name associated with the profile. Maximum of 20 characters.</para>
        /// <para><paramref name="string url (optional)"/> : URL associated with the profile. Will be prepended with "http://" if not present. Maximum of 100 characters.</para>
        /// <para><paramref name="string location (optional)"/> : The city or country describing where the user of the account is located. The contents are not normalized or geocoded in any way. Maximum of 30 characters.</para>
        /// <para><paramref name="string description (optional)"/> : A description of the user owning the account. Maximum of 160 characters.</para>
        /// <para><paramref name="bool include_entities (optional)"/> : The entities node will not be included when set to false.</para>
        /// <para><paramref name="bool skip_status (optional) "/>: When set to true, statuses will not be included in the returned user objects.</para>
        /// </summary>
        /// <returns>
        /// The profile.
        /// </returns>
        /// <param name='parameters'>
        /// Parameters.
        /// </param>
        public User UpdateProfile(params Expression<Func<string,object>>[] parameters)
        {
            return this.Tokens.AccessApi<User>(MethodType.Post, "account/update_profile", parameters);
        }
        
        /// <summary>
        /// <para>Updates the authenticating user's profile background image. This method can also be used to enable or disable the profile background image.</para>
        /// <para>Although each parameter is marked as optional, at least one of image, tile or use must be provided when making this request.</para>
        /// <para>Avaliable parameters: </para>
        /// <para><paramref name="string image (optional)"/> : The background image for the profile, base64-encoded. Must be a valid GIF, JPG, or PNG image of less than 800 kilobytes in size. Images with width larger than 2048 pixels will be forcibly scaled down. The image must be provided as raw multipart data, not a URL.</para>
        /// <para><paramref name="bool tile (optional)"/> : Whether or not to tile the background image. If set to true, t or 1 the background image will be displayed tiled. The image will not be tiled otherwise.</para>
        /// <para><paramref name="bool include_entities (optional)"/> : The entities node will not be included when set to false.</para>
        /// <para><paramref name="bool skip_status (optional)"/> : When set to true, statuses will not be included in the returned user objects.</para>
        /// <para><paramref name="bool use (optional)"/> : Determines whether to display the profile background image or not. When set to true, the background image will be displayed if an image is being uploaded with the request, or has been uploaded previously. An error will be returned if you try to use a background image when one is not being uploaded or does not exist. If this parameter is defined but set to anything other than true, the background image will stop being used.</para>
        /// </summary>
        /// <returns>The profile.</returns>
        /// <param name='parameters'>
        /// Parameters.
        /// </param>
        public User UpdateProfileBackgroundImage(params Expression<Func<string,object>>[] parameters)
        {
            return this.Tokens.AccessApi<User>(MethodType.Post, "account/update_profile_background_image", parameters);
        }
        
        /// <summary>
        /// <para>Uploads a profile banner on behalf of the authenticating user. For best results, upload an image that is exactly 1252px by 626px and smaller than 5MB. Images will be resized for a number of display options. Users with an uploaded profile banner will have a profile_banner_url node in their Users objects. More information about sizing variations can be found in User Profile Images and Banners and GET users/profile_banner.</para>
        /// <para>Profile banner images are processed asynchronously. The profile_banner_url and its variant sizes will not necessary be available directly after upload.</para>
        /// <para>Avaliable parameters: </para>
        /// <para><paramref name="string banner (required)"/> : The Base64-encoded or raw image data being uploaded as the user's new profile banner.</para>
        /// <para><paramref name="string width (optional)"/> : The width of the preferred section of the image being uploaded in pixels. Use with height, offset_left, and offset_top to select the desired region of the image to use.</para>
        /// <para><paramref name="string height (optional)"/> : The height of the preferred section of the image being uploaded in pixels. Use with width, offset_left, and offset_top to select the desired region of the image to use.</para>
        /// <para><paramref name="string offset_left (optional)"/> : The number of pixels by which to offset the uploaded image from the left. Use with height, width, and offset_top to select the desired region of the image to use.</para>
        /// <para><paramref name="string offset_top (optional)"/> : The number of pixels by which to offset the uploaded image from the top. Use with height, width, and offset_left to select the desired region of the image to use.</para>
        /// </summary>
        /// <param name='parameters'>
        /// Parameters.
        /// </param>
        public void UpdateProfileBanner(params Expression<Func<string,object>>[] parameters)
        {
            this.Tokens.SendRequest(MethodType.PostNoResponse, "account/update_profile_banner", parameters).Dispose();
        }
        
        /// <summary>
        /// <para>Sets one or more hex values that control the color scheme of the authenticating user's profile page on twitter.com. Each parameter's value must be a valid hexidecimal value, and may be either three or six characters (ex: #fff or #ffffff).</para>
        /// <para>Avaliable parameters: </para>
        /// <para><paramref name="string profile_background_color (optional)"/> : Profile background color.</para>
        /// <para><paramref name="string profile_link_color (optional)"/> : Profile link color.</para>
        /// <para><paramref name="string profile_sidebar_border_color (optional)"/> : Profile sidebar's border color.</para>
        /// <para><paramref name="string profile_sidebar_fill_color (optional)"/> : Profile sidebar's background color.</para>
        /// <para><paramref name="string profile_text_color (optional)"/> : Profile text color.</para>
        /// <para><paramref name="bool include_entities (optional)"/> : The entities node will not be included when set to false.</para>
        /// <para><paramref name="bool skip_status (optional)"/> : When set to true, statuses will not be included in the returned user objects.</para>
        /// </summary>
        /// <returns>The profile.</returns>
        /// <param name='parameters'>
        /// Parameters.
        /// </param>
        public User UpdateProfileColors(params Expression<Func<string,object>>[] parameters)
        {
            return this.Tokens.AccessApi<User>(MethodType.Post, "account/update_profile_colors", parameters);
        }
        
        /// <summary>
        /// <para>Updates the authenticating user's profile image. Note that this method expects raw multipart data, not a URL to an image.</para>
        /// <para>This method asynchronously processes the uploaded file before updating the user's profile image URL. You can either update your local cache the next time you request the user's information, or, at least 5 seconds after uploading the image, ask for the updated URL using GET users/show.</para>
        /// <para>Avaliable parameters: </para> 
        /// <para><paramref name="string image (required)"/> : The avatar image for the profile, base64-encoded. Must be a valid GIF, JPG, or PNG image of less than 700 kilobytes in size. Images with width larger than 500 pixels will be scaled down. Animated GIFs will be converted to a static GIF of the first frame, removing the animation.</para>
        /// <para><paramref name="bool include_entities (optional)"/> : The entities node will not be included when set to false.</para>
        /// <para><paramref name="bool skip_status (optional)"/> : When set to true, statuses will not be included in the returned user objects.</para>
        /// </summary>
        /// <returns>The profile.</returns>
        /// <param name='parameters'>
        /// Parameters.
        /// </param>
        public User UpdateProfileImage(params Expression<Func<string,object>>[] parameters)
        {
            return this.Tokens.AccessApi<User>(MethodType.Post, "account/update_profile_image", parameters);
        }
    }
        
}