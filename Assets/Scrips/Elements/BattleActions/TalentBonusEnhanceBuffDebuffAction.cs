using System;
using System.Collections;
using System.Collections.Generic;

namespace Elements
{
    public class TalentBonusEnhanceBuffDebuffAction : ActionParameter
    {
        public override void ExecAction(
            UnitCtrl _source,
            BasePartsData _target,
            int _num,
            UnitActionController _sourceActionController,
            Skill _skill,
            float _starttime,
            Dictionary<int, bool> _enabledChildAction,
            ValueDictionary _valueDictionary,
            Action<string> action = null)
        {
            base.ExecAction(_source, _target, _num, _sourceActionController, _skill, _starttime, _enabledChildAction, _valueDictionary);

            if (_target == null || _target.Owner == null)
                return;

            UnitCtrl target = _target.Owner;
            AppendIsAlreadyExeced(target, _num);

            long effectValue = BattleUtil.DoubleToLong(_valueDictionary[eValueNumber.VALUE_1]);
            float maxTime = (float)_valueDictionary[eValueNumber.VALUE_2];
            target.AppendCoroutine(updateBuffDebuff(target, effectValue, maxTime), ePauseType.SYSTEM);
        }

        private IEnumerator updateBuffDebuff(UnitCtrl target, long effectValue, float maxTime)
        {
            if (target == null)
                yield break;

            float time = maxTime;
            target.TalentBonusEnhancePercent += effectValue;

            while (!BattleUtil.LessThanOrApproximately(time, 0f)
                   && (long)target.Hp >= 1
                   && !target.IdleOnly)
            {
                time -= target.DeltaTimeForPause;
                yield return null;
            }

            target.TalentBonusEnhancePercent -= effectValue;
        }
    }
}
