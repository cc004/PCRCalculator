using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnityEngine
{
    public class AnimationCurve
    {
        public enum WeightedMode
        {
            None,
            In,
            Out,
            Both
        }

        public class Keyframe
        {
            public float time, value, inSlope, outSlope, inWeight, outWeight;
            public WeightedMode weightedMode;

        }
        public enum WrapMode
        {
            Once = 1,
            Loop = 2,
            PingPong = 4,
            Default = 0,
            ClampForever = 8,
            Clamp = 1
        }

        public Keyframe[] m_Curve;
        public WrapMode m_PreInfinity, m_PostInfinity;

        internal float Evaluate(float time)
        {
            if (m_PostInfinity != WrapMode.Loop || m_PostInfinity != WrapMode.Loop)
                throw new NotImplementedException();

            if (m_Curve == null || m_Curve.Length == 0)
                return 0;

            int n = m_Curve.Length;
            if (n == 1) return m_Curve[0].value;

            float startTime = m_Curve[0].time;
            float endTime = m_Curve[n - 1].time;
            float duration = endTime - startTime;

            // Loop WrapMode
            if (duration > 0)
            {
                while (time < startTime) time += duration;
                while (time > endTime) time -= duration;
            }

            // 找到 time 所在区间
            Keyframe k0 = m_Curve[0], k1 = m_Curve[n - 1];
            for (int i = 0; i < n - 1; i++)
            {
                if (time >= m_Curve[i].time && time <= m_Curve[i + 1].time)
                {
                    k0 = m_Curve[i];
                    k1 = m_Curve[i + 1];
                    break;
                }
            }

            if (k0.weightedMode != WeightedMode.None || k0.weightedMode != WeightedMode.None)
                throw new NotImplementedException();

            float t = (time - k0.time) / (k1.time - k0.time);

            // Hermite 插值
            float h00 = 2 * t * t * t - 3 * t * t + 1;
            float h10 = t * t * t - 2 * t * t + t;
            float h01 = -2 * t * t * t + 3 * t * t;
            float h11 = t * t * t - t * t;

            float dt = k1.time - k0.time;
            return h00 * k0.value + h10 * k0.outSlope * dt + h01 * k1.value + h11 * k1.inSlope * dt;
        }
    }
}
