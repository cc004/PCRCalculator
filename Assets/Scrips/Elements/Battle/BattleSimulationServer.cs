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
        private bool isRunning = false;
        private int port = 8080;
        private bool simulationInProgress = false;
        public Text conentText;

        void OnEnable()
        {
            StartServer();
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
                Debug.Log("HTTP服务器已停止");
            }
            catch (Exception ex)
            {
                Debug.LogError($"停止HTTP服务器失败: {ex.Message}");
            }
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
                if (context.Request.HttpMethod != "POST")
                {
                    SendResponse(context, 405, JsonConvert.SerializeObject(new BattleSimulationResponse 
                    { 
                        Success = false, 
                        Message = "仅支持POST请求" 
                    }));
                    return;
                }

                if (simulationInProgress)
                {
                    SendResponse(context, 429, JsonConvert.SerializeObject(new BattleSimulationResponse 
                    { 
                        Success = false, 
                        Message = "另一个战斗模拟正在进行中" 
                    }));
                    return;
                }

                string requestBody;
                using (var reader = new StreamReader(context.Request.InputStream))
                {
                    requestBody = reader.ReadToEnd();
                }

                var battleRequest = JsonConvert.DeserializeObject<BattleSimulationRequest>(requestBody);

                simulationInProgress = true;

                UnityMainThreadDispatcher.Instance().Enqueue(() => {
                    try
                    {
                        var player_name = battleRequest.PlayerList.Select(u => u.GetUnitName()).ToList();
                        var enemy_name = battleRequest.EnemyList.Select(u => u.GetUnitName()).ToList();
                        conentText.text += "Processing: " +
                                          string.Join(", ", player_name) + " vs. " +
                                          string.Join(", ", enemy_name) + "\n";
                    }
                    catch { }
                });

                // 按照 GAUBOptimization.cs 的方式：在主线程中调用 StartByServer，然后在后台线程等待
                MyGameCtrl.Instance.isBattleFinish = false;
                UnityMainThreadDispatcher.Instance().Enqueue(() => {
                    try
                    {
                        MyGameCtrl.Instance.StartByServer(battleRequest);
                    }
                    catch (Exception ex)
                    {
                        Debug.LogError($"StartByServer 失败: {ex.Message}");
                        MyGameCtrl.Instance.isBattleFinish = true;
                    }
                });

                // 在后台线程中等待战斗完成
                while (!MyGameCtrl.Instance.isBattleFinish)
                {
                    Thread.Sleep(100);
                }

                // 获取战斗结果
                try
                {
                    long totalDamage = 0;
                    int battleFrames = 0;
                    int currentSeed = 0;
                    eBattleResult battleResult = eBattleResult.LOSE;

                    battleResult = BattleManager.Instance.battleResult;
                    totalDamage = GuildCalculator.Instance.GetTotalDamage();
                    battleFrames = BattleHeaderController.CurrentFrameCount;
                    currentSeed = MyGameCtrl.Instance.CurrentSeedForSave;

                    var result = new BattleSimulationResponse
                    {
                        PlayerList = MyGameCtrl.Instance.tempData.playerList,
                        EnemyList = MyGameCtrl.Instance.tempData.enemyList,
                        Success = true,
                        Message = "战斗模拟完成",
                        TotalDamage = totalDamage,
                        BattleFrames = battleFrames,
                        CurrentSeed = currentSeed,
                        Result = battleResult
                    };

                    var resultJson = JsonConvert.SerializeObject(result);
                    
                    // 更新 UI 日志（在主线程）
                    UnityMainThreadDispatcher.Instance().Enqueue(() => {
                        try
                        {
                            conentText.text += $"Battle Result: {battleResult}, Total Damage: {totalDamage}, Frames: {battleFrames}, Seed: {currentSeed}\n";
                        }
                        catch { }
                    });

                    SendResponse(context, 200, resultJson);
                }
                catch (Exception ex)
                {
                    SendResponse(context, 500, JsonConvert.SerializeObject(new BattleSimulationResponse 
                    { 
                        Success = false, 
                        Message = $"获取战斗结果失败: {ex.Message}" 
                    }));
                }
                finally
                {
                    simulationInProgress = false;
                }
            }
            catch (Exception ex)
            {
                simulationInProgress = false;
                SendResponse(context, 500, JsonConvert.SerializeObject(new BattleSimulationResponse 
                { 
                    Success = false, 
                    Message = $"请求处理失败: {ex.Message}" 
                }));
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