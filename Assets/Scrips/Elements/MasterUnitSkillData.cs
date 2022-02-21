// Decompiled with JetBrains decompiler
// Type: Elements.MasterUnitSkillData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System.Collections.Generic;

//using Sqlite3Plugin;

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
      protected int _unit_id;
      protected int _union_burst;
      protected int _main_skill_1;
      protected int _main_skill_2;
      protected int _main_skill_3;
      protected int _main_skill_4;
      protected int _main_skill_5;
      protected int _main_skill_6;
      protected int _main_skill_7;
      protected int _main_skill_8;
      protected int _main_skill_9;
      protected int _main_skill_10;
      protected int _ex_skill_1;
      protected int _ex_skill_evolution_1;
      protected int _ex_skill_2;
      protected int _ex_skill_evolution_2;
      protected int _ex_skill_3;
      protected int _ex_skill_evolution_3;
      protected int _ex_skill_4;
      protected int _ex_skill_evolution_4;
      protected int _ex_skill_5;
      protected int _ex_skill_evolution_5;
      protected int _sp_skill_1;
      protected int _sp_skill_2;
      protected int _sp_skill_3;
      protected int _sp_skill_4;
      protected int _sp_skill_5;
      protected int _union_burst_evolution;
      protected int _main_skill_evolution_1;
      protected int _main_skill_evolution_2;
      protected int _sp_skill_evolution_1;
      protected int _sp_skill_evolution_2;

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
        UnionBurstIds = new List<int>();
        MainSkillIds = new List<int>();
        ExSkillIds = new List<int>();
        ExSkillEvolutionIds = new List<int>();
        SpSkillIds = new List<int>();
        UnionBurstEvolutionIds = new List<int>();
        MainSkillEvolutionIds = new List<int>();
        SpSkillEvolutionIds = new List<int>();
        UnionBurstIds.Add(union_burst);
        MainSkillIds.Add(main_skill_1);
        MainSkillIds.Add(main_skill_2);
        MainSkillIds.Add(main_skill_3);
        MainSkillIds.Add(main_skill_4);
        MainSkillIds.Add(main_skill_5);
        MainSkillIds.Add(main_skill_6);
        MainSkillIds.Add(main_skill_7);
        MainSkillIds.Add(main_skill_8);
        MainSkillIds.Add(main_skill_9);
        MainSkillIds.Add(main_skill_10);
        ExSkillIds.Add(ex_skill_1);
        ExSkillIds.Add(ex_skill_2);
        ExSkillIds.Add(ex_skill_3);
        ExSkillIds.Add(ex_skill_4);
        ExSkillIds.Add(ex_skill_5);
        ExSkillEvolutionIds.Add(ex_skill_evolution_1);
        ExSkillEvolutionIds.Add(ex_skill_evolution_2);
        ExSkillEvolutionIds.Add(ex_skill_evolution_3);
        ExSkillEvolutionIds.Add(ex_skill_evolution_4);
        ExSkillEvolutionIds.Add(ex_skill_evolution_5);
        SpSkillIds.Add(sp_skill_1);
        SpSkillIds.Add(sp_skill_2);
        SpSkillIds.Add(sp_skill_3);
        SpSkillIds.Add(sp_skill_4);
        SpSkillIds.Add(sp_skill_5);
        UnionBurstEvolutionIds.Add(union_burst_evolution);
        MainSkillEvolutionIds.Add(main_skill_evolution_1);
        MainSkillEvolutionIds.Add(main_skill_evolution_2);
        SpSkillEvolutionIds.Add(sp_skill_evolution_1);
        SpSkillEvolutionIds.Add(sp_skill_evolution_2);
      }

      public int unit_id => _unit_id;

      public int union_burst => _union_burst;

      public int main_skill_1 => _main_skill_1;

      public int main_skill_2 => _main_skill_2;

      public int main_skill_3 => _main_skill_3;

      public int main_skill_4 => _main_skill_4;

      public int main_skill_5 => _main_skill_5;

      public int main_skill_6 => _main_skill_6;

      public int main_skill_7 => _main_skill_7;

      public int main_skill_8 => _main_skill_8;

      public int main_skill_9 => _main_skill_9;

      public int main_skill_10 => _main_skill_10;

      public int ex_skill_1 => _ex_skill_1;

      public int ex_skill_evolution_1 => _ex_skill_evolution_1;

      public int ex_skill_2 => _ex_skill_2;

      public int ex_skill_evolution_2 => _ex_skill_evolution_2;

      public int ex_skill_3 => _ex_skill_3;

      public int ex_skill_evolution_3 => _ex_skill_evolution_3;

      public int ex_skill_4 => _ex_skill_4;

      public int ex_skill_evolution_4 => _ex_skill_evolution_4;

      public int ex_skill_5 => _ex_skill_5;

      public int ex_skill_evolution_5 => _ex_skill_evolution_5;

      public int sp_skill_1 => _sp_skill_1;

      public int sp_skill_2 => _sp_skill_2;

      public int sp_skill_3 => _sp_skill_3;

      public int sp_skill_4 => _sp_skill_4;

      public int sp_skill_5 => _sp_skill_5;

      public int union_burst_evolution => _union_burst_evolution;

      public int main_skill_evolution_1 => _main_skill_evolution_1;

      public int main_skill_evolution_2 => _main_skill_evolution_2;

      public int sp_skill_evolution_1 => _sp_skill_evolution_1;

      public int sp_skill_evolution_2 => _sp_skill_evolution_2;

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
        _unit_id = unit_id;
        _union_burst = union_burst;
        _main_skill_1 = main_skill_1;
        _main_skill_2 = main_skill_2;
        _main_skill_3 = main_skill_3;
        _main_skill_4 = main_skill_4;
        _main_skill_5 = main_skill_5;
        _main_skill_6 = main_skill_6;
        _main_skill_7 = main_skill_7;
        _main_skill_8 = main_skill_8;
        _main_skill_9 = main_skill_9;
        _main_skill_10 = main_skill_10;
        _ex_skill_1 = ex_skill_1;
        _ex_skill_evolution_1 = ex_skill_evolution_1;
        _ex_skill_2 = ex_skill_2;
        _ex_skill_evolution_2 = ex_skill_evolution_2;
        _ex_skill_3 = ex_skill_3;
        _ex_skill_evolution_3 = ex_skill_evolution_3;
        _ex_skill_4 = ex_skill_4;
        _ex_skill_evolution_4 = ex_skill_evolution_4;
        _ex_skill_5 = ex_skill_5;
        _ex_skill_evolution_5 = ex_skill_evolution_5;
        _sp_skill_1 = sp_skill_1;
        _sp_skill_2 = sp_skill_2;
        _sp_skill_3 = sp_skill_3;
        _sp_skill_4 = sp_skill_4;
        _sp_skill_5 = sp_skill_5;
        _union_burst_evolution = union_burst_evolution;
        _main_skill_evolution_1 = main_skill_evolution_1;
        _main_skill_evolution_2 = main_skill_evolution_2;
        _sp_skill_evolution_1 = sp_skill_evolution_1;
        _sp_skill_evolution_2 = sp_skill_evolution_2;
        SetUp();
      }
    }
  }
}
