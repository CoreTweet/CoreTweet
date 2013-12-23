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
        public StreamingMessage(Tokens tokens) : base(tokens) { }
        
        public MessageType MessageType { get; set; }
        
        public static StreamingMessage Parse(Tokens tokens, dynamic x)
        {
            if(x.IsDefined("text"))
                return CoreBase.Convert<StatusMessage>(tokens, x);
            else if(x.IsDefined("delete"))
                return CoreBase.Convert<IDMessage>(tokens, x);
            else if(x.IsDefined("friends"))
                return CoreBase.Convert<FriendsMessage>(tokens, x);
            else if(x.IsDefined("event"))
                return CoreBase.Convert<EventMessage>(tokens, x);
            else if(x.IsDefined("limit"))
                return CoreBase.Convert<LimitMessage>(tokens, x);
            else if(x.IsDefined("warning"))
                return CoreBase.Convert<WarningMessage>(tokens, x);
            else if(x.IsDefined("disconnect"))
                return CoreBase.Convert<DisconnectMessage>(tokens, x);
            else if(x.IsDefined("scrub_geo"))
                return CoreBase.Convert<IDMessage>(tokens, x);
            else if(x.IsDefined("status_withheld"))
                return CoreBase.Convert<IDMessage>(tokens, x);
            else if(x.IsDefined("user_withheld"))
                return CoreBase.Convert<IDMessage>(tokens, x);
            else if(x.IsDefined("for_user"))
                return CoreBase.Convert<EnvelopesMessage>(tokens, x);
            else if(x.IsDefined("control"))
                return CoreBase.Convert<ControlMessage>(tokens, x);
            else
                return null;
        }
        
    }
    
    public class StatusMessage : StreamingMessage
    {
        public StatusMessage(Tokens tokens) : base(tokens) { }
        
        public Status Status{ get; set; }
        
        internal override void ConvertBase(dynamic e)
        {
            Status = CoreBase.Convert<Status>(this.Tokens, e);
            MessageType = MessageType.Create;
        }
    }
 
    public class FriendsMessage : StreamingMessage,IEnumerable<long>
    {
        public FriendsMessage(Tokens tokens) : base(tokens) { }
    
        public long[] IDs { get; set; }
        
        public IEnumerator<long> GetEnumerator()
        {
            return ((IEnumerable<long>)IDs).GetEnumerator();
        }
        
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return IDs.GetEnumerator();
        }
        
        internal override void ConvertBase(dynamic e)
        {
			IDs = (long[])e.friends;
            MessageType = MessageType.Friends;
        }
    }
    
    public class LimitMessage : StreamingMessage
    {
        public LimitMessage(Tokens tokens) : base(tokens){ }
        
        public int Limit { get; set; }
        
        internal override void ConvertBase(dynamic e)
        {
            Limit = e.limit;
            MessageType = MessageType.Limit;
        }
    }
    
    public class IDMessage : StreamingMessage
    {
        public IDMessage(Tokens token) : base(token){ }
        
        public long Id { get; set; }
	
        public long UserId { get; set; }
	
        public long? UpToStatusId { get; set; }
	
        public string[] WithheldInCountries { get; set; }
	
        internal override void ConvertBase(dynamic e)
        {
            dynamic x = e.IsDefined("delete") ? e.delete : 
                        e.IsDefined("scrub_geo") ? e.scrub_geo : 
                        e.IsDefined("status_withheld") ? e.status_withheld :
                        e.IsDefined("user_withheld") ? e.user_withheld : 
                        null;
            
            Id = x.id;
            UserId = x.user_id;
            UpToStatusId = x.IsDefined("up_to_status_id") ? x.up_to_status_id : null;
            
            WithheldInCountries = x.IsDefined("withheld_in_countries") ? 
                ((dynamic[])x.withheld_in_countries).Cast<string>().ToArray() : 
                null;
                        
            MessageType = e.IsDefined("delete") ? MessageType.Delete : 
                          e.IsDefined("scrub_geo") ? MessageType.ScrubGeo : 
                          e.IsDefined("status_withheld") ? MessageType.StatusWithheld :
                          MessageType.UserWithheld;
        }
    }

    public class DisconnectMessage : StreamingMessage
    {
        public DisconnectMessage(Tokens token) : base(token){ }
        
        public DisconnectCode Code { get; set; }
	
        public string StreamName { get; set; }
	
        public string Reason { get; set; }
	
        internal override void ConvertBase(dynamic e)
        {
            Code = (DisconnectCode)e.disconnect.code;
            StreamName = e.disconnect.stream_name;
            Reason = e.disconnect.reason;
            MessageType = MessageType.Disconnect;
        }
    }

    public class WarningMessage : StreamingMessage
    {
        public WarningMessage(Tokens token) : base(token){ }
        
        public string Code { get; set; }

        public string Message { get; set; }

        public int PercentFull { get; set; }

        internal override void ConvertBase(dynamic e)
        {
            Code = e.warning.code;
            Message = e.warning.message;
            PercentFull = e.warning.percent_full;
            MessageType = MessageType.Warning;
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
        public EventMessage(Tokens token) : base(token){ }

        public User Target { get; set; }

        public User Source { get; set; }

        public EventCode Event { get; set; }

        public StreamingMessage TargetObject { get; set; }
        
        public EventType TargetType { get; set; }

        public DateTime CreatedAt { get; set; }

        internal override void ConvertBase(dynamic e)
        {
            Target = e.IsDefined("target") ? CoreBase.Convert<User>(this.Tokens, e.target) : null;
            Source = e.IsDefined("source") ? CoreBase.Convert<User>(this.Tokens, e.source) : null;
            EventCode evt;
            Enum.TryParse<EventCode>(Enum.GetNames(typeof(EventCode))
                        .Select(s => s.Select(x => char.IsUpper(x) ? "_" + x.ToString().ToLower() : x.ToString())
                                      .JoinToString()
                                      .Remove(0, 1))
                        .First(x => x.Equals(e.@event)), out evt);
            
            Event = evt;
            var eventStr = Event.ToString();
            
            TargetObject = eventStr.Contains("List") ?
                           CoreBase.Convert<List>(this.Tokens, e.target_object) :
                           eventStr.Contains("favorite") ? 
                           CoreBase.Convert<Status>(this.Tokens, e.target_object) : null;
            
            TargetType = TargetObject == null ? EventType.Null : 
                         eventStr.Contains("List") ? EventType.List : 
                         EventType.Status;
            
            MessageType = MessageType.Event;
        }
    }
    
    public class EnvelopesMessage : StreamingMessage
    {
        public EnvelopesMessage(Tokens token) : base(token){ }
        
        public long ForUser { get; set; }

        public StreamingMessage Message { get; set; }
        
        internal override void ConvertBase(dynamic e)
        {
            ForUser = e.for_user;
            Message = StreamingMessage.Parse(this.Tokens, e.message);
            MessageType = MessageType.Envelopes;
        } 
    }
    
    public class ControlMessage : StreamingMessage
    {
        public ControlMessage(Tokens token) : base(token){ }
        
        public string ControlUri { get; set; }
        
        internal override void ConvertBase(dynamic e)
        {
            ControlUri = e.control.control_uri;
            MessageType = MessageType.Control;
        }
    }
    
    public class RawJsonMessage : StreamingMessage
    {
        public RawJsonMessage(Tokens token) : base(token){ }
        
        public string Json { get; set; }
        
        internal override void ConvertBase(dynamic e)
        {
            Json = e;
        }
    }
}

