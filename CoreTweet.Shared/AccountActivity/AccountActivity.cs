// The MIT License (MIT)
//
// CoreTweet - A .NET Twitter Library supporting Twitter API 1.1
// Copyright (c) 2013-2018 CoreTweet Development Team
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
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using CoreTweet.Core;

#if ASYNC
using System.Threading;
using System.Threading.Tasks;
#endif

namespace CoreTweet.AccountActivity
{
    public partial class AccountActivityEnvironment : ApiProviderBase
    {
        /// <summary>
        /// Gets or sets the the name of a dev environment.
        /// </summary>
        public string EnvName { get; set; }

        public AccountActivityEnvironment(TokensBase e, string env_name) : base(e) { EnvName = env_name; }

#if SYNC
        private Webhook PostWebhooksImpl(IEnumerable<KeyValuePair<string, object>> parameters, string urlPrefix, string urlSuffix)
        {
            var param = parameters.ToArray();
            return this.Tokens.AccessApiImpl<Webhook>(MethodType.Post, $"account_activity/all/{EnvName}/webhooks", param, "", urlPrefix, urlSuffix);
        }

        private ListedResponse<Webhook> GetWebhooksImpl(IEnumerable<KeyValuePair<string, object>> parameters, string urlPrefix, string urlSuffix)
        {
            var param = parameters.ToArray();
            return this.Tokens.AccessApiArrayImpl<Webhook>(MethodType.Get, $"account_activity/all/{EnvName}/webhooks", param, "", urlPrefix, urlSuffix);
        }

        private void PutWebhooksImpl(IEnumerable<KeyValuePair<string, object>> parameters, string urlPrefix, string urlSuffix)
        {
            var id = parameters.Single(x => x.Key == "webhook_id").Value;
            this.Tokens.AccessApiNoResponseImpl(MethodType.Put, $"account_activity/all/{EnvName}/webhooks/{id}", Enumerable.Empty<KeyValuePair<string, object>>(), urlPrefix, urlSuffix);
        }

        private void DeleteWebhooksImpl(IEnumerable<KeyValuePair<string, object>> parameters, string urlPrefix, string urlSuffix)
        {
            var id = parameters.Single(x => x.Key == "webhook_id").Value;
            this.Tokens.AccessApiNoResponseImpl(MethodType.Delete, $"account_activity/all/{EnvName}/webhooks/{id}", Enumerable.Empty<KeyValuePair<string, object>>(), urlPrefix, urlSuffix);
        }

        private void PostSubscriptionsImpl(IEnumerable<KeyValuePair<string, object>> parameters, string urlPrefix, string urlSuffix)
        {
            var param = parameters.ToArray();
            this.Tokens.AccessApiNoResponseImpl(MethodType.Post, $"account_activity/all/{EnvName}/subscriptions", param, urlPrefix, urlSuffix);
        }

        private void GetSubscriptionsImpl(IEnumerable<KeyValuePair<string, object>> parameters, string urlPrefix, string urlSuffix)
        {
            var param = parameters.ToArray();
            this.Tokens.AccessApiNoResponseImpl(MethodType.Get, $"account_activity/all/{EnvName}/subscriptions", param, urlPrefix, urlSuffix);
        }

        private void DeleteSubscriptionsImpl(IEnumerable<KeyValuePair<string, object>> parameters, string urlPrefix, string urlSuffix)
        {
            var param = parameters.ToArray();
            this.Tokens.AccessApiNoResponseImpl(MethodType.Delete, $"account_activity/all/{EnvName}/subscriptions", param, urlPrefix, urlSuffix);
        }

        private SubscriptionsList SubscriptionsListImpl(IEnumerable<KeyValuePair<string, object>> parameters, string urlPrefix, string urlSuffix)
        {
            var param = parameters.ToArray();
            return this.Tokens.AccessApiImpl<SubscriptionsList>(MethodType.Get, $"account_activity/all/{EnvName}/subscriptions/list", param, "", urlPrefix, urlSuffix);
        }
#endif
#if ASYNC
        private Task<Webhook> PostWebhooksAsyncImpl(IEnumerable<KeyValuePair<string, object>> parameters, CancellationToken cancellationToken, string urlPrefix, string urlSuffix)
        {
            var param = parameters.ToArray();
            return this.Tokens.AccessApiAsyncImpl<Webhook>(MethodType.Post, $"account_activity/all/{EnvName}/webhooks", param, cancellationToken, "", urlPrefix, urlSuffix);
        }

        private Task<ListedResponse<Webhook>> GetWebhooksAsyncImpl(IEnumerable<KeyValuePair<string, object>> parameters, CancellationToken cancellationToken, string urlPrefix, string urlSuffix)
        {
            var param = parameters.ToArray();
            return this.Tokens.AccessApiArrayAsyncImpl<Webhook>(MethodType.Get, $"account_activity/all/{EnvName}/webhooks", param, cancellationToken, "", urlPrefix, urlSuffix);
        }

        private Task PutWebhooksAsyncImpl(IEnumerable<KeyValuePair<string, object>> parameters, CancellationToken cancellationToken, string urlPrefix, string urlSuffix)
        {
            var id = parameters.Single(x => x.Key == "webhook_id").Value;
            return this.Tokens.AccessApiNoResponseAsyncImpl(MethodType.Put, $"account_activity/all/{EnvName}/webhooks/{id}", Enumerable.Empty<KeyValuePair<string, object>>(), cancellationToken, urlPrefix, urlSuffix);
        }

        private Task DeleteWebhooksAsyncImpl(IEnumerable<KeyValuePair<string, object>> parameters, CancellationToken cancellationToken, string urlPrefix, string urlSuffix)
        {
            var id = parameters.Single(x => x.Key == "webhook_id").Value;
            return this.Tokens.AccessApiNoResponseAsyncImpl(MethodType.Delete, $"account_activity/all/{EnvName}/webhooks/{id}", Enumerable.Empty<KeyValuePair<string, object>>(), cancellationToken, urlPrefix, urlSuffix);
        }

        private Task PostSubscriptionsAsyncImpl(IEnumerable<KeyValuePair<string, object>> parameters, CancellationToken cancellationToken, string urlPrefix, string urlSuffix)
        {
            var param = parameters.ToArray();
            return this.Tokens.AccessApiNoResponseAsyncImpl(MethodType.Post, $"account_activity/all/{EnvName}/subscriptions", param, cancellationToken, urlPrefix, urlSuffix);
        }

        private Task GetSubscriptionsAsyncImpl(IEnumerable<KeyValuePair<string, object>> parameters, CancellationToken cancellationToken, string urlPrefix, string urlSuffix)
        {
            var param = parameters.ToArray();
            return this.Tokens.AccessApiNoResponseAsyncImpl(MethodType.Get, $"account_activity/all/{EnvName}/subscriptions", param, cancellationToken, urlPrefix, urlSuffix);
        }

        private Task DeleteSubscriptionsAsyncImpl(IEnumerable<KeyValuePair<string, object>> parameters, CancellationToken cancellationToken, string urlPrefix, string urlSuffix)
        {
            var param = parameters.ToArray();
            return this.Tokens.AccessApiNoResponseAsyncImpl(MethodType.Delete, $"account_activity/all/{EnvName}/subscriptions", param, cancellationToken, urlPrefix, urlSuffix);
        }

        private Task<SubscriptionsList> SubscriptionsListAsyncImpl(IEnumerable<KeyValuePair<string, object>> parameters, CancellationToken cancellationToken, string urlPrefix, string urlSuffix)
        {
            var param = parameters.ToArray();
            return this.Tokens.AccessApiAsyncImpl<SubscriptionsList>(MethodType.Get, $"account_activity/all/{EnvName}/subscriptions/list", param, cancellationToken, "", urlPrefix, urlSuffix);
        }
#endif
    }

    public partial class AccountActivityPremiumApi : ApiProviderBase
    {
        public AccountActivityEnvironment Env(string env_name) => new AccountActivityEnvironment(this.IncludedTokens, env_name);
    }

    /// <summary>
    /// Provides a set of methods for the wrapper of GET/POST account_activity.
    /// </summary>
    public partial class AccountActivityApi : ApiProviderBase
    {
        internal AccountActivityApi(TokensBase e) : base(e) { }

        public AccountActivityPremiumApi Premium => new AccountActivityPremiumApi(IncludedTokens);

        public AccountActivityEnterpriseApi Enterprise => new AccountActivityEnterpriseApi(IncludedTokens);
    }
}
