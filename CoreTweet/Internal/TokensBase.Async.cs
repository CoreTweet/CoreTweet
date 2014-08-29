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
using System.Linq.Expressions;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

#if WIN_RT || WP
using Windows.Storage;
using Windows.Storage.Streams;
#endif

namespace CoreTweet.Core
{
    public partial class TokensBase
    {
        internal Task<T> AccessApiAsync<T>(MethodType type, string url, Expression<Func<string, object>>[] parameters, string jsonPath = "")
        {
            return this.AccessApiAsyncImpl<T>(type, url, InternalUtils.ExpressionsToDictionary(parameters), CancellationToken.None, jsonPath);
        }

        internal Task<T> AccessApiAsync<T, TV>(MethodType type, string url, TV parameters, CancellationToken cancellationToken, string jsonPath = "")
        {
            return this.AccessApiAsyncImpl<T>(type, url, InternalUtils.ResolveObject(parameters), cancellationToken, jsonPath);
        }

        internal Task<T> AccessApiAsync<T>(MethodType type, string url, IDictionary<string, object> parameters, CancellationToken cancellationToken, string jsonPath = "")
        {
            return this.AccessApiAsyncImpl<T>(type, url, parameters, cancellationToken, jsonPath);
        }

        internal Task<T> AccessApiAsyncImpl<T>(MethodType type, string url, IEnumerable<KeyValuePair<string, object>> parameters, CancellationToken cancellationToken, string jsonPath)
        {
            return this.SendRequestAsyncImpl(type, InternalUtils.GetUrl(url), parameters, cancellationToken)
                .ContinueWith(
                    t => InternalUtils.ReadResponse(t, s => CoreBase.Convert<T>(s, jsonPath), cancellationToken),
                    cancellationToken
                ).Unwrap().CheckCanceled(cancellationToken);
        }

        internal Task<ListedResponse<T>> AccessApiArrayAsync<T>(MethodType type, string url, Expression<Func<string, object>>[] parameters, string jsonPath = "")
        {
            return this.AccessApiArrayAsyncImpl<T>(type, url, InternalUtils.ExpressionsToDictionary(parameters), CancellationToken.None, jsonPath);
        }

        internal Task<ListedResponse<T>> AccessApiArrayAsync<T, TV>(MethodType type, string url, TV parameters, CancellationToken cancellationToken, string jsonPath = "")
        {
            return this.AccessApiArrayAsyncImpl<T>(type, url, InternalUtils.ResolveObject(parameters), cancellationToken, jsonPath);
        }

        internal Task<ListedResponse<T>> AccessApiArrayAsync<T>(MethodType type, string url, IDictionary<string, object> parameters, CancellationToken cancellationToken, string jsonPath = "")
        {
            return this.AccessApiArrayAsyncImpl<T>(type, url, parameters, cancellationToken, jsonPath);
        }

        internal Task<ListedResponse<T>> AccessApiArrayAsyncImpl<T>(MethodType type, string url, IEnumerable<KeyValuePair<string, object>> parameters, CancellationToken cancellationToken, string jsonPath)
        {
            return this.SendRequestAsyncImpl(type, InternalUtils.GetUrl(url), parameters, cancellationToken)
                .ContinueWith(
                    t => InternalUtils.ReadResponse(t, s => new ListedResponse<T>(CoreBase.ConvertArray<T>(s, jsonPath)), cancellationToken),
                    cancellationToken
                ).Unwrap().CheckCanceled(cancellationToken);
        }

        internal Task<DictionaryResponse<TKey, TValue>> AccessApiDictionaryAsync<TKey, TValue>(MethodType type, string url, Expression<Func<string, object>>[] parameters, string jsonPath = "")
        {
            return this.AccessApiDictionaryAsyncImpl<TKey, TValue>(type, url, InternalUtils.ExpressionsToDictionary(parameters), CancellationToken.None, jsonPath);
        }

        internal Task<DictionaryResponse<TKey, TValue>> AccessApiDictionaryAsync<TKey, TValue, TV>(MethodType type, string url, TV parameters, CancellationToken cancellationToken, string jsonPath = "")
        {
            return this.AccessApiDictionaryAsyncImpl<TKey, TValue>(type, url, InternalUtils.ResolveObject(parameters), cancellationToken, jsonPath);
        }

        internal Task<DictionaryResponse<TKey, TValue>> AccessApiDictionaryAsync<TKey, TValue>(MethodType type, string url, IDictionary<string, object> parameters, CancellationToken cancellationToken, string jsonPath = "")
        {
            return this.AccessApiDictionaryAsyncImpl<TKey, TValue>(type, url, parameters, cancellationToken, jsonPath);
        }

        internal Task<DictionaryResponse<TKey, TValue>> AccessApiDictionaryAsyncImpl<TKey, TValue>(MethodType type, string url, IEnumerable<KeyValuePair<string, object>> parameters, CancellationToken cancellationToken, string jsonPath)
        {
            return this.SendRequestAsyncImpl(type, InternalUtils.GetUrl(url), parameters, cancellationToken)
                .ContinueWith(
                    t => InternalUtils.ReadResponse(t, s => new DictionaryResponse<TKey, TValue>(CoreBase.Convert<Dictionary<TKey, TValue>>(s, jsonPath)), cancellationToken),
                    cancellationToken
                ).Unwrap().CheckCanceled(cancellationToken);
        }

        internal Task AccessApiNoResponseAsync(string url, Expression<Func<string, object>>[] parameters)
        {
            return this.AccessApiNoResponseAsyncImpl(url, InternalUtils.ExpressionsToDictionary(parameters), CancellationToken.None);
        }

        internal Task AccessApiNoResponseAsync<TV>(string url, TV parameters, CancellationToken cancellationToken)
        {
            return this.AccessApiNoResponseAsyncImpl(url, InternalUtils.ResolveObject(parameters), cancellationToken);
        }

        internal Task AccessApiNoResponseAsync(string url, IDictionary<string, object> parameters, CancellationToken cancellationToken)
        {
            return this.AccessApiNoResponseAsyncImpl(url, parameters, cancellationToken);
        }

        internal Task AccessApiNoResponseAsyncImpl(string url, IEnumerable<KeyValuePair<string, object>> parameters, CancellationToken cancellationToken)
        {
            return this.SendRequestAsyncImpl(MethodType.Post, InternalUtils.GetUrl(url), parameters, cancellationToken)
                .ContinueWith(t =>
                {
                    if(t.IsFaulted)
                        throw t.Exception.InnerException;

                    t.Result.Dispose();
                }, cancellationToken);
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
        /// <para>The Result property on the task object returns a stream.</para>
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
        /// <para>The Result property on the task object returns a stream.</para>
        /// </returns>
        public Task<AsyncResponse> SendRequestAsync<T>(MethodType type, string url, T parameters, CancellationToken cancellationToken = default(CancellationToken))
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
        /// <para>The Result property on the task object returns a stream.</para>
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
        /// <para>The Result property on the task object returns a stream.</para>
        /// </returns>
        public Task<AsyncResponse> SendStreamingRequestAsync(MethodType type, string url, IEnumerable<KeyValuePair<string, object>> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            var options = this.ConnectionOptions != null ? (ConnectionOptions)this.ConnectionOptions.Clone() : new ConnectionOptions();
#if !(PCL || WP)
            options.UseCompression = options.UseCompressionOnStreaming;
#endif
#if !(PCL || WIN_RT || WP)
            options.ReadWriteTimeout = Timeout.Infinite;
#endif
            return this.SendRequestAsyncImpl(type, url, parameters, options, cancellationToken);
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
        public Task<AsyncResponse> SendRequestAsyncImpl(MethodType type, string url, IEnumerable<KeyValuePair<string, object>> parameters, CancellationToken cancellationToken)
        {
            return this.SendRequestAsyncImpl(type, url, parameters, this.ConnectionOptions, cancellationToken);
        }

        private Task<AsyncResponse> SendRequestAsyncImpl(MethodType type, string url, IEnumerable<KeyValuePair<string, object>> parameters, ConnectionOptions options, CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(() =>
            {
                var prmArray = CollectionToCommaSeparatedString(parameters);
                if(type != MethodType.Get && prmArray.Any(x => x.Value is Stream || x.Value is IEnumerable<byte>
#if !(PCL || WIN_RT)
                    || x.Value is FileInfo
#endif
#if WIN_RT || WP
                    || x.Value is IInputStream
#endif
#if WIN_RT
                    || x.Value is IBuffer || x.Value is IInputStreamReference || x.Value is IStorageItem
#endif
                ))
                {
                    return Request.HttpPostWithMultipartFormDataAsync(
                        url,
                        prmArray,
                        CreateAuthorizationHeader(type, url, null),
                        options,
                        cancellationToken
                    )
                    .ContinueWith(new Func<Task<AsyncResponse>, Task<AsyncResponse>>(InternalUtils.ResponseCallback), cancellationToken)
                    .Unwrap();
                }
                else
                {
                    var header = CreateAuthorizationHeader(type, url, prmArray);
                    return (type == MethodType.Get
                        ? Request.HttpGetAsync(
                            url,
                            prmArray,
                            header,
                            options,
                            cancellationToken
                        )
                        : Request.HttpPostAsync(
                            url,
                            prmArray,
                            header,
                            options,
                            cancellationToken
                        )
                    )
                    .ContinueWith(new Func<Task<AsyncResponse>, Task<AsyncResponse>>(InternalUtils.ResponseCallback), cancellationToken)
                    .Unwrap();
                }
            }).Unwrap();
        }
    }
}
