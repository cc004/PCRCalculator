using System.Collections.Generic;

namespace Elements
{
    public class NoDamageAction : ActionParameter
    {
        private static readonly Dictionary<eNoDamageType, UnitCtrl.eAbnormalState> ABNORMAL_STATE_DIC = new Dictionary<eNoDamageType, UnitCtrl.eAbnormalState>
        {
            {
                eNoDamageType.NO_DAMAGE,
                UnitCtrl.eAbnormalState.NO_DAMAGE_MOTION
            },
            {
                eNoDamageType.PHYSICS_DODGE,
                UnitCtrl.eAbnormalState.PHYSICS_DODGE
            },
            {
                eNoDamageType.ABNORMAL,
                UnitCtrl.eAbnormalState.NO_ABNORMAL
            },
            {
                eNoDamageType.DEBUFF,
                UnitCtrl.eAbnormalState.NO_DEBUF
            },
            {
                eNoDamageType.BREAK_POINT,
                UnitCtrl.eAbnormalState.PARTS_NO_DAMAGE
            },
            {
                eNoDamageType.NO_DAMAGE2,
                UnitCtrl.eAbnormalState.NO_DAMAGE_MOTION2
            }
        };

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
            /*switch (ActionDetail1)
            {
                case 1:
                    _target.Owner.SetAbnormalState(_source, UnitCtrl.eAbnormalState.NO_DAMAGE_MOTION, _valueDictionary[eValueNumber.VALUE_1], this, _skill);
                    break;
                case 2:
                    _target.Owner.SetAbnormalState(_source, UnitCtrl.eAbnormalState.PHYSICS_DODGE, _valueDictionary[eValueNumber.VALUE_1], this, _skill);
                    break;
                case 4:
                    _target.Owner.SetAbnormalState(_source, UnitCtrl.eAbnormalState.NO_ABNORMAL, _valueDictionary[eValueNumber.VALUE_1], this, _skill);
                    break;
                case 5:
                    _target.Owner.SetAbnormalState(_source, UnitCtrl.eAbnormalState.NO_DEBUF, _valueDictionary[eValueNumber.VALUE_1], this, _skill);
                    break;
                case 6:
                    _target.Owner.SetAbnormalState(_source, UnitCtrl.eAbnormalState.PARTS_NO_DAMAGE, _valueDictionary[eValueNumber.VALUE_1], this, _skill);
                    break;
            }*/
            eNoDamageType actionDetail = (eNoDamageType)base.ActionDetail1;
            switch (actionDetail)
            {
                case eNoDamageType.NO_DAMAGE:
                case eNoDamageType.PHYSICS_DODGE:
                case eNoDamageType.ABNORMAL:
                case eNoDamageType.DEBUFF:
                case eNoDamageType.BREAK_POINT:
                case eNoDamageType.NO_DAMAGE2:
                    _target.Owner.SetAbnormalState(_source, ABNORMAL_STATE_DIC[actionDetail], _valueDictionary[eValueNumber.VALUE_1], this, _skill, 0f, 0f, _reduceEnergy: false, _isDamageRelease: false, 1f, _valueDictionary[eValueNumber.VALUE_3] == 0f);
                    break;
                case eNoDamageType.ALL_DODGE:
                    break;
            }
            action($"{((eNoDamageType)base.ActionDetail1).GetDescription()},持续{ _valueDictionary[eValueNumber.VALUE_1]}秒");
        }

        public override void SetLevel(float _level)
        {
            base.SetLevel(_level);
            Value[eValueNumber.VALUE_1] = (float)(MasterData.action_value_1 + MasterData.action_value_2 * _level);
        }

        private enum eNoDamageType
        {
            [System.ComponentModel.Description("无敌")]
            NO_DAMAGE = 1,
            [System.ComponentModel.Description("物理闪避")]
            PHYSICS_DODGE = 2,
            [System.ComponentModel.Description("伤害闪避")]
            ALL_DODGE = 3,
            [System.ComponentModel.Description("异常状态免疫")]
            ABNORMAL = 4,
            [System.ComponentModel.Description("DEBUFF免疫")]
            DEBUFF = 5,
            [System.ComponentModel.Description("BREAK免疫")]
            BREAK_POINT = 6,
            [System.ComponentModel.Description("无敌2")]
            NO_DAMAGE2

        }
    }
}
