// Decompiled with JetBrains decompiler
// Type: Elements.AttachmentChangeData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System;
using Spine;

namespace Elements
{
  [Serializable]
  public class AttachmentChangeData
  {
    public string TargetAttachmentName = "";
    public string AppliedAttachmentName = "";

    public int TargetIndex { get; set; }

    public Attachment TargetAttachment { get; set; }

    public Attachment AppliedAttachment { get; set; }
  }
}
