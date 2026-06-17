[CmdletBinding()]
param(
    [string]$Configuration = "Release",
    [string]$Runtime = "win-x64",
    [string]$Version = "1.0.0",
    [switch]$SkipPublish,
    [switch]$InstallInnoSetup
)

$ErrorActionPreference = "Stop"
Set-StrictMode -Version Latest

$root = Split-Path -Parent $MyInvocation.MyCommand.Path
$project = Join-Path $root "PrimeiraTelaWinUI.csproj"
$distDir = Join-Path $root "dist"
$publishDir = Join-Path $distDir "SIGEV-Distribuicao"
$samplesDir = Join-Path $root "Samples"
$installerScript = Join-Path $root "installer\SIGEV.iss"
$installerOutput = Join-Path $distDir "SIGEV-Setup-x64.exe"

function Assert-InWorkspace {
    param([Parameter(Mandatory = $true)][string]$Path)

    $resolvedRoot = (Resolve-Path -LiteralPath $root).Path
    $resolvedPath = if (Test-Path -LiteralPath $Path) {
        (Resolve-Path -LiteralPath $Path).Path
    } else {
        [System.IO.Path]::GetFullPath($Path)
    }

    if (-not $resolvedPath.StartsWith($resolvedRoot, [System.StringComparison]::OrdinalIgnoreCase)) {
        throw "Caminho fora do workspace: $resolvedPath"
    }
}

function Find-InnoSetupCompiler {
    $command = Get-Command ISCC.exe -ErrorAction SilentlyContinue
    if ($command) {
        return $command.Source
    }

    $candidates = @(
        (Join-Path ${env:ProgramFiles} "Inno Setup 6\ISCC.exe"),
        (Join-Path ${env:ProgramFiles(x86)} "Inno Setup 6\ISCC.exe"),
        (Join-Path ${env:LOCALAPPDATA} "Programs\Inno Setup 6\ISCC.exe")
    )

    foreach ($candidate in $candidates) {
        if (Test-Path -LiteralPath $candidate) {
            return $candidate
        }
    }

    return $null
}

function Install-InnoSetupCompiler {
    $winget = Get-Command winget.exe -ErrorAction SilentlyContinue
    if (-not $winget) {
        throw "Inno Setup não encontrado. Instale o Inno Setup 6 ou rode em uma máquina com winget."
    }

    & $winget.Source install --id JRSoftware.InnoSetup -e --silent --accept-package-agreements --accept-source-agreements
    if ($LASTEXITCODE -ne 0) {
        throw "Falha ao instalar o Inno Setup via winget. Código: $LASTEXITCODE"
    }
}

function Write-DistributionExtras {
    $examplesDir = Join-Path $publishDir "Exemplos"
    New-Item -ItemType Directory -Force -Path $examplesDir | Out-Null

    if (Test-Path -LiteralPath $samplesDir) {
        Copy-Item -Path (Join-Path $samplesDir "*") -Destination $examplesDir -Force
    }

    $readme = @"
SIGEV - Sistema Inteligente de Gestão da Evasão

Para abrir o aplicativo, execute PrimeiraTelaWinUI.exe.
Os arquivos de exemplo ficam na pasta Exemplos.
"@
    Set-Content -LiteralPath (Join-Path $publishDir "LEIA-ME.txt") -Value $readme -Encoding UTF8

    $launcher = "@echo off`r`nstart """" ""%~dp0PrimeiraTelaWinUI.exe""`r`n"
    Set-Content -LiteralPath (Join-Path $publishDir "Abrir SIGEV.bat") -Value $launcher -Encoding ASCII
}

if (-not (Test-Path -LiteralPath $distDir)) {
    New-Item -ItemType Directory -Path $distDir | Out-Null
}

if (-not $SkipPublish) {
    Assert-InWorkspace -Path $publishDir

    if (Test-Path -LiteralPath $publishDir) {
        Remove-Item -LiteralPath $publishDir -Recurse -Force
    }

    & dotnet publish $project -c $Configuration -r $Runtime --self-contained true -o $publishDir
    if ($LASTEXITCODE -ne 0) {
        throw "Falha no dotnet publish. Código: $LASTEXITCODE"
    }
}

Write-DistributionExtras

$iscc = Find-InnoSetupCompiler
if (-not $iscc -and $InstallInnoSetup) {
    Install-InnoSetupCompiler
    $iscc = Find-InnoSetupCompiler
}

if (-not $iscc) {
    throw "Inno Setup não encontrado. Instale com: winget install --id JRSoftware.InnoSetup -e"
}

& $iscc "/DMyAppVersion=$Version" $installerScript
if ($LASTEXITCODE -ne 0) {
    throw "Falha ao compilar o instalador. Código: $LASTEXITCODE"
}

if (-not (Test-Path -LiteralPath $installerOutput)) {
    throw "Instalador não encontrado após o build: $installerOutput"
}

$hash = Get-FileHash -LiteralPath $installerOutput -Algorithm SHA256
$file = Get-Item -LiteralPath $installerOutput

[PSCustomObject]@{
    Installer = $file.FullName
    SizeMB = [Math]::Round($file.Length / 1MB, 2)
    SHA256 = $hash.Hash
}
