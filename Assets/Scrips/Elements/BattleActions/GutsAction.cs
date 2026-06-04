using System;
using System.Collections.Generic;

namespace Elements
{
    public class GutsAction : ActionParameter
    {
        private BasePartsData parts;

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
            ValueDictionary _valueDictionary,
            Action<string> action = null)
        {
            base.ExecAction(_source, _target, _num, _sourceActionController, _skill, _starttime, _enabledChildAction, _valueDictionary);

            UnitCtrl targetOwner = _target?.Owner;
            if (targetOwner == null || targetOwner.Hp < 1 || _source == null)
                return;

            LongWithEx attackBase = GetActionBaseParameter(_source);
            int valueType = BattleUtil.DoubleToInt(_valueDictionary[eValueNumber.VALUE_1]);
            LongWithEx healValue = 0;
            if (valueType == (int)eValueType.NORMAL)
            {
                healValue = BattleUtil.DoubleToLong(_valueDictionary[eValueNumber.VALUE_2] + _valueDictionary[eValueNumber.VALUE_4] * (DoubleWithEx)attackBase);
            }
            else if (valueType == (int)eValueType.PERCENT)
            {
                healValue = BattleUtil.DoubleToLong((_valueDictionary[eValueNumber.VALUE_2] + _valueDictionary[eValueNumber.VALUE_4] * (DoubleWithEx)attackBase) * (DoubleWithEx)targetOwner.MaxHp / 100.0);
            }

            SkillEffectCtrl effect = null;
            NormalSkillEffect execEffectData = ActionEffectList != null && ActionEffectList.Count >= 2 ? ActionEffectList[1] : null;

            AppendIsAlreadyExeced(targetOwner, _num);
            targetOwner.AddGuts(
                (eStateIconType)ActionDetail2,
                ActionDetail1 == (int)eValueElementType.PHYSICS ? UnitCtrl.eInhibitHealType.PHYSICS : UnitCtrl.eInhibitHealType.MAGIC,
                healValue,
                (float)_valueDictionary[eValueNumber.VALUE_5],
                BattleUtil.DoubleToInt(_valueDictionary[eValueNumber.VALUE_6]),
                BattleUtil.DoubleToInt(_valueDictionary[eValueNumber.VALUE_7]),
                effect,
                execEffectData,
                _skill,
                _source);

            action?.Invoke($"为{targetOwner.UnitNameEx}添加根性效果，死亡前恢复{healValue}HP，持续{_valueDictionary[eValueNumber.VALUE_5]}秒");
        }

        public override void SetLevel(float _level)
        {
            base.SetLevel(_level);
            Value[eValueNumber.VALUE_2] = MasterData.action_value_2 + MasterData.action_value_3 * _level;
        }

        private LongWithEx GetActionBaseParameter(UnitCtrl source)
        {
            if (source.IsPartsBoss)
            {
                return ActionDetail1 == (int)eValueElementType.PHYSICS ? parts.GetAtkZero() : parts.GetMagicStrZero();
            }

            return ActionDetail1 == (int)eValueElementType.PHYSICS ? source.AtkZero : source.MagicStrZero;
        }

        private enum eValueElementType
        {
            PHYSICS = 1,
            MAGIC = 2,
        }

        private enum eValueType
        {
            NORMAL = 1,
            PERCENT = 2,
        }
    }
}
