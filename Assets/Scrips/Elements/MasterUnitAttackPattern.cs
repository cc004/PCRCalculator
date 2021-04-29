// Decompiled with JetBrains decompiler
// Type: Elements.MasterUnitAttackPattern
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using CodeStage.AntiCheat.ObscuredTypes;
//using Sqlite3Plugin;
using System.Collections.Generic;

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
            protected ObscuredInt _pattern_id;
            protected ObscuredInt _unit_id;
            protected ObscuredInt _loop_start;
            protected ObscuredInt _loop_end;
            protected ObscuredInt _atk_pattern_1;
            protected ObscuredInt _atk_pattern_2;
            protected ObscuredInt _atk_pattern_3;
            protected ObscuredInt _atk_pattern_4;
            protected ObscuredInt _atk_pattern_5;
            protected ObscuredInt _atk_pattern_6;
            protected ObscuredInt _atk_pattern_7;
            protected ObscuredInt _atk_pattern_8;
            protected ObscuredInt _atk_pattern_9;
            protected ObscuredInt _atk_pattern_10;
            protected ObscuredInt _atk_pattern_11;
            protected ObscuredInt _atk_pattern_12;
            protected ObscuredInt _atk_pattern_13;
            protected ObscuredInt _atk_pattern_14;
            protected ObscuredInt _atk_pattern_15;
            protected ObscuredInt _atk_pattern_16;
            protected ObscuredInt _atk_pattern_17;
            protected ObscuredInt _atk_pattern_18;
            protected ObscuredInt _atk_pattern_19;
            protected ObscuredInt _atk_pattern_20;

            public List<int> PatternList { get; set; }

            public void SetUp()
            {
                this.PatternList = new List<int>();
                this.PatternList.Add((int)this.atk_pattern_1);
                this.PatternList.Add((int)this.atk_pattern_2);
                this.PatternList.Add((int)this.atk_pattern_3);
                this.PatternList.Add((int)this.atk_pattern_4);
                this.PatternList.Add((int)this.atk_pattern_5);
                this.PatternList.Add((int)this.atk_pattern_6);
                this.PatternList.Add((int)this.atk_pattern_7);
                this.PatternList.Add((int)this.atk_pattern_8);
                this.PatternList.Add((int)this.atk_pattern_9);
                this.PatternList.Add((int)this.atk_pattern_10);
                this.PatternList.Add((int)this.atk_pattern_11);
                this.PatternList.Add((int)this.atk_pattern_12);
                this.PatternList.Add((int)this.atk_pattern_13);
                this.PatternList.Add((int)this.atk_pattern_15);
                this.PatternList.Add((int)this.atk_pattern_16);
                this.PatternList.Add((int)this.atk_pattern_17);
                this.PatternList.Add((int)this.atk_pattern_18);
                this.PatternList.Add((int)this.atk_pattern_19);
                this.PatternList.Add((int)this.atk_pattern_20);
            }

            public ObscuredInt pattern_id => this._pattern_id;

            public ObscuredInt unit_id => this._unit_id;

            public ObscuredInt loop_start => this._loop_start;

            public ObscuredInt loop_end => this._loop_end;

            public ObscuredInt atk_pattern_1 => this._atk_pattern_1;

            public ObscuredInt atk_pattern_2 => this._atk_pattern_2;

            public ObscuredInt atk_pattern_3 => this._atk_pattern_3;

            public ObscuredInt atk_pattern_4 => this._atk_pattern_4;

            public ObscuredInt atk_pattern_5 => this._atk_pattern_5;

            public ObscuredInt atk_pattern_6 => this._atk_pattern_6;

            public ObscuredInt atk_pattern_7 => this._atk_pattern_7;

            public ObscuredInt atk_pattern_8 => this._atk_pattern_8;

            public ObscuredInt atk_pattern_9 => this._atk_pattern_9;

            public ObscuredInt atk_pattern_10 => this._atk_pattern_10;

            public ObscuredInt atk_pattern_11 => this._atk_pattern_11;

            public ObscuredInt atk_pattern_12 => this._atk_pattern_12;

            public ObscuredInt atk_pattern_13 => this._atk_pattern_13;

            public ObscuredInt atk_pattern_14 => this._atk_pattern_14;

            public ObscuredInt atk_pattern_15 => this._atk_pattern_15;

            public ObscuredInt atk_pattern_16 => this._atk_pattern_16;

            public ObscuredInt atk_pattern_17 => this._atk_pattern_17;

            public ObscuredInt atk_pattern_18 => this._atk_pattern_18;

            public ObscuredInt atk_pattern_19 => this._atk_pattern_19;

            public ObscuredInt atk_pattern_20 => this._atk_pattern_20;

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
                this._pattern_id = (ObscuredInt)pattern_id;
                this._unit_id = (ObscuredInt)unit_id;
                this._loop_start = (ObscuredInt)loop_start;
                this._loop_end = (ObscuredInt)loop_end;
                this._atk_pattern_1 = (ObscuredInt)atk_pattern_1;
                this._atk_pattern_2 = (ObscuredInt)atk_pattern_2;
                this._atk_pattern_3 = (ObscuredInt)atk_pattern_3;
                this._atk_pattern_4 = (ObscuredInt)atk_pattern_4;
                this._atk_pattern_5 = (ObscuredInt)atk_pattern_5;
                this._atk_pattern_6 = (ObscuredInt)atk_pattern_6;
                this._atk_pattern_7 = (ObscuredInt)atk_pattern_7;
                this._atk_pattern_8 = (ObscuredInt)atk_pattern_8;
                this._atk_pattern_9 = (ObscuredInt)atk_pattern_9;
                this._atk_pattern_10 = (ObscuredInt)atk_pattern_10;
                this._atk_pattern_11 = (ObscuredInt)atk_pattern_11;
                this._atk_pattern_12 = (ObscuredInt)atk_pattern_12;
                this._atk_pattern_13 = (ObscuredInt)atk_pattern_13;
                this._atk_pattern_14 = (ObscuredInt)atk_pattern_14;
                this._atk_pattern_15 = (ObscuredInt)atk_pattern_15;
                this._atk_pattern_16 = (ObscuredInt)atk_pattern_16;
                this._atk_pattern_17 = (ObscuredInt)atk_pattern_17;
                this._atk_pattern_18 = (ObscuredInt)atk_pattern_18;
                this._atk_pattern_19 = (ObscuredInt)atk_pattern_19;
                this._atk_pattern_20 = (ObscuredInt)atk_pattern_20;
                this.SetUp();
            }
            public UnitAttackPattern(int unitid,PCRCaculator.UnitSkillData data)
            {
                this._pattern_id = (ObscuredInt)unitid*100+1;
                this._unit_id = (ObscuredInt)unitid;
                this._loop_start = (ObscuredInt)data.loopStart;
                this._loop_end = (ObscuredInt)data.loopEnd;
                this._atk_pattern_1 = (ObscuredInt)data.atkPatterns[0];
                this._atk_pattern_2 = (ObscuredInt)data.atkPatterns[1];
                this._atk_pattern_3 = (ObscuredInt)data.atkPatterns[2];
                this._atk_pattern_4 = (ObscuredInt)data.atkPatterns[3];
                this._atk_pattern_5 = (ObscuredInt)data.atkPatterns[4];
                this._atk_pattern_6 = (ObscuredInt)data.atkPatterns[5];
                this._atk_pattern_7 = (ObscuredInt)data.atkPatterns[6];
                this._atk_pattern_8 = (ObscuredInt)data.atkPatterns[7];
                this._atk_pattern_9 = (ObscuredInt)data.atkPatterns[8];
                this._atk_pattern_10 = (ObscuredInt)data.atkPatterns[9];
                this._atk_pattern_11 = (ObscuredInt)data.atkPatterns[10];
                this._atk_pattern_12 = (ObscuredInt)data.atkPatterns[11];
                this._atk_pattern_13 = (ObscuredInt)data.atkPatterns[12];
                this._atk_pattern_14 = (ObscuredInt)data.atkPatterns[13];
                this._atk_pattern_15 = (ObscuredInt)data.atkPatterns[14];
                this._atk_pattern_16 = (ObscuredInt)data.atkPatterns[15];
                this._atk_pattern_17 = (ObscuredInt)data.atkPatterns[16];
                this._atk_pattern_18 = (ObscuredInt)data.atkPatterns[17];
                this._atk_pattern_19 = (ObscuredInt)data.atkPatterns[18];
                this._atk_pattern_20 = (ObscuredInt)data.atkPatterns[19];
                this.SetUp();

            }
            public UnitAttackPattern(PCRCaculator.UnitAttackPattern data)
            {
                this._pattern_id = (ObscuredInt)data.pattern_id;
                this._unit_id = (ObscuredInt)data.unit_id;
                this._loop_start = (ObscuredInt)data.loop_start;
                this._loop_end = (ObscuredInt)data.loop_end;
                this._atk_pattern_1 = (ObscuredInt)data.atk_patterns[0];
                this._atk_pattern_2 = (ObscuredInt)data.atk_patterns[1];
                this._atk_pattern_3 = (ObscuredInt)data.atk_patterns[2];
                this._atk_pattern_4 = (ObscuredInt)data.atk_patterns[3];
                this._atk_pattern_5 = (ObscuredInt)data.atk_patterns[4];
                this._atk_pattern_6 = (ObscuredInt)data.atk_patterns[5];
                this._atk_pattern_7 = (ObscuredInt)data.atk_patterns[6];
                this._atk_pattern_8 = (ObscuredInt)data.atk_patterns[7];
                this._atk_pattern_9 = (ObscuredInt)data.atk_patterns[8];
                this._atk_pattern_10 = (ObscuredInt)data.atk_patterns[9];
                this._atk_pattern_11 = (ObscuredInt)data.atk_patterns[10];
                this._atk_pattern_12 = (ObscuredInt)data.atk_patterns[11];
                this._atk_pattern_13 = (ObscuredInt)data.atk_patterns[12];
                this._atk_pattern_14 = (ObscuredInt)data.atk_patterns[13];
                this._atk_pattern_15 = (ObscuredInt)data.atk_patterns[14];
                this._atk_pattern_16 = (ObscuredInt)data.atk_patterns[15];
                this._atk_pattern_17 = (ObscuredInt)data.atk_patterns[16];
                this._atk_pattern_18 = (ObscuredInt)data.atk_patterns[17];
                this._atk_pattern_19 = (ObscuredInt)data.atk_patterns[18];
                this._atk_pattern_20 = (ObscuredInt)data.atk_patterns[19];
                this.SetUp();

            }

        }
    }
}
