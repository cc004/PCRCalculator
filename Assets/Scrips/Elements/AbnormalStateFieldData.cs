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

    protected override bool judgeWorking(UnitCtrl _unit)
    {
      if (IsTargetAbnormalStateBuff)
        return true;
      if (_unit.ModeChangeUnableStateBarrier)
        return false;
      return !_unit.IsNoDamageMotion();
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
      if (TargetAction.IsChangeSpeedAction() && owner.OverwriteSpeedDataCount > 0)
      {
        return;
      }
      base.OnEnter(_parts, _source);
      if (owner.IsSummonOrPhantom)
        TargetAction.AppendTargetNum(owner, 0);
      if (!TargetAction.IsAppendTargetNum(owner))
        TargetAction.AppendTargetNum(owner, 0);
      SourceActionController.ExecAction(TargetAction, Skill, _parts, 0, 0.0f, _ignoreProtect: true);
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
      if (Enable)
        updateTarget();
    }

    public void CallUpdateTarget()
    {
      if (Enable)
        updateTarget();
    }

  }
}
