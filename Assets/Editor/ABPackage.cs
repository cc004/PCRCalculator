﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;
public class ExportAssetBundles
{
    [MenuItem("Export/Build AssetBundle From Selection - Track dependencies")]
    static void ExportResource()
    {         // Bring up save panel         
        string path = EditorUtility.SaveFilePanel ("Save Resource", "", "New Resource", "unity3d");         
        if (path.Length != 0) 
        {             // Build the resource file from the active selection.         
            Object[] selection = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);             
            BuildPipeline.BuildAssetBundle(Selection.activeObject, selection, path, BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets,BuildTarget.Android);
            Selection.objects = selection;         
        }     
    }     
    [MenuItem("Export/Build AssetBundle From Selection - No dependency tracking")]     
    static void ExportResourceNoTrack () 
    {         // Bring up save panel         
        string path = EditorUtility.SaveFilePanel ("Save Resource", "", "New Resource", "unity3d");
        if (path.Length != 0) 
        {             // Build the resource file from the active selection.             
            BuildPipeline.BuildAssetBundle(Selection.activeObject, Selection.objects, path,BuildAssetBundleOptions.AppendHashToAssetBundleName,BuildTarget.Android);
        }     
    } 
}
