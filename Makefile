MONO_PATH?=/usr/bin

EX_NUGET:=ExternalDependencies/nuget/bin/nuget
EX_DOXYGEN:=ExternalDependencies/doxygen/bin/doxygen

XBUILD?=$(MONO_PATH)/xbuild
MONO?=$(MONO_PATH)/mono
GIT?=$(shell which git)

NUGET?=$(EX_NUGET)
DOXYGEN?=$(shell hash doxygen 2>/dev/null || echo $(EX_DOXYGEN) && which doxygen)

REST_APIS_GEN:=RestApisGen/bin/RestApisGen.exe

all: binary docs ;

binary: nuget-packages-restore rest-apis
	$(XBUILD) CoreTweet-Mono.sln /p:Configuration=Release

docs: external-tools binary
	$(DOXYGEN)

# External tools

external-tools: nuget doxygen ;

nuget: $(NUGET) ;
doxygen: $(DOXYGEN) ;

submodule:
	$(GIT) submodule update --init --recursive

$(EX_DOXYGEN): submodule
	cd ExternalDependencies/doxygen && ./configure && $(MAKE)

$(EX_NUGET): submodule
	cd ExternalDependencies/nuget && $(MAKE)

# NuGet

nuget-packages-restore: external-tools
	[ -d packages ] || \
	    for i in CoreTweet*/; do \
	        cd $$i ; \
	        ../$(NUGET) restore -ConfigFile packages.config -PackagesDirectory ../packages ; \
	        cd .. ; \
	    done

# RestApis

rest-apis: CoreTweet.Shared/RestApis.cs ;

CoreTweet.Shared/RestApis.cs: $(REST_APIS_GEN)
	$(MONO) $(REST_APIS_GEN)

$(REST_APIS_GEN):
	$(XBUILD) RestApisGen/RestApisGen.csproj /p:Configuration=Debug

# Clean

clean:
	$(RM) -rf Release
	$(RM) CoreTweet.Shared/RestApis.cs
	$(RM) CoreTweet.FSharp/ParameterRecords.fs

# Nonfree

all-nonfree: binary-nonfree docs package-nonfree ;

binary-nonfree: nuget-packages-restore
	$(XBUILD) CoreTweet-All.sln /p:Configuration=Release

package-nonfree: external-tools binary-nonfree nuspec-nonfree
	$(NUGET) pack CoreTweet.nuspec -OutputDirectory Release
	$(NUGET) pack CoreTweet.Streaming.Reactive.nuspec -OutputDirectory Release
