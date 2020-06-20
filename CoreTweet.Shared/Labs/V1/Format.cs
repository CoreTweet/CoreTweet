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
using System.Runtime.Serialization;
using CoreTweet.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CoreTweet.Labs.V1
{
    /// <summary>
    /// Format for all the objects returned as part of the response, including those returned in expansions.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Format
    {
        [EnumMember(Value = "compact")]
        Compact,
        [EnumMember(Value = "default")]
        Default,
        [EnumMember(Value = "detailed")]
        Detailed,
    }

    public static class FormatExtensions
    {
        public static string ToQueryString(this Format value)
        {
            switch (value)
            {
                case Format.Compact: return "compact";
                case Format.Default: return "default";
                case Format.Detailed: return "detailed";
                default: throw new ArgumentException(nameof(value));
            }
        }
    }
}
