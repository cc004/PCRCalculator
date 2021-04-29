// Decompiled with JetBrains decompiler
// Type: Elements.MasterUnitSkillData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using CodeStage.AntiCheat.ObscuredTypes;
//using Sqlite3Plugin;
using System.Collections.Generic;

namespace Elements
{
  public class MasterUnitSkillData : AbstractMasterData
  {
    /*public const string TABLE_NAME = "unit_skill_data";
    private MasterUnitDatabase _db;
    private Dictionary<int, MasterUnitSkillData.UnitSkillData> _lazyPrimaryKeyDictionary;

    public MasterUnitSkillData.UnitSkillData this[int id] => this.Get(id);

    public MasterUnitSkillData(MasterUnitDatabase db)
      : base((AbstractMasterDatabase) db)
    {
      this._lazyPrimaryKeyDictionary = new Dictionary<int, MasterUnitSkillData.UnitSkillData>();
      this._db = db;
    }

    public MasterUnitSkillData.UnitSkillData Get(int unit_id)
    {
      int key = unit_id;
      MasterUnitSkillData.UnitSkillData unitSkillData = (MasterUnitSkillData.UnitSkillData) null;
      if (!this._lazyPrimaryKeyDictionary.TryGetValue(key, out unitSkillData))
      {
        unitSkillData = this._db != null ? this._SelectOne(unit_id) : (MasterUnitSkillData.UnitSkillData) null;
        this._lazyPrimaryKeyDictionary[key] = unitSkillData;
      }
      return unitSkillData;
    }

    public bool HasKey(int unit_id) => this.Get(unit_id) != null;

    private MasterUnitSkillData.UnitSkillData _SelectOne(int unit_id)
    {
      NAOCHNBMGCB queryUnitSkillData = this._db.GetSelectQuery_UnitSkillData();
      if (queryUnitSkillData == null)
        return (MasterUnitSkillData.UnitSkillData) null;
      if (!queryUnitSkillData.BindInt(1, unit_id))
        return (MasterUnitSkillData.UnitSkillData) null;
      MasterUnitSkillData.UnitSkillData unitSkillData = (MasterUnitSkillData.UnitSkillData) null;
      if (queryUnitSkillData.Step())
        unitSkillData = this._CreateCachedOrmByQueryResult((ODBKLOJPCHG) queryUnitSkillData);
      queryUnitSkillData.Reset();
      return unitSkillData;
    }

    private MasterUnitSkillData.UnitSkillData _CreateCachedOrmByQueryResult(
      ODBKLOJPCHG query)
    {
      MasterUnitSkillData.UnitSkillData unitSkillData = (MasterUnitSkillData.UnitSkillData) null;
      int unit_id = query.GetInt(0);
      int key = unit_id;
      if (!this._lazyPrimaryKeyDictionary.TryGetValue(key, out unitSkillData))
      {
        int union_burst = query.GetInt(1);
        int main_skill_1 = query.GetInt(2);
        int main_skill_2 = query.GetInt(3);
        int main_skill_3 = query.GetInt(4);
        int main_skill_4 = query.GetInt(5);
        int main_skill_5 = query.GetInt(6);
        int main_skill_6 = query.GetInt(7);
        int main_skill_7 = query.GetInt(8);
        int main_skill_8 = query.GetInt(9);
        int main_skill_9 = query.GetInt(10);
        int main_skill_10 = query.GetInt(11);
        int ex_skill_1 = query.GetInt(12);
        int ex_skill_evolution_1 = query.GetInt(13);
        int ex_skill_2 = query.GetInt(14);
        int ex_skill_evolution_2 = query.GetInt(15);
        int ex_skill_3 = query.GetInt(16);
        int ex_skill_evolution_3 = query.GetInt(17);
        int ex_skill_4 = query.GetInt(18);
        int ex_skill_evolution_4 = query.GetInt(19);
        int ex_skill_5 = query.GetInt(20);
        int ex_skill_evolution_5 = query.GetInt(21);
        int sp_skill_1 = query.GetInt(22);
        int sp_skill_2 = query.GetInt(23);
        int sp_skill_3 = query.GetInt(24);
        int sp_skill_4 = query.GetInt(25);
        int sp_skill_5 = query.GetInt(26);
        int union_burst_evolution = query.GetInt(27);
        int main_skill_evolution_1 = query.GetInt(28);
        int main_skill_evolution_2 = query.GetInt(29);
        int sp_skill_evolution_1 = query.GetInt(30);
        int sp_skill_evolution_2 = query.GetInt(31);
        unitSkillData = new MasterUnitSkillData.UnitSkillData(unit_id, union_burst, main_skill_1, main_skill_2, main_skill_3, main_skill_4, main_skill_5, main_skill_6, main_skill_7, main_skill_8, main_skill_9, main_skill_10, ex_skill_1, ex_skill_evolution_1, ex_skill_2, ex_skill_evolution_2, ex_skill_3, ex_skill_evolution_3, ex_skill_4, ex_skill_evolution_4, ex_skill_5, ex_skill_evolution_5, sp_skill_1, sp_skill_2, sp_skill_3, sp_skill_4, sp_skill_5, union_burst_evolution, main_skill_evolution_1, main_skill_evolution_2, sp_skill_evolution_1, sp_skill_evolution_2);
        this._lazyPrimaryKeyDictionary.Add(key, unitSkillData);
      }
      return unitSkillData;
    }

    public override void Unload() => this._lazyPrimaryKeyDictionary = (Dictionary<int, MasterUnitSkillData.UnitSkillData>) null;
    */
    public class UnitSkillData
    {
      protected ObscuredInt _unit_id;
      protected ObscuredInt _union_burst;
      protected ObscuredInt _main_skill_1;
      protected ObscuredInt _main_skill_2;
      protected ObscuredInt _main_skill_3;
      protected ObscuredInt _main_skill_4;
      protected ObscuredInt _main_skill_5;
      protected ObscuredInt _main_skill_6;
      protected ObscuredInt _main_skill_7;
      protected ObscuredInt _main_skill_8;
      protected ObscuredInt _main_skill_9;
      protected ObscuredInt _main_skill_10;
      protected ObscuredInt _ex_skill_1;
      protected ObscuredInt _ex_skill_evolution_1;
      protected ObscuredInt _ex_skill_2;
      protected ObscuredInt _ex_skill_evolution_2;
      protected ObscuredInt _ex_skill_3;
      protected ObscuredInt _ex_skill_evolution_3;
      protected ObscuredInt _ex_skill_4;
      protected ObscuredInt _ex_skill_evolution_4;
      protected ObscuredInt _ex_skill_5;
      protected ObscuredInt _ex_skill_evolution_5;
      protected ObscuredInt _sp_skill_1;
      protected ObscuredInt _sp_skill_2;
      protected ObscuredInt _sp_skill_3;
      protected ObscuredInt _sp_skill_4;
      protected ObscuredInt _sp_skill_5;
      protected ObscuredInt _union_burst_evolution;
      protected ObscuredInt _main_skill_evolution_1;
      protected ObscuredInt _main_skill_evolution_2;
      protected ObscuredInt _sp_skill_evolution_1;
      protected ObscuredInt _sp_skill_evolution_2;

      public List<int> UnionBurstIds { get; set; }

      public List<int> ExSkillIds { get; set; }

      public List<int> ExSkillEvolutionIds { get; set; }

      public List<int> MainSkillIds { get; set; }

      public List<int> SpSkillIds { get; set; }

      public List<int> SpSkillEvolutionIds { get; set; }

      public List<int> UnionBurstEvolutionIds { get; set; }

      public List<int> MainSkillEvolutionIds { get; set; }

      public void SetUp()
      {
        this.UnionBurstIds = new List<int>();
        this.MainSkillIds = new List<int>();
        this.ExSkillIds = new List<int>();
        this.ExSkillEvolutionIds = new List<int>();
        this.SpSkillIds = new List<int>();
        this.UnionBurstEvolutionIds = new List<int>();
        this.MainSkillEvolutionIds = new List<int>();
        this.SpSkillEvolutionIds = new List<int>();
        this.UnionBurstIds.Add((int) this.union_burst);
        this.MainSkillIds.Add((int) this.main_skill_1);
        this.MainSkillIds.Add((int) this.main_skill_2);
        this.MainSkillIds.Add((int) this.main_skill_3);
        this.MainSkillIds.Add((int) this.main_skill_4);
        this.MainSkillIds.Add((int) this.main_skill_5);
        this.MainSkillIds.Add((int) this.main_skill_6);
        this.MainSkillIds.Add((int) this.main_skill_7);
        this.MainSkillIds.Add((int) this.main_skill_8);
        this.MainSkillIds.Add((int) this.main_skill_9);
        this.MainSkillIds.Add((int) this.main_skill_10);
        this.ExSkillIds.Add((int) this.ex_skill_1);
        this.ExSkillIds.Add((int) this.ex_skill_2);
        this.ExSkillIds.Add((int) this.ex_skill_3);
        this.ExSkillIds.Add((int) this.ex_skill_4);
        this.ExSkillIds.Add((int) this.ex_skill_5);
        this.ExSkillEvolutionIds.Add((int) this.ex_skill_evolution_1);
        this.ExSkillEvolutionIds.Add((int) this.ex_skill_evolution_2);
        this.ExSkillEvolutionIds.Add((int) this.ex_skill_evolution_3);
        this.ExSkillEvolutionIds.Add((int) this.ex_skill_evolution_4);
        this.ExSkillEvolutionIds.Add((int) this.ex_skill_evolution_5);
        this.SpSkillIds.Add((int) this.sp_skill_1);
        this.SpSkillIds.Add((int) this.sp_skill_2);
        this.SpSkillIds.Add((int) this.sp_skill_3);
        this.SpSkillIds.Add((int) this.sp_skill_4);
        this.SpSkillIds.Add((int) this.sp_skill_5);
        this.UnionBurstEvolutionIds.Add((int) this.union_burst_evolution);
        this.MainSkillEvolutionIds.Add((int) this.main_skill_evolution_1);
        this.MainSkillEvolutionIds.Add((int) this.main_skill_evolution_2);
        this.SpSkillEvolutionIds.Add((int) this.sp_skill_evolution_1);
        this.SpSkillEvolutionIds.Add((int) this.sp_skill_evolution_2);
      }

      public ObscuredInt unit_id => this._unit_id;

      public ObscuredInt union_burst => this._union_burst;

      public ObscuredInt main_skill_1 => this._main_skill_1;

      public ObscuredInt main_skill_2 => this._main_skill_2;

      public ObscuredInt main_skill_3 => this._main_skill_3;

      public ObscuredInt main_skill_4 => this._main_skill_4;

      public ObscuredInt main_skill_5 => this._main_skill_5;

      public ObscuredInt main_skill_6 => this._main_skill_6;

      public ObscuredInt main_skill_7 => this._main_skill_7;

      public ObscuredInt main_skill_8 => this._main_skill_8;

      public ObscuredInt main_skill_9 => this._main_skill_9;

      public ObscuredInt main_skill_10 => this._main_skill_10;

      public ObscuredInt ex_skill_1 => this._ex_skill_1;

      public ObscuredInt ex_skill_evolution_1 => this._ex_skill_evolution_1;

      public ObscuredInt ex_skill_2 => this._ex_skill_2;

      public ObscuredInt ex_skill_evolution_2 => this._ex_skill_evolution_2;

      public ObscuredInt ex_skill_3 => this._ex_skill_3;

      public ObscuredInt ex_skill_evolution_3 => this._ex_skill_evolution_3;

      public ObscuredInt ex_skill_4 => this._ex_skill_4;

      public ObscuredInt ex_skill_evolution_4 => this._ex_skill_evolution_4;

      public ObscuredInt ex_skill_5 => this._ex_skill_5;

      public ObscuredInt ex_skill_evolution_5 => this._ex_skill_evolution_5;

      public ObscuredInt sp_skill_1 => this._sp_skill_1;

      public ObscuredInt sp_skill_2 => this._sp_skill_2;

      public ObscuredInt sp_skill_3 => this._sp_skill_3;

      public ObscuredInt sp_skill_4 => this._sp_skill_4;

      public ObscuredInt sp_skill_5 => this._sp_skill_5;

      public ObscuredInt union_burst_evolution => this._union_burst_evolution;

      public ObscuredInt main_skill_evolution_1 => this._main_skill_evolution_1;

      public ObscuredInt main_skill_evolution_2 => this._main_skill_evolution_2;

      public ObscuredInt sp_skill_evolution_1 => this._sp_skill_evolution_1;

      public ObscuredInt sp_skill_evolution_2 => this._sp_skill_evolution_2;

      public UnitSkillData(
        int unit_id = 0,
        int union_burst = 0,
        int main_skill_1 = 0,
        int main_skill_2 = 0,
        int main_skill_3 = 0,
        int main_skill_4 = 0,
        int main_skill_5 = 0,
        int main_skill_6 = 0,
        int main_skill_7 = 0,
        int main_skill_8 = 0,
        int main_skill_9 = 0,
        int main_skill_10 = 0,
        int ex_skill_1 = 0,
        int ex_skill_evolution_1 = 0,
        int ex_skill_2 = 0,
        int ex_skill_evolution_2 = 0,
        int ex_skill_3 = 0,
        int ex_skill_evolution_3 = 0,
        int ex_skill_4 = 0,
        int ex_skill_evolution_4 = 0,
        int ex_skill_5 = 0,
        int ex_skill_evolution_5 = 0,
        int sp_skill_1 = 0,
        int sp_skill_2 = 0,
        int sp_skill_3 = 0,
        int sp_skill_4 = 0,
        int sp_skill_5 = 0,
        int union_burst_evolution = 0,
        int main_skill_evolution_1 = 0,
        int main_skill_evolution_2 = 0,
        int sp_skill_evolution_1 = 0,
        int sp_skill_evolution_2 = 0)
      {
        this._unit_id = (ObscuredInt) unit_id;
        this._union_burst = (ObscuredInt) union_burst;
        this._main_skill_1 = (ObscuredInt) main_skill_1;
        this._main_skill_2 = (ObscuredInt) main_skill_2;
        this._main_skill_3 = (ObscuredInt) main_skill_3;
        this._main_skill_4 = (ObscuredInt) main_skill_4;
        this._main_skill_5 = (ObscuredInt) main_skill_5;
        this._main_skill_6 = (ObscuredInt) main_skill_6;
        this._main_skill_7 = (ObscuredInt) main_skill_7;
        this._main_skill_8 = (ObscuredInt) main_skill_8;
        this._main_skill_9 = (ObscuredInt) main_skill_9;
        this._main_skill_10 = (ObscuredInt) main_skill_10;
        this._ex_skill_1 = (ObscuredInt) ex_skill_1;
        this._ex_skill_evolution_1 = (ObscuredInt) ex_skill_evolution_1;
        this._ex_skill_2 = (ObscuredInt) ex_skill_2;
        this._ex_skill_evolution_2 = (ObscuredInt) ex_skill_evolution_2;
        this._ex_skill_3 = (ObscuredInt) ex_skill_3;
        this._ex_skill_evolution_3 = (ObscuredInt) ex_skill_evolution_3;
        this._ex_skill_4 = (ObscuredInt) ex_skill_4;
        this._ex_skill_evolution_4 = (ObscuredInt) ex_skill_evolution_4;
        this._ex_skill_5 = (ObscuredInt) ex_skill_5;
        this._ex_skill_evolution_5 = (ObscuredInt) ex_skill_evolution_5;
        this._sp_skill_1 = (ObscuredInt) sp_skill_1;
        this._sp_skill_2 = (ObscuredInt) sp_skill_2;
        this._sp_skill_3 = (ObscuredInt) sp_skill_3;
        this._sp_skill_4 = (ObscuredInt) sp_skill_4;
        this._sp_skill_5 = (ObscuredInt) sp_skill_5;
        this._union_burst_evolution = (ObscuredInt) union_burst_evolution;
        this._main_skill_evolution_1 = (ObscuredInt) main_skill_evolution_1;
        this._main_skill_evolution_2 = (ObscuredInt) main_skill_evolution_2;
        this._sp_skill_evolution_1 = (ObscuredInt) sp_skill_evolution_1;
        this._sp_skill_evolution_2 = (ObscuredInt) sp_skill_evolution_2;
        this.SetUp();
      }
    }
  }
}
