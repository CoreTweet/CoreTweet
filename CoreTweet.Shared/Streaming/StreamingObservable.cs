// The MIT License (MIT)
//
// CoreTweet - A .NET Twitter Library supporting Twitter API 1.1
// Copyright (c) 2013-2016 CoreTweet Development Team
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

#if ASYNC
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static CoreTweet.Streaming.StreamingApi;

namespace CoreTweet.Streaming
{
    internal class StreamingObservable : IObservable<StreamingMessage>
    {
        public StreamingObservable(StreamingApi client, StreamingType type, IEnumerable<KeyValuePair<string, object>> parameters)
        {
            this.client = client;
            this.type = type;
            this.parameters = parameters.ToArray();
        }

        private readonly StreamingApi client;
        private readonly StreamingType type;
        private readonly KeyValuePair<string, object>[] parameters;

        public IDisposable Subscribe(IObserver<StreamingMessage> observer)
        {
            var conn = new StreamingConnection();
            conn.Start(observer, this.client, this.type, this.parameters);
            return conn;
        }
    }

    internal class StreamingConnection : IDisposable
    {
        private readonly CancellationTokenSource cancel = new CancellationTokenSource();

        public void Start(IObserver<StreamingMessage> observer, StreamingApi client, StreamingType type, KeyValuePair<string, object>[] parameters)
        {
            var token = this.cancel.Token;
            client.IncludedTokens.SendStreamingRequestAsync(GetMethodType(type), client.GetUrl(type), parameters, token)
                .Done(res => res.GetResponseStreamAsync(), token)
                .Unwrap()
                .Done(stream =>
                {
                    using(var reader = new StreamReader(stream))
                    using(token.Register(reader.Dispose))
                    {
                        foreach(var s in reader.EnumerateLines().Where(x => !string.IsNullOrEmpty(x)))
                        {
                            try
                            {
                                observer.OnNext(StreamingMessage.Parse(s));
                            }
                            catch(ParsingException ex)
                            {
                                observer.OnNext(RawJsonMessage.Create(s, ex));
                            }
                        }
                    }
                    observer.OnCompleted();
                }, token, TaskContinuationOptions.LongRunning)
                .ContinueWith(t =>
                {
                    if(!token.IsCancellationRequested)
                    {
                        if(t.Exception != null)
                            observer.OnError(t.Exception.InnerExceptions.Count == 1 ? t.Exception.InnerException : t.Exception);
                        else if(t.IsCanceled)
                            observer.OnError(new TaskCanceledException(t));
                    }
                });
        }

        public void Dispose()
        {
            this.cancel.Cancel();
        }
    }
}
#endif
