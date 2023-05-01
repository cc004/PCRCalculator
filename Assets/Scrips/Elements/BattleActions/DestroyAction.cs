using Elements.Battle;
using System;
using System.Collections.Generic;

namespace Elements
{
    public class DestroyAction : ActionParameter
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
          Dictionary<eValueNumber, FloatWithEx> _valueDictinary,
          System.Action<string> action)
        {
            base.ExecAction(_source, _target, _num, _sourceActionController, _skill, _starttime, _enabledChildAction, _valueDictinary);
            AppendIsAlreadyExeced(_target.Owner, _num);
            switch ((eDestroyType)ActionDetail1)
            {
                case eDestroyType.NORMAL:
                    _target.Owner.SetDamage(new DamageData
                    {
                        Damage = _target.Owner.Hp,
                        DamageType = DamageData.eDamageType.NONE,
                        Source = _source,
                        ActionType = eActionType.DESTROY,
                        IsAlwaysChargeEnegry = IsAlwaysChargeEnegry
                    }, _byAttack: false, base.ActionId, null, _hasEffect: false, null, _energyAdd: true, base.OnDefeatEnemy, _noMotion: false, 1f, 1f, null, EnergyChargeMultiple, base.UsedChargeEnergyByReceiveDamage);
                    break;
                case eDestroyType.DELETE:
                    _target.Owner.IdleOnly = true;
                    _target.Owner.CurrentState = UnitCtrl.ActionState.IDLE;
                    battleManager.CallbackIdleOnlyDone(_target.Owner);
                    List<UnitCtrl> unitCtrlList = _target.Owner.IsOther ? battleManager.EnemyList : battleManager.UnitList;
                    if (unitCtrlList.Contains(_target.Owner))
                        unitCtrlList.Remove(_target.Owner);
                    BattleManager.Instance.shouldUpdateSkillTarget = true;
                    _target.Owner.IsDead = true;
                    _target.Owner.CureAllAbnormalState();
                    battleManager.CallbackFadeOutDone(_target.Owner);
                    battleManager.CallbackDead(_target.Owner);
                    _target.Owner.gameObject.SetActive(false);
                    _target.Owner.SetCurrentHpZero();
                    /*using (List<SkillEffectCtrl>.Enumerator enumerator = _target.Owner.RepeatEffectList.GetEnumerator())
                    {
                      while (enumerator.MoveNext())
                        enumerator.Current.SetTimeToDie(true);
                      break;
                    }*/
                    break;
            }
            action($"即死{_target.Owner.UnitNameEx}");
        }

        private enum eDestroyType
        {
            NORMAL,
            DELETE,
        }
    }
}
