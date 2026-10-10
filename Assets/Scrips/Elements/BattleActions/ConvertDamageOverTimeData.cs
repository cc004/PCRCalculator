using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elements
{
    public class ConvertDamageOverTimeData
    {
        // Properties
        public bool Enable { get; set; }
        public double Timer { get; set; }
        public UnitCtrl Target { get; set; }
        public UnitCtrl Source { get; set; }
        public double DamageCutValue { get; set; }
        public int NumberOfDamage { get; set; }
        public List<long> DamageList { get; set; } = new List<long>();
        public ConvertDamageOverTimeAction OwnerAction { get; set; }


        // RVA: 0x3581DF8 Offset: 0x3581DF8 VA: 0x3581DF8
        public void StartTimer()
        {
            Target.AppendCoroutine(updateTimer(), ePauseType.SYSTEM);
        }

        // RVA: 0x3581F88 Offset: 0x3581F88 VA: 0x3581F88
        private IEnumerator updateTimer()
        {
            while (Enable)
            {
                if (Target.IsDead || Target.IdleOnly || Timer <= 0.0)
                {
                    Disable(false);
                    yield break;
                }
                Timer -= Target.DeltaTimeForPause;
                yield return null;
            }
        }

        // RVA: 0x3581C7C Offset: 0x3581C7C VA: 0x3581C7C
        public void Disable(bool _resetDamage)
        {
            Timer = 0;
            Enable = false;
            Target.OnChangeState?.Invoke(Target, eStateIconType.CONVERT_DAMAGE_OVER_TIME, false);
            if (_resetDamage)
            {
                DamageList.Clear();
            }
        }

        // RVA: 0x3582068 Offset: 0x3582068 VA: 0x3582068
        public void AddDamage(long _value)
        {
            if (_value != 0)
            {
                bool empty = DamageList.Count == 0;
                for(int i = 0; i < NumberOfDamage; ++ i)
                {
                    if (DamageList.Count <= i)
                    {
                        DamageList.Add(_value);
                    }
                    else
                    {
                        DamageList[i] += _value;
                    }
                }
                if (empty)
                {
                    Target.AppendCoroutine(updateDamage(), ePauseType.SYSTEM);
                }
            }
        }

        // RVA: 0x3582244 Offset: 0x3582244 VA: 0x3582244
        private IEnumerator updateDamage()
        {
            double timer = 0.0;
            float interval = Target.GetSlipDamageExtendValue();
            while(DamageList.Count > 0)
            {
                if (Target.IsDead || Target.IdleOnly)
                    break;
                timer -= Target.DeltaTimeForPause;
                if (BattleUtil.LessThanOrApproximately(timer, 0.0))
                {
                    var damageData = new DamageData
                    {
                        Damage = DamageList[0],
                        DamageType = DamageData.eDamageType.NONE,
                        Source = Target,
                        SourceForDamageNumEffect = Source,
                        ActionType = eActionType.CONVERT_DAMAGE_OVER_TIME,
                        Target = Target.GetFirstParts(),
                    };
                    Target.SetDamage(damageData, false, OwnerAction.ActionId, null, true, null, false, OwnerAction.OnDefeatEnemy, false, 1, 1, null, OwnerAction.EnergyChargeMultiple, OwnerAction.UsedChargeEnergyByReceiveDamage, null);
                    DamageList.RemoveAt(0);
                    timer = interval;
                }
                yield return null;
            }
        }
    }
}
