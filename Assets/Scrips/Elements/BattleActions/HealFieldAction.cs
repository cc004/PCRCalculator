// Decompiled with JetBrains decompiler
// Type: Elements.HealFieldAction
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using Elements.Battle;
using System;
using System.Collections.Generic;

namespace Elements
{
  public class HealFieldAction : ActionParameter
  {
    private const int REPEAT_NUMBER = 2;
    private BasePartsData parts;

    public override void Initialize()
    {
      base.Initialize();
      //Singleton<LCEGKJFKOPD>.Instance.LoadHealFieldEffect();
    }

    public override void ExecActionOnStart(
      Skill _skill,
      UnitCtrl _source,
      UnitActionController _sourceActionController)
    {
      base.ExecActionOnStart(_skill, _source, _sourceActionController);
      this.parts = (BasePartsData) _source.BossPartsListForBattle.Find((Predicate<PartsData>) (e => e.Index == _skill.ParameterTarget));
    }

    public override void ExecAction(
      UnitCtrl _source,
      BasePartsData _target,
      int _num,
      UnitActionController _sourceActionController,
      Skill _skill,
      float _starttime,
      Dictionary<int, bool> _enabledChildAction,
      Dictionary<eValueNumber, float> _valueDictionary)
    {
      base.ExecAction(_source, _target, _num, _sourceActionController, _skill, _starttime, _enabledChildAction, _valueDictionary);
      float num = this.ActionDetail1 % 2 != 0 ? _valueDictionary[eValueNumber.VALUE_1] + _valueDictionary[eValueNumber.VALUE_3] * (_source.IsPartsBoss ? (float) this.parts.GetAtkZero() : (float) (int) _source.AtkZero) : _valueDictionary[eValueNumber.VALUE_1] + _valueDictionary[eValueNumber.VALUE_3] * (_source.IsPartsBoss ? (float) this.parts.GetMagicStrZero() : (float) (int) _source.MagicStrZero);
      HealFieldData healFieldData = new HealFieldData();
      healFieldData.KNLCAOOKHPP = eFieldType.HEAL;
      healFieldData.HKDBJHAIOMB = this.ActionDetail1 > 2 ? eFieldExecType.REPEAT : eFieldExecType.NORMAL;
      healFieldData.StayTime = _valueDictionary[eValueNumber.VALUE_5];
      healFieldData.CenterX = _target.GetLocalPosition().x + this.Position;
      healFieldData.Size = _valueDictionary[eValueNumber.VALUE_7];
      healFieldData.LCHLGLAFJED = _source.IsOther ? eFieldTargetType.ENEMY : eFieldTargetType.PLAYER;
      healFieldData.EGEPDDJBILL = (double) _skill.BlackOutTime > 0.0 ? _source : (UnitCtrl) null;
      healFieldData.ValueType = (HealFieldData.eValueType) this.ActionDetail2;
      healFieldData.EffectType = (HealFieldData.eEffectType) this.ActionDetail3;
      healFieldData.TargetList = new List<BasePartsData>();
      healFieldData.Value = num;
      healFieldData.PPOJKIDHGNJ = _source;
      healFieldData.IsMagic = this.ActionDetail1 % 2 == 0;
      this.battleManager.ExecField(healFieldData, this.ActionId);
    }

    public override void SetLevel(float _level)
    {
      base.SetLevel(_level);
      this.Value[eValueNumber.VALUE_1] = (float) ((double) this.MasterData.action_value_1 + (double) this.MasterData.action_value_2 * (double) _level);
      this.Value[eValueNumber.VALUE_3] = (float) ((double) this.MasterData.action_value_3 + (double) this.MasterData.action_value_4 * (double) _level);
      this.Value[eValueNumber.VALUE_5] = (float) ((double) this.MasterData.action_value_5 + (double) this.MasterData.action_value_6 * (double) _level);
    }
  }
}
