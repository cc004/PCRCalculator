// Decompiled with JetBrains decompiler
// Type: Elements.ChangeEnergyRecoveryRatioByDamage
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System.Collections.Generic;

namespace Elements
{
  public class ChangeEnergyRecoveryRatioByDamage : ActionParameter
  {
    public const float ENERGY_CHARGE_BASE_MULTIPLE = 1f;

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
      Queue<List<int>> intListQueue = new Queue<List<int>>();
      intListQueue.Enqueue(ActionChildrenIndexes);
      while (intListQueue.Count > 0)
      {
        List<int> intList = intListQueue.Dequeue();
        for (int index = 0; index < intList.Count; ++index)
        {
          _skill.ActionParameters[intList[index]].EnergyChargeMultiple = _valueDictionary[eValueNumber.VALUE_1];
          intListQueue.Enqueue(_skill.ActionParameters[intList[index]].ActionChildrenIndexes);
        }
      }
    }
  }
}
