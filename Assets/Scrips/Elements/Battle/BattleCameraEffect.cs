using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Elements.Battle
{
    public interface IBattleCameraEffectForUnitActionController
    {
        void StartShake(ShakeEffect shake, Skill skill, UnitCtrl unit);
        void UpdateShake();
        void ClearShake();
    }

    public class BattleCameraEffect : IBattleCameraEffectForUnitActionController
    {
        private BattleManager battleManager;
        private List<ShakeEffect> currentShake = new List<ShakeEffect>();
        private Vector3 ShakePosition;

        public BattleCameraEffect(BattleManager parent)
        {
            this.battleManager = parent;
        }

        public void StartShake(ShakeEffect shake, Skill skill, UnitCtrl unit)
        {
            if (shake.ShakeType == ShakeType.RANDOM)
            {
                // Debug.Log($"{skill?.SkillName}:{unit?.UnitName} shake random");
            }
            currentShake.Add(shake);
            shake.ResetStart(skill, unit);
        }

        public void UpdateShake()
		{
			if (currentShake.Count == 0)
            {
                return;
            }
            float num = 0f;
            for (int num2 = currentShake.Count - 1; num2 >= 0; num2--)
            {
                ShakeEffect shakeEffect = currentShake[num2];
                bool num3 = shakeEffect.Simulate(battleManager.FNHFJLAENPF);
                //if (num <= shakeEffect.CurrentAmplutude)
                //{
                //    num = shakeEffect.CurrentAmplutude;
                //    ShakePosition = shakeEffect.CurrentShakePos;
                //}
                if (num3)
                {
                    currentShake.RemoveAt(num2);
                }
                if (shakeEffect.GetOwnerPause())
                {
                    ShakePosition = Vector3.zero;
                }
            }
            //setPlayCameraPosition();
		}

        public void ClearShake()
        {
            currentShake.Clear();
        }
    }
}
