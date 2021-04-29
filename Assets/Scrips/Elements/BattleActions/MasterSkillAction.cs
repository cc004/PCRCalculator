// Decompiled with JetBrains decompiler
// Type: Elements.MasterSkillAction
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using CodeStage.AntiCheat.ObscuredTypes;
//using Sqlite3Plugin;
using System.Collections.Generic;

namespace Elements
{
    public class MasterSkillAction : AbstractMasterData
    {
        /*public const string TABLE_NAME = "skill_action";
        //private MasterSkillDatabase _db;
        private Dictionary<int, MasterSkillAction.SkillAction> _strictPrimaryKeyDictionary;
        private Dictionary<int, MasterSkillAction.SkillAction> _lazyPrimaryKeyDictionary;

        public static implicit operator Dictionary<int, MasterSkillAction.SkillAction>(
          MasterSkillAction data) => data.dictionary;

        public MasterSkillAction.SkillAction this[int id] => this.Get(id);

        public Dictionary<int, MasterSkillAction.SkillAction> dictionary
        {
          get
          {
            if (this._strictPrimaryKeyDictionary == null)
              this._StrictLoadAllEntries();
            return this._strictPrimaryKeyDictionary;
          }
        }

        public MasterSkillAction(MasterSkillDatabase db)
          : base((AbstractMasterDatabase) db)
        {
          this._lazyPrimaryKeyDictionary = new Dictionary<int, MasterSkillAction.SkillAction>();
          this._db = db;
        }

        public MasterSkillAction.SkillAction Get(int action_id)
        {
          int key = action_id;
          MasterSkillAction.SkillAction skillAction = (MasterSkillAction.SkillAction) null;
          if (this._strictPrimaryKeyDictionary != null)
          {
            this._strictPrimaryKeyDictionary.TryGetValue(key, out skillAction);
            return skillAction;
          }
          if (!this._lazyPrimaryKeyDictionary.TryGetValue(key, out skillAction))
          {
            skillAction = this._db != null ? this._SelectOne(action_id) : (MasterSkillAction.SkillAction) null;
            this._lazyPrimaryKeyDictionary[key] = skillAction;
          }
          return skillAction;
        }

        public bool HasKey(int action_id) => this.Get(action_id) != null;

        private MasterSkillAction.SkillAction _SelectOne(int action_id)
        {
          NAOCHNBMGCB querySkillAction = this._db.GetSelectQuery_SkillAction();
          if (querySkillAction == null)
            return (MasterSkillAction.SkillAction) null;
          if (!querySkillAction.BindInt(1, action_id))
            return (MasterSkillAction.SkillAction) null;
          MasterSkillAction.SkillAction skillAction = (MasterSkillAction.SkillAction) null;
          if (querySkillAction.Step())
            skillAction = this._CreateCachedOrmByQueryResult((ODBKLOJPCHG) querySkillAction);
          querySkillAction.Reset();
          return skillAction;
        }

        private MasterSkillAction.SkillAction _CreateCachedOrmByQueryResult(
          ODBKLOJPCHG query)
        {
          MasterSkillAction.SkillAction skillAction = (MasterSkillAction.SkillAction) null;
          int action_id = query.GetInt(0);
          int key = action_id;
          if (this._strictPrimaryKeyDictionary != null)
          {
            this._strictPrimaryKeyDictionary.TryGetValue(key, out skillAction);
            return skillAction;
          }
          if (!this._lazyPrimaryKeyDictionary.TryGetValue(key, out skillAction))
          {
            int class_id = query.GetInt(1);
            byte action_type = (byte) query.GetInt(2);
            int action_detail_1 = query.GetInt(3);
            int action_detail_2 = query.GetInt(4);
            int action_detail_3 = query.GetInt(5);
            double action_value_1 = query.GetDouble(6);
            double action_value_2 = query.GetDouble(7);
            double action_value_3 = query.GetDouble(8);
            double action_value_4 = query.GetDouble(9);
            double action_value_5 = query.GetDouble(10);
            double action_value_6 = query.GetDouble(11);
            double action_value_7 = query.GetDouble(12);
            int target_assignment = query.GetInt(13);
            int target_area = query.GetInt(14);
            int target_range = query.GetInt(15);
            int target_type = query.GetInt(16);
            int target_number = query.GetInt(17);
            int target_count = query.GetInt(18);
            string text1 = query.GetText(19);
            string text2 = query.GetText(20);
            skillAction = new MasterSkillAction.SkillAction(action_id, class_id, action_type, action_detail_1, action_detail_2, action_detail_3, action_value_1, action_value_2, action_value_3, action_value_4, action_value_5, action_value_6, action_value_7, target_assignment, target_area, target_range, target_type, target_number, target_count, text1, text2);
            this._lazyPrimaryKeyDictionary.Add(key, skillAction);
          }
          return skillAction;
        }
        */
        /*public override void Unload()
        {
          //this._lazyPrimaryKeyDictionary = (Dictionary<int, MasterSkillAction.SkillAction>) null;
          //this._strictPrimaryKeyDictionary = (Dictionary<int, MasterSkillAction.SkillAction>) null;
        }*/
        /*
            private void _StrictLoadAllEntries()
            {
              Dictionary<int, MasterSkillAction.SkillAction> dictionary = new Dictionary<int, MasterSkillAction.SkillAction>();
              using (ODBKLOJPCHG querySkillAction = this._db.GetSelectAllQuery_SkillAction())
              {
                while (querySkillAction.Step())
                {
                  int key = querySkillAction.GetInt(0);
                  dictionary.Add(key, this._CreateCachedOrmByQueryResult(querySkillAction));
                }
              }
              this._strictPrimaryKeyDictionary = dictionary;
              this._lazyPrimaryKeyDictionary = (Dictionary<int, MasterSkillAction.SkillAction>) null;
            }*/

        public class SkillAction
        {
            protected ObscuredInt _action_id;
            protected ObscuredInt _class_id;
            protected ObscuredByte _action_type;
            protected ObscuredInt _action_detail_1;
            protected ObscuredInt _action_detail_2;
            protected ObscuredInt _action_detail_3;
            protected ObscuredDouble _action_value_1;
            protected ObscuredDouble _action_value_2;
            protected ObscuredDouble _action_value_3;
            protected ObscuredDouble _action_value_4;
            protected ObscuredDouble _action_value_5;
            protected ObscuredDouble _action_value_6;
            protected ObscuredDouble _action_value_7;
            protected ObscuredInt _target_assignment;
            protected ObscuredInt _target_area;
            protected ObscuredInt _target_range;
            protected ObscuredInt _target_type;
            protected ObscuredInt _target_number;
            protected ObscuredInt _target_count;
            protected ObscuredString _description;
            protected ObscuredString _level_up_disp;

            public int DependActionId { get; set; }

            public ObscuredInt action_id => this._action_id;

            public ObscuredInt class_id => this._class_id;

            public ObscuredByte action_type => this._action_type;

            public ObscuredInt action_detail_1 => this._action_detail_1;

            public ObscuredInt action_detail_2 => this._action_detail_2;

            public ObscuredInt action_detail_3 => this._action_detail_3;

            public ObscuredDouble action_value_1 => this._action_value_1;

            public ObscuredDouble action_value_2 => this._action_value_2;

            public ObscuredDouble action_value_3 => this._action_value_3;

            public ObscuredDouble action_value_4 => this._action_value_4;

            public ObscuredDouble action_value_5 => this._action_value_5;

            public ObscuredDouble action_value_6 => this._action_value_6;

            public ObscuredDouble action_value_7 => this._action_value_7;

            public ObscuredInt target_assignment => this._target_assignment;

            public ObscuredInt target_area => this._target_area;

            public ObscuredInt target_range => this._target_range;

            public ObscuredInt target_type => this._target_type;

            public ObscuredInt target_number => this._target_number;

            public ObscuredInt target_count => this._target_count;

            public ObscuredString description => this._description;

            public ObscuredString level_up_disp => this._level_up_disp;

            public SkillAction(
              int action_id = 0,
              int class_id = 0,
              byte action_type = 0,
              int action_detail_1 = 0,
              int action_detail_2 = 0,
              int action_detail_3 = 0,
              double action_value_1 = 0.0,
              double action_value_2 = 0.0,
              double action_value_3 = 0.0,
              double action_value_4 = 0.0,
              double action_value_5 = 0.0,
              double action_value_6 = 0.0,
              double action_value_7 = 0.0,
              int target_assignment = 0,
              int target_area = 0,
              int target_range = 0,
              int target_type = 0,
              int target_number = 0,
              int target_count = 0,
              string description = "",
              string level_up_disp = "")
            {
                this._action_id = (ObscuredInt)action_id;
                this._class_id = (ObscuredInt)class_id;
                this._action_type = (ObscuredByte)action_type;
                this._action_detail_1 = (ObscuredInt)action_detail_1;
                this._action_detail_2 = (ObscuredInt)action_detail_2;
                this._action_detail_3 = (ObscuredInt)action_detail_3;
                this._action_value_1 = (ObscuredDouble)action_value_1;
                this._action_value_2 = (ObscuredDouble)action_value_2;
                this._action_value_3 = (ObscuredDouble)action_value_3;
                this._action_value_4 = (ObscuredDouble)action_value_4;
                this._action_value_5 = (ObscuredDouble)action_value_5;
                this._action_value_6 = (ObscuredDouble)action_value_6;
                this._action_value_7 = (ObscuredDouble)action_value_7;
                this._target_assignment = (ObscuredInt)target_assignment;
                this._target_area = (ObscuredInt)target_area;
                this._target_range = (ObscuredInt)target_range;
                this._target_type = (ObscuredInt)target_type;
                this._target_number = (ObscuredInt)target_number;
                this._target_count = (ObscuredInt)target_count;
                this._description = (ObscuredString)description;
                this._level_up_disp = (ObscuredString)level_up_disp;
            }
            public SkillAction(PCRCaculator.SkillAction data)
            {
                this._action_id = (ObscuredInt)data.actionid;
                this._class_id = (ObscuredInt)data.classid;
                this._action_type = (ObscuredByte)data.type;
                this._action_detail_1 = (ObscuredInt)data.details[0];
                this._action_detail_2 = (ObscuredInt)data.details[1];
                this._action_detail_3 = (ObscuredInt)data.details[2];
                this._action_value_1 = (ObscuredDouble)data.values[0];
                this._action_value_2 = (ObscuredDouble)data.values[1];
                this._action_value_3 = (ObscuredDouble)data.values[2];
                this._action_value_4 = (ObscuredDouble)data.values[3];
                this._action_value_5 = (ObscuredDouble)data.values[4];
                this._action_value_6 = (ObscuredDouble)data.values[5];
                this._action_value_7 = (ObscuredDouble)data.values[6];
                this._target_assignment = (ObscuredInt)data.target_assigment;
                this._target_area = (ObscuredInt)data.target_area;
                this._target_range = (ObscuredInt)data.target_range;
                this._target_type = (ObscuredInt)data.target_type;
                this._target_number = (ObscuredInt)data.target_number;
                this._target_count = (ObscuredInt)data.target_count;
                this._description = (ObscuredString)data.description;
                this._level_up_disp = (ObscuredString)"";

            }
        }
    }
}
