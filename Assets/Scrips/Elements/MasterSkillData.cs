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

            public int depend_action_1 => _depend_action_1;

            public int depend_action_2 => _depend_action_2;

            public int depend_action_3 => _depend_action_3;

            public int depend_action_4 => _depend_action_4;

            public int depend_action_5 => _depend_action_5;

            public int depend_action_6 => _depend_action_6;

            public int depend_action_7 => _depend_action_7;

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
                _depend_action_1 = depend_action_1;
                _depend_action_2 = depend_action_2;
                _depend_action_3 = depend_action_3;
                _depend_action_4 = depend_action_4;
                _depend_action_5 = depend_action_5;
                _depend_action_6 = depend_action_6;
                _depend_action_7 = depend_action_7;
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
                _action_1 = data.skillactions[0];
                _action_2 = data.skillactions[1];
                _action_3 = data.skillactions[2];
                _action_4 = data.skillactions[3];
                _action_5 = data.skillactions[4];
                _action_6 = data.skillactions[5];
                _action_7 = data.skillactions[6];
                _action_8 = data.skillactions[7];
                _action_9 = data.skillactions[8];
                _action_10 = data.skillactions[9];
                _depend_action_1 = data.dependactions[0];
                _depend_action_2 = data.dependactions[1];
                _depend_action_3 = data.dependactions[2];
                _depend_action_4 = data.dependactions[3];
                _depend_action_5 = data.dependactions[4];
                _depend_action_6 = data.dependactions[5];
                _depend_action_7 = data.dependactions[6];
                _depend_action_8 = data.dependactions[7];
                _depend_action_9 = data.dependactions[8];
                _depend_action_10 = data.dependactions[9];
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
