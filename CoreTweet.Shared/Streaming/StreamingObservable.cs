// The MIT License (MIT)
//
// CoreTweet - A .NET Twitter Library supporting Twitter API 1.1
// Copyright (c) 2013-2018 CoreTweet Development Team
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
using System.Text;
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
            if (observer == null) throw new ArgumentNullException(nameof(observer));

            var conn = new StreamingConnection();
            conn.Start(observer, this.client, this.type, this.parameters);
            return conn;
        }
    }

    internal class StreamingConnection : IDisposable
    {
        private readonly CancellationTokenSource cancel = new CancellationTokenSource();

        public async void Start(IObserver<StreamingMessage> observer, StreamingApi client, StreamingType type, KeyValuePair<string, object>[] parameters)
        {
            var token = this.cancel.Token;

            try
            {
                // Make sure that all the operations are run in background
                var firstTask = Task.Run(() => client.IncludedTokens.SendStreamingRequestAsync(GetMethodType(type), client.GetUrl(type), parameters, token), token);

				// Setting the buffer size is a workaround for streaming delay
				// https://github.com/CoreTweet/CoreTweet/pull/155
                using (var res = await firstTask.ConfigureAwait(false))
                using (var reader = new StreamReader(await res.GetResponseStreamAsync().ConfigureAwait(false), Encoding.UTF8, true, 16384))
                {
                    while (!reader.EndOfStream)
                    {
                        if (token.IsCancellationRequested) return;

                        var line = await reader.ReadLineAsync().ConfigureAwait(false);
                        if (!string.IsNullOrEmpty(line))
                        {
                            StreamingMessage message;
                            try
                            {
                                message = StreamingMessage.Parse(line);
                            }
                            catch (ParsingException ex)
                            {
                                message = RawJsonMessage.Create(line, ex);
                            }

                            if (token.IsCancellationRequested) return;
                            observer.OnNext(message);
                        }
                    }
                }

                observer.OnCompleted();
            }
            catch (Exception ex)
            {
                if (!token.IsCancellationRequested)
                    observer.OnError(ex);
            }
        }

        public void Dispose()
        {
            this.cancel.Cancel();
        }
    }
}
#endif
