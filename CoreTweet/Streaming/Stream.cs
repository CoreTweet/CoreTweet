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
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using CoreTweet.Core;

namespace CoreTweet.Streaming
{
    /// <summary>
    /// Types of twitter streaming.
    /// </summary>
    public enum StreamingType
    {
        /// <summary>
        /// The user stream.
        /// </summary>
        User,
        /// <summary>
        /// The site stream.
        /// </summary>
        Site,
        /// <summary>
        /// The filter stream.
        /// </summary>
        Filter,
        /// <summary>
        /// The sample stream.
        /// </summary>
        Sample,
        /// <summary>
        /// The firehose stream.
        /// </summary>
        Firehose
    }
    
    public class StreamingApi : ApiProviderBase
    {
        protected internal StreamingApi(TokensBase tokens) : base(tokens) { }

#if !PCL
        IEnumerable<string> Connect(StreamingParameters parameters, MethodType type, string url)
        {
            using(var str = this.Tokens.SendRequest(type, url, parameters.Parameters))
            using(var reader = new StreamReader(str.GetResponseStream()))
                foreach(var s in reader.EnumerateLines()
                                       .Where(x => !string.IsNullOrEmpty(x)))
                    yield return s;
        }

        /// <summary>
        /// Starts the twitter stream.
        /// </summary>
        /// <returns>
        /// The stream messages.
        /// </returns>
        /// <param name='type'>
        /// Type of streaming.
        /// </param>
        /// <param name='parameters'>
        /// Parameters of streaming.
        /// </param>
        public IEnumerable<StreamingMessage> StartStream(StreamingType type, StreamingParameters parameters = null)
        {
            if(parameters == null)
                parameters = new StreamingParameters();

            var url = type == StreamingType.User ? "https://userstream.twitter.com/1.1/user.json" : 
                      type == StreamingType.Site ? " https://sitestream.twitter.com/1.1/site.json " :
                      type == StreamingType.Filter ? "https://stream.twitter.com/1.1/statuses/filter.json" :
                      type == StreamingType.Sample ? "https://stream.twitter.com/1.1/statuses/sample.json" :
                      type == StreamingType.Firehose ? "https://stream.twitter.com/1.1/statuses/firehose.json" : "";
            
            var str = this.Connect(parameters, type == StreamingType.Filter ? MethodType.Post : MethodType.Get, url)
                .Where(x => !string.IsNullOrEmpty(x));
            
            foreach(var s in str)
            {
                yield return RawJsonMessage.Create(this.Tokens, s) ;
                yield return StreamingMessage.Parse(this.Tokens, s);
            }
        }
#endif
    }
    
    /// <summary>
    /// Parameters for streaming API.
    /// </summary>
    public class StreamingParameters
    {
        /// <summary>
        /// Gets the raw parameters.
        /// </summary>
        /// <value>
        /// The parameters.
        /// </value>
        public List<KeyValuePair<string, object>> Parameters { get; private set; }
        
        /// <summary>
        /// <para>Initializes a new instance of the <see cref="CoreTweet.Streaming.StreamingParameters"/> class.</para>
        /// <para>Avaliable parameters: </para>
        /// <para>*Note: In filter stream, at least one predicate parameter (follow, locations, or track) must be specified.</para>
        /// <para><paramref name="bool stall_warnings (optional)"/> : Specifies whether stall warnings should be delivered.</para>
        /// <para><paramref name="string, IEnumerable<string> follow (optional*, required in site stream, ignored in user stream)"/> : A list of user IDs or comma separated string of ones, indicating the users to return statuses for in the stream. </para>
        /// <para><paramref name="string, IEnumerable<string> track (optional*)"/> : Keywords to track. Phrases of keywords are specified by a list or comma separated string. </para>
        /// <para><paramref name="string, IEnumerable<string> location (optional*)"/> : A list of longitude,latitude pairs or comma separated string of ones, specifying a set of bounding boxes to filter Tweets by. example: "-74,40,-73,41" </para>
        /// <para><paramref name="string with (optional)"/> : Specifies whether to return information for just the authenticating user (with => "user"), or include messages from accounts the user follows (with => "followings").</para>
        /// </summary>
        /// <param name='streamingParameters'>
        /// Streaming parameters.
        /// </param>
        /// <seealso cref="http://dev.twitter.com/docs/streaming-apis/parameters"/>
        public StreamingParameters(params Expression<Func<string,object>>[] streamingParameters)
         : this(InternalUtils.ExpressionsToDictionary(streamingParameters)) { }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="CoreTweet.Streaming.StreamingParameters"/> class.
        /// </summary>
        /// <param name='streamingParameters'>
        /// Streaming parameters.
        /// </param>
        /// <seealso cref="http://dev.twitter.com/docs/streaming-apis/parameters"/>
        public StreamingParameters(IEnumerable<KeyValuePair<string, object>> streamingParameters)
        {
            Parameters = streamingParameters.ToList();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CoreTweet.Streaming.StreamingParameters"/> class.
        /// </summary>
        /// <param name='streamingParameters'>
        /// Streaming parameters.
        /// </param>
        /// <seealso cref="http://dev.twitter.com/docs/streaming-apis/parameters"/>
        public static StreamingParameters Create<T>(T streamingParameters)
        {
            return new StreamingParameters(InternalUtils.ResolveObject(streamingParameters));
        }
    }

}

