﻿// Decompiled with JetBrains decompiler
// Type: Elements.AbnormalStateFieldData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using Elements.Battle;
using UnityEngine;

namespace Elements
{
  public class AbnormalStateFieldData : AbnormalStateDataBase
  {
    public ActionParameter TargetAction { get; set; }

    public UnitActionController SourceActionController { get; set; }

    public override void StartField()
    {
      /*if ((Object) this.HGMNJJBLJIO != (Object) null)
      {
        this.skillEffect = this.IPEGKPHMBBN.GetEffect(this.PPOJKIDHGNJ.IsLeftDir ? this.LALMMFAOJDP : this.HGMNJJBLJIO);
        this.initializeSkillEffect();
      }*/
      base.StartField();
    }

    public override void OnRepeat()
    {
    }

    public override void OnExit(BasePartsData _parts)
    {
      base.OnExit(_parts);
      this.TargetAction.ResetHitData();
      _parts.Owner.DisableAbnormalStateById(this.TargetAction.AbnormalStateFieldAction.TargetAbnormalState, this.TargetAction.ActionId, false);
    }

    public override void OnEnter(BasePartsData _parts)
    {
      UnitCtrl owner = _parts.Owner;
      base.OnEnter(_parts);
      if (_parts.Owner.IsSummonOrPhantom)
        this.TargetAction.AppendTargetNum(_parts.Owner, 0);
      this.SourceActionController.ExecAction(this.TargetAction, this.PMHDBOJMEAD, _parts, 0, 0.0f);
    }

    public override void ResetTarget(UnitCtrl _unit, UnitCtrl.eAbnormalState _abnormalState)
    {
      base.ResetTarget(_unit, _abnormalState);
      if (this.TargetAction.AbnormalStateFieldAction.TargetAbnormalState != _abnormalState)
        return;
      this.TargetAction.ResetHitData();
      if (!_unit.IsPartsBoss)
      {
        this.TargetList.Remove(_unit.GetFirstParts());
      }
      else
      {
        for (int index = 0; index < _unit.BossPartsListForBattle.Count; ++index)
          this.TargetList.Remove((BasePartsData) _unit.BossPartsListForBattle[index]);
      }
    }
  }
}