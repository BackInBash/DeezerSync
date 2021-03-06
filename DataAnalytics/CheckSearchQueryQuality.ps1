#
# Test Search Querys on Raw Data
#

$count = 1593
$foundEquals = 0
$foundLike = 0

$files = Get-ChildItem .\Documents\GitHub\DeezerSync\DataAnalytics\PreparedData\SearchResults\

foreach ($dat in $files) {

    $res = Get-Content $dat.FullName | ConvertFrom-Json
    # Stage 1
    if (($res.title -eq $data.Searching.title) -and (($res.artist -eq $data.Searching.artist) -or ($res.remixArtist -eq $data.Searching.remixArtist)) -and (($res.duration - 1 -eq $data.Searching.duration) -or ($res.duration + 1 -eq $data.Searching.duration))) {
        Write-Host "FOUND EQ URL 1 "$res.url " URL 2: " $data.Searching.url
        $foundEquals++
        break
    }
    else {
        if (($res.title -like $data.Searching.title) -or (($res.artist -like $data.Searching.artist) -or ($res.remixArtist -like $data.Searching.remixArtist)) -and (($res.duration - 1 -eq $data.Searching.duration) -or ($res.duration + 1 -eq $data.Searching.duration))) {
            Write-Host "FOUND LIKE URL 1 "$res.url " URL 2: " $data.Searching.url
            $foundLike++
            break
        }
    }
}
Write-Host "Equals "$foundEquals "Like: "$foundLike "Count" $files.Count

