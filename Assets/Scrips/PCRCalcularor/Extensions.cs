using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCRCalcularor
{
    internal class Extensions
    {
        public static void OverrideWith<TKey, TValue>(Dictionary<TKey, TValue> self,
            Dictionary<TKey, TValue> other)
        {
            foreach (var pair in other)
            {
                self[pair.Key] = pair.Value;
            }
        }
    }
}
