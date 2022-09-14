using System.Collections.Generic;

namespace Elements
{
    public class DamageCutAction : ActionParameter
    {
        private static readonly Dictionary<eDamageCutType, UnitCtrl.eAbnormalState> abnormalStateDic = new Dictionary<eDamageCutType, UnitCtrl.eAbnormalState>
    {
      {
        eDamageCutType.ATK,
        UnitCtrl.eAbnormalState.CUT_ATK_DAMAGE
      },
      {
        eDamageCutType.MGC,
        UnitCtrl.eAbnormalState.CUT_MGC_DAMAGE
      },
      {
        eDamageCutType.ALL,
        UnitCtrl.eAbnormalState.CUT_ALL_DAMAGE
      }
    };

        public override void ExecActionOnStart(
          Skill _skill,
          UnitCtrl _source,
          UnitActionController _sourceActionController) => base.ExecActionOnStart(_skill, _source, _sourceActionController);

        public override void ExecAction(
          UnitCtrl _source,
          BasePartsData _target,
          int _num,
          UnitActionController _sourceActionController,
          Skill _skill,
          float _starttime,
          Dictionary<int, bool> _enabledChildAction,
          Dictionary<eValueNumber, FloatWithEx> _valueDictionary, System.Action<string> action)
        {
            base.ExecAction(_source, _target, _num, _sourceActionController, _skill, _starttime, _enabledChildAction, _valueDictionary);
            AppendIsAlreadyExeced(_target.Owner, _num);
            _target.Owner.SetAbnormalState(_source, abnormalStateDic[(eDamageCutType)ActionDetail1], _valueDictionary[eValueNumber.VALUE_3], this, _skill, (int)_valueDictionary[eValueNumber.VALUE_1]);
            action($"{ (int)_valueDictionary[eValueNumber.VALUE_1]}%{((eDamageCutType)ActionDetail1).ToString()}减伤");
        }

        public override void SetLevel(float _level)
        {
            base.SetLevel(_level);
            Value[eValueNumber.VALUE_1] = (float)(MasterData.action_value_1 + MasterData.action_value_2 * _level);
            Value[eValueNumber.VALUE_3] = (float)(MasterData.action_value_3 + MasterData.action_value_4 * _level);
            
        }

        private enum eDamageCutType
        {
            ATK = 1,
            MGC = 2,
            ALL = 3,
        }
    }
}
