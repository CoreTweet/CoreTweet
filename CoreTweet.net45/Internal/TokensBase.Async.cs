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

namespace CoreTweet.Core
{
    partial class TokensBase
    {
        internal Task<T> AccessApiAsync<T>(MethodType type, string url, Expression<Func<string, object>>[] parameters, CancellationToken cancellationToken, string jsonPath = "")
        {
            return this.AccessApiAsyncImpl<T>(type, url, InternalUtils.ExpressionsToDictionary(parameters), cancellationToken, jsonPath);
        }

        internal Task<T> AccessApiAsync<T, TV>(MethodType type, string url, TV parameters, CancellationToken cancellationToken, string jsonPath = "")
        {
            return this.AccessApiAsyncImpl<T>(type, url, InternalUtils.ResolveObject(parameters), cancellationToken, jsonPath);
        }

        internal Task<T> AccessApiAsync<T>(MethodType type, string url, IDictionary<string, object> parameters, CancellationToken cancellationToken, string jsonPath = "")
        {
            return this.AccessApiAsyncImpl<T>(type, url, parameters, cancellationToken, jsonPath);
        }

        private Task<T> AccessApiAsyncImpl<T>(MethodType type, string url, IEnumerable<KeyValuePair<string, object>> parameters, CancellationToken cancellationToken, string jsonPath)
        {
            return this.SendRequestAsync(type, InternalUtils.GetUrl(url), parameters, cancellationToken)
                .ContinueWith(
                    t => InternalUtils.ReadResponse(t, s => CoreBase.Convert<T>(this, s, jsonPath), cancellationToken),
                    cancellationToken
                ).Unwrap();
        }

        internal Task<IEnumerable<T>> AccessApiArrayAsync<T>(MethodType type, string url, Expression<Func<string, object>>[] parameters, CancellationToken cancellationToken, string jsonPath = "")
        {
            return this.AccessApiArrayAsyncImpl<T>(type, url, InternalUtils.ExpressionsToDictionary(parameters), cancellationToken, jsonPath);
        }

        internal Task<IEnumerable<T>> AccessApiArrayAsync<T, TV>(MethodType type, string url, TV parameters, CancellationToken cancellationToken, string jsonPath = "")
        {
            return this.AccessApiArrayAsyncImpl<T>(type, url, InternalUtils.ResolveObject(parameters), cancellationToken, jsonPath);
        }

        internal Task<IEnumerable<T>> AccessApiArrayAsync<T>(MethodType type, string url, IDictionary<string, object> parameters, CancellationToken cancellationToken, string jsonPath = "")
        {
            return this.AccessApiArrayAsyncImpl<T>(type, url, parameters, cancellationToken, jsonPath);
        }

        private Task<IEnumerable<T>> AccessApiArrayAsyncImpl<T>(MethodType type, string url, IEnumerable<KeyValuePair<string, object>> parameters, CancellationToken cancellationToken, string jsonPath)
        {
            return this.SendRequestAsync(type, InternalUtils.GetUrl(url), parameters, cancellationToken)
                .ContinueWith(
                    t => InternalUtils.ReadResponse(t, s => CoreBase.ConvertArray<T>(this, s, jsonPath), cancellationToken),
                    cancellationToken
                ).Unwrap();
        }

        internal Task AccessApiNoResponseAsync(string url, Expression<Func<string, object>>[] parameters, CancellationToken cancellationToken)
        {
            return this.AccessApiNoResponseAsyncImpl(url, InternalUtils.ExpressionsToDictionary(parameters), cancellationToken);
        }

        internal Task AccessApiNoResponseAsync<TV>(string url, TV parameters, CancellationToken cancellationToken)
        {
            return this.AccessApiNoResponseAsyncImpl(url, InternalUtils.ResolveObject(parameters), cancellationToken);
        }

        internal Task AccessApiNoResponseAsync(string url, IDictionary<string, object> parameters, CancellationToken cancellationToken)
        {
            return this.AccessApiNoResponseAsyncImpl(url, parameters, cancellationToken);
        }

        private Task AccessApiNoResponseAsyncImpl(string url, IEnumerable<KeyValuePair<string, object>> parameters, CancellationToken cancellationToken)
        {
            return this.SendRequestAsync(MethodType.Post, InternalUtils.GetUrl(url), parameters, cancellationToken)
                .ContinueWith(t =>
                {
                    if(t.IsCanceled)
                        throw new TaskCanceledException(t);
                    if(t.Exception != null)
                        t.Exception.Handle(ex => false);
                    t.Result.Dispose();
                }, cancellationToken);
        }

        /// <summary>
        /// Sends a request to the specified url with the specified parameters.
        /// </summary>
        /// <returns>
        /// The stream.
        /// </returns>
        /// <param name='type'>
        /// Type of HTTP request.
        /// </param>
        /// <param name='url'>
        /// URL.
        /// </param>
        /// <param name="cancellationToken">
        /// Cancellation token.
        /// </param>
        /// <param name='parameters'>
        /// Parameters.
        /// </param>
        public Task<HttpWebResponse> SendRequestAsync(MethodType type, string url, CancellationToken cancellationToken = default(CancellationToken), params Expression<Func<string, object>>[] parameters)
        {
            return this.SendRequestAsyncImpl(type, url, InternalUtils.ExpressionsToDictionary(parameters), cancellationToken);
        }

        /// <summary>
        /// Sends a request to the specified url with the specified parameters.
        /// </summary>
        /// <returns>
        /// The stream.
        /// </returns>
        /// <param name='type'>
        /// Type of HTTP request.
        /// </param>
        /// <param name='url'>
        /// URL.
        /// </param>
        /// <param name='parameters'>
        /// Parameters.
        /// </param>
        /// <param name="cancellationToken">
        /// Cancellation token.
        /// </param>
        public Task<HttpWebResponse> SendRequestAsync<T>(MethodType type, string url, T parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.SendRequestAsyncImpl(type, url, InternalUtils.ResolveObject(parameters), cancellationToken);
        }

        /// <summary>
        /// Sends a request to the specified url with the specified parameters.
        /// </summary>
        /// <returns>
        /// The stream.
        /// </returns>
        /// <param name='type'>
        /// Type of HTTP request.
        /// </param>
        /// <param name='url'>
        /// URL.
        /// </param>
        /// <param name='parameters'>
        /// Parameters.
        /// </param>
        /// <param name="cancellationToken">
        /// Cancellation token.
        /// </param>
        public Task<HttpWebResponse> SendRequestAsync(MethodType type, string url, IDictionary<string, object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.SendRequestAsyncImpl(type, url, parameters, cancellationToken);
        }

        private static HttpWebResponse ResponseCallback(Task<HttpWebResponse> t)
        {
            if(t.IsCanceled)
                throw new TaskCanceledException(t);
            if(t.Exception != null)
            {
                t.Exception.Handle(ex =>
                {
                    var wex = ex as WebException;
                    if(ex != null)
                    {
                        var tex = TwitterException.Create(wex);
                        if(tex != null)
                            throw tex;
                    }
                    return false;
                });
            }

            return t.Result;
        }

        private Task<HttpWebResponse> SendRequestAsyncImpl(MethodType type, string url, IEnumerable<KeyValuePair<string, object>> parameters, CancellationToken cancellationToken)
        {
            var prmArray = CollectionToCommaSeparatedString(parameters);
            if(type != MethodType.Get && prmArray.Any(x => x.Value is Stream || x.Value is IEnumerable<byte>
#if !PCL
                || x.Value is FileInfo
#endif
               ))
            {
                return Request.HttpPostWithMultipartFormDataAsync(
                    url,
                    prmArray,
                    CreateAuthorizationHeader(type, url, null),
                    UserAgent,
#if !PCL
                    Proxy,
#endif
                    cancellationToken
                ).ContinueWith(new Func<Task<HttpWebResponse>, HttpWebResponse>(ResponseCallback), cancellationToken);
            }
            else
            {
                var header = CreateAuthorizationHeader(type, url, prmArray);
                return (type == MethodType.Get
                    ? Request.HttpGetAsync(
                        url,
                        prmArray,
                        header,
                        UserAgent,
#if !PCL
                        Proxy,
#endif
                        cancellationToken
                    )
                    : Request.HttpPostAsync(
                        url,
                        prmArray,
                        header,
                        UserAgent,
#if !PCL
                        Proxy,
#endif
                        cancellationToken
                    )
                ).ContinueWith(new Func<Task<HttpWebResponse>, HttpWebResponse>(ResponseCallback), cancellationToken);
            }
        }
    }
}
