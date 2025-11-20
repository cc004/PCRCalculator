using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnityEngine
{
    public class Application
    {
        internal static string streamingAssetsPath = "Data/StreamingAssets";
        internal static string persistentDataPath = "Data";
        internal static RuntimePlatform platform = RuntimePlatform.WindowsEditor;
        internal static string dataPath = "Data";
        internal static string version = "1.0";
        internal static bool runInBackground = true;
        internal static int targetFrameRate = 60;

        internal static void Quit()
        {
            Environment.Exit(0);
        }
    }
}
