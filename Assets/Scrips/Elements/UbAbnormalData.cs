// Decompiled with JetBrains decompiler
// Type: Elements.UbAbnormalData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System.Collections;
using Elements.Battle;

namespace Elements
{
  public class UbAbnormalData : ISingletonField
  {
    //private static Yggdrasil<UbAbnormalData> staticSingletonTree;
    private static BattleManager staticBattleManager;

    public UnitCtrl.eAbnormalState AbnormalState { get; set; }

    public float EffectTime { get; set; }

    public float Value { get; set; }

    public float Timer { get; set; }

    public SkillEffectCtrl Effect { get; set; }

    private BattleManager battleManager => staticBattleManager;

    public static void StaticRelease()
    {
     // UbAbnormalData.staticSingletonTree = (Yggdrasil<UbAbnormalData>) null;
      //UbAbnormalData.staticBattleManager = (NJMKHBJJPFF) null;
    }

    public UbAbnormalData()
    {
      //if (UbAbnormalData.staticSingletonTree != null)
      //  return;
      //UbAbnormalData.staticSingletonTree = this.CreateSingletonTree<UbAbnormalData>();
      staticBattleManager = BattleManager.Instance;
    }

        public UnitCtrl Source { get; set; }

        public void Exec(UnitCtrl _target)
        {
            if (this.Timer > 0f)
            {
                this.Timer = 0f;
                SealData unableStateGuardData = _target.UnableStateGuardData;
                if (this.Source.IsOther != _target.IsOther && ChangeSpeedAction.IsUnableAbnormalState(this.AbnormalState) && unableStateGuardData != null && unableStateGuardData.GetCurrentCount() > 0 && !_target.IsNoDamageMotion() && !_target.IsAbnormalState(UnitCtrl.eAbnormalState.NO_ABNORMAL))
                {
                    if (unableStateGuardData.DisplayCount)
                    {
                        unableStateGuardData.RemoveSeal(1, true);
                    }
                    return;
                }
                _target.SetAbnormalState(_target, this.AbnormalState, this.EffectTime, null, null, this.Value, 0f, false, false, 1f, true);
            }
        }

    public void StartTimer(UnitCtrl _target) => battleManager.AppendCoroutine(TimerUpdate(_target), ePauseType.SYSTEM, _target);

    public IEnumerator TimerUpdate(UnitCtrl _target)
    {
      do
      {
        yield return null;
        Timer -= battleManager.DeltaTime_60fps;
      }
      while (Timer >= 0.0 && !_target.IsDead);
      //this.Effect.SetTimeToDie(true);
    }
  }
}
