using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnityEngine
{
    public class Application
    {
        internal static string streamingAssetsPath = "Data/StreamingAssets";
        internal static string _persistentDataPath;
        internal static RuntimePlatform platform = RuntimePlatform.WindowsEditor;
        internal static string dataPath = "Data";
        internal static string version = "1.0";
        internal static bool runInBackground = true;
        internal static int targetFrameRate = 60;
        internal static string companyName = "PCRFans";
        internal static string productName = "PCRGuildCalculator";

        /// <summary>
        /// 获取跨平台的持久数据路径
        /// </summary>
        public static string persistentDataPath
        {
            get
            {
                if (string.IsNullOrEmpty(_persistentDataPath))
                {
                    _persistentDataPath = GetPlatformPersistentDataPath();
                }
                return _persistentDataPath;
            }
            set { _persistentDataPath = value; }
        }

        /// <summary>
        /// 根据当前操作系统返回相应的持久数据路径
        /// </summary>
        private static string GetPlatformPersistentDataPath()
        {
            var osVersion = Environment.OSVersion.Platform;
            
            switch (osVersion)
            {
                case PlatformID.Win32NT:
                case PlatformID.Win32Windows:
                    // Windows: %APPDATA%\[CompanyName]\[ProductName]
                    return Path.Combine(
                        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "Low",
                        companyName,
                        productName);

                case PlatformID.Unix:
                    // Linux: ~/.local/share/[CompanyName]/[ProductName]
                    if (IsRunningOnMac())
                    {
                        // macOS: ~/Library/Application Support/[CompanyName]/[ProductName]
                        return Path.Combine(
                            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                            companyName,
                            productName);
                    }
                    else
                    {
                        // Linux
                        return Path.Combine(
                            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                            "unity3d",
                            companyName,
                            productName);
                    }

                case PlatformID.MacOSX:
                    // macOS: ~/Library/Application Support/[CompanyName]/[ProductName]
                    return Path.Combine(
                        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                        companyName,
                        productName);

                default:
                    // 默认路径
                    return Path.Combine(
                        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                        companyName,
                        productName);
            }
        }

        /// <summary>
        /// 检测是否运行在 macOS 上
        /// </summary>
        private static bool IsRunningOnMac()
        {
            string osDesc = System.Runtime.InteropServices.RuntimeInformation.OSDescription?.ToLower() ?? "";
            return osDesc.Contains("darwin") || osDesc.Contains("macos");
        }

        internal static void Quit()
        {
            Environment.Exit(0);
        }
    }
}
