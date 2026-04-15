using Elements;
using Elements.Battle;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using OfficeOpenXml;
using Org.BouncyCastle.Asn1.Ocsp;
using PCRCaculator;
using PCRCaculator.Battle;
using PCRCaculator.Guild;
using PCRCalculatorServer;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using UnityEngine;
using Debug = UnityEngine.Debug;
using UnitData = PCRCaculator.UnitData;

public static class UnitDataExtension
{
    public static UnitData Max(this UnitData data)
    {
        data.SetMax();
        data.SetMaxLove();
        return data;
    }
}
public class CommandLineServer : MonoBehaviour
{
    public static bool serverMode = false;
    public static bool debugMode = false;
    public static bool batchMode = false;
    public static string excelPath = "";
    private static int serverPort = 8080;
    private static string serverHost = "localhost";
    private static string targetScene = "BattleScene";
    
    // 用于优雅关闭的信号
    private static readonly CancellationTokenSource shutdownTokenSource = new CancellationTokenSource();
    private static readonly ManualResetEvent serverReadyEvent = new ManualResetEvent(false);

    private static int working;

    public static void Main(string[] args)
    {
        if (!Directory.Exists("Data"))
        {
            var exeDir = AppContext.BaseDirectory;
            Directory.SetCurrentDirectory(exeDir);
        }

        ParseCommandLineArgs();
        if (Debugger.IsAttached)
        {
            // debugMode = true;
            Debug.Log("检测到调试器附加，调试模式已启用");
        }
#if ENABLE_THREADING
        if (serverMode)
        {
            RuntimeHelpers.RunClassConstructor(typeof(ApiController).TypeHandle);

            Host.CreateDefaultBuilder(args)
                .ConfigureLogging(logger => logger.SetMinimumLevel(debugMode ? LogLevel.Information : LogLevel.Error))
                .ConfigureServices(services =>
                {
                    services.AddSingleton(new ServerOptions { DebugMode = debugMode });
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseUrls($"http://{serverHost}:{serverPort}/").UseStartup<Startup>();
                }).Build().Run();
        }
        else
        {
            // var controller = new ThreadController(1);
            var controller = new ThreadController();

            var requests = new[]
            {
                JsonConvert.DeserializeObject<BattleSimulationRequest>(File.ReadAllText("request1.json")),
            };

            if (debugMode)
            {
                foreach (var request in requests)
                {
                    request.IsSkipping = false;
                }
            }

            long s = 0, s2 = 0;

            Stopwatch sw = new Stopwatch();
            sw.Start();

            void Callback()
            {
                --working;
                s += BattleManager.Instance.FrameCount;
                s2 += BattleHeaderController.CurrentFrameCount;
                var now = sw.ElapsedMilliseconds;
                Console.WriteLine(
                    $"[Thread #{Thread.CurrentThread.ManagedThreadId}]" +
                    $"总帧数：{BattleManager.Instance.FrameCount}/{BattleHeaderController.CurrentFrameCount} frames " +
                    $"总伤害: {GuildCalculator.Instance.GetTotalDamage()} 模拟速度 {s * 1000 / now}/{s2 * 1000 / now} fps ");
            }

            for (;;)
                foreach (var request in requests)
                {
                    ++working;
                    controller.DispatchTask(request, Callback, null);
                    while (working > 100)
                    {
                        Thread.Sleep(0);
                    }
                }
        }
#else

        new SpineCreator().Awake();
        new BaseBackManager().Awake();
        new MainManager().Awake();
        const float parentScale = 1 / 540f;
        const float parentOffset = 5000f * parentScale;
        new MyGameCtrl()
        {
            unitParent = new()
            {
                localScale = parentScale * Vector3.one,
                localPosition = new Vector3(0, parentOffset, 0)
            },
            enemyParent = new()
            {
                localScale = parentScale * Vector3.one,
                localPosition = new Vector3(0, parentOffset, 0)
            },
            firearmPrefab = new ()
        }.Awake();
        new BattleHeaderController().Awake();
        new BattleUIManager()
        {
            parent = new(),
            timeScaleSlider = new(),
            timeText = new(),
        }.Awake();
        new GuildManager()
        {
            calSlider = new()
        }.Awake();
        new GuildCalculator().Awake();

        GuildManager.Instance.SettingData.start_hp = "1,1,1,1,1";
        GuildManager.Instance.SettingData.start_tp = "0,0,0,0,0";
            

        MainManager.Instance.Start();
        while (!MainManager.Instance.LoadFinished)
            Thread.Sleep(0);

        new BattleManager()
        {
            gameObject = new(),
            playCamera = new(),
            bgTransform = new()
        }.Awake();
        BattleUIManager.Instance.Start();
        new TalentLevelManager().Awake();
        new TalentSkillTreeManager().Awake();
        new RoleMasteryManager().Awake();
        new TeamSkillTreeManager().Awake();

        MyGameCtrl.Instance.playerUnitCtrl = new List<UnitCtrl>();
        MyGameCtrl.Instance.enemyUnitCtrl = new List<UnitCtrl>();
        MyGameCtrl.Instance.extraEffectEnemyUnitCtrl = new List<UnitCtrl>();

        MyGameCtrl.Instance.Start();

        var watch = new Stopwatch();
        var times = 0;
        var i = 0;
        var unit = MainManager.Instance.UnitRarityDic.Keys.ToArray();
        watch.Start();
        long last = 0;

        long s = 0, s2 = 0;

        if (serverMode)
        {
            new BattleSimulationServer().Awake();
            BattleSimulationServer.Instance.port = serverPort;
            BattleSimulationServer.Instance.host = serverHost;
            BattleSimulationServer.Instance.debugMode = debugMode;
            BattleSimulationServer.Instance.StartServer();
            Debug.Log($"服务器模式已启动，监听 http://{serverHost}:{serverPort}/");
            
            serverReadyEvent.Set();
            shutdownTokenSource.Token.WaitHandle.WaitOne();
            
            // 优雅关闭
            Debug.Log("服务器正在关闭...");
            BattleSimulationServer.Instance.StopServer();
        }
        else if (batchMode)
        {
            
        }
        else
        {
            for (;;)
            {
                BattleManager.Instance.enabled = false;
                BattleManager.Instance.battleFinished = false;
                BattleManager.Instance.battleFinishedProcessed = false;

                MyGameCtrl.Instance.StartByServer(JsonConvert.DeserializeObject<BattleSimulationRequest>(File.ReadAllText("request.json")));
                /*
                var seed = 1714804813;
                MyGameCtrl.Instance.tempData.randomData = new();
                MyGameCtrl.Instance.tempData.randomData.RandomSeed = seed;
                Console.WriteLine("seed: " + seed);*/
                /*

                MyGameCtrl.Instance.tempData.randomData = new();
                MyGameCtrl.Instance.tempData.randomData.RandomSeed = 1128444592;
                */
                ++times;

                MyGameCtrl.Instance.tempData.skipping = debugMode ? false : true;

                IEnumerator UpdateBattleManager()
                {
                    for (; ; )
                    {
                        if (BattleManager.Instance.enabled)
                            BattleManager.Instance.Update();
                        yield return null;
                    }
                }

                CoroutineRunner.AppendCoroutine(UpdateBattleManager());

                while (!BattleManager.Instance.battleFinishedProcessed)
                {
                    CoroutineRunner.Update();
                }

                CoroutineRunner.running.Clear();
                CoroutineRunner.pending.Clear();

                /*
                Console.WriteLine("模拟结果：");
                foreach (var u in BattleManager.Instance.UnitList.Concat(BattleManager.Instance.EnemyList))
                {
                    Console.WriteLine($"{u.UnitName} HP = {(long)u.Hp}");
                }*/
                var now = watch.ElapsedMilliseconds;
                s += BattleManager.Instance.FrameCount;
                s2 += BattleHeaderController.CurrentFrameCount;
                Console.WriteLine($"总帧数：{BattleManager.Instance.FrameCount}/{BattleHeaderController.CurrentFrameCount} frames " +
                                $"模拟时间：{now - last}ms 模拟速度 {s * 1000 / now}/{s2 * 1000 / now} fps 总伤害: {GuildCalculator.Instance.GetTotalDamage()}");
                last = now;
                if (debugMode)
                    break;
            }
        }
#endif


    }

    private static void ParseCommandLineArgs()
    {
        string[] args = Environment.GetCommandLineArgs();

        for (int i = 0; i < args.Length; i++)
        {
            switch (args[i].ToLower())
            {
                case "-batch":
                case "--batch":
                    batchMode = true;
                    Debug.Log("批处理模式已启用");
                    break;
                
                case "-path":
                case "--path":
                    excelPath = args[i + 1];
                    ++ i;
                    break;

                case "-server":
                case "--server":
                    serverMode = true;
                    Debug.Log("服务器模式已启用");
                    break;

                case "-port":
                case "--port":
                    if (i + 1 < args.Length && int.TryParse(args[i + 1], out int port))
                    {
                        serverPort = port;
                        Debug.Log($"服务器端口设置为: {port}");
                        i++; // 跳过下一个参数
                    }
                    break;

                case "-host":
                case "--host":
                    if (i + 1 < args.Length)
                    {
                        var host = args[i + 1];
                        serverHost = host;
                        Debug.Log($"服务器绑定域名设置为: {host}");
                        i++; // 跳过下一个参数
                    }
                    break;
                
                case "-debug":
                case "--debug":
                    debugMode = true;
                    Debug.Log("调试模式已启用");
                    break;

                case "-scene":
                case "--scene":
                    if (i + 1 < args.Length)
                    {
                        targetScene = args[i + 1];
                        Debug.Log($"目标场景设置为: {targetScene}");
                        i++;
                    }
                    break;
            }
        }
    }

    /// <summary>
    /// 触发服务器关闭信号（可从其他线程调用）
    /// </summary>
    public static void RequestShutdown()
    {
        Debug.Log("收到关闭请求");
        shutdownTokenSource.Cancel();
    }
}
