$ErrorActionPreference = 'Stop';
$packageName= 'smartsystemmenu'
$toolsDir   = "$(Split-Path -parent $MyInvocation.MyCommand.Definition)"
$url        = 'https://github.com/AlexanderPro/SmartSystemMenu/releases/download/v2.23.0/SmartSystemMenu_v2.23.0.zip'

$packageArgs = @{
  packageName   = $packageName
  destination   = $toolsDir
  fileType      = 'zip'
  url           = $url
  softwareName  = 'SmartSystemMenu*'
  checksum      = '1e0baf3506613bd3985e55d56a30ea1f81f4a2c5806a70b479aa1c2f55e54baa'
  checksumType  = 'sha256'
}

Install-ChocolateyZipPackage @packageArgs









