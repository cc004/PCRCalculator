﻿using System.Collections.Generic;

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
      if (_sourceActionController.Skill1IsChargeTime || ActionDetail1 != 0)
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
      eReleaseType actionDetail1 = (eReleaseType) ActionDetail1;
      ContinuousAttackNearByData _data = new ContinuousAttackNearByData
      {
        Enable = true,
        ReleaseType = actionDetail1,
        AttackType = (AttackActionBase.eAttackType) ActionDetail2,
        StateIconType = (eStateIconType) ActionDetail3,
        Size = _valueDictionary[eValueNumber.VALUE_1],
        BaseDamage = _valueDictionary[eValueNumber.VALUE_2],
        AttackEfficiency = _valueDictionary[eValueNumber.VALUE_4],
        Timer = actionDetail1 == eReleaseType.TIMER ? (float)_valueDictionary[eValueNumber.VALUE_6] : 0.0f,
        ReduceEnergy = actionDetail1 == eReleaseType.ENERGY ? (float)_valueDictionary[eValueNumber.VALUE_6] : 0.0f,
        AbsorberValue = battleManager.KIHOGJBONDH,
        Skill = _skill,
        ActionId = ActionId
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
      Value[eValueNumber.VALUE_2] = (float) (MasterData.action_value_2 + MasterData.action_value_3 * _level);
      Value[eValueNumber.VALUE_4] = (float) (MasterData.action_value_4 + MasterData.action_value_5 * _level);
      Value[eValueNumber.VALUE_6] = (float) (MasterData.action_value_6 + MasterData.action_value_7 * _level);
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

      public eReleaseType ReleaseType { get; set; }

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
