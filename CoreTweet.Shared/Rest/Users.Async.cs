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

#if !NET35
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using CoreTweet.Core;

namespace CoreTweet.Rest
{
    partial class Users
    {
        //GET Methods

        /// <summary>
        /// <para>Returns a collection of users that the specified user can "contribute" to as an asynchronous operation.</para>
        /// <para>Note: A user_id or screen_name is required.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> user_id (optional)</para>
        /// <para>- <c>string</c> screen_name (optional)</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// <para>- <c>bool</c> skip_status (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the users.</para>
        /// </returns>
        public Task<ListedResponse<User>> ContributeesAsync(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiArrayAsync<User>(MethodType.Get, "users/contributees", parameters);
        }

        /// <summary>
        /// <para>Returns a collection of users that the specified user can "contribute" to as an asynchronous operation.</para>
        /// <para>Note: A user_id or screen_name is required.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> user_id (optional)</para>
        /// <para>- <c>string</c> screen_name (optional)</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// <para>- <c>bool</c> skip_status (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the users.</para>
        /// </returns>
        public Task<ListedResponse<User>> ContributeesAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiArrayAsync<User>(MethodType.Get, "users/contributees", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Returns a collection of users that the specified user can "contribute" to as an asynchronous operation.</para>
        /// <para>Note: A user_id or screen_name is required.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> user_id (optional)</para>
        /// <para>- <c>string</c> screen_name (optional)</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// <para>- <c>bool</c> skip_status (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the users.</para>
        /// </returns>
        public Task<ListedResponse<User>> ContributeesAsync<T>(T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiArrayAsync<User, T>(MethodType.Get, "users/contributees", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Returns a collection of users who can contribute to the specified account as an asynchronous operation.</para>
        /// <para>Note: A user_id or screen_name is required.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> user_id (optional)</para>
        /// <para>- <c>string</c> screen_name (optional)</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// <para>- <c>bool</c> skip_status (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the users.</para>
        /// </returns>
        public Task<ListedResponse<User>> ContributorsAsync(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiArrayAsync<User>(MethodType.Get, "users/contributors", parameters);
        }

        /// <summary>
        /// <para>Returns a collection of users who can contribute to the specified account as an asynchronous operation.</para>
        /// <para>Note: A user_id or screen_name is required.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> user_id (optional)</para>
        /// <para>- <c>string</c> screen_name (optional)</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// <para>- <c>bool</c> skip_status (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the users.</para>
        /// </returns>
        public Task<ListedResponse<User>> ContributorsAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiArrayAsync<User>(MethodType.Get, "users/contributors", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Returns a collection of users who can contribute to the specified account as an asynchronous operation.</para>
        /// <para>Note: A user_id or screen_name is required.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> user_id (optional)</para>
        /// <para>- <c>string</c> screen_name (optional)</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// <para>- <c>bool</c> skip_status (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the users.</para>
        /// </returns>
        public Task<ListedResponse<User>> ContributorsAsync<T>(T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiArrayAsync<User, T>(MethodType.Get, "users/contributors", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Returns fully-hydrated user objects for up to 100 users per request, as specified by comma-separated values passed to the user_id and/or screen_name parameters as an asynchronous operation.</para>
        /// <para>This method is especially useful when used in conjunction with collections of user IDs returned from GET friends/ids and GET followers/ids.</para>
        /// <para>GET users/show is used to retrieve a single user object.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> / <c>IEnumerable&lt;long&gt;</c> user_id (optional)</para>
        /// <para>- <c>string</c> / <c>IEnumerable&lt;string&gt;</c> screen_name (optional)</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the users.</para>
        /// </returns>
        public Task<ListedResponse<User>> LookupAsync(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiArrayAsync<User>(MethodType.Get, "users/lookup", parameters);
        }

        /// <summary>
        /// <para>Returns fully-hydrated user objects for up to 100 users per request, as specified by comma-separated values passed to the user_id and/or screen_name parameters as an asynchronous operation.</para>
        /// <para>This method is especially useful when used in conjunction with collections of user IDs returned from GET friends/ids and GET followers/ids.</para>
        /// <para>GET users/show is used to retrieve a single user object.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> / <c>IEnumerable&lt;long&gt;</c> user_id (optional)</para>
        /// <para>- <c>string</c> / <c>IEnumerable&lt;string&gt;</c> screen_name (optional)</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the users.</para>
        /// </returns>
        public Task<ListedResponse<User>> LookupAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiArrayAsync<User>(MethodType.Get, "users/lookup", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Returns fully-hydrated user objects for up to 100 users per request, as specified by comma-separated values passed to the user_id and/or screen_name parameters as an asynchronous operation.</para>
        /// <para>This method is especially useful when used in conjunction with collections of user IDs returned from GET friends/ids and GET followers/ids.</para>
        /// <para>GET users/show is used to retrieve a single user object.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> / <c>IEnumerable&lt;long&gt;</c> user_id (optional)</para>
        /// <para>- <c>string</c> / <c>IEnumerable&lt;string&gt;</c> screen_name (optional)</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the users.</para>
        /// </returns>
        public Task<ListedResponse<User>> LookupAsync<T>(T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiArrayAsync<User, T>(MethodType.Get, "users/lookup", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Returns the size of the specified user's profile banner as an asynchronous operation.</para>
        /// <para>If the user has not uploaded a profile banner, a HTTP 404 will be served instead.</para>
        /// <para>This method can be used instead of string manipulation on the profile_banner_url returned in user objects as described in User Profile Images and Banners.</para>
        /// <para>Note: Always specify either an user_id or screen_name when requesting this method.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> id (optional)</para>
        /// <para>- <c>string</c> screen_name (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the sizes.</para>
        /// </returns>
        public Task<ProfileBannerSizes> ProfileBannerAsync(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiAsync<ProfileBannerSizes>(MethodType.Get, "users/profile_banner", parameters, "sizes");
        }

        /// <summary>
        /// <para>Returns the size of the specified user's profile banner as an asynchronous operation.</para>
        /// <para>If the user has not uploaded a profile banner, a HTTP 404 will be served instead.</para>
        /// <para>This method can be used instead of string manipulation on the profile_banner_url returned in user objects as described in User Profile Images and Banners.</para>
        /// <para>Note: Always specify either an user_id or screen_name when requesting this method.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> id (optional)</para>
        /// <para>- <c>string</c> screen_name (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the sizes.</para>
        /// </returns>
        public Task<ProfileBannerSizes> ProfileBannerAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<ProfileBannerSizes>(MethodType.Get, "users/profile_banner", parameters, cancellationToken, "sizes");
        }

        /// <summary>
        /// <para>Returns the size of the specified user's profile banner as an asynchronous operation.</para>
        /// <para>If the user has not uploaded a profile banner, a HTTP 404 will be served instead.</para>
        /// <para>This method can be used instead of string manipulation on the profile_banner_url returned in user objects as described in User Profile Images and Banners.</para>
        /// <para>Note: Always specify either an user_id or screen_name when requesting this method.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> id (optional)</para>
        /// <para>- <c>string</c> screen_name (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the sizes.</para>
        /// </returns>
        public Task<ProfileBannerSizes> ProfileBannerAsync<T>(T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<ProfileBannerSizes, T>(MethodType.Get, "users/profile_banner", parameters, cancellationToken, "sizes");
        }

        /// <summary>
        /// <para>Provides a simple, relevance-based search interface to public user accounts on Twitter as an asynchronous operation.</para>
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
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the users.</para>
        /// </returns>
        public Task<ListedResponse<User>> SearchAsync(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiArrayAsync<User>(MethodType.Get, "users/search", parameters);
        }

        /// <summary>
        /// <para>Provides a simple, relevance-based search interface to public user accounts on Twitter as an asynchronous operation.</para>
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
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the users.</para>
        /// </returns>
        public Task<ListedResponse<User>> SearchAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiArrayAsync<User>(MethodType.Get, "users/search", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Provides a simple, relevance-based search interface to public user accounts on Twitter as an asynchronous operation.</para>
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
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the users.</para>
        /// </returns>
        public Task<ListedResponse<User>> SearchAsync<T>(T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiArrayAsync<User, T>(MethodType.Get, "users/search", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Returns a variety of information about the user specified by the required user_id or screen_name parameter as an asynchronous operation.</para>
        /// <para>The author's most recent Tweet will be returned inline when possible.</para>
        /// <para>GET users/lookup is used to retrieve a bulk collection of user objects.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> user_id (required)</para>
        /// <para>- <c>string</c> screen_name (required)</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the user.</para>
        /// </returns>
        public Task<UserResponse> ShowAsync(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiAsync<UserResponse>(MethodType.Get, "users/show", parameters);
        }

        /// <summary>
        /// <para>Returns a variety of information about the user specified by the required user_id or screen_name parameter as an asynchronous operation.</para>
        /// <para>The author's most recent Tweet will be returned inline when possible.</para>
        /// <para>GET users/lookup is used to retrieve a bulk collection of user objects.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> user_id (required)</para>
        /// <para>- <c>string</c> screen_name (required)</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the user.</para>
        /// </returns>
        public Task<UserResponse> ShowAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<UserResponse>(MethodType.Get, "users/show", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Returns a variety of information about the user specified by the required user_id or screen_name parameter as an asynchronous operation.</para>
        /// <para>The author's most recent Tweet will be returned inline when possible.</para>
        /// <para>GET users/lookup is used to retrieve a bulk collection of user objects.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>long</c> user_id (required)</para>
        /// <para>- <c>string</c> screen_name (required)</para>
        /// <para>- <c>bool</c> include_entities (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the user.</para>
        /// </returns>
        public Task<UserResponse> ShowAsync<T>(T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<UserResponse, T>(MethodType.Get, "users/show", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Access to Twitter's suggested user list as an asynchronous operation.</para>
        /// <para>This returns the list of suggested user categories.</para>
        /// <para>The category can be used in GET users/suggestions/:slug to get the users in that category.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> lang (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the categories.</para>
        /// </returns>
        public Task<ListedResponse<Category>> SuggestionsAsync(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiArrayAsync<Category>(MethodType.Get, "users/suggestions", parameters);
        }

        /// <summary>
        /// <para>Access to Twitter's suggested user list as an asynchronous operation.</para>
        /// <para>This returns the list of suggested user categories.</para>
        /// <para>The category can be used in GET users/suggestions/:slug to get the users in that category.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> lang (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the categories.</para>
        /// </returns>
        public Task<ListedResponse<Category>> SuggestionsAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiArrayAsync<Category>(MethodType.Get, "users/suggestions", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Access to Twitter's suggested user list as an asynchronous operation.</para>
        /// <para>This returns the list of suggested user categories.</para>
        /// <para>The category can be used in GET users/suggestions/:slug to get the users in that category.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> lang (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the categories.</para>
        /// </returns>
        public Task<ListedResponse<Category>> SuggestionsAsync<T>(T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiArrayAsync<Category, T>(MethodType.Get, "users/suggestions", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Access the users in a given category of the Twitter suggested user list as an asynchronous operation.</para>
        /// <para>It is recommended that applications cache this data for no more than one hour.</para>
        /// <para>- <c>string</c> slug (required)</para>
        /// <para>- <c>string</c> lang (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the category.</para>
        /// </returns>
        public Task<CategoryResponse> SuggestionAsync(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessParameterReservedApiAsync<CategoryResponse>(MethodType.Get, "users/suggestions/{slug}", "slug", InternalUtils.ExpressionsToDictionary(parameters), CancellationToken.None);
        }

        /// <summary>
        /// <para>Access the users in a given category of the Twitter suggested user list as an asynchronous operation.</para>
        /// <para>It is recommended that applications cache this data for no more than one hour.</para>
        /// <para>- <c>string</c> slug (required)</para>
        /// <para>- <c>string</c> lang (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the category.</para>
        /// </returns>
        public Task<CategoryResponse> SuggestionAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessParameterReservedApiAsync<CategoryResponse>(MethodType.Get, "users/suggestions/{slug}", "slug", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Access the users in a given category of the Twitter suggested user list as an asynchronous operation.</para>
        /// <para>It is recommended that applications cache this data for no more than one hour.</para>
        /// <para>- <c>string</c> slug (required)</para>
        /// <para>- <c>string</c> lang (optional)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the category.</para>
        /// </returns>
        public Task<CategoryResponse> SuggestionAsync<T>(T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessParameterReservedApiAsync<CategoryResponse>(MethodType.Get, "users/suggestions/{slug}", "slug", InternalUtils.ResolveObject(parameters), cancellationToken);
        }

        /// <summary>
        /// <para>Access the users in a given category of the Twitter suggested user list and return their most recent status if they are not a protected user as an asynchronous operation.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> slug (required)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the category.</para>
        /// </returns>
        public Task<ListedResponse<User>> SuggestedMembersAsync(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessParameterReservedApiArrayAsync<User>(MethodType.Get, "users/suggestions/{slug}/members", "slug", InternalUtils.ExpressionsToDictionary(parameters), CancellationToken.None);
        }

        /// <summary>
        /// <para>Access the users in a given category of the Twitter suggested user list and return their most recent status if they are not a protected user as an asynchronous operation.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> slug (required)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the category.</para>
        /// </returns>
        public Task<ListedResponse<User>> SuggestedMembersAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessParameterReservedApiArrayAsync<User>(MethodType.Get, "users/suggestions/{slug}/members", "slug", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Access the users in a given category of the Twitter suggested user list and return their most recent status if they are not a protected user as an asynchronous operation.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> slug (required)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the category.</para>
        /// </returns>
        public Task<ListedResponse<User>> SuggestedMembersAsync<T>(T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessParameterReservedApiArrayAsync<User>(MethodType.Get, "users/suggestions/{slug}/members", "slug", InternalUtils.ResolveObject(parameters), cancellationToken);
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
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the user.</para>
        /// </returns>
        public Task<UserResponse> ReportSpamAsync(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiAsync<UserResponse>(MethodType.Post, "users/report_spam", parameters);
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
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the user.</para>
        /// </returns>
        public Task<UserResponse> ReportSpamAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<UserResponse>(MethodType.Post, "users/report_spam", parameters, cancellationToken);
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
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the user.</para>
        /// </returns>
        public Task<UserResponse> ReportSpam<T>(T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<UserResponse, T>(MethodType.Post, "users/report_spam", parameters, cancellationToken);
        }
    }
}
#endif
