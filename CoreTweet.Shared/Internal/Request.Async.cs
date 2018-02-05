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

#if ASYNC
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using CoreTweet.Core;

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
        /// Initializes a new instance of the <see cref="AsyncResponse"/> class with a specified source.
        /// </summary>
        public AsyncResponse(HttpResponseMessage source)
        {
            this.Source = source;
            this.StatusCode = (int)source.StatusCode;
            this.Headers = source.Headers.ToDictionary(x => x.Key, x => x.Value.JoinToString(", "), StringComparer.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Gets the source of the response.
        /// </summary>
        public HttpResponseMessage Source { get; }

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
        public Task<Stream> GetResponseStreamAsync()
        {
            return this.Source.Content.ReadAsStreamAsync();
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
        private static async Task<AsyncResponse> ExecuteRequest(HttpRequestMessage req, string authorizationHeader, ConnectionOptions options, CancellationToken cancellationToken, IProgress<UploadProgressInfo> progress)
        {
            req.Headers.TryAddWithoutValidation("User-Agent", options.UserAgent);
            req.Headers.ExpectContinue = false;
            req.Headers.Authorization = AuthenticationHeaderValue.Parse(authorizationHeader);
            req.Headers.ConnectionClose = options.DisableKeepAlive;

            if (req.Content != null)
            {
                var contentLength = req.Content.Headers.ContentLength;

                if (!contentLength.HasValue)
                    req.Headers.TransferEncodingChunked = true; // Do not buffer the content

                if (progress != null)
                    req.Content = new ProgressHttpContent(req.Content, contentLength, progress);
            }

            return new AsyncResponse(await options.GetHttpClient()
                .SendAsync(req, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false));
        }

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
            if(options == null) options = ConnectionOptions.Default;
            var req = new HttpRequestMessage(HttpMethod.Get, url);
            return ExecuteRequest(req, authorizationHeader, options, cancellationToken, null);
        }

        internal static Task<AsyncResponse> HttpPostAsync(Uri url, string contentType, byte[] content, string authorizationHeader, ConnectionOptions options, CancellationToken cancellationToken, IProgress<UploadProgressInfo> progress = null)
        {
            if(options == null) options = ConnectionOptions.Default;

            var req = new HttpRequestMessage(HttpMethod.Post, url);
            var httpContent = new ByteArrayContent(content);
            httpContent.Headers.ContentType = MediaTypeHeaderValue.Parse(contentType);
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
            if(options == null) options = ConnectionOptions.Default;

            var req = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new FormUrlEncodedContent(prm.Select(kvp => new KeyValuePair<string, string>(kvp.Key, kvp.Value.ToString())))
            };
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
            if(options == null) options = ConnectionOptions.Default;

            var req = new HttpRequestMessage(HttpMethod.Post, url);
            var content = new MultipartFormDataContent();

            var toDispose = new List<IDisposable>();

            try
            {
                foreach (var x in prm)
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    var value = x.Value;

                    var valueString = value as string;
                    if (valueString != null)
                    {
                        content.Add(new StringContent(valueString), x.Key);
                        continue;
                    }

                    var fileName = "file";
                    var valueStream = value as Stream;

                    if (valueStream == null)
                    {
#if WIN_RT
                        var valueStorageItem = value as IStorageItem;
                        if (valueStorageItem != null)
                            fileName = valueStorageItem.Name;
#endif

                        if (value is ArraySegment<byte>)
                        {
                            var v = (ArraySegment<byte>)value;
                            content.Add(new ByteArrayContent(v.Array, v.Offset, v.Count), x.Key, fileName);
                            continue;
                        }

                        var valueBytes = value as IEnumerable<byte>;
                        if (valueBytes != null)
                        {
                            content.Add(new ByteArrayContent(valueBytes as byte[] ?? valueBytes.ToArray()), x.Key, fileName);
                            continue;
                        }

#if FILEINFO
                        var valueFileInfo = value as FileInfo;
                        if (valueFileInfo != null)
                        {
                            fileName = valueFileInfo.Name;
                            valueStream = valueFileInfo.OpenRead();
                            toDispose.Add(valueStream);
                        }
#endif

#if !FILEINFO
                        TypeInfo valueType;
#endif

#if WIN_RT
                        IInputStreamReference valueInputStreamReference;
                        IInputStream valueInputStream;
                        IBuffer valueBuffer;
                        if ((valueInputStreamReference = value as IInputStreamReference) != null)
                        {
                            valueInputStream = await valueInputStreamReference
                                .OpenSequentialReadAsync()
                                .AsTask(cancellationToken)
                                .ConfigureAwait(false);
                            valueStream = valueInputStream.AsStreamForRead();
                            toDispose.Add(valueStream);
                            toDispose.Add(valueInputStream);
                        }
                        else if ((valueInputStream = value as IInputStream) != null)
                        {
                            valueStream = valueInputStream.AsStreamForRead();
                        }
                        else if ((valueBuffer = value as IBuffer) != null)
                        {
                            valueStream = valueBuffer.AsStream();
                            toDispose.Add(valueStream);
                        }
                        else
#endif

#if !FILEINFO
                        if ((valueType = value.GetType().GetTypeInfo()).FullName == "System.IO.FileInfo")
                        {
                            fileName = (string)valueType.GetDeclaredProperty("Name").GetValue(value);
                            valueStream = (Stream)valueType.GetDeclaredMethod("OpenRead").Invoke(value, null);
                            toDispose.Add(valueStream);
                        }
#endif
                    }

                    if (valueStream != null)
                    {
                        content.Add(new StreamContent(valueStream), x.Key, fileName);
                        continue;
                    }

                    content.Add(new StringContent(value.ToString()), x.Key);
                }

                cancellationToken.ThrowIfCancellationRequested();
                req.Content = content;
                return await ExecuteRequest(req, authorizationHeader, options, cancellationToken, progress).ConfigureAwait(false);
            }
            finally
            {
                foreach (var x in toDispose)
                    x.Dispose();
            }
        }

        internal static Task<AsyncResponse> HttpDeleteAsync(Uri url, string authorizationHeader, ConnectionOptions options, CancellationToken cancellationToken)
        {
            if (options == null) options = ConnectionOptions.Default;
            var req = new HttpRequestMessage(HttpMethod.Delete, url);
            return ExecuteRequest(req, authorizationHeader, options, cancellationToken, null);
        }
    }
}
#endif
