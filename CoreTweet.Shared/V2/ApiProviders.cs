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

namespace CoreTweet.V2
{
    public class V2Api : ApiProviderBase
    {
        internal V2Api(TokensBase e) : base(e)
        {
        }

        /// <summary>
        /// Gets the wrapper of Tweet Lookup API on Twitter API v2.
        /// </summary>
        public TweetLookupApi TweetLookupApi => new TweetLookupApi(this.Tokens);

        /// <summary>
        /// Gets the wrapper of User Lookup API on Twitter API v2.
        /// </summary>
        public UserLookupApi UserLookupApi => new UserLookupApi(this.Tokens);

        /// <summary>
        /// Gets the wrapper of Recent search API on Twitter API v2.
        /// </summary>
        public RecentSearchApi RecentSearchApi => new RecentSearchApi(this.Tokens);

        /// <summary>
        /// Gets the wrapper of Filtered stream API on Twitter API v2.
        /// </summary>
        public FilteredStreamApi FilteredStreamApi => new FilteredStreamApi(this.Tokens);

        /// <summary>
        /// Gets the wrapper of Sampled stream API on Twitter API v2.
        /// </summary>
        public SampledStreamApi SampledStreamApi => new SampledStreamApi(this.Tokens);

        /// <summary>
        /// Gets the wrapper of Hide replies API on Twitter API v2.
        /// </summary>
        public HideRepliesApi HideRepliesApi => new HideRepliesApi(this.Tokens);
    }
}
