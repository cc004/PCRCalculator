using Elements;
using Elements.Battle;
using Newtonsoft.Json.Linq;
using PCRCaculator.Guild;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PCRCaculator
{
    public class RemoteSimulator : IDisposable
    {
        private readonly OfflineServerSession server;
        private JObject request;
        private readonly CancellationTokenSource lifetime = new CancellationTokenSource();
        private readonly CancellationToken taskToken;
        private int disposed;

        public RemoteSimulator()
        {
            taskToken = lifetime.Token;
            server = OfflineServerManager.Acquire();
        }

        internal static string GetServerContext()
        {
#if UNITY_5_3_OR_NEWER
            return (MainManager.Instance == null ? "" : JsonConvert.SerializeObject(MainManager.Instance.Version))
                + "\n" + UnityEngine.Application.persistentDataPath;
#else
            return (Environment.GetEnvironmentVariable("PCRCALC_VERSION_DATA") ?? "") + "\n"
                + (Environment.GetEnvironmentVariable("PCRCALC_PERSISTENT_DATA_PATH") ?? "");
#endif
        }

        public static string GetHostRuntimeId()
        {
            var architecture = RuntimeInformation.OSArchitecture;
            if (architecture != Architecture.Arm64 && architecture != Architecture.X64)
                throw new PlatformNotSupportedException("脱机模拟仅支持 x64 或 ARM64 桌面系统。");
            var os = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "win"
                : RuntimeInformation.IsOSPlatform(OSPlatform.OSX) ? "osx"
                : RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? "linux"
                : throw new PlatformNotSupportedException("此系统不支持脱机模拟。");
            return os + (architecture == Architecture.Arm64 ? "-arm64" : "-x64");
        }

        public static string GetDotnetPath()
        {
            var configured = Environment.GetEnvironmentVariable("PCRCALC_DOTNET_PATH");
            if (!string.IsNullOrWhiteSpace(configured))
            {
                if (!File.Exists(configured))
                    throw new FileNotFoundException("PCRCALC_DOTNET_PATH 指定的 dotnet 不存在。", configured);
                return Path.GetFullPath(configured);
            }
            var executable = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "dotnet.exe" : "dotnet";
            var directories = new List<string>();
            var dotnetRoot = Environment.GetEnvironmentVariable("DOTNET_ROOT");
            if (!string.IsNullOrWhiteSpace(dotnetRoot))
                directories.Add(dotnetRoot);
            directories.AddRange((Environment.GetEnvironmentVariable("PATH") ?? "").Split(Path.PathSeparator));
            directories.Add(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "dotnet"));
            directories.Add("/usr/local/share/dotnet");
            directories.Add("/usr/local/share/dotnet/x64");
            directories.Add("/opt/homebrew/bin");
            directories.Add(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".dotnet"));
            foreach (var directory in directories.Where(x => !string.IsNullOrWhiteSpace(x)))
            {
                var candidate = Path.Combine(directory, executable);
                if (File.Exists(candidate))
                    return Path.GetFullPath(candidate);
            }
            return executable;
        }

        public static string GetEditorServerDirectory()
        {
            return Path.Combine(GetProjectRoot(), "server", GetHostRuntimeId());
        }

        private static string GetProjectRoot()
        {
#if UNITY_5_3_OR_NEWER
            return Path.GetFullPath(Path.Combine(UnityEngine.Application.dataPath, ".."));
#else
            return AppContext.BaseDirectory;
#endif
        }

        internal static ProcessStartInfo CreateServerStartInfo(int port)
        {
            var directories = new List<string>();
#if UNITY_EDITOR
            directories.Add(GetEditorServerDirectory());
            directories.Add(Path.Combine(GetProjectRoot(), "server"));
#elif UNITY_STANDALONE_OSX
            directories.Add(Path.Combine(UnityEngine.Application.dataPath, "Resources", "server"));
            // Also accept the older server directory beside the .app.
            directories.Add(Path.GetFullPath(Path.Combine(UnityEngine.Application.dataPath, "..", "..", "server")));
#elif UNITY_5_3_OR_NEWER
            directories.Add(Path.Combine(GetProjectRoot(), "server"));
#else
            directories.Add(Path.Combine(AppContext.BaseDirectory, "server"));
#endif
            var executable = RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                ? "PCRCalculatorServer.exe" : "PCRCalculatorServer";
            var arguments = $"--server --port {port} --host localhost";
            foreach (var directory in directories)
            {
                var path = Path.Combine(directory, executable);
                if (File.Exists(path))
                    return ServerStartInfo(path, arguments, directory);
            }
#if UNITY_EDITOR
            foreach (var configuration in new[] { "Debug", "Release" })
            {
                var directory = Path.Combine(GetProjectRoot(), "PCRCalculatorServer", "bin", configuration, "net8.0");
                var assembly = Path.Combine(directory, "PCRCalculatorServer.dll");
                if (File.Exists(assembly))
                    return ServerStartInfo(GetDotnetPath(), "\"" + assembly + "\" " + arguments, directory);
            }
            throw new FileNotFoundException("找不到脱机模拟程序。请先 dotnet build PCRCalculatorServer/PCRCalculatorServer.csproj，或使用 PCR Build/Prepare Offline Server (Editor)。");
#else
            throw new FileNotFoundException("找不到随客户端打包的脱机模拟程序：" + Path.Combine(directories[0], executable));
#endif
        }

        private static ProcessStartInfo ServerStartInfo(string path, string arguments, string directory)
        {
            var info = new ProcessStartInfo(path, arguments)
            {
                WorkingDirectory = directory,
                UseShellExecute = false,
                RedirectStandardError = true,
                CreateNoWindow = true
            };
#if UNITY_5_3_OR_NEWER
            // The companion's own save file may select a different database version.
            if (MainManager.Instance != null && MainManager.Instance.Version != null)
                info.EnvironmentVariables["PCRCALC_VERSION_DATA"] = JsonConvert.SerializeObject(MainManager.Instance.Version);
            info.EnvironmentVariables["PCRCALC_PERSISTENT_DATA_PATH"] = UnityEngine.Application.persistentDataPath;
#endif
            return info;
        }

        public void CreateRequest()
        {
            CreateRequest(GuildManager.Instance.GetBattleData(true));
            // Batch calculation intentionally uses a fresh seed for each simulation.
            request["Settings"]["UseFixedRandomSeed"] = false;
        }

        public void CreateRequest(GuildBattleData battleData)
        {
            var request = new BattleSimulationRequest();
            var settings = battleData.SettingData;
            var group = settings.GetCurrentPlayerGroup();

            request.BattleTime = settings.limitTime;
            request.GuildEnemyList = battleData.enemyData.Select(e => e.enemy_id).ToList();
            request.WaveGroupId = group.waveGroupId;
            request.LabyrinthQuestId = group.labyrinthQuestId;
            request.LabyrinthDifficulty = group.labyrinthDifficulty;

            request.IsAutoMode = battleData.isAutoMode;
            request.IsGuildBattle = true;
            request.IsSemanMode = battleData.isSemanMode;
            request.IsSetMode = battleData.isSetMode;
            request.IsSkipping = true;
            request.IsTalentQuest = GuildManager.Instance.IsTalentQuestBoss;
            request.UseSequenceUB = battleData.useSequenceUB;
            request.MasterSkillNodeLevels = group.teamSkill;
            request.RoleMastery = group.roleMastery;
            request.NeedOnceResult = true;
            request.LabyrinthEnhancedNodeIds = group.labyrinthEnhancedNodeIds;
            request.LabyrinthRelicIds = group.labyrinthRelicIds;
            request.LabyrinthAlphaRupee = group.labyrinthAlphaRupee;
            request.LabyrinthOwnedUnitsCount = group.labyrinthOwnedUnitsCount;
            request.LabyrinthOwnedUnitRoleCounts = group.labyrinthOwnedUnitRoleCounts;
            request.LabyrinthOwnedUnitTalentCounts = group.labyrinthOwnedUnitTalentCounts;
            request.PlayerList = settings.GetCurrentPlayerData().playrCharacters;
            request.SemanUBExecTimeList = battleData.SemanUBExecTimeList;
            request.SemanUBSequenceData = battleData.SemanUBSequenceData;
            request.Settings = settings.GetCurrentRandomData();
            request.TalentFullInfo = true;
            request.TalentSkillLevels = group.talentLevel;
            request.TalentSkillNodeLevels = group.talentSkill;
            request.ToMaxUnitAttr = false;
            request.UBExecTimeList = battleData.UBExecTimeList;
            this.request = JObject.FromObject(request);
        }

        public Task<BattleSimulationResponse[]> Simulate(int times)
        {
            return Send(new JArray(Enumerable.Range(0, times).Select(_ => request.DeepClone())), CancellationToken.None);
        }

        public async Task<BattleSimulationResponse> SimulateSeman(List<List<int>> sequence,
            SemanOptimizationTarget target, int tpDemand, CancellationToken token)
        {
            var candidate = (JObject)request.DeepClone();
            candidate["SemanUBExecTimeList"] = JArray.FromObject(sequence);
            candidate["OptimizationTarget"] = (int)target;
            candidate["OptimizationTpDemand"] = tpDemand;
            candidate["IsSemanMode"] = true;
            candidate["NeedOnceResult"] = false;
            var results = await Send(new JArray(candidate), token).ConfigureAwait(false);
            if (results.Length != 1 || results[0] == null || !results[0].Success)
                throw new InvalidOperationException(results.FirstOrDefault()?.Message ?? "脱机模拟未返回结果。");
            if (!results[0].OptimizationFitness.HasValue || results[0].SemanUBNum == null || results[0].SemanUBNum.Count != 5)
                throw new InvalidOperationException("脱机模拟程序不支持自动优化，请更新 server 程序。");
            return results[0];
        }

        private async Task<BattleSimulationResponse[]> Send(JArray requests, CancellationToken token)
        {
            using (var linked = CancellationTokenSource.CreateLinkedTokenSource(token, taskToken))
            {
                var json = await server.Send(requests.ToString(), linked.Token).ConfigureAwait(false);
                return JsonConvert.DeserializeObject<BattleSimulationResponse[]>(json)
                    ?? throw new InvalidOperationException("脱机模拟返回了空结果。");
            }
        }

        public void Dispose()
        {
            if (Interlocked.Exchange(ref disposed, 1) != 0)
                return;
            // A task owns its requests, not the shared companion process.
            lifetime.Cancel();
            lifetime.Dispose();
        }
    }
}
