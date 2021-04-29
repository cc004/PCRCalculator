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

    public SimpleMemoryStream(byte[] _data) => this.data = _data;

    public int ReadByte() => (int) this.data[this.position++];

    public int Read(byte[] _dest_buffer, int _dest_offset, int _copy_size)
    {
      Buffer.BlockCopy((Array) this.data, (int) this.position, (Array) _dest_buffer, _dest_offset, _copy_size);
      this.position += (long) _copy_size;
      return _copy_size;
    }

    public long Seek(long _position, SeekOrigin _origin)
    {
      switch (_origin)
      {
        case SeekOrigin.Begin:
          this.position = _position;
          break;
        case SeekOrigin.Current:
          this.position += _position;
          break;
      }
      return this.position;
    }

    public long Position
    {
      get => this.position;
      set => this.position = value;
    }

    public void Close()
    {
    }
  }
}
