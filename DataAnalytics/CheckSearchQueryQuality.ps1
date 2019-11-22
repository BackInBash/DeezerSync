#
# Test Search Querys on Raw Data
#

$count = 1593
$foundEquals = 0
$foundLike = 0

for($i=1; $i -le $count; $i++){
    $data = Get-Content .\Documents\GitHub\DeezerSync\DataAnalytics\RawData\SearchResults\$i.json -Raw | ConvertFrom-Json
    foreach($res in $data.Results){

        # Stage 1
        if(($res.title -eq $data.Searching.title) -and (($res.artist -eq $data.Searching.artist) -or ($res.remixArtist -eq $data.Searching.remixArtist))){
            Write-Host "FOUND  Track 1 "$res.title " Track 2: " $data.Searching.title "Artist 1" $res.artist" Artist 2:" $data.Searching.artist " One out of " $data.Results.Count
            $foundEquals++
            break
        }
        else
        {
            if(($res.title -like $data.Searching.title) -and ($res.artist -like $data.Searching.artist)){
                Write-Host "Contains " $res.title " " $data.Searching.title " One out of " $data.Results.Count
                $foundLike++
                break
            }
        }
    }
}
Write-Host "Equals "$foundEquals "Like: "$foundLike

