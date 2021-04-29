// Decompiled with JetBrains decompiler
// Type: Elements.StrikeBackData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using Cute;
using Elements.Battle;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Elements
{
  public class StrikeBackData : ISingletonField
  {
    //protected static readonly Yggdrasil<StrikeBackData> SINGLETON_TREE = SingletonTreeTreeFunc.CreateSingletonTree<StrikeBackData>();
    //private BattleEffectPoolInterface battleEffectPool;

    public StrikeBackData.eStrikeBackType StrikeBackType { get; set; }

    public int Damage { get; set; }

    public EnchantStrikeBackAction.eStrikeBackEffectType EffectType { get; set; }

    public int ActionId { get; set; }

    public StrikeBackDataSet DataSet { get; set; }

    public bool IsDieing { get; set; }

    public bool Execed { get; set; }

    public List<NormalSkillEffect> ActionEffectList { get; set; }

    public Skill Skill { get; set; }

    //public StrikeBackData() => this.battleEffectPool = (BattleEffectPoolInterface) StrikeBackData.SINGLETON_TREE.Get<BattleEffectPool>();

    public void Exec(UnitCtrl _target, UnitCtrl _source, int _recovery, Action _callback)
    {
      this.Execed = true;
      if (_target.CurrentState == UnitCtrl.ActionState.SKILL_1)
        _target.AppendCoroutine(this.waitStateIdle(_target, (Action) (() => this.execImpl(_target, _source, _recovery, _callback))), ePauseType.SYSTEM, _source);
      else
        this.execImpl(_target, _source, _recovery, _callback);
    }

    private IEnumerator waitStateIdle(UnitCtrl _target, Action _callback)
    {
      while (_target.CurrentState == UnitCtrl.ActionState.SKILL_1)
        yield return (object) null;
      _callback.Call();
    }

    private void execImpl(UnitCtrl _target, UnitCtrl _source, int _recovery, Action _callback)
    {
      this.IsDieing = true;
      DamageData _damageData = new DamageData()
      {
        Target = _target.GetFirstParts(true),
        Damage = (long) this.Damage,
        DamageType = DamageData.eDamageType.NONE,
        ActionType = eActionType.ENCHANT_STRIKE_BACK
      };
      _target.SetDamage(_damageData, false, this.ActionId);
      //CircleEffectController skillEffect = this.DataSet.SkillEffect as CircleEffectController;
      switch (this.StrikeBackType)
      {
        case StrikeBackData.eStrikeBackType.PHYSICAL_DRAIN:
        case StrikeBackData.eStrikeBackType.MAGIC_DRAIN:
        case StrikeBackData.eStrikeBackType.BOTH_DRAIN:
          _source.SetRecovery(_recovery, UnitCtrl.eInhibitHealType.PHYSICS, _source);
          break;
      }
      /*switch (this.EffectType)
      {
        case EnchantStrikeBackAction.eStrikeBackEffectType.TOY:
        case EnchantStrikeBackAction.eStrikeBackEffectType.OMEME:
        case EnchantStrikeBackAction.eStrikeBackEffectType.OMEME_PLUS:
          skillEffect.DisableTop(_source.IsLeftDir, _callback);
          break;
      }*/
      /*for (int index = 0; index < this.ActionEffectList.Count; ++index)
      {
        SkillEffectCtrl effect = this.battleEffectPool.GetEffect(_source.IsLeftDir ? this.ActionEffectList[index].PrefabLeft : this.ActionEffectList[index].Prefab);
        effect.transform.parent = ExceptNGUIRoot.Transform;
        effect.InitializeSort();
        effect.PlaySe(_source.SoundUnitId, _source.IsLeftDir);
        effect.SetPossitionAppearanceType(this.ActionEffectList[index], _target.GetFirstParts(true), _source, this.Skill);
        effect.ExecAppendCoroutine((double) this.Skill.BlackOutTime > 0.0 ? _source : (UnitCtrl) null);
      }*/
    }

    public enum eStrikeBackType
    {
      PHYSICAL_GUARD = 1,
      MAGIC_GUARD = 2,
      PHYSICAL_DRAIN = 3,
      MAGIC_DRAIN = 4,
      BOTH_GUARD = 5,
      BOTH_DRAIN = 6,
    }
  }
}
