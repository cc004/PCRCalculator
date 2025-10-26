// Decompiled with JetBrains decompiler
// Type: Elements.AbnormalStateFieldData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using Elements.Battle;

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

    public override void OnExit(BasePartsData _parts, UnitCtrl _source)
    {
      base.OnExit(_parts, _source);
      TargetAction.ResetHitData();
      _parts.Owner.DisableAbnormalStateById(TargetAction.AbnormalStateFieldAction.TargetAbnormalState, TargetAction.ActionId, false);
    }

    public override void OnEnter(BasePartsData _parts, UnitCtrl _source)
    {
      UnitCtrl owner = _parts.Owner;
      if (this.TargetAction.ActionType == eActionType.CHANGE_SPEED && (this.TargetAction.ActionDetail1 == 1 || this.TargetAction.ActionDetail1 == 2) && owner.OverwriteSpeedDataCount > 0)
      {
        return;
      }
      base.OnEnter(_parts, _source);
      if (_parts.Owner.IsSummonOrPhantom)
        TargetAction.AppendTargetNum(_parts.Owner, 0);
      SourceActionController.ExecAction(TargetAction, PMHDBOJMEAD, _parts, 0, 0.0f);
    }

    public override void ResetTarget(UnitCtrl _unit, UnitCtrl.eAbnormalState _abnormalState, bool _strict)
    {
      base.ResetTarget(_unit, _abnormalState, _strict);
      if (_strict)
      {
        if (TargetAction.AbnormalStateFieldAction.TargetAbnormalState != _abnormalState)
          return;
      }
      else
      {
        if (UnitCtrl.GetAbnormalStateCategory(TargetAction.AbnormalStateFieldAction.TargetAbnormalState) != UnitCtrl.GetAbnormalStateCategory(_abnormalState))
          return;
      }
      TargetAction.ResetHitData();
      if (!_unit.IsPartsBoss)
      {
        TargetList.Remove(_unit.GetFirstParts());
        TargetSet.Remove(_unit.GetFirstParts());
      }
      else
      {
        for (int index = 0; index < _unit.BossPartsListForBattle.Count; ++index)
        {
          TargetList.Remove(_unit.BossPartsListForBattle[index]);
          TargetSet.Remove(_unit.BossPartsListForBattle[index]);
        }
      }
    }
  }
}
