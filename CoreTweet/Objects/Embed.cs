// The MIT License (MIT)
//
// CoreTweet - A .NET Twitter Library supporting Twitter API 1.1
// Copyright (c) 2013 lambdalice
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
using System.Net;
using CoreTweet.Core;
using Newtonsoft.Json;

namespace CoreTweet
{
    public class Embed : CoreBase
    {
        /// <summary>
        /// The html code that can be embedded.
        /// </summary>
        /// <value>
        /// The html.
        /// </value>
        [JsonProperty("html")]
        public string Html { get; set; }

        /// <summary>
        /// The name of the author.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [JsonProperty("author_name")]
        public string AuthorName { get; set; }

        /// <summary>
        /// The URL of the author.
        /// </summary>
        /// <value>
        /// The URL.
        /// </value>
        [JsonProperty("author_url")]
        [JsonConverter(typeof(UriConverter))]
        public Uri AuthorUrl { get; set; }

        /// <summary>
        /// The URL of ther provider.
        /// </summary>
        /// <value>
        /// The URL.
        /// </value>
        [JsonProperty("provider_url")]
        [JsonConverter(typeof(UriConverter))]
        public string ProviderUrl{ get; set; }
  
        /// <summary>
        /// The name of the provider.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [JsonProperty("provider_name")]
        public string ProviderName{ get; set; }

        /// <summary>
        /// The URL.
        /// </summary>
        /// <value>
        /// The URL.
        /// </value>
        [JsonProperty("url")]
        [JsonConverter(typeof(UriConverter))]
        public Uri Url{ get; set; }

        /// <summary>
        /// The version.
        /// </summary>
        /// <value>
        /// The version.
        /// </value>
        [JsonProperty("version")]
        public string Version { get; set; }

        /// <summary>
        /// The type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        [JsonProperty("type")]
        public string Type{ get; set; }

        /// <summary>
        /// The height of the embed object.
        /// </summary>
        /// <value>
        /// The height.
        /// </value>
        [JsonProperty("height")]
        public int? Height{ get; set; }

        /// <summary>
        /// The width of the embed object.
        /// </summary>
        /// <value>
        /// The width.
        /// </value>
        [JsonProperty("width")]
        public int? Width{ get; set; }

        /// <summary>
        /// The age of the cache.
        /// </summary>
        /// <value>
        /// The age.
        /// </value>
        [JsonProperty("cache_age")]
        public string CacheAge{ get; set; }
    }
}

