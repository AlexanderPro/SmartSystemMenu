$ErrorActionPreference = 'Stop';
$packageName= 'smartsystemmenu'
$toolsDir   = "$(Split-Path -parent $MyInvocation.MyCommand.Definition)"
$url        = 'https://github.com/AlexanderPro/SmartSystemMenu/releases/download/v2.30.0/SmartSystemMenu_v2.30.0.zip'

$packageArgs = @{
  packageName   = $packageName
  destination   = $toolsDir
  fileType      = 'zip'
  url           = $url
  softwareName  = 'SmartSystemMenu*'
  checksum      = '97bde7771d8a4587eb622517971f0ecdd52a6a17a616f4966c38b027f737af9f'
  checksumType  = 'sha256'
}

Install-ChocolateyZipPackage @packageArgs
