// Decompiled with JetBrains decompiler
// Type: Elements.ChangeParameterFieldAction
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using Elements.Battle;
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
          Dictionary<eValueNumber, FloatWithEx> _valueDictionary,
          System.Action<string> action)
        {
            base.ExecAction(_source, _target, _num, _sourceActionController, _skill, _starttime, _enabledChildAction, _valueDictionary);
            UnitCtrl.BuffParamKind changeParamKind = BuffDebuffAction.GetChangeParamKind(this.ActionDetail1);
            var intReverseTruncate = (float)BattleUtil.FloatToIntReverseTruncate(_valueDictionary[eValueNumber.VALUE_1]);
            GameObject gameObject1 = (GameObject)null;
            GameObject gameObject2 = (GameObject)null;
            if (this.ActionEffectList.Count > 0)
            {
                gameObject1 = this.ActionEffectList[0].Prefab;
                gameObject2 = this.ActionEffectList[0].PrefabLeft;
            }
            ChangeParameterFieldData parameterFieldData = new ChangeParameterFieldData();
            parameterFieldData.KNLCAOOKHPP = eFieldType.HEAL;
            parameterFieldData.HKDBJHAIOMB = eFieldExecType.NORMAL;
            parameterFieldData.StayTime = _valueDictionary[eValueNumber.VALUE_3];
            parameterFieldData.CenterX = _target.GetLocalPosition().x + this.Position;
            parameterFieldData.Size = _valueDictionary[eValueNumber.VALUE_5];
            parameterFieldData.LCHLGLAFJED = _source.IsOther == (this.ActionDetail1 % 10 == 1) ? eFieldTargetType.PLAYER : eFieldTargetType.ENEMY;
            parameterFieldData.EGEPDDJBILL = (double)_skill.BlackOutTime > 0.0 ? _source : (UnitCtrl)null;
            parameterFieldData.EffectType = (ChangeParameterFieldData.eEffectType)this.ActionDetail3;
            parameterFieldData.TargetList = new List<BasePartsData>();
            parameterFieldData.Value = intReverseTruncate;
            parameterFieldData.BuffParamKind = changeParamKind;
            parameterFieldData.ValueType = (BuffDebuffAction.eChangeParameterType)this.ActionDetail2;
            parameterFieldData.PPOJKIDHGNJ = _source;
            parameterFieldData.HGMNJJBLJIO = gameObject1;
            parameterFieldData.LALMMFAOJDP = gameObject2;
            parameterFieldData.IsBuff = this.ActionDetail1 % 10 != 1;
            parameterFieldData.AlreadyExecedTargetCount = new Dictionary<UnitCtrl, int>();
            this.battleManager.ExecField(parameterFieldData, this.ActionId);
            action?.Invoke("释放领域，起点：" + parameterFieldData.CenterX + "范围：" + parameterFieldData.Size + "持续时间：" + parameterFieldData.StayTime);
        }

        public override void SetLevel(float _level)
        {
            base.SetLevel(_level);
            this.Value[eValueNumber.VALUE_1] = (float)((double)this.MasterData.action_value_1 + (double)this.MasterData.action_value_2 * (double)_level);
            this.Value[eValueNumber.VALUE_3] = (float)((double)this.MasterData.action_value_3 + (double)this.MasterData.action_value_4 * (double)_level);
        }
    }
}
