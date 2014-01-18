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

    /// <summary>GET/POST statuses</summary>
    public class Statuses : TokenIncluded
    {
        internal Statuses(Tokens e) : base(e) { }
        //UNDONE: Implement update_with_media
                
        //GET Methods

        /// <summary>
        /// <para>Returns the 20 most recent mentions (tweets containing a users's @screen_name) for the authenticating user. The timeline returned is the equivalent of the one seen when you view your mentions on twitter.com. This method can only return up to 800 tweets. See Working with Timelines for instructions on traversing timelines.</para>
        /// <seealso cref="https://dev.twitter.com/docs/working-with-timelines"/>
        /// <para>Avaliable parameters: </para>
        /// <para><paramref name="int count (optional)"/> : Specifies the number of tweets to try and retrieve, up to a maximum of 200. The value of count is best thought of as a limit to the number of tweets to return because suspended or deleted content is removed after the count has been applied. We include retweets in the count, even if include_rts is not supplied. It is recommended you always send include_rts=1 when using this API method.</para>
        /// <para><paramref name="int since_id(optional)"/> : Returns results with an ID greater than (that is, more recent than) the specified ID. There are limits to the number of Tweets which can be accessed through the API. If the limit of Tweets has occured since the since_id, the since_id will be forced to the oldest ID available.</para>
        /// <para><paramref name="int max_id (optional)"/> : Returns results with an ID less than (that is, older than) or equal to the specified ID.</para>
        /// <para><paramref name="bool trim_user (optional)"/> : When set to true, each tweet returned in a timeline will include a user object including only the status authors numerical ID. Omit this parameter to receive the complete user object.</para>
        /// <para><paramref name="bool contributor_details (optional)"/> : This parameter enhances the contributors element of the status response to include the screen_name of the contributor. By default only the user_id of the contributor is included.</para>
        /// <para><paramref name="bool include_entities (optional)"/> : The entities node will be disincluded when set to false.</para>
        /// </summary>
        /// <param name='tokens'>OAuth Tokens.</param>
        /// <param name='parameters'>Parameters.</param>
        /// <returns>The statuses.</returns>
        public IEnumerable<Status> MentionsTimeline(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiArray<Status>(MethodType.Get, "statuses/mentions_timeline", parameters);
        }

        /// <summary>
        /// <para>Returns a collection of the most recent Tweets posted by the user indicated by the screen_name or user_id parameters. User timelines belonging to protected users may only be requested when the authenticated user either "owns" the timeline or is an approved follower of the owner. The timeline returned is the equivalent of the one seen when you view a user's profile on twitter.com. This method can only return up to 3,200 of a user's most recent Tweets. Native retweets of other statuses by the user is included in this total, regardless of whether include_rts is set to false when requesting this resource.</para>
        /// <para>Avaliable parameters: </para>
        /// <para><paramref name="int user_id (optional)"/> : The ID of the user for whom to return results for.</para>
        /// <para><paramref name="string screen_name (optional)"/> : The screen name of the user for whom to return results for.</para>
        /// <para><paramref name="int count (optional)"/> : Specifies the number of tweets to try and retrieve, up to a maximum of 200. The value of count is best thought of as a limit to the number of tweets to return because suspended or deleted content is removed after the count has been applied. We include retweets in the count, even if include_rts is not supplied. It is recommended you always send include_rts=1 when using this API method.</para>
        /// <para><paramref name="int since_id(optional)"/> : Returns results with an ID greater than (that is, more recent than) the specified ID. There are limits to the number of Tweets which can be accessed through the API. If the limit of Tweets has occured since the since_id, the since_id will be forced to the oldest ID available.</para>
        /// <para><paramref name="int max_id (optional)"/> : Returns results with an ID less than (that is, older than) or equal to the specified ID.</para>
        /// <para><paramref name="bool trim_user (optional)"/> : When set to true, t or 1, each tweet returned in a timeline will include a user object including only the status authors numerical ID. Omit this parameter to receive the complete user object.</para>
        /// <para><paramref name="bool contributor_details (optional)"/> : This parameter enhances the contributors element of the status response to include the screen_name of the contributor. By default only the user_id of the contributor is included.</para>
        /// <para><paramref name="bool include_rts (optional)"/> : When set to false, the timeline will strip any native retweets (though they will still count toward both the maximal length of the timeline and the slice selected by the count parameter). Note: If you're using the trim_user parameter in conjunction with include_rts, the retweets will still contain a full user object.</para>
        /// <para><paramref name="bool exclude_replies (optional)"/> : This parameter will prevent replies from appearing in the returned timeline. Using exclude_replies with the count parameter will mean you will receive up-to count tweets — this is because the count parameter retrieves that many tweets before filtering out retweets and replies. This parameter is only supported for JSON and XML responses.</para>
        /// </summary>
        /// <param name='tokens'>OAuth Tokens.</param>
        /// <param name='parameters'>Parameters.</param>
        /// <returns>The statuses.</returns>
        public IEnumerable<Status> UserTimeline(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiArray<Status>(MethodType.Get, "statuses/user_timeline", parameters);
        }

        /// <summary>
        /// <para>Returns a collection of the most recent Tweets and retweets posted by the authenticating user and the users they follow. The home timeline is central to how most users interact with the Twitter service. Up to 800 Tweets are obtainable on the home timeline. It is more volatile for users that follow many users or follow users who tweet frequently.</para>
        /// <para>Avaliable parameters: </para>
        /// <para><paramref name="int count (optional)"/> : Specifies the number of tweets to try and retrieve, up to a maximum of 200. The value of count is best thought of as a limit to the number of tweets to return because suspended or deleted content is removed after the count has been applied. We include retweets in the count, even if include_rts is not supplied. It is recommended you always send include_rts=1 when using this API method.</para>
        /// <para><paramref name="int since_id(optional)"/> : Returns results with an ID greater than (that is, more recent than) the specified ID. There are limits to the number of Tweets which can be accessed through the API. If the limit of Tweets has occured since the since_id, the since_id will be forced to the oldest ID available.</para>
        /// <para><paramref name="int max_id (optional)"/> : Returns results with an ID less than (that is, older than) or equal to the specified ID.</para>
        /// <para><paramref name="bool trim_user (optional)"/> : When set to true, t or 1, each tweet returned in a timeline will include a user object including only the status authors numerical ID. Omit this parameter to receive the complete user object.</para>
        /// <para><paramref name="bool contributor_details (optional)"/> : This parameter enhances the contributors element of the status response to include the screen_name of the contributor. By default only the user_id of the contributor is included.</para>
        /// <para><paramref name="bool include_entities (optional)"/> : The entities node will be disincluded when set to false.</para>
        /// <para><paramref name="bool exclude_replies (optional)"/> : This parameter will prevent replies from appearing in the returned timeline. Using exclude_replies with the count parameter will mean you will receive up-to count tweets — this is because the count parameter retrieves that many tweets before filtering out retweets and replies. This parameter is only supported for JSON and XML responses.</para>
        /// </summary>
        /// <param name='tokens'>OAuth Tokens.</param>
        /// <param name='parameters'>Parameters.</param>
        /// <returns>The statuses.</returns>
        public IEnumerable<Status> HomeTimeline(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiArray<Status>(MethodType.Get, "statuses/home_timeline", parameters);
        }

        /// <summary>
        /// <para>Returns the most recent tweets authored by the authenticating user that have recently been retweeted by others. This timeline is a subset of the user's GET statuses/user_timeline.</para>
        /// <seealso cref="https://dev.twitter.com/docs/working-with-timelines"/>
        /// <para>Avaliable parameters: </para>
        /// <para><paramref name="int count (optional)"/> : Specifies the number of tweets to try and retrieve, up to a maximum of 200. The value of count is best thought of as a limit to the number of tweets to return because suspended or deleted content is removed after the count has been applied. We include retweets in the count, even if include_rts is not supplied. It is recommended you always send include_rts=1 when using this API method.</para>
        /// <para><paramref name="int since_id(optional)"/> : Returns results with an ID greater than (that is, more recent than) the specified ID. There are limits to the number of Tweets which can be accessed through the API. If the limit of Tweets has occured since the since_id, the since_id will be forced to the oldest ID available.</para>
        /// <para><paramref name="int max_id (optional)"/> : Returns results with an ID less than (that is, older than) or equal to the specified ID.</para>
        /// <para><paramref name="bool trim_user (optional)"/> : When set to true, each tweet returned in a timeline will include a user object including only the status authors numerical ID. Omit this parameter to receive the complete user object.</para>
        /// <para><paramref name="bool include_entities (optional)"/> : The tweet entities node will be disincluded when set to false.</para>
        /// <para><paramref name="bool include_user_entities (optional)"/> :The user entities node will be disincluded when set to false..</para>
        /// </summary>
        /// <param name='tokens'>OAuth Tokens.</param>
        /// <param name='parameters'>Parameters.</param>
        /// <returns>The statuses.</returns>
        public IEnumerable<Status> RetweetsOfMe(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiArray<Status>(MethodType.Get, "statuses/retweets_of_me", parameters);
        }
            
            
        /// <summary>
        /// <para>Returns information allowing the creation of an embedded representation of a Tweet on third party sites. See the oEmbed specification for information about the response format.</para>
        /// <para>While this endpoint allows a bit of customization for the final appearance of the embedded Tweet, be aware that the appearance of the rendered Tweet may change over time to be consistent with Twitter's Display Requirements. Do not rely on any class or id parameters to stay constant in the returned markup.</para>
        /// <para>Avaliable parameters: </para>
        /// <para><paramref name="long id (required)"/> : The Tweet/status ID to return embed code for.</para>
        /// <para><paramref name="int maxwidth (optional)"/> : The maximum width in pixels that the embed should be rendered at. This value is constrained to be between 250 and 550 pixels. Note that Twitter does not support the oEmbed maxheight parameter. Tweets are fundamentally text, and are therefore of unpredictable height that cannot be scaled like an image or video. Relatedly, the oEmbed response will not provide a value for height. Implementations that need consistent heights for Tweets should refer to the hide_thread and hide_media parameters below.</para>
        /// <para><paramref name="bool hide_media (optional)"/> : Specifies whether the embedded Tweet should automatically expand images which were uploaded via POST statuses/update_with_media. When set to either true, t or 1 images will not be expanded. Defaults to false.</para>
        /// <para><paramref name="bool hide_thread (optional)"/> : Specifies whether the embedded Tweet should automatically show the original message in the case that the embedded Tweet is a reply. When set to either true, t or 1 the original Tweet will not be shown. Defaults to false.</para>
        /// <para><paramref name="bool omit_script (optional)"/> : Specifies whether the embedded Tweet HTML should include a "script" element pointing to widgets.js. In cases where a page already includes widgets.js, setting this value to true will prevent a redundant script element from being included. When set to either true, t or 1 the "script" element will not be included in the embed HTML, meaning that pages must include a reference to widgets.js manually. Defaults to false.</para>
        /// <para><paramref name="string align (optional)"/> : Specifies whether the embedded Tweet should be left aligned, right aligned, or centered in the page. Valid values are left, right, center, and none. Defaults to none, meaning no alignment styles are specified for the Tweet.</para>
        /// <para><paramref name="string related (optional)"/> : A value for the TWT related parameter, as described in Web Intents. This value will be forwarded to all Web Intents calls.</para>
        /// <para><paramref name="string lang (optional)"/> : Language code for the rendered embed. This will affect the text and localization of the rendered HTML.</para>
        /// </summary>
        /// <returns>The HTML code and more.</returns>
        /// <param name='parameters'>
        /// Parameters.
        /// </param> 
        public Embed Oembed(params Expression<Func<string,object>>[] parameters)
        {
            return this.Tokens.AccessApi<Embed>(MethodType.Get, "statuses/oembed", parameters);
        }
            
        /// <summary>
        /// <para>Returns a single Tweet, specified by the id parameter. The Tweet's author will also be embedded within the tweet.</para>
        /// <para>Avaliable parameters: </para>
        /// <para><paramref name="long id (required)"/> : The numerical ID of the desired Tweet.</para>
        /// <para><paramref name="bool trim_user (optional)"/> : When set to true, each tweet returned in a timeline will include a user object including only the status authors numerical ID. Omit this parameter to receive the complete user object.</para>
        /// <para><paramref name="bool include_entities (optional)"/> : The entities node will be disincluded when set to false.</para>
        /// </summary>
        /// <returns>The status.</returns>
        /// <param name='parameters'>
        /// Parameters.
        /// </param>
        public Status Show(params Expression<Func<string,object>>[] parameters)
        {
            return this.Tokens.AccessApi<Status>(MethodType.Get, 
                string.Format("statuses/show/{0}", parameters.First(x => x.Parameters[0].Name == "id").Compile()("").ToString()),
                    parameters.Where(x => x.Parameters[0].Name != "id").ToArray());
        }
            
        /// <summary>
        /// <para>Returns up to 100 of the first retweets of a given tweet.</para>
        /// <para>Avaliable parameters: </para>
        /// <para><paramref name="long id (required)"/> : The numerical ID of the desired Tweet.</para>
        /// <para><paramref name="bool trim_user (optional)"/> : When set to true, each tweet returned in a timeline will include a user object including only the status authors numerical ID. Omit this parameter to receive the complete user object.</para>
        /// <para><paramref name="int count (optional)"/> : Specifies the number of records to retrieve. Must be less than or equal to 100.</para>
        /// </summary>
        /// <returns>Statuses.</returns>
        /// <param name='parameters'>
        /// Parameters.
        /// </param>
        public IEnumerable<Status> Retweets(params Expression<Func<string,object>>[] parameters)
        {
            return this.Tokens.AccessApiArray<Status>(MethodType.Get, 
                string.Format("statuses/retweets/{0}", parameters.First(x => x.Parameters[0].Name == "id").Compile()("").ToString()),
                    parameters.Where(x => x.Parameters[0].Name != "id").ToArray());
        }

        //POST Methods

        /// <summary>
        /// <para>Updates the authenticating user's current status, also known as tweeting. To upload an image to accompany the tweet, use POST statuses/update_with_media.</para>
        /// <para>For each update attempt, the update text is compared with the authenticating user's recent tweets. Any attempt that would result in duplication will be blocked, resulting in a 403 error. Therefore, a user cannot submit the same status twice in a row.</para>
        /// <para>While not rate limited by the API a user is limited in the number of tweets they can create at a time. If the number of updates posted by the user reaches the current allowed limit this method will return an HTTP 403 error.</para>
        /// <para>Avaliable parameters: </para>
        /// <para><paramref name="string status (required)"/> : The text of your status update, typically up to 140 characters. URL encode as necessary. t.co link wrapping may effect character counts.</para>
        /// <para><paramref name="long in_reply_to_status_id (optional)"/> : The ID of an existing status that the update is in reply to.</para>
        /// <para><paramref name="double lat (optional)"/> : The latitude of the location this tweet refers to. This parameter will be ignored unless it is inside the range -90.0 to +90.0 (North is positive) inclusive. It will also be ignored if there isn't a corresponding long parameter.</para>
        /// <para><paramref name="double long (optional)"/> : The longitude of the location this tweet refers to. The valid ranges for longitude is -180.0 to +180.0 (East is positive) inclusive. This parameter will be ignored if outside that range, if it is not a number, if geo_enabled is disabled, or if there not a corresponding lat parameter.</para>
        /// <para><paramref name="string place_id (optional)"/> : A place in the world. These IDs can be retrieved from GET geo/reverse_geocode.</para>
        /// <para><paramref name="bool display_coordinates (optional)"/> : Whether or not to put a pin on the exact coordinates a tweet has been sent from.</para>
        /// <para><paramref name="bool trim_user (optional)"/> : When set to true, each tweet returned in a timeline will include a user object including only the status authors numerical ID. Omit this parameter to receive the complete user object.</para>
        /// </summary>
        /// <returns>The updated status.</returns>
        /// <param name='parameters'>
        /// Parameters.
        /// </param>
        public Status Update(params Expression<Func<string,object>>[] parameters)
        {
            return this.Tokens.AccessApi<Status>(MethodType.Post, "statuses/update", parameters);
        }
            
        /// <summary>
        /// <para>Destroys the status specified by the required ID parameter. The authenticating user must be the author of the specified status. Returns the destroyed status if successful.</para>
        /// <para>Avaliable parameters: </para>
        /// <para><paramref name="long id (required)"/> : The numerical ID of the desired status.</para>
        /// <para><paramref name="bool trim_user (optional)"/> : When set to true, each tweet returned in a timeline will include a user object including only the status authors numerical ID. Omit this parameter to receive the complete user object.</para>
        /// </summary>
        /// <returns>The destroied status.</returns>
        /// <param name='parameters'>
        /// Parameters.
        /// </param>
        public Status Destroy(params Expression<Func<string,object>>[] parameters)
        {
            return this.Tokens.AccessApi<Status>(MethodType.Post, 
                string.Format("statuses/destroy/{0}", parameters.First(x => x.Parameters[0].Name == "id").Compile()("").ToString()), 
                    parameters.Where(x => x.Parameters[0].Name != "id").ToArray());
        }
            
        /// <summary>
        /// <para>Retweets a tweet. Returns the original tweet with retweet details embedded.</para>
        /// <para>Avaliable parameters: </para>
        /// <para><paramref name="long id (required)"/> : The numerical ID of the desired status.</para>
        /// <para><paramref name="bool trim_user (optional)"/> : When set to true, each tweet returned in a timeline will include a user object including only the status authors numerical ID. Omit this parameter to receive the complete user object.</para>
        /// </summary>
        /// <returns>The retweeted status.</returns>
        /// <param name='parameters'>
        /// Parameters.
        /// </param>
        public Status Retweet(params Expression<Func<string,object>>[] parameters)
        {
            return this.Tokens.AccessApi<Status>(MethodType.Post, 
                string.Format("statuses/retweet/{0}", parameters.First(x => x.Parameters[0].Name == "id").Compile()("").ToString()),
                    parameters.Where(x => x.Parameters[0].Name != "id").ToArray());
        }
    }
}
