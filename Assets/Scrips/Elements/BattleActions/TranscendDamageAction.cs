using System;
using System.Collections;
using System.Collections.Generic;

namespace Elements
{
    public class TranscendDamageDataForSkillAction
    {
        public double Timer { get; set; }
        public double TranscendRate { get; set; }
        public double TranscendLimit { get; set; }
        public UnitCtrl Target { get; set; }

        public void StartTimer()
        {
            Target.AppendCoroutine(updateTimer(), ePauseType.SYSTEM, Target);
        }

        public DoubleWithEx CalcTranscendDamage(DoubleWithEx damage, DoubleWithEx totalDamage)
        {
            if (totalDamage == 0.0)
                return 0.0;

            DoubleWithEx ratio = totalDamage.Select(total =>
            {
                if (total == 0.0)
                    return 0.0;
                return Math.Min(TranscendRate * total, TranscendLimit) / total;
            }, $"transcend:{TranscendRate}:{TranscendLimit}");

            return damage * ratio;
        }

        public void Disable()
        {
            Timer = 0.0;
            Target?.RemoveTranscendDamageData(this);
        }

        private IEnumerator updateTimer()
        {
            while (Timer > 0.0)
            {
                if (Target == null || Target.IsDead || Target.IdleOnly)
                    break;

                Timer -= Target.DeltaTimeForPause;
                yield return null;
            }

            Disable();
        }
    }

    public class TranscendDamageAction : ActionParameter
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
            Action<string> callBack = null)
        {
            base.ExecAction(_source, _target, _num, _sourceActionController, _skill, _starttime, _enabledChildAction, _valueDictionary);

            if (_target == null)
                return;

            UnitCtrl target = _target.Owner;
            AppendIsAlreadyExeced(target, _num);

            var data = new TranscendDamageDataForSkillAction
            {
                Target = target,
                Timer = _valueDictionary[eValueNumber.VALUE_4],
                TranscendRate = _valueDictionary[eValueNumber.VALUE_1] / 100.0,
                TranscendLimit = _valueDictionary[eValueNumber.VALUE_3]
            };
            target.AddTranscendDamageData(data);
        }

        public override void SetLevel(float _level)
        {
            base.SetLevel(_level);
            Value[eValueNumber.VALUE_1] = MasterData.action_value_1 + MasterData.action_value_2 * _level;
            Value[eValueNumber.VALUE_4] = MasterData.action_value_4 + MasterData.action_value_5 * _level;
        }
    }
}
