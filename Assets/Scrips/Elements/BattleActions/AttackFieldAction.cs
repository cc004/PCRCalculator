﻿// Decompiled with JetBrains decompiler
// Type: Elements.AttackFieldAction
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using Elements.Battle;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Elements
{
    public class AttackFieldAction : ActionParameter
    {
        private const int REPEAT_NUMBER = 2;
        private BasePartsData parts;

        public override void Initialize()
        {
            base.Initialize();
            //Singleton<LCEGKJFKOPD>.Instance.LoadAttackFieldEffect(this.ActionDetail2);
        }

        public override void ExecActionOnStart(
          Skill _skill,
          UnitCtrl _source,
          UnitActionController _sourceActionController)
        {
            base.ExecActionOnStart(_skill, _source, _sourceActionController);
            this.parts = (BasePartsData)_source.BossPartsListForBattle.Find((Predicate<PartsData>)(e => e.Index == _skill.ParameterTarget));
        }

        public override void ExecAction(
          UnitCtrl _source,
          BasePartsData _target,
          int _num,
          UnitActionController _sourceActionController,
          Skill _skill,
          float _starttime,
          Dictionary<int, bool> _enabledChildAction,
          Dictionary<eValueNumber, float> _valueDictionary,
          System.Action<string> action)
        {
            base.ExecAction(_source, _target, _num, _sourceActionController, _skill, _starttime, _enabledChildAction, _valueDictionary);
            DamageData.eDamageType eDamageType;
            float num;
            if (this.ActionDetail1 % 2 == 0)
            {
                eDamageType = DamageData.eDamageType.MGC;
                num = _valueDictionary[eValueNumber.VALUE_1] + _valueDictionary[eValueNumber.VALUE_3] * (_source.IsPartsBoss ? (float)this.parts.GetMagicStrZero() : (float)(int)_source.MagicStrZero);
            }
            else
            {
                eDamageType = DamageData.eDamageType.ATK;
                num = _valueDictionary[eValueNumber.VALUE_1] + _valueDictionary[eValueNumber.VALUE_3] * (_source.IsPartsBoss ? (float)this.parts.GetAtkZero() : (float)(int)_source.AtkZero);
            }
            GameObject gameObject1 = (GameObject)null;
            GameObject gameObject2 = (GameObject)null;
            if (this.ActionEffectList.Count > 0)
            {
                gameObject1 = this.ActionEffectList[0].Prefab;
                gameObject2 = this.ActionEffectList[0].PrefabLeft;
            }
            AttackFieldData attackFieldData = new AttackFieldData();
            attackFieldData.KNLCAOOKHPP = eFieldType.HEAL;
            attackFieldData.HKDBJHAIOMB = this.ActionDetail1 > 2 ? eFieldExecType.REPEAT : eFieldExecType.NORMAL;
            attackFieldData.StayTime = _valueDictionary[eValueNumber.VALUE_5];
            attackFieldData.CenterX = _target.GetLocalPosition().x + this.Position;
            attackFieldData.Size = _valueDictionary[eValueNumber.VALUE_7];
            attackFieldData.LCHLGLAFJED = _source.IsOther ? eFieldTargetType.PLAYER : eFieldTargetType.ENEMY;
            attackFieldData.EGEPDDJBILL = (double)_skill.BlackOutTime > 0.0 ? _source : (UnitCtrl)null;
            attackFieldData.EffectId = this.ActionDetail2;
            attackFieldData.TargetList = new List<BasePartsData>();
            attackFieldData.Value = num;
            attackFieldData.DamageType = eDamageType;
            attackFieldData.ActionId = this.ActionId;
            attackFieldData.PMHDBOJMEAD = _skill;
            attackFieldData.PPOJKIDHGNJ = _source;
            attackFieldData.HGMNJJBLJIO = gameObject1;
            attackFieldData.LALMMFAOJDP = gameObject2;
            attackFieldData.EnergyChargeMultiple = this.EnergyChargeMultiple;
            attackFieldData.AbsorberValue = this.battleManager.KIHOGJBONDH;
            attackFieldData.onExec = action;
            action?.Invoke("领域中心：" + attackFieldData.CenterX + "范围：" + attackFieldData.Size + "\n");
            this.battleManager.ExecField(attackFieldData, this.ActionId);
        }

        public override void SetLevel(float _level)
        {
            base.SetLevel(_level);
            this.Value[eValueNumber.VALUE_1] = (float)((double)this.MasterData.action_value_1 + (double)this.MasterData.action_value_2 * (double)_level);
            this.Value[eValueNumber.VALUE_3] = (float)((double)this.MasterData.action_value_3 + (double)this.MasterData.action_value_4 * (double)_level);
            this.Value[eValueNumber.VALUE_5] = (float)((double)this.MasterData.action_value_5 + (double)this.MasterData.action_value_6 * (double)_level);
        }
    }
}