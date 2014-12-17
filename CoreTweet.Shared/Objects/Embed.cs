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
    /// Represents an oEmbed representation of a Tweet.
    /// </summary>
    public class Embed : CoreBase, ITwitterResponse
    {
        /// <summary>
        /// Gets or sets the HTML code that can be embedded.
        /// </summary>
        [JsonProperty("html")]
        public string Html { get; set; }

        /// <summary>
        /// Gets or sets the name of the author.
        /// </summary>
        [JsonProperty("author_name")]
        public string AuthorName { get; set; }

        /// <summary>
        /// Gets or sets the URL of the author.
        /// </summary>
        [JsonProperty("author_url")]
        [JsonConverter(typeof(UriConverter))]
        public Uri AuthorUrl { get; set; }

        /// <summary>
        /// Gets or sets the URL of the provider.
        /// </summary>
        [JsonProperty("provider_url")]
        [JsonConverter(typeof(UriConverter))]
        public Uri ProviderUrl{ get; set; }

        /// <summary>
        /// Gets or sets the name of the provider.
        /// </summary>
        [JsonProperty("provider_name")]
        public string ProviderName{ get; set; }

        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        [JsonProperty("url")]
        [JsonConverter(typeof(UriConverter))]
        public Uri Url{ get; set; }

        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        [JsonProperty("version")]
        public string Version { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        [JsonProperty("type")]
        public string Type{ get; set; }

        /// <summary>
        /// Gets or sets the height of the embed object.
        /// </summary>
        [JsonProperty("height")]
        public int? Height{ get; set; }

        /// <summary>
        /// Gets or sets the width of the embed object.
        /// </summary>
        [JsonProperty("width")]
        public int? Width{ get; set; }

        /// <summary>
        /// Gets or sets the age of the cache.
        /// </summary>
        [JsonProperty("cache_age")]
        public string CacheAge{ get; set; }

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

