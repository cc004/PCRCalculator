// Decompiled with JetBrains decompiler
// Type: Elements.MasterSkillData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System.Collections.Generic;

using PCRCaculator;
//using LitJson;
//using Sqlite3Plugin;

namespace Elements
{
    public class MasterSkillData : AbstractMasterData
    {
        public const string TABLE_NAME = "skill_data";
        //private MasterSkillDatabase _db;
        private Dictionary<int, SkillData> _lazyPrimaryKeyDictionary = new Dictionary<int, SkillData>();

        public SkillData this[int id] => Get(id);

        /**public MasterSkillData(MasterSkillDatabase db)
          : base((AbstractMasterDatabase) db)
        {
          this._lazyPrimaryKeyDictionary = new Dictionary<int, MasterSkillData.SkillData>();
          this._db = db;
        }*/

        public SkillData Get(int skill_id)
        {
            int key = skill_id;
            SkillData skillData = null;
            if (!_lazyPrimaryKeyDictionary.TryGetValue(key, out skillData))
            {
                //skillData = this._db != null ? this._SelectOne(skill_id) : (MasterSkillData.SkillData) null;
                //this._lazyPrimaryKeyDictionary[key] = skillData;
            }
            return skillData;
        }

        public bool HasKey(int skill_id) => Get(skill_id) != null;

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
            public List<MainSkillRateData> SkillValue = new List<MainSkillRateData>();
            //private MasterDataManager masterDataMng = ManagerSingleton<MasterDataManager>.Instance;
            protected int _skill_id;
            protected string _name;
            protected int _skill_type;
            protected int _skill_area_width;
            protected double _skill_cast_time;
            protected double _boss_ub_cool_time;
            protected int _action_1;
            protected int _action_2;
            protected int _action_3;
            protected int _action_4;
            protected int _action_5;
            protected int _action_6;
            protected int _action_7;
            protected int _action_8;
            protected int _action_9;
            protected int _action_10;
            protected int _action_11;
            protected int _action_12;
            protected int _action_13;
            protected int _action_14;
            protected int _action_15;
            protected int _action_16;
            protected int _action_17;
            protected int _action_18;
            protected int _action_19;
            protected int _action_20;
            protected int _depend_action_1;
            protected int _depend_action_2;
            protected int _depend_action_3;
            protected int _depend_action_4;
            protected int _depend_action_5;
            protected int _depend_action_6;
            protected int _depend_action_7;
            protected int _depend_action_8;
            protected int _depend_action_9;
            protected int _depend_action_10;
            protected int _depend_action_11;
            protected int _depend_action_12;
            protected int _depend_action_13;
            protected int _depend_action_14;
            protected int _depend_action_15;
            protected int _depend_action_16;
            protected int _depend_action_17;
            protected int _depend_action_18;
            protected int _depend_action_19;
            protected int _depend_action_20;
            protected string _description;
            protected int _icon_type;

            public int SkillId => skill_id;

            public string Name => name;

            public List<int> ActionIds { get; private set; }

            public List<int> DependedIds { get; private set; }

            public List<MasterSkillAction.SkillAction> ActionDataList { get; set; }

            public void SetUp()
            {
                ActionIds = new List<int>();
                ActionIds.Add(action_1);
                ActionIds.Add(action_2);
                ActionIds.Add(action_3);
                ActionIds.Add(action_4);
                ActionIds.Add(action_5);
                ActionIds.Add(action_6);
                ActionIds.Add(action_7);
                ActionIds.Add(_action_8);
                ActionIds.Add(_action_9);
                ActionIds.Add(_action_10);
                ActionIds.Add(_action_11);
                ActionIds.Add(_action_12);
                ActionIds.Add(_action_13);
                ActionIds.Add(_action_14);
                ActionIds.Add(_action_15);
                ActionIds.Add(_action_16);
                ActionIds.Add(_action_17);
                ActionIds.Add(_action_18);
                ActionIds.Add(_action_19);
                ActionIds.Add(_action_20);
                DependedIds = new List<int>();
                DependedIds.Add(depend_action_1);
                DependedIds.Add(depend_action_2);
                DependedIds.Add(depend_action_3);
                DependedIds.Add(depend_action_4);
                DependedIds.Add(depend_action_5);
                DependedIds.Add(depend_action_6);
                DependedIds.Add(depend_action_7);
                DependedIds.Add(_depend_action_8);
                DependedIds.Add(_depend_action_9);
                DependedIds.Add(_depend_action_10);
                DependedIds.Add(_depend_action_11);
                DependedIds.Add(_depend_action_12);
                DependedIds.Add(_depend_action_13);
                DependedIds.Add(_depend_action_14);
                DependedIds.Add(_depend_action_15);
                DependedIds.Add(_depend_action_16);
                DependedIds.Add(_depend_action_17);
                DependedIds.Add(_depend_action_18);
                DependedIds.Add(_depend_action_19);
                DependedIds.Add(_depend_action_20);
                ActionDataList = new List<MasterSkillAction.SkillAction>();
                int index = 0;
                for (int count = ActionIds.Count; index < count; ++index)
                {
                    if (ActionIds[index] != 0)
                    {
                        SkillAction skillAction1 = null;
                        MasterSkillAction.SkillAction skillAction = null;
                        if(MainManager.Instance.SkillActionDic.TryGetValue(ActionIds[index],out skillAction1))
                        {
                            skillAction = new MasterSkillAction.SkillAction(skillAction1);
                        }                        
                        if (skillAction != null)
                        {
                            skillAction.DependActionId = DependedIds[index];
                            ActionDataList.Add(skillAction);
                        }
                    }
                }
            }

            public int skill_id => _skill_id;

            public string name => _name;

            public bool nameIsNull { get; private set; }

            public int skill_type => _skill_type;

            public int skill_area_width => _skill_area_width;

            public double skill_cast_time => _skill_cast_time;
            public double boss_ub_cool_time => _boss_ub_cool_time;
            public int action_1 => _action_1;

            public int action_2 => _action_2;

            public int action_3 => _action_3;

            public int action_4 => _action_4;

            public int action_5 => _action_5;

            public int action_6 => _action_6;

            public int action_7 => _action_7;
            public int action_8 => _action_8;
            public int action_9 => _action_9;
            public int action_10 => _action_10;
            public int action_11 => _action_11;
            public int action_12 => _action_12;
            public int action_13 => _action_13;
            public int action_14 => _action_14;
            public int action_15 => _action_15;
            public int action_16 => _action_16;
            public int action_17 => _action_17;
            public int action_18 => _action_18;
            public int action_19 => _action_19;
            public int action_20 => _action_20;

            public int depend_action_1 => _depend_action_1;

            public int depend_action_2 => _depend_action_2;

            public int depend_action_3 => _depend_action_3;

            public int depend_action_4 => _depend_action_4;

            public int depend_action_5 => _depend_action_5;

            public int depend_action_6 => _depend_action_6;

            public int depend_action_7 => _depend_action_7;
            public int depend_action_8 => _depend_action_8;
            public int depend_action_9 => _depend_action_9;
            public int depend_action_10 => _depend_action_10;
            public int depend_action_11 => _depend_action_11;
            public int depend_action_12 => _depend_action_12;
            public int depend_action_13 => _depend_action_13;
            public int depend_action_14 => _depend_action_14;
            public int depend_action_15 => _depend_action_15;
            public int depend_action_16 => _depend_action_16;
            public int depend_action_17 => _depend_action_17;
            public int depend_action_18 => _depend_action_18;
            public int depend_action_19 => _depend_action_19;
            public int depend_action_20 => _depend_action_20;

            public string description => _description;

            public int icon_type => _icon_type;

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
              int action_8 = 0,
              int action_9 = 0,
              int action_10 = 0,
              int action_11 = 0,
              int action_12 = 0,
              int action_13 = 0,
              int action_14 = 0,
              int action_15 = 0,
              int action_16 = 0,
              int action_17 = 0,
              int action_18 = 0,
              int action_19 = 0,
              int action_20 = 0,
              int depend_action_1 = 0,
              int depend_action_2 = 0,
              int depend_action_3 = 0,
              int depend_action_4 = 0,
              int depend_action_5 = 0,
              int depend_action_6 = 0,
              int depend_action_7 = 0,
              int depend_action_8 = 0,
              int depend_action_9 = 0,
              int depend_action_10 = 0,
              int depend_action_11 = 0,
              int depend_action_12 = 0,
              int depend_action_13 = 0,
              int depend_action_14 = 0,
              int depend_action_15 = 0,
              int depend_action_16 = 0,
              int depend_action_17 = 0,
              int depend_action_18 = 0,
              int depend_action_19 = 0,
              int depend_action_20 = 0,
              string description = "",
              int icon_type = 0)
            {
                _skill_id = skill_id;
                this.nameIsNull = nameIsNull;
                _name = name;
                _skill_type = skill_type;
                _skill_area_width = skill_area_width;
                _skill_cast_time = skill_cast_time;
                _action_1 = action_1;
                _action_2 = action_2;
                _action_3 = action_3;
                _action_4 = action_4;
                _action_5 = action_5;
                _action_6 = action_6;
                _action_7 = action_7;
                _action_8 = action_8;
                _action_9 = action_9;
                _action_10 = action_10;
                _action_11 = action_11;
                _action_12 = action_12;
                _action_13 = action_13;
                _action_14 = action_14;
                _action_15 = action_15;
                _action_16 = action_16;
                _action_17 = action_17;
                _action_18 = action_18;
                _action_19 = action_19;
                _action_20 = action_20;
                _depend_action_1 = depend_action_1;
                _depend_action_2 = depend_action_2;
                _depend_action_3 = depend_action_3;
                _depend_action_4 = depend_action_4;
                _depend_action_5 = depend_action_5;
                _depend_action_6 = depend_action_6;
                _depend_action_7 = depend_action_7;
                _depend_action_8 = depend_action_8;
                _depend_action_9 = depend_action_9;
                _depend_action_10 = depend_action_10;
                _depend_action_11 = depend_action_11;
                _depend_action_12 = depend_action_12;
                _depend_action_13 = depend_action_13;
                _depend_action_14 = depend_action_14;
                _depend_action_15 = depend_action_15;
                _depend_action_16 = depend_action_16;
                _depend_action_17 = depend_action_17;
                _depend_action_18 = depend_action_18;
                _depend_action_19 = depend_action_19;
                _depend_action_20 = depend_action_20;
                _description = description;
                _icon_type = icon_type;
                SetUp();
            }
            public SkillData(PCRCaculator.SkillData data)
            {
                _skill_id = data.skillid;
                nameIsNull = false;
                _name = data.name;
                _skill_type = data.type;
                _skill_area_width = data.areawidth;
                _skill_cast_time = data.casttime;
                int SafeGet(int[] values, int index) => values != null && index < values.Length ? values[index] : 0;
                _action_1 = SafeGet(data.skillactions, 0);
                _action_2 = SafeGet(data.skillactions, 1);
                _action_3 = SafeGet(data.skillactions, 2);
                _action_4 = SafeGet(data.skillactions, 3);
                _action_5 = SafeGet(data.skillactions, 4);
                _action_6 = SafeGet(data.skillactions, 5);
                _action_7 = SafeGet(data.skillactions, 6);
                _action_8 = SafeGet(data.skillactions, 7);
                _action_9 = SafeGet(data.skillactions, 8);
                _action_10 = SafeGet(data.skillactions, 9);
                _action_11 = SafeGet(data.skillactions, 10);
                _action_12 = SafeGet(data.skillactions, 11);
                _action_13 = SafeGet(data.skillactions, 12);
                _action_14 = SafeGet(data.skillactions, 13);
                _action_15 = SafeGet(data.skillactions, 14);
                _action_16 = SafeGet(data.skillactions, 15);
                _action_17 = SafeGet(data.skillactions, 16);
                _action_18 = SafeGet(data.skillactions, 17);
                _action_19 = SafeGet(data.skillactions, 18);
                _action_20 = SafeGet(data.skillactions, 19);
                _depend_action_1 = SafeGet(data.dependactions, 0);
                _depend_action_2 = SafeGet(data.dependactions, 1);
                _depend_action_3 = SafeGet(data.dependactions, 2);
                _depend_action_4 = SafeGet(data.dependactions, 3);
                _depend_action_5 = SafeGet(data.dependactions, 4);
                _depend_action_6 = SafeGet(data.dependactions, 5);
                _depend_action_7 = SafeGet(data.dependactions, 6);
                _depend_action_8 = SafeGet(data.dependactions, 7);
                _depend_action_9 = SafeGet(data.dependactions, 8);
                _depend_action_10 = SafeGet(data.dependactions, 9);
                _depend_action_11 = SafeGet(data.dependactions, 10);
                _depend_action_12 = SafeGet(data.dependactions, 11);
                _depend_action_13 = SafeGet(data.dependactions, 12);
                _depend_action_14 = SafeGet(data.dependactions, 13);
                _depend_action_15 = SafeGet(data.dependactions, 14);
                _depend_action_16 = SafeGet(data.dependactions, 15);
                _depend_action_17 = SafeGet(data.dependactions, 16);
                _depend_action_18 = SafeGet(data.dependactions, 17);
                _depend_action_19 = SafeGet(data.dependactions, 18);
                _depend_action_20 = SafeGet(data.dependactions, 19);
                _description = data.describes;
                _icon_type = data.icon;
                SetUp();

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
