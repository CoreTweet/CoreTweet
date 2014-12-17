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

using CoreTweet.Core;
using Newtonsoft.Json;

namespace CoreTweet
{
    /// <summary>
    /// Represents the current configuration used by Twitter including twitter.com slugs which are not usernames, maximum photo resolutions, and t.co URL lengths.
    /// </summary>
    public class Configurations : CoreBase, ITwitterResponse
    {
        /// <summary>
        /// Gets or sets the count of the characters that is reserved for a media.
        /// </summary>
        [JsonProperty("characters_reserved_per_media")]
        public int CharactersReservedPerMedia { get; set; }

        /// <summary>
        /// Gets or sets the count of the limit of the count of medias that can be uploaded at once.
        /// </summary>
        [JsonProperty("max_media_per_upload")]
        public int MaxMediaPerUpload{ get; set; }

        /// <summary>
        /// <para>Gets or sets the paths in twitter.com/ that is not an username.</para>
        /// <para>They are used for pages of Twitter.</para>
        /// </summary>
        [JsonProperty("non_username_paths")]
        public string[] NonUsernamePaths{ get; set; }

        /// <summary>
        /// Gets or sets the limit of the size of media that can be uploaded.
        /// </summary>
        [JsonProperty("photo_size_limit")]
        public int PhotoSizeLimit{ get; set; }

        /// <summary>
        /// Gets or sets the length of the shorten URL.
        /// </summary>
        [JsonProperty("short_url_length")]
        public int ShortUrlLength{ get; set; }

        /// <summary>
        /// Gets or sets the length of the shorten URL that uses SSL.
        /// </summary>
        [JsonProperty("short_url_length_https")]
        public int ShortUrlLengthHttps{ get; set; }

        /// <summary>
        /// Gets or sets the sizes of a photo.
        /// </summary>
        [JsonProperty("photo_sizes")]
        public MediaSizes PhotoSizes{ get; set; }

        /// <summary>
        /// Gets or sets the rate limit of the response.
        /// </summary>
        public RateLimit RateLimit { get; set; }

        /// <summary>
        /// Gets or sets the JSON of the response
        /// </summary>
        public string Json { get; set; }
    }

    /// <summary>
    /// Represents a language code, name and status.
    /// </summary>
    public class Language : CoreBase
    {
        /// <summary>
        /// Gets or sets the the language code.
        /// </summary>
        [JsonProperty("code")]
        public string Code{ get; set; }

        /// <summary>
        /// Gets or sets the name of the language.
        /// </summary>
        [JsonProperty("name")]
        public string Name{ get; set; }

        /// <summary>
        /// Gets or sets the status of the language.
        /// </summary>
        [JsonProperty("status")]
        public string Status{ get; set; }
    }

    /// <summary>
    /// Represents the Twitter Terms of Service.
    /// </summary>
    public class StringResponse : CoreBase, ITwitterResponse
    {
        [JsonProperty]
        private string privacy = null;

        [JsonProperty]
        private string tos = null;

        /// <summary>
        /// Gets or sets the value of response.
        /// </summary>
        public string Value
        {
            get
            {
                return this.privacy ?? this.tos;
            }
        }

        /// <summary>
        /// Gets or sets the rate limit of the response.
        /// </summary>
        public RateLimit RateLimit { get; set; }

        /// <summary>
        /// Gets or sets the JSON of the response
        /// </summary>
        public string Json { get; set; }

        /// <summary>
        /// Returns the ID of this instance.
        /// </summary>
        /// <returns>The ID of this instance.</returns>
        public override string ToString()
        {
            return this.Value;
        }
    }
}

