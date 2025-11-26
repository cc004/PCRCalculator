using Elements;
using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#if UNITY_5_3_OR_NEWER || !DONT_USE_EX
public class ValueDictionary
{
    public readonly DoubleWithEx[] Values = new DoubleWithEx[7];
    public readonly bool[] HasValues = new bool[7];

    public DoubleWithEx this[eValueNumber eValueNumber]
    {
        get => Values[(int)eValueNumber];
        set
        {
            var index = (int)eValueNumber;
            HasValues[index] = true;
            Values[index] = value;
        }
    }

    public bool TryGetValue(eValueNumber eValueNumber, out DoubleWithEx val)
    {
        var index = (int)eValueNumber;
        if (HasValues[index])
        {
            val = Values[index];
            return true;
        }
        else
        {
            val = default;
            return false;
        }
    }

    public void Add(eValueNumber eValueNumber, DoubleWithEx f)
    {
        var index = (int)eValueNumber;
        HasValues[index] = true;
        Values[index] = f;
    }

    public void Clear()
    {
        for (var i = 0; i < 7; ++i)
        {
            HasValues[i] = false;
        }
    }
}
#else
public unsafe class ValueDictionary
{
    [StructLayout(LayoutKind.Sequential)]
    private struct Container
    {
        public fixed double Values[7];
        public fixed bool HasValues[7];
    }

    private static readonly Container* _pool = (Container*)Marshal.AllocHGlobal(
        sizeof(Container) * 0x10000);

    private static int poolOffset = 0;

    private Container* container;

    public ValueDictionary()
    {
        poolOffset = (poolOffset + 1) & 0xffff;
        container = _pool + poolOffset;
        Clear();
    }

    public DoubleWithEx this[eValueNumber eValueNumber]
    {
        get => *(DoubleWithEx*)(container->Values + (int)eValueNumber);
        set
        {
            var index = (int)eValueNumber;
            container->HasValues[index] = true;
            container->Values[index] = value;
        }
    }

    public bool TryGetValue(eValueNumber eValueNumber, out DoubleWithEx val)
    {
        var index = (int)eValueNumber;
        if (container->HasValues[index])
        {
            val = container->Values[index];
            return true;
        }
        else
        {
            val = default;
            return false;
        }
    }

    public void Add(eValueNumber eValueNumber, DoubleWithEx f)
    {
        var index = (int)eValueNumber;
        container->HasValues[index] = true;
        container->Values[index] = f;
    }

    public void Clear()
    {
        for (var i = 0; i < 7; ++i)
        {
            container->HasValues[i] = false;
        }
    }
}
#endif