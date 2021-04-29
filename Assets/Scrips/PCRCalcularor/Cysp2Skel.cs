using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

namespace PCR_cysp2skel
{
    public class CurrentClassAnimData
    {
        public int type = 0;
        public int count = 0;
        public byte[] data = new byte[] { };
    }
    public class CurrentCharAnimData
    {
        public int id = 0;
        public int count = 0;
        public byte[] data = new byte[] { };
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
        public static byte[] GetUnitSkelBytes(int unitId,bool saveFile = false,string savePath = "")
        {
            int unitType = 0;
            bool isEnemy = unitId >= 199999;
            if (!isEnemy)
                unitType = PCRCaculator.MainManager.Instance.UnitRarityDic[unitId].detailData.motionType;
            if (unitType == 0)
                unitType = unitId;
            string char_base = isEnemy ? "spine_" + unitId + "_chara_base.cysp.unity3d" : "spine_000000_chara_base.cysp.unity3d";
            string type = unitType < 10 ? "0" + unitType : "" + unitType;
            string common_battle = "spine_" + type + "_common_battle.cysp.unity3d";
            string spine_battle = "spine_" + unitId + "_battle.cysp.unity3d";
            try
            {
                byte[] CHAR_BASE = PCRCaculator.ABExTool.GetAssetBundleByName<TextAsset>(char_base).bytes;
                byte[] BATTLE = PCRCaculator.ABExTool.GetAssetBundleByName<TextAsset>(spine_battle).bytes;
                byte[] COMMOM_BATTLE =  (isEnemy && unitId != 407001) ? new byte[] { } : PCRCaculator.ABExTool.GetAssetBundleByName<TextAsset>(common_battle).bytes;
                var result = Cysp2Skel(unitId, CHAR_BASE, BATTLE, COMMOM_BATTLE, saveFile, savePath);
                return result;
            }
            catch(System.Exception e)
            {
                PCRCaculator.MainManager.Instance.WindowConfigMessage(
                    "合成角色" + unitId + "的动画时发生错误：" + e.Message + "\n可能原因：找不到" + char_base + "/" + spine_battle, null);
            }
            return null;
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
        public static byte[] sliceCyspAnimation(byte[] cysp, out Int32 count)
        {
            count = BitConverter.ToInt32(cysp, 12);
            int length = (count + 1) * 32;
            byte[] buf = new byte[] { };
            Array.Resize<byte>(ref buf, cysp.Length - length);
            /*for (int i = 0; i < (count + 1) * 32; i++)
            {
                buf[i] = cysp[i];
            }*/
            for (int i = 0; i < cysp.Length - length; i++)
            {
                buf[i] = cysp[length + i];
            }
            return buf;
        }
        public static byte[] Cysp2Skel(int baseUnitId,byte[] CHAR_BASE,byte[] BATTLE,byte[] COMMON_BATTLE,bool saveFile,string filePath)
        {
            byte[] generalBattleSkeletonData;
            List<byte[]> generalAdditionAnimations = new List<byte[]>();
            List<int> generalCounts = new List<int>();
            CurrentClassAnimData currentClassAnimData = new CurrentClassAnimData();
            CurrentCharAnimData currentCharaAnimData;
            //string commonId;
            if (baseUnitId >= 200000 && baseUnitId!=407001)
            {
                //string type = baseUnitId >= 400000 ? "summon" : "enemy";
                generalBattleSkeletonData = CHAR_BASE;//loadData(type + "\\" + baseUnitId + "_CHARA_BASE.cysp.bytes");
                //Console.WriteLine("成功读取基础骨骼 " + baseUnitId + "_CHARA_BASE.cysp,共" + generalBattleSkeletonData.Length + "个字节。");
                /*for (int i = 0; i < additionAnimations.Length; i++)
                {
                    byte[] data = loadData("common\\" + baseId + "_" + additionAnimations[i] + ".cysp");
                    Console.WriteLine("成功读取附加动画 " + baseId + "_" + additionAnimations[i] + ".cysp,共" + data.Length + "个字节。");
                    Int32 k = 0;
                    generalAdditionAnimations.Add(sliceCyspAnimation(data, out k));
                    generalCounts.Add(k);
                }
                currentClassAnimData = new CurrentClassAnimData();
                //currentClassAnimData.type = currentClass;
                currentClassAnimData.data = sliceCyspAnimation(loadData("common\\" + commonId + "_COMMON_BATTLE.cysp"), out currentClassAnimData.count);
                Console.WriteLine("成功读取共通动画" + commonId + "_COMMON_BATTLE.cysp,共" + currentClassAnimData.data.Length + "个字节。");*/
                currentCharaAnimData = new CurrentCharAnimData();
                //currentCharaAnimData.id = baseUnitId;
                //currentCharaAnimData.data = sliceCyspAnimation(loadData(type + "\\" + baseUnitId + "_BATTLE.cysp.bytes"), out currentCharaAnimData.count);
                currentCharaAnimData.data = sliceCyspAnimation(BATTLE, out currentCharaAnimData.count);
                //Console.WriteLine("成功读取角色动画" + baseUnitId + "_BATTLE.cysp，共" + currentCharaAnimData.data.Length + "个字节。");

            }
            else
            {
                //string baseId = "000000";
                //if (unitData.ContainsKey(baseUnitId))
                //{
                //    if (unitType == 0)
                //    {
                //        commonId = "" + baseUnitId;
                //       baseId = "" + baseUnitId;
                //    }
                //    else if (unitType < 10)
                //    {
                //        commonId = "0" + unitType;
                //    }
                //    else
                //    {
                //        commonId = unitType.ToString();
                //    }
                //}
                //else
                //{
                //    if (baseUnitId == 170101)
                //       commonId = "04";
                //    else if (baseUnitId == 170201)
                //        commonId = "06";
                //    else
                //        commonId = "01";
                //}
                //string baseUnitId = "102901";
                generalBattleSkeletonData = CHAR_BASE;//loadData("common\\" + baseId + "_CHARA_BASE.cysp");
                //Console.WriteLine("成功读取基础骨骼 " + baseId + "_CHARA_BASE.cysp,共" + generalBattleSkeletonData.Length + "个字节。");
                /*for (int i = 0; i < additionAnimations.Length; i++)
                {
                    byte[] data = loadData("common\\" + baseId + "_" + additionAnimations[i] + ".cysp");
                    //Console.WriteLine("成功读取附加动画 " + baseId + "_" + additionAnimations[i] + ".cysp,共" + data.Length + "个字节。");
                    Int32 k = 0;
                    generalAdditionAnimations.Add(sliceCyspAnimation(data, out k));
                    generalCounts.Add(k);
                }*/
                currentClassAnimData = new CurrentClassAnimData();
                //currentClassAnimData.type = currentClass;
                //currentClassAnimData.data = sliceCyspAnimation(loadData("common\\" + commonId + "_COMMON_BATTLE.cysp"), out currentClassAnimData.count);
                currentClassAnimData.data = sliceCyspAnimation(COMMON_BATTLE, out currentClassAnimData.count);
                //Console.WriteLine("成功读取共通动画" + commonId + "_COMMON_BATTLE.cysp,共" + currentClassAnimData.data.Length + "个字节。");
                currentCharaAnimData = new CurrentCharAnimData();
                //currentCharaAnimData.id = baseUnitId;
                //currentCharaAnimData.data = sliceCyspAnimation(loadData("unit\\" + baseUnitId + "_BATTLE.cysp.bytes"), out currentCharaAnimData.count);
                currentCharaAnimData.data = sliceCyspAnimation(BATTLE, out currentCharaAnimData.count);
                //Console.WriteLine("成功读取角色动画" + baseUnitId + "_BATTLE.cysp，共" + currentCharaAnimData.data.Length + "个字节。");
            }
            var animationCount = 0;
            var classAnimCount = currentClassAnimData.count;
            animationCount += classAnimCount;
            var unitAnimCount = currentCharaAnimData.count;
            animationCount += unitAnimCount;
            /*for (int i = 0; i < additionAnimations.Length; i++)
            {
                if (i < generalCounts.Count)
                    animationCount += generalCounts[i];
            }*/

            var newBuffSize = generalBattleSkeletonData.Length - 64 + 1 + currentClassAnimData.data.Length + currentCharaAnimData.data.Length;
            /*for (int i = 0; i < additionAnimations.Length; i++)
            {
                if (i < generalAdditionAnimations.Count)
                    newBuffSize += generalAdditionAnimations[i].Length;
            }*/
            var newBuff = new byte[] { };
            Array.Resize<byte>(ref newBuff, newBuffSize);
            var pos = generalBattleSkeletonData.Length;
            var offset = 0;
            //Buffer.BlockCopy(generalBattleSkeletonData, 0, newBuff, offset, pos);
            for (int i = 64; i < pos; i++)
            {
                newBuff[i - 64] = generalBattleSkeletonData[i];
            }
            //Console.WriteLine("成功写入基础骨骼，起始位置：" + offset);
            offset += pos - 64;
            newBuff[offset] = (byte)animationCount;//默认动画不超过128，不然溢出了
            //Console.WriteLine("已加载" + animationCount + "个动画,在" + offset + "处写入数据");
            offset++;
            pos = currentClassAnimData.data.Length;
            for (int i = 0; i < pos; i++)
            {
                newBuff[offset + i] = currentClassAnimData.data[i];
            }
            //Console.WriteLine("成功写入战斗动画，起始位置：" + offset);

            offset += pos;
            pos = currentCharaAnimData.data.Length;
            for (int i = 0; i < pos; i++)
            {
                newBuff[offset + i] = currentCharaAnimData.data[i];
            }
            //Console.WriteLine("成功写入角色动画，起始位置：" + offset);

            offset += pos;
            foreach (byte[] data0 in generalAdditionAnimations)
            {
                pos = data0.Length;
                for (int i = 0; i < pos; i++)
                {
                    newBuff[offset + i] = data0[i];
                }
                //Console.WriteLine("成功写入附加动画，起始位置：" + offset);
                offset += pos;
            }
            //Console.WriteLine("动画写入完成！");
            //Check(newBuff);
            //这里改成你自己的要生成的目录！
            //string filePath = @"D:\\PCRCalculator\\out\\" + baseUnitId;// + ".skel.bytes";
            //if (!Directory.Exists(filePath))  //不存在文件夹，创建
            //{
            //    Directory.CreateDirectory(filePath);  //创建新的文件夹
            //}
            //CopyFiles(baseUnitId);
            if (saveFile)
            {
                FileStream fileStream;
                if (File.Exists(filePath))
                {
                    fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Write);
                }
                else
                {
                    fileStream = new FileStream(filePath, FileMode.Create);
                }
                //FileStream fileStream = new FileStream(@"D:\\PCRCalculator\\test.skel.bytes", FileMode.Create);            
                BinaryWriter binaryWriter = new BinaryWriter(fileStream);
                binaryWriter.Write(newBuff);
                binaryWriter.Flush();
                binaryWriter.Close();
                fileStream.Close();
            }
            //Console.WriteLine("保存到" + filePath);
            //Console.ReadKey();
            //downloadBlob(newBuff.buffer, 'some-file.skel', 'application/octet-stream');
            return newBuff;
        }
        /*public void CopyFiles(int baseUnitId)
        {
            string pLocalFilePath = "D:\\PCRCalculator\\AnimPlayer-master\\AnimPlayer-master\\unit\\" + (baseUnitId + 30) + ".atlas.txt";//要复制的文件路径
            string pSaveFilePath = "D:\\PCRCalculator\\out\\" + baseUnitId + "\\" + (baseUnitId + 30) + ".atlas.txt";//指定存储的路径

            if (baseUnitId > 300000)
            {
                pLocalFilePath = "D:\\PCRCalculator\\AnimPlayer-master\\AnimPlayer-master\\enemy\\" + (baseUnitId) + ".atlas.txt";
                pSaveFilePath = "D:\\PCRCalculator\\out\\" + baseUnitId + "\\" + (baseUnitId) + ".atlas.txt";//指定存储的路径
            }
            if (File.Exists(pLocalFilePath))//必须判断要复制的文件是否存在
            {
                File.Copy(pLocalFilePath, pSaveFilePath, true);//三个参数分别是源文件路径，存储路径，若存储路径有相同文件是否替换
            }
            else
            {
                Console.WriteLine("目标文件不存在！");
            }
            string pLocalFilePath2 = "D:\\PCRCalculator\\AnimPlayer-master\\AnimPlayer-master\\unit\\" + (baseUnitId + 30) + ".png";//要复制的文件路径
            string pSaveFilePath2 = "D:\\PCRCalculator\\out\\" + baseUnitId + "\\" + (baseUnitId + 30) + ".png";//指定存储的路径

            if (baseUnitId > 300000)
            {
                pLocalFilePath2 = "D:\\PCRCalculator\\AnimPlayer-master\\AnimPlayer-master\\enemy\\" + (baseUnitId) + ".png";
                pSaveFilePath2 = "D:\\PCRCalculator\\out\\" + baseUnitId + "\\" + (baseUnitId) + ".png";
            }
            if (File.Exists(pLocalFilePath2))//必须判断要复制的文件是否存在
            {
                File.Copy(pLocalFilePath2, pSaveFilePath2, true);//三个参数分别是源文件路径，存储路径，若存储路径有相同文件是否替换
            }
            else
            {
                Console.WriteLine("目标文件不存在！");
            }
        }*/
        class Filed : ApplicationException
        {
            //public MyException(){}
            public Filed(string message) : base(message) { }

            public override string Message
            {
                get
                {
                    return base.Message;
                }
            }
        }


    }

    public static class Cysp2Skel
    {

    }
}