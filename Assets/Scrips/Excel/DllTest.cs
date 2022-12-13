using System;
using System.Runtime.InteropServices;
using System.Threading;

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]

public struct OpenFileName
{
    public int structSize;
    public IntPtr dlgOwner;
    public IntPtr instance;
    public String filter;
    public String customFilter;
    public int maxCustFilter;
    public int filterInde;
    public String file;
    public int maxFile;
    public String fileTitle;
    public int maxFileTitle;
    public String initialDir;
    public String title;
    public int flags;
    public short fileOffset;
    public short fileExtension;
    public String defExt;
    public IntPtr custData;
    public IntPtr hook;
    public String templateName;
    public IntPtr reservedPtr;
    public int reservedInt;
    public int flagsEx;

}




public class DllTest
{
    private static T YieldRun<T, TArg>(Func<TArg, T> action, TArg arg)
    {
        var sema = new Semaphore(0, 1);
        T result = default;
        new Thread(() =>
        {
            result = action(arg);
            sema.Release();
        }).Start();
        sema.WaitOne();
        return result;
    }

    [DllImport("Comdlg32.dll", SetLastError = true, ThrowOnUnmappableChar = true, CharSet = CharSet.Auto, CallingConvention = CallingConvention.Winapi)]
    public static extern bool GetOpenFileName([In, Out]ref OpenFileName ofn);
    public static bool GetOpenFileName1([In, Out]ref OpenFileName ofn)
    {
        var (result, ofn2) = YieldRun<(bool, OpenFileName), OpenFileName>(ofn =>
        {
            var r = GetOpenFileName(ref ofn);
            return (r, ofn);
        }, ofn);
        ofn = ofn2;
        return result;
    }

    [DllImport("Comdlg32.dll", SetLastError = true, ThrowOnUnmappableChar = true, CharSet = CharSet.Auto, CallingConvention = CallingConvention.Winapi)]
    public static extern bool GetSaveFileName([In, Out] ref OpenFileName ofn);
    public static bool GetSaveFileName1([In, Out] ref OpenFileName ofn)
    {
        var (result, ofn2) = YieldRun<(bool, OpenFileName), OpenFileName>(ofn =>
        {
            var r = GetSaveFileName(ref ofn);
            return (r, ofn);
        }, ofn);
        ofn = ofn2;
        return result;
    }
}