// Decompiled with JetBrains decompiler
// Type: CodeStage.AntiCheat.ObscuredTypes.OMJDMPIJJDN
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

namespace CodeStage.AntiCheat.ObscuredTypes
{
  internal class OMJDMPIJJDN
  {
    public static byte[] GetBytes(Decimal NHOJIFLMFDJ)
    {
      int[] bits = Decimal.GetBits(NHOJIFLMFDJ);
      List<byte> byteList = new List<byte>();
      foreach (int num in bits)
        byteList.AddRange((IEnumerable<byte>) BitConverter.GetBytes(num));
      return byteList.ToArray();
    }

    public static Decimal ToDecimal(byte[] DOJDGDHBMIP)
    {
      if (DOJDGDHBMIP.Length != 16)
        throw new Exception("[ACTk] A decimal must be created from exactly 16 bytes");
      int[] bits = new int[4];
      for (int startIndex = 0; startIndex <= 15; startIndex += 4)
        bits[startIndex / 4] = BitConverter.ToInt32(DOJDGDHBMIP, startIndex);
      return new Decimal(bits);
    }
  }
}
