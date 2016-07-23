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

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
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
                Json = res.Json,
                RateLimit = res.RateLimit
            };
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
                Json = res.Json,
                RateLimit = res.RateLimit
            };
        }

#if SYNC
        private CollectionsListResult ListImpl(IEnumerable<KeyValuePair<string, object>> parameters)
        {
            return ToCollectionsListResult(
                this.Tokens.AccessApiImpl<CollectionsApiResult>(MethodType.Get, "collections/list", parameters, ""));
        }

        private TimelineResponse ShowImpl(IEnumerable<KeyValuePair<string, object>> parameters)
        {
            var res = this.Tokens.AccessApiImpl<CollectionsApiResult>(MethodType.Get, "collections/show", parameters, "");
            var timeline = GetTimeline(res.Objects, res.Response.TimelineId) as TimelineResponse;
            timeline.Json = res.Json;
            timeline.RateLimit = res.RateLimit;
            return timeline;
        }

        private CollectionEntriesResult EntriesImpl(IEnumerable<KeyValuePair<string, object>> parameters)
        {
            return ToCollectionEntriesResult(
                this.Tokens.AccessApiImpl<CollectionsApiResult>(MethodType.Get, "collections/entries", parameters, ""));
        }
#endif
    }
}
