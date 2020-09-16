MONO_PATH?=/usr/bin
DOTNET_PATH?=/usr/bin
MONO_CS_SHELL_CONF?=~/.config/csharp
NUGET_EXE:=ExternalDependencies/lib/nuget/NuGet.exe

ifeq (, $(shell which msbuild))
XBUILD?=$(MONO_PATH)/xbuild /clp:Verbosity=minimal /p:NoWarn="1591,1573"
else
XBUILD?=$(MONO_PATH)/msbuild /m /v:m "/nowarn:CS1591;CS1573" 
endif

MONO?=$(MONO_PATH)/mono
DOTNET?=$(DOTNET_PATH)/dotnet
GIT?=$(shell which git)

NUGET?=ExternalDependencies/bin/nuget
DOXYGEN?=$(shell hash doxygen 2>/dev/null || echo ":" && which doxygen)

REST_APIS_GEN:=RestApisGen/bin/RestApisGen.exe
SLN?=CoreTweet-Mono.sln

define NUGET_BIN
#!/usr/bin/env bash
MONO_PATH=$(MONO_PATH):$$MONO_PATH $(MONO) $(abspath $(NUGET_EXE)) $$@
endef

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

export NUGET_BIN
$(NUGET): $(NUGET_EXE)
	mkdir -p $(dir $(NUGET))
	echo "$$NUGET_BIN" > $(NUGET)
	chmod +x $(NUGET)

$(NUGET_EXE):
	curl -LSso $(NUGET_EXE) --create-dirs https://dist.nuget.org/win-x86-commandline/v5.5.1/nuget.exe

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
