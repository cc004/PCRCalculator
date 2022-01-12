using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Newtonsoft0.Json;

namespace PCRCaculator.Battle
{
    [System.Serializable]
    public class ActionParameterOnPrefabDetail
    {
        //public Data data;
        public bool Visible;
        public List<ActionExecTime> execTime;
        public List<ActionExecTime> ExecTimeForPrefab;//ExecTimeForPrefab
        public List<ActionExecTimeCombo> ExecTimeCombo = new List<ActionExecTimeCombo>();
        public int ActionId;
        //public List<NormalSkillEffect> ActionEffectList;

        public List<ActionExecTime> ExecTime { get => execTime; set => execTime = value; }
        /*public sealed class Data
        {
            public bool Visible;
            public List<ActionExecTime> execTime;
            public List<ActionExecTime> ExecTimeForPrefab;//ExecTimeForPrefab
            public List<ActionExecTimeCombo> ExecTimeCombo;
            public int ActionId;
            //public List<NormalSkillEffect> ActionEffectList;

        }*/
    }
    [System.Serializable]
    public class ActionExecTimeCombo
    {
        public float StartTime;
        public float OffsetTime;
        public float Weight;
        public int Count;
        public eComboInterporationType InterporationType;
        public object Curve;
    }
    public abstract class Attachment
    {
        private string name;

        public Attachment(string name)
        {
            this.Name = name;
        }

        public string Name { get => name; set => name = value; }

        public override string ToString() => Name;

        
    }
    [System.Serializable]
    public class ActionExecTime
    {
        public float Time;
        public eDamageEffectType DamageNumType;
        public float Weight;
        public float DamageNumScale;

        public ActionExecTime() { }
        public ActionExecTime(float time,float weight)
        {
            Time = time;
            Weight = weight;
        }
    }
    public static class StaticAilmentData
    {
        public static readonly AilmentData[] ailmentDatas;
        static StaticAilmentData()
        {
            ailmentDatas = new AilmentData[32]{
                new AilmentData(1,8,1,"减速"),
                new AilmentData(2,8,2,"加速"),
                new AilmentData(3,8,3,"麻痹"),
                new AilmentData(4,8,4,"冻结"),
                new AilmentData(5,8,5,"束缚"),
                new AilmentData(6,8,6,"睡眠"),
                new AilmentData(7,8,7,"眩晕"),
                new AilmentData(8,8,8,"石化"),
                new AilmentData(9,8,9,"拘留"),
                new AilmentData(10,9,0,"拘留（造成伤害）"),
                new AilmentData(11,9,1,"毒"),
                new AilmentData(12,9,2,"烧伤"),
                new AilmentData(13,9,3,"诅咒"),
                new AilmentData(14,11,0,"魅惑"),
                new AilmentData(15,12,-1,"黑暗"),
                new AilmentData(16,13,0,"沉默"),
                new AilmentData(17,30,0,"即死"),
                new AilmentData(18,3,-1,"击退"),
                new AilmentData(19,11,1,"混乱"),
                new AilmentData(20,9,4,"猛毒"),
                new AilmentData(21,56,-1,"千里眼"),
                new AilmentData(22,59,-1,"回复伤害"),
                new AilmentData(23,61,-1,"恐慌"),
                new AilmentData(24,60,-1,"刻印"),
                new AilmentData(25,62,-1,"害怕"),
                new AilmentData(26,69,-1,"トナカイ化"),
                new AilmentData(27,8,10,"气绝"),
                new AilmentData(28,9,5,"诅咒"),
                new AilmentData(29,70,-1,"HP变化"),
                new AilmentData(30,8,11,"时停"),
                new AilmentData(31,76,-1,"HP回复减少"),
                new AilmentData(32,78,-1,"弱体伤害上升"),

            };
        }
    }
    public class AilmentData
    {
        public readonly int ailment_id;
        public readonly int ailment_action;
        public readonly int ailment_detail1;
        public readonly string ailment_name;

        public AilmentData() { }
        public AilmentData(int ailment_id, int ailment_action, int ailment_detail1,string name)
        {
            this.ailment_id = ailment_id;
            this.ailment_action = ailment_action;
            this.ailment_detail1 = ailment_detail1;
            ailment_name = name;
        }
    }
    public class ResistData
    {
        public readonly int resist_ststus_id;
        public readonly int[] ailments;
        static Dictionary<int, int[]> resistDataDic;
        static ResistData()
        {
            string jsonStr = MainManager.Instance.LoadJsonDatas("Datas/ResistDataDic");
            resistDataDic = JsonConvert.DeserializeObject<Dictionary<int, int[]>>(jsonStr);
        }
        public ResistData()
        {
            resist_ststus_id = 0;
            ailments = new int[32]
            {
                0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                0,0,0,0,0,0,0,0,0,0,0,0
            };

        }
        public ResistData(int id,int[] ails)
        {
            resist_ststus_id = id;
            ailments = ails;
        }
        public ResistData(int cent)
        {
            resist_ststus_id = cent;
            if(!resistDataDic.TryGetValue(cent,out ailments))
            {
                ailments = new int[32];
            }
            /*switch (cent)
            {
                case 300000001:
                    ailments = new int[20]{
                100,0,100,100,100,
                100,100,100,100,100,
                100,100,100,100,100,
                100,100,0,100,100};
                    break;
                case 300000002:
                    ailments = new int[20]{
                100,0,30,30,30,
                30,30,30,30,30,
                100,100,100,100,100,
                100,100,0,100,100};
                    break;
                case 300000003:
                    ailments = new int[20]{
                100,0,100,100,100,
                100,100,100,30,30,
                30,30,30,100,100,
                100,100,0,100,30};
                    break;
                case 300000004:
                    ailments = new int[20]{
                100,0,30,30,30,
                30,30,30,30,30,
                30,30,30,30,100,
                100,100,0,30,30};
                    break;
                case 300000005:
                    ailments = new int[20]{
                30,0,100,100,100,
                100,100,100,100,100,
                100,100,100,100,30,
                30,100,0,100,100};
                    break;
                case 300000006:
                    ailments = new int[20]{
                100,0,100,100,100,
                100,100,100,100,100,
                100,100,100,100,30,
                100,100,0,100,100};
                    break;
                case 300000007:
                    ailments = new int[20]{
                100,0,100,100,100,
                100,100,100,100,100,
                100,100,100,100,30,
                100,100,100,100,100};
                    break;
                case 300000008:
                    ailments = new int[20]{
                100,0,100,100,100,
                100,100,100,30,30,
                30,30,30,100,30,
                100,100,100,100,30};
                    break;
                case 300000009:
                    ailments = new int[20]{
                100,0,100,100,100,
                100,100,100,100,100,
                100,100,100,100,100,
                100,100,100,100,100};
                    break;
                case 300000010:
                    ailments = new int[20]{
                0,0,0,0,0,
                0,0,0,30,30,
                0,0,0,100,100,
                100,100,0,100,0};
                    break;



                default:
                    ailments = new int[20]{
                cent,cent,cent,cent,cent,
                cent,cent,cent,cent,cent,
                cent,cent,cent,cent,cent,
                cent,cent,cent,cent,cent};
                    break;

            }*/
        }

    }
    
}
