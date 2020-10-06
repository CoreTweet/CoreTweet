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

using System.Runtime.Serialization;
using CoreTweet.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CoreTweet.V2
{
    /// <summary>
    /// Contains withholding details for withheld content.
    /// </summary>
    public class Withheld : CoreBase
    {
        /// <summary>
        /// Indicates if the content is being withheld for on the basis of copyright infringement.
        /// </summary>
        [JsonProperty("copyright")]
        public bool? Copyright { get; set; }

        /// <summary>
        /// Provides a list of countries where this user is not available.
        /// </summary>
        [JsonProperty("country_codes")]
        public string[] CountryCodes { get; set; }

        /// <summary>
        /// Indicates whether the content being withheld is a Tweet or a user.
        /// </summary>
        [JsonProperty("scope")]
        public WithheldScope Scope { get; set; }
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum WithheldScope
    {
        [EnumMember(Value = "tweet")]
        Tweet,
        [EnumMember(Value = "user")]
        User,
    }
}
