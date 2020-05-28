MONO_PATH?=/usr/bin
DOTNET_PATH?=/usr/bin
MONO_CS_SHELL_CONF?=~/.config/csharp
EX_NUGET:=ExternalDependencies/nuget/bin/nuget

ifeq (, $(shell which msbuild))
XBUILD?=$(MONO_PATH)/xbuild /clp:Verbosity=minimal /p:NoWarn="1591,1573"
else
XBUILD?=$(MONO_PATH)/msbuild /m /v:m "/nowarn:CS1591;CS1573" 
endif

MONO?=$(MONO_PATH)/mono
DOTNET?=$(DOTNET_PATH)/dotnet
GIT?=$(shell which git)
PATCH?=$(shell which patch)

NUGET?=$(EX_NUGET)
DOXYGEN?=$(shell hash doxygen 2>/dev/null || echo ":" && which doxygen)

REST_APIS_GEN:=RestApisGen/bin/RestApisGen.exe
SLN?=CoreTweet-Mono.sln

all: binary docs ;

binary: nuget-packages-restore rest-apis
	$(XBUILD) $(SLN) /p:Configuration=Release

binary-netstandard: rest-apis
	$(DOTNET) build CoreTweet/CoreTweet.csproj -c Release

docs: external-tools binary
	$(DOXYGEN)

# External tools

external-tools: nuget ;

nuget: $(NUGET) ;

submodule:
	$(GIT) submodule update --init --recursive
	$(PATCH) -p1 -d ExternalDependencies/nuget < misc/0001-Update-NuGet.exe.patch

$(EX_NUGET): submodule
	cd ExternalDependencies/nuget && $(MAKE)

# NuGet

nuget-packages-restore: external-tools
	$(NUGET) restore CoreTweet-All.sln -PackagesDirectory packages

# RestApis

rest-apis: CoreTweet.Shared/RestApis.cs ;

CoreTweet.Shared/RestApis.cs: $(REST_APIS_GEN)
	$(MONO) $(REST_APIS_GEN)

$(REST_APIS_GEN):
	$(XBUILD) RestApisGen/RestApisGen.csproj /p:Configuration=Debug

# Shell

shell: binary
	[ -d $(MONO_CS_SHELL_CONF) ] || mkdir -p $(MONO_CS_SHELL_CONF);
	cp Release/net40/*.dll $(MONO_CS_SHELL_CONF);
	cp misc/coretweet_for_shell.cs $(MONO_CS_SHELL_CONF);

# Clean

clean:
	$(RM) -rf Release
	$(RM) -rf RestApisGen/bin
	$(RM) CoreTweet.Shared/RestApis.cs
	$(PATCH) -R -p1 -d ExternalDependencies/nuget < misc/0001-Update-NuGet.exe.patch
