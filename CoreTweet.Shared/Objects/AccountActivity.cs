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
using CoreTweet.Core;
using Newtonsoft.Json;

namespace CoreTweet
{
    public class Webhook : CoreBase
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("valid")]
        public bool IsValid { get; set; }

        [JsonProperty("created_at")]
        public DateTimeOffset CreatedAt { get; set; }
    }

    public class EnvironmentsResponse : CoreBase
    {
        [JsonProperty("environments")]
        public Environment[] Environments { get; set; }
    }

    public class Environment : CoreBase
    {
        [JsonProperty("environment_name")]
        public string EnvironmentName { get; set; }

        [JsonProperty("webhooks")]
        public Webhook[] Webhooks { get; set; }
    }

    public class SubscriptionsCountResponse : CoreBase
    {
        [JsonProperty("account_name")]
        public string AccountName { get; set; }

        [JsonProperty("subscriptions_count_all")]
        public string SubscriptionsCountAll { get; set; }

        [JsonProperty("subscriptions_count_direct_messages")]
        public string SubscriptionsCountDirectMessages { get; set; }
    }

    public class SubscriptionsListResponse : CoreBase
    {
        [JsonProperty("environment")]
        public string Environment { get; set; }

        [JsonProperty("application_id")]
        public string ApplicationId { get; set; }

        [JsonProperty("subscriptions")]
        public UserId[] Subscriptions { get; set; }
    }
}
