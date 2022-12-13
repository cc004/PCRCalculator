// Decompiled with JetBrains decompiler
// Type: Elements.Battle.KGNDCAGJKDC
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using PCRCaculator.Guild;
using static UnityEngine.UI.GridLayoutGroup;

namespace Elements.Battle
{
    public class BattleUnionBurstController
    {
        private BattleManager battleManager;

        public BattleUnionBurstController(BattleManager BattleManager) => battleManager = BattleManager;

        public void TryExecUnionBurst()
        {
            if (battleManager.GetIsPlayCutin() || battleManager.GameState == eBattleGameState.NEXT_WAVE_PROCESS)
                return;
            bool flag = false;
            for (int DNGOJHOHHMF = 0; DNGOJHOHHMF < battleManager.GetUnitCtrlLength(); ++DNGOJHOHHMF)
            {
                UnitCtrl unitCtrl = battleManager.GetUnitCtrl(DNGOJHOHHMF);
                if (unitCtrl.UnionBurstSkillId != 0)
                    unitCtrl.UpdateSkillTarget();
                if (unitCtrl.JudgeSkillReadyAndIsMyTurn() && !unitCtrl.lastCanReleaseSkill)
                {
                    unitCtrl.critPoint = new UnitCtrl.CritPoint
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
                        battleManager.ChargeSkillTurn = eChargeSkillTurn.PLAYER;

                        battleManager.HBDACBKGHIK = unitCtrl;
                        //this.battleManager.UnitUiCtrl.UpdateUi();
                        //if (!this.battleManager.IsDefenceReplayMode)
                        // this.battleManager.UnitUiCtrl.PlaySkillOn(unitCtrl);
                        unitCtrl.StartCutIn();
                        flag = true;
                        break;
                    }
                }
            }
            for (int DNGOJHOHHMF = 0; DNGOJHOHHMF < battleManager.GetUnitCtrlLength(); ++DNGOJHOHHMF)
                battleManager.GetUnitCtrl(DNGOJHOHHMF).IsUbExecTrying = false;
            if (flag)
                return;
            TryExecEnemyUnionBurst();
            /*for (int DNGOJHOHHMF = 0; DNGOJHOHHMF < battleManager.GetEnemyCtrlLength(); ++DNGOJHOHHMF)
            {
                UnitCtrl enemyCtrl = battleManager.GetEnemyCtrl(DNGOJHOHHMF);
                if (enemyCtrl.UnionBurstSkillId != 0)
                    enemyCtrl.UpdateSkillTarget();
                if (enemyCtrl.JudgeSkillReadyAndIsMyTurn() && enemyCtrl.CurrentState == UnitCtrl.ActionState.IDLE)
                {
                    enemyCtrl.SetDirectionAuto();
                    enemyCtrl.UpdateSkillTarget();
                    if (enemyCtrl.JudgeSkillReadyAndIsMyTurn() && enemyCtrl.CurrentState == UnitCtrl.ActionState.IDLE)
                    {
                        battleManager.ChargeSkillTurn = eChargeSkillTurn.ENEMY;
                        //this.battleManager.UnitUiCtrl.UpdateUi();
                        //if (this.battleManager.IsDefenceReplayMode)
                        //  this.battleManager.UnitUiCtrl.PlaySkillOn(enemyCtrl);
                        enemyCtrl.StartCutIn();
                        break;
                    }
                }
            }*/
        }
        public void TryExecEnemyUnionBurst()
        {
            for (int i = 0; i < battleManager.GetEnemyCtrlLength(); i++)
            {
                UnitCtrl enemyCtrl = battleManager.GetEnemyCtrl(i);
                if (enemyCtrl.UnionBurstSkillId != 0)
                {
                    enemyCtrl.UpdateSkillTarget();
                }
                if ((enemyCtrl.IsSummonOrPhantom && (enemyCtrl.SummonSource == null || (long)enemyCtrl.SummonSource.Hp <= 0)) || !enemyCtrl.JudgeSkillReadyAndIsMyTurn() || enemyCtrl.CurrentState != 0)
                {
                    if (!enemyCtrl.energyjudged && enemyCtrl.JudgeSkillReadyAndIsMyTurnExceptEnergy() && enemyCtrl.CurrentState == UnitCtrl.ActionState.IDLE)
                    {
                        enemyCtrl.energyjudged = true;
                        var e = enemyCtrl.Energy;

                        GuildCalculator.Instance.dmglist.Add(new ProbEvent
                        {
                            unit = enemyCtrl.UnitNameEx,
                            predict = hash => e.Emulate(hash) >= 1000f,
                            exp = hash => $"{e.ToExpression(hash)} >= 1000",
                            description = $"({BattleHeaderController.CurrentFrameCount}){enemyCtrl.UnitNameEx}的UB提前开出"
                        });
                    }
                    continue;
                }
                enemyCtrl.SetDirectionAuto();
                enemyCtrl.UpdateSkillTarget();
                if (enemyCtrl.JudgeSkillReadyAndIsMyTurn() && enemyCtrl.CurrentState == UnitCtrl.ActionState.IDLE)
                {
                    battleManager.ChargeSkillTurn = eChargeSkillTurn.ENEMY;
                    battleManager.HBDACBKGHIK = enemyCtrl;
                    //battleManager.OFMPGBKBOPM.UpdateUi();
                    //if (battleManager.BJKKBMOLHDH)
                    //{
                    //    battleManager.OFMPGBKBOPM.PlaySkillOn(enemyCtrl);
                    //}
                    enemyCtrl.StartCutIn();
                    break;
                }
            }
        }


        public void Release() => battleManager = null;
    }
}
