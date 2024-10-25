# Parse_Get-AppxPackage
Little console app to parse output from Get-AppxPackage
I was helping my church do a quick audit of software installed on Windows 11 pcs and found that I could use Powershell in admin mode and run:

`Get-AppxPackage -Allusers > output.txt`

That got me a list of installed software but not in useful format:
```
Name                   : Microsoft.AAD.BrokerPlugin
Publisher              : CN=Microsoft Windows, O=Microsoft Corporation, L=Redmond, S=Washington, C=US
Architecture           : Neutral
ResourceId             : neutral
Version                : 1000.19041.1.0
PackageFullName        : Microsoft.AAD.BrokerPlugin_1000.19041.1.0_neutral_neutral_cw5n1h2txyewy
InstallLocation        : C:\Windows\SystemApps\Microsoft.AAD.BrokerPlugin_cw5n1h2txyewy
IsFramework            : False
PackageFamilyName      : Microsoft.AAD.BrokerPlugin_cw5n1h2txyewy
PublisherId            : cw5n1h2txyewy
PackageUserInformation : {S-1-5-18 [S-1-5-18]: Staged}
IsResourcePackage      : False
IsBundle               : False
IsDevelopmentMode      : False
NonRemovable           : True
IsPartiallyStaged      : False
SignatureKind          : System
Status                 : Ok
```

So I wrote this to turn that file into a CSV file for more useful work. Takes to parameters, -i for input file and -o for output file.
