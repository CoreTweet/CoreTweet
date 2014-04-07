all: binary docs package ;

binary:
	[ -d packages ] || scripts/nuget_restore.sh
	xbuild /p:Configuration=Release

docs:
	[ -f ExternalDependencies/doxygen/bin/doxygen  ] || scripts/configure_doxygen.sh
	scripts/makedoc.sh

package:
	[ -d ExternalDependencies/nuget  ] || git submodule update --init --recursive
	scripts/nuget_pack.sh

clean:
	rm -rf Binary/Nightly