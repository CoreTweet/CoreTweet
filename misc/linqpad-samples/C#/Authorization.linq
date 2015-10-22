<Query Kind="Statements">
  <Reference Relative="..\..\net45\CoreTweet.dll">&lt;MyDocuments&gt;\Repo\CoreTweet\Release\net45\CoreTweet.dll</Reference>
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>CoreTweet</Namespace>
</Query>

// Get a request token.
var session = OAuth.Authorize(
	"Input your consumer key",
	"Input your consumer secret"
);

session.Dump("Visit AuthorizeUri in a web browser and input the PIN code.");

// Wait until you get the PIN code.
var verifier = Console.ReadLine();

// Get the access token.
var tokens = session.GetTokens(verifier);
tokens.AccessToken.Dump("Access token");
tokens.AccessTokenSecret.Dump("Access token secret");

// Show your user information.
tokens.Account.VerifyCredentials().Dump("That's you!");
