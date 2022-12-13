using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQLite4Unity3d;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Elements;
using PCRCaculator.Guild;
#if !UNITY_EDITOR
using System.IO;
#endif

namespace PCRCaculator.SQL
{
    public class SQLiteTool
    {
        public static string DatabaseName = "redive_jp.db";
        public static string DatabaseName_cn = "redive_cn.db";

        public static string GetDBPath(bool cn = false)
        {
            var DatabaseName = cn ? DatabaseName_cn : SQLiteTool.DatabaseName;
#if UNITY_EDITOR
            var loadDb = string.Format(@"Assets/StreamingAssets/{0}", DatabaseName);
#else
        // check if file exists in Application.persistentDataPath
        //var filepath = string.Format("{0}/{1}", Application.persistentDataPath, DatabaseName);
        
            Debug.Log("Database not in Persistent path");
            // if it doesn't ->
            // open StreamingAssets directory and load the db ->

#if UNITY_ANDROID 
            var loadDb2 = new WWW("jar:file://" + Application.dataPath + "!/assets/" + DatabaseName);  // this is the path to your StreamingAssets in android
            while (!loadDb.isDone) { }  // CAREFUL here, for safety reasons you shouldn't let this while loop unattended, place a timer and error check
            // then save to Application.persistentDataPath
            var loadDb = string.Format("{0}/{1}", Application.persistentDataPath, DatabaseName);
            File.WriteAllBytes(loadDb, loadDb.bytes);
#elif UNITY_IOS
                 var loadDb = Application.dataPath + "/Raw/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
                // then save to Application.persistentDataPath
                //File.Copy(loadDb, filepath);
#elif UNITY_WP8
                var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
                // then save to Application.persistentDataPath
               // File.Copy(loadDb, filepath);

#elif UNITY_WINRT
		var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
		// then save to Application.persistentDataPath
		//File.Copy(loadDb, filepath);
		
#elif UNITY_STANDALONE_OSX
		var loadDb = Application.dataPath + "/Resources/Data/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
		// then save to Application.persistentDataPath
		//File.Copy(loadDb, filepath);
#else
	var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
	// then save to Application.persistentDataPath
	//File.Copy(loadDb, filepath);

#endif

            Debug.Log("Database written");

        //var dbPath = filepath;
#endif
            return loadDb;
        }

        
        protected SQLiteTool(string path, bool readOnly)
        {
            ConnectDB(path, readOnly);
        }

        private string path;
        private bool @readonly;
        private List<SQLiteConnection> connections = new List<SQLiteConnection>();
        private void ConnectDB(string path, bool readOnly)
        {
            // Get an absolute path to the database file
            //var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "MyData.db");
            this.path = path;
            this.@readonly = readOnly;
        }
        public IEnumerable<T> GetDatas<T>(Func<T, bool> func = null) where T : new()
        {
            var conn = new SQLiteConnection(path, @readonly ? SQLiteOpenFlags.ReadOnly : SQLiteOpenFlags.ReadWrite);
            lock (connections) connections.Add(conn);
            return func == null ? conn.Table<T>() : conn.Table<T>().Where(func);
        }
        public Dictionary<int, T> GetDatasDic<T>(Func<T, int> func, Func<T, bool> select = null) where T : new()
        {
            Dictionary<int, T> dict = new Dictionary<int, T>();
            var list = GetDatas<T>(select);
            foreach (var item in list)
                dict[func(item)] = item;
            return dict;
        }

        public Dictionary<int, List<T>> GetDatasDicList<T>(Func<T, int> func, Func<T, bool> select = null) where T : new()
        {
            Dictionary<int, List<T>> dic = new Dictionary<int, List<T>>();
            var list = GetDatas<T>(select);
            foreach (var dd in list)
            {
                if (dic.TryGetValue(func(dd), out var list1))
                {
                    list1.Add(dd);
                }
                else
                    dic[func(dd)] = new List<T> { dd };
            }
            return dic;
        }
        /*
        public int UpdateDB(List<object> list)
        {
            return db.UpdateAll(list);
        }
        public void Commit()
        {
            db.Commit();
        }*/
        public void CloseDB()
        {
            foreach  (var conn in connections) conn.Close();
        }
    }

    public sealed class SQLData : SQLiteTool
    {
        private Dictionary<int, List<unit_rarity>> rarityDic;
        private Dictionary<int, unit_skill_data> unitSkillDataDic;
        private Dictionary<int, List<unit_attack_pattern>> unitAttackPatternDic;
        private Dictionary<int, List<unit_promotion>> equipDic;
        private Dictionary<int, List<unit_promotion_status>> rankStateDic;
        private Dictionary<int, unit_data> unitDic;
        private Dictionary<int, unit_enemy_data> dic1;
        private Dictionary<int, equipment_enhance_rate> dic2;
        private Dictionary<int, wave_group_data> waveDic;

        public (Dictionary<int, UnitStoryData>, Dictionary<int, List<int>>) Pair { get; private set; }
        public Dictionary<int, UnitRarityData> Dic1 { get; private set; }
        public Dictionary<int, EnemyData> Dic2 { get; private set; }
        public Dictionary<int, SkillData> Dic3 { get; private set; }
        public Dictionary<int, SkillAction> Dic4 { get; private set; }
        public Dictionary<int, UnitAttackPattern> Dic5 { get; private set; }
        public Dictionary<int, GuildEnemyData> Dic6 { get; private set; }
        public Dictionary<int, MasterEnemyMParts.EnemyMParts> Dic7 { get; private set; }
        public Dictionary<int, EquipmentData> Dic8 { get; private set; }
        public Dictionary<int, UniqueEquipmentData> Dic9 { get; private set; }
        public Dictionary<int, string> Dic10 { get; private set; }
        public Dictionary<int, string[]> Dic11 { get; private set; }
        public Dictionary<int, string> Dic12 { get; private set; }

        private string path;
        private bool @readonly;

        private SQLData(string path, bool readOnly) : base(path, readOnly)
        {
        }

        public static SQLData OpenDB(bool cn = false)
        {

            //_connection = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
            //Debug.Log("Final PATH: " + dbPath);
            var temp = new SQLData(GetDBPath(cn), true);
            return temp;
        }

        // TRAP: this method uses thread static field, so make sure not to run the method two times at once
        public Task ParallelGetAll()
        {
            var rarityDicTask = Task.Run(() =>
            {
                rarityDic = GetDatasDicList<unit_rarity>(a => a.unit_id, a => a.unit_id <= 499999);
            });
            var unitSkillDataDicTask = Task.Run(() =>
            {
                unitSkillDataDic = GetDatasDic<unit_skill_data>(a => a.unit_id);
            });
            var unitAttackPatternDicTask = Task.Run(() =>
            {
                unitAttackPatternDic = GetDatasDicList<unit_attack_pattern>(a => a.unit_id);
            });
            var equipDicTask = Task.Run(() =>
            {
                equipDic = GetDatasDicList<unit_promotion>(a => a.unit_id);
            });
            var rankStateDicTask = Task.Run(() =>
            {
                rankStateDic = GetDatasDicList<unit_promotion_status>(a => a.unit_id);
            });
            var unitDicTask = Task.Run(() =>
            {
                unitDic = GetDatasDic<unit_data>(a => a.unit_id);
            });
            var dic1Task = Task.Run(() =>
            {
                dic1 = GetDatasDic<unit_enemy_data>(a => a.unit_id);
            });
            var dic2Task = Task.Run(() =>
            {
                dic2 = GetDatasDic<equipment_enhance_rate>(a => a.equipment_id);
            });
            var waveDicTask = Task.Run(() =>
            {
                waveDic = GetDatasDic<wave_group_data>(a => a.wave_group_id);
            });

            var tasks = new List<Task>();

            tasks.Add(
                Task.WhenAll(rarityDicTask, unitSkillDataDicTask, unitAttackPatternDicTask, equipDicTask,
                        rankStateDicTask, unitDicTask)
                .ContinueWith(_ => Dic1 = GetUnitRarityData()));
            tasks.Add(
                Task.WhenAll(dic1Task, unitSkillDataDicTask, unitAttackPatternDicTask)
                    .ContinueWith(_ => Dic2 = GetEnemyDataDic()));
            tasks.Add(
                Task.Run(() => Dic3 = GetSkillDataDic()));
            tasks.Add(
                Task.Run(() => Dic4 = GetSkillActionDic()));
            tasks.Add(unitAttackPatternDicTask.ContinueWith(
                _ => Dic5 = GetUnitAttackPatternDic()));
            tasks.Add(waveDicTask.ContinueWith(
                _ => Dic6 = GetGuildEnemyData()));
            tasks.Add(
                Task.Run(() => Dic7 = GetMPartsData()));
            tasks.Add(dic2Task.ContinueWith(
                _ => Dic8 = GetEquipmentData()));
            tasks.Add(
                Task.Run(() => Dic9 = GetUEQData()));
            tasks.Add(
                Task.Run(() => Pair = GetUnitStoryData()));
            tasks.Add(
                Task.Run(() => Dic10 = GetUnitName_cn()));
            tasks.Add(
                Task.Run(() => Dic11 = GetSkillName_cn()));
            tasks.Add(
                Task.Run(() => Dic12 = GetSkillActionDes_cn()));
            return Task.WhenAll(tasks);
        }
        private Dictionary<int, UnitRarityData> GetUnitRarityData()
        {
            var dic = new Dictionary<int, UnitRarityData>();
            foreach (var pair in rarityDic)
            {
                List<int[]> eq = new List<int[]>();
                List<BaseData> datas1 = new List<BaseData>();
                try
                {
                    foreach (var t2 in equipDic[pair.Key])
                        eq.Add(t2.GetEquips());
                    foreach (var t3 in rankStateDic[pair.Key])
                        datas1.Add(t3.GetBaseData());
                    eq.RemoveAt(eq.Count - 1);
                    datas1.RemoveAt(datas1.Count - 1);
                }
                catch (KeyNotFoundException ex)
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
                        t4.search_area_width, t4.atk_type, (float)t4.normal_atk_cast_time, t4.guild_id);
                }
                catch (KeyNotFoundException ex)
                {
                    Debug.LogError($"{pair.Key}的DETAIL数据丢失！");
                    detailData = new UnitDetailData();
                }
                UnitSkillData skillData = new UnitSkillData();
                unit_skill_data sk = unitSkillDataDic[pair.Key];
                if (pair.Key == 105701 && MainManager.Instance.useJapanData)
                {
                    sk = unitSkillDataDic[170101];
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
                unit_attack_pattern pattern = unitAttackPatternDic[pair.Key][0];
                skillData.loopStart = pattern.loop_start;
                skillData.loopEnd = pattern.loop_end;
                skillData.atkPatterns = pattern.GetAllPatterns().ToArray();
                List<BaseData> d1 = new List<BaseData>();
                List<BaseData> d2 = new List<BaseData>();
                foreach (var t6 in pair.Value)
                {
                    d1.Add(t6.GetBaseData());
                    d2.Add(t6.GetBaseDataGrowth());
                }
                UnitRarityData rarityData = new UnitRarityData(pair.Key, d1, d2, rankData, detailData, skillData);
                dic.Add(pair.Key, rarityData);
            }

            return dic;
        }
        private Dictionary<int, SkillData> GetSkillDataDic()
        {
            Dictionary<int, SkillData> dic = new Dictionary<int, SkillData>();
            var list = GetDatas<skill_data>();
            foreach (var dd in list)
            {
                SkillData sk = new SkillData(dd.skill_id, dd.name, dd.skill_type,
                    dd.skill_area_width, (float)dd.skill_cast_time, dd.GetAllActionIDs().ToArray(),
                    dd.GetAllDependActionIDs().ToArray(), dd.description, dd.icon_type);
                dic.Add(sk.skillid, sk);
            }
            return dic;
        }
        private Dictionary<int, SkillAction> GetSkillActionDic()
        {
            Dictionary<int, SkillAction> dic = new Dictionary<int, SkillAction>();
            var list = GetDatas<skill_action>();
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
        private Dictionary<int, UnitAttackPattern> GetUnitAttackPatternDic()
        {
            Dictionary<int, UnitAttackPattern> dic = new Dictionary<int, UnitAttackPattern>();
            foreach (var dd in unitAttackPatternDic.Values.SelectMany(x => x))
            {
                UnitAttackPattern attackPattern = new UnitAttackPattern(dd.pattern_id, dd.unit_id, dd.loop_start, dd.loop_end, dd.GetAllPatterns().ToArray());
                dic.Add(dd.pattern_id, attackPattern);
            }
            return dic;
        }
        private Dictionary<int, GuildEnemyData> GetGuildEnemyData()
        {
            Dictionary<int, GuildEnemyData> dic = new Dictionary<int, GuildEnemyData>();
            var list = GetDatas<clan_battle_2_map_data>();
            Dictionary<int, List<clan_battle_2_map_data>> dic2 = new Dictionary<int, List<clan_battle_2_map_data>>();
            foreach (var tt in list)
            {
                if (dic2.TryGetValue(tt.clan_battle_id, out var list2))
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
        private Dictionary<int, MasterEnemyMParts.EnemyMParts> GetMPartsData()
        {
            Dictionary<int, MasterEnemyMParts.EnemyMParts> dic = new Dictionary<int, MasterEnemyMParts.EnemyMParts>();
            var list = GetDatas<enemy_m_parts>();
            foreach (var dd in list)
            {
                dic.Add(dd.enemy_id, new MasterEnemyMParts.EnemyMParts(dd.enemy_id, dd.child_enemy_parameter_1, dd.child_enemy_parameter_2, dd.child_enemy_parameter_3, dd.child_enemy_parameter_4, dd.child_enemy_parameter_5));
            }
            return dic;
        }
        private Dictionary<int, EnemyData> GetEnemyDataDic()
        {
            Dictionary<int, EnemyData> dic = new Dictionary<int, EnemyData>();
            //Dictionary<int, EnemyData> dic = new Dictionary<int, EnemyData>();

            var list = GetDatas<enemy_parameter>(a => a.enemy_id >= 400000000 && a.enemy_id <= 500000000);
            int idx = 0;
            foreach (var dd in list)
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

                var skList = unitSkillDataDic[e.unit_id];//GetDatas<unit_skill_data>(a => a.unit_id == e.unit_id)[0];
                skillData.UB = skList.union_burst;
                skillData.MainSkills = skList.GetMainSkillIDs();
                skillData.enemyAttackPatterns = new List<UnitAttackPattern>();
                var list0 = unitAttackPatternDic[e.unit_id];//GetDatas<unit_attack_pattern>(a=>a.unit_id == e.unit_id);
                foreach (var dd3 in list0)
                {
                    UnitAttackPattern attackPattern = new UnitAttackPattern(dd3.pattern_id, dd3.unit_id, dd3.loop_start, dd3.loop_end, dd3.GetAllPatterns().ToArray());
                    skillData.enemyAttackPatterns.Add(attackPattern);
                }
                e.skillData = skillData;

                dic.Add(e.enemy_id, e);
                idx++;

            }
            return dic;
        }

        private Dictionary<int, EquipmentData> GetEquipmentData()
        {
            Dictionary<int, EquipmentData> dic = new Dictionary<int, EquipmentData>();
            var list = GetDatas<equipment_data>();
            foreach (var dd in list)
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
        private Dictionary<int, UniqueEquipmentData> GetUEQData()
        {
            Dictionary<int, UniqueEquipmentData> dic = new Dictionary<int, UniqueEquipmentData>();
            var list = GetDatas<unique_equipment_data>();
            foreach (var dd in list)
            {
                UniqueEquipmentData unique = new UniqueEquipmentData();
                unique.equipment_id = dd.equipment_id;
                unique.baseValue = dd.GetBaseData();
                int unitid = (unique.equipment_id - 130001) * 10 + 100001;
                dic[unitid] = unique;
            }
            var list2 = GetDatas<unique_equipment_enhance_rate>();
            foreach (var dd in list2)
            {
                int equipment_id = dd.equipment_id;
                int unitid = (equipment_id - 130001) * 10 + 100001;
                dic[unitid].enhanceValue = dd.GetBaseData();
            }
            return dic;
        }

        private Dictionary<int, string> GetUnitName_cn()
        {
            Dictionary<int, string> dic = new Dictionary<int, string>();
            var list = GetDatas<unit_data>();
            foreach (var dd in list)
            {
                dic.Add(dd.unit_id, dd.unit_name);
            }
            return dic;
        }
        private Dictionary<int, string[]> GetSkillName_cn()
        {
            Dictionary<int, string[]> dic = new Dictionary<int, string[]>();
            var list = GetDatas<skill_data>();
            foreach (var dd in list)
            {
                dic.Add(dd.skill_id, new string[2] { dd.name, dd.description });
            }
            return dic;
        }
        private Dictionary<int, string> GetSkillActionDes_cn()
        {
            Dictionary<int, string> dic = new Dictionary<int, string>();
            var list = GetDatas<skill_action>();
            foreach (var dd in list)
            {
                dic.Add(dd.action_id, dd.description);
            }
            return dic;
        }

        public (Dictionary<int, UnitStoryData>, Dictionary<int, List<int>>) GetUnitStoryData()
        {
            Dictionary<int, UnitStoryData> unitStoryDic = new Dictionary<int, UnitStoryData>();
            Dictionary<int, List<int>> unitStoryEffectDic = new Dictionary<int, List<int>>();
            Dictionary<int, List<List<int[]>>> add_vals = new Dictionary<int, List<List<int[]>>>();
            Dictionary<int, List<int>> ef_id = new Dictionary<int, List<int>>();
            var list = GetDatas<chara_story_status>();

            foreach (var dd in list)
            {
                int storyid = dd.story_id;
                int unitid = storyid / 1000 * 100 + 1;
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

            return (unitStoryDic, unitStoryEffectDic);
        }

    }
}