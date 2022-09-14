using System.Collections;
using System.Collections.Generic;

namespace Elements
{
	public class SpyAction : ActionParameter
	{
		public enum eSpyType
		{
			SPY = 1
		}

		public enum eCancelTriggerType
		{
			NONE,
			DAMAGED
		}

		public class eSpyType_DictComparer : IEqualityComparer<eSpyType>
		{
			public bool Equals(eSpyType _x, eSpyType _y)
			{
				return _x == _y;
			}

			public int GetHashCode(eSpyType _obj)
			{
				return (int)_obj;
			}
		}

		public static readonly Dictionary<eSpyType, UnitCtrl.eAbnormalState> AbnormalStateDic = new Dictionary<eSpyType, UnitCtrl.eAbnormalState>(new eSpyType_DictComparer())
		{
			{
				eSpyType.SPY,
				UnitCtrl.eAbnormalState.SPY
			}
		};

		public override void ExecActionOnStart(Skill _skill, UnitCtrl _source, UnitActionController _sourceActionController)
		{
			base.ExecActionOnStart(_skill, _source, _sourceActionController);
		}

		public override void ExecAction(UnitCtrl _source, BasePartsData _target, int _num, UnitActionController _sourceActionController, Skill _skill, float _starttime, Dictionary<int, bool> _enabledChildAction, Dictionary<eValueNumber, FloatWithEx> _valueDictionary)
		{
			base.ExecAction(_source, _target, _num, _sourceActionController, _skill, _starttime, _enabledChildAction, _valueDictionary);
			AppendIsAlreadyExeced(_target.Owner, _num);
			UnitCtrl.eAbnormalState abnormalState = AbnormalStateDic[(eSpyType)base.ActionDetail1];
			List<UnitCtrl> targetList = (_target.Owner.IsOther ? base.battleManager.EnemyList : base.battleManager.UnitList);
			_target.Owner.SetAbnormalState(_source, abnormalState, (base.AbnormalStateFieldAction == null) ? (float)_valueDictionary[eValueNumber.VALUE_1] : 90f, this, _skill);
			_target.Owner.AppendCoroutine(updateSpy(_target.Owner, targetList), ePauseType.SYSTEM, _target.Owner);
			eCancelTriggerType actionDetail = (eCancelTriggerType)base.ActionDetail2;
			if (actionDetail != eCancelTriggerType.DAMAGED || _target.Owner.OnDamageListForSpyDisableByAttack.ContainsKey(base.ActionId))
			{
				return;
			}
			_target.Owner.OnDamageListForSpyDisableByAttack.Add(base.ActionId, delegate
			{
				if (_target.Owner.IsAbnormalState(abnormalState))
				{
					_target.Owner.DisableAbnormalStateById(abnormalState, base.ActionId, _isReleasedByDamage: true);
				}
			});
		}

		public override void SetLevel(float _level)
		{
			base.SetLevel(_level);
			base.Value[eValueNumber.VALUE_1] = (float)((double)base.MasterData.action_value_1 + (double)base.MasterData.action_value_2 * (double)_level);
		}

		private IEnumerator updateSpy(UnitCtrl _target, List<UnitCtrl> _targetList)
		{
			yield return null;
			while (true)
			{
				if (!_target.IsAbnormalState(UnitCtrl.eAbnormalState.SPY) || _target.IsDead)
				{
					yield break;
				}
				if (!_targetList.Exists((UnitCtrl e) => ((!e.IsDead && (long)e.Hp > 0) || e.HasUnDeadTime) && !e.IsStealth && !e.IsAbnormalState(UnitCtrl.eAbnormalState.SPY)))
				{
					break;
				}
				yield return null;
			}
			foreach (UnitCtrl _target2 in _targetList)
			{
				_target2.DispelAbnormalState(UnitCtrl.eAbnormalState.SPY);
			}
		}
	}
}
