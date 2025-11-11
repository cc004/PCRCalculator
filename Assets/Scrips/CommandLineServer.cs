using System;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using Elements.Battle;

namespace PCRCaculator
{
    /// <summary>
    /// 命令行服务器启动器
    /// 使用方式：
    /// Unity.exe -batchmode -nographics -executeMethod CommandLineServer.StartServer
    /// 或在启动时添加命令行参数: -server -port 8080
    /// </summary>
    public class CommandLineServer : MonoBehaviour
    {
        public static bool serverMode = false;
        private static int serverPort = 8080;
        private static string targetScene = "BattleScene";

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void OnRuntimeInitialize()
        {
            // 检查命令行参数
            ParseCommandLineArgs();

            if (serverMode)
            {
                Debug.Log("检测到服务器模式，准备启动战斗模拟服务器...");
                
                // 创建一个协调器对象来等待 MainManager 加载完成
                var coordinator = new GameObject("ServerCoordinator");
                coordinator.AddComponent<ServerLoadCoordinator>();
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

        private static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.name == targetScene)
            {
                Debug.Log($"场景 {targetScene} 已加载，正在启动服务器...");
                SceneManager.sceneLoaded -= OnSceneLoaded;
                
                // 延迟一帧确保场景完全初始化
                var starter = new GameObject("ServerStarter");
                var component = starter.AddComponent<DelayedServerStarter>();
            }
        }

        private static void StartServerInScene()
        {
            try
            {
                // 查找所有 Canvas，然后在其中查找 Server 子对象
                var allCanvases = GameObject.FindObjectsOfType<Canvas>();
                Debug.Log($"场景中找到 {allCanvases.Length} 个 Canvas");
                
                Transform serverObject = null;
                foreach (var canvas in allCanvases)
                {
                    var server = canvas.transform.Find("Server");
                    if (server != null)
                    {
                        serverObject = server;
                        Debug.Log($"在 Canvas '{canvas.name}' 中找到 Server 对象");
                        break;
                    }
                }

                if (serverObject == null)
                {
                    Debug.LogError("未在任何 Canvas 中找到名为 'Server' 的 GameObject");
                    return;
                }

                // 激活 server GameObject（会自动触发 OnEnable 启动服务器）
                serverObject.gameObject.SetActive(true);
                Debug.Log($"已激活 {serverObject.parent.name}/Server GameObject，服务器将在端口 {serverPort} 上启动");
            }
            catch (Exception ex)
            {
                Debug.LogError($"启动服务器失败: {ex.Message}\n{ex.StackTrace}");
            }
        }

        // 用于编辑器或 -executeMethod 调用
        public static void StartServer()
        {
            Debug.Log("通过 StartServer 方法启动服务器");
            serverMode = true;
            OnRuntimeInitialize();
        }

        // 服务器加载协调器 - 等待 MainManager 加载完成后再切换场景
        private class ServerLoadCoordinator : MonoBehaviour
        {
            private void Start()
            {
                StartCoroutine(WaitForMainManagerAndLoadScene());
            }

            private System.Collections.IEnumerator WaitForMainManagerAndLoadScene()
            {
                Debug.Log("[ServerLoadCoordinator] 等待 MainManager.Instance.LoadFinished...");
                
                // 等待 MainManager.Instance 存在且 LoadFinished 为 true
                while (MainManager.Instance == null || !MainManager.Instance.LoadFinished)
                {
                    yield return new UnityEngine.WaitForSeconds(0.1f);
                }
                
                Debug.Log("[ServerLoadCoordinator] MainManager 加载完成");
                
                // 如果不在目标场景，则加载场景
                if (SceneManager.GetActiveScene().name != targetScene)
                {
                    Debug.Log($"[ServerLoadCoordinator] 正在加载场景: {targetScene}");
                    SceneManager.sceneLoaded += OnSceneLoaded;
                    SceneManager.LoadScene(targetScene);
                }
                else
                {
                    Debug.Log("[ServerLoadCoordinator] 已在目标场景，直接启动服务器");
                    // 已经在目标场景，直接启动
                    StartServerInScene();
                    Destroy(gameObject);
                }
            }
        }

        // 延迟启动组件
        private class DelayedServerStarter : MonoBehaviour
        {
            private void Start()
            {
                StartServerInScene();
                // 启动后销毁自己
                Destroy(gameObject);
            }
        }
    }
}
