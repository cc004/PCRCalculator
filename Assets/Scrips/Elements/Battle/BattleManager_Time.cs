// Decompiled with JetBrains decompiler
// Type: Elements.Battle.HGBODHJEPAH
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System.Collections;

namespace Elements.Battle
{
  public interface BattleManager_Time
  {
    float DeltaTime_60fps { get; }

    void AppendCoroutine(IEnumerator PGEADNDHDIL, ePauseType MHPJECIHDDL, UnitCtrl GEDLBPMPOKB);

    void StartCoroutineIgnoreFps(IEnumerator PGEADNDHDIL);
  }
}
