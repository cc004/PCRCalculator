// Decompiled with JetBrains decompiler
// Type: Elements.MasterSkillAction
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll


//using Sqlite3Plugin;

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
            protected int _action_id;
            protected int _class_id;
            protected byte _action_type;
            protected int _action_detail_1;
            protected int _action_detail_2;
            protected int _action_detail_3;
            protected double _action_value_1;
            protected double _action_value_2;
            protected double _action_value_3;
            protected double _action_value_4;
            protected double _action_value_5;
            protected double _action_value_6;
            protected double _action_value_7;
            protected int _target_assignment;
            protected int _target_area;
            protected int _target_range;
            protected int _target_type;
            protected int _target_number;
            protected int _target_count;
            protected string _description;
            protected string _level_up_disp;

            public int DependActionId { get; set; }

            public int action_id => _action_id;

            public int class_id => _class_id;

            public byte action_type => _action_type;

            public int action_detail_1 => _action_detail_1;

            public int action_detail_2 => _action_detail_2;

            public int action_detail_3 => _action_detail_3;

            public double action_value_1 => _action_value_1;

            public double action_value_2 => _action_value_2;

            public double action_value_3 => _action_value_3;

            public double action_value_4 => _action_value_4;

            public double action_value_5 => _action_value_5;

            public double action_value_6 => _action_value_6;

            public double action_value_7 => _action_value_7;

            public int target_assignment => _target_assignment;

            public int target_area => _target_area;

            public int target_range => _target_range;

            public int target_type => _target_type;

            public int target_number => _target_number;

            public int target_count => _target_count;

            public string description => _description;

            public string level_up_disp => _level_up_disp;
            /*
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
                _action_id = action_id;
                _class_id = class_id;
                _action_type = action_type;
                _action_detail_1 = action_detail_1;
                _action_detail_2 = action_detail_2;
                _action_detail_3 = action_detail_3;
                _action_value_1 = action_value_1;
                _action_value_2 = action_value_2;
                _action_value_3 = action_value_3;
                _action_value_4 = action_value_4;
                _action_value_5 = action_value_5;
                _action_value_6 = action_value_6;
                _action_value_7 = action_value_7;
                _target_assignment = target_assignment;
                _target_area = target_area;
                _target_range = target_range;
                _target_type = target_type;
                _target_number = target_number;
                _target_count = target_count;
                _description = description;
                _level_up_disp = level_up_disp;
            }*/
            public SkillAction(PCRCaculator.SkillAction data)
            {
                _action_id = data.actionid;
                _class_id = data.classid;
                _action_type = (byte)data.type;
                _action_detail_1 = data.details[0];
                _action_detail_2 = data.details[1];
                _action_detail_3 = data.details[2];
                _action_value_1 = data.values[0];
                _action_value_2 = data.values[1];
                _action_value_3 = data.values[2];
                _action_value_4 = data.values[3];
                _action_value_5 = data.values[4];
                _action_value_6 = data.values[5];
                _action_value_7 = data.values[6];
                _target_assignment = data.target_assigment;
                _target_area = data.target_area;
                _target_range = data.target_range;
                _target_type = data.target_type;
                _target_number = data.target_number;
                _target_count = data.target_count;
                _description = data.description;
                _level_up_disp = "";

            }
        }
    }
}
