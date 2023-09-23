$ErrorActionPreference = 'Stop';
$packageName= 'smartsystemmenu'
$toolsDir   = "$(Split-Path -parent $MyInvocation.MyCommand.Definition)"
$url        = 'https://github.com/AlexanderPro/SmartSystemMenu/releases/download/v2.25.1/SmartSystemMenu_v2.25.1.zip'

$packageArgs = @{
  packageName   = $packageName
  destination   = $toolsDir
  fileType      = 'zip'
  url           = $url
  softwareName  = 'SmartSystemMenu*'
  checksum      = '8897d35e57de60113e3ad6fb0e2257ac17e0151b9560ac9027184b3315b71fee'
  checksumType  = 'sha256'
}

Install-ChocolateyZipPackage @packageArgs









