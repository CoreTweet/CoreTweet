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
    public abstract class ResponseBase : CoreBase, ITwitterResponse
    {
        [JsonProperty("errors")]
        public PartialError[] Errors { get; set; }

        /// <summary>
        /// Gets or sets the rate limit of the response.
        /// </summary>
        /// <remarks>
        /// This property will always be null when obtained from (most of) the POST endpoints, unless the rate is explicitly stated in the Twitter official documentation.
        /// </remarks>
        public RateLimit RateLimit { get; set; }

        /// <summary>
        /// Gets or sets the JSON of the response.
        /// </summary>
        public string Json { get; set; }
    }

    public class CursoredResponseMeta : CoreBase
    {
        /// <summary>
        /// The number of results returned in the response.
        /// </summary>
        [JsonProperty("result_count")]
        public int ResultCount { get; set; }
        // MEMO: the document is wrong (actual key is `result_count`, not `count`)

        /// <summary>
        /// A value that encodes the next 'page' of results that can be requested, via the <c>pagination_token</c> request parameter.
        /// </summary>
        [JsonProperty("next_token")]
        public string NextToken { get; set; }

        /// <summary>
        /// A value that encodes the previous 'page' of results that can be requested, via the <c>pagination_token</c> request parameter.
        /// </summary>
        [JsonProperty("previous_token")]
        public string PreviousToken { get; set; }
    }
}
