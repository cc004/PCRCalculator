using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using BepInEx.Unity.IL2CPP;

namespace PcrApiDump
{
    /// <summary>
    /// PcrApiDump —— 把游戏走网络层的每一次 API 请求/响应以**明文**写入 jsonl 文件。
    ///
    /// 原理：游戏（PCR）的请求体构造与响应解包都在 Cute.NetworkTask 上完成：
    ///   请求：参数对象(Cute.PostParams 子类) → 明文 JSON(CreateBodyJson/PostJsonData) → Coneshell 加密 → body
    ///   响应：密文 → Coneshell 解密 → 明文 JSON(UnpackedJson) → LitJson.JsonData(ResponseData) → 各 Task 的 Parse()
    /// 所以只要把上面几个"明文经过点"用 Harmony 拦下来读出字符串即可，**完全不参与加解密的实现**，
    /// 既不需要破解 Coneshell，也不会改动任何游戏数据。
    ///
    /// 输出：默认 &lt;BepInEx根&gt;\PcrApiDump\api-YYYYMMDD-HHmmss.jsonl，一行一条事件：
    ///   {"ts":"...","t":1234,"dir":"req","src":"set_CreateBodyJson","task":"Elements.HomeIndexTask",
    ///    "api":"HomeIndex","url":"https://...","path":"/tool/sdk_check","code":0,"body":{ ... 明文 JSON ... }}
    /// </summary>
    [BepInPlugin(Guid, Name, Version)]
    [BepInProcess("PrincessConnectReDive.exe")]
    public class Plugin : BasePlugin
    {
        public const string Guid = "com.analysis.pcrapidump";
        public const string Name = "PcrApiDump";
        public const string Version = "1.1.0";

        internal static ManualLogSource Logger;

        internal static ConfigEntry<bool> Enabled;
        internal static ConfigEntry<string> OutputDirectory;
        internal static ConfigEntry<bool> MaskSensitive;
        internal static ConfigEntry<bool> IncludeHeaders;
        internal static ConfigEntry<bool> CaptureRawConeshell;
        internal static ConfigEntry<bool> CaptureConeshellState;
        internal static ConfigEntry<bool> CaptureParamsFallback;
        internal static ConfigEntry<bool> PatchDerivedParse;
        internal static ConfigEntry<int> MaxFileSizeMB;
        internal static ConfigEntry<int> FlushIntervalSeconds;
        internal static ConfigEntry<bool> DumpMethods;
        internal static ConfigEntry<bool> DumpMethodsOnly;
        internal static ConfigEntry<bool> DumpFields;
        public override void Load()
        {
            Logger = base.Log;

            Enabled = Config.Bind("General", "Enabled", true,
                "总开关：是否 hook 游戏网络层并把明文进出参写入文件");
            OutputDirectory = Config.Bind("General", "OutputDirectory", DefaultOutputDir(),
                "输出目录。留空或不存在则用 <BepInEx根>\\PcrApiDump");
            MaskSensitive = Config.Bind("General", "MaskSensitive", false,
                "true = 把 dmm_onetime_token / udid / onetime_token 的值替换成 ***（默认保留明文）");
            IncludeHeaders = Config.Bind("General", "IncludeHeaders", true,
                "请求条目附带 HTTP headers 快照（含 udid/session 等，注意保密）");
            CaptureRawConeshell = Config.Bind("General", "CaptureRawConeshell", true,
                "额外 hook Coneshell.Network.Pack/Unpack：捕获没能关联到具体 Task 的裸明文（兜底，标记 src=coneshell）");
            CaptureConeshellState = Config.Bind("General", "CaptureConeshellState", true,
                "取证模式：另外记录 Coneshell.Network 的 InitializeContext 入参、KeepPackNonce、Pack/Unpack 的明文↔密文成对（写到 coneshell-*.jsonl）。含密钥材料候选，注意保密");
            CaptureParamsFallback = Config.Bind("General", "CaptureParamsFallback", true,
                "明文 JSON 取不到时，退而序列化 Cute.PostParams 参数对象（标记 src=params）");
            PatchDerivedParse = Config.Bind("General", "PatchDerivedParse", false,
                "额外给每个 Task 子类覆写的 Parse() 打补丁（约 900 处，启动会慢几秒）。响应侧主通道是 set_UnpackedJson，一般不需要开");
            MaxFileSizeMB = Config.Bind("General", "MaxFileSizeMB", 64,
                "单文件大小上限（MB），超过自动切新文件");
            FlushIntervalSeconds = Config.Bind("General", "FlushIntervalSeconds", 2,
                "落盘间隔（秒）。0 = 每条立即 flush（更稳但更慢）");
            DumpMethods = Config.Bind("General", "DumpMethods", true,
                "启动后导出 il2cpp 的 methods.csv（方法→RVA）并把内存里解包后的 GameAssembly.dll 镜像 dump 成 gameassembly.bin，供 Ghidra 反编译。只读，不 hook 任何东西");

            DumpMethodsOnly = Config.Bind("General", "DumpMethodsOnly", false,
                "只做 methods.csv + gameassembly.bin 的 dump，完全不安装任何 hook（不碰 Coneshell / 不碰 NetworkTask.Parse）。反编译取代码时建议开这个，避免与其它插件或反调试机制冲突");

            DumpFields = Config.Bind("General", "DumpFields", true,
                "启动后导出 dump.cs（类/字段+偏移/方法+签名+RVA）与 classes.csv / fields.csv / methods_sig.csv。这是 Il2CppDumper 的等价物——本游戏 metadata header 被抹，Dumper 跑不了，只能在进程内经 il2cpp API 取。只读");

            try { Config.Save(); } catch { }

            if (!Enabled.Value)
            {
                Logger.LogInfo("[PcrApiDump] disabled by config");
                return;
            }

            if (DumpMethodsOnly.Value)
            {
                Logger.LogInfo("[PcrApiDump] DumpMethodsOnly=true：跳过所有 hook，只做方法表 + 镜像 dump");
                try { MethodDump.OnceAfterStart(OutputDirectory.Value); }
                catch (Exception ex) { Logger.LogWarning("[PcrApiDump] MethodDump 启动失败: " + ex.Message); }
                if (DumpFields.Value)
                {
                    try { FieldDump.OnceAfterStart(OutputDirectory.Value); }
                    catch (Exception ex) { Logger.LogWarning("[PcrApiDump] FieldDump 启动失败: " + ex.Message); }
                }
                return;
            }

            DumpWriter.Init(OutputDirectory.Value, MaxFileSizeMB.Value, FlushIntervalSeconds.Value, MaskSensitive.Value);
            Logger.LogInfo("[PcrApiDump] output dir: " + DumpWriter.CurrentFilePath);
            StateDump.Init(OutputDirectory.Value, CaptureConeshellState != null && CaptureConeshellState.Value);
            if (StateDump.CurrentFilePath != null)
                Logger.LogInfo("[PcrApiDump] coneshell state: " + StateDump.CurrentFilePath);
            try { AddComponent<HookInstaller>(); } catch (Exception ex) { Logger.LogWarning("[PcrApiDump] AddComponent 失败: " + ex.Message); }

            // 先试一次；interop 还没加载的话由 HookInstaller 每 0.5s 重试
            Hooks.Install();
            if (!Hooks.Installed) Logger.LogInfo("[PcrApiDump] 等待 interop 程序集加载后自动重试 hook 安装");
            if (DumpMethods.Value)
            {
                try { MethodDump.OnceAfterStart(OutputDirectory.Value); }
                catch (Exception ex) { Logger.LogWarning("[PcrApiDump] MethodDump 启动失败: " + ex.Message); }
            }
            if (DumpFields.Value)
            {
                try { FieldDump.OnceAfterStart(OutputDirectory.Value); }
                catch (Exception ex) { Logger.LogWarning("[PcrApiDump] FieldDump 启动失败: " + ex.Message); }
            }
        }

        public override bool Unload()
        {
            try { Hooks.Uninstall(); } catch { }
            try { DumpWriter.Shutdown(); } catch { }
            try { StateDump.Shutdown(); } catch { }
            return true;
        }

        private static string DefaultOutputDir()
        {
            try { return Path.Combine(Paths.BepInExRootPath, "PcrApiDump"); }
            catch { return "PcrApiDump"; }
        }
    }
}
