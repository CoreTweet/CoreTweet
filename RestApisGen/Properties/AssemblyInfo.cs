using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.InteropServices;

// アセンブリに関する一般情報は以下の属性セットをとおして制御されます。
// アセンブリに関連付けられている情報を変更するには、
// これらの属性値を変更してください。
[assembly: AssemblyTitle("RestApisGen")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("RestApisGen")]
[assembly: AssemblyCopyright("(c) 2013-2016 CoreTweet Development Team")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// ComVisible を false に設定すると、その型はこのアセンブリ内で COM コンポーネントから 
// 参照不可能になります。COM からこのアセンブリ内の型にアクセスする場合は、
// その型の ComVisible 属性を true に設定してください。
[assembly: ComVisible(false)]

// 次の GUID は、このプロジェクトが COM に公開される場合の、typelib の ID です
[assembly: Guid("380f31e7-9e6a-4dbd-bf2a-f23265f2eed1")]

// アセンブリのバージョン情報は、以下の 4 つの値で構成されています:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// すべての値を指定するか、下のように '*' を使ってビルドおよびリビジョン番号を 
// 既定値にすることができます:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]

[assembly: SuppressMessage("Common Practices and Code Improvements", "RECS0063:Warns when a culture-aware 'StartsWith' call is used by default.", Justification = "Too many warnings and this code will be run on developers' machines only.")]
[assembly: SuppressMessage("Common Practices and Code Improvements", "RECS0062:Warns when a culture-aware 'LastIndexOf' call is used by default.")]
[assembly: SuppressMessage("Redundancies in Code", "RECS0134:Check for inequality before assignment is redundant if (x != value) x = value;", Justification = "いや、消したらアカンやろ", Scope = "member", Target = "~M:RestApisGen.ParameterRecordsFs.Generate(System.Collections.Generic.IEnumerable{RestApisGen.ApiParent},System.IO.TextWriter)")]
