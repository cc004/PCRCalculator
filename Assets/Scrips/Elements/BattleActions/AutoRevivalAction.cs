using System;
using System.Collections.Generic;
using UnityEngine;

namespace Elements
{
    public class AutoRevivalAction : ActionParameter
    {
        private BasePartsData parts; // 0x1C8

        public override void ExecActionOnStart(Skill _skill, UnitCtrl _source, UnitActionController _sourceActionController)
        {
            base.ExecActionOnStart(_skill, _source, _sourceActionController);
            parts = _source.BossPartsListForBattle.Find(e => e.Index == _skill.ParameterTarget);
        }

        public override void ExecActionOnWaveStart(Skill _skill, UnitCtrl _source, UnitActionController _sourceActionController)
        {
            base.ExecActionOnWaveStart(_skill, _source, _sourceActionController);
            _source.AutoRevivalData = new AutoRevivalData(
                parts,
                ActionDetail1,
                ActionDetail3,
                Value,
                _source,
                (eStateIconType)ActionDetail2,
                EffectType,
                _skill
            );
        }


        public override void ExecAction(UnitCtrl _source, BasePartsData _target, int _num, UnitActionController _sourceActionController, Skill _skill, float _starttime, Dictionary<int, bool> _enabledChildAction, ValueDictionary _valueDictionary, Action<string> callBack = null)
        {
            base.ExecAction(_source, _target, _num, _sourceActionController, _skill, _starttime, _enabledChildAction, _valueDictionary, callBack);
        }

        public override void SetLevel(float _level)
        {
            base.SetLevel(_level);

            double v1 = MasterData.action_value_2;
            double v2 = MasterData.action_value_3;
            Value[eValueNumber.VALUE_2] = v1 + v2 * _level;;
        }
    }
}