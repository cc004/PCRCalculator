// Decompiled with JetBrains decompiler
// Type: Elements.UbAbnormalData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using Elements.Battle;
using System.Collections;

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

    private BattleManager battleManager => UbAbnormalData.staticBattleManager;

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
      UbAbnormalData.staticBattleManager = BattleManager.Instance;
    }

    public void Exec(UnitCtrl _target)
    {
      if ((double) this.Timer <= 0.0)
        return;
      this.Timer = 0.0f;
      _target.SetAbnormalState(_target, this.AbnormalState, this.EffectTime, (ActionParameter) null, (Skill) null, this.Value);
    }

    public void StartTimer(UnitCtrl _target) => this.battleManager.AppendCoroutine(this.TimerUpdate(_target), ePauseType.SYSTEM, _target);

    public IEnumerator TimerUpdate(UnitCtrl _target)
    {
      do
      {
        yield return (object) null;
        this.Timer -= this.battleManager.DeltaTime_60fps;
      }
      while ((double) this.Timer >= 0.0 && !_target.IsDead);
      //this.Effect.SetTimeToDie(true);
    }
  }
}
