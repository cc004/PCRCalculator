// Decompiled with JetBrains decompiler
// Type: Elements.AttackAction
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Diagnostics;
using Elements.Battle;
using PCRCaculator.Guild;
using UnityEngine;

namespace Elements
{
  public class AttackAction : AttackActionBase
  {
    private const int DAMAGE_BASE = 100;
        private enum eDecideTargetAtkType
        {
            SELECT_ATTACK_TYPE,
            TARGET_LOWER_DEF_TYPE
        }
        public override void ExecAction(
          UnitCtrl _source,
          BasePartsData _target,
          int _num,
          UnitActionController _sourceActionController,
          Skill _skill,
          float _starttime,
          Dictionary<int, bool> _enabledChildAction,
          Dictionary<eValueNumber, FloatWithEx> _valueDictionary,
          Action<string> action = null)
        {
            if (BattleHeaderController.CurrentFrameCount == 4846 && _source.UnitId == 107101)
            {
                Debugger.Break();
            }
            _target.IncrementUbAttackHitCount();
            eAttackType actionDetail1 = (eAttackType)ActionDetail1;
            if (_num == 0)
            {
                if (_target.Owner.IsAbnormalState(UnitCtrl.eAbnormalState.DAMAGE_LIMIT_ATK))
                {
                    base.LimitDamageDictionaryAtk[_target] = (long)_target.Owner.GetAbnormalStateMainValue(UnitCtrl.eAbnormalStateCategory.DAMAGE_LIMIT_ATK);
                }
                if (_target.Owner.IsAbnormalState(UnitCtrl.eAbnormalState.DAMAGE_LIMIT_MGC))
                {
                    base.LimitDamageDictionaryMgc[_target] = (long)_target.Owner.GetAbnormalStateMainValue(UnitCtrl.eAbnormalStateCategory.DAMAGE_LIMIT_MGC);
                }
                if (_target.Owner.IsAbnormalState(UnitCtrl.eAbnormalState.DAMAGE_LIMIT_ALL))
                {
                    if (base.LimitDamageDictionaryAtk.ContainsKey(_target))
                    {
                        base.LimitDamageDictionaryAtk[_target] = (long)Mathf.Min(_target.Owner.GetAbnormalStateMainValue(UnitCtrl.eAbnormalStateCategory.DAMAGE_LIMIT_ALL), base.LimitDamageDictionaryAtk[_target]);
                    }
                    else
                    {
                        base.LimitDamageDictionaryAtk.Add(_target, (long)_target.Owner.GetAbnormalStateMainValue(UnitCtrl.eAbnormalStateCategory.DAMAGE_LIMIT_ALL));
                    }
                    if (base.LimitDamageDictionaryMgc.ContainsKey(_target))
                    {
                        base.LimitDamageDictionaryMgc[_target] = (long)Mathf.Min(_target.Owner.GetAbnormalStateMainValue(UnitCtrl.eAbnormalStateCategory.DAMAGE_LIMIT_ALL), base.LimitDamageDictionaryMgc[_target]);
                    }
                    else
                    {
                        base.LimitDamageDictionaryMgc.Add(_target, (long)_target.Owner.GetAbnormalStateMainValue(UnitCtrl.eAbnormalStateCategory.DAMAGE_LIMIT_ALL));
                    }
                }
            }

            if (!HitOnceDic.ContainsKey(_target))
            {
                HitOnceDic.Add(_target, false);
                HitOnceKeyList.Add(_target);
            }
            bool flag = judgeMiss(_target, _source, _num, _sourceActionController, _skill, _starttime, _enabledChildAction, actionDetail1);
            if (flag && base.battleManager.FEDKJAIEDGI == 0f)
            {
                //add scripts
                string describe = "MISS";
                action?.Invoke(describe);
                //end add
                return;
            }

            bool _isCritical = (double)_valueDictionary[eValueNumber.VALUE_5] == _num + 1;
            bool isPhysicalForTarget = judgeIsPhysical(actionDetail1);
            eDecideTargetAtkType actionDetail2 = (eDecideTargetAtkType)base.ActionDetail2;
            if (actionDetail2 == eDecideTargetAtkType.TARGET_LOWER_DEF_TYPE)
            {
                int defZero = _target.GetDefZero();
                int magicDefZero = _target.GetMagicDefZero();
                if (defZero != magicDefZero)
                {
                    isPhysicalForTarget = defZero < magicDefZero;
                }
            }
            DamageData damageData = createDamageData(_source, _target, _num, _valueDictionary, actionDetail1, _isCritical, _skill, eActionType.ATTACK,isPhysicalForTarget);
            if (!TotalDamageDictionary.ContainsKey(_target))
            {
                FloatWithEx num1 = 0;
                List<CriticalData> criticalDataList = new List<CriticalData>();
                for (int index = 0; index < ActionExecTimeList.Count; ++index)
                {
                    CriticalData criticalData = new CriticalData();
                    var data = new RandomData(_source, _target.Owner, ActionId, 1, damageData.CriticalRate,
                        damageData.CriticalDamageRate);
                    double num2 = BattleManager.Random(0.0f, 1f,
                        data);


                    if (MyGameCtrl.Instance.tempData.isGuildBattle && (MyGameCtrl.Instance.tempData.randomData.ForceNoCritical_player && _target.Owner.IsOther || MyGameCtrl.Instance.tempData.randomData.ForceNoCritical_enemy && !_target.Owner.IsOther))
                        num2 = 1;
                    if (MyGameCtrl.Instance.tempData.isGuildBattle && MyGameCtrl.Instance.tempData.randomData.ForceCritical_player && _target.Owner.IsOther)
                        num2 = 0;
                    //start add
                    if (MyGameCtrl.Instance.tempData.isGuildBattle && MyGameCtrl.Instance.tempData.randomData.TryJudgeRandomSpecialSetting(damageData.Source, _target.Owner, _skill, eActionType.ATTACK, BattleHeaderController.CurrentFrameCount, out float fix))
                    {
                        num2 = fix;
                    }

                    criticalData.ExpectedDamage = BattleUtil.FloatToInt(damageData.TotalDamageForLogBarrier * ActionExecTimeList[index].Weight / ActionWeightSum);
                    double criticalRate = damageData.CriticalRate;
                    //if (num2 <= criticalRate && damageData.CriticalDamageRate != 0.0)
                    if (_valueDictionary[eValueNumber.VALUE_5] == (float) (index + 1))
                    {
                        criticalData.critVar = 1f;
                        criticalData.IsCritical = true;
                    }
                    else if (damageData.CriticalDamageRate != 0.0 && damageData.CriticalRateForLogBarrier != 0f)
                    {
                        if (num2 <= criticalRate)
                            criticalData.IsCritical = true;
                        criticalData.critVar = FloatWithEx.Binomial(damageData.CriticalRate, criticalData.IsCritical,
                            $"rnd:{data.id}:{(int) (criticalRate * 1000)}");
                    }
                    else
                        criticalData.critVar = 0f;

                    criticalData.ExpectedDamage *= 1f + criticalData.critVar * (2f * damageData.CriticalDamageRate - 1);

                    criticalData.ExpectedDamage = BattleUtil.FloatToInt(criticalData.ExpectedDamage);
                    if (!damageData.IgnoreDef)
                    {
                        switch (damageData.DamageType)
                        {
                            case DamageData.eDamageType.ATK:
                                var defZero = (float)damageData.Target.GetDefZero();
                                var num3 = Mathf.Max(0.0f, defZero - damageData.DefPenetrate);
                                criticalData.ExpectedDamage = (criticalData.ExpectedDamage * (1.0f - num3 / (defZero + 100.0f))).Floor();
                                //criticalData.ExpectedDamageNotCritical = (criticalData.ExpectedDamageNotCritical * (1f - num3 / (defZero + 100f))).Floor();

                                break;
                            case DamageData.eDamageType.MGC:
                                float magicDefZero = damageData.Target.GetMagicDefZero();
                                float num4 = Mathf.Max(0.0f, magicDefZero - damageData.DefPenetrate);
                                criticalData.ExpectedDamage = (criticalData.ExpectedDamage * (1.0f - num4 / (magicDefZero + 100.0f))).Floor();
                                //criticalData.ExpectedDamageNotCritical = (criticalData.ExpectedDamageNotCritical * (1f - num4 / (magicDefZero + 100f))).Floor();

                                break;
                        }
                    }
                    criticalData.CriticalRate = damageData.CriticalDamageRate;
                    num1 += criticalData.ExpectedDamage;
                    criticalDataList.Add(criticalData);
                }
                CriticalDataDictionary.Add(_target, criticalDataList);
                TotalDamageDictionary.Add(_target, num1);
            }
            damageData.critVar = CriticalDataDictionary[_target][_num].critVar;
            damageData.IsLogBarrierCritical = CriticalDataDictionary[_target][_num].IsCritical;
            damageData.LogBarrierExpectedDamage = CriticalDataDictionary[_target][_num].ExpectedDamage;
            damageData.TotalDamageForLogBarrier = TotalDamageDictionary[_target];
            if (flag)
            {
                _target.Owner.RecoverDodgeTP(damageData.DamageType, (int)damageData.Damage, damageData.ActionType, (int)base.CriticalDataDictionary[_target][_num].ExpectedDamageNotCritical, (int)damageData.TotalDamageForLogBarrier, damageData.Source, damageData.IgnoreDef, damageData.Target, damageData.DefPenetrate, null, _skill, EnergyChargeMultiple);
                return;
            }
            if (_target.Owner.SetDamage(damageData, true, ActionId, OnDamageHit, _skill: _skill, _onDefeat: OnDefeatEnemy, _damageWeight: ActionExecTimeList[_num].Weight, _damageWeightSum: ActionWeightSum, _energyChargeMultiple: EnergyChargeMultiple, callBack: action) != 0L)
                HitOnceDic[_target] = true;
            //if (_skill.AnimId != eSpineCharacterAnimeId.ATTACK)
            //    return;
            //_target.ShowHitEffect(_source.ToadDatas.Count > 0 ? _skill.WeaponType : _source.WeaponSeType, _skill, _source.IsLeftDir);
        }

    protected override float getCriticalDamageRate(Dictionary<eValueNumber, FloatWithEx> _valueDictionary) =>  _valueDictionary[eValueNumber.VALUE_6] == 0.0 ? 1f : (float)_valueDictionary[eValueNumber.VALUE_6];

    public override void SetLevel(float _level)
    {
      base.SetLevel(_level);
      Value[eValueNumber.VALUE_1] = (float) (MasterData.action_value_1 + MasterData.action_value_2 * _level);
      Value[eValueNumber.VALUE_3] = (float) (MasterData.action_value_3 + MasterData.action_value_4 * _level);
    }
  }
}
