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

#if !NET35    
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace CoreTweet.Core
{
    public partial class TokensBase
    {
        internal Task<T> AccessApiAsync<T>(MethodType type, string url, Expression<Func<string, object>>[] parameters, string jsonPath = "")
        {
            return this.AccessApiAsyncImpl<T>(type, url, InternalUtils.ExpressionsToDictionary(parameters), CancellationToken.None, jsonPath);
        }

        internal Task<T> AccessApiAsync<T>(MethodType type, string url, object parameters, CancellationToken cancellationToken, string jsonPath = "")
        {
            return this.AccessApiAsyncImpl<T>(type, url, InternalUtils.ResolveObject(parameters), cancellationToken, jsonPath);
        }

        internal Task<T> AccessApiAsync<T>(MethodType type, string url, IDictionary<string, object> parameters, CancellationToken cancellationToken, string jsonPath = "")
        {
            return this.AccessApiAsyncImpl<T>(type, url, parameters, cancellationToken, jsonPath);
        }

        internal Task<T> AccessApiAsyncImpl<T>(MethodType type, string url, IEnumerable<KeyValuePair<string, object>> parameters, CancellationToken cancellationToken, string jsonPath)
        {
            return this.SendRequestAsyncImpl(type, InternalUtils.GetUrl(this.ConnectionOptions, url), parameters, cancellationToken)
                .ReadResponse(s => CoreBase.Convert<T>(s, jsonPath), cancellationToken);
        }

        internal Task<ListedResponse<T>> AccessApiArrayAsync<T>(MethodType type, string url, Expression<Func<string, object>>[] parameters, string jsonPath = "")
        {
            return this.AccessApiArrayAsyncImpl<T>(type, url, InternalUtils.ExpressionsToDictionary(parameters), CancellationToken.None, jsonPath);
        }

        internal Task<ListedResponse<T>> AccessApiArrayAsync<T>(MethodType type, string url, object parameters, CancellationToken cancellationToken, string jsonPath = "")
        {
            return this.AccessApiArrayAsyncImpl<T>(type, url, InternalUtils.ResolveObject(parameters), cancellationToken, jsonPath);
        }

        internal Task<ListedResponse<T>> AccessApiArrayAsync<T>(MethodType type, string url, IDictionary<string, object> parameters, CancellationToken cancellationToken, string jsonPath = "")
        {
            return this.AccessApiArrayAsyncImpl<T>(type, url, parameters, cancellationToken, jsonPath);
        }

        internal Task<ListedResponse<T>> AccessApiArrayAsyncImpl<T>(MethodType type, string url, IEnumerable<KeyValuePair<string, object>> parameters, CancellationToken cancellationToken, string jsonPath)
        {
            return this.SendRequestAsyncImpl(type, InternalUtils.GetUrl(this.ConnectionOptions, url), parameters, cancellationToken)
                .ReadResponse(s => new ListedResponse<T>(CoreBase.ConvertArray<T>(s, jsonPath)), cancellationToken);
        }

        internal Task<DictionaryResponse<TKey, TValue>> AccessApiDictionaryAsync<TKey, TValue>(MethodType type, string url, Expression<Func<string, object>>[] parameters, string jsonPath = "")
        {
            return this.AccessApiDictionaryAsyncImpl<TKey, TValue>(type, url, InternalUtils.ExpressionsToDictionary(parameters), CancellationToken.None, jsonPath);
        }

        internal Task<DictionaryResponse<TKey, TValue>> AccessApiDictionaryAsync<TKey, TValue>(MethodType type, string url, object parameters, CancellationToken cancellationToken, string jsonPath = "")
        {
            return this.AccessApiDictionaryAsyncImpl<TKey, TValue>(type, url, InternalUtils.ResolveObject(parameters), cancellationToken, jsonPath);
        }

        internal Task<DictionaryResponse<TKey, TValue>> AccessApiDictionaryAsync<TKey, TValue>(MethodType type, string url, IDictionary<string, object> parameters, CancellationToken cancellationToken, string jsonPath = "")
        {
            return this.AccessApiDictionaryAsyncImpl<TKey, TValue>(type, url, parameters, cancellationToken, jsonPath);
        }

        internal Task<DictionaryResponse<TKey, TValue>> AccessApiDictionaryAsyncImpl<TKey, TValue>(MethodType type, string url, IEnumerable<KeyValuePair<string, object>> parameters, CancellationToken cancellationToken, string jsonPath)
        {
            return this.SendRequestAsyncImpl(type, InternalUtils.GetUrl(this.ConnectionOptions, url), parameters, cancellationToken)
                .ReadResponse(s => new DictionaryResponse<TKey, TValue>(CoreBase.Convert<Dictionary<TKey, TValue>>(s, jsonPath)), cancellationToken);
        }

        internal Task AccessApiNoResponseAsync(string url, Expression<Func<string, object>>[] parameters)
        {
            return this.AccessApiNoResponseAsyncImpl(url, InternalUtils.ExpressionsToDictionary(parameters), CancellationToken.None);
        }

        internal Task AccessApiNoResponseAsync(string url, object parameters, CancellationToken cancellationToken)
        {
            return this.AccessApiNoResponseAsyncImpl(url, InternalUtils.ResolveObject(parameters), cancellationToken);
        }

        internal Task AccessApiNoResponseAsync(string url, IDictionary<string, object> parameters, CancellationToken cancellationToken)
        {
            return this.AccessApiNoResponseAsyncImpl(url, parameters, cancellationToken);
        }

        internal Task AccessApiNoResponseAsyncImpl(string url, IEnumerable<KeyValuePair<string, object>> parameters, CancellationToken cancellationToken)
        {
            return this.SendRequestAsyncImpl(MethodType.Post, InternalUtils.GetUrl(this.ConnectionOptions, url), parameters, cancellationToken)
                .Done(res => res.Dispose(), cancellationToken);
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
                var prmArray = FormatParameters(parameters);
                var uri = CreateUri(type, url, prmArray);
                if(type != MethodType.Get && ContainsBinaryData(prmArray))
                {
                    return Request.HttpPostWithMultipartFormDataAsync(
                        uri,
                        prmArray,
                        CreateAuthorizationHeader(type, uri, null),
                        options,
                        cancellationToken
                    )
                    .ResponseCallback(cancellationToken);
                }

                return (type == MethodType.Get
                    ? Request.HttpGetAsync(
                        uri,
                        CreateAuthorizationHeader(type, uri, null),
                        options,
                        cancellationToken
                    )
                    : Request.HttpPostAsync(
                        uri,
                        prmArray,
                        CreateAuthorizationHeader(type, uri, prmArray),
                        options,
                        cancellationToken
                    )
                )
                .ResponseCallback(cancellationToken);
            }, cancellationToken).Unwrap();
        }
    }
}
#endif
