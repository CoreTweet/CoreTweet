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

using System.Collections.Generic;
using System.Globalization;
using System.IO;
using CoreTweet.Core;

namespace CoreTweet.Rest
{
    partial class Collections
    {
        private static Timeline GetTimeline(CollectionObjects objects, string id)
        {
            var timeline = objects.Timelines[id];
            if (timeline.Id == null) timeline.Id = id;
            if (timeline.User == null) timeline.User = objects.Users[timeline.UserId];
            return timeline;
        }

        private static CollectionsListResult ToCollectionsListResult(CollectionsApiResult res)
        {
            return new CollectionsListResult
            {
                Results = res.Response.Results.ConvertAll(x => GetTimeline(res.Objects, x.TimelineId)),
                Cursors = res.Response.Cursors,
                RateLimit = res.RateLimit,
                Json = res.Json
            };
        }

        private static TimelineResponse ToTimelineResponse(CollectionsApiResult res)
        {
            var timeline = GetTimeline(res.Objects, res.Response.TimelineId) as TimelineResponse;
            timeline.RateLimit = res.RateLimit;
            timeline.Json = res.Json;
            return timeline;
        }

        private static CollectionEntriesResult ToCollectionEntriesResult(CollectionsApiResult res)
        {
            return new CollectionEntriesResult
            {
                Entries = res.Response.Timeline.ConvertAll(x =>
                {
                    var tweet = res.Objects.Tweets[x.Tweet.Id];
                    tweet.User = res.Objects.Users[tweet.User.Id.Value.ToString("D")];
                    return new TimelineEntry
                    {
                        FeatureContext = x.FeatureContext,
                        Tweet = tweet,
                        SortIndex = long.Parse(x.Tweet.SortIndex, NumberFormatInfo.InvariantInfo)
                    };
                }),
                Timeline = GetTimeline(res.Objects, res.Response.TimelineId),
                Position = res.Response.Position,
                RateLimit = res.RateLimit,
                Json = res.Json
            };
        }

#if SYNC
        private CollectionsApiResult AccessApi(MethodType type, string apiName, IEnumerable<KeyValuePair<string, object>> parameters, string urlPrefix, string urlSuffix)
        {
            return this.Tokens.AccessApiImpl<CollectionsApiResult>(type, "collections/" + apiName, parameters, "", urlPrefix, urlSuffix);
        }

        private CollectionsListResult ListImpl(IEnumerable<KeyValuePair<string, object>> parameters, string urlPrefix, string urlSuffix)
        {
            return ToCollectionsListResult(this.AccessApi(MethodType.Get, "list", parameters, urlPrefix, urlSuffix));
        }

        private TimelineResponse ShowImpl(IEnumerable<KeyValuePair<string, object>> parameters, string urlPrefix, string urlSuffix)
        {
            return ToTimelineResponse(this.AccessApi(MethodType.Get, "show", parameters, urlPrefix, urlSuffix));
        }

        private CollectionEntriesResult EntriesImpl(IEnumerable<KeyValuePair<string, object>> parameters, string urlPrefix, string urlSuffix)
        {
            return ToCollectionEntriesResult(this.AccessApi(MethodType.Get, "entries", parameters, urlPrefix, urlSuffix));
        }

        private TimelineResponse CreateImpl(IEnumerable<KeyValuePair<string, object>> parameters, string urlPrefix, string urlSuffix)
        {
            return ToTimelineResponse(this.AccessApi(MethodType.Post, "create", parameters, urlPrefix, urlSuffix));
        }

        private TimelineResponse UpdateImpl(IEnumerable<KeyValuePair<string, object>> parameters, string urlPrefix, string urlSuffix)
        {
            return ToTimelineResponse(this.AccessApi(MethodType.Post, "update", parameters, urlPrefix, urlSuffix));
        }
#endif
    }
}
