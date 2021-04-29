// Decompiled with JetBrains decompiler
// Type: Elements.Battle.BattleEffectPoolInterface
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using UnityEngine;

namespace Elements.Battle
{
    public interface BattleEffectPoolInterface
    {
        SkillEffectCtrl GetEffect(GameObject MDOJNMEMHLN, UnitCtrl DCMBKLBBCHD = null);
        SkillEffectCtrl GetEffect(GameObject MDOJNMEMHLN,FirearmCtrlData prefabData, UnitCtrl DCMBKLBBCHD = null);

        //DamageEffectCtrlBase LoadNumberEffect(GameObject MDOJNMEMHLN);
    }
}
