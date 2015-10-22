<Query Kind="FSharpProgram">
  <Reference Relative="..\..\net45\CoreTweet.dll">&lt;MyDocuments&gt;\Repo\CoreTweet\Release\net45\CoreTweet.dll</Reference>
  <NuGetReference>CoreTweet.FSharp</NuGetReference>
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>CoreTweet</Namespace>
</Query>

// This sample cannot run on LINQPad.
// Please try on Visual Studio or fsc.

let tokens =
    Tokens.Create(
        "Input the consumer key",
        "Input the consumer key",
        "Input the access token",
        "Input the access token secret")

// Adding CoreTweet.FSharp to reference, you can use records to specify parameters.
let timeline =
    tokens.Statuses.HomeTimeline(
        { StatusesHomeTimelineParameterDefault with
            count = Some(2)
            exclude_replies = Some(true) })

Dump timeline