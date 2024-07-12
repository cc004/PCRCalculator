using System;
using System.IO;
using AssetsTools;
using AssetsTools.NET.Extra;
using AssetsTools.NET;
using UnityEngine;
using System.Collections.Generic;
using PCRCaculator;
using PCRCaculator.Update;


public static class AssetExtensions
{

    public static AssetBundleFile DecompressToMemory(BundleFileInstance bundleInst)
    {
        return BundleHelper.UnpackBundleToStream(bundleInst.file, new MemoryStream());
    }

    public static AssetBundle getAssetBundle(this WWW www)
    {
        if (www.bytes == null) return null;
        try
        {
            bool fileChanged = false;
            AssetsManager am = new AssetsManager();
            /*
            AssetBundleFile ab;
#if PLATFORM_ANDROID
            am.LoadClassPackage(Path.Combine(Application.persistentDataPath, "classdata.tpk"));
#else
            am.LoadClassPackage(Path.Combine(Application.streamingAssetsPath, "classdata.tpk"));
#endif*/
            BundleFileInstance bundleFileInstance;
            bundleFileInstance = am.LoadBundleFile(new MemoryStream(www.bytes), ABExTool.persistentDataPath, unpackIfPacked: false);
            bundleFileInstance.file = DecompressToMemory(bundleFileInstance);

#if PLATFORM_ANDROID
            var preferredPlatform = (uint)BuildTarget.Android;
#elif UNITY_STANDALONE_OSX
            var preferredPlatform = (uint)BuildTarget.StandaloneOSX;
#else
            var preferredPlatform = (uint)BuildTarget.StandaloneWindows64;
#endif
            for (int i = 0; i < bundleFileInstance.file.BlockAndDirInfo.DirectoryInfos.Count; ++i)
            {
                AssetsFileInstance assetsFileInstance = am.LoadAssetsFileFromBundle(bundleFileInstance, 0);
                /*
                if (i == 0)
                    am.LoadClassDatabaseFromPackage(assetsFileInstance.file.Metadata.UnityVersion);
                */
                if (assetsFileInstance.file.Metadata.UnityVersion != "2020.3.17f1")
                {
                    assetsFileInstance.file.Metadata.UnityVersion = "2020.3.17f1";
                    fileChanged = true;
                }

                if (assetsFileInstance.file.Metadata.TargetPlatform != preferredPlatform)
                {
                    assetsFileInstance.file.Metadata.TargetPlatform = preferredPlatform;

                    bundleFileInstance.file.BlockAndDirInfo.DirectoryInfos[i].SetNewData(assetsFileInstance.file);
                    fileChanged = true;
                }

            }

            if (fileChanged)
            {
                using (var ms = new MemoryStream())
                {
                    using (var aw = new AssetsFileWriter(ms))
                        bundleFileInstance.file.Write(aw);
                    return AssetBundle.LoadFromMemory(ms.ToArray());
                }
            }
            else
            {
                return AssetBundle.LoadFromMemory(www.bytes);
            }
        }
        catch
        {
            return null;
        }
    }
}