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

                if (!x.pure)
                    return adds.Aggregate<KeyValuePair<int, FloatWithEx>, SumFloatWithEx>(@base, 
                        (current, val) => current.Sum(val.Key, val.Value));
            }
            else
                adds.Add(hash, x);
            value += x;
            return this;
        }
    }

    public class FloatWithEx : IComparable<FloatWithEx>, IEquatable<FloatWithEx>
    {
        internal bool pure => avg2 == avg * avg;
        private float value, avg;
        private double avg2;
        private Func<float> root;
        
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
                avg2 = p,
                root = () => rand.NextDouble() < p ? 1 : 0
            };
            return result;
        }


        public FloatWithEx Select(Func<float, float> selector)
        {
            if (pure) return selector(value);
            double x = selector((float)Math.Sqrt(avg2));
            return new FloatWithEx
            {
                value = selector(value),
                avg = selector(avg),
                avg2 = x * x,
                root = () => selector(root())
            };
        }

        private static FloatWithEx Default = 0f;

        private static FloatWithEx Op(FloatWithEx a, FloatWithEx b,
            Func<float, float, float> op,
            Func<FloatWithEx, FloatWithEx, double> op2)
        {
            a = a ?? Default;
            b = b ?? Default;
            var vala = a.value;
            var valb = b.value;

            if (a.pure)
            {
                if (b.pure)
                    return op(a.value, b.value);
                else
                    return new FloatWithEx
                    {
                        value = op(vala, b.value),
                        avg = op(vala, b.avg),
                        avg2 = op2(a, b),
                        root = () => op(vala, b.root())
                    };
            }
            else if (b.pure)
                return new FloatWithEx
                {
                    value = op(a.value, valb),
                    avg = op(a.avg, valb),
                    avg2 = op2(a, b),
                    root = () => op(a.root(), valb)
                };
            else
                return new FloatWithEx
                {
                    value = op(a.value, b.value),
                    avg = op(a.avg, b.avg),
                    avg2 = op2(a, b),
                    root = () => (op(a.root(), b.root()))
                };
        }

        public float Expect => avg;
        private float expectedCache = float.NaN;
        public float Expected
        {
            get
            {
                return avg;
            }
        }
        private static double Rand(double u, double d)
        {
            double u1, u2, z, x;
            if (d <= 0)
            {

                return u;
            }
            u1 = rand.NextDouble();
            u2 = rand.NextDouble();

            z = Math.Sqrt(-2 * Math.Log(u1)) * Math.Sin(2 * Math.PI * u2);

            x = u + d * z;
            return x;

        }
        public float Probability(Func<float, bool> predict)
        {
            const int N = 1000;
            int s = 0;
            for (int i = 0; i < N; ++i)
                if (predict((float)Rand(avg, Stddev))) ++s;
            return (float)s / N;
        }

        public double Stddev => Math.Sqrt(Math.Max(0, avg2 - avg * avg));

        public static implicit operator float(FloatWithEx self) => self.value;
        public static implicit operator FloatWithEx(float x) => new FloatWithEx
        { 
            value = x, avg = x,
            root = () => x, avg2 = x * x
        };
        public FloatWithEx Log() => Select(Mathf.Log);
        public FloatWithEx Max(float f) => Select(x => Mathf.Max(x, f));
        public FloatWithEx Min(float f) => Select(x => Mathf.Min(x, f));
        public override string ToString() => pure ? ((int)(float)this).ToString() : $"{(int)(float)this}[{(int)Expect}±{(int)Stddev}]";
        public FloatWithEx Floor() => Select(Mathf.Floor);
        public int CompareTo(FloatWithEx other) => ((float)this).CompareTo(other);
        public static FloatWithEx operator +(FloatWithEx a, FloatWithEx b) => Op(a, b, (x, y) => x + y, (x, y) => x.avg2 + y.avg2 + 2 * x.avg * y.avg);
        public static FloatWithEx operator -(FloatWithEx a, FloatWithEx b) => Op(a, b, (x, y) => x - y, (x, y) => x.avg2 + y.avg2 - 2 * x.avg * y.avg);
        public static FloatWithEx operator *(FloatWithEx a, FloatWithEx b) => Op(a, b, (x, y) => x * y, (x, y) => x.avg2 * y.avg2);
        public static FloatWithEx operator /(FloatWithEx a, FloatWithEx b) => Op(a, b, (x, y) => x / y, (x, y) => x.avg2 / y.avg2);

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
