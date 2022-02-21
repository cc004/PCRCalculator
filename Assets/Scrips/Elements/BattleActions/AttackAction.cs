// Decompiled with JetBrains decompiler
// Type: Elements.AttackAction
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using Elements.Battle;
using PCRCaculator.Guild;
using UnityEngine;

namespace Elements
{
  public class AttackAction : AttackActionBase
  {
    private const int DAMAGE_BASE = 100;

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
            _target.IncrementUbAttackHitCount();
            eAttackType actionDetail1 = (eAttackType)ActionDetail1;
            if (!HitOnceDic.ContainsKey(_target))
            {
                HitOnceDic.Add(_target, false);
                HitOnceKeyList.Add(_target);
            }
            if (judgeMiss(_target, _source, _num, _sourceActionController, _skill, _starttime, _enabledChildAction, actionDetail1))
            {
                //add scripts
                string describe = "MISS";
                action?.Invoke(describe);
                //end add
                return;
            }

            bool _isCritical = (double)_valueDictionary[eValueNumber.VALUE_5] == _num + 1;
            DamageData damageData = createDamageData(_source, _target, _num, _valueDictionary, actionDetail1, _isCritical, _skill, eActionType.ATTACK);
            if (!TotalDamageDictionary.ContainsKey(_target))
            {
                FloatWithEx num1 = 0;
                List<CriticalData> criticalDataList = new List<CriticalData>();
                for (int index = 0; index < ActionExecTimeList.Count; ++index)
                {
                    CriticalData criticalData = new CriticalData();
                    double num2 = BattleManager.Random(0.0f, 1f,
                        new RandomData(_source, _target.Owner, ActionId, 1, damageData.CriticalRate, damageData.CriticalDamageRate));


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
                    if (num2 <= criticalRate && damageData.CriticalDamageRate != 0.0)
                    {
                        criticalData.IsCritical = true;
                        criticalData.ExpectedDamage *= 1f + FloatWithEx.Binomial(damageData.CriticalRate, true) * (2f * damageData.CriticalDamageRate - 1);
                    }
                    else
                    {
                        criticalData.ExpectedDamage *= 1f + FloatWithEx.Binomial(damageData.CriticalRate, false) * (2f * damageData.CriticalDamageRate - 1);
                    }

                    criticalData.ExpectedDamage = criticalData.ExpectedDamage.Floor();
                    if (!damageData.IgnoreDef)
                    {
                        switch (damageData.DamageType)
                        {
                            case DamageData.eDamageType.ATK:
                                var defZero = (float)damageData.Target.GetDefZero();
                                var num3 = Mathf.Max(0.0f, defZero - damageData.DefPenetrate);
                                criticalData.ExpectedDamage = (criticalData.ExpectedDamage * (1.0f - num3 / (defZero + 100.0f)));
                                break;
                            case DamageData.eDamageType.MGC:
                                float magicDefZero = damageData.Target.GetMagicDefZero();
                                float num4 = Mathf.Max(0.0f, magicDefZero - damageData.DefPenetrate);
                                criticalData.ExpectedDamage = (criticalData.ExpectedDamage * (1.0f - num4 / (magicDefZero + 100.0f)));
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
            damageData.IsLogBarrierCritical = CriticalDataDictionary[_target][_num].IsCritical;
            damageData.LogBarrierExpectedDamage = CriticalDataDictionary[_target][_num].ExpectedDamage.Floor();
            damageData.TotalDamageForLogBarrier = TotalDamageDictionary[_target].Floor();
            if (_target.Owner.SetDamage(damageData, true, ActionId, OnDamageHit, _skill: _skill, _onDefeat: OnDefeatEnemy, _damageWeight: ActionExecTimeList[_num].Weight, _damageWeightSum: ActionWeightSum, _energyChargeMultiple: EnergyChargeMultiple, callBack: action) != 0L)
                HitOnceDic[_target] = true;
            if (_skill.AnimId != eSpineCharacterAnimeId.ATTACK)
                return;
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
