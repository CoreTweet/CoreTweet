#!/bin/sh
mono ExternalDependencies/nuget/src/CommandLine/bin/Release/NuGet.exe pack CoreTweet.nuspec -OutputDirectory Binary/Nightly
mono ExternalDependencies/nuget/src/CommandLine/bin/Release/NuGet.exe  pack CoreTweet.Streaming.Reactive.nuspec -OutputDirectory Binary/Nightly
