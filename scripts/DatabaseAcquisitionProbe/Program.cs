using PCRApi;
using PCRCaculator;
using PCRCaculator.SQL;
using Mono.Data.Sqlite;
using System.Reflection;

// Explicit opt-in integration probe: uses the same manifest/download/decrypt/unhash path as startup.
if (args.Length != 3) throw new ArgumentException("Usage: <empty-or-reused-data-directory> <StreamingAssets-directory> <JP-version>");
var root = Path.GetFullPath(args[0]);
Directory.CreateDirectory(root);
Directory.SetCurrentDirectory(root); // Prevent repository-root databases from masking a cold start.
ABExTool.persistentDataPath = root;
UnityEngine.Application.persistentDataPath = root;
typeof(UnityEngine.Application).GetField("streamingAssetsPath", BindingFlags.Static | BindingFlags.NonPublic)!.SetValue(null, Path.GetFullPath(args[1]));
MainManager.Instance = new MainManager();
SQLiteTool.sql = "SELECT 1;";
var source = Path.Combine(root, "jp" + args[2] + ".db");
var existed = File.Exists(source);
var before = existed ? File.GetLastWriteTimeUtc(source) : default;
var manager = new AssetManager();
manager.Initialize(args[2]);
var reader = new Reader(manager);
reader.Prepare();
using var connection = new SqliteConnection("Data Source=" + source);
connection.Open();
using var command = connection.CreateCommand();
command.CommandText = "SELECT COUNT(*) FROM resist_data";
var rows = Convert.ToInt32(command.ExecuteScalar());
if (rows == 0) throw new Exception("Decoded resist_data is empty");
if (existed && File.GetLastWriteTimeUtc(source) != before) throw new Exception("Existing source database was rewritten");
Console.WriteLine($"PASS version={args[2]} mode={(existed ? "reuse" : "cold")} bytes={new FileInfo(source).Length} resist_rows={rows}");
class Reader : SQLiteTool { public Reader(AssetManager manager) : base(manager, true) {} }
