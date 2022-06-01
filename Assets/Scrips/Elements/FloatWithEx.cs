using System;
using UnityEngine;
using Random = System.Random;

namespace Elements
{
    public class FloatWithEx : IComparable<FloatWithEx>, IEquatable<FloatWithEx>
    {
        private bool pure => min == max;
        private readonly float value, avg, min, max;
        private readonly Func<int, float> root;
        private int hash;
        private float cache;

        public FloatWithEx(float value = 0f, float avg = 0f, float min = 0f, float max = 0f,
            Func<int, float> root = null)
        {
            this.value = value;
            this.avg = avg;
            this.min = min;
            this.max = max;
            this.root = root;
        }

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
        
        private static Random rand = new Random();

        public static FloatWithEx Binomial(float p, bool val)
        {
            p = Mathf.Clamp(p, 0f, 1f);
            if (p == 0f) return 0f;
            if (p == 1f) return 1f;
            var result = new FloatWithEx(val ? 1 : 0, p, root: _ => rand.NextDouble() < p ? 1 : 0, min: 0,
                max: 1);
            return result;
        }


        public FloatWithEx Select(Func<float, float> selector)
        {
            if (pure) return selector(value);
            float a = selector(min), b = selector(max);
            return new FloatWithEx(selector(value), selector(avg), root: hash => selector(Emulate(hash)),
                min: Mathf.Min(a, b), max: Mathf.Max(a, b));
        }

        private static FloatWithEx Default = 0f;

        private static FloatWithEx Op(FloatWithEx a, FloatWithEx b,
            Func<float, float, float> op)
        {
            a = a ?? Default;
            b = b ?? Default;
            var vala = a.value;
            var valb = b.value;
            var xs = new[]
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
                return new FloatWithEx(op(vala, b.value), op(vala, b.avg),
                    root: hash => op(vala, b.Emulate(hash)), min: Mathf.Min(xs), max: Mathf.Max(xs));
            }

            if (b.pure)
                return new FloatWithEx(op(a.value, valb), op(a.avg, valb),
                    root: hash => op(a.Emulate(hash), valb), min: Mathf.Min(xs), max: Mathf.Max(xs));
            return new FloatWithEx(op(a.value, b.value), op(a.avg, b.avg),
                root: hash => op(a.Emulate(hash), b.Emulate(hash)), min: Mathf.Min(xs), max: Mathf.Max(xs));
        }

        public float Expect => avg;
        public float Expected(int N)
        {
            double s = 0;
            for (int i = 0; i < N; ++i)
                s += Emulate(rand.Next());
            return (float)(s / N);
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
                if (predict(Emulate(rand.Next()))) ++s;
            return (float)s / N;
        }

        public static implicit operator float(FloatWithEx self) => self.value;
        public static implicit operator FloatWithEx(float x) =>
            new FloatWithEx(x, x, x, x, null);
        public FloatWithEx Log() => Select(Mathf.Log);
        public FloatWithEx Max(float f) => Select(x => Mathf.Max(x, f));
        public FloatWithEx Min(float f) => Select(x => Mathf.Min(x, f));
        public override string ToString() => pure ? ((float)this).ToString() : $"{(int)(float)this}[{(int)Expect}]";
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

        public bool StrictlyEquals(float x)
        {
            return root == null && value == x;
        }

        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        public FloatWithEx ZeroCapForHp()
        {
            return new FloatWithEx(value >= 0f ? value : 0f, avg, min, max, root);
        }
    }
}
