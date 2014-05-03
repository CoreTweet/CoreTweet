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
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CoreTweet
{
    partial class Request
    {
        private static void DelayAction(int timeout, CancellationToken cancellationToken, Action action)
        {
            var timer = new Timer(_ => action(), null, timeout, Timeout.Infinite);
            cancellationToken.Register(timer.Dispose);
        }

        /// <summary>
        /// Sends a GET request.
        /// </summary>
        /// <returns>The response.</returns>
        /// <param name="url">URL.</param>
        /// <param name="prm">Parameters.</param>
        /// <param name="authorizationHeader">String of OAuth header.</param>
        /// <param name="userAgent">User-Agent header.</param>
        /// <param name="proxy">Proxy information for the request.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        internal static Task<HttpWebResponse> HttpGetAsync(string url, IEnumerable<KeyValuePair<string, object>> prm, string authorizationHeader, ConnectionOptions options, CancellationToken cancellationToken)
        {
            var task = new TaskCompletionSource<HttpWebResponse>();
            if(cancellationToken.IsCancellationRequested)
            {
                task.TrySetCanceled();
                return task.Task;
            }

            try
            {
                if(prm == null) prm = new Dictionary<string, object>();
                if(options == null) options = new ConnectionOptions();
                var req = (HttpWebRequest)WebRequest.Create(url + '?' + CreateQueryString(prm));

                var reg = cancellationToken.Register(() =>
                {
                    task.TrySetCanceled();
                    req.Abort();
                });

#if !PCL                
                req.ReadWriteTimeout = options.ReadWriteTimeout;
                req.UserAgent = options.UserAgent;
                req.Proxy = options.Proxy;
#endif
                req.Headers[HttpRequestHeader.Authorization] = authorizationHeader;
                if(options.BeforeRequestAction != null) options.BeforeRequestAction(req);

                var timeoutCancellation = new CancellationTokenSource();
                DelayAction(options.Timeout, timeoutCancellation.Token, () =>
                {
#if !PCL //If PCL, Abort will throw RequestCanceled
                    task.TrySetException(new WebException("Timeout", WebExceptionStatus.Timeout));
#endif
                    req.Abort();
                });
                req.BeginGetResponse(ar =>
                {
                    timeoutCancellation.Cancel();
                    reg.Dispose();
                    try
                    {
                        task.TrySetResult((HttpWebResponse)req.EndGetResponse(ar));
                    }
                    catch(Exception ex)
                    {
                        task.TrySetException(ex);
                    }
                }, null);
            }
            catch(Exception ex)
            {
                task.TrySetException(ex);
            }

            return task.Task;
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
        /// <param name="cancellationToken">Cancellation token.</param>
        internal static Task<HttpWebResponse> HttpPostAsync(string url, IEnumerable<KeyValuePair<string, object>> prm, string authorizationHeader, ConnectionOptions options, CancellationToken cancellationToken)
        {
            var task = new TaskCompletionSource<HttpWebResponse>();
            if(cancellationToken.IsCancellationRequested)
            {
                task.TrySetCanceled();
                return task.Task;
            }

            try
            {
                if(options == null) options = new ConnectionOptions();
                if(prm == null) prm = new Dictionary<string, object>();
                var data = Encoding.UTF8.GetBytes(CreateQueryString(prm));
                var req = (HttpWebRequest)WebRequest.Create(url);

                var reg = cancellationToken.Register(() =>
                {
                    task.TrySetCanceled();
                    req.Abort();
                });

                Configure100Continue(req);
                req.Method = "POST";
#if !PCL
                req.ReadWriteTimeout = options.ReadWriteTimeout;
                req.UserAgent = options.UserAgent;
                req.Proxy = options.Proxy;
#endif
                req.ContentType = "application/x-www-form-urlencoded";
                req.Headers[HttpRequestHeader.Authorization] = authorizationHeader;
#if !PCL
                req.ContentLength = data.LongLength;
#endif
                if(options.BeforeRequestAction != null) options.BeforeRequestAction(req);

                var timeoutCancellation = new CancellationTokenSource();
                DelayAction(options.Timeout, timeoutCancellation.Token, () =>
                {
#if !PCL
                    task.TrySetException(new WebException("Timeout", WebExceptionStatus.Timeout));
#endif
                    req.Abort();
                });
                req.BeginGetRequestStream(reqStrAr =>
                {
                    try
                    {
                        using(var stream = req.EndGetRequestStream(reqStrAr))
                            stream.Write(data, 0, data.Length);

                        req.BeginGetResponse(resAr =>
                        {
                            timeoutCancellation.Cancel();
                            reg.Dispose();
                            try
                            {
                                task.TrySetResult((HttpWebResponse)req.EndGetResponse(resAr));
                            }
                            catch(Exception ex)
                            {
                                task.TrySetException(ex);
                            }
                        }, null);
                    }
                    catch(Exception ex)
                    {
                        task.TrySetException(ex);
                    }
                }, null);
            }
            catch(Exception ex)
            {
                task.TrySetException(ex);
            }

            return task.Task;
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
        /// <param name="cancellationToken">Cancellation token.</param>
        internal static Task<HttpWebResponse> HttpPostWithMultipartFormDataAsync(string url, IEnumerable<KeyValuePair<string, object>> prm, string authorizationHeader, ConnectionOptions options, CancellationToken cancellationToken)
        {
            var task = new TaskCompletionSource<HttpWebResponse>();
            if(cancellationToken.IsCancellationRequested)
            {
                task.TrySetCanceled();
                return task.Task;
            }

            try
            {
                if(options == null) options = new ConnectionOptions();
                var boundary = Guid.NewGuid().ToString();
                var req = (HttpWebRequest)WebRequest.Create(url);

                var reg = cancellationToken.Register(() =>
                {
                    task.TrySetCanceled();
                    req.Abort();
                });

                Configure100Continue(req);
                req.Method = "POST";
#if !PCL
                req.ReadWriteTimeout = options.ReadWriteTimeout;
                req.UserAgent = options.UserAgent;
                req.Proxy = options.Proxy;
#endif
                req.ContentType = "multipart/form-data;boundary=" + boundary;
                req.Headers[HttpRequestHeader.Authorization] = authorizationHeader;
                if(options.BeforeRequestAction != null) options.BeforeRequestAction(req);

                var timeoutCancellation = new CancellationTokenSource();
                DelayAction(options.Timeout, timeoutCancellation.Token, () =>
                {
#if !PCL
                    task.TrySetException(new WebException("Timeout", WebExceptionStatus.Timeout));
#endif
                    req.Abort();
                });
                req.BeginGetRequestStream(reqStrAr =>
                {
                    try
                    {
                        using(var stream = req.EndGetRequestStream(reqStrAr))
                            WriteMultipartFormData(stream, boundary, prm);

                        req.BeginGetResponse(resAr =>
                        {
                            timeoutCancellation.Cancel();
                            reg.Dispose();
                            try
                            {
                                task.TrySetResult((HttpWebResponse)req.EndGetResponse(resAr));
                            }
                            catch(Exception ex)
                            {
                                task.TrySetException(ex);
                            }
                        }, null);
                    }
                    catch(Exception ex)
                    {
                        task.TrySetException(ex);
                    }
                }, null);
            }
            catch(Exception ex)
            {
                task.TrySetException(ex);
            }

            return task.Task;
        }

        private static void Configure100Continue(HttpWebRequest req)
        {
#if PCL
            try
            {
                var servicePointProp = req.GetType().GetProperty("ServicePoint");
                var servicePoint = servicePointProp.GetValue(req, null);
                var expect100ContinueProp = servicePointProp.PropertyType.GetProperty("Expect100Continue");
                expect100ContinueProp.SetValue(servicePoint, false, null);
            }
            catch { }
#else
            req.ServicePoint.Expect100Continue = false;
#endif
        }
    }
}
