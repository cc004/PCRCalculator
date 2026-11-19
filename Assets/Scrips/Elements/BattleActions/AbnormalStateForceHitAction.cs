using System.Collections.Generic;

namespace Elements
{
    public class AbnormalStateForceHitAction : ActionParameter
    {
        private static readonly eValueNumber[] ForceHitIdValues =
        {
            eValueNumber.VALUE_4,
            eValueNumber.VALUE_5,
            eValueNumber.VALUE_6,
            eValueNumber.VALUE_7
        };

        public override void ExecAction(
            UnitCtrl _source,
            BasePartsData _target,
            int _num,
            UnitActionController _sourceActionController,
            Skill _skill,
            float _starttime,
            Dictionary<int, bool> _enabledChildAction,
            ValueDictionary _valueDictionary)
        {
            base.ExecAction(
                _source,
                _target,
                _num,
                _sourceActionController,
                _skill,
                _starttime,
                _enabledChildAction,
                _valueDictionary);

            var forceHitIds = new HashSet<int>();
            foreach (eValueNumber valueNumber in ForceHitIdValues)
            {
                int forceHitId = BattleUtil.DoubleToInt(_valueDictionary[valueNumber]);
                if (forceHitId != -1)
                {
                    forceHitIds.Add(forceHitId);
                }
            }
            _target.Owner.SetForceHit((eActionType)ActionDetail1, forceHitIds,
                BattleUtil.DoubleToInt(_valueDictionary[eValueNumber.VALUE_1]));
        }
    }
}
