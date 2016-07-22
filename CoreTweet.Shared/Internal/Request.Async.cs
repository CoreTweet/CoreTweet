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

#if ASYNC
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CoreTweet.Core;

#if WIN_RT
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.Web.Http;
using Windows.Web.Http.Filters;
using Windows.Web.Http.Headers;
#else
using System.Net.Http;
using System.Net.Http.Headers;
#endif

namespace CoreTweet
{
    /// <summary>
    /// Represents an asynchronous response.
    /// </summary>
    public class AsyncResponse : IDisposable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AsyncResponse"/> class with a specified source.
        /// </summary>
        /// <param name="source"></param>
#if PCL
        internal
#else
        public
#endif
        AsyncResponse(HttpResponseMessage source)
        {
            this.Source = source;
            this.StatusCode = (int)source.StatusCode;
#if WIN_RT
            this.Headers = source.Headers;
#else
            this.Headers = source.Headers.ToDictionary(x => x.Key, x => x.Value.FirstOrDefault(), StringComparer.OrdinalIgnoreCase);
#endif
        }

        /// <summary>
        /// Gets the source of the response.
        /// </summary>
#if PCL
        internal
#else
        public
#endif
        HttpResponseMessage Source { get; }

        /// <summary>
        /// Gets the status code of the response.
        /// </summary>
        public int StatusCode { get; }

        /// <summary>
        /// Gets the headers of the response.
        /// </summary>
        public IDictionary<string, string> Headers { get; }

        /// <summary>
        /// Gets the stream that is used to read the body of the response from the server as an asynchronous operation.
        /// </summary>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the <see cref="System.IO.Stream"/> containing the body of the response.</para>
        /// </returns>
        public
#if WIN_RT
        async
#endif
        Task<Stream> GetResponseStreamAsync()
        {
#if WIN_RT
            return (await this.Source.Content.ReadAsInputStreamAsync().AsTask().ConfigureAwait(false))
                .AsStreamForRead(0);
#else
            return this.Source.Content.ReadAsStreamAsync();
#endif
        }

        /// <summary>
        /// Closes the stream and releases all the resources.
        /// </summary>
        public void Dispose()
        {
            this.Source?.Dispose();
        }
    }

    partial class Request
    {
#if WIN_RT
        private static async Task<AsyncResponse> ExecuteRequest(HttpRequestMessage req, string authorizationHeader, ConnectionOptions options, CancellationToken cancellationToken, IProgress<UploadProgressInfo> progress)
        {
            req.Headers.Add("User-Agent", options.UserAgent);
            req.Headers.Expect.Clear();
            req.Headers.Authorization = HttpCredentialsHeaderValue.Parse(authorizationHeader);
            if(options.DisableKeepAlive)
            {
                req.Headers.Connection.Clear();
                req.Headers.Connection.Add(new HttpConnectionOptionHeaderValue("close"));
            }

            var handler = new HttpBaseProtocolFilter();
            handler.AutomaticDecompression = options.UseCompression;
            handler.UseProxy = options.UseProxy;

            using (var cancellation = new CancellationTokenSource())
            {
                cancellationToken.Register(cancellation.Cancel);
                if (options.Timeout != Timeout.Infinite)
                    cancellation.CancelAfter(options.Timeout);

                var client = new HttpClient(handler);
                var task = client.SendRequestAsync(req, HttpCompletionOption.ResponseHeadersRead).AsTask(
                    cancellation.Token,
                    progress == null ? null : new SimpleProgress<HttpProgress>(x =>
                    {
                        if (x.Stage == HttpProgressStage.SendingContent)
                            progress.Report(new UploadProgressInfo((long)x.BytesSent, (long?)x.TotalBytesToSend));
                    }));
                return new AsyncResponse(await task.ConfigureAwait(false));
            }
        }
#else
        private static async Task<AsyncResponse> ExecuteRequest(HttpRequestMessage req, string authorizationHeader, ConnectionOptions options, CancellationToken cancellationToken, IProgress<UploadProgressInfo> progress)
        {
            req.Headers.TryAddWithoutValidation("User-Agent", options.UserAgent);
            req.Headers.ExpectContinue = false;
            req.Headers.Authorization = AuthenticationHeaderValue.Parse(authorizationHeader);
            req.Headers.ConnectionClose = options.DisableKeepAlive;

#if SYNC
            var handler = new WebRequestHandler();
            // WebRequestHandler.ReadWriteTimeout doesn't accept -1
            handler.ReadWriteTimeout = options.ReadWriteTimeout == Timeout.Infinite
                ? int.MaxValue : options.ReadWriteTimeout;
#else
            var handler = new HttpClientHandler();
#endif

            if (options.UseCompression)
                handler.AutomaticDecompression = CompressionType;

            handler.UseProxy = options.UseProxy;
#if WEBPROXY
            handler.Proxy = options.Proxy;
#endif

            var client = new HttpClient(handler) { Timeout = new TimeSpan(TimeSpan.TicksPerMillisecond * options.Timeout) };

            var total = req.Content?.Headers.ContentLength;
            if (progress != null && total != null)
                progress.Report(new UploadProgressInfo(0, total));

            //TODO: detail progress report
            var res = await client.SendAsync(req, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);

            if (progress != null && total != null)
                progress.Report(new UploadProgressInfo(total.Value, total));

            return new AsyncResponse(res);
        }
#endif

        /// <summary>
        /// Sends a GET request as an asynchronous operation.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="authorizationHeader">The OAuth header.</param>
        /// <param name="options">The connection options for the request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns the response.</para>
        /// </returns>
        internal static Task<AsyncResponse> HttpGetAsync(Uri url, string authorizationHeader, ConnectionOptions options, CancellationToken cancellationToken)
        {
            if(options == null) options = new ConnectionOptions();
            var req = new HttpRequestMessage(HttpMethod.Get, url);
            return ExecuteRequest(req, authorizationHeader, options, cancellationToken, null);
        }

        internal static Task<AsyncResponse> HttpPostAsync(Uri url, string contentType, byte[] content, string authorizationHeader, ConnectionOptions options, CancellationToken cancellationToken, IProgress<UploadProgressInfo> progress = null)
        {
            if(options == null) options = new ConnectionOptions();

            var req = new HttpRequestMessage(HttpMethod.Post, url);
#if WIN_RT
            var httpContent = new HttpBufferContent(content.AsBuffer());
            httpContent.Headers.ContentType = HttpMediaTypeHeaderValue.Parse(contentType);
#else
            var httpContent = new ByteArrayContent(content);
            httpContent.Headers.ContentType = MediaTypeHeaderValue.Parse(contentType);
#endif
            req.Content = httpContent;
            return ExecuteRequest(req, authorizationHeader, options, cancellationToken, progress);
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
        internal static Task<AsyncResponse> HttpPostAsync(Uri url, IEnumerable<KeyValuePair<string, object>> prm, string authorizationHeader, ConnectionOptions options, CancellationToken cancellationToken, IProgress<UploadProgressInfo> progress = null)
        {
            if(prm == null) prm = new Dictionary<string, object>();
            if(options == null) options = new ConnectionOptions();

            var req = new HttpRequestMessage(HttpMethod.Post, url);
#if WIN_RT
            req.Content = new HttpFormUrlEncodedContent(
                prm.Select(kvp => new KeyValuePair<string, string>(kvp.Key, kvp.Value.ToString())));
#else
            req.Content = new FormUrlEncodedContent(
                prm.Select(kvp => new KeyValuePair<string, string>(kvp.Key, kvp.Value.ToString())));
#endif
            return ExecuteRequest(req, authorizationHeader, options, cancellationToken, progress);
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
        internal static async Task<AsyncResponse> HttpPostWithMultipartFormDataAsync(Uri url, KeyValuePair<string, object>[] prm, string authorizationHeader, ConnectionOptions options, CancellationToken cancellationToken, IProgress<UploadProgressInfo> progress)
        {
            if(options == null) options = new ConnectionOptions();

            var req = new HttpRequestMessage(HttpMethod.Post, url);
            var toDispose = new List<IDisposable>();

#if WIN_RT
            var content = new HttpMultipartFormDataContent();
            foreach(var x in prm)
            {
                cancellationToken.ThrowIfCancellationRequested();

                var valueStream = x.Value as Stream;
                var valueInputStream = x.Value as IInputStream;
                var valueArraySegment = x.Value as ArraySegment<byte>?;
                var valueBytes = x.Value as IEnumerable<byte>;
                var valueBuffer = x.Value as IBuffer;
                var valueInputStreamReference = x.Value as IInputStreamReference;
                var valueStorageItem = x.Value as IStorageItem;

                if (valueInputStreamReference != null)
                {
                    valueInputStream = await valueInputStreamReference.OpenSequentialReadAsync();
                    toDispose.Add(valueInputStream);
                }
                else if (valueStream != null)
                {
                    valueInputStream = valueStream.AsInputStream();
                }
                else if (valueArraySegment != null)
                {
                    valueBuffer = valueArraySegment.Value.Array.AsBuffer(valueArraySegment.Value.Offset, valueArraySegment.Value.Count);
                }
                else if (valueBytes != null)
                {
                    var valueByteArray = valueBytes as byte[] ?? valueBytes.ToArray();
                    valueBuffer = valueByteArray.AsBuffer();
                }

                if(valueInputStream != null)
                    content.Add(new HttpStreamContent(valueInputStream), x.Key, valueStorageItem?.Name ?? "file");
                else if(valueBuffer != null)
                    content.Add(new HttpBufferContent(valueBuffer), x.Key, valueStorageItem?.Name ?? "file");
                else
                    content.Add(new HttpStringContent(x.Value.ToString()), x.Key);
            }
            cancellationToken.ThrowIfCancellationRequested();
            req.Content = content;
            var res = await ExecuteRequest(req, authorizationHeader, options, cancellationToken, progress).ConfigureAwait(false);
#else
            var content = new MultipartFormDataContent();
            foreach (var x in prm)
            {
                cancellationToken.ThrowIfCancellationRequested();
                
                var valueStream = x.Value as Stream;
                var valueArraySegment = x.Value as ArraySegment<byte>?;
                var valueBytes = x.Value as IEnumerable<byte>;

#if FILEINFO
                var valueFileInfo = x.Value as FileInfo;
                var fileName = "file";
                if (valueFileInfo != null)
                {
                    valueStream = valueFileInfo.OpenRead();
                    fileName = valueFileInfo.Name;
                    toDispose.Add(valueStream);
                }
#else
                const string fileName = "file";
#endif

                if (valueStream != null)
                {
                    content.Add(new StreamContent(valueStream), x.Key, fileName);
                }
                else if (valueArraySegment != null)
                {
                    content.Add(
                        new ByteArrayContent(valueArraySegment.Value.Array, valueArraySegment.Value.Offset, valueArraySegment.Value.Count),
                        x.Key, fileName);
                }
                else if (valueBytes != null)
                {
                    content.Add(new ByteArrayContent(valueBytes as byte[] ?? valueBytes.ToArray()), x.Key, fileName);
                }
                else
                {
                    content.Add(new StringContent(x.Value.ToString()), x.Key);
                }
            }
            cancellationToken.ThrowIfCancellationRequested();
            req.Content = content;
            var res = await ExecuteRequest(req, authorizationHeader, options, cancellationToken, progress).ConfigureAwait(false);
#endif

            foreach (var x in toDispose)
                x.Dispose();

            return res;
        }
    }
}
#endif
