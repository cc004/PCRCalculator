using Elements;
using Elements.Battle;
using Microsoft.AspNetCore.Mvc;
using PCRCaculator;
using PCRCaculator.Guild;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using UnityEngine;

namespace PCRCalculatorServer
{
    [ApiController]
    public class ApiController : Controller
    {
        private static readonly ThreadController controller = new(Environment.ProcessorCount);
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
            if (serverOptions.DebugMode)
            {
                foreach (var request in battleRequest)
                {
                    request.IsSkipping = false;
                }
            }

            using var sema = new Semaphore(0, battleRequest.Length);

            var response = new BattleSimulationResponse[battleRequest.Length];

            void OnException(Exception ex)
            {
                Debug.LogError($"[BattleSimulationServer] StartByServer 失败: {ex.Message}\n{ex.StackTrace}");
            }

            void OnFinish(int index)
            {
                // 获取战斗结果
                try
                {
                    Debug.Log($"[BattleSimulationServer] 开始获取战斗结果...");

                    var battleResult = BattleManager.Instance.battleResult;

                    var totalDamage = GuildCalculator.Instance.GetTotalDamage();

                    var battleFrames = BattleHeaderController.CurrentFrameCount;

                    var currentSeed = MyGameCtrl.Instance.CurrentSeedForSave;
                    var playerList = MyGameCtrl.Instance.tempData.playerList;
                    var enemyList = MyGameCtrl.Instance.tempData.enemyList;

                    response[index] = new BattleSimulationResponse
                    {
                        PlayerList = playerList,
                        EnemyList = enemyList,
                        Success = true,
                        Message = "战斗模拟完成",
                        TotalDamage = totalDamage,
                        BattleFrames = battleFrames,
                        CurrentSeed = currentSeed,
                        TalentLevel = GuildManager.Instance.GetTalentLevelDescription(),
                        TalentSkill = GuildManager.Instance.GetFinalTalentSkillNodeDescription(),
                        TeamSkill = GuildManager.Instance.GetFinalTeamSkillNodeDescription(),
                        Result = battleResult
                    };

                    if (battleRequest[index].IsGuildBattle && battleRequest[index].IsSemanMode)
                    {
                        response[index].SemanUBNum = BattleManager.Instance.semanubmanager.UBNums;
                    }

                    Debug.Log(
                        $"[BattleSimulationServer] ✓ 战斗结果获取成功: Result={battleResult}, Damage={totalDamage}, Frames={battleFrames}, Seed={currentSeed}");

                }
                catch (Exception ex)
                {
                    Debug.LogError($"[BattleSimulationServer] ✗ 获取战斗结果失败: {ex.Message}\n{ex.StackTrace}");
                }
                finally
                {
                    sema.Release();
                }
            }

            if (serverOptions.DebugMode)
            {
                foreach (var req in battleRequest)
                    Debug.Log($"[BattleSimulationServer] 战斗请求：{JsonSerializer.Serialize(req, DebugJsonOptions)}");
            }

            for (var i = 0; i < battleRequest.Length; ++i)
            {
                var i2 = i;
                controller.DispatchTask(battleRequest[i], () => OnFinish(i2), OnException);
            }

            await Task.Run(() =>
            {
                for (var i = 0; i < battleRequest.Length; ++i)
                    sema.WaitOne();
            });
            return response;
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
