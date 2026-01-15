using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#if ENABLE_THREADING
internal class ConcurrentCache<TKey, TValue>
{
    [ThreadStatic]
    private static Dictionary<TKey, TValue> threadLocalCache;

    private static Dictionary<TKey, TValue> globalCache = new();

    public bool TryGetValue(TKey key, out TValue value)
    {
        threadLocalCache ??= new();
        if (threadLocalCache.TryGetValue(key, out value)) return true;

        lock (globalCache)
        {
            if (globalCache.TryGetValue(key, out value))
            {
                threadLocalCache[key] = value;
                return true;
            }
        }

        return false;
    }

    public TValue this[TKey key]
    {
        set => Add(key, value);
    }

    public void Add(TKey key, TValue value)
    {
        threadLocalCache ??= new();
        threadLocalCache.Add(key, value);
        lock (globalCache)
        {
            globalCache.TryAdd(key, value);
        }
    }
}
#else

internal class ConcurrentCache<TKey, TValue> : Dictionary<TKey, TValue> {}

#endif