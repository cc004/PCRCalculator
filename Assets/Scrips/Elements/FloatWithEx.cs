using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Elements
{
    public class FloatWithEx : IComparable<FloatWithEx>, IEquatable<FloatWithEx>
    {
        private bool pure => min == max;
        private float value, avg, min, max;
        private Func<int, float> root;
        private int hash;
        private float cache;

        public float Emulate(int hash)
        {
            if (root == null) return value;
            if (hash != this.hash)
            {
                cache = root(hash);
                this.hash = hash;
            }
            return cache;
        }
        
        private static System.Random rand = new System.Random();

        public static FloatWithEx Binomial(float p, bool val)
        {
            p = Mathf.Clamp(p, 0f, 1f);
            if (p == 0f) return 0f;
            if (p == 1f) return 1f;
            var result = new FloatWithEx
            {
                value = val ? 1 : 0,
                avg = p,
                root = _ => rand.NextDouble() < p ? 1 : 0,
                min = 0,
                max = 1
            };
            return result;
        }


        public FloatWithEx Select(Func<float, float> selector)
        {
            if (pure) return selector(value);
            float a = selector(min), b = selector(max);
            return new FloatWithEx
            {
                value = selector(value),
                avg = selector(avg),
                root = hash => selector(Emulate(hash)),
                min = Mathf.Min(a, b), max = Mathf.Max(a, b)
            };
        }

        private static FloatWithEx Default = 0f;

        private static FloatWithEx Op(FloatWithEx a, FloatWithEx b,
            Func<float, float, float> op)
        {
            a = a ?? Default;
            b = b ?? Default;
            var vala = a.value;
            var valb = b.value;
            var xs = new float[]
            {
                op(a.max, b.max),
                op(a.min, b.min),
                op(a.max, b.min),
                op(a.min, b.max)
            };
            if (a.pure)
            {
                if (b.pure)
                    return op(a.value, b.value);
                else
                    return new FloatWithEx
                    {
                        value = op(vala, b.value),
                        avg = op(vala, b.avg),
                        root = hash => op(vala, b.Emulate(hash)),
                        min = Mathf.Min(xs), max = Mathf.Max(xs)
                    };
            }
            else if (b.pure)
                return new FloatWithEx
                {
                    value = op(a.value, valb),
                    avg = op(a.avg, valb),
                    root = hash => op(a.Emulate(hash), valb),
                    min = Mathf.Min(xs),
                    max = Mathf.Max(xs)
                };
            else
                return new FloatWithEx
                {
                    value = op(a.value, b.value),
                    avg = op(a.avg, b.avg),
                    root = hash => op(a.Emulate(hash), b.Emulate(hash)),
                    min = Mathf.Min(xs),
                    max = Mathf.Max(xs)
                };
        }

        public float Expect => avg;
        public float Expected
        {
            get
            {
                const int N = 1000;
                double s = 0;
                for (int i = 0; i < N; ++i)
                    s += Emulate(rand.Next());
                return (float)(s / N);
            }
        }
        public float Stddev
        {
            get
            {
                const int N = 1000;
                double s = 0, s2 = 0;
                for (int i = 0; i < N; ++i)
                {
                    var x = Emulate(rand.Next());
                    s += x;
                    s2 += x * x;
                }
                return (float)Math.Sqrt((s2 - s * s / N) / N);
            }
        }
        public float Probability(Func<float, bool> predict, int N = 100)
        {
            int s = 0;
            for (int i = 0; i < N; ++i)
                if (predict((float)Emulate(rand.Next()))) ++s;
            return (float)s / N;
        }

        public static implicit operator float(FloatWithEx self) => self.value;
        public static implicit operator FloatWithEx(float x) => new FloatWithEx
        { 
            value = x, avg = x, min = x, max = x,
            root = null
        };
        public FloatWithEx Log() => Select(Mathf.Log);
        public FloatWithEx Max(float f) => Select(x => Mathf.Max(x, f));
        public FloatWithEx Min(float f) => Select(x => Mathf.Min(x, f));
        public override string ToString() => pure ? ((int)(float)this).ToString() : $"{(int)(float)this}[{(int)Expect}]";
        public FloatWithEx Floor() => Select(Mathf.Floor);
        public int CompareTo(FloatWithEx other) => ((float)this).CompareTo(other);
        public static FloatWithEx operator +(FloatWithEx a, FloatWithEx b) => Op(a, b, (x, y) => x + y);
        public static FloatWithEx operator -(FloatWithEx a, FloatWithEx b) => Op(a, b, (x, y) => x - y);
        public static FloatWithEx operator *(FloatWithEx a, FloatWithEx b) => Op(a, b, (x, y) => x * y);
        public static FloatWithEx operator /(FloatWithEx a, FloatWithEx b) => Op(a, b, (x, y) => x / y);

        public static bool operator ==(FloatWithEx a, FloatWithEx b) => a.Equals(b);
        public static bool operator !=(FloatWithEx a, FloatWithEx b) => !a.Equals(b);
        public bool Equals(FloatWithEx other)
        {
            return value.Equals(other.value);
        }

        public override bool Equals(object obj)
        {
            return obj is FloatWithEx other && Equals(other);
        }

        public override int GetHashCode()
        {
            return value.GetHashCode();
        }
    }
}
