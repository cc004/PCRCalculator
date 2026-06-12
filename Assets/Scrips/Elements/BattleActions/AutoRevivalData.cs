using System;
using System.Collections;
using Elements.Battle;
using UnityEngine;

namespace Elements
{
    public class AutoRevivalData
    {
        public enum eValueElementType // TypeDefIndex: 20977
        {
            PHYSICS = 1,
            MAGIC = 2,
        }

        // Namespace: 
        public enum eValueType // TypeDefIndex: 20978
        {
            NORMAL = 1,
            PERCENT = 2,
        }
        // Fields
        private const int ENCHANT_UNBEATABLE_VALUE = 99;
        private const int NO_DISP_NUM = 1;
        private BasePartsData partsData; // 0x10
        private AutoRevivalData.eValueElementType valueElementType; // 0x18
        private double value; // 0x20
        private double parameterRate; // 0x28
        private UnitCtrl owner; // 0x30
        private int counter; // 0x38
        private double timer; // 0x40
        private bool isEnchantUnbeatable; // 0x48
        private eStateIconType iconType; // 0x4C
        private AutoRevivalData.eValueType valueType; // 0x50
        private eEffectType effectType; // 0x54
        private Skill skill; // 0x58
        public BattleManager battleManager => BattleManager.Instance;

        public AutoRevivalData(BasePartsData _partsData, int _detail1, int _detail3, ValueDictionary _valueDictionary, UnitCtrl _owner, eStateIconType _iconType, eEffectType _efddfectType, Skill _skill)
        {
            partsData = _partsData;
            owner = _owner;
            valueElementType = (eValueElementType)_detail1;
            valueType = (eValueType)(int)_valueDictionary[eValueNumber.VALUE_1];
            value = _valueDictionary[eValueNumber.VALUE_2];
            parameterRate = _valueDictionary[eValueNumber.VALUE_4];
            counter = (int)_valueDictionary[eValueNumber.VALUE_6];
            timer = _valueDictionary[eValueNumber.VALUE_5];
            iconType = _iconType;
            effectType = _efddfectType;
            isEnchantUnbeatable = _detail3 == ENCHANT_UNBEATABLE_VALUE;
            if (!iconType.Equals(eStateIconType.NONE))
            {
                if (counter != NO_DISP_NUM)
                    _owner.OnChangeStateNum?.Invoke(_owner, iconType, counter);
            }
            owner.AppendCoroutine(updateTimer(owner), ePauseType.SYSTEM, owner);
            skill = _skill;
        }

        // RVA: 0x28EF7B0 Offset: 0x28EF7B0 VA: 0x28EF7B0
        private IEnumerator updateTimer(UnitCtrl _target)
        {
            while (timer > 0.0 && counter >= 1)
            {
                if (!_target.IdleOnly && !_target.IsDead)
                {
                    timer -= _target.DeltaTimeForPause;
                    yield return null;
                }
                else
                {
                    break;
                }
            }
            if (counter >= 1)
            {
                _target.OnChangeState?.Invoke(_target, iconType, false);
            }
            timer = 0.0;
            counter = 0;
        }

        // RVA: 0x28EF870 Offset: 0x28EF870 VA: 0x28EF870
        public bool TryExec()
        {
            if (counter == 0 || timer <= 0.0)
                return false;
            counter--;
            if (counter == 1)
            {
                owner.OnChangeState?.Invoke(owner, iconType, false);
            }
            else
            {
                owner.OnChangeStateNum?.Invoke(owner, iconType, counter);
            }
            cureUnableAbnormalState();
            applyHeal();
            if (isEnchantUnbeatable)
            {
                owner.IsUnbeatableForAutoRevival = true;
            }
            execSkill();
            return true;
        }

        // RVA: 0x28EF94C Offset: 0x28EF94C VA: 0x28EF94C
        private void cureUnableAbnormalState()
        {
            foreach (var state in Enum.GetValues(typeof(UnitCtrl.eAbnormalState)))
            {
                UnitCtrl.eAbnormalState abnormalState = (UnitCtrl.eAbnormalState)state;
                if (judgeReleaseTargetState(abnormalState))
                {
                    owner.EnableAbnormalState(abnormalState, false, false, false, true);
                }
            }
            if (owner.ToadDatas.Count > 0)
            {
                owner.ToadDatas[0].Enable = false;
            }
        }

        // RVA: 0x28F0064 Offset: 0x28F0064 VA: 0x28F0064
        private bool judgeReleaseTargetState(UnitCtrl.eAbnormalState _state)
        {
            if (!UnitCtrl.IsUnableAbnormalState(_state))
                return _state == UnitCtrl.eAbnormalState.SILENCE;
            var state = owner.GetCategoryDataSafe(UnitCtrl.GetAbnormalStateCategory(_state));
            if (state != null)
            {
                return state.ActionType != eActionType.FORCE_CHANGE_SPEED;
            }
            // if should be unreachable
            return true;
        }

        // RVA: 0x28EFD54 Offset: 0x28EFD54 VA: 0x28EFD54
        private void applyHeal()
        {
            double healValue = 0;
            if (valueType == eValueType.PERCENT)
            {
                healValue = BattleUtil.DoubleToLong(value * owner.MaxHp / 100.0);
            }
            else if (valueType == eValueType.NORMAL)
            {
                LongWithEx atk = 0;
                if (owner.IsPartsBoss)
                {
                    if (valueElementType == eValueElementType.PHYSICS)
                    {
                        atk = partsData.GetAtkZero();
                    }
                    else
                    {
                        atk = partsData.GetMagicStrZero();
                    }
                }
                else
                {
                    if (valueElementType == eValueElementType.PHYSICS)
                    {
                        atk = owner.AtkZero;
                    }
                    else
                    {
                        atk = owner.MagicStrZero;
                    }
                }
                healValue = BattleUtil.DoubleToLong(value + parameterRate * atk);
            }
            else
            {
                Debug.LogError($"AutoRevivalData.applyHeal invalid valueType:{valueType}");
            }

            owner.SetRecovery(
                (LongWithEx)healValue,
                0,
                valueElementType == eValueElementType.PHYSICS ? UnitCtrl.eInhibitHealType.PHYSICS : UnitCtrl.eInhibitHealType.MAGIC,
                owner,
                // false,
                // null,
                UnitCtrl.GetHealDownValue(owner),
                effectType == eEffectType.NONE,
                true,
                false,
                false,
                true,
                null,
                true
            );
        }

        // RVA: 0x28EFF80 Offset: 0x28EFF80 VA: 0x28EFF80
        private void execSkill()
        {
            if (battleManager.ChargeSkillTurn != eChargeSkillTurn.NONE)
            {
                owner.AppendCoroutine(waitChargeSkillTurnNone(), ePauseType.SYSTEM, owner);
            }
            else
            {
                trySkill();
            }
        }

        // RVA: 0x28F0118 Offset: 0x28F0118 VA: 0x28F0118
        private void trySkill()
        {
            if (owner.KnockBackEnableCount == 0)
            {
                execSkillImpl();
            }
            else
            {
                owner.AppendCoroutine(waitKnockBackEnd(), ePauseType.SYSTEM, owner);
            }
        }

        // RVA: 0x28F0280 Offset: 0x28F0280 VA: 0x28F0280
        private void execSkillImpl()
        {
            if (!owner.IsDead)
            {
                if (!owner.IsUnableActionState())
                {
                    owner.CancelByAwake = true;
                    owner.CurrentTriggerSkillId = skill.SkillId;
                    owner.SetState(UnitCtrl.ActionState.SKILL, 0, skill.SkillId, false);
                    if (isEnchantUnbeatable)
                    {
                        owner.AppendCoroutine(waitSkillEndAndReleaseUnbeatable(), ePauseType.SYSTEM, owner);
                    }
                }
            }
        }

        // RVA: 0x28F0180 Offset: 0x28F0180 VA: 0x28F0180
        private IEnumerator waitChargeSkillTurnNone()
        {
            while (battleManager.ChargeSkillTurn != eChargeSkillTurn.NONE)
            {
                yield return null;
            }
            trySkill();
        }

        // RVA: 0x28F0200 Offset: 0x28F0200 VA: 0x28F0200
        private IEnumerator waitKnockBackEnd()
        {
            while (owner.KnockBackEnableCount > 0)
            {
                yield return null;
            }
            if (!owner.IsDead)
                execSkillImpl();
        }

        // RVA: 0x28F0340 Offset: 0x28F0340 VA: 0x28F0340
        private IEnumerator waitSkillEndAndReleaseUnbeatable()
        {
            while (owner.CurrentState == UnitCtrl.ActionState.SKILL
                && !owner.IdleOnly
                && !owner.IsDead)
            {
                yield return null;
            }
            owner.IsUnbeatableForAutoRevival = false;
            battleManager.UpdateAbnormalStateField();
        }
    }
}