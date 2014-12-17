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
    /// Represents the rate limits for a resource family.
    /// </summary>
    public class RateLimit : CoreBase
    {
        /// <summary>
        /// Gets or sets the left of requesting the resource within the current rate limiting window.
        /// </summary>
        [JsonProperty("remaining")]
        public int Remaining { get; set; }

        /// <summary>
        /// Gets or sets the reset expiration time.
        /// </summary>
        [JsonProperty("reset")]
        [JsonConverter(typeof(DateTimeOffsetConverter))]
        public DateTimeOffset Reset { get; set; }

        /// <summary>
        /// Gets or sets the value that represents the limit count within the current rate limiting window.
        /// </summary>
        [JsonProperty("limit")]
        public int Limit { get; set; }
    }
}

