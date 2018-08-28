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
using System.Linq;
using CoreTweet.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace CoreTweet.AccountActivity
{
    /// <summary>
    /// Provides event types in Twitter Account Activity API.
    /// </summary>
    public enum ActivityEventType
    {
        /// <summary>
        /// Tweet status payload when any of the following actions
        /// are taken by or to the subscription user:
        /// Tweets, Retweets, Replies, @mentions, QuoteTweets
        /// </summary>
        TweetCreateEvents,
        /// <summary>
        /// Favorite (like) event status with the user and target.
        /// </summary>
        FavoriteEvents,
        /// <summary>
        /// Follow event with the user and target.
        /// </summary>
        FollowEvents,
        /// <summary>
        /// Block event with the user and target.
        /// </summary>
        BlockEvents,
        /// <summary>
        /// Mute event with the user and target.
        /// </summary>
        MuteEvents,
        /// <summary>
        /// Revoke events sent when a user removes application authorization
        /// and subscription is automatically deleted.
        /// </summary>
        UserEvent,
        /// <summary>
        /// Direct message status with the user and target.
        /// </summary>
        DirectMessageEvents,
        /// <summary>
        /// Direct message typing event with the user and target.
        /// </summary>
        DirectMessageIndicateTypingEvents,
        /// <summary>
        /// Direct message read event with the user and target.
        /// </summary>
        DirectMessageMarkReadEvents,
        /// <summary>
        /// Notice of deleted Tweets to make it easier to maintain compliance.
        /// </summary>
        TweetDeleteEvents
    }

    /// <summary>
    /// Represents an Account Activity event. This class is an abstract class.
    /// </summary>
    public abstract class ActivityEvent : CoreBase
    {
        /// <summary>
        /// Gets the type of the event.
        /// </summary>
        public ActivityEventType Type => this.GetEventType();

        /// <summary>
        /// Gets or sets the raw JSON.
        /// </summary>
        public string Json { get; set; }

        /// <summary>
        /// Gets the type of the event.
        /// </summary>
        /// <returns>The type of the event.</returns>
        protected abstract ActivityEventType GetEventType();

        /// <summary>
        /// Converts the JSON to a <see cref="ActivityEvent"/> object.
        /// </summary>
        /// <param name="x">The JSON value.</param>
        /// <returns>The <see cref="ActivityEvent"/> object.</returns>
        public static ActivityEvent Parse(string x)
        {
            try
            {
                var j = JObject.Parse(x);
                ActivityEvent e;

                if (j["tweet_create_events"] != null)
                    e = CoreBase.Convert<TweetCreateEvents>(x);
                else if (j["favorite_events"] != null)
                    e = CoreBase.Convert<FavoriteEvents>(x);
                else if (j["follow_events"] != null)
                    e = CoreBase.Convert<FollowEvents>(x);
                else if (j["block_events"] != null)
                    e = CoreBase.Convert<BlockEvents>(x);
                else if (j["mute_events"] != null)
                    e = CoreBase.Convert<MuteEvents>(x);
                else if (j["user_event"] != null)
                    e = CoreBase.Convert<UserEvent>(x, "user_event");
                else if (j["direct_message_events"] != null)
                    e = CoreBase.Convert<DirectMessageEvents>(x);
                else if (j["direct_message_indicate_typing_events"] != null)
                    e = CoreBase.Convert<DirectMessageIndicateTypingEvents>(x);
                else if (j["direct_message_mark_read_events"] != null)
                    e = CoreBase.Convert<DirectMessageMarkReadEvents>(x);
                else if (j["tweet_delete_events"] != null)
                    e = CoreBase.Convert<TweetDeleteEvents>(x);
                else
                    throw new ParsingException("on account activity api, cannot parse the json: unsupported type", j.ToString(Formatting.Indented), null);

                e.Json = x;
                return e;
            }
            catch (ParsingException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new ParsingException("on account activity api, cannot parse the json", x, e);

            }
        }
    }

    /// <summary>
    /// Represents an event containing the for_user_id field.
    /// </summary>
    public abstract class UserSpecificActivityEvent : ActivityEvent
    {
        // TODO: should this be `string` or `long` (as in `User`)?
        [JsonProperty("for_user_id")]
        public long ForUserId { get; set; }
    }

    /// <summary>
    /// Represents an event containing the users field.
    /// </summary>
    public abstract class UsersInvolvingActivityEvent : UserSpecificActivityEvent
    {
        [JsonProperty("users")]
        public IDictionary<string, User> Users { get; set; }
    }

    /// <summary>
    /// Represents an event containing the apps field.
    /// </summary>
    public abstract class AppsInvolvingActivityEvent : UsersInvolvingActivityEvent
    {
        [JsonProperty("apps")]
        public IDictionary<string, MessageSourceApp> Apps { get; set; }
    }

    /// <summary>
    /// Tweet status payload when any of the following actions
    /// are taken by or to the subscription user:
    /// Tweets, Retweets, Replies, @mentions, QuoteTweets
    /// </summary>
    public class TweetCreateEvents : UserSpecificActivityEvent, IEnumerable<Status>
    {
        [JsonProperty("tweet_create_events")]
        public Status[] Items { get; set; }

        protected override ActivityEventType GetEventType() => ActivityEventType.TweetCreateEvents;

        public IEnumerator<Status> GetEnumerator() => ((IEnumerable<Status>)Items).GetEnumerator();

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => Items.GetEnumerator();
    }

    public class FavoriteEvent : CoreBase
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("created_at"), JsonConverter(typeof(DateTimeOffsetConverter))]
        public DateTimeOffset CreatedAt { get; set; }

        [JsonProperty("timestamp_ms"), JsonConverter(typeof(TimestampConverter))]
        public DateTimeOffset Timestamp { get; set; }

        [JsonProperty("favorited_status")]
        public Status FavoritedStatus { get; set; }

        [JsonProperty("user")]
        public User User { get; set; }
    }

    /// <summary>
    /// Favorite (like) event status with the user and target.
    /// </summary>
    public class FavoriteEvents : UserSpecificActivityEvent, IEnumerable<FavoriteEvent>
    {
        [JsonProperty("favorite_events")]
        public FavoriteEvent[] Items { get; set; }

        protected override ActivityEventType GetEventType() => ActivityEventType.FavoriteEvents;

        public IEnumerator<FavoriteEvent> GetEnumerator() => ((IEnumerable<FavoriteEvent>)Items).GetEnumerator();

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => Items.GetEnumerator();
    }

    /// <summary>
    /// Represents an event item that has the `created_timestamp` property.
    /// </summary>
    public abstract class TimestampEventItem : CoreBase
    {
        [JsonProperty("created_timestamp"), JsonConverter(typeof(TimestampConverter))]
        public DateTimeOffset CreatedTimestamp { get; set; }
    }

    public abstract class TypedEventItem : TimestampEventItem
    {
        public enum EventItemType
        {
            Follow,
            Block,
            Mute,
            MessageCreate
        }

        [JsonProperty("type")]
        private string _type { get; set; }

        // TODO: can/should we use `Newtonsoft.Json.Converters.StringEnumConverter` instead of this?
        private static readonly Dictionary<string, EventItemType> _mapping = new Dictionary<string, EventItemType>()
        {
            { "follow", EventItemType.Follow },
            { "block",  EventItemType.Block },
            { "mute",   EventItemType.Mute },
            { "message_create", EventItemType.MessageCreate }
        };

        [JsonIgnore]
        public EventItemType Type
        {
            get { return _mapping[_type]; }
            set { _type = _mapping.First(x => x.Value == value).Key; }
        }
    }

    public class UserToUserEventItem: TypedEventItem
    {
        [JsonProperty("source")]
        public User Source { get; set; }

        [JsonProperty("target")]
        public User Target { get; set; }
    }

    /// <summary>
    /// Follow event with the user and target.
    /// </summary>
    public class FollowEvents : UserSpecificActivityEvent, IEnumerable<UserToUserEventItem>
    {
        [JsonProperty("follow_events")]
        public UserToUserEventItem[] Items { get; set; }

        protected override ActivityEventType GetEventType() => ActivityEventType.FollowEvents;

        public IEnumerator<UserToUserEventItem> GetEnumerator() => ((IEnumerable<UserToUserEventItem>)Items).GetEnumerator();

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => Items.GetEnumerator();
    }

    /// <summary>
    /// Block event with the user and target.
    /// </summary>
    public class BlockEvents : UserSpecificActivityEvent, IEnumerable<UserToUserEventItem>
    {
        [JsonProperty("block_events")]
        public UserToUserEventItem[] Items { get; set; }

        protected override ActivityEventType GetEventType() => ActivityEventType.BlockEvents;

        public IEnumerator<UserToUserEventItem> GetEnumerator() => ((IEnumerable<UserToUserEventItem>)Items).GetEnumerator();

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => Items.GetEnumerator();
    }

    /// <summary>
    /// Mute event with the user and target.
    /// </summary>
    public class MuteEvents : UserSpecificActivityEvent, IEnumerable<UserToUserEventItem>
    {
        [JsonProperty("mute_events")]
        public UserToUserEventItem[] Items { get; set; }

        protected override ActivityEventType GetEventType() => ActivityEventType.MuteEvents;

        public IEnumerator<UserToUserEventItem> GetEnumerator() => ((IEnumerable<UserToUserEventItem>)Items).GetEnumerator();

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => Items.GetEnumerator();
    }

    public class IdOnlyApp : CoreBase
    {
        [JsonProperty("app_id")]
        public string AppId { get; set; }
    }

    public class IdOnlyUser : CoreBase
    {
        // TODO: is it ok to be `long`?
        [JsonProperty("user_id")]
        public long UserId { get; set; }
    }

    public class UserEventRevoke : CoreBase
    {
        // TODO: is it really ok to use `DateTimeOffsetConverter`? the format here is "yyyy-MM-dd'T'HH:mm:ssZ".
        [JsonProperty("date_time"), JsonConverter(typeof(DateTimeOffsetConverter))]
        public DateTimeOffset DateTime { get; set; }

        [JsonProperty("target")]
        public IdOnlyApp Target { get; set; }

        [JsonProperty("source")]
        public IdOnlyUser Source { get; set; }
    }

    /// <summary>
    /// Revoke events sent when a user removes application authorization and subscription is automatically deleted.
    /// </summary>
    public class UserEvent : ActivityEvent
    {
        [JsonProperty("revoke")]
        public UserEventRevoke Revoke { get; set; }

        protected override ActivityEventType GetEventType() => ActivityEventType.UserEvent;
    }

    /// <summary>
    /// Direct message status with the user and target.
    /// </summary>
    public class DirectMessageEvents : AppsInvolvingActivityEvent, IEnumerable<MessageCreateEvent>
    {
        [JsonProperty("direct_message_events")]
        public MessageCreateEvent[] Items { get; set; }

        protected override ActivityEventType GetEventType() => ActivityEventType.DirectMessageEvents;

        public IEnumerator<MessageCreateEvent> GetEnumerator() => ((IEnumerable<MessageCreateEvent>)Items).GetEnumerator();

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => Items.GetEnumerator();
    }

    public class EventTarget : CoreBase
    {
        // TODO: is it ok to be `long`?
        [JsonProperty("recipient_id")]
        public long RecipientId { get; set; }
    }

    public class DirectMessageIndicateTypingEvent : TimestampEventItem
    {
        // TODO: is it ok to be `long`?
        [JsonProperty("sender_id")]
        public long SenderId { get; set; }

        [JsonProperty("target")]
        public EventTarget Target { get; set; }
    }

    /// <summary>
    /// Direct message typing event with the user and target.
    /// </summary>
    public class DirectMessageIndicateTypingEvents : UsersInvolvingActivityEvent, IEnumerable<DirectMessageIndicateTypingEvent>
    {
        [JsonProperty("direct_message_indicate_typing_events")]
        public DirectMessageIndicateTypingEvent[] Items { get; set; }

        protected override ActivityEventType GetEventType() => ActivityEventType.DirectMessageIndicateTypingEvents;

        public IEnumerator<DirectMessageIndicateTypingEvent> GetEnumerator() => ((IEnumerable<DirectMessageIndicateTypingEvent>)Items).GetEnumerator();

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => Items.GetEnumerator();
    }

    public class DirectMessageMarkReadEvent : DirectMessageIndicateTypingEvent
    {
        [JsonProperty("last_read_event_id")]
        public string LastReadEventId { get; set; }
    }

    /// <summary>
    /// Direct message read event with the user and target.
    /// </summary>
    public class DirectMessageMarkReadEvents : UsersInvolvingActivityEvent, IEnumerable<DirectMessageMarkReadEvent>
    {
        [JsonProperty("direct_message_mark_read_events")]
        public DirectMessageMarkReadEvent[] Items { get; set; }

        protected override ActivityEventType GetEventType() => ActivityEventType.DirectMessageMarkReadEvents;

        public IEnumerator<DirectMessageMarkReadEvent> GetEnumerator() => ((IEnumerable<DirectMessageMarkReadEvent>)Items).GetEnumerator();

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => Items.GetEnumerator();
    }

    public class DeletedStatus : CoreBase
    {
        // TODO: is it ok to be `long`?
        [JsonProperty("id")]
        public long Id { get; set; }

        // TODO: is it ok to be `long`?
        [JsonProperty("user_id")]
        public long UserId { get; set; }
    }

    public class TweetDeleteEvent : CoreBase
    {
        [JsonProperty("status")]
        public DeletedStatus Status { get; set; }

        [JsonProperty("timestamp_ms"), JsonConverter(typeof(TimestampConverter))]
        public DateTimeOffset Timestamp { get; set; }
    }

    /// <summary>
    /// Notice of deleted Tweets to make it easier to maintain compliance.
    /// </summary>
    public class TweetDeleteEvents : UserSpecificActivityEvent, IEnumerable<TweetDeleteEvent>
    {
        [JsonProperty("tweet_delete_events")]
        public TweetDeleteEvent[] Items { get; set; }

        protected override ActivityEventType GetEventType() => ActivityEventType.TweetDeleteEvents;

        public IEnumerator<TweetDeleteEvent> GetEnumerator() => ((IEnumerable<TweetDeleteEvent>)Items).GetEnumerator();

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => Items.GetEnumerator();
    }
}
