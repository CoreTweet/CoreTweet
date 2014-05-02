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
using System.Threading;
using System.Threading.Tasks;
using CoreTweet.Core;

namespace CoreTweet.Rest
{
    partial class Users
    {
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
        public Task<IEnumerable<User>> ContributeesAsync(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiArrayAsync<User>(MethodType.Get, "users/contributees", parameters);
        }
        public Task<IEnumerable<User>> ContributeesAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiArrayAsync<User>(MethodType.Get, "users/contributees", parameters, cancellationToken);
        }
        public Task<IEnumerable<User>> ContributeesAsync<T>(T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiArrayAsync<User, T>(MethodType.Get, "users/contributees", parameters, cancellationToken);
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
        public Task<IEnumerable<User>> ContributorsAsync(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiArrayAsync<User>(MethodType.Get, "users/contributors", parameters);
        }
        public Task<IEnumerable<User>> ContributorsAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiArrayAsync<User>(MethodType.Get, "users/contributors", parameters, cancellationToken);
        }
        public Task<IEnumerable<User>> ContributorsAsync<T>(T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiArrayAsync<User, T>(MethodType.Get, "users/contributors", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Returns fully-hydrated user objects for up to 100 users per request, as specified by comma-separated values passed to the user_id and/or screen_name parameters.</para>
        /// <para>This method is especially useful when used in conjunction with collections of user IDs returned from GET friends/ids and GET followers/ids.</para>
        /// <para>GET users/show is used to retrieve a single user object.</para>
        /// <para>Avaliable parameters: </para>
        /// <para><paramref name="string, IEnumerable<long> user_id (optional)"/> : A list of user IDs or comma separated string of ones, up to 100 are allowed in a single request. You are strongly encouraged to use a POST for larger requests.</para>
        /// <para><paramref name="string, IEnumerable<string> screen_name (optional)"/> : A list of screen names or comma separated string of ones, up to 100 are allowed in a single request. You are strongly encouraged to use a POST for larger (up to 100 screen names) requests.</para>
        /// <para><paramref name="bool include_entities (optional)"/> : The entities node that may appear within embedded statuses will be disincluded when set to false.</para>
        /// </summary>
        /// <returns>Users.</returns>
        /// <param name='parameters'>
        /// Parameters.
        /// </param>
        public Task<IEnumerable<User>> LookupAsync(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiArrayAsync<User>(MethodType.Get, "users/lookup", parameters);
        }
        public Task<IEnumerable<User>> LookupAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiArrayAsync<User>(MethodType.Get, "users/lookup", parameters, cancellationToken);
        }
        public Task<IEnumerable<User>> LookupAsync<T>(T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiArrayAsync<User, T>(MethodType.Get, "users/lookup", parameters, cancellationToken);
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
        public Task<ProfileBannerSizes> ProfileBannerAsync(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiAsync<ProfileBannerSizes>(MethodType.Get, "users/profile_banner", parameters, "sizes");
        }
        public Task<ProfileBannerSizes> ProfileBannerAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<ProfileBannerSizes>(MethodType.Get, "users/profile_banner", parameters, cancellationToken, "sizes");
        }
        public Task<ProfileBannerSizes> ProfileBannerAsync<T>(T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<ProfileBannerSizes, T>(MethodType.Get, "users/profile_banner", parameters, cancellationToken, "sizes");
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
        public Task<IEnumerable<User>> SearchAsync(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiArrayAsync<User>(MethodType.Get, "users/search", parameters);
        }
        public Task<IEnumerable<User>> SearchAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiArrayAsync<User>(MethodType.Get, "users/search", parameters, cancellationToken);
        }
        public Task<IEnumerable<User>> SearchAsync<T>(T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiArrayAsync<User, T>(MethodType.Get, "users/search", parameters, cancellationToken);
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
        public Task<User> ShowAsync(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiAsync<User>(MethodType.Get, "users/show", parameters);
        }
        public Task<User> ShowAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<User>(MethodType.Get, "users/show", parameters, cancellationToken);
        }
        public Task<User> ShowAsync<T>(T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<User, T>(MethodType.Get, "users/show", parameters, cancellationToken);
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
        public Task<IEnumerable<Category>> SuggestionsAsync(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiArrayAsync<Category>(MethodType.Get, "users/suggestions", parameters);
        }
        public Task<IEnumerable<Category>> SuggestionsAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiArrayAsync<Category>(MethodType.Get, "users/suggestions", parameters, cancellationToken);
        }
        public Task<IEnumerable<Category>> SuggestionsAsync<T>(T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiArrayAsync<Category, T>(MethodType.Get, "users/suggestions", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Access the users in a given category of the Twitter suggested user list.</para>
        /// <para>It is recommended that applications cache this data for no more than one hour.</para>
        /// <para><paramref name="string slug (required)"/> : The short name of list or a category</para>
        /// <para><paramref name="string lang (optional)"/> : Restricts the suggested categories to the requested language. The language must be specified by the appropriate two letter ISO 639-1 representation. Currently supported languages are provided by the GET help/languages API request. Unsupported language codes will receive English (en) results. If you use lang in this request, ensure you also include it when requesting the GET users/suggestions/:slug list.</para>
        /// </summary>
        /// <returns>The category.</returns>
        /// <param name="parameters">
        /// Parameters.
        /// </param>
        public Task<Category> SuggestionAsync(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessParameterReservedApiAsync<Category>(MethodType.Get, "users/suggestions/{slug}", "slug", InternalUtils.ExpressionsToDictionary(parameters), CancellationToken.None);
        }
        public Task<Category> SuggestionAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessParameterReservedApiAsync<Category>(MethodType.Get, "users/suggestions/{slug}", "slug", parameters, cancellationToken);
        }
        public Task<Category> SuggestionAsync<T>(T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessParameterReservedApiAsync<Category>(MethodType.Get, "users/suggestions/{slug}", "slug", InternalUtils.ResolveObject(parameters), cancellationToken);
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
        public Task<IEnumerable<User>> SuggestedMembersAsync(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessParameterReservedApiArrayAsync<User>(MethodType.Get, "users/suggestions/{slug}/members", "slug", InternalUtils.ExpressionsToDictionary(parameters), CancellationToken.None);
        }
        public Task<IEnumerable<User>> SuggestedMembersAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessParameterReservedApiArrayAsync<User>(MethodType.Get, "users/suggestions/{slug}/members", "slug", parameters, cancellationToken);
        }
        public Task<IEnumerable<User>> SuggestedMembersAsync<T>(T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessParameterReservedApiArrayAsync<User>(MethodType.Get, "users/suggestions/{slug}/members", "slug", InternalUtils.ResolveObject(parameters), cancellationToken);
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
        public Task<User> ReportSpamAsync(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiAsync<User>(MethodType.Post, "users/report_spam", parameters);
        }
        public Task<User> ReportSpamAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<User>(MethodType.Post, "users/report_spam", parameters, cancellationToken);
        }
        public Task<User> ReportSpam<T>(T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<User, T>(MethodType.Post, "users/report_spam", parameters, cancellationToken);
        }
    }
}
