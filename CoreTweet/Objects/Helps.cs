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
using System.Linq;
using CoreTweet.Core;
using Newtonsoft.Json;

namespace CoreTweet
{
    public class Configurations : CoreBase
    {
        /// <summary>
        /// Count of the characters that is reserved for a media.
        /// </summary>
        /// <value>
        /// The count.
        /// </value>
        [JsonProperty("characters_reserver_per_media")]
        public int CharactersReservedPerMedia { get; set; }

        /// <summary>
        /// Limit of the count of medias that can be uploaded at once.
        /// </summary>
        /// <value>
        /// The count.
        /// </value>
        [JsonProperty("max_media_per_upload")]
        public int MaxMediaPerUpload{ get; set; }

        /// <summary>
        /// Paths in twitter.com/ that is not an username.
        /// They are used for pages of Twitter.
        /// </summary>
        /// <value>
        /// The non-username paths.
        /// </value>
        [JsonProperty("non_username_paths")]
        public string[] NonUsernamePaths{ get; set; }

        /// <summary>
        /// Limit of the size of media that can be uploaded.
        /// </summary>
        /// <value>
        /// The limit value.
        /// </value>
        [JsonProperty("photo_size_limit")]
        public int PhotoSizeLimit{ get; set; }

        /// <summary>
        /// Length of the shorten URL.
        /// </summary>
        /// <value>
        /// The length.
        /// </value>
        [JsonProperty("short_url_length")]
        public int ShortUrlLength{ get; set; }

        /// <summary>
        /// Length of the shorten URL that uses SSL.
        /// </summary>
        /// <value>
        /// The length.
        /// </value>
        [JsonProperty("short_url_length_https")]
        public int ShortUrlLengthHttps{ get; set; }

        /// <summary>
        /// Sizes of a photo.
        /// </summary>
        /// <value>
        /// Sizes.
        /// </value>
        [JsonProperty("photo_sizes")]
        public Sizes PhotoSizes{ get; set; }
    }
    
    public class Language : CoreBase
    {
        /// <summary>
        /// The language code.
        /// </summary>
        /// <value>
        /// The code.
        /// </value>
        [JsonProperty("code")]
        public string Code{ get; set; }

        /// <summary>
        /// Name of the language.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [JsonProperty("name")]
        public string Name{ get; set; }

        /// <summary>
        /// Status of the language.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        [JsonProperty("status")]
        public string Status{ get; set; }
    }
}

