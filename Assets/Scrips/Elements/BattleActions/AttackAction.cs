// Decompiled with JetBrains decompiler
// Type: Elements.AttackAction
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using Elements.Battle;
using System.Collections.Generic;
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
          System.Action<string> action = null)
        {
            _target.IncrementUbAttackHitCount();
            AttackActionBase.eAttackType actionDetail1 = (AttackActionBase.eAttackType)this.ActionDetail1;
            if (!this.HitOnceDic.ContainsKey(_target))
            {
                this.HitOnceDic.Add(_target, false);
                this.HitOnceKeyList.Add(_target);
            }
            if (this.judgeMiss(_target, _source, _num, _sourceActionController, _skill, _starttime, _enabledChildAction, actionDetail1))
            {
                //add scripts
                string describe = "MISS";
                action?.Invoke(describe);
                //end add
                return;
            }

            bool _isCritical = (double)_valueDictionary[eValueNumber.VALUE_5] == (double)(_num + 1);
            DamageData damageData = this.createDamageData(_source, _target, _num, _valueDictionary, actionDetail1, _isCritical, _skill, eActionType.ATTACK);
            if (!this.TotalDamageDictionary.ContainsKey(_target))
            {
                FloatWithEx num1 = 0;
                List<CriticalData> criticalDataList = new List<CriticalData>();
                for (int index = 0; index < this.ActionExecTimeList.Count; ++index)
                {
                    CriticalData criticalData = new CriticalData();
                    double num2 = (double)BattleManager.Random(0.0f, 1f,
                        new PCRCaculator.Guild.RandomData(_source, _target.Owner, ActionId, 1, damageData.CriticalRate, damageData.CriticalDamageRate));


                    if (MyGameCtrl.Instance.tempData.isGuildBattle && (MyGameCtrl.Instance.tempData.randomData.ForceNoCritical_player && _target.Owner.IsOther || MyGameCtrl.Instance.tempData.randomData.ForceNoCritical_enemy && !_target.Owner.IsOther))
                        num2 = 1;
                    if (MyGameCtrl.Instance.tempData.isGuildBattle && MyGameCtrl.Instance.tempData.randomData.ForceCritical_player && _target.Owner.IsOther)
                        num2 = 0;
                    //start add
                    if (MyGameCtrl.Instance.tempData.isGuildBattle && MyGameCtrl.Instance.tempData.randomData.TryJudgeRandomSpecialSetting(damageData.Source, _target.Owner, _skill, eActionType.ATTACK, BattleHeaderController.CurrentFrameCount, out float fix))
                    {
                        num2 = fix;
                    }

                    criticalData.ExpectedDamage = BattleUtil.FloatToInt(damageData.TotalDamageForLogBarrier * this.ActionExecTimeList[index].Weight / this.ActionWeightSum);
                    double criticalRate = (double)damageData.CriticalRate;
                    if (num2 <= criticalRate && (double)damageData.CriticalDamageRate != 0.0)
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
                                float magicDefZero = (float)damageData.Target.GetMagicDefZero();
                                float num4 = Mathf.Max(0.0f, magicDefZero - damageData.DefPenetrate);
                                criticalData.ExpectedDamage = (criticalData.ExpectedDamage * (1.0f - num4 / (magicDefZero + 100.0f)));
                                break;
                        }
                    }
                    criticalData.CriticalRate = damageData.CriticalDamageRate;
                    num1 += criticalData.ExpectedDamage;
                    criticalDataList.Add(criticalData);
                }
                this.CriticalDataDictionary.Add(_target, criticalDataList);
                this.TotalDamageDictionary.Add(_target, num1);
            }
            damageData.IsLogBarrierCritical = this.CriticalDataDictionary[_target][_num].IsCritical;
            damageData.LogBarrierExpectedDamage = this.CriticalDataDictionary[_target][_num].ExpectedDamage.Floor();
            damageData.TotalDamageForLogBarrier = this.TotalDamageDictionary[_target].Floor();
            if (_target.Owner.SetDamage(damageData, true, this.ActionId, this.OnDamageHit, _skill: _skill, _onDefeat: this.OnDefeatEnemy, _damageWeight: this.ActionExecTimeList[_num].Weight, _damageWeightSum: this.ActionWeightSum, _energyChargeMultiple: this.EnergyChargeMultiple, callBack: action) != 0L)
                this.HitOnceDic[_target] = true;
            if (_skill.AnimId != eSpineCharacterAnimeId.ATTACK)
                return;
            //_target.ShowHitEffect(_source.ToadDatas.Count > 0 ? _skill.WeaponType : _source.WeaponSeType, _skill, _source.IsLeftDir);
        }

    protected override float getCriticalDamageRate(Dictionary<eValueNumber, FloatWithEx> _valueDictionary) =>  _valueDictionary[eValueNumber.VALUE_6] == 0.0 ? 1f : (float)_valueDictionary[eValueNumber.VALUE_6];

    public override void SetLevel(float _level)
    {
      base.SetLevel(_level);
      this.Value[eValueNumber.VALUE_1] = (float) ((double) this.MasterData.action_value_1 + (double) this.MasterData.action_value_2 * (double) _level);
      this.Value[eValueNumber.VALUE_3] = (float) ((double) this.MasterData.action_value_3 + (double) this.MasterData.action_value_4 * (double) _level);
    }
  }
}
