﻿// Decompiled with JetBrains decompiler
// Type: Elements.PassiveSealAction
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE4A7FA8-7E00-4124-8344-C695120E3AA4
// Assembly location: C:\Users\user\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;

namespace Elements
{
  public class PassiveSealAction : ActionParameter
  {
    private const int DISPLAY_COUNT_VALUE = 1;

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
      Dictionary<eValueNumber, float> _valueDictionary)
    {
      base.ExecAction(_source, _target, _num, _sourceActionController, _skill, _starttime, _enabledChildAction, _valueDictionary);
      PassiveSealData _data = new PassiveSealData()
      {
        DisplayCount = this.ActionDetail2 == 1,
        SealTarget = (PassiveSealAction.eSealTarget) this.ActionDetail3,
        Source = _source,
        Target = _target.Owner,
        TargetStateIcon = (eStateIconType) _valueDictionary[eValueNumber.VALUE_2],
        SealDuration = _valueDictionary[eValueNumber.VALUE_3],
        LifeTime = _valueDictionary[eValueNumber.VALUE_5],
        SealNumLimit = (int) _valueDictionary[eValueNumber.VALUE_1]
      };
      this.AppendIsAlreadyExeced(_target.Owner, _num);
      _source.AppendCoroutine(_data.Update(), ePauseType.SYSTEM);
      _target.Owner.AddPassiveSeal((PassiveSealAction.ePassiveTiming) this.ActionDetail1, _data);
    }

    public override void SetLevel(float _level)
    {
      base.SetLevel(_level);
      this.Value[eValueNumber.VALUE_3] = (float) ((double) this.MasterData.action_value_3 + (double) this.MasterData.action_value_4 * (double) _level);
      this.Value[eValueNumber.VALUE_5] = (float) ((double) this.MasterData.action_value_5 + (double) this.MasterData.action_value_6 * (double) _level);
    }

    public enum ePassiveTiming
    {
      BUFF = 1,
    }

    public enum eSealTarget
    {
      SOURCE,
    }
  }
}