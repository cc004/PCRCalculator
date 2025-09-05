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
using UnityEngine.Assertions;

namespace Elements
{
    public class DoubleWithEx : IComparable<DoubleWithEx>, IEquatable<DoubleWithEx>, IWithEx
    {
        private bool pure => min == max;

        public double ToDouble() => value;
        public long ToLong() => (long)value;
        public int CompareTo(object obj) => obj is IWithEx other ? value.CompareTo(other.ToDouble()) : 1;
        private readonly double value, avg, min, max;
        private readonly Func<int, double> root;
        private int hash;
        private double cache;
        public string select, op;
        private string rnd;
        public DoubleWithEx op1, op2;

        public DoubleWithEx(double value = 0.0, double avg = 0.0, double min = 0.0, double max = 0.0,
            Func<int, double> root = null)
        {
            this.value = value;
            this.avg = avg;
            this.min = min;
            this.max = max;
            this.root = root;
        }

        public double Emulate(int hash)
        {
            if (root == null) return value;
            if (hash != this.hash)
            {
                cache = root(hash);
                this.hash = hash;
            }
            return cache;
        }

        public static Random rndd = new Random();
        public static UnityRandom rand = new UnityRandom();
        public static List<int> cachedSeed = new List<int>();
        public static void SetState(int seed)
        {
            rand.InitState(seed);
            cachedSeed.Clear();
        }

        public static int GetRandN(int n)
        {
            while (cachedSeed.Count <= n) cachedSeed.Add(rand.Range(0, 1000));
            return cachedSeed[n];
        }
        public static DoubleWithEx Binomial(double p, bool val, string rnd, int order)
        {
            p = Math.Clamp(p, 0.0, 1.0);
            if (p == 0.0) return 0.0;
            if (p == 1.0) return 1.0;
            var p0 = (long) (p * 1000);
            var result = new DoubleWithEx(val ? 1 : 0, p, root: _ => GetRandN(order) < p0 ? 1 : 0, min: 0,
                max: 1)
            {
                rnd = rnd
            };
            return result;
        }


        public DoubleWithEx Select(Func<double, double> selector, string select)
        {
            if (pure) return selector(value);
            double a = selector(min), b = selector(max);
            return new DoubleWithEx(selector(value), selector(avg), root: hash => selector(Emulate(hash)),
                min: Math.Min(a, b), max: Math.Max(a, b))
            {
                select = select, op1 = this
            };
        }

        private static DoubleWithEx Default = new DoubleWithEx(0.0);

        private static DoubleWithEx Op(DoubleWithEx a, DoubleWithEx b,
            Func<double, double, double> op, string ops)
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
                return new DoubleWithEx(op(vala, b.value), op(vala, b.avg),
                    root: hash => op(vala, b.Emulate(hash)), min: xs.Min(), max: xs.Max())
                {
                    op = ops, op1 = vala, op2 = b
                };
            }

            if (b.pure)
                return new DoubleWithEx(op(a.value, valb), op(a.avg, valb),
                    root: hash => op(a.Emulate(hash), valb), min: xs.Min(), max: xs.Max())
                {
                    op = ops,
                    op1 = a,
                    op2 = valb
                };
            return new DoubleWithEx(op(a.value, b.value), op(a.avg, b.avg),
                root: hash => op(a.Emulate(hash), b.Emulate(hash)), min: xs.Min(), max: xs.Max())
            {
                op = ops,
                op1 = a,
                op2 = b
            };
        }

        public double Expect => avg;
        public double Expected(int N)
        {
            double s = 0;
            for (int i = 0; i < N; ++i)
            {
                SetState(rndd.Next());
                s += Emulate(rndd.Next());
            }
            return s / N;
        }
        public double Stddev
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
                return Math.Sqrt(Math.Max(0, (s2 - s * s / N) / N));
            }
        }
        public double Probability(Func<double, bool> predict)
        {
            var N = GuildManager.StaticsettingData.n2;
            int s = 0;
            for (int i = 0; i < N; ++i)
            {
                SetState(rndd.Next());
                if (predict(Emulate(rndd.Next()))) ++s;
            }
            return s / N;
        }

        public static implicit operator double(DoubleWithEx self) => self.value;

        public static implicit operator DoubleWithEx(double x) =>
            new DoubleWithEx(x, x, x, x, null);

        public static explicit operator LongWithEx(DoubleWithEx x) {
            // 判断是否接近整数
            Assert.IsTrue(Math.Abs(x.value - Math.Round(x.value)) < 1e-8);
            Assert.IsTrue(Math.Abs(x.avg - Math.Round(x.avg)) < 1e-8);
            Assert.IsTrue(Math.Abs(x.min - Math.Round(x.min)) < 1e-8);
            Assert.IsTrue(Math.Abs(x.max - Math.Round(x.max)) < 1e-8);
            return new LongWithEx((long)x.value, (long)x.avg, (long)x.min, (long)x.max,
                x.root == null ? null : (hash => (long)x.root(hash)))
            {
                select = x.select ?? null,
                op = x.op ?? null,
                op1 = x.op1 ?? null,
                op2 = x.op2 ?? null
            };
        }

        // public LongWithEx ToLongWithEx() => (LongWithEx)this;

		public static bool Approximately(double _a, double _b)
		{
			return Math.Abs(_b - _a) < Math.Max(1E-14 * Math.Max(Math.Abs(_a), Math.Abs(_b)), 2.220446049250313E-16);
		}

        public LongWithEx ToLongWithEx()
        {
            double _num = value;
            if (_num >= 0.0)
            {
                long num = Mathf.CeilToInt((float)_num);
                return Approximately(_num, num) ? (LongWithEx)this.Ceil() : (LongWithEx)this.Floor();
            }
            long num1 = Mathf.FloorToInt((float)_num);
            return Approximately(_num, num1) ? (LongWithEx)this.Floor() : (LongWithEx)this.Floor();
        }

        public DoubleWithEx Max(double f)
        {
            if (min > f) return this;
            return Select(x => Math.Max(x, f), $"max:{f}");
        }

        public DoubleWithEx Min(double f)
        {
            if (max < f) return this;
            return Select(x => Math.Min(x, f), $"min:{f}");
        }

        public override string ToString() => pure ? ((double)this).ToString() : $"{(int)(double)this}[{(int)Expect}]";
        public DoubleWithEx Floor() => Select(Math.Floor, "floor");
        public DoubleWithEx Ceil() => Select(Math.Ceiling, "ceil");
        public DoubleWithEx Abs() => Select(Math.Abs, "abs");
        public int CompareTo(DoubleWithEx other) => ((double)this).CompareTo(other);
        public static DoubleWithEx operator +(DoubleWithEx a, DoubleWithEx b) => Op(a, b, (x, y) => x + y, "add");
        public static DoubleWithEx operator -(DoubleWithEx a, DoubleWithEx b) => Op(a, b, (x, y) => x - y, "sub");
        public static DoubleWithEx operator *(DoubleWithEx a, DoubleWithEx b)
        {
            if (b.StrictlyEquals(1.0)) return a;
            if (a.StrictlyEquals(1.0)) return b;
            return Op(a, b, (x, y) => x * y, "mul");
        }

        public static DoubleWithEx operator /(DoubleWithEx a, DoubleWithEx b) => Op(a, b, (x, y) => x / y, "div");

        public static DoubleWithEx operator +(DoubleWithEx a, double b) => a + (DoubleWithEx)b;
        public static DoubleWithEx operator +(double a, DoubleWithEx b) => (DoubleWithEx)a + b;
        public static DoubleWithEx operator +(DoubleWithEx a, float b) => a + (DoubleWithEx)b;
        public static DoubleWithEx operator +(float a, DoubleWithEx b) => (DoubleWithEx)a + b;

        public static DoubleWithEx operator -(DoubleWithEx a, double b) => a - (DoubleWithEx)b;
        public static DoubleWithEx operator -(double a, DoubleWithEx b) => (DoubleWithEx)a - b;
        public static DoubleWithEx operator -(DoubleWithEx a, float b) => a - (DoubleWithEx)b;
        public static DoubleWithEx operator -(float a, DoubleWithEx b) => (DoubleWithEx)a - b;

        public static DoubleWithEx operator *(DoubleWithEx a, double b) => a * (DoubleWithEx)b;
        public static DoubleWithEx operator *(double a, DoubleWithEx b) => (DoubleWithEx)a * b;
        public static DoubleWithEx operator *(DoubleWithEx a, float b) => a * (DoubleWithEx)b;
        public static DoubleWithEx operator *(float a, DoubleWithEx b) => (DoubleWithEx)a * b;

        public static DoubleWithEx operator /(DoubleWithEx a, double b) => a / (DoubleWithEx)b;
        public static DoubleWithEx operator /(double a, DoubleWithEx b) => (DoubleWithEx)a / b;
        public static DoubleWithEx operator /(DoubleWithEx a, float b) => a / (DoubleWithEx)b;
        public static DoubleWithEx operator /(float a, DoubleWithEx b) => (DoubleWithEx)a / b;

        public static bool operator ==(DoubleWithEx a, DoubleWithEx b) => a.Equals(b);
        public static bool operator !=(DoubleWithEx a, DoubleWithEx b) => !a.Equals(b);
        public bool Equals(DoubleWithEx other)
        {
            return value.Equals(other.value);
        }
        public bool Equals(IWithEx other)
        {
            return value.Equals(other.ToDouble());
        }

        public override bool Equals(object obj)
        {
            return obj is DoubleWithEx other && Equals(other);
        }

        public bool StrictlyEquals(double x)
        {
            return pure && value == x;
        }

        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        public DoubleWithEx ZeroCapForHp()
        {
            return new DoubleWithEx(value >= 0.0 ? value : 0.0, avg, min, max, root)
            {
                op = op, op1 = op1, op2 = op2, rnd = rnd, select = select, id = id
            };
        }
        
        public int id;

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

        public int ToExpression(StringBuilder sb, int hash, int num = 0)
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
            else throw new NotImplementedException($" this={this} rnd={rnd} op={op} select={select} op1={op1} op2={op2}");
            id = num++;
            return num;
        }
    }
}
