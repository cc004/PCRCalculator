using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elements;

namespace PCRCaculator.SQL
{
    public class unit_ex_equipment_slot
    {
        public int unit_id { get; set; }
        public int slot_category_1 { get; set; }
        public int slot_category_2 { get; set; }
        public int slot_category_3 { get; set; }
    }

    public class ex_equipment_enhance_data
    {
        public int rarity { get; set; }
        public int enhance_level { get; set; }
    }

    public class ex_equipment_sub_status_group : DbBaseData
    {
        public int ex_equipment_id { get; set; }
        public int group_id { get; set; }
    }
    public class ex_equipment_sub_status : DbBaseData
    {
        public int status { get; set; }
        public int group_id { get; set; }
        public int value_1 { get; set; }
        public int value_2 { get; set; }
        public int value_3 { get; set; }
        public int value_4 { get; set; }
        public int value_5 { get; set; }

        public int[] values => new int[] { value_1, value_2, value_3, value_4, value_5 };
    }

    public class ex_equip_5_exceed_limit_group
    {
        public int ex_equipment_id { get; set; }
        public int status_group_id { get; set; }
        public int skill_group_id { get; set; }
    }

    public class ex_equip_5_exceed_limit_skill
    {
        public int group_id { get; set; }
        public int rank { get; set; }
        public int skill_id { get; set; }
    }

    public class ex_equip_5_exceed_limit_status
    {
        public int status_group_id { get; set; }
        public int rank { get; set; }
        public int type_1 { get; set; }
        public int value_1 { get; set; }
        public int type_2 { get; set; }
        public int value_2 { get; set; }
        public int type_3 { get; set; }
        public int value_3 { get; set; }
        public int type_4 { get; set; }
        public int value_4 { get; set; }

        public int GetValue(int type)
        {
            return (type_1 == type ? value_1 : 0) + (type_2 == type ? value_2 : 0)
                + (type_3 == type ? value_3 : 0) + (type_4 == type ? value_4 : 0);
        }
    }

    public class ex_equipment_data
    {
        public Dictionary<int, ex_equip_5_exceed_limit_status> exceedStatus;
        public Dictionary<int, int> exceedSkills;
        public int SliderMax => exceedStatus?.Keys.Max() ?? levelMax;

        public int levelMax;
        public ex_equipment_data SetEnhanceLevelMax(int level)
        {
            levelMax = level;
            return this;
        }

        public int ex_equipment_id { get; set; }
        public string name { get; set; }
        public int category { get; set; }
        public int talent_id { get; set; }
        public int rarity { get; set; }
        public bool clan_battle_equip_flag { get; set; }

        public int default_hp { get; set; }
        public int default_atk { get; set; }
        public int default_magic_str { get; set; }
        public int default_def { get; set; }
        public int default_magic_def { get; set; }
        public int default_physical_critical { get; set; }
        public int default_magic_critical { get; set; }
        public int default_wave_hp_recovery { get; set; }
        public int default_wave_energy_recovery { get; set; }
        public int default_dodge { get; set; }
        public int default_physical_penetrate { get; set; }
        public int default_magic_penetrate { get; set; }
        public int default_life_steal { get; set; }
        public int default_hp_recovery_rate { get; set; }
        public int default_energy_recovery_rate { get; set; }
        public int default_energy_reduce_rate { get; set; }
        public int default_accuracy { get; set; }
        public int max_hp { get; set; }
        public int max_atk { get; set; }
        public int max_magic_str { get; set; }
        public int max_def { get; set; }
        public int max_magic_def { get; set; }
        public int max_physical_critical { get; set; }
        public int max_magic_critical { get; set; }
        public int max_wave_hp_recovery { get; set; }
        public int max_wave_energy_recovery { get; set; }
        public int max_dodge { get; set; }
        public int max_physical_penetrate { get; set; }
        public int max_magic_penetrate { get; set; }
        public int max_life_steal { get; set; }
        public int max_hp_recovery_rate { get; set; }
        public int max_energy_recovery_rate { get; set; }
        public int max_energy_reduce_rate { get; set; }
        public int max_accuracy { get; set; }
        public int passive_skill_id_1 { get; set; }
        public int passive_skill_id_2 { get; set; }
        public int passive_skill_powerup { get; set; }
        public int group_id { get; set; }
        public Dictionary<eParamType, int[]> status { get; set; }
            = new Dictionary<eParamType, int[]>();

        public eParamType DefaultSubStat(bool magic)
        {
            var attack = magic ? eParamType.MAGIC_ATK : eParamType.ATK;
            var penetrate = magic ? eParamType.MAGIC_PENETRATE : eParamType.PHYSICAL_PENETRATE;
            if (category >= 300)
            {
                if (status.ContainsKey(eParamType.ENERGY_RECOVERY_RATE)) return eParamType.ENERGY_RECOVERY_RATE;
            }
            else
            {
                if (status.ContainsKey(penetrate)) return penetrate;
                var other = magic ? eParamType.PHYSICAL_PENETRATE : eParamType.MAGIC_PENETRATE;
                if (status.ContainsKey(other)) return other;
            }
            if (category >= 200)
            {
                if (status.ContainsKey(attack)) return attack;
                var other = magic ? eParamType.ATK : eParamType.MAGIC_ATK;
                if (status.ContainsKey(other)) return other;
            }
            return status.Keys.FirstOrDefault();
        }

        public bool HasAttackBuff()
        {
            var db = MainManager.Instance;
            if (!db.SkillDataDic.TryGetValue(passive_skill_id_1, out var skill)) return false;
            foreach (int id in skill.skillactions)
            {
                if (!db.SkillActionDic.TryGetValue(id, out var action) || action.type != (int)eActionType.BUFF_DEBUFF)
                    continue;
                int detail = action.details[0];
                var kind = BuffDebuffAction.GetChangeParamKind(detail);
                if (detail % 10 == 0 && (kind == UnitCtrl.BuffParamKind.ATK || kind == UnitCtrl.BuffParamKind.MAGIC_STR))
                    return true;
            }
            return false;
        }
        public BaseData GetDefaultBaseData()
        {
            return new BaseData(default_hp, default_atk, default_def, default_magic_str, default_magic_def, default_physical_critical,//0-5
            default_magic_critical, default_dodge, default_life_steal, default_wave_hp_recovery, default_wave_energy_recovery, //6-9
            default_physical_penetrate, default_magic_penetrate, default_energy_recovery_rate, default_hp_recovery_rate,//10-13
            default_energy_reduce_rate, default_accuracy);
        }
        public BaseData GetMaxBaseData()
        {
            return new BaseData(max_hp, max_atk, max_def, max_magic_str, max_magic_def, max_physical_critical,//0-5
            max_magic_critical, max_dodge, max_life_steal, max_wave_hp_recovery, max_wave_energy_recovery, //6-9
            max_physical_penetrate, max_magic_penetrate, max_energy_recovery_rate, max_hp_recovery_rate,//10-13
            max_energy_reduce_rate, max_accuracy);
        }
    }
}
