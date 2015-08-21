param (
    [string]$oldver,
    [string]$newver
)

$yml = ls "*.yml"
$nuspec = ls "*.nuspec"
$foofile = ls "*file"
$cs = ls -Path ".\CoreTweet.Shared" -Filter "*.cs" -Recurse

foreach ($x in $yml + $nuspec + $foofile + $cs) {
    $before = Get-Content $x.FullName -Raw
    $after = $before -replace $oldver, $newver
    if ($before -ne $after) {
        [IO.File]::WriteAllText($x.FullName, $after)
    }
}
