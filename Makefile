MONO_PATH?=/usr/bin
MONODEVELOP_DIR?=/usr/lib/monodevelop

XBUILD=$(MONO_PATH)/xbuild
MONO=$(MONO_PATH)/mono
GIT?=$(shell which git)
NUGET?=ExternalDependencies/nuget/bin/nuget
DOXYGEN?=$(shell hash doxygen 2>/dev/null || echo ExternalDependencies/doxygen/bin/doxygen && which doxygen)

all: binary docs ;

binary: nuget-packages-restore rest-apis
	$(XBUILD) CoreTweet-Mono.sln /p:Configuration=Release

docs: external-tools binary
	$(DOXYGEN)

# External tools

external-tools: nuget doxygen;

nuget: $(NUGET) ;
doxygen: $(DOXYGEN) ;

submodule:
	$(GIT) submodule update --init --recursive

ExternalDependencies/doxygen/bin/doxygen: submodule
	cd ExternalDependencies/doxygen && ./configure && $(MAKE)

ExternalDependencies/nuget/bin/nuget: submodule
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

CoreTweet.Shared/RestApis.cs:
	$(MONO) $(MONODEVELOP_DIR)/AddIns/MonoDevelop.TextTemplating/TextTransform.exe CoreTweet.Shared/RestApis.tt -out CoreTweet.Shared/RestApis.cs

# Clean

clean:
	$(RM) -rf Binary/Nightly
	$(RM) CoreTweet.Shared/RestApis.cs

# Nonfree

all-nonfree: binary-nonfree docs package-nonfree ;

binary-nonfree: nuget-packages-restore
	$(XBUILD) CoreTweet-All.sln /p:Configuration=Release

package-nonfree: external-tools binary-nonfree nuspec-nonfree
	$(NUGET) pack CoreTweet.nuspec -OutputDirectory Release
	$(NUGET) pack CoreTweet.Streaming.Reactive.nuspec -OutputDirectory Release
