// Decompiled with JetBrains decompiler
// Type: Elements.ChangeBodyWidthAction
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System.Collections.Generic;

namespace Elements
{
  public class ChangeBodyWidthAction : ActionParameter
  {
		private enum eTargetType
		{
			SINGLE,
			PARTS
		}

		public override void ExecAction(UnitCtrl _source, BasePartsData _target, int _num, UnitActionController _sourceActionController, Skill _skill, float _starttime, Dictionary<int, bool> _enabledChildAction, Dictionary<eValueNumber, FloatWithEx> _valueDictionary)
		{
			switch (base.ActionDetail1)
			{
				case 1:
					_target.Owner.BossPartsListForBattle.Find((PartsData e) => e.Index == base.ActionDetail2).BodyWidthValue = _valueDictionary[eValueNumber.VALUE_1];
					break;
				case 0:
					_target.Owner.BodyWidth = _valueDictionary[eValueNumber.VALUE_1];
					break;
            }
            battleManager.QueueUpdateSkillTarget();
        }
		public override void SetLevel(float _level)
		{
		}
	}
}
