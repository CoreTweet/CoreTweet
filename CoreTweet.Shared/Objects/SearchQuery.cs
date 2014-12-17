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
using CoreTweet.Core;
using Newtonsoft.Json;

namespace CoreTweet
{
    /// <summary>
    /// Represents a saved search query used in the Twitter Search API.
    /// </summary>
    public class SearchQuery : CoreBase
    {
        /// <summary>
        /// Gets or sets the created time of the saved search.
        /// </summary>
        [JsonProperty("created_at")]
        [JsonConverter(typeof(DateTimeOffsetConverter))]
        public DateTimeOffset CreatedAt{ get; set; }

        /// <summary>
        /// Gets or sets the ID of the saved search.
        /// </summary>
        [JsonProperty("id")]
        public long? Id{ get; set; }

        /// <summary>
        /// Gets or sets the name of the saved search.
        /// </summary>
        [JsonProperty("name")]
        public string Name{ get; set; }

        /// <summary>
        /// Gets or sets the query of the saved search.
        /// </summary>
        [JsonProperty("query")]
        public string Query{ get; set; }

        /// <summary>
        /// Returns the ID of this instance.
        /// </summary>
        /// <returns>The ID of this instance.</returns>
        public override string ToString()
        {
            return this.Id.HasValue ? this.Id.Value.ToString("D") : "";
        }
    }

    /// <summary>
    /// Represents a saved search query used in the Twitter Search API with the rate limit.
    /// </summary>
    public class SearchQueryResponse : SearchQuery, ITwitterResponse
    {
        /// <summary>
        /// Gets or sets the rate limit of the response.
        /// </summary>
        public RateLimit RateLimit { get; set; }

        /// <summary>
        /// Gets or sets the JSON of the response
        /// </summary>
        public string Json { get; set; }
    }
}

