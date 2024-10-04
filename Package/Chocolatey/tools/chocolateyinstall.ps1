$ErrorActionPreference = 'Stop';
$packageName= 'smartsystemmenu'
$toolsDir   = "$(Split-Path -parent $MyInvocation.MyCommand.Definition)"
$url        = 'https://github.com/AlexanderPro/SmartSystemMenu/releases/download/v2.27.2/SmartSystemMenu_v2.27.2.zip'

$packageArgs = @{
  packageName   = $packageName
  destination   = $toolsDir
  fileType      = 'zip'
  url           = $url
  softwareName  = 'SmartSystemMenu*'
  checksum      = '77e6383f27eea36841e1ec2a29d20fdf32332af5c0a031257909a781e6d3ebf8'
  checksumType  = 'sha256'
}

Install-ChocolateyZipPackage @packageArgs









