using Elements;
using Elements.Battle;
using OfficeOpenXml;
using PCRCaculator;
using PCRCaculator.Battle;
using PCRCaculator.Guild;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class CommandLineServer : MonoBehaviour
{
    public static bool serverMode = true;
    private static int serverPort = 8080;
    private static string targetScene = "BattleScene";

    public static void Main(string[] args)
    {

        ParseCommandLineArgs();

        new SpineCreator().Awake();
        new BaseBackManager().Awake();
        new MainManager().Awake();
        new MyGameCtrl()
        {
            unitParent = new(),
            enemyParent = new(),
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
            bgTransform = new(),
        }.Awake();
        BattleUIManager.Instance.Start();

        MyGameCtrl.Instance.playerUnitCtrl = new List<UnitCtrl>();
        MyGameCtrl.Instance.enemyUnitCtrl = new List<UnitCtrl>();
        MyGameCtrl.Instance.extraEffectEnemyUnitCtrl = new List<UnitCtrl>();

        MyGameCtrl.Instance.Start();

        IEnumerator UpdateBattleManager()
        {
            for (;;)
            {
                if (BattleManager.Instance.enabled)
                    BattleManager.Instance.Update();
                yield return null;
            }
        }

        CoroutineRunner.AppendCoroutine(UpdateBattleManager());

        var watch = new Stopwatch();
        var times = 0;
        var i = 0;
        var unit = MainManager.Instance.UnitRarityDic.Keys.ToArray();
        watch.Start();
        long last = 0;

        long s = 0;

        for (;;)
        {
            BattleManager.Instance.enabled = false;
            BattleManager.Instance.battleFinished = false;
            BattleManager.Instance.battleFinishedProcessed = false;
            
            MyGameCtrl.Instance.StartByServer(new BattleSimulationRequest
            {
                PlayerList = [
                    new PCRCaculator.UnitData(unit[i + 0], 200, 3, 20),
                    new PCRCaculator.UnitData(unit[i + 1], 200, 3, 20),
                    new PCRCaculator.UnitData(unit[i + 2], 200, 3, 20),
                    new PCRCaculator.UnitData(unit[i + 3], 200, 3, 20),
                    new PCRCaculator.UnitData(unit[i + 4], 200, 3, 20),
                ],
                EnemyList = [
                    new PCRCaculator.UnitData(unit[i + 5], 200, 3, 20),
                    new PCRCaculator.UnitData(unit[i + 6], 200, 3, 20),
                    new PCRCaculator.UnitData(unit[i + 7], 200, 3, 20),
                    new PCRCaculator.UnitData(unit[i + 8], 200, 3, 20),
                    new PCRCaculator.UnitData(unit[i + 9], 200, 3, 20),
                ],
                IsGuildBattle = false,
            });
            ++times;

            BattleManager.Instance.skipping = true;

            while (!BattleManager.Instance.battleFinishedProcessed)
            {
                CoroutineRunner.Update();
            }
            /*
            Console.WriteLine("模拟结果：");
            foreach (var u in BattleManager.Instance.UnitList.Concat(BattleManager.Instance.EnemyList))
            {
                Console.WriteLine($"{u.UnitName} HP = {(long)u.Hp}");
            }*/
            var now = watch.ElapsedMilliseconds;
            s += BattleManager.Instance.FrameCount;
            Console.WriteLine($"总帧数：{BattleManager.Instance.FrameCount} frames 模拟时间：{now - last}ms 模拟速度 {s * 1000 / now} fps");
            last = now;
        }



    }

    private static void ParseCommandLineArgs()
    {
        string[] args = Environment.GetCommandLineArgs();

        for (int i = 0; i < args.Length; i++)
        {
            switch (args[i].ToLower())
            {
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

        // 也可以通过环境变量控制
        string envServer = Environment.GetEnvironmentVariable("PCR_SERVER_MODE");
        if (!string.IsNullOrEmpty(envServer) && envServer.ToLower() == "true")
        {
            serverMode = true;
            Debug.Log("通过环境变量启用服务器模式");
        }

        string envPort = Environment.GetEnvironmentVariable("PCR_SERVER_PORT");
        if (!string.IsNullOrEmpty(envPort) && int.TryParse(envPort, out int envPortValue))
        {
            serverPort = envPortValue;
            Debug.Log($"通过环境变量设置服务器端口: {envPortValue}");
        }
    }

}