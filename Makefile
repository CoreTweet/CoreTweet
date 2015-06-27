MONO_PATH:=/usr/bin
MONODEVELOP_DIR:=/usr/lib/monodevelop

EX_NUGET:=ExternalDependencies/nuget/bin/nuget
EX_DOXYGEN:=ExternalDependencies/doxygen/bin/doxygen
EX_T4:=ExternalDependencies/t4/bin/TextTransform.exe
MD_T4:=$(MONODEVELOP_DIR)/AddIns/MonoDevelop.TextTemplating/TextTransform.exe

XBUILD:=$(MONO_PATH)/xbuild
MONO:=$(MONO_PATH)/mono
GIT:=$(shell which git)

NUGET:=$(EX_NUGET)
DOXYGEN:=$(shell hash doxygen 2>/dev/null || echo $(EX_DOXYGEN) && which doxygen)
T4:=$(shell if [ -f $(MD_T4) ]; then echo $(MD_T4); else echo $(EX_T4); fi)

all: binary docs ;

binary: nuget-packages-restore rest-apis
	$(XBUILD) CoreTweet-Mono.sln /p:Configuration=Release

docs: external-tools binary
	$(DOXYGEN)

# External tools

external-tools: nuget doxygen t4 ;

nuget: $(NUGET) ;
doxygen: $(DOXYGEN) ;
t4: $(T4) ;

submodule:
	$(GIT) submodule update --init --recursive

$(EX_DOXYGEN): submodule
	cd ExternalDependencies/doxygen && ./configure && $(MAKE)

$(EX_NUGET): submodule
	cd ExternalDependencies/nuget && $(MAKE)

$(EX_T4) : submodule
	cd ExternalDependencies/t4 && $(XBUILD)

a:
	echo $(T4)

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
	$(MONO) $(T4) CoreTweet.Shared/RestApis.tt -out CoreTweet.Shared/RestApis.cs

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
