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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using Codeplex.Data;
using CoreTweet;
using CoreTweet.Core;
using Alice.Extensions;
using Alice.Functional.Monads;

namespace CoreTweet.Streaming
{
    public enum StreamingType
    {
        User,
        Site,
        Public
    }
    
    public partial class StreamingApi : TokenIncluded
    {
        protected internal StreamingApi(Tokens tokens) : base(tokens) { }

        IEnumerable<string> Connect(StreamingParameters parameters, MethodType type, string url)
        {
            using(var str = this.Tokens.SendRequest(type, url, parameters.Parameters))
            using(var reader = new StreamReader(str))
                foreach(var s in reader.EnumerateLines()
                                       .Where(x => !string.IsNullOrEmpty(x)))
                    yield return s;
        }
        
        /// <summary>
        /// <para> Starts the stream. </para>
        /// <para>
        /// <paramref name="Create(StatusMessage)"/> : This is called when a new status was created.
        /// </para>
        /// <para>
        /// <paramref name="Delete(IDMessage)"/> : This is called when a status was deleted.
        /// </para>
        /// <para>
        /// <paramref name="Friends(FriendsMessage)"/> : This is called when the user stream starts.
        /// </para>
        /// <para>
        /// <paramref name="Events(EventsMessage)"/> : This is called when a event happens.
        /// </para>
        /// <para>
        /// <paramref name="Limit(LimitMessage)"/> : This is called when some API reaches the own limit.
        /// </para>
        /// <para>
        /// <paramref name="Warning(WarningMessage)"/> : This is called when the warning message was sent.
        /// Add the "stall_warning => true" to the parameters to use this endpoint.
        /// </para>
        /// <para>
        /// <paramref name="Disconnect(DisconnectMessage)"/> : This is called when the connection is disconnected.
        /// </para>
        /// <para>
        /// <paramref name="ScrubGeo(IDMessage)"/> : This is called when the geo data is deleted.
        /// </para>
        /// <para>
        /// <paramref name="StatusWithheld(IDMessage)"/> : This is called when status withheld happens.
        /// </para>
        /// <para>
        /// <paramref name="UserWithheld(IDMessage)"/> : This is called when user withheld happens.
        /// </para>
        /// <para>
        /// <paramref name="Envelopes(EnvelopesMessage)"/> : This is called when Site Streams are sent the message.
        /// </para>
        /// <para>
        /// <paramref name="Control(ControlMessage)"/> : This is called when a control message are sent.
        /// </para>
        /// <para>
        /// <paramref name="RawJson(RawJsonMessage)"/> : This is the endpoint to get the raw json data.
        /// </para>
        /// </summary>
        /// <param name='parameters'>
        /// Parameters for Streaming API.
        /// </param>
        /// <param name='type'>
        /// Type of Streaming API.
        /// </param>
        /// <param name='streamingActions'>
        /// Actions for streaming API.
        /// </param>
        public void StartStream(StreamingParameters parameters, 
                                StreamingType type,
                                params Expression<Func<string,Action<StreamingMessage>>>[] streamingActions)
        {
            //new Task(() => 
			{
    
                var actions = streamingActions.ToDictionary(e => (MessageType)Enum.Parse(typeof(MessageType), e.Parameters[0].Name),
                                                            e => e.Compile()(""));
                this.StartStream(parameters,type).ForEach(x => 
                {
                    var err = new Error<Action<StreamingMessage>>(
						() => actions.First(y => y.Key == x.MessageType).Value);

					if(!err.IsError)
						err.Value(x);
                });
            }//).Start();
            
        }

		public IEnumerable<StreamingMessage> StartStream(StreamingParameters parameters, StreamingType type)
		{
			var url = type == StreamingType.User ? "https://userstream.twitter.com/1.1/user.json" : 
				type == StreamingType.Site ? " https://sitestream.twitter.com/1.1/site.json " :
					type == StreamingType.Public ? "https://stream.twitter.com/1.1/statuses/filter.json" : "";
			
			var str = this.Connect(parameters, type == StreamingType.Public ? MethodType.Post : MethodType.Get, url)
				.Where(x => !string.IsNullOrEmpty(x));
			
			foreach(var s in str)
			{
				yield return CoreBase.Convert<RawJsonMessage>(this.Tokens, s);
				yield return StreamingMessage.Parse(this.Tokens, DynamicJson.Parse(s));
			}
		}

         
        
        
    }
    
    /// <summary>
    /// Parameters for streaming API.
    /// </summary>
    public class StreamingParameters
    {
        public IDictionary<string,object> Parameters { get; private set; }
        
        /// <summary>
        /// <para>Initializes a new instance of the <see cref="CoreTweet.Streaming.StreamingParameters"/> class.</para>
        /// <para>Avaliable parameters: </para>
        /// <para>*Note: In filter stream, at least one predicate parameter (follow, locations, or track) must be specified.</para>
        /// <para><paramref name="bool stall_warnings (optional)"/> : Specifies whether stall warnings should be delivered.</para>
        /// <para><paramref name="string follow (optional*, required in site stream, ignored in user stream)"/> : A comma separated list of user IDs, indicating the users to return statuses for in the stream. </para>
        /// <para><paramref name="string track (optional*)"/> : Keywords to track. Phrases of keywords are specified by a comma-separated list. </para>
        /// <para><paramref name="string location (optional*)"/> : A comma-separated list of longitude,latitude pairs specifying a set of bounding boxes to filter Tweets by. example: "-74,40,-73,41" </para>
        /// <para><paramref name="string with (optional)"/> : Specifies whether to return information for just the authenticating user (with => "user"), or include messages from accounts the user follows (with => "followings").</para>
        /// </summary>
        /// <param name='streamingParameters'>
        /// Streaming parameters.
        /// </param>
        /// <seealso cref="http://dev.twitter.com/docs/streaming-apis/parameters"/>
        public StreamingParameters(params Expression<Func<string,object>>[] streamingParameters)
         : this(streamingParameters.ToDictionary(e => e.Parameters[0].Name, e => e.Compile()(""))) { }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="CoreTweet.Streaming.StreamingParameters"/> class.
        /// </summary>
        /// <param name='streamingParameters'>
        /// Streaming parameters.
        /// </param>
        /// <seealso cref="http://dev.twitter.com/docs/streaming-apis/parameters"/>
        public StreamingParameters(IDictionary<string,object> streamingParameters)
		{
            Parameters = streamingParameters;
		}
    }

}

