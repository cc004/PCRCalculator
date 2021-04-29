// Decompiled with JetBrains decompiler
// Type: Elements.MasterSkillData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using CodeStage.AntiCheat.ObscuredTypes;
//using LitJson;
//using Sqlite3Plugin;
using System.Collections.Generic;

namespace Elements
{
    public class MasterSkillData : AbstractMasterData
    {
        public const string TABLE_NAME = "skill_data";
        //private MasterSkillDatabase _db;
        private Dictionary<int, MasterSkillData.SkillData> _lazyPrimaryKeyDictionary = new Dictionary<int, SkillData>();

        public MasterSkillData.SkillData this[int id] => this.Get(id);

        /**public MasterSkillData(MasterSkillDatabase db)
          : base((AbstractMasterDatabase) db)
        {
          this._lazyPrimaryKeyDictionary = new Dictionary<int, MasterSkillData.SkillData>();
          this._db = db;
        }*/

        public MasterSkillData.SkillData Get(int skill_id)
        {
            int key = skill_id;
            MasterSkillData.SkillData skillData = (MasterSkillData.SkillData)null;
            if (!this._lazyPrimaryKeyDictionary.TryGetValue(key, out skillData))
            {
                //skillData = this._db != null ? this._SelectOne(skill_id) : (MasterSkillData.SkillData) null;
                //this._lazyPrimaryKeyDictionary[key] = skillData;
            }
            return skillData;
        }

        public bool HasKey(int skill_id) => this.Get(skill_id) != null;

        /*private MasterSkillData.SkillData _SelectOne(int skill_id)
        {
            NAOCHNBMGCB selectQuerySkillData = this._db.GetSelectQuery_SkillData();
            if (selectQuerySkillData == null)
                return (MasterSkillData.SkillData)null;
            if (!selectQuerySkillData.BindInt(1, skill_id))
                return (MasterSkillData.SkillData)null;
            MasterSkillData.SkillData skillData = (MasterSkillData.SkillData)null;
            if (selectQuerySkillData.Step())
                skillData = this._CreateCachedOrmByQueryResult((ODBKLOJPCHG)selectQuerySkillData);
            selectQuerySkillData.Reset();
            return skillData;
        }*/

        /*private MasterSkillData.SkillData _CreateCachedOrmByQueryResult(ODBKLOJPCHG query)
        {
            MasterSkillData.SkillData skillData = (MasterSkillData.SkillData)null;
            int skill_id = query.GetInt(0);
            int key = skill_id;
            if (!this._lazyPrimaryKeyDictionary.TryGetValue(key, out skillData))
            {
                bool nameIsNull = query.IsNull(1);
                string text1 = query.GetText(1);
                int skill_type = query.GetInt(2);
                int skill_area_width = query.GetInt(3);
                double skill_cast_time = query.GetDouble(4);
                int action_1 = query.GetInt(5);
                int action_2 = query.GetInt(6);
                int action_3 = query.GetInt(7);
                int action_4 = query.GetInt(8);
                int action_5 = query.GetInt(9);
                int action_6 = query.GetInt(10);
                int action_7 = query.GetInt(11);
                int depend_action_1 = query.GetInt(12);
                int depend_action_2 = query.GetInt(13);
                int depend_action_3 = query.GetInt(14);
                int depend_action_4 = query.GetInt(15);
                int depend_action_5 = query.GetInt(16);
                int depend_action_6 = query.GetInt(17);
                int depend_action_7 = query.GetInt(18);
                string text2 = query.GetText(19);
                int icon_type = query.GetInt(20);
                skillData = new MasterSkillData.SkillData(skill_id, nameIsNull, text1, skill_type, skill_area_width, skill_cast_time, action_1, action_2, action_3, action_4, action_5, action_6, action_7, depend_action_1, depend_action_2, depend_action_3, depend_action_4, depend_action_5, depend_action_6, depend_action_7, text2, icon_type);
                this._lazyPrimaryKeyDictionary.Add(key, skillData);
            }
            return skillData;
        }*/

        //public override void Unload() => this._lazyPrimaryKeyDictionary = (Dictionary<int, MasterSkillData.SkillData>) null;

        public class SkillData
        {
            public List<MasterSkillData.MainSkillRateData> SkillValue = new List<MasterSkillData.MainSkillRateData>();
            //private MasterDataManager masterDataMng = ManagerSingleton<MasterDataManager>.Instance;
            protected ObscuredInt _skill_id;
            protected ObscuredString _name;
            protected ObscuredInt _skill_type;
            protected ObscuredInt _skill_area_width;
            protected ObscuredDouble _skill_cast_time;
            protected ObscuredInt _action_1;
            protected ObscuredInt _action_2;
            protected ObscuredInt _action_3;
            protected ObscuredInt _action_4;
            protected ObscuredInt _action_5;
            protected ObscuredInt _action_6;
            protected ObscuredInt _action_7;
            protected ObscuredInt _depend_action_1;
            protected ObscuredInt _depend_action_2;
            protected ObscuredInt _depend_action_3;
            protected ObscuredInt _depend_action_4;
            protected ObscuredInt _depend_action_5;
            protected ObscuredInt _depend_action_6;
            protected ObscuredInt _depend_action_7;
            protected ObscuredString _description;
            protected ObscuredInt _icon_type;

            public int SkillId => (int)this.skill_id;

            public string Name => (string)this.name;

            public List<int> ActionIds { get; private set; }

            public List<int> DependedIds { get; private set; }

            public List<MasterSkillAction.SkillAction> ActionDataList { get; set; }

            public void SetUp()
            {
                this.ActionIds = new List<int>();
                this.ActionIds.Add((int)this.action_1);
                this.ActionIds.Add((int)this.action_2);
                this.ActionIds.Add((int)this.action_3);
                this.ActionIds.Add((int)this.action_4);
                this.ActionIds.Add((int)this.action_5);
                this.ActionIds.Add((int)this.action_6);
                this.ActionIds.Add((int)this.action_7);
                this.DependedIds = new List<int>();
                this.DependedIds.Add((int)this.depend_action_1);
                this.DependedIds.Add((int)this.depend_action_2);
                this.DependedIds.Add((int)this.depend_action_3);
                this.DependedIds.Add((int)this.depend_action_4);
                this.DependedIds.Add((int)this.depend_action_5);
                this.DependedIds.Add((int)this.depend_action_6);
                this.DependedIds.Add((int)this.depend_action_7);
                this.ActionDataList = new List<MasterSkillAction.SkillAction>();
                int index = 0;
                for (int count = this.ActionIds.Count; index < count; ++index)
                {
                    if (this.ActionIds[index] != 0)
                    {
                        PCRCaculator.SkillAction skillAction1 = null;
                        MasterSkillAction.SkillAction skillAction = null;
                        if(PCRCaculator.MainManager.Instance.SkillActionDic.TryGetValue(ActionIds[index],out skillAction1))
                        {
                            skillAction = new MasterSkillAction.SkillAction(skillAction1);
                        }                        
                        if (skillAction != null)
                        {
                            skillAction.DependActionId = this.DependedIds[index];
                            this.ActionDataList.Add(skillAction);
                        }
                    }
                }
            }

            public ObscuredInt skill_id => this._skill_id;

            public ObscuredString name => this._name;

            public bool nameIsNull { get; private set; }

            public ObscuredInt skill_type => this._skill_type;

            public ObscuredInt skill_area_width => this._skill_area_width;

            public ObscuredDouble skill_cast_time => this._skill_cast_time;

            public ObscuredInt action_1 => this._action_1;

            public ObscuredInt action_2 => this._action_2;

            public ObscuredInt action_3 => this._action_3;

            public ObscuredInt action_4 => this._action_4;

            public ObscuredInt action_5 => this._action_5;

            public ObscuredInt action_6 => this._action_6;

            public ObscuredInt action_7 => this._action_7;

            public ObscuredInt depend_action_1 => this._depend_action_1;

            public ObscuredInt depend_action_2 => this._depend_action_2;

            public ObscuredInt depend_action_3 => this._depend_action_3;

            public ObscuredInt depend_action_4 => this._depend_action_4;

            public ObscuredInt depend_action_5 => this._depend_action_5;

            public ObscuredInt depend_action_6 => this._depend_action_6;

            public ObscuredInt depend_action_7 => this._depend_action_7;

            public ObscuredString description => this._description;

            public ObscuredInt icon_type => this._icon_type;

            public SkillData(
              int skill_id = 0,
              bool nameIsNull = true,
              string name = "",
              int skill_type = 0,
              int skill_area_width = 0,
              double skill_cast_time = 0.0,
              int action_1 = 0,
              int action_2 = 0,
              int action_3 = 0,
              int action_4 = 0,
              int action_5 = 0,
              int action_6 = 0,
              int action_7 = 0,
              int depend_action_1 = 0,
              int depend_action_2 = 0,
              int depend_action_3 = 0,
              int depend_action_4 = 0,
              int depend_action_5 = 0,
              int depend_action_6 = 0,
              int depend_action_7 = 0,
              string description = "",
              int icon_type = 0)
            {
                this._skill_id = (ObscuredInt)skill_id;
                this.nameIsNull = nameIsNull;
                this._name = (ObscuredString)name;
                this._skill_type = (ObscuredInt)skill_type;
                this._skill_area_width = (ObscuredInt)skill_area_width;
                this._skill_cast_time = (ObscuredDouble)skill_cast_time;
                this._action_1 = (ObscuredInt)action_1;
                this._action_2 = (ObscuredInt)action_2;
                this._action_3 = (ObscuredInt)action_3;
                this._action_4 = (ObscuredInt)action_4;
                this._action_5 = (ObscuredInt)action_5;
                this._action_6 = (ObscuredInt)action_6;
                this._action_7 = (ObscuredInt)action_7;
                this._depend_action_1 = (ObscuredInt)depend_action_1;
                this._depend_action_2 = (ObscuredInt)depend_action_2;
                this._depend_action_3 = (ObscuredInt)depend_action_3;
                this._depend_action_4 = (ObscuredInt)depend_action_4;
                this._depend_action_5 = (ObscuredInt)depend_action_5;
                this._depend_action_6 = (ObscuredInt)depend_action_6;
                this._depend_action_7 = (ObscuredInt)depend_action_7;
                this._description = (ObscuredString)description;
                this._icon_type = (ObscuredInt)icon_type;
                this.SetUp();
            }
            public SkillData(PCRCaculator.SkillData data)
            {
                this._skill_id = (ObscuredInt)data.skillid;
                this.nameIsNull = false;
                this._name = (ObscuredString)data.name;
                this._skill_type = (ObscuredInt)data.type;
                this._skill_area_width = (ObscuredInt)data.areawidth;
                this._skill_cast_time = (ObscuredDouble)data.casttime;
                this._action_1 = (ObscuredInt)data.skillactions[0];
                this._action_2 = (ObscuredInt)data.skillactions[1];
                this._action_3 = (ObscuredInt)data.skillactions[2];
                this._action_4 = (ObscuredInt)data.skillactions[3];
                this._action_5 = (ObscuredInt)data.skillactions[4];
                this._action_6 = (ObscuredInt)data.skillactions[5];
                this._action_7 = (ObscuredInt)data.skillactions[6];
                this._depend_action_1 = (ObscuredInt)data.dependactions[0];
                this._depend_action_2 = (ObscuredInt)data.dependactions[1];
                this._depend_action_3 = (ObscuredInt)data.dependactions[2];
                this._depend_action_4 = (ObscuredInt)data.dependactions[3];
                this._depend_action_5 = (ObscuredInt)data.dependactions[4];
                this._depend_action_6 = (ObscuredInt)data.dependactions[5];
                this._depend_action_7 = (ObscuredInt)data.dependactions[6];
                this._description = (ObscuredString)data.describes;
                this._icon_type = (ObscuredInt)data.icon;
                this.SetUp();

            }
            public void ChangeSkillCastTime(float time)
            {
                _skill_cast_time = time;
            }
        }

        public class MainSkillRateData
        {
            public float Increment;
            public float Offset;

            /*public MainSkillRateData(JsonData jsonData)
            {
              this.Increment = (float) jsonData["incr"].ToDouble();
              this.Offset = (float) jsonData["offset"].ToDouble();
            }*/
        }
    }
}
