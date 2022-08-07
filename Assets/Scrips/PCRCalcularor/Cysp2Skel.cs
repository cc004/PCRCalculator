using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using Elements;
using PCRApi;
using PCRCaculator;
using Spine;
using Spine.Unity;
using UnityEngine;

namespace PCR_cysp2skel
{
    public class CurrentClassAnimData
    {
        public int type = 0;
        public int count;
        public byte[] data = { };
    }
    public class CurrentCharAnimData
    {
        public int id = 0;
        public int count;
        public byte[] data = { };
    }
    public class LoadingSkeleton
    {
        public int id = 0;
        public int baseId = 0;
    }
    /*public class UnitData
    {
        public string name = "未定义";
        public int type = 0;
        public bool hasRarity6 = false;
    }*/
    public class MainTransClass
    {
        //string basepath = "D:\\PCRCalculator\\AnimPlayer-master\\AnimPlayer-master\\";

        //Dictionary<int, UnitData> unitData = new Dictionary<int, UnitData>();
        //bool isEnemy = true;
        //int currentClass = '1';
        //static string[] additionAnimations = new string[6] { "DEAR", "NO_WEAPON", "POSING", "RACE", "RUN_JUMP", "SMILE" };
        //int[] enemyIDs = new int[] { 302600, 302601 };
        //int[] summonIDs = new int[] { 403101, 404201, 407701, 408401, 408402, 408403 };

        public static SkeletonData GetUnitSkelBytes(int unitId, AtlasAsset atlas)
        {
            var unitType = MainManager.Instance.UnitRarityDic.TryGetValue(unitId, out var d) ? d.detailData.motionType : 0;
            string char_base = $"spine_{(unitType == 0 ? unitId : 0):D6}_chara_base.cysp.unity3d";
            string common_battle = $"spine_{(unitType == 0 ? unitId: unitType):D2}_common_battle.cysp.unity3d";
            string spine_battle = $"spine_{unitId:D6}_battle.cysp.unity3d";
            try
            {
                var CHAR_BASE = ABExTool.GetAssetBundleByName<TextAsset>(char_base);
                var BATTLE = ABExTool.GetAssetBundleByName<TextAsset>(spine_battle);
                var COMMON_BATTLE =  ABExTool.GetAssetBundleByName<TextAsset>(common_battle);

                if (CHAR_BASE == null)
                {
                    throw new Exception("找不到" + char_base);
                }

                if (BATTLE == null)
                {
                    throw new Exception("找不到" + spine_battle);
                }

                var binary = new SkeletonBinary(new AtlasAttachmentLoader(atlas.GetAtlas()));
                var data = binary.ReadSkeletonDevisionCyspSkeleton(new SimpleMemoryStream(CHAR_BASE.bytes));
                binary.ReadSkeletonDevisionCyspAnimation(new SimpleMemoryStream(BATTLE.bytes), data);
                if (COMMON_BATTLE != null)
                    binary.ReadSkeletonDevisionCyspAnimation(new SimpleMemoryStream(COMMON_BATTLE.bytes), data);
                return data;
            }
            catch(Exception e)
            {
                    MainManager.Instance.WindowConfigMessage(
                        "合成角色" + unitId + "的动画时发生错误:" + e.Message, null);
                    return null;
            }
        }
        /*public void TranslateEnimy(int unitid)
        {
            try
            {
                Cysp2Skel(unitid);

            }
            catch (Filed filed)
            {
                Console.WriteLine("id为" + unitid + "的角色动画拼接失败，原因为：" + filed.Message);

            }
            Console.WriteLine("全部完成！");
            Console.ReadKey();

        }*/
        /*public byte[] loadData(string fullpath)
        {
            BinaryReader br;
            try
            {
                FileStream file = new FileStream(basepath + fullpath, FileMode.Open);
                br = new BinaryReader(file);
                byte[] data = br.ReadBytes((int)file.Length);
                br.Close();
                return data;//文件应该不会太大……吧
            }
            catch (IOException e)
            {
                //Console.WriteLine(e.Message + "\n Cannot open file!");
                throw new Filed(e.Message);
            }
        }*/
    }
}