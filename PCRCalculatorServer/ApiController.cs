using Elements;
using Elements.Battle;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Ocsp;
using PCRCaculator;
using PCRCaculator.Guild;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UnityEngine;

namespace PCRCalculatorServer
{
    [ApiController]
    public class ApiController : Controller
    {
        private static readonly ThreadController controller = new();
        private static readonly JsonSerializerOptions DebugJsonOptions = new()
        {
            IncludeFields = true,
            WriteIndented = true
        };
        private readonly ServerOptions serverOptions;

        public ApiController(ServerOptions serverOptions)
        {
            this.serverOptions = serverOptions ?? throw new ArgumentNullException(nameof(serverOptions));
        }

        [HttpPost("/simulate")]
        public async Task<BattleSimulationResponse[]> Simulate(BattleSimulationRequest[] battleRequest)
        {
            battleRequest ??= Array.Empty<BattleSimulationRequest>();

            var (expandedRequests, requestMap, groupSizes) = ExpandRequests(battleRequest);

            if (expandedRequests.Count == 0)
            {
                return AggregateResponses(battleRequest.Length, groupSizes, requestMap, Array.Empty<BattleSimulationResponse>());
            }

            if (serverOptions.DebugMode)
            {
                foreach (var request in expandedRequests)
                {
                    request.IsSkipping = false;
                }
            }

            using var sema = new Semaphore(0, expandedRequests.Count);

            var response = new BattleSimulationResponse[expandedRequests.Count];

            void OnException(int index, Exception ex)
            {
                Debug.LogError($"[BattleSimulationServer] StartByServer 失败: {ex.Message}\n{ex.StackTrace}");
                response[index] = CreateFailedResponse($"StartByServer 失败: {ex.Message}");
                sema.Release();
            }

            void OnFinish(int index)
            {
                // 获取战斗结果
                try
                {
                    var request = expandedRequests[index];
                    var battleResult = BattleManager.Instance.battleResult;

                    var totalDamage = GuildCalculator.Instance.GetTotalDamage();

                    var battleFrames = BattleHeaderController.CurrentFrameCount;

                    var currentSeed = MyGameCtrl.Instance.CurrentSeedForSave;
                    var playerList = MyGameCtrl.Instance.tempData.playerList;
                    var enemyList = MyGameCtrl.Instance.tempData.enemyList;

                    OnceResultData onceResult = null;

                    if (request.NeedOnceResult)
                    {
                        var BattleResult = GuildCalculator.Instance.BattleResult;

                        List<string> errorList = new List<string>();
                        List<List<float>> ubExecTime = GuildCalculator.Instance.CreateUBExecTimeData();
                        for (int i = 0; i < 5; i++)
                        {
                            if (ubExecTime.Count > i && MyGameCtrl.Instance.tempData.UBExecTimeList.Count > i)
                            {
                                if (MyGameCtrl.Instance.tempData.UBExecTimeList[i].Count != ubExecTime[i].Count)
                                {
                                    errorList.Add(GuildCalculator.Instance.players[i].UnitName + "的某个UB释放失败！");
                                }
                            }
                        }
                        var currentFrame = BattleHeaderController.CurrentFrameCount;
                        int additionalTime = (BattleResult == eBattleResult.WIN && true) ? 20 : 0;
                        var backTime = Mathf.CeilToInt((MyGameCtrl.Instance.tempData.SettingData.limitTime * 60 - currentFrame) / 60.0f) + additionalTime;
                        string prefix = true ? (BattleResult == eBattleResult.WIN ? "返" : "剩") : "剩";
                        var detail = $"{prefix}{backTime}s";
                        onceResult = new OnceResultData
                        {
                            id = 0,
                            exceptDamage = GuildCalculator.Instance.totalDamageExcept.Expect,
                            criticalEX = GuildCalculator.Instance.totalDamageCriEX,
                            currentDamage = totalDamage,
                            randomSeed = MyGameCtrl.Instance.CurrentSeedForSave,
                            backTime = backTime,
                            backTimeText = detail,
                            warnings = errorList,
                            BattleResult = BattleResult,
                        };
                    }

                    response[index] = new BattleSimulationResponse
                    {
                        PlayerList = playerList,
                        EnemyList = enemyList,
                        Success = true,
                        Message = "战斗模拟完成",
                        Stats = request.CalcStats != 0 ? MyGameCtrl.Instance.BattleStats.ToBattleStatsData() : null,
                        BattleFrames = battleFrames,
                        CurrentSeed = currentSeed,
                        TalentLevel = GuildManager.Instance.GetTalentLevelDescription(),
                        TalentSkill = GuildManager.Instance.GetFinalTalentSkillNodeDescription(),
                        TeamSkill = GuildManager.Instance.GetFinalTeamSkillNodeDescription(),
                        Result = battleResult,
                        OnceResult = onceResult
                    };

                    if (request.IsGuildBattle && request.IsSemanMode)
                    {
                        response[index].SemanUBNum = BattleManager.Instance.semanubmanager.UBNums;
                    }

                    // Debug.Log($"[BattleSimulationServer] ✓ 战斗结果获取成功: Result={battleResult}, Damage={totalDamage}, Frames={battleFrames}, Seed={currentSeed}");

                }
                catch (Exception ex)
                {
                    Debug.LogError($"[BattleSimulationServer] ✗ 获取战斗结果失败: {ex.Message}\n{ex.StackTrace}");
                    response[index] = CreateFailedResponse($"获取战斗结果失败: {ex.Message}");
                }
                finally
                {
                    sema.Release();
                }
            }

            if (serverOptions.DebugMode)
            {
                foreach (var req in expandedRequests)
                    Debug.Log($"[BattleSimulationServer] 战斗请求：{JsonSerializer.Serialize(req, DebugJsonOptions)}");
            }

            for (var i = 0; i < expandedRequests.Count; ++i)
            {
                var i2 = i;
                controller.DispatchTask(expandedRequests[i], () => OnFinish(i2), ex => OnException(i2, ex));
            }

            await Task.Run(() =>
            {
                for (var i = 0; i < expandedRequests.Count; ++i)
                    sema.WaitOne();
            });
            return AggregateResponses(battleRequest.Length, groupSizes, requestMap, response);
        }

        private static (List<BattleSimulationRequest> ExpandedRequests, List<int> RequestMap, int[] GroupSizes) ExpandRequests(BattleSimulationRequest[] battleRequest)
        {
            var expandedRequests = new List<BattleSimulationRequest>();
            var requestMap = new List<int>();
            var groupSizes = new int[battleRequest.Length];

            for (var i = 0; i < battleRequest.Length; ++i)
            {
                var request = battleRequest[i];
                if (request == null)
                {
                    groupSizes[i] = 0;
                    continue;
                }

                var repeatCount = request.RepeatCount <= 0 ? 1 : request.RepeatCount;
                groupSizes[i] = repeatCount;
                request.RepeatCount = 1;

                if (repeatCount == 1)
                {
                    expandedRequests.Add(request);
                    requestMap.Add(i);
                    continue;
                }

                var requestJson = Newtonsoft.Json.JsonConvert.SerializeObject(request);
                for (var j = 0; j < repeatCount; ++j)
                {
                    var clonedRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<BattleSimulationRequest>(requestJson);
                    if (clonedRequest == null)
                    {
                        throw new InvalidOperationException("展开重复请求失败：无法反序列化 BattleSimulationRequest。");
                    }
                    clonedRequest.RepeatCount = 1;
                    expandedRequests.Add(clonedRequest);
                    requestMap.Add(i);
                }
            }

            return (expandedRequests, requestMap, groupSizes);
        }

        private static BattleSimulationResponse[] AggregateResponses(int requestCount, int[] groupSizes, List<int> requestMap, BattleSimulationResponse[] expandedResponses)
        {
            var groupedResponses = new List<BattleSimulationResponse>[requestCount];
            for (var i = 0; i < requestCount; ++i)
            {
                groupedResponses[i] = new List<BattleSimulationResponse>(groupSizes[i]);
            }

            for (var i = 0; i < expandedResponses.Length; ++i)
            {
                var requestIndex = requestMap[i];
                groupedResponses[requestIndex].Add(expandedResponses[i] ?? CreateFailedResponse("模拟失败：未返回结果。"));
            }

            var responses = new BattleSimulationResponse[requestCount];
            for (var i = 0; i < requestCount; ++i)
            {
                var runs = groupedResponses[i];
                if (runs.Count == 0)
                {
                    responses[i] = CreateFailedResponse("请求为空。");
                    continue;
                }

                if (runs.Count == 1)
                {
                    responses[i] = runs[0];
                    continue;
                }

                var first = runs.FirstOrDefault(r => r != null && r.Success)
                    ?? runs.FirstOrDefault(r => r != null)
                    ?? CreateFailedResponse("模拟失败：未返回结果。");
                var allSuccess = runs.All(r => r != null && r.Success);
                var errors = runs
                    .Where(r => r == null || !r.Success)
                    .Select(r => r?.Message)
                    .Where(m => !string.IsNullOrWhiteSpace(m))
                    .Distinct()
                    .ToList();

                responses[i] = new BattleSimulationResponse
                {
                    PlayerList = first.PlayerList,
                    EnemyList = first.EnemyList,
                    Success = allSuccess,
                    Message = allSuccess ? $"战斗模拟完成（重复 {runs.Count} 次）" : (errors.Count > 0 ? string.Join(" | ", errors) : "部分重复模拟失败。"),
                    Stats = first.Stats,
                    BattleFrames = first.BattleFrames,
                    CurrentSeed = first.CurrentSeed,
                    TalentLevel = first.TalentLevel,
                    TalentSkill = first.TalentSkill,
                    TeamSkill = first.TeamSkill,
                    SemanUBNum = first.SemanUBNum,
                    Result = first.Result,
                    OnceResult = first.OnceResult,
                    ResultList = runs.Select(r => r?.Result ?? first.Result).ToList(),
                    BattleFramesList = runs.Select(r => r?.BattleFrames ?? first.BattleFrames).ToList(),
                    CurrentSeedList = runs.Select(r => r?.CurrentSeed ?? first.CurrentSeed).ToList()
                };
            }

            return responses;
        }

        private static BattleSimulationResponse CreateFailedResponse(string message)
        {
            return new BattleSimulationResponse
            {
                Success = false,
                Message = message
            };
        }
    }

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.IncludeFields = true;
                    options.JsonSerializerOptions.PropertyNamingPolicy = null;
                    options.JsonSerializerOptions.DictionaryKeyPolicy = null;
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
