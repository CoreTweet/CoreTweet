// The MIT License (MIT)
//
// CoreTweet - A .NET Twitter Library supporting Twitter API 1.1
// Copyright (c) 2013-2016 CoreTweet Development Team
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
    /// Represents the result of the uploaded media.
    /// </summary>
    public class MediaUploadResult : CoreBase, ITwitterResponse
    {
        /// <summary>
        /// Gets or sets the remaining time before the media ID expires.
        /// </summary>
        [JsonProperty("expires_after_secs")]
        public int ExpiresAfterSecs { get; set; }

        /// <summary>
        /// Gets or sets the data of the image.
        /// </summary>
        [JsonProperty("image")]
        public UploadedImage Image { get; set; }

        /// <summary>
        /// Gets or sets the ID of the media.
        /// </summary>
        [JsonProperty("media_id")]
        public long MediaId { get; set; }

        /// <summary>
        /// Gets or sets the size of the media.
        /// </summary>
        [JsonProperty("size")]
        public long Size { get; set; }

        /// <summary>
        /// Gets or sets the data of the video.
        /// </summary>
        [JsonProperty("video")]
        public UploadedVideo Video { get; set; }

        /// <summary>
        /// Gets or sets the rate limit of the response.
        /// </summary>
        public RateLimit RateLimit { get; set; }

        /// <summary>
        /// Gets or sets the JSON of the response.
        /// </summary>
        public string Json { get; set; }

        /// <summary>
        /// Returns the ID of this instance.
        /// </summary>
        /// <returns>The ID of this instance.</returns>
        public override string ToString()
        {
            return this.MediaId.ToString("D");
        }
    }

    /// <summary>
    /// Represents the detail data of the uploaded image.
    /// </summary>
    public class UploadedImage : CoreBase
    {
        /// <summary>
        /// Gets or sets the width of the image.
        /// </summary>
        [JsonProperty("w")]
        public int Width { get; set; }

        /// <summary>
        /// Gets or sets the height of the image.
        /// </summary>
        [JsonProperty("h")]
        public int Height { get; set; }

        /// <summary>
        /// Gets or sets the type of the image.
        /// </summary>
        [JsonProperty("image_type")]
        public string ImageType { get; set; }
    }

    /// <summary>
    /// Represents the detail data of the uploaded video.
    /// </summary>
    public class UploadedVideo : CoreBase
    {
        /// <summary>
        /// Gets or sets the MIME type of the video.
        /// </summary>
        [JsonProperty("video_type")]
        public string VideoType { get; set; }
    }

    /// <summary>
    /// Provides the type of media to upload.
    /// </summary>
    public enum UploadMediaType
    {
        /// <summary>
        /// An image file.
        /// </summary>
        Image,
        /// <summary>
        /// A video file.
        /// </summary>
        Video
    }

    /// <summary>
    /// Represents the result of INIT command.
    /// </summary>
    public class UploadInitCommandResult : CoreBase, ITwitterResponse
    {
        /// <summary>
        /// Gets or sets the remaining time before the media ID expires.
        /// </summary>
        [JsonProperty("expires_after_secs")]
        public int ExpiresAfterSecs { get; set; }

        /// <summary>
        /// Gets or sets the ID of the media.
        /// </summary>
        [JsonProperty("media_id")]
        public long MediaId { get; set; }

        /// <summary>
        /// Gets or sets the rate limit of the response.
        /// </summary>
        public RateLimit RateLimit { get; set; }

        /// <summary>
        /// Gets or sets the JSON of the response.
        /// </summary>
        public string Json { get; set; }

        /// <summary>
        /// Returns the ID of this instance.
        /// </summary>
        /// <returns>The ID of this instance.</returns>
        public override string ToString()
        {
            return this.MediaId.ToString("D");
        }
    }

    public class ProcessingInfo : CoreBase
    {
        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("check_after_secs")]
        public int? CheckAfterSecs { get; set; }

        [JsonProperty("progress_percent")]
        public int? ProgressPercent { get; set; }

        [JsonProperty("error")]
        public MediaProcessingError Error { get; set; }
    }

    public class MediaProcessingError : CoreBase
    {
        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }

    public class UploadFinalizeCommandResult : MediaUploadResult
    {
        [JsonProperty("processing_info")]
        public ProcessingInfo ProcessingInfo { get; set; }
    }
}