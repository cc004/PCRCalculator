using Elements;
using Elements.Battle;
using Newtonsoft.Json;
using Org.BouncyCastle.Utilities;
using PCRCaculator;
using PCRCaculator.Battle;
using PCRCaculator.Guild;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;


#if ENABLE_THREADING
public class ThreadController : IDisposable
{
    private readonly BlockingCollection<(BattleSimulationRequest request, Action callback, Action<Exception> callbackFailed)> queue = new();

    public ThreadController(int? workerNum = null)
    {
        int effectiveWorkerNum = workerNum ?? ProcessorHelper.GetTotalLogicalProcessorCount();
        
        Console.WriteLine($"[ThreadController] 检测到处理器组数: {ProcessorHelper.GetProcessorGroupCount()}, 总逻辑处理器数: {ProcessorHelper.GetTotalLogicalProcessorCount()}, 使用线程数: {effectiveWorkerNum}");
        
        new SpineCreator().Awake();
        new BaseBackManager().Awake();
        new MainManager().Awake();

        new BattleUIManager()
        {
            parent = new(),
            timeScaleSlider = new(),
            timeText = new(),
        }.Awake();

        MainManager.Instance.Start();
        while (!MainManager.Instance.LoadFinished)
            Thread.Sleep(0);

        for (var i = 0; i < effectiveWorkerNum; ++i)
        {
            int threadIndex = i;
            new Thread(() => WorkerTask(threadIndex), 5 * 1024 * 1024).Start();
        }
    }

    public void DispatchTask(BattleSimulationRequest request, Action callback, Action<Exception> callbackFailed)
    {
        queue.Add((request, callback, callbackFailed));
    }

    private void WorkerTask(int threadIndex)
    {
        ProcessorHelper.SetThreadGroupAffinityForIndex(threadIndex);
        ThreadInit();
        foreach (var (request, callback, callbackFailed) in queue.GetConsumingEnumerable())
        {
            try
            {
                // Debug.Log($"[BattleSimulationServer] 战斗请求：{JsonConvert.SerializeObject(request)}");
                Simulate(request);
                callback();
            }
            catch (Exception e)
            {
                if (callbackFailed != null)
                    callbackFailed(e);
                else
                    Console.WriteLine(e);
            }
        }
    }
    private static void ThreadInit()
    {
        new GuildManager()
        {
            calSlider = new()
        }.Awake();

        GuildManager.Instance.SettingData.start_hp = "1,1,1,1,1";
        GuildManager.Instance.SettingData.start_tp = "0,0,0,0,0";

        new TalentLevelManager().Awake();
        new TalentSkillTreeManager().Awake();
        new TeamSkillTreeManager().Awake();
        new RoleMasteryManager().Awake();

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
            firearmPrefab = new()
        }.Awake();
        new BattleHeaderController().Awake();
        new GuildCalculator().Awake();

        new BattleManager()
        {
            gameObject = new(),
            playCamera = new(),
            bgTransform = new()
        }.Awake();
        MyGameCtrl.Instance.playerUnitCtrl = new List<UnitCtrl>();
        MyGameCtrl.Instance.enemyUnitCtrl = new List<UnitCtrl>();
        MyGameCtrl.Instance.extraEffectEnemyUnitCtrl = new List<UnitCtrl>();

        MyGameCtrl.Instance.Start();

        CoroutineRunner.Initialize();
    }

    [ThreadStatic] private static Stopwatch watch;

    private static void Simulate(BattleSimulationRequest request)
    {
        try
        {
            BattleManager.Instance.enabled = false;
            BattleManager.Instance.battleFinished = false;
            BattleManager.Instance.battleFinishedProcessed = false;

            MyGameCtrl.Instance.StartByServer(request);

            // MyGameCtrl.Instance.tempData.skipping = request.IsSkipping;

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
            
            watch ??= new Stopwatch();
            watch.Restart();
            
            while (!BattleManager.Instance.battleFinishedProcessed)
            {
                CoroutineRunner.Update();
                if (watch.ElapsedMilliseconds > 60000)
                {
                    Console.WriteLine("battle stuck detected, request = " + JsonConvert.SerializeObject(request));
                    throw new Exception();
                }
            }
        }
        finally
        {
            if (!MyGameCtrl.Instance.isBattleFinish)
            {
                BattleManager.Instance.DoCleaning(1);
            }

            UnityEngine.CoroutineRunner.pending.Clear();
            UnityEngine.CoroutineRunner.running.Clear();

        }

    }

    public void Dispose()
    {
        queue.CompleteAdding();
        queue.Dispose();
    }
}

#endif
