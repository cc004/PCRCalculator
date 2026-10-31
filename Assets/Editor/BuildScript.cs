using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

public static class BuildScript
{
    private const string DefaultOutputRoot = "Builds";
    private const string ProductNameFallback = "PCRGuildCalculator";
    private const string AndroidFormatApk = "apk";
    private const string AndroidFormatAab = "aab";

    public static void BuildFromCommandLine()
    {
        Execute(BuildRequest.FromEnvironment(Environment.GetCommandLineArgs()));
    }

    [MenuItem("PCR Build/Windows")]
    public static void BuildWindows64()
    {
        Execute(BuildRequest.ForTargets(BuildPlatform.Windows64));
    }

    [MenuItem("PCR Build/Linux")]
    public static void BuildLinux64()
    {
        Execute(BuildRequest.ForTargets(BuildPlatform.Linux64));
    }

    [MenuItem("PCR Build/Android")]
    public static void BuildAndroid()
    {
        Execute(BuildRequest.ForTargets(BuildPlatform.Android));
    }

    [MenuItem("PCR Build/All")]
    public static void BuildAll()
    {
        Execute(BuildRequest.ForTargets(BuildPlatform.Windows64, BuildPlatform.Linux64, BuildPlatform.Android));
    }

    public static void BuildAppleSilicon()
    {
        BuildLegacyAppleSilicon();
    }

    private static void Execute(BuildRequest request)
    {
        string[] scenes = GetEnabledScenes();
        BuildOptions buildOptions = BuildOptions.StrictMode;
        if (request.DevelopmentBuild)
        {
            buildOptions |= BuildOptions.Development;
        }

        BuildOptions androidBuildOptions = buildOptions | BuildOptions.CompressWithLz4HC;

        AssetDatabase.SaveAssets();

        foreach (BuildPlatform platform in request.Platforms)
        {
            switch (platform)
            {
                case BuildPlatform.Windows64:
                    BuildDesktopPlayer(
                        request,
                        scenes,
                        buildOptions,
                        BuildTargetGroup.Standalone,
                        BuildTarget.StandaloneWindows64,
                        "Windows",
                        GetDesktopExecutableName(".exe"),
                        "win-x64",
                        shouldZip: true);
                    break;
                case BuildPlatform.Linux64:
                    BuildLinuxServerPackage(request);
                    break;
                case BuildPlatform.Android:
                    BuildAndroidPlayer(request, scenes, androidBuildOptions);
                    break;
                default:
                    throw new InvalidOperationException($"Unsupported build platform: {platform}");
            }
        }
    }

    private static void BuildDesktopPlayer(
        BuildRequest request,
        string[] scenes,
        BuildOptions buildOptions,
        BuildTargetGroup targetGroup,
        BuildTarget target,
        string platformFolder,
        string executableName,
        string runtimeId,
        bool shouldZip)
    {
        EnsureBuildTargetSupported(targetGroup, target);
        SwitchActiveBuildTarget(targetGroup, target);

        string targetRoot = Path.Combine(request.OutputRoot, platformFolder);
        string appRoot = Path.Combine(targetRoot, GetDesktopOutputFolderName(platformFolder));
        string executablePath = Path.Combine(appRoot, executableName);

        RecreateDirectory(targetRoot);
        Directory.CreateDirectory(appRoot);

        BuildPlayerOptions options = new BuildPlayerOptions
        {
            scenes = scenes,
            locationPathName = executablePath,
            targetGroup = targetGroup,
            target = target,
            options = buildOptions,
        };

        BuildReport report = BuildPipeline.BuildPlayer(options);
        AssertBuildSucceeded(report, target);

        if (shouldZip && request.ZipDesktopBuilds)
        {
            string zipName = platformFolder == "Windows"
                ? $"{GetBundleVersion()}.zip"
                : $"{GetBundleVersion()}-{platformFolder.ToLowerInvariant()}-x64.zip";
            string zipPath = Path.Combine(request.OutputRoot, zipName);
            RecreateZip(targetRoot, zipPath);
        }
    }

    private static void BuildAndroidPlayer(BuildRequest request, string[] scenes, BuildOptions buildOptions)
    {
        EnsureBuildTargetSupported(BuildTargetGroup.Android, BuildTarget.Android);
        SwitchActiveBuildTarget(BuildTargetGroup.Android, BuildTarget.Android);

        string targetRoot = Path.Combine(request.OutputRoot, "Android");
        string extension = request.AndroidFormat == AndroidFormatAab ? ".aab" : ".apk";
        string artifactPath = Path.Combine(targetRoot, GetBundleVersion() + extension);

        RecreateDirectory(targetRoot);

        AndroidSigningSettingsSnapshot signingSnapshot = AndroidSigningSettingsSnapshot.Capture();
        bool previousBuildAppBundle = EditorUserBuildSettings.buildAppBundle;

        try
        {
            EditorUserBuildSettings.buildAppBundle = request.AndroidFormat == AndroidFormatAab;
            ApplyAndroidSigningFromEnvironment();

            BuildPlayerOptions options = new BuildPlayerOptions
            {
                scenes = scenes,
                locationPathName = artifactPath,
                targetGroup = BuildTargetGroup.Android,
                target = BuildTarget.Android,
                options = buildOptions,
            };

            BuildReport report = BuildPipeline.BuildPlayer(options);
            AssertBuildSucceeded(report, BuildTarget.Android);
        }
        finally
        {
            signingSnapshot.Restore();
            EditorUserBuildSettings.buildAppBundle = previousBuildAppBundle;
        }
    }

    private static void BuildLinuxServerPackage(BuildRequest request)
    {
        string projectRoot = Path.GetFullPath(Path.Combine(Application.dataPath, ".."));
        string target = "linux";
        var arguments = $"{Quote(Path.Combine(projectRoot, "scripts", "build_server.py"))} --target {target} --output-root {Quote(request.OutputRoot)}";
        if (!request.ServerSelfContained) arguments += " --framework-dependent";
        if (!request.ZipDesktopBuilds) arguments += " --no-zip";
        using var process = Process.Start(new ProcessStartInfo
        {
            FileName = "python3", Arguments = arguments, WorkingDirectory = projectRoot,
            UseShellExecute = false,
        });
        if (process == null) throw new InvalidOperationException("Failed to start server package helper (python3 required).");
        process.WaitForExit();
        if (process.ExitCode != 0) throw new InvalidOperationException($"Server package failed: exit {process.ExitCode}");
    }

    private static void BuildLegacyAppleSilicon()
    {
        EnsureBuildTargetSupported(BuildTargetGroup.Standalone, BuildTarget.StandaloneOSX);
        SwitchActiveBuildTarget(BuildTargetGroup.Standalone, BuildTarget.StandaloneOSX);

        string targetRoot = Path.Combine(Path.GetFullPath(DefaultOutputRoot), "MacOS");
        string outputPath = Path.Combine(targetRoot, "mzq.app");
        string zipPath = Path.Combine(Path.GetFullPath(DefaultOutputRoot), $"{GetBundleVersion()}-mac-arm.zip");

        RecreateDirectory(targetRoot);

        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions
        {
            scenes = GetEnabledScenes(),
            locationPathName = outputPath,
            targetGroup = BuildTargetGroup.Standalone,
            target = BuildTarget.StandaloneOSX,
            options = BuildOptions.StrictMode | BuildOptions.CompressWithLz4HC,
        };

        BuildReport report = BuildPipeline.BuildPlayer(buildPlayerOptions);
        AssertBuildSucceeded(report, BuildTarget.StandaloneOSX);

        RecreateZip(targetRoot, zipPath);
    }

    private static void ApplyAndroidSigningFromEnvironment()
    {
        string keystorePath = ReadEnvironment("PCRCALC_ANDROID_KEYSTORE_NAME");
        if (string.IsNullOrWhiteSpace(keystorePath))
        {
            return;
        }

        string keystorePassword = RequireEnvironment("PCRCALC_ANDROID_KEYSTORE_PASS");
        string keyaliasName = RequireEnvironment("PCRCALC_ANDROID_KEYALIAS_NAME");
        string keyaliasPassword = RequireEnvironment("PCRCALC_ANDROID_KEYALIAS_PASS");

        PlayerSettings.Android.useCustomKeystore = true;
        PlayerSettings.Android.keystoreName = Path.GetFullPath(keystorePath);
        PlayerSettings.Android.keystorePass = keystorePassword;
        PlayerSettings.Android.keyaliasName = keyaliasName;
        PlayerSettings.Android.keyaliasPass = keyaliasPassword;
    }

    private static string RequireEnvironment(string name)
    {
        string value = ReadEnvironment(name);
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new InvalidOperationException($"Environment variable {name} is required when using a custom Android keystore.");
        }

        return value;
    }

    private static string ReadEnvironment(string name)
    {
        return Environment.GetEnvironmentVariable(name) ?? string.Empty;
    }

    private static void EnsureBuildTargetSupported(BuildTargetGroup group, BuildTarget target)
    {
        if (!BuildPipeline.IsBuildTargetSupported(group, target))
        {
            throw new InvalidOperationException($"Unity build support for {target} is not installed.");
        }
    }

    private static void SwitchActiveBuildTarget(BuildTargetGroup group, BuildTarget target)
    {
        if (EditorUserBuildSettings.activeBuildTarget == target)
        {
            return;
        }

        if (!EditorUserBuildSettings.SwitchActiveBuildTarget(group, target))
        {
            throw new InvalidOperationException($"Failed to switch active build target to {target}.");
        }
    }

    private static string[] GetEnabledScenes()
    {
        string[] scenes = EditorBuildSettings.scenes
            .Where(scene => scene.enabled)
            .Select(scene => scene.path)
            .ToArray();

        if (scenes.Length == 0)
        {
            throw new InvalidOperationException("No enabled scenes found in EditorBuildSettings.");
        }

        return scenes;
    }

    private static void AssertBuildSucceeded(BuildReport report, BuildTarget target)
    {
        if (report.summary.result != BuildResult.Succeeded)
        {
            throw new InvalidOperationException($"Build for {target} failed with result {report.summary.result}.");
        }

        UnityEngine.Debug.Log(
            $"Build for {target} succeeded. Output size: {report.summary.totalSize} bytes. Duration: {report.summary.totalTime}.");
    }

    private static void RecreateDirectory(string directoryPath)
    {
        if (Directory.Exists(directoryPath))
        {
            Directory.Delete(directoryPath, true);
        }

        Directory.CreateDirectory(directoryPath);
    }

    private static void RecreateZip(string sourceDirectory, string zipPath)
    {
        if (File.Exists(zipPath))
        {
            File.Delete(zipPath);
        }

        ZipFile.CreateFromDirectory(sourceDirectory, zipPath);
        UnityEngine.Debug.Log($"Build output zipped to {zipPath}");
    }

    private static string GetDesktopExecutableName(string extension)
    {
        return GetProductFolderName() + extension;
    }

    private static string GetDesktopOutputFolderName(string platformFolder)
    {
        return platformFolder == "Windows" ? GetBundleVersion() : GetProductFolderName();
    }

    private static string GetProductFolderName()
    {
        string productName = PlayerSettings.productName;
        return string.IsNullOrWhiteSpace(productName) ? ProductNameFallback : productName;
    }

    private static string GetBundleVersion()
    {
        return string.IsNullOrWhiteSpace(PlayerSettings.bundleVersion) ? "0.0.0" : PlayerSettings.bundleVersion;
    }

    private static string Quote(string value)
    {
        return "\"" + value.Replace("\"", "\\\"") + "\"";
    }

    private enum BuildPlatform
    {
        Windows64,
        Linux64,
        Android,
    }

    private sealed class BuildRequest
    {
        public IReadOnlyList<BuildPlatform> Platforms { get; private set; }
        public string OutputRoot { get; private set; }
        public bool DevelopmentBuild { get; private set; }
        public bool ServerSelfContained { get; private set; }
        public bool ZipDesktopBuilds { get; private set; }
        public string AndroidFormat { get; private set; }

        public static BuildRequest ForTargets(params BuildPlatform[] platforms)
        {
            return new BuildRequest
            {
                Platforms = platforms,
                OutputRoot = Path.GetFullPath(DefaultOutputRoot),
                DevelopmentBuild = false,
                ServerSelfContained = true,
                ZipDesktopBuilds = true,
                AndroidFormat = AndroidFormatApk,
            };
        }

        public static BuildRequest FromEnvironment(string[] args)
        {
            Dictionary<string, string> values = ParseArgs(args);
            string targetText = FirstNonEmpty(
                GetArgumentValue(values, "-pcrBuildTarget"),
                Environment.GetEnvironmentVariable("PCRCALC_BUILD_TARGET"),
                "all");

            string outputRoot = FirstNonEmpty(
                GetArgumentValue(values, "-pcrOutputRoot"),
                Environment.GetEnvironmentVariable("PCRCALC_OUTPUT_ROOT"),
                DefaultOutputRoot);

            string androidFormat = FirstNonEmpty(
                GetArgumentValue(values, "-pcrAndroidFormat"),
                Environment.GetEnvironmentVariable("PCRCALC_ANDROID_FORMAT"),
                AndroidFormatApk);

            return new BuildRequest
            {
                Platforms = ParsePlatforms(targetText),
                OutputRoot = Path.GetFullPath(outputRoot),
                DevelopmentBuild = ParseBool(
                    FirstNonEmpty(
                        GetArgumentValue(values, "-pcrDevelopmentBuild"),
                        Environment.GetEnvironmentVariable("PCRCALC_DEVELOPMENT_BUILD"),
                        "false")),
                ServerSelfContained = ParseBool(
                    FirstNonEmpty(
                        GetArgumentValue(values, "-pcrServerSelfContained"),
                        Environment.GetEnvironmentVariable("PCRCALC_SERVER_SELF_CONTAINED"),
                        "true")),
                ZipDesktopBuilds = ParseBool(
                    FirstNonEmpty(
                        GetArgumentValue(values, "-pcrZipDesktopBuilds"),
                        Environment.GetEnvironmentVariable("PCRCALC_ZIP_DESKTOP_BUILDS"),
                        "true")),
                AndroidFormat = NormalizeAndroidFormat(androidFormat),
            };
        }

        private static Dictionary<string, string> ParseArgs(string[] args)
        {
            Dictionary<string, string> result = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            for (int index = 0; index < args.Length; index++)
            {
                string arg = args[index];
                if (!arg.StartsWith("-"))
                {
                    continue;
                }

                if (index + 1 < args.Length && !args[index + 1].StartsWith("-"))
                {
                    result[arg] = args[index + 1];
                    index++;
                }
                else
                {
                    result[arg] = "true";
                }
            }

            return result;
        }

        private static string GetArgumentValue(Dictionary<string, string> values, string name)
        {
            return values.TryGetValue(name, out string value) ? value : string.Empty;
        }

        private static IReadOnlyList<BuildPlatform> ParsePlatforms(string text)
        {
            List<BuildPlatform> platforms = new List<BuildPlatform>();
            foreach (string rawPart in text.Split(new[] { ',', ';', '+' }, StringSplitOptions.RemoveEmptyEntries))
            {
                string part = rawPart.Trim().ToLowerInvariant();
                switch (part)
                {
                    case "all":
                        AddPlatform(platforms, BuildPlatform.Windows64);
                        AddPlatform(platforms, BuildPlatform.Linux64);
                        AddPlatform(platforms, BuildPlatform.Android);
                        break;
                    case "windows":
                    case "win":
                    case "win64":
                        AddPlatform(platforms, BuildPlatform.Windows64);
                        break;
                    case "linux":
                    case "linux64":
                        AddPlatform(platforms, BuildPlatform.Linux64);
                        break;
                    case "android":
                        AddPlatform(platforms, BuildPlatform.Android);
                        break;
                    default:
                        throw new InvalidOperationException($"Unsupported build target '{rawPart}'.");
                }
            }

            if (platforms.Count == 0)
            {
                throw new InvalidOperationException("No build targets were resolved from the request.");
            }

            return platforms.ToArray();
        }

        private static bool ParseBool(string value)
        {
            string normalized = value.Trim().ToLowerInvariant();
            return normalized == "1" || normalized == "true" || normalized == "yes" || normalized == "on";
        }

        private static string NormalizeAndroidFormat(string value)
        {
            string normalized = value.Trim().ToLowerInvariant();
            return normalized == AndroidFormatAab ? AndroidFormatAab : AndroidFormatApk;
        }

        private static void AddPlatform(List<BuildPlatform> platforms, BuildPlatform platform)
        {
            if (!platforms.Contains(platform))
            {
                platforms.Add(platform);
            }
        }

        private static string FirstNonEmpty(params string[] values)
        {
            foreach (string value in values)
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    return value;
                }
            }

            return string.Empty;
        }
    }

    private struct AndroidSigningSettingsSnapshot
    {
        private readonly bool useCustomKeystore;
        private readonly string keystoreName;
        private readonly string keystorePass;
        private readonly string keyaliasName;
        private readonly string keyaliasPass;

        private AndroidSigningSettingsSnapshot(
            bool useCustomKeystore,
            string keystoreName,
            string keystorePass,
            string keyaliasName,
            string keyaliasPass)
        {
            this.useCustomKeystore = useCustomKeystore;
            this.keystoreName = keystoreName;
            this.keystorePass = keystorePass;
            this.keyaliasName = keyaliasName;
            this.keyaliasPass = keyaliasPass;
        }

        public static AndroidSigningSettingsSnapshot Capture()
        {
            return new AndroidSigningSettingsSnapshot(
                PlayerSettings.Android.useCustomKeystore,
                PlayerSettings.Android.keystoreName,
                PlayerSettings.Android.keystorePass,
                PlayerSettings.Android.keyaliasName,
                PlayerSettings.Android.keyaliasPass);
        }

        public void Restore()
        {
            PlayerSettings.Android.useCustomKeystore = useCustomKeystore;
            PlayerSettings.Android.keystoreName = keystoreName;
            PlayerSettings.Android.keystorePass = keystorePass;
            PlayerSettings.Android.keyaliasName = keyaliasName;
            PlayerSettings.Android.keyaliasPass = keyaliasPass;
        }
    }
}
