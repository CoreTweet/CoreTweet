param (
    [string]$oldver,
    [string]$newver
)

$yml = ls "*.yml"
$nuspec = ls "*.nuspec"
$foofile = ls "*file"
$cs = ls -Path ".\CoreTweet.Shared" -Filter "*.cs" -Recurse
$props = ls "*.props"

foreach ($x in $yml + $nuspec + $foofile + $cs + $props) {
    $before = Get-Content $x.FullName -Raw
    $after = $before -replace $oldver, $newver
    if ($before -ne $after) {
        [IO.File]::WriteAllText($x.FullName, $after)
    }
}
