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
using System.Net;

namespace CoreTweet
{
    /// <summary>
    /// Properties for requesting.
    /// </summary>
    public class ConnectionOptions
#if !PCL
        : ICloneable
#endif
    {
        public ConnectionOptions()
        {
            this.Timeout = 100000;
#if !PCL
            this.ReadWriteTimeout = 300000;
            this.UserAgent = "CoreTweet";
#endif
        }

        /// <summary>
        /// Gets or sets the time-out value in milliseconds.
        /// </summary>
        public int Timeout { get; set; }

#if !PCL
        /// <summary>
        /// Gets or sets a time-out in milliseconds when writing to or reading from a stream.
        /// </summary>
        public int ReadWriteTimeout { get; set; }

        /// <summary>
        /// Gets or sets the value of the User-agent HTTP header.
        /// </summary>
        public string UserAgent { get; set; }

        /// <summary>
        /// Gets or sets proxy information for the request.
        /// </summary>
        public IWebProxy Proxy { get; set; }
#endif

        /// <summary>
        /// Gets or sets action which is called before sending request.
        /// </summary>
        public Action<HttpWebRequest> BeforeRequestAction { get; set; }

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>A copy of this instance.</returns>
        public object Clone()
        {
            return new ConnectionOptions()
            {
                Timeout = this.Timeout,
#if !PCL
                ReadWriteTimeout = this.ReadWriteTimeout,
                UserAgent = this.UserAgent,
                Proxy = this.Proxy,
#endif
                BeforeRequestAction = this.BeforeRequestAction
            };
        }
    }
}
