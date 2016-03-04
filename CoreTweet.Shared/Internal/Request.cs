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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using CoreTweet.Core;

namespace CoreTweet
{
    /// <summary>
    /// Provides a set of static (Shared in Visual Basic) methods for sending a request to Twitter and some other web services.
    /// </summary>
    internal static partial class Request
    {
        private static string CreateQueryString(IEnumerable<KeyValuePair<string, string>> prm)
        {
            return prm.Select(x => UrlEncode(x.Key) + "=" + UrlEncode(x.Value)).JoinToString("&");
        }

        internal static string CreateQueryString(IEnumerable<KeyValuePair<string, object>> prm)
        {
            return CreateQueryString(prm.Select(x => new KeyValuePair<string, string>(x.Key, x.Value.ToString())));
        }

#if !WIN_RT
        private static void WriteMultipartFormData(Stream stream, string boundary, IEnumerable<KeyValuePair<string, object>> prm)
        {
            const int bufferSize = 81920;

            foreach(var x in prm)
            {
                var valueStream = x.Value as Stream;
                var valueArraySegment = x.Value as ArraySegment<byte>?;
                var valueBytes = x.Value as IEnumerable<byte>;
#if !PCL
                var valueFile = x.Value as FileInfo;
#endif
                var valueString = x.Value.ToString();
#if WP
                var valueInputStream = x.Value as Windows.Storage.Streams.IInputStream;
                if(valueInputStream != null) valueStream = valueInputStream.AsStreamForRead();
#endif

                stream.WriteString("--" + boundary + "\r\n");
                if(valueStream != null || valueBytes != null || valueArraySegment != null
#if !PCL
                    || valueFile != null
#endif
                   )
                {
                    stream.WriteString("Content-Type: application/octet-stream\r\n");
                }
                stream.WriteString(string.Format(@"Content-Disposition: form-data; name=""{0}""", x.Key));
#if !PCL
                if(valueFile != null)
                    stream.WriteString(string.Format(@"; filename=""{0}""",
                        valueFile.Name.Replace("\n", "%0A").Replace("\r", "%0D").Replace("\"", "%22")));
                else
#endif
                if(valueStream != null || valueBytes != null || valueArraySegment != null)
                    stream.WriteString(@"; filename=""file""");
                stream.WriteString("\r\n\r\n");

#if !PCL
                if(valueFile != null)
                    valueStream = valueFile.OpenRead();
#endif
                if(valueStream != null)
                {
                    var buffer = new byte[bufferSize];
                    int count;
                    while((count = valueStream.Read(buffer, 0, bufferSize)) > 0)
                        stream.Write(buffer, 0, count);
                }
                else if(valueArraySegment != null)
                {
                    stream.Write(valueArraySegment.Value.Array, valueArraySegment.Value.Offset, valueArraySegment.Value.Count);
                }
                else if(valueBytes != null)
                {
                    var buffer = valueBytes as byte[];
                    if(buffer != null)
                        stream.Write(buffer, 0, buffer.Length);
                    else
                    {
                        buffer = new byte[bufferSize];
                        var i = 0;
                        foreach(var b in valueBytes)
                        {
                            buffer[i++] = b;
                            if(i == bufferSize)
                            {
                                stream.Write(buffer, 0, bufferSize);
                                i = 0;
                            }
                        }
                        if(i > 0)
                            stream.Write(buffer, 0, i);
                    }
                }
                else
                    stream.WriteString(valueString);

#if !PCL
                if(valueFile != null)
                    valueStream.Close();
#endif

                stream.WriteString("\r\n");
            }
            stream.WriteString("--" + boundary + "--");
        }
#endif

#if !WP
        private const DecompressionMethods CompressionType = DecompressionMethods.GZip | DecompressionMethods.Deflate;
#endif

#if !(PCL || WIN_RT || WP)
        internal static HttpWebResponse HttpGet(Uri url, string authorizationHeader, ConnectionOptions options)
        {
            if(options == null) options = new ConnectionOptions();
            var req = (HttpWebRequest)WebRequest.Create(url);
            req.Timeout = options.Timeout;
            req.ReadWriteTimeout = options.ReadWriteTimeout;
            req.UserAgent = options.UserAgent;
            req.Proxy = options.Proxy;
            req.Headers.Add(HttpRequestHeader.Authorization, authorizationHeader);
            if(options.UseCompression)
                req.AutomaticDecompression = CompressionType;
            if (options.DisableKeepAlive)
                req.KeepAlive = false;
            options.BeforeRequestAction?.Invoke(req);
            return (HttpWebResponse)req.GetResponse();
        }

        internal static HttpWebResponse HttpPost(Uri url, IEnumerable<KeyValuePair<string, object>> prm, string authorizationHeader, ConnectionOptions options)
        {
            if(prm == null) prm = new Dictionary<string,object>();
            if(options == null) options = new ConnectionOptions();
            var data = Encoding.UTF8.GetBytes(CreateQueryString(prm));
            var req = (HttpWebRequest)WebRequest.Create(url);
            req.ServicePoint.Expect100Continue = false;
            req.Method = "POST";
            req.Timeout = options.Timeout;
            req.ReadWriteTimeout = options.ReadWriteTimeout;
            req.UserAgent = options.UserAgent;
            req.Proxy = options.Proxy;
            req.ContentType = "application/x-www-form-urlencoded";
            req.ContentLength = data.Length;
            req.Headers.Add(HttpRequestHeader.Authorization, authorizationHeader);
            if(options.UseCompression)
                req.AutomaticDecompression = CompressionType;
            if (options.DisableKeepAlive)
                req.KeepAlive = false;
            options.BeforeRequestAction?.Invoke(req);
            using(var reqstr = req.GetRequestStream())
                reqstr.Write(data, 0, data.Length);
            return (HttpWebResponse)req.GetResponse();
        }

        internal static HttpWebResponse HttpPostWithMultipartFormData(Uri url, IEnumerable<KeyValuePair<string, object>> prm, string authorizationHeader, ConnectionOptions options)
        {
            if(options == null) options = new ConnectionOptions();
            var boundary = Guid.NewGuid().ToString();
            var req = (HttpWebRequest)WebRequest.Create(url);
            req.ServicePoint.Expect100Continue = false;
            req.Method = "POST";
            req.Timeout = options.Timeout;
            req.ReadWriteTimeout = options.ReadWriteTimeout;
            req.UserAgent = options.UserAgent;
            req.Proxy = options.Proxy;
            req.ContentType = "multipart/form-data;boundary=" + boundary;
            req.Headers.Add(HttpRequestHeader.Authorization, authorizationHeader);
            req.SendChunked = true;
            if(options.UseCompression)
                req.AutomaticDecompression = CompressionType;
            if (options.DisableKeepAlive)
                req.KeepAlive = false;
            options.BeforeRequestAction?.Invoke(req);
            using(var reqstr = req.GetRequestStream())
                WriteMultipartFormData(reqstr, boundary, prm);
            return (HttpWebResponse)req.GetResponse();
        }
#endif

        /// <summary>
        /// Generates the signature.
        /// </summary>
        /// <param name="t">The tokens.</param>
        /// <param name="httpMethod">The HTTP method.</param>
        /// <param name="url">The URL.</param>
        /// <param name="prm">The parameters.</param>
        /// <returns>The signature.</returns>
        internal static string GenerateSignature(Tokens t, MethodType httpMethod, Uri url, IEnumerable<KeyValuePair<string, string>> prm)
        {
            var key = Encoding.UTF8.GetBytes(
                string.Format("{0}&{1}", UrlEncode(t.ConsumerSecret),
                              UrlEncode(t.AccessTokenSecret)));
            var prmstr = prm.Select(x => new KeyValuePair<string, string>(UrlEncode(x.Key), UrlEncode(x.Value)))
                .Concat(
                    url.Query.TrimStart('?').Split(new[] { '&' }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(x =>
                        {
                            var s = x.Split('=');
                            return new KeyValuePair<string, string>(s[0], s[1]);
                        })
                )
                .OrderBy(x => x.Key).ThenBy(x => x.Value)
                .Select(x => x.Key + "=" + x.Value)
                .JoinToString("&");
            var msg = Encoding.UTF8.GetBytes(
                string.Format("{0}&{1}&{2}",
                    httpMethod.ToString().ToUpperInvariant(),
                    UrlEncode(url.GetComponents(UriComponents.Scheme | UriComponents.UserInfo | UriComponents.Host | UriComponents.Port | UriComponents.Path, UriFormat.UriEscaped)),
                    UrlEncode(prmstr)
                ));
            return Convert.ToBase64String(SecurityUtils.HmacSha1(key, msg));
        }

        /// <summary>
        /// Generates the parameters.
        /// </summary>
        /// <param name="consumerKey">The consumer key.</param>
        /// <param name="token">The token.</param>
        /// <returns>The parameters.</returns>
        internal static Dictionary<string, string> GenerateParameters(string consumerKey, string token)
        {
            var ret = new Dictionary<string, string>() {
                {"oauth_consumer_key", consumerKey},
                {"oauth_signature_method", "HMAC-SHA1"},
                {"oauth_timestamp", ((DateTimeOffset.UtcNow - InternalUtils.unixEpoch).Ticks / 10000000L).ToString("D")},
                {"oauth_nonce", new Random().Next(int.MinValue, int.MaxValue).ToString("X")},
                {"oauth_version", "1.0"}
            };
            if(!string.IsNullOrEmpty(token))
                ret.Add("oauth_token", token);
            return ret;
        }

        /// <summary>
        /// Encodes the specified text.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>The encoded text.</returns>
        internal static string UrlEncode(string text)
        {
            if(string.IsNullOrEmpty(text))
                return "";
            return Encoding.UTF8.GetBytes(text)
                .Select(x => x < 0x80 && "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_.~"
                        .Contains(((char)x).ToString()) ? ((char)x).ToString() : ('%' + x.ToString("X2")))
                .JoinToString();
        }
    }
}

