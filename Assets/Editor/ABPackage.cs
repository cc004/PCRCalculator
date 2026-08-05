using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;

public class ExportAssetBundles
{
    [MenuItem("Export/Build AssetBundle From Selection - Track dependencies")]
    static void ExportResource()
    {
        string path = EditorUtility.SaveFilePanel("Save Resource", "", "New Resource", "unity3d");
        if (path.Length == 0)
        {
            return;
        }

        Object[] selection = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);
        BuildSelectionBundle(selection, path, BuildAssetBundleOptions.None);
        Selection.objects = selection;
    }

    [MenuItem("Export/Build AssetBundle From Selection - No dependency tracking")]
    static void ExportResourceNoTrack()
    {
        string path = EditorUtility.SaveFilePanel("Save Resource", "", "New Resource", "unity3d");
        if (path.Length == 0)
        {
            return;
        }

        BuildSelectionBundle(Selection.objects, path, BuildAssetBundleOptions.AppendHashToAssetBundleName);
    }

    private static void BuildSelectionBundle(Object[] selection, string path, BuildAssetBundleOptions options)
    {
        var assetNames = new List<string>();
        AddAssetPath(assetNames, Selection.activeObject);
        foreach (Object asset in selection)
        {
            AddAssetPath(assetNames, asset);
        }

        if (assetNames.Count == 0)
        {
            EditorUtility.DisplayDialog("Build AssetBundle", "Select at least one project asset.", "OK");
            return;
        }

        string outputDirectory = Path.GetDirectoryName(path);
        string bundleName = Path.GetFileName(path);
        var builds = new[]
        {
            new AssetBundleBuild
            {
                assetBundleName = bundleName,
                assetNames = assetNames.ToArray()
            }
        };

        AssetBundleManifest manifest = BuildPipeline.BuildAssetBundles(
            outputDirectory,
            builds,
            options,
            BuildTarget.Android);
        if (manifest == null)
        {
            Debug.LogError($"Failed to build AssetBundle '{bundleName}'.");
        }
    }

    private static void AddAssetPath(List<string> assetNames, Object asset)
    {
        if (asset == null)
        {
            return;
        }

        string assetPath = AssetDatabase.GetAssetPath(asset);
        if (!string.IsNullOrEmpty(assetPath)
            && !AssetDatabase.IsValidFolder(assetPath)
            && !assetNames.Contains(assetPath))
        {
            assetNames.Add(assetPath);
        }
    }
}
