// Decompiled with JetBrains decompiler
// Type: Elements.DamageCutAction
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System.Collections.Generic;

namespace Elements
{
  public class DamageCutAction : ActionParameter
  {
    private static readonly Dictionary<DamageCutAction.eDamageCutType, UnitCtrl.eAbnormalState> abnormalStateDic = new Dictionary<DamageCutAction.eDamageCutType, UnitCtrl.eAbnormalState>()
    {
      {
        DamageCutAction.eDamageCutType.ATK,
        UnitCtrl.eAbnormalState.CUT_ATK_DAMAGE
      },
      {
        DamageCutAction.eDamageCutType.MGC,
        UnitCtrl.eAbnormalState.CUT_MGC_DAMAGE
      },
      {
        DamageCutAction.eDamageCutType.ALL,
        UnitCtrl.eAbnormalState.CUT_ALL_DAMAGE
      }
    };

    public override void ExecActionOnStart(
      Skill _skill,
      UnitCtrl _source,
      UnitActionController _sourceActionController) => base.ExecActionOnStart(_skill, _source, _sourceActionController);

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
      this.AppendIsAlreadyExeced(_target.Owner, _num);
      _target.Owner.SetAbnormalState(_source, DamageCutAction.abnormalStateDic[(DamageCutAction.eDamageCutType) this.ActionDetail1], _valueDictionary[eValueNumber.VALUE_3], (ActionParameter) this, _skill, (float) (int) _valueDictionary[eValueNumber.VALUE_1]);
    }

    public override void SetLevel(float _level)
    {
      base.SetLevel(_level);
      this.Value[eValueNumber.VALUE_1] = (float) ((double) this.MasterData.action_value_1 + (double) this.MasterData.action_value_2 * (double) _level);
      this.Value[eValueNumber.VALUE_3] = (float) ((double) this.MasterData.action_value_3 + (double) this.MasterData.action_value_4 * (double) _level);
    }

    private enum eDamageCutType
    {
      ATK = 1,
      MGC = 2,
      ALL = 3,
    }
  }
}
