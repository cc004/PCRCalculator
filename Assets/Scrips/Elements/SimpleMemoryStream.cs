// Decompiled with JetBrains decompiler
// Type: Elements.SimpleMemoryStream
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System;
using System.IO;

namespace Elements
{
  public class SimpleMemoryStream
  {
    private byte[] data;
    private long position;

    public SimpleMemoryStream(byte[] _data) => data = _data;

    public int ReadByte() => data[position++];

    public int Read(byte[] _dest_buffer, int _dest_offset, int _copy_size)
    {
      Buffer.BlockCopy(data, (int) position, _dest_buffer, _dest_offset, _copy_size);
      position += _copy_size;
      return _copy_size;
    }

    public long Seek(long _position, SeekOrigin _origin)
    {
      switch (_origin)
      {
        case SeekOrigin.Begin:
          position = _position;
          break;
        case SeekOrigin.Current:
          position += _position;
          break;
      }
      return position;
    }

    public long Position
    {
      get => position;
      set => position = value;
    }

    public void Close()
    {
    }
  }
}
