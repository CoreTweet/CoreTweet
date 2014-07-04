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
using System.Linq.Expressions;
using CoreTweet.Core;

namespace CoreTweet.Rest
{
    /// <summary>
    /// Provides a set of methods for the wrapper of GET/POST users.
    /// </summary>
    public partial class Users : ApiProviderBase
    {
        internal Users(TokensBase e) : base(e) { }

#if !(PCL || WIN_RT || WP)
        //GET Methods

        /// <summary>
        /// <para>Returns a collection of users that the specified user can "contribute" to.</para>
        /// <para>Note: A user_id or screen_name is required.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> user_id (optional)</para>
        /// <para>- <c>string</c> screen_name (optional)</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// <para>- <c>bool</c> skip_status (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The users.</returns>
        public ListedResponse<User> Contributees(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiArray<User>(MethodType.Get, "users/contributees", parameters);
        }

        /// <summary>
        /// <para>Returns a collection of users that the specified user can "contribute" to.</para>
        /// <para>Note: A user_id or screen_name is required.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> user_id (optional)</para>
        /// <para>- <c>string</c> screen_name (optional)</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// <para>- <c>bool</c> skip_status (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The users.</returns>
        public ListedResponse<User> Contributees(IDictionary<string, object> parameters)
        {
            return this.Tokens.AccessApiArray<User>(MethodType.Get, "users/contributees", parameters);
        }

        /// <summary>
        /// <para>Returns a collection of users that the specified user can "contribute" to.</para>
        /// <para>Note: A user_id or screen_name is required.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> user_id (optional)</para>
        /// <para>- <c>string</c> screen_name (optional)</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// <para>- <c>bool</c> skip_status (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The users.</returns>
        public ListedResponse<User> Contributees<T>(T parameters)
        {
            return this.Tokens.AccessApiArray<User, T>(MethodType.Get, "users/contributees", parameters);
        }

        /// <summary>
        /// <para>Returns a collection of users who can contribute to the specified account.</para>
        /// <para>Note: A user_id or screen_name is required.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> user_id (optional)</para>
        /// <para>- <c>string</c> screen_name (optional)</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// <para>- <c>bool</c> skip_status (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The users.</returns>
        public ListedResponse<User> Contributors(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiArray<User>(MethodType.Get, "users/contributors", parameters);
        }

        /// <summary>
        /// <para>Returns a collection of users who can contribute to the specified account.</para>
        /// <para>Note: A user_id or screen_name is required.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> user_id (optional)</para>
        /// <para>- <c>string</c> screen_name (optional)</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// <para>- <c>bool</c> skip_status (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The users.</returns>
        public ListedResponse<User> Contributors(IDictionary<string, object> parameters)
        {
            return this.Tokens.AccessApiArray<User>(MethodType.Get, "users/contributors", parameters);
        }

        /// <summary>
        /// <para>Returns a collection of users who can contribute to the specified account.</para>
        /// <para>Note: A user_id or screen_name is required.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> user_id (optional)</para>
        /// <para>- <c>string</c> screen_name (optional)</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// <para>- <c>bool</c> skip_status (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The users.</returns>
        public ListedResponse<User> Contributors<T>(T parameters)
        {
            return this.Tokens.AccessApiArray<User, T>(MethodType.Get, "users/contributors", parameters);
        }

        /// <summary>
        /// <para>Returns fully-hydrated user objects for up to 100 users per request, as specified by comma-separated values passed to the user_id and/or screen_name parameters.</para>
        /// <para>This method is especially useful when used in conjunction with collections of user IDs returned from GET friends/ids and GET followers/ids.</para>
        /// <para>GET users/show is used to retrieve a single user object.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> / <c>IEnumerable&lt;long&gt;</c> user_id (optional)</para>
        /// <para>- <c>string</c> / <c>IEnumerable&lt;string&gt;</c> screen_name (optional)</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The users.</returns>
        public ListedResponse<User> Lookup(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiArray<User>(MethodType.Get, "users/lookup", parameters);
        }

        /// <summary>
        /// <para>Returns fully-hydrated user objects for up to 100 users per request, as specified by comma-separated values passed to the user_id and/or screen_name parameters.</para>
        /// <para>This method is especially useful when used in conjunction with collections of user IDs returned from GET friends/ids and GET followers/ids.</para>
        /// <para>GET users/show is used to retrieve a single user object.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> / <c>IEnumerable&lt;long&gt;</c> user_id (optional)</para>
        /// <para>- <c>string</c> / <c>IEnumerable&lt;string&gt;</c> screen_name (optional)</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The users.</returns>
        public ListedResponse<User> Lookup(IDictionary<string, object> parameters)
        {
            return this.Tokens.AccessApiArray<User>(MethodType.Get, "users/lookup", parameters);
        }

        /// <summary>
        /// <para>Returns fully-hydrated user objects for up to 100 users per request, as specified by comma-separated values passed to the user_id and/or screen_name parameters.</para>
        /// <para>This method is especially useful when used in conjunction with collections of user IDs returned from GET friends/ids and GET followers/ids.</para>
        /// <para>GET users/show is used to retrieve a single user object.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> / <c>IEnumerable&lt;long&gt;</c> user_id (optional)</para>
        /// <para>- <c>string</c> / <c>IEnumerable&lt;string&gt;</c> screen_name (optional)</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The users.</returns>
        public ListedResponse<User> Lookup<T>(T parameters)
        {
            return this.Tokens.AccessApiArray<User, T>(MethodType.Get, "users/lookup", parameters);
        }

        /// <summary>
        /// <para>Returns the size of the specified user's profile banner.</para>
        /// <para>If the user has not uploaded a profile banner, a HTTP 404 will be served instead.</para>
        /// <para>This method can be used instead of string manipulation on the profile_banner_url returned in user objects as described in User Profile Images and Banners.</para>
        /// <para>Note: Always specify either an user_id or screen_name when requesting this method.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> id (optional)</para>
        /// <para>- <c>string</c> screen_name (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The sizes.</returns>
        public ProfileBannerSizes ProfileBanner(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApi<ProfileBannerSizes>(MethodType.Get, "users/profile_banner", parameters, "sizes");
        }

        /// <summary>
        /// <para>Returns the size of the specified user's profile banner.</para>
        /// <para>If the user has not uploaded a profile banner, a HTTP 404 will be served instead.</para>
        /// <para>This method can be used instead of string manipulation on the profile_banner_url returned in user objects as described in User Profile Images and Banners.</para>
        /// <para>Note: Always specify either an user_id or screen_name when requesting this method.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> id (optional)</para>
        /// <para>- <c>string</c> screen_name (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The sizes.</returns>
        public ProfileBannerSizes ProfileBanner(IDictionary<string, object> parameters)
        {
            return this.Tokens.AccessApi<ProfileBannerSizes>(MethodType.Get, "users/profile_banner", parameters, "sizes");
        }

        /// <summary>
        /// <para>Returns the size of the specified user's profile banner.</para>
        /// <para>If the user has not uploaded a profile banner, a HTTP 404 will be served instead.</para>
        /// <para>This method can be used instead of string manipulation on the profile_banner_url returned in user objects as described in User Profile Images and Banners.</para>
        /// <para>Note: Always specify either an user_id or screen_name when requesting this method.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> id (optional)</para>
        /// <para>- <c>string</c> screen_name (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The sizes.</returns>
        public ProfileBannerSizes ProfileBanner<T>(T parameters)
        {
            return this.Tokens.AccessApi<ProfileBannerSizes, T>(MethodType.Get, "users/profile_banner", parameters, "sizes");
        }

        /// <summary>
        /// <para>Provides a simple, relevance-based search interface to public user accounts on Twitter.</para>
        /// <para>Try querying by topical interest, full name, company name, location, or other criteria.</para>
        /// <para>Exact match searches are not supported.</para>
        /// <para>Only the first 1,000 matching results are available.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> q (required)</para>
        /// <para>- <c>int</c> page (optional)</para>
        /// <para>- <c>int</c> count (optional)</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The users.</returns>
        public ListedResponse<User> Search(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiArray<User>(MethodType.Get, "users/search", parameters);
        }

        /// <summary>
        /// <para>Provides a simple, relevance-based search interface to public user accounts on Twitter.</para>
        /// <para>Try querying by topical interest, full name, company name, location, or other criteria.</para>
        /// <para>Exact match searches are not supported.</para>
        /// <para>Only the first 1,000 matching results are available.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> q (required)</para>
        /// <para>- <c>int</c> page (optional)</para>
        /// <para>- <c>int</c> count (optional)</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The users.</returns>
        public ListedResponse<User> Search(IDictionary<string, object> parameters)
        {
            return this.Tokens.AccessApiArray<User>(MethodType.Get, "users/search", parameters);
        }

        /// <summary>
        /// <para>Provides a simple, relevance-based search interface to public user accounts on Twitter.</para>
        /// <para>Try querying by topical interest, full name, company name, location, or other criteria.</para>
        /// <para>Exact match searches are not supported.</para>
        /// <para>Only the first 1,000 matching results are available.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> q (required)</para>
        /// <para>- <c>int</c> page (optional)</para>
        /// <para>- <c>int</c> count (optional)</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The users.</returns>
        public ListedResponse<User> Search<T>(T parameters)
        {
            return this.Tokens.AccessApiArray<User, T>(MethodType.Get, "users/search", parameters);
        }

        /// <summary>
        /// <para>Returns a variety of information about the user specified by the required user_id or screen_name parameter.</para>
        /// <para>The author's most recent Tweet will be returned inline when possible.</para>
        /// <para>GET users/lookup is used to retrieve a bulk collection of user objects.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> user_id (required)</para>
        /// <para>- <c>string</c> screen_name (required)</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The user.</returns>
        public UserResponse Show(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApi<UserResponse>(MethodType.Get, "users/show", parameters);
        }

        /// <summary>
        /// <para>Returns a variety of information about the user specified by the required user_id or screen_name parameter.</para>
        /// <para>The author's most recent Tweet will be returned inline when possible.</para>
        /// <para>GET users/lookup is used to retrieve a bulk collection of user objects.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> user_id (required)</para>
        /// <para>- <c>string</c> screen_name (required)</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The user.</returns>
        public UserResponse Show(IDictionary<string, object> parameters)
        {
            return this.Tokens.AccessApi<UserResponse>(MethodType.Get, "users/show", parameters);
        }

        /// <summary>
        /// <para>Returns a variety of information about the user specified by the required user_id or screen_name parameter.</para>
        /// <para>The author's most recent Tweet will be returned inline when possible.</para>
        /// <para>GET users/lookup is used to retrieve a bulk collection of user objects.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> user_id (required)</para>
        /// <para>- <c>string</c> screen_name (required)</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The user.</returns>
        public UserResponse Show<T>(T parameters)
        {
            return this.Tokens.AccessApi<UserResponse, T>(MethodType.Get, "users/show", parameters);
        }

        /// <summary>
        /// <para>Access to Twitter's suggested user list.</para>
        /// <para>This returns the list of suggested user categories.</para>
        /// <para>The category can be used in GET users/suggestions/:slug to get the users in that category.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> lang (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The catgories.</returns>
        public ListedResponse<Category> Suggestions(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiArray<Category>(MethodType.Get, "users/suggestions", parameters);
        }

        /// <summary>
        /// <para>Access to Twitter's suggested user list.</para>
        /// <para>This returns the list of suggested user categories.</para>
        /// <para>The category can be used in GET users/suggestions/:slug to get the users in that category.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> lang (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The catgories.</returns>
        public ListedResponse<Category> Suggestions(IDictionary<string, object> parameters)
        {
            return this.Tokens.AccessApiArray<Category>(MethodType.Get, "users/suggestions", parameters);
        }

        /// <summary>
        /// <para>Access to Twitter's suggested user list.</para>
        /// <para>This returns the list of suggested user categories.</para>
        /// <para>The category can be used in GET users/suggestions/:slug to get the users in that category.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> lang (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The catgories.</returns>
        public ListedResponse<Category> Suggestions<T>(T parameters)
        {
            return this.Tokens.AccessApiArray<Category, T>(MethodType.Get, "users/suggestions", parameters);
        }

        /// <summary>
        /// <para>Access the users in a given category of the Twitter suggested user list.</para>
        /// <para>It is recommended that applications cache this data for no more than one hour.</para>
        /// <para>- <c>string</c> slug (required)</para>
        /// <para>- <c>string</c> lang (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The category.</returns>
        public CategoryResponse Suggestion(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessParameterReservedApi<CategoryResponse>(MethodType.Get, "users/suggestions/{slug}", "slug", InternalUtils.ExpressionsToDictionary(parameters));
        }

        /// <summary>
        /// <para>Access the users in a given category of the Twitter suggested user list.</para>
        /// <para>It is recommended that applications cache this data for no more than one hour.</para>
        /// <para>- <c>string</c> slug (required)</para>
        /// <para>- <c>string</c> lang (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The category.</returns>
        public CategoryResponse Suggestion(IDictionary<string, object> parameters)
        {
            return this.Tokens.AccessParameterReservedApi<CategoryResponse>(MethodType.Get, "users/suggestions/{slug}", "slug", parameters);
        }

        /// <summary>
        /// <para>Access the users in a given category of the Twitter suggested user list.</para>
        /// <para>It is recommended that applications cache this data for no more than one hour.</para>
        /// <para>- <c>string</c> slug (required)</para>
        /// <para>- <c>string</c> lang (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The category.</returns>
        public CategoryResponse Suggestion<T>(T parameters)
        {
            return this.Tokens.AccessParameterReservedApi<CategoryResponse>(MethodType.Get, "users/suggestions/{slug}", "slug", InternalUtils.ResolveObject(parameters));
        }

        /// <summary>
        /// <para>Access the users in a given category of the Twitter suggested user list and return their most recent status if they are not a protected user.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> slug (required)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The users.</returns>
        public ListedResponse<User> SuggestedMembers(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessParameterReservedApiArray<User>(MethodType.Get, "users/suggestions/{slug}/members", "slug", InternalUtils.ExpressionsToDictionary(parameters));
        }

        /// <summary>
        /// <para>Access the users in a given category of the Twitter suggested user list and return their most recent status if they are not a protected user.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> slug (required)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The users.</returns>
        public ListedResponse<User> SuggestedMembers(IDictionary<string, object> parameters)
        {
            return this.Tokens.AccessParameterReservedApiArray<User>(MethodType.Get, "users/suggestions/{slug}/members", "slug", parameters);
        }

        /// <summary>
        /// <para>Access the users in a given category of the Twitter suggested user list and return their most recent status if they are not a protected user.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> slug (required)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The users.</returns>
        public ListedResponse<User> SuggestedMembers<T>(T parameters)
        {
            return this.Tokens.AccessParameterReservedApiArray<User>(MethodType.Get, "users/suggestions/{slug}/members", "slug", InternalUtils.ResolveObject(parameters));
        }

        //POST Method

        /// <summary>
        /// <para>Report the specified user as a spam account to Twitter.</para>
        /// <para>Additionally performs the equivalent of POST blocks/create on behalf of the authenticated user.</para>
        /// <para>Note: One of these parameters must be provided.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> screen_name (optional)</para>
        /// <para>- <c>long</c> user_id (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The user.</returns>
        public UserResponse ReportSpam(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApi<UserResponse>(MethodType.Post, "users/report_spam", parameters);
        }

        /// <summary>
        /// <para>Report the specified user as a spam account to Twitter.</para>
        /// <para>Additionally performs the equivalent of POST blocks/create on behalf of the authenticated user.</para>
        /// <para>Note: One of these parameters must be provided.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> screen_name (optional)</para>
        /// <para>- <c>long</c> user_id (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The user.</returns>
        public UserResponse ReportSpam(IDictionary<string, object> parameters)
        {
            return this.Tokens.AccessApi<UserResponse>(MethodType.Post, "users/report_spam", parameters);
        }

        /// <summary>
        /// <para>Report the specified user as a spam account to Twitter.</para>
        /// <para>Additionally performs the equivalent of POST blocks/create on behalf of the authenticated user.</para>
        /// <para>Note: One of these parameters must be provided.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> screen_name (optional)</para>
        /// <para>- <c>long</c> user_id (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The user.</returns>
        public UserResponse ReportSpam<T>(T parameters)
        {
            return this.Tokens.AccessApi<UserResponse, T>(MethodType.Post, "users/report_spam", parameters);
        }
#endif
    }
}
