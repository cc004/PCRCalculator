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
            var flag = false;
            foreach (var handle in ab.Files)
            {
                if (handle.Name.EndsWith(".resS")) continue;
                var asset = handle.ToAssetsFile();
#if PLATFORM_ANDROID
                if (asset.MetadataHeader.UnityVersion == "2017.4.37c2" ||
                    asset.MetadataHeader.TargetPlatform != (int) BuildTarget.Android)
                {
                    asset.MetadataHeader.UnityVersion = "2018.4.30f1";
                    asset.MetadataHeader.TargetPlatform = (int) BuildTarget.Android;
                    handle.LoadAssetsFile(asset);
                    flag = true;
                }
#else
                if (asset.MetadataHeader.UnityVersion == "2017.4.37c2")
                {
                    asset.MetadataHeader.UnityVersion = "2018.4.30f1";
                    handle.LoadAssetsFile(asset);
                    flag = true;
                }
#endif
            }

            if (!flag) return www.assetBundle;

            UnityBinaryWriter w = new UnityBinaryWriter();
            ab.Write(w);

            return AssetBundle.LoadFromMemory(w.ToBytes());
        }
        catch
        {
            return www.assetBundle;
        }
    }
}