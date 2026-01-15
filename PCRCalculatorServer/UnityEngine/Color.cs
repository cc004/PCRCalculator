using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnityEngine
{
    public struct Color
    {
        internal static Color white;
        internal static Color gray;
        internal static Color black;
        internal static Color yellow;
        internal static Color red;
        public float r;
        public float g;
        public float b;
        public float a;

        public Color()
        {
        }

        public Color(float v1, float v2, float v3)
        {
            this.r = v1;
            this.g = v2;
            this.b = v3;
        }

        public Color(float v1, float v2, float v3, float v4)
        {
            this.r = v1;
            this.g = v2;
            this.b = v3;
            this.a = v4;
        }

        internal static object Lerp(object color, Color white, float v)
        {
            throw new NotImplementedException();
        }

        public static implicit operator Color(Color32 v)
        {
            return new Color(v.a, v.b, v.c, v.d);
        }
    }
}
