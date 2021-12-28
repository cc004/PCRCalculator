// Decompiled with JetBrains decompiler
// Type: Elements.GiveValueAction
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using Elements.Battle;
using System;
using System.Collections.Generic;

namespace Elements
{
  public class GiveValueAction : ActionParameter, ISingletonField
  {
    protected BasePartsData parts;

    public override void ExecActionOnStart(
      Skill _skill,
      UnitCtrl _source,
      UnitActionController _sourceActionController)
    {
      base.ExecActionOnStart(_skill, _source, _sourceActionController);
      switch ((GiveValueAction.eAdditiveValueType)(float)this.Value[eValueNumber.VALUE_1])
      {
        case GiveValueAction.eAdditiveValueType.DEFEAT_NUMBER:
          ActionParameter actionParameter1 = _skill.ActionParameters[0];
          int index = 0;
          for (int count = _skill.ActionParameters.Count; index < count; ++index)
          {
            ActionParameter actionParameter2 = _skill.ActionParameters[index];
            actionParameter2.OnDefeatEnemy += (Action) (() => ++_skill.DefeatEnemyCount);
            if (actionParameter2.ActionId != this.ActionDetail2 && actionParameter2.ActionId != this.ActionDetail3)
              actionParameter1 = (double) actionParameter1.ExecTime[actionParameter1.ExecTime.Length - 1] > (double) actionParameter2.ExecTime[actionParameter2.ExecTime.Length - 1] ? actionParameter1 : actionParameter2;
          }
          break;
        case GiveValueAction.eAdditiveValueType.AHEAD_UNIT_NUM:
        case GiveValueAction.eAdditiveValueType.BEHIND_UNIT_NUM:
          this.parts = _source.IsPartsBoss ? (BasePartsData) _source.BossPartsListForBattle.Find((Predicate<PartsData>) (e => e.Index == _skill.ParameterTarget)) : _source.GetFirstParts();
          break;
      }
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
      base.ExecAction(_source, _target, _num, _sourceActionController, _skill, _starttime, _enabledChildAction, _valueDictionary);
      Dictionary<eValueNumber, FloatWithEx> _addValue = new Dictionary<eValueNumber, FloatWithEx>();
      eValueNumber eValueNumber = (eValueNumber)(this.ActionDetail2 - 1);
      _addValue.Add(eValueNumber, 0.0f);
      this.createValue(_source, _skill, _valueDictionary, _addValue, eValueNumber, _target);
      BattleLogIntreface battleLog = this.battleLog;
      UnitCtrl unitCtrl = _source;
      UnitCtrl owner = _target.Owner;
      int actionId = this.ActionId;
      UnitCtrl JELADBAMFKH = unitCtrl;
      UnitCtrl LIMEKPEENOB = owner;
      battleLog.AppendBattleLog(eBattleLogType.GIVE_VALUE_ADDITIONAL, 0, 0L, 0L, 0, actionId, JELADBAMFKH: JELADBAMFKH, LIMEKPEENOB: LIMEKPEENOB);
      this.setValue(_addValue, _skill.ActionParameters.Find((Predicate<ActionParameter>) (e => e.ActionId == this.ActionDetail1)));
    }

        /*protected virtual void createValue(
          UnitCtrl _source,
          Skill _skill,
          Dictionary<eValueNumber, FloatWithEx> _valueDictionary,
          Dictionary<eValueNumber, FloatWithEx> _addValue,
          eValueNumber _evalue,
          BasePartsData _target)
        {
          switch ((int) _valueDictionary[eValueNumber.VALUE_1])
          {
            case 0:
              _addValue[_evalue] = this.calcHpValue(_source, _valueDictionary);
              break;
            case 1:
              _addValue[_evalue] = this.calcDamageValue(_source, _valueDictionary);
              break;
            case 2:
              _addValue[_evalue] = (float) _skill.DefeatEnemyCount * _valueDictionary[eValueNumber.VALUE_2];
              break;
            case 3:
              break;
            case 4:
              _addValue[_evalue] = (float) this.TargetList.Count * _valueDictionary[eValueNumber.VALUE_2];
              break;
            case 5:
              _addValue[_evalue] = (float) _skill.DamagedPartsList.Count * _valueDictionary[eValueNumber.VALUE_2];
              break;
            case 6:
              _addValue[_evalue] = (float) _skill.TotalDamage * _valueDictionary[eValueNumber.VALUE_2];
              break;
            case 7:
              _addValue[_evalue] = (float) (int) _target.Owner.Atk * _valueDictionary[eValueNumber.VALUE_2];
              break;
            case 8:
              _addValue[_evalue] = (float) (int) _target.Owner.MagicStr * _valueDictionary[eValueNumber.VALUE_2];
              break;
            case 9:
              _addValue[_evalue] = (float) (int) _target.Owner.Def * _valueDictionary[eValueNumber.VALUE_2];
              break;
            case 10:
              _addValue[_evalue] = (float) (int) _target.Owner.MagicDef * _valueDictionary[eValueNumber.VALUE_2];
              break;
            case 11:
              List<UnitCtrl> _targetList1 = !_source.IsOther ? this.battleManager.UnitList : this.battleManager.EnemyList;
              _addValue[_evalue] = (float) this.countUnitNumCompareX(_targetList1, this.parts, true) * _valueDictionary[eValueNumber.VALUE_2];
              break;
            case 12:
              List<UnitCtrl> _targetList2 = !_source.IsOther ? this.battleManager.UnitList : this.battleManager.EnemyList;
              _addValue[_evalue] = (float) this.countUnitNumCompareX(_targetList2, this.parts, false) * _valueDictionary[eValueNumber.VALUE_2];
              break;
            default:
              if ((double) _valueDictionary[eValueNumber.VALUE_1] > 200.0)
              {
                eStateIconType key = (eStateIconType) ((double) _valueDictionary[eValueNumber.VALUE_1] % 200.0);
                SealData sealData = (SealData) null;
                if (!_target.Owner.SealDictionary.TryGetValue(key, out sealData))
                  break;
                _addValue[_evalue] = (float) sealData.GetCurrentCount() * _valueDictionary[eValueNumber.VALUE_2];
                break;
              }
              if ((double) _valueDictionary[eValueNumber.VALUE_1] <= 100.0)
                break;
              StrikeBackDataSet strikeBackDataSet = (StrikeBackDataSet) null;
              if (!_target.Owner.StrikeBackDictionary.TryGetValue((EnchantStrikeBackAction.eStrikeBackEffectType) ((int) _valueDictionary[eValueNumber.VALUE_1] - 100), out strikeBackDataSet))
                break;
              _addValue[_evalue] = (float) strikeBackDataSet.DataList.Count * _valueDictionary[eValueNumber.VALUE_2];
              break;
          }
        }*/
        protected virtual void createValue(
      UnitCtrl _source,
      Skill _skill,
      Dictionary<eValueNumber, FloatWithEx> _valueDictionary,
      Dictionary<eValueNumber, FloatWithEx> _addValue,
      eValueNumber _evalue,
      BasePartsData _target)
        {
            switch ((int)_valueDictionary[eValueNumber.VALUE_1])
            {
                case 0:
                    _addValue[_evalue] = this.calcHpValue(_source, _valueDictionary);
                    break;
                case 1:
                    _addValue[_evalue] = this.calcDamageValue(_source, _valueDictionary);
                    break;
                case 2:
                    _addValue[_evalue] = (float)_skill.DefeatEnemyCount * _valueDictionary[eValueNumber.VALUE_2];
                    break;
                case 3:
                    break;
                case 4:
                    _addValue[_evalue] = (float)this.TargetList.Count * _valueDictionary[eValueNumber.VALUE_2];
                    break;
                case 5:
                    _addValue[_evalue] = (float)_skill.DamagedPartsList.Count * _valueDictionary[eValueNumber.VALUE_2];
                    break;
                case 6:
                    _addValue[_evalue] = new FloatWithEx
                    {
                        ex = _skill.ExDamage,
                        value = _skill.TotalDamage
                    } * _valueDictionary[eValueNumber.VALUE_2];
                    break;
                case 7:
                    _addValue[_evalue] = (float)(int)_target.Owner.Atk * _valueDictionary[eValueNumber.VALUE_2];
                    break;
                case 8:
                    _addValue[_evalue] = (float)(int)_target.Owner.MagicStr * _valueDictionary[eValueNumber.VALUE_2];
                    break;
                case 9:
                    _addValue[_evalue] = (float)(int)_target.Owner.Def * _valueDictionary[eValueNumber.VALUE_2];
                    break;
                case 10:
                    _addValue[_evalue] = (float)(int)_target.Owner.MagicDef * _valueDictionary[eValueNumber.VALUE_2];
                    break;
                case 11:
                    List<UnitCtrl> _targetList1 = !_source.IsOther ? this.battleManager.UnitList : this.battleManager.EnemyList;
                    _addValue[_evalue] = (float)this.countUnitNumCompareX(_targetList1, this.parts, true) * _valueDictionary[eValueNumber.VALUE_2];
                    break;
                case 12:
                    List<UnitCtrl> _targetList2 = !_source.IsOther ? this.battleManager.UnitList : this.battleManager.EnemyList;
                    _addValue[_evalue] = (float)this.countUnitNumCompareX(_targetList2, this.parts, false) * _valueDictionary[eValueNumber.VALUE_2];
                    break;
                default:
                    if ((double)_valueDictionary[eValueNumber.VALUE_1] > 200.0)
                    {
                        eStateIconType key = (double)_valueDictionary[eValueNumber.VALUE_1] <= 2000.0 ? (eStateIconType)((double)_valueDictionary[eValueNumber.VALUE_1] % 200.0) : (eStateIconType)((double)_valueDictionary[eValueNumber.VALUE_1] % 2000.0);
                        SealData sealData = (SealData)null;
                        if (!_target.Owner.SealDictionary.TryGetValue(key, out sealData))
                            break;
                        _addValue[_evalue] = (float)sealData.GetCurrentCount() * _valueDictionary[eValueNumber.VALUE_2];
                        break;
                    }
                    if ((double)_valueDictionary[eValueNumber.VALUE_1] <= 100.0)
                        break;
                    StrikeBackDataSet strikeBackDataSet = (StrikeBackDataSet)null;
                    if (!_target.Owner.StrikeBackDictionary.TryGetValue((EnchantStrikeBackAction.eStrikeBackEffectType)((int)_valueDictionary[eValueNumber.VALUE_1] - 100), out strikeBackDataSet))
                        break;
                    break;
            }
        }


        protected virtual void setValue(Dictionary<eValueNumber, FloatWithEx> _value, ActionParameter _action)
    {
    }

    protected virtual float calcHpValue(
      UnitCtrl _source,
      Dictionary<eValueNumber, FloatWithEx> _valueDictionary) => 0.0f;

    protected virtual float calcDamageValue(
      UnitCtrl _source,
      Dictionary<eValueNumber, FloatWithEx> _valueDictionary) => 0.0f;

    protected int countUnitNumCompareX(
      List<UnitCtrl> _targetList,
      BasePartsData _source,
      bool _ahead)
    {
      int num = 0;
      foreach (UnitCtrl target in _targetList)
      {
        if (target.IsPartsBoss)
        {
          foreach (PartsData partsData in target.BossPartsListForBattle)
          {
            if (this.comparePosition(_source, (BasePartsData) partsData, _ahead))
              ++num;
          }
        }
        else if (this.comparePosition(_source, target.GetFirstParts(), _ahead))
          ++num;
      }
      return num;
    }

    private bool comparePosition(BasePartsData _source, BasePartsData _target, bool _ahead)
    {
      if (_source == _target || (long) _target.Owner.Hp == 0L || _target.Owner.IsStealth || _target.Owner.IsSummonOrPhantom && _target.Owner.IdleOnly)
        return false;
      float x1 = _source.GetBottomTransformPosition().x;
      float x2 = _target.GetBottomTransformPosition().x;
      if (BattleUtil.Approximately(x1, x2))
        return true;
      return !_source.Owner.IsLeftDir ? (double) x1 < (double) x2 == _ahead : (double) x1 > (double) x2 == _ahead;
    }

    protected enum eAdditiveValueType
    {
      HP = 0,
      DAMAGE = 1,
      DEFEAT_NUMBER = 2,
      SEAL = 3,
      TARGET = 4,
      DAMAGED_TARGET = 5,
      SKILL_DAMAGE = 6,
      TARGET_ATK = 7,
      TARGET_MAGIC_ATK = 8,
      TARGET_DEF = 9,
      TARGET_MAGIC_DEF = 10, // 0x0000000A
      AHEAD_UNIT_NUM = 11, // 0x0000000B
      BEHIND_UNIT_NUM = 12, // 0x0000000C
      STRIKE_BACK_COUNT = 100, // 0x00000064
      SEAL_COUNT = 200, // 0x000000C8
    }
  }
}
