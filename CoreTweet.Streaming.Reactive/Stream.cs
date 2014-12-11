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
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;

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

            return Observable.Create<StreamingMessage>((observer, cancel) =>
            {
                var url = type == StreamingType.User ? "https://userstream.twitter.com/1.1/user.json" :
                          type == StreamingType.Site ? " https://sitestream.twitter.com/1.1/site.json " :
                          type == StreamingType.Filter ? "https://stream.twitter.com/1.1/statuses/filter.json" :
                          type == StreamingType.Sample ? "https://stream.twitter.com/1.1/statuses/sample.json" :
                          type == StreamingType.Firehose ? "https://stream.twitter.com/1.1/statuses/firehose.json" : "";

                return e.IncludedTokens.SendStreamingRequestAsync(type == StreamingType.Filter ? MethodType.Post : MethodType.Get, url, parameters.Parameters, cancel)
                    .ContinueWith(task =>
                    {
                        if(task.IsFaulted)
                            task.Exception.InnerException.Rethrow();

                        return task.Result.GetResponseStreamAsync();
                    }, cancel)
                    .Unwrap()
                    .ContinueWith(task =>
                    {
                        if(task.IsFaulted)
                            task.Exception.InnerException.Rethrow();

                        try
                        {
                            using (var reader = new StreamReader(task.Result))
                            using (var reg = cancel.Register(() => reader.Dispose()))
                            {
                                foreach (var s in reader.EnumerateLines().Where(x => !string.IsNullOrEmpty(x)))
                                {
#if !DEBUG
                                    try
                                    {
#endif
                                    observer.OnNext(StreamingMessage.Parse(s));
#if !DEBUG
                                    }
                                    catch
                                    {
                                        observer.OnNext(RawJsonMessage.Create(s));
                                    }
#endif
                                }
                            }
                        }
                        finally
                        {
                            cancel.ThrowIfCancellationRequested();
                        }
                    }, cancel, TaskContinuationOptions.LongRunning, TaskScheduler.Default);
            });
        }
    }
}
