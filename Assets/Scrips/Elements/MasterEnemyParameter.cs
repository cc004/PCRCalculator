// Decompiled with JetBrains decompiler
// Type: Elements.MasterEnemyParameter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System.Collections.Generic;

//using Sqlite3Plugin;

namespace Elements
{
  public class MasterEnemyParameter : AbstractMasterData
  {
    public const string TABLE_NAME = "enemy_parameter";
    //private MasterEnemyDatabase _db;
    private Dictionary<int, EnemyParameter> _lazyPrimaryKeyDictionary;

    /*public MasterEnemyParameter(MasterEnemyDatabase db)
      : base((AbstractMasterDatabase) db)
    {
      this._lazyPrimaryKeyDictionary = new Dictionary<int, MasterEnemyParameter.EnemyParameter>();
      this._db = db;
    }*/

    /*protected MasterEnemyParameter.EnemyParameter Get(int enemy_id)
    {
      int key = enemy_id;
      MasterEnemyParameter.EnemyParameter enemyParameter = (MasterEnemyParameter.EnemyParameter) null;
      if (!this._lazyPrimaryKeyDictionary.TryGetValue(key, out enemyParameter))
      {
        enemyParameter = this._db != null ? this._SelectOne(enemy_id) : (MasterEnemyParameter.EnemyParameter) null;
        this._lazyPrimaryKeyDictionary[key] = enemyParameter;
      }
      return enemyParameter;
    }*/

    //protected bool HasKey(int enemy_id) => this.Get(enemy_id) != null;

    /*private MasterEnemyParameter.EnemyParameter _SelectOne(int enemy_id)
    {
      NAOCHNBMGCB queryEnemyParameter = this._db.GetSelectQuery_EnemyParameter();
      if (queryEnemyParameter == null)
        return (MasterEnemyParameter.EnemyParameter) null;
      if (!queryEnemyParameter.BindInt(1, enemy_id))
        return (MasterEnemyParameter.EnemyParameter) null;
      MasterEnemyParameter.EnemyParameter enemyParameter = (MasterEnemyParameter.EnemyParameter) null;
      if (queryEnemyParameter.Step())
        enemyParameter = this._CreateCachedOrmByQueryResult((ODBKLOJPCHG) queryEnemyParameter);
      queryEnemyParameter.Reset();
      return enemyParameter;
    }*/

    /*private MasterEnemyParameter.EnemyParameter _CreateCachedOrmByQueryResult(
      ODBKLOJPCHG query)
    {
      MasterEnemyParameter.EnemyParameter enemyParameter = (MasterEnemyParameter.EnemyParameter) null;
      int enemy_id = query.GetInt(0);
      int key = enemy_id;
      if (!this._lazyPrimaryKeyDictionary.TryGetValue(key, out enemyParameter))
      {
        int unit_id = query.GetInt(1);
        string text = query.GetText(2);
        int level = query.GetInt(3);
        int rarity = query.GetInt(4);
        int promotion_level = query.GetInt(5);
        int hp = query.GetInt(6);
        int atk = query.GetInt(7);
        int magic_str = query.GetInt(8);
        int def = query.GetInt(9);
        int magic_def = query.GetInt(10);
        int physical_critical = query.GetInt(11);
        int magic_critical = query.GetInt(12);
        int wave_hp_recovery = query.GetInt(13);
        int wave_energy_recovery = query.GetInt(14);
        int dodge = query.GetInt(15);
        int physical_penetrate = query.GetInt(16);
        int magic_penetrate = query.GetInt(17);
        int life_steal = query.GetInt(18);
        int hp_recovery_rate = query.GetInt(19);
        int energy_recovery_rate = query.GetInt(20);
        int energy_reduce_rate = query.GetInt(21);
        int union_burst_level = query.GetInt(22);
        int main_skill_lv_1 = query.GetInt(23);
        int main_skill_lv_2 = query.GetInt(24);
        int main_skill_lv_3 = query.GetInt(25);
        int main_skill_lv_4 = query.GetInt(26);
        int main_skill_lv_5 = query.GetInt(27);
        int main_skill_lv_6 = query.GetInt(28);
        int main_skill_lv_7 = query.GetInt(29);
        int main_skill_lv_8 = query.GetInt(30);
        int main_skill_lv_9 = query.GetInt(31);
        int main_skill_lv_10 = query.GetInt(32);
        int ex_skill_lv_1 = query.GetInt(33);
        int ex_skill_lv_2 = query.GetInt(34);
        int ex_skill_lv_3 = query.GetInt(35);
        int ex_skill_lv_4 = query.GetInt(36);
        int ex_skill_lv_5 = query.GetInt(37);
        int resist_status_id = query.GetInt(38);
        int accuracy = query.GetInt(39);
        int unique_equipment_flag_1 = query.GetInt(40);
        enemyParameter = new MasterEnemyParameter.EnemyParameter(enemy_id, unit_id, text, level, rarity, promotion_level, hp, atk, magic_str, def, magic_def, physical_critical, magic_critical, wave_hp_recovery, wave_energy_recovery, dodge, physical_penetrate, magic_penetrate, life_steal, hp_recovery_rate, energy_recovery_rate, energy_reduce_rate, union_burst_level, main_skill_lv_1, main_skill_lv_2, main_skill_lv_3, main_skill_lv_4, main_skill_lv_5, main_skill_lv_6, main_skill_lv_7, main_skill_lv_8, main_skill_lv_9, main_skill_lv_10, ex_skill_lv_1, ex_skill_lv_2, ex_skill_lv_3, ex_skill_lv_4, ex_skill_lv_5, resist_status_id, accuracy, unique_equipment_flag_1);
        this._lazyPrimaryKeyDictionary.Add(key, enemyParameter);
      }
      return enemyParameter;
    }*/

    //public override void Unload() => this._lazyPrimaryKeyDictionary = (Dictionary<int, MasterEnemyParameter.EnemyParameter>) null;

    /*public MasterEnemyParameter.EnemyParameter GetFromAllKind(int unit_id) => 
            (((this.Get(unit_id) ?? 
            (MasterEnemyParameter.EnemyParameter) 
            ManagerSingleton<MasterDataManager>.Instance.masterEventEnemyParameter.Get(unit_id)) ?? 
            (MasterEnemyParameter.EnemyParameter) ManagerSingleton<MasterDataManager>.Instance.masterTowerEnemyParameter.Get(unit_id)) ?? 
            (MasterEnemyParameter.EnemyParameter) ManagerSingleton<MasterDataManager>.Instance.masterSekaiEnemyParameter.Get(unit_id)) ?? 
            (MasterEnemyParameter.EnemyParameter) ManagerSingleton<MasterDataManager>.Instance.masterShioriEnemyParameter.Get(unit_id);
*/
    public class EnemyParameter
    {
      protected int _enemy_id;
      protected int _unit_id;
      protected string _name;
      protected int _level;
      protected int _rarity;
      protected int _promotion_level;
      protected int _hp;
      protected int _atk;
      protected int _magic_str;
      protected int _def;
      protected int _magic_def;
      protected int _physical_critical;
      protected int _magic_critical;
      protected int _wave_hp_recovery;
      protected int _wave_energy_recovery;
      protected int _dodge;
      protected int _physical_penetrate;
      protected int _magic_penetrate;
      protected int _life_steal;
      protected int _hp_recovery_rate;
      protected int _energy_recovery_rate;
      protected int _energy_reduce_rate;
      protected int _union_burst_level;
      protected int _main_skill_lv_1;
      protected int _main_skill_lv_2;
      protected int _main_skill_lv_3;
      protected int _main_skill_lv_4;
      protected int _main_skill_lv_5;
      protected int _main_skill_lv_6;
      protected int _main_skill_lv_7;
      protected int _main_skill_lv_8;
      protected int _main_skill_lv_9;
      protected int _main_skill_lv_10;
      protected int _ex_skill_lv_1;
      protected int _ex_skill_lv_2;
      protected int _ex_skill_lv_3;
      protected int _ex_skill_lv_4;
      protected int _ex_skill_lv_5;
      protected int _resist_status_id;
      protected int _accuracy;
      protected int _unique_equipment_flag_1;

      public int enemy_id => _enemy_id;

      public int unit_id => _unit_id;

      public string name => _name;

      public int level => _level;

      public int rarity => _rarity;

      public int promotion_level => _promotion_level;

      public int hp => _hp;

      public int atk => _atk;

      public int magic_str => _magic_str;

      public int def => _def;

      public int magic_def => _magic_def;

      public int physical_critical => _physical_critical;

      public int magic_critical => _magic_critical;

      public int wave_hp_recovery => _wave_hp_recovery;

      public int wave_energy_recovery => _wave_energy_recovery;

      public int dodge => _dodge;

      public int physical_penetrate => _physical_penetrate;

      public int magic_penetrate => _magic_penetrate;

      public int life_steal => _life_steal;

      public int hp_recovery_rate => _hp_recovery_rate;

      public int energy_recovery_rate => _energy_recovery_rate;

      public int energy_reduce_rate => _energy_reduce_rate;

      public int union_burst_level => _union_burst_level;

      public int main_skill_lv_1 => _main_skill_lv_1;

      public int main_skill_lv_2 => _main_skill_lv_2;

      public int main_skill_lv_3 => _main_skill_lv_3;

      public int main_skill_lv_4 => _main_skill_lv_4;

      public int main_skill_lv_5 => _main_skill_lv_5;

      public int main_skill_lv_6 => _main_skill_lv_6;

      public int main_skill_lv_7 => _main_skill_lv_7;

      public int main_skill_lv_8 => _main_skill_lv_8;

      public int main_skill_lv_9 => _main_skill_lv_9;

      public int main_skill_lv_10 => _main_skill_lv_10;

      public int ex_skill_lv_1 => _ex_skill_lv_1;

      public int ex_skill_lv_2 => _ex_skill_lv_2;

      public int ex_skill_lv_3 => _ex_skill_lv_3;

      public int ex_skill_lv_4 => _ex_skill_lv_4;

      public int ex_skill_lv_5 => _ex_skill_lv_5;

      public int resist_status_id => _resist_status_id;

      public int accuracy => _accuracy;

      public int unique_equipment_flag_1 => _unique_equipment_flag_1;

      public EnemyParameter(
        int enemy_id = 0,
        int unit_id = 0,
        string name = "",
        int level = 0,
        int rarity = 0,
        int promotion_level = 0,
        int hp = 0,
        int atk = 0,
        int magic_str = 0,
        int def = 0,
        int magic_def = 0,
        int physical_critical = 0,
        int magic_critical = 0,
        int wave_hp_recovery = 0,
        int wave_energy_recovery = 0,
        int dodge = 0,
        int physical_penetrate = 0,
        int magic_penetrate = 0,
        int life_steal = 0,
        int hp_recovery_rate = 0,
        int energy_recovery_rate = 0,
        int energy_reduce_rate = 0,
        int union_burst_level = 0,
        int main_skill_lv_1 = 0,
        int main_skill_lv_2 = 0,
        int main_skill_lv_3 = 0,
        int main_skill_lv_4 = 0,
        int main_skill_lv_5 = 0,
        int main_skill_lv_6 = 0,
        int main_skill_lv_7 = 0,
        int main_skill_lv_8 = 0,
        int main_skill_lv_9 = 0,
        int main_skill_lv_10 = 0,
        int ex_skill_lv_1 = 0,
        int ex_skill_lv_2 = 0,
        int ex_skill_lv_3 = 0,
        int ex_skill_lv_4 = 0,
        int ex_skill_lv_5 = 0,
        int resist_status_id = 0,
        int accuracy = 0,
        int unique_equipment_flag_1 = 0)
      {
        _enemy_id = enemy_id;
        _unit_id = unit_id;
        _name = name;
        _level = level;
        _rarity = rarity;
        _promotion_level = promotion_level;
        _hp = hp;
        _atk = atk;
        _magic_str = magic_str;
        _def = def;
        _magic_def = magic_def;
        _physical_critical = physical_critical;
        _magic_critical = magic_critical;
        _wave_hp_recovery = wave_hp_recovery;
        _wave_energy_recovery = wave_energy_recovery;
        _dodge = dodge;
        _physical_penetrate = physical_penetrate;
        _magic_penetrate = magic_penetrate;
        _life_steal = life_steal;
        _hp_recovery_rate = hp_recovery_rate;
        _energy_recovery_rate = energy_recovery_rate;
        _energy_reduce_rate = energy_reduce_rate;
        _union_burst_level = union_burst_level;
        _main_skill_lv_1 = main_skill_lv_1;
        _main_skill_lv_2 = main_skill_lv_2;
        _main_skill_lv_3 = main_skill_lv_3;
        _main_skill_lv_4 = main_skill_lv_4;
        _main_skill_lv_5 = main_skill_lv_5;
        _main_skill_lv_6 = main_skill_lv_6;
        _main_skill_lv_7 = main_skill_lv_7;
        _main_skill_lv_8 = main_skill_lv_8;
        _main_skill_lv_9 = main_skill_lv_9;
        _main_skill_lv_10 = main_skill_lv_10;
        _ex_skill_lv_1 = ex_skill_lv_1;
        _ex_skill_lv_2 = ex_skill_lv_2;
        _ex_skill_lv_3 = ex_skill_lv_3;
        _ex_skill_lv_4 = ex_skill_lv_4;
        _ex_skill_lv_5 = ex_skill_lv_5;
        _resist_status_id = resist_status_id;
        _accuracy = accuracy;
        _unique_equipment_flag_1 = unique_equipment_flag_1;
      }
    }
  }
}
