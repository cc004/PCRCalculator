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
        //只在windows打包时执行
        if (target == BuildTarget.StandaloneWindows64 || target == BuildTarget.StandaloneWindows) 
        {
            string sourceShortcutPath = "公共目录.lnk";
            string buildDir = Path.GetDirectoryName(pathToBuiltProject);
            string targetShortcutPath = Path.Combine(buildDir, "公共目录.lnk");
            if (File.Exists(sourceShortcutPath))
            {
                File.Copy(sourceShortcutPath, targetShortcutPath, true);
                Debug.Log($"✅ 快捷方式已生成: {sourceShortcutPath} -> {targetShortcutPath}");
            }
            else
            {
                Debug.LogError("源快捷方式未找到，请检查路径是否正确！");
            }
        }
    }
}
#endif