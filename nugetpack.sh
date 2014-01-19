#!/bin/sh
nuget pack CoreTweet/CoreTweet.nuspec -OutputDirectory Binary/Nightly
nuget pack CoreTweet.Streaming.Reactive/CoreTweet.Streaming.Reactive.nuspec -OutputDirectory Binary/Nightly
