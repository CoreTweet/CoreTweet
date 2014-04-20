#!/bin/sh
mono ExternalDependencies/nuget/NuGet.exe pack CoreTweet.nuspec -OutputDirectory Binary/Nightly
mono ExternalDependencies/nuget/NuGet.exe pack CoreTweet.Streaming.Reactive.nuspec -OutputDirectory Binary/Nightly
