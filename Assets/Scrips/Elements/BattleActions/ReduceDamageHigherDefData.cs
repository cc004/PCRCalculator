using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Timers;
using UnityEngine;

namespace Elements
{
    public class ReduceDamageByHigherDefData
    {
        public bool Enable { get; set; }
        public double Timer { get; set; }
        public UnitCtrl Target { get; set; }

        public void StartTimer()
        {
            Target.AppendCoroutine(updateTimer(), ePauseType.SYSTEM);
        }

        // RVA: 0x35B8030 Offset: 0x35B8030 VA: 0x35B8030
        private IEnumerator updateTimer()
        {
            while(Timer > 0)
            {
                if (!Enable || Target.IsDead || Target.IdleOnly)
                    break;
                Timer -= Target.DeltaTimeForPause;
                yield return null;
            }
            Disable();
        }

        // RVA: 0x35B7E3C Offset: 0x35B7E3C VA: 0x35B7E3C
        public void Disable()
        {
            Timer = 0;
            Enable = false;
            Target.OnChangeState?.Invoke(Target, eStateIconType.REDUCE_DAMAGE_BY_HIGHER_DEF, false);
        }

    }
}