open System
open System.Diagnostics

let _ =
  use p = Process.Start("dotnet", "build build.proj -t:Repl")
  p.WaitForExit()
  if p.ExitCode <> 0 then
    exit p.ExitCode

#r "nuget: Newtonsoft.Json, 12.0.3"
#r "Release/netstandard2.1/CoreTweet.dll"
#r "Release/netstandard2.1/CoreTweet.Tests.dll"

open CoreTweet

let _ = CoreTweet.Tests.ApiTests.SetupTokens()

let tokens = CoreTweet.Tests.ApiTests.Tokens
let apponly = CoreTweet.Tests.ApiTests.ApponlyToken