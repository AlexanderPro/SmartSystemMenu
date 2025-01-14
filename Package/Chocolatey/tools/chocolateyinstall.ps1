$ErrorActionPreference = 'Stop';
$packageName= 'smartsystemmenu'
$toolsDir   = "$(Split-Path -parent $MyInvocation.MyCommand.Definition)"
$url        = 'https://github.com/AlexanderPro/SmartSystemMenu/releases/download/v2.29.0/SmartSystemMenu_v2.29.0.zip'

$packageArgs = @{
  packageName   = $packageName
  destination   = $toolsDir
  fileType      = 'zip'
  url           = $url
  softwareName  = 'SmartSystemMenu*'
  checksum      = '42b6c3378255b8893a3fe6301f610be48e96da920f0a9583bfb17dd8842ae70d'
  checksumType  = 'sha256'
}

Install-ChocolateyZipPackage @packageArgs
