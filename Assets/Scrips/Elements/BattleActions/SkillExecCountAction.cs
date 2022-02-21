// Decompiled with JetBrains decompiler
// Type: Elements.SkillExecCountAction
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

namespace Elements
{
  public class SkillExecCountAction : ActionParameter
  {
    public override void ExecActionOnWaveStart(
      Skill _skill,
      UnitCtrl _source,
      UnitActionController _sourceActionController)
    {
      base.ExecActionOnWaveStart(_skill, _source, _sourceActionController);
      if (!_source.SkillExecCountDictionary.ContainsKey(ActionDetail1))
        return;
      _source.SkillExecCountDictionary[ActionDetail1] = 0;
    }

    public override void ExecActionOnStart(
      Skill _skill,
      UnitCtrl _source,
      UnitActionController _sourceActionController) => base.ExecActionOnStart(_skill, _source, _sourceActionController);

    public override void ExecAction(
      UnitCtrl _source,
      BasePartsData _target,
      int _num,
      UnitActionController _sourceActionController,
      Skill _skill,
      float _starttime,
      Dictionary<int, bool> _enabledChildAction,
      Dictionary<eValueNumber, FloatWithEx> _valueDictionary)
    {
      if (!_source.SkillExecCountDictionary.ContainsKey(ActionDetail1))
        _source.SkillExecCountDictionary.Add(ActionDetail1, 0);
      _source.SkillExecCountDictionary[ActionDetail1] = Mathf.Min((int) _valueDictionary[eValueNumber.VALUE_1], _source.SkillExecCountDictionary[ActionDetail1] + 1);
    }

    public override void SetLevel(float _level) => base.SetLevel(_level);
  }
}
