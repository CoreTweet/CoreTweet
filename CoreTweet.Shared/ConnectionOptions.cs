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
using System.Net;

#if ASYNC
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
        [Obsolete("This property will removed in future release.")]
        public string ApiVersion
        {
            get
            {
                return UrlPrefix;
            }
            set
            {
                UrlPrefix = value;
            }
        }

        internal string UrlPrefix { get; set; } = "1.1";
        internal string UrlSuffix { get; set; } = ".json";

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

        /// <summary>
        /// Gets or sets the proxy information for the request.
        /// </summary>
        public IWebProxy Proxy { get; set; } = null;

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

#if !NETCORE
        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>A new object that is a copy of this instance.</returns>
        object ICloneable.Clone()
        {
            return Clone();
        }
#endif

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>A new object that is a copy of this instance.</returns>
        public ConnectionOptions Clone()
        {
            return new ConnectionOptions()
            {
                ApiUrl = this.ApiUrl,
                UploadUrl = this.UploadUrl,
                UserStreamUrl = this.UserStreamUrl,
                SiteStreamUrl = this.SiteStreamUrl,
                StreamUrl = this.StreamUrl,
                UrlPrefix = this.UrlPrefix,
                UrlSuffix = this.UrlSuffix,
                Timeout = this.Timeout,
#if SYNC
                ReadWriteTimeout = this.ReadWriteTimeout,
#endif
                UseProxy = this.UseProxy,
                Proxy = this.Proxy,
                UserAgent = this.UserAgent,
                UseCompression = this.UseCompression,
                UseCompressionOnStreaming = this.UseCompressionOnStreaming,
                DisableKeepAlive = this.DisableKeepAlive
            };
        }

#if ASYNC
        private Tuple<HttpClient, HttpClientHandler> httpClientTuple;

        private bool IsOptionsChanged(Tuple<HttpClient, HttpClientHandler> httpClientTuple)
        {
            if (httpClientTuple == null) return true;

            var httpClient = httpClientTuple.Item1;
            var handler = httpClientTuple.Item2;

            return (this.UseCompression && handler.AutomaticDecompression == DecompressionMethods.None)
                || this.UseProxy != handler.UseProxy
                || !Equals(this.Proxy, handler.Proxy)
                || this.Timeout != httpClient.Timeout.Ticks / TimeSpan.TicksPerMillisecond;
        }

        internal HttpClient GetHttpClient()
        {
            // Copy the reference for thread safety
            var httpClientTuple = this.httpClientTuple;

            if (this.IsOptionsChanged(httpClientTuple))
            {
                var handler = new HttpClientHandler();
                var httpClient = new HttpClient(handler);

                handler.AutomaticDecompression = this.UseCompression
                    ? Request.CompressionType : DecompressionMethods.None;
                handler.UseProxy = this.UseProxy;
                handler.Proxy = this.Proxy;
                httpClient.Timeout = new TimeSpan(TimeSpan.TicksPerMillisecond * this.Timeout);

                httpClientTuple = Tuple.Create(httpClient, handler);
                this.httpClientTuple = httpClientTuple;
            }

            return httpClientTuple.Item1;
        }
#endif
    }
}
