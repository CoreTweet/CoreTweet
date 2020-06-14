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
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace CoreTweet.Core
{
    public partial class TokensBase
    {
        internal Task<T> AccessApiAsync<T>(MethodType type, string url, Expression<Func<string, object>>[] parameters, string jsonPath = "", string urlPrefix = null, string urlSuffix = null)
        {
            return this.AccessApiAsyncImpl<T>(type, url, InternalUtils.ExpressionsToDictionary(parameters), CancellationToken.None, jsonPath, urlPrefix, urlSuffix);
        }

        internal Task<T> AccessApiAsync<T>(MethodType type, string url, object parameters, CancellationToken cancellationToken, string jsonPath = "", string urlPrefix = null, string urlSuffix = null)
        {
            return this.AccessApiAsyncImpl<T>(type, url, InternalUtils.ResolveObject(parameters), cancellationToken, jsonPath, urlPrefix, urlSuffix);
        }

        internal Task<T> AccessApiAsync<T>(MethodType type, string url, IDictionary<string, object> parameters, CancellationToken cancellationToken, string jsonPath = "", string urlPrefix = null, string urlSuffix = null)
        {
            return this.AccessApiAsyncImpl<T>(type, url, parameters, cancellationToken, jsonPath, urlPrefix, urlSuffix);
        }

        internal Task<T> AccessApiAsyncImpl<T>(MethodType type, string url, IEnumerable<KeyValuePair<string, object>> parameters, CancellationToken cancellationToken, string jsonPath, string urlPrefix, string urlSuffix)
        {
            var connectionOptions = this.ConnectionOptions;

            if (urlPrefix != null || urlSuffix != null)
            {
                connectionOptions = this.ConnectionOptions.Clone();

                if (urlPrefix != null)
                {
                    connectionOptions.UrlPrefix = urlPrefix;
                }

                if (urlSuffix != null)
                {
                    connectionOptions.UrlSuffix = urlSuffix;
                }
            }

            return this.SendRequestAsyncImpl(type, InternalUtils.GetUrl(connectionOptions, url), parameters, cancellationToken)
                .ReadResponse(s => CoreBase.Convert<T>(s, jsonPath), cancellationToken);
        }

        internal Task<ListedResponse<T>> AccessApiArrayAsync<T>(MethodType type, string url, Expression<Func<string, object>>[] parameters, string jsonPath = "", string urlPrefix = null, string urlSuffix = null)
        {
            return this.AccessApiArrayAsyncImpl<T>(type, url, InternalUtils.ExpressionsToDictionary(parameters), CancellationToken.None, jsonPath, urlPrefix, urlSuffix);
        }

        internal Task<ListedResponse<T>> AccessApiArrayAsync<T>(MethodType type, string url, object parameters, CancellationToken cancellationToken, string jsonPath = "", string urlPrefix = null, string urlSuffix = null)
        {
            return this.AccessApiArrayAsyncImpl<T>(type, url, InternalUtils.ResolveObject(parameters), cancellationToken, jsonPath, urlPrefix, urlSuffix);
        }

        internal Task<ListedResponse<T>> AccessApiArrayAsync<T>(MethodType type, string url, IDictionary<string, object> parameters, CancellationToken cancellationToken, string jsonPath = "", string urlPrefix = null, string urlSuffix = null)
        {
            return this.AccessApiArrayAsyncImpl<T>(type, url, parameters, cancellationToken, jsonPath, urlPrefix, urlSuffix);
        }

        internal Task<ListedResponse<T>> AccessApiArrayAsyncImpl<T>(MethodType type, string url, IEnumerable<KeyValuePair<string, object>> parameters, CancellationToken cancellationToken, string jsonPath, string urlPrefix, string urlSuffix)
        {
            var connectionOptions = this.ConnectionOptions;

            if (urlPrefix != null || urlSuffix != null)
            {
                connectionOptions = this.ConnectionOptions.Clone();

                if (urlPrefix != null)
                {
                    connectionOptions.UrlPrefix = urlPrefix;
                }

                if (urlSuffix != null)
                {
                    connectionOptions.UrlSuffix = urlSuffix;
                }
            }

            return this.SendRequestAsyncImpl(type, InternalUtils.GetUrl(connectionOptions, url), parameters, cancellationToken)
                .ReadResponse(s => new ListedResponse<T>(CoreBase.ConvertArray<T>(s, jsonPath)), cancellationToken);
        }

        internal Task<DictionaryResponse<TKey, TValue>> AccessApiDictionaryAsync<TKey, TValue>(MethodType type, string url, Expression<Func<string, object>>[] parameters, string jsonPath = "", string urlPrefix = null, string urlSuffix = null)
        {
            return this.AccessApiDictionaryAsyncImpl<TKey, TValue>(type, url, InternalUtils.ExpressionsToDictionary(parameters), CancellationToken.None, jsonPath, urlPrefix, urlSuffix);
        }

        internal Task<DictionaryResponse<TKey, TValue>> AccessApiDictionaryAsync<TKey, TValue>(MethodType type, string url, object parameters, CancellationToken cancellationToken, string jsonPath = "", string urlPrefix = null, string urlSuffix = null)
        {
            return this.AccessApiDictionaryAsyncImpl<TKey, TValue>(type, url, InternalUtils.ResolveObject(parameters), cancellationToken, jsonPath, urlPrefix, urlSuffix);
        }

        internal Task<DictionaryResponse<TKey, TValue>> AccessApiDictionaryAsync<TKey, TValue>(MethodType type, string url, IDictionary<string, object> parameters, CancellationToken cancellationToken, string jsonPath = "", string urlPrefix = null, string urlSuffix = null)
        {
            return this.AccessApiDictionaryAsyncImpl<TKey, TValue>(type, url, parameters, cancellationToken, jsonPath, urlPrefix, urlSuffix);
        }

        internal Task<DictionaryResponse<TKey, TValue>> AccessApiDictionaryAsyncImpl<TKey, TValue>(MethodType type, string url, IEnumerable<KeyValuePair<string, object>> parameters, CancellationToken cancellationToken, string jsonPath, string urlPrefix, string urlSuffix)
        {
            var connectionOptions = this.ConnectionOptions;

            if (urlPrefix != null || urlSuffix != null)
            {
                connectionOptions = this.ConnectionOptions.Clone();

                if (urlPrefix != null)
                {
                    connectionOptions.UrlPrefix = urlPrefix;
                }

                if (urlSuffix != null)
                {
                    connectionOptions.UrlSuffix = urlSuffix;
                }
            }

            return this.SendRequestAsyncImpl(type, InternalUtils.GetUrl(connectionOptions, url), parameters, cancellationToken)
                .ReadResponse(s => new DictionaryResponse<TKey, TValue>(CoreBase.Convert<Dictionary<TKey, TValue>>(s, jsonPath)), cancellationToken);
        }

        internal Task AccessApiNoResponseAsync(MethodType type, string url, Expression<Func<string, object>>[] parameters, string urlPrefix = null, string urlSuffix = null)
        {
            return this.AccessApiNoResponseAsyncImpl(type, url, InternalUtils.ExpressionsToDictionary(parameters), CancellationToken.None, urlPrefix, urlSuffix);
        }

        internal Task AccessApiNoResponseAsync(MethodType type, string url, object parameters, CancellationToken cancellationToken, string urlPrefix = null, string urlSuffix = null)
        {
            return this.AccessApiNoResponseAsyncImpl(type, url, InternalUtils.ResolveObject(parameters), cancellationToken, urlPrefix, urlSuffix);
        }

        internal Task AccessApiNoResponseAsync(MethodType type, string url, IDictionary<string, object> parameters, CancellationToken cancellationToken, string urlPrefix = null, string urlSuffix = null)
        {
            return this.AccessApiNoResponseAsyncImpl(type, url, parameters, cancellationToken, urlPrefix, urlSuffix);
        }

        internal Task AccessApiNoResponseAsyncImpl(MethodType type, string url, IEnumerable<KeyValuePair<string, object>> parameters, CancellationToken cancellationToken, string urlPrefix, string urlSuffix)
        {
            var connectionOptions = this.ConnectionOptions;

            if (urlPrefix != null || urlSuffix != null)
            {
                connectionOptions = this.ConnectionOptions.Clone();

                if (urlPrefix != null)
                {
                    connectionOptions.UrlPrefix = urlPrefix;
                }

                if (urlSuffix != null)
                {
                    connectionOptions.UrlSuffix = urlSuffix;
                }
            }

            return this.SendRequestAsyncImpl(type, InternalUtils.GetUrl(connectionOptions, url), parameters, cancellationToken)
                .Done(res => res.Dispose(), CancellationToken.None);
        }

        internal Task<T> AccessJsonParameteredApiAsync<T>(string url, Expression<Func<string, object>>[] parameters, string[] jsonMap, string jsonPath = "", string urlPrefix = null, string urlSuffix = null)
        {
            return this.AccessJsonParameteredApiAsyncImpl<T>(url, InternalUtils.ExpressionsToDictionary(parameters), jsonMap, CancellationToken.None, jsonPath, urlPrefix, urlSuffix);
        }

        internal Task<T> AccessJsonParameteredApiAsync<T>(string url, IDictionary<string, object> parameters, string[] jsonMap, CancellationToken cancellationToken, string jsonPath = "", string urlPrefix = null, string urlSuffix = null)
        {
            return this.AccessJsonParameteredApiAsyncImpl<T>(url, parameters, jsonMap, cancellationToken, jsonPath, urlPrefix, urlSuffix);
        }

        internal Task<T> AccessJsonParameteredApiAsync<T>(string url, object parameters, string[] jsonMap, CancellationToken cancellationToken, string jsonPath = "", string urlPrefix = null, string urlSuffix = null)
        {
            return this.AccessJsonParameteredApiAsyncImpl<T>(url, InternalUtils.ResolveObject(parameters), jsonMap, cancellationToken, jsonPath, urlPrefix, urlSuffix);
        }

        internal Task<T> AccessJsonParameteredApiAsyncImpl<T>(string url, IEnumerable<KeyValuePair<string, object>> parameters, string[] jsonMap, CancellationToken cancellationToken, string jsonPath, string urlPrefix, string urlSuffix)
        {
            var connectionOptions = this.ConnectionOptions;

            if (urlPrefix != null || urlSuffix != null)
            {
                connectionOptions = this.ConnectionOptions.Clone();

                if (urlPrefix != null)
                {
                    connectionOptions.UrlPrefix = urlPrefix;
                }

                if (urlSuffix != null)
                {
                    connectionOptions.UrlSuffix = urlSuffix;
                }
            }

            return this.SendJsonRequestAsync(InternalUtils.GetUrl(connectionOptions, url), parameters, jsonMap, cancellationToken)
                .ReadResponse(s => CoreBase.Convert<T>(s, jsonPath), cancellationToken);
        }

        internal Task<ListedResponse<T>> AccessJsonParameteredApiArrayAsync<T>(string url, Expression<Func<string, object>>[] parameters, string[] jsonMap, string jsonPath = "", string urlPrefix = null, string urlSuffix = null)
        {
            return this.AccessJsonParameteredApiArrayAsyncImpl<T>(url, InternalUtils.ExpressionsToDictionary(parameters), jsonMap, CancellationToken.None, jsonPath, urlPrefix, urlSuffix);
        }

        internal Task<ListedResponse<T>> AccessJsonParameteredApiArrayAsync<T>(string url, IDictionary<string, object> parameters, string[] jsonMap, CancellationToken cancellationToken, string jsonPath = "", string urlPrefix = null, string urlSuffix = null)
        {
            return this.AccessJsonParameteredApiArrayAsyncImpl<T>(url, parameters, jsonMap, cancellationToken, jsonPath, urlPrefix, urlSuffix);
        }

        internal Task<ListedResponse<T>> AccessJsonParameteredApiArrayAsync<T>(string url, object parameters, string[] jsonMap, CancellationToken cancellationToken, string jsonPath = "", string urlPrefix = null, string urlSuffix = null)
        {
            return this.AccessJsonParameteredApiArrayAsyncImpl<T>(url, InternalUtils.ResolveObject(parameters), jsonMap, cancellationToken, jsonPath, urlPrefix, urlSuffix);
        }

        internal Task<ListedResponse<T>> AccessJsonParameteredApiArrayAsyncImpl<T>(string url, IEnumerable<KeyValuePair<string, object>> parameters, string[] jsonMap, CancellationToken cancellationToken, string jsonPath, string urlPrefix, string urlSuffix)
        {
            var connectionOptions = this.ConnectionOptions;

            if (urlPrefix != null || urlSuffix != null)
            {
                connectionOptions = this.ConnectionOptions.Clone();

                if (urlPrefix != null)
                {
                    connectionOptions.UrlPrefix = urlPrefix;
                }

                if (urlSuffix != null)
                {
                    connectionOptions.UrlSuffix = urlSuffix;
                }
            }

            return this.SendJsonRequestAsync(InternalUtils.GetUrl(connectionOptions, url), parameters, jsonMap, cancellationToken)
                .ReadResponse(s => new ListedResponse<T>(CoreBase.ConvertArray<T>(s, jsonPath)), cancellationToken);
        }

        internal Task<AsyncResponse> SendJsonRequestAsync(string fullUrl, IEnumerable<KeyValuePair<string, object>> parameters, string[] jsonMap, CancellationToken cancellationToken)
        {
            return this.PostContentAsync(fullUrl, JsonContentType, InternalUtils.MapDictToJson(parameters, jsonMap), cancellationToken);
        }

        /// <summary>
        /// Sends a request to the specified url with the specified parameters as an asynchronous operation.
        /// </summary>
        /// <param name="type">The type of HTTP request.</param>
        /// <param name="url">The URL.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns a <see cref="AsyncResponse"/>.</para>
        /// </returns>
        public Task<AsyncResponse> SendRequestAsync(MethodType type, string url, CancellationToken cancellationToken = default(CancellationToken), params Expression<Func<string, object>>[] parameters)
        {
            return this.SendRequestAsyncImpl(type, url, InternalUtils.ExpressionsToDictionary(parameters), this.ConnectionOptions, cancellationToken);
        }

        /// <summary>
        /// Sends a request to the specified url with the specified parameters as an asynchronous operation.
        /// </summary>
        /// <param name="type">The type of HTTP request.</param>
        /// <param name="url">The URL.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns a <see cref="AsyncResponse"/>.</para>
        /// </returns>
        public Task<AsyncResponse> SendRequestAsync(MethodType type, string url, object parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.SendRequestAsyncImpl(type, url, InternalUtils.ResolveObject(parameters), this.ConnectionOptions, cancellationToken);
        }

        /// <summary>
        /// Sends a request to the specified url with the specified parameters as an asynchronous operation.
        /// </summary>
        /// <param name="type">The type of HTTP request.</param>
        /// <param name="url">The URL.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns a <see cref="AsyncResponse"/>.</para>
        /// </returns>
        public Task<AsyncResponse> SendRequestAsync(MethodType type, string url, IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.SendRequestAsyncImpl(type, url, parameters, this.ConnectionOptions, cancellationToken);
        }

        /// <summary>
        /// Sends a streaming request to the specified url with the specified parameters as an asynchronous operation.
        /// </summary>
        /// <param name="type">The type of HTTP request.</param>
        /// <param name="url">The URL.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns a <see cref="AsyncResponse"/>.</para>
        /// </returns>
        public Task<AsyncResponse> SendStreamingRequestAsync(MethodType type, string url, IEnumerable<KeyValuePair<string, object>> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            var options = this.ConnectionOptions != null ? this.ConnectionOptions.Clone() : new ConnectionOptions();
            options.UseCompression = options.UseCompressionOnStreaming;
#if SYNC
            options.ReadWriteTimeout = Timeout.Infinite;
#endif
            return this.SendRequestAsyncImpl(type, url, parameters, options, cancellationToken);
        }

        /// <summary>
        /// Sends a request to the specified url with the specified content as an asynchronous operation.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="contentType">The Content-Type header.</param>
        /// <param name="content">The content.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns a <see cref="AsyncResponse"/>.</para>
        /// </returns>
        public Task<AsyncResponse> PostContentAsync(string url, string contentType, byte[] content, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (string.IsNullOrEmpty(url))
                throw new ArgumentNullException(nameof(url));
            if (string.IsNullOrEmpty(contentType) != (content == null))
                throw new ArgumentException("Both contentType and content must be null or not null.");
            if (contentType.StartsWith("application/x-www-form-urlencoded", StringComparison.OrdinalIgnoreCase))
                throw new ArgumentException("Use SendRequest method to send the content in application/x-www-form-urlencoded.");

            var uri = new Uri(url);
            return Request.HttpPostAsync(uri, contentType, content, CreateAuthorizationHeader(MethodType.Post, uri, null), this.ConnectionOptions, cancellationToken)
                .ResponseCallback(cancellationToken);
        }

        /// <summary>
        /// Sends a streaming request to the specified url with the specified parameters as an asynchronous operation.
        /// </summary>
        /// <param name="type">The type of HTTP request.</param>
        /// <param name="url">The URL.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// <para>The task object representing the asynchronous operation.</para>
        /// <para>The Result property on the task object returns a stream.</para>
        /// </returns>
        internal Task<AsyncResponse> SendRequestAsyncImpl(MethodType type, string url, IEnumerable<KeyValuePair<string, object>> parameters, CancellationToken cancellationToken, IProgress<UploadProgressInfo> progress = null)
        {
            return this.SendRequestAsyncImpl(type, url, parameters, this.ConnectionOptions, cancellationToken, progress);
        }

        private Task<AsyncResponse> SendRequestAsyncImpl(MethodType type, string url, IEnumerable<KeyValuePair<string, object>> parameters, ConnectionOptions options, CancellationToken cancellationToken, IProgress<UploadProgressInfo> progress = null)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                var tcs = new TaskCompletionSource<AsyncResponse>();
                tcs.SetCanceled();
                return tcs.Task;
            }

            var prmArray = InternalUtils.FormatParameters(parameters);
            var uri = CreateUri(type, url, prmArray);

            Task<AsyncResponse> task;
            switch (type)
            {
                case MethodType.Get:
                    task = Request.HttpGetAsync(
                        uri,
                        CreateAuthorizationHeader(type, uri, null),
                        options,
                        cancellationToken
                    );
                    break;
                case MethodType.Post:
                    task = ContainsBinaryData(prmArray)
                        ? Request.HttpPostWithMultipartFormDataAsync(
                            uri,
                            prmArray,
                            CreateAuthorizationHeader(type, uri, null),
                            options,
                            cancellationToken,
                            progress
                        )
                        : Request.HttpPostAsync(
                            uri,
                            prmArray,
                            CreateAuthorizationHeader(type, uri, prmArray),
                            options,
                            cancellationToken,
                            progress
                        );
                    break;
                case MethodType.Put:
                    task = Request.HttpPutAsync(
                        uri,
                        prmArray,
                        CreateAuthorizationHeader(type, uri, prmArray),
                        options,
                        cancellationToken,
                        progress
                    );
                    break;
                case MethodType.Delete:
                    task = Request.HttpDeleteAsync(
                        uri,
                        CreateAuthorizationHeader(type, uri, null),
                        options,
                        cancellationToken
                    );
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type));
            }

            return task.ResponseCallback(cancellationToken);
        }
    }
}
#endif
