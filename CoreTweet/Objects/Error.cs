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

namespace CoreTweet
{
    public enum ErrorVersion
    {
        /// <summary>
        /// This error object is an instance of <see cref="V1.Error"/>.
        /// </summary>
        V1 = 1,

        /// <summary>
        /// This error object is an instance of <see cref="V2.Error"/>.
        /// </summary>
        V2 = 2,

        /// <summary>
        /// This error object is an instance of <see cref="Error"/>.
        /// </summary>
        Unknown = -1
    }

    public interface IError
    {
        /// <summary>
        /// Determines which one of <see cref="V1.Error"/>, <see cref="V2.Error"/>, or <see cref="Error"/> this error object is.
        /// </summary>
        ErrorVersion Version { get; }

        /// <summary>
        /// The error message.
        /// </summary>
        string Message { get; }
    }

    /// <summary>
    /// Represents the error response from Twitter API.
    /// </summary>
    public class Error : CoreBase, IError
    {
        [JsonIgnore]
        public ErrorVersion Version => ErrorVersion.Unknown;

        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
