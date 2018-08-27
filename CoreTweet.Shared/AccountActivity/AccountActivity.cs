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
    /// <summary>
    /// Provides a set of methods for the wrapper of GET/POST account_activity.
    /// </summary>
    public class AccountActivityApi : ApiProviderBase
    {
        internal AccountActivityApi(TokensBase e) : base(e) { }

#if SYNC
        /// <summary>
        /// <para>Registers a webhook URL for all event types.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> env_name (required)</para>
        /// <para>- <c>string</c> url (required)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The webhook URL object.</returns>
        public Webhook Register(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessParameterReservedApi<Webhook>(MethodType.Post, "account_activity/all/{env_name}/webhooks", new [] { "env_name" }, InternalUtils.ExpressionsToDictionary(parameters));
        }

        /// <summary>
        /// <para>Registers a webhook URL for all event types.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> env_name (required)</para>
        /// <para>- <c>string</c> url (required)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The webhook URL object.</returns>
        public Webhook Register(IDictionary<string, object> parameters)
        {
            return this.Tokens.AccessParameterReservedApi<Webhook>(MethodType.Post, "account_activity/all/{env_name}/webhooks", new [] { "env_name" }, parameters);
        }

        /// <summary>
        /// <para>Registers a webhook URL for all event types.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> env_name (required)</para>
        /// <para>- <c>string</c> url (required)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The webhook URL object.</returns>
        public Webhook Register(object parameters)
        {
            return this.Tokens.AccessParameterReservedApi<Webhook>(MethodType.Post, "account_activity/all/{env_name}/webhooks", new [] { "env_name" }, InternalUtils.ResolveObject(parameters));
        }

        /// <summary>
        /// <para>Registers a webhook URL for all event types.</para>
        /// </summary>
        /// <param name="env_name">required.</param>
        /// <param name="url">required.</param>
        /// <returns>The webhook URL object.</returns>
        public Webhook Register(string env_name, string url)
        {
            var parameters = new Dictionary<string, object>();
            if(env_name == null) throw new ArgumentNullException("env_name");
            parameters.Add("env_name", env_name);
            if(url == null) throw new ArgumentNullException("url");
            parameters.Add("url", url);
            return this.Tokens.AccessParameterReservedApi<Webhook>(MethodType.Post, "account_activity/all/{env_name}/webhooks", new [] { "env_name" }, parameters);
        }

#endif
#if ASYNC

        /// <summary>
        /// <para>Registers a webhook URL for all event types.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> env_name (required)</para>
        /// <para>- <c>string</c> url (required)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The webhook URL object.</returns>
        public Task<Webhook> RegisterAsync(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessParameterReservedApiAsync<Webhook>(MethodType.Post, "account_activity/all/{env_name}/webhooks", new[] { "env_name" }, InternalUtils.ExpressionsToDictionary(parameters), CancellationToken.None);
        }

        /// <summary>
        /// <para>Registers a webhook URL for all event types.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> env_name (required)</para>
        /// <para>- <c>string</c> url (required)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The webhook URL object.</returns>
        public Task<Webhook> RegisterAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessParameterReservedApiAsync<Webhook>(MethodType.Post, "account_activity/all/{env_name}/webhooks", new[] { "env_name" }, parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Registers a webhook URL for all event types.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> env_name (required)</para>
        /// <para>- <c>string</c> url (required)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The webhook URL object.</returns>
        public Task<Webhook> RegisterAsync(object parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessParameterReservedApiAsync<Webhook>(MethodType.Post, "account_activity/all/{env_name}/webhooks", new[] { "env_name" }, InternalUtils.ResolveObject(parameters), cancellationToken);
        }

        /// <summary>
        /// <para>Registers a webhook URL for all event types.</para>
        /// </summary>
        /// <param name="env_name">required.</param>
        /// <param name="url">required.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The webhook URL object.</returns>
        public Task<Webhook> RegisterAsync(string env_name, string url, CancellationToken cancellationToken = default(CancellationToken))
        {
            var parameters = new Dictionary<string, object>();
            if (env_name == null) throw new ArgumentNullException("env_name");
            parameters.Add("env_name", env_name);
            if (url == null) throw new ArgumentNullException("url");
            parameters.Add("url", url);
            return this.Tokens.AccessParameterReservedApiAsync<Webhook>(MethodType.Post, "account_activity/all/{env_name}/webhooks", new[] { "env_name" }, parameters, cancellationToken);
        }

#endif

#if SYNC
        /// <summary>
        /// <para>Returns all environments, webhook URLs and their statuses for the authenticating app.</para>
        /// <para>Available parameters: Nothing.</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The list of environments object.</returns>
        public EnvironmentsResponse ListEnvironments(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApi<EnvironmentsResponse>(MethodType.Get, "account_activity/all/webhooks", parameters);
        }

        /// <summary>
        /// <para>Returns all environments, webhook URLs and their statuses for the authenticating app.</para>
        /// <para>Available parameters: Nothing.</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The list of environments object.</returns>
        public EnvironmentsResponse ListEnvironments(IDictionary<string, object> parameters)
        {
            return this.Tokens.AccessApi<EnvironmentsResponse>(MethodType.Get, "account_activity/all/webhooks", parameters);
        }

        /// <summary>
        /// <para>Returns all environments, webhook URLs and their statuses for the authenticating app.</para>
        /// <para>Available parameters: Nothing.</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The list of environments object.</returns>
        public EnvironmentsResponse ListEnvironments(object parameters)
        {
            return this.Tokens.AccessApi<EnvironmentsResponse>(MethodType.Get, "account_activity/all/webhooks", parameters);
        }

        /// <summary>
        /// <para>Returns all environments, webhook URLs and their statuses for the authenticating app.</para>
        /// </summary>
        /// <returns>The list of environments object.</returns>
        public EnvironmentsResponse ListEnvironments()
        {
            var parameters = new Dictionary<string, object>();
            return this.Tokens.AccessApi<EnvironmentsResponse>(MethodType.Get, "account_activity/all/webhooks", parameters);
        }

#endif
#if ASYNC

        /// <summary>
        /// <para>Returns all environments, webhook URLs and their statuses for the authenticating app.</para>
        /// <para>Available parameters:</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The list of environments object.</returns>
        public Task<EnvironmentsResponse> ListEnvironmentsAsync(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiAsync<EnvironmentsResponse>(MethodType.Get, "account_activity/all/webhooks", parameters);
        }

        /// <summary>
        /// <para>Returns all environments, webhook URLs and their statuses for the authenticating app.</para>
        /// <para>Available parameters:</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The list of environments object.</returns>
        public Task<EnvironmentsResponse> ListEnvironmentsAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<EnvironmentsResponse>(MethodType.Get, "account_activity/all/webhooks", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Returns all environments, webhook URLs and their statuses for the authenticating app.</para>
        /// <para>Available parameters:</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The list of environments object.</returns>
        public Task<EnvironmentsResponse> ListEnvironmentsAsync(object parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<EnvironmentsResponse>(MethodType.Get, "account_activity/all/webhooks", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Returns all environments, webhook URLs and their statuses for the authenticating app.</para>
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The list of environments object.</returns>
        public Task<EnvironmentsResponse> ListEnvironmentsAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var parameters = new Dictionary<string, object>();
            return this.Tokens.AccessApiAsync<EnvironmentsResponse>(MethodType.Get, "account_activity/all/webhooks", parameters, cancellationToken);
        }

#endif

#if SYNC
        /// <summary>
        /// <para>Returns all webhook URLs and their statuses for the environment.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> env_name (required)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The list of environments object.</returns>
        public ListedResponse<Webhook> ListWebhooks(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessParameterReservedApiArray<Webhook>(MethodType.Get, "account_activity/all/{env_name}/webhooks", new [] { "env_name" }, InternalUtils.ExpressionsToDictionary(parameters));
        }

        /// <summary>
        /// <para>Returns all webhook URLs and their statuses for the environment.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> env_name (required)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The list of environments object.</returns>
        public ListedResponse<Webhook> ListWebhooks(IDictionary<string, object> parameters)
        {
            return this.Tokens.AccessParameterReservedApiArray<Webhook>(MethodType.Get, "account_activity/all/{env_name}/webhooks", new [] { "env_name" }, parameters);
        }

        /// <summary>
        /// <para>Returns all webhook URLs and their statuses for the environment.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> env_name (required)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The list of environments object.</returns>
        public ListedResponse<Webhook> ListWebhooks(object parameters)
        {
            return this.Tokens.AccessParameterReservedApiArray<Webhook>(MethodType.Get, "account_activity/all/{env_name}/webhooks", new [] { "env_name" }, InternalUtils.ResolveObject(parameters));
        }

        /// <summary>
        /// <para>Returns all webhook URLs and their statuses for the environment.</para>
        /// </summary>
        /// <param name="env_name">required.</param>
        /// <returns>The list of environments object.</returns>
        public ListedResponse<Webhook> ListWebhooks(string env_name)
        {
            var parameters = new Dictionary<string, object>();
            if(env_name == null) throw new ArgumentNullException("env_name");
            parameters.Add("env_name", env_name);
            return this.Tokens.AccessParameterReservedApiArray<Webhook>(MethodType.Get, "account_activity/all/{env_name}/webhooks", new [] { "env_name" }, parameters);
        }

#endif
#if ASYNC

        /// <summary>
        /// <para>Returns all webhook URLs and their statuses for the environment.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> env_name (required)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The list of environments object.</returns>
        public Task<ListedResponse<Webhook>> ListWebhooksAsync(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessParameterReservedApiArrayAsync<Webhook>(MethodType.Get, "account_activity/all/{env_name}/webhooks", new[] { "env_name" }, InternalUtils.ExpressionsToDictionary(parameters), CancellationToken.None);
        }

        /// <summary>
        /// <para>Returns all webhook URLs and their statuses for the environment.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> env_name (required)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The list of environments object.</returns>
        public Task<ListedResponse<Webhook>> ListWebhooksAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessParameterReservedApiArrayAsync<Webhook>(MethodType.Get, "account_activity/all/{env_name}/webhooks", new[] { "env_name" }, parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Returns all webhook URLs and their statuses for the environment.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> env_name (required)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The list of environments object.</returns>
        public Task<ListedResponse<Webhook>> ListWebhooksAsync(object parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessParameterReservedApiArrayAsync<Webhook>(MethodType.Get, "account_activity/all/{env_name}/webhooks", new[] { "env_name" }, InternalUtils.ResolveObject(parameters), cancellationToken);
        }

        /// <summary>
        /// <para>Returns all webhook URLs and their statuses for the environment.</para>
        /// </summary>
        /// <param name="env_name">required.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The list of environments object.</returns>
        public Task<ListedResponse<Webhook>> ListWebhooksAsync(string env_name, CancellationToken cancellationToken = default(CancellationToken))
        {
            var parameters = new Dictionary<string, object>();
            if (env_name == null) throw new ArgumentNullException("env_name");
            parameters.Add("env_name", env_name);
            return this.Tokens.AccessParameterReservedApiArrayAsync<Webhook>(MethodType.Get, "account_activity/all/{env_name}/webhooks", new[] { "env_name" }, parameters, cancellationToken);
        }

#endif

#if SYNC
        /// <summary>
        /// <para>Triggers the challenge response check (CRC) for the given enviroments webhook for all activites.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> env_name (required)</para>
        /// <para>- <c>string</c> webhook_id (required)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public void Trigger(params Expression<Func<string, object>>[] parameters)
        {
            this.Tokens.AccessParameterReservedApiNoResponse(MethodType.Put, "account_activity/all/{env_name}/webhooks/{webhook_id}", new [] { "env_name", "webhook_id" }, InternalUtils.ExpressionsToDictionary(parameters));
        }

        /// <summary>
        /// <para>Triggers the challenge response check (CRC) for the given enviroments webhook for all activites.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> env_name (required)</para>
        /// <para>- <c>string</c> webhook_id (required)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public void Trigger(IDictionary<string, object> parameters)
        {
            this.Tokens.AccessParameterReservedApiNoResponse(MethodType.Put, "account_activity/all/{env_name}/webhooks/{webhook_id}", new [] { "env_name", "webhook_id" }, parameters);
        }

        /// <summary>
        /// <para>Triggers the challenge response check (CRC) for the given enviroments webhook for all activites.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> env_name (required)</para>
        /// <para>- <c>string</c> webhook_id (required)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public void Trigger(object parameters)
        {
            this.Tokens.AccessParameterReservedApiNoResponse(MethodType.Put, "account_activity/all/{env_name}/webhooks/{webhook_id}", new [] { "env_name", "webhook_id" }, InternalUtils.ResolveObject(parameters));
        }

        /// <summary>
        /// <para>Triggers the challenge response check (CRC) for the given enviroments webhook for all activites.</para>
        /// </summary>
        /// <param name="env_name">required.</param>
        /// <param name="webhook_id">required.</param>
        /// <returns></returns>
        public void Trigger(string env_name, string webhook_id)
        {
            var parameters = new Dictionary<string, object>();
            if(env_name == null) throw new ArgumentNullException("env_name");
            parameters.Add("env_name", env_name);
            if(webhook_id == null) throw new ArgumentNullException("webhook_id");
            parameters.Add("webhook_id", webhook_id);
            this.Tokens.AccessParameterReservedApiNoResponse(MethodType.Put, "account_activity/all/{env_name}/webhooks/{webhook_id}", new [] { "env_name", "webhook_id" }, parameters);
        }

#endif
#if ASYNC

        /// <summary>
        /// <para>Triggers the challenge response check (CRC) for the given enviroments webhook for all activites.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> env_name (required)</para>
        /// <para>- <c>string</c> webhook_id (required)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public Task TriggerAsync(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessParameterReservedApiNoResponseAsync(MethodType.Put, "account_activity/all/{env_name}/webhooks/{webhook_id}", new[] { "env_name", "webhook_id" }, InternalUtils.ExpressionsToDictionary(parameters), CancellationToken.None);
        }

        /// <summary>
        /// <para>Triggers the challenge response check (CRC) for the given enviroments webhook for all activites.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> env_name (required)</para>
        /// <para>- <c>string</c> webhook_id (required)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public Task TriggerAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessParameterReservedApiNoResponseAsync(MethodType.Put, "account_activity/all/{env_name}/webhooks/{webhook_id}", new[] { "env_name", "webhook_id" }, parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Triggers the challenge response check (CRC) for the given enviroments webhook for all activites.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> env_name (required)</para>
        /// <para>- <c>string</c> webhook_id (required)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public Task TriggerAsync(object parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessParameterReservedApiNoResponseAsync(MethodType.Put, "account_activity/all/{env_name}/webhooks/{webhook_id}", new[] { "env_name", "webhook_id" }, InternalUtils.ResolveObject(parameters), cancellationToken);
        }

        /// <summary>
        /// <para>Triggers the challenge response check (CRC) for the given enviroments webhook for all activites.</para>
        /// </summary>
        /// <param name="env_name">required.</param>
        /// <param name="webhook_id">required.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public Task TriggerAsync(string env_name, string webhook_id, CancellationToken cancellationToken = default(CancellationToken))
        {
            var parameters = new Dictionary<string, object>();
            if (env_name == null) throw new ArgumentNullException("env_name");
            parameters.Add("env_name", env_name);
            if (webhook_id == null) throw new ArgumentNullException("webhook_id");
            parameters.Add("webhook_id", webhook_id);
            return this.Tokens.AccessParameterReservedApiNoResponseAsync(MethodType.Put, "account_activity/all/{env_name}/webhooks/{webhook_id}", new[] { "env_name", "webhook_id" }, parameters, cancellationToken);
        }

#endif

#if SYNC
        /// <summary>
        /// <para>Subscribes the provided application to all events for the provided environment for all message types.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> env_name (required)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public void Subscribe(params Expression<Func<string, object>>[] parameters)
        {
            this.Tokens.AccessParameterReservedApiNoResponse(MethodType.Post, "account_activity/all/{env_name}/subscriptions", new [] { "env_name" }, InternalUtils.ExpressionsToDictionary(parameters));
        }

        /// <summary>
        /// <para>Subscribes the provided application to all events for the provided environment for all message types.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> env_name (required)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public void Subscribe(IDictionary<string, object> parameters)
        {
            this.Tokens.AccessParameterReservedApiNoResponse(MethodType.Post, "account_activity/all/{env_name}/subscriptions", new [] { "env_name" }, parameters);
        }

        /// <summary>
        /// <para>Subscribes the provided application to all events for the provided environment for all message types.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> env_name (required)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public void Subscribe(object parameters)
        {
            this.Tokens.AccessParameterReservedApiNoResponse(MethodType.Post, "account_activity/all/{env_name}/subscriptions", new [] { "env_name" }, InternalUtils.ResolveObject(parameters));
        }

        /// <summary>
        /// <para>Subscribes the provided application to all events for the provided environment for all message types.</para>
        /// </summary>
        /// <param name="env_name">required.</param>
        /// <returns></returns>
        public void Subscribe(string env_name)
        {
            var parameters = new Dictionary<string, object>();
            if(env_name == null) throw new ArgumentNullException("env_name");
            parameters.Add("env_name", env_name);
            this.Tokens.AccessParameterReservedApiNoResponse(MethodType.Post, "account_activity/all/{env_name}/subscriptions", new [] { "env_name" }, parameters);
        }

#endif
#if ASYNC

        /// <summary>
        /// <para>Subscribes the provided application to all events for the provided environment for all message types.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> env_name (required)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public Task SubscribeAsync(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessParameterReservedApiNoResponseAsync(MethodType.Post, "account_activity/all/{env_name}/subscriptions", new[] { "env_name" }, InternalUtils.ExpressionsToDictionary(parameters), CancellationToken.None);
        }

        /// <summary>
        /// <para>Subscribes the provided application to all events for the provided environment for all message types.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> env_name (required)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public Task SubscribeAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessParameterReservedApiNoResponseAsync(MethodType.Post, "account_activity/all/{env_name}/subscriptions", new[] { "env_name" }, parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Subscribes the provided application to all events for the provided environment for all message types.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> env_name (required)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public Task SubscribeAsync(object parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessParameterReservedApiNoResponseAsync(MethodType.Post, "account_activity/all/{env_name}/subscriptions", new[] { "env_name" }, InternalUtils.ResolveObject(parameters), cancellationToken);
        }

        /// <summary>
        /// <para>Subscribes the provided application to all events for the provided environment for all message types.</para>
        /// </summary>
        /// <param name="env_name">required.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public Task SubscribeAsync(string env_name, CancellationToken cancellationToken = default(CancellationToken))
        {
            var parameters = new Dictionary<string, object>();
            if (env_name == null) throw new ArgumentNullException("env_name");
            parameters.Add("env_name", env_name);
            return this.Tokens.AccessParameterReservedApiNoResponseAsync(MethodType.Post, "account_activity/all/{env_name}/subscriptions", new[] { "env_name" }, parameters, cancellationToken);
        }

#endif

#if SYNC
        /// <summary>
        /// <para>Returns the count of subscriptions that are currently active on your account for all activities.</para>
        /// <para>Available parameters: Nothing.</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public SubscriptionsCountResponse Count(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApi<SubscriptionsCountResponse>(MethodType.Get, "account_activity/subscriptions/count", parameters);
        }

        /// <summary>
        /// <para>Returns the count of subscriptions that are currently active on your account for all activities.</para>
        /// <para>Available parameters: Nothing.</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public SubscriptionsCountResponse Count(IDictionary<string, object> parameters)
        {
            return this.Tokens.AccessApi<SubscriptionsCountResponse>(MethodType.Get, "account_activity/subscriptions/count", parameters);
        }

        /// <summary>
        /// <para>Returns the count of subscriptions that are currently active on your account for all activities.</para>
        /// <para>Available parameters: Nothing.</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public SubscriptionsCountResponse Count(object parameters)
        {
            return this.Tokens.AccessApi<SubscriptionsCountResponse>(MethodType.Get, "account_activity/subscriptions/count", parameters);
        }

        /// <summary>
        /// <para>Returns the count of subscriptions that are currently active on your account for all activities.</para>
        /// </summary>
        /// <returns></returns>
        public SubscriptionsCountResponse Count()
        {
            var parameters = new Dictionary<string, object>();
            return this.Tokens.AccessApi<SubscriptionsCountResponse>(MethodType.Get, "account_activity/subscriptions/count", parameters);
        }

#endif
#if ASYNC

        /// <summary>
        /// <para>Returns the count of subscriptions that are currently active on your account for all activities.</para>
        /// <para>Available parameters:</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public Task<SubscriptionsCountResponse> CountAsync(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessApiAsync<SubscriptionsCountResponse>(MethodType.Get, "account_activity/subscriptions/count", parameters);
        }

        /// <summary>
        /// <para>Returns the count of subscriptions that are currently active on your account for all activities.</para>
        /// <para>Available parameters:</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public Task<SubscriptionsCountResponse> CountAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<SubscriptionsCountResponse>(MethodType.Get, "account_activity/subscriptions/count", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Returns the count of subscriptions that are currently active on your account for all activities.</para>
        /// <para>Available parameters:</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public Task<SubscriptionsCountResponse> CountAsync(object parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessApiAsync<SubscriptionsCountResponse>(MethodType.Get, "account_activity/subscriptions/count", parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Returns the count of subscriptions that are currently active on your account for all activities.</para>
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public Task<SubscriptionsCountResponse> CountAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var parameters = new Dictionary<string, object>();
            return this.Tokens.AccessApiAsync<SubscriptionsCountResponse>(MethodType.Get, "account_activity/subscriptions/count", parameters, cancellationToken);
        }

#endif

#if SYNC
        /// <summary>
        /// <para>Provides a way to determine if a webhook configuration is subscribed to the provided user’s events.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> env_name (required)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public void Determine(params Expression<Func<string, object>>[] parameters)
        {
            this.Tokens.AccessParameterReservedApiNoResponse(MethodType.Get, "account_activity/all/{env_name}/subscriptions", new [] { "env_name" }, InternalUtils.ExpressionsToDictionary(parameters));
        }

        /// <summary>
        /// <para>Provides a way to determine if a webhook configuration is subscribed to the provided user’s events.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> env_name (required)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public void Determine(IDictionary<string, object> parameters)
        {
            this.Tokens.AccessParameterReservedApiNoResponse(MethodType.Get, "account_activity/all/{env_name}/subscriptions", new [] { "env_name" }, parameters);
        }

        /// <summary>
        /// <para>Provides a way to determine if a webhook configuration is subscribed to the provided user’s events.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> env_name (required)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public void Determine(object parameters)
        {
            this.Tokens.AccessParameterReservedApiNoResponse(MethodType.Get, "account_activity/all/{env_name}/subscriptions", new [] { "env_name" }, InternalUtils.ResolveObject(parameters));
        }

        /// <summary>
        /// <para>Provides a way to determine if a webhook configuration is subscribed to the provided user’s events.</para>
        /// </summary>
        /// <param name="env_name">required.</param>
        /// <returns></returns>
        public void Determine(string env_name)
        {
            var parameters = new Dictionary<string, object>();
            if(env_name == null) throw new ArgumentNullException("env_name");
            parameters.Add("env_name", env_name);
            this.Tokens.AccessParameterReservedApiNoResponse(MethodType.Get, "account_activity/all/{env_name}/subscriptions", new [] { "env_name" }, parameters);
        }

#endif
#if ASYNC

        /// <summary>
        /// <para>Provides a way to determine if a webhook configuration is subscribed to the provided user’s events.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> env_name (required)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public Task DetermineAsync(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessParameterReservedApiNoResponseAsync(MethodType.Get, "account_activity/all/{env_name}/subscriptions", new[] { "env_name" }, InternalUtils.ExpressionsToDictionary(parameters), CancellationToken.None);
        }

        /// <summary>
        /// <para>Provides a way to determine if a webhook configuration is subscribed to the provided user’s events.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> env_name (required)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public Task DetermineAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessParameterReservedApiNoResponseAsync(MethodType.Get, "account_activity/all/{env_name}/subscriptions", new[] { "env_name" }, parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Provides a way to determine if a webhook configuration is subscribed to the provided user’s events.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> env_name (required)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public Task DetermineAsync(object parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessParameterReservedApiNoResponseAsync(MethodType.Get, "account_activity/all/{env_name}/subscriptions", new[] { "env_name" }, InternalUtils.ResolveObject(parameters), cancellationToken);
        }

        /// <summary>
        /// <para>Provides a way to determine if a webhook configuration is subscribed to the provided user’s events.</para>
        /// </summary>
        /// <param name="env_name">required.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public Task DetermineAsync(string env_name, CancellationToken cancellationToken = default(CancellationToken))
        {
            var parameters = new Dictionary<string, object>();
            if (env_name == null) throw new ArgumentNullException("env_name");
            parameters.Add("env_name", env_name);
            return this.Tokens.AccessParameterReservedApiNoResponseAsync(MethodType.Get, "account_activity/all/{env_name}/subscriptions", new[] { "env_name" }, parameters, cancellationToken);
        }

#endif

#if SYNC
        /// <summary>
        /// <para>Returns a list of the current All Activity type subscriptions.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> env_name (required)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The list of subscriptions object.</returns>
        public SubscriptionsListResponse ListSubscriptions(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessParameterReservedApi<SubscriptionsListResponse>(MethodType.Get, "account_activity/all/{env_name}/subscriptions/list", new [] { "env_name" }, InternalUtils.ExpressionsToDictionary(parameters));
        }

        /// <summary>
        /// <para>Returns a list of the current All Activity type subscriptions.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> env_name (required)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The list of subscriptions object.</returns>
        public SubscriptionsListResponse ListSubscriptions(IDictionary<string, object> parameters)
        {
            return this.Tokens.AccessParameterReservedApi<SubscriptionsListResponse>(MethodType.Get, "account_activity/all/{env_name}/subscriptions/list", new [] { "env_name" }, parameters);
        }

        /// <summary>
        /// <para>Returns a list of the current All Activity type subscriptions.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> env_name (required)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The list of subscriptions object.</returns>
        public SubscriptionsListResponse ListSubscriptions(object parameters)
        {
            return this.Tokens.AccessParameterReservedApi<SubscriptionsListResponse>(MethodType.Get, "account_activity/all/{env_name}/subscriptions/list", new [] { "env_name" }, InternalUtils.ResolveObject(parameters));
        }

        /// <summary>
        /// <para>Returns a list of the current All Activity type subscriptions.</para>
        /// </summary>
        /// <param name="env_name">required.</param>
        /// <returns>The list of subscriptions object.</returns>
        public SubscriptionsListResponse ListSubscriptions(string env_name)
        {
            var parameters = new Dictionary<string, object>();
            if(env_name == null) throw new ArgumentNullException("env_name");
            parameters.Add("env_name", env_name);
            return this.Tokens.AccessParameterReservedApi<SubscriptionsListResponse>(MethodType.Get, "account_activity/all/{env_name}/subscriptions/list", new [] { "env_name" }, parameters);
        }

#endif
#if ASYNC

        /// <summary>
        /// <para>Returns a list of the current All Activity type subscriptions.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> env_name (required)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The list of subscriptions object.</returns>
        public Task<SubscriptionsListResponse> ListSubscriptionsAsync(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessParameterReservedApiAsync<SubscriptionsListResponse>(MethodType.Get, "account_activity/all/{env_name}/subscriptions/list", new[] { "env_name" }, InternalUtils.ExpressionsToDictionary(parameters), CancellationToken.None);
        }

        /// <summary>
        /// <para>Returns a list of the current All Activity type subscriptions.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> env_name (required)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The list of subscriptions object.</returns>
        public Task<SubscriptionsListResponse> ListSubscriptionsAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessParameterReservedApiAsync<SubscriptionsListResponse>(MethodType.Get, "account_activity/all/{env_name}/subscriptions/list", new[] { "env_name" }, parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Returns a list of the current All Activity type subscriptions.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> env_name (required)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The list of subscriptions object.</returns>
        public Task<SubscriptionsListResponse> ListSubscriptionsAsync(object parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessParameterReservedApiAsync<SubscriptionsListResponse>(MethodType.Get, "account_activity/all/{env_name}/subscriptions/list", new[] { "env_name" }, InternalUtils.ResolveObject(parameters), cancellationToken);
        }

        /// <summary>
        /// <para>Returns a list of the current All Activity type subscriptions.</para>
        /// </summary>
        /// <param name="env_name">required.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The list of subscriptions object.</returns>
        public Task<SubscriptionsListResponse> ListSubscriptionsAsync(string env_name, CancellationToken cancellationToken = default(CancellationToken))
        {
            var parameters = new Dictionary<string, object>();
            if (env_name == null) throw new ArgumentNullException("env_name");
            parameters.Add("env_name", env_name);
            return this.Tokens.AccessParameterReservedApiAsync<SubscriptionsListResponse>(MethodType.Get, "account_activity/all/{env_name}/subscriptions/list", new[] { "env_name" }, parameters, cancellationToken);
        }

#endif

#if SYNC
        /// <summary>
        /// <para>Removes the webhook from the provided application’s all activities configuration.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> env_name (required)</para>
        /// <para>- <c>string</c> webhook_id (required)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public void Remove(params Expression<Func<string, object>>[] parameters)
        {
            this.Tokens.AccessParameterReservedApiNoResponse(MethodType.Delete, "account_activity/all/{env_name}/webhooks/{webhook_id}", new [] { "env_name", "webhook_id" }, InternalUtils.ExpressionsToDictionary(parameters));
        }

        /// <summary>
        /// <para>Removes the webhook from the provided application’s all activities configuration.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> env_name (required)</para>
        /// <para>- <c>string</c> webhook_id (required)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public void Remove(IDictionary<string, object> parameters)
        {
            this.Tokens.AccessParameterReservedApiNoResponse(MethodType.Delete, "account_activity/all/{env_name}/webhooks/{webhook_id}", new [] { "env_name", "webhook_id" }, parameters);
        }

        /// <summary>
        /// <para>Removes the webhook from the provided application’s all activities configuration.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> env_name (required)</para>
        /// <para>- <c>string</c> webhook_id (required)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public void Remove(object parameters)
        {
            this.Tokens.AccessParameterReservedApiNoResponse(MethodType.Delete, "account_activity/all/{env_name}/webhooks/{webhook_id}", new [] { "env_name", "webhook_id" }, InternalUtils.ResolveObject(parameters));
        }

        /// <summary>
        /// <para>Removes the webhook from the provided application’s all activities configuration.</para>
        /// </summary>
        /// <param name="env_name">required.</param>
        /// <param name="webhook_id">required.</param>
        /// <returns></returns>
        public void Remove(string env_name, string webhook_id)
        {
            var parameters = new Dictionary<string, object>();
            if(env_name == null) throw new ArgumentNullException("env_name");
            parameters.Add("env_name", env_name);
            if(webhook_id == null) throw new ArgumentNullException("webhook_id");
            parameters.Add("webhook_id", webhook_id);
            this.Tokens.AccessParameterReservedApiNoResponse(MethodType.Delete, "account_activity/all/{env_name}/webhooks/{webhook_id}", new [] { "env_name", "webhook_id" }, parameters);
        }

#endif
#if ASYNC

        /// <summary>
        /// <para>Removes the webhook from the provided application’s all activities configuration.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> env_name (required)</para>
        /// <para>- <c>string</c> webhook_id (required)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public Task RemoveAsync(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessParameterReservedApiNoResponseAsync(MethodType.Delete, "account_activity/all/{env_name}/webhooks/{webhook_id}", new[] { "env_name", "webhook_id" }, InternalUtils.ExpressionsToDictionary(parameters), CancellationToken.None);
        }

        /// <summary>
        /// <para>Removes the webhook from the provided application’s all activities configuration.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> env_name (required)</para>
        /// <para>- <c>string</c> webhook_id (required)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public Task RemoveAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessParameterReservedApiNoResponseAsync(MethodType.Delete, "account_activity/all/{env_name}/webhooks/{webhook_id}", new[] { "env_name", "webhook_id" }, parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Removes the webhook from the provided application’s all activities configuration.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> env_name (required)</para>
        /// <para>- <c>string</c> webhook_id (required)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public Task RemoveAsync(object parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessParameterReservedApiNoResponseAsync(MethodType.Delete, "account_activity/all/{env_name}/webhooks/{webhook_id}", new[] { "env_name", "webhook_id" }, InternalUtils.ResolveObject(parameters), cancellationToken);
        }

        /// <summary>
        /// <para>Removes the webhook from the provided application’s all activities configuration.</para>
        /// </summary>
        /// <param name="env_name">required.</param>
        /// <param name="webhook_id">required.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public Task RemoveAsync(string env_name, string webhook_id, CancellationToken cancellationToken = default(CancellationToken))
        {
            var parameters = new Dictionary<string, object>();
            if (env_name == null) throw new ArgumentNullException("env_name");
            parameters.Add("env_name", env_name);
            if (webhook_id == null) throw new ArgumentNullException("webhook_id");
            parameters.Add("webhook_id", webhook_id);
            return this.Tokens.AccessParameterReservedApiNoResponseAsync(MethodType.Delete, "account_activity/all/{env_name}/webhooks/{webhook_id}", new[] { "env_name", "webhook_id" }, parameters, cancellationToken);
        }

#endif

#if SYNC
        /// <summary>
        /// <para>Deactivates subscription(s) for the provided user context and application for all activities.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> env_name (required)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public void Deactivate(params Expression<Func<string, object>>[] parameters)
        {
            this.Tokens.AccessParameterReservedApiNoResponse(MethodType.Delete, "account_activity/all/{env_name}/subscriptions", new [] { "env_name" }, InternalUtils.ExpressionsToDictionary(parameters));
        }

        /// <summary>
        /// <para>Deactivates subscription(s) for the provided user context and application for all activities.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> env_name (required)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public void Deactivate(IDictionary<string, object> parameters)
        {
            this.Tokens.AccessParameterReservedApiNoResponse(MethodType.Delete, "account_activity/all/{env_name}/subscriptions", new [] { "env_name" }, parameters);
        }

        /// <summary>
        /// <para>Deactivates subscription(s) for the provided user context and application for all activities.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> env_name (required)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public void Deactivate(object parameters)
        {
            this.Tokens.AccessParameterReservedApiNoResponse(MethodType.Delete, "account_activity/all/{env_name}/subscriptions", new [] { "env_name" }, InternalUtils.ResolveObject(parameters));
        }

        /// <summary>
        /// <para>Deactivates subscription(s) for the provided user context and application for all activities.</para>
        /// </summary>
        /// <param name="env_name">required.</param>
        /// <returns></returns>
        public void Deactivate(string env_name)
        {
            var parameters = new Dictionary<string, object>();
            if(env_name == null) throw new ArgumentNullException("env_name");
            parameters.Add("env_name", env_name);
            this.Tokens.AccessParameterReservedApiNoResponse(MethodType.Delete, "account_activity/all/{env_name}/subscriptions", new [] { "env_name" }, parameters);
        }

#endif
#if ASYNC

        /// <summary>
        /// <para>Deactivates subscription(s) for the provided user context and application for all activities.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> env_name (required)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public Task DeactivateAsync(params Expression<Func<string, object>>[] parameters)
        {
            return this.Tokens.AccessParameterReservedApiNoResponseAsync(MethodType.Delete, "account_activity/all/{env_name}/subscriptions", new[] { "env_name" }, InternalUtils.ExpressionsToDictionary(parameters), CancellationToken.None);
        }

        /// <summary>
        /// <para>Deactivates subscription(s) for the provided user context and application for all activities.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> env_name (required)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public Task DeactivateAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessParameterReservedApiNoResponseAsync(MethodType.Delete, "account_activity/all/{env_name}/subscriptions", new[] { "env_name" }, parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Deactivates subscription(s) for the provided user context and application for all activities.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> env_name (required)</para>
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public Task DeactivateAsync(object parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Tokens.AccessParameterReservedApiNoResponseAsync(MethodType.Delete, "account_activity/all/{env_name}/subscriptions", new[] { "env_name" }, InternalUtils.ResolveObject(parameters), cancellationToken);
        }

        /// <summary>
        /// <para>Deactivates subscription(s) for the provided user context and application for all activities.</para>
        /// </summary>
        /// <param name="env_name">required.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public Task DeactivateAsync(string env_name, CancellationToken cancellationToken = default(CancellationToken))
        {
            var parameters = new Dictionary<string, object>();
            if (env_name == null) throw new ArgumentNullException("env_name");
            parameters.Add("env_name", env_name);
            return this.Tokens.AccessParameterReservedApiNoResponseAsync(MethodType.Delete, "account_activity/all/{env_name}/subscriptions", new[] { "env_name" }, parameters, cancellationToken);
        }

#endif
    }
}
