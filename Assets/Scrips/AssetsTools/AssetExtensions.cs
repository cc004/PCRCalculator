using System;
using System.IO;
using AssetsTools;
using UnityEngine;


public static class AssetExtensions
{
    public static AssetBundle getAssetBundle(this WWW www)
    {
        return www.assetBundle;
        try
        {
            var ab = AssetBundleFile.LoadFromMemory(www.bytes);
            var flag = false;

            ab.Header.format = 6;
            ab.Header.versionPlayer = "2020.3.17f1";

            foreach (var handle in ab.Files)
            {
                if (handle.Name.EndsWith(".resS")) continue;
                var asset = handle.ToAssetsFile();
                asset.Header.Version = 17;

                if (asset.MetadataHeader.UnityVersion == "0.0.0")
                {
                    asset.MetadataHeader.UnityVersion = "2020.3.17f1";
                }
#if PLATFORM_ANDROID
                if (asset.MetadataHeader.TargetPlatform != (int) BuildTarget.Android)
                {
                    asset.MetadataHeader.TargetPlatform = (int) BuildTarget.Android;
                }
#endif
                handle.LoadAssetsFile(asset);
                flag = true;
            }
            if (!flag) return www.assetBundle;

            UnityBinaryWriter w = new UnityBinaryWriter();
            ab.Write(w);

            File.WriteAllBytes("temp.unity3d", w.ToBytes());

            return AssetBundle.LoadFromMemory(w.ToBytes());
        }
        catch (Exception e)
        {
            return www.assetBundle;
        }
    }
}