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

using CoreTweet.Core;
using Newtonsoft.Json;

namespace CoreTweet.V2
{
    public class RecentSearchMeta : CoreBase
    {
        /// <summary>
        /// Tweet ID of the most recent (largest ID) Tweet in the response. When implementing a polling use pattern, the first response contains the ID needed for setting the <c>since_id</c> for the next polling request.
        /// </summary>
        [JsonProperty("newest_id")]
        public long NewestId { get; set; }

        /// <summary>
        /// Tweet ID of the oldest (smallest ID) Tweet in the response.
        /// </summary>
        [JsonProperty("oldest_id")]
        public long OldestId { get; set; }

        /// <summary>
        /// Included when there is an additional 'page' of data to request.
        /// </summary>
        [JsonProperty("next_token")]
        public string NextToken { get; set; }

        /// <summary>
        /// Indicated the number of Tweets in the response.
        /// </summary>
        [JsonProperty("result_count")]
        public int ResultCount { get; set; }
    }

    public class RecentSearchResponse : ResponseBase
    {
        [JsonProperty("data")]
        public Tweet[] Data { get; set; }

        /// <summary>
        /// Returns the requested <see cref="TweetExpansions"/>, if available.
        /// </summary>
        [JsonProperty("includes")]
        public TweetResponseIncludes Includes { get; set; }

        [JsonProperty("meta")]
        public RecentSearchMeta Meta { get; set; }
    }
}
