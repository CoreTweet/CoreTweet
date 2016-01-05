MONO_PATH?=/usr/bin
MONO_CS_SHELL_CONF?=~/.config/csharp

EX_NUGET:=ExternalDependencies/nuget/bin/nuget

XBUILD?=$(MONO_PATH)/xbuild
MONO?=$(MONO_PATH)/mono
GIT?=$(shell which git)

NUGET?=$(EX_NUGET)
DOXYGEN?=$(shell hash doxygen 2>/dev/null || echo ":" && which doxygen)

REST_APIS_GEN:=RestApisGen/bin/RestApisGen.exe

all: binary docs ;

binary: nuget-packages-restore rest-apis
	$(XBUILD) CoreTweet-Mono.sln /p:Configuration=Release

docs: external-tools binary
	$(DOXYGEN)

# External tools

external-tools: nuget ;

nuget: $(NUGET) ;

submodule:
	$(GIT) submodule update --init --recursive

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
	$(RM) CoreTweet.Shared/RestApis.cs

# Nonfree

all-nonfree: binary-nonfree docs package-nonfree ;

binary-nonfree: nuget-packages-restore
	$(XBUILD) CoreTweet-All.sln /p:Configuration=Release

package-nonfree: external-tools binary-nonfree nuspec-nonfree
	$(NUGET) pack CoreTweet.nuspec -OutputDirectory Release
