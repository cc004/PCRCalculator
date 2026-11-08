using PCRCaculator;
using UnityEditor;

[InitializeOnLoad]
internal static class OfflineServerEditorLifecycle
{
    static OfflineServerEditorLifecycle()
    {
        AssemblyReloadEvents.beforeAssemblyReload += Shutdown;
        EditorApplication.quitting += Shutdown;
        EditorApplication.playModeStateChanged += state =>
        {
            if (state == PlayModeStateChange.ExitingPlayMode)
                Shutdown();
        };
    }

    private static void Shutdown()
    {
        foreach (var panel in UnityEngine.Object.FindObjectsOfType<Elements.Battle.GAUBOptimization>(true))
            panel.StopGA();
        PCRCaculator.Guild.GuildManager.CancelRemoteCalculation();
        OfflineServerManager.Shutdown();
    }
}
