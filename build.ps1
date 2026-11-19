<#
.SYNOPSIS
    Windows build entry for PCRCalculator: Unity player packages and the .NET offline server.

.DESCRIPTION
    PowerShell counterpart of ./build.sh. It reads the editor version pinned in
    ProjectSettings/ProjectVersion.txt, locates that exact Unity editor, exports the
    PCRCALC_* settings Assets/Editor/BuildScript.cs reads, and runs the batchmode entry
    point BuildScript.BuildFromCommandLine.

    Target "linux" only packages the .NET server through scripts/build_server.py and needs
    no Unity install: the Unity side would shell out to a "python3" command that a plain
    Windows machine does not have. When "linux" is combined with player targets, the server
    package is built first here and Unity is asked for the remaining platforms.

    Windows desktop players go through the same Mono scripting backend CI uses with
    -Mono (BuildScript.BuildWindowsMonoFromCommandLine); without it the project's current
    backend is used.

.PARAMETER Target
    all | windows | linux | android | mac | mac-intel | editor-server, or a comma separated
    list such as "windows,linux". Defaults to windows.

.PARAMETER Unity
    Path to Unity.exe. Defaults to UNITY_PATH, then the Unity Hub / registry entries for the
    pinned editor version.

.PARAMETER OutputRoot
    Build output root. Defaults to <project>/Builds, which BuildScript.cs also defaults to.

.PARAMETER Log
    Unity log file. Defaults to <OutputRoot>/unity-build.log and is printed when the build fails.

.PARAMETER AndroidFormat
    apk (default) or aab, only used for the android target.

.PARAMETER Dotnet
    dotnet executable used for the server publish. Defaults to PCRCALC_DOTNET_PATH, DOTNET_ROOT,
    the standard install directory, then PATH. The resolved path is exported as
    PCRCALC_DOTNET_PATH so the editor publishes the server with the same SDK.

.PARAMETER Development
    Sets BuildOptions.Development.

.PARAMETER FrameworkDependent
    Publishes the offline server framework dependent instead of self contained.

.PARAMETER NoZip
    Skips the desktop .zip archives (the unpacked folders are still produced).

.PARAMETER Mono
    Uses the Mono scripting backend entry points (CI parity). Requires a single platform.

.PARAMETER Graphics
    Keeps the graphics device; by default the editor runs with -nographics.

.PARAMETER UnityArgs
    Extra arguments appended to the Unity command line.

.PARAMETER DryRun
    Resolves and prints everything without packaging or building.

.EXAMPLE
    .\build.ps1

.EXAMPLE
    .\build.ps1 -Target windows -Mono -NoZip

.EXAMPLE
    .\build.ps1 -Target linux -FrameworkDependent

.EXAMPLE
    .\build.ps1 -Target windows,linux -Development -Unity "D:\Unity\6000.0.58f2\Editor\Unity.exe"

.NOTES
    From cmd.exe: pwsh -NoProfile -ExecutionPolicy Bypass -File build.ps1 -Target windows
#>
# Keep #requires below the help block: a #requires placed before it makes PowerShell
# stop treating the comment as comment-based help, so -Help would print nothing useful.
#requires -version 5.1
[CmdletBinding()]
param(
    [string]$Target = 'windows',
    [string]$Unity = $env:UNITY_PATH,
    [string]$OutputRoot,
    [string]$Log,
    [ValidateSet('apk', 'aab')][string]$AndroidFormat = 'apk',
    [string]$Dotnet,
    [switch]$Development,
    [switch]$FrameworkDependent,
    [switch]$NoZip,
    [switch]$Mono,
    [switch]$Graphics,
    [string[]]$UnityArgs = @(),
    [switch]$DryRun,
    [Alias('h', '?')][switch]$Help
)

$ErrorActionPreference = 'Stop'

$ProjectRoot = $PSScriptRoot
if (-not $ProjectRoot) { $ProjectRoot = (Get-Location).Path }
$ProjectVersionFile = Join-Path $ProjectRoot 'ProjectSettings\ProjectVersion.txt'
$ServerPackageScript = Join-Path $ProjectRoot 'scripts\build_server.py'

function Write-Step([string]$Text) {
    Write-Host ''
    Write-Host "==> $Text" -ForegroundColor Cyan
}

function Write-Detail([string]$Text) {
    Write-Host "    $Text" -ForegroundColor DarkGray
}

function Stop-Build([string]$Text) {
    Write-Host ''
    Write-Host "ERROR: $Text" -ForegroundColor Red
    exit 1
}

function Resolve-FullPath([string]$Path, [string]$Base) {
    if (-not $Path) { return $null }
    if ([System.IO.Path]::IsPathRooted($Path)) { return [System.IO.Path]::GetFullPath($Path) }
    return [System.IO.Path]::GetFullPath((Join-Path $Base $Path))
}

function Show-Usage {
    $help = Get-Help $PSCommandPath -Detailed
    $synopsis = $help.Synopsis
    # Without the comment block Get-Help falls back to the bare parameter syntax line.
    if (-not $synopsis -or $synopsis -match '^build\.ps1 \[') {
        Write-Host 'PCRCalculator build script (Unity players + .NET offline server)'
        Write-Host ''
        Write-Host 'Usage: .\build.ps1 [-Target windows|linux|android|mac|mac-intel|all|<list>] [-Unity <Unity.exe>]'
        Write-Host '                  [-OutputRoot <dir>] [-Log <file>] [-AndroidFormat apk|aab] [-Dotnet <dotnet.exe>]'
        Write-Host '                  [-Development] [-FrameworkDependent] [-NoZip] [-Mono] [-Graphics] [-DryRun] [-Help]'
        Write-Host ''
        Write-Host 'Run "Get-Help .\build.ps1 -Detailed" for the full description.'
        return
    }
    Write-Host ($help | Out-String)
}

function Split-Targets([string]$Text) {
    $known = @('all', 'windows', 'linux', 'android', 'mac', 'mac-intel', 'editor-server')
    $aliases = @{
        'win' = 'windows'; 'win64' = 'windows'; 'windows64' = 'windows'; 'windows64bit' = 'windows'
        'linux64' = 'linux'; 'server' = 'linux'; 'linux-server' = 'linux'
        'androidplayer' = 'android'
        'macos' = 'mac'; 'osx' = 'mac'; 'mac-arm64' = 'mac'; 'macos-arm64' = 'mac'
        'mac-x64' = 'mac-intel'; 'macos-intel' = 'mac-intel'; 'macintel' = 'mac-intel'
        'editorserver' = 'editor-server'; 'editor' = 'editor-server'
    }

    $resolved = @()
    foreach ($raw in ($Text -split '[,;+]')) {
        $part = $raw.Trim().ToLowerInvariant()
        if (-not $part) { continue }
        if ($aliases.ContainsKey($part)) { $part = $aliases[$part] }
        if ($known -notcontains $part) {
            Stop-Build "Unknown target '$raw'. Known targets: $($known -join ', ')."
        }
        if ($part -eq 'all') {
            foreach ($expanded in @('windows', 'mac', 'linux', 'android')) {
                if ($resolved -notcontains $expanded) { $resolved += $expanded }
            }
            continue
        }
        if ($resolved -notcontains $part) { $resolved += $part }
    }

    if ($resolved.Count -eq 0) { Stop-Build 'No build target was resolved.' }
    return $resolved
}

function Read-EditorVersion {
    if (-not (Test-Path -LiteralPath $ProjectVersionFile)) {
        Stop-Build "Missing $ProjectVersionFile."
    }
    $match = Select-String -LiteralPath $ProjectVersionFile -Pattern '^\s*m_EditorVersion:\s*(\S+)' | Select-Object -First 1
    if (-not $match) { Stop-Build "Cannot read m_EditorVersion from $ProjectVersionFile." }
    return $match.Matches[0].Groups[1].Value
}

function Read-BundleVersion {
    # PlayerSettings.bundleVersion, the version BuildScript.cs uses in artifact names.
    $file = Join-Path $ProjectRoot 'ProjectSettings\ProjectSettings.asset'
    if (Test-Path -LiteralPath $file) {
        $match = Select-String -LiteralPath $file -Pattern '^\s{2}bundleVersion:\s*(\S+)' | Select-Object -First 1
        if ($match) { return $match.Matches[0].Groups[1].Value }
    }
    return '0.0.0'
}

function Resolve-DotnetPath([string]$Explicit) {
    if ($Explicit) {
        if (-not (Test-Path -LiteralPath $Explicit -PathType Leaf)) { Stop-Build "dotnet not found: $Explicit" }
        return [System.IO.Path]::GetFullPath($Explicit)
    }

    $candidates = @()
    if ($env:PCRCALC_DOTNET_PATH) { $candidates += (Join-Path $env:PCRCALC_DOTNET_PATH 'dotnet.exe') }
    if ($env:DOTNET_ROOT) { $candidates += (Join-Path $env:DOTNET_ROOT 'dotnet.exe') }
    if ($env:ProgramFiles) { $candidates += (Join-Path $env:ProgramFiles 'dotnet\dotnet.exe') }
    $onPath = Get-Command 'dotnet.exe' -ErrorAction SilentlyContinue | Select-Object -First 1
    if ($onPath) { $candidates += $onPath.Source }

    foreach ($candidate in $candidates) {
        if ($candidate -and (Test-Path -LiteralPath $candidate -PathType Leaf)) {
            return [System.IO.Path]::GetFullPath($candidate)
        }
    }

    Stop-Build 'dotnet.exe was not found. Install the .NET 8 SDK (https://dotnet.microsoft.com/download) or pass -Dotnet <path>.'
}

function Assert-DotnetSdk([string]$DotnetPath) {
    $sdks = @(& $DotnetPath --list-sdks 2>$null)
    if ($LASTEXITCODE -ne 0 -or $sdks.Count -eq 0) {
        Stop-Build "dotnet --list-sdks failed for $DotnetPath."
    }

    $majors = @()
    foreach ($line in $sdks) {
        if ("$line" -match '^(\d+)\.') { $majors += [int]$Matches[1] }
    }
    if (-not ($majors | Where-Object { $_ -ge 8 })) {
        Stop-Build "The .NET 8 SDK is required (PCRCalculatorServer targets net8.0) but only these SDKs are installed: $($majors -join ', ')."
    }
    return $majors
}

function Resolve-Python {
    $candidates = @(
        @{ Exe = 'py'; Arguments = @('-3') },
        @{ Exe = 'python3'; Arguments = @() },
        @{ Exe = 'python'; Arguments = @() }
    )

    foreach ($candidate in $candidates) {
        $command = Get-Command $candidate.Exe -ErrorAction SilentlyContinue | Select-Object -First 1
        if (-not $command) { continue }
        $probeArguments = @($candidate.Arguments) + @('-c', 'import sys; print(sys.version_info[0])')
        $probe = @(& $command.Source @probeArguments 2>$null)
        if ($LASTEXITCODE -eq 0 -and "$($probe -join '')".Trim() -eq '3') {
            return @{ Exe = $command.Source; Arguments = @($candidate.Arguments) }
        }
    }

    Stop-Build 'Python 3 was not found; scripts/build_server.py needs it. Install Python 3 or drop "linux" from -Target.'
}

function Resolve-UnityEditor([string]$Version, [string]$Explicit) {
    $candidates = New-Object System.Collections.Generic.List[string]
    if ($Explicit) { $candidates.Add($Explicit) }
    if ($env:UNITY_PATH -and $env:UNITY_PATH -ne $Explicit) { $candidates.Add($env:UNITY_PATH) }

    foreach ($hive in @('HKLM:\SOFTWARE\Unity Technologies\Installer', 'HKCU:\SOFTWARE\Unity Technologies\Installer')) {
        $key = Join-Path $hive "Unity $Version"
        if (-not (Test-Path -LiteralPath $key)) { continue }
        $properties = Get-ItemProperty -LiteralPath $key -ErrorAction SilentlyContinue
        if (-not $properties) { continue }
        foreach ($property in @('Location x64', 'Location x86')) {
            $location = $properties.$property
            if ($location) { $candidates.Add((Join-Path $location 'Editor\Unity.exe')) }
        }
    }

    $hubRoots = @()
    if ($env:ProgramFiles) { $hubRoots += (Join-Path $env:ProgramFiles 'Unity\Hub\Editor') }
    if (${env:ProgramFiles(x86)}) { $hubRoots += (Join-Path ${env:ProgramFiles(x86)} 'Unity\Hub\Editor') }
    $secondaryFile = Join-Path $env:APPDATA 'UnityHub\secondaryInstallPath.json'
    if (Test-Path -LiteralPath $secondaryFile) {
        $secondaryRoot = ((Get-Content -LiteralPath $secondaryFile -Raw) -replace '"', '').Trim()
        if ($secondaryRoot) { $hubRoots += $secondaryRoot }
    }
    foreach ($hubRoot in $hubRoots) {
        $candidates.Add((Join-Path $hubRoot "$Version\Editor\Unity.exe"))
    }

    # Custom install locations such as <drive>:\Unity\<version>.
    foreach ($drive in (Get-PSDrive -PSProvider FileSystem -ErrorAction SilentlyContinue | Where-Object { $_.Name -match '^[A-Za-z]$' })) {
        $candidates.Add((Join-Path $drive.Root "Unity\$Version\Editor\Unity.exe"))
    }

    foreach ($candidate in $candidates) {
        if ($candidate -and (Test-Path -LiteralPath $candidate -PathType Leaf)) {
            return [System.IO.Path]::GetFullPath($candidate)
        }
    }

    $tried = ($candidates | Where-Object { $_ } | Select-Object -Unique)
    $triedText = if ($tried) { ($tried -join "`n    ") } else { '(no candidates)' }
    Stop-Build "Unity $Version was not found.`n  Install that editor version, set UNITY_PATH, or pass -Unity <path>.`n  Tried:`n    $triedText"
}

function Get-StandaloneScriptingBackend {
    # ProjectSettings.asset stores "scriptingBackend:" followed by platform lines;
    # Standalone 0 = Mono2x, 1 = IL2CPP. Returns $null when it cannot be read.
    $file = Join-Path $ProjectRoot 'ProjectSettings\ProjectSettings.asset'
    if (-not (Test-Path -LiteralPath $file)) { return $null }
    $lines = Get-Content -LiteralPath $file
    for ($i = 0; $i -lt $lines.Count; $i++) {
        if ($lines[$i] -notmatch '^\s*scriptingBackend:\s*$') { continue }
        for ($j = $i + 1; $j -lt $lines.Count; $j++) {
            if ($lines[$j] -match '^\S') { break }
            if ($lines[$j] -match '^\s+Standalone:\s*(\d+)') { return [int]$matches[1] }
        }
        break
    }
    return $null
}

function Assert-PlatformSupport([string]$EditorPath, [string]$Platform) {
    $hostIsWindows = ($env:OS -eq 'Windows_NT')
    $required = $null
    switch ($Platform) {
        'mac' {
            if ($hostIsWindows) { Stop-Build 'macOS players cannot be built on Windows; run ./build.sh on macOS.' }
            $required = 'MacStandaloneSupport'
        }
        'mac-intel' {
            if ($hostIsWindows) { Stop-Build 'macOS players cannot be built on Windows; run ./build.sh on macOS.' }
            $required = 'MacStandaloneSupport'
        }
        'android' { $required = 'AndroidPlayer' }
        'windows' { $required = 'windowsstandalonesupport' }
        default { return }
    }

    $playbackEngines = Join-Path (Split-Path -Parent $EditorPath) 'Data\PlaybackEngines'
    if (-not (Test-Path -LiteralPath (Join-Path $playbackEngines $required))) {
        Stop-Build "Unity build support '$required' is missing from $playbackEngines. Add it for $Version in Unity Hub (Add modules)."
    }

    # The project builds Standalone with IL2CPP; that needs the il2cpp player variation,
    # which only ships with the "Windows Build Support (IL2CPP)" module.
    if ($Platform -eq 'windows' -and -not $Mono) {
        if ((Get-StandaloneScriptingBackend) -eq 1) {
            $variations = Join-Path $playbackEngines 'windowsstandalonesupport\Variations'
            $hasIl2Cpp = (Test-Path -LiteralPath $variations) -and
                (Get-ChildItem -LiteralPath $variations -Directory -Filter '*il2cpp*' -ErrorAction SilentlyContinue)
            if (-not $hasIl2Cpp) {
                Stop-Build ("The project uses the IL2CPP scripting backend for Standalone, but '$variations' has no " +
                    "il2cpp player. Install 'Windows Build Support (IL2CPP)' in Unity Hub, or build with -Mono.")
            }
        }
    }
}

function ConvertTo-CommandLine([string[]]$ArgumentList) {
    $parts = foreach ($argument in $ArgumentList) {
        if ($null -eq $argument) { continue }
        if ($argument -match '[\s"]') { '"' + ($argument -replace '"', '\"') + '"' } else { $argument }
    }
    return ($parts -join ' ')
}

function Write-Artifacts([string]$Root) {
    if (-not (Test-Path -LiteralPath $Root)) { return }
    $archives = @(Get-ChildItem -LiteralPath $Root -File -Filter '*.zip' -ErrorAction SilentlyContinue)
    $folders = @(Get-ChildItem -LiteralPath $Root -Directory -ErrorAction SilentlyContinue)
    if ($archives.Count -eq 0 -and $folders.Count -eq 0) { return }

    Write-Step 'Output'
    foreach ($folder in ($folders | Sort-Object LastWriteTime -Descending)) {
        Write-Host ("    [dir]  {0}" -f $folder.FullName)
    }
    foreach ($archive in ($archives | Sort-Object LastWriteTime -Descending)) {
        Write-Host ("    [zip]  {0}  ({1:N1} MB)" -f $archive.FullName, ($archive.Length / 1MB))
    }
}

function Show-BuildLog([string]$LogPath, [int]$Tail = 80) {
    if (-not (Test-Path -LiteralPath $LogPath)) {
        Write-Host "    (no Unity log at $LogPath)" -ForegroundColor DarkGray
        return
    }

    Write-Step "Last $Tail lines of $LogPath"
    $lines = @(Get-Content -LiteralPath $LogPath -Tail $Tail -ErrorAction SilentlyContinue)
    foreach ($line in $lines) {
        if ($line -match '(?i)\berror\b|exception|failed|Aborting batchmode') {
            Write-Host "    $line" -ForegroundColor Red
        } else {
            Write-Host "    $line" -ForegroundColor DarkGray
        }
    }
}

function Find-LogHint([string]$LogPath) {
    if (-not (Test-Path -LiteralPath $LogPath)) { return $null }
    $content = Get-Content -LiteralPath $LogPath -ErrorAction SilentlyContinue
    $text = ($content -join "`n")

    if ($text -match 'No valid Unity Editor license found|License is not active|No active license') {
        return 'Unity has no batchmode license. Open the editor once and sign in, or set UNITY_LICENSE in CI.'
    }
    if ($text -match 'Multiple Unity instances cannot open the same project') {
        return 'Close the Unity editor that has this project open, then retry.'
    }
    if ($text -match 'Scripts have compiler errors|error CS\d+') {
        return 'C# compilation failed; fix the reported errors on the console.'
    }
    if ($text -match "Build completed with a result of 'Failed'") {
        return 'Unity reports a failed build; see the "error" lines above.'
    }
    if ($text -match 'Aborting batchmode due to failure') {
        return 'The editor aborted; see the failing lines above.'
    }
    return $null
}

if ($Help) {
    Show-Usage
    exit 0
}

$resolvedTargets = Split-Targets $Target
$serverRequested = $resolvedTargets -contains 'linux'
$unityTargets = @($resolvedTargets | Where-Object { $_ -ne 'linux' })
$serverOnly = ($unityTargets.Count -eq 0)

$OutputRoot = Resolve-FullPath $OutputRoot $ProjectRoot
if (-not $OutputRoot) { $OutputRoot = (Join-Path $ProjectRoot 'Builds') }
if (-not $Log) { $Log = Join-Path $OutputRoot 'unity-build.log' } else { $Log = Resolve-FullPath $Log $ProjectRoot }

$Version = Read-EditorVersion
$BundleVersion = Read-BundleVersion

Write-Step 'Build request'
Write-Detail "Project     : $ProjectRoot"
Write-Detail "Editor      : $Version"
Write-Detail "Player ver  : $BundleVersion"
Write-Detail "Targets     : $($resolvedTargets -join ', ')"
Write-Detail "Output root : $OutputRoot"
Write-Detail "Log         : $Log"
if ($DryRun) { Write-Detail 'Mode        : dry run (nothing is built)' }

$dotnetPath = Resolve-DotnetPath $Dotnet
$sdkMajors = Assert-DotnetSdk $dotnetPath
Write-Detail "dotnet      : $dotnetPath (SDK $(($sdkMajors | Sort-Object)[-1]))"

# ---------------------------------------------------------------- server package
if ($serverRequested) {
    Write-Step 'Package .NET server (scripts/build_server.py)'
    $python = Resolve-Python
    Write-Detail "python      : $($python.Exe) $($python.Arguments -join ' ')"

    $serverArguments = @($ServerPackageScript, '--target', 'linux', '--output-root', $OutputRoot)
    if ($FrameworkDependent) { $serverArguments += '--framework-dependent' }
    if ($NoZip) { $serverArguments += '--no-zip' }
    $serverCommandLine = ConvertTo-CommandLine (@($python.Arguments) + $serverArguments)
    Write-Detail "command     : & '$($python.Exe)' $serverCommandLine"

    if (-not $DryRun) {
        New-Item -ItemType Directory -Force -Path $OutputRoot | Out-Null
        $pythonArguments = @($python.Arguments) + $serverArguments
        & $python.Exe @pythonArguments
        if ($LASTEXITCODE -ne 0) { Stop-Build "Server packaging failed with exit code $LASTEXITCODE." }
        Write-Detail 'server package done'
    }
}

if ($serverOnly) {
    if (-not $DryRun) {
        Write-Artifacts $OutputRoot
        Write-Host ''
        Write-Host 'Server package is ready.' -ForegroundColor Green
    }
    exit 0
}

# ---------------------------------------------------------------- unity player
Write-Step 'Resolve Unity editor'
$editorPath = Resolve-UnityEditor $Version $Unity
Write-Detail "unity       : $editorPath"

$editorVersion = (Get-Item -LiteralPath $editorPath).VersionInfo.ProductVersion
if ($editorVersion -and -not $editorVersion.StartsWith($Version)) {
    Write-Host "    warning: $editorPath reports $editorVersion but the project pins $Version." -ForegroundColor Yellow
}

foreach ($platform in $unityTargets) {
    Assert-PlatformSupport $editorPath $platform
}

$entryMethod = 'BuildScript.BuildFromCommandLine'
if ($Mono) {
    if ($unityTargets.Count -ne 1) {
        Stop-Build '-Mono needs exactly one platform; the Mono entry points reject multi platform requests.'
    }
    switch ($unityTargets[0]) {
        'windows' { $entryMethod = 'BuildScript.BuildWindowsMonoFromCommandLine' }
        'mac' { $entryMethod = 'BuildScript.BuildMacMonoFromCommandLine' }
        'mac-intel' { $entryMethod = 'BuildScript.BuildMacMonoFromCommandLine' }
        default { Stop-Build "-Mono is not supported for target '$($unityTargets[0])'." }
    }
}

$unityLock = Join-Path $ProjectRoot 'Temp\UnityLockfile'
if (Test-Path -LiteralPath $unityLock) {
    Write-Host '    warning: Temp\UnityLockfile exists; close the Unity editor if it has this project open.' -ForegroundColor Yellow
}

$env:PCRCALC_BUILD_TARGET = ($unityTargets -join ',')
$env:PCRCALC_OUTPUT_ROOT = $OutputRoot
$env:PCRCALC_ANDROID_FORMAT = $AndroidFormat
$env:PCRCALC_DEVELOPMENT_BUILD = $Development.IsPresent.ToString().ToLowerInvariant()
$env:PCRCALC_SERVER_SELF_CONTAINED = (-not $FrameworkDependent).ToString().ToLowerInvariant()
$env:PCRCALC_ZIP_DESKTOP_BUILDS = (-not $NoZip).ToString().ToLowerInvariant()
$env:PCRCALC_DOTNET_PATH = $dotnetPath

$unityArgumentList = @(
    '-batchmode',
    '-quit',
    '-projectPath', $ProjectRoot,
    '-logFile', $Log,
    '-executeMethod', $entryMethod
)
if (-not $Graphics) { $unityArgumentList += '-nographics' }
if ($UnityArgs) { $unityArgumentList += $UnityArgs }

$unityCommandLine = ConvertTo-CommandLine $unityArgumentList

Write-Step 'Unity batchmode build'
Write-Detail "method      : $entryMethod"
Write-Detail "targets     : $($unityTargets -join ', ')"
Write-Detail "command     : & '$editorPath' $unityCommandLine"

if ($DryRun) {
    Write-Host ''
    Write-Host 'Dry run finished; nothing was built.' -ForegroundColor Green
    exit 0
}

New-Item -ItemType Directory -Force -Path $OutputRoot | Out-Null
New-Item -ItemType Directory -Force -Path (Split-Path -Parent $Log) | Out-Null

$stopwatch = [System.Diagnostics.Stopwatch]::StartNew()

# Unity rewrites ProjectSettings\*.asset while building (the Mono entry flips
# scriptingBackend/Standalone to Mono2x). Snapshot them so a build never leaves
# unrelated project settings staged for commit.
$settingsDir = Join-Path $ProjectRoot 'ProjectSettings'
$settingsBackup = Join-Path ([System.IO.Path]::GetTempPath()) ('pcr-projsettings-' + [Guid]::NewGuid().ToString('N'))
$settingsFiles = @()
if (Test-Path -LiteralPath $settingsDir) {
    New-Item -ItemType Directory -Force -Path $settingsBackup | Out-Null
    foreach ($settingsFile in Get-ChildItem -LiteralPath $settingsDir -Filter *.asset -File) {
        Copy-Item -LiteralPath $settingsFile.FullName -Destination (Join-Path $settingsBackup $settingsFile.Name) -Force
        $settingsFiles += $settingsFile.FullName
    }
}

# Unity.exe is a GUI subsystem binary, so & does not wait and $LASTEXITCODE stays unset.
# Start the process directly so Windows PowerShell 5.1 retains its native handle.
$startInfo = New-Object System.Diagnostics.ProcessStartInfo
$startInfo.FileName = $editorPath
$startInfo.Arguments = $unityCommandLine
$startInfo.UseShellExecute = $false
$startInfo.CreateNoWindow = $true
$startInfo.WorkingDirectory = $ProjectRoot
$process = [System.Diagnostics.Process]::Start($startInfo)
try {
    $process.WaitForExit()
    $exitCode = $process.ExitCode
} finally {
    $process.Dispose()
}
$stopwatch.Stop()

$restoredSettings = @()
foreach ($settingsFile in $settingsFiles) {
    $backupFile = Join-Path $settingsBackup (Split-Path -Leaf $settingsFile)
    if (-not (Test-Path -LiteralPath $backupFile)) { continue }
    $beforeHash = (Get-FileHash -LiteralPath $backupFile -Algorithm SHA256).Hash
    $afterHash = (Get-FileHash -LiteralPath $settingsFile -Algorithm SHA256).Hash
    if ($beforeHash -ne $afterHash) {
        Copy-Item -LiteralPath $backupFile -Destination $settingsFile -Force
        $restoredSettings += Split-Path -Leaf $settingsFile
    }
}
if (Test-Path -LiteralPath $settingsBackup) { Remove-Item -LiteralPath $settingsBackup -Recurse -Force -ErrorAction SilentlyContinue }

$elapsed = $stopwatch.Elapsed.ToString('hh\:mm\:ss')

Write-Detail "exit code : $exitCode"

if ($restoredSettings.Count -gt 0) {
    Write-Detail "settings  : restored $($restoredSettings -join ', ') (Unity rewrote them during the build)"
}

if ($exitCode -ne 0) {
    Write-Host ''
    Write-Host "Unity exited with code $exitCode after $elapsed." -ForegroundColor Red
    $hint = Find-LogHint $Log
    if ($hint) { Write-Host "  hint: $hint" -ForegroundColor Yellow }
    Show-BuildLog $Log
    exit 1
}

$failedBuildPattern = "Build completed with a result of 'Failed'"
if (Test-Path -LiteralPath $Log) {
    $logText = (Get-Content -LiteralPath $Log -ErrorAction SilentlyContinue) -join "`n"
    if ($logText -match [regex]::Escape($failedBuildPattern)) {
        Write-Host ''
        Write-Host 'Unity reported a failed build even though it exited with 0.' -ForegroundColor Red
        Show-BuildLog $Log
        exit 1
    }
}

Write-Step 'Result'
Write-Detail "elapsed     : $elapsed"
Write-Artifacts $OutputRoot

$expectedZip = Join-Path $OutputRoot ("PCRCalculator-{0}-win-x64.zip" -f $BundleVersion)
if (-not $NoZip -and $unityTargets -contains 'windows' -and -not (Test-Path -LiteralPath $expectedZip)) {
    Write-Host "    warning: expected archive was not produced: $expectedZip" -ForegroundColor Yellow
}

Write-Host ''
Write-Host 'Build finished.' -ForegroundColor Green
exit 0
