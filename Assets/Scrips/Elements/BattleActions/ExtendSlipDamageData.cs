using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elements
{
    public class ExtendSlipDamageData
    {
        private double timer; // 0x30

        // Properties
        public bool Enable { get; set; }
        public float ExtendValue { get; set; }
        public UnitCtrl Target { get; set; }
        public int EnvironmentId { get; set; }
        public double Duration { get; set; }

        // RVA: 0x3593ED8 Offset: 0x3593ED8 VA: 0x3593ED8
        public void Initialize()
        {
            Enable = true;
            Target.AppendCoroutine(updateTimer(), ePauseType.SYSTEM);
        }

        // RVA: 0x3593F60 Offset: 0x3593F60 VA: 0x3593F60
        private IEnumerator updateTimer()
        {
            timer = 0;
            while (timer < Duration)
            {
                if (!Enable || Target.IsDead || Target.IdleOnly)
                    break;
                timer += Target.DeltaTimeForPause;
                yield return null;
            }
            Enable = false; // no found in code, but seems to be necessary to disable the effect after duration ends
        }

    }
}