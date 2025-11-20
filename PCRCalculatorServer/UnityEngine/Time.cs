using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnityEngine
{
    public class Time
    {
        internal static float deltaTime = 1 / 60f;
        internal static float timeScale;
        internal static float realtimeSinceStartup;
        internal static float unscaledDeltaTime;
    }
}
