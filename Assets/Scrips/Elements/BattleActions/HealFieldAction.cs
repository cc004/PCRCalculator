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
      parts = _source.BossPartsListForBattle.Find(e => e.Index == _skill.ParameterTarget);
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
      float num = ActionDetail1 % 2 != 0 ? _valueDictionary[eValueNumber.VALUE_1] + _valueDictionary[eValueNumber.VALUE_3] * (_source.IsPartsBoss ? parts.GetAtkZero() : (float) (int) _source.AtkZero) : _valueDictionary[eValueNumber.VALUE_1] + _valueDictionary[eValueNumber.VALUE_3] * (_source.IsPartsBoss ? parts.GetMagicStrZero() : (float) (int) _source.MagicStrZero);
      HealFieldData healFieldData = new HealFieldData();
      healFieldData.KNLCAOOKHPP = eFieldType.HEAL;
      healFieldData.HKDBJHAIOMB = ActionDetail1 > 2 ? eFieldExecType.REPEAT : eFieldExecType.NORMAL;
      healFieldData.StayTime = _valueDictionary[eValueNumber.VALUE_5];
      healFieldData.CenterX = _target.GetLocalPosition().x + Position;
      healFieldData.Size = _valueDictionary[eValueNumber.VALUE_7];
      healFieldData.LCHLGLAFJED = _source.IsOther ? eFieldTargetType.ENEMY : eFieldTargetType.PLAYER;
      healFieldData.EGEPDDJBILL = _skill.BlackOutTime > 0.0 ? _source : null;
      healFieldData.ValueType = (HealFieldData.eValueType) ActionDetail2;
      healFieldData.EffectType = (HealFieldData.eEffectType) ActionDetail3;
      healFieldData.TargetList = new List<BasePartsData>();
      healFieldData.Value = num;
      healFieldData.PPOJKIDHGNJ = _source;
      healFieldData.IsMagic = ActionDetail1 % 2 == 0;
      battleManager.ExecField(healFieldData, ActionId);
    }

    public override void SetLevel(float _level)
    {
      base.SetLevel(_level);
      Value[eValueNumber.VALUE_1] = (float) (MasterData.action_value_1 + MasterData.action_value_2 * _level);
      Value[eValueNumber.VALUE_3] = (float) (MasterData.action_value_3 + MasterData.action_value_4 * _level);
      Value[eValueNumber.VALUE_5] = (float) (MasterData.action_value_5 + MasterData.action_value_6 * _level);
    }
  }
}
