<Query Kind="Statements">
  <Reference Relative="..\..\net45\CoreTweet.dll">&lt;MyDocuments&gt;\Repo\CoreTweet\Release\net45\CoreTweet.dll</Reference>
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>CoreTweet</Namespace>
</Query>

// Get the access token.
var apponly = OAuth2.GetToken(
	"Input your consumer key",
	"Input your consumer secret"
);
	
apponly.BearerToken.Dump("Access token");

// Test
apponly.Search.Tweets(q: "CoreTweet").Dump();
