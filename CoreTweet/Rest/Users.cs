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
using System.IO;
using CoreTweet.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Alice.Extensions;

namespace CoreTweet.Rest
{

    /// <summary>GET/POST users</summary>
    public class Users : TokenIncluded
    {
        internal Users(Tokens e) : base(e) { }
            
        //GET Methods
            
        /// <summary>
        /// <para>Returns a collection of users that the specified user can "contribute" to.</para>
        /// <para>Note: A user_id or screen_name is required.</para>
        /// <para>Avaliable parameters: </para>
        /// <para><paramref name="long user_id (optional)"/> : The ID of the user for whom to return results for. Helpful for disambiguating when a valid user ID is also a valid screen name.</para>
        /// <para><paramref name="string screen_name (optional)"/> : The screen name of the user for whom to return results for.</para>
        /// <para><paramref name="bool include_entities (optional)"/> : The entities node will be disincluded when set to false.</para>
        /// <para><paramref name="bool skip_status (optional)"/> : When set to either true, t or 1 statuses will not be included in the returned user objects.</para>
        /// </summary>
        /// <returns>Users.</returns>
        /// <param name='parameters'>
        /// Parameters.
        /// </param>
        public IEnumerable<User> Contributees(params Expression<Func<string,object>>[] parameters)
        {
            return this.Tokens.AccessApiArray<User>(MethodType.Get, "users/contributees", parameters);
        }
            
        /// <summary>
        /// <para>Returns a collection of users who can contribute to the specified account.</para>
        /// <para>Note: A user_id or screen_name is required.</para>
        /// <para>Avaliable parameters: </para>
        /// <para><paramref name="long user_id (optional)"/> : The ID of the user for whom to return results for.</para>
        /// <para><paramref name="string screen_name (optional)"/> : The screen name of the user for whom to return results for.</para>
        /// <para><paramref name="bool include_entities (optional)"/> : The entities node will be disincluded when set to false.</para>
        /// <para><paramref name="bool skip_status (optional)"/> : When set to either true, t or 1 statuses will not be included in the returned user objects.</para>
        /// </summary>
        /// <returns>Users.</returns>
        /// <param name='parameters'>
        /// Parameters.
        /// </param>
        public IEnumerable<User> Contributors(params Expression<Func<string,object>>[] parameters)
        {
            return this.Tokens.AccessApiArray<User>(MethodType.Get, "users/contributors", parameters);
        }
            
        /// <summary>
        /// <para>Returns fully-hydrated user objects for up to 100 users per request, as specified by comma-separated values passed to the user_id and/or screen_name parameters.</para>
        /// <para>This method is especially useful when used in conjunction with collections of user IDs returned from GET friends/ids and GET followers/ids.</para>
        /// <para>GET users/show is used to retrieve a single user object.</para>
        /// <para>Avaliable parameters: </para>
        /// <para><paramref name="string user_id (optional)"/> : A comma separated list of user IDs, up to 100 are allowed in a single request. You are strongly encouraged to use a POST for larger requests.</para>
        /// <para><paramref name="string screen_name (optional)"/> : A comma separated list of screen names, up to 100 are allowed in a single request. You are strongly encouraged to use a POST for larger (up to 100 screen names) requests.</para>
        /// <para><paramref name="bool include_entities (optional)"/> : The entities node that may appear within embedded statuses will be disincluded when set to false.</para>
        /// </summary>
        /// <returns>Users.</returns>
        /// <param name='parameters'>
        /// Parameters.
        /// </param>
        public IEnumerable<User> Lookup(params Expression<Func<string,object>>[] parameters)
        {
            return this.Tokens.AccessApiArray<User>(MethodType.Get, "users/lookup", parameters);
        }
            
        /// <summary>
        /// <para>Returns the size of the specified user's profile banner. If the user has not uploaded a profile banner, a HTTP 404 will be served instead. This method can be used instead of string manipulation on the profile_banner_url returned in user objects as described in User Profile Images and Banners.</para>
        /// <para>Note: Always specify either an user_id or screen_name when requesting this method.</para>
        /// <para>Avaliable parameters: </para>
        /// <para><paramref name="long id (optional)"/> : The ID of the user for whom to return results for. Helpful for disambiguating when a valid user ID is also a valid screen name.</para>
        /// <para><paramref name="string screen_name (optional)"/> : The screen name of the user for whom to return results for. Helpful for disambiguating when a valid screen name is also a user ID.</para>
        /// </summary>
        /// <returns>The size.</returns>
        /// <param name='parameters'>
        /// Parameters.
        /// </param>
        public Size ProfileBanner(params Expression<Func<string,object>>[] parameters)
        {
            var j = JObject.Parse(from x in this.Tokens.SendRequest(MethodType.Get, "users/profile_banner", parameters).Use()
                                  from y in new StreamReader(x).Use()
                                  select y.ReadToEnd());
            return j["web"].ToObject<Size>();
        }
            
        /// <summary>
        /// <para>Provides a simple, relevance-based search interface to public user accounts on Twitter. Try querying by topical interest, full name, company name, location, or other criteria. Exact match searches are not supported.</para>
        /// <para>Only the first 1,000 matching results are available.</para>
        /// <para>Avaliable parameters: </para>
        /// <para><paramref name="string q (required)"/> : The search query to run against people search.</para>
        /// <para><paramref name="int page (optional)"/> : Specifies the page of results to retrieve.</para>
        /// <para><paramref name="int count (optional)"/> : The number of potential user results to retrieve per page. This value has a maximum of 20.</para>
        /// <para><paramref name="bool include_entities (optional)"/> : The entities node will be disincluded from embedded tweet objects when set to false.</para>
        /// </summary>
        /// <returns>Users.</returns>
        /// <param name='parameters'>
        /// Parameters.
        /// </param>
        public IEnumerable<User> Search(params Expression<Func<string,object>>[] parameters)
        {
            return this.Tokens.AccessApiArray<User>(MethodType.Get, "users/search", parameters);
        }
            
        /// <summary>
        /// <para>Returns a variety of information about the user specified by the required user_id or screen_name parameter. The author's most recent Tweet will be returned inline when possible.</para>
        /// <para>GET users/lookup is used to retrieve a bulk collection of user objects.</para>
        /// <para>Avaliable parameters: </para>
        /// <para><paramref name="long user_id (required)"/> : The ID of the user for whom to return results for. Either an id or screen_name is required for this method.</para>
        /// <para><paramref name="string screen_name (required)"/> : The screen name of the user for whom to return results for. Either a id or screen_name is required for this method.</para>
        /// <para><paramref name="bool include_entities (optional)"/> : The entities node will be disincluded when set to false.</para>
        /// </summary>
        /// <returns>The user.</returns>
        /// <param name='parameters'>
        /// Parameters.
        /// </param>
        public User Show(params Expression<Func<string,object>>[] parameters)
        {
            return this.Tokens.AccessApi<User>(MethodType.Get, "users/show", parameters);
        }
            
        /// <summary>
        /// <para>Access to Twitter's suggested user list. This returns the list of suggested user categories. The category can be used in GET users/suggestions/:slug to get the users in that category.</para>
        /// <para>Avaliable parameters: </para>
        /// <para><paramref name="string lang (optional)"/> : Restricts the suggested categories to the requested language. The language must be specified by the appropriate two letter ISO 639-1 representation. Currently supported languages are provided by the GET help/languages API request. Unsupported language codes will receive English (en) results. If you use lang in this request, ensure you also include it when requesting the GET users/suggestions/:slug list.</para>
        /// </summary>
        /// <returns>Catgories.</returns>
        /// <param name='parameters'>
        /// Parameters.
        /// </param>
        public IEnumerable<Category> Suggestions(params Expression<Func<string,object>>[] parameters)
        {
            return this.Tokens.AccessApiArray<Category>(MethodType.Get, "users/suggestions", parameters);
        }
            
        /// <summary>
        /// <para>Access the users in a given category of the Twitter suggested user list and return their most recent status if they are not a protected user.</para>
        /// <para>Avaliable parameters: </para>
        /// <para><paramref name="string slug (required)"/> : The short name of list or a category</para>
        /// </summary>
        /// <returns>Users.</returns>
        /// <param name='parameters'>
        /// Parameters.
        /// </param>
        public IEnumerable<User> SuggestedMembers(params Expression<Func<string,object>>[] parameters)
        {
            return this.Tokens.AccessApiArray<User>(MethodType.Get, string.Format("users/suggestions/{0}/members", 
                    parameters.First(x => x.Parameters[0].Name == "id").Compile()("").ToString()), 
                         parameters.Where(x => x.Parameters[0].Name != "id").ToArray());
        }
            
        //POST Method
            
        /// <summary>
        /// <para>Report the specified user as a spam account to Twitter. Additionally performs the equivalent of POST blocks/create on behalf of the authenticated user.</para>
        /// <para>Note: One of these parameters must be provided.</para>
        /// <para>Avaliable parameters: </para>
        /// <para><paramref name="string screen_name (optional)"/> : The ID or screen_name of the user you want to report as a spammer. Helpful for disambiguating when a valid screen name is also a user ID.</para>
        /// <para><paramref name="long user_id (optional)"/> : The ID of the user you want to report as a spammer. Helpful for disambiguating when a valid user ID is also a valid screen name.</para>
        /// </summary>
        /// <returns>The User.</returns>
        /// <param name='parameters'>
        /// Parameters.
        /// </param>
        public User ReportSpam(params Expression<Func<string,object>>[] parameters)
        {
            return this.Tokens.AccessApi<User>(MethodType.Post, "users/report_spam", parameters);
        }
    }
}