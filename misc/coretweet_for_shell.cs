// CoreTweet for Mono C# Shell
//
// Required Files:
// CoreTweet.Streaming.Reactive.dll CoreTweet.dll Newtonsoft.Json.dll
//
// * Place this script and required dlls into $HOME/.scripts/csharp .
// * You need an internet connection to use this (at least the first time).
// * You also need a consumer key and a consumer secret key.
// * Your data will be saved to $HOME/.twtokens (on *nix) or $(Environment.SpecialFolder.ApplicationData)\csharp\twtokens.xml (on Windows).
// * Your tokens will be appear as an variable 'tokens' and 'apponly'.

using CoreTweet;
using CoreTweet.Core;
using CoreTweet.Streaming;
LoadAssembly("System.Runtime.Serialization");
using System.Xml;
using System.Runtime.Serialization;
using System.IO;
Tokens tokens;
OAuth2Token apponly;
{
  var ds = new DataContractSerializer(typeof(TokensBase[]));
  var unix = (Environment.OSVersion.Platform == PlatformID.Unix || Environment.OSVersion.Platform == PlatformID.MacOSX);
  var tf = unix ? ".twtokens" : "twtokens.xml";
  var home = unix ? Environment.GetEnvironmentVariable("HOME") : Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "csharp");
  var tpath = Path.Combine(home, tf);
  if(!File.Exists(tpath)) {
      Console.WriteLine
(@"== CoreTweet for Mono C# Shell ==
==        Setup  Wizard        ==
* You need a consumer key and a consumer secret key.
* Your data will be saved to {0} .
* Your tokens will be appear as an variable 'tokens' and 'apponly'.

", tpath);
      Console.Write("consumer key>");
      var ck = Console.ReadLine();
      Console.Write("consumer secret>");
      var cs = Console.ReadLine();
      var a = OAuth.Authorize(ck, cs);
      Console.WriteLine(a.AuthorizeUri);
      Console.Write("pin>");
      tokens = a.GetTokens(Console.ReadLine());
      apponly = OAuth2.GetToken(ck, cs);
      using(var f = File.OpenWrite(tpath))
          ds.WriteObject(f, new TokensBase[]{tokens, apponly});
      Console.WriteLine("Saved to {0} . Don't forget to do chmod 600.", tpath);
  } else using (var f = File.OpenRead(Path.Combine(home, tf)))
  {
    var t = (TokensBase[])ds.ReadObject(f);
    tokens = (Tokens)t[0]; apponly = (OAuth2Token)t[1];
  }
}
Console.WriteLine("* CoreTweet for Mono C# Shell is enabled.");

