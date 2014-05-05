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
    /// Disconnect code.
    /// </summary>
    public enum DisconnectCode
    {
        Shutdown,
        DuplicateStream,
        ControlRequest,
        Stall,
        Normal,
        TokenRevoked,
        AdminLogout,
        Reserved,
        MaxMessageLimit,
        StreamException,
        BrokerStall,
        ShedLoad
    }

    /// <summary>
    /// Event code.
    /// </summary>
    public enum EventCode
    {
        Block,
        Unblock,
        Favorite,
        Unfavorite,
        Follow,
        Unfollow,
        ListCreated,
        ListDestroyed,
        ListUpdated,
        ListMemberAdded,
        ListMemberRemoved,
        ListUserSubscribed,
        ListUserUnsubscribed,
        UserUpdate
    }

    /// <summary>
    /// Message type.
    /// </summary>
    public enum MessageType
    {
        Delete,
        ScrubGeo,
        StatusWithheld,
        UserWithheld,
        Disconnect,
        Warning,
        Event,
        Envelopes,
        Create,
        Friends,
        Limit,
        Control,
        RawJson
    }

    /// <summary>
    /// Base class of streaming messages.
    /// </summary>
    public abstract class StreamingMessage : CoreBase
    {
        /// <summary>
        /// Gets the type of this message.
        /// </summary>
        /// <value>The type.</value>
        public MessageType Type { get { return GetMessageType(); } }

        /// <summary>
        /// Gets the message type
        /// </summary>
        internal abstract MessageType GetMessageType();

        /// <summary>
        /// Parse the specified json
        /// </summary>
        internal static StreamingMessage Parse(TokensBase tokens, string x)
        {
            var j = JObject.Parse(x);
            try
            {
                if (j["text"] != null)
                    return StatusMessage.Parse(tokens, j);
                else if (j["friends"] != null)
                    return CoreBase.Convert<FriendsMessage>(tokens, x);
                else if (j["event"] != null)
                    return EventMessage.Parse(tokens, j);
                else if (j["for_user"] != null)
                    return EnvelopesMessage.Parse(tokens, j);
                else if (j["control"] != null)
                    return CoreBase.Convert<ControlMessage>(tokens, x);
                else
                    return ExtractRoot(tokens, j);
            }
            catch (ParsingException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new ParsingException("on streaming, cannot parse the json", j.ToString(Formatting.Indented), e);
            }
        }

        /// <summary>
        /// Extracts the root to parse
        /// </summary>
        static StreamingMessage ExtractRoot(TokensBase tokens, JObject jo)
        {
            JToken jt;
            if (jo.TryGetValue("disconnect", out jt))
                return jt.ToObject<DisconnectMessage>();
            else if (jo.TryGetValue("warning", out jt))
                return jt.ToObject<WarningMessage>();
            else if (jo.TryGetValue("control", out jt))
                return jt.ToObject<ControlMessage>();
            else if (jo.TryGetValue("delete", out jt))
            {
                var id = jt.ToObject<IDMessage>();
                id.messageType = MessageType.Delete;
                return id;
            }
            else if (jo.TryGetValue("scrub_geo", out jt))
            {
                var id = jt.ToObject<IDMessage>();
                id.messageType = MessageType.ScrubGeo;
                return id;
            }
            else if (jo.TryGetValue("limit", out jt))
            {
                return jt.ToObject<LimitMessage>();
            }
            else if (jo.TryGetValue("status_withheld", out jt))
            {
                var id = jt.ToObject<IDMessage>();
                id.messageType = MessageType.StatusWithheld;
                return id;
            }
            else if (jo.TryGetValue("user_withheld", out jt))
            {
                var id = jt.ToObject<IDMessage>();
                id.messageType = MessageType.UserWithheld;
                return id;
            }
            else
                throw new ParsingException("on streaming, cannot parse the json", jo.ToString(Formatting.Indented), null);
        }

    }

    /// <summary>
    /// Status message.
    /// </summary>
    public class StatusMessage : StreamingMessage
    {
        /// <summary>
        /// The status.
        /// </summary>
        /// <value>The status.</value>
        public Status Status { get; set; }

        internal override MessageType GetMessageType()
        {
            return MessageType.Create;
        }

        internal static StatusMessage Parse(TokensBase t, JObject j)
        {
            return new StatusMessage()
            {
                Status = j.ToObject<Status>()
            };
        }
    }

    /// <summary>
    /// Message contains ids of friends.
    /// </summary>
    [JsonObject]
    public class FriendsMessage : StreamingMessage, IEnumerable<long>
    {
        /// <summary>
        /// The ids of friends.
        /// </summary>
        /// <value>The friends.</value>
        [JsonProperty("friends")]
        public long[] Friends { get; set; }

        internal override MessageType GetMessageType()
        {
            return MessageType.Friends;
        }

        /// <summary>
        /// IEnumerable\<T\> implementation
        /// </summary>
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
    /// Message that notices limit.
    /// </summary>
    public class LimitMessage : StreamingMessage
    {
        [JsonProperty("track")]
        public int Track { get; set; }

        internal override MessageType GetMessageType()
        {
            return MessageType.Limit;
        }
    }

    /// <summary>
    /// Message contains ids.
    /// </summary>
    public class IDMessage : StreamingMessage
    {
        /// <summary>
        /// ID.
        /// </summary>
        /// <value>The ID.</value>
        [JsonProperty("id")]
        public long Id { get; set; }

        /// <summary>
        /// User's ID.
        /// </summary>
        /// <value>The user ID.</value>
        [JsonProperty("user_id")]
        public long UserId { get; set; }

        /// <summary>
        /// Status ID.
        /// </summary>
        /// <value>Up to status ID.</value>
        [JsonProperty("up_to_status_id")]
        public long? UpToStatusId { get; set; }

        /// <summary>
        /// Withhelds.
        /// </summary>
        /// <value>The withheld in countries.</value>
        [JsonProperty("withheld_in_countries")]
        public string[] WithheldInCountries { get; set; }

        internal MessageType messageType { get; set; }

        internal override MessageType GetMessageType()
        {
            return messageType;
        }
    }

    /// <summary>
    /// Message published when Twitter disconnects the stream.
    /// </summary>
    public class DisconnectMessage : StreamingMessage
    {
        /// <summary>
        /// The disconnect code.
        /// </summary>
        /// <value>The code.</value>
        [JsonProperty("code")]
        public DisconnectCode Code { get; set; }

        /// <summary>
        /// The screen name of current stream.
        /// </summary>
        /// <value>The name of the stream.</value>
        [JsonProperty("stream_name")]
        public string StreamName { get; set; }

        /// <summary>
        /// Human readable message for the reason.
        /// </summary>
        /// <value>The reason.</value>
        [JsonProperty("reason")]
        public string Reason { get; set; }

        internal override MessageType GetMessageType()
        {
            return MessageType.Disconnect;
        }
    }

    /// <summary>
    /// Warning message.
    /// </summary>
    public class WarningMessage : StreamingMessage
    {
        /// <summary>
        /// Warning code.
        /// </summary>
        /// <value>The code.</value>
        [JsonProperty("code")]
        public string Code { get; set; }

        /// <summary>
        /// Warning message.
        /// </summary>
        /// <value>The message.</value>
        [JsonProperty("message")]
        public string Message { get; set; }

        /// <summary>
        /// Percentage of the stall messages
        /// </summary>
        /// <value>The percent full.</value>
        [JsonProperty("percent_full")]
        public int? PercentFull { get; set; }

        /// <summary>
        /// Target user ID.
        /// </summary>
        /// <value>The user ID.</value>
        [JsonProperty("user_id")]
        public long? UserId { get; set; }

        internal override MessageType GetMessageType()
        {
            return MessageType.Warning;
        }
    }

    /// <summary>
    /// Event target type.
    /// </summary>
    public enum EventTargetType
    {
        List,
        Status,
        Null
    }

    /// <summary>
    /// Event message.
    /// </summary>
    public class EventMessage : StreamingMessage
    {
        /// <summary>
        /// The target.
        /// </summary>
        public User Target { get; set; }

        /// <summary>
        /// The source.
        /// </summary>
        public User Source { get; set; }

        /// <summary>
        /// The event code.
        /// </summary>
        public EventCode Event { get; set; }

        /// <summary>
        /// The type of target,
        /// </summary>
        public EventTargetType TargetType { get; set; }

        /// <summary>
        /// The target status.
        /// </summary>
        public Status TargetStatus { get; set; }

        /// <summary>
        /// The target list.
        /// </summary>
        public CoreTweet.List TargetList { get; set; }

        /// <summary>
        /// When this event happened.
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; }

        internal override MessageType GetMessageType()
        {
            return MessageType.Event;
        }

        internal static EventMessage Parse(TokensBase t, JObject j)
        {
            var e = new EventMessage();
            e.Target = j["target"].ToObject<User>();
            e.Source = j["source"].ToObject<User>();
            e.Event = (EventCode)Enum.Parse(typeof(EventCode), ((string)j["event"]).Replace("_", ""), true);
            e.CreatedAt = DateTimeOffset.ParseExact((string)j["created_at"], "ddd MMM dd HH:mm:ss K yyyy",
                                                  System.Globalization.DateTimeFormatInfo.InvariantInfo,
                                                  System.Globalization.DateTimeStyles.AllowWhiteSpaces);
            var eventstr = (string)j["event"];
            e.TargetType = eventstr.Contains("List") ? EventTargetType.List :
                eventstr.Contains("favorite") ? EventTargetType.Status : EventTargetType.Null;
            switch (e.TargetType)
            {
                case EventTargetType.Status:
                    e.TargetStatus = j["target_object"].ToObject<Status>();
                    break;
                case EventTargetType.List:
                    e.TargetList = j["target_object"].ToObject<CoreTweet.List>();
                    break;
                default:
                    break;
            }
            return e;
        }
    }

    /// <summary>
    /// Envelopes message.
    /// </summary>
    public class EnvelopesMessage : StreamingMessage
    {
        /// <summary>
        /// User ID.
        /// </summary>
        public long ForUser { get; set; }

        /// <summary>
        /// The message.
        /// </summary>
        public StreamingMessage Message { get; set; }

        internal override MessageType GetMessageType()
        {
            return MessageType.Envelopes;
        }

        internal static EnvelopesMessage Parse(TokensBase t, JObject j)
        {
            return new EnvelopesMessage()
            {
                ForUser = (long)j["for_user"],
                Message = StreamingMessage.Parse(t, j["message"].ToString(Formatting.None))
            };
        }
    }

    /// <summary>
    /// Control message.
    /// </summary>
    public class ControlMessage : StreamingMessage
    {
        /// <summary>
        /// The URI.
        /// </summary>
        /// <value>The control URI.</value>
        [JsonProperty("control_uri")]
        public string ControlUri { get; set; }

        internal override MessageType GetMessageType()
        {
            return MessageType.Control;
        }
    }

    /// <summary>
    /// Raw JSON message.
    /// </summary>
    public class RawJsonMessage : StreamingMessage
    {
        /// <summary>
        /// The raw JSON.
        /// </summary>
        public string Json { get; set; }

        internal static RawJsonMessage Create(TokensBase t, string json)
        {
            return new RawJsonMessage
            {
                Json = json
            };
        }

        internal override MessageType GetMessageType()
        {
            return MessageType.RawJson;
        }
    }
}

