using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using PCRCaculator.Guild;
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
        private string select, op, rnd;
        private FloatWithEx op1, op2;

        private FloatWithEx(float value = 0f, float avg = 0f, float min = 0f, float max = 0f,
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

        private static Random rndd = new Random();
        private static UnityRandom rand = new UnityRandom();
        private static List<int> cachedSeed = new List<int>();
        public static void SetState(int seed)
        {
            rand.InitState(seed);
            cachedSeed.Clear();
        }

        private static int GetRandN(int n)
        {
            while (cachedSeed.Count <= n) cachedSeed.Add(rand.Range(0, 1000));
            return cachedSeed[n];
        }
        public static FloatWithEx Binomial(float p, bool val, string rnd, int order)
        {
            p = Mathf.Clamp(p, 0f, 1f);
            if (p == 0f) return 0f;
            if (p == 1f) return 1f;
            var p0 = (int) (p * 1000);
            var result = new FloatWithEx(val ? 1 : 0, p, root: _ => GetRandN(order) < p0 ? 1 : 0, min: 0,
                max: 1)
            {
                rnd = rnd
            };
            return result;
        }


        public FloatWithEx Select(Func<float, float> selector, string select)
        {
            if (pure) return selector(value);
            float a = selector(min), b = selector(max);
            return new FloatWithEx(selector(value), selector(avg), root: hash => selector(Emulate(hash)),
                min: Mathf.Min(a, b), max: Mathf.Max(a, b))
            {
                select = select, op1 = this
            };
        }

        private static FloatWithEx Default = 0f;

        private static FloatWithEx Op(FloatWithEx a, FloatWithEx b,
            Func<float, float, float> op, string ops)
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
                    root: hash => op(vala, b.Emulate(hash)), min: Mathf.Min(xs), max: Mathf.Max(xs))
                {
                    op = ops, op1 = vala, op2 = b
                };
            }

            if (b.pure)
                return new FloatWithEx(op(a.value, valb), op(a.avg, valb),
                    root: hash => op(a.Emulate(hash), valb), min: Mathf.Min(xs), max: Mathf.Max(xs))
                {
                    op = ops,
                    op1 = a,
                    op2 = valb
                }; ;
            return new FloatWithEx(op(a.value, b.value), op(a.avg, b.avg),
                root: hash => op(a.Emulate(hash), b.Emulate(hash)), min: Mathf.Min(xs), max: Mathf.Max(xs))
            {
                op = ops,
                op1 = a,
                op2 = b
            }; ;
        }

        public float Expect => avg;
        public float Expected(int N)
        {
            double s = 0;
            for (int i = 0; i < N; ++i)
            {
                SetState(rndd.Next());
                s += Emulate(rndd.Next());
            }
            return (float)(s / N);
        }
        public float Stddev
        {
            get
            {
                var N = GuildManager.StaticsettingData.n1;
                double s = 0, s2 = 0;
                for (int i = 0; i < N; ++i)
                {
                    SetState(rndd.Next());
                    double x = Emulate(rndd.Next());
                    s += x;
                    s2 += x * x;
                }
                return (float)Math.Sqrt(Math.Max(0, (s2 - s * s / N) / N));
            }
        }
        public float Probability(Func<float, bool> predict)
        {
            var N = GuildManager.StaticsettingData.n1;
            int s = 0;
            for (int i = 0; i < N; ++i)
            {
                SetState(rndd.Next());
                if (predict(Emulate(rndd.Next()))) ++s;
            }
            return (float)s / N;
        }

        public static implicit operator float(FloatWithEx self) => self.value;
        public static implicit operator FloatWithEx(float x) =>
            new FloatWithEx(x, x, x, x, null);
        public FloatWithEx Max(float f)
        {
            if (min > f) return this;
            return Select(x => Mathf.Max(x, f), $"max:{f}");
        }

        public FloatWithEx Min(float f)
        {
            if (max < f) return this;
            return Select(x => Mathf.Min(x, f), $"min:{f}");
        }

        public override string ToString() => pure ? ((float)this).ToString() : $"{(int)(float)this}[{(int)Expect}]";
        public FloatWithEx Floor() => Select(Mathf.Floor, "floor");
        public FloatWithEx Ceil() => Select(Mathf.Ceil, "ceil");
        public FloatWithEx Abs() => Select(Mathf.Abs, "abs");
        public int CompareTo(FloatWithEx other) => ((float)this).CompareTo(other);
        public static FloatWithEx operator +(FloatWithEx a, FloatWithEx b) => Op(a, b, (x, y) => x + y, "add");
        public static FloatWithEx operator -(FloatWithEx a, FloatWithEx b) => Op(a, b, (x, y) => x - y, "sub");
        public static FloatWithEx operator *(FloatWithEx a, FloatWithEx b)
        {
            if (b.StrictlyEquals(1f)) return a;
            if (a.StrictlyEquals(1f)) return b;
            return Op(a, b, (x, y) => x * y, "mul");
        }

        public static FloatWithEx operator /(FloatWithEx a, FloatWithEx b) => Op(a, b, (x, y) => x / y, "div");

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
            return pure && value == x;
        }

        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        public FloatWithEx ZeroCapForHp()
        {
            return new FloatWithEx(value >= 0f ? value : 0f, avg, min, max, root)
            {
                op = op, op1 = op1, op2 = op2, rnd = rnd, select = select, id = id
            };
        }
        
        private int id;

        public string ToExpression(int hash)
        {
#if UNITY_EDITOR
            var sb = new StringBuilder();
            ToExpression(sb, hash);
            sb.Remove(sb.Length - 1, 1);
            return sb.ToString();
#else
            return string.Empty;
#endif
        }

        private int ToExpression(StringBuilder sb, int hash, int num = 0)
        {
            if (this.hash == hash) return num;
            this.hash = hash;
            if (pure)
            {
                sb.AppendFormat("pure:{0}|", value);
            }
            else if (select != null)
            {
                num = op1.ToExpression(sb, hash, num);
                sb.AppendFormat("{0}:{1}|", select, op1.id);
            }
            else if (op != null)
            {
                num = op1.ToExpression(sb, hash, num);
                num = op2.ToExpression(sb, hash, num);
                if (num == 3657) Debugger.Break();
                sb.AppendFormat("{0}:{1}:{2}|", op, op1.id, op2.id);
            }
            else if (rnd != null)
                sb.AppendFormat("{0}|", rnd);
            else throw new NotImplementedException();
            id = num++;
            return num;
        }
    }
}
