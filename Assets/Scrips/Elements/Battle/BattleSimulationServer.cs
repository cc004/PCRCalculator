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
        public bool Success { get; set; }
        public string Message { get; set; }
        public int TotalDamage { get; set; }
        public float RemainingTime { get; set; }
        public int BattleFrames { get; set; }
        public int CurrentSeed { get; set; }
        public BattleResult Result { get; set; }
    }

    [Serializable]
    public class GuildBattleSettingData
    {
        public bool UseFixedRandomSeed { get; set; }
        public int RandomSeed { get; set; }
        public float CalSpeed { get; set; } = 1.0f;
        public bool UseLogBarrierNew { get; set; }
        public bool IsViolent { get; set; }
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
        public bool IsAutoMode { get; set; }
        public bool IsSetMode { get; set; }
        public bool IsSemanMode { get; set; }
        public int StopFrame { get; set; } = -1;
        public bool IsGuildBattle { get; set; } = true;
    }

    public class BattleSimulationServer : MonoBehaviour
    {
        private HttpListener httpListener;
        private Thread listenerThread;
        private bool isRunning = false;
        private int port = 8080;
        private bool simulationInProgress = false;
        private BattleSimulationResponse currentResult = null;

        void Start()
        {
            StartServer();
        }

        void OnDestroy()
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
                
                // 在主线程中执行战斗模拟
                UnityMainThreadDispatcher.Instance().Enqueue(() => {
                    try
                    {
                        simulationInProgress = true;
                        currentResult = null;

                        // 直接使用 MyGameCtrl 提供的 StartByServer，不依赖 MainManager
                        MyGameCtrl.Instance.StartByServer(battleRequest);

                        // 添加战斗结果监听
                        StartCoroutine(WaitForBattleResult(context));
                    }
                    catch (Exception ex)
                    {
                        simulationInProgress = false;
                        SendResponse(context, 500, JsonConvert.SerializeObject(new BattleSimulationResponse 
                        { 
                            Success = false, 
                            Message = $"战斗模拟失败: {ex.Message}" 
                        }));
                    }
                });
            }
            catch (Exception ex)
            {
                SendResponse(context, 500, JsonConvert.SerializeObject(new BattleSimulationResponse 
                { 
                    Success = false, 
                    Message = $"请求处理失败: {ex.Message}" 
                }));
            }
        }

        private System.Collections.IEnumerator WaitForBattleResult(HttpListenerContext context)
        {
            // 等待战斗完成
            while (GuildCalculator.Instance == null || !GuildCalculator.Instance.isFinishCalc)
            {
                yield return new WaitForSeconds(0.1f);
            }

            // 等待一帧以确保所有数据都已更新
            yield return new WaitForEndOfFrame();

            try
            {
                var calculator = GuildCalculator.Instance;
                var eResult = calculator.BattleResult; // eBattleResult

                // map to our BattleResult
                BattleResult mappedResult = BattleResult.TimeOver;
                switch (eResult)
                {
                    case Elements.eBattleResult.WIN:
                        mappedResult = BattleResult.Victory;
                        break;
                    case Elements.eBattleResult.LOSE:
                        mappedResult = BattleResult.Lose;
                        break;
                    case Elements.eBattleResult.TIME_OVER:
                        mappedResult = BattleResult.TimeOver;
                        break;
                    default:
                        mappedResult = BattleResult.TimeOver;
                        break;
                }

                var remainingSeconds = MyGameCtrl.Instance.tempData.SettingData.limitTime * 60 - BattleHeaderController.CurrentFrameCount;

                var result = new BattleSimulationResponse
                {
                    Success = true,
                    Message = "战斗模拟完成",
                    TotalDamage = (int)calculator.GetTotalDamage(),
                    BattleFrames = BattleHeaderController.CurrentFrameCount,
                    RemainingTime = remainingSeconds,
                    CurrentSeed = MyGameCtrl.Instance.CurrentSeedForSave,
                    Result = mappedResult
                };

                SendResponse(context, 200, JsonConvert.SerializeObject(result));
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