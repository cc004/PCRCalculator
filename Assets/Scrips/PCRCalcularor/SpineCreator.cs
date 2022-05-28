using System;
using System.IO;
using PCR_cysp2skel;
using Spine.Unity;
using UnityEngine;

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

        public SkeletonDataAsset Createskeletondata(int skinID, float scale = 0.5f, bool useAB = false, string p0 = null)
        {
            //int skinID = prefabID >= 200000 ? prefabID : prefabID + 30;
            Texture2D texture = new Texture2D(0, 0);
            var p = p0 ?? "spine_sdnormal_" + skinID + ".unity3d";
            string ab_atlas = p;
            
            try
            {
                //if (useAB)
                //{
                    texture = ABExTool.GetAssetBundleByName<Texture2D>(p, "png");
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
                tempMateral.mainTexture.name = "force_override";//skinID.ToString();
                tempMateral.mainTextureScale = new Vector2(1, 1);
                tempMateral.mainTextureOffset = Vector2.zero;
                //string path_atlas = Application.streamingAssetsPath + "/Datas/Unit/" + prefabID + "/" + skinID + ".atlas.txt";
                //string path_skel = Application.streamingAssetsPath + "/Datas/Unit/" + prefabID + "/" + prefabID + ".skel.bytes";
                TextAsset atlas;
                //if (useAB)
                //{
                    atlas = ABExTool.GetAssetBundleByName<TextAsset>(ab_atlas, "txt");
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
                return skeletonData;
            }
            catch (Exception e)
            {
                MainManager.Instance.WindowConfigMessage(
    "合成角色" + skinID + "的动画时发生错误：" + e.Message + "\n可能原因：找不到" + ab_atlas + "/" + p, null);

            }
            return null;
        }
    }
}
