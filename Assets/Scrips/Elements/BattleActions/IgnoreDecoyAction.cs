// Decompiled with JetBrains decompiler
// Type: Elements.IgnoreDecoyAction
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

namespace Elements
{
  public class IgnoreDecoyAction : ActionParameter
  {
    public override void ExecActionOnStart(
      Skill _skill,
      UnitCtrl _source,
      UnitActionController _sourceActionController)
    {
      base.ExecActionOnStart(_skill, _source, _sourceActionController);
      this.IgnoreDecoy = true;
      foreach (int actionChildrenIndex in this.ActionChildrenIndexes)
        _skill.ActionParameters[actionChildrenIndex].IgnoreDecoy = true;
    }
  }
}
