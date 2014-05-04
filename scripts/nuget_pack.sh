#!/bin/sh
ExternalDependencies/nuget/bin/nuget pack Binary/Nightly/CoreTweet.nuspec -OutputDirectory Binary/Nightly
ExternalDependencies/nuget/bin/nuget pack Binary/Nightly/CoreTweet.Streaming.Reactive.nuspec -OutputDirectory Binary/Nightly
