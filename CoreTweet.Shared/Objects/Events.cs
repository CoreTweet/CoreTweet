using System;
using System.Collections.Generic;
using System.Linq;
using CoreTweet.Core;
using Newtonsoft.Json;

namespace CoreTweet
{
    public class Event : CoreBase
    {
        [JsonProperty("for_user_id")]
        public string ForUserId { get; set; }

        [JsonProperty("tweet_create_events")]
        public Status[] TweetCreateEvents { get; set; }

        [JsonProperty("favorite_events")]
        public FavoriteEvent[] FavoriteEvents { get; set; }

        [JsonProperty("follow_events")]
        public SourceTargetEvent<User, User> FollowEvents { get; set; }

        [JsonProperty("block_events")]
        public SourceTargetEvent<User, User> BlockEvents { get; set; }

        [JsonProperty("mute_events")]
        public SourceTargetEvent<User, User> MuteEvents { get; set; }

        [JsonProperty("user_event")]
        public UserEvent UserEvent { get; set; }

        [JsonProperty("direct_message_events")]
        public DirectMessageEvent[] DirectMessageEvents { get; set; }

        [JsonProperty("direct_message_indicate_typing_events")]
        public DirectMessageIndicateTypingEvent[] DirectMessageIndicateTypingEvents { get; set; }

        [JsonProperty("direct_message_mark_read_events")]
        public DirectMessageMarkReadEvent[] DirectMessageMarkReadEvents { get; set; }

        [JsonProperty("tweet_delete_events")]
        public TweetDeleteEvent[] TweetDeleteEvents { get; set; }

        [JsonProperty("apps")]
        public IDictionary<string, App> Apps { get; set; }

        [JsonProperty("users")]
        public IDictionary<string, User> Users { get; set; }
    }

    public class FavoriteEvent : CoreBase
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("created_at")]
        [JsonConverter(typeof(DateTimeOffsetConverter))]
        public DateTimeOffset CreatedAt { get; set; }

        [JsonProperty("timestamp_ms")]
        [JsonConverter(typeof(TimestampConverter))]
        public DateTimeOffset Timestamp { get; set; }

        [JsonProperty("favorited_status")]
        public Status FavoritedStatus { get; set; }

        [JsonProperty("user")]
        public User User { get; set; }
    }

    public class SourceTargetEvent<TSource, TTarget> : CoreBase
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("created_timestamp")]
        [JsonConverter(typeof(TimestampConverter))]
        public DateTimeOffset Timestamp { get; set; }

        [JsonProperty("source")]
        public TSource Source { get; set; }

        [JsonProperty("target")]
        public TTarget Target { get; set; }
    }

    public class UserEvent : CoreBase
    {
        [JsonProperty("revoke")]
        public UserRevokeEvent Revoke { get; set; }
    }

    public class UserRevokeEvent : CoreBase
    {
        [JsonProperty("date_time")]
        public DateTimeOffset DateTime { get; set; }

        [JsonProperty("target")]
        public AppId Target { get; set; }

        [JsonProperty("source")]
        public UserId Source { get; set; }
    }

    public abstract class DirectMessageEventObjectBase : CoreBase
    {
        [JsonProperty("created_timestamp")]
        [JsonConverter(typeof(TimestampConverter))]
        public DateTimeOffset CreatedTimestamp { get; set; }

        [JsonProperty("target")]
        public RecipientId Target { get; set; }

        [JsonProperty("sender_id")]
        public string SenderId { get; set; }
    }

    public class DirectMessageEvent : CoreBase
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("created_timestamp")]
        [JsonConverter(typeof(TimestampConverter))]
        public DateTimeOffset CreatedTimestamp { get; set; }

        [JsonProperty("message_create")]
        public Message MessageCreate { get; set; }
    }

    public class Message : DirectMessageEventObjectBase
    {
        [JsonProperty("source_app_id")]
        public string SourceAppId { get; set; }

        [JsonProperty("message_data")]
        public MessageData MessageData { get; set; }
    }

    public class RecipientId : CoreBase
    {
        [JsonProperty("recipient_id")]
        public string Id { get; set; }
    }

    public class DirectMessageIndicateTypingEvent : DirectMessageEventObjectBase
    {
    }

    public class DirectMessageMarkReadEvent : DirectMessageEventObjectBase
    {
        [JsonProperty("last_read_event_id")]
        public string LastReadEventId { get; set; }
    }

    public class TweetDeleteEvent : CoreBase
    {
        [JsonProperty("status")]
        public StatusIds Status { get; set; }

        [JsonProperty("timestamp_ms")]
        [JsonConverter(typeof(TimestampConverter))]
        public DateTimeOffset TimestampMs { get; set; }
    }

    public class AppId : CoreBase
    {
        [JsonProperty("app_id")]
        public string Id { get; set; }
    }

    public class UserId : CoreBase
    {
        [JsonProperty("user_id")]
        public string Id { get; set; }
    }

    public class StatusIds : CoreBase
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("user_id")]
        public string UserId { get; set; }
    }

    public class App : CoreBase
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
