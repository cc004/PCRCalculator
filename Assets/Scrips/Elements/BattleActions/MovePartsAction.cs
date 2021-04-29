// Decompiled with JetBrains decompiler
// Type: Elements.MovePartsAction
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

namespace Elements
{
  public class MovePartsAction : ActionParameter
  {
    private BasePartsData parts;

    public override void ExecActionOnStart(
      Skill _skill,
      UnitCtrl _source,
      UnitActionController _sourceActionController)
    {
      base.ExecActionOnStart(_skill, _source, _sourceActionController);
      this.parts = (BasePartsData) _source.BossPartsListForBattle.Find((Predicate<PartsData>) (e => (double) e.Index == (double) this.Value[eValueNumber.VALUE_4]));
    }

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
      if (_num % 2 == 0)
      {
        this.parts.PositionX += _valueDictionary[eValueNumber.VALUE_1];
        this.parts.BodyWidthValue = 0.0f;
        this.parts.Owner.PartsMotionPrefix = (int) _valueDictionary[eValueNumber.VALUE_5];
        this.OnActionEnd = (ActionParameter.OnActionEndDelegate) (() =>
        {
          this.parts.Owner.PartsMotionPrefix = 0;
          this.parts.PositionX = this.parts.InitialPositionX;
          this.OnActionEnd = (ActionParameter.OnActionEndDelegate) null;
        });
      }
      else
      {
        this.parts.Owner.PartsMotionPrefix = 0;
        this.parts.BodyWidthValue = 300f;
        this.parts.PositionX = this.parts.InitialPositionX;
        this.OnActionEnd = (ActionParameter.OnActionEndDelegate) null;
      }
    }
  }
}
