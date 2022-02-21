// Decompiled with JetBrains decompiler
// Type: Elements.Battle.KGNDCAGJKDC
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

namespace Elements.Battle
{
  public class BattleUnionBurstController
  {
    private BattleManager battleManager;

    public BattleUnionBurstController(BattleManager BattleManager) => this.battleManager = BattleManager;

    public void TryExecUnionBurst()
    {
      if (this.battleManager.GetIsPlayCutin() || this.battleManager.GameState == eBattleGameState.NEXT_WAVE_PROCESS)
        return;
      bool flag = false;
      for (int DNGOJHOHHMF = 0; DNGOJHOHHMF < this.battleManager.GetUnitCtrlLength(); ++DNGOJHOHHMF)
      {
        UnitCtrl unitCtrl = this.battleManager.GetUnitCtrl(DNGOJHOHHMF);
        if (unitCtrl.UnionBurstSkillId != 0)
          unitCtrl.UpdateSkillTarget();
        if (unitCtrl.JudgeSkillReadyAndIsMyTurn() && !unitCtrl.lastCanReleaseSkill)
        {
            unitCtrl.critPoint = new UnitCtrl.CritPoint()
            {
                description = "连点",
                description2 = "连点",
                priority = UnitCtrl.eCritPointPriority.FullEnergy
            };
        }
        unitCtrl.lastCanReleaseSkill = unitCtrl.JudgeSkillReadyAndIsMyTurn();

        if (unitCtrl.IsAutoOrUbExecTrying() && unitCtrl.JudgeSkillReadyAndIsMyTurn())
        {
          unitCtrl.SetDirectionAuto();
          unitCtrl.UpdateSkillTarget();
          if (unitCtrl.IsAutoOrUbExecTrying() && unitCtrl.JudgeSkillReadyAndIsMyTurn())
          {
            this.battleManager.ChargeSkillTurn = eChargeSkillTurn.PLAYER;
            //this.battleManager.UnitUiCtrl.UpdateUi();
            //if (!this.battleManager.IsDefenceReplayMode)
             // this.battleManager.UnitUiCtrl.PlaySkillOn(unitCtrl);
            unitCtrl.StartCutIn();
            flag = true;
            break;
          }
        }
      }
      for (int DNGOJHOHHMF = 0; DNGOJHOHHMF < this.battleManager.GetUnitCtrlLength(); ++DNGOJHOHHMF)
        this.battleManager.GetUnitCtrl(DNGOJHOHHMF).IsUbExecTrying = false;
      if (flag)
        return;
      for (int DNGOJHOHHMF = 0; DNGOJHOHHMF < this.battleManager.GetEnemyCtrlLength(); ++DNGOJHOHHMF)
      {
        UnitCtrl enemyCtrl = this.battleManager.GetEnemyCtrl(DNGOJHOHHMF);
        if (enemyCtrl.UnionBurstSkillId != 0)
          enemyCtrl.UpdateSkillTarget();
        if (enemyCtrl.JudgeSkillReadyAndIsMyTurn() && enemyCtrl.CurrentState == UnitCtrl.ActionState.IDLE)
        {
          enemyCtrl.SetDirectionAuto();
          enemyCtrl.UpdateSkillTarget();
          if (enemyCtrl.JudgeSkillReadyAndIsMyTurn() && enemyCtrl.CurrentState == UnitCtrl.ActionState.IDLE)
          {
            this.battleManager.ChargeSkillTurn = eChargeSkillTurn.ENEMY;
            //this.battleManager.UnitUiCtrl.UpdateUi();
            //if (this.battleManager.IsDefenceReplayMode)
            //  this.battleManager.UnitUiCtrl.PlaySkillOn(enemyCtrl);
            enemyCtrl.StartCutIn();
            break;
          }
        }
      }
    }

    public void Release() => this.battleManager =  null;
  }
}
