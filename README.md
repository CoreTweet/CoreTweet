CoreTweet
=========

Yet another .NET Twitter Library...

Simplest authorizing:
```csharp
var session = OAuth.Authorize("consumer_key", "consumer_secret");
var tokens = OAuth.GetTokens(session, "PINCODE");
```

Tweeting is very easy:
```csharp
tokens.Statuses.Update(status => "hello");
```

Go with the Streaming API and LINQ:
```csharp
foreach(var status in tokens.Streaming.StartStream(StreamingType.Sample)
                                      .OfType<StatusMessage>()
                                      .Select(x => x.Status))
    Console.WriteLine("{0}: {1}", status.User.ScreenName, status.Text);
```

Get fantastic experiences with Rx:
```csharp
using CoreTweet.Streaming.Reactive;

var stream = t.Streaming.StartObservableStream(StreamingType.Filter, new StreamingParameters(track => "tea")).Publish();

stream.OfType<StatusMessage>()
    .Subscribe(x => Console.WriteLine("{0} says about tea: {1}", x.Status.User.ScreenName, x.Status.Text));

var disposable = stream.Connect();
await Task.Delay(30 * 1000);
disposable.Dispose();
```

Oh yes why don't you throw away any ```StatusUpdateOptions``` and it kinds???

## Files

CoreTweet.dll ... the main library

CoreTweet.Streaming.Reactive.dll ... the extension for Rx

## License

This software is licensed under the MIT License.

## Install

Now available in [NuGet](https://www.nuget.org/packages/CoreTweet)!

Or please download a binary from [Releases](https://github.com/lambdalice/CoreTweet/releases).

## Website

To see the document of CoreTweet, visit [our website](http://lambdalice.github.io/CoreTweet/) now!
