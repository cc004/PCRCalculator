// Decompiled with JetBrains decompiler
// Type: Elements.LogBarrierAction
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System.Collections.Generic;
using PCRCaculator.Guild;

namespace Elements
{
  public class LogBarrierAction : ActionParameter
  {
    private static readonly Dictionary<eDamageCutType, UnitCtrl.eAbnormalState> abnormalStateDic = new Dictionary<eDamageCutType, UnitCtrl.eAbnormalState>
    {
      {
        eDamageCutType.ATK,
        UnitCtrl.eAbnormalState.LOG_ATK_BARRIR
      },
      {
        eDamageCutType.MGC,
        UnitCtrl.eAbnormalState.LOG_MGC_BARRIR
      },
      {
        eDamageCutType.ALL,
        UnitCtrl.eAbnormalState.LOG_ALL_BARRIR
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
      Dictionary<eValueNumber, FloatWithEx> _valueDictionary, System.Action<string> action)
    {
      base.ExecAction(_source, _target, _num, _sourceActionController, _skill, _starttime, _enabledChildAction, _valueDictionary);
      AppendIsAlreadyExeced(_target.Owner, _num);
      if (GuildManager.StaticsettingData.GetCurrentPlayerGroup().useLogBarrierNew != GuildPlayerGroupData.LogBarrierType.FullBarrier)
            {
                return;
            }
      _target.Owner.SetAbnormalState(_source, abnormalStateDic[(eDamageCutType) ActionDetail1], _valueDictionary[eValueNumber.VALUE_3], this, _skill, _valueDictionary[eValueNumber.VALUE_1], _valueDictionary[eValueNumber.VALUE_5]);
            action($"对数盾");
        }

    public override void SetLevel(float _level)
    {
      base.SetLevel(_level);
      Value[eValueNumber.VALUE_1] = (float) (MasterData.action_value_1 + MasterData.action_value_2 * _level);
      Value[eValueNumber.VALUE_3] = (float) (MasterData.action_value_3 + MasterData.action_value_4 * _level);
    }

    private enum eDamageCutType
    {
      ATK = 1,
      MGC = 2,
      ALL = 3,
    }
  }
}
