$releases = 'https://github.com/AlexanderPro/SmartSystemMenu/releases'

function global:au_GetLatest {
    $download_page = Invoke-WebRequest -Uri $releases
    $regex   = '/AlexanderPro/SmartSystemMenu/releases/download/.*?/SmartSystemMenu_v.*?.zip$'
    $url     = "https://github.com/" + ($download_page.links | ? href -match $regex | select -First 1 -expand href)
    
    $verregex= [regex]'/SmartSystemMenu_v(.*?).zip$'
    $version = $verregex.Match( $url ).Groups[1]
    return @{ 
    Version = $version;
    URL32 = $url;
    ChecksumType32 = 'sha256'
    }
}


function global:au_SearchReplace {
    @{
        "tools\chocolateyInstall.ps1" = @{
            "(^[$]url\s*=\s*)('.*')"      = "`$1'$($Latest.URL32)'"           #1
            "(?i)(^\s*checksum\s*=\s*)'.*'"   = "`$1'$($Latest.Checksum32)'"      #2
        }
    }
}


update -ChecksumFor 32