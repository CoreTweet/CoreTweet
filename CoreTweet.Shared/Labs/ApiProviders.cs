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
using System.Net;
using System.Threading;
using CoreTweet.Core;
using CoreTweet.Rest;
using CoreTweet.Streaming;

namespace CoreTweet.Labs
{
    public class LabsApi : ApiProviderBase
    {
        internal LabsApi(TokensBase e) : base(e)
        {
        }

        public LabsV1Api V1 => new LabsV1Api(this.Tokens);
        public LabsV2Api V2 => new LabsV2Api(this.Tokens);
    }

    public class LabsV1Api : ApiProviderBase
    {
        internal LabsV1Api(TokensBase e) : base(e)
        {
        }

        /// <summary>
        /// Gets the wrapper of Filtered stream v1 API on Labs v2.
        /// </summary>
        public V1.FilteredStreamApi FilteredStreamApi => new V1.FilteredStreamApi(this.Tokens);

        /// <summary>
        /// Gets the wrapper of Sampled stream v1 API on Labs v2.
        /// </summary>
        public V1.SampledStreamApi SampledStreamApi => new V1.SampledStreamApi(this.Tokens);

        /// <summary>
        /// Gets the wrapper of Tweet metrics v1 API on Labs v2.
        /// </summary>
        public V1.TweetMetricsApi TweetMetricsApi => new V1.TweetMetricsApi(this.Tokens);
    }

    public class LabsV2Api : ApiProviderBase
    {
        internal LabsV2Api(TokensBase e) : base(e)
        {
        }

        /// <summary>
        /// Gets the wrapper of Hide replies v2 API on Labs v2.
        /// </summary>
        public V2.HideRepliesApi HideRepliesApi => new V2.HideRepliesApi(this.Tokens);
        /// <summary>
        /// Gets the wrapper of Recent search v2 API on Labs v2.
        /// </summary>
        public V2.RecentSearchApi RecentSearchApi => new V2.RecentSearchApi(this.Tokens);
        /// <summary>
        /// Gets the wrapper of Tweets and Users v2 API on Labs v2.
        /// </summary>
        public V2.TweetsAndUsersApi TweetsAndUsersApi => new V2.TweetsAndUsersApi(this.Tokens);
    }
}
