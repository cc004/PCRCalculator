﻿// Decompiled with JetBrains decompiler
// Type: Elements.AbnormalStateFieldAction
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using Elements.Battle;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Elements
{
  public class AbnormalStateFieldAction : ActionParameter
  {
    private ActionParameter targetAction;

    public UnitCtrl.eAbnormalState TargetAbnormalState { get; set; }

    public override void ExecActionOnStart(
      Skill _skill,
      UnitCtrl _source,
      UnitActionController _sourceActionController)
    {
      base.ExecActionOnStart(_skill, _source, _sourceActionController);
      this.targetAction = _skill.ActionParameters.Find((Predicate<ActionParameter>) (e => e.ActionId == this.ActionDetail1));
      this.targetAction.AbnormalStateFieldAction = this;
    }

    public override void ReadyAction(
      UnitCtrl _source,
      UnitActionController _sourceActionController,
      Skill _skill)
    {
      base.ReadyAction(_source, _sourceActionController, _skill);
      this.targetAction.CancelByIfForAll = true;
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
      Action<string> action = null)
    {
      base.ExecAction(_source, _target, _num, _sourceActionController, _skill, _starttime, _enabledChildAction, _valueDictionary);
      GameObject gameObject1 = (GameObject) null;
      GameObject gameObject2 = (GameObject) null;
      if (this.ActionEffectList.Count > 0)
      {
        gameObject1 = this.ActionEffectList[0].Prefab;
        gameObject2 = this.ActionEffectList[0].PrefabLeft;
      }
      AbnormalStateFieldData abnormalStateFieldData = new AbnormalStateFieldData();
      abnormalStateFieldData.KNLCAOOKHPP = eFieldType.HEAL;
      abnormalStateFieldData.HKDBJHAIOMB = eFieldExecType.NORMAL;
      abnormalStateFieldData.StayTime = _valueDictionary[eValueNumber.VALUE_1];
      abnormalStateFieldData.CenterX = _target.GetLocalPosition().x + this.Position;
      abnormalStateFieldData.Size = _valueDictionary[eValueNumber.VALUE_3];
      abnormalStateFieldData.EGEPDDJBILL = (double) _skill.BlackOutTime > 0.0 ? _source : (UnitCtrl) null;
      abnormalStateFieldData.TargetList = new List<BasePartsData>();
      abnormalStateFieldData.PPOJKIDHGNJ = _source;
      abnormalStateFieldData.HGMNJJBLJIO = gameObject1;
      abnormalStateFieldData.LALMMFAOJDP = gameObject2;
      abnormalStateFieldData.TargetAction = this.targetAction;
      abnormalStateFieldData.SourceActionController = _sourceActionController;
      abnormalStateFieldData.LCHLGLAFJED = _source.IsOther ? eFieldTargetType.PLAYER : eFieldTargetType.ENEMY;
      abnormalStateFieldData.PMHDBOJMEAD = _skill;
            //add scripts
            string describe = "展开中心为" + abnormalStateFieldData.CenterX +"，大小为" + abnormalStateFieldData.Size + "的领域";
            action(describe);
            //end add
      this.battleManager.ExecField((AbnormalStateDataBase) abnormalStateFieldData, this.ActionId);
    }

    public override void SetLevel(float _level)
    {
      base.SetLevel(_level);
      this.Value[eValueNumber.VALUE_1] = (float) ((double) this.MasterData.action_value_1 + (double) this.MasterData.action_value_2 * (double) _level);
    }
  }
}