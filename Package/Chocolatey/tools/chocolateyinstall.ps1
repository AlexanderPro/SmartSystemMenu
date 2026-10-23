$ErrorActionPreference = 'Stop';
$packageName= 'smartsystemmenu'
$toolsDir   = "$(Split-Path -parent $MyInvocation.MyCommand.Definition)"
$url        = 'https://github.com/AlexanderPro/SmartSystemMenu/releases/download/v2.34.0/SmartSystemMenu_v2.34.0.zip'

$packageArgs = @{
  packageName   = $packageName
  destination   = $toolsDir
  fileType      = 'zip'
  url           = $url
  softwareName  = 'SmartSystemMenu*'
  checksum      = 'ec310d9fbb8332fe911cd1423ee8f654bedb01d3862100f9fb0d79a5664fac1b'
  checksumType  = 'sha256'
}

Install-ChocolateyZipPackage @packageArgs
