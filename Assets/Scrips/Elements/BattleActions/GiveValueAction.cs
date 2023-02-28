// Decompiled with JetBrains decompiler
// Type: Elements.GiveValueAction
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System.Collections.Generic;
using Elements.Battle;

namespace Elements
{
    public class GiveValueAction : ActionParameter, ISingletonField
    {
        protected BasePartsData parts;
        protected BasePartsData parameterParts;

        public override void ExecActionOnStart(
      Skill _skill,
      UnitCtrl _source,
      UnitActionController _sourceActionController)
        {
            base.ExecActionOnStart(_skill, _source, _sourceActionController);
            switch ((eAdditiveValueType)(float)Value[eValueNumber.VALUE_1])
            {
                case eAdditiveValueType.DEFEAT_NUMBER:
                    ActionParameter actionParameter1 = _skill.ActionParameters[0];
                    int index = 0;
                    for (int count = _skill.ActionParameters.Count; index < count; ++index)
                    {
                        ActionParameter actionParameter2 = _skill.ActionParameters[index];
                        actionParameter2.OnDefeatEnemy += () => ++_skill.DefeatEnemyCount;
                        if (actionParameter2.ActionId != ActionDetail2 && actionParameter2.ActionId != ActionDetail3)
                            actionParameter1 = actionParameter1.ExecTime[actionParameter1.ExecTime.Length - 1] > (double)actionParameter2.ExecTime[actionParameter2.ExecTime.Length - 1] ? actionParameter1 : actionParameter2;
                    }
                    break;
                case eAdditiveValueType.AHEAD_UNIT_NUM:
                case eAdditiveValueType.BEHIND_UNIT_NUM:
                    parts = _source.IsPartsBoss ? _source.BossPartsListForBattle.Find(e => e.Index == _skill.ParameterTarget) : _source.GetFirstParts();
                    break;
            }
            if (base.Value[eValueNumber.VALUE_6] != 0f)
            {
                parameterParts = (_source.IsPartsBoss ? _source.BossPartsListForBattle.Find((PartsData e) => e.Index == (int)base.Value[eValueNumber.VALUE_6]) : _source.GetFirstParts());
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
            eValueNumber eValueNumber = (eValueNumber)(ActionDetail2 - 1);
            _addValue.Add(eValueNumber, 0.0f);
            createValue(_source, _skill, _valueDictionary, _addValue, eValueNumber, _target);
            BattleLogIntreface battleLog = this.battleLog;
            UnitCtrl unitCtrl = _source;
            UnitCtrl owner = _target.Owner;
            int actionId = ActionId;
            UnitCtrl JELADBAMFKH = unitCtrl;
            UnitCtrl LIMEKPEENOB = owner;
            battleLog.AppendBattleLog(eBattleLogType.GIVE_VALUE_ADDITIONAL, 0, 0L, 0L, 0, actionId, JELADBAMFKH: JELADBAMFKH, LIMEKPEENOB: LIMEKPEENOB);
            setValue(_addValue, _skill.ActionParameters.Find(e => e.ActionId == ActionDetail1));
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
                    _addValue[_evalue] = calcHpValue(_source, _valueDictionary);
                    break;
                case 1:
                    _addValue[_evalue] = calcDamageValue(_source, _valueDictionary);
                    break;
                case 2:
                    _addValue[_evalue] = (float)_skill.DefeatEnemyCount * _valueDictionary[eValueNumber.VALUE_2];
                    break;
                case 3:
                    break;
                case 4:
                    _addValue[_evalue] = (float)TargetList.Count * _valueDictionary[eValueNumber.VALUE_2];
                    break;
                case 5:
                    _addValue[_evalue] = (float)_skill.DamagedPartsList.Count * _valueDictionary[eValueNumber.VALUE_2];
                    break;
                case 6:
                    _addValue[_evalue] = _skill.TotalDamage * _valueDictionary[eValueNumber.VALUE_2];
                    break;
                case 7:
                    //_addValue[_evalue] = BattleUtil.FloatToInt(_target.Owner.Atk) * _valueDictionary[eValueNumber.VALUE_2];
                    if (_target.Owner == _source && parameterParts != null)
                    {
                        _addValue[_evalue] = parameterParts.GetAtkZero() * _valueDictionary[eValueNumber.VALUE_2];
                    }
                    else
                    {
                        _addValue[_evalue] = _target.Owner.AtkZero.Floor() * _valueDictionary[eValueNumber.VALUE_2];
                    }
                    break;
                case 8:
                    //_addValue[_evalue] = BattleUtil.FloatToInt(_target.Owner.MagicStr) * _valueDictionary[eValueNumber.VALUE_2];
                    if (_target.Owner == _source && parameterParts != null)
                    {
                        _addValue[_evalue] = parameterParts.GetMagicStrZero() * _valueDictionary[eValueNumber.VALUE_2];
                    }
                    else
                    {
                        _addValue[_evalue] = _target.Owner.MagicStrZero.Floor() * _valueDictionary[eValueNumber.VALUE_2];
                    }
                    break;
                case 9:
                    //_addValue[_evalue] = (float)(int)_target.Owner.Def * _valueDictionary[eValueNumber.VALUE_2];
                    if (_target.Owner == _source && parameterParts != null)
                    {
                        _addValue[_evalue] = parameterParts.GetDefZero() * _valueDictionary[eValueNumber.VALUE_2];
                    }
                    else
                    {
                        _addValue[_evalue] = _target.Owner.DefZero.Floor() * _valueDictionary[eValueNumber.VALUE_2];
                    }
                    break;
                case 10:
                    //_addValue[_evalue] = (float)(int)_target.Owner.MagicDef * _valueDictionary[eValueNumber.VALUE_2];
                    if (_target.Owner == _source && parameterParts != null)
                    {
                        _addValue[_evalue] = parameterParts.GetMagicDefZero() * _valueDictionary[eValueNumber.VALUE_2];
                    }
                    else
                    {
                        _addValue[_evalue] = _target.Owner.MagicDefZero.Floor() * _valueDictionary[eValueNumber.VALUE_2];
                    }
                    break;
                case 11:
                    List<UnitCtrl> _targetList1 = !_source.IsOther ? battleManager.UnitList : battleManager.EnemyList;
                    _addValue[_evalue] = (float)countUnitNumCompareX(_targetList1, parts, true) * _valueDictionary[eValueNumber.VALUE_2];
                    break;
                case 12:
                    List<UnitCtrl> _targetList2 = !_source.IsOther ? battleManager.UnitList : battleManager.EnemyList;
                    _addValue[_evalue] = (float)countUnitNumCompareX(_targetList2, parts, false) * _valueDictionary[eValueNumber.VALUE_2];
                    break;
                case 13:
                    {
                        long num = _target.Owner.MaxHp;
                        float value = (float)(num - (long)_target.Owner.Hp) / (float)num * _valueDictionary[eValueNumber.VALUE_2];
                        _addValue[_evalue] = BattleUtil.FloatToPerMille(value);
                        return;
                    }
                default:
                    if (_valueDictionary[eValueNumber.VALUE_1] > 200f)
                    {
                        eStateIconType eStateIconType = eStateIconType.INVALID_VALUE;
                        eStateIconType = ((!(_valueDictionary[eValueNumber.VALUE_1] > 2000f)) ? ((eStateIconType)(_valueDictionary[eValueNumber.VALUE_1] % 200f)) : ((eStateIconType)(_valueDictionary[eValueNumber.VALUE_1] % 2000f)));
                        SealData value = null;
                        if (_target.Owner.SealDictionary.TryGetValue(eStateIconType, out value))
                        {
                            _addValue[_evalue] = (float)value.GetCurrentCount() * _valueDictionary[eValueNumber.VALUE_2];
                        }
                    }
                    else if (_valueDictionary[eValueNumber.VALUE_1] > 100f)
                    {
                        StrikeBackDataSet value2 = null;
                        if (_target.Owner.StrikeBackDictionary.TryGetValue((EnchantStrikeBackAction.eStrikeBackEffectType)((int)_valueDictionary[eValueNumber.VALUE_1] - 100), out value2))
                        {
                            _addValue[_evalue] = (float)value2.DataList.Count * _valueDictionary[eValueNumber.VALUE_2];
                        }
                    }
                    else if (_valueDictionary[eValueNumber.VALUE_1] >= 20f)
                    {
                        int key = (int)_valueDictionary[eValueNumber.VALUE_1] % 10;
                        if (_source.SkillExecCountDictionary.TryGetValue(key, out var value3))
                        {
                            _addValue[_evalue] = (float)value3 * _valueDictionary[eValueNumber.VALUE_2];
                        }
                    }

                    break;
            }
        }


        protected virtual void setValue(Dictionary<eValueNumber, FloatWithEx> _value, ActionParameter _action)
        {
        }

        protected virtual FloatWithEx calcDamageValue(
            UnitCtrl _source,
            Dictionary<eValueNumber, FloatWithEx> _valueDictionary) => ((float)_source.MaxHp - _source.Hp) / (float)_source.MaxHp * _valueDictionary[eValueNumber.VALUE_2];

        protected virtual FloatWithEx calcHpValue(
            UnitCtrl _source,
            Dictionary<eValueNumber, FloatWithEx> _valueDictionary) => _source.Hp / (float)_source.MaxHp * _valueDictionary[eValueNumber.VALUE_2];
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
                        if (comparePosition(_source, partsData, _ahead))
                            ++num;
                    }
                }
                else if (comparePosition(_source, target.GetFirstParts(), _ahead))
                    ++num;
            }
            return num;
        }

        private bool comparePosition(BasePartsData _source, BasePartsData _target, bool _ahead)
        {
            if (_source == _target || (long)_target.Owner.Hp == 0L || _target.Owner.IsStealth || _target.Owner.IsSummonOrPhantom && _target.Owner.IdleOnly)
                return false;
            float x1 = _source.GetBottomTransformPosition().x;
            float x2 = _target.GetBottomTransformPosition().x;
            if (BattleUtil.Approximately(x1, x2))
                return true;
            return !_source.Owner.IsLeftDir ? x1 < (double)x2 == _ahead : x1 > (double)x2 == _ahead;
        }

        public enum eAdditiveValueType
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
            SEAL_COUNT_THREE_DIGIT = 2000

        }
    }
}