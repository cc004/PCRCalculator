using PCR_cysp2skel;
using Spine;
using Spine.Unity;
using System;
using System.IO;
using UnityEngine;

namespace PCRCaculator
{
    public class SpineCreator : MonoBehaviour
    {
        public static SpineCreator Instance;
        public Material spineMaterial;
        //public Texture2D texture;
        public void Awake()
        {
            Instance = this;
        }
#if !UNITY_5_3_OR_NEWER
        private static Dictionary<(int, float, bool ,string, string, int), SkeletonDataAsset> _skeletonCache = new(); // 缓存skeletondata加速，unity端并非瓶颈 不适用
#endif
        public SkeletonDataAsset Createskeletondata(int skinID, float scale = 0.5f, bool useAB = false, string p0 = null, string skinName = null,
            int prefabOverride = -1)
        {
#if !UNITY_5_3_OR_NEWER
            var cacheKey = (skinID, scale, useAB, p0, skinName, prefabOverride);
            if (_skeletonCache.TryGetValue(cacheKey, out var val)) return val;
#endif
            //int skinID = prefabID >= 200000 ? prefabID : prefabID + 30;
            var p = p0 ?? "spine_sdnormal_" + skinID + ".unity3d";
            string ab_atlas = p;
            if (prefabOverride != -1) p = "spine_sdnormal_" + prefabOverride + ".unity3d";
            try
            {
#if UNITY_5_3_OR_NEWER
            Texture2D texture = new Texture2D(0, 0);
            
                //if (useAB)
                //{
                    texture = ABExTool.GetAssetBundleByName<Texture2D>(p, (skinName ?? $"{(prefabOverride != -1 ? prefabOverride : skinID)}") + ".png");
                //}
                //else
                //{
                    /*
                    string path = Application.streamingAssetsPath + "/Datas/Unit/" + prefabID + "/" + skinID + ".png";//\Datas\Unit\112001
                    FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
                    byte[] bytes = new byte[fs.Length];
                    fs.Read(bytes, 0, (int)fs.Length);
                    texture.LoadImage(bytes);*/

                //}
                Material tempMateral = new Material(spineMaterial);
                tempMateral.mainTexture = texture;
                if (texture != null)
                    tempMateral.mainTexture.name = "force_override";//skinID.ToString();
                tempMateral.mainTextureScale = new Vector2(1, 1);
                tempMateral.mainTextureOffset = Vector2.zero;
#else
                Material tempMateral = null;
#endif
            //string path_atlas = Application.streamingAssetsPath + "/Datas/Unit/" + prefabID + "/" + skinID + ".atlas.txt";
            //string path_skel = Application.streamingAssetsPath + "/Datas/Unit/" + prefabID + "/" + prefabID + ".skel.bytes";
            TextAsset atlas;
                //if (useAB)
                //{
                    atlas = ABExTool.GetAssetBundleByName<TextAsset>(ab_atlas, (skinName ?? $"{(prefabOverride != -1 ? prefabOverride : skinID)}") + ".atlas.txt");
                //}
                //else
                //    atlas = new TextAsset(File.ReadAllText(path_atlas));
                var atlasAsset = AtlasAsset.CreateRuntimeInstance(atlas, new Material[1] { tempMateral }, true);
                
                //if (useAB)
                //{
                //}
                //else
                //    bytes_0 = File.ReadAllBytes(path_skel);
                //skel = new TextAsset(Convert.ToBase64String(bytes_0));
                string skelName = skinID + ".skel.bytes";
                //if (atlas == null)
                //{
                //    MainManager.Instance.WindowConfigMessage("角色" + skinID + "的骨骼动画丢失！\n 找不到" + ab_atlas, null);
                //}
                var skeletonData = p0 != null
                    ? SkeletonDataAsset.CreateRuntimeInstance(ABExTool.GetAssetBundleByName<TextAsset>(p0, ".skel").bytes, skelName, atlasAsset, true, scale)
                    : SkeletonDataAsset.CreateRuntimeInstance(MainTransClass.GetUnitSkelBytes(skinID - skinID / 10 % 10 * 10, atlasAsset), skelName, atlasAsset, true, scale);
                //var sa = SkeletonAnimation.NewSkeletonAnimationGameObject(skeletonData); // Spawn a new SkeletonAnimation GameObject.
#if !UNITY_5_3_OR_NEWER
                _skeletonCache[cacheKey] = skeletonData;
#endif
                return skeletonData;
            }
#if UNITY_EDITOR
            catch (NotImplementedException e)
#else
            catch (Exception e)
#endif
            {
                MainManager.Instance.WindowConfigMessage("合成角色" + skinID + "的动画时发生错误：" + e.Message + "\n可能原因：找不到" + ab_atlas + "/" + p, null);

            }
            return null;
        }
    }
}
