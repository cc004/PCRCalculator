using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnityEngine
{
    internal class Input
    {
        internal static object mousePosition;

        internal static bool GetKey(KeyCode downArrow)
        {
            return false;
        }

        internal static bool GetKeyDown(KeyCode s)
        {
            return false;
        }

        internal static bool GetKeyUp(KeyCode key)
        {
            return false;
        }

        internal static bool GetMouseButtonDown(int v)
        {
            return false;
        }
    }
}
