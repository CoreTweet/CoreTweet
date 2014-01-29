// The MIT License (MIT)
//
// CoreTweet - A .NET Twitter Library supporting Twitter API 1.1
// Copyright (c) 2013 lambdalice
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
using System.Linq;
using System.Collections.Generic;
using CoreTweet;
using CoreTweet.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Alice.Extensions;

namespace CoreTweet.Streaming
{
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

    public abstract class StreamingMessage : CoreBase
    {
        public MessageType Type { get { return GetMessageType(); } }

        internal abstract MessageType GetMessageType();
        
        public static StreamingMessage Parse(Tokens tokens, string x)
        {
            var j = JObject.Parse(x);
            if(j["text"] != null)
                return StatusMessage.Parse(tokens, x);
            else if(j["delete"] != null)
                return CoreBase.Convert<IDMessage>(tokens, x);
            else if(j["friends"] != null)
                return CoreBase.Convert<FriendsMessage>(tokens, x);
            else if(j["event"] != null)
                return EventMessage.Parse(tokens, x);
            else if(j["limit"] != null)
                return CoreBase.Convert<LimitMessage>(tokens, x);
            else if(j["warning"] != null)
                return CoreBase.Convert<WarningMessage>(tokens, x);
            else if(j["disconnect"] != null)
                return CoreBase.Convert<DisconnectMessage>(tokens, x);
            else if(j["scrub_geo"] != null)
                return CoreBase.Convert<IDMessage>(tokens, x);
            else if(j["status_withheld"] != null)
                return CoreBase.Convert<IDMessage>(tokens, x);
            else if(j["user_withheld"] != null)
                return CoreBase.Convert<IDMessage>(tokens, x);
            else if(j["for_user"] != null)
                return EnvelopesMessage.Parse(tokens, x);
            else if(j["control"] != null)
                return CoreBase.Convert<ControlMessage>(tokens, x);
            else
                return null;
        }

        static StreamingMessage ExtractRoot(Tokens tokens, string s)
        {
            var jo = JObject.Parse(s);
            JToken jt;
            if(jo.TryGetValue("disconnect", out jt))
            {
                var x = jt.ToObject<DisconnectMessage>();
                x.Tokens = tokens;
                return x;
            } 
            else if(jo.TryGetValue("warning", out jt))
            {
                var x = jt.ToObject<WarningMessage>();
                x.Tokens = tokens;
                return x;
            } 
            else if(jo.TryGetValue("control", out jt))
            {
                var x = jt.ToObject<ControlMessage>();
                x.Tokens = tokens;
                return x;
            }
            else if(jo.TryGetValue("delete", out jt))
            {
                var id = jt.ToObject<IDMessage>();
                id.messageType = MessageType.Delete;
                id.Tokens = tokens;
                return id;
            }
            else if(jo.TryGetValue("scrub_gep", out jt))
            {
                var id = jt.ToObject<IDMessage>();
                id.messageType = MessageType.ScrubGeo;
                id.Tokens = tokens;
                return id;
            }
            else if(jo.TryGetValue("status_withheld", out jt))
            {
                var id = jt.ToObject<IDMessage>();
                id.messageType = MessageType.StatusWithheld;
                id.Tokens = tokens;
                return id;
            }
            else if(jo.TryGetValue("user_withheld", out jt))
            {
                var id = jt.ToObject<IDMessage>();
                id.messageType = MessageType.UserWithheld;
                id.Tokens = tokens;
                return id;
            }
            else throw new InvalidOperationException("cannot extract the json: " + s);
        }
        
    }

    public class StatusMessage : StreamingMessage
    {
        public Status Status { get; set; }

        internal override MessageType GetMessageType()
        {
            return MessageType.Create;
        }

        internal static StatusMessage Parse(Tokens t, string json)
        {
            var s = new StatusMessage();
            var j = JObject.Parse(json);
            s.Status = j.ToObject<Status>();
            s.Tokens = t;
            return s;
        }
    }

    [JsonObject]
    public class FriendsMessage : StreamingMessage,IEnumerable<long>
    {
        [JsonProperty("ids")]
        public long[] IDs { get; set; }

        internal override MessageType GetMessageType()
        {
            return MessageType.Friends;
        }
        
        public IEnumerator<long> GetEnumerator()
        {
            return ((IEnumerable<long>)IDs).GetEnumerator();
        }
        
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return IDs.GetEnumerator();
        }
    }
    
    public class LimitMessage : StreamingMessage
    {
        [JsonProperty("limit")]
        public int Limit { get; set; }

        internal override MessageType GetMessageType()
        {
            return MessageType.Limit;
        }
    }
    
    public class IDMessage : StreamingMessage
    {
        [JsonProperty("id")]
        public long ID { get; set; }
    
        [JsonProperty("user_id")]
        public long UserID { get; set; }
    
        [JsonProperty("up_to_status_id")]
        public long? UpToStatusID { get; set; }
    
        [JsonProperty("withheld_in_countries")]
        public string[] WithheldInCountries { get; set; }

        internal MessageType messageType { get; set; }

        internal override MessageType GetMessageType()
        {
            return messageType;
        }
    }

    public class DisconnectMessage : StreamingMessage
    {
        [JsonProperty("code")]
        public DisconnectCode Code { get; set; }
    
        [JsonProperty("stream_name")]
        public string StreamName { get; set; }
    
        [JsonProperty("reason")]
        public string Reason { get; set; }
    
        internal override MessageType GetMessageType()
        {
            return MessageType.Disconnect;
        }
    }

    public class WarningMessage : StreamingMessage
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("percent_full")]
        public int PercentFull { get; set; }

        internal override MessageType GetMessageType()
        {
            return MessageType.Warning;
        } 
    }
    
    public enum EventType
    {
        List,
        Status,
        Null
    }

    public class EventMessage : StreamingMessage
    {
        public User Target { get; set; }

        public User Source { get; set; }

        public EventCode Event { get; set; }

        public EventType TargetType { get; set; }

        public CoreBase TargetObject { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        internal override MessageType GetMessageType()
        {
            return MessageType.Event;
        }

        internal static EventMessage Parse(Tokens t, string json)
        {
            var e = new EventMessage();
            var j = JObject.Parse(json);
            e.Target = j["target"].ToObject<User>();
            e.Source = j["source"].ToObject<User>();
            e.Event = (EventCode)Enum.Parse(typeof(EventCode), (string)j["event"], true);
            e.CreatedAt = DateTimeOffset.ParseExact((string)j["created_at"], "ddd MMM dd HH:mm:ss K yyyy",
                                                  System.Globalization.DateTimeFormatInfo.InvariantInfo, 
                                                  System.Globalization.DateTimeStyles.AllowWhiteSpaces);
            var eventstr = (string)j["event"];
            e.TargetType = eventstr.Contains("List") ? EventType.List : 
                eventstr.Contains("favorite") ? EventType.Status : EventType.Null;
            switch(e.TargetType)
            {
                case EventType.Status:
                    e.TargetObject = j["target_object"].ToObject<Status>();
                    break;
                case EventType.List:
                    e.TargetObject = j["target_object"].ToObject<List>();
                    break;
                default:
                    e.TargetObject = null;
                    break;
            }
            e.Tokens = t;
            return e;
        }
    }
    
    public class EnvelopesMessage : StreamingMessage
    {
        public long ForUser { get; set; }

        public StreamingMessage Message { get; set; }

        internal override MessageType GetMessageType()
        {
            return MessageType.Envelopes;
        }

        internal static EnvelopesMessage Parse(Tokens t, string json)
        {
            var e = new EnvelopesMessage();
            var j = JObject.Parse(json);
            e.ForUser = (long)j["for_user"];
            e.Message = StreamingMessage.Parse(t, j["message"].ToString(Formatting.None));
            e.Tokens = t;
            return e;
        }
    }
    
    public class ControlMessage : StreamingMessage
    {
        [JsonProperty("control_uri")] 
        public string ControlUri { get; set; }
           
        internal override MessageType GetMessageType()
        {
            return MessageType.Control;
        }
    }
    
    public class RawJsonMessage : StreamingMessage
    {
        public string Json { get; set; }

        public static RawJsonMessage Create(Tokens t, string json)
        {
            return new RawJsonMessage
            {
                Json = json,
                Tokens = t
            };
        }

        internal override MessageType GetMessageType()
        {
            return MessageType.RawJson;
        }
    }
}

