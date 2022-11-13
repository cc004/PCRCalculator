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
    /*
    public static class SQLData2
    {
        public static void ClearCache()
        {
            unit_Attack_PatternsDic = null;
            unit_Skill_DatasDic = null;
        }

        private static Dictionary<int,unit_skill_data> unit_Skill_DatasDic;
        private static Dictionary<int,List<unit_attack_pattern>> unit_Attack_PatternsDic;
        /// <summary>
        /// 获取数据，姬塔数据默认六星覆盖
        /// </summary>
        /// <param name="data"></param>
        /// <param name="dic"></param>
        /// <returns></returns>
        public static void GetUnitRarityData(this SQLiteTool data, Dictionary<int, UnitRarityData> dic)
        {
            //Dictionary<int, UnitRarityData> dic = new Dictionary<int, UnitRarityData>();
            Dictionary<int, List<unit_rarity>> rarityDic = data.GetDatasDicList<unit_rarity>(a => a.unit_id,a=>a.unit_id<=499999);
            
            if (unit_Skill_DatasDic == null)
                unit_Skill_DatasDic = data.GetDatasDic<unit_skill_data>(a => a.unit_id);
            if (unit_Attack_PatternsDic == null)            
                unit_Attack_PatternsDic = data.GetDatasDicList<unit_attack_pattern>(a => a.unit_id);
            
            Dictionary<int, List<unit_promotion>> equipDic = data.GetDatasDicList<unit_promotion>(a => a.unit_id);
            
            Dictionary<int, List<unit_promotion_status>> rankStateDic = data.GetDatasDicList<unit_promotion_status>(a => a.unit_id);
            
            Dictionary<int, unit_data> unitDic = data.GetDatasDic<unit_data>(a => a.unit_id);
            
            foreach(var pair in rarityDic)
            {
                List<int[]> eq = new List<int[]>();
                List<BaseData> datas1 = new List<BaseData>();
                try
                {
                    foreach (var t2 in equipDic[pair.Key])
                        eq.Add(t2.GetEquips());
                    foreach (var t3 in rankStateDic[pair.Key])
                        datas1.Add(t3.GetBaseData());
                }
                catch(KeyNotFoundException ex)
                {
                    Debug.LogError($"{pair.Key}的RANK数据丢失！");
                }
                UnitRankData rankData = new UnitRankData(eq, datas1);
                UnitDetailData detailData;
                try
                {
                    unit_data t4 = unitDic[pair.Key];
                    detailData = new UnitDetailData(t4.unit_id,
                        t4.unit_name, t4.rarity, t4.motion_type, t4.se_type,
                        t4.search_area_width, t4.atk_type, t4.normal_atk_cast_time, t4.guild_id);
                }
                catch(KeyNotFoundException ex)
                {
                    Debug.LogError($"{pair.Key}的DETAIL数据丢失！");
                    detailData = new UnitDetailData();
                }
                UnitSkillData skillData = new UnitSkillData();
                unit_skill_data sk = unit_Skill_DatasDic[pair.Key];
                if(pair.Key == 105701 && MainManager.Instance.useJapanData)
                {
                    sk = unit_Skill_DatasDic[170101];
                }
                skillData.UB = sk.union_burst;
                skillData.UB_ev = sk.union_burst_evolution;
                skillData.skill_1 = sk.main_skill_1;
                skillData.skill_1_ev = sk.main_skill_evolution_1;
                skillData.skill_2 = sk.main_skill_2;
                skillData.skill_2_ev = sk.main_skill_evolution_2;
                skillData.skill_3 = sk.main_skill_3;
                skillData.skill_4 = sk.main_skill_4;
                skillData.skill_5 = sk.main_skill_5;
                skillData.skill_6 = sk.main_skill_6;
                skillData.skill_7 = sk.main_skill_7;
                skillData.skill_8 = sk.main_skill_8;
                skillData.skill_9 = sk.main_skill_9;
                skillData.skill_10 = sk.main_skill_10;
                skillData.EXskill = sk.ex_skill_1;
                skillData.EXskill_ev = sk.ex_skill_evolution_1;
                skillData.EXskill_2 = sk.ex_skill_2;
                skillData.EXskill_ev2 = sk.ex_skill_evolution_2;
                skillData.EXskill_3 = sk.ex_skill_3;
                skillData.EXskill_ev3 = sk.ex_skill_evolution_3;
                skillData.EXskill_4 = sk.ex_skill_4;
                skillData.EXskill_ev4 = sk.ex_skill_evolution_4;
                skillData.EXskill_5 = sk.ex_skill_5;
                skillData.EXskill_ev5 = sk.ex_skill_evolution_5;
                skillData.SPskill_1 = sk.sp_skill_1;
                skillData.SPskill_1_ev = sk.sp_skill_evolution_1;
                skillData.SPskill_2 = sk.sp_skill_2;
                skillData.SPskill_2_ev = sk.sp_skill_evolution_2;
                skillData.SPskill_3 = sk.sp_skill_3;
                skillData.SPskill_4 = sk.sp_skill_4;
                skillData.SPskill_5 = sk.sp_skill_5;
                skillData.SPUB = sk.sp_union_burst;
                unit_attack_pattern pattern = unit_Attack_PatternsDic[pair.Key][0];
                skillData.loopStart = pattern.loop_start;
                skillData.loopEnd = pattern.loop_end;
                skillData.atkPatterns = pattern.GetAllPatterns().ToArray();
                List<BaseData> d1 = new List<BaseData>();
                List<BaseData> d2 = new List<BaseData>();
                foreach(var t6 in pair.Value)
                {
                    d1.Add(t6.GetBaseData());
                    d2.Add(t6.GetBaseDataGrowth());
                }
                UnitRarityData rarityData = new UnitRarityData(pair.Key, d1, d2, rankData, detailData, skillData);
                dic.Add(pair.Key, rarityData);
            }

        }
        public static void GetUnitStoryData(this SQLiteTool data, Dictionary<int, UnitStoryData> unitStoryDic, Dictionary<int, List<int>> unitStoryEffectDic)
        {
            Dictionary<int, List<List<int[]>>> add_vals = new Dictionary<int, List<List<int[]>>>();
            Dictionary<int, List<int>> ef_id = new Dictionary<int, List<int>>();
            var list = data.GetDatas<chara_story_status>();

            foreach(var dd in list)
            {
                int storyid = dd.story_id;
                int unitid = Mathf.FloorToInt(storyid / 1000) * 100 + 1;
                if (!add_vals.ContainsKey(unitid))
                {
                    add_vals.Add(unitid, new List<List<int[]>>());
                    ef_id[unitid] = dd.GetCharIdList();                   
                }
                List<int[]> li = dd.GetStatesValueList();
                add_vals[unitid].Add(li);
            }
            foreach (int id in add_vals.Keys)
            {
                UnitStoryData unitStory = new UnitStoryData(id, add_vals[id].Count + 1, ef_id[id], add_vals[id]);
                unitStoryDic[id] = unitStory;
                if (!unitStoryEffectDic.ContainsKey(id))
                {
                    unitStoryEffectDic.Add(id, new List<int>());
                }
                foreach (int efid in unitStory.effectCharacters)
                {
                    if (!unitStoryEffectDic[id].Contains(efid))
                    {
                        unitStoryEffectDic[id].Add(efid);
                    }

                }
            }
        }
        
        public static Dictionary<int, SkillData> GetSkillDataDic(this SQLiteTool data)
        {
            Dictionary<int, SkillData> dic = new Dictionary<int, SkillData>(); 
            var list = data.GetDatas<skill_data>();
            foreach(var dd in list)
            {
                SkillData sk = new SkillData(dd.skill_id, dd.name, dd.skill_type,
                    dd.skill_area_width, dd.skill_cast_time, dd.GetAllActionIDs().ToArray(),
                    dd.GetAllDependActionIDs().ToArray(), dd.description, dd.icon_type);
                dic.Add(sk.skillid, sk);
            }
            return dic;
        }
        public static Dictionary<int, SkillAction> GetSkillActionDic(this SQLiteTool data)
        {
            Dictionary<int, SkillAction> dic = new Dictionary<int, SkillAction>();
            var list = data.GetDatas<skill_action>();
            foreach (var dd in list)
            {
                SkillAction action = new SkillAction(dd.action_id, dd.class_id,
                    dd.action_type, dd.GetDE(), dd.GetValues(), dd.target_assignment,
                    dd.target_area, dd.target_range, dd.target_type, dd.target_number, dd.target_count,
                    dd.description, dd.level_up_disp);
                dic.Add(action.actionid, action);
            }
            return dic;
        }
        public static Dictionary<int, UnitAttackPattern> GetUnitAttackPatternDic(this SQLiteTool data)
        {
            Dictionary<int, UnitAttackPattern> dic = new Dictionary<int, UnitAttackPattern>();
            var list = data.GetDatas<unit_attack_pattern>();
            foreach(var dd in list)
            {

                UnitAttackPattern attackPattern = new UnitAttackPattern(dd.pattern_id, dd.unit_id, dd.loop_start, dd.loop_end, dd.GetAllPatterns().ToArray());
                dic.Add(dd.pattern_id, attackPattern);
            }
            return dic;
        }
        public static Dictionary<int, GuildEnemyData> GetGuildEnemyData(this SQLiteTool data)
        {
            var waveDic = data.GetDatasDic<wave_group_data>(a => a.wave_group_id);
            Dictionary<int, GuildEnemyData> dic = new Dictionary<int, GuildEnemyData>();
            var list = data.GetDatas<clan_battle_2_map_data>();
            Dictionary<int, List<clan_battle_2_map_data>> dic2 = new Dictionary<int, List<clan_battle_2_map_data>>();
            foreach(var tt in list)
            {
                if(dic2.TryGetValue(tt.clan_battle_id,out var list2))
                {
                    list2.Add(tt);
                }
                else
                {
                    dic2.Add(tt.clan_battle_id, new List<clan_battle_2_map_data> { tt });
                }
            }
            foreach (var pair in dic2)
            {
                GuildEnemyData enemyData = new GuildEnemyData();
                enemyData.enemyIds = new int[pair.Value.Count][];
                for (int i = 0; i < pair.Value.Count; i++)
                {
                    var t = pair.Value[i];
                    enemyData.enemyIds[i] = new int[5]
                    {
                        waveDic[t.wave_group_id_1].enemy_id_1,
                        waveDic[t.wave_group_id_2].enemy_id_1,
                        waveDic[t.wave_group_id_3].enemy_id_1,
                        waveDic[t.wave_group_id_4].enemy_id_1,
                        waveDic[t.wave_group_id_5].enemy_id_1,
                    };
                }
                dic.Add(pair.Key, enemyData);

            }
            return dic;
        }
        public static Dictionary<int, MasterEnemyMParts.EnemyMParts> GetMPartsData(this SQLiteTool data)
        {
            Dictionary<int, MasterEnemyMParts.EnemyMParts> dic = new Dictionary<int, MasterEnemyMParts.EnemyMParts>();
            var list = data.GetDatas<enemy_m_parts>();
            foreach(var dd in list)
            {
                dic.Add(dd.enemy_id, new MasterEnemyMParts.EnemyMParts(dd.enemy_id,dd.child_enemy_parameter_1, dd.child_enemy_parameter_2, dd.child_enemy_parameter_3, dd.child_enemy_parameter_4, dd.child_enemy_parameter_5));
            }
            return dic;
        }
        public static void GetEnemyDataDic(this SQLiteTool data, Dictionary<int, EnemyData> dic)
        {
            //Dictionary<int, EnemyData> dic = new Dictionary<int, EnemyData>();
            Dictionary<int, unit_enemy_data> dic1 = data.GetDatasDic<unit_enemy_data>(a => a.unit_id);
            
            var list = data.GetDatas<enemy_parameter>(a=>a.enemy_id >= 400000000 && a.enemy_id <= 500000000);
            if (unit_Skill_DatasDic == null)
                unit_Skill_DatasDic = data.GetDatasDic<unit_skill_data>(a=>a.unit_id);
            if (unit_Attack_PatternsDic == null)
            {
                unit_Attack_PatternsDic = data.GetDatasDicList<unit_attack_pattern>(a => a.unit_id);
                /*var  unit_Attack_Patterns = data.GetDatas<unit_attack_pattern>();
                foreach(var pa in unit_Attack_Patterns)
                {
                    if (unit_Attack_PatternsDic.TryGetValue(pa.unit_id, out var list1))
                    {
                        list1.Add(pa);
                    }
                    else
                        unit_Attack_PatternsDic[pa.unit_id] = new List<unit_attack_pattern> { pa };
                }
            }
            int idx = 0;
            foreach(var dd in list)
            {
                EnemyData e = new EnemyData();
                e.enemy_id = dd.enemy_id;
                e.unit_id = dd.unit_id;
                e.name = dd.name;
                e.level = dd.level;
                e.rarity = dd.rarity;
                e.promotion_level = dd.promotion_level;
                e.baseData = dd.GetBaseData();
                e.union_burst_level = dd.union_burst_level;
                e.main_skill_lvs = new List<int>
                {
                    dd.main_skill_lv_1,dd.main_skill_lv_2,dd.main_skill_lv_3,dd.main_skill_lv_4,
                    dd.main_skill_lv_5,dd.main_skill_lv_6,dd.main_skill_lv_7,dd.main_skill_lv_8,
                    dd.main_skill_lv_9,dd.main_skill_lv_10
                };
                e.ex_skill_lvs = new List<int>
                {
                    dd.ex_skill_lv_1,dd.ex_skill_lv_2,dd.ex_skill_lv_3,dd.ex_skill_lv_4,dd.ex_skill_lv_5,
                };
                e.resist_status_id = dd.resist_status_id;
                e.resist_variation_id = dd.resist_variation_id;
                e.unique_equipment_flag_1 = dd.unique_equipment_flag_1;
                e.break_durability = dd.break_durability;
                e.virtual_hp = dd.virtual_hp;
                EnemyDetailData detailData = new EnemyDetailData();
                unit_enemy_data tp2 = dic1[dd.unit_id];
                detailData.unit_id = tp2.unit_id;
                detailData.unit_name = tp2.unit_name;
                detailData.prefab_id = tp2.prefab_id;
                detailData.motion_type = tp2.motion_type;
                detailData.se_type = tp2.se_type;
                detailData.move_speed = tp2.move_speed;
                detailData.search_area_width = tp2.search_area_width;
                detailData.atk_type = tp2.atk_type;
                detailData.normal_atk_cast_time = tp2.normal_atk_cast_time;
                detailData.cutin = tp2.cutin;
                detailData.visual_change_flag = tp2.visual_change_flag;
                detailData.comment = tp2.comment;
                e.detailData = detailData;
                EnemySkillData skillData = new EnemySkillData();

                var skList = unit_Skill_DatasDic[e.unit_id];//data.GetDatas<unit_skill_data>(a => a.unit_id == e.unit_id)[0];
                skillData.UB = skList.union_burst;
                skillData.MainSkills = skList.GetMainSkillIDs();
                skillData.enemyAttackPatterns = new List<UnitAttackPattern>();
                var list0 = unit_Attack_PatternsDic[e.unit_id];//data.GetDatas<unit_attack_pattern>(a=>a.unit_id == e.unit_id);
                foreach (var dd3 in list0)
                {
                    UnitAttackPattern attackPattern = new UnitAttackPattern(dd3.pattern_id, dd3.unit_id, dd3.loop_start, dd3.loop_end, dd3.GetAllPatterns().ToArray());
                    skillData.enemyAttackPatterns.Add(attackPattern);
                }
                e.skillData = skillData;

                dic.Add(e.enemy_id, e);
                idx++;
                    
            }
            //return dic;
        }
        public static Dictionary<int, EquipmentData> GetEquipmentData(this SQLiteTool data)
        {
            Dictionary<int, EquipmentData> dic = new Dictionary<int, EquipmentData>();
            var list = data.GetDatas<equipment_data>();
            var dic2 = data.GetDatasDic<equipment_enhance_rate>(a=>a.equipment_id);
            foreach(var dd in list)
            {
                EquipmentData equipment = new EquipmentData();
                equipment.equipment_id = dd.equipment_id;
                equipment.equipment_name = dd.equipment_name;
                equipment.promotion_level = dd.promotion_level;
                equipment.description = dd.description;
                equipment.craftFlg = dd.craft_flg;
                equipment.equipmentEnhancePoint = dd.equipment_enhance_point;
                equipment.salePrice = dd.sale_price;
                equipment.requireLevel = dd.require_level;
                equipment.data = dd.GetBaseData();
                equipment.data_rate = dic2.ContainsKey(dd.equipment_id) ? dic2[dd.equipment_id].GetBaseData() : new BaseData();
                dic.Add(dd.equipment_id, equipment);
            }
            return dic;
        }
        public static Dictionary<int, UniqueEquipmentData> GetUEQData(this SQLiteTool data)
        {
            Dictionary<int, UniqueEquipmentData> dic = new Dictionary<int, UniqueEquipmentData>();
            var list = data.GetDatas<unique_equipment_data>();
            foreach(var dd in list)
            {
                UniqueEquipmentData unique = new UniqueEquipmentData();
                unique.equipment_id = dd.equipment_id;
                unique.baseValue = dd.GetBaseData();
                int unitid = (unique.equipment_id - 130001) * 10 + 100001;
                dic[unitid] = unique;
            }
            var list2 = data.GetDatas<unique_equipment_enhance_rate>();
            foreach(var dd in list2)
            {
                int equipment_id = dd.equipment_id;
                int unitid = (equipment_id - 130001) * 10 + 100001;
                dic[unitid].enhanceValue = dd.GetBaseData();
            }
            return dic;
        }

        public static Dictionary<int, string> GetUnitName_cn(this SQLiteTool data)
        {
            Dictionary<int, string> dic = new Dictionary<int, string>();
            var list = data.GetDatas<unit_data>();
            foreach(var dd in list)
            {
                dic.Add(dd.unit_id, dd.unit_name);
            }
            return dic;
        }
        public static Dictionary<int, string[]> GetSkillName_cn(this SQLiteTool data)
        {
            Dictionary<int, string[]> dic = new Dictionary<int, string[]>();
            var list = data.GetDatas<skill_data>();
            foreach (var dd in list)
            {
                dic.Add(dd.skill_id, new string[2] { dd.name,dd.description});
            }
            return dic;
        }
        public static Dictionary<int, string> GetSkillActionDes_cn(this SQLiteTool data)
        {
            Dictionary<int, string> dic = new Dictionary<int, string>();
            var list = data.GetDatas<skill_action>();
            foreach (var dd in list)
            {
                dic.Add(dd.action_id, dd.description);
            }
            return dic;
        }
    }*/
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
            energy_recovery_rate, energy_reduce_rate, accuracy);
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
        public float normal_atk_cast_time { get; set; }
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
        public float skill_cast_time { get; set; }
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
        public List<int> GetAllActionIDs()
        {
            List<int> ids = new List<int>
            {
                action_1,action_2,action_3,action_4,
                action_5,action_6,action_7
            };           

            return ids;
        }
        public List<int> GetAllDependActionIDs()
        {
            List<int> ids = new List<int>
            {
                depend_action_1,depend_action_2,depend_action_3,depend_action_4,
                depend_action_5,depend_action_6,depend_action_7
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
        public float action_value_1 { get; set; }
        public float action_value_2 { get; set; }
        public float action_value_3 { get; set; }
        public float action_value_4 { get; set; }
        public float action_value_5 { get; set; }
        public float action_value_6 { get; set; }
        public float action_value_7 { get; set; }
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
    public class equipment_data
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
        public float hp { get; set; }
        public float atk { get; set; }
        public float magic_str { get; set; }
        public float def { get; set; }
        public float magic_def { get; set; }
        public float physical_critical { get; set; }
        public float magic_critical { get; set; }
        public float wave_hp_recovery { get; set; }
        public float wave_energy_recovery { get; set; }
        public float dodge { get; set; }
        public float physical_penetrate { get; set; }
        public float magic_penetrate { get; set; }
        public float life_steal { get; set; }
        public float hp_recovery_rate { get; set; }
        public float energy_recovery_rate { get; set; }
        public float energy_reduce_rate { get; set; }
        public int enable_donation { get; set; }
        public float accuracy { get; set; }

        public int display_item { get; set; }
        public int item_type { get; set; }

        public BaseData GetBaseData()
        {
            return new BaseData(hp, atk, magic_str, def, magic_def, physical_critical,//0-5
            magic_critical, wave_hp_recovery, wave_energy_recovery, dodge,//6-9
            physical_penetrate, magic_penetrate, life_steal, hp_recovery_rate,//10-13
            energy_recovery_rate, energy_reduce_rate, accuracy);
        }
    }
    public class equipment_enhance_rate
    {
        [PrimaryKey]
        public int equipment_id { get; set; }
        public string equipment_name { get; set; }
        public string description { get; set; }
        public int promotion_level { get; set; }
        public float hp { get; set; }
        public float atk { get; set; }
        public float magic_str { get; set; }
        public float def { get; set; }
        public float magic_def { get; set; }
        public float physical_critical { get; set; }
        public float magic_critical { get; set; }
        public float wave_hp_recovery { get; set; }
        public float wave_energy_recovery { get; set; }
        public float dodge { get; set; }
        public float physical_penetrate { get; set; }
        public float magic_penetrate { get; set; }
        public float life_steal { get; set; }
        public float hp_recovery_rate { get; set; }
        public float energy_recovery_rate { get; set; }
        public float energy_reduce_rate { get; set; }
        public float accuracy { get; set; }
        public BaseData GetBaseData()
        {
            return new BaseData(hp, atk, magic_str, def, magic_def, physical_critical,//0-5
            magic_critical, wave_hp_recovery, wave_energy_recovery, dodge,//6-9
            physical_penetrate, magic_penetrate, life_steal, hp_recovery_rate,//10-13
            energy_recovery_rate, energy_reduce_rate, accuracy);
        }
    }
    public class unit_rarity
    {
        [PrimaryKey]
        public int unit_id { get; set; }
        [PrimaryKey]
        public int rarity { get; set; }
        public float hp { get; set; }
        public float atk { get; set; }
        public float magic_str { get; set; }
        public float def { get; set; }
        public float magic_def { get; set; }
        public float physical_critical { get; set; }
        public float magic_critical { get; set; }
        public float wave_hp_recovery { get; set; }
        public float wave_energy_recovery { get; set; }
        public float dodge { get; set; }
        public float physical_penetrate { get; set; }
        public float magic_penetrate { get; set; }
        public float life_steal { get; set; }
        public float hp_recovery_rate { get; set; }
        public float energy_recovery_rate { get; set; }
        public float energy_reduce_rate { get; set; }
        public float accuracy { get; set; }
        public float hp_growth { get; set; }
        public float atk_growth { get; set; }
        public float magic_str_growth { get; set; }
        public float def_growth { get; set; }
        public float magic_def_growth { get; set; }
        public float physical_critical_growth { get; set; }
        public float magic_critical_growth { get; set; }
        public float wave_hp_recovery_growth { get; set; }
        public float wave_energy_recovery_growth { get; set; }
        public float dodge_growth { get; set; }
        public float physical_penetrate_growth { get; set; }
        public float magic_penetrate_growth { get; set; }
        public float life_steal_growth { get; set; }
        public float hp_recovery_rate_growth { get; set; }
        public float energy_recovery_rate_growth { get; set; }
        public float energy_reduce_rate_growth { get; set; }
        public float accuracy_growth { get; set; }
        public int unit_material_id { get; set; }
        public int consume_num { get; set; }
        public int consume_gold { get; set; }
        public BaseData GetBaseData()
        {
            return new BaseData(hp, atk, magic_str, def, magic_def, physical_critical,//0-5
            magic_critical, wave_hp_recovery, wave_energy_recovery, dodge,//6-9
            physical_penetrate, magic_penetrate, life_steal, hp_recovery_rate,//10-13
            energy_recovery_rate, energy_reduce_rate, accuracy);
        }
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
    public class unit_promotion_status
    {
        [PrimaryKey]
        public int unit_id { get; set; }
        [PrimaryKey]
        public int promotion_level { get; set; }
        public float hp { get; set; }
        public float atk { get; set; }
        public float magic_str { get; set; }
        public float def { get; set; }
        public float magic_def { get; set; }
        public float physical_critical { get; set; }
        public float magic_critical { get; set; }
        public float wave_hp_recovery { get; set; }
        public float wave_energy_recovery { get; set; }
        public float dodge { get; set; }
        public float physical_penetrate { get; set; }
        public float magic_penetrate { get; set; }
        public float life_steal { get; set; }
        public float hp_recovery_rate { get; set; }
        public float energy_recovery_rate { get; set; }
        public float energy_reduce_rate { get; set; }
        public float accuracy { get; set; }
        public BaseData GetBaseData()
        {
            return new BaseData(hp, atk, magic_str, def, magic_def, physical_critical,//0-5
            magic_critical, wave_hp_recovery, wave_energy_recovery, dodge,//6-9
            physical_penetrate, magic_penetrate, life_steal, hp_recovery_rate,//10-13
            energy_recovery_rate, energy_reduce_rate, accuracy);
        }

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
        public float normal_atk_cast_time { get; set; }
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

        public List<int[]> GetStatesValueList()
        {
            return new List<int[]>
            {
                new int[2]{ status_type_1 , status_rate_1 },
                new int[2]{ status_type_2 , status_rate_2 },
                new int[2]{ status_type_3 , status_rate_3 },
                new int[2]{ status_type_4 , status_rate_4 },
                new int[2]{ status_type_5 , status_rate_5 }
            };
        }
        public List<int> GetCharIdList()
        {
            return new List<int>()
            {
                chara_id_1, chara_id_2, chara_id_3, chara_id_4, chara_id_5, chara_id_6, chara_id_7, chara_id_8,
                chara_id_9,
                chara_id_10
            };
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
    public class unique_equipment_data
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
        public float hp { get; set; }
        public float atk { get; set; }
        public float magic_str { get; set; }
        public float def { get; set; }
        public float magic_def { get; set; }
        public float physical_critical { get; set; }
        public float magic_critical { get; set; }
        public float wave_hp_recovery { get; set; }
        public float wave_energy_recovery { get; set; }
        public float dodge { get; set; }
        public float physical_penetrate { get; set; }
        public float magic_penetrate { get; set; }
        public float life_steal { get; set; }
        public float hp_recovery_rate { get; set; }
        public float energy_recovery_rate { get; set; }
        public float energy_reduce_rate { get; set; }
        public float accuracy { get; set; }
        public BaseData GetBaseData()
        {
            return new BaseData(hp, atk, magic_str, def, magic_def, physical_critical,//0-5
            magic_critical, wave_hp_recovery, wave_energy_recovery, dodge,//6-9
            physical_penetrate, magic_penetrate, life_steal, hp_recovery_rate,//10-13
            energy_recovery_rate, energy_reduce_rate, accuracy);
        }

    }
    public class unique_equipment_enhance_rate
    {
        [PrimaryKey]
        public int equipment_id { get; set; }
        public string equipment_name { get; set; }
        public string description { get; set; }
        public int promotion_level { get; set; }
        public float hp { get; set; }
        public float atk { get; set; }
        public float magic_str { get; set; }
        public float def { get; set; }
        public float magic_def { get; set; }
        public float physical_critical { get; set; }
        public float magic_critical { get; set; }
        public float wave_hp_recovery { get; set; }
        public float wave_energy_recovery { get; set; }
        public float dodge { get; set; }
        public float physical_penetrate { get; set; }
        public float magic_penetrate { get; set; }
        public float life_steal { get; set; }
        public float hp_recovery_rate { get; set; }
        public float energy_recovery_rate { get; set; }
        public float energy_reduce_rate { get; set; }
        public float accuracy { get; set; }
        public BaseData GetBaseData()
        {
            return new BaseData(hp, atk, magic_str, def, magic_def, physical_critical,//0-5
            magic_critical, wave_hp_recovery, wave_energy_recovery, dodge,//6-9
            physical_penetrate, magic_penetrate, life_steal, hp_recovery_rate,//10-13
            energy_recovery_rate, energy_reduce_rate, accuracy);
        }

    }

}

