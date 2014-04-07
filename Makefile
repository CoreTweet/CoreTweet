all: binary docs package ;

binary: nuget_packages_restore
	xbuild /p:Configuration=Release

docs: external_tools binary
	scripts/makedoc.sh

package: external_tools binary
	scripts/nuget_pack.sh

external_tools:
	[ -f ExternalDependencies/doxygen/bin/doxygen  ] || scripts/make_external.sh

nuget_packages_restore: external_tools
	[ -d packages ] || scripts/nuget_restore.sh

clean:
	rm -rf Binary/Nightly
