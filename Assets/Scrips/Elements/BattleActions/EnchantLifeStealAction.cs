// Decompiled with JetBrains decompiler
// Type: Elements.EnchantLifeStealAction
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using Cute;
using System.Collections.Generic;

namespace Elements
{
    public class EnchantLifeStealAction : ActionParameter
    {
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
          Dictionary<eValueNumber, FloatWithEx> _valueDictionary)
        {
            base.ExecAction(_source, _target, _num, _sourceActionController, _skill, _starttime, _enabledChildAction, _valueDictionary);
            this.AppendIsAlreadyExeced(_target.Owner, _num);
            Queue<int> intQueue = new Queue<int>();
            int num = 0;
            for (int index = (int)_valueDictionary[eValueNumber.VALUE_3]; num < index; ++num)
                intQueue.Enqueue((int)_valueDictionary[eValueNumber.VALUE_1]);
            _target.Owner.LifeStealQueueList.Add(intQueue);
            _target.Owner.OnChangeState.Call<UnitCtrl, eStateIconType, bool>(_target.Owner, eStateIconType.BUFF_ADD_LIFE_STEAL, true);
            _target.Owner.MyOnChangeAbnormalState?.Invoke(_target.Owner, eStateIconType.BUFF_ADD_LIFE_STEAL, true, 90, "1次");
        }

        public override void SetLevel(float _level)
        {
            base.SetLevel(_level);
            this.Value[eValueNumber.VALUE_1] = (float)((double)this.MasterData.action_value_1 + (double)this.MasterData.action_value_2 * (double)_level);
            this.Value[eValueNumber.VALUE_3] = (float)((double)this.MasterData.action_value_3 + (double)this.MasterData.action_value_4 * (double)_level);
        }
    }
}
