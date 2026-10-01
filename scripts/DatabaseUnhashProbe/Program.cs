using System.Diagnostics;
using System.Reflection;
using Mono.Data.Sqlite;
using Newtonsoft.Json;
using PCRCaculator.SQL;

var unhash = typeof(SQLiteTool).GetMethod("UnhashDatabase", BindingFlags.Static | BindingFlags.NonPublic);
void Require(bool condition, string message)
{
    if (!condition) throw new InvalidOperationException(message);
}
int Restore(SqliteConnection connection, Dictionary<string, Dictionary<string, string>> rainbow)
{
    try { return (int)unhash.Invoke(null, [connection, rainbow]); }
    catch (TargetInvocationException exception) { throw exception.InnerException; }
}
void Execute(SqliteConnection connection, string sql)
{
    using var command = connection.CreateCommand();
    command.CommandText = sql;
    command.ExecuteNonQuery();
}
object Scalar(SqliteConnection connection, string sql)
{
    using var command = connection.CreateCommand();
    command.CommandText = sql;
    return command.ExecuteScalar();
}
List<string> Names(SqliteConnection connection, string sql, int field = 0)
{
    using var command = connection.CreateCommand();
    command.CommandText = sql;
    using var reader = command.ExecuteReader();
    var names = new List<string>();
    while (reader.Read()) names.Add(reader.GetString(field));
    return names;
}
string Quote(string name) => "\"" + name.Replace("\"", "\"\"") + "\"";
Dictionary<string, string> Map(string name, params (string hash, string original)[] columns)
{
    var result = columns.ToDictionary(pair => pair.hash, pair => pair.original);
    result.Add("--table_name", name);
    return result;
}
void Scenario(string name, Action<SqliteConnection> test)
{
    using var connection = new SqliteConnection("Data Source=:memory:;Version=3;");
    connection.Open();
    test(connection);
    Console.WriteLine($"PASS {name}");
}
void MustFail(SqliteConnection connection, Dictionary<string, Dictionary<string, string>> rainbow)
{
    bool failed = false;
    try { Restore(connection, rainbow); }
    catch (InvalidDataException) { failed = true; }
    Require(failed, "Invalid data must fail atomically.");
}

Scenario("legacy quoted identifiers, defaults, unknown columns and stale mappings", connection =>
{
    Execute(connection, "CREATE TABLE 'h_table' ('h_id' INTEGER PRIMARY KEY, 'h_name' TEXT DEFAULT 'h_name', 'unknown' BLOB, UNIQUE ('h_name')); INSERT INTO h_table VALUES (1, 'Alice', X'0102'); CREATE TABLE untouched (id INT);");
    var rainbow = new Dictionary<string, Dictionary<string, string>>
    {
        ["h_table"] = Map("unit", ("h_id", "id"), ("h_name", "name"), ("absent", "stale")),
        ["absent_table"] = Map("unused", ("a", "b"))
    };
    Require(Restore(connection, rainbow) == 1, "Wrong restored count.");
    Require(Names(connection, "PRAGMA table_info(unit)", 1).SequenceEqual(["id", "name", "unknown"]), "Column mapping failed.");
    Require((string)Scalar(connection, "SELECT name FROM unit") == "Alice", "Rows changed.");
    Execute(connection, "INSERT INTO unit(id) VALUES (2)");
    Require((string)Scalar(connection, "SELECT name FROM unit WHERE id=2") == "h_name", "Default literal was rewritten.");
    Require((string)Scalar(connection, "SELECT hex(unknown) FROM unit WHERE id=1") == "0102", "Unknown column lost.");
    bool uniqueFailed = false;
    try { Execute(connection, "INSERT INTO unit VALUES (3, 'Alice', NULL)"); }
    catch (SqliteException) { uniqueFailed = true; }
    Require(uniqueFailed, "Unique constraint lost.");
    Require(Restore(connection, rainbow) == 0, "Restore is not idempotent.");
});

Scenario("transition aliases with permuted columns prefer the more complete mapping", connection =>
{
    Execute(connection, "CREATE TABLE old_hash(a INTEGER PRIMARY KEY, x TEXT); INSERT INTO old_hash VALUES(1,'A'),(2,'B'); CREATE TABLE new_hash(y TEXT, b INTEGER PRIMARY KEY); INSERT INTO new_hash VALUES('B',2),('A',1);");
    var rainbow = new Dictionary<string, Dictionary<string, string>>
    {
        ["old_hash"] = Map("unit", ("a", "id")),
        ["new_hash"] = Map("unit", ("b", "id"), ("y", "name"))
    };
    Restore(connection, rainbow);
    Require(Names(connection, "SELECT name FROM sqlite_master WHERE type='table'").SequenceEqual(["unit"]), "Duplicate tables remain.");
    Require(Convert.ToInt64(Scalar(connection, "SELECT COUNT(*) FROM unit")) == 2, "Rows duplicated.");
    Require((string)Scalar(connection, "SELECT name FROM unit WHERE id=1") == "A", "Permuted column data corrupted.");
});

Scenario("plaintext plus hash transition", connection =>
{
    Execute(connection, "CREATE TABLE unit(id INTEGER PRIMARY KEY, name TEXT); INSERT INTO unit VALUES(1,'A'); CREATE TABLE hashed(b TEXT, a INTEGER PRIMARY KEY); INSERT INTO hashed VALUES('A',1);");
    Restore(connection, new() { ["hashed"] = Map("unit", ("a", "id"), ("b", "name")) });
    Require(Names(connection, "SELECT name FROM sqlite_master WHERE type='table'").SequenceEqual(["unit"]), "Plaintext transition failed.");
    Require(Convert.ToInt64(Scalar(connection, "SELECT COUNT(*) FROM unit")) == 1, "Plaintext rows duplicated.");
});

Scenario("unmapped fields are verified by primary key and a fixed column permutation", connection =>
{
    Execute(connection, "CREATE TABLE old_hash(a INTEGER PRIMARY KEY, x TEXT); INSERT INTO old_hash VALUES(1,'A'),(2,'B'); CREATE TABLE new_hash(y TEXT, b INTEGER PRIMARY KEY); INSERT INTO new_hash VALUES('B',2),('A',1);");
    Restore(connection, new() { ["old_hash"] = Map("unit"), ["new_hash"] = Map("unit") });
    Require(Names(connection, "SELECT name FROM sqlite_master WHERE type='table'").SequenceEqual(["unit"]), "Unmapped duplicate remains.");
    Require(Convert.ToInt64(Scalar(connection, "SELECT COUNT(*) FROM unit")) == 2, "Unmapped rows changed.");
});

Scenario("unmapped mismatches cannot be discarded", connection =>
{
    Execute(connection, "CREATE TABLE old_hash(a INTEGER PRIMARY KEY, x TEXT); INSERT INTO old_hash VALUES(1,'A'); CREATE TABLE new_hash(y TEXT, b INTEGER PRIMARY KEY); INSERT INTO new_hash VALUES('B',1);");
    MustFail(connection, new() { ["old_hash"] = Map("unit"), ["new_hash"] = Map("unit") });
});

Scenario("mismatched duplicates roll back earlier successful tables", connection =>
{
    Execute(connection, "CREATE TABLE first_hash(x INT); INSERT INTO first_hash VALUES(3); CREATE TABLE old_hash(a INT); INSERT INTO old_hash VALUES(1); CREATE TABLE new_hash(b INT); INSERT INTO new_hash VALUES(2);");
    MustFail(connection, new()
    {
        ["first_hash"] = Map("first", ("x", "id")),
        ["old_hash"] = Map("unit", ("a", "id")),
        ["new_hash"] = Map("unit", ("b", "id"))
    });
    Require(Names(connection, "SELECT name FROM sqlite_master WHERE type='table' ORDER BY name").SequenceEqual(["first_hash", "new_hash", "old_hash"]), "Transaction did not roll back.");
});

Scenario("duplicate multiplicity is checked", connection =>
{
    Execute(connection, "CREATE TABLE old_hash(a INT); INSERT INTO old_hash VALUES(1),(1),(2); CREATE TABLE new_hash(b INT); INSERT INTO new_hash VALUES(1),(2),(2);");
    MustFail(connection, new() { ["old_hash"] = Map("unit", ("a", "id")), ["new_hash"] = Map("unit", ("b", "id")) });
});

Scenario("column collisions are rejected and missing mappings remain intact", connection =>
{
    Execute(connection, "CREATE TABLE hash(a INT, b INT); CREATE TABLE unknown_hash(z INT); INSERT INTO unknown_hash VALUES(7);");
    MustFail(connection, new() { ["hash"] = Map("unit", ("a", "id"), ("b", "ID")) });
    Restore(connection, new());
    Require(Convert.ToInt64(Scalar(connection, "SELECT z FROM unknown_hash")) == 7, "Unknown table lost.");
});

if (args.Length > 0)
{
    Require(args.Length >= 3 && args[0] == "--rainbow", "Usage: --rainbow <rainbow.json> <db> [<db> ...]");
    var timer = Stopwatch.StartNew();
    var rainbow = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(File.ReadAllText(args[1]));
    Console.WriteLine($"Loaded {rainbow.Count} rainbow aliases in {timer.Elapsed.TotalSeconds:F3}s");
    foreach (string sourcePath in args.Skip(2))
    {
        // Always copy: downloaded originals can be hard links to shared blobs.
        string testPath = Path.Combine(Path.GetTempPath(), $"unhash-probe-{Guid.NewGuid():N}.db");
        File.Copy(sourcePath, testPath);
        try
        {
            using var connection = new SqliteConnection($"Data Source={testPath};Version=3;");
            connection.Open();
            var tables = Names(connection, "SELECT name FROM sqlite_master WHERE type='table' AND name NOT LIKE 'sqlite_%'");
            var expected = new Dictionary<string, (long rows, int known)>();
            foreach (var group in tables.Where(rainbow.ContainsKey).GroupBy(name => rainbow[name]["--table_name"]))
            {
                var candidates = group.Select(name => (name, known: Names(connection, $"PRAGMA table_info({Quote(name)})", 1).Count(rainbow[name].ContainsKey)));
                var chosen = candidates.OrderByDescending(table => table.known).ThenBy(table => table.name, StringComparer.Ordinal).First();
                string original = tables.Contains(group.Key) ? group.Key : chosen.name;
                expected.Add(group.Key, (Convert.ToInt64(Scalar(connection, $"SELECT COUNT(*) FROM {Quote(original)}")), chosen.known));
            }
            timer.Restart();
            int restored = Restore(connection, rainbow);
            double seconds = timer.Elapsed.TotalSeconds;
            var remaining = Names(connection, "SELECT name FROM sqlite_master WHERE type='table' AND name NOT LIKE 'sqlite_%'");
            Require(!remaining.Any(rainbow.ContainsKey), "Known hashes remain after restore.");
            foreach (var table in expected)
            {
                Require(remaining.Contains(table.Key), $"Missing original table: {table.Key}");
                Require(Convert.ToInt64(Scalar(connection, $"SELECT COUNT(*) FROM {Quote(table.Key)}")) == table.Value.rows, $"Rows changed: {table.Key}");
                int known = Names(connection, $"PRAGMA table_info({Quote(table.Key)})", 1).Count(column => !System.Text.RegularExpressions.Regex.IsMatch(column, "^[0-9a-f]{64}$"));
                Require(known >= table.Value.known, $"Recovered columns lost: {table.Key}");
            }
            Require((string)Scalar(connection, "PRAGMA integrity_check") == "ok", "SQLite integrity check failed.");
            Require(Restore(connection, rainbow) == 0, "Real database restore is not idempotent.");
            Console.WriteLine($"PASS {Path.GetFileName(sourcePath)}: {tables.Count} -> {remaining.Count} tables; {restored} restored in {seconds:F3}s; row counts, column coverage, integrity and idempotence verified.");
        }
        finally
        {
            SqliteConnection.ClearAllPools();
            File.Delete(testPath);
        }
    }
}
