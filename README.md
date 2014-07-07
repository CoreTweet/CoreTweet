CoreTweet 
=========

[![Build Status](https://travis-ci.org/CoreTweet/CoreTweet.svg?branch=test%2Ftravis)](https://travis-ci.org/CoreTweet/CoreTweet)

Yet Another .NET Twitter Library...

Simplest authorizing:
```csharp
var session = OAuth.Authorize("consumer_key", "consumer_secret");
var tokens = OAuth.GetTokens(session, "PINCODE");
```

Tweeting is very easy:
```csharp
tokens.Statuses.Update(status => "hello");
```

We provides the most modern way to use Twitter's API asynchronously:
```csharp
var tokenSource = new CancellationTokenSource();
tokens.Statuses.UpdateWithMediaAsync(
    new { status = "Yummy!", media = new FileInfo(@"C:\test.jpg") },
    tokenSource.Token
);
// oh! that was a photo of my dog!!
tokenSource.Cancel();
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

## Platforms

We support both of Windows .NET and Mono, and CoreTweet works on following platforms:

* .NET Framework 3.5 (without Rx support)
* .NET Framework 4.0
* .NET Framework 4.5
* Windows 8
* Windows Phone 8 Silverlight
* Windows Phone 8.1
* Xamarin Android / iOS

## Files

CoreTweet.dll ... the main library

CoreTweet.Streaming.Reactive.dll ... the extension for Rx

## Documentation

Documents of API is [here](http://coretweet.github.io/docs/index.html).

Visit [Wiki](https://github.com/CoreTweet/CoreTweet/wiki) to get more information such as examples.

## Install

Now available in [NuGet](https://www.nuget.org/packages/CoreTweet)!

Or please download a binary from [Releases](https://github.com/lambdalice/CoreTweet/releases).

## Build

You can't build PCL/WindowsRT binaries on Mono (on Linux) because they requires non-free libraries.

### On Windows

#### Requires

* .NET Framework 4.5
* Windows PowerShell
* Visual Studio 2013
* Xamarin Starter

#### Step

* Run PowerShell as an admin and execute

```
Set-ExecutionPolicy AllSigned
```

* Run build.ps1

### On Linux and other Unix-like

#### Requires

* Mono 3.x
* make
* XBuild
* Doxygen (if not installed, automatically build from source)

#### Step

* Run make

## Contributing

CoreTweet is not stable and need tests. Report to [Issues](https://github.com/CoreTweet/CoreTweet/issues?state=open) if you find any problems.

We seriously need your help for writing documents.

Please go to [Wiki](https://github.com/CoreTweet/CoreTweet/wiki) and write API documents, articles or/and some tips!

Pull requests are welcome. Write, write, write and send!

## License

This software is licensed under the MIT License.
