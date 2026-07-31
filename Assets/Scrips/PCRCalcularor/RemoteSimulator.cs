using Elements;
using Elements.Battle;
using Newtonsoft.Json.Linq;
using PCRCaculator.Guild;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PCRCaculator
{
    public class RemoteSimulator : IDisposable
    {
        private readonly Process serverProcess;
        private readonly int port;
        private static HttpClient client = new HttpClient();
        private JObject request;

        public RemoteSimulator()
        {
            var listener = new TcpListener(IPAddress.Loopback, 0);
            listener.Start();
            port = ((IPEndPoint)listener.LocalEndpoint).Port;
            listener.Stop();

            serverProcess = Process.Start(
                Path.GetFullPath("server/PCRCalculatorServer.exe"), $"--server --port {port} --host localhost");
        }

        public void CreateRequest()
        {
            var request = new BattleSimulationRequest();
            var battleData = GuildManager.Instance.GetBattleData(true);
            var group = GuildManager.Instance.SettingData.GetCurrentPlayerGroup();

            request.BattleTime = GuildManager.Instance.SettingData.limitTime;
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
            request.MasterSkillNodeLevels = group.teamSkill;
            request.NeedOnceResult = true;
            request.LabyrinthEnhancedNodeIds = group.labyrinthEnhancedNodeIds;
            request.LabyrinthRelicIds = group.labyrinthRelicIds;
            request.LabyrinthAlphaRupee = group.labyrinthAlphaRupee;
            request.PlayerList = GuildManager.Instance.SettingData.GetCurrentPlayerData().playrCharacters;
            request.SemanUBExecTimeList = battleData.SemanUBExecTimeList;
            request.Settings = GuildManager.Instance.SettingData.GetCurrentRandomData();
            request.TalentFullInfo = true;
            request.TalentSkillLevels = group.talentLevel;
            request.TalentSkillNodeLevels = group.talentSkill;
            request.ToMaxUnitAttr = false;
            request.UBExecTimeList = battleData.UBExecTimeList; 
            request.Settings.UseFixedRandomSeed = false;
            this.request = JObject.FromObject(request);
        }

        public async Task<BattleSimulationResponse[]> Simulate(int times)
        {
            var content = new JArray(Enumerable.Range(0, times)
                .Select(_ => request)).ToString();
            UnityEngine.Debug.Log(content);
            var resp = await client.PostAsync($"http://localhost:{port}/simulate", new StringContent(
                content, Encoding.UTF8, "application/json"));
            content = await resp.Content.ReadAsStringAsync();
            UnityEngine.Debug.Log(content);
            return JsonConvert.DeserializeObject<BattleSimulationResponse[]>(content);
        }

        public void Dispose()
        {
            if (!serverProcess.HasExited)
            {
                try
                {
                    serverProcess.Kill();
                }
                catch
                {

                }
            }
            serverProcess.Dispose();
        }
    }
}
