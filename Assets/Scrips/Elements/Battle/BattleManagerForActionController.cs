// Decompiled with JetBrains decompiler
// Type: Elements.Battle.GHPNJFDPICH
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elements.Battle
{
  public interface BattleManagerForActionController
  {
    void SetSkillExeScreen(
      UnitCtrl PKHLDNEDPBH,
      float MOOBIHCMIEJ,
      Color BHIJOLHCGKC,
      bool AJMFAOIFPKA);

    void AddBlackOutTarget(UnitCtrl JELADBAMFKH, UnitCtrl LIMEKPEENOB, BasePartsData IMFNIHLGODB);

    UnitCtrl DecoyUnit { get; }

    UnitCtrl DecoyEnemy { get; }

    List<UnitCtrl> UnitList { get; }

    List<UnitCtrl> EnemyList { get; }

    CoroutineManager CoroutineManager { get; }

    HashSet<UnitCtrl> BlackoutUnitTargetList { get; }

    float DeltaTime_60fps { get; }

    void SetSkillExeScreenActive(UnitCtrl FNHGFDNICFG, Color BHIJOLHCGKC);

    List<long> PBCLBKCKHAI { get; }

    void CallbackActionEnd(long NBLAEJPILJM);

    Dictionary<string, GameObject> DAIFDPFABCM { get; }

    void AppendCoroutine(IEnumerator PGEADNDHDIL, ePauseType MHPJECIHDDL, UnitCtrl GEDLBPMPOKB);

    Coroutine StartCoroutine(IEnumerator LJBCBFFGDDK);

    bool IsDefenceReplayMode { get; }

    void StartChangeScale(Skill COKKKOEFNAB, float KCLCLFDDAHK);

    void SetForegroundEnable(bool GABGIKMFNFG);

    int KIHOGJBONDH { get; }
  }
}
