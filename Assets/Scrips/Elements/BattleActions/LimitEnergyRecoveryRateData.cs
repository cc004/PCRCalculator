using System.Collections;
using System.Collections.Generic;

namespace Elements
{
	public class LimitEnergyRecoveryRateData // TypeDefIndex: 20398
	{
		// Fields
		public DoubleWithEx Value; // 0x10
		public bool Enable; // 0x18
		public float Timer; // 0x1C
		public bool DisableIcon; // 0x20

		// RVA: 0x28075BC Offset: 0x28075BC VA: 0x28075BC
		public void StartTimer(UnitCtrl _owner)
		{
			_owner.AppendCoroutine(updateTimer(_owner), ePauseType.SYSTEM);
		}

		// RVA: 0x28075F8 Offset: 0x28075F8 VA: 0x28075F8
		private IEnumerator updateTimer(UnitCtrl _owner)
		{
			// 基于Duration属性递减timer
			var timer = Timer;

			while (timer > 0 && Enable && _owner != null && !_owner.IsDead)
			{
				timer -= _owner.DeltaTimeForPause;
				yield return null;
			}

			if (!DisableIcon)
			{
				_owner.button?.SetAbnormalIcons(_owner, (eStateIconType)150, false);
			}
			Enable = false;
		}

	}

}
