#!/bin/sh
nuget pack CoreTweet.nuspec -OutputDirectory Binary/Nightly
nuget pack CoreTweet.Streaming.Reactive.nuspec -OutputDirectory Binary/Nightly
