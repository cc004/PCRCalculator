using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using PCRCaculator;
using Elements.Battle;

namespace Elements
{

    public class EveryAttackCriticalData 
    {
        public float Timer; 
        public eStateIconType IconType { get; set; }
        public bool Enable { get; set; }

        public void StartTimer(UnitCtrl _owner) 
        { 
            var enumerator = updateTimer(_owner);
            if (_owner != null && enumerator != null)
            {
                _owner.AppendCoroutine(enumerator, ePauseType.SYSTEM);
            }
        }

        private IEnumerator updateTimer(UnitCtrl _owner) 
        { 
            while (Enable && Timer > 0f)
            {
                Timer -= _owner != null ? _owner.DeltaTimeForPause : BattleManager.Instance.DeltaTime_60fps;
                yield return null;
            }
            Timer = 0f;
            Enable = false;
            _owner?.OnChangeState?.Invoke(_owner, IconType, false);
        }

    }


}