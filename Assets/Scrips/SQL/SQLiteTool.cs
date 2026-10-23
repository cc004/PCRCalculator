using System.Collections;
using System.Collections.Generic;
using SQLite4Unity3d;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
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
            this.path = ResolveSourceDatabasePath(mgr);

            if (File.Exists(this.path))
                Debug.Log($"使用数据库来源 {mgr.DataVer}.db: {this.path}");
            else
                Debug.Log($"未找到{mgr.DataVer}.db，将自动下载存放至公共目录，如果是jp数据库需要反哈希，无法直接食用");

            if (!File.Exists(this.path))
            {
                DownloadAndPrepareSourceDatabase(this.path);
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
                try
                {
                    if (!string.IsNullOrEmpty(dbPath) && File.Exists(dbPath))
                        File.Delete(dbPath);
                }
                catch (Exception cleanupException)
                {
                    Debug.LogWarning($"清理失败的数据库补丁临时文件时出错: {cleanupException}");
                }

                throw new InvalidDataException($"数据库 {mgr.DataVer} 补丁或解析失败", e);
            }

            ConnectDB(path, @readonly);
        }

        private void DownloadAndPrepareSourceDatabase(string destinationPath)
        {
            var directory = Path.GetDirectoryName(destinationPath);
            if (!string.IsNullOrEmpty(directory))
                Directory.CreateDirectory(directory);

            var stagingPath = $"{destinationPath}.download-{Guid.NewGuid():N}.tmp";
            try
            {
                File.WriteAllBytes(stagingPath, mgr.ResolveDatabase());
                this.path = stagingPath;
                Unhash();

                if (File.Exists(destinationPath))
                    throw new IOException($"数据库正式路径在发布前意外出现: {destinationPath}");
                File.Move(stagingPath, destinationPath);
            }
            catch (Exception exception)
            {
                if (File.Exists(stagingPath))
                {
                    if (DatabaseCacheContract.TryQuarantineRejectedCache(
                            stagingPath,
                            out var quarantinedPath,
                            out var quarantineError))
                    {
                        Debug.LogError(
                            $"数据库下载/反哈希失败，staging 已保留: dataVersion={mgr.DataVer}, " +
                            $"file={Path.GetFileName(quarantinedPath)}, error={exception}");
                    }
                    else
                    {
                        Debug.LogError(
                            $"数据库下载/反哈希失败且 staging 无法保留: dataVersion={mgr.DataVer}, " +
                            $"quarantineError={quarantineError}, error={exception}");
                        try
                        {
                            File.Delete(stagingPath);
                        }
                        catch (Exception cleanupException)
                        {
                            Debug.LogError(
                                $"数据库 staging 清理失败: dataVersion={mgr.DataVer}, error={cleanupException}");
                        }
                    }
                }

                throw new InvalidDataException(
                    $"数据库 {mgr.DataVer} 下载或反哈希失败，未发布到正式来源路径",
                    exception);
            }
            finally
            {
                this.path = destinationPath;
            }
        }

        public static string ResolveSourceDatabasePath(IDataBaseManager manager)
        {
            if (manager == null)
                return null;

            var rootPath = $"{manager.DataVer}.db";
            if (File.Exists(rootPath))
                return rootPath;

#if UNITY_STANDALONE_OSX && !UNITY_EDITOR
            var macOSAppRootPath = GetMacOSAppRootPath(rootPath);
            if (File.Exists(macOSAppRootPath))
                return macOSAppRootPath;
#endif

            return Path.Combine(ABExTool.persistentDataPath, $"{manager.DataVer}.db");
        }

#if UNITY_STANDALONE_OSX && !UNITY_EDITOR
        private static string GetMacOSAppRootPath(string fileName)
        {
            DirectoryInfo directory = new DirectoryInfo(UnityEngine.Application.dataPath);
            while (directory != null && !directory.Name.EndsWith(".app", StringComparison.OrdinalIgnoreCase))
            {
                directory = directory.Parent;
            }

            return directory?.Parent == null ? string.Empty : Path.Combine(directory.Parent.FullName, fileName);
        }
#endif

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
            return ReadDatas(func, allowMissingSchema: false);
        }

        protected T[] GetOptionalDatas<T>(Func<T, bool> func = null) where T : new()
        {
            return ReadDatas(func, allowMissingSchema: true);
        }

        private T[] ReadDatas<T>(Func<T, bool> func, bool allowMissingSchema) where T : new()
        {
            var conn = new SQLiteConnection(path, @readonly ? SQLiteOpenFlags.ReadOnly : SQLiteOpenFlags.ReadWrite);
            lock (connections) connections.Add(conn);
            try
            {
                return func == null ? conn.Table<T>().ToArray() : conn.Table<T>().Where(func).ToArray();
            }
            catch (Exception ex)
            {
                var message = $"数据库读取失败: dataVersion={mgr.DataVer}, table={typeof(T).Name}";
                if (allowMissingSchema && IsMissingOptionalSchema(ex))
                {
                    Debug.LogWarning($"{message}; optionalSchema=true; error={ex.Message}");
                    return Array.Empty<T>();
                }

                var wrapped = new InvalidDataException(message, ex);
                Debug.LogError($"{wrapped}");
                throw wrapped;
            }
        }

        protected Dictionary<int, int[]> ReadResistDataByAilmentId()
        {
            try
            {
                using var dbConnection = new SqliteConnection("Data Source=" + path);
                dbConnection.Open();

                var ailmentColumns = new List<(int ailmentId, string columnName)>();
                using (var schemaCommand = dbConnection.CreateCommand())
                {
                    schemaCommand.CommandText = "PRAGMA table_info(resist_data)";
                    using var schemaReader = schemaCommand.ExecuteReader();
                    while (schemaReader.Read())
                    {
                        var columnName = schemaReader["name"]?.ToString();
                        const string prefix = "ailment_";
                        if (columnName != null &&
                            columnName.StartsWith(prefix, StringComparison.Ordinal) &&
                            int.TryParse(columnName.Substring(prefix.Length), out var ailmentId) &&
                            ailmentId > 0)
                        {
                            ailmentColumns.Add((ailmentId, columnName));
                        }
                    }
                }

                if (ailmentColumns.Count == 0)
                    throw new InvalidDataException("resist_data 不包含 ailment_* 列");

                ailmentColumns.Sort((left, right) => left.ailmentId.CompareTo(right.ailmentId));
                var maxAilmentId = ailmentColumns[ailmentColumns.Count - 1].ailmentId;
                var result = new Dictionary<int, int[]>();

                using var dataCommand = dbConnection.CreateCommand();
                dataCommand.CommandText = "SELECT * FROM resist_data";
                using var dataReader = dataCommand.ExecuteReader();
                var resistStatusIdOrdinal = dataReader.GetOrdinal("resist_status_id");
                var ailmentOrdinals = ailmentColumns
                    .Select(column => (column.ailmentId, ordinal: dataReader.GetOrdinal(column.columnName)))
                    .ToArray();

                while (dataReader.Read())
                {
                    var ailments = new int[maxAilmentId];
                    foreach (var column in ailmentOrdinals)
                    {
                        if (!dataReader.IsDBNull(column.ordinal))
                            ailments[column.ailmentId - 1] = Convert.ToInt32(dataReader.GetValue(column.ordinal));
                    }

                    result[Convert.ToInt32(dataReader.GetValue(resistStatusIdOrdinal))] = ailments;
                }

                return result;
            }
            catch (Exception ex)
            {
                var wrapped = new InvalidDataException(
                    $"数据库读取失败: dataVersion={mgr.DataVer}, table=resist_data",
                    ex);
                Debug.LogError($"{wrapped}");
                throw wrapped;
            }
        }

        private static bool IsMissingOptionalSchema(Exception exception)
        {
            var message = exception?.ToString() ?? string.Empty;
            return message.IndexOf("no such table", StringComparison.OrdinalIgnoreCase) >= 0 ||
                   message.IndexOf("no such column", StringComparison.OrdinalIgnoreCase) >= 0;
        }
        public Dictionary<int, T> GetDatasDic<T>(Func<T, int> func, Func<T, bool> select = null) where T : new()
        {
            Dictionary<int, T> dict = new Dictionary<int, T>();
            var list = GetDatas<T>(select);
            foreach (var item in list)
                dict[func(item)] = item;
            return dict;
        }

        protected Dictionary<int, T> GetOptionalDatasDic<T>(Func<T, int> func, Func<T, bool> select = null)
            where T : new()
        {
            var dict = new Dictionary<int, T>();
            foreach (var item in GetOptionalDatas<T>(select))
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
            SQLiteConnection[] openConnections;
            lock (connections)
            {
                openConnections = connections.ToArray();
                connections.Clear();
            }

            foreach (var connection in openConnections)
            {
                try
                {
                    connection.Close();
                }
                catch (Exception exception)
                {
                    Debug.LogWarning($"关闭数据库连接失败: dataVersion={mgr.DataVer}, error={exception}");
                }
            }
        }
        private void Unhash()
        {
            string rainbowJsonContent = MainManager.Instance.LoadJsonDatas("Datas/rainbow");
            if (string.IsNullOrEmpty(rainbowJsonContent))
            {
                throw new InvalidDataException("Rainbow table not found; database unhashing cannot continue.");
            }

            Debug.Log($"Start Unhashing {mgr.DataVer}.db");
            var stopwatch = Stopwatch.StartNew();
            var rainbow = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(rainbowJsonContent);
            if (rainbow == null)
                throw new InvalidDataException("Rainbow table is empty.");

            using (var dbConnection = new SqliteConnection($"Data Source={path};Version=3;"))
            {
                dbConnection.Open();

                int restoredTables = UnhashDatabase(dbConnection, rainbow);
                Debug.Log($"Unhashing complete: {restoredTables} tables, {stopwatch.Elapsed.TotalSeconds:F2}s.");
            }
        }

        private sealed class UnhashTable
        {
            public string Name;
            public string CreateSql;
            public List<string> Columns;
            public Dictionary<string, string> OriginalColumns;
            public List<string> PrimaryKeyColumns;
            public int KnownColumns;
        }

        protected List<string> GetTableColumns(SqliteConnection dbConnection, string tableName)
        {
            return ReadTableColumns(dbConnection, null, tableName);
        }

        private static List<string> ReadTableColumns(SqliteConnection dbConnection, SqliteTransaction transaction, string tableName,
            SortedDictionary<int, string> primaryKey = null)
        {
            List<string> columns = new List<string>();
            using (var command = dbConnection.CreateCommand())
            {
                command.Transaction = transaction;
                command.CommandText = $"PRAGMA table_info({QuoteIdentifier(tableName)})";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        columns.Add(reader.GetString(1));
                        int keyPosition = Convert.ToInt32(reader.GetValue(5));
                        if (primaryKey != null && keyPosition > 0)
                            primaryKey.Add(keyPosition, reader.GetString(1));
                    }
                }
            }
            return columns;
        }

        // Look up only tables in this database; rainbow contains aliases for many historical versions.
        private static int UnhashDatabase(SqliteConnection dbConnection,
            Dictionary<string, Dictionary<string, string>> rainbow)
        {
            using (var transaction = dbConnection.BeginTransaction())
            {
                try
                {
                    var tables = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                    using (var command = dbConnection.CreateCommand())
                    {
                        command.Transaction = transaction;
                        command.CommandText = "SELECT name, sql FROM sqlite_master WHERE type='table'";
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string name = reader.GetString(0);
                                if (!name.StartsWith("sqlite_", StringComparison.OrdinalIgnoreCase))
                                    tables.Add(name, reader.GetString(1));
                            }
                        }
                    }

                    var groups = new Dictionary<string, List<UnhashTable>>(StringComparer.OrdinalIgnoreCase);
                    foreach (var table in tables)
                    {
                        if (!rainbow.TryGetValue(table.Key, out var mapping))
                            continue;
                        if (mapping == null || !mapping.TryGetValue("--table_name", out string originalName) ||
                            string.IsNullOrEmpty(originalName))
                            throw new InvalidDataException($"Invalid rainbow table mapping: {table.Key}");
                        if (!groups.TryGetValue(originalName, out var aliases))
                        {
                            aliases = new List<UnhashTable>();
                            groups.Add(originalName, aliases);
                        }
                        aliases.Add(ReadUnhashTable(dbConnection, transaction, table.Key, table.Value, mapping));
                    }

                    foreach (var group in groups)
                    {
                        string originalName = group.Key;
                        var aliases = group.Value;
                        UnhashTable source;
                        if (tables.TryGetValue(originalName, out string originalSql))
                        {
                            source = aliases.FirstOrDefault(table =>
                                string.Equals(table.Name, originalName, StringComparison.OrdinalIgnoreCase)) ??
                                ReadUnhashTable(dbConnection, transaction, originalName, originalSql, null);
                        }
                        else
                        {
                            source = aliases.OrderByDescending(table => table.KnownColumns)
                                .ThenBy(table => table.Name, StringComparer.Ordinal).First();
                        }

                        // Transition releases contain two aliases of the same table, sometimes with permuted columns.
                        // Compare recovered columns by name before discarding an alias; never append duplicate rows.
                        foreach (var alias in aliases)
                            if (alias != source)
                                VerifyDuplicateTable(dbConnection, transaction, originalName, source, alias);

                        if (!string.Equals(source.Name, originalName, StringComparison.OrdinalIgnoreCase))
                        {
                            var replacements = source.OriginalColumns.ToDictionary(pair => pair.Value,
                                pair => pair.Key, StringComparer.OrdinalIgnoreCase);
                            replacements[source.Name] = originalName;
                            ExecuteUnhashCommand(dbConnection, transaction, RewriteCreateTable(source.CreateSql, replacements));
                            string destinationColumns = string.Join(", ", source.Columns.Select(column =>
                                QuoteIdentifier(replacements[column])));
                            string sourceColumns = string.Join(", ", source.Columns.Select(QuoteIdentifier));
                            ExecuteUnhashCommand(dbConnection, transaction,
                                $"INSERT INTO {QuoteIdentifier(originalName)} ({destinationColumns}) " +
                                $"SELECT {sourceColumns} FROM {QuoteIdentifier(source.Name)}");
                        }

                        foreach (var alias in aliases)
                            if (!string.Equals(alias.Name, originalName, StringComparison.OrdinalIgnoreCase))
                                ExecuteUnhashCommand(dbConnection, transaction, $"DROP TABLE {QuoteIdentifier(alias.Name)}");
                    }
                    transaction.Commit();
                    return groups.Count;
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw new InvalidDataException("数据库反哈希事务失败", e);
                }
            }
        }

        private static UnhashTable ReadUnhashTable(SqliteConnection connection, SqliteTransaction transaction,
            string name, string createSql, Dictionary<string, string> mapping)
        {
            var primaryKey = new SortedDictionary<int, string>();
            var table = new UnhashTable
            {
                Name = name,
                CreateSql = createSql,
                Columns = ReadTableColumns(connection, transaction, name, primaryKey),
                OriginalColumns = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            };
            table.PrimaryKeyColumns = primaryKey.Values.ToList();
            foreach (string column in table.Columns)
            {
                string originalColumn = column;
                if (mapping != null && mapping.TryGetValue(column, out string recoveredColumn))
                {
                    if (string.IsNullOrEmpty(recoveredColumn))
                        throw new InvalidDataException($"Invalid rainbow column mapping: {name}.{column}");
                    originalColumn = recoveredColumn;
                    table.KnownColumns++;
                }
                if (table.OriginalColumns.ContainsKey(originalColumn))
                    throw new InvalidDataException($"Duplicate recovered column: {name}.{originalColumn}");
                table.OriginalColumns.Add(originalColumn, column);
            }
            return table;
        }

        private static void VerifyDuplicateTable(SqliteConnection connection, SqliteTransaction transaction,
            string originalName, UnhashTable source, UnhashTable alias)
        {
            long sourceCount = Convert.ToInt64(ExecuteUnhashScalar(connection, transaction,
                $"SELECT COUNT(*) FROM {QuoteIdentifier(source.Name)}"));
            long aliasCount = Convert.ToInt64(ExecuteUnhashScalar(connection, transaction,
                $"SELECT COUNT(*) FROM {QuoteIdentifier(alias.Name)}"));
            if (source.Columns.Count != alias.Columns.Count || sourceCount != aliasCount)
                throw new InvalidDataException($"Transition tables differ: {originalName}, {source.Name}, {alias.Name}");
            if (sourceCount == 0)
                return;

            var commonColumns = source.OriginalColumns.Keys.Where(alias.OriginalColumns.ContainsKey).ToList();
            if (commonColumns.Count == 0)
            {
                VerifyUnmappedDuplicate(connection, transaction, originalName, source, alias);
                return;
            }
            string sourceColumns = string.Join(", ", commonColumns.Select(column => QuoteIdentifier(source.OriginalColumns[column])));
            string aliasColumns = string.Join(", ", commonColumns.Select(column => QuoteIdentifier(alias.OriginalColumns[column])));
            // Include multiplicity, so identical projections with different duplicate counts also fail verification.
            string sourceRows = $"SELECT {sourceColumns}, COUNT(*) FROM {QuoteIdentifier(source.Name)} GROUP BY {sourceColumns}";
            string aliasRows = $"SELECT {aliasColumns}, COUNT(*) FROM {QuoteIdentifier(alias.Name)} GROUP BY {aliasColumns}";
            string comparison = $"SELECT EXISTS({sourceRows} EXCEPT {aliasRows}) OR EXISTS({aliasRows} EXCEPT {sourceRows})";
            if (Convert.ToInt64(ExecuteUnhashScalar(connection, transaction, comparison)) != 0)
                throw new InvalidDataException($"Transition table data differs: {originalName}, {source.Name}, {alias.Name}");
        }

        private static void VerifyUnmappedDuplicate(SqliteConnection connection, SqliteTransaction transaction,
            string originalName, UnhashTable source, UnhashTable alias)
        {
            // Some known tables have no recovered field names yet. Align rows by the declared primary key,
            // then compare whole column streams as a multiset to allow a fixed permutation of columns.
            if (source.PrimaryKeyColumns.Count == 0 || source.PrimaryKeyColumns.Count != alias.PrimaryKeyColumns.Count)
                throw new InvalidDataException($"Cannot verify transition tables without a primary key: {originalName}");
            var sourceHashes = ReadColumnFingerprints(connection, transaction, source);
            var aliasHashes = ReadColumnFingerprints(connection, transaction, alias);
            bool sameKeys = source.PrimaryKeyColumns.Select(column => sourceHashes[column]).SequenceEqual(
                alias.PrimaryKeyColumns.Select(column => aliasHashes[column]));
            bool sameColumns = sourceHashes.Values.OrderBy(hash => hash, StringComparer.Ordinal).SequenceEqual(
                aliasHashes.Values.OrderBy(hash => hash, StringComparer.Ordinal));
            if (!sameKeys || !sameColumns)
                throw new InvalidDataException($"Unmapped transition table data differs: {originalName}");
        }

        private static Dictionary<string, string> ReadColumnFingerprints(SqliteConnection connection,
            SqliteTransaction transaction, UnhashTable table)
        {
            var hashes = table.Columns.Select(column => SHA256.Create()).ToArray();
            try
            {
                using (var command = connection.CreateCommand())
                {
                    command.Transaction = transaction;
                    command.CommandText = $"SELECT {string.Join(", ", table.Columns.Select(QuoteIdentifier))} " +
                        $"FROM {QuoteIdentifier(table.Name)} ORDER BY {string.Join(", ", table.PrimaryKeyColumns.Select(QuoteIdentifier))}";
                    using (var reader = command.ExecuteReader())
                        while (reader.Read())
                            for (int i = 0; i < hashes.Length; i++)
                            {
                                object value = reader.GetValue(i);
                                // Keep SQL value types and cell boundaries in the fingerprint, including NULL and blobs.
                                byte[] bytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(
                                    new object[] { value.GetType().FullName, value }) + "\n");
                                hashes[i].TransformBlock(bytes, 0, bytes.Length, bytes, 0);
                            }
                }
                var result = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                for (int i = 0; i < hashes.Length; i++)
                {
                    hashes[i].TransformFinalBlock(new byte[0], 0, 0);
                    result.Add(table.Columns[i], Convert.ToBase64String(hashes[i].Hash));
                }
                return result;
            }
            finally
            {
                foreach (var hash in hashes) hash.Dispose();
            }
        }

        private static string QuoteIdentifier(string name)
        {
            return "\"" + name.Replace("\"", "\"\"") + "\"";
        }

        private static readonly Regex SqlIdentifierTokens = new Regex(
            @"'(''|[^'])*'|--[^\r\n]*|/\*[\s\S]*?\*/|""(""""|[^""])*""|`(``|[^`])*`|\[[^\]]*\]|[\w$]+|[^\s]",
            RegexOptions.CultureInvariant);

        private static string RewriteCreateTable(string sql, Dictionary<string, string> replacements)
        {
            // Replace identifier tokens once, preserving defaults, comments, constraints and unknown columns.
            // The game uses SQLite's legacy single-quoted identifiers in CREATE TABLE statements.
            int depth = 0;
            bool literalContext = false;
            return SqlIdentifierTokens.Replace(sql, match =>
            {
                string token = match.Value;
                if (token.StartsWith("--", StringComparison.Ordinal) || token.StartsWith("/*", StringComparison.Ordinal))
                    return token;
                if (token == "(") depth++;
                if (token == ")") depth--;
                if (token == "," && depth == 1) literalContext = false;
                if (string.Equals(token, "DEFAULT", StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(token, "CHECK", StringComparison.OrdinalIgnoreCase))
                    literalContext = true;
                if (token[0] == '\'' && literalContext)
                    return token;
                string identifier = token;
                if (token[0] == '"' || token[0] == '`' || token[0] == '\'')
                    identifier = token.Substring(1, token.Length - 2).Replace(new string(token[0], 2), token[0].ToString());
                else if (token[0] == '[')
                    identifier = token.Substring(1, token.Length - 2);
                return replacements.TryGetValue(identifier, out string replacement) ? QuoteIdentifier(replacement) : token;
            });
        }

        private static object ExecuteUnhashScalar(SqliteConnection connection, SqliteTransaction transaction, string sql)
        {
            using (var command = connection.CreateCommand())
            {
                command.Transaction = transaction;
                command.CommandText = sql;
                return command.ExecuteScalar();
            }
        }

        private static void ExecuteUnhashCommand(SqliteConnection connection, SqliteTransaction transaction, string sql)
        {
            using (var command = connection.CreateCommand())
            {
                command.Transaction = transaction;
                command.CommandText = sql;
                command.ExecuteNonQuery();
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
        public int PlayerProcessLimit { get; private set; }
        public int PlayerLevelLimit { get; private set; }
        public int UniqueEqLevelLimit { get; private set; }
        public Dictionary<int, unit_talent[]> TalentDic { get; private set; }
        public Dictionary<int, enemy_talent_weakness> EnemyTalentWeaknessDic { get; private set; }
        public Dictionary<int, talent_weakness> TalentWeaknessDic { get; private set; }
        public Dictionary<int, int[]> WeaknessDic { get; private set; }
        public Dictionary<int, talentFormationData> TalentFormationDic { get; private set; }
        public Dictionary<int, EnemyData> Dic2 { get; private set; }
        public Dictionary<int, EnemyData> TowerEnemyParameterDic { get; private set; }
        public Dictionary<int, WaveGroupDataBase> SecretDungeonWaveGroupDataDic { get; private set; }
        public Dictionary<int, EnemyData> TalentQuestEnemyParameterDic { get; private set; }
        public Dictionary<int, WaveGroupDataBase> TalentQuestWaveGroupDataDic { get; private set; }
        public Dictionary<int, EnemyData> AbyssEnemyParameterDic { get; private set; }
        public Dictionary<int, WaveGroupDataBase> AbyssWaveGroupDataDic { get; private set; }
        public Dictionary<int, int> AbyssQuestFilterDic { get; private set; }
        public Dictionary<int, int> AbyssBossFilterDic { get; private set; }
        public Dictionary<int, EnemyData> LabyrinthEnemyParameterDic { get; private set; }
        public Dictionary<int, EnemyData> MirageEnemyParameterDic { get; private set; }
        public Dictionary<int, WaveGroupDataBase> MirageWaveGroupDataDic { get; private set; }
        public Dictionary<int, WaveGroupDataBase> LabyrinthWaveGroupDataDic { get; private set; }
        public Dictionary<int, SkillData> Dic3 { get; private set; }
        public Dictionary<int, SkillAction> Dic4 { get; private set; }
        public Dictionary<int, UnitAttackPattern> Dic5 { get; private set; }
        public Elements.MasterUnitSkillDataRf masterUnitSkillDataRf { get; private set; }
        public Dictionary<int, GuildEnemyData> Dic6 { get; private set; }
        public Dictionary<int, MasterEnemyMParts.EnemyMParts> Dic7 { get; private set; }
        public Dictionary<int, ailment_data> AilmentDataDic { get; private set; }
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
        public Dictionary<int, labyrinth_enhance_node> LabyrinthEnhanceNodeDic { get; private set; }
        public Dictionary<int, labyrinth_enhance_data> LabyrinthEnhanceDataDic { get; private set; }
        public Dictionary<int, labyrinth_enhance_information> LabyrinthEnhanceInformationDic { get; private set; }
        public Dictionary<int, labyrinth_quest_data> LabyrinthQuestDataDic { get; private set; }
        public Dictionary<int, labyrinth_difficulty_effect> LabyrinthDifficultyEffectDic { get; private set; }
        public Dictionary<int, labyrinth_difficulty_enemy_data> LabyrinthDifficultyEnemyDataDic { get; private set; }
        public Dictionary<int, labyrinth_relic> LabyrinthRelicDic { get; private set; }
        public Dictionary<int, labyrinth_relic_effect_group> LabyrinthRelicEffectGroupDic { get; private set; }
        public Dictionary<int, labyrinth_relic_effect> LabyrinthRelicEffectDic { get; private set; }
        public Dictionary<int, List<labyrinth_relic_mark_effect>> LabyrinthRelicMarkEffectDic { get; private set; }
        public Dictionary<int, List<relic_action_exec_time>> RelicActionExecTimeDic { get; private set; }
        public Dictionary<int, List<relic_skill_exec_time>> RelicSkillExecTimeDic { get; private set; }
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

        public static int CalculatePlayerLevelLimit(IEnumerable<experience_team> levels)
        {
            return Math.Max(0, levels.Select(row => row.team_level).DefaultIfEmpty(1).Max() - 1);
        }

        public static int CalculateUniqueEqLevelLimit(IEnumerable<unique_equipment_enhance_data> enhancements)
        {
            return Math.Max(0, enhancements
                .Where(row => row != null && row.equip_slot == 1)
                .Select(row => row.enhance_level)
                .DefaultIfEmpty(0).Max());
        }

        public static int CalculatePlayerProcessLimit(IEnumerable<unit_promotion> promotions)
        {
            int limit = 0;
            foreach (var promotion in promotions)
            {
                if (promotion == null || promotion.unit_id < 100000 || promotion.unit_id >= 200000
                    || promotion.promotion_level < 8)
                    continue;

                int equipmentCount = promotion.GetEquips().Count(id => id > 0 && id != 999999);
                if (equipmentCount < 3)
                    continue;

                int rank = promotion.promotion_level;
                // Keep the existing progression encoding: R10-3 = 13, R10-4 = 14, R10-5 = 15.
                int process = rank == 8 ? PlayerSetting.MinimumPlayerProcess
                    : rank == 9 ? (equipmentCount >= 5 ? 12 : 11)
                    : (rank - 10) * 3 + 13 + Math.Min(2, equipmentCount - 3);
                limit = Math.Max(limit, process);
            }
            return limit;
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
                PlayerProcessLimit = CalculatePlayerProcessLimit(equipDic.Values.SelectMany(rows => rows));
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
                EnemyTalentWeaknessDic = GetOptionalDatasDic<enemy_talent_weakness>(a => a.enemy_id);
            });
            var talentWeaknessTask = Task.Run(() =>
            {
                TalentWeaknessDic = GetOptionalDatasDic<talent_weakness>(a => a.resist_id);
            });
            var talentDicTask = Task.Run(() => TalentDic = GetDatas<unit_talent>()
                        .GroupBy(x => x.unit_id)
                        .ToDictionary(g => g.Key, g => g.ToArray()));
            var UnitRoleDataDicTask =
                Task.Run(() => UnitRoleDataDic = GetOptionalDatasDic<unit_role_data>(a => a.unit_id));
            var tasks = new List<Task>();
            tasks.Add(Task.Run(() => PlayerLevelLimit = CalculatePlayerLevelLimit(GetDatas<experience_team>())));
            tasks.Add(Task.Run(() => UniqueEqLevelLimit = CalculateUniqueEqLevelLimit(GetDatas<unique_equipment_enhance_data>())));

            tasks.Add(
                DatabaseCacheContract.RunAfterSuccessAsync(
                    Task.WhenAll(rarityDicTask, unitSkillDataDicTask, unitAttackPatternDicTask, equipDicTask,
                        rankStateDicTask, unitDicTask, talentDicTask, UnitRoleDataDicTask),
                    () => Dic1 = GetUnitRarityData()));
            tasks.Add(
                DatabaseCacheContract.RunAfterSuccessAsync(
                    Task.WhenAll(dic1Task, unitSkillDataDicTask, unitAttackPatternDicTask),
                    () => Dic2 = GetEnemyDataDic()));
            tasks.Add(
                DatabaseCacheContract.RunAfterSuccessAsync(
                    Task.WhenAll(dic1Task, unitSkillDataDicTask, unitAttackPatternDicTask),
                    () => TowerEnemyParameterDic = GetTowerEnemyDataDic()));
            tasks.Add(
                DatabaseCacheContract.RunAfterSuccessAsync(
                    Task.WhenAll(dic1Task, unitSkillDataDicTask, unitAttackPatternDicTask),
                    () => TalentQuestEnemyParameterDic = GetTalentQuestEnemyDataDic()));
            tasks.Add(
                DatabaseCacheContract.RunAfterSuccessAsync(
                    Task.WhenAll(dic1Task, unitSkillDataDicTask, unitAttackPatternDicTask),
                    () => AbyssEnemyParameterDic = GetAbyssEnemyDataDic()));
            tasks.Add(
                DatabaseCacheContract.RunAfterSuccessAsync(
                    Task.WhenAll(dic1Task, unitSkillDataDicTask, unitAttackPatternDicTask),
                    () => LabyrinthEnemyParameterDic = GetLabyrinthEnemyDataDic()));
            tasks.Add(
                DatabaseCacheContract.RunAfterSuccessAsync(
                    Task.WhenAll(dic1Task, unitSkillDataDicTask, unitAttackPatternDicTask),
                    () => MirageEnemyParameterDic = GetMirageEnemyDataDic()));
            tasks.Add(
                DatabaseCacheContract.RunAfterSuccessAsync(
                    Task.WhenAll(enemyTalentWeaknessTask, talentWeaknessTask),
                    () => WeaknessDic = GetWeaknessDict()));
            tasks.Add(
                Task.Run(() => AilmentDataDic = GetDatasDic<ailment_data>(a => a.ailment_id)));
            tasks.Add(
                Task.Run(() => resistDataDic = ReadResistDataByAilmentId()));
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
                    }, optional: true)));
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
                    }, optional: true)));
            tasks.Add(
                Task.Run(() => LabyrinthWaveGroupDataDic = GetWaveGroupDataDic<labyrinth_wave_group_data>(
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
                    }, optional: true)));
            tasks.Add(
                Task.Run(() => Dic3 = GetSkillDataDic()));
            tasks.Add(
                Task.Run(() => Dic4 = GetSkillActionDic()));
            tasks.Add(DatabaseCacheContract.RunAfterSuccessAsync(
                unitAttackPatternDicTask,
                () => Dic5 = GetUnitAttackPatternDic()));
            tasks.Add(DatabaseCacheContract.RunAfterSuccessAsync(
                waveDicTask,
                () => Dic6 = GetGuildEnemyData()));
            tasks.Add(
                Task.Run(() => Dic7 = GetMPartsData()));
            tasks.Add(DatabaseCacheContract.RunAfterSuccessAsync(
                dic2Task,
                () => Dic8 = GetEquipmentData()));
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
                Task.Run(() => eReductions = GetOptionalDatas<EReduction>().ToList()));
            tasks.Add(
                Task.Run(() => rbs = GetOptionalDatas<promotion_bonus>()
                    .GroupBy(x => x.unit_id)
                    .ToDictionary(g => g.Key, g => g.ToDictionary(x => x.promotion_level, x => x))));
            tasks.Add(
                Task.Run(() => TalentSkillNodeDic = GetTalentSkillData()));
            // tasks.Add(
            // Task.Run(() => TalentSkillEnhanceLevelDic = GetDatasDic<talent_skill_enhance_level>(a => a.enhance_level_id)));
            tasks.Add(
                Task.Run(() => TalentSkillEnhanceDataDic = GetTalentSkillEnhanceData()));
            tasks.Add(
                Task.Run(() => paramTypeDic = GetOptionalDatasDic<param_type>(a => a.parameter_type)));
            tasks.Add(
                Task.Run(() => TeamSkillNodeDic = GetTeamSkillData()));
            tasks.Add(
                Task.Run(() => TeamSkillEnhanceDataDic = GetTeamSkillEnhanceData()));
            tasks.Add(
                Task.Run(() => ExtraEffectDataDic = GetOptionalDatasDic<extra_effect_data>(a => a.extra_effect_id)));
            tasks.Add(
                Task.Run(() => TalentQuestBattleEffectDic = GetOptionalDatasDic<talent_quest_battle_effect>(a => a.id)));
            tasks.Add(
                Task.Run(() => AbyssBattleEffectDic = GetOptionalDatasDic<abyss_battle_effect>(a => a.id)));
            tasks.Add(
                Task.Run(() => AbyssQuestFilterDic = GetAbyssQuestFilteredDic()));
            tasks.Add(
                Task.Run(() => AbyssBossFilterDic = GetAbyssBossFilteredDic()));
            tasks.Add(
                Task.Run(() => MirageBattleEffectDic = GetOptionalDatasDic<mirage_battle_effect>(a => a.id)));
            tasks.Add(
                Task.Run(() => MirageNemesisAreaDic = GetOptionalDatasDic<mirage_nemesis_area>(a => a.nemesis_id)));
            tasks.Add(
                Task.Run(() => UnitRoleMasteryIdDic = GetOptionalDatasDic<unit_role_mastery_id>(a => a.mastery_id)));
            tasks.Add(
                Task.Run(() => UnitRoleMasterySlotEnhanceDataDic = GetUnitRoleMasterySlotEnhanceData()));
            tasks.Add(Task.Run(() =>
            {
                ConnectRankStatusDic = GetOptionalDatas<connect_rank_status>()
                    .GroupBy(x => x.role_id)
                    .ToDictionary(g => g.Key, g => g.OrderBy(x => x.rank_from).ThenBy(x => x.rank_to).ToList());
            }));
            tasks.Add(
                Task.Run(() => ConnectRankChartDic = GetOptionalDatasDic<connect_rank_chart>(a => a.rank)));
            tasks.Add(Task.Run(() =>
                ConnectRankBonusList = GetOptionalDatas<connect_rank_bonus>()
                    .OrderBy(x => x.bonus_level)
                    .ThenBy(x => x.bonus_number)
                    .ToList()));
            tasks.Add(
                Task.Run(() => ConnectRankBoostDic = GetOptionalDatasDic<connect_rank_boost>(a => a.system_id)));
            tasks.Add(
                Task.Run(() => ConnectRankLimitCommonDic = GetOptionalDatasDic<connect_rank_limit_common>(a => a.max_rank)));
            tasks.Add(Task.Run(() =>
            {
                ConnectRankLimitItemDic = GetOptionalDatas<connect_rank_limit_item>()
                    .GroupBy(x => x.group_id)
                    .ToDictionary(g => g.Key, g => g.OrderBy(x => x.max_rank).ToList());
            }));
            tasks.Add(Task.Run(() =>
            {
                ConnectRankEnhanceDic = GetOptionalDatas<connect_rank_enhance>()
                    .GroupBy(x => x.role_id)
                    .ToDictionary(g => g.Key, g => g.OrderBy(x => x.rank).ToList());
            }));
            tasks.Add(
                Task.Run(() => LabyrinthEnhanceNodeDic = GetOptionalDatasDic<labyrinth_enhance_node>(a => a.node_id)));
            tasks.Add(
                Task.Run(() => LabyrinthEnhanceDataDic = GetOptionalDatasDic<labyrinth_enhance_data>(a => a.effect_id)));
            tasks.Add(
                Task.Run(() => LabyrinthEnhanceInformationDic = GetOptionalDatasDic<labyrinth_enhance_information>(a => a.information_id)));
            tasks.Add(
                Task.Run(() => LabyrinthQuestDataDic = GetOptionalDatasDic<labyrinth_quest_data>(a => a.quest_id)));
            tasks.Add(
                Task.Run(() => LabyrinthDifficultyEnemyDataDic = GetOptionalDatasDic<labyrinth_difficulty_enemy_data>(a => a.difficulty)));
            tasks.Add(
                Task.Run(() => LabyrinthDifficultyEffectDic = GetOptionalDatasDic<labyrinth_difficulty_effect>(a => a.id)));
            tasks.Add(
                Task.Run(() => LabyrinthRelicDic = GetOptionalDatasDic<labyrinth_relic>(a => a.relic_id)));
            tasks.Add(
                Task.Run(() => LabyrinthRelicEffectGroupDic = GetOptionalDatasDic<labyrinth_relic_effect_group>(a => a.effect_group_id)));
            tasks.Add(
                Task.Run(() => LabyrinthRelicEffectDic = GetOptionalDatasDic<labyrinth_relic_effect>(a => a.effect_id)));
            tasks.Add(Task.Run(() =>
            {
                LabyrinthRelicMarkEffectDic = GetOptionalDatas<labyrinth_relic_mark_effect>()
                    .GroupBy(x => x.relic_mark_id)
                    .ToDictionary(g => g.Key, g => g.OrderBy(x => x.trigger_count).ToList());
            }));
            tasks.Add(Task.Run(() =>
            {
                RelicActionExecTimeDic = GetOptionalDatas<relic_action_exec_time>()
                    .GroupBy(x => x.action_id)
                    .ToDictionary(g => g.Key, g => g.OrderBy(x => x.id).ToList());
            }));
            tasks.Add(Task.Run(() =>
            {
                RelicSkillExecTimeDic = GetOptionalDatas<relic_skill_exec_time>()
                    .GroupBy(x => x.skill_id)
                    .ToDictionary(g => g.Key, g => g.OrderBy(x => x.id).ToList());
            }));
            tasks.Add(Task.Run(() =>
            {
                masterUnitSkillDataRf = new MasterUnitSkillDataRf();
                masterUnitSkillDataRf.dict = GetOptionalDatas<MasterUnitSkillDataRf.UnitSkillDataRf>()
                    .GroupBy(x => x.skill_id)
                    .ToDictionary(g => g.Key, g => g.OrderBy(x => x.min_lv).ToList());
            }));

            tasks.Add(Task.Run(() =>
            {
                var enhanceLevel = GetOptionalDatas<ex_equipment_enhance_data>()
                    .GroupBy(x => x.rarity)
                    .ToDictionary(g => g.Key, g => g.Max(x => x.enhance_level));

                var subStatusGroupData = GetOptionalDatas<ex_equipment_sub_status_group>();
                var subStatusGroups = subStatusGroupData != null ?
                    subStatusGroupData.GroupBy(x => x.ex_equipment_id)
                        .ToDictionary(g => g.Key, g => g.ToList()) :
                    new Dictionary<int, List<ex_equipment_sub_status_group>>();

                var subStatusesData = GetOptionalDatas<ex_equipment_sub_status>();
                var subStatuses = subStatusesData != null ?
                    subStatusesData.GroupBy(x => x.group_id)
                    .ToDictionary(g => g.Key, g => g.ToDictionary(x => x.status, x => x.values)) : new Dictionary<int, Dictionary<int, int[]>>();

                var categories = GetOptionalDatas<ex_equipment_data>()
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

                unitExEquips = GetOptionalDatas<unit_ex_equipment_slot>()
                    .ToDictionary(x => x.unit_id,
                        x => new Dictionary<int, ex_equipment_data>[]
                        {
                            categories.TryGetValue(x.slot_category_1, out var val) ? val : empty,
                            categories.TryGetValue(x.slot_category_2, out var val2) ? val2 : empty,
                            categories.TryGetValue(x.slot_category_3, out var val3) ? val3 : empty,
                        });

                extraEffectUnitGroupDic = GetOptionalDatas<extra_effect_unit_group>()
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
        private Dictionary<int, EnemyData> GetTowerEnemyDataDic()
        {
            var dic = new Dictionary<int, EnemyData>();
            foreach (var dd in GetOptionalDatas<tower_enemy_parameter>())
            {
                if (!dic1.TryGetValue(dd.unit_id, out var detail) ||
                    !unitSkillDataDic.TryGetValue(dd.unit_id, out var skills) ||
                    !unitAttackPatternDic.TryGetValue(dd.unit_id, out var patterns))
                {
                    continue;
                }

                var enemy = new EnemyData
                {
                    enemy_id = dd.enemy_id,
                    unit_id = dd.unit_id,
                    name = dd.name,
                    level = dd.level,
                    rarity = dd.rarity,
                    promotion_level = dd.promotion_level,
                    baseData = dd.GetBaseData(),
                    union_burst_level = dd.union_burst_level,
                    main_skill_lvs = new List<int>
                    {
                        dd.main_skill_lv_1, dd.main_skill_lv_2, dd.main_skill_lv_3, dd.main_skill_lv_4,
                        dd.main_skill_lv_5, dd.main_skill_lv_6, dd.main_skill_lv_7, dd.main_skill_lv_8,
                        dd.main_skill_lv_9, dd.main_skill_lv_10
                    },
                    ex_skill_lvs = new List<int>
                    {
                        dd.ex_skill_lv_1, dd.ex_skill_lv_2, dd.ex_skill_lv_3, dd.ex_skill_lv_4, dd.ex_skill_lv_5
                    },
                    resist_status_id = dd.resist_status_id,
                    resist_variation_id = dd.resist_variation_id,
                    detailData = new EnemyDetailData
                    {
                        unit_id = detail.unit_id,
                        unit_name = detail.unit_name,
                        prefab_id = detail.prefab_id,
                        motion_type = detail.motion_type,
                        se_type = detail.se_type,
                        move_speed = detail.move_speed,
                        search_area_width = detail.search_area_width,
                        atk_type = detail.atk_type,
                        normal_atk_cast_time = detail.normal_atk_cast_time,
                        cutin = detail.cutin,
                        visual_change_flag = detail.visual_change_flag,
                        comment = detail.comment
                    },
                    skillData = new EnemySkillData
                    {
                        UB = skills.union_burst,
                        raw_data = skills,
                        MainSkills = skills.GetMainSkillIDs(),
                        enemyAttackPatterns = patterns.Select(pattern => new UnitAttackPattern(
                            pattern.pattern_id, pattern.unit_id, pattern.loop_start, pattern.loop_end,
                            pattern.GetAllPatterns().ToArray())).ToList()
                    }
                };
                dic.Add(enemy.enemy_id, enemy);
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

            var list = GetOptionalDatas<abyss_enemy_parameter>();
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

            var list = GetOptionalDatas<mirage_enemy_parameter>();
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
        private Dictionary<int, EnemyData> GetLabyrinthEnemyDataDic()
        {
            Dictionary<int, EnemyData> dic = new Dictionary<int, EnemyData>();

            var list = GetOptionalDatas<labyrinth_enemy_parameter>();
            if (list is not null)
            {
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

                    var skList = unitSkillDataDic[e.unit_id];
                    skillData.UB = skList.union_burst;
                    skillData.raw_data = skList;
                    skillData.MainSkills = skList.GetMainSkillIDs();
                    skillData.enemyAttackPatterns = new List<UnitAttackPattern>();
                    var list0 = unitAttackPatternDic[e.unit_id];
                    foreach (var dd3 in list0)
                    {
                        UnitAttackPattern attackPattern = new UnitAttackPattern(dd3.pattern_id, dd3.unit_id, dd3.loop_start, dd3.loop_end, dd3.GetAllPatterns().ToArray());
                        skillData.enemyAttackPatterns.Add(attackPattern);
                    }
                    e.skillData = skillData;

                    dic.Add(e.enemy_id, e);
                }
            }
            return dic;
        }
        private Dictionary<int, WaveGroupDataBase> GetWaveGroupDataDic<T>(Func<T, int> getId, Func<T, int> getWaveGroupId, Func<T, int, int> getEnemyId, bool optional = false) where T : class, new()
        {
            Dictionary<int, WaveGroupDataBase> dic = new Dictionary<int, WaveGroupDataBase>();

            var list = optional ? GetOptionalDatas<T>() : GetDatas<T>();
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
            var list = GetOptionalDatas<ex_equipment_data>();
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
            var node_list = GetOptionalDatas<talent_skill_node>();
            var titleDic = GetOptionalDatas<team_skill_title>().ToDictionary(x => x.title_id, x => x.title_text);
            var enhanceLevelDic = GetOptionalDatas<talent_skill_enhance_level>().ToDictionary(x => x.enhance_level_id);

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
            var enhanceList = GetOptionalDatas<talent_skill_enhance_data>();

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
            var node_list = GetOptionalDatas<team_skill_node>();
            var titleDic = GetOptionalDatas<team_skill_title>().ToDictionary(x => x.title_id, x => x.title_text);
            var enhanceLevelDic = GetOptionalDatas<team_skill_enhance_level>().ToDictionary(x => x.enhance_level_id);

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
            var enhanceList = GetOptionalDatas<team_skill_enhance_data>();

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
            var overwriteDic = GetOptionalDatasDic<talent_formation_overwrite>(a => a.id);
            var bonusDic = GetOptionalDatasDic<talent_formation_bonus>(a => a.formation_bonus_id);

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
            var slotDataList = GetOptionalDatas<unit_role_mastery_slot_data>();
            var enhanceDataList = GetOptionalDatas<unit_role_mastery_enhance_data>();

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
            var abyssQuestDataList = GetOptionalDatas<abyss_quest_data>();

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
            var abyssBossDataList = GetOptionalDatas<abyss_boss_data>();

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
