// Decompiled with JetBrains decompiler
// Type: Elements.AttackFieldData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System;
using Elements.Battle;
using System.Collections;

namespace Elements
{
    public class SlipDamageUpData
    {
        // Properties
        public double Value { get; set; }
        public float Timer { get; set; }
        public long LimitDamage { get; set; }
        public bool Enable { get; set; }
        public bool DisplayIcon { get; set; }

        // Methods
        public void StartTimer(UnitCtrl _owner)
        {
            if (DisplayIcon)
            {
                var iconCounter = _owner.SlipDamageUpIconCounter;
                if (iconCounter == 0)
                {
                    _owner.OnChangeState(_owner, eStateIconType.SLIP_DAMAGE_UP, true);
                }
                _owner.SlipDamageUpIconCounter = iconCounter + 1;
            }
            _owner.AppendCoroutine(updateTimer(_owner), ePauseType.SYSTEM);
        }

        // RVA: 0x21989C8 Offset: 0x21989C8 VA: 0x21989C8
        private IEnumerator updateTimer(UnitCtrl _owner)
        {
            while (Enable && !_owner.IdleOnly && !_owner.IsDead && Timer > 0f)
            {
                Timer -= _owner.DeltaTimeForPause;
                yield return null;
            }

            Timer = 0f;
            Enable = false;

            if (DisplayIcon)
            {
                if (--_owner.SlipDamageUpIconCounter == 0)
                {
                    _owner.OnChangeState(_owner, eStateIconType.SLIP_DAMAGE_UP, false);
                }
            }
        }


    }
}
