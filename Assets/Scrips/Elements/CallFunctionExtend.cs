// Decompiled with JetBrains decompiler
// Type: Cute.PEOJMLFEHPE
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System;

namespace Cute
{
  public static class CallFunctionExtend
  {
    public static void Call(this Action DMFGKJIEEBF)
    {
      if (DMFGKJIEEBF == null)
        return;
      DMFGKJIEEBF();
    }

    public static void Call<T1>(this Action<T1> DMFGKJIEEBF, T1 JEOCPILJNAD)
    {
      if (DMFGKJIEEBF == null)
        return;
      DMFGKJIEEBF(JEOCPILJNAD);
    }

    public static void Call<T1, T2>(
      this Action<T1, T2> DMFGKJIEEBF,
      T1 JEOCPILJNAD,
      T2 IDAFJHFJKOL)
    {
      if (DMFGKJIEEBF == null)
        return;
      DMFGKJIEEBF(JEOCPILJNAD, IDAFJHFJKOL);
    }

    public static void Call<T1, T2, T3>(
      this Action<T1, T2, T3> DMFGKJIEEBF,
      T1 JEOCPILJNAD,
      T2 IDAFJHFJKOL,
      T3 ADIFIOLCOPN)
    {
      if (DMFGKJIEEBF == null)
        return;
      DMFGKJIEEBF(JEOCPILJNAD, IDAFJHFJKOL, ADIFIOLCOPN);
    }

    public static void Call<T1, T2, T3, T4>(
      this Action<T1, T2, T3, T4> DMFGKJIEEBF,
      T1 JEOCPILJNAD,
      T2 IDAFJHFJKOL,
      T3 ADIFIOLCOPN,
      T4 IJIFHOIAODL)
    {
      if (DMFGKJIEEBF == null)
        return;
      DMFGKJIEEBF(JEOCPILJNAD, IDAFJHFJKOL, ADIFIOLCOPN, IJIFHOIAODL);
    }

    public static TR Call<TR>(this Func<TR> GJOLOFLPBGH) => GJOLOFLPBGH == null ? default (TR) : GJOLOFLPBGH();

    public static TR Call<T1, TR>(this Func<T1, TR> GJOLOFLPBGH, T1 JEOCPILJNAD) => GJOLOFLPBGH == null ? default (TR) : GJOLOFLPBGH(JEOCPILJNAD);

    public static TR Call<T1, T2, TR>(
      this Func<T1, T2, TR> GJOLOFLPBGH,
      T1 JEOCPILJNAD,
      T2 IDAFJHFJKOL) => GJOLOFLPBGH == null ? default (TR) : GJOLOFLPBGH(JEOCPILJNAD, IDAFJHFJKOL);

    public static TR Call<T1, T2, T3, TR>(
      this Func<T1, T2, T3, TR> GJOLOFLPBGH,
      T1 JEOCPILJNAD,
      T2 IDAFJHFJKOL,
      T3 ADIFIOLCOPN) => GJOLOFLPBGH == null ? default (TR) : GJOLOFLPBGH(JEOCPILJNAD, IDAFJHFJKOL, ADIFIOLCOPN);

    public static TR[] GetAllFuncCallResults<TR>(this Func<TR> GJOLOFLPBGH) => GJOLOFLPBGH == null ? new TR[0] : CallAllFunc(GJOLOFLPBGH, ABCBPPLJGML => ((Func<TR>) ABCBPPLJGML)());

    /*public static TR[] GetAllFuncCallResults<T1, TR>(this Func<T1, TR> GJOLOFLPBGH, T1 JEOCPILJNAD) => GJOLOFLPBGH == null ? new TR[0] : PEOJMLFEHPE.CallAllFunc<TR>((Delegate) GJOLOFLPBGH, new Func<Delegate, TR>(new PEOJMLFEHPE.MBCJLGKDOCC<T1, TR>()
    {
      arg1 = JEOCPILJNAD
    }.GetAllFuncCallResultsb__0));*/

    /*public static TR[] GetAllFuncCallResults<T1, T2, TR>(
      this Func<T1, T2, TR> GJOLOFLPBGH,
      T1 JEOCPILJNAD,
      T2 IDAFJHFJKOL) => GJOLOFLPBGH == null ? new TR[0] : PEOJMLFEHPE.CallAllFunc<TR>((Delegate) GJOLOFLPBGH, new Func<Delegate, TR>(new PEOJMLFEHPE.BJBLIOELPIE<T1, T2, TR>()
    {
      arg1 = JEOCPILJNAD,
      arg2 = IDAFJHFJKOL
    }.GetAllFuncCallResultsb__0));*/

    /*public static TR[] GetAllFuncCallResults<T1, T2, T3, TR>(
      this Func<T1, T2, T3, TR> GJOLOFLPBGH,
      T1 JEOCPILJNAD,
      T2 IDAFJHFJKOL,
      T3 ADIFIOLCOPN) => GJOLOFLPBGH == null ? new TR[0] : PEOJMLFEHPE.CallAllFunc<TR>((Delegate) GJOLOFLPBGH, new Func<Delegate, TR>(new PEOJMLFEHPE.LCJCMDJLOJK<T1, T2, T3, TR>()
    {
      arg1 = JEOCPILJNAD,
      arg2 = IDAFJHFJKOL,
      arg3 = ADIFIOLCOPN
    }.GetAllFuncCallResultsb__0));*/

    private static TR[] CallAllFunc<TR>(Delegate GJOLOFLPBGH, Func<Delegate, TR> JOHDEMHHGPF)
    {
      Delegate[] invocationList = GJOLOFLPBGH.GetInvocationList();
      int length = invocationList.Length;
      TR[] rArray = new TR[length];
      for (int index = 0; index < length; ++index)
      {
        TR r = JOHDEMHHGPF(invocationList[index]);
        rArray[index] = r;
      }
      return rArray;
    }
  }
}
