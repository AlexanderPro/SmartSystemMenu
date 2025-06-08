$ErrorActionPreference = 'Stop';
$packageName= 'smartsystemmenu'
$toolsDir   = "$(Split-Path -parent $MyInvocation.MyCommand.Definition)"
$url        = 'https://github.com/AlexanderPro/SmartSystemMenu/releases/download/v2.31.0/SmartSystemMenu_v2.31.0.zip'

$packageArgs = @{
  packageName   = $packageName
  destination   = $toolsDir
  fileType      = 'zip'
  url           = $url
  softwareName  = 'SmartSystemMenu*'
  checksum      = '208012e702efc2c855ae951895ce959855f335eb92355ba93bf81b03fbeda304'
  checksumType  = 'sha256'
}

Install-ChocolateyZipPackage @packageArgs
