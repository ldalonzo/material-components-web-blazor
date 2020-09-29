param (
  [Parameter(Mandatory = $true)]
  [string]
  $TestResultsBasePath,

  [Parameter(Mandatory = $false)]
  [string]
  $NuGetPackagesBasePath = '$(UserProfile)\.nuget',

  [Parameter(Mandatory = $true)]
  [string]
  $CodecovUploadToken
)

function Send-CoverageResultsToCodecov {
  [CmdletBinding(PositionalBinding = $false)]
  param(
      [Parameter(Mandatory = $true)]
      [string]
      $TestResultsBasePath,

      [Parameter(Mandatory = $true)]
      [string]
      $NuGetPackagesBasePath,

      [Parameter(Mandatory = $true)]
      [string]
      $CodecovUploadToken
  )

  $codecovFullPath = (Join-Path $NuGetPackagesBasePath -ChildPath "codecov\1.12.3\tools\codecov.exe")

  if (!(Test-Path $codecovFullPath)) {
    Write-Error "Could NOT find codecov.exe"
  }

  $files = Get-ChildItem $TestResultsBasePath -Recurse -Filter *opencover.xml

  foreach ($file in $files) {
    & $codecovFullPath -f $file.FullName -t $CodecovUploadToken
  }
}

Send-CoverageResultsToCodecov -TestResultsBasePath $TestResultsBasePath -NuGetPackagesBasePath $NuGetPackagesBasePath -CodecovUploadToken $CodecovUploadToken
