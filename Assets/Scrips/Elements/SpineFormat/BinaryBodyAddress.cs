// Decompiled with JetBrains decompiler
// Type: Elements.SpineFormat.BinaryBodyAddress
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

namespace Elements.SpineFormat
{
  public struct BinaryBodyAddress
  {
    public const int SIZE = 32;
    private long _bufferPosStart;
    private long _bufferSize;
    private int _binaryFormat;
    private int __dummy_size__1;
    private int __dummy_size__2;
    private int __dummy_size__3;

    public int binaryFormat
    {
      get => this._binaryFormat;
      set => this._binaryFormat = value;
    }

    public long bufferPosStart
    {
      get => this._bufferPosStart;
      set => this._bufferPosStart = value;
    }

    public long bufferSize
    {
      get => this._bufferSize;
      set => this._bufferSize = value;
    }
  }
}
