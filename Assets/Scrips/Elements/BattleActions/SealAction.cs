﻿// Decompiled with JetBrains decompiler
// Type: Elements.SealAction
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using Cute;
using Elements.Battle;
using System.Collections.Generic;
using UnityEngine;

namespace Elements
{
  public class SealAction : ActionParameter
  {
    private const int DISPLAY_COUNT_NUM = 1;

    public override void Initialize()
    {
      base.Initialize();
      //Singleton<LCEGKJFKOPD>.Instance.LoadSealActionEffect();
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
      Dictionary<eValueNumber, float> _valueDictionary)
    {
      base.ExecAction(_source, _target, _num, _sourceActionController, _skill, _starttime, _enabledChildAction, _valueDictionary);
      this.AppendIsAlreadyExeced(_target.Owner, _num);
      eStateIconType eStateIconType = (eStateIconType) _valueDictionary[eValueNumber.VALUE_2];
      if (!_target.Owner.SealDictionary.ContainsKey(eStateIconType))
      {
        SealData sealData = new SealData()
        {
          Max = (int) _valueDictionary[eValueNumber.VALUE_1],
          DisplayCount = this.ActionDetail1 == 1
        };
        _target.Owner.SealDictionary.Add(eStateIconType, sealData);
      }
      else
        _target.Owner.SealDictionary[eStateIconType].Max = Mathf.Max((int) _valueDictionary[eValueNumber.VALUE_1], _target.Owner.SealDictionary[eStateIconType].Max);
      if ((double) _valueDictionary[eValueNumber.VALUE_4] == 0.0)
        return;
      SealData seal = _target.Owner.SealDictionary[eStateIconType];
      if (seal.GetCurrentCount() == 0 && (double) _valueDictionary[eValueNumber.VALUE_4] > 0.0)
      {
        _target.Owner.OnChangeState.Call<UnitCtrl, eStateIconType, bool>(_target.Owner, eStateIconType, true);
                _target.Owner.MyOnChangeAbnormalState?.Invoke(_target.Owner, eStateIconType, true, 90, "NaN");

                /*if (this.ActionEffectList.Count != 0)
                {
                  NormalSkillEffect actionEffect = this.ActionEffectList[0];
                  GameObject MDOJNMEMHLN = _target.Owner.IsLeftDir ? actionEffect.PrefabLeft : actionEffect.Prefab;
                  SkillEffectCtrl skillEffectCtrl = seal.Effect = this.battleEffectPool.GetEffect(MDOJNMEMHLN);
                  skillEffectCtrl.transform.parent = ExceptNGUIRoot.Transform;
                  skillEffectCtrl.SortTarget = _target.Owner;
                  skillEffectCtrl.InitializeSort();
                  skillEffectCtrl.SetPossitionAppearanceType(actionEffect, _target, _target.Owner, _skill);
                  skillEffectCtrl.ExecAppendCoroutine();
                }*/
            }
            if ((double) _valueDictionary[eValueNumber.VALUE_4] > 0.0)
        seal.AddSeal(_valueDictionary[eValueNumber.VALUE_3], _target.Owner, eStateIconType, (int) _valueDictionary[eValueNumber.VALUE_4]);
      if ((double) _valueDictionary[eValueNumber.VALUE_4] >= 0.0)
        return;
      seal.RemoveSeal(-(int) _valueDictionary[eValueNumber.VALUE_4]);
    }
  }
}