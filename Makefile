all: binary docs package ;

binary: nuget-packages-restore
	xbuild CoreTweet-Mono.sln /p:Configuration=Release

docs: external-tools binary
	scripts/makedoc.sh

# NuSpec

nuspec: 
	cp nuspecs/CoreTweet-Mono.nuspec Binary/Nightly/CoreTweet.nuspec
	cp nuspecs/CoreTweet.Streaming.Reactive-Mono.nuspec Binary/Nightly/CoreTweet.Streaming.Reactive.nuspec

# External tools

external-tools: ExternalDependencies/doxygen/bin/doxygen ExternalDependencies/nuget/bin/nuget;

ExternalDependencies/doxygen/bin/doxygen:
	git submodule update --init --recursive
	hash doxygen 2>/dev/null || { cd ExternalDependencies/doxygen && ./configure && make; }

ExternalDependencies/nuget/bin/nuget:
	git submodule update --init --recursive
	cd ExternalDependencies/nuget && make

# NuGet

nuget-packages-restore: external-tools
	[ -f packages/repositories.config ] || scripts/nuget_restore.sh

package: external-tools binary nuspec
	scripts/nuget_pack.sh

# Clean

clean:
	rm -rf Binary/Nightly

# Nonfree

all-nonfree: binary-nonfree docs package-nonfree ;

binary-nonfree: nuget-packages-restore
	xbuild CoreTweet-All.sln /p:Configuration=Release

nuspec-nonfree:
	cp nuspecs/CoreTweet.nuspec Binary/Nightly/CoreTweet.nuspec
	cp nuspecs/CoreTweet.Streaming.Reactive.nuspec Binary/Nightly/CoreTweet.Streaming.Reactive.nuspec


package-nonfree: external-tools binary-nonfree nuspec-nonfree
	scripts/nuget_pack.sh
