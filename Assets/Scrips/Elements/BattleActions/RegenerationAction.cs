using System.Collections.Generic;

namespace Elements
{
    public class RegenerationAction : ActionParameter
    {
        private BasePartsData parts;
        private static readonly Dictionary<eTargetType, UnitCtrl.eAbnormalState> ABNORMAL_STATE_DIC = new Dictionary<eTargetType, UnitCtrl.eAbnormalState>
        {
            {
                eTargetType.HP,
                UnitCtrl.eAbnormalState.REGENERATION
            },
            {
                eTargetType.TP,
                UnitCtrl.eAbnormalState.TP_REGENERATION
            },
            {
                eTargetType.HP2,
                UnitCtrl.eAbnormalState.REGENERATION2
            },
            {
                eTargetType.TP2,
                UnitCtrl.eAbnormalState.TP_REGENERATION2
            }
        };
        public override void ExecActionOnStart(
          Skill _skill,
          UnitCtrl _source,
          UnitActionController _sourceActionController)
        {
            base.ExecActionOnStart(_skill, _source, _sourceActionController);
            parts = _source.BossPartsListForBattle.Find(e => e.Index == _skill.ParameterTarget);
        }

        public override void ExecAction(
          UnitCtrl _source,
          BasePartsData _target,
          int _num,
          UnitActionController _sourceActionController,
          Skill _skill,
          float _starttime,
          Dictionary<int, bool> _enabledChildAction,
          Dictionary<eValueNumber, FloatWithEx> _valueDictionary,
          System.Action<string> action)
        {
            base.ExecAction(_source, _target, _num, _sourceActionController, _skill, _starttime, _enabledChildAction, _valueDictionary);
            AppendIsAlreadyExeced(_target.Owner, _num);
            int num = ((base.ActionDetail1 != 1) ? (_source.IsPartsBoss ? parts.GetMagicStrZero() : ((int)_source.MagicStrZero)) : (_source.IsPartsBoss ? parts.GetAtkZero() : ((int)_source.AtkZero)));
            int val = calculateHealValue(num, _valueDictionary);
            _target.Owner.SetAbnormalState(_source, ABNORMAL_STATE_DIC[(eTargetType)base.ActionDetail2], _valueDictionary[eValueNumber.VALUE_5], this, _skill, val, base.ActionDetail1, _reduceEnergy: false, _isDamageRelease: false, 1f, _valueDictionary[eValueNumber.VALUE_7] == 0f);
            action($"每秒{((eTargetType)ActionDetail2).ToString()}恢复{val}");
        }

        public override void SetLevel(float _level)
        {
            base.SetLevel(_level);
            Value[eValueNumber.VALUE_1] = (float)(MasterData.action_value_1 + MasterData.action_value_2 * _level);
            Value[eValueNumber.VALUE_3] = (float)(MasterData.action_value_3 + MasterData.action_value_4 * _level);
            Value[eValueNumber.VALUE_5] = (float)(MasterData.action_value_5 + MasterData.action_value_6 * _level);
        }
        private int calculateHealValue(int _statusValue, Dictionary<eValueNumber, FloatWithEx> _valueDictionary)
        {
            switch (base.ActionDetail2)
            {
                case 1:
                case 3:
                    return (int)(_valueDictionary[eValueNumber.VALUE_1] + _valueDictionary[eValueNumber.VALUE_3] * (float)_statusValue);
                case 2:
                case 4:
                    return (int)_valueDictionary[eValueNumber.VALUE_1];
                default:
                    return 0;
            }
        }
        public enum eParameterType
        {
            PHYSICS = 1,
            MAGIC = 2,
        }

        private enum eTargetType
        {
            HP = 1,
            TP = 2,
            HP2,
            TP2
        }
    }
}
