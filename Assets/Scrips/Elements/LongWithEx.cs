using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using PCRCaculator.Guild;
using UnityEngine;
using Random = System.Random;
using Elements;

namespace Elements
{
    public class LongWithEx : IComparable<LongWithEx>, IEquatable<LongWithEx>, IWithEx
    {
        private bool pure => min == max;

        public double ToDouble() => value;
        public long ToLong() => value;
        public int CompareTo(object obj) => obj is IWithEx other ? value.CompareTo(other.ToLong()) : 1;
        private readonly long value, avg, min, max;
        private readonly Func<int, long> root;
        private int hash;
        private long cache;
        public string select, rnd;
        public string op;
        public DoubleWithEx op1, op2;

        internal LongWithEx(long value = 0, long avg = 0, long min = 0, long max = 0,
            Func<int, long> root = null)
        {
            this.value = value;
            this.avg = avg;
            this.min = min;
            this.max = max;
            this.root = root;
        }

        public long Emulate(int hash)
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
            DoubleWithEx.SetState(seed);
        }

        private static int GetRandN(int n)
        {
            while (DoubleWithEx.cachedSeed.Count <= n) DoubleWithEx.cachedSeed.Add(DoubleWithEx.rand.Range(0, 1000));
            return DoubleWithEx.cachedSeed[n];
        }
        // public static LongWithEx Binomial(long p, bool val, string rnd, int order)
        // {
        //     p = Math.Clamp(p, 0, 1000);
        //     if (p == 0) return 0;
        //     if (p == 1000) return 1;
        //     var result = new LongWithEx(val ? 1 : 0, p, root: _ => GetRandN(order) < p ? 1 : 0, min: 0,
        //         max: 1)
        //     {
        //         rnd = rnd
        //     };
        //     return result;
        // }


        public LongWithEx Select(Func<long, long> selector, string select)
        {
            if (pure) return selector(value);
            long a = selector(min), b = selector(max);
            return new LongWithEx(selector(value), selector(avg), root: hash => selector(Emulate(hash)),
                min: Math.Min(a, b), max: Math.Max(a, b))
            {
                select = select, op1 = (DoubleWithEx)this
            };
        }

        public DoubleWithEx Select(Func<long, double> selector, string select)
        {
            if (pure) return selector(value);
            double a = selector(min), b = selector(max);
            return new DoubleWithEx(selector(value), selector(avg), root: hash => selector(Emulate(hash)),
                min: Math.Min(a, b), max: Math.Max(a, b))
            {
                select = select, op1 = (DoubleWithEx)this
            };
        }

        private static LongWithEx Default = new LongWithEx(0);

        private static LongWithEx Op(LongWithEx a, LongWithEx b,
            Func<long, long, long> op, string ops)
        {
            a ??= Default;
            b ??= Default;
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
                return new LongWithEx(op(vala, b.value), op(vala, b.avg),
                    root: hash => op(vala, b.Emulate(hash)), min: xs.Min(), max: xs.Max())
                {
                    op = ops, op1 = vala, op2 = (DoubleWithEx)b
                };
            }

            if (b.pure)
                return new LongWithEx(op(a.value, valb), op(a.avg, valb),
                    root: hash => op(a.Emulate(hash), valb), min: xs.Min(), max: xs.Max())
                {
                    op = ops,
                    op1 = (DoubleWithEx)a,
                    op2 = valb
                };
            return new LongWithEx(op(a.value, b.value), op(a.avg, b.avg),
                root: hash => op(a.Emulate(hash), b.Emulate(hash)), min: xs.Min(), max: xs.Max())
            {
                op = ops,
                op1 = (DoubleWithEx)a,
                op2 = (DoubleWithEx)b
            };
        }

        public long Expect => avg;
        public long Expected(int N)
        {
            long s = 0;
            for (int i = 0; i < N; ++i)
            {
                SetState(rndd.Next());
                s += Emulate(rndd.Next());
            }
            return s / N;
        }
        public long Stddev
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
                return (long)Math.Sqrt(Math.Max(0, (s2 - s * s / N) / N));
            }
        }
        public float Probability(Func<long, bool> predict)
        {
            var N = GuildManager.StaticsettingData.n2;
            int s = 0;
            for (int i = 0; i < N; ++i)
            {
                SetState(rndd.Next());
                if (predict(Emulate(rndd.Next()))) ++s;
            }
            return (float)s / N;
        }

        public static implicit operator long(LongWithEx self) => self.value;

        public static implicit operator LongWithEx(long x) =>
            new LongWithEx(x, x, x, x, null);

        public static explicit operator DoubleWithEx(LongWithEx x) =>
            new DoubleWithEx((double)x.value, (double)x.avg, (double)x.min, (double)x.max,
                x.root == null ? null : (hash => (double)x.root(hash)))
            {
                select = x.select ?? null,
                op = x.op ?? null,
                op1 = x.op1 ?? null,
                op2 = x.op2 ?? null
            };
        
        public DoubleWithEx ToDoubleWithEx() => (DoubleWithEx)this;

        public LongWithEx Max(long f)
        {
            if (min > f) return this;
            return Select(x => Math.Max(x, f), $"max:{f}");
        }

        public LongWithEx Min(long f)
        {
            if (max < f) return this;
            return Select(x => Math.Min(x, f), $"min:{f}");
        }

        public override string ToString() => pure ? ((long)this).ToString() : $"{(long)this}[{(long)Expect}]";
        public LongWithEx Abs() => Select(Math.Abs, "abs");

        public int CompareTo(LongWithEx other) => ((long)this).CompareTo(other);
        public static LongWithEx operator +(LongWithEx a, LongWithEx b) => Op(a, b, (x, y) => x + y, "add");
        public static LongWithEx operator -(LongWithEx a, LongWithEx b) => Op(a, b, (x, y) => x - y, "sub");
        public static LongWithEx operator *(LongWithEx a, LongWithEx b)
        {
            if (b.StrictlyEquals(1)) return a;
            if (a.StrictlyEquals(1)) return b;
            return Op(a, b, (x, y) => x * y, "mul");
        }

        public static DoubleWithEx operator /(LongWithEx a, LongWithEx b) => (DoubleWithEx)a / (DoubleWithEx)b;

        public static bool operator ==(LongWithEx a, LongWithEx b) => a.Equals(b);
        public static bool operator !=(LongWithEx a, LongWithEx b) => !a.Equals(b);

        public static DoubleWithEx operator +(LongWithEx a, double b) => (DoubleWithEx)a + b;
        public static DoubleWithEx operator +(double a, LongWithEx b) => a + (DoubleWithEx)b;
        public static DoubleWithEx operator +(LongWithEx a, float b) => (DoubleWithEx)a + b;
        public static DoubleWithEx operator +(float a, LongWithEx b) => a + (DoubleWithEx)b;

        public static DoubleWithEx operator -(LongWithEx a, double b) => (DoubleWithEx)a - b;
        public static DoubleWithEx operator -(double a, LongWithEx b) => a - (DoubleWithEx)b;
        public static DoubleWithEx operator -(LongWithEx a, float b) => (DoubleWithEx)a - b;
        public static DoubleWithEx operator -(float a, LongWithEx b) => a - (DoubleWithEx)b;

        public static DoubleWithEx operator *(LongWithEx a, double b) => (DoubleWithEx)a * b;
        public static DoubleWithEx operator *(double a, LongWithEx b) => a * (DoubleWithEx)b;
        public static DoubleWithEx operator *(LongWithEx a, float b) => (DoubleWithEx)a * b;
        public static DoubleWithEx operator *(float a, LongWithEx b) => a * (DoubleWithEx)b;

        public static DoubleWithEx operator /(LongWithEx a, double b) => (DoubleWithEx)a / b;
        public static DoubleWithEx operator /(double a, LongWithEx b) => a / (DoubleWithEx)b;
        public static DoubleWithEx operator /(LongWithEx a, float b) => (DoubleWithEx)a / b;
        public static DoubleWithEx operator /(float a, LongWithEx b) => a / (DoubleWithEx)b;

        public bool Equals(LongWithEx other)
        {
            if (other is null) return false;
            return value.Equals(other.value);
        }

        public bool Equals(IWithEx other)
        {
            if (other is null) return false;
            return value.Equals(other.ToLong());
        }

        public override bool Equals(object obj)
        {
            return obj is LongWithEx other && Equals(other);
        }

        public bool StrictlyEquals(long x)
        {
            return pure && value == x;
        }

        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        public LongWithEx ZeroCapForHp()
        {
            return new LongWithEx(value >= 0 ? value : 0, avg, min, max, root)
            {
                op = op, op1 = op1, op2 = op2, rnd = rnd, select = select, id = id
            };
        }
        
        private int id;

        public string ToExpression(int hash)
        {
            if (ExcelHelper.ExcelHelper.AsmExportEnabled)
            {
                var sb = new StringBuilder();
                ToExpression(sb, hash);
                sb.Remove(sb.Length - 1, 1);
                return sb.ToString();
            }

            return string.Empty;
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
                sb.AppendFormat("{0}:{1}:{2}|", op, op1.id, op2.id);
            }
            else if (rnd != null)
                sb.AppendFormat("{0}|", rnd);
            else throw new NotImplementedException($" {this} {rnd} {op} {select}");
            id = num++;
            return num;
        }
    }
}
