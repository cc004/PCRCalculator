// Decompiled with JetBrains decompiler
// Type: Elements.MasterUnitAttackPattern
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System.Collections.Generic;

using PCRCaculator;
//using Sqlite3Plugin;

namespace Elements
{
    public class MasterUnitAttackPattern : AbstractMasterData
    {
        /*public const string TABLE_NAME = "unit_attack_pattern";
        private MasterUnitDatabase _db;
        private Dictionary<int, MasterUnitAttackPattern.UnitAttackPattern> _lazyPrimaryKeyDictionary;

        public MasterUnitAttackPattern.UnitAttackPattern this[int id] => this.Get(id);

        public MasterUnitAttackPattern(MasterUnitDatabase db)
          : base((AbstractMasterDatabase) db)
        {
          this._lazyPrimaryKeyDictionary = new Dictionary<int, MasterUnitAttackPattern.UnitAttackPattern>();
          this._db = db;
        }

        public MasterUnitAttackPattern.UnitAttackPattern Get(int pattern_id)
        {
          int key = pattern_id;
          MasterUnitAttackPattern.UnitAttackPattern unitAttackPattern = (MasterUnitAttackPattern.UnitAttackPattern) null;
          if (!this._lazyPrimaryKeyDictionary.TryGetValue(key, out unitAttackPattern))
          {
            unitAttackPattern = this._db != null ? this._SelectOne(pattern_id) : (MasterUnitAttackPattern.UnitAttackPattern) null;
            this._lazyPrimaryKeyDictionary[key] = unitAttackPattern;
          }
          return unitAttackPattern;
        }

        public bool HasKey(int pattern_id) => this.Get(pattern_id) != null;

        private MasterUnitAttackPattern.UnitAttackPattern _SelectOne(
          int pattern_id)
        {
          NAOCHNBMGCB unitAttackPattern1 = this._db.GetSelectQuery_UnitAttackPattern();
          if (unitAttackPattern1 == null)
            return (MasterUnitAttackPattern.UnitAttackPattern) null;
          if (!unitAttackPattern1.BindInt(1, pattern_id))
            return (MasterUnitAttackPattern.UnitAttackPattern) null;
          MasterUnitAttackPattern.UnitAttackPattern unitAttackPattern2 = (MasterUnitAttackPattern.UnitAttackPattern) null;
          if (unitAttackPattern1.Step())
            unitAttackPattern2 = this._CreateCachedOrmByQueryResult((ODBKLOJPCHG) unitAttackPattern1);
          unitAttackPattern1.Reset();
          return unitAttackPattern2;
        }

        private MasterUnitAttackPattern.UnitAttackPattern _CreateCachedOrmByQueryResult(
          ODBKLOJPCHG query)
        {
          MasterUnitAttackPattern.UnitAttackPattern unitAttackPattern = (MasterUnitAttackPattern.UnitAttackPattern) null;
          int pattern_id = query.GetInt(0);
          int key = pattern_id;
          if (!this._lazyPrimaryKeyDictionary.TryGetValue(key, out unitAttackPattern))
          {
            int unit_id = query.GetInt(1);
            int loop_start = query.GetInt(2);
            int loop_end = query.GetInt(3);
            int atk_pattern_1 = query.GetInt(4);
            int atk_pattern_2 = query.GetInt(5);
            int atk_pattern_3 = query.GetInt(6);
            int atk_pattern_4 = query.GetInt(7);
            int atk_pattern_5 = query.GetInt(8);
            int atk_pattern_6 = query.GetInt(9);
            int atk_pattern_7 = query.GetInt(10);
            int atk_pattern_8 = query.GetInt(11);
            int atk_pattern_9 = query.GetInt(12);
            int atk_pattern_10 = query.GetInt(13);
            int atk_pattern_11 = query.GetInt(14);
            int atk_pattern_12 = query.GetInt(15);
            int atk_pattern_13 = query.GetInt(16);
            int atk_pattern_14 = query.GetInt(17);
            int atk_pattern_15 = query.GetInt(18);
            int atk_pattern_16 = query.GetInt(19);
            int atk_pattern_17 = query.GetInt(20);
            int atk_pattern_18 = query.GetInt(21);
            int atk_pattern_19 = query.GetInt(22);
            int atk_pattern_20 = query.GetInt(23);
            unitAttackPattern = new MasterUnitAttackPattern.UnitAttackPattern(pattern_id, unit_id, loop_start, loop_end, atk_pattern_1, atk_pattern_2, atk_pattern_3, atk_pattern_4, atk_pattern_5, atk_pattern_6, atk_pattern_7, atk_pattern_8, atk_pattern_9, atk_pattern_10, atk_pattern_11, atk_pattern_12, atk_pattern_13, atk_pattern_14, atk_pattern_15, atk_pattern_16, atk_pattern_17, atk_pattern_18, atk_pattern_19, atk_pattern_20);
            this._lazyPrimaryKeyDictionary.Add(key, unitAttackPattern);
          }
          return unitAttackPattern;
        }

        public override void Unload() => this._lazyPrimaryKeyDictionary = (Dictionary<int, MasterUnitAttackPattern.UnitAttackPattern>) null;
        */
        public class UnitAttackPattern
        {
            protected int _pattern_id;
            protected int _unit_id;
            protected int _loop_start;
            protected int _loop_end;
            protected int _atk_pattern_1;
            protected int _atk_pattern_2;
            protected int _atk_pattern_3;
            protected int _atk_pattern_4;
            protected int _atk_pattern_5;
            protected int _atk_pattern_6;
            protected int _atk_pattern_7;
            protected int _atk_pattern_8;
            protected int _atk_pattern_9;
            protected int _atk_pattern_10;
            protected int _atk_pattern_11;
            protected int _atk_pattern_12;
            protected int _atk_pattern_13;
            protected int _atk_pattern_14;
            protected int _atk_pattern_15;
            protected int _atk_pattern_16;
            protected int _atk_pattern_17;
            protected int _atk_pattern_18;
            protected int _atk_pattern_19;
            protected int _atk_pattern_20;

            public List<int> PatternList { get; set; }

            public void SetUp()
            {
                PatternList = new List<int>();
                PatternList.Add(atk_pattern_1);
                PatternList.Add(atk_pattern_2);
                PatternList.Add(atk_pattern_3);
                PatternList.Add(atk_pattern_4);
                PatternList.Add(atk_pattern_5);
                PatternList.Add(atk_pattern_6);
                PatternList.Add(atk_pattern_7);
                PatternList.Add(atk_pattern_8);
                PatternList.Add(atk_pattern_9);
                PatternList.Add(atk_pattern_10);
                PatternList.Add(atk_pattern_11);
                PatternList.Add(atk_pattern_12);
                PatternList.Add(atk_pattern_13);
                PatternList.Add(atk_pattern_15);
                PatternList.Add(atk_pattern_16);
                PatternList.Add(atk_pattern_17);
                PatternList.Add(atk_pattern_18);
                PatternList.Add(atk_pattern_19);
                PatternList.Add(atk_pattern_20);
            }

            public int pattern_id => _pattern_id;

            public int unit_id => _unit_id;

            public int loop_start => _loop_start;

            public int loop_end => _loop_end;

            public int atk_pattern_1 => _atk_pattern_1;

            public int atk_pattern_2 => _atk_pattern_2;

            public int atk_pattern_3 => _atk_pattern_3;

            public int atk_pattern_4 => _atk_pattern_4;

            public int atk_pattern_5 => _atk_pattern_5;

            public int atk_pattern_6 => _atk_pattern_6;

            public int atk_pattern_7 => _atk_pattern_7;

            public int atk_pattern_8 => _atk_pattern_8;

            public int atk_pattern_9 => _atk_pattern_9;

            public int atk_pattern_10 => _atk_pattern_10;

            public int atk_pattern_11 => _atk_pattern_11;

            public int atk_pattern_12 => _atk_pattern_12;

            public int atk_pattern_13 => _atk_pattern_13;

            public int atk_pattern_14 => _atk_pattern_14;

            public int atk_pattern_15 => _atk_pattern_15;

            public int atk_pattern_16 => _atk_pattern_16;

            public int atk_pattern_17 => _atk_pattern_17;

            public int atk_pattern_18 => _atk_pattern_18;

            public int atk_pattern_19 => _atk_pattern_19;

            public int atk_pattern_20 => _atk_pattern_20;

            public UnitAttackPattern(
              int pattern_id = 0,
              int unit_id = 0,
              int loop_start = 0,
              int loop_end = 0,
              int atk_pattern_1 = 0,
              int atk_pattern_2 = 0,
              int atk_pattern_3 = 0,
              int atk_pattern_4 = 0,
              int atk_pattern_5 = 0,
              int atk_pattern_6 = 0,
              int atk_pattern_7 = 0,
              int atk_pattern_8 = 0,
              int atk_pattern_9 = 0,
              int atk_pattern_10 = 0,
              int atk_pattern_11 = 0,
              int atk_pattern_12 = 0,
              int atk_pattern_13 = 0,
              int atk_pattern_14 = 0,
              int atk_pattern_15 = 0,
              int atk_pattern_16 = 0,
              int atk_pattern_17 = 0,
              int atk_pattern_18 = 0,
              int atk_pattern_19 = 0,
              int atk_pattern_20 = 0)
            {
                _pattern_id = pattern_id;
                _unit_id = unit_id;
                _loop_start = loop_start;
                _loop_end = loop_end;
                _atk_pattern_1 = atk_pattern_1;
                _atk_pattern_2 = atk_pattern_2;
                _atk_pattern_3 = atk_pattern_3;
                _atk_pattern_4 = atk_pattern_4;
                _atk_pattern_5 = atk_pattern_5;
                _atk_pattern_6 = atk_pattern_6;
                _atk_pattern_7 = atk_pattern_7;
                _atk_pattern_8 = atk_pattern_8;
                _atk_pattern_9 = atk_pattern_9;
                _atk_pattern_10 = atk_pattern_10;
                _atk_pattern_11 = atk_pattern_11;
                _atk_pattern_12 = atk_pattern_12;
                _atk_pattern_13 = atk_pattern_13;
                _atk_pattern_14 = atk_pattern_14;
                _atk_pattern_15 = atk_pattern_15;
                _atk_pattern_16 = atk_pattern_16;
                _atk_pattern_17 = atk_pattern_17;
                _atk_pattern_18 = atk_pattern_18;
                _atk_pattern_19 = atk_pattern_19;
                _atk_pattern_20 = atk_pattern_20;
                SetUp();
            }
            public UnitAttackPattern(int unitid,UnitSkillData data)
            {
                _pattern_id = (int)unitid*100+1;
                _unit_id = unitid;
                _loop_start = data.loopStart;
                _loop_end = data.loopEnd;
                _atk_pattern_1 = data.atkPatterns[0];
                _atk_pattern_2 = data.atkPatterns[1];
                _atk_pattern_3 = data.atkPatterns[2];
                _atk_pattern_4 = data.atkPatterns[3];
                _atk_pattern_5 = data.atkPatterns[4];
                _atk_pattern_6 = data.atkPatterns[5];
                _atk_pattern_7 = data.atkPatterns[6];
                _atk_pattern_8 = data.atkPatterns[7];
                _atk_pattern_9 = data.atkPatterns[8];
                _atk_pattern_10 = data.atkPatterns[9];
                _atk_pattern_11 = data.atkPatterns[10];
                _atk_pattern_12 = data.atkPatterns[11];
                _atk_pattern_13 = data.atkPatterns[12];
                _atk_pattern_14 = data.atkPatterns[13];
                _atk_pattern_15 = data.atkPatterns[14];
                _atk_pattern_16 = data.atkPatterns[15];
                _atk_pattern_17 = data.atkPatterns[16];
                _atk_pattern_18 = data.atkPatterns[17];
                _atk_pattern_19 = data.atkPatterns[18];
                _atk_pattern_20 = data.atkPatterns[19];
                SetUp();

            }
            public UnitAttackPattern(PCRCaculator.UnitAttackPattern data)
            {
                _pattern_id = data.pattern_id;
                _unit_id = data.unit_id;
                _loop_start = data.loop_start;
                _loop_end = data.loop_end;
                _atk_pattern_1 = data.atk_patterns[0];
                _atk_pattern_2 = data.atk_patterns[1];
                _atk_pattern_3 = data.atk_patterns[2];
                _atk_pattern_4 = data.atk_patterns[3];
                _atk_pattern_5 = data.atk_patterns[4];
                _atk_pattern_6 = data.atk_patterns[5];
                _atk_pattern_7 = data.atk_patterns[6];
                _atk_pattern_8 = data.atk_patterns[7];
                _atk_pattern_9 = data.atk_patterns[8];
                _atk_pattern_10 = data.atk_patterns[9];
                _atk_pattern_11 = data.atk_patterns[10];
                _atk_pattern_12 = data.atk_patterns[11];
                _atk_pattern_13 = data.atk_patterns[12];
                _atk_pattern_14 = data.atk_patterns[13];
                _atk_pattern_15 = data.atk_patterns[14];
                _atk_pattern_16 = data.atk_patterns[15];
                _atk_pattern_17 = data.atk_patterns[16];
                _atk_pattern_18 = data.atk_patterns[17];
                _atk_pattern_19 = data.atk_patterns[18];
                _atk_pattern_20 = data.atk_patterns[19];
                SetUp();

            }

        }
    }
}
