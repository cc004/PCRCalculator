// Decompiled with JetBrains decompiler
// Type: Elements.MasterEnemyParameter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using CodeStage.AntiCheat.ObscuredTypes;
//using Sqlite3Plugin;
using System.Collections.Generic;

namespace Elements
{
  public class MasterEnemyParameter : AbstractMasterData
  {
    public const string TABLE_NAME = "enemy_parameter";
    //private MasterEnemyDatabase _db;
    private Dictionary<int, MasterEnemyParameter.EnemyParameter> _lazyPrimaryKeyDictionary;

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
      protected ObscuredInt _enemy_id;
      protected ObscuredInt _unit_id;
      protected ObscuredString _name;
      protected ObscuredInt _level;
      protected ObscuredInt _rarity;
      protected ObscuredInt _promotion_level;
      protected ObscuredInt _hp;
      protected ObscuredInt _atk;
      protected ObscuredInt _magic_str;
      protected ObscuredInt _def;
      protected ObscuredInt _magic_def;
      protected ObscuredInt _physical_critical;
      protected ObscuredInt _magic_critical;
      protected ObscuredInt _wave_hp_recovery;
      protected ObscuredInt _wave_energy_recovery;
      protected ObscuredInt _dodge;
      protected ObscuredInt _physical_penetrate;
      protected ObscuredInt _magic_penetrate;
      protected ObscuredInt _life_steal;
      protected ObscuredInt _hp_recovery_rate;
      protected ObscuredInt _energy_recovery_rate;
      protected ObscuredInt _energy_reduce_rate;
      protected ObscuredInt _union_burst_level;
      protected ObscuredInt _main_skill_lv_1;
      protected ObscuredInt _main_skill_lv_2;
      protected ObscuredInt _main_skill_lv_3;
      protected ObscuredInt _main_skill_lv_4;
      protected ObscuredInt _main_skill_lv_5;
      protected ObscuredInt _main_skill_lv_6;
      protected ObscuredInt _main_skill_lv_7;
      protected ObscuredInt _main_skill_lv_8;
      protected ObscuredInt _main_skill_lv_9;
      protected ObscuredInt _main_skill_lv_10;
      protected ObscuredInt _ex_skill_lv_1;
      protected ObscuredInt _ex_skill_lv_2;
      protected ObscuredInt _ex_skill_lv_3;
      protected ObscuredInt _ex_skill_lv_4;
      protected ObscuredInt _ex_skill_lv_5;
      protected ObscuredInt _resist_status_id;
      protected ObscuredInt _accuracy;
      protected ObscuredInt _unique_equipment_flag_1;

      public ObscuredInt enemy_id => this._enemy_id;

      public ObscuredInt unit_id => this._unit_id;

      public ObscuredString name => this._name;

      public ObscuredInt level => this._level;

      public ObscuredInt rarity => this._rarity;

      public ObscuredInt promotion_level => this._promotion_level;

      public ObscuredInt hp => this._hp;

      public ObscuredInt atk => this._atk;

      public ObscuredInt magic_str => this._magic_str;

      public ObscuredInt def => this._def;

      public ObscuredInt magic_def => this._magic_def;

      public ObscuredInt physical_critical => this._physical_critical;

      public ObscuredInt magic_critical => this._magic_critical;

      public ObscuredInt wave_hp_recovery => this._wave_hp_recovery;

      public ObscuredInt wave_energy_recovery => this._wave_energy_recovery;

      public ObscuredInt dodge => this._dodge;

      public ObscuredInt physical_penetrate => this._physical_penetrate;

      public ObscuredInt magic_penetrate => this._magic_penetrate;

      public ObscuredInt life_steal => this._life_steal;

      public ObscuredInt hp_recovery_rate => this._hp_recovery_rate;

      public ObscuredInt energy_recovery_rate => this._energy_recovery_rate;

      public ObscuredInt energy_reduce_rate => this._energy_reduce_rate;

      public ObscuredInt union_burst_level => this._union_burst_level;

      public ObscuredInt main_skill_lv_1 => this._main_skill_lv_1;

      public ObscuredInt main_skill_lv_2 => this._main_skill_lv_2;

      public ObscuredInt main_skill_lv_3 => this._main_skill_lv_3;

      public ObscuredInt main_skill_lv_4 => this._main_skill_lv_4;

      public ObscuredInt main_skill_lv_5 => this._main_skill_lv_5;

      public ObscuredInt main_skill_lv_6 => this._main_skill_lv_6;

      public ObscuredInt main_skill_lv_7 => this._main_skill_lv_7;

      public ObscuredInt main_skill_lv_8 => this._main_skill_lv_8;

      public ObscuredInt main_skill_lv_9 => this._main_skill_lv_9;

      public ObscuredInt main_skill_lv_10 => this._main_skill_lv_10;

      public ObscuredInt ex_skill_lv_1 => this._ex_skill_lv_1;

      public ObscuredInt ex_skill_lv_2 => this._ex_skill_lv_2;

      public ObscuredInt ex_skill_lv_3 => this._ex_skill_lv_3;

      public ObscuredInt ex_skill_lv_4 => this._ex_skill_lv_4;

      public ObscuredInt ex_skill_lv_5 => this._ex_skill_lv_5;

      public ObscuredInt resist_status_id => this._resist_status_id;

      public ObscuredInt accuracy => this._accuracy;

      public ObscuredInt unique_equipment_flag_1 => this._unique_equipment_flag_1;

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
        this._enemy_id = (ObscuredInt) enemy_id;
        this._unit_id = (ObscuredInt) unit_id;
        this._name = (ObscuredString) name;
        this._level = (ObscuredInt) level;
        this._rarity = (ObscuredInt) rarity;
        this._promotion_level = (ObscuredInt) promotion_level;
        this._hp = (ObscuredInt) hp;
        this._atk = (ObscuredInt) atk;
        this._magic_str = (ObscuredInt) magic_str;
        this._def = (ObscuredInt) def;
        this._magic_def = (ObscuredInt) magic_def;
        this._physical_critical = (ObscuredInt) physical_critical;
        this._magic_critical = (ObscuredInt) magic_critical;
        this._wave_hp_recovery = (ObscuredInt) wave_hp_recovery;
        this._wave_energy_recovery = (ObscuredInt) wave_energy_recovery;
        this._dodge = (ObscuredInt) dodge;
        this._physical_penetrate = (ObscuredInt) physical_penetrate;
        this._magic_penetrate = (ObscuredInt) magic_penetrate;
        this._life_steal = (ObscuredInt) life_steal;
        this._hp_recovery_rate = (ObscuredInt) hp_recovery_rate;
        this._energy_recovery_rate = (ObscuredInt) energy_recovery_rate;
        this._energy_reduce_rate = (ObscuredInt) energy_reduce_rate;
        this._union_burst_level = (ObscuredInt) union_burst_level;
        this._main_skill_lv_1 = (ObscuredInt) main_skill_lv_1;
        this._main_skill_lv_2 = (ObscuredInt) main_skill_lv_2;
        this._main_skill_lv_3 = (ObscuredInt) main_skill_lv_3;
        this._main_skill_lv_4 = (ObscuredInt) main_skill_lv_4;
        this._main_skill_lv_5 = (ObscuredInt) main_skill_lv_5;
        this._main_skill_lv_6 = (ObscuredInt) main_skill_lv_6;
        this._main_skill_lv_7 = (ObscuredInt) main_skill_lv_7;
        this._main_skill_lv_8 = (ObscuredInt) main_skill_lv_8;
        this._main_skill_lv_9 = (ObscuredInt) main_skill_lv_9;
        this._main_skill_lv_10 = (ObscuredInt) main_skill_lv_10;
        this._ex_skill_lv_1 = (ObscuredInt) ex_skill_lv_1;
        this._ex_skill_lv_2 = (ObscuredInt) ex_skill_lv_2;
        this._ex_skill_lv_3 = (ObscuredInt) ex_skill_lv_3;
        this._ex_skill_lv_4 = (ObscuredInt) ex_skill_lv_4;
        this._ex_skill_lv_5 = (ObscuredInt) ex_skill_lv_5;
        this._resist_status_id = (ObscuredInt) resist_status_id;
        this._accuracy = (ObscuredInt) accuracy;
        this._unique_equipment_flag_1 = (ObscuredInt) unique_equipment_flag_1;
      }
    }
  }
}
