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
        
        public static StreamingMessage Parse(TokensBase tokens, string x)
        {
            var j = JObject.Parse(x);
            try
            {
                if(j["text"] != null)
                    return StatusMessage.Parse(tokens, j);
                else if(j["friends"] != null)
                    return CoreBase.Convert<FriendsMessage>(tokens, x);
                else if(j["event"] != null)
                    return EventMessage.Parse(tokens, j);
                else if(j["for_user"] != null)
                    return EnvelopesMessage.Parse(tokens, j);
                else if(j["control"] != null)
                    return CoreBase.Convert<ControlMessage>(tokens, x);
                else
                    return ExtractRoot(tokens, j);
            } 
            catch(ParsingException)
            {
                throw;
            } 
            catch(Exception e)
            {
                throw new ParsingException("on streaming, cannot parse the json", j.ToString(Formatting.Indented), e);
            }
        }

        static StreamingMessage ExtractRoot(TokensBase tokens, JObject jo)
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
                var id = jt.ToObject<IDMessage>();
                id.messageType = MessageType.Delete;
                return id;
            } 
            else if(jo.TryGetValue("scrub_geo", out jt))
            {
                var id = jt.ToObject<IDMessage>();
                id.messageType = MessageType.ScrubGeo;
                return id;
            }
            else if(jo.TryGetValue("limit", out jt))
            {
                return jt.ToObject<LimitMessage>();
            }
            else if(jo.TryGetValue("status_withheld", out jt))
            {
                var id = jt.ToObject<IDMessage>();
                id.messageType = MessageType.StatusWithheld;
                return id;
            } 
            else if(jo.TryGetValue("user_withheld", out jt))
            {
                var id = jt.ToObject<IDMessage>();
                id.messageType = MessageType.UserWithheld;
                return id;
            } 
            else
                throw new ParsingException("on streaming, cannot parse the json", jo.ToString(Formatting.Indented), null);
        }
        
    }

    public class StatusMessage : StreamingMessage
    {
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

    [JsonObject]
    public class FriendsMessage : StreamingMessage,IEnumerable<long>
    {
        [JsonProperty("friends")]
        public long[] Friends { get; set; }

        internal override MessageType GetMessageType()
        {
            return MessageType.Friends;
        }
        
        public IEnumerator<long> GetEnumerator()
        {
            return ((IEnumerable<long>)Friends).GetEnumerator();
        }
        
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return Friends.GetEnumerator();
        }
    }
    
    public class LimitMessage : StreamingMessage
    {
        [JsonProperty("track")]
        public int Track { get; set; }

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
    
    public enum EventTargetType
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

        public EventTargetType TargetType { get; set; }

        public Status TargetStatus { get; set; }

        public CoreTweet.List TargetList { get; set; }

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
            switch(e.TargetType)
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
    
    public class EnvelopesMessage : StreamingMessage
    {
        public long ForUser { get; set; }

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

        public static RawJsonMessage Create(TokensBase t, string json)
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

