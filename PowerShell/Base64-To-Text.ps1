# Put this into a shortcut:
# C:\Users\joe\AppData\Local\Microsoft\WindowsApps\pwsh.exe -ExecutionPolicy Bypass -File C:\Util\Base64-To-Text.ps1

$base64String = Read-Host "Enter Base64 string"
$bytes = [System.Convert]::FromBase64String($base64String)
$text = [System.Text.Encoding]::UTF8.GetString($bytes)
Write-Host "----------------------------"
Write-Host $text
Write-Host "----------------------------"
Read-Host "Press ENTER to exit"
