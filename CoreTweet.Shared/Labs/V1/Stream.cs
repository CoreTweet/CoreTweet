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

using System;
using System.Collections.Generic;
using System.IO;
#if LINQASYNC
using System.Linq;
using System.Runtime.CompilerServices;
#endif
using System.Text;
#if ASYNC
using System.Threading;
using System.Threading.Tasks;
#endif
using CoreTweet.Core;
using Newtonsoft.Json;

namespace CoreTweet.Labs.V1
{
    public class FilterRule : CoreBase
    {
        /// <summary>
        /// ID of the filter rule that matched against the Tweet delivered.
        /// </summary>
        [JsonProperty("id")]
        public long Id { get; set; }

        /// <summary>
        /// The rule text as submitted when creating the filter.
        /// </summary>
        [JsonProperty("value")]
        public string Value { get; set; }

        /// <summary>
        /// The tag label of the filter rule that matched against the Tweet delivered.
        /// </summary>
        [JsonProperty("tag")]
        public string Tag { get; set; }
    }

    public class FilterRulesPostCreateResponseMetaSummary : CoreBase
    {
        /// <summary>
        /// The number of rules that have been created as a result of this request.
        /// </summary>
        [JsonProperty("created")]
        public int Created { get; set; }

        /// <summary>
        /// The number of rules that could not be created as part of this request. If this value is greater than <c>0</c>, the <c>errors</c> object will be present and it will detail the reason for each rule.
        /// </summary>
        [JsonProperty("not_created")]
        public int NotCreated { get; set; }
    }

    public class FilterRulesPostDeleteResponseMetaSummary : CoreBase
    {
        /// <summary>
        /// The number of rules that have been deleted as a result of this request.
        /// </summary>
        [JsonProperty("deleted")]
        public int Deleted { get; set; }

        /// <summary>
        /// The number of rules that could not be deleted as part of this request. If this value is greater than <c>0</c>, the <c>errors</c> object will be present and it will detail the reason for each rule.
        /// </summary>
        [JsonProperty("not_deleted")]
        public int NotDeleted { get; set; }
    }

    public class FilterRulesGetResponseMeta : CoreBase
    {
        /// <summary>
        /// The time when the request body was returned.
        /// </summary>
        [JsonProperty("sent")]
        [JsonConverter(typeof(DateTimeOffsetConverter))]
        public DateTimeOffset Sent { get; set; }
    }

    public class FilterRulesPostCreateResponseMeta : CoreBase
    {
        /// <summary>
        /// The time when the request body was returned.
        /// </summary>
        [JsonProperty("sent")]
        [JsonConverter(typeof(DateTimeOffsetConverter))]
        public DateTimeOffset Sent { get; set; }

        [JsonProperty("meta")]
        public FilterRulesPostCreateResponseMetaSummary Summary { get; set; }
    }

    public class FilterRulesPostDeleteResponseMeta : CoreBase
    {
        /// <summary>
        /// The time when the request body was returned.
        /// </summary>
        [JsonProperty("sent")]
        [JsonConverter(typeof(DateTimeOffsetConverter))]
        public DateTimeOffset Sent { get; set; }

        [JsonProperty("meta")]
        public FilterRulesPostDeleteResponseMetaSummary Summary { get; set; }
    }

    public class FilterRulesGetResponse : ResponseBase
    {
        [JsonProperty("data")]
        public FilterRule[] Data { get; set; }

        [JsonProperty("meta")]
        public FilterRulesGetResponseMeta Meta { get; set; }
    }

    public class FilterRulesPostCreateResponse : ResponseBase
    {
        [JsonProperty("data")]
        public FilterRule[] Data { get; set; }

        [JsonProperty("meta")]
        public FilterRulesPostCreateResponseMeta Meta { get; set; }
    }

    public class FilterRulesPostDeleteResponse : ResponseBase
    {
        [JsonProperty("data")]
        public FilterRule[] Data { get; set; }

        [JsonProperty("meta")]
        public FilterRulesPostDeleteResponseMeta Meta { get; set; }
    }

    public class FilterStreamResponse : CoreBase
    {
        [JsonProperty("data")]
        public Tweet Data { get; set; }

        /// <summary>
        /// Returns the requested <see cref="TweetExpansions"/>, if available.
        /// </summary>
        [JsonProperty("includes")]
        public TweetResponseIncludes Includes { get; set; }

        /// <summary>
        /// Contains the list of filters that matched against the Tweet delivered.
        /// </summary>
        [JsonProperty("matching_rules")]
        public FilterRule[] MatchingRules { get; set; }
    }

    public class SampleStreamResponse : CoreBase
    {
        [JsonProperty("data")]
        public Tweet Data { get; set; }

        /// <summary>
        /// Returns the requested <see cref="TweetExpansions"/>, if available.
        /// </summary>
        [JsonProperty("includes")]
        public TweetResponseIncludes Includes { get; set; }
    }

#if ASYNC
    public class LineDelimitedJsonStreamObservable<T> : IObservable<T>
        where T : CoreBase
    {
        private readonly Stream _stream;

        internal LineDelimitedJsonStreamObservable(Stream stream)
        {
            _stream = stream;
        }

        public IDisposable Subscribe(IObserver<T> observer)
        {
            return new Subscription(observer, _stream);
        }

        private class Subscription : IDisposable
        {
            private readonly CancellationTokenSource _source = new CancellationTokenSource();

            public Subscription(IObserver<T> observer, Stream stream)
            {
                Task.Run(async () =>
                {
                    try
                    {
                        using (var reader = new StreamReader(stream, Encoding.UTF8, true, 16384))
                        {
                            while (!_source.Token.IsCancellationRequested && !reader.EndOfStream)
                            {
                                var line = await reader.ReadLineAsync().ConfigureAwait(false);
                                if (string.IsNullOrEmpty(line)) continue;
                                var converted = CoreBase.Convert<T>(line);
                                if (_source.Token.IsCancellationRequested) break;
                                observer.OnNext(converted);
                            }
                        }
                        observer.OnCompleted();
                    }
                    catch (Exception ex)
                    {
                        if (_source.Token.IsCancellationRequested) return;
                        observer.OnError(ex);
                    }
                });
            }

            public void Dispose()
            {
                _source.Cancel();
            }
        }
    }
#endif

    public class LineDelimitedJsonStreamResponseStreamer<T> : IDisposable
#if LINQASYNC
        , IAsyncDisposable
#endif
        where T : CoreBase
    {
        private readonly Stream _stream;
        private readonly IDisposable _disposable;

        internal LineDelimitedJsonStreamResponseStreamer(Stream stream, IDisposable disposable)
        {
            _stream = stream;
            _disposable = disposable;
        }

#if LINQASYNC
        public async IAsyncEnumerable<T> StreamAsAsyncEnumerable([EnumeratorCancellation] CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var reader = new StreamReader(_stream, Encoding.UTF8, true, 16384))
            {
                while (!cancellationToken.IsCancellationRequested && !reader.EndOfStream)
                {
                    var line = await reader.ReadLineAsync().ConfigureAwait(false);
                    if (cancellationToken.IsCancellationRequested) break;
                    if (string.IsNullOrEmpty(line)) continue;
                    yield return CoreBase.Convert<T>(line);
                }
            }
        }
#endif

#if SYNC
        public IEnumerable<T> StreamAsEnumerable()
        {
            using (var reader = new StreamReader(_stream, Encoding.UTF8, true, 16384))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (string.IsNullOrEmpty(line)) continue;
                    yield return CoreBase.Convert<T>(line);
                }
            }
        }
#endif

#if ASYNC
        public LineDelimitedJsonStreamObservable<T> StreamAsObservable()
        {
            return new LineDelimitedJsonStreamObservable<T>(_stream);
        }
#endif

        public void Dispose()
        {
            _stream.Dispose();
            _disposable.Dispose();
        }

#if LINQASYNC
        public async ValueTask DisposeAsync()
        {
            await _stream.DisposeAsync().ConfigureAwait(false);
            _disposable.Dispose();
        }
#endif
    }

    public partial class FilteredStreamApi
    {
#if SYNC
        internal FilterRulesPostDeleteResponse DeleteRulesImpl(IEnumerable<KeyValuePair<string, object>> parameters, string[] jsonmap, string urlPrefix, string urlSuffix)
        {
            var options = Tokens.ConnectionOptions.Clone();
            return this.Tokens.AccessJsonParameteredApiImpl<FilterRulesPostDeleteResponse>("tweets/stream/filter/rules", parameters, jsonmap, "", urlPrefix, urlSuffix);
        }
#endif

#if ASYNC
        internal Task<FilterRulesPostDeleteResponse> DeleteRulesAsyncImpl(IEnumerable<KeyValuePair<string, object>> parameters, string[] jsonmap, CancellationToken cancellationToken, string urlPrefix, string urlSuffix)
        {
            var options = Tokens.ConnectionOptions.Clone();
            return this.Tokens.AccessJsonParameteredApiAsyncImpl<FilterRulesPostDeleteResponse>("tweets/stream/filter/rules", parameters, jsonmap, cancellationToken, "", urlPrefix, urlSuffix);
        }
#endif

#if SYNC
        internal LineDelimitedJsonStreamResponseStreamer<FilterStreamResponse> FilterImpl(IEnumerable<KeyValuePair<string, object>> parameters, string urlPrefix, string urlSuffix)
        {
            var options = Tokens.ConnectionOptions.Clone();
            options.UrlPrefix = urlPrefix;
            options.UrlSuffix = urlSuffix;
            var response = Tokens.SendStreamingRequest(MethodType.Get, InternalUtils.GetUrl(options, "tweets/stream/filter"), parameters);
            var stream = response.GetResponseStream();
            return new LineDelimitedJsonStreamResponseStreamer<FilterStreamResponse>(stream, response);
        }
#endif

#if ASYNC
        internal async Task<LineDelimitedJsonStreamResponseStreamer<FilterStreamResponse>> FilterAsyncImpl(IEnumerable<KeyValuePair<string, object>> parameters, CancellationToken cancellationToken, string urlPrefix, string urlSuffix)
        {
            var options = Tokens.ConnectionOptions.Clone();
            options.UrlPrefix = urlPrefix;
            options.UrlSuffix = urlSuffix;
            var response = await Tokens.SendStreamingRequestAsync(MethodType.Get, InternalUtils.GetUrl(options, "tweets/stream/filter"), parameters, cancellationToken).ConfigureAwait(false);
            var stream = await response.Source.Content.ReadAsStreamAsync().ConfigureAwait(false);
            return new LineDelimitedJsonStreamResponseStreamer<FilterStreamResponse>(stream, response);
        }
#endif
    }

    public partial class SampledStreamApi
    {
#if SYNC
        internal LineDelimitedJsonStreamResponseStreamer<SampleStreamResponse> SampleImpl(IEnumerable<KeyValuePair<string, object>> parameters, string urlPrefix, string urlSuffix)
        {
            var options = Tokens.ConnectionOptions.Clone();
            options.UrlPrefix = urlPrefix;
            options.UrlSuffix = urlSuffix;
            var response = Tokens.SendStreamingRequest(MethodType.Get, InternalUtils.GetUrl(options, "tweets/stream/sample"), parameters);
            var stream = response.GetResponseStream();
            return new LineDelimitedJsonStreamResponseStreamer<SampleStreamResponse>(stream, response);
        }
#endif

#if ASYNC
        internal async Task<LineDelimitedJsonStreamResponseStreamer<SampleStreamResponse>> SampleAsyncImpl(IEnumerable<KeyValuePair<string, object>> parameters, CancellationToken cancellationToken, string urlPrefix, string urlSuffix)
        {
            var options = Tokens.ConnectionOptions.Clone();
            options.UrlPrefix = urlPrefix;
            options.UrlSuffix = urlSuffix;
            var response = await Tokens.SendStreamingRequestAsync(MethodType.Get, InternalUtils.GetUrl(options, "tweets/stream/sample"), parameters, cancellationToken).ConfigureAwait(false);
            var stream = await response.Source.Content.ReadAsStreamAsync().ConfigureAwait(false);
            return new LineDelimitedJsonStreamResponseStreamer<SampleStreamResponse>(stream, response);
        }
#endif
    }
}
