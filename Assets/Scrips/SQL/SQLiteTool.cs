using System.Collections;
using System.Collections.Generic;
using SQLite4Unity3d;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Elements;
using Mono.Data.Sqlite;
using PCRApi.CN;
using PCRCaculator.Guild;
using UnityEngine.UIElements;
using Debug = UnityEngine.Debug;
using SQLite3 = SQLite4Unity3d.SQLite3;
using Newtonsoft.Json;

#if !UNITY_EDITOR
using System.IO;
#endif

namespace PCRCaculator.SQL
{
    public class SQLiteTool
    {
        private IDataBaseManager mgr;

        public static string sql;

        protected SQLiteTool(IDataBaseManager mgr, bool readOnly)
        {
            this.mgr = mgr;
            this.@readonly = readOnly;
        }

        public void Prepare()
        {

            string rootPath = $"{mgr.DataVer}.db";
            string publicPath = Path.Combine(ABExTool.persistentDataPath, $"{mgr.DataVer}.db");

            // 检查第一个路径（本体目录）
            if (File.Exists(rootPath))
            {
                this.path = rootPath;
                Debug.Log($"使用本体目录{mgr.DataVer}.db");
            }
            else
            {
                // 检查第二个路径（公共目录）
                if (File.Exists(publicPath))
                {
                    this.path = publicPath;
                    Debug.Log($"使用公共目录{mgr.DataVer}.db");
                }
                else
                {
                    Debug.Log($"未找到{mgr.DataVer}.db，将自动下载存放至公共目录，如果是jp数据库需要反哈希，无法直接食用");
                    this.path = publicPath;
                }
            }

            if (!File.Exists(this.path))
            {
                File.WriteAllBytes(this.path, mgr.ResolveDatabase());
                if (!mgr.DataVer.StartsWith("jp"))
                {
                    Unhash();
                }
            }

            string dbPath = null;
            try

            {

#if UNITY_ANDROID
                    dbPath =
 Path.Combine(ABExTool.persistentDataPath, Path.GetFileNameWithoutExtension(path) + "_patch.db");
#else
                dbPath = Path.GetTempFileName();
#endif
                File.Copy(path, dbPath, true);

                string connectingPath = "Data Source=" + dbPath;
                //MainManager.Instance.debugtext.text += connectingPath;
                //构造数据库连接
                using var dbConnection = new SqliteConnection(connectingPath);
                //打开数据库
                dbConnection.Open();
                var trans = dbConnection.BeginTransaction();
#if UNITY_5_3_OR_NEWER
                var num = UnsafeNativeMethods.sqlite3_exec((dbConnection._sql as Mono.Data.Sqlite.SQLite3)!._sql,
                    SqliteConvert.ToUTF8(sql),
                    IntPtr.Zero, IntPtr.Zero, out var intPtr);
                var flag = num != 0;
                if (flag)
                {
                    string onljihpignl = (intPtr == IntPtr.Zero) ? "" : Marshal.PtrToStringAnsi(intPtr);
                    throw new Exception(onljihpignl);
                }
#else
                new Mono.Data.Sqlite.SqliteCommand(dbConnection)
                {
                    CommandText = sql
                }.ExecuteNonQuery();
#endif

                trans.Commit();

                path = dbPath;
            }
            catch (Exception e)
            {
                Debug.LogError($"error occured while trying to patch database: {e}");
            }

            ConnectDB(path, @readonly);
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
        public T[] GetDatas<T>(Func<T, bool> func = null) where T : new()
        {
            var conn = new SQLiteConnection(path, @readonly ? SQLiteOpenFlags.ReadOnly : SQLiteOpenFlags.ReadWrite);
            lock (connections) connections.Add(conn);
            try
            {
                return func == null ? conn.Table<T>().ToArray() : conn.Table<T>().Where(func).ToArray();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public Dictionary<int, T> GetDatasDic<T>(Func<T, int> func, Func<T, bool> select = null) where T : new()
        {
            Dictionary<int, T> dict = new Dictionary<int, T>();
            var list = GetDatas<T>(select);
            if (list == null) return dict;
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
            foreach (var conn in connections) conn.Close();
        }
        private void Unhash()
        {
            string rainbowJsonContent = MainManager.Instance.LoadJsonDatas("Datas/rainbow");
            if (string.IsNullOrEmpty(rainbowJsonContent))
            {
                Debug.LogError("Rainbow table not found, unhashing skipped.");
                return;
            }

            Debug.Log($"Start Unhashing {mgr.DataVer}.db");
            var jsonObject = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(rainbowJsonContent);

            using (var dbConnection = new SqliteConnection($"Data Source={path};Version=3;"))
            {
                dbConnection.Open();

                foreach (var entry in jsonObject)
                {
                    string hashedTableName = entry.Key;
                    var colsDict = entry.Value;
                    string intactTableName = colsDict["--table_name"];

                    string createTableStatement = GetCreateTable(dbConnection, hashedTableName);
                    string createDecTableStatement = GetCreateTable(dbConnection, intactTableName);

                    if (createTableStatement == null)
                    {
                        if (createDecTableStatement == null)
                        {
                            Debug.LogWarning($"CreateTableStatement for '{intactTableName}' not found.");
                        }
                        continue;
                    }

                    List<string> hashedCols = new List<string>();
                    List<string> intactCols = new List<string>();

                    foreach (var colEntry in colsDict)
                    {
                        if (colEntry.Key != "--table_name")
                        {
                            hashedCols.Add(colEntry.Key);
                            intactCols.Add(colEntry.Value);
                        }

                        createTableStatement = createTableStatement.Replace(
                            colEntry.Key == "--table_name" ? hashedTableName : colEntry.Key,
                            colEntry.Key == "--table_name" ? intactTableName : colEntry.Value
                        );
                    }

                    var hashedColsFromDb = GetTableColumns(dbConnection, hashedTableName);
                    foreach (var col in hashedColsFromDb)
                    {
                        if (!hashedCols.Contains(col))
                        {
                            hashedCols.Add(col);
                            intactCols.Add(col);
                        }
                    }

                    string insertStatement = $"INSERT INTO {intactTableName} (`{string.Join("`, `", intactCols)}`) SELECT `{string.Join("`, `", hashedCols)}` FROM {hashedTableName}";
                    string dropTableStatement = $"DROP TABLE {hashedTableName}";

                    List<string> transactionCmds = new List<string> { createTableStatement, insertStatement, dropTableStatement };

                    if (!ExecTransaction(dbConnection, transactionCmds))
                    {
                        Debug.LogError($"Failed when executing a transaction for '{intactTableName}' ({hashedTableName}). Transaction: {string.Join("; ", transactionCmds)}");
                        continue;
                    }
                }
                Debug.Log("Unhashing complete.");
            }
        }

        private string GetCreateTable(SqliteConnection dbConnection, string tableName)
        {
            using (var command = dbConnection.CreateCommand())
            {
                command.CommandText = $"SELECT sql FROM sqlite_master WHERE type='table' AND name='{tableName}'";
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return reader.GetString(0);
                    }
                }
            }
            return null;
        }

        protected List<string> GetTableColumns(SqliteConnection dbConnection, string tableName)
        {
            List<string> columns = new List<string>();
            using (var command = dbConnection.CreateCommand())
            {
                command.CommandText = $"PRAGMA table_info({tableName})";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        columns.Add(reader.GetString(1));
                    }
                }
            }
            return columns;
        }

        private bool ExecTransaction(SqliteConnection dbConnection, List<string> commands)
        {
            using (var transaction = dbConnection.BeginTransaction())
            {
                try
                {
                    foreach (var cmd in commands)
                    {
                        using (var command = dbConnection.CreateCommand())
                        {
                            command.CommandText = cmd;
                            command.Transaction = transaction;
                            command.ExecuteNonQuery();
                        }
                    }
                    transaction.Commit();
                    return true;
                }
                catch (Exception e)
                {
                    Debug.LogError($"Transaction failed: {e}");
                    transaction.Rollback();
                    return false;
                }
            }
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
        public (Dictionary<int, UnitStoryData>, Dictionary<int, List<int>>, Dictionary<int, int>) Pair { get; private set; }
        public Dictionary<int, UnitRarityData> Dic1 { get; private set; }
        public Dictionary<int, unit_talent[]> TalentDic { get; private set; }
        public Dictionary<int, enemy_talent_weakness> EnemyTalentWeaknessDic { get; private set; }
        public Dictionary<int, talent_weakness> TalentWeaknessDic { get; private set; }
        public Dictionary<int, int[]> WeaknessDic { get; private set; }
        public Dictionary<int, talentFormationData> TalentFormationDic { get; private set; }
        public Dictionary<int, EnemyData> Dic2 { get; private set; }
        public Dictionary<int, WaveGroupDataBase> SecretDungeonWaveGroupDataDic { get; private set; }
        public Dictionary<int, EnemyData> TalentQuestEnemyParameterDic { get; private set; }
        public Dictionary<int, WaveGroupDataBase> TalentQuestWaveGroupDataDic { get; private set; }
        public Dictionary<int, EnemyData> AbyssEnemyParameterDic { get; private set; }
        public Dictionary<int, WaveGroupDataBase> AbyssWaveGroupDataDic { get; private set; }
        public Dictionary<int, int> AbyssQuestFilterDic { get; private set; }
        public Dictionary<int, int> AbyssBossFilterDic { get; private set; }
        public Dictionary<int, EnemyData> MirageEnemyParameterDic { get; private set; }
        public Dictionary<int, WaveGroupDataBase> MirageWaveGroupDataDic { get; private set; }
        public Dictionary<int, SkillData> Dic3 { get; private set; }
        public Dictionary<int, SkillAction> Dic4 { get; private set; }
        public Dictionary<int, UnitAttackPattern> Dic5 { get; private set; }
        public Elements.MasterUnitSkillDataRf masterUnitSkillDataRf { get; private set; }
        public Dictionary<int, GuildEnemyData> Dic6 { get; private set; }
        public Dictionary<int, MasterEnemyMParts.EnemyMParts> Dic7 { get; private set; }
        public Dictionary<int, int[]> resistDataDic { get; private set; }
        public Dictionary<int, EquipmentData> Dic8 { get; private set; }
        public Dictionary<int, unique_equip_enhance_rate[]> Dic9 { get; private set; }
        public Dictionary<int, unique_equip_enhance_rate[]> Dic92 { get; private set; }
        public Dictionary<int, string> Dic10 { get; private set; }
        public Dictionary<int, string> ExEquipmentName_cn { get; private set; }
        public Dictionary<int, string[]> Dic11 { get; private set; }
        public Dictionary<int, string> Dic12 { get; private set; }
        public List<EReduction> eReductions { get; private set; }
        public Dictionary<int, Dictionary<int, ex_equipment_data>[]> unitExEquips { get; private set; }
        public Dictionary<int, List<extra_effect_unit_group>> extraEffectUnitGroupDic { get; private set; }
        public Dictionary<int, TalentSkillNode> TalentSkillNodeDic { get; private set; }
        public Dictionary<int, team_skill_title> TalentSkillTitleDic { get; private set; }
        // public Dictionary<int, talent_skill_enhance_level> TalentSkillEnhanceLevelDic { get; private set; }
        public Dictionary<int, TalentSkillEnhanceData> TalentSkillEnhanceDataDic { get; private set; }
        public Dictionary<int, param_type> paramTypeDic { get; private set; }
        public Dictionary<int, TeamSkillNode> TeamSkillNodeDic { get; private set; }
        public Dictionary<int, TeamSkillEnhanceData> TeamSkillEnhanceDataDic { get; private set; }
        public Dictionary<int, extra_effect_data> ExtraEffectDataDic { get; private set; }
        public Dictionary<int, talent_quest_battle_effect> TalentQuestBattleEffectDic { get; private set; }
        public Dictionary<int, abyss_battle_effect> AbyssBattleEffectDic { get; private set; }
        public Dictionary<int, mirage_battle_effect> MirageBattleEffectDic { get; private set; }
        public Dictionary<int, unit_role_data> UnitRoleDataDic { get; private set; }
        public Dictionary<int, unit_role_mastery_id> UnitRoleMasteryIdDic { get; private set; }
        public Dictionary<int, unit_role_mastery_slot_data> UnitRoleSlotDataDic { get; private set; }
        public Dictionary<int, unit_role_mastery_enhance_data[]> UnitRoleMasteryEnhanceDataDic { get; private set; }
        public Dictionary<int, UnitRoleMasterySlotEnhanceData> UnitRoleMasterySlotEnhanceDataDic { get; private set; }
        public Dictionary<int, List<connect_rank_status>> ConnectRankStatusDic { get; private set; }
        public Dictionary<int, connect_rank_chart> ConnectRankChartDic { get; private set; }
        public List<connect_rank_bonus> ConnectRankBonusList { get; private set; }
        public Dictionary<int, connect_rank_boost> ConnectRankBoostDic { get; private set; }
        public Dictionary<int, connect_rank_limit_common> ConnectRankLimitCommonDic { get; private set; }
        public Dictionary<int, List<connect_rank_limit_item>> ConnectRankLimitItemDic { get; private set; }
        public Dictionary<int, List<connect_rank_enhance>> ConnectRankEnhanceDic { get; private set; }
        public Dictionary<int, mirage_nemesis_area> MirageNemesisAreaDic { get; private set; }
        // public Dictionary<(int, int), promotion_bonus> rbs
        public Dictionary<int, Dictionary<int, promotion_bonus>> rbs
        {
            get;
            private set;
        }

        private bool @readonly;

        private SQLData(IDataBaseManager mgr, bool readOnly) : base(mgr, readOnly)
        {
        }

        public static SQLData OpenDB(IDataBaseManager mgr)
        {
            //_connection = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
            //Debug.Log("Final PATH: " + dbPath);
            var temp = new SQLData(mgr, true);
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
            var enemyTalentWeaknessTask = Task.Run(() =>
            {
                EnemyTalentWeaknessDic = GetDatasDic<enemy_talent_weakness>(a => a.enemy_id);
            });
            var talentWeaknessTask = Task.Run(() =>
            {
                TalentWeaknessDic = GetDatasDic<talent_weakness>(a => a.resist_id);
            });
            var talentDicTask = Task.Run(() => TalentDic = GetDatas<unit_talent>()
                        .GroupBy(x => x.unit_id)
                        .ToDictionary(g => g.Key, g => g.ToArray()));
            var UnitRoleDataDicTask =
                Task.Run(() => UnitRoleDataDic = GetDatasDic<unit_role_data>(a => a.unit_id));
            var tasks = new List<Task>();

            tasks.Add(
                Task.WhenAll(rarityDicTask, unitSkillDataDicTask, unitAttackPatternDicTask, equipDicTask,
                        rankStateDicTask, unitDicTask, talentDicTask, UnitRoleDataDicTask)
                .ContinueWith(_ => Dic1 = GetUnitRarityData()));
            tasks.Add(
                Task.WhenAll(dic1Task, unitSkillDataDicTask, unitAttackPatternDicTask)
                    .ContinueWith(_ => Dic2 = GetEnemyDataDic()));
            tasks.Add(
                Task.WhenAll(dic1Task, unitSkillDataDicTask, unitAttackPatternDicTask)
                .ContinueWith(_ => TalentQuestEnemyParameterDic = GetTalentQuestEnemyDataDic()));
            tasks.Add(
                Task.WhenAll(dic1Task, unitSkillDataDicTask, unitAttackPatternDicTask)
                .ContinueWith(_ => AbyssEnemyParameterDic = GetAbyssEnemyDataDic()));
            tasks.Add(
                Task.WhenAll(dic1Task, unitSkillDataDicTask, unitAttackPatternDicTask)
                .ContinueWith(_ => MirageEnemyParameterDic = GetMirageEnemyDataDic()));
            tasks.Add(
                Task.WhenAll(enemyTalentWeaknessTask, talentWeaknessTask)
                    .ContinueWith(_ => WeaknessDic = GetWeaknessDict()));
            tasks.Add(
                Task.Run(() => resistDataDic = GetResistData()));
            tasks.Add(
                Task.Run(() => TalentFormationDic = GetTalentFormationData()));
            tasks.Add(
                Task.Run(() => SecretDungeonWaveGroupDataDic = GetWaveGroupDataDic<wave_group_data>(
                    data => data.id,
                    data => data.wave_group_id,
                    (data, index) =>
                    {
                        return index switch
                        {
                            1 => data.enemy_id_1,
                            2 => data.enemy_id_2,
                            3 => data.enemy_id_3,
                            4 => data.enemy_id_4,
                            5 => data.enemy_id_5,
                            _ => 0,
                        };
                    }).Where(w => w.Value.id / 100000 == 5020).ToDictionary(w => w.Key, w => w.Value)));
            tasks.Add(
                Task.Run(() => TalentQuestWaveGroupDataDic = GetWaveGroupDataDic<talent_quest_wave_group_data>(
                    data => data.id,
                    data => data.wave_group_id,
                    (data, index) =>
                    {
                        return index switch
                        {
                            1 => data.enemy_id_1,
                            2 => data.enemy_id_2,
                            3 => data.enemy_id_3,
                            4 => data.enemy_id_4,
                            5 => data.enemy_id_5,
                            _ => 0,
                        };
                    })
                ));
            tasks.Add(
                Task.Run(() => AbyssWaveGroupDataDic = GetWaveGroupDataDic<abyss_wave_group_data>(
                    data => data.id,
                    data => data.wave_group_id,
                    (data, index) =>
                    {
                        return index switch
                        {
                            1 => data.enemy_id_1,
                            2 => data.enemy_id_2,
                            3 => data.enemy_id_3,
                            4 => data.enemy_id_4,
                            5 => data.enemy_id_5,
                            _ => 0,
                        };
                    })));
            tasks.Add(
            Task.Run(() => MirageWaveGroupDataDic = GetWaveGroupDataDic<mirage_wave_group_data>(
                data => data.wave_group_id,
                data => data.wave_group_id,
                (data, index) =>
                {
                    return index switch
                    {
                        1 => data.enemy_id_1,
                        2 => data.enemy_id_2,
                        3 => data.enemy_id_3,
                        4 => data.enemy_id_4,
                        5 => data.enemy_id_5,
                        _ => 0,
                    };
                })));
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
                Task.Run(() => Dic92 = GetUEQ2Data()));
            tasks.Add(
                Task.Run(() => Pair = GetUnitStoryData()));
            tasks.Add(
                Task.Run(() => Dic10 = GetUnitName_cn()));
            tasks.Add(
                Task.Run(() => ExEquipmentName_cn = GetExEquipmentName_cn()));
            tasks.Add(
                Task.Run(() => Dic11 = GetSkillName_cn()));
            tasks.Add(
                Task.Run(() => Dic12 = GetSkillActionDes_cn()));
            tasks.Add(
                Task.Run(() => eReductions = GetDatas<EReduction>().ToList()));
            tasks.Add(
                Task.Run(() => rbs = GetDatas<promotion_bonus>()
                    .GroupBy(x => x.unit_id)
                    .ToDictionary(g => g.Key, g => g.ToDictionary(x => x.promotion_level, x => x))));
            tasks.Add(
                Task.Run(() => TalentSkillNodeDic = GetTalentSkillData()));
            // tasks.Add(
            // Task.Run(() => TalentSkillEnhanceLevelDic = GetDatasDic<talent_skill_enhance_level>(a => a.enhance_level_id)));
            tasks.Add(
                Task.Run(() => TalentSkillEnhanceDataDic = GetTalentSkillEnhanceData()));
            tasks.Add(
                Task.Run(() => paramTypeDic = GetDatasDic<param_type>(a => a.parameter_type)));
            tasks.Add(
                Task.Run(() => TeamSkillNodeDic = GetTeamSkillData()));
            tasks.Add(
                Task.Run(() => TeamSkillEnhanceDataDic = GetTeamSkillEnhanceData()));
            tasks.Add(
                Task.Run(() => ExtraEffectDataDic = GetDatasDic<extra_effect_data>(a => a.extra_effect_id)));
            tasks.Add(
                Task.Run(() => TalentQuestBattleEffectDic = GetDatasDic<talent_quest_battle_effect>(a => a.id)));
            tasks.Add(
                Task.Run(() => AbyssBattleEffectDic = GetDatasDic<abyss_battle_effect>(a => a.id)));
            tasks.Add(
                Task.Run(() => AbyssQuestFilterDic = GetAbyssQuestFilteredDic()));
            tasks.Add(
                Task.Run(() => AbyssBossFilterDic = GetAbyssBossFilteredDic()));
            tasks.Add(
                Task.Run(() => MirageBattleEffectDic = GetDatasDic<mirage_battle_effect>(a => a.id)));
            tasks.Add(
                Task.Run(() => MirageNemesisAreaDic = GetDatasDic<mirage_nemesis_area>(a => a.nemesis_id)));
            tasks.Add(
                Task.Run(() => UnitRoleMasteryIdDic = GetDatasDic<unit_role_mastery_id>(a => a.mastery_id)));
            tasks.Add(
                Task.Run(() => UnitRoleMasterySlotEnhanceDataDic = GetUnitRoleMasterySlotEnhanceData()));
            tasks.Add(Task.Run(() =>
            {
                ConnectRankStatusDic = (GetDatas<connect_rank_status>() ?? Array.Empty<connect_rank_status>())
                    .GroupBy(x => x.role_id)
                    .ToDictionary(g => g.Key, g => g.OrderBy(x => x.rank_from).ThenBy(x => x.rank_to).ToList());
            }));
            tasks.Add(
                Task.Run(() => ConnectRankChartDic = GetDatasDic<connect_rank_chart>(a => a.rank)));
            tasks.Add(Task.Run(() =>
                ConnectRankBonusList = (GetDatas<connect_rank_bonus>() ?? Array.Empty<connect_rank_bonus>())
                    .OrderBy(x => x.bonus_level)
                    .ThenBy(x => x.bonus_number)
                    .ToList()));
            tasks.Add(
                Task.Run(() => ConnectRankBoostDic = GetDatasDic<connect_rank_boost>(a => a.system_id)));
            tasks.Add(
                Task.Run(() => ConnectRankLimitCommonDic = GetDatasDic<connect_rank_limit_common>(a => a.max_rank)));
            tasks.Add(Task.Run(() =>
            {
                ConnectRankLimitItemDic = (GetDatas<connect_rank_limit_item>() ?? Array.Empty<connect_rank_limit_item>())
                    .GroupBy(x => x.group_id)
                    .ToDictionary(g => g.Key, g => g.OrderBy(x => x.max_rank).ToList());
            }));
            tasks.Add(Task.Run(() =>
            {
                ConnectRankEnhanceDic = (GetDatas<connect_rank_enhance>() ?? Array.Empty<connect_rank_enhance>())
                    .GroupBy(x => x.role_id)
                    .ToDictionary(g => g.Key, g => g.OrderBy(x => x.rank).ToList());
            }));
            tasks.Add(Task.Run(() =>
            {
                masterUnitSkillDataRf = new MasterUnitSkillDataRf();
                try
                {
                    masterUnitSkillDataRf.dict = GetDatas<MasterUnitSkillDataRf.UnitSkillDataRf>()
                        .GroupBy(x => x.skill_id)
                        .ToDictionary(g => g.Key, g => g.OrderBy(x => x.min_lv).ToList());
                }
                catch
                {
                    masterUnitSkillDataRf.dict = new Dictionary<int, List<MasterUnitSkillDataRf.UnitSkillDataRf>>();
                }
            }));

            tasks.Add(Task.Run(() =>
            {
                var enhanceLevel = GetDatas<ex_equipment_enhance_data>()
                    .GroupBy(x => x.rarity)
                    .ToDictionary(g => g.Key, g => g.Max(x => x.enhance_level));

                var subStatusGroupData = GetDatas<ex_equipment_sub_status_group>();
                var subStatusGroups = subStatusGroupData != null ?
                    subStatusGroupData.GroupBy(x => x.ex_equipment_id)
                        .ToDictionary(g => g.Key, g => g.ToList()) :
                    new Dictionary<int, List<ex_equipment_sub_status_group>>();

                var subStatusesData = GetDatas<ex_equipment_sub_status>();
                var subStatuses = subStatusesData != null ?
                    subStatusesData.GroupBy(x => x.group_id)
                    .ToDictionary(g => g.Key, g => g.ToDictionary(x => x.status, x => x.values)) : new Dictionary<int, Dictionary<int, int[]>>();

                var categories = GetDatas<ex_equipment_data>()
                    // .Where(x => x.clan_battle_equip_flag)
                    .Select(x =>
                    {
                        if (subStatusGroups.ContainsKey(x.ex_equipment_id))
                        {
                            foreach (var group in subStatusGroups[x.ex_equipment_id])
                            {
                                var groupId = group.group_id;
                                x.group_id = groupId;
                                if (subStatuses.ContainsKey(groupId))
                                {
                                    foreach (var kvp in subStatuses[groupId])
                                    {
                                        x.status[(eParamType)kvp.Key] = kvp.Value;
                                    }
                                }
                            }
                        }
                        return x.SetEnhanceLevelMax(enhanceLevel[x.rarity]);
                    })
                    .GroupBy(x => x.category)
                    .ToDictionary(g => g.Key,
                        g => g.ToDictionary(x => x.ex_equipment_id));

                var empty = new Dictionary<int, ex_equipment_data>();

                unitExEquips = GetDatas<unit_ex_equipment_slot>()
                    .ToDictionary(x => x.unit_id,
                        x => new Dictionary<int, ex_equipment_data>[]
                        {
                            categories.TryGetValue(x.slot_category_1, out var val) ? val : empty,
                            categories.TryGetValue(x.slot_category_2, out var val2) ? val2 : empty,
                            categories.TryGetValue(x.slot_category_3, out var val3) ? val3 : empty,
                        });

                extraEffectUnitGroupDic = GetDatas<extra_effect_unit_group>()
                    .GroupBy(x => x.group_id)
                    .ToDictionary(g => g.Key, g => g.ToList());
            }));
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
                    //eq.RemoveAt(eq.Count - 1);
                    //datas1.RemoveAt(datas1.Count - 1);
                }
                catch (KeyNotFoundException ex)
                {
                    Debug.LogWarning($"{pair.Key}的RANK数据丢失！");
                    continue;
                }
                UnitRankData rankData = new UnitRankData(eq, datas1);
                UnitDetailData detailData;
                List<int> talent_id = new List<int>();
                int unit_role_id = 0;
                try
                {
                    unit_data t4 = unitDic[pair.Key];
                    if (TalentDic != null)
                    {
                        talent_id = TalentDic[pair.Key].Select(x => x.talent_id).ToList();
                    }
                    if (UnitRoleDataDic.Count > 0)
                    {
                        unit_role_id = UnitRoleDataDic[pair.Key].unit_role_id;
                    }
                    detailData = new UnitDetailData(t4.unit_id,
                        t4.unit_name, t4.rarity, t4.motion_type, t4.se_type,
                        t4.search_area_width, t4.atk_type, (float)t4.normal_atk_cast_time, t4.guild_id, talent_id, unit_role_id)
                    {
                        moveSpeed = t4.move_speed,
                        prefabIdBattle = t4.prefab_id_battle != 0 ? t4.prefab_id_battle : t4.prefab_id
                    };
                }
                catch (KeyNotFoundException ex)
                {
                    Debug.LogWarning($"{pair.Key}的DETAIL数据丢失！");
                    detailData = new UnitDetailData();
                    continue;
                }
                UnitSkillData skillData = new UnitSkillData();
                unit_skill_data sk = unitSkillDataDic[pair.Key];
                if (pair.Key == 105701)
                {
                    sk = unitSkillDataDic.TryGetValue(170301, out var val) ? val : unitSkillDataDic[170101];
                }
                skillData.UB = sk.union_burst;
                skillData.UB_ev = sk.union_burst_evolution;
                skillData.skill_1 = sk.main_skill_1;
                skillData.skill_1_ev = sk.main_skill_evolution_1;
                skillData.skill_1_rev = sk.main_skill_revolution_1;
                skillData.skill_2 = sk.main_skill_2;
                skillData.skill_2_ev = sk.main_skill_evolution_2;
                skillData.skill_2_rev = sk.main_skill_revolution_2;
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
                skillData.SPskill_1_rev = sk.sp_skill_revolution_1;
                skillData.SPskill_2 = sk.sp_skill_2;
                skillData.SPskill_2_ev = sk.sp_skill_evolution_2;
                skillData.SPskill_2_rev = sk.sp_skill_revolution_2;
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
            skill_data[] list = GetDatas<skill_data>();
            if (list == null)
                return dic;
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

            var list = GetDatas<enemy_parameter>(a => a.enemy_id >= 400000000 && a.enemy_id <= 620000000);
            int idx = 0;
            foreach (var dd in list)
            {
                if (!dic1.ContainsKey(dd.unit_id))
                    continue;
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

                if (!unitSkillDataDic.ContainsKey(e.unit_id) || !unitAttackPatternDic.ContainsKey(e.unit_id)) continue;

                var skList = unitSkillDataDic[e.unit_id];//GetDatas<unit_skill_data>(a => a.unit_id == e.unit_id)[0];
                skillData.UB = skList.union_burst;
                skillData.raw_data = skList;
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
        private Dictionary<int, EnemyData> GetTalentQuestEnemyDataDic()
        {
            Dictionary<int, EnemyData> dic = new Dictionary<int, EnemyData>();
            //Dictionary<int, EnemyData> dic = new Dictionary<int, EnemyData>();

            var list = GetDatas<talent_quest_enemy_parameter>();
            if (list is not null)
            {
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

                    if (!unitSkillDataDic.ContainsKey(e.unit_id) || !unitAttackPatternDic.ContainsKey(e.unit_id)) continue;

                    var skList = unitSkillDataDic[e.unit_id];//GetDatas<unit_skill_data>(a => a.unit_id == e.unit_id)[0];
                    skillData.UB = skList.union_burst;
                    skillData.raw_data = skList;
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
            }
            return dic;
        }
        private Dictionary<int, EnemyData> GetAbyssEnemyDataDic()
        {
            Dictionary<int, EnemyData> dic = new Dictionary<int, EnemyData>();
            //Dictionary<int, EnemyData> dic = new Dictionary<int, EnemyData>();

            var list = GetDatas<abyss_enemy_parameter>();
            if (list is not null)
            {
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

                    if (!unitSkillDataDic.ContainsKey(e.unit_id) || !unitAttackPatternDic.ContainsKey(e.unit_id)) continue;

                    var skList = unitSkillDataDic[e.unit_id];//GetDatas<unit_skill_data>(a => a.unit_id == e.unit_id)[0];
                    skillData.UB = skList.union_burst;
                    skillData.raw_data = skList;
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
            }
            return dic;
        }
        private Dictionary<int, EnemyData> GetMirageEnemyDataDic()
        {
            Dictionary<int, EnemyData> dic = new Dictionary<int, EnemyData>();
            //Dictionary<int, EnemyData> dic = new Dictionary<int, EnemyData>();

            var list = GetDatas<mirage_enemy_parameter>();
            if (list is not null)
            {
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

                    if (!unitSkillDataDic.ContainsKey(e.unit_id) || !unitAttackPatternDic.ContainsKey(e.unit_id)) continue;

                    var skList = unitSkillDataDic[e.unit_id];//GetDatas<unit_skill_data>(a => a.unit_id == e.unit_id)[0];
                    skillData.UB = skList.union_burst;
                    skillData.raw_data = skList;
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
            }
            return dic;
        }
        private Dictionary<int, WaveGroupDataBase> GetWaveGroupDataDic<T>(Func<T, int> getId, Func<T, int> getWaveGroupId, Func<T, int, int> getEnemyId) where T : class, new()
        {
            Dictionary<int, WaveGroupDataBase> dic = new Dictionary<int, WaveGroupDataBase>();

            var list = GetDatas<T>();
            if (list is not null)
            {
                foreach (var data in list)
                {
                    WaveGroupDataBase waveGroupData = new WaveGroupDataBase();
                    waveGroupData.id = getId(data);
                    waveGroupData.wave_group_id = getWaveGroupId(data);

                    // 添加所有enemy_id到enemy_ids列表中，忽略值为0的enemy_id
                    waveGroupData.enemy_ids = new List<int>();
                    for (int i = 1; i <= 5; i++)
                    {
                        int enemyId = getEnemyId(data, i);
                        if (enemyId != 0) waveGroupData.enemy_ids.Add(enemyId);
                    }

                    dic.Add(waveGroupData.id, waveGroupData);
                }
            }

            return dic;
        }
        private Dictionary<int, EquipmentData> GetEquipmentData()
        {
            Dictionary<int, EquipmentData> dic = new Dictionary<int, EquipmentData>();
            var list = GetDatas<equipment_data>();
            var dic3 = GetDatas<equipment_enhance_data>().GroupBy(x => x.promotion_level).ToDictionary(g => g.Key, g => g.Max(x => x.equipment_enhance_level));

            foreach (var dd in list)
            {
                EquipmentData equipment = new EquipmentData();
                equipment.equipment_id = dd.equipment_id;
                equipment.equipment_name = dd.equipment_name;
                equipment.promotion_level = dic3.TryGetValue(dd.promotion_level, out var val) ? val : 0;
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

        private Dictionary<int, unique_equip_enhance_rate[]> GetUEQData()
        {
            var dic = GetDatasDic<unit_unique_equipment>(
                x => x.equip_id)?.ToDictionary(p => p.Key, p => p.Value as unit_unique_equip);

            dic = dic ?? GetDatasDic<unit_unique_equip>(x => x.equip_id);

            var basearr = GetDatasDic<unique_equipment_data>(x => x.equipment_id);

            return Combine(GetDatas<unique_equip_enhance_rate>(), GetDatas<unique_equipment_enhance_rate>())
                .GroupBy(x => x.equipment_id)
                .Where(x => dic.TryGetValue(x.Key, out var val) && val.equip_slot == 1)
                .ToDictionary(x => dic[x.Key].unit_id, x => x.OrderBy(x => x.min_lv).Select(x =>
                {
                    x.baseData = basearr[x.equipment_id];
                    return x;
                }).ToArray());
        }

        private Dictionary<int, unique_equip_enhance_rate[]> GetUEQ2Data()
        {
            var dic = GetDatasDic<unit_unique_equipment>(
                x => x.equip_id)?.ToDictionary(p => p.Key, p => p.Value as unit_unique_equip);

            dic = dic ?? GetDatasDic<unit_unique_equip>(x => x.equip_id);

            var basearr = GetDatasDic<unique_equipment_data>(x => x.equipment_id);

            return Combine(GetDatas<unique_equip_enhance_rate>(), GetDatas<unique_equipment_enhance_rate>())
                .GroupBy(x => x.equipment_id)
                .Where(x => dic.TryGetValue(x.Key, out var val) && val.equip_slot == 2)
                .ToDictionary(x => dic[x.Key].unit_id, x => x.OrderBy(x => x.min_lv).Select(x =>
                {
                    x.baseData = basearr[x.equipment_id];
                    return x;
                }).ToArray());
        }

        private IEnumerable<unique_equip_enhance_rate> Combine(unique_equip_enhance_rate[] a, unique_equipment_enhance_rate[] b)
        {
            if (a == null)
            {
                foreach (var x in b)
                {
                    yield return x;
                }
                yield break;
            }

            var set = new HashSet<int>();

            foreach (var x in a)
            {
                yield return x;
                set.Add(x.equipment_id);
            }

            foreach (var x in b)
            {
                if (!set.Contains(x.equipment_id)) yield return x;
            }
        }

        private Dictionary<int, string> GetExEquipmentName_cn()
        {
            Dictionary<int, string> dic = new Dictionary<int, string>();
            var list = GetDatas<ex_equipment_data>();
            foreach (var dd in list)
            {
                dic.Add(dd.ex_equipment_id, dd.name);
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

        public (Dictionary<int, UnitStoryData>, Dictionary<int, List<int>>, Dictionary<int, int>) GetUnitStoryData()
        {
            Dictionary<int, UnitStoryData> unitStoryDic = new Dictionary<int, UnitStoryData>();
            Dictionary<int, List<int>> unitStoryEffectDic = new Dictionary<int, List<int>>();
            Dictionary<int, List<((int, int)[], int)>> add_vals = new Dictionary<int, List<((int, int)[], int)>>();
            Dictionary<int, List<int>> ef_id = new Dictionary<int, List<int>>();
            var list = GetDatas<chara_story_status>();
            var lovedict = GetDatas<story_detail>().ToDictionary(x => x.story_id, x => x.love_level);
            foreach (var dd in list)
            {
                int storyid = dd.story_id;
                if (!lovedict.ContainsKey(storyid)) continue;
                int unitid = storyid / 1000 * 100 + 1;
                if (!add_vals.ContainsKey(unitid))
                {
                    add_vals.Add(unitid, new List<((int, int)[], int)>());
                    ef_id[unitid] = dd.GetCharIdList();
                }
                var li = dd.GetStatesValueList();
                add_vals[unitid].Add((li, lovedict[storyid]));
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

            return (unitStoryDic, unitStoryEffectDic, lovedict);
        }

        public Dictionary<int, TalentSkillNode> GetTalentSkillData()
        {
            var node_list = GetDatas<talent_skill_node>();
            var titleDic = GetDatas<team_skill_title>().ToDictionary(x => x.title_id, x => x.title_text);
            var enhanceLevelDic = GetDatas<talent_skill_enhance_level>().ToDictionary(x => x.enhance_level_id);

            return node_list.ToDictionary(
                dd => dd.node_id,
                dd =>
                {
                    var enhanceLevelDict = new[]
                    {
                        dd.enhance_level_id_1,
                        dd.enhance_level_id_2,
                        dd.enhance_level_id_3,
                        dd.enhance_level_id_4,
                        dd.enhance_level_id_5
                    }
                    .Where(id => id != 0 && enhanceLevelDic.TryGetValue(id, out var data))
                    .ToDictionary(
                        id => id,
                        id => new[]
                        {
                            enhanceLevelDic[id].enhance_id_1,
                            enhanceLevelDic[id].enhance_id_2,
                            enhanceLevelDic[id].enhance_id_3,
                            enhanceLevelDic[id].enhance_id_4,
                            enhanceLevelDic[id].enhance_id_5
                        }.Where(x => x != 0).ToArray()
                    );

                    string titleText = titleDic.TryGetValue(dd.title_id, out var text) ? text : "未知";

                    return new TalentSkillNode(
                        dd.node_id,
                        dd.page_num,
                        dd.pos_x,
                        dd.pos_y,
                        new List<int> { dd.pre_node_1, dd.pre_node_2, dd.pre_node_3, dd.pre_node_4, dd.pre_node_5 },
                        enhanceLevelDict,
                        dd.icon_id,
                        dd.title_id,
                        titleText
                    );
                }
            );
        }
        public Dictionary<int, TalentSkillEnhanceData> GetTalentSkillEnhanceData()
        {
            var enhanceList = GetDatas<talent_skill_enhance_data>();

            return enhanceList.ToDictionary(
                dd => dd.enhance_id,
                dd =>
                {
                    var talentIds = new List<int>
                    {
                        dd.talent_id_1,
                        dd.talent_id_2,
                        dd.talent_id_3,
                        dd.talent_id_4,
                        dd.talent_id_5
                    };

                    talentIds = talentIds.Where(id => id != 0).ToList();

                    return new TalentSkillEnhanceData(
                        enhance_id: dd.enhance_id,
                        talent_ids: talentIds,
                        parameter_type: dd.parameter_type,
                        enhance_value: dd.enhance_value
                    );
                }
            );
        }
        public Dictionary<int, TeamSkillNode> GetTeamSkillData()
        {
            var node_list = GetDatas<team_skill_node>();
            var titleDic = GetDatas<team_skill_title>().ToDictionary(x => x.title_id, x => x.title_text);
            var enhanceLevelDic = GetDatas<team_skill_enhance_level>().ToDictionary(x => x.enhance_level_id);

            return node_list.ToDictionary(
                dd => dd.node_id,
                dd =>
                {
                    var enhanceLevelDict = new[]
                    {
                        dd.enhance_level_id_1,
                        dd.enhance_level_id_2,
                        dd.enhance_level_id_3,
                        dd.enhance_level_id_4,
                        dd.enhance_level_id_5
                    }
                    .Where(id => id != 0 && enhanceLevelDic.TryGetValue(id, out var data))
                    .ToDictionary(
                        id => id,
                        id => new[]
                        {
                            enhanceLevelDic[id].enhance_id_1,
                            enhanceLevelDic[id].enhance_id_2,
                            enhanceLevelDic[id].enhance_id_3,
                            enhanceLevelDic[id].enhance_id_4,
                            enhanceLevelDic[id].enhance_id_5
                        }.Where(x => x != 0).ToArray()
                    );

                    string titleText = titleDic.TryGetValue(dd.title_id, out var text) ? text : "未知";

                    return new TeamSkillNode(
                        dd.node_id,
                        dd.pos_x,
                        dd.pos_y,
                        dd.pre_node_id,
                        enhanceLevelDict,
                        dd.icon_id,
                        dd.noise_id,
                        dd.title_id,
                        titleText
                    );
                }
            );
        }
        public Dictionary<int, TeamSkillEnhanceData> GetTeamSkillEnhanceData()
        {
            var enhanceList = GetDatas<team_skill_enhance_data>();

            return enhanceList.ToDictionary(
                dd => dd.enhance_id,
                dd =>
                {
                    var talentIds = new List<int>
                    {
                        dd.talent_id_1,
                        dd.talent_id_2,
                        dd.talent_id_3,
                        dd.talent_id_4,
                        dd.talent_id_5
                    };

                    talentIds = talentIds.Where(id => id != 0).ToList();

                    return new TeamSkillEnhanceData(
                        enhance_id: dd.enhance_id,
                        talent_ids: talentIds,
                        parameter_type: dd.parameter_type,
                        enhance_value: dd.enhance_value
                    );
                }
            );
        }
        private Dictionary<int, int[]> GetResistData()
        {
            Dictionary<int, int[]> dict = new Dictionary<int, int[]>();

            var resistList = GetDatas<resist_data>();
            foreach (var resist in resistList)
            {
                dict[resist.resist_status_id] = new int[]
                {
                    resist.ailment_1,
                    resist.ailment_2,
                    resist.ailment_3,
                    resist.ailment_4,
                    resist.ailment_5,
                    resist.ailment_6,
                    resist.ailment_7,
                    resist.ailment_8,
                    resist.ailment_9,
                    resist.ailment_10,
                    resist.ailment_11,
                    resist.ailment_12,
                    resist.ailment_13,
                    resist.ailment_14,
                    resist.ailment_15,
                    resist.ailment_16,
                    resist.ailment_17,
                    resist.ailment_18,
                    resist.ailment_19,
                    resist.ailment_20,
                    resist.ailment_21,
                    resist.ailment_22,
                    resist.ailment_23,
                    resist.ailment_24,
                    resist.ailment_25,
                    resist.ailment_26,
                    resist.ailment_27,
                    resist.ailment_28,
                    resist.ailment_29,
                    resist.ailment_30,
                    resist.ailment_31,
                    resist.ailment_32,
                    resist.ailment_33,
                    resist.ailment_34,
                    resist.ailment_35,
                    resist.ailment_36,
                    resist.ailment_37,
                    resist.ailment_38,
                    resist.ailment_39,
                    resist.ailment_40,
                    resist.ailment_41,
                    resist.ailment_42,
                    resist.ailment_43,
                    resist.ailment_44,
                    resist.ailment_45,
                    resist.ailment_46,
                    resist.ailment_47,
                    resist.ailment_48,
                    resist.ailment_49,
                    resist.ailment_50
                };
            }

            return dict;
        }

        private Dictionary<int, int[]> GetWeaknessDict()
        {
            Dictionary<int, int[]> dict = new Dictionary<int, int[]>();

            if (EnemyTalentWeaknessDic == null || TalentWeaknessDic == null)
                return dict;

            foreach (var etw in EnemyTalentWeaknessDic.Values)
            {
                int enemyId = etw.enemy_id;
                int resistId = etw.resist_id;

                if (TalentWeaknessDic.TryGetValue(resistId, out var tw))
                {
                    dict[enemyId] = new int[] {
                        tw.talent_1,
                        tw.talent_2,
                        tw.talent_3,
                        tw.talent_4,
                        tw.talent_5
                    };
                }
            }

            return dict;
        }

        private Dictionary<int, talentFormationData> GetTalentFormationData()
        {
            var overwriteDic = GetDatasDic<talent_formation_overwrite>(a => a.id);
            var bonusDic = GetDatasDic<talent_formation_bonus>(a => a.formation_bonus_id);

            if (overwriteDic == null || bonusDic == null)
                return new Dictionary<int, talentFormationData>();
            overwriteDic[1] = new talent_formation_overwrite
            {
                id = 1,
                content_type = 1,
                target_value_1 = 1,
                target_value_2 = 1,
                formation_bonus_id_1 = 1,
                formation_bonus_id_2 = 1,
                formation_bonus_id_3 = 1,
                formation_bonus_id_4 = 1,
                formation_bonus_id_5 = 1
            };
            var result = new Dictionary<int, talentFormationData>();
            foreach (var kvp in overwriteDic)
            {
                result[kvp.Key] = new talentFormationData(kvp.Value, bonusDic);
            }
            return result;
        }

        private Dictionary<int, UnitRoleMasterySlotEnhanceData> GetUnitRoleMasterySlotEnhanceData()
        {
            var slotDataList = GetDatas<unit_role_mastery_slot_data>();
            var enhanceDataList = GetDatas<unit_role_mastery_enhance_data>();

            // 将slot数据按mastery_id和slot_level分组
            var slotDataDict = new Dictionary<int, Dictionary<int, unit_role_mastery_slot_data>>();
            foreach (var slotData in slotDataList)
            {
                if (!slotDataDict.ContainsKey(slotData.mastery_id))
                    slotDataDict[slotData.mastery_id] = new Dictionary<int, unit_role_mastery_slot_data>();

                slotDataDict[slotData.mastery_id][slotData.slot_level] = slotData;
            }

            // 将增强数据按mastery_id、slot_level和enhance_level分组
            var enhanceDataDict = new Dictionary<int, Dictionary<int, Dictionary<int, unit_role_mastery_enhance_data>>>();
            foreach (var enhanceData in enhanceDataList)
            {
                if (!enhanceDataDict.ContainsKey(enhanceData.mastery_id))
                    enhanceDataDict[enhanceData.mastery_id] = new Dictionary<int, Dictionary<int, unit_role_mastery_enhance_data>>();

                if (!enhanceDataDict[enhanceData.mastery_id].ContainsKey(enhanceData.slot_level))
                    enhanceDataDict[enhanceData.mastery_id][enhanceData.slot_level] = new Dictionary<int, unit_role_mastery_enhance_data>();

                enhanceDataDict[enhanceData.mastery_id][enhanceData.slot_level][enhanceData.enhance_level] = enhanceData;
            }

            // 创建最终的整合字典
            UnitRoleMasterySlotEnhanceDataDic = new Dictionary<int, UnitRoleMasterySlotEnhanceData>();

            foreach (var slotDataByMastery in slotDataDict)
            {
                var masteryId = slotDataByMastery.Key;
                var slotDataItems = slotDataByMastery.Value;

                var integratedData = new UnitRoleMasterySlotEnhanceData
                {
                    mastery_id = masteryId,
                    slot_level_info = new Dictionary<int, SlotLevelInfo>()
                };

                // 为每个slot_level添加图标、名称信息以及增强数据
                foreach (var slotDataEntry in slotDataItems)
                {
                    var slotLevel = slotDataEntry.Key;
                    var slotData = slotDataEntry.Value;

                    var slotLevelInfo = new SlotLevelInfo
                    {
                        icon_id = slotData.icon_id,
                        name = slotData.name,
                        enhance_level_info = new Dictionary<int, EnhanceLevelInfo>()
                    };

                    // 添加增强数据到SlotLevelInfo中
                    if (enhanceDataDict.ContainsKey(masteryId) &&
                        enhanceDataDict[masteryId].ContainsKey(slotLevel))
                    {
                        var enhanceDataByLevel = enhanceDataDict[masteryId][slotLevel];
                        foreach (var enhanceDataEntry in enhanceDataByLevel)
                        {
                            var enhanceLevel = enhanceDataEntry.Key;
                            var enhanceData = enhanceDataEntry.Value;

                            var enhanceLevelInfo = new EnhanceLevelInfo
                            {
                                role_param_type_1 = enhanceData.role_param_type_1,
                                role_param_type_2 = enhanceData.role_param_type_2,
                                enhance_value_1 = enhanceData.enhance_value_1,
                                enhance_value_2 = enhanceData.enhance_value_2
                            };

                            slotLevelInfo.enhance_level_info[enhanceLevel] = enhanceLevelInfo;
                        }
                    }

                    // 将SlotLevelInfo添加到slot_level_info字典中
                    integratedData.slot_level_info[slotLevel] = slotLevelInfo;
                }

                UnitRoleMasterySlotEnhanceDataDic[masteryId] = integratedData;
            }

            return UnitRoleMasterySlotEnhanceDataDic;
        }

        private Dictionary<int, int> GetAbyssQuestFilteredDic()
        {
            var abyssQuestDataList = GetDatas<abyss_quest_data>();

            if (abyssQuestDataList == null || !abyssQuestDataList.Any())
                return new Dictionary<int, int>();

            return abyssQuestDataList
                .GroupBy(x => x.abyss_id)
                .Where(g =>
                {
                    var prefixes = g.Select(x => x.wave_group_id / 10000 * 10000) // 筛选wave_group_id前5个数字
                                   .Distinct()
                                   .Take(2)
                                   .ToList();
                    return prefixes.Count == 1;
                })
                .ToDictionary(g => g.Key, g => g.First().wave_group_id / 10000 * 10000);
        }
        private Dictionary<int, int> GetAbyssBossFilteredDic()
        {
            var abyssBossDataList = GetDatas<abyss_boss_data>();

            if (abyssBossDataList == null || !abyssBossDataList.Any())
                return new Dictionary<int, int>();

            return abyssBossDataList
                .GroupBy(x => x.abyss_id)
                .Where(g =>
                {
                    var prefixes = g.Select(x => x.wave_group_id / 10000 * 10000)
                                   .Distinct()
                                   .Take(2)
                                   .ToList();
                    return prefixes.Count == 1;
                })
                .ToDictionary(g => g.Key, g => g.First().wave_group_id / 10000 * 10000);
        }
    }
}
