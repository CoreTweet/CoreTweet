// The MIT License (MIT)
//
// CoreTweet - A .NET Twitter Library supporting Twitter API 1.1
// Copyright (c) 2013-2015 CoreTweet Development Team
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

#if WIN_RT
using Windows.Web.Http;
#endif

namespace CoreTweet
{
    /// <summary>
    /// Properties for requesting.
    /// </summary>
    public class ConnectionOptions
#if !(PCL || WIN_RT || WP)
        : ICloneable
#endif
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CoreTweet.ConnectionOptions"/> class.
        /// </summary>
        public ConnectionOptions()
        {
            this.ApiUrl = "https://api.twitter.com";
            this.UploadUrl = "https://upload.twitter.com";
            this.UserStreamUrl = "https://userstream.twitter.com";
            this.SiteStreamUrl = "https://sitestream.twitter.com";
            this.StreamUrl = "https://stream.twitter.com";
            this.ApiVersion = "1.1";
            this.Timeout = 100000;
#if !(PCL || WIN_RT || WP)
            this.ReadWriteTimeout = 300000;
            this.Proxy = WebRequest.DefaultWebProxy;
#endif
            this.UserAgent = "CoreTweet";
#if !(PCL || WP)
            this.UseCompression = true;
            this.UseCompressionOnStreaming = false;
            this.DisableKeepAlive = true;
#endif
        }

        /// <summary>
        /// Gets or sets the URL of REST API.
        /// <para>Default: <c>"https://api.twitter.com"</c></para>
        /// </summary>
        public string ApiUrl { get; set; }

        /// <summary>
        /// Gets or sets the URL of upload API.
        /// <para>Default: <c>"https://upload.twitter.com"</c></para>
        /// </summary>
        public string UploadUrl { get; set; }

        /// <summary>
        /// Gets or sets the URL of User Streams API.
        /// <para>Default: <c>"https://userstream.twitter.com"</c></para>
        /// </summary>
        public string UserStreamUrl { get; set; }

        /// <summary>
        /// Gets or sets the URL of Site Streams API.
        /// <para>Default: <c>"https://sitestream.twitter.com"</c></para>
        /// </summary>
        public string SiteStreamUrl { get; set; }

        /// <summary>
        /// Gets or sets the URL of Public Streams API.
        /// <para>Default: <c>"https://stream.twitter.com"</c></para>
        /// </summary>
        public string StreamUrl { get; set; }

        /// <summary>
        /// Gets or sets the version of the Twitter API.
        /// <para>Default: <c>"1.1"</c></para>
        /// </summary>
        public string ApiVersion { get; set; }

        private int timeout;
        /// <summary>
        /// Gets or sets the time-out value in milliseconds.
        /// </summary>
        public int Timeout
        {
            get
            {
                return this.timeout;
            }
            set
            {
                if(value <= 0 && value != System.Threading.Timeout.Infinite)
                    throw new ArgumentOutOfRangeException();
                this.timeout = value;
            }
        }

#if !(PCL || WIN_RT || WP)
        private int readWriteTimeout;
        /// <summary>
        /// Gets or sets a time-out in milliseconds when writing to or reading from a stream.
        /// </summary>
        public int ReadWriteTimeout
        {
            get
            {
                return this.readWriteTimeout;
            }
            set
            {
                if(value <= 0 && value != System.Threading.Timeout.Infinite)
                    throw new ArgumentOutOfRangeException();
                this.readWriteTimeout = value;
            }
        }

        /// <summary>
        /// Gets or sets the proxy information for the request.
        /// </summary>
        public IWebProxy Proxy { get; set; }
#endif

        /// <summary>
        /// Gets or sets the value of the User-agent HTTP header.
        /// </summary>
        public string UserAgent { get; set; }

#if !(PCL || WP)
        /// <summary>
        /// Gets or sets whether the compression is used on non-streaming requests.
        /// </summary>
        public bool UseCompression { get; set; }

        /// <summary>
        /// Gets or sets whether the compression is used on streaming requests.
        /// </summary>
        public bool UseCompressionOnStreaming { get; set; }

        /// <summary>
        /// Gets or sets whether Keep-Alive requests are disabled.
        /// </summary>
        public bool DisableKeepAlive { get; set; }
#endif

#if !PCL
        /// <summary>
        /// Gets or sets the action which is called before sending request.
        /// </summary>
#if WIN_RT
        public Action<HttpRequestMessage> BeforeRequestAction { get; set; }
#else
        public Action<HttpWebRequest> BeforeRequestAction { get; set; }
#endif
#endif

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>A new object that is a copy of this instance.</returns>
        public object Clone()
        {
            return new ConnectionOptions()
            {
                ApiUrl = this.ApiUrl,
                UploadUrl = this.UploadUrl,
                UserStreamUrl = this.UserStreamUrl,
                SiteStreamUrl = this.SiteStreamUrl,
                StreamUrl = this.StreamUrl,
                ApiVersion = this.ApiVersion,
                Timeout = this.Timeout,
#if !(PCL || WIN_RT || WP)
                ReadWriteTimeout = this.ReadWriteTimeout,
                Proxy = this.Proxy,
#endif
                UserAgent = this.UserAgent,
#if !(PCL || WP)
                UseCompression = this.UseCompression,
                UseCompressionOnStreaming = this.UseCompressionOnStreaming,
                DisableKeepAlive = this.DisableKeepAlive,
#endif
#if !PCL
                BeforeRequestAction = this.BeforeRequestAction
#endif
            };
        }
    }
}
