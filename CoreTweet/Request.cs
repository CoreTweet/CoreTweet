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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using CoreTweet.Core;

/// <summary>
/// The twitter library.
/// </summary>
namespace CoreTweet
{
    /// <summary>
    /// The type of the HTTP method.
    /// </summary>
    public enum MethodType
    {
        /// <summary>
        /// GET method.
        /// </summary>
        Get,
        /// <summary>
        /// POST method.
        /// </summary>
        Post
    }

    /// <summary>
    /// Sends a request to Twitter and some other web services.
    /// </summary>
    internal static partial class Request
    {
        private static string CreateQueryString(IEnumerable<KeyValuePair<string, object>> prm)
        {
            return prm.Select(x => Rfc3986EscapeDataString(x.Key) + "=" + Rfc3986EscapeDataString(x.Value.ToString())).JoinToString("&");
        }

#if !WIN_RT
        private static void WriteMultipartFormData(Stream stream, string boundary, IEnumerable<KeyValuePair<string, object>> prm)
        {
            prm.ForEach(x =>
            {
                var valueStream = x.Value as Stream;
                var valueBytes = x.Value as IEnumerable<byte>;
#if !PCL
                var valueFile = x.Value as FileInfo;
#endif
                var valueString = x.Value.ToString();

                stream.WriteString("--" + boundary + "\r\n");
                if(valueStream != null || valueBytes != null
#if !PCL
                    || valueFile != null
#endif
                   )
                {
                    stream.WriteString("Content-Type: application/octet-stream\r\n");
                }
                stream.WriteString(String.Format(@"Content-Disposition: form-data; name=""{0}""", x.Key));
#if !PCL
                if(valueFile != null)
                    stream.WriteString(String.Format(@"; filename=""{0}""", valueFile.Name));
                else
#endif
                if(valueStream != null || valueBytes != null)
                    stream.WriteString(@"; filename=""file""");
                stream.WriteString("\r\n\r\n");

#if !PCL
                if(valueFile != null)
                    valueStream = valueFile.OpenRead();
#endif
                if(valueStream != null)
                {
                    while (true)
                    {
                        var buffer = new byte[4096];
                        var count = valueStream.Read(buffer, 0, buffer.Length);
                        if (count == 0) break;
                        stream.Write(buffer, 0, count);
                    }
                }
                else if(valueBytes != null)
                    valueBytes.ForEach(b => stream.WriteByte(b));
                else
                    stream.WriteString(valueString);

#if !PCL
                if(valueFile != null)
                    valueStream.Close();
#endif

                stream.WriteString("\r\n");
            });
            stream.WriteString("--" + boundary + "--");
        }
#endif

#if !(PCL || WIN_RT || WP)
        /// <summary>
        /// Sends a GET request.
        /// </summary>
        /// <returns>The response.</returns>
        /// <param name="url">URL.</param>
        /// <param name="prm">Parameters.</param>
        /// <param name="authorizationHeader">String of OAuth header.</param>
        /// <param name="userAgent">User-Agent header.</param>
        /// <param name="proxy">Proxy information for the request.</param>
        internal static HttpWebResponse HttpGet(string url, IEnumerable<KeyValuePair<string, object>> prm, string authorizationHeader, ConnectionOptions options)
        {
            if(prm == null) prm = new Dictionary<string,object>();
            if(options == null) options = new ConnectionOptions();
            var req = (HttpWebRequest)WebRequest.Create(url + '?' + CreateQueryString(prm));
            req.Timeout = options.Timeout;
            req.ReadWriteTimeout = options.ReadWriteTimeout;
            req.UserAgent = options.UserAgent;
            req.Proxy = options.Proxy;
            req.Headers.Add(HttpRequestHeader.Authorization, authorizationHeader);
            if(options.BeforeRequestAction != null) options.BeforeRequestAction(req);
            return (HttpWebResponse)req.GetResponse();
        }

        /// <summary>
        /// Sends a POST request.
        /// </summary>
        /// <returns>The response.</returns>
        /// <param name="url">URL.</param>
        /// <param name="prm">Parameters.</param>
        /// <param name="authorizationHeader">String of OAuth header.</param>
        /// <param name="userAgent">User-Agent header.</param>
        /// <param name="proxy">Proxy information for the request.</param>
        internal static HttpWebResponse HttpPost(string url, IEnumerable<KeyValuePair<string, object>> prm, string authorizationHeader, ConnectionOptions options)
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
            if(options.BeforeRequestAction != null) options.BeforeRequestAction(req);
            using(var reqstr = req.GetRequestStream())
                reqstr.Write(data, 0, data.Length);
            return (HttpWebResponse)req.GetResponse();
        }

        /// <summary>
        /// Sends a POST request with multipart/form-data.
        /// </summary>
        /// <returns>The response.</returns>
        /// <param name="url">URL.</param>
        /// <param name="prm">Parameters.</param>
        /// <param name="authorizationHeader">String of OAuth header.</param>
        /// <param name="userAgent">User-Agent header.</param>
        /// <param name="proxy">Proxy information for the request.</param>
        internal static HttpWebResponse HttpPostWithMultipartFormData(string url, IEnumerable<KeyValuePair<string, object>> prm, string authorizationHeader, ConnectionOptions options)
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
            using(var memstr = new MemoryStream())
            {
                WriteMultipartFormData(memstr, boundary, prm);
                req.ContentLength = memstr.Length;
                if(options.BeforeRequestAction != null) options.BeforeRequestAction(req);

                using(var reqstr = req.GetRequestStream())
                    memstr.WriteTo(reqstr);
            }
            return (HttpWebResponse)req.GetResponse();
        }
#endif

        /// <summary>
        /// Generates the signature.
        /// </summary>
        /// <returns>The signature.</returns>
        /// <param name="t">Tokens.</param>
        /// <param name="httpMethod">The http method.</param>
        /// <param name="url">the URL.</param>
        /// <param name="prm">Parameters.</param>
        internal static string GenerateSignature(Tokens t, string httpMethod, string url, IEnumerable<KeyValuePair<string, string>> prm)
        {
            var key = Encoding.UTF8.GetBytes(
                string.Format("{0}&{1}", UrlEncode(t.ConsumerSecret),
                              UrlEncode(t.AccessTokenSecret) ?? ""));
            var uri = new Uri(url);
            var msg = System.Text.Encoding.UTF8.GetBytes(
                string.Format("{0}&{1}&{2}", httpMethod,
                    UrlEncode(string.Format("{0}://{1}{2}", uri.Scheme, uri.Host, uri.AbsolutePath)),
                    UrlEncode(prm.OrderBy(x => x.Key)
                        .ThenBy(x => x.Value)
                        .Select(x => string.Format("{0}={1}", UrlEncode(x.Key), UrlEncode(x.Value)))
                        .JoinToString("&")
                    )
                ));
            return Convert.ToBase64String(SecurityUtils.HmacSha1(key, msg));
        }

        /// <summary>
        /// Generates the parameters.
        /// </summary>
        /// <returns>The parameters.</returns>
        /// <param name="consumerKey">Consumer key.</param>
        /// <param name="token">Token.</param>
        internal static Dictionary<string, string> GenerateParameters(string consumerKey, string token)
        {
            var ret = new Dictionary<string, string>() {
                {"oauth_consumer_key", consumerKey},
                {"oauth_signature_method", "HMAC-SHA1"},
                {"oauth_timestamp", ((DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)).Ticks
                    / 10000000L).ToString("D")},
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
        /// <returns>The encoded text.</returns>
        /// <param name="text">Text.</param>
        internal static string UrlEncode(string text)
        {
            if(string.IsNullOrEmpty(text))
                return "";
            return Encoding.UTF8.GetBytes(text)
                .Select(x => x < 0x80 && "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_.~"
                        .Contains(((char)x).ToString()) ? ((char)x).ToString() : ('%' + x.ToString("X2")))
                .JoinToString();
        }

        /// <summary>
        /// Encodes the given string based on RFC3986
        /// </summary>
        /// <param name="text">string value.</param>
        /// <returns>The encodes text.</returns>
        internal static string Rfc3986EscapeDataString(string text)
        {
#if NET45
            return Uri.EscapeDataString(text);      
#else
            text = Uri.EscapeDataString(text);
            foreach(var x in "()!*'")
            {
#if (PCL || WIN_RT || WP)
                text = text.Replace(x.ToString(), '%' + ((byte)x).ToString("X2"));
#else
                text = text.Replace(x.ToString(), Uri.HexEscape(x));
#endif
            }
            return text;
#endif 
        }

    }
}

