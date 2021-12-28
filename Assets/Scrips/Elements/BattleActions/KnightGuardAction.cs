// Decompiled with JetBrains decompiler
// Type: Elements.KnightGuardAction
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Elements
{
  public class KnightGuardAction : ActionParameter
  {
    private BasePartsData parts;

    public override void ExecActionOnStart(
      Skill _skill,
      UnitCtrl _source,
      UnitActionController _sourceActionController)
    {
      base.ExecActionOnStart(_skill, _source, _sourceActionController);
      this.parts = (BasePartsData) _source.BossPartsListForBattle.Find((Predicate<PartsData>) (e => e.Index == _skill.ParameterTarget));
    }

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
      if ((long) _target.Owner.Hp <= 0L)
        return;
      KnightGuardAction.eValueType eValueType = (KnightGuardAction.eValueType)(float)_valueDictionary[eValueNumber.VALUE_1];
      float num = _source.IsPartsBoss ? (eValueType == KnightGuardAction.eValueType.PHYSICS ? (float) this.parts.GetAtkZero() : (float) this.parts.GetMagicStrZero()) : (float) (int) (eValueType == KnightGuardAction.eValueType.PHYSICS ? _source.AtkZero : _source.MagicStrZero);
      KnightGuardData _knightGuardData = new KnightGuardData()
      {
        Value = (int) ((double) _valueDictionary[eValueNumber.VALUE_2] + (double) _valueDictionary[eValueNumber.VALUE_4] * (double) num),
        LifeTime = _valueDictionary[eValueNumber.VALUE_6],
        KnightGuardType = (KnightGuardAction.eKnightGuardType) this.ActionDetail1,
        StateIconType = this.ActionDetail2 == 0 ? eStateIconType.INVALID_VALUE : (eStateIconType) this.ActionDetail2,
        InhibitHealType = eValueType == KnightGuardAction.eValueType.PHYSICS ? UnitCtrl.eInhibitHealType.PHYSICS : UnitCtrl.eInhibitHealType.MAGIC,
        Skill = _skill,
        Source = _source
      };
      /*if (this.ActionEffectList.Count != 0)
      {
        NormalSkillEffect actionEffect = this.ActionEffectList[0];
        GameObject MDOJNMEMHLN = _target.Owner.IsLeftDir ? actionEffect.PrefabLeft : actionEffect.Prefab;
        SkillEffectCtrl skillEffectCtrl = _knightGuardData.Effect = this.battleEffectPool.GetEffect(MDOJNMEMHLN);
        skillEffectCtrl.transform.parent = ExceptNGUIRoot.Transform;
        skillEffectCtrl.SortTarget = _target.Owner;
        skillEffectCtrl.InitializeSort();
        skillEffectCtrl.SetPossitionAppearanceType(actionEffect, _target, _target.Owner, _skill);
        skillEffectCtrl.ExecAppendCoroutine();
      }*/
      //if (this.ActionEffectList.Count > 1)
      //  _knightGuardData.ExecEffectData = this.ActionEffectList[1];
      this.AppendIsAlreadyExeced(_target.Owner, _num);
      _target.Owner.AddKnightGuard(_knightGuardData);
    }

    public override void SetLevel(float _level)
    {
      base.SetLevel(_level);
      this.Value[eValueNumber.VALUE_2] = (float) ((double) this.MasterData.action_value_2 + (double) this.MasterData.action_value_3 * (double) _level);
      this.Value[eValueNumber.VALUE_4] = (float) ((double) this.MasterData.action_value_4 + (double) this.MasterData.action_value_5 * (double) _level);
      this.Value[eValueNumber.VALUE_6] = (float) ((double) this.MasterData.action_value_6 + (double) this.MasterData.action_value_7 * (double) _level);
    }

    public enum eKnightGuardType
    {
      HEAL = 1,
    }

    public enum eValueType
    {
      PHYSICS = 1,
      MAGIC = 2,
    }
  }
}
