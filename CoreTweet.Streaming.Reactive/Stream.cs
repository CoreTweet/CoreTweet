// The MIT License (MIT)
//
// CoreTweet - A .NET Twitter Library supporting Twitter API 1.1
// Copyright (c) 2013-2015 CoreTweet Development Team
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
using System.Reactive.Linq;
using System.Threading.Tasks;
using CoreTweet.Core;
using static CoreTweet.Streaming.StreamingApi;

namespace CoreTweet.Streaming.Reactive
{
    /// <summary>
    /// Extensions for Reactive Extension(Rx).
    /// </summary>
    public static class Extension
    {
        /// <summary>
        /// Starts the stream.
        /// </summary>
        /// <returns>The observable object.</returns>
        /// <param name="e">Tokens.</param>
        /// <param name="type">Type of streaming API.</param>
        /// <param name="parameters">Parameters.</param>
        [Obsolete]
        public static IObservable<StreamingMessage> StartObservableStream(this StreamingApi e, StreamingType type, StreamingParameters parameters = null)
        {
            if(parameters == null)
                parameters = new StreamingParameters();

            return AccessStreamingApiAsObservableImpl(e, type, parameters.Parameters);
        }

        private static IObservable<StreamingMessage> AccessStreamingApiAsObservableImpl(StreamingApi e, StreamingType type, IEnumerable<KeyValuePair<string, object>> parameters)
        {
            return Observable.Create<StreamingMessage>((observer, cancel) =>
            {
                return e.IncludedTokens.SendStreamingRequestAsync(GetMethodType(type), e.GetUrl(type), parameters, cancel)
                    .ContinueWith(task =>
                    {
                        if(task.IsFaulted)
                            task.Exception.InnerException.Rethrow();

                        return task.Result.GetResponseStreamAsync();
                    }, cancel)
                    .Unwrap()
                    .ContinueWith(task =>
                    {
                        if(task.IsFaulted)
                            task.Exception.InnerException.Rethrow();

                        try
                        {
                            using (var reader = new StreamReader(task.Result))
                            using (cancel.Register(() => reader.Dispose()))
                            {
                                foreach (var s in reader.EnumerateLines().Where(x => !string.IsNullOrEmpty(x)))
                                {
                                    try
                                    {
                                        observer.OnNext(StreamingMessage.Parse(s));
                                    }
                                    catch (ParsingException ex)
                                    {
                                        observer.OnNext(RawJsonMessage.Create(s, ex));
                                    }
                                }
                            }
                        }
                        finally
                        {
                            cancel.ThrowIfCancellationRequested();
                        }
                    }, cancel, TaskContinuationOptions.LongRunning, TaskScheduler.Default);
            });
        }

        private static IObservable<StreamingMessage> AccessStreamingApiAsObservable(StreamingApi e, StreamingType type, Expression<Func<string, object>>[] parameters)
        {
            return AccessStreamingApiAsObservableImpl(e, type, InternalUtils.ExpressionsToDictionary(parameters));
        }

        private static IObservable<StreamingMessage> AccessStreamingApiAsObservable(StreamingApi e, StreamingType type, IDictionary<string, object> parameters)
        {
            return AccessStreamingApiAsObservableImpl(e, type, parameters);
        }

        private static IObservable<StreamingMessage> AccessStreamingApiAsObservable(StreamingApi e, StreamingType type, object parameters)
        {
            return AccessStreamingApiAsObservableImpl(e, type, InternalUtils.ResolveObject(parameters));
        }

        /// <summary>
        /// Streams messages for a single user.
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> delimited (optional, not affects CoreTweet)</para>
        /// <para>- <c>bool</c> stall_warnings (optional)</para>
        /// <para>- <c>string</c> with (optional)</para>
        /// <para>- <c>string</c> replies (optional)</para>
        /// <para>- <c>string</c> track (optional)</para>
        /// <para>- <c>IEnumerable&lt;double&gt;</c> locations (optional)</para>
        /// </summary>
        /// <param name="e">The <see cref="StreamingApi"/> instance.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The stream messages.</returns>
        public static IObservable<StreamingMessage> UserAsObservable(this StreamingApi e, params Expression<Func<string, object>>[] parameters)
        {
            return AccessStreamingApiAsObservable(e, StreamingType.User, parameters);
        }

        /// <summary>
        /// Streams messages for a single user.
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> delimited (optional, not affects CoreTweet)</para>
        /// <para>- <c>bool</c> stall_warnings (optional)</para>
        /// <para>- <c>string</c> with (optional)</para>
        /// <para>- <c>string</c> replies (optional)</para>
        /// <para>- <c>string</c> track (optional)</para>
        /// <para>- <c>IEnumerable&lt;double&gt;</c> locations (optional)</para>
        /// </summary>
        /// <param name="e">The <see cref="StreamingApi"/> instance.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The stream messages.</returns>
        public static IObservable<StreamingMessage> UserAsObservable(this StreamingApi e, IDictionary<string, object> parameters)
        {
            return AccessStreamingApiAsObservable(e, StreamingType.User, parameters);
        }

        /// <summary>
        /// Streams messages for a single user.
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> delimited (optional, not affects CoreTweet)</para>
        /// <para>- <c>bool</c> stall_warnings (optional)</para>
        /// <para>- <c>string</c> with (optional)</para>
        /// <para>- <c>string</c> replies (optional)</para>
        /// <para>- <c>string</c> track (optional)</para>
        /// <para>- <c>IEnumerable&lt;double&gt;</c> locations (optional)</para>
        /// </summary>
        /// <param name="e">The <see cref="StreamingApi"/> instance.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The stream messages.</returns>
        public static IObservable<StreamingMessage> UserAsObservable(this StreamingApi e, object parameters)
        {
            return AccessStreamingApiAsObservable(e, StreamingType.User, parameters);
        }

        /// <summary>
        /// Streams messages for a single user.
        /// </summary>
        /// <param name="e">The <see cref="StreamingApi"/> instance.</param>
        /// <param name="stall_warnings">Specifies whether stall warnings should be delivered.</param>
        /// <param name="with">Specifies whether to return information for just the authenticating user, or include messages from accounts the user follows.</param>
        /// <param name="replies">Specifies whether to return additional &#64;replies.</param>
        /// <param name="track">Includes additional Tweets matching the specified keywords. Phrases of keywords are specified by a comma-separated list.</param>
        /// <param name="locations">Includes additional Tweets falling within the specified bounding boxes.</param>
        /// <returns>The stream messages.</returns>
        public static IObservable<StreamingMessage> UserAsObservable(this StreamingApi e, bool? stall_warnings = null, string with = null, string replies = null, string track = null, IEnumerable<double> locations = null)
        {
            var parameters = new Dictionary<string, object>();
            if (stall_warnings != null) parameters.Add(nameof(stall_warnings), stall_warnings);
            if (with != null) parameters.Add(nameof(with), with);
            if (replies != null) parameters.Add(nameof(replies), replies);
            if (track != null) parameters.Add(nameof(track), track);
            if (locations != null) parameters.Add(nameof(locations), locations);
            return AccessStreamingApiAsObservableImpl(e, StreamingType.User, parameters);
        }

        /// <summary>
        /// Streams messages for a set of users.
        /// <para>Available parameters:</para>
        /// <para>- <c>IEnumerable&lt;long&gt;</c> follow (required)</para>
        /// <para>- <c>string</c> delimited (optional, not affects CoreTweet)</para>
        /// <para>- <c>bool</c> stall_warnings (optional)</para>
        /// <para>- <c>string</c> with (optional)</para>
        /// <para>- <c>string</c> replies (optional)</para>
        /// </summary>
        /// <param name="e">The <see cref="StreamingApi"/> instance.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The stream messages.</returns>
        public static IObservable<StreamingMessage> SiteAsObservable(this StreamingApi e, params Expression<Func<string, object>>[] parameters)
        {
            return AccessStreamingApiAsObservable(e, StreamingType.Site, parameters);
        }

        /// <summary>
        /// Streams messages for a set of users.
        /// <para>Available parameters:</para>
        /// <para>- <c>IEnumerable&lt;long&gt;</c> follow (required)</para>
        /// <para>- <c>string</c> delimited (optional, not affects CoreTweet)</para>
        /// <para>- <c>bool</c> stall_warnings (optional)</para>
        /// <para>- <c>string</c> with (optional)</para>
        /// <para>- <c>string</c> replies (optional)</para>
        /// </summary>
        /// <param name="e">The <see cref="StreamingApi"/> instance.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The stream messages.</returns>
        public static IObservable<StreamingMessage> SiteAsObservable(this StreamingApi e, IDictionary<string, object> parameters)
        {
            return AccessStreamingApiAsObservable(e, StreamingType.Site, parameters);
        }

        /// <summary>
        /// Streams messages for a set of users.
        /// <para>Available parameters:</para>
        /// <para>- <c>IEnumerable&lt;long&gt;</c> follow (required)</para>
        /// <para>- <c>string</c> delimited (optional, not affects CoreTweet)</para>
        /// <para>- <c>bool</c> stall_warnings (optional)</para>
        /// <para>- <c>string</c> with (optional)</para>
        /// <para>- <c>string</c> replies (optional)</para>
        /// </summary>
        /// <param name="e">The <see cref="StreamingApi"/> instance.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The stream messages.</returns>
        public static IObservable<StreamingMessage> SiteAsObservable(this StreamingApi e, object parameters)
        {
            return AccessStreamingApiAsObservable(e, StreamingType.Site, parameters);
        }

        /// <summary>
        /// Streams messages for a set of users.
        /// </summary>
        /// <param name="e">The <see cref="StreamingApi"/> instance.</param>
        /// <param name="follow">A comma separated list of user IDs, indicating the users to return statuses for in the stream.</param>
        /// <param name="stall_warnings">Specifies whether stall warnings should be delivered.</param>
        /// <param name="with">Specifies whether to return information for just the users specified in the follow parameter, or include messages from accounts they follow.</param>
        /// <param name="replies">Specifies whether to return additional &#64;replies.</param>
        /// <returns>The stream messages.</returns>
        public static IObservable<StreamingMessage> SiteAsObservable(this StreamingApi e, IEnumerable<long> follow, bool? stall_warnings = null, string with = null, string replies = null)
        {
            if (follow == null) throw new ArgumentNullException(nameof(follow));
            var parameters = new Dictionary<string, object>();
            parameters.Add(nameof(follow), follow);
            if (stall_warnings != null) parameters.Add(nameof(stall_warnings), stall_warnings);
            if (with != null) parameters.Add(nameof(with), with);
            if (replies != null) parameters.Add(nameof(replies), replies);
            return AccessStreamingApiAsObservableImpl(e, StreamingType.Site, parameters);
        }

        /// <summary>
        /// Returns public statuses that match one or more filter predicates.
        /// <para>Multiple parameters may be specified which allows most clients to use a single connection to the Streaming API.</para>
        /// <para>Note: At least one predicate parameter (follow, locations, or track) must be specified.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>IEnumerable&lt;long&gt;</c> follow (optional)</para>
        /// <para>- <c>string</c> track (optional)</para>
        /// <para>- <c>IEnumerable&lt;double&gt;</c> locations (optional)</para>
        /// <para>- <c>string</c> delimited (optional, not affects CoreTweet)</para>
        /// <para>- <c>bool</c> stall_warnings (optional)</para>
        /// </summary>
        /// <param name="e">The <see cref="StreamingApi"/> instance.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The stream messages.</returns>
        public static IObservable<StreamingMessage> FilterAsObservable(this StreamingApi e, params Expression<Func<string, object>>[] parameters)
        {
            return AccessStreamingApiAsObservable(e, StreamingType.Filter, parameters);
        }

        /// <summary>
        /// Returns public statuses that match one or more filter predicates.
        /// <para>Multiple parameters may be specified which allows most clients to use a single connection to the Streaming API.</para>
        /// <para>Note: At least one predicate parameter (follow, locations, or track) must be specified.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>IEnumerable&lt;long&gt;</c> follow (optional)</para>
        /// <para>- <c>string</c> track (optional)</para>
        /// <para>- <c>IEnumerable&lt;double&gt;</c> locations (optional)</para>
        /// <para>- <c>string</c> delimited (optional, not affects CoreTweet)</para>
        /// <para>- <c>bool</c> stall_warnings (optional)</para>
        /// </summary>
        /// <param name="e">The <see cref="StreamingApi"/> instance.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The stream messages.</returns>
        public static IObservable<StreamingMessage> FilterAsObservable(this StreamingApi e, IDictionary<string, object> parameters)
        {
            return AccessStreamingApiAsObservable(e, StreamingType.Filter, parameters);
        }

        /// <summary>
        /// Returns public statuses that match one or more filter predicates.
        /// <para>Multiple parameters may be specified which allows most clients to use a single connection to the Streaming API.</para>
        /// <para>Note: At least one predicate parameter (follow, locations, or track) must be specified.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>IEnumerable&lt;long&gt;</c> follow (optional)</para>
        /// <para>- <c>string</c> track (optional)</para>
        /// <para>- <c>IEnumerable&lt;double&gt;</c> locations (optional)</para>
        /// <para>- <c>string</c> delimited (optional, not affects CoreTweet)</para>
        /// <para>- <c>bool</c> stall_warnings (optional)</para>
        /// </summary>
        /// <param name="e">The <see cref="StreamingApi"/> instance.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The stream messages.</returns>
        public static IObservable<StreamingMessage> FilterAsObservable(this StreamingApi e, object parameters)
        {
            return AccessStreamingApiAsObservable(e, StreamingType.Filter, parameters);
        }

        /// <summary>
        /// Returns public statuses that match one or more filter predicates.
        /// <para>Multiple parameters may be specified which allows most clients to use a single connection to the Streaming API.</para>
        /// <para>Note: At least one predicate parameter (follow, locations, or track) must be specified.</para>
        /// </summary>
        /// <param name="e">The <see cref="StreamingApi"/> instance.</param>
        /// <param name="follow">A comma separated list of user IDs, indicating the users to return statuses for in the stream.</param>
        /// <param name="track">Keywords to track. Phrases of keywords are specified by a comma-separated list.</param>
        /// <param name="locations">Specifies a set of bounding boxes to track.</param>
        /// <param name="stall_warnings">Specifies whether stall warnings should be delivered.</param>
        /// <returns>The stream messages.</returns>
        public static IObservable<StreamingMessage> FilterAsObservable(this StreamingApi e, IEnumerable<long> follow = null, string track = null, IEnumerable<double> locations = null, bool? stall_warnings = null)
        {
            if (follow == null && track == null && locations == null)
                throw new ArgumentException("At least one predicate parameter (follow, locations, or track) must be specified.");
            var parameters = new Dictionary<string, object>();
            if (follow != null) parameters.Add(nameof(follow), follow);
            if (track != null) parameters.Add(nameof(track), track);
            if (locations != null) parameters.Add(nameof(locations), locations);
            if (stall_warnings != null) parameters.Add(nameof(stall_warnings), stall_warnings);
            return AccessStreamingApiAsObservableImpl(e, StreamingType.Filter, parameters);
        }

        /// <summary>
        /// Returns a small random sample of all public statuses.
        /// <para>The Tweets returned by the default access level are the same, so if two different clients connect to this endpoint, they will see the same Tweets.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> delimited (optional, not affects CoreTweet)</para>
        /// <para>- <c>bool</c> stall_warnings (optional)</para>
        /// </summary>
        /// <param name="e">The <see cref="StreamingApi"/> instance.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The stream messages.</returns>
        public static IObservable<StreamingMessage> SampleAsObservable(this StreamingApi e, params Expression<Func<string, object>>[] parameters)
        {
            return AccessStreamingApiAsObservable(e, StreamingType.Sample, parameters);
        }

        /// <summary>
        /// Returns a small random sample of all public statuses.
        /// <para>The Tweets returned by the default access level are the same, so if two different clients connect to this endpoint, they will see the same Tweets.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> delimited (optional, not affects CoreTweet)</para>
        /// <para>- <c>bool</c> stall_warnings (optional)</para>
        /// </summary>
        /// <param name="e">The <see cref="StreamingApi"/> instance.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The stream messages.</returns>
        public static IObservable<StreamingMessage> SampleAsObservable(this StreamingApi e, IDictionary<string, object> parameters)
        {
            return AccessStreamingApiAsObservable(e, StreamingType.Sample, parameters);
        }

        /// <summary>
        /// Returns a small random sample of all public statuses.
        /// <para>The Tweets returned by the default access level are the same, so if two different clients connect to this endpoint, they will see the same Tweets.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>string</c> delimited (optional, not affects CoreTweet)</para>
        /// <para>- <c>bool</c> stall_warnings (optional)</para>
        /// </summary>
        /// <param name="e">The <see cref="StreamingApi"/> instance.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The stream messages.</returns>
        public static IObservable<StreamingMessage> SampleAsObservable(this StreamingApi e, object parameters)
        {
            return AccessStreamingApiAsObservable(e, StreamingType.Sample, parameters);
        }

        /// <summary>
        /// Returns a small random sample of all public statuses.
        /// <para>The Tweets returned by the default access level are the same, so if two different clients connect to this endpoint, they will see the same Tweets.</para>
        /// </summary>
        /// <param name="e">The <see cref="StreamingApi"/> instance.</param>
        /// <param name="stall_warnings">Specifies whether stall warnings should be delivered.</param>
        /// <returns>The stream messages.</returns>
        public static IObservable<StreamingMessage> SampleAsObservable(this StreamingApi e, bool? stall_warnings = null)
        {
            var parameters = new Dictionary<string, object>();
            if (stall_warnings != null) parameters.Add(nameof(stall_warnings), stall_warnings);
            return AccessStreamingApiAsObservableImpl(e, StreamingType.Sample, parameters);
        }

        /// <summary>
        /// Returns all public statuses. Few applications require this level of access.
        /// <para>Creative use of a combination of other resources and various access levels can satisfy nearly every application use case.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>int</c> count (optional)</para>
        /// <para>- <c>string</c> delimited (optional, not affects CoreTweet)</para>
        /// <para>- <c>bool</c> stall_warnings (optional)</para>
        /// </summary>
        /// <param name="e">The <see cref="StreamingApi"/> instance.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The stream messages.</returns>
        public static IObservable<StreamingMessage> FirehoseAsObservable(this StreamingApi e, params Expression<Func<string, object>>[] parameters)
        {
            return AccessStreamingApiAsObservable(e, StreamingType.Firehose, parameters);
        }

        /// <summary>
        /// Returns all public statuses. Few applications require this level of access.
        /// <para>Creative use of a combination of other resources and various access levels can satisfy nearly every application use case.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>int</c> count (optional)</para>
        /// <para>- <c>string</c> delimited (optional, not affects CoreTweet)</para>
        /// <para>- <c>bool</c> stall_warnings (optional)</para>
        /// </summary>
        /// <param name="e">The <see cref="StreamingApi"/> instance.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The stream messages.</returns>
        public static IObservable<StreamingMessage> FirehoseAsObservable(this StreamingApi e, IDictionary<string, object> parameters)
        {
            return AccessStreamingApiAsObservable(e, StreamingType.Firehose, parameters);
        }

        /// <summary>
        /// Returns all public statuses. Few applications require this level of access.
        /// <para>Creative use of a combination of other resources and various access levels can satisfy nearly every application use case.</para>
        /// <para>Available parameters:</para>
        /// <para>- <c>int</c> count (optional)</para>
        /// <para>- <c>string</c> delimited (optional, not affects CoreTweet)</para>
        /// <para>- <c>bool</c> stall_warnings (optional)</para>
        /// </summary>
        /// <param name="e">The <see cref="StreamingApi"/> instance.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The stream messages.</returns>
        public static IObservable<StreamingMessage> FirehoseAsObservable(this StreamingApi e, object parameters)
        {
            return AccessStreamingApiAsObservable(e, StreamingType.Firehose, parameters);
        }

        /// <summary>
        /// Returns all public statuses. Few applications require this level of access.
        /// <para>Creative use of a combination of other resources and various access levels can satisfy nearly every application use case.</para>
        /// </summary>
        /// <param name="e">The <see cref="StreamingApi"/> instance.</param>
        /// <param name="count">The number of messages to backfill.</param>
        /// <param name="stall_warnings">Specifies whether stall warnings should be delivered.</param>
        /// <returns>The stream messages.</returns>
        public static IObservable<StreamingMessage> FirehoseAsObservable(this StreamingApi e, int? count = null, bool? stall_warnings = null)
        {
            var parameters = new Dictionary<string, object>();
            if (count != null) parameters.Add(nameof(count), count);
            if (stall_warnings != null) parameters.Add(nameof(stall_warnings), stall_warnings);
            return AccessStreamingApiAsObservableImpl(e, StreamingType.Firehose, parameters);
        }
    }
}
