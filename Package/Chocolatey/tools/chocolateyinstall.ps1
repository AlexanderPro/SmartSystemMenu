$ErrorActionPreference = 'Stop';
$packageName= 'smartsystemmenu'
$toolsDir   = "$(Split-Path -parent $MyInvocation.MyCommand.Definition)"
$url        = 'https://github.com/AlexanderPro/SmartSystemMenu/releases/download/v2.21.2/SmartSystemMenu_v2.21.2.zip'

$packageArgs = @{
  packageName   = $packageName
  destination   = $toolsDir
  fileType      = 'zip'
  url           = $url
  softwareName  = 'SmartSystemMenu*'
  checksum      = 'f3291c8501c2bc431b5ca3b37eaee2b997fdedad828f1a73da538415e792c2ca'
  checksumType  = 'sha256'
}

Install-ChocolateyZipPackage @packageArgs









