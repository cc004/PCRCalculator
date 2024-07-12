using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using SQLite4Unity3d;
using PCRCaculator.Guild;
using Elements;

namespace PCRCaculator.SQL
{
    public class DbBaseData
    {
        public double hp { get; set; }
        public double atk { get; set; }
        public double magic_str { get; set; }
        public double def { get; set; }
        public double magic_def { get; set; }
        public double physical_critical { get; set; }
        public double magic_critical { get; set; }
        public double wave_hp_recovery { get; set; }
        public double wave_energy_recovery { get; set; }
        public double dodge { get; set; }
        public double physical_penetrate { get; set; }
        public double magic_penetrate { get; set; }
        public double life_steal { get; set; }
        public double hp_recovery_rate { get; set; }
        public double energy_recovery_rate { get; set; }
        public double energy_reduce_rate { get; set; }
        public double accuracy { get; set; }
        public BaseData GetBaseData()
        {
            return new BaseData(hp, atk, magic_str, def, magic_def, physical_critical,//0-5
            magic_critical, wave_hp_recovery, wave_energy_recovery, dodge,//6-9
            physical_penetrate, magic_penetrate, life_steal, hp_recovery_rate,//10-13
            energy_recovery_rate, energy_reduce_rate, accuracy);
        }
    }

    public class enemy_m_parts
    {
        [PrimaryKey]
        public int enemy_id { get; set; }
        public string name { get; set; }
        public int child_enemy_parameter_1 { get; set; }
        public int child_enemy_parameter_2 { get; set; }
        public int child_enemy_parameter_3 { get; set; }
        public int child_enemy_parameter_4 { get; set; }
        public int child_enemy_parameter_5 { get; set; }
    }
    public class wave_group_data
    {
        public int wave_group_id { get; set; }
        public int enemy_id_1 { get; set; }
    }
    public class clan_battle_2_map_data
    {
        public int id { get; set; }
        public int clan_battle_id { get; set; }
        public int phase { get; set; }
        public int wave_group_id_1 { get; set; }
        public int wave_group_id_2 { get; set; }
        public int wave_group_id_3 { get; set; }
        public int wave_group_id_4 { get; set; }
        public int wave_group_id_5 { get; set; }
    }
    public class enemy_parameter
    {
        [PrimaryKey]
        public int enemy_id { get; set; }
        public int unit_id { get; set; }
        public string name { get; set; }
        public int level { get; set; }
        public int rarity { get; set; }
        public int promotion_level { get; set; }
        public int hp { get; set; }
        public int atk { get; set; }
        public int magic_str { get; set; }
        public int def { get; set; }
        public int magic_def { get; set; }
        public int physical_critical { get; set; }
        public int magic_critical { get; set; }
        public int wave_hp_recovery { get; set; }
        public int wave_energy_recovery { get; set; }
        public int dodge { get; set; }
        public int physical_penetrate { get; set; }
        public int magic_penetrate { get; set; }
        public int life_steal { get; set; }
        public int hp_recovery_rate { get; set; }
        public int energy_recovery_rate { get; set; }
        public int energy_reduce_rate { get; set; }
        public int union_burst_level { get; set; }
        public int main_skill_lv_1 { get; set; }
        public int main_skill_lv_2 { get; set; }
        public int main_skill_lv_3 { get; set; }
        public int main_skill_lv_4 { get; set; }
        public int main_skill_lv_5 { get; set; }
        public int main_skill_lv_6 { get; set; }
        public int main_skill_lv_7 { get; set; }
        public int main_skill_lv_8 { get; set; }
        public int main_skill_lv_9 { get; set; }
        public int main_skill_lv_10 { get; set; }
        public int ex_skill_lv_1 { get; set; }
        public int ex_skill_lv_2 { get; set; }
        public int ex_skill_lv_3 { get; set; }
        public int ex_skill_lv_4 { get; set; }
        public int ex_skill_lv_5 { get; set; }
        public int resist_status_id { get; set; }
        public int resist_variation_id { get; set; }
        public int accuracy { get; set; }
        public int unique_equipment_flag_1 { get; set; }
        public int break_durability { get; set; }
        public int virtual_hp { get; set; }

        public BaseData GetBaseData()
        {
            return new BaseData(hp, atk, magic_str, def, magic_def, physical_critical,//0-5
            magic_critical, wave_hp_recovery, wave_energy_recovery, dodge,//6-9
            physical_penetrate, magic_penetrate, life_steal, hp_recovery_rate,//10-13
            energy_recovery_rate, energy_reduce_rate, accuracy)
            {
                realhp = hp
            };
        }
    }
    public class unit_enemy_data
    {
        [PrimaryKey]
        public int unit_id { get; set; }
        public string unit_name { get; set; }
        public int prefab_id { get; set; }
        public int motion_type { get; set; }
        public int se_type { get; set; }
        public int move_speed { get; set; }
        public int search_area_width { get; set; }
        public int atk_type { get; set; }
        public double normal_atk_cast_time { get; set; }
        public int cutin { get; set; }
        public int cutin_star6 { get; set; }
        public int visual_change_flag { get; set; }
        public string comment { get; set; }
    }
    public class unit_attack_pattern
    {
        [PrimaryKey]
        public int pattern_id { get; set; }
        public int unit_id { get; set; }
        public int loop_start { get; set; }
        public int loop_end { get; set; }
        public int atk_pattern_1 { get; set; }
        public int atk_pattern_2 { get; set; }
        public int atk_pattern_3 { get; set; }
        public int atk_pattern_4 { get; set; }
        public int atk_pattern_5 { get; set; }
        public int atk_pattern_6 { get; set; }
        public int atk_pattern_7 { get; set; }
        public int atk_pattern_8 { get; set; }
        public int atk_pattern_9 { get; set; }
        public int atk_pattern_10 { get; set; }
        public int atk_pattern_11 { get; set; }
        public int atk_pattern_12 { get; set; }
        public int atk_pattern_13 { get; set; }
        public int atk_pattern_14 { get; set; }
        public int atk_pattern_15 { get; set; }
        public int atk_pattern_16 { get; set; }
        public int atk_pattern_17 { get; set; }
        public int atk_pattern_18 { get; set; }
        public int atk_pattern_19 { get; set; }
        public int atk_pattern_20 { get; set; }


        public List<int> GetAllPatterns()
        {
            List<int> ids = new List<int>();
            PropertyInfo[] pArray = typeof(unit_attack_pattern).GetProperties();
            foreach (var p in pArray)
            {
                if (p.PropertyType == typeof(int) && p.Name.Contains("atk_pattern_"))
                {
                    int value = (int)(p.GetValue(this) ?? 0);
                    ids.Add(value);
                }
            }

            return ids;
        }

    }
    public class unit_skill_data
    {
        [PrimaryKey]
        public int unit_id { get; set; }
        public int union_burst { get; set; }
        public int main_skill_1 { get; set; }
        public int main_skill_2 { get; set; }
        public int main_skill_3 { get; set; }
        public int main_skill_4 { get; set; }
        public int main_skill_5 { get; set; }
        public int main_skill_6 { get; set; }
        public int main_skill_7 { get; set; }
        public int main_skill_8 { get; set; }
        public int main_skill_9 { get; set; }
        public int main_skill_10 { get; set; }
        public int ex_skill_1 { get; set; }
        public int ex_skill_2 { get; set; }
        public int ex_skill_3 { get; set; }
        public int ex_skill_4 { get; set; }
        public int ex_skill_5 { get; set; }
        public int ex_skill_evolution_1 { get; set; }
        public int ex_skill_evolution_2 { get; set; }
        public int ex_skill_evolution_3 { get; set; }
        public int ex_skill_evolution_4 { get; set; }
        public int ex_skill_evolution_5 { get; set; }
        public int sp_union_burst { get; set; }
        public int sp_skill_1 { get; set; }
        public int sp_skill_2 { get; set; }
        public int sp_skill_3 { get; set; }
        public int sp_skill_4 { get; set; }
        public int sp_skill_5 { get; set; }
        public int union_burst_evolution { get; set; }
        public int main_skill_evolution_1 { get; set; }
        public int main_skill_evolution_2 { get; set; }
        public int sp_skill_evolution_1 { get; set; }
        public int sp_skill_evolution_2 { get; set; }

        public List<int> GetAllSkillIDs()
        {
            List<int> ids = new List<int>();
            PropertyInfo[] pArray = typeof(unit_skill_data).GetProperties();
            foreach (var p in pArray)
            {
                if (p.PropertyType == typeof(int) && p.Name != "unit_id")
                {
                    int value = (int)(p.GetValue(this) ?? 0);
                    if (value > 0)
                        ids.Add(value);
                }
            }

            return ids;
        }
        public List<int> GetMainSkillIDs()
        {
            List<int> ids = new List<int>();
            PropertyInfo[] pArray = typeof(unit_skill_data).GetProperties();
            foreach (var p in pArray)
            {
                if (p.PropertyType == typeof(int) && p.Name.Contains("main_skill_"))
                {
                    int value = (int)(p.GetValue(this) ?? 0);
                    ids.Add(value);
                }
            }

            return ids;
        }
    }
    public class skill_data
    {
        [PrimaryKey]
        public int skill_id { get; set; }
        public string name { get; set; }
        public int skill_type { get; set; }
        public int skill_area_width { get; set; }
        public double skill_cast_time { get; set; }
        public int boss_ub_cool_time { get; set; }
        public int action_1 { get; set; }
        public int action_2 { get; set; }
        public int action_3 { get; set; }
        public int action_4 { get; set; }
        public int action_5 { get; set; }
        public int action_6 { get; set; }
        public int action_7 { get; set; }
        public int depend_action_1 { get; set; }
        public int depend_action_2 { get; set; }
        public int depend_action_3 { get; set; }
        public int depend_action_4 { get; set; }
        public int depend_action_5 { get; set; }
        public int depend_action_6 { get; set; }
        public int depend_action_7 { get; set; }
        public string description { get; set; }
        public int icon_type { get; set; }
        public virtual List<int> GetAllActionIDs()
        {
            List<int> ids = new List<int>
            {
                action_1,action_2,action_3,action_4,
                action_5,action_6,action_7,0,0,0
            };           

            return ids;
        }
        public virtual List<int> GetAllDependActionIDs()
        {
            List<int> ids = new List<int>
            {
                depend_action_1,depend_action_2,depend_action_3,depend_action_4,
                depend_action_5,depend_action_6,depend_action_7,0,0,0
            };

            return ids;
        }
    }

    [Table("skill_data")]
    public class skill_data_new : skill_data
    {
        public int action_8 { get; set; }
        public int action_9 { get; set; }
        public int action_10 { get; set; }
        public int depend_action_8 { get; set; }
        public int depend_action_9 { get; set; }
        public int depend_action_10 { get; set; }

        public override List<int> GetAllActionIDs()
        {
            List<int> ids = new List<int>
            {
                action_1,action_2,action_3,action_4,
                action_5,action_6,action_7,action_8,
                action_9,action_10,
            };

            return ids;
        }
        public override List<int> GetAllDependActionIDs()
        {
            List<int> ids = new List<int>
            {
                depend_action_1,depend_action_2,depend_action_3,depend_action_4,
                depend_action_5,depend_action_6,depend_action_7,depend_action_8,
                depend_action_9,depend_action_10
            };

            return ids;
        }
    }
    public class skill_action
    {
        [PrimaryKey]
        public int action_id { get; set; }
        public int class_id { get; set; }
        public int action_type { get; set; }
        public int action_detail_1 { get; set; }
        public int action_detail_2 { get; set; }
        public int action_detail_3 { get; set; }
        public double action_value_1 { get; set; }
        public double action_value_2 { get; set; }
        public double action_value_3 { get; set; }
        public double action_value_4 { get; set; }
        public double action_value_5 { get; set; }
        public double action_value_6 { get; set; }
        public double action_value_7 { get; set; }
        public int target_assignment { get; set; }
        public int target_area { get; set; }
        public int target_range { get; set; }
        public int target_type { get; set; }
        public int target_number { get; set; }
        public int target_count { get; set; }
        public string description { get; set; }
        public string level_up_disp { get; set; }

        public int[] GetDE() => new int[3] { action_detail_1, action_detail_2, action_detail_3 };
        public double[] GetValues()
        {
            return new double[7]
            {
                action_value_1,
                action_value_2,
                action_value_3,
                action_value_4,
                action_value_5,
                action_value_6,
                action_value_7
            };
        }
    }
    public class equipment_enhance_data
    {
        public int promotion_level { get; set; }
        public int equipment_enhance_level { get; set; }
    }

    public class equipment_data : DbBaseData
    {
        [PrimaryKey]
        public int equipment_id { get; set; }
        public string equipment_name { get; set; }
        public string description { get; set; }
        public int promotion_level { get; set; }
        public bool craft_flg { get; set; }
        public int equipment_enhance_point { get; set; }
        public int sale_price { get; set; }
        public int require_level { get; set; }
        public int enable_donation { get; set; }
        public int display_item { get; set; }
        public int item_type { get; set; }
    }
    public class equipment_enhance_rate : DbBaseData
    {
        [PrimaryKey]
        public int equipment_id { get; set; }
        public string equipment_name { get; set; }
        public string description { get; set; }
        public int promotion_level { get; set; }
    }

    public class promotion_bonus : DbBaseData
    {
        public int unit_id { get; set; }
        public int promotion_level { get; set; }
    }
    public class unit_rarity : DbBaseData
    {
        [PrimaryKey]
        public int unit_id { get; set; }
        [PrimaryKey]
        public int rarity { get; set; }
        public double hp_growth { get; set; }
        public double atk_growth { get; set; }
        public double magic_str_growth { get; set; }
        public double def_growth { get; set; }
        public double magic_def_growth { get; set; }
        public double physical_critical_growth { get; set; }
        public double magic_critical_growth { get; set; }
        public double wave_hp_recovery_growth { get; set; }
        public double wave_energy_recovery_growth { get; set; }
        public double dodge_growth { get; set; }
        public double physical_penetrate_growth { get; set; }
        public double magic_penetrate_growth { get; set; }
        public double life_steal_growth { get; set; }
        public double hp_recovery_rate_growth { get; set; }
        public double energy_recovery_rate_growth { get; set; }
        public double energy_reduce_rate_growth { get; set; }
        public double accuracy_growth { get; set; }
        public int unit_material_id { get; set; }
        public int consume_num { get; set; }
        public int consume_gold { get; set; }
        public BaseData GetBaseDataGrowth()
        {
            return new BaseData(hp_growth, atk_growth, magic_str_growth, def_growth, magic_def_growth, physical_critical_growth,//0-5
            magic_critical_growth, wave_hp_recovery_growth, wave_energy_recovery_growth, dodge_growth,//6-9
            physical_penetrate_growth, magic_penetrate_growth, life_steal_growth, hp_recovery_rate_growth,//10-13
            energy_recovery_rate_growth, energy_reduce_rate_growth, accuracy_growth);
        }
    }
    public class unit_promotion
    {
        [PrimaryKey]
        public int unit_id { get; set; }
        [PrimaryKey]
        public int promotion_level { get; set; }
        public int equip_slot_1 { get; set; }
        public int equip_slot_2 { get; set; }
        public int equip_slot_3 { get; set; }
        public int equip_slot_4 { get; set; }
        public int equip_slot_5 { get; set; }
        public int equip_slot_6 { get; set; }

        public int[] GetEquips()
        {
            return new int[6]
            {
                equip_slot_1 , equip_slot_2 , equip_slot_3,
                equip_slot_4,equip_slot_5,equip_slot_6
            };
        }

    }
    public class unit_promotion_status : DbBaseData
    {
        [PrimaryKey]
        public int unit_id { get; set; }
        [PrimaryKey]
        public int promotion_level { get; set; }
    }
    public class unit_data
    {
        [PrimaryKey]
        public int unit_id { get; set; }
        public string unit_name { get; set; }
        public string kana { get; set; }
        public int prefab_id { get; set; }
        public int prefab_id_battle { get; set; }
        public int is_limited { get; set; }
        public int rarity { get; set; }
        public int motion_type { get; set; }
        public int se_type { get; set; }
        public int move_speed { get; set; }
        public int search_area_width { get; set; }
        public int atk_type { get; set; }
        public double normal_atk_cast_time { get; set; }
        public int cutin_1 { get; set; }
        public int cutin_2 { get; set; }
        public int cutin1_star6 { get; set; }
        public int cutin2_star6 { get; set; }
        public int guild_id { get; set; }
        public int exskill_display { get; set; }
        public string comment { get; set; }
        public int only_disp_owned { get; set; }
        public string start_time { get; set; }
        public string end_time { get; set; }
        public int original_unit_id { get; set; }



    }
    public class story_detail
    {
        public int story_id { get; set; }
        public int love_level { get; set; }
    }
    public class chara_story_status
    {
        [PrimaryKey]
        public int story_id { get; set; }
        public string unlock_story_name { get; set; }
        public int status_type_1 { get; set; }
        public int status_rate_1 { get; set; }
        public int status_type_2 { get; set; }
        public int status_rate_2 { get; set; }
        public int status_type_3 { get; set; }
        public int status_rate_3 { get; set; }
        public int status_type_4 { get; set; }
        public int status_rate_4 { get; set; }
        public int status_type_5 { get; set; }
        public int status_rate_5 { get; set; }
        public int chara_id_1 { get; set; }
        public int chara_id_2 { get; set; }
        public int chara_id_3 { get; set; }
        public int chara_id_4 { get; set; }
        public int chara_id_5 { get; set; }
        public int chara_id_6 { get; set; }
        public int chara_id_7 { get; set; }
        public int chara_id_8 { get; set; }
        public int chara_id_9 { get; set; }
        public int chara_id_10 { get; set; }

        public (int, int)[] GetStatesValueList()
        {
            return new (int, int)[]
            {
                ( status_type_1 , status_rate_1 ),
                ( status_type_2 , status_rate_2 ),
                ( status_type_3 , status_rate_3 ),
                ( status_type_4 , status_rate_4 ),
                ( status_type_5 , status_rate_5 )
            };
        }
        public List<int> GetCharIdList()
        {
            return new int[]
            {
                chara_id_1, chara_id_2, chara_id_3, chara_id_4, chara_id_5, chara_id_6, chara_id_7, chara_id_8,
                chara_id_9,
                chara_id_10
            }.Where(x => x != 0).Select(x => 100 * x + 1).ToList();
            /*
        List<int> ids = new List<int>();
        PropertyInfo[] pArray = typeof(chara_story_status).GetProperties();
        foreach (var p in pArray)
        {
            if (p.PropertyType == typeof(int) && p.Name.Contains("chara_id_"))
            {
                int value = (int)(p.GetValue(this) ?? 0);
                if (value > 0)
                {
                    int unitid = value * 100 + 1;
                    ids.Add(unitid);

                }
            }
        }

        return ids;*/
        }
    }
    public class unique_equipment_data : DbBaseData
    {
        [PrimaryKey]
        public int equipment_id { get; set; }
        public string equipment_name { get; set; }
        public string description { get; set; }
        public int promotion_level { get; set; }
        public int craft_flg { get; set; }
        public int equipment_enhance_point { get; set; }
        public int sale_price { get; set; }
        public int require_level { get; set; }
    }
    public class unique_equipment_enhance_rate : unique_equip_enhance_rate
    {
        public override int max_lv { get => -1; }
        public override int min_lv { get => 2; }
    }
    public class unit_unique_equip
    {
        public int unit_id { get; set; }
        public int equip_slot { get; set; }
        public int equip_id { get; set; }
    }

    public class unit_unique_equipment : unit_unique_equip
    {
    }

    public class unique_equip_enhance_rate : DbBaseData
    {
        public unique_equipment_data baseData;
        public virtual int min_lv { get; set; }
        public virtual int max_lv { get; set; }
        public int equipment_id { get; set; }
    }

}

