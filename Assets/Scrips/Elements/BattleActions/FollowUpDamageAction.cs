using System.Collections;
using System.Collections.Generic;

namespace Elements
{
    public sealed class FollowUpDamageAction : ActionParameter
    {
        public override void ExecAction(UnitCtrl source, BasePartsData target, int num,
            UnitActionController controller, Skill skill, float startTime,
            Dictionary<int, bool> enabledChildAction, ValueDictionary values)
        {
            AppendIsAlreadyExeced(target.Owner, num);
            var effect = new FollowUpDamageEffect((double)values[eValueNumber.VALUE_1] / 100.0,
                (double)values[eValueNumber.VALUE_2]);
            var effects = ActionDetail1 == 1 ? target.Owner.PhysicalFollowUp : target.Owner.MagicFollowUp;
            var icon = ActionDetail1 == 1 ? eStateIconType.PHYSICAL_FOLLOW_UP : eStateIconType.MAGIC_FOLLOW_UP;
            effects.Add(effect);
            if (effects.Count == 1)
                target.Owner.OnChangeState?.Invoke(target.Owner, icon, true);
            target.Owner.AppendCoroutine(effect.Expire(target.Owner, effects, icon, values[eValueNumber.VALUE_3]), ePauseType.SYSTEM);
        }

        public override void SetLevel(float level)
        {
            base.SetLevel(level);
            Value[eValueNumber.VALUE_3] = MasterData.action_value_3 + MasterData.action_value_4 * level;
        }
    }

    public sealed class FollowUpDamageEffect
    {
        private readonly double rate;
        private readonly double limit;

        public FollowUpDamageEffect(double rate, double limit)
        {
            this.rate = rate;
            this.limit = limit;
        }

        public double Calculate(long damage) => limit > 0 ? System.Math.Min(limit, damage * rate) : damage * rate;

        public IEnumerator Expire(UnitCtrl owner, List<FollowUpDamageEffect> effects, eStateIconType icon, double time)
        {
            do
            {
                yield return null;
                time -= owner.DeltaTimeForPause;
            } while (time >= 0 && !owner.IsDead);
            if (effects.Remove(this) && effects.Count == 0)
                owner.OnChangeState?.Invoke(owner, icon, false);
        }
    }
}
