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
using System.Threading;
using System.Threading.Tasks;

#if WIN8
using System.Net.Http;
using System.Net.Http.Headers;
#elif WIN_RT
using Windows.Web.Http;
using Windows.Web.Http.Headers;
#endif

#if WIN_RT
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Storage;
using Windows.Storage.Streams;
#endif

namespace CoreTweet
{
    public class AsyncResponse : IDisposable
    {
#if WIN_RT
        public AsyncResponse(HttpResponseMessage source)
#elif PCL
        internal AsyncResponse(HttpWebResponse source)
#else
        public AsyncResponse(HttpWebResponse source)
#endif
        {
            this.Source = source;
            this.StatusCode = (int)source.StatusCode;
#if WIN8
            this.Headers = source.Headers.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.FirstOrDefault());
#elif WIN_RT
            this.Headers = source.Headers;
#else
            this.Headers = source.Headers.AllKeys.ToDictionary(k => k, k => source.Headers[k]);
#endif
        }

#if WIN_RT
        public HttpResponseMessage Source { get; private set; }
#elif PCL
        private HttpWebResponse Source { get; set; }
#else
        public HttpWebResponse Source { get; private set; }
#endif

        public int StatusCode { get; private set; }

        public IDictionary<string, string> Headers { get; private set; }

        public Stream GetResponseStream()
        {
#if WIN_RT
            throw new PlatformNotSupportedException();
#else
            return this.Source.GetResponseStream();
#endif
        }

#if WIN_RT && !WIN8
        public async Task<Stream> GetResponseStreamAsync()
        {
            return (await this.Source.Content.ReadAsInputStreamAsync()).AsStreamForRead(0);
        }
#else
        public Task<Stream> GetResponseStreamAsync()
        {
#if WIN8
            return this.Source.Content.ReadAsStreamAsync();
#else
            return Task.Factory.StartNew(() => this.Source.GetResponseStream());
#endif
        }
#endif

        protected virtual void Dispose(bool disposing)
        {
            if(disposing)
            {
                if(this.Source != null)
                {
#if PCL || WIN_RT
                    this.Source.Dispose();
#else
                    this.Source.Close();
#endif
                }
                this.Source = null;
                this.Headers = null;
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
    }

    partial class Request
    {
        private static void DelayAction(int timeout, CancellationToken cancellationToken, Action action)
        {
#if WIN8
            var timer = Windows.System.Threading.ThreadPoolTimer.CreateTimer(
                _ => action(),
                TimeSpan.FromMilliseconds(timeout)
            );
            cancellationToken.Register(timer.Cancel);
#else
            var timer = new Timer(_ => action(), null, timeout, Timeout.Infinite);
            cancellationToken.Register(timer.Dispose);
#endif
        }

#if WIN_RT
        private static Task<AsyncResponse> ExecuteRequest(HttpClient client, HttpRequestMessage req, string authorizationHeader, ConnectionOptions options, CancellationToken cancellationToken)
        {
            var splitAuth = authorizationHeader.Split(new[] { ' ' }, 2);
            req.Headers.Add("User-Agent", options.UserAgent);
#if WIN8
            req.Headers.ExpectContinue = false;
            req.Headers.Authorization = new AuthenticationHeaderValue(splitAuth[0], splitAuth[1]);
#else
            req.Headers.Expect.Clear();
            req.Headers.Authorization = new HttpCredentialsHeaderValue(splitAuth[0], splitAuth[1]);
#endif
            if(options.BeforeRequestAction != null)
                options.BeforeRequestAction(req);
            var cancellation = new CancellationTokenSource();
            var reg = cancellationToken.Register(cancellation.Cancel);
#if WIN8
            var task = client.SendAsync(req, HttpCompletionOption.ResponseHeadersRead, cancellation.Token);
#else
            var task = client.SendRequestAsync(req, HttpCompletionOption.ResponseHeadersRead).AsTask(cancellation.Token);
#endif
            var timeoutCancellation = new CancellationTokenSource();
            DelayAction(options.Timeout, timeoutCancellation.Token, cancellation.Cancel);
            return task.ContinueWith(t =>
            {
                timeoutCancellation.Cancel();
                reg.Dispose();
                if(t.IsFaulted)
                    throw t.Exception.InnerException;
                if(!cancellationToken.IsCancellationRequested && cancellation.IsCancellationRequested)
                    throw new TimeoutException();
                return new AsyncResponse(t.Result);
            }, cancellationToken);
        }
#endif

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
        internal static Task<AsyncResponse> HttpGetAsync(string url, IEnumerable<KeyValuePair<string, object>> prm, string authorizationHeader, ConnectionOptions options, CancellationToken cancellationToken)
        {
            if(options == null) options = new ConnectionOptions();
            if(prm == null) prm = new Dictionary<string, object>();
            var reqUrl = url + '?' + CreateQueryString(prm);

#if WIN_RT
            var client = new HttpClient();
            var req = new HttpRequestMessage(HttpMethod.Get, new Uri(reqUrl));
            req.Headers.Add("Authorization", authorizationHeader);
            return ExecuteRequest(client, req, authorizationHeader, options, cancellationToken);
#else
            var task = new TaskCompletionSource<AsyncResponse>();
            if(cancellationToken.IsCancellationRequested)
            {
                task.TrySetCanceled();
                return task.Task;
            }

            try
            {
                if(prm == null) prm = new Dictionary<string, object>();
                var req = (HttpWebRequest)WebRequest.Create(reqUrl);

                var reg = cancellationToken.Register(() =>
                {
                    task.TrySetCanceled();
                    req.Abort();
                });

#if NET45 || WP
                req.AllowReadStreamBuffering = false;
#endif
#if !(PCL || WP)                
                req.ReadWriteTimeout = options.ReadWriteTimeout;
#endif
#if !PCL
                req.UserAgent = options.UserAgent;
#endif
#if !(PCL || WP)
                req.Proxy = options.Proxy;
#endif
                req.Headers[HttpRequestHeader.Authorization] = authorizationHeader;
#if !PCL
                if(options.BeforeRequestAction != null) options.BeforeRequestAction(req);
#endif

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
                        task.TrySetResult(new AsyncResponse((HttpWebResponse)req.EndGetResponse(ar)));
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
#endif
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
        internal static Task<AsyncResponse> HttpPostAsync(string url, IEnumerable<KeyValuePair<string, object>> prm, string authorizationHeader, ConnectionOptions options, CancellationToken cancellationToken)
        {
            if(options == null) options = new ConnectionOptions();
            if(prm == null) prm = new Dictionary<string, object>();

#if WIN_RT
            var client = new HttpClient();
            var req = new HttpRequestMessage(HttpMethod.Post, new Uri(url));
            req.Content =
#if WIN8
                new FormUrlEncodedContent(
#else
                new HttpFormUrlEncodedContent(
#endif
                    prm.Select(kvp =>new KeyValuePair<string, string>(kvp.Key, kvp.Value.ToString()))
                );
            return ExecuteRequest(client, req, authorizationHeader, options, cancellationToken);
#else
            var task = new TaskCompletionSource<AsyncResponse>();
            if(cancellationToken.IsCancellationRequested)
            {
                task.TrySetCanceled();
                return task.Task;
            }

            try
            {
                var data = Encoding.UTF8.GetBytes(CreateQueryString(prm));
                var req = (HttpWebRequest)WebRequest.Create(url);

                var reg = cancellationToken.Register(() =>
                {
                    task.TrySetCanceled();
                    req.Abort();
                });

#if NET45|| WP
                req.AllowReadStreamBuffering = false;
#endif
                req.Method = "POST";
                req.ContentType = "application/x-www-form-urlencoded";
                req.Headers[HttpRequestHeader.Authorization] = authorizationHeader;
#if !(PCL || WP)
                req.ServicePoint.Expect100Continue = false;
                req.ReadWriteTimeout = options.ReadWriteTimeout;
#endif
#if !PCL
                req.UserAgent = options.UserAgent;
                req.ContentLength = data.Length;
#endif
#if !(PCL || WP)
                req.Proxy = options.Proxy;
#endif
#if !PCL
                if(options.BeforeRequestAction != null) options.BeforeRequestAction(req);
#endif

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
                                task.TrySetResult(new AsyncResponse((HttpWebResponse)req.EndGetResponse(resAr)));
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
#endif
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
        internal static
#if WIN_RT
        async
#endif
        Task<AsyncResponse> HttpPostWithMultipartFormDataAsync(string url, IEnumerable<KeyValuePair<string, object>> prm, string authorizationHeader, ConnectionOptions options, CancellationToken cancellationToken)
        {
            if(options == null) options = new ConnectionOptions();

#if WIN_RT
            var client = new HttpClient();
            var req = new HttpRequestMessage(HttpMethod.Post, new Uri(url));
#if WIN8
            var content = new MultipartFormDataContent();
#else
            var content = new HttpMultipartFormDataContent();
#endif
            foreach(var x in prm)
            {
                cancellationToken.ThrowIfCancellationRequested();

                var valueStream = x.Value as Stream;
                var valueInputStream = x.Value as IInputStream;
                var valueBytes = x.Value as IEnumerable<byte>;
                var valueBuffer = x.Value as IBuffer;
                var valueInputStreamReference = x.Value as IInputStreamReference;
                var valueStorageItem = x.Value as IStorageItem;

                if(valueInputStreamReference != null)
                    valueInputStream = await valueInputStreamReference.OpenSequentialReadAsync();

#if WIN8
                if(valueInputStream != null)
                    valueStream = valueInputStream.AsStreamForRead();
                if(valueBuffer != null)
                    valueStream = valueBuffer.AsStream();

                if(valueStream != null)
                    content.Add(new StreamContent(valueStream), x.Key, valueStorageItem != null ? valueStorageItem.Name : "file");
                else if(valueBytes != null)
                {
                    var valueByteArray = valueBytes as byte[];
                    if(valueByteArray == null) valueByteArray = valueBytes.ToArray();
                    content.Add(new ByteArrayContent(valueByteArray), x.Key, valueStorageItem != null ? valueStorageItem.Name : "file");
                }
                else
                    content.Add(new StringContent(x.Value.ToString()), x.Key);
#else
                if (valueStream != null)
                    valueInputStream = valueStream.AsInputStream();
                if (valueBytes != null)
                {
                    var valueByteArray = valueBytes as byte[];
                    if (valueByteArray == null) valueByteArray = valueBytes.ToArray();
                    valueBuffer = valueByteArray.AsBuffer();
                }

                if (valueInputStream != null)
                    content.Add(new HttpStreamContent(valueInputStream), x.Key, valueStorageItem != null ? valueStorageItem.Name : "file");
                else if (valueBuffer != null)
                    content.Add(new HttpBufferContent(valueBuffer), x.Key, valueStorageItem != null ? valueStorageItem.Name : "file");
                else
                    content.Add(new HttpStringContent(x.Value.ToString()), x.Key);
#endif
            }
            cancellationToken.ThrowIfCancellationRequested();
            req.Content = content;
            return await ExecuteRequest(client, req, authorizationHeader, options, cancellationToken);
#else
            return Task.Factory.StartNew(() =>
            {
                cancellationToken.ThrowIfCancellationRequested();
                var task = new TaskCompletionSource<AsyncResponse>();

                var boundary = Guid.NewGuid().ToString();
                var req = (HttpWebRequest)WebRequest.Create(url);

                var reg = cancellationToken.Register(() =>
                {
                    task.TrySetCanceled();
                    req.Abort();
                });

#if NET45 || WP
                req.AllowReadStreamBuffering = false;
#endif
                req.Method = "POST";
#if !(PCL || WP)
                req.ServicePoint.Expect100Continue = false;
                req.ReadWriteTimeout = options.ReadWriteTimeout;
#endif
#if !PCL
                req.UserAgent = options.UserAgent;
#endif
#if !(PCL || WP)
                req.Proxy = options.Proxy;
#endif
                req.ContentType = "multipart/form-data;boundary=" + boundary;
                req.Headers[HttpRequestHeader.Authorization] = authorizationHeader;
                cancellationToken.ThrowIfCancellationRequested();
                var memstr = new MemoryStream();
                try
                {
                    WriteMultipartFormData(memstr, boundary, prm);
                    cancellationToken.ThrowIfCancellationRequested();
#if !PCL
                    req.ContentLength = memstr.Length;
                    if(options.BeforeRequestAction != null) options.BeforeRequestAction(req);
#endif

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
                            memstr.Seek(0, SeekOrigin.Begin);
                            using(var stream = req.EndGetRequestStream(reqStrAr))
                                memstr.CopyTo(stream);

                            req.BeginGetResponse(resAr =>
                            {
                                timeoutCancellation.Cancel();
                                reg.Dispose();
                                try
                                {
                                    task.TrySetResult(new AsyncResponse((HttpWebResponse)req.EndGetResponse(resAr)));
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
                        finally
                        {
                            memstr.Dispose();
                        }
                    }, null);

                    return task.Task;
                }
                catch
                {
                    memstr.Dispose();
                    throw;
                }
            }, cancellationToken).CheckCanceled(cancellationToken).Unwrap();
#endif
        }
    }
}
