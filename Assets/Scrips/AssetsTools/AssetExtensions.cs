using System;
using System.IO;
using AssetsTools;
using UnityEngine;


public static class AssetExtensions
{
    public static AssetBundle getAssetBundle(this WWW www)
    {
        try
        {
            var ab = AssetBundleFile.LoadFromMemory(www.bytes);

            foreach (var handle in ab.Files)
            {
                if (handle.Name.EndsWith(".resS")) continue;
                var asset = handle.ToAssetsFile();

                asset.MetadataHeader.UnityVersion = "2020.3.17f1";

#if PLATFORM_ANDROID
            if (asset.MetadataHeader.TargetPlatform != (int) BuildTarget.Android)
            {
                asset.MetadataHeader.TargetPlatform = (int) BuildTarget.Android;
            }
#endif
                handle.LoadAssetsFile(asset);
            }

            UnityBinaryWriter w = new UnityBinaryWriter();
            ab.Write(w);

            return AssetBundle.LoadFromMemory(w.ToBytes());
        }
        catch
        {
            return null;
        }
    }
}