using System.Collections.Generic;
using UnityEngine;

namespace Elements
{
	public class EffectAction : ActionParameter
	{
		public override void ExecActionOnStart(Skill _skill, UnitCtrl _source, UnitActionController _sourceActionController)
		{
			base.ExecActionOnStart(_skill, _source, _sourceActionController);
		}

		public override void ExecAction(UnitCtrl _source, BasePartsData _target, int _num, UnitActionController _sourceActionController, Skill _skill, float _starttime, Dictionary<int, bool> _enabledChildAction, Dictionary<eValueNumber, FloatWithEx> _valueDictionary)
		{
			base.ExecAction(_source, _target, _num, _sourceActionController, _skill, _starttime, _enabledChildAction, _valueDictionary);
			/*foreach (NormalSkillEffect actionEffect in base.ActionEffectList)
			{
				GameObject gameObject = null;
				gameObject = (_target.Owner.IsLeftDir ? actionEffect.PrefabLeft : actionEffect.Prefab);
				SkillEffectCtrl effect = base.battleEffectPool.GetEffect(gameObject);
				effect.transform.parent = ExceptNGUIRoot.Transform;
				effect.SortTarget = _target.Owner;
				effect.InitializeSort();
				effect.SetPossitionAppearanceType(actionEffect, _target, _target.Owner, _skill);
				effect.ExecAppendCoroutine();
				effect.PlaySe(_source.SoundUnitId, _source.IsLeftDir);
			}*/
		}

		public override void SetLevel(float _level)
		{
			base.SetLevel(_level);
		}
	}
}
