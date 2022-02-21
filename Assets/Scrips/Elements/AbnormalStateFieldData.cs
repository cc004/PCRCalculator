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

    public override void OnExit(BasePartsData _parts)
    {
      base.OnExit(_parts);
      TargetAction.ResetHitData();
      _parts.Owner.DisableAbnormalStateById(TargetAction.AbnormalStateFieldAction.TargetAbnormalState, TargetAction.ActionId, false);
    }

    public override void OnEnter(BasePartsData _parts)
    {
      UnitCtrl owner = _parts.Owner;
      base.OnEnter(_parts);
      if (_parts.Owner.IsSummonOrPhantom)
        TargetAction.AppendTargetNum(_parts.Owner, 0);
      SourceActionController.ExecAction(TargetAction, PMHDBOJMEAD, _parts, 0, 0.0f);
    }

    public override void ResetTarget(UnitCtrl _unit, UnitCtrl.eAbnormalState _abnormalState)
    {
      base.ResetTarget(_unit, _abnormalState);
      if (TargetAction.AbnormalStateFieldAction.TargetAbnormalState != _abnormalState)
        return;
      TargetAction.ResetHitData();
      if (!_unit.IsPartsBoss)
      {
        TargetList.Remove(_unit.GetFirstParts());
      }
      else
      {
        for (int index = 0; index < _unit.BossPartsListForBattle.Count; ++index)
          TargetList.Remove(_unit.BossPartsListForBattle[index]);
      }
    }
  }
}
