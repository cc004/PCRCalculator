using System;
using System.IO;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

public static class UniversalDoNotShipCleanup
{
    [PostProcessBuild(int.MaxValue)]
    public static void OnPostprocessBuild(BuildTarget target, string pathToBuiltProject)
    {
        try
        {
            string outputDirectory = Path.GetDirectoryName(Path.GetFullPath(pathToBuiltProject));
            if (string.IsNullOrEmpty(outputDirectory) || !Directory.Exists(outputDirectory))
                return;

            string exeOrAppName = Path.GetFileNameWithoutExtension(pathToBuiltProject);

            string[] dangerousPatterns = {
                "_BackUpThisFolder_ButDontShipItWithYourGame",
                "_BurstDebugInformation_DoNotShip",
            };

            foreach (string pattern in dangerousPatterns)
            {
                string folderToDelete = Path.Combine(outputDirectory, exeOrAppName + pattern);
                if (Directory.Exists(folderToDelete))
                {
                    Debug.Log($"[UniversalCleanup] Removing DoNotShip folder for {target}: {folderToDelete}");
                    Directory.Delete(folderToDelete, recursive: true);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogWarning($"[UniversalCleanup] Failed to clean up after building for {target}: {e}");
        }
    }
}
