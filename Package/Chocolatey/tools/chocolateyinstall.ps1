$ErrorActionPreference = 'Stop';
$packageName= 'smartsystemmenu'
$toolsDir   = "$(Split-Path -parent $MyInvocation.MyCommand.Definition)"
$url        = 'https://github.com/AlexanderPro/SmartSystemMenu/releases/download/v2.27.0/SmartSystemMenu_v2.27.0.zip'

$packageArgs = @{
  packageName   = $packageName
  destination   = $toolsDir
  fileType      = 'zip'
  url           = $url
  softwareName  = 'SmartSystemMenu*'
  checksum      = 'c4c28e0574b0ece51472d5af847a46da25782e1f2d7bd12a22b4eddfc8202d9d'
  checksumType  = 'sha256'
}

Install-ChocolateyZipPackage @packageArgs









