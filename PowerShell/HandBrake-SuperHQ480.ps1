$InputFile = $args[0]
if (-not [string]::IsNullOrEmpty($InputFile) -and $InputFile -is [string]) {
	$InputFilePath = $InputFile
} elseif ($InputFile -is [System.IO.FileInfo]) {
    $InputFilePath = $InputFile.FullName
}
$OutputFilePath = $InputFile -replace '\.mkv$', '.mp4'

if ([string]::IsNullOrEmpty($InputFilePath)) {
	exit 1
}
if ([string]::IsNullOrEmpty($InputFilePath)) {
	exit 1
}
if ($InputFilePath -eq $OutputFilePath) {
	exit 1
}

$ArgumentList = "--input `"$InputFile`" --output `"$OutputFilePath`" --preset `"Super HQ 480p30 Surround`" --first-subtitle --subtitle-burned"
# Write-Host $ArgumentList

Start-Process -Wait -NoNewWindow -FilePath "C:\HandbrakeCLI\HandBrakeCLI.exe" -ArgumentList $ArgumentList
