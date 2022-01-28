using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;
using System.IO;
using System;

namespace PCRCaculator
{
    public class SpineCreator : MonoBehaviour
    {
        public static SpineCreator Instance;
        public Material spineMaterial;
        //public Texture2D texture;
        private void Awake()
        {
            Instance = this;
        }

        public SkeletonDataAsset Createskeletondata(int prefabID, float scale = 0.5f, bool useAB = false, string p0 = null)
        {
            int skinID = prefabID >= 200000 ? prefabID : prefabID + 30;
            if (prefabID == 407001)
                skinID = 107031;
            Texture2D texture = new Texture2D(0, 0);
            var p = p0 ?? "spine_sdnormal_" + skinID + ".unity3d";
            string ab_atlas = p;

            try
            {
                if (useAB)
                {
                    texture = ABExTool.GetAssetBundleByName<Texture2D>(p, "png");
                }
                else
                {
                    string path = Application.streamingAssetsPath + "/Datas/Unit/" + prefabID + "/" + skinID + ".png";//\Datas\Unit\112001
                    FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
                    byte[] bytes = new byte[fs.Length];
                    fs.Read(bytes, 0, (int)fs.Length);
                    texture.LoadImage(bytes);

                }
                Material tempMateral = new Material(this.spineMaterial);
                tempMateral.mainTexture = texture;
                tempMateral.mainTexture.name = skinID.ToString();
                tempMateral.mainTextureScale = new Vector2(1, 1);
                tempMateral.mainTextureOffset = Vector2.zero;
                string path_atlas = Application.streamingAssetsPath + "/Datas/Unit/" + prefabID + "/" + skinID + ".atlas.txt";
                string path_skel = Application.streamingAssetsPath + "/Datas/Unit/" + prefabID + "/" + prefabID + ".skel.bytes";
                TextAsset atlas;
                if (useAB)
                {
                    atlas = ABExTool.GetAssetBundleByName<TextAsset>(ab_atlas, "txt");
                }
                else
                    atlas = new TextAsset(File.ReadAllText(path_atlas));
                var atlasAsset = AtlasAsset.CreateRuntimeInstance(atlas, new Material[1] { tempMateral }, true);

                byte[] bytes_0;
                if (useAB)
                {
                    if (p0 != null)
                        bytes_0 = ABExTool.GetAssetBundleByName<TextAsset>(p0, ".skel").bytes;
                    else
                        bytes_0 = PCR_cysp2skel.MainTransClass.GetUnitSkelBytes(prefabID);
                }
                else
                    bytes_0 = File.ReadAllBytes(path_skel);
                //skel = new TextAsset(Convert.ToBase64String(bytes_0));
                string skelName = prefabID + ".skel.bytes";
                if (atlas == null)
                {
                    MainManager.Instance.WindowConfigMessage("角色" + prefabID + "的骨骼动画丢失！\n 找不到" + ab_atlas, null);
                }
                var skeletonData = SkeletonDataAsset.CreateRuntimeInstance(bytes_0, skelName, atlasAsset, true, scale);
                //var sa = SkeletonAnimation.NewSkeletonAnimationGameObject(skeletonData); // Spawn a new SkeletonAnimation GameObject.
                return skeletonData;
            }
            catch (System.Exception e)
            {
                PCRCaculator.MainManager.Instance.WindowConfigMessage(
    "合成角色" + prefabID + "的动画时发生错误：" + e.Message + "\n可能原因：找不到" + ab_atlas + "/" + p, null);

            }
            return null;
        }
    }
}
