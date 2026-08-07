using System;
using System.Collections;
using System.Net;
using System.Threading;
using UnityEngine;
using System.Text;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using PCRCaculator;
using PCRCaculator.Battle;
using PCRCaculator.Guild;
using UnityEngine.UI;
using System.Linq;

namespace Elements.Battle
{
    public enum BattleResult
    {
        Victory = 0,
        Lose = 1,
        TimeOver = 2
    }

    public enum BattleStatus
    {
        Queued,      // 在队列中等待
        Processing,  // 正在处理
        Completed,   // 已完成
        Failed       // 失败
    }

    public class BattleSubmitResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string Uid { get; set; }
    }

    public class BattleQueryResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public BattleStatus Status { get; set; }
        public int QueuePosition { get; set; }  // 如果在队列中，显示位置
        public BattleSimulationResponse Result { get; set; }  // 如果完成，返回结果
    }

    [Serializable]
    public class BuffDebuffEventData
    {
        public int TargetUnitId { get; set; }
        public int SourceUnitId { get; set; }
        public UnitCtrl.BuffParamKind BuffParamKind { get; set; }
        public int BuffParamKindValue { get; set; }
        public string BuffParamKindName { get; set; }
        public string BuffParamKindDescription { get; set; }
        public bool IsBuff { get; set; }
        public long Value { get; set; }
        public int StartFrame { get; set; }
        public int EndFrame { get; set; }
        public bool IsActiveAtBattleEnd { get; set; }
        public int SkillId { get; set; }
        public int ActionId { get; set; }
        public string Describe { get; set; }
    }

    [Serializable]
    public class BossDefEventData
    {
        public int TargetUnitId { get; set; }
        public int Frame { get; set; }
        public long Value { get; set; }
        public int SourceUnitId { get; set; }
        public int SkillId { get; set; }
        public int ActionId { get; set; }
        public string Describe { get; set; }
    }

    [Serializable]
    public class SkillExecEventData
    {
        public int UnitId { get; set; }
        public int SkillId { get; set; }
        public string SkillName { get; set; }
        public int ActionId { get; set; }
        public int StartFrame { get; set; }
        public int EndFrame { get; set; }
    }

    [Serializable]
    public class HitTraceEventData
    {
        public int Frame { get; set; }
        public int SourceUnitId { get; set; }
        public int TargetUnitId { get; set; }
        public int SkillId { get; set; }
        public string SkillName { get; set; }
        public int ActionId { get; set; }
        public long Damage { get; set; }
        public DamageData.eDamageType DamageType { get; set; }
        public int DamageTypeValue { get; set; }
        public string DamageTypeName { get; set; }
        public bool IsCritical { get; set; }
        public long RemainingHp { get; set; }
        public int TargetPartsIndex { get; set; }
        public eActionType ActionType { get; set; }
        public int ActionTypeValue { get; set; }
        public string ActionTypeName { get; set; }
        public bool IsSlipDamage { get; set; }
    }

    [Serializable]
    public class TpChangeEventData
    {
        public int Frame { get; set; }
        public int TargetUnitId { get; set; }
        public int TargetLaneIndex { get; set; }
        public int SourceUnitId { get; set; }
        public int SourceLaneIndex { get; set; }
        public int SetEnergyTypeValue { get; set; }
        public string SetEnergyTypeName { get; set; }
        public int SkillId { get; set; }
        public string SkillName { get; set; }
        public int ActionId { get; set; }
        public double EnergyBefore { get; set; }
        public double EnergyAfter { get; set; }
        public double EnergyCap { get; set; }
        public double DeltaBeforeCap { get; set; }
        public double AppliedDelta { get; set; }
        public double OverflowDelta { get; set; }
        public bool IsIncrease { get; set; }
        public bool IsReadyBefore { get; set; }
        public bool IsReadyAfter { get; set; }
        public bool CrossedReady { get; set; }
    }

    [Serializable]
    public class AutoSemanTeacherPayloadData
    {
        public string ToggleProfile { get; set; }
        public int SemanSkillToggleIndex { get; set; }
        public List<List<int>> TeacherGenesByLane { get; set; }
        public List<List<float>> UBTime { get; set; }
        public List<int> SemanUBNum { get; set; }
        public int CalcStats { get; set; }
        public int Seed { get; set; }
    }

    [Serializable]
    public class BattleStatsData
    {
        public long TotalDamage { get; set; }
        public long TotalMagicBuff { get; set; }
        public long TotalPhysicsBuff { get; set; }
        public long TotalMagicDebuff { get; set; }
        public long TotalPhysicsDebuff { get; set; }
        public double ScorePhysicsExpectation { get; set; }
        public double ScoreMagicExpectation { get; set; }
        public List<long> PlayerTpGained { get; set; }
        public List<long> PlayerDamageList { get; set; }
        public List<long> PlayerNonUbDamageList { get; set; }
        public List<List<int>> UBToleranceFrames { get; set; }
        public List<List<float>> UBTime { get; set; }
        public List<List<int>> SkillReadyCrossingFrames { get; set; }
        public List<List<int>> AutoSemanUBExecTimeList { get; set; }
        public List<AutoSemanTeacherPayloadData> AutoSemanTeacherPayloads { get; set; }
        public List<BuffDebuffEventData> BuffDebuffEvents { get; set; }
        public List<BossDefEventData> BossDefEvents { get; set; }
        public List<BossDefEventData> BossMagicDefEvents { get; set; }
        public List<SkillExecEventData> SkillExecEvents { get; set; }
        public List<HitTraceEventData> HitTraceEvents { get; set; }
        public List<TpChangeEventData> TpChangeEvents { get; set; }
    }

    [Serializable]
    public class BattleResponseValidationData
    {
        public bool Checked { get; set; }
        public bool Ok { get; set; } = true;
        public string FailureCode { get; set; } = "";
        public string FailureMessage { get; set; } = "";
        public bool WinConsistencyOk { get; set; } = true;
        public long TotalDamage { get; set; }
        public int BattleFrames { get; set; }
        public int LimitFrames { get; set; }
        public int CurrentSeed { get; set; }
        public bool RequestFixedSeed { get; set; }
        public int RequestSeed { get; set; }
        public int MainEnemyId { get; set; }
        public int MainUnitId { get; set; }
        public long MainInitialCurrentHp { get; set; }
        public long MainInitialMaxHp { get; set; }
        public long MainFinalHp { get; set; }
        public bool MainIsDead { get; set; }
        public bool SupportsSimpleGuildHpCheck { get; set; }
    }

    [Serializable]
    public class BattleSimulationResponse
    {
        public List<PCRCaculator.UnitData> PlayerList { get; set; }
        public List<PCRCaculator.UnitData> EnemyList { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public BattleStatsData Stats { get; set; }
        // public float RemainingTime { get; set; }
        public int BattleFrames { get; set; }
        public int CurrentSeed { get; set; }
        public string TalentLevel { get; set; }
        public string TalentSkill { get; set; }
        public string TeamSkill { get; set; }
        public List<int> SemanUBNum { get; set; }
        public eBattleResult Result { get; set; }
        public List<eBattleResult> ResultList { get; set; }
        public List<int> BattleFramesList { get; set; }
        public List<int> CurrentSeedList { get; set; }
        public OnceResultData OnceResult { get; set; }
        public BattleResponseValidationData Validation { get; set; }
    }

    public class BattleTask
    {
        public string Uid { get; set; }
        public BattleSimulationRequest Request { get; set; }
        public BattleStatus Status { get; set; }
        public BattleSimulationResponse Result { get; set; }
        public string ErrorMessage { get; set; }
    }

    [Serializable]
    public class BattleSimulationRequest
    {
        public int CalcStats { get; set; } = 0;
        public List<PCRCaculator.UnitData> PlayerList { get; set; }
        public List<PCRCaculator.UnitData> EnemyList { get; set; }
        // 如果要进行工会战，请提供 EnemyData 列表
        public List<int> GuildEnemyList { get; set; }
        public List<List<float>> UBExecTimeList { get; set; }
        public List<List<int>> SemanUBExecTimeList { get; set; }
        public List<int> SemanUBSequenceData { get; set; }
        public GuildRandomData Settings { get; set; } = new GuildRandomData();
        public Dictionary<int, int> TalentSkillLevels { get; set; }
        public Dictionary<int, int> TalentSkillNodeLevels { get; set; }
        public Dictionary<int, int> MasterSkillNodeLevels { get; set; }
        [JsonConverter(typeof(RoleMasteryJsonConverter))]
        public Dictionary<int, (int slotLevel, int enhanceLevel)> RoleMastery { get; set; }
        public List<int> LabyrinthEnhancedNodeIds { get; set; } = new List<int>();
        public List<int> LabyrinthRelicIds { get; set; } = new List<int>();
        public int LabyrinthAlphaRupee { get; set; }
        public int LabyrinthQuestId { get; set; }
        public int LabyrinthDifficulty { get; set; }
        public int BattleTime { get; set; } = 90;
        public bool TalentFullInfo { get; set; }
        public bool IsTalentQuest { get; set; }
        public bool ToMaxUnitAttr { get; set; }
        public bool IsAutoMode { get; set; }
        public bool IsSetMode { get; set; }
        public bool IsSemanMode { get; set; }
        public bool UseSequenceUB { get; set; }
        public bool IsGuildBattle { get; set; } = true;
        public bool IsSkipping { get; set; } = true;
        public int WaveGroupId { get; set; } = 0;
        public List<bool> SetToggles { get; set; }
        public bool NeedOnceResult { get; set; }
        public int RepeatCount { get; set; } = 1;

        public BattleSimulationRequest DeepClone()
        {
            return new BattleSimulationRequest
            {
                CalcStats = this.CalcStats,
                BattleTime = this.BattleTime,
                RepeatCount = 1,
                WaveGroupId = this.WaveGroupId,
                IsAutoMode = this.IsAutoMode,
                IsSetMode = this.IsSetMode,
                IsSemanMode = this.IsSemanMode,
                UseSequenceUB = this.UseSequenceUB,
                IsGuildBattle = this.IsGuildBattle,
                IsSkipping = this.IsSkipping,
                TalentFullInfo = this.TalentFullInfo,
                IsTalentQuest = this.IsTalentQuest,
                ToMaxUnitAttr = this.ToMaxUnitAttr,
                NeedOnceResult = this.NeedOnceResult,
                PlayerList = this.PlayerList?.Select(u => u.Clone()).ToList(),
                EnemyList = this.EnemyList?.Select(u => u.Clone()).ToList(),
                GuildEnemyList = this.GuildEnemyList?.ToList(),
                UBExecTimeList = this.UBExecTimeList?.Select(l => l.ToList()).ToList(),
                SemanUBExecTimeList = this.SemanUBExecTimeList?.Select(l => l.ToList()).ToList(),
                SemanUBSequenceData = this.SemanUBSequenceData?.ToList(),
                SetToggles = this.SetToggles?.ToList(),
                Settings = this.Settings != null ? new GuildRandomData
                {
                    DataName = this.Settings.DataName,
                    UseFixedRandomSeed = this.Settings.UseFixedRandomSeed,
                    RandomSeed = this.Settings.RandomSeed,
                    ForceNoCritical_player = this.Settings.ForceNoCritical_player,
                    ForceNoCritical_enemy = this.Settings.ForceNoCritical_enemy,
                    ForceIgnoreDodge_player = this.Settings.ForceIgnoreDodge_player,
                    ForceIgnoreDodge_enemy = this.Settings.ForceIgnoreDodge_enemy,
                    ForceCritical_player = this.Settings.ForceCritical_player,
                    randomSpecialDatas = this.Settings.randomSpecialDatas?.Select(s => s != null ? new GuildRandomSpecialData
                    {
                        fixTimeExec = s.fixTimeExec,
                        startFream = s.startFream,
                        endFream = s.endFream,
                        fixCountExec = s.fixCountExec,
                        countEcexNum = s.countEcexNum,
                        sourceNum = s.sourceNum,
                        sourceSkillNum = s.sourceSkillNum,
                        sourceSkillType = s.sourceSkillType,
                        targetNum = s.targetNum,
                        resuleType = s.resuleType
                    } : null).ToList()
                } : null,
                TalentSkillLevels = this.TalentSkillLevels != null ? new Dictionary<int, int>(this.TalentSkillLevels) : null,
                TalentSkillNodeLevels = this.TalentSkillNodeLevels != null ? new Dictionary<int, int>(this.TalentSkillNodeLevels) : null,
                MasterSkillNodeLevels = this.MasterSkillNodeLevels != null ? new Dictionary<int, int>(this.MasterSkillNodeLevels) : null,
                RoleMastery = this.RoleMastery != null ? new Dictionary<int, (int, int)>(this.RoleMastery) : null,
                LabyrinthEnhancedNodeIds = this.LabyrinthEnhancedNodeIds?.ToList(),
                LabyrinthRelicIds = this.LabyrinthRelicIds?.ToList(),
                LabyrinthAlphaRupee = this.LabyrinthAlphaRupee,
                LabyrinthQuestId = this.LabyrinthQuestId,
                LabyrinthDifficulty = this.LabyrinthDifficulty,
            };
        }
    }

#if UNITY_5_3_OR_NEWER
    public class BattleSimulationServer : MonoBehaviour // no use
    {
        public static BattleSimulationServer Instance;
        private HttpListener httpListener;
        private Thread listenerThread;
        private Thread queueProcessorThread;
        private bool isRunning = false;
        public bool debugMode = false;
        public int port = 8080;
        public Text conentText;

        // 异步队列系统
        private Queue<BattleTask> battleQueue = new Queue<BattleTask>();
        private Dictionary<string, BattleTask> battleTasks = new Dictionary<string, BattleTask>();
        private object queueLock = new object();
        private bool isProcessing = false;
        public string host = "localhost";

        public void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        void OnEnable()
        {
            StartServer();
        }

        void OnDisable()
        {
            StopServer();
        }

#if ENABLE_THREADING
        private ThreadController controller = new();
#endif

        public void StartServer()
        {
            if (isRunning) return;

            try
            {
                Debug.Log($"[BattleSimulationServer] 正在创建 HttpListener，端口: {port}");
                httpListener = new HttpListener();
                httpListener.Prefixes.Add($"http://{host}:{port}/");
                httpListener.Start();
                Debug.Log("[BattleSimulationServer] HttpListener 已启动");
                
                isRunning = true;
                listenerThread = new Thread(ListenForRequests);
                listenerThread.Start();

                // 启动队列处理线程
                queueProcessorThread = new Thread(ProcessQueue);
                queueProcessorThread.Start();
                
                Debug.Log($"[BattleSimulationServer] ✓ HTTP服务器已成功启动，监听端口: {port}");
            }
            catch (Exception ex)
            {
                Debug.LogError($"[BattleSimulationServer] ✗ 启动HTTP服务器失败: {ex.Message}\n{ex.StackTrace}");
            }
        }

        public void StopServer()
        {
            if (!isRunning) return;

            try
            {
                isRunning = false;
                httpListener?.Stop();
                listenerThread?.Join();
                queueProcessorThread?.Join();
                Debug.Log("HTTP服务器已停止");
            }
            catch (Exception ex)
            {
                Debug.LogError($"停止HTTP服务器失败: {ex.Message}");
            }
        }

        private string GenerateUid()
        {
            return Guid.NewGuid().ToString("N").Substring(0, 16);
        }

        private void ListenForRequests()
        {
            while (isRunning)
            {
                try
                {
                    var context = httpListener.GetContext();
                    ThreadPool.QueueUserWorkItem((_) => HandleRequest(context));
                }
                catch (Exception ex)
                {
                    if (isRunning)
                    {
                        Debug.LogError($"处理请求时发生错误: {ex.Message}");
                    }
                }
            }
        }

        private void HandleRequest(HttpListenerContext context)
        {
            string path = context.Request.Url.AbsolutePath;
            string method = context.Request.HttpMethod;
            Debug.Log($"[BattleSimulationServer] 收到请求: {method} {path}");
            
            if (!MainManager.Instance.LoadFinished)
            {
                Debug.LogWarning("[BattleSimulationServer] MainManager 尚未加载完成，返回 503");
                SendResponse(context, 503, JsonConvert.SerializeObject(new 
                { 
                    Success = false, 
                    Message = "服务器尚未准备好，请稍后再试。" 
                }));
                return;
            }
            try
            {
                // POST /simulate - 提交战斗请求
                if (method == "POST" && path == "/simulate")
                {
                    Debug.Log("[BattleSimulationServer] 处理提交战斗请求...");
                    HandleSubmitBattle(context);
                }
                // GET /result/{uid} - 查询战斗结果
                else if (method == "GET" && path.StartsWith("/result/"))
                {
                    string uid = path.Substring("/result/".Length);
                    Debug.Log($"[BattleSimulationServer] 查询战斗结果: UID={uid}");
                    HandleQueryBattle(context, uid);
                }
                // GET /playerdata - 获取练度数据
                else if (method == "GET" && path == "/playerdata")
                {
                    Debug.Log("[BattleSimulationServer] 获取练度数据请求");
                    HandleGetPlayerData(context);
                }
                else
                {
                    Debug.LogWarning($"[BattleSimulationServer] 未知的请求路径: {method} {path}");
                    SendResponse(context, 404, JsonConvert.SerializeObject(new 
                    { 
                        Success = false, 
                        Message = "404!"
                    }));
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"[BattleSimulationServer] 请求处理异常: {ex.Message}\n{ex.StackTrace}");
                SendResponse(context, 500, JsonConvert.SerializeObject(new 
                { 
                    Success = false, 
                    Message = $"请求处理失败: {ex.Message}" 
                }));
            }
        }

        private void HandleSubmitBattle(HttpListenerContext context)
        {
            try
            {
                string requestBody;
                using (var reader = new StreamReader(context.Request.InputStream))
                {
                    requestBody = reader.ReadToEnd();
                }

                var battleRequest = JsonConvert.DeserializeObject<BattleSimulationRequest>(requestBody);
                if (debugMode)
                    battleRequest.IsSkipping = false;
                
                // 生成 UID
                string uid = GenerateUid();

                // 创建任务
                var task = new BattleTask
                {
                    Uid = uid,
                    Request = battleRequest,
                    Status = BattleStatus.Queued,
                    Result = null,
                    ErrorMessage = null
                };

                // 加入队列
                lock (queueLock)
                {
                    battleQueue.Enqueue(task);
                    battleTasks[uid] = task;
                    Debug.Log($"[BattleSimulationServer] 任务已加入队列，当前队列长度: {battleQueue.Count}");
                }

                // 更新 UI
#if UNITY_5_3_OR_NEWER
                UnityMainThreadDispatcher.Instance().Enqueue(() => {
                    try
                    {
                        if (conentText != null)
                        {
                            var player_name = battleRequest.PlayerList?.Select(u => u.GetUnitName()).ToList() ?? new List<string>();
                            var enemy_name = battleRequest.EnemyList?.Select(u => u.GetUnitName()).ToList() ?? new List<string>();
                            conentText.text += $"[{uid}] Queued: " +
                                              string.Join(", ", player_name) + " vs. " +
                                              string.Join(", ", enemy_name) + "\n";
                        }
                    }
                    catch { }
                });
#endif

                // 返回 UID
                var response = new BattleSubmitResponse
                {
                    Success = true,
                    Message = "战斗请求已加入队列",
                    Uid = uid
                };

                Debug.Log($"[BattleSimulationServer] ✓ 战斗请求已成功加入队列，UID: {uid}");
                SendResponse(context, 200, JsonConvert.SerializeObject(response));
            }
            catch (Exception ex)
            {
                Debug.LogError($"[BattleSimulationServer] ✗ 提交战斗失败: {ex.Message}\n{ex.StackTrace}");
                SendResponse(context, 500, JsonConvert.SerializeObject(new BattleSubmitResponse
                {
                    Success = false,
                    Message = $"提交战斗失败: {ex.Message}",
                    Uid = null
                }));
            }
        }

        private void HandleQueryBattle(HttpListenerContext context, string uid)
        {
            try
            {
                BattleTask task;
                lock (queueLock)
                {
                    if (!battleTasks.TryGetValue(uid, out task))
                    {
                        SendResponse(context, 404, JsonConvert.SerializeObject(new BattleQueryResponse
                        {
                            Success = false,
                            Message = "未找到该 UID 的战斗任务",
                            Status = BattleStatus.Failed
                        }));
                        return;
                    }
                }

                var response = new BattleQueryResponse
                {
                    Success = true,
                    Status = task.Status
                };

                switch (task.Status)
                {
                    case BattleStatus.Queued:
                        // 计算队列位置
                        int position = 0;
                        lock (queueLock)
                        {
                            position = battleQueue.ToList().FindIndex(t => t.Uid == uid) + 1;
                        }
                        response.Message = $"战斗在队列中，位置: {position}";
                        response.QueuePosition = position;
                        break;

                    case BattleStatus.Processing:
                        response.Message = "战斗正在进行中";
                        response.QueuePosition = 0;
                        break;

                    case BattleStatus.Completed:
                        response.Message = "战斗已完成";
                        response.Result = task.Result;
                        break;

                    case BattleStatus.Failed:
                        response.Message = $"战斗失败: {task.ErrorMessage}";
                        break;
                }

                SendResponse(context, 200, JsonConvert.SerializeObject(response));
            }
            catch (Exception ex)
            {
                SendResponse(context, 500, JsonConvert.SerializeObject(new BattleQueryResponse
                {
                    Success = false,
                    Message = $"查询失败: {ex.Message}",
                    Status = BattleStatus.Failed
                }));
            }
        }

        private void HandleGetPlayerData(HttpListenerContext context)
        {
            try
            {
                var guildManager = GuildManager.Instance;
                var playerSetting = MainManager.Instance.PlayerSetting;

                var response = new
                {
                    Success = true,
                    PlayerLevel = playerSetting.playerLevel,
                    PlayerRank = playerSetting.ProcessStr(),
                    PlayerUniqueEqLv = playerSetting.maxUniqueEqLv,
                    TalentLevel = guildManager.GetTalentLevelDescription(),
                    TalentSkill = guildManager.GetFinalTalentSkillNodeDescription(),
                    TeamSkill = guildManager.GetFinalTeamSkillNodeDescription(),
                    Message = "成功获取练度数据"
                };

                SendResponse(context, 200, JsonConvert.SerializeObject(response));
            }
            catch (Exception ex)
            {
                Debug.LogError($"[BattleSimulationServer] 获取练度数据失败: {ex.Message}\n{ex.StackTrace}");
                SendResponse(context, 500, JsonConvert.SerializeObject(new
                {
                    Success = false,
                    PlayerLevel = 0,
                    PlayerRank = (string)null,
                    PlayerUniqueEqLv = 0,
                    TalentLevel = (string)null,
                    TalentSkill = (string)null,
                    TeamSkill = (string)null,
                    Message = $"获取练度数据失败: {ex.Message}"
                }));
            }
        }

        private void ProcessQueue()
        {
            while (isRunning)
            {
                BattleTask task = null;

                // 从队列取出任务
                lock (queueLock)
                {
                    if (battleQueue.Count > 0 && !isProcessing)
                    {
                        task = battleQueue.Dequeue();
                        task.Status = BattleStatus.Processing;
                        isProcessing = true;
                        Debug.Log($"[BattleSimulationServer] 从队列取出任务: UID={task.Uid}，剩余队列: {battleQueue.Count}");
                    }
                }

                if (task != null)
                {
                    Debug.Log($"[BattleSimulationServer] 开始处理战斗: UID={task.Uid}");
                    ProcessBattle(task);
                    isProcessing = false;
                    Debug.Log($"[BattleSimulationServer] 战斗处理完成: UID={task.Uid}");
                }
                else
                {
                    Thread.Sleep(100);
                }
            }
            
            Debug.Log("[BattleSimulationServer] 队列处理线程已停止");
        }

        private void ProcessBattle(BattleTask task)
        {
            try
            {
                Debug.Log($"[BattleSimulationServer] ProcessBattle 开始: UID={task.Uid}");

                void OnException(Exception ex)
                {
                    Debug.LogError($"[BattleSimulationServer] StartByServer 失败: {ex.Message}\n{ex.StackTrace}");
                    task.Status = BattleStatus.Failed;
                    task.ErrorMessage = ex.Message;
                }

                void OnFinish()
                {
                    // 获取战斗结果
                    try
                    {
                        Debug.Log($"[BattleSimulationServer] 开始获取战斗结果...");

                        long totalDamage = 0;
                        int battleFrames = 0;
                        int currentSeed = 0;

                        var battleResult = BattleManager.Instance.battleResult;

                        totalDamage = GuildCalculator.Instance.GetTotalDamage();

                        battleFrames = BattleHeaderController.CurrentFrameCount;

                        currentSeed = MyGameCtrl.Instance.CurrentSeedForSave;
                        var playerList = MyGameCtrl.Instance.tempData.playerList;
                        var enemyList = MyGameCtrl.Instance.tempData.enemyList;

                        task.Result = new BattleSimulationResponse
                        {
                            PlayerList = playerList,
                            EnemyList = enemyList,
                            Success = true,
                            Message = "战斗模拟完成",
                            BattleFrames = battleFrames,
                            CurrentSeed = currentSeed,
                            TalentLevel = GuildManager.Instance.GetTalentLevelDescription(),
                            TalentSkill = GuildManager.Instance.GetFinalTalentSkillNodeDescription(),
                            TeamSkill = GuildManager.Instance.GetFinalTeamSkillNodeDescription(),
                            Result = battleResult
                        };

                        if (task.Request.IsGuildBattle && task.Request.IsSemanMode)
                        {
                            task.Result.SemanUBNum = BattleManager.Instance.semanubmanager.UBNums;
                        }

                        task.Status = BattleStatus.Completed;
                        Debug.Log($"[BattleSimulationServer] ✓ 战斗结果获取成功: Result={battleResult}, Damage={totalDamage}, Frames={battleFrames}, Seed={currentSeed}");

#if UNITY_5_3_OR_NEWER
                    UnityMainThreadDispatcher.Instance().Enqueue(() => {
                        try
                        {
                            if (conentText != null)
                            {
                                conentText.text += $"[{task.Uid}] Completed: Result={battleResult}, Damage={totalDamage}, Frames={battleFrames}\n";
                            }
                        }
                        catch { }
                    });
#endif
                    }
                    catch (Exception ex)
                    {
                        Debug.LogError($"[BattleSimulationServer] ✗ 获取战斗结果失败: {ex.Message}\n{ex.StackTrace}");
                        task.Status = BattleStatus.Failed;
                        task.ErrorMessage = $"获取战斗结果失败: {ex.Message}";

#if UNITY_5_3_OR_NEWER
                    UnityMainThreadDispatcher.Instance().Enqueue(() => {
                        try
                        {
                            if (conentText != null)
                            {
                                conentText.text += $"[{task.Uid}] Failed: {task.ErrorMessage}\n";
                            }
                        }
                        catch { }
                    });
#endif
                    }
                }

#if ENABLE_THREADING
                controller.DispatchTask(task.Request, OnFinish, OnException);
#else
                // 更新 UI
#if UNITY_5_3_OR_NEWER
                UnityMainThreadDispatcher.Instance().Enqueue(() => {
                    try
                    {
                        if (conentText != null)
                        {
                            conentText.text += $"[{task.Uid}] Processing...\n";
                        }
                    }
                    catch { }
                });
#endif

                // 按照 GAUBOptimization.cs 的方式：在主线程中调用 StartByServer，然后在后台线程等待
#if UNITY_5_3_OR_NEWER
                MyGameCtrl.Instance.isBattleFinish = false;
                UnityMainThreadDispatcher.Instance().Enqueue(() => {
                    try
                    {
                        MyGameCtrl.Instance.StartByServer(task.Request);
                    }
                    catch (Exception ex)
                    {
                        Debug.LogError($"[BattleSimulationServer] StartByServer 失败: {ex.Message}\n{ex.StackTrace}");
                        MyGameCtrl.Instance.isBattleFinish = true;
                        task.Status = BattleStatus.Failed;
                        task.ErrorMessage = ex.Message;
                    }
                });

                // 在后台线程中等待战斗完成
                while (!MyGameCtrl.Instance.isBattleFinish)
                {
                    Thread.Sleep(100);
                }
#else
                try
                {
                    BattleManager.Instance.enabled = false;
                    BattleManager.Instance.battleFinished = false;
                    BattleManager.Instance.battleFinishedProcessed = false;
                    MyGameCtrl.Instance.StartByServer(task.Request);
                    // MyGameCtrl.Instance.tempData.skipping = true;
                    // BattleManager.Instance.skipping = true;

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

                    while (!MyGameCtrl.Instance.isBattleFinish)
                    {
                        UnityEngine.CoroutineRunner.Update();
                    }
                }
                catch (Exception ex)
                {
                }

                if (!MyGameCtrl.Instance.isBattleFinish)
                {
                    BattleManager.Instance.CoroutineManager.ClearAll();
                    BattleManager.Instance.onEndFadeOut = (System.Action)null;
                    BattleManager.Instance.battleFinishedProcessed = true;
                    MyGameCtrl.Instance.OnBattleFinished(1);
                }

                UnityEngine.CoroutineRunner.pending.Clear();
                UnityEngine.CoroutineRunner.running.Clear();

#endif
                if (debugMode)
                    Debug.LogWarning($"[BattleSimulationServer] 战斗请求：{JsonConvert.SerializeObject(task.Request)}");

                // 如果已经标记为失败，直接返回
                if (task.Status == BattleStatus.Failed)
                {
                    var playerList = MyGameCtrl.Instance.tempData.playerList;
                    var enemyList = MyGameCtrl.Instance.tempData.enemyList;
                    var player_name = string.Join(", ", playerList?.Select(u => u.GetUnitName()) ?? new List<string>());
                    var enemy_name = string.Join(", ", enemyList?.Select(u => u.GetUnitName()) ?? new List<string>());
                    Debug.LogWarning($"[BattleSimulationServer] 战斗 {player_name} vs {enemy_name} 已标记为失败: {task.ErrorMessage}");
                    Debug.LogWarning($"[BattleSimulationServer] 战斗请求：{JsonConvert.SerializeObject(task.Request)}");
                    return;
                }

                OnFinish();
#endif
            }
            catch (Exception ex)
            {
                Debug.LogError($"[BattleSimulationServer] ✗ ProcessBattle 异常: {ex.Message}\n{ex.StackTrace}");
                task.Status = BattleStatus.Failed;
                task.ErrorMessage = $"处理战斗失败: {ex.Message}";
            }
        }

        private void SendResponse(HttpListenerContext context, int statusCode, string message)
        {
            try
            {
                byte[] buffer = Encoding.UTF8.GetBytes(message);
                context.Response.StatusCode = statusCode;
                context.Response.ContentType = "application/json; charset=utf-8";
                context.Response.ContentLength64 = buffer.Length;
                context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                context.Response.OutputStream.Write(buffer, 0, buffer.Length);
            }
            catch (Exception ex)
            {
                Debug.LogError($"发送响应失败: {ex.Message}");
            }
            finally
            {
                context.Response.OutputStream.Close();
            }
        }
    }
#endif
}
