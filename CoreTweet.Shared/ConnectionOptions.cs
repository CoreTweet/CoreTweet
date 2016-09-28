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

using System;
using System.Net;

#if WIN_RT
using Windows.Web.Http;
using Windows.Web.Http.Filters;
#elif ASYNC
using System.Net.Http;
#endif

namespace CoreTweet
{
    /// <summary>
    /// Properties for requesting.
    /// </summary>
    public class ConnectionOptions
#if !NETCORE
        : ICloneable
#endif
    {
        internal static readonly ConnectionOptions Default = new ConnectionOptions();

        /// <summary>
        /// Gets or sets the URL of REST API.
        /// <para>Default: <c>"https://api.twitter.com"</c></para>
        /// </summary>
        public string ApiUrl { get; set; } = "https://api.twitter.com";

        /// <summary>
        /// Gets or sets the URL of upload API.
        /// <para>Default: <c>"https://upload.twitter.com"</c></para>
        /// </summary>
        public string UploadUrl { get; set; } = "https://upload.twitter.com";

        /// <summary>
        /// Gets or sets the URL of User Streams API.
        /// <para>Default: <c>"https://userstream.twitter.com"</c></para>
        /// </summary>
        public string UserStreamUrl { get; set; } = "https://userstream.twitter.com";

        /// <summary>
        /// Gets or sets the URL of Site Streams API.
        /// <para>Default: <c>"https://sitestream.twitter.com"</c></para>
        /// </summary>
        public string SiteStreamUrl { get; set; } = "https://sitestream.twitter.com";

        /// <summary>
        /// Gets or sets the URL of Public Streams API.
        /// <para>Default: <c>"https://stream.twitter.com"</c></para>
        /// </summary>
        public string StreamUrl { get; set; } = "https://stream.twitter.com";

        /// <summary>
        /// Gets or sets the version of the Twitter API.
        /// <para>Default: <c>"1.1"</c></para>
        /// </summary>
        public string ApiVersion { get; set; } = "1.1";

        private int timeout = 100000;
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

#if SYNC
        private int readWriteTimeout = 300000;
        /// <summary>
        /// Gets or sets a time-out in milliseconds when writing to or reading from a stream.
        /// This value will be applied to only sync API methods.
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
#endif

        /// <summary>
        /// Gets or sets a value that indicates whether the handler uses a proxy for requests.
        /// </summary>
        public bool UseProxy { get; set; } = true;

#if WEBPROXY
        /// <summary>
        /// Gets or sets the proxy information for the request.
        /// </summary>
        public IWebProxy Proxy { get; set; } = null;
#endif

        /// <summary>
        /// Gets or sets the value of the User-agent HTTP header.
        /// </summary>
        public string UserAgent { get; set; } = "CoreTweet";

        /// <summary>
        /// Gets or sets whether the compression is used on non-streaming requests.
        /// </summary>
        public bool UseCompression { get; set; } = true;

        /// <summary>
        /// Gets or sets whether the compression is used on streaming requests.
        /// </summary>
        public bool UseCompressionOnStreaming { get; set; } = false;

        /// <summary>
        /// Gets or sets whether Keep-Alive requests are disabled.
        /// </summary>
        public bool DisableKeepAlive { get; set; } = true;

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
#if SYNC
                ReadWriteTimeout = this.ReadWriteTimeout,
#endif
                UseProxy = this.UseProxy,
#if WEBPROXY
                Proxy = this.Proxy,
#endif
                UserAgent = this.UserAgent,
                UseCompression = this.UseCompression,
                UseCompressionOnStreaming = this.UseCompressionOnStreaming,
                DisableKeepAlive = this.DisableKeepAlive
            };
        }

#if WIN_RT
        private HttpClient httpClient;
        private HttpBaseProtocolFilter handler;

        internal HttpClient GetHttpClient()
        {
            if (this.httpClient == null)
            {
                this.handler = new HttpBaseProtocolFilter();
                this.httpClient = new HttpClient(this.handler);
            }

            this.handler.AutomaticDecompression = this.UseCompression;
            this.handler.UseProxy = this.UseProxy;

            return this.httpClient;
        }
#elif ASYNC
        private HttpClient httpClient;
        private HttpClientHandler handler;

        private bool IsOptionsChanged()
        {
            return this.httpClient == null
                || (this.UseCompression && this.handler.AutomaticDecompression == DecompressionMethods.None)
                || this.UseProxy != this.handler.UseProxy
#if WEBPROXY
                || this.Proxy != this.handler.Proxy
#endif
                || this.Timeout != this.httpClient.Timeout.Ticks / TimeSpan.TicksPerMillisecond;
        }

        internal HttpClient GetHttpClient()
        {
            if (this.IsOptionsChanged())
            {
                this.handler = new HttpClientHandler();
                this.httpClient = new HttpClient(this.handler);

                this.handler.AutomaticDecompression = this.UseCompression
                    ? Request.CompressionType : DecompressionMethods.None;
                this.handler.UseProxy = this.UseProxy;
#if WEBPROXY
                this.Proxy = this.Proxy;
#endif
                this.httpClient.Timeout = new TimeSpan(TimeSpan.TicksPerMillisecond * this.Timeout);
            }

            return this.httpClient;
        }
#endif
    }
}
