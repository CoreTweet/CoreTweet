<Query Kind="FSharpProgram">
  <Reference Relative="..\..\net45\CoreTweet.dll">&lt;MyDocuments&gt;\Repo\CoreTweet\Release\net45\CoreTweet.dll</Reference>
  <NuGetReference>CoreTweet.FSharp</NuGetReference>
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>CoreTweet</Namespace>
</Query>

// This sample cannot be run on LINQPad.
// Please use Visual Studio or fsc.

let tokens =
    Tokens.Create(
        "Input your consumer key",
        "Input your consumer secret",
        "Input your access token",
        "Input your access token secret")

// You can use records to specify parameters by adding CoreTweet.FSharp to reference.
let timeline =
    tokens.Statuses.HomeTimeline(
        { StatusesHomeTimelineParameterDefault with
            count = Some(2)
            exclude_replies = Some(true) })

Dump timeline
