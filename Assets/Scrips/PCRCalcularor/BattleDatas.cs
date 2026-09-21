using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace PCRCaculator.Battle
{
    [Serializable]
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
    [Serializable]
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
            Name = name;
        }

        public string Name { get => name; set => name = value; }

        public override string ToString() => Name;

        
    }
    [Serializable]
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
        public static readonly Dictionary<int, ResistVariationData> resistVariationDataDic =
            new Dictionary<int, ResistVariationData>();
    }
    public class ResistData
    {
        public readonly int resist_ststus_id;
        public readonly int[] ailments;
        static Dictionary<int, int[]> resistDataDic => MainManager.Instance.ResistDataDic;
        static ResistData()
        {
            // string jsonStr = MainManager.Instance.LoadJsonDatas("Datas/ResistDataDic");
            // resistDataDic = JsonConvert.DeserializeObject<Dictionary<int, int[]>>(jsonStr);
        }
        public ResistData()
        {
            resist_ststus_id = 0;
            ailments = Array.Empty<int>();
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
                ailments = Array.Empty<int>();
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
    public class ResistVariationData
    {

        public int resist_variation_id;// => _resist_variation_id;

        public int value_1;// => _value_1;

        public int value_2;// => _value_2;

        public int value_3;// => _value_3;

        public int value_4;// => _value_4;

        public int[] DebuffDecrement
        {
            get;
            set;
        }

        public ResistVariationData(int resist_variation_id = 0, int value_1 = 0, int value_2 = 0, int value_3 = 0, int value_4 = 0)
        {
            this.resist_variation_id = resist_variation_id;
            this.value_1 = value_1;
            this.value_2 = value_2;
            this.value_3 = value_3;
            this.value_4 = value_4;
            SetUp();
        }

        public void SetUp()
        {
            DebuffDecrement = new int[4];
            DebuffDecrement[0] = value_1;
            DebuffDecrement[1] = value_2;
            DebuffDecrement[2] = value_3;
            DebuffDecrement[3] = value_4;
        }
    }
    
}
