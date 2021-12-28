// Decompiled with JetBrains decompiler
// Type: Elements.ContinuousAttackNearByAction
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

namespace Elements
{
  public class ContinuousAttackNearByAction : ActionParameter
  {
    public override void ExecActionOnStart(
      Skill _skill,
      UnitCtrl _source,
      UnitActionController _sourceActionController)
    {
      base.ExecActionOnStart(_skill, _source, _sourceActionController);
      if (_sourceActionController.Skill1IsChargeTime || this.ActionDetail1 != 0)
        return;
      _sourceActionController.Skill1IsChargeTime = _skill.SkillId == _source.UnionBurstSkillId;
    }

    public override void ExecAction(
      UnitCtrl _source,
      BasePartsData _target,
      int _num,
      UnitActionController _sourceActionController,
      Skill _skill,
      float _starttime,
      Dictionary<int, bool> _enabledChildAction,
      Dictionary<eValueNumber, FloatWithEx> _valueDictionary)
    {
      base.ExecAction(_source, _target, _num, _sourceActionController, _skill, _starttime, _enabledChildAction, _valueDictionary);
      ContinuousAttackNearByAction.eReleaseType actionDetail1 = (ContinuousAttackNearByAction.eReleaseType) this.ActionDetail1;
      ContinuousAttackNearByAction.ContinuousAttackNearByData _data = new ContinuousAttackNearByAction.ContinuousAttackNearByData()
      {
        Enable = true,
        ReleaseType = actionDetail1,
        AttackType = (AttackActionBase.eAttackType) this.ActionDetail2,
        StateIconType = (eStateIconType) this.ActionDetail3,
        Size = _valueDictionary[eValueNumber.VALUE_1],
        BaseDamage = _valueDictionary[eValueNumber.VALUE_2],
        AttackEfficiency = _valueDictionary[eValueNumber.VALUE_4],
        Timer = actionDetail1 == ContinuousAttackNearByAction.eReleaseType.TIMER ? (float)_valueDictionary[eValueNumber.VALUE_6] : 0.0f,
        ReduceEnergy = actionDetail1 == ContinuousAttackNearByAction.eReleaseType.ENERGY ? (float)_valueDictionary[eValueNumber.VALUE_6] : 0.0f,
        AbsorberValue = this.battleManager.KIHOGJBONDH,
        Skill = _skill,
        ActionId = this.ActionId
      };
      /*if (this.ActionEffectList.Count != 0)
      {
        NormalSkillEffect actionEffect = this.ActionEffectList[0];
        GameObject MDOJNMEMHLN = _target.Owner.IsLeftDir ? actionEffect.PrefabLeft : actionEffect.Prefab;
        SkillEffectCtrl skillEffectCtrl = _data.Effect = this.battleEffectPool.GetEffect(MDOJNMEMHLN);
        skillEffectCtrl.transform.parent = ExceptNGUIRoot.Transform;
        skillEffectCtrl.SortTarget = _target.Owner;
        skillEffectCtrl.InitializeSort();
        skillEffectCtrl.SetPossitionAppearanceType(actionEffect, _target, _target.Owner, _skill);
        skillEffectCtrl.ExecAppendCoroutine();
      }*/
      //_target.Owner.StartCountinuousAttackNearByAttack(_data);
    }

    public override void SetLevel(float _level)
    {
      base.SetLevel(_level);
      this.Value[eValueNumber.VALUE_2] = (float) ((double) this.MasterData.action_value_2 + (double) this.MasterData.action_value_3 * (double) _level);
      this.Value[eValueNumber.VALUE_4] = (float) ((double) this.MasterData.action_value_4 + (double) this.MasterData.action_value_5 * (double) _level);
      this.Value[eValueNumber.VALUE_6] = (float) ((double) this.MasterData.action_value_6 + (double) this.MasterData.action_value_7 * (double) _level);
    }

    public enum eReleaseType
    {
      ENERGY,
      TIMER,
    }

    public class ContinuousAttackNearByData
    {
      public SkillEffectCtrl Effect;

      public bool Enable { get; set; }

      public ContinuousAttackNearByAction.eReleaseType ReleaseType { get; set; }

      public AttackActionBase.eAttackType AttackType { get; set; } = AttackActionBase.eAttackType.PHYSICAL;

      public eStateIconType StateIconType { get; set; }

      public float Size { get; set; }

      public float BaseDamage { get; set; }

      public float AttackEfficiency { get; set; }

      public float Timer { get; set; }

      public float ReduceEnergy { get; set; }

      public int AbsorberValue { get; set; }

      public Skill Skill { get; set; }

      public int ActionId { get; set; }
    }
  }
}
