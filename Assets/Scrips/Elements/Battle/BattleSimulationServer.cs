using System;
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

    public class BattleSimulationResponse
    {
        public List<PCRCaculator.UnitData> PlayerList { get; set; }
        public List<PCRCaculator.UnitData> EnemyList { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public long TotalDamage { get; set; }
        public float RemainingTime { get; set; }
        public int BattleFrames { get; set; }
        public int CurrentSeed { get; set; }
        public eBattleResult Result { get; set; }
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
    public class GuildBattleSettingData
    {
        public bool UseFixedRandomSeed { get; set; }
        public int RandomSeed { get; set; }
        public bool UseLogBarrierNew { get; set; }
        public int UBTryingCount { get; set; } = 30;
    }

    public class BattleSimulationRequest
    {
        public List<PCRCaculator.UnitData> PlayerList { get; set; }
        public List<PCRCaculator.UnitData> EnemyList { get; set; }
        // 如果要进行工会战，请提供 EnemyData 列表
        public List<PCRCaculator.EnemyData> GuildEnemyList { get; set; }
        public List<List<float>> UBExecTimeList { get; set; }
        public List<List<int>> SemanUBExecTimeList { get; set; }
        public GuildBattleSettingData Settings { get; set; } = new GuildBattleSettingData();
        public bool ToMaxUnitAttr { get; set; }
        public bool IsAutoMode { get; set; }
        public bool IsSetMode { get; set; }
        public bool IsSemanMode { get; set; }
        public bool IsGuildBattle { get; set; } = true;
    }

    public class BattleSimulationServer : MonoBehaviour
    {
        private HttpListener httpListener;
        private Thread listenerThread;
        private Thread queueProcessorThread;
        private bool isRunning = false;
        public int port = 8080;  // 改为 public 以支持命令行设置
        public Text conentText;
        public bool autoStart = false;  // 是否自动启动

        // 异步队列系统
        private Queue<BattleTask> battleQueue = new Queue<BattleTask>();
        private Dictionary<string, BattleTask> battleTasks = new Dictionary<string, BattleTask>();
        private object queueLock = new object();
        private bool isProcessing = false;

        void Awake()
        {
            // 检查命令行参数或环境变量
            CheckAutoStart();
        }

        void OnEnable()
        {
            if (autoStart)
            {
                StartServer();
            }
        }

        private void CheckAutoStart()
        {
            // 检查命令行参数
            string[] args = System.Environment.GetCommandLineArgs();
            if (args.Any(arg => arg.ToLower() == "-server" || arg.ToLower() == "--server"))
            {
                autoStart = true;
                Debug.Log("检测到 -server 参数，将自动启动服务器");
            }

            // 检查环境变量
            string envServer = System.Environment.GetEnvironmentVariable("PCR_SERVER_MODE");
            if (!string.IsNullOrEmpty(envServer) && envServer.ToLower() == "true")
            {
                autoStart = true;
                Debug.Log("检测到 PCR_SERVER_MODE 环境变量，将自动启动服务器");
            }

            // 检查端口设置
            string envPort = System.Environment.GetEnvironmentVariable("PCR_SERVER_PORT");
            if (!string.IsNullOrEmpty(envPort) && int.TryParse(envPort, out int envPortValue))
            {
                port = envPortValue;
                Debug.Log($"通过环境变量设置服务器端口: {envPortValue}");
            }
        }

        void OnDisable()
        {
            StopServer();
        }

        public void StartServer()
        {
            if (isRunning) return;

            try
            {
                httpListener = new HttpListener();
                httpListener.Prefixes.Add($"http://localhost:{port}/");
                httpListener.Start();
                
                isRunning = true;
                listenerThread = new Thread(ListenForRequests);
                listenerThread.Start();

                // 启动队列处理线程
                queueProcessorThread = new Thread(ProcessQueue);
                queueProcessorThread.Start();
                
                Debug.Log($"HTTP服务器已启动，监听端口: {port}");
            }
            catch (Exception ex)
            {
                Debug.LogError($"启动HTTP服务器失败: {ex.Message}");
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
            try
            {
                string path = context.Request.Url.AbsolutePath;
                string method = context.Request.HttpMethod;

                // POST /simulate - 提交战斗请求
                if (method == "POST" && path == "/simulate")
                {
                    HandleSubmitBattle(context);
                }
                // GET /result/{uid} - 查询战斗结果
                else if (method == "GET" && path.StartsWith("/result/"))
                {
                    string uid = path.Substring("/result/".Length);
                    HandleQueryBattle(context, uid);
                }
                else
                {
                    SendResponse(context, 404, JsonConvert.SerializeObject(new 
                    { 
                        Success = false, 
                        Message = "404!"
                    }));
                }
            }
            catch (Exception ex)
            {
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
                }

                // 更新 UI
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

                // 返回 UID
                var response = new BattleSubmitResponse
                {
                    Success = true,
                    Message = "战斗请求已加入队列",
                    Uid = uid
                };

                SendResponse(context, 200, JsonConvert.SerializeObject(response));
            }
            catch (Exception ex)
            {
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
                    }
                }

                if (task != null)
                {
                    ProcessBattle(task);
                    isProcessing = false;
                }
                else
                {
                    Thread.Sleep(100);
                }
            }
        }

        private void ProcessBattle(BattleTask task)
        {
            try
            {
                // 更新 UI
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

                // 按照 GAUBOptimization.cs 的方式：在主线程中调用 StartByServer，然后在后台线程等待
                MyGameCtrl.Instance.isBattleFinish = false;
                UnityMainThreadDispatcher.Instance().Enqueue(() => {
                    try
                    {
                        MyGameCtrl.Instance.StartByServer(task.Request);
                    }
                    catch (Exception ex)
                    {
                        Debug.LogError($"StartByServer 失败: {ex.Message}");
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

                // 如果已经标记为失败，直接返回
                if (task.Status == BattleStatus.Failed)
                {
                    return;
                }

                // 获取战斗结果
                try
                {
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
                        TotalDamage = totalDamage,
                        BattleFrames = battleFrames,
                        CurrentSeed = currentSeed,
                        Result = battleResult
                    };

                    task.Status = BattleStatus.Completed;

                    // 更新 UI 日志
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
                }
                catch (Exception ex)
                {
                    task.Status = BattleStatus.Failed;
                    task.ErrorMessage = $"获取战斗结果失败: {ex.Message}";
                    
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
                }
            }
            catch (Exception ex)
            {
                task.Status = BattleStatus.Failed;
                task.ErrorMessage = $"处理战斗失败: {ex.Message}";
                Debug.LogError($"ProcessBattle 异常: {ex.Message}");
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
}