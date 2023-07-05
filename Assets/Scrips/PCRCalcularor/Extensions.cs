using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCRCalcularor
{
    public interface IOverride<in T>
    {
        void OverrideWith(T other);
    }
    public interface IOverride2<in T>
    {
        void Override2With(T other);
    }

    internal static class Extensions
    {
        public static void TryAdd<TKey, TValue>(this Dictionary<TKey, TValue> self,
            Dictionary<TKey, TValue> other)
        {
            foreach (var pair in other)
                self.TryAdd(pair.Key, pair.Value);
        }

        public static void OverrideWith<TKey, TValue>(this Dictionary<TKey, TValue> self,
            Dictionary<TKey, TValue> other)
        {
            foreach (var pair in other)
            {
                if (self.TryGetValue(pair.Key, out var val))
                {
                    if (val is IOverride<TValue> val2)
                    {
                        val2.OverrideWith(pair.Value);
                    }
                    else
                    {
                        self[pair.Key] = pair.Value;
                    }
                }
                else
                    self.Add(pair.Key, pair.Value);
            }
        }
        public static void Override2With<TKey, TValue>(this Dictionary<TKey, TValue> self,
            Dictionary<TKey, TValue> other) where TValue : IOverride2<TValue>
        {
            foreach (var pair in other)
            {
                if (self.TryGetValue(pair.Key, out var val))
                {
                    pair.Value.Override2With(val);
                }
            }
        }
    }
}
