// Decompiled with JetBrains decompiler
// Type: Elements.KnightGuardData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

namespace Elements
{
  public class KnightGuardData
  {
    public int Value;
    public float LifeTime;
    public KnightGuardAction.eKnightGuardType KnightGuardType = KnightGuardAction.eKnightGuardType.HEAL;
    public SkillEffectCtrl Effect;
    public NormalSkillEffect ExecEffectData;
    public eStateIconType StateIconType = eStateIconType.INVALID_VALUE;
    public UnitCtrl.eInhibitHealType InhibitHealType = UnitCtrl.eInhibitHealType.NO_EFFECT;
    public Skill Skill;
    public UnitCtrl Source;
  }
}
