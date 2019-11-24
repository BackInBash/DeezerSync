#
# Get False Positives from Prepared Data
#

$counter = 0

$files = Get-ChildItem .\Documents\GitHub\DeezerSync\DataAnalytics\PreparedData\SearchResults\

foreach ($dat in $files) {

    $res = Get-Content $dat.FullName | ConvertFrom-Json
    # Stage 1
    if ($res.Searching.title -notlike $res.Reported.title) {
        Write-Host $dat.Name
        $guid = New-Guid

        $newpath = join-path .\Documents\GitHub\DeezerSync\DataAnalytics\PreparedData\FalsePositives\ $($res.Searching.id.ToString()+"-"+$guid.Guid+".json")
        $res | ConvertTo-Json | Out-File $newpath
        $counter++
    }
}
Write-Host "Found "$counter " False Positives out of " $files.Count

