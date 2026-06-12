// Assets/Editor/AutoCreateLnk.cs
#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System.IO;
using UnityEditor.Callbacks;

public class AutoCreateLnk
{
    [PostProcessBuild(999)]
    public static void OnPostprocessBuild(BuildTarget target, string pathToBuiltProject)
    {
        // Run for desktop players so each package has the expected writable folder.
        if (target == BuildTarget.StandaloneWindows64
            || target == BuildTarget.StandaloneWindows
            || target == BuildTarget.StandaloneOSX
            || target == BuildTarget.StandaloneLinux64)
        {
            string buildDir = Path.GetDirectoryName(pathToBuiltProject);
            string saveDir = Path.Combine(buildDir, "save");

            // 创建save文件夹
            if (!Directory.Exists(saveDir))
            {
                Directory.CreateDirectory(saveDir);
                Debug.Log($"✅ save文件夹已创建: {saveDir}");
            }
            else
            {
                Debug.Log($"ℹ️ save文件夹已存在: {saveDir}");
            }

            // 复制快捷方式（仅限Windows平台）
            if (target == BuildTarget.StandaloneWindows64 || target == BuildTarget.StandaloneWindows)
            {
                string sourceShortcutPath = "公共目录.lnk";
                string targetShortcutPath = Path.Combine(buildDir, "公共目录.lnk");
                if (File.Exists(sourceShortcutPath))
                {
                    File.Copy(sourceShortcutPath, targetShortcutPath, true);
                    Debug.Log($"✅ 快捷方式已生成: {sourceShortcutPath} -> {targetShortcutPath}");
                }
                else
                {
                    Debug.LogWarning("⚠️ 源快捷方式未找到，跳过复制: " + sourceShortcutPath);
                }
            }
            else if (target == BuildTarget.StandaloneOSX || target == BuildTarget.StandaloneLinux64)
            {
                Debug.Log("ℹ️ 非 Windows 桌面平台，跳过快捷方式创建");
            }
        }
    }
}
#endif
