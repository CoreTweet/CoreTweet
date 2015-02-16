all: binary docs ;

binary: nuget-packages-restore
	xbuild CoreTweet-Mono.sln /p:Configuration=Release

docs: external-tools binary
	if hash "doxygen" 2> /dev/null; then \
	    doxygen ; \
	else \
	    ExternalDependencies/doxygen/bin/doxygen ; \
	fi

# External tools

external-tools: ExternalDependencies/doxygen/bin/doxygen ExternalDependencies/nuget/bin/nuget;

submodule:
	git submodule update --init --recursive

ExternalDependencies/doxygen/bin/doxygen: submodule
	hash doxygen 2>/dev/null || { cd ExternalDependencies/doxygen && ./configure && make; }

ExternalDependencies/nuget/bin/nuget: submodule
	cd ExternalDependencies/nuget && make

# NuGet

nuget-packages-restore: external-tools
	[ -f packages/repositories.config ] || \
	    for i in CoreTweet*/; do \
	        cd $$i ; \
	        ../ExternalDependencies/nuget/bin/nuget restore -ConfigFile packages.config -PackagesDirectory ../packages ; \
	        cd .. ; \
	    done

# Clean

clean:
	rm -rf Binary/Nightly

# Nonfree

all-nonfree: binary-nonfree docs package-nonfree ;

binary-nonfree: nuget-packages-restore
	xbuild CoreTweet-All.sln /p:Configuration=Release

package-nonfree: external-tools binary-nonfree nuspec-nonfree
	ExternalDependencies/nuget/bin/nuget pack CoreTweet.nuspec -OutputDirectory Release
	ExternalDependencies/nuget/bin/nuget pack CoreTweet.Streaming.Reactive.nuspec -OutputDirectory Release
