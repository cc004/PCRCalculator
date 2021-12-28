// Decompiled with JetBrains decompiler
// Type: Elements.AweAction
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using Elements.Battle;
using System.Collections.Generic;
using UnityEngine;

namespace Elements
{
  public class AweAction : ActionParameter
  {
    public const int AWE_NOT_COUNT_VALUE = -1;

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
            double dodgeByLevelDiff = (double)BattleUtil.GetDodgeByLevelDiff(_skill.Level, _target.GetLevel());

            double num = (double) BattleManager.Random(0.0f, 1f, new PCRCaculator.Guild.RandomData(_source, _target.Owner, ActionId, 4, (float)dodgeByLevelDiff));
      AweAction.AweData _aweData = new AweAction.AweData()
      {
        LifeTime = _valueDictionary[eValueNumber.VALUE_3],
        AweType = (AweAction.eAweType) this.ActionDetail1,
        CountLimit = this.ActionDetail2 == 0 ? -1 : this.ActionDetail2,
        Value = _valueDictionary[eValueNumber.VALUE_1] / 100f
      };
      /*if (this.ActionEffectList.Count != 0)
      {
        NormalSkillEffect actionEffect = this.ActionEffectList[0];
        GameObject MDOJNMEMHLN = _target.Owner.IsLeftDir ? actionEffect.PrefabLeft : actionEffect.Prefab;
        SkillEffectCtrl skillEffectCtrl = _aweData.Effect = this.battleEffectPool.GetEffect(MDOJNMEMHLN);
        skillEffectCtrl.transform.parent = ExceptNGUIRoot.Transform;
        skillEffectCtrl.SortTarget = _target.Owner;
        skillEffectCtrl.InitializeSort();
        skillEffectCtrl.SetPossitionAppearanceType(actionEffect, _target, _target.Owner, _skill);
        skillEffectCtrl.ExecAppendCoroutine();
      }*/
      //double dodgeByLevelDiff = (double) BattleUtil.GetDodgeByLevelDiff(_skill.Level, _target.GetLevel());
      if (num <= dodgeByLevelDiff)
      {
        this.AppendIsAlreadyExeced(_target.Owner, _num);
        _target.Owner.AddAweData(_aweData);
      }
      else
      {
        ActionExecedData actionExecedData = this.AlreadyExecedData[_target.Owner][_num];
        if (actionExecedData.ExecedPartsNumber != actionExecedData.TargetPartsNumber)
          return;
        if (actionExecedData.TargetPartsNumber == 1)
          _target.Owner.SetMissAtk(_source, eMissLogType.DODGE_CHARM, _parts: _target);
        else
          _target.Owner.SetMissAtk(_source, eMissLogType.DODGE_CHARM);
      }
    }

    public override void SetLevel(float _level)
    {
      base.SetLevel(_level);
      this.Value[eValueNumber.VALUE_1] = (float) ((double) this.MasterData.action_value_1 + (double) this.MasterData.action_value_2 * (double) _level);
      this.Value[eValueNumber.VALUE_3] = (float) ((double) this.MasterData.action_value_3 + (double) this.MasterData.action_value_4 * (double) _level);
    }

    public class AweData
    {
      public float Value;
      public float LifeTime;
      public int CountLimit = -1;
      public AweAction.eAweType AweType = AweAction.eAweType.UB_AND_SKILL;
      public SkillEffectCtrl Effect;
    }

    public enum eAweType
    {
      UB_ONLY,
      UB_AND_SKILL,
    }
  }
}
