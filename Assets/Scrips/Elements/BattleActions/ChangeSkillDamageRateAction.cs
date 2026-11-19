using System;
using System.Collections;
using System.Collections.Generic;

namespace Elements
{
    /// <summary>
    /// action_type 135 CHANGE_SKILL_DAMAGE_RATE：一段时间内改变「技能伤害倍率」。
    /// master 数据里 action_value_1 = 倍率百分比（如 3.0 / 9.0），action_value_3 = 持续时间（秒）。
    /// </summary>
    public class ChangeSkillDamageRateData
    {
        public float Timer { get; set; }
        public double Rate { get; set; }
        public UnitCtrl Target { get; set; }

        public int SkillGroup { get; set; }

        public bool IsIncrease { get; set; }

        public eStateIconType Icon
        {
            get
            {
                if (SkillGroup == 1)
                    return IsIncrease ? eStateIconType.UB_DAMAGE_UP : eStateIconType.UB_DAMAGE_DOWN;
                return IsIncrease ? eStateIconType.SKILL_DAMAGE_UP : eStateIconType.SKILL_DAMAGE_DOWN;
            }
        }

        public void StartTimer()
        {
            Target?.AppendCoroutine(updateTimer(), ePauseType.SYSTEM);
        }

        public void Disable()
        {
            Timer = 0f;
            Target?.RemoveChangeSkillDamageRateData(this);
        }

        private IEnumerator updateTimer()
        {
            while (Timer > 0.0)
            {
                if (Target == null || Target.IsDead)
                    break;

                Timer -= Target.DeltaTimeForPause;
                yield return null;
            }

            Disable();
        }
    }

    public class ChangeSkillDamageRateAction : ActionParameter
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

            if (_target == null || _target.Owner == null)
                return;

            AppendIsAlreadyExeced(_target.Owner, _num);

            float timer = (float)_valueDictionary[eValueNumber.VALUE_3];

            if (ActionDetail1 != 10 && ActionDetail1 != 11 && ActionDetail1 != 20 && ActionDetail1 != 21)
                return;

            var data = new ChangeSkillDamageRateData
            {
                Target = _target.Owner,
                SkillGroup = ActionDetail1 / 10,
                IsIncrease = ActionDetail1 % 10 == 0,
                Timer = timer,
                Rate = _valueDictionary[eValueNumber.VALUE_1] / 100.0
            };

            _target.Owner.AddChangeSkillDamageRateData(data);
        }

        public override void SetLevel(float _level)
        {
            base.SetLevel(_level);
            Value[eValueNumber.VALUE_1] = MasterData.action_value_1 + MasterData.action_value_2 * _level;
            Value[eValueNumber.VALUE_3] = MasterData.action_value_3 + MasterData.action_value_4 * _level;
        }
    }
}
