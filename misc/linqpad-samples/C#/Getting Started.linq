<Query Kind="Statements">
  <Reference Relative="..\..\net45\CoreTweet.dll">&lt;MyDocuments&gt;\Repo\CoreTweet\Release\net45\CoreTweet.dll</Reference>
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>CoreTweet</Namespace>
  <Namespace>CoreTweet.Streaming</Namespace>
</Query>

var tokens = Tokens.Create(
	"Input the consumer key",
	"Input the consumer key",
	"Input the access token",
	"Input the access token secret"
);

var apponly = OAuth2Token.Create(
	"Input the consumer key",
	"Input the consumer key",
	"Input the access token"
);

// Write the code to run below.
// If you need some help, visit our wiki:
// https://github.com/CoreTweet/CoreTweet/wiki

tokens.Statuses.Update(status: "Hello, CoreTweet!").Dump();