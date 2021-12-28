// Decompiled with JetBrains decompiler
// Type: Elements.AttackSealAction
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using Cute;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Elements
{
  public class AttackSealAction : ActionParameter
  {
    private const int DISPLAY_COUNT_NUM = 1;

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
      base.ExecAction(_source, _target, _num, _sourceActionController, _skill, _starttime, _enabledChildAction, _valueDictionary);
      this.AppendIsAlreadyExeced(_target.Owner, _num);
      eStateIconType key = (eStateIconType) (float)_valueDictionary[eValueNumber.VALUE_2];
      if (!_target.Owner.SealDictionary.ContainsKey(key))
      {
        SealData sealData = new SealData()
        {
          Max = (int) _valueDictionary[eValueNumber.VALUE_1],
          DisplayCount = this.ActionDetail2 == 1
        };
        _target.Owner.SealDictionary.Add(key, sealData);
      }
      Dictionary<UnitCtrl, Dictionary<int, AttackSealData>> dataDictionary = (Dictionary<UnitCtrl, Dictionary<int, AttackSealData>>) null;
      switch ((AttackSealAction.eExecConditionType) this.ActionDetail1)
      {
        case AttackSealAction.eExecConditionType.DAMAGE_ONCE:
          if (this.ActionDetail3 == 1)
          {
            dataDictionary = _target.Owner.DamageOnceOwnerSealDateDictionary;
            break;
          }
          break;
        case AttackSealAction.eExecConditionType.HIT:
        case AttackSealAction.eExecConditionType.CRITICAL:
          switch ((AttackSealAction.eSealTarget) this.ActionDetail3)
          {
            case AttackSealAction.eSealTarget.TARGET:
              dataDictionary = _target.Owner.DamageSealDataDictionary;
              break;
            case AttackSealAction.eSealTarget.OWNER:
              dataDictionary = _target.Owner.DamageOwnerSealDataDictionary;
              break;
          }
          break;
      }
      if (dataDictionary.ContainsKey(_source) && dataDictionary[_source].ContainsKey(this.ActionId))
      {
        dataDictionary[_source][this.ActionId].LimitTime = _valueDictionary[eValueNumber.VALUE_3];
      }
      else
      {
        AttackSealData _attackSealData = new AttackSealData()
        {
          LimitTime = _valueDictionary[eValueNumber.VALUE_3],
          IconType = key,
          ActionId = this.ActionId,
          OnlyCritical = this.ActionDetail1 == 4
        };
        if (!dataDictionary.ContainsKey(_source))
          dataDictionary.Add(_source, new Dictionary<int, AttackSealData>()
          {
            {
              this.ActionId,
              _attackSealData
            }
          });
        else
          dataDictionary[_source].Add(this.ActionId, _attackSealData);
        _sourceActionController.AppendCoroutine(this.updateAttackSealData(_attackSealData, _source, (Action) (() => dataDictionary[_source].Remove(this.ActionId))), ePauseType.SYSTEM, _source);
      }
    }

    private IEnumerator updateAttackSealData(
      AttackSealData _attackSealData,
      UnitCtrl _unitCtrl,
      Action _callback)
    {
      while (!_unitCtrl.IdleOnly)
      {
        _attackSealData.LimitTime -= _unitCtrl.DeltaTimeForPause;
        if ((double) _attackSealData.LimitTime < 0.0)
        {
          _callback.Call();
          break;
        }
        yield return (object) null;
      }
    }

    public override void SetLevel(float _level)
    {
      base.SetLevel(_level);
      this.Value[eValueNumber.VALUE_3] = (float) ((double) this.MasterData.action_value_3 + (double) this.MasterData.action_value_4 * (double) _level);
    }

    public enum eExecConditionType
    {
      DAMAGE_ONCE = 1,
      TARGET = 2,
      HIT = 3,
      CRITICAL = 4,
    }

    public enum eSealTarget
    {
      TARGET,
      OWNER,
    }
  }
}
