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
using System.Security.Cryptography;
using System.Text;

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
        Post,
        /// <summary>
        /// POST method without any response.
        /// </summary>
        PostNoResponse
    }

    /// <summary>
    /// Sends a request to Twitter and some other web services.
    /// </summary>
    internal static class Request
    {
        /// <summary>
        /// Sends a GET request.
        /// </summary>
        /// <returns>The response.</returns>
        /// <param name="url">URL.</param>
        /// <param name="prm">Parameters.</param>
        /// <param name="authorizationHeader">String of OAuth header.</param>
        /// <param name="userAgent">User-Agent header.</param>
        /// <param name="proxy">Proxy information for the request.</param>
        internal static Stream HttpGet(string url, IDictionary<string, object> prm, string authorizationHeader, string userAgent, IWebProxy proxy)
        {
            if (prm == null) prm = new Dictionary<string, object>();
            var req = (HttpWebRequest)WebRequest.Create(url + '?' +
                prm.Select(x => UrlEncode(x.Key) + "=" + UrlEncode(x.Value.ToString())).JoinToString("&")
            );
            req.UserAgent = userAgent;
            req.Proxy = proxy;
            req.Headers.Add(HttpRequestHeader.Authorization, authorizationHeader);
            return req.GetResponse().GetResponseStream();
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
        /// <param name="response">If it set false, won't try to get any responses and will return null.</param>
        internal static Stream HttpPost(string url, IDictionary<string, object> prm, string authorizationHeader, string userAgent, IWebProxy proxy, bool response)
        {
            if (prm == null) prm = new Dictionary<string, object>();
            var data = Encoding.UTF8.GetBytes(
                prm.Select(x => UrlEncode(x.Key) + "=" + UrlEncode(x.Value.ToString())).JoinToString("&"));
            var req = (HttpWebRequest)WebRequest.Create(url);
            req.ServicePoint.Expect100Continue = false;
            req.Method = "POST";
            req.UserAgent = userAgent;
            req.Proxy = proxy;
            req.ContentType = "application/x-www-form-urlencoded";
            req.ContentLength = data.Length;
            req.Headers.Add(HttpRequestHeader.Authorization, authorizationHeader);
            using (var reqstr = req.GetRequestStream())
                reqstr.Write(data, 0, data.Length);
            return response ? req.GetResponse().GetResponseStream() : null;
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
        /// <param name="response">If it set false, won't try to get any responses and will return null.</param>
        internal static Stream HttpPostWithMultipartFormData(string url, IDictionary<string, object> prm, string authorizationHeader, string userAgent, IWebProxy proxy, bool response)
        {
            var boundary = Guid.NewGuid().ToString();
            var req = (HttpWebRequest)WebRequest.Create(url);
            req.ServicePoint.Expect100Continue = false;
            req.Method = "POST";
            req.UserAgent = userAgent;
            req.Proxy = proxy;
            req.ContentType = "multipart/form-data;boundary=" + boundary;
            req.Headers.Add(HttpRequestHeader.Authorization, authorizationHeader);
            using (var reqstr = req.GetRequestStream())
            {
                Action<string> writeStr = s =>
                {
                    var bytes = Encoding.UTF8.GetBytes(s);
                    reqstr.Write(bytes, 0, bytes.Length);
                };

                prm.ForEach(x =>
                {
                    var valueStream = x.Value as Stream;
                    var valueBytes = x.Value as IEnumerable<byte>;
                    var valueFile = x.Value as FileInfo;
                    var valueString = x.Value.ToString();

                    writeStr("--" + boundary + "\r\n");
                    if (valueStream != null || valueBytes != null || valueFile != null)
                        writeStr("Content-Type: application/octet-stream\r\n");
                    writeStr(String.Format(@"Content-Disposition: form-data; name=""{0}""", x.Key));
                    if (valueFile != null)
                        writeStr(String.Format(@"; filename=""{0}""", valueFile.Name));
                    else if (valueStream != null || valueBytes != null)
                        writeStr(@"; filename=""file""");
                    writeStr("\r\n\r\n");

                    if (valueFile != null)
                        valueStream = valueFile.OpenRead();
                    if (valueStream != null)
                    {
                        while (true)
                        {
                            var buffer = new byte[4096];
                            var count = valueStream.Read(buffer, 0, buffer.Length);
                            if (count == 0) break;
                            reqstr.Write(buffer, 0, count);
                        }
                    }
                    else if (valueBytes != null)
                        valueBytes.ForEach(b => reqstr.WriteByte(b));
                    else
                        writeStr(valueString);

                    if (valueFile != null)
                        valueStream.Close();

                    writeStr("\r\n");
                });
                writeStr("--" + boundary + "--");
            }
            return response ? req.GetResponse().GetResponseStream() : null;
        }

        /// <summary>
        /// Generates the signature.
        /// </summary>
        /// <returns>The signature.</returns>
        /// <param name="t">Tokens.</param>
        /// <param name="httpMethod">The http method.</param>
        /// <param name="url">the URL.</param>
        /// <param name="prm">Parameters.</param>
        internal static string GenerateSignature(Tokens t, string httpMethod, string url, SortedDictionary<string, string> prm)
        {
            using (var hs1 = new HMACSHA1())
            {
                hs1.Key = Encoding.UTF8.GetBytes(
                    string.Format("{0}&{1}", UrlEncode(t.ConsumerSecret),
                                  UrlEncode(t.AccessTokenSecret) ?? ""));
                var uri = new Uri(url);
                var hash = hs1.ComputeHash(
                    System.Text.Encoding.UTF8.GetBytes(
                    string.Format("{0}&{1}&{2}", httpMethod, UrlEncode(string.Format("{0}://{1}{2}", uri.Scheme, uri.Host, uri.AbsolutePath)),
                                      UrlEncode(prm.Select(x => string.Format("{0}={1}", UrlEncode(x.Key), UrlEncode(x.Value)))
                                         .JoinToString("&")))));
                return Convert.ToBase64String(hash);
            }
        }

        /// <summary>
        /// Generates the parameters.
        /// </summary>
        /// <returns>The parameters.</returns>
        /// <param name="ConsumerKey ">Consumer key.</param>
        /// <param name="token">Token.</param>
        internal static SortedDictionary<string, string> GenerateParameters(string consumerKey, string token)
        {
            var ret = new SortedDictionary<string, string>() {
                {"oauth_consumer_key", consumerKey},
                {"oauth_signature_method", "HMAC-SHA1"},
                {"oauth_timestamp", ((DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)).Ticks
                    / 10000000L).ToString("D")},
                {"oauth_nonce", new Random().Next(int.MinValue, int.MaxValue).ToString("X")},
                {"oauth_version", "1.0"}
            };
            if (!string.IsNullOrEmpty(token))
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
            if (string.IsNullOrEmpty(text))
                return "";
            return Encoding.UTF8.GetBytes(text)
                .Select(x => x < 0x80 && "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_.~"
                        .Contains((char)x) ? ((char)x).ToString() : ('%' + x.ToString("X2")))
                .JoinToString();
        }
    }
}

