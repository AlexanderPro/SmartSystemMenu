$ErrorActionPreference = 'Stop';
$packageName= 'smartsystemmenu'
$toolsDir   = "$(Split-Path -parent $MyInvocation.MyCommand.Definition)"
$url        = 'https://github.com/AlexanderPro/SmartSystemMenu/releases/download/v2.32.1/SmartSystemMenu_v2.32.1.zip'

$packageArgs = @{
  packageName   = $packageName
  destination   = $toolsDir
  fileType      = 'zip'
  url           = $url
  softwareName  = 'SmartSystemMenu*'
  checksum      = '5a707ed11075539231fa50185b42dec7b79019c9ef1038b73cecf1e0a7d5357b'
  checksumType  = 'sha256'
}

Install-ChocolateyZipPackage @packageArgs
