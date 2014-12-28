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
using CoreTweet.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CoreTweet.Streaming
{
    /// <summary>
    /// Provides disconnect codes in Twitter Streaming API.
    /// </summary>
    public enum DisconnectCode
    {
        /// <summary>
        /// The feed was shutdown (possibly a machine restart)
        /// </summary>
        Shutdown = 1,
        /// <summary>
        /// The same endpoint was connected too many times.
        /// </summary>
        DuplicateStream,
        /// <summary>
        /// Control streams was used to close a stream (applies to sitestreams).
        /// </summary>
        ControlRequest,
        /// <summary>
        /// The client was reading too slowly and was disconnected by the server.
        /// </summary>
        Stall,
        /// <summary>
        /// The client appeared to have initiated a disconnect.
        /// </summary>
        Normal,
        /// <summary>
        /// An oauth token was revoked for a user (applies to site and userstreams).
        /// </summary>
        TokenRevoked,
        /// <summary>
        /// The same credentials were used to connect a new stream and the oldest was disconnected.
        /// </summary>
        AdminLogout,
        /// <summary>
        /// <para>Reserved for internal use.</para>
        /// <para>Will not be delivered to external clients.</para>
        /// </summary>
        Reserved,
        /// <summary>
        /// The stream connected with a negative count parameter and was disconnected after all backfill was delivered.
        /// </summary>
        MaxMessageLimit,
        /// <summary>
        /// An internal issue disconnected the stream.
        /// </summary>
        StreamException,
        /// <summary>
        /// An internal issue disconnected the stream.
        /// </summary>
        BrokerStall,
        /// <summary>
        /// <para>The host the stream was connected to became overloaded and streams were disconnected to balance load.</para>
        /// <para>Reconnect as usual.</para>
        /// </summary>
        ShedLoad
    }

    /// <summary>
    /// Provides event codes in Twitter Streaming API.
    /// </summary>
    public enum EventCode
    {
        /// <summary>
        /// The user blocks a user.
        /// </summary>
        Block,
        /// <summary>
        /// The user unblocks a user.
        /// </summary>
        Unblock,
        /// <summary>
        /// The user favorites a Tweet.
        /// </summary>
        Favorite,
        /// <summary>
        /// The user unfavorites a Tweet.
        /// </summary>
        Unfavorite,
        /// <summary>
        /// The user follows a user.
        /// </summary>
        Follow,
        /// <summary>
        /// The user unfollows a user.
        /// </summary>
        Unfollow,
        /// <summary>
        /// The user creates a List.
        /// </summary>
        ListCreated,
        /// <summary>
        /// The user destroys a List.
        /// </summary>
        ListDestroyed,
        /// <summary>
        /// The user updates a List.
        /// </summary>
        ListUpdated,
        /// <summary>
        /// The user adds a user to a List.
        /// </summary>
        ListMemberAdded,
        /// <summary>
        /// The user removes a user from a List.
        /// </summary>
        ListMemberRemoved,
        /// <summary>
        /// The user subscribes a List.
        /// </summary>
        ListUserSubscribed,
        /// <summary>
        /// The user unsubscribes a List.
        /// </summary>
        ListUserUnsubscribed,
        /// <summary>
        /// The user updates a List.
        /// </summary>
        UserUpdate,
        /// <summary>
        /// The user mutes a user.
        /// </summary>
        Mute,
        /// <summary>
        /// The user unmutes a user.
        /// </summary>
        Unmute,
        /// <summary>
        /// The user favorites a retweet.
        /// </summary>
        FavoritedRetweet
    }

    /// <summary>
    /// Provides message types in Twitter Streaming API.
    /// </summary>
    public enum MessageType
    {
        /// <summary>
        /// The message indicates the Tweet has been deleted.
        /// </summary>
        DeleteStatus,
        /// <summary>
        /// The message indicates the Direct Message has been deleted.
        /// </summary>
        DeleteDirectMessage,
        /// <summary>
        /// The message indicates that geolocated data must be stripped from a range of Tweets.
        /// </summary>
        ScrubGeo,
        /// <summary>
        /// The message indicates that the indicated tweet has had their content withheld.
        /// </summary>
        StatusWithheld,
        /// <summary>
        /// The message indicates that indicated user has had their content withheld.
        /// </summary>
        UserWithheld,
        /// <summary>
        /// The message indicates that the user has been deleted.
        /// </summary>
        UserDelete,
        /// <summary>
        /// The message indicates that the user has canceled the deletion.
        /// </summary>
        //TODO: need investigation
        UserUndelete,
        /// <summary>
        /// The message indicates that the user has been suspended.
        /// </summary>
        UserSuspend,
        /// <summary>
        /// The message indicates that the streams may be shut down for a variety of reasons.
        /// </summary>
        Disconnect,
        /// <summary>
        /// <para>The message indicates the current health of the connection.</para>
        /// <para>This can be only sent when connected to a stream using the stall_warnings parameter.</para>
        /// </summary>
        Warning,
        /// <summary>
        /// The message is about non-Tweet events.
        /// </summary>
        Event,
        /// <summary>
        /// <para>The message is sent to identify the target of each message.</para>
        /// <para>In Site Streams, an additional wrapper is placed around every message, except for blank keep-alive lines.</para>
        /// </summary>
        Envelopes,
        /// <summary>
        /// The message is a new Tweet.
        /// </summary>
        Create,
        /// <summary>
        /// The message is a new Direct Message.
        /// </summary>
        DirectMesssage,
        /// <summary>
        /// <para>The message is a list of the userÅfs friends.</para>
        /// <para>Twitter sends a preamble before starting regular message delivery upon establishing a User Stream connection.</para>
        /// </summary>
        Friends,
        /// <summary>
        /// The message indicates that a filtered stream has matched more Tweets than its current rate limit allows to be delivered.
        /// </summary>
        Limit,
        /// <summary>
        /// The message is sent to modify the Site Streams connection without reconnecting.
        /// </summary>
        Control,
        /// <summary>
        /// The message is in raw JSON format.
        /// </summary>
        RawJson
    }

    /// <summary>
    /// Represents a streaming message. This class is an abstract class.
    /// </summary>
    public abstract class StreamingMessage : CoreBase
    {
        /// <summary>
        /// Gets the type of the message.
        /// </summary>
        public MessageType Type { get { return GetMessageType(); } }

        /// <summary>
        /// Gets or sets the raw JSON.
        /// </summary>
        public string Json { get; set; }

        /// <summary>
        /// Gets the type of the message.
        /// </summary>
        /// <returns>The type of the message.</returns>
        protected abstract MessageType GetMessageType();

        /// <summary>
        /// Converts the JSON to a <see cref="StreamingMessage"/> object.
        /// </summary>
        /// <param name="x">The JSON value.</param>
        /// <returns>The <see cref="StreamingMessage"/> object.</returns>
        public static StreamingMessage Parse(string x)
        {
            try
            {
                var j = JObject.Parse(x);
                StreamingMessage m;

                if(j["text"] != null)
                    m = StatusMessage.Parse(j);
                else if(j["direct_message"] != null)
                    m = CoreBase.Convert<DirectMessageMessage>(x);
                else if(j["friends"] != null)
                    m = CoreBase.Convert<FriendsMessage>(x);
                else if(j["event"] != null)
                    m = EventMessage.Parse(j);
                else if(j["for_user"] != null)
                    m = EnvelopesMessage.Parse(j);
                else if(j["control"] != null)
                    m = CoreBase.Convert<ControlMessage>(x);
                else
                    m = ExtractRoot(j);

                m.Json = x;
                return m;
            }
            catch(ParsingException)
            {
                throw;
            }
            catch(Exception e)
            {
                throw new ParsingException("on streaming, cannot parse the json", x, e);
            }
        }

        static StreamingMessage ExtractRoot(JObject jo)
        {
            JToken jt;
            if(jo.TryGetValue("disconnect", out jt))
                return jt.ToObject<DisconnectMessage>();
            else if(jo.TryGetValue("warning", out jt))
                return jt.ToObject<WarningMessage>();
            else if(jo.TryGetValue("control", out jt))
                return jt.ToObject<ControlMessage>();
            else if(jo.TryGetValue("delete", out jt))
            {
                JToken status;
                DeleteMessage id;
                if (((JObject)jt).TryGetValue("status", out status))
                {
                    id = status.ToObject<DeleteMessage>();
                    id.messageType = MessageType.DeleteStatus;
                }
                else
                {
                    id = jt["direct_message"].ToObject<DeleteMessage>();
                    id.messageType = MessageType.DeleteDirectMessage;
                }
                var timestamp = jt["timestamp_ms"];
                if (timestamp != null)
                    id.Timestamp = InternalUtils.GetUnixTimeMs(long.Parse((string)timestamp));
                return id;
            }
            else if(jo.TryGetValue("scrub_geo", out jt))
            {
                return jt.ToObject<ScrubGeoMessage>();
            }
            else if(jo.TryGetValue("limit", out jt))
            {
                return jt.ToObject<LimitMessage>();
            }
            else if(jo.TryGetValue("status_withheld", out jt))
            {
                return jt.ToObject<StatusWithheldMessage>();
            }
            else if(jo.TryGetValue("user_withheld", out jt))
            {
                return jt.ToObject<UserWithheldMessage>();
            }
            else if(jo.TryGetValue("user_delete", out jt))
            {
                var m = jt.ToObject<UserMessage>();
                m.messageType = MessageType.UserDelete;
                return m;
            }
            else if(jo.TryGetValue("user_undelete", out jt))
            {
                var m = jt.ToObject<UserMessage>();
                m.messageType = MessageType.UserUndelete;
                return m;
            }
            else if(jo.TryGetValue("user_suspend", out jt))
            {
                // user_suspend doesn't have 'timestamp_ms' field
                var m = jt.ToObject<UserMessage>();
                m.messageType = MessageType.UserSuspend;
                return m;
            }
            else
                throw new ParsingException("on streaming, cannot parse the json: unsupported type", jo.ToString(Formatting.Indented), null);
        }
    }

    /// <summary>
    /// Represents a streaming message containing a timestamp.
    /// </summary>
    public abstract class TimestampMessage : StreamingMessage
    {
        /// <summary>
        /// Gets or sets the timestamp.
        /// </summary>
        [JsonProperty("timestamp_ms")]
        [JsonConverter(typeof(TimestampConverter))]
        public DateTimeOffset Timestamp { get; set; }
    }

    /// <summary>
    /// Represents a status message.
    /// </summary>
    public class StatusMessage : TimestampMessage
    {
        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        public Status Status { get; set; }

        /// <summary>
        /// Gets the type of the message.
        /// </summary>
        /// <returns>The type of the message.</returns>
        protected override MessageType GetMessageType()
        {
            return MessageType.Create;
        }

        internal static StatusMessage Parse(JObject j)
        {
            var m = new StatusMessage()
            {
                Status = j.ToObject<Status>()
            };

            var timestamp = j["timestamp_ms"];
            if (timestamp != null)
                m.Timestamp = InternalUtils.GetUnixTimeMs(long.Parse((string)timestamp));

            return m;
        }
    }

    /// <summary>
    /// Represents a Direct message message.
    /// </summary>
    public class DirectMessageMessage : StreamingMessage
    {
        /// <summary>
        /// The direct message.
        /// </summary>
        /// <value>The direct message.</value>
        [JsonProperty("direct_message")]
        public DirectMessage DirectMessage { get; set; }

        /// <summary>
        /// Gets the type of the message.
        /// </summary>
        /// <returns>The type of the message.</returns>
        protected override MessageType GetMessageType()
        {
            return MessageType.DirectMesssage;
        }
    }

    /// <summary>
    /// Represents a message contains ids of friends.
    /// </summary>
    [JsonObject]
    public class FriendsMessage : StreamingMessage,IEnumerable<long>
    {
        /// <summary>
        /// Gets or sets the ids of friends.
        /// </summary>
        [JsonProperty("friends")]
        public long[] Friends { get; set; }

        /// <summary>
        /// Gets the type of the message.
        /// </summary>
        /// <returns>The type of the message.</returns>
        protected override MessageType GetMessageType()
        {
            return MessageType.Friends;
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>An IEnumerator object that can be used to iterate through the collection.</returns>
        public IEnumerator<long> GetEnumerator()
        {
            return ((IEnumerable<long>)Friends).GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return Friends.GetEnumerator();
        }
    }

    /// <summary>
    /// Represents the message with the rate limit.
    /// </summary>
    public class LimitMessage : TimestampMessage
    {
        /// <summary>
        /// Gets or sets a total count of the number of undelivered Tweets since the connection was opened.
        /// </summary>
        [JsonProperty("track")]
        public int Track { get; set; }

        /// <summary>
        /// Gets the type of the message.
        /// </summary>
        /// <returns>The type of the message.</returns>
        protected override MessageType GetMessageType()
        {
            return MessageType.Limit;
        }
    }

    /// <summary>
    /// Represents a delete message of statuses and direct messages.
    /// </summary>
    public class DeleteMessage : TimestampMessage
    {
        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        [JsonProperty("id")]
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the ID of the user.
        /// </summary>
        [JsonProperty("user_id")]
        public long UserId { get; set; }

        internal MessageType messageType { get; set; }

        /// <summary>
        /// Gets the type of the message.
        /// </summary>
        /// <returns>The type of the message.</returns>
        protected override MessageType GetMessageType()
        {
            return messageType;
        }
    }

    /// <summary>
    /// Represents a scrub-get message.
    /// </summary>
    public class ScrubGeoMessage : TimestampMessage
    {
        /// <summary>
        /// Gets or sets the ID of the user.
        /// </summary>
        [JsonProperty("user_id")]
        public long UserId { get; set; }

        /// <summary>
        /// Gets or sets the ID of the status.
        /// </summary>
        //TODO: improve this comment
        [JsonProperty("up_to_status_id")]
        public long UpToStatusId { get; set; }

        /// <summary>
        /// Gets the type of the message.
        /// </summary>
        /// <returns>The type of the message.</returns>
        protected override MessageType GetMessageType()
        {
            return MessageType.ScrubGeo;
        }
    }

    /// <summary>
    /// Represents a withheld message.
    /// </summary>
    public class UserWithheldMessage : TimestampMessage
    {
        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        [JsonProperty("id")]
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the withhelds in countries.
        /// </summary>
        [JsonProperty("withheld_in_countries")]
        public string[] WithheldInCountries { get; set; }

        /// <summary>
        /// Gets the type of the message.
        /// </summary>
        /// <returns>The type of the message.</returns>
        protected override MessageType GetMessageType()
        {
            return MessageType.UserWithheld;
        }
    }

    /// <summary>
    /// Represents a withheld message.
    /// </summary>
    public class StatusWithheldMessage : TimestampMessage
    {
        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        [JsonProperty("id")]
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the ID of the user.
        /// </summary>
        [JsonProperty("user_id")]
        public long UserId { get; set; }

        /// <summary>
        /// Gets or sets the withhelds in countries.
        /// </summary>
        [JsonProperty("withheld_in_countries")]
        public string[] WithheldInCountries { get; set; }

        /// <summary>
        /// Gets the type of the message.
        /// </summary>
        /// <returns>The type of the message.</returns>
        protected override MessageType GetMessageType()
        {
            return MessageType.StatusWithheld;
        }
    }

    /// <summary>
    /// Represents a message contains the ID of an user.
    /// </summary>
    public class UserMessage : TimestampMessage
    {
        /// <summary>
        /// Gets or sets the ID of the user.
        /// </summary>
        [JsonProperty("id")]
        public long Id { get; set; }

        internal MessageType messageType { get; set; }

        /// <summary>
        /// Gets the type of the message.
        /// </summary>
        /// <returns>The type of the message.</returns>
        protected override MessageType GetMessageType()
        {
            return messageType;
        }
    }

    /// <summary>
    /// Represents the message published when Twitter disconnects the stream.
    /// </summary>
    public class DisconnectMessage : StreamingMessage
    {
        /// <summary>
        /// Gets or sets the disconnect code.
        /// </summary>
        [JsonProperty("code")]
        public DisconnectCode Code { get; set; }

        /// <summary>
        /// Gets or sets the stream name of current stream.
        /// </summary>
        [JsonProperty("stream_name")]
        public string StreamName { get; set; }

        /// <summary>
        /// Gets or sets the human readable message of the reason.
        /// </summary>
        [JsonProperty("reason")]
        public string Reason { get; set; }

        /// <summary>
        /// Gets the type of the message.
        /// </summary>
        /// <returns>The type of the message.</returns>
        protected override MessageType GetMessageType()
        {
            return MessageType.Disconnect;
        }
    }

    /// <summary>
    /// Represents a warning message.
    /// </summary>
    public class WarningMessage : TimestampMessage
    {
        /// <summary>
        /// Gets or sets the warning code.
        /// </summary>
        [JsonProperty("code")]
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets the warning message.
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the percentage of the stall messages
        /// </summary>
        [JsonProperty("percent_full")]
        public int? PercentFull { get; set; }

        /// <summary>
        /// Gets or sets the target user ID.
        /// </summary>
        [JsonProperty("user_id")]
        public long? UserId { get; set; }

        /// <summary>
        /// Gets the type of the message.
        /// </summary>
        /// <returns>The type of the message.</returns>
        protected override MessageType GetMessageType()
        {
            return MessageType.Warning;
        }
    }

    /// <summary>
    /// Provides the event target type.
    /// </summary>
    public enum EventTargetType
    {
        /// <summary>
        /// The event is about a List.
        /// </summary>
        List,
        /// <summary>
        /// The event is about a Tweet.
        /// </summary>
        Status,
        /// <summary>
        /// The event is unknown.
        /// </summary>
        Null
    }

    /// <summary>
    /// Represents an event message.
    /// </summary>
    public class EventMessage : StreamingMessage
    {
        /// <summary>
        /// Gets or sets the target user.
        /// </summary>
        public User Target { get; set; }

        /// <summary>
        /// Gets or sets the source.
        /// </summary>
        public User Source { get; set; }

        /// <summary>
        /// Gets or sets the event code.
        /// </summary>
        public EventCode Event { get; set; }

        /// <summary>
        /// Gets or sets the type of target.
        /// </summary>
        public EventTargetType TargetType { get; set; }

        /// <summary>
        /// Gets or sets the target status.
        /// </summary>
        public Status TargetStatus { get; set; }

        /// <summary>
        /// Gets or sets the target List.
        /// </summary>
        public CoreTweet.List TargetList { get; set; }

        /// <summary>
        /// Gets or sets the time when the event happened.
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// Gets the type of the message.
        /// </summary>
        /// <returns>The type of the message.</returns>
        protected override MessageType GetMessageType()
        {
            return MessageType.Event;
        }

        internal static EventMessage Parse(JObject j)
        {
            var e = new EventMessage();
            e.Target = j["target"].ToObject<User>();
            e.Source = j["source"].ToObject<User>();
            e.Event = (EventCode)Enum.Parse(typeof(EventCode),
                                            ((string)j["event"])
                                                .Replace("objectType", "")
                                                .Replace("_",""),
                                            true);
            e.CreatedAt = DateTimeOffset.ParseExact((string)j["created_at"], "ddd MMM dd HH:mm:ss K yyyy",
                                                  System.Globalization.DateTimeFormatInfo.InvariantInfo,
                                                  System.Globalization.DateTimeStyles.AllowWhiteSpaces);
            var eventstr = (string)j["event"];
            e.TargetType = eventstr.Contains("list") ? EventTargetType.List :
                eventstr.Contains("favorite") ? EventTargetType.Status : EventTargetType.Null;
            switch(e.TargetType)
            {
                case EventTargetType.Status:
                    e.TargetStatus = j["target_object"].ToObject<Status>();
                    break;
                case EventTargetType.List:
                    e.TargetList = j["target_object"].ToObject<CoreTweet.List>();
                    break;
            }
            return e;
        }
    }

    /// <summary>
    /// Provides an envelopes message.
    /// </summary>
    public class EnvelopesMessage : StreamingMessage
    {
        /// <summary>
        /// Gets or sets the ID of the user.
        /// </summary>
        public long ForUser { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        public StreamingMessage Message { get; set; }

        /// <summary>
        /// Gets the type of the message.
        /// </summary>
        /// <returns>The type of the message.</returns>
        protected override MessageType GetMessageType()
        {
            return MessageType.Envelopes;
        }

        internal static EnvelopesMessage Parse(JObject j)
        {
            return new EnvelopesMessage()
            {
                ForUser = (long)j["for_user"],
                Message = StreamingMessage.Parse(j["message"].ToString(Formatting.None))
            };
        }
    }

    /// <summary>
    /// Represents a control message.
    /// </summary>
    public class ControlMessage : StreamingMessage
    {
        /// <summary>
        /// Gets or sets the URI.
        /// </summary>
        [JsonProperty("control_uri")]
        public string ControlUri { get; set; }

        /// <summary>
        /// Gets the type of the message.
        /// </summary>
        /// <returns>The type of the message.</returns>
        protected override MessageType GetMessageType()
        {
            return MessageType.Control;
        }
    }

    /// <summary>
    /// Represents a raw JSON message.
    /// </summary>
    public class RawJsonMessage : StreamingMessage
    {
        internal static RawJsonMessage Create(string json)
        {
            return new RawJsonMessage
            {
                Json = json
            };
        }

        /// <summary>
        /// Gets the type of the message.
        /// </summary>
        /// <returns>The type of the message.</returns>
        protected override MessageType GetMessageType()
        {
            return MessageType.RawJson;
        }
    }
}

