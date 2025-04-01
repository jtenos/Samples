Get-ChildItem *.mkv | ForEach-Object {
    $Mp4File = "Converted\$($_.Name.Replace('.mkv', '.mp4'))"
    C:\HandbrakeCLI\HandBrakeCLI.exe -i "$($_.Name)" -o "$Mp4File" --preset "Super HQ 480p30 Surround" --subtitle 1 --subtitle-burn
}
