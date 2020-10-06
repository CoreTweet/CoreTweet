CoreTweet
=========

[![Build Status on Travis CI](https://travis-ci.org/CoreTweet/CoreTweet.svg?branch=test%2Ftravis)](https://travis-ci.org/CoreTweet/CoreTweet)
[![Build Status on AppVeyor](https://ci.appveyor.com/api/projects/status/github/CoreTweet/CoreTweet)](https://ci.appveyor.com/project/azyobuzin/CoreTweet)
[![Gitter chat](https://badges.gitter.im/CoreTweet/gitter.png)](https://gitter.im/CoreTweet)
[![Standard](https://img.shields.io/endpoint?url=https%3A%2F%2Ftwbadges.glitch.me%2Fbadges%2Fstandard)](https://developer.twitter.com/en/docs/twitter-api)
[![v2](https://img.shields.io/endpoint?url=https%3A%2F%2Ftwbadges.glitch.me%2Fbadges%2Fv2)](https://developer.twitter.com/en/docs/twitter-api)

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

We provide the most modern way to use Twitter's API asynchronously:
```csharp
var tokenSource = new CancellationTokenSource();
var task = tokens.Statuses.UpdateWithMediaAsync(
    new { status = "Yummy!", media = new FileInfo(@"C:\test.jpg") },
    tokenSource.Token
);
// oh! that was a photo of my dog!!
tokenSource.Cancel();
```

Go with the Streaming API and LINQ:
```csharp
var sampleStream = tokens.Streaming.Sample()
    .OfType<StatusMessage>()
    .Select(x => x.Status);
foreach(var status in sampleStream)
    Console.WriteLine("{0}: {1}", status.User.ScreenName, status.Text);
```

Get fantastic experiences with Rx:
```csharp
var disposable = tokens.Streaming.FilterAsObservable(track => "tea")
    .OfType<StatusMessage>()
    .Subscribe(x => Console.WriteLine("{0} says about tea: {1}", x.Status.User.ScreenName, x.Status.Text));

await Task.Delay(30 * 1000);
disposable.Dispose();
```

Various types of method overloads:
```csharp
tokens.Statuses.Update(status => "hello");

tokens.Statuses.Update(new { status = "hello" });

tokens.Statuses.Update(new YourClass("hello"));

tokens.Statuses.Update(status: "hello");

tokens.Statuses.Update(new Dictionary<string, object>()
{
    {"status", "hello"}
});
```

Oh yes why don't you throw away any ```StatusUpdateOptions``` and it kinds???

## Latest Build Results

* [Mono on Linux](https://travis-ci.org/CoreTweet/CoreTweet)
* [Microsoft .NET Framework on Windows Azure](https://ci.appveyor.com/project/azyobuzin/CoreTweet)

## Platforms

We support both of Windows .NET and Mono, and CoreTweet works on following platforms:

* .NET Framework 3.5 (without Rx support)
* .NET Framework 4.0
* .NET Framework 4.5
* .NET Standard 1.3
* Universal Windows Platform
* Xamarin Android / iOS

## Documentation

Documents of API is [here](http://coretweet.github.io/docs/index.html).

Visit [Wiki](https://github.com/CoreTweet/CoreTweet/wiki) to get more information such as examples.

## Install

Now available on [NuGet](https://www.nuget.org/packages/CoreTweet)!
```
PM> Install-Package CoreTweet
```

Or please download a binary from [Releases](https://github.com/CoreTweet/CoreTweet/releases).

## Build

You can't build PCL/WindowsRT binaries on Mono (on Linux) because they require non-free libraries.

### On Windows

#### Requires

* .NET Framework 4.6
* Windows PowerShell
* Visual Studio 2017
* .NET Core 3.1 SDK
* Doxygen (optional: used to generate documentation)

#### Step

* Run PowerShell as an admin and execute

```
Set-ExecutionPolicy AllSigned
```

* Run build.ps1

### On Linux and other Unix-like

#### Requires

* Mono 4.x or above
* make
* XBuild
* Doxygen (optional: used to generate documentation)

#### Step

* Run make

## Contributing

Please report to [Issues](https://github.com/CoreTweet/CoreTweet/issues?state=open) if you find any problems.

We seriously need your help for writing documents.

Please go to [Wiki](https://github.com/CoreTweet/CoreTweet/wiki) and write API documentation, articles and/or some tips!

Pull requests are welcome.

## License

This software is licensed under the MIT License.
