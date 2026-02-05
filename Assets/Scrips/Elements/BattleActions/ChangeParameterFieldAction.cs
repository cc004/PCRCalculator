// Decompiled with JetBrains decompiler
// Type: Elements.ChangeParameterFieldAction
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Elements
{
    public class ChangeParameterFieldAction : ActionParameter
    {
        private const int DETAIL_DIGIT = 10;
        private const int DETAIL_DEBUFF = 1;

        public override void Initialize()
        {
            base.Initialize();
            //Singleton<LCEGKJFKOPD>.Instance.LoadDebuffFieldEffect();
        }

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
          ValueDictionary _valueDictionary,
          Action<string> action)
        {
            base.ExecAction(_source, _target, _num, _sourceActionController, _skill, _starttime, _enabledChildAction, _valueDictionary);
            UnitCtrl.BuffParamKind changeParamKind = BuffDebuffAction.GetChangeParamKind(ActionDetail1);
            var intReverseTruncate = (float)BattleUtil.DoubleToIntReverseTruncate(_valueDictionary[eValueNumber.VALUE_1]);
            GameObject gameObject1 = null;
            GameObject gameObject2 = null;
            if (ActionEffectList.Count > 0)
            {
                gameObject1 = ActionEffectList[0].Prefab;
                gameObject2 = ActionEffectList[0].PrefabLeft;
            }
            ChangeParameterFieldData parameterFieldData = new ChangeParameterFieldData();
            parameterFieldData.FildType = eFieldType.HEAL;
            parameterFieldData.ExecType = eFieldExecType.NORMAL;
            parameterFieldData.StayTime = (float)_valueDictionary[eValueNumber.VALUE_3];
            parameterFieldData.CenterX = _target.GetLocalPosition().x + Position;
            parameterFieldData.Size = (float)_valueDictionary[eValueNumber.VALUE_5];
            parameterFieldData.FieldTargetType = _source.IsOther == (ActionDetail1 % 10 == 1) ? eFieldTargetType.PLAYER : eFieldTargetType.ENEMY;
            parameterFieldData.PauseTarget = _skill.BlackOutTime > 0.0 ? _source : null;
            parameterFieldData.EffectType = (ChangeParameterFieldData.eEffectType)ActionDetail3;
            parameterFieldData.TargetList = new List<BasePartsData>();
            parameterFieldData.TargetSet = new HashSet<BasePartsData>();
            parameterFieldData.Value = intReverseTruncate;
            parameterFieldData.BuffParamKind = changeParamKind;
            parameterFieldData.ValueType = (BuffDebuffAction.eChangeParameterType)ActionDetail2;
            parameterFieldData.Source = _source;
            parameterFieldData.UniqueEffectPrefab = gameObject1;
            parameterFieldData.UniqueEffectPrefabLeft = gameObject2;
            parameterFieldData.IsBuff = ActionDetail1 % 10 != 1;
            parameterFieldData.AlreadyExecedTargetCount = new Dictionary<UnitCtrl, int>();
            parameterFieldData.TriggerCall = TriggerCall;
            parameterFieldData.Cache();
            battleManager.ExecField(parameterFieldData, ActionId);
            action?.Invoke("释放领域，起点：" + parameterFieldData.CenterX + "范围：" + parameterFieldData.Size + "持续时间：" + parameterFieldData.StayTime);
        }

        public override DoubleWithEx GetActionBaseValue(UnitCtrl _source, eValueNumber _type)
        {
            // 默认值
            DoubleWithEx value = 0.0;

            Value.TryGetValue(_type, out value);

            if (_type != eValueNumber.VALUE_1) // small different
                return value;

            UnitCtrl.BuffParamKind paramKind = BuffDebuffAction.GetChangeParamKind(ActionDetail1);

            if (!BuffDebuffAction.BUFF_DEBUFF_ENHANCE_TARGET_TYPE.Contains(paramKind))
                return value;

            if (IsExEquipSkillAction)
                return value;

            long enhanceZero = 0;
            if (_source != null)
            {
                if (ActionDetail1 % 10 != 1)
                    enhanceZero = _source.BuffEnhanceZero;
                else
                    enhanceZero = _source.DebuffEnhanceZero;
            }

            double rate = enhanceZero / 10000.0 + 1.0;
            return value * rate;
        }

        public override void SetLevel(float _level)
        {
            base.SetLevel(_level);
            Value[eValueNumber.VALUE_1] = (float)(MasterData.action_value_1 + MasterData.action_value_2 * _level);
            Value[eValueNumber.VALUE_3] = (float)(MasterData.action_value_3 + MasterData.action_value_4 * _level);
        }
    }
}
