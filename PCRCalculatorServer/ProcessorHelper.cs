using System;
using System.Runtime.InteropServices;

public static class ProcessorHelper
{
    private const ushort ALL_PROCESSOR_GROUPS = 0xFFFF;

    [StructLayout(LayoutKind.Sequential)]
    public struct GROUP_AFFINITY
    {
        public ulong Mask;
        public ushort Group;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public ushort[] Reserved;
    }

    [DllImport("kernel32.dll")]
    private static extern ushort GetActiveProcessorGroupCount();

    [DllImport("kernel32.dll")]
    private static extern uint GetActiveProcessorCount(ushort GroupNumber);

    [DllImport("kernel32.dll")]
    private static extern IntPtr GetCurrentThread();

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern bool SetThreadGroupAffinity(IntPtr hThread, ref GROUP_AFFINITY GroupAffinity, out GROUP_AFFINITY PreviousGroupAffinity);

    [DllImport("kernel32.dll")]
    private static extern bool GetNumaHighestNodeNumber(out uint HighestNodeNumber);

    [DllImport("kernel32.dll")]
    private static extern bool GetNumaProcessorNode(byte Processor, out byte NodeNumber);

    private static int _totalLogicalProcessors = -1;
    private static int _processorGroupCount = -1;
    private static int[] _processorsPerGroup;

    public static int GetTotalLogicalProcessorCount()
    {
        if (_totalLogicalProcessors >= 0)
            return _totalLogicalProcessors;

        try
        {
            uint count = GetActiveProcessorCount(ALL_PROCESSOR_GROUPS);
            _totalLogicalProcessors = count > 0 ? (int)count : Environment.ProcessorCount;
        }
        catch
        {
            _totalLogicalProcessors = Environment.ProcessorCount;
        }
        return _totalLogicalProcessors;
    }

    public static int GetProcessorGroupCount()
    {
        if (_processorGroupCount >= 0)
            return _processorGroupCount;

        try
        {
            _processorGroupCount = GetActiveProcessorGroupCount();
        }
        catch
        {
            _processorGroupCount = 1;
        }
        return _processorGroupCount;
    }

    public static int GetProcessorsInGroup(int groupIndex)
    {
        if (_processorsPerGroup == null)
        {
            InitializeGroupInfo();
        }
        if (groupIndex >= 0 && groupIndex < _processorsPerGroup.Length)
        {
            return _processorsPerGroup[groupIndex];
        }
        return 64;
    }

    private static void InitializeGroupInfo()
    {
        int groupCount = GetProcessorGroupCount();
        _processorsPerGroup = new int[groupCount];
        for (int i = 0; i < groupCount; i++)
        {
            try
            {
                _processorsPerGroup[i] = (int)GetActiveProcessorCount((ushort)i);
            }
            catch
            {
                _processorsPerGroup[i] = 64;
            }
        }
    }

    public static void SetThreadGroupAffinityForIndex(int threadIndex)
    {
        if (GetProcessorGroupCount() <= 1)
            return;

        try
        {
            int groupIndex = ComputeGroupForThread(threadIndex);
            int processorsInGroup = GetProcessorsInGroup(groupIndex);
            ulong mask = (1UL << processorsInGroup) - 1;

            var affinity = new GROUP_AFFINITY
            {
                Mask = mask,
                Group = (ushort)groupIndex,
                Reserved = new ushort[3]
            };

            SetThreadGroupAffinity(GetCurrentThread(), ref affinity, out _);
        }
        catch
        {
        }
    }

    public static void SetThreadGroupAffinityForGroup(int groupIndex)
    {
        if (GetProcessorGroupCount() <= 1)
            return;

        try
        {
            int processorsInGroup = GetProcessorsInGroup(groupIndex);
            ulong mask = (1UL << processorsInGroup) - 1;

            var affinity = new GROUP_AFFINITY
            {
                Mask = mask,
                Group = (ushort)groupIndex,
                Reserved = new ushort[3]
            };

            SetThreadGroupAffinity(GetCurrentThread(), ref affinity, out _);
        }
        catch
        {
        }
    }

    private static int ComputeGroupForThread(int threadIndex)
    {
        int groupCount = GetProcessorGroupCount();
        if (groupCount <= 1)
            return 0;

        int processorsSeen = 0;
        for (int g = 0; g < groupCount; g++)
        {
            int processorsInGroup = GetProcessorsInGroup(g);
            if (threadIndex < processorsSeen + processorsInGroup)
            {
                return g;
            }
            processorsSeen += processorsInGroup;
        }
        return threadIndex % groupCount;
    }
}
