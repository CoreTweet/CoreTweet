#!/bin/sh
ExternalDependencies/nuget/bin/nuget pack CoreTweet.nuspec -OutputDirectory Binary/Nightly
ExternalDependencies/nuget/bin/nuget pack CoreTweet.Streaming.Reactive.nuspec -OutputDirectory Binary/Nightly
