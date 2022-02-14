using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEditor;
using Newtonsoft0.Json;
using System.IO;
using Mono.Data.Sqlite;
using System.Linq;
using Debug = UnityEngine.Debug;

namespace PCRCaculator
{
    public class SQL2Json
    {
        private Dictionary<int, EquipmentData> equipmentDic = new Dictionary<int, EquipmentData>();//装备类与装备id的对应字典
        private Dictionary<int, UnitRarityData> unitRarityDic = new Dictionary<int, UnitRarityData>();//角色基础数据与角色id的对应字典 
        private Dictionary<int, UnitStoryData> unitStoryDic = new Dictionary<int, UnitStoryData>();//角色羁绊奖励列表
        private Dictionary<int, List<int>> unitStoryEffectDic = new Dictionary<int, List<int>>();//角色的马甲列表
        public Dictionary<int, SkillData> skillDataDic = new Dictionary<int, SkillData>();//所有的技能列表
        public Dictionary<int, SkillAction> skillActionDic = new Dictionary<int, SkillAction>();//所有小技能列表
        private Dictionary<int, string> unitName_cn = new Dictionary<int, string>();//角色中文名字
        private Dictionary<int, string[]> skillNameAndDescribe_cn = new Dictionary<int, string[]>();//技能中文名字和描述
        private Dictionary<int, string> skillActionDescribe_cn = new Dictionary<int, string>();//技能片段中文描述

        private Dictionary<int, QuestData> questDataDic = new Dictionary<int, QuestData>();//普通地图数据
        private Dictionary<int, WaveGroupData> waveGroupDataDic = new Dictionary<int, WaveGroupData>();//怪物波次数据
        private Dictionary<int, EnemyRewardData> enemyRewardDataDic = new Dictionary<int, EnemyRewardData>();//怪物掉落数据
        private Dictionary<int, EquipmentCraft> equipmentCraftDic = new Dictionary<int, EquipmentCraft>();//装备合成数据
        private Dictionary<int, QuestRewardData> questRewardDic = new Dictionary<int, QuestRewardData>();//地图掉落数据
        private Dictionary<int, EquipmentGet> equipmentGetDic = new Dictionary<int, EquipmentGet>();//装备获得数据
        private List<int> exp_cost;
        private List<int> skill_cost;

        private Dictionary<int, EnemyData> enemyDataDic = new Dictionary<int, EnemyData>();//怪物数据
        private Dictionary<int, Elements.MasterEnemyMParts.EnemyMParts> enemyMPartsDic = new Dictionary<int, Elements.MasterEnemyMParts.EnemyMParts>();
        private Dictionary<int, SkillData> enemySkillDataDic = new Dictionary<int, SkillData>();//怪物技能数据
        private Dictionary<int, SkillAction> enemySkillActionDic = new Dictionary<int, SkillAction>();//怪物技能片段数据

        private Dictionary<int, UnitAttackPattern> unitAttackPatternDic = new Dictionary<int, UnitAttackPattern>();//角色技能循环字典

        private Dictionary<int, UniqueEquipmentData> uniqueEquipmentDataDic = new Dictionary<int, UniqueEquipmentData>();//角色专武字典

        private Dictionary<int, int[]> resistDataDic = new Dictionary<int, int[]>();
        private Dictionary<int, Guild.GuildEnemyData> guildEnemyDataList = new Dictionary<int, Guild.GuildEnemyData>();

        private SQLiteHelper sql;
        public int loadCharacterMax = 500000;//最多加载到的角色序号
        private static bool editor = false;
        const string conn = "redive_jp.db";
        const string conn_cn = "redive_cn.db";


#if UNITY_EDITOR
        [MenuItem("PCRTools/db2json")]
#endif 
        public static (Dictionary<int, SkillAction>, Dictionary<int, SkillData>) Db2json()
        {
            SQL2Json a = new SQL2Json();
            a.Load();
            a.SaveDics2Json();
            //a.SaveUnitAttackPatternDic2Json();
            return (a.skillActionDic, a.skillDataDic);
        }
#if UNITY_EDITOR
        [MenuItem("PCRTools/createNewDB")]
#endif

        public static void CreateNewDB()
        {
            string tablename = "role";
            string title =
            "INSERT INTO `" + tablename + "` VALUES(";
            string[] nameList = new string[12]
            {
                "roleNname","s1_Name","s1_usetime","s1_holdtime","s2_name","s2_usetime",
                "s2_holdtime","atktime","waitetime","roleicon","group","actcycle"
            };
            string[] nameType = new string[12]
            {
                "TEXT","STRING","DOUBLE","DOUBLE","TEXT","DOUBLE","DOUBLE","DOUBLE",
                "DOUBLE","TEXT","TEXT","VARCHAR"
            };
            string textline = "INSERT INTO `" + tablename + "` VALUES({0});\n";
            string path = Application.dataPath + "/MyText3.txt";
            FileStream fs = new FileStream(path, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            SQL2Json a = new SQL2Json();
            a.Load();

            for (int i = 0; i < 10; i++)
            {
                string text0 = "";
                List<string> details = new List<string>();
                UnitRarityData b = a.unitRarityDic[100101 + i * 100];
                details.Add(b.unitName);
                details.Add(a.skillDataDic[b.skillData.skill_1].name);
                details.Add(a.skillDataDic[b.skillData.skill_1].casttime.ToString());

                for (int j = 0; j < 12; j++)
                {
                    text0 += "/*" + nameList[j] + "*/";
                    if (j < 11)
                    {
                        text0 += ",";
                    }
                }
                sw.WriteLine(string.Format(textline, text0));

            }
            //sw.Write("Hello World!!!!");
            //清空缓冲区
            sw.Flush();
            //关闭流
            sw.Close();
            fs.Close();
            Debug.Log("Finish!");
        }
#if UNITY_EDITOR

        [MenuItem("PCRTools/从DB导入工会战怪物json文件-jp")]
#endif

        public static void CreateEnemyJson()
        {
            SQL2Json a = new SQL2Json();
            a.Load_3();
            a.SaveDics2Json_3();
        }
#if UNITY_EDITOR

        [MenuItem("PCRTools/从DB导入工会战怪物json文件-cn")]
#endif

        public static void CreateEnemyJson2((Dictionary<int, SkillAction>, Dictionary<int, SkillData>) skill)
        {
            SQL2Json a = new SQL2Json();
            a.Load_3(true, skill);
            a.SaveDics2Json_3();
        }
#if UNITY_EDITOR

        [MenuItem("PCRTools/从DB导入角色攻击循环json文件-jp")]
#endif

        public static void CreateAttackPatternJson()
        {
            SQL2Json a = new SQL2Json();
            a.Load_4();
            a.SaveUnitAttackPatternDic2Json();
        }
#if UNITY_EDITOR

        [MenuItem("PCRTools/从DB导入角色攻击循环json文件-cn")]
#endif

        public static void CreateAttackPatternJson2()
        {
            SQL2Json a = new SQL2Json();
            a.Load_4(true);
            a.SaveUnitAttackPatternDic2Json();
        }
#if UNITY_EDITOR

        [MenuItem("PCRTools/从DB导入专武json文件")]
#endif

        public static void CreateUniqueEquipmentJson()
        {
            SQL2Json a = new SQL2Json();
            a.Load_5();
            a.SaveUniqueEquipmentDataDic2Json();
        }

#if UNITY_EDITOR

        [MenuItem("PCRTools/从DB导入抗性文件")]
#endif

        public static void CreateResistDataDic()
        {
            SQL2Json a = new SQL2Json();
            a.Load_6();
            a.SaveResistDataDic();

        }
#if UNITY_EDITOR

        [MenuItem("PCRTools/从DB导入会战文件")]
#endif

        public static void CreateClanBattleData()
        {
            SQL2Json a = new SQL2Json();
            a.Load_7();
            a.SaveClanBattleData();

        }

        public static void CreateAllSQLDataInPlayerMode_jp()
        {
            _jpconn = null; _cnconn = null;
            editor = false;
            Db2json();
            //CreateCraftjson();
            CreateEnemyJson();
            CreateAttackPatternJson();
            CreateUniqueEquipmentJson();
        }
        public static void CreateAllSQLDataInPlayerMode_cn()
        {
            _jpconn = null; _cnconn = null;
            var skill = Db2json(); 

            CreateEnemyJson2(skill);
            CreateAttackPatternJson2();
            CreateUniqueEquipmentJson();
            CreateClanBattleData();
        }

        private void Load()
        {
            //string conn = "data source=redive_jp.db";
            //string conn = "redive_jp.db";

            sql = JpConnection;
            
            LoadUnitRarityData(true);
            LoadUnitStoryData();
            LoadEquipmentData();
            LoadSkillData(true);

            sql.CloseConnection();

            sql = CnConnection;
            //LoadEquipmentData();
            LoadSkillData();
            LoadChineseBaseData();
            LoadUnitRarityData();
            LoadUnitStoryData();
            sql.CloseConnection();

            /*sql = JpConnection;
            LoadEquipmentData();
            //LoadUnitStoryData();
            LoadSkillData(true);
            sql.CloseConnection();*/


        }
        private void Load_2()
        {
            sql = JpConnection;
            LoadEquipmentData();
            /*sql.CloseConnection();

            string conn_cn = "redive_cn.db";
            sql = CnConnection;*/
            LoadQuestData();
            LoadWaveGroupData();
            LoadEnemyRewardData();
            LoadEquipmentCraft();
            LoadEXPCost();
            LoadSkillCost();
            sql.CloseConnection();

        }
        private void Load_3(bool cn=false, (Dictionary<int, SkillAction>, Dictionary<int, SkillData>) skill = default)
        {

            //string conn = "redive_cn.db";
            //string conn = "redive_cn_old.db";
            if (cn)
            {
                sql = CnConnection;
                LoadEnemyData(skill);
                sql.CloseConnection();
            }
            else
            {
                sql = JpConnection;
                LoadEnemyData(skill);
                sql.CloseConnection();
            }

        }
        private void Load_4(bool cn=false)
        {
            if (cn)
            {
                sql = CnConnection;
                //LoadUnitRarityData();
                LoadUnitAttackPattern();
                sql.CloseConnection();
                sql = JpConnection;
                //LoadUnitRarityData();
                LoadUnitAttackPattern();
                sql.CloseConnection();
            }
            else
            {
                sql = JpConnection;
                //LoadUnitRarityData();
                LoadUnitAttackPattern();
                sql.CloseConnection();
                sql = CnConnection;
                //LoadUnitRarityData();
                LoadUnitAttackPattern();
                sql.CloseConnection();
            }
        }
        private void Load_5()
        {

            sql = JpConnection;
            LoadUniqueEquipmentData();
            sql.CloseConnection();
        }
        private void Load_6()
        {
            sql = JpConnection;
            LoadResistDada();
            sql.CloseConnection();
        }
        private void Load_7()
        {
            sql = CnConnection;
            LoadClanBattleData();
            sql.CloseConnection();

        }
        private static SQLiteHelper _jpconn, _cnconn;
        private static SQLiteHelper JpConnection => _jpconn = _jpconn ?? new SQLiteHelper(conn);
        private static SQLiteHelper CnConnection => _cnconn = _cnconn ?? new SQLiteHelper(conn_cn, true);
        private void LoadEquipmentData()
        {
            Dictionary<int, BaseData> dic = new Dictionary<int, BaseData>();
            //读取数据表
            SqliteDataReader reader = sql.ReadFullTable("equipment_enhance_rate");
            while (reader.Read())
            {
                dic.Add(reader.GetInt32(reader.GetOrdinal("equipment_id")), GetBaseDataFromReader(reader));
            }
            reader = sql.ReadFullTable("equipment_data");
            while (reader.Read())
            {
                int id = reader.GetInt32(reader.GetOrdinal("equipment_id"));
                if (!equipmentDic.ContainsKey(id))
                {
                    //if (id >= 113000) { break; }//超过113000为装备碎片，不需要这些数据
                    if (!dic.ContainsKey(id))
                    {
                        if (id < 113000)
                        {
                            Debug.LogError("装备" + id + "的升级数据丢失！");
                        }
                        BaseData defaultdata = new BaseData();
                        dic.Add(id, defaultdata);
                    }
                    EquipmentData equipmentData = new EquipmentData(
                        id,
                        reader.GetString(reader.GetOrdinal("equipment_name")),
                        reader.GetInt32(reader.GetOrdinal("promotion_level")),
                        reader.GetString(reader.GetOrdinal("description")),
                        reader.GetInt32(reader.GetOrdinal("craft_flg")) == 1,
                        reader.GetInt32(reader.GetOrdinal("equipment_enhance_point")),
                        reader.GetInt32(reader.GetOrdinal("sale_price")),
                        reader.GetInt32(reader.GetOrdinal("craft_flg")),

                        GetBaseDataFromReader(reader), dic[id]);
                    this.equipmentDic.Add(id, equipmentData);
                }
            }
        }
        private void LoadUnitStoryData()
        {
            Dictionary<int, List<List<int[]>>> add_vals = new Dictionary<int, List<List<int[]>>>();
            Dictionary<int, List<int>> ef_id = new Dictionary<int, List<int>>();
            SqliteDataReader reader = sql.ReadFullTable("chara_story_status");

            while (reader.Read())
            {
                int storyid = reader.GetInt32(reader.GetOrdinal("story_id"));
                int unitid = Mathf.FloorToInt(storyid / 1000) * 100 + 1;
                if (!add_vals.ContainsKey(unitid))
                {
                    add_vals.Add(unitid, new List<List<int[]>>());
                    ef_id.Add(unitid, new List<int>());
                    for (int i = 1; i < 6; i++)
                    {
                        int i1 = reader.GetInt32(reader.GetOrdinal("chara_id_" + i));
                        if (i1 != 0)
                        {
                            ef_id[unitid].Add(i1 * 100 + 1);
                        }
                    }
                }
                List<int[]> li = new List<int[]>();
                for (int i = 1; i < 6; i++)
                {
                    int i2 = reader.GetInt32(reader.GetOrdinal("status_type_" + i));
                    if (i2 != 0)
                    {
                        li.Add(new int[2] { i2, reader.GetInt32(reader.GetOrdinal("status_rate_" + i)) });
                    }
                }
                add_vals[unitid].Add(li);
            }
            foreach (int id in add_vals.Keys)
            {
                UnitStoryData unitStory = new UnitStoryData(id, add_vals[id].Count + 1, ef_id[id], add_vals[id]);
                if (!unitStoryDic.ContainsKey(id))
                    unitStoryDic.Add(id, unitStory);
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
        private void LoadSkillData(bool isJapanSQL = false)
        {
            var reader = sql.ReadFullTable("skill_action");

            while (reader.Read())
            {
                int actionid = reader.GetInt32(reader.GetOrdinal("action_id"));
                int[] de = new int[3]
                {
                reader.GetInt32(reader.GetOrdinal("action_detail_1")),
                reader.GetInt32(reader.GetOrdinal("action_detail_2")),
                reader.GetInt32(reader.GetOrdinal("action_detail_3")),

                };
                float[] va = new float[7]
                {
                reader.GetFloat(reader.GetOrdinal("action_value_1")),
                reader.GetFloat(reader.GetOrdinal("action_value_2")),
                reader.GetFloat(reader.GetOrdinal("action_value_3")),
                reader.GetFloat(reader.GetOrdinal("action_value_4")),
                reader.GetFloat(reader.GetOrdinal("action_value_5")),
                reader.GetFloat(reader.GetOrdinal("action_value_6")),
                reader.GetFloat(reader.GetOrdinal("action_value_7"))
                };
                SkillAction skillAction = new SkillAction(
                    actionid,
                    reader.GetInt32(reader.GetOrdinal("class_id")),
                    reader.GetInt32(reader.GetOrdinal("action_type")),
                    de, va,
                    reader.GetInt32(reader.GetOrdinal("target_assignment")),
                    reader.GetInt32(reader.GetOrdinal("target_area")),
                    reader.GetInt32(reader.GetOrdinal("target_range")),
                    reader.GetInt32(reader.GetOrdinal("target_type")),
                    reader.GetInt32(reader.GetOrdinal("target_number")),
                    reader.GetInt32(reader.GetOrdinal("target_count")),
                    reader.GetString(reader.GetOrdinal("description")),
                    reader.GetString(reader.GetOrdinal("level_up_disp"))
                    );
                /*
                if (!skillActionDic.ContainsKey(actionid))
                    skillActionDic.Add(actionid, skillAction);
                else
                {
                    if (isJapanSQL)
                    {
                        if(actionid<= 200000000)
                        {
                            skillActionDic[actionid] = skillAction;
                        }
                    }
                }*/

                // override jpdb by cndb
                skillActionDic[actionid] = skillAction;
            }

            reader = sql.ReadFullTable("skill_data");

            while (reader.Read())
            {
                int skillid = reader.GetInt32(reader.GetOrdinal("skill_id"));
                int[] actions = new int[7]{
                reader.GetInt32(reader.GetOrdinal("action_1")),
                reader.GetInt32(reader.GetOrdinal("action_2")),
                reader.GetInt32(reader.GetOrdinal("action_3")),
                reader.GetInt32(reader.GetOrdinal("action_4")),
                reader.GetInt32(reader.GetOrdinal("action_5")),
                reader.GetInt32(reader.GetOrdinal("action_6")),
                reader.GetInt32(reader.GetOrdinal("action_7"))
            };
                int[] deactions = new int[7]{
                reader.GetInt32(reader.GetOrdinal("depend_action_1")),
                reader.GetInt32(reader.GetOrdinal("depend_action_2")),
                reader.GetInt32(reader.GetOrdinal("depend_action_3")),
                reader.GetInt32(reader.GetOrdinal("depend_action_4")),
                reader.GetInt32(reader.GetOrdinal("depend_action_5")),
                reader.GetInt32(reader.GetOrdinal("depend_action_6")),
                reader.GetInt32(reader.GetOrdinal("depend_action_7"))
            };
                SkillData skillData = new SkillData(skillid,
                    reader.GetString(reader.GetOrdinal("name")),
                    reader.GetInt32(reader.GetOrdinal("skill_type")),
                    reader.GetInt32(reader.GetOrdinal("skill_area_width")),
                    reader.GetFloat(reader.GetOrdinal("skill_cast_time")),
                    actions, deactions,
                    reader.GetString(reader.GetOrdinal("description")),
                    reader.GetInt32(reader.GetOrdinal("icon_type"))
                    );

                if (skillActionDic.TryGetValue(skillData.skillactions[0], out var ac1) &&
                        ac1.type == 17 && ac1.details[0] == 7 && ac1.values[2] == 90 &&
                    skillActionDic.TryGetValue(skillData.skillactions[0] + 1, out var ac2) &&
                        ac2.type == 73) //removed log barrier, recover it
                {
                    skillData.skillactions[1] = skillData.skillactions[0] + 1;
                }
                /*
                if (!skillDataDic.ContainsKey(skillid))
                    skillDataDic.Add(skillid, skillData);
                else
                {
                    if (isJapanSQL)
                    {
                        if (skillid <= 2000000)
                            skillDataDic[skillid] = skillData;
                    }
                }*/
                // override jpdb by cndb
                skillDataDic[skillid] = skillData;
            }
        }
        private void LoadUnitRarityData(bool isJapen = false)
        {
            Dictionary<int, List<BaseData>> dic = new Dictionary<int, List<BaseData>>();
            Dictionary<int, List<int[]>> unitRankdic = new Dictionary<int, List<int[]>>();
            Dictionary<int, List<BaseData>> unitRaritydic = new Dictionary<int, List<BaseData>>();
            Dictionary<int, List<BaseData>> unitRarityRatedic = new Dictionary<int, List<BaseData>>();
            Dictionary<int, UnitDetailData> unitDetaildic = new Dictionary<int, UnitDetailData>();
            Dictionary<int, UnitSkillData> unitSkilldic = new Dictionary<int, UnitSkillData>();
            Dictionary<int, List<int[]>> unitAtkPatterndic = new Dictionary<int, List<int[]>>();
            SqliteDataReader reader = sql.ReadFullTable("unit_promotion_status");

            while (reader.Read())
            {
                int id = reader.GetInt32(reader.GetOrdinal("unit_id"));
                int rank = reader.GetInt32(reader.GetOrdinal("promotion_level"));
                if (!dic.ContainsKey(id))
                {
                    dic.Add(id, new List<BaseData>());
                }
                if (dic[id].Count == rank - 2)
                {
                    dic[id].Add(GetBaseDataFromReader(reader));
                }
                else
                {
                    Debug.LogError("加载" + id + " 的RANK面板数据时出错！");
                }
            }
            reader = sql.ReadFullTable("unit_promotion");
            while (reader.Read())
            {
                int id = reader.GetInt32(reader.GetOrdinal("unit_id"));
                int rank = reader.GetInt32(reader.GetOrdinal("promotion_level"));
                if (!unitRankdic.ContainsKey(id))
                {
                    unitRankdic.Add(id, new List<int[]>());
                }
                if (unitRankdic[id].Count < rank)
                {
                    int[] equipl_solt = new int[6] {
                    reader.GetInt32(reader.GetOrdinal("equip_slot_1")),
                    reader.GetInt32(reader.GetOrdinal("equip_slot_2")),
                    reader.GetInt32(reader.GetOrdinal("equip_slot_3")),
                    reader.GetInt32(reader.GetOrdinal("equip_slot_4")),
                    reader.GetInt32(reader.GetOrdinal("equip_slot_5")),
                    reader.GetInt32(reader.GetOrdinal("equip_slot_6"))
                };
                    unitRankdic[id].Add(equipl_solt);
                }
                else
                {
                    Debug.LogError("加载" + id + " 的RANK装备数据时出错！");
                }
            }
            reader = sql.ReadFullTable("unit_rarity");
            while (reader.Read())
            {
                int id = reader.GetInt32(reader.GetOrdinal("unit_id"));
                int rarity = reader.GetInt32(reader.GetOrdinal("rarity"));
                if (!unitRaritydic.ContainsKey(id))
                {
                    unitRaritydic.Add(id, new List<BaseData>());
                }
                if (!unitRarityRatedic.ContainsKey(id))
                {
                    unitRarityRatedic.Add(id, new List<BaseData>());
                }
                if (unitRaritydic[id].Count == rarity - 1)
                {
                    unitRaritydic[id].Add(GetBaseDataFromReader(reader));
                }
                else
                {
                    Debug.LogError("加载" + id + " 的星级数据时出错！");
                }
                if (unitRarityRatedic[id].Count == rarity - 1)
                {
                    unitRarityRatedic[id].Add(GetBaseDataFromReader(reader, true));
                }
                else
                {
                    Debug.LogError("加载" + id + " 的星级升级数据时出错！");
                }

            }
            reader = sql.ReadFullTable("unit_data");
            while (reader.Read())
            {
                int id = reader.GetInt32(reader.GetOrdinal("unit_id"));
                UnitDetailData unitDetailData = new UnitDetailData(
                    id,
                    reader.GetString(reader.GetOrdinal("unit_name")),
                    reader.GetInt32(reader.GetOrdinal("rarity")),
                    reader.GetInt32(reader.GetOrdinal("motion_type")),
                    reader.GetInt32(reader.GetOrdinal("se_type")),
                    reader.GetInt32(reader.GetOrdinal("search_area_width")),
                    reader.GetInt32(reader.GetOrdinal("atk_type")),
                    reader.GetFloat(reader.GetOrdinal("normal_atk_cast_time")),
                    reader.GetInt32(reader.GetOrdinal("guild_id"))
                    );
                unitDetaildic.Add(id, unitDetailData);
            }
            reader = sql.ReadFullTable("unit_attack_pattern");
            while (reader.Read())
            {
                int patternid = reader.GetInt32(reader.GetOrdinal("pattern_id"));
                //if (patternid >= 20000000) { break; }//只读取人物的数据，不管怪物的数据
                int unitid = reader.GetInt32(reader.GetOrdinal("unit_id"));
                int[] s2e = new int[2]
{
                reader.GetInt32(reader.GetOrdinal("loop_start")),
                reader.GetInt32(reader.GetOrdinal("loop_end"))
};
                int[] loops = new int[20] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                for (int i = 0; i < 20; i++)
                {
                    loops[i] = reader.GetInt32(reader.GetOrdinal("atk_pattern_" + (i + 1)));
                }
                List<int[]> dlist = new List<int[]>();
                dlist.Add(s2e);
                dlist.Add(loops);

                if (!unitAtkPatterndic.ContainsKey(unitid))
                {
                    unitAtkPatterndic.Add(unitid, dlist);
                }
                if (!unitAttackPatternDic.ContainsKey(patternid))

                    this.unitAttackPatternDic.Add(patternid, new UnitAttackPattern(patternid, unitid, s2e[0], s2e[1], loops));

            }


            reader = sql.ReadFullTable("unit_skill_data");
            while (reader.Read())
            {
                int unitid = reader.GetInt32(reader.GetOrdinal("unit_id"));
                //if (unitid >= 200000) { break; }
                int[] skills = new int[10]
                {
                reader.GetInt32(reader.GetOrdinal("union_burst")),
                reader.GetInt32(reader.GetOrdinal("union_burst_evolution")),
                reader.GetInt32(reader.GetOrdinal("main_skill_1")),
                reader.GetInt32(reader.GetOrdinal("main_skill_evolution_1")),
                reader.GetInt32(reader.GetOrdinal("main_skill_2")),
                reader.GetInt32(reader.GetOrdinal("main_skill_evolution_2")),
                reader.GetInt32(reader.GetOrdinal("ex_skill_1")),
                reader.GetInt32(reader.GetOrdinal("ex_skill_evolution_1")),
                reader.GetInt32(reader.GetOrdinal("sp_skill_1")),
                reader.GetInt32(reader.GetOrdinal("sp_skill_2")),
                };
                if (!unitAtkPatterndic.ContainsKey(unitid)) { continue; }
                UnitSkillData unitSkillData = new UnitSkillData(skills,
                    unitAtkPatterndic[unitid][0][0],
                    unitAtkPatterndic[unitid][0][1],
                    unitAtkPatterndic[unitid][1]);
                unitSkillData.skill_3 = reader.GetInt32(reader.GetOrdinal("main_skill_3"));
                unitSkillData.skill_4 = reader.GetInt32(reader.GetOrdinal("main_skill_3"));
                unitSkillData.skill_5 = reader.GetInt32(reader.GetOrdinal("main_skill_3"));
                unitSkillData.skill_6 = reader.GetInt32(reader.GetOrdinal("main_skill_3"));
                unitSkillData.skill_7 = reader.GetInt32(reader.GetOrdinal("main_skill_3"));
                unitSkillData.skill_8 = reader.GetInt32(reader.GetOrdinal("main_skill_3"));
                unitSkillData.skill_9 = reader.GetInt32(reader.GetOrdinal("main_skill_3"));
                unitSkillData.skill_10 = reader.GetInt32(reader.GetOrdinal("main_skill_3"));
                unitSkillData.EXskill_2 = reader.GetInt32(reader.GetOrdinal("ex_skill_2"));
                unitSkillData.EXskill_ev2 = reader.GetInt32(reader.GetOrdinal("ex_skill_evolution_2"));
                unitSkillData.EXskill_3 = reader.GetInt32(reader.GetOrdinal("ex_skill_3"));
                unitSkillData.EXskill_ev3 = reader.GetInt32(reader.GetOrdinal("ex_skill_evolution_3"));
                unitSkillData.EXskill_4 = reader.GetInt32(reader.GetOrdinal("ex_skill_4"));
                unitSkillData.EXskill_ev4 = reader.GetInt32(reader.GetOrdinal("ex_skill_evolution_4"));
                unitSkillData.EXskill_5 = reader.GetInt32(reader.GetOrdinal("ex_skill_5"));
                unitSkillData.EXskill_ev5 = reader.GetInt32(reader.GetOrdinal("ex_skill_evolution_5"));
                unitSkillData.SPskill_3 = reader.GetInt32(reader.GetOrdinal("sp_skill_3"));
                unitSkillData.SPskill_4 = reader.GetInt32(reader.GetOrdinal("sp_skill_4"));
                unitSkillData.SPskill_5 = reader.GetInt32(reader.GetOrdinal("sp_skill_5"));
                if (isJapen)
                {
                    unitSkillData.SPskill_1_ev = reader.GetInt32(reader.GetOrdinal("sp_skill_evolution_1"));
                    unitSkillData.SPskill_2_ev = reader.GetInt32(reader.GetOrdinal("sp_skill_evolution_2"));
                }


                unitSkilldic.Add(unitid, unitSkillData);
            }
            foreach (int id in unitRankdic.Keys)
            {
                if (id <= 500000 && !unitRarityDic.ContainsKey(id))//只加载到指定角色
                {
                    if (!dic.ContainsKey(id)) { dic.Add(id, new List<BaseData>()); }
                    UnitRankData unitRankData = new UnitRankData(unitRankdic[id], dic[id]);
                    if (!unitDetaildic.ContainsKey(id)) { unitDetaildic.Add(id, new UnitDetailData()); }
                    if (!unitSkilldic.ContainsKey(id)) { unitSkilldic.Add(id, new UnitSkillData()); }
                    if (!unitRaritydic.ContainsKey(id)) { unitRaritydic.Add(id, new List<BaseData>()); }
                    if (!unitRarityRatedic.ContainsKey(id)) { unitRarityRatedic.Add(id, new List<BaseData>()); }
                    UnitRarityData unitRarityData = new UnitRarityData(id, unitRaritydic[id],
                        unitRarityRatedic[id], unitRankData, unitDetaildic[id], unitSkilldic[id]);
                    unitRarityDic.Add(id, unitRarityData);

                }
            }
            if (unitRankdic.ContainsKey(170101))
            {
                unitRarityDic[170101].ChangeRankData(unitRarityDic[105701].GetRankData());
            }
            if (unitRankdic.ContainsKey(170201))
            {
                unitRarityDic[170201].ChangeRankData(unitRarityDic[107601].GetRankData());
            }

        }
        private void LoadUnitAttackPattern()
        {
            var reader = sql.ReadFullTable("unit_attack_pattern");
            while (reader.Read())
            {
                int patternid = reader.GetInt32(reader.GetOrdinal("pattern_id"));
                //if (patternid >= 20000000) { break; }//只读取人物的数据，不管怪物的数据
                int unitid = reader.GetInt32(reader.GetOrdinal("unit_id"));
                int[] s2e = new int[2]
{
                reader.GetInt32(reader.GetOrdinal("loop_start")),
                reader.GetInt32(reader.GetOrdinal("loop_end"))
};
                int[] loops = new int[20] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                for (int i = 0; i < 20; i++)
                {
                    loops[i] = reader.GetInt32(reader.GetOrdinal("atk_pattern_" + (i + 1)));
                }
                List<int[]> dlist = new List<int[]>();
                dlist.Add(s2e);
                dlist.Add(loops);
                if (!unitAttackPatternDic.ContainsKey(patternid))
                    this.unitAttackPatternDic.Add(patternid, new UnitAttackPattern(patternid, unitid, s2e[0], s2e[1], loops));

            }

        }
        private void LoadChineseBaseData()
        {
            SqliteDataReader reader = sql.ReadFullTable("unit_data");

            while (reader.Read())
            {
                int id = reader.GetInt32(reader.GetOrdinal("unit_id"));
                string name = reader.GetString(reader.GetOrdinal("unit_name"));
                unitName_cn.Add(id, name);
            }

            reader = sql.ReadFullTable("skill_data");

            while (reader.Read())
            {
                int id = reader.GetInt32(reader.GetOrdinal("skill_id"));
                string name = reader.GetString(reader.GetOrdinal("name"));
                string describe = reader.GetString(reader.GetOrdinal("description"));
                string[] data = new string[2] { name, describe };
                skillNameAndDescribe_cn.Add(id, data);
            }

            reader = sql.ReadFullTable("skill_action");

            while (reader.Read())
            {
                int id = reader.GetInt32(reader.GetOrdinal("action_id"));
                string describe = reader.GetString(reader.GetOrdinal("description"));
                skillActionDescribe_cn.Add(id, describe);
            }


        }
        private BaseData GetBaseDataFromReader(SqliteDataReader reader, bool isGrowth = false)
        {
            string[] names = new string[17]
            {
            "hp","atk","magic_str","def","magic_def","physical_critical","magic_critical",
            "wave_hp_recovery","wave_energy_recovery","dodge","physical_penetrate",
            "magic_penetrate","life_steal","hp_recovery_rate","energy_recovery_rate",
            "energy_reduce_rate","accuracy"
            };
            if (isGrowth)
            {
                for (int i = 0; i < 17; i++)
                {
                    names[i] += "_growth";
                }
            }
            BaseData baseData = new BaseData(
                    (float)reader.GetDouble(reader.GetOrdinal(names[0])),
                    (float)reader.GetDouble(reader.GetOrdinal(names[1])),
                    (float)reader.GetDouble(reader.GetOrdinal(names[2])),
                    (float)reader.GetDouble(reader.GetOrdinal(names[3])),
                    (float)reader.GetDouble(reader.GetOrdinal(names[4])),
                    (float)reader.GetDouble(reader.GetOrdinal(names[5])),
                    (float)reader.GetDouble(reader.GetOrdinal(names[6])),
                    (float)reader.GetDouble(reader.GetOrdinal(names[7])),
                    (float)reader.GetDouble(reader.GetOrdinal(names[8])),
                    (float)reader.GetDouble(reader.GetOrdinal(names[9])),
                    (float)reader.GetDouble(reader.GetOrdinal(names[10])),
                    (float)reader.GetDouble(reader.GetOrdinal(names[11])),
                    (float)reader.GetDouble(reader.GetOrdinal(names[12])),
                    (float)reader.GetDouble(reader.GetOrdinal(names[13])),
                    (float)reader.GetDouble(reader.GetOrdinal(names[14])),
                    (float)reader.GetDouble(reader.GetOrdinal(names[15])),
                    (float)reader.GetDouble(reader.GetOrdinal(names[16]))

                    );
            return baseData;
        }
        private void SaveDics2Json()
        {
            List<string> dic1 = new List<string>();
            foreach (int a in equipmentDic.Keys)
            {
                dic1.Add(equipmentDic[a].ToString());
            }
            List<string> dic2 = new List<string>();
            foreach (int b in unitRarityDic.Keys)
            {
                dic2.Add(unitRarityDic[b].ToString());
            }
            List<string> dic3 = new List<string>();
            foreach (int c in unitStoryDic.Keys)
            {
                dic3.Add(unitStoryDic[c].ToString());
            }
            List<string> dic5 = new List<string>();
            foreach (int e in skillDataDic.Keys)
            {
                dic5.Add(skillDataDic[e].ToString());
            }
            List<string> dic6 = new List<string>();
            foreach (int f in skillActionDic.Keys)
            {
                dic6.Add(skillActionDic[f].ToString());
            }
            AllData alldata = new AllData(dic1, dic2, dic3, unitStoryEffectDic, dic5, dic6, unitName_cn, skillNameAndDescribe_cn, skillActionDescribe_cn);
            //string filePath = Application.dataPath + "/Txts/AllData.txt";
            string filePath = GetSaveDataPath() + "/AllData.json";

            string saveJsonStr = JsonConvert.SerializeObject(alldata);
            StreamWriter sw = new StreamWriter(filePath);
            sw.Write(saveJsonStr);
            sw.Close();
            Debug.Log("成功！" + filePath);
        }

        private void LoadQuestData()
        {
            SqliteDataReader reader = sql.ReadFullTable("quest_data");
            while (reader.Read())
            {
                int id = reader.GetInt32(reader.GetOrdinal("quest_id"));
                QuestData questData = new QuestData(
                    id,
                    reader.GetInt32(reader.GetOrdinal("area_id")),
                    reader.GetString(reader.GetOrdinal("quest_name")),
                    reader.GetInt32(reader.GetOrdinal("limit_team_level")),
                    reader.GetInt32(reader.GetOrdinal("position_x")),
                    reader.GetInt32(reader.GetOrdinal("position_y")),
                    reader.GetInt32(reader.GetOrdinal("icon_id")),
                    reader.GetInt32(reader.GetOrdinal("stamina")),
                    reader.GetInt32(reader.GetOrdinal("stamina_start")),
                    reader.GetInt32(reader.GetOrdinal("team_exp")),
                    reader.GetInt32(reader.GetOrdinal("unit_exp")),
                    reader.GetInt32(reader.GetOrdinal("love")),
                    reader.GetInt32(reader.GetOrdinal("limit_time")),
                    reader.GetInt32(reader.GetOrdinal("daily_limit")),
                    reader.GetInt32(reader.GetOrdinal("clear_reward_group")),
                    reader.GetInt32(reader.GetOrdinal("rank_reward_group")),
                    reader.GetInt32(reader.GetOrdinal("wave_group_id_1")),
                    reader.GetInt32(reader.GetOrdinal("wave_group_id_2")),
                    reader.GetInt32(reader.GetOrdinal("wave_group_id_3")),
                    reader.GetInt32(reader.GetOrdinal("enemy_image_1")),
                    reader.GetInt32(reader.GetOrdinal("enemy_image_2")),
                    reader.GetInt32(reader.GetOrdinal("enemy_image_3")),
                    reader.GetInt32(reader.GetOrdinal("enemy_image_4")),
                    reader.GetInt32(reader.GetOrdinal("enemy_image_5")),
                    reader.GetInt32(reader.GetOrdinal("reward_image_1")),
                    reader.GetInt32(reader.GetOrdinal("reward_image_2")),
                    reader.GetInt32(reader.GetOrdinal("reward_image_3")),
                    reader.GetInt32(reader.GetOrdinal("reward_image_4")),
                    reader.GetInt32(reader.GetOrdinal("reward_image_5"))
                    );
                questDataDic.Add(id, questData);
            }

        }
        private void LoadWaveGroupData()
        {
            SqliteDataReader reader = sql.ReadFullTable("wave_group_data");
            while (reader.Read())
            {
                int wave_id = reader.GetInt32(reader.GetOrdinal("wave_group_id"));
                if(wave_id > 180010023)
                {
                    continue;
                }
                List<WaveGroupData.EnemyDropData> dropDatas = new List<WaveGroupData.EnemyDropData>();
                for (int i = 1; i < 6; i++)
                {
                    dropDatas.Add(GetEnemyDropData(reader, i));
                }
                WaveGroupData groupData = new WaveGroupData(
                    reader.GetInt32(reader.GetOrdinal("id")),
                    wave_id,
                    reader.GetInt32(reader.GetOrdinal("odds")),
                    dropDatas
                    );
                waveGroupDataDic.Add(wave_id, groupData);
            }

        }
        private WaveGroupData.EnemyDropData GetEnemyDropData(SqliteDataReader reader, int num)
        {
            return new WaveGroupData.EnemyDropData(
                reader.GetInt32(reader.GetOrdinal("enemy_id_" + num)),
                reader.GetInt32(reader.GetOrdinal("drop_gold_" + num)),
                reader.GetInt32(reader.GetOrdinal("drop_reward_id_" + num))
                );
        }
        private void LoadEnemyRewardData()
        {
            SqliteDataReader reader = sql.ReadFullTable("enemy_reward_data");
            while (reader.Read())
            {
                int id = reader.GetInt32(reader.GetOrdinal("drop_reward_id"));
                List<EnemyRewardData.RewardData> dropDatas = new List<EnemyRewardData.RewardData>();
                for (int i = 1; i < 6; i++)
                {
                    dropDatas.Add(GetRewardData(reader, i));
                }
                EnemyRewardData rewardData = new EnemyRewardData(
                    id,
                    reader.GetInt32(reader.GetOrdinal("drop_count")),
                    dropDatas
                    );
                enemyRewardDataDic.Add(id, rewardData);
            }


        }
        private EnemyRewardData.RewardData GetRewardData(SqliteDataReader reader, int num)
        {
            return new EnemyRewardData.RewardData(
                reader.GetInt32(reader.GetOrdinal("reward_type_" + num)),
                reader.GetInt32(reader.GetOrdinal("reward_id_" + num)),
                reader.GetInt32(reader.GetOrdinal("reward_num_" + num)),
                reader.GetInt32(reader.GetOrdinal("odds_" + num))
                );
        }
        private void LoadEquipmentCraft()
        {
            SqliteDataReader reader = sql.ReadFullTable("equipment_craft");
            while (reader.Read())
            {
                int id = reader.GetInt32(reader.GetOrdinal("equipment_id"));
                List<int> a = new List<int>();
                List<int> b = new List<int>();
                for(int i = 1; i < 11; i++)
                {
                    a.Add(reader.GetInt32(reader.GetOrdinal("condition_equipment_id_" + i)));
                    b.Add(reader.GetInt32(reader.GetOrdinal("consume_num_" + i)));
                }
                EquipmentCraft equipmentCraft = new EquipmentCraft(
                    id,
                    reader.GetInt32(reader.GetOrdinal("crafted_cost")),
                    a, b
                    );
                equipmentCraftDic.Add(id,equipmentCraft);
            }
        }
        private void LoadEXPCost()
        {
            exp_cost = new List<int>();
            exp_cost.Add(0);
            SqliteDataReader reader = sql.ReadFullTable("experience_unit");
            while (reader.Read())
            {
                exp_cost.Add(reader.GetInt32(reader.GetOrdinal("total_exp")));
            }

        }
        private void LoadSkillCost()
        {
            skill_cost = new List<int>();
            skill_cost.Add(0);
            SqliteDataReader reader = sql.ReadFullTable("skill_cost");
            while (reader.Read())
            {
                skill_cost.Add(reader.GetInt32(reader.GetOrdinal("cost")));
            }
            for(int i = 1; i < skill_cost.Count; i++)
            {
                skill_cost[i] += skill_cost[i - 1];
            }
        }
        private void LoadEnemyData((Dictionary<int, SkillAction> action, Dictionary<int, SkillData> skill) skill)
        {
            Dictionary<int, EnemyDetailData> detailDic = new Dictionary<int, EnemyDetailData>();
            SqliteDataReader reader = sql.ReadFullTable("unit_enemy_data");
            while (reader.Read())
            {
                int unit_id = reader.GetInt32(reader.GetOrdinal("unit_id"));
                if (JudgeIsNeedEnemy(unit_id))
                {
                    EnemyDetailData detailData = new EnemyDetailData();
                    detailData.unit_id = unit_id;
                    detailData.unit_name = reader.GetString(reader.GetOrdinal("unit_name"));
                    detailData.prefab_id = reader.GetInt32(reader.GetOrdinal("prefab_id"));
                    detailData.motion_type = reader.GetInt32(reader.GetOrdinal("motion_type"));
                    detailData.se_type = reader.GetInt32(reader.GetOrdinal("se_type"));
                    detailData.move_speed = reader.GetInt32(reader.GetOrdinal("move_speed"));
                    detailData.search_area_width = reader.GetInt32(reader.GetOrdinal("search_area_width"));
                    detailData.atk_type = reader.GetInt32(reader.GetOrdinal("atk_type"));
                    detailData.normal_atk_cast_time = reader.GetFloat(reader.GetOrdinal("normal_atk_cast_time"));
                    detailData.cutin = reader.GetInt32(reader.GetOrdinal("cutin"));
                    detailData.visual_change_flag = reader.GetInt32(reader.GetOrdinal("visual_change_flag"));
                    detailData.comment = reader.GetString(reader.GetOrdinal("comment"));
                    detailDic.Add(unit_id, detailData);
                }
            }
            Dictionary<int, List<EnemyAttackPattern>> patternDic = new Dictionary<int, List<EnemyAttackPattern>>();
            reader = sql.ReadFullTable("unit_attack_pattern");
            while (reader.Read())
            {
                int unit_id = reader.GetInt32(reader.GetOrdinal("unit_id"));
                if (JudgeIsNeedEnemy(unit_id))
                {
                    EnemyAttackPattern attackPattern = new EnemyAttackPattern();
                    attackPattern.pattern_id = reader.GetInt32(reader.GetOrdinal("pattern_id"));
                    attackPattern.unit_id = unit_id;
                    attackPattern.loopStart = reader.GetInt32(reader.GetOrdinal("loop_start"));
                    attackPattern.loopEnd = reader.GetInt32(reader.GetOrdinal("loop_end"));
                    int[] loops = new int[20] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                    for (int i = 0; i < 20; i++)
                    {
                        loops[i] = reader.GetInt32(reader.GetOrdinal("atk_pattern_" + (i + 1)));
                    }
                    attackPattern.atkPatterns = loops;
                    if (!patternDic.ContainsKey(unit_id))
                    {
                        List<EnemyAttackPattern> li = new List<EnemyAttackPattern>() { attackPattern };
                        patternDic.Add(unit_id, li);
                    }
                    else
                    {
                        patternDic[unit_id].Add(attackPattern);
                    }
                }
            }
            Dictionary<int, EnemySkillData> skillDic = new Dictionary<int, EnemySkillData>();
            reader = sql.ReadFullTable("unit_skill_data");
            while (reader.Read())
            {
                int unit_id = reader.GetInt32(reader.GetOrdinal("unit_id"));
                if (JudgeIsNeedEnemy(unit_id))
                {
                    EnemySkillData skillData = new EnemySkillData();
                    skillData.UB = reader.GetInt32(reader.GetOrdinal("union_burst"));
                    for (int i = 1; i < 11; i++)
                    {
                        var skill_id = reader.GetInt32(reader.GetOrdinal("main_skill_" + i));
                        if (skill_id != 0)
                        {
                            var sk = skill.skill[skill_id];
                            if (skill.action.TryGetValue(sk.skillactions[0], out var ac1) &&
                                ac1.type == 17 && ac1.details[0] == 7 && ac1.values[2] == 90 &&
                                skill.action.TryGetValue(sk.skillactions[1], out var ac2) && ac2.type == 73)
                            {
                                //remove current log barrier
                                skill_id = 0;
                            }
                        }

                        skillData.MainSkills.Add(skill_id);
                    }


                    var id = skillData.MainSkills.FindIndex(sk => sk == 0);
                    if (skill.action.FirstOrDefault(pair => pair.Value.type == 73 && pair.Key / 1000 == unit_id).Key !=
                        0)
                    {
                        Debug.Log($"log barrier skill exists for {unit_id}");
                    }
                    if (id >= 0)
                        skillData.MainSkills[id] =
                            skill.action.FirstOrDefault(pair => pair.Value.type == 73 && pair.Key / 1000 == unit_id).Key / 100;
                    if (patternDic.ContainsKey(unit_id))
                        skillData.enemyAttackPatterns = patternDic[unit_id];
                    else
                        Debug.Log("角色" + unit_id + "的技能循环数据丢失！");
                    skillDic.Add(unit_id, skillData);
                }
            }
            enemyDataDic.Clear();
            reader = sql.ReadFullTable("enemy_parameter");
            while (reader.Read())
            {
                int enemy_id = reader.GetInt32(reader.GetOrdinal("enemy_id"));
                if (enemy_id >= 401010101 && enemy_id <= 501010501)
                {
                    EnemyData enemyData = new EnemyData();
                    enemyData.enemy_id = enemy_id;
                    enemyData.unit_id = reader.GetInt32(reader.GetOrdinal("unit_id"));
                    enemyData.name = reader.GetString(reader.GetOrdinal("name"));
                    enemyData.level = reader.GetInt32(reader.GetOrdinal("level"));
                    enemyData.rarity = reader.GetInt32(reader.GetOrdinal("rarity"));
                    enemyData.promotion_level = reader.GetInt32(reader.GetOrdinal("promotion_level"));
                    enemyData.baseData = GetBaseDataFromReader(reader, false);
                    enemyData.union_burst_level = reader.GetInt32(reader.GetOrdinal("union_burst_level"));
                    for (int i = 1; i < 11; i++)
                    {
                        enemyData.main_skill_lvs.Add(reader.GetInt32(reader.GetOrdinal("main_skill_lv_" + i)));
                    }
                    for (int i = 1; i < 6; i++)
                    {
                        enemyData.ex_skill_lvs.Add(reader.GetInt32(reader.GetOrdinal("ex_skill_lv_" + i)));
                    }
                    enemyData.resist_status_id = reader.GetInt32(reader.GetOrdinal("resist_status_id"));
                    enemyData.unique_equipment_flag_1 = reader.GetInt32(reader.GetOrdinal("unique_equipment_flag_1"));
                    try
                    {
                        enemyData.break_durability = reader.GetInt32(reader.GetOrdinal("break_durability"));
                    }
                    catch
                    {

                    }
                    try
                    {
                        enemyData.virtual_hp = reader.GetInt32(reader.GetOrdinal("virtual_hp"));
                    }
                    catch
                    {

                    }
                    if (detailDic.ContainsKey(enemyData.unit_id))
                        enemyData.detailData = detailDic[enemyData.unit_id];
                    else
                        Debug.Log("角色" + enemyData.unit_id + "的详细数据丢失！");
                    if (skillDic.ContainsKey(enemyData.unit_id))
                        enemyData.skillData = skillDic[enemyData.unit_id];
                    else
                        Debug.Log("角色" + enemyData.unit_id + "的技能数据丢失！");
                    enemyDataDic.Add(enemy_id, enemyData);
                }
            }
            reader = sql.ReadFullTable("enemy_m_parts");
            while (reader.Read())
            {
                int enemy_id = reader.GetInt32(reader.GetOrdinal("enemy_id"));
                if (enemy_id >= 401021505 && enemy_id <= 499999999)
                {
                    int id_1 = reader.GetInt32(reader.GetOrdinal("child_enemy_parameter_1"));
                    int id_2 = reader.GetInt32(reader.GetOrdinal("child_enemy_parameter_2"));
                    int id_3 = reader.GetInt32(reader.GetOrdinal("child_enemy_parameter_3"));
                    int id_4 = reader.GetInt32(reader.GetOrdinal("child_enemy_parameter_4"));
                    int id_5 = reader.GetInt32(reader.GetOrdinal("child_enemy_parameter_5"));
                    var data = new Elements.MasterEnemyMParts.EnemyMParts(enemy_id, id_1, id_2, id_3, id_4, id_5);
                    enemyMPartsDic.Add(enemy_id, data);
                }
            }
        }
        private void LoadUniqueEquipmentData()
        {
            SqliteDataReader reader = sql.ReadFullTable("unique_equipment_data");
            while (reader.Read())
            {
                UniqueEquipmentData unique = new UniqueEquipmentData();
                unique.equipment_id = reader.GetInt32(reader.GetOrdinal("equipment_id"));
                unique.baseValue = GetBaseDataFromReader(reader);
                int unitid = (unique.equipment_id-130001)*10+100001;
                uniqueEquipmentDataDic.Add(unitid, unique);
            }
            reader = sql.ReadFullTable("unique_equipment_enhance_rate");
            while (reader.Read())
            {
                int equipment_id = reader.GetInt32(reader.GetOrdinal("equipment_id"));
                int unitid = (equipment_id - 130001) * 10 + 100001;
                uniqueEquipmentDataDic[unitid].enhanceValue = GetBaseDataFromReader(reader);
            }

        }
        private void LoadResistDada()
        {
            resistDataDic.Clear();
            SqliteDataReader reader = sql.ReadFullTable("resist_data");
            while (reader.Read())
            {
                int id = reader.GetInt32(reader.GetOrdinal("resist_status_id"));
                int[] ailments = new int[32];
                for(int i = 1; i < 33; i++)
                {
                    ailments[i - 1] = reader.GetInt32(reader.GetOrdinal("ailment_" + i)); ;
                }
                resistDataDic.Add(id, ailments);
            }
        }
        private void LoadClanBattleData()
        {
            guildEnemyDataList.Clear();
            Dictionary<int, int> waveGroupDic = new Dictionary<int, int>();
            SqliteDataReader reader = sql.ReadFullTable("wave_group_data");
            while (reader.Read())
            {
                int id = reader.GetInt32(reader.GetOrdinal("wave_group_id"));
                if(id>= 401010011)
                {
                    int enemyid = reader.GetInt32(reader.GetOrdinal("enemy_id_1"));
                    waveGroupDic.Add(id, enemyid);
                }
            }
            reader = sql.ReadFullTable("clan_battle_2_map_data");
            var lst = new List<(int id, int group, int[])>();
            while (reader.Read())
            {
                lst.Add((reader.GetInt32(reader.GetOrdinal("id")), reader.GetInt32(reader.GetOrdinal("clan_battle_id")), new[]
                {
                    reader.GetInt32(reader.GetOrdinal("wave_group_id_1")),
                    reader.GetInt32(reader.GetOrdinal("wave_group_id_2")),
                    reader.GetInt32(reader.GetOrdinal("wave_group_id_3")),
                    reader.GetInt32(reader.GetOrdinal("wave_group_id_4")),
                    reader.GetInt32(reader.GetOrdinal("wave_group_id_5"))
                }));
            }
            guildEnemyDataList = lst.GroupBy(d => d.group).ToDictionary(g => g.Key, g => new Guild.GuildEnemyData
            {
                enemyIds = g.OrderBy(d => d.id).Select(d => d.Item3.Select(wave => waveGroupDic[wave]).ToArray()).ToArray()
            });
        }
        private void SaveDics2Json_3()
        {
            string filePath = GetSaveDataPath() + "/EnemyDataDic.json";
            string saveJsonStr = JsonConvert.SerializeObject(enemyDataDic);
            StreamWriter sw = new StreamWriter(filePath);
            sw.Write(saveJsonStr);
            sw.Close();
            filePath = GetSaveDataPath() + "/EnemyMPartsDic.json";
            saveJsonStr = JsonConvert.SerializeObject(enemyMPartsDic);
            var sw2 = new StreamWriter(filePath);
            sw2.Write(saveJsonStr);
            sw2.Close();

            Debug.Log("成功！" + filePath);

        }
        private void SaveUnitAttackPatternDic2Json()
        {
            string filePath = GetSaveDataPath() + "/UnitAtttackPatternDic.json";
            string saveJsonStr = JsonConvert.SerializeObject(unitAttackPatternDic);
            StreamWriter sw = new StreamWriter(filePath);
            sw.Write(saveJsonStr);
            sw.Close();
            Debug.Log("成功！" + filePath);

        }
        private void SaveUniqueEquipmentDataDic2Json()
        {
            string filePath = GetSaveDataPath() + "/UniqueEquipmentDataDic.json";
            string saveJsonStr = JsonConvert.SerializeObject(uniqueEquipmentDataDic);
            StreamWriter sw = new StreamWriter(filePath);
            sw.Write(saveJsonStr);
            sw.Close();
            Debug.Log("成功！" + filePath);

        }
        private void SaveResistDataDic()
        {
            string filePath = GetSaveDataPath() + "/ResistDataDic.json";
            string saveJsonStr = JsonConvert.SerializeObject(resistDataDic);
            StreamWriter sw = new StreamWriter(filePath);
            sw.Write(saveJsonStr);
            sw.Close();
            Debug.Log("成功！" + filePath);

        }
        private void SaveClanBattleData()
        {
            string filePath = GetSaveDataPath() + "/GuildEnemyDatas.json";
            string saveJsonStr = JsonConvert.SerializeObject(guildEnemyDataList);
            StreamWriter sw = new StreamWriter(filePath);
            sw.Write(saveJsonStr);
            sw.Close();
            Debug.Log("成功！" + filePath);

        }
        private string GetSaveDataPath()
        {
            if(editor)
            {
                return Application.dataPath + "/Resources/Datas";
            }
            else
            {
                string path = Application.streamingAssetsPath + "/Datas";
                if (!Directory.Exists(path))
                {
                    //目标目录下不存在此文件夹即创建子文件夹
                    Directory.CreateDirectory(path);
                }
                return path;
            }
        }
        private static bool JudgeIsNeedEnemy(int unit_id)
        {
            return true;
            /*return (unit_id >= 300000 && unit_id <= 390000) 
                || (unit_id >= 208600 && unit_id <= 299999)
                ;*/

        }
    }
}