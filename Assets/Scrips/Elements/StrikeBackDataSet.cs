// Decompiled with JetBrains decompiler
// Type: Elements.StrikeBackDataSet
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System.Collections.Generic;

namespace Elements
{
  public class StrikeBackDataSet
  {
    public List<StrikeBackData> DataList { get; set; }

    public SkillEffectCtrl SkillEffect { get; set; }

        public void SetTimeToDie() { }// => this.SkillEffect.SetTimeToDie(true);
  }
}
