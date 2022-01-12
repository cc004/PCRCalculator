using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Elements
{
    public class SumFloatWithEx
    {
        private FloatWithEx @base;
        private FloatWithEx value;
        private Dictionary<int, FloatWithEx> adds;

        public static implicit operator FloatWithEx(SumFloatWithEx self)
        {
            return self.value;
        }

        public static implicit operator SumFloatWithEx(FloatWithEx x)
        {
            return new SumFloatWithEx()
            {
                @base = x,
                value = x,
                adds = new Dictionary<int, FloatWithEx>()
            };
        }

        public SumFloatWithEx Sum(int hash, FloatWithEx x)
        {
            if (adds.ContainsKey(hash))
            {
                adds.Remove(hash);

                if (x.notpure)
                    return adds.Aggregate<KeyValuePair<int, FloatWithEx>, SumFloatWithEx>(@base, 
                        (current, val) => current.Sum(val.Key, val.Value));
            }
            else
                adds.Add(hash, x);
            value += x;
            return this;
        }
    }

    public struct FloatWithEx : IComparable<FloatWithEx>, IEquatable<FloatWithEx>
    {

        internal bool notpure;
        private float value;
        private float ex;
        private Func<float, float> selector;
        private double[] distribution;
        private float min, max;
        private const int N = 100;
        private HashSet<FloatWithEx> sums;

        public FloatWithEx Copy()
        {
            var result = new FloatWithEx();
            result.notpure = notpure;
            result.value = value;
            result.selector = selector;
            result.ex = ex;
            if (distribution != null)
            {
                result.max = max;
                result.min = min;
                result.distribution = new double[N];
                Buffer.BlockCopy(distribution, 0, result.distribution, 0, N * sizeof(double));
            }
            return result;
        }

        public static FloatWithEx Binomial(float p, bool val)
        {
            p = Mathf.Clamp(p, 0f, 1f);
            if (p == 0f) return 0f;
            if (p == 1f) return 1f;
            var result = new FloatWithEx
            {
                value = val ? 1 : 0,
                ex = p,
                min = 0,
                max = 1,
                notpure = true,
                distribution = new double[N]
            };
            result.distribution[0] = 1 - p;
            result.distribution[N - 1] = p;
            return result;
        }
        
        public FloatWithEx Select(Func<float, float> selector)
        {
            if (!notpure)
                return (float)selector(value);
            var result = Copy();
            var last = result.selector;
            result.selector = result.selector == null ? selector : x => selector(last(x));
            if (result.selector(max) == result.selector(min))
                return (float) result;
            return result;
        }

        private static FloatWithEx Op(FloatWithEx a, FloatWithEx b,
            Func<float, float, float> op)
        {
            if (!a.notpure) return b.Select(x => op(a.value, x));
            if (!b.notpure) return a.Select(x => op(x, b.value));
            var result = new FloatWithEx();
            var sela = a.selector ?? (x => x);
            var selb = b.selector ?? (x => x);
            result.notpure = true;
            var edge = new float[]
            {
                op(sela(a.min), selb(b.min)),
                op(sela(a.min), selb(b.max)),
                op(sela(a.max), selb(b.min)),
                op(sela(a.max), selb(b.max))
            };
            result.min = Mathf.Min(edge);
            result.max = Mathf.Max(edge);
            result.value = op(sela(a.value), selb(b.value));
            result.ex = op(sela(a.ex), selb(b.ex));
            result.distribution = new double[N];
            for (int i = 0; i < N; ++i)
            for (int j = 0; j < N; ++j)
            {
                var vala = sela((a.max - a.min) * i / (N - 1) + a.min);
                var valb = selb((b.max - b.min) * j / (N - 1) + b.min);
                var index = Mathf.RoundToInt((op(vala, valb) - result.min) / (result.max - result.min) * (N - 1));
                if (index > N - 1 || index < 0)
                {

                }
                result.distribution[index] += a.distribution[i] * b.distribution[j];
            }

            var aa = result.distribution.Sum();
            return result;
        }
        
        public float Expect => (float)(selector?.Invoke(ex) ?? ex);
        public float Expected
        {
            get
            {
                var selector = this.selector ?? (x => x);
                if (!notpure) return (float)selector(value);
                var s = 0.0;
                for (var i = 0; i < N; ++i)
                {
                    var val = selector((max - min) * i / (N - 1) + min);
                    s += distribution[i] * val;
                }
                return (float)s;
            }
        }

        public float Stddev
        {
            get
            {
                var selector = this.selector ?? (x => x);
                if (!notpure) return 0f;
                var s = 0.0;
                for (var i = 0; i < N; ++i)
                {
                    var x = selector((max - min) * i / (N - 1) + min);
                    s += distribution[i] * x * x;
                }

                var avg = Expected;
                return Mathf.Sqrt((float)s - avg * avg);
            }
        }
        
        public static implicit operator float(FloatWithEx self) => (float)(self.selector == null ? self.value : self.selector(self.value));
        public static implicit operator FloatWithEx(float x) => new FloatWithEx { value = x, ex = x };
        public FloatWithEx Log() => Select(Mathf.Log);
        public FloatWithEx Max(float f) => Select(x => Mathf.Max(x, f));
        public FloatWithEx Min(float f) => Select(x => Mathf.Min(x, f));
        public override string ToString() => notpure ? $"{(int)(float)this}[{(int)Expected}]" : ((int)(float)this).ToString();
        public FloatWithEx Floor() => Select(Mathf.Floor);
        public int CompareTo(FloatWithEx other) => ((float)this).CompareTo(other);
        public static FloatWithEx operator +(FloatWithEx a, FloatWithEx b) => Op(a, b, (x, y) => x + y);
        public static FloatWithEx operator -(FloatWithEx a, FloatWithEx b) => Op(a, b, (x, y) => x - y);
        public static FloatWithEx operator *(FloatWithEx a, FloatWithEx b) => Op(a, b, (x, y) => x * y);
        public static FloatWithEx operator /(FloatWithEx a, FloatWithEx b) => Op(a, b, (x, y) => x / y);

        public bool Equals(FloatWithEx other)
        {
            return Mathf.Abs(value).Equals(Mathf.Abs(other.value));
        }

        public override bool Equals(object obj)
        {
            return obj is FloatWithEx other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Mathf.Abs(value).GetHashCode();
        }
    }
}
