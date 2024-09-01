# Run a remote exe as a background task
# so it will continue to run even if the
# local computer is disconnected

Invoke-Command -ComputerName OtherPC -AsJob -ScriptBlock { & "C:\DoSomething.exe" }
