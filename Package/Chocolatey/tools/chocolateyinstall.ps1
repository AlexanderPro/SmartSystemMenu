$ErrorActionPreference = 'Stop';
$packageName= 'smartsystemmenu'
$toolsDir   = "$(Split-Path -parent $MyInvocation.MyCommand.Definition)"
$url        = 'https://github.com/AlexanderPro/SmartSystemMenu/releases/download/v2.33.1/SmartSystemMenu_v2.33.1.zip'

$packageArgs = @{
  packageName   = $packageName
  destination   = $toolsDir
  fileType      = 'zip'
  url           = $url
  softwareName  = 'SmartSystemMenu*'
  checksum      = 'fadb3f53e525f3f8b2570e0830a9f066288fb89a51cabd5f5346a56be15e5f74'
  checksumType  = 'sha256'
}

Install-ChocolateyZipPackage @packageArgs
