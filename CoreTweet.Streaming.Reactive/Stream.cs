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
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Concurrency;
using System.Reactive.PlatformServices;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using CoreTweet;
using CoreTweet.Core;
using CoreTweet.Streaming;

namespace CoreTweet.Streaming.Reactive
{

    /// <summary>
    /// Extensions for Reactive Extension(Rx).
    /// </summary>
    public static class Extension
    {
        /// <summary>
        /// Starts the stream.
        /// </summary>
        /// <returns>The observable object.</returns>
        /// <param name="e">Tokens.</param>
        /// <param name="type">Type of streaming API.</param>
        /// <param name="parameters">Parameters.</param>
        public static IObservable<StreamingMessage> StartObservableStream(this StreamingApi e, StreamingType type, StreamingParameters parameters = null)
        {
            if(parameters == null)
                parameters = new StreamingParameters();
            return ReactiveBase(e, type, parameters);
        }

        static StreamReader Connect(Tokens e, StreamingParameters parameters, MethodType type, string url)
        {
            return new StreamReader(e.SendRequest(type, url, parameters.Parameters));
        }


        static IObservable<StreamingMessage> ReactiveBase(this StreamingApi e, StreamingType type, StreamingParameters parameters = null)
        {
            return Observable.Create<StreamingMessage>((observer, cancel) => Task.Factory.StartNew(() =>
            {
                var url = type == StreamingType.User ? "https://userstream.twitter.com/1.1/user.json" :
                          type == StreamingType.Site ? " https://sitestream.twitter.com/1.1/site.json " :
                          type == StreamingType.Filter || type == StreamingType.Public ? "https://stream.twitter.com/1.1/statuses/filter.json" :
                          type == StreamingType.Sample ? "https://stream.twitter.com/1.1/statuses/sample.json" :
                          type == StreamingType.Firehose ? "https://stream.twitter.com/1.1/statuses/firehose.json" : "";

                cancel.ThrowIfCancellationRequested();

                using(var reader = Connect(e.IncludedTokens, parameters, type == StreamingType.Public ? MethodType.Post : MethodType.Get, url))
                {
                    cancel.ThrowIfCancellationRequested();
                    cancel.Register(() => reader.Close());

                    foreach(var s in reader.EnumerateLines().Where(x => !string.IsNullOrEmpty(x)))
                    {
                        observer.OnNext(RawJsonMessage.Create(e.IncludedTokens, s));
                        observer.OnNext(StreamingMessage.Parse(e.IncludedTokens, s));
                    }
                }
            }, cancel, TaskCreationOptions.LongRunning, TaskScheduler.Default));
        }

        static IEnumerable<string> EnumerateLines(this StreamReader streamReader)
        {
            while(!streamReader.EndOfStream)
                yield return streamReader.ReadLine();
        }
    }

}

