using UnityEditor;

public class BuildScript
{
    public static void BuildAppleSilicon()
    {
		string version = PlayerSettings.bundleVersion;
        string outputPath = "Builds/MacOS/mzq.app";
        string zipFileName = $"{version}-mac-arm.zip";


        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
        buildPlayerOptions.scenes = new[] { 
			"Assets/Scenes/GuildScene.unity" ,
			"Assets/Scenes/BeginScene.unity" ,
			"Assets/Scenes/BattleScene.unity" 
		}; 
        buildPlayerOptions.locationPathName = outputPath;
        buildPlayerOptions.target = BuildTarget.StandaloneOSX;
        buildPlayerOptions.options = BuildOptions.None;

        BuildPipeline.BuildPlayer(buildPlayerOptions);

        ZipBuildOutput("Builds/MacOS", zipFileName);
    }

    private static void ZipBuildOutput(string sourceDirectory, string zipFilePath)
    {
        System.IO.Compression.ZipFile.CreateFromDirectory(sourceDirectory, zipFilePath);
        UnityEngine.Debug.Log($"Build output zipped to {zipFilePath}");
    }
}
