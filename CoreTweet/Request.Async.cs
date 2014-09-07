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
using Windows.Web.Http.Filters;
using Windows.Web.Http.Headers;
#endif

#if WIN_RT
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Storage;
using Windows.Storage.Streams;
#endif

namespace CoreTweet
{
    /// <summary>
    /// Represents an asynchronous response.
    /// </summary>
    public class AsyncResponse : IDisposable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CoreTweet.AsyncResponse"/> class with a specified source.
        /// </summary>
        /// <param name="source"></param>
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
            this.Headers = source.Headers.ToDictionary(kvp => kvp.Key.ToLowerInvariant(), kvp => kvp.Value.FirstOrDefault());
#elif WIN_RT
            this.Headers = source.Headers;
#else
            this.Headers = source.Headers.AllKeys.ToDictionary(k => k.ToLowerInvariant(), k => source.Headers[k]);
#endif
        }

        /// <summary>
        /// Gets the source of the response.
        /// </summary>
#if WIN_RT
        public HttpResponseMessage Source { get; private set; }
#elif PCL
        private HttpWebResponse Source { get; set; }
#else
        public HttpWebResponse Source { get; private set; }
#endif

        /// <summary>
        /// Gets the status code of the response.
        /// </summary>
        public int StatusCode { get; private set; }

        /// <summary>
        /// Gets the headers of the response.
        /// </summary>
        public IDictionary<string, string> Headers { get; private set; }

        /// <summary>
        /// Gets the stream that is used to read the body of the response from the server.
        /// </summary>
        /// <exception cref="System.PlatformNotSupportedException">The platform only supports asynchronous methods.</exception>
        /// <returns>A <see cref="System.IO.Stream"/> containing the body of the response.</returns>
        public Stream GetResponseStream()
        {
#if WIN_RT
            throw new PlatformNotSupportedException();
#else
            return this.Source.GetResponseStream();
#endif
        }

        /// <summary>
        /// Gets the stream that is used to read the body of the response from the server as an asynchronous operation.
        /// </summary>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the <see cref="System.IO.Stream"/> containing the body of the response.</para>
        /// </returns>
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

        /// <summary>
        /// Closes the stream and releases all the resources.
        /// </summary>
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

        /// <summary>
        /// Closes the stream and releases all the resources.
        /// </summary>
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
            if(timeout == Timeout.Infinite) return;
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
        private static async Task<AsyncResponse> ExecuteRequest(HttpRequestMessage req, string authorizationHeader, ConnectionOptions options, CancellationToken cancellationToken)
        {
            var splitAuth = authorizationHeader.Split(new[] { ' ' }, 2);
            req.Headers.Add("User-Agent", options.UserAgent);
#if WIN8
            req.Headers.ExpectContinue = false;
            req.Headers.Authorization = new AuthenticationHeaderValue(splitAuth[0], splitAuth[1]);
            if (options.DisableKeepAlive)
                req.Headers.ConnectionClose = true;
#else
            req.Headers.Expect.Clear();
            req.Headers.Authorization = new HttpCredentialsHeaderValue(splitAuth[0], splitAuth[1]);
            if (options.DisableKeepAlive)
            {
                req.Headers.Connection.Clear();
                req.Headers.Connection.Add(new HttpConnectionOptionHeaderValue("close"));
            }
#endif
            if(options.BeforeRequestAction != null)
                options.BeforeRequestAction(req);
            var cancellation = new CancellationTokenSource();
#if WIN8
            var handler = new HttpClientHandler();
            if(options.UseCompression)
                handler.AutomaticDecompression = CompressionType;
#else
            var handler = new HttpBaseProtocolFilter();
            handler.AutomaticDecompression = options.UseCompression;
#endif
            using(var reg = cancellationToken.Register(cancellation.Cancel))
            using(var client = new HttpClient(handler))
            {
#if WIN8
                var task = client.SendAsync(req, HttpCompletionOption.ResponseHeadersRead, cancellation.Token);
#else
                var task = client.SendRequestAsync(req, HttpCompletionOption.ResponseHeadersRead).AsTask(cancellation.Token);
#endif
                var timeoutCancellation = new CancellationTokenSource();
                DelayAction(options.Timeout, timeoutCancellation.Token, cancellation.Cancel);
                return await task.ContinueWith(t =>
                {
                    timeoutCancellation.Cancel();
                    if (t.IsFaulted)
                        throw t.Exception.InnerException;
                    if (!cancellationToken.IsCancellationRequested && cancellation.IsCancellationRequested)
                        throw new TimeoutException();
                    return new AsyncResponse(t.Result);
                }, cancellationToken).ConfigureAwait(false);
            }
        }
#endif

        /// <summary>
        /// Sends a GET request as an asynchronous operation.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="prm">The parameters.</param>
        /// <param name="authorizationHeader">The OAuth header.</param>
        /// <param name="options">The connection options for the request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the response.</para>
        /// </returns>
        internal static Task<AsyncResponse> HttpGetAsync(string url, IEnumerable<KeyValuePair<string, object>> prm, string authorizationHeader, ConnectionOptions options, CancellationToken cancellationToken)
        {
            if(options == null) options = new ConnectionOptions();
            if(prm == null) prm = new Dictionary<string, object>();
            var reqUrl = url + '?' + CreateQueryString(prm);

#if WIN_RT
            var req = new HttpRequestMessage(HttpMethod.Get, new Uri(reqUrl));
            return ExecuteRequest(req, authorizationHeader, options, cancellationToken);
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

#if !(PCL || WP)
                req.ReadWriteTimeout = options.ReadWriteTimeout;
                req.Proxy = options.Proxy;
                if(options.UseCompression)
                    req.AutomaticDecompression = CompressionType;
                if (options.DisableKeepAlive)
                    req.KeepAlive = false;
#endif
                req.Headers[HttpRequestHeader.Authorization] = authorizationHeader;
#if !PCL
                req.UserAgent = options.UserAgent;
                if(options.BeforeRequestAction != null) options.BeforeRequestAction(req);
#endif

                var timeoutCancellation = new CancellationTokenSource();
                DelayAction(options.Timeout, timeoutCancellation.Token, () =>
                {
#if PCL
                    task.TrySetException(new TimeoutException());
#else
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
        /// Sends a POST request as an asynchronous operation.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="prm">The parameters.</param>
        /// <param name="authorizationHeader">The OAuth header.</param>
        /// <param name="options">The connection options for the request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the response.</para>
        /// </returns>
        internal static Task<AsyncResponse> HttpPostAsync(string url, IEnumerable<KeyValuePair<string, object>> prm, string authorizationHeader, ConnectionOptions options, CancellationToken cancellationToken)
        {
            if(options == null) options = new ConnectionOptions();
            if(prm == null) prm = new Dictionary<string, object>();

#if WIN_RT
            var req = new HttpRequestMessage(HttpMethod.Post, new Uri(url));
            req.Content =
#if WIN8
                new FormUrlEncodedContent(
#else
                new HttpFormUrlEncodedContent(
#endif
                    prm.Select(kvp =>new KeyValuePair<string, string>(kvp.Key, kvp.Value.ToString()))
                );
            return ExecuteRequest(req, authorizationHeader, options, cancellationToken);
#else
            var task = new TaskCompletionSource<AsyncResponse>();
            if(cancellationToken.IsCancellationRequested)
            {
                task.TrySetCanceled();
                return task.Task;
            }

            try
            {
                var req = (HttpWebRequest)WebRequest.Create(url);

                var reg = cancellationToken.Register(() =>
                {
                    task.TrySetCanceled();
                    req.Abort();
                });

                req.Method = "POST";
                req.ContentType = "application/x-www-form-urlencoded";
                req.Headers[HttpRequestHeader.Authorization] = authorizationHeader;
#if !(PCL || WP)
                req.ServicePoint.Expect100Continue = false;
                req.ReadWriteTimeout = options.ReadWriteTimeout;
                req.Proxy = options.Proxy;
                if(options.UseCompression)
                    req.AutomaticDecompression = CompressionType;
                if (options.DisableKeepAlive)
                    req.KeepAlive = false;
#endif
#if !PCL
                req.UserAgent = options.UserAgent;
                if (options.BeforeRequestAction != null) options.BeforeRequestAction(req);
#endif

                var timeoutCancellation = new CancellationTokenSource();
                DelayAction(options.Timeout, timeoutCancellation.Token, () =>
                {
#if PCL
                    task.TrySetException(new TimeoutException());
#else
                    task.TrySetException(new WebException("Timeout", WebExceptionStatus.Timeout));
#endif
                    req.Abort();
                });
                req.BeginGetRequestStream(reqStrAr =>
                {
                    try
                    {
                        var data = Encoding.UTF8.GetBytes(CreateQueryString(prm));
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
        /// Sends a POST request with multipart/form-data as an asynchronous operation.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="prm">The parameters.</param>
        /// <param name="authorizationHeader">The OAuth header.</param>
        /// <param name="options">The connection options for the request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the response.</para>
        /// </returns>
        internal static
#if WIN_RT
        async
#endif
        Task<AsyncResponse> HttpPostWithMultipartFormDataAsync(string url, IEnumerable<KeyValuePair<string, object>> prm, string authorizationHeader, ConnectionOptions options, CancellationToken cancellationToken)
        {
            if(options == null) options = new ConnectionOptions();

#if WIN_RT
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
            return await ExecuteRequest(req, authorizationHeader, options, cancellationToken).ConfigureAwait(false);
#else
            var task = new TaskCompletionSource<AsyncResponse>();
            if(cancellationToken.IsCancellationRequested)
            {
                task.TrySetCanceled();
                return task.Task;
            }

            try
            {
                var boundary = Guid.NewGuid().ToString();
                var req = (HttpWebRequest)WebRequest.Create(url);

                var reg = cancellationToken.Register(() =>
                {
                    task.TrySetCanceled();
                    req.Abort();
                });

                req.Method = "POST";
#if !(PCL || WP)
                req.ServicePoint.Expect100Continue = false;
                req.ReadWriteTimeout = options.ReadWriteTimeout;
                req.Proxy = options.Proxy;
                req.SendChunked = true;
                if(options.UseCompression)
                    req.AutomaticDecompression = CompressionType;
                if (options.DisableKeepAlive)
                    req.KeepAlive = false;
#endif
#if !PCL
                req.UserAgent = options.UserAgent;
#endif
                req.ContentType = "multipart/form-data;boundary=" + boundary;
                req.Headers[HttpRequestHeader.Authorization] = authorizationHeader;
#if !PCL
                if(options.BeforeRequestAction != null) options.BeforeRequestAction(req);
#endif

                var timeoutCancellation = new CancellationTokenSource();
                DelayAction(options.Timeout, timeoutCancellation.Token, () =>
                {
#if PCL
                    task.TrySetException(new TimeoutException());
#else
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
    }
}
