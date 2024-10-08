﻿// Decompiled with JetBrains decompiler
// Type: Elements.SpineFormat.BinaryHeader
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System;

namespace Elements.SpineFormat
{
  public struct BinaryHeader
  {
    public const int SIZE = 32;
    public const int MAJOR_VESRION = 0;
    public const int MINER_VESRION = 1;
    public const int TAG_LENGTH = 4;
    public const char TAG1 = 'c';
    public const char TAG2 = 'y';
    public const char TAG3 = 's';
    public const char TAG4 = 'p';
    private byte _tag1;
    private byte _tag2;
    private byte _tag3;
    private byte _tag4;
    private int _majorVersion;
    private int _minerVersion;
    private int _bodyCount;
    private int __dummy_size__1;
    private int __dummy_size__2;
    private int __dummy_size__3;
    private int __dummy_size__4;

    public void Init(byte[] bytes)
    {
      int num1 = 0;
      byte[] numArray1 = bytes;
      int index1 = num1;
      int num2 = index1 + 1;
      _tag1 = numArray1[index1];
      byte[] numArray2 = bytes;
      int index2 = num2;
      int num3 = index2 + 1;
      _tag2 = numArray2[index2];
      byte[] numArray3 = bytes;
      int index3 = num3;
      int num4 = index3 + 1;
      _tag3 = numArray3[index3];
      byte[] numArray4 = bytes;
      int index4 = num4;
      int counter = index4 + 1;
      _tag4 = numArray4[index4];
      _majorVersion = ByteToInt(bytes, ref counter);
      _minerVersion = ByteToInt(bytes, ref counter);
      _bodyCount = ByteToInt(bytes, ref counter);
    }

    private int ByteToInt(byte[] bytes, ref int counter)
    {
      int int32 = BitConverter.ToInt32(bytes, counter);
      counter += 4;
      return int32;
    }

    public void Init(int bodyCount)
    {
      SetTag();
      SetVersion();
      _bodyCount = bodyCount;
    }

    public bool CheckTag() => _tag1 == 99 && _tag2 == 121 && (_tag3 == 115 && _tag4 == 112);

    public bool CheckVersion() => _majorVersion == 0 && _minerVersion == 1;

    public int majorVersion
    {
      get => _majorVersion;
      set => _majorVersion = value;
    }

    public int minerVersion
    {
      get => _minerVersion;
      set => _minerVersion = value;
    }

    public int bodyCount
    {
      get => _bodyCount;
      set => _bodyCount = value;
    }

    private void SetVersion()
    {
      _majorVersion = 0;
      _minerVersion = 1;
    }

    private void SetTag()
    {
      _tag1 = Convert.ToByte('c');
      _tag2 = Convert.ToByte('y');
      _tag3 = Convert.ToByte('s');
      _tag4 = Convert.ToByte('p');
    }
  }
}
