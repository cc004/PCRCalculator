using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEngine;
using Elements.Battle;

namespace Elements
{
    public class DamageCutForSpecialNullificationData
    {

        public enum eDamageCutType // TypeDefIndex: 21072
        {
            // Fields
            ATK = 1,
            MGC = 2,
            ALL = 3,
        }

        // Namespace: 
        public enum eReleaseType // TypeDefIndex: 21073
        {
            TIMER = 0,
            NO_FRONT = 1,
        }


        private DamageCutForSpecialNullificationData.eDamageCutType damageCutType; // 0x10
        private double damageCutValue; // 0x18
        private UnitCtrl owner; // 0x20
        private eStateIconType iconType; // 0x28
        private bool enable; // 0x2C
        private double timer; // 0x30
        private BattleManager battleManager => BattleManager.Instance;
        // private IBattleEffectPool battleEffectPool; // 0x40
        private List<SkillEffectCtrl> skillEffectList; // 0x48
        private Skill skill; // 0x50

        // Methods

        // RVA: 0x2919F84 Offset: 0x2919F84 VA: 0x2919F84
        public DamageCutForSpecialNullificationData(DamageCutForSpecialNullificationData.eDamageCutType _damageCutType, double _value, double _limitTime, UnitCtrl _owner, eStateIconType _iconType, DamageCutForSpecialNullificationData.eReleaseType _releaseType, 
        // IBattleManagerForActionParameter _battleManager, IBattleEffectPool _battleEffectPool,
        List<NormalSkillEffect> _actionEffectList, Skill _skill)
        {
            enable = true;
            damageCutType = _damageCutType;
            damageCutValue = _value;
            owner = _owner;
            iconType = _iconType;
            timer = _limitTime;
            skill = _skill;
            // battleManager = _battleManager;
            // battleEffectPool = _battleEffectPool;
            // skillEffectList = new List<SkillEffectCtrl>();
            if (iconType != eStateIconType.NONE)
            {
                owner.OnChangeState?.Invoke(owner, iconType, true);
            }
            if (_releaseType == eReleaseType.NO_FRONT)
            {
                if (judgeNoFront(owner))
                {
                    owner.SetMissAtk(owner, eMissLogType.MISS_DAMAGE_CUT_NO_FRONT);
                    Disable();
                }
                else
                {
                    startEffect(_actionEffectList);
                    owner.AppendCoroutine(updateFrontNum(owner), ePauseType.SYSTEM, null);
                }
            }
            else if (_releaseType == eReleaseType.TIMER)
            {
                owner.AppendCoroutine(updateTimer(owner), ePauseType.SYSTEM, null);
                startEffect(_actionEffectList);
            }
            else
            {
                UnityEngine.Debug.LogError("未知的解除类型");
            }
        }

        // RVA: 0x291A41C Offset: 0x291A41C VA: 0x291A41C
        private void startEffect(List<NormalSkillEffect> _normalSkillEffects)
        {
            // ignore effect
        }

        // RVA: 0x291A388 Offset: 0x291A388 VA: 0x291A388
        private IEnumerator updateTimer(UnitCtrl _target)
        {
            while(enable && timer > 0)
            {
                if (_target.IdleOnly || _target.IsDead)
                {
                    break;
                }
                timer -= _target.DeltaTimeForPause;
                yield return null;
            }
            Disable();
            timer = 0.0;
        }

        // RVA: 0x291A8C8 Offset: 0x291A8C8 VA: 0x291A8C8
        private IEnumerator updateFrontNum(UnitCtrl _target)
        {
            while(enable)
            {
                if (judgeNoFront(_target))
                {
                    break;
                }
                if (_target.IdleOnly || _target.IsDead)
                {
                    break;
                }
                yield return null;
            }
            Disable();
            timer = 0.0;
        }

        // RVA: 0x291A560 Offset: 0x291A560 VA: 0x291A560
        private bool judgeNoFront(UnitCtrl _target)
        {
            var units = _target.IsOther ? battleManager.EnemyList : battleManager.UnitList;
            foreach (var unit in units)
            {
                if (UnityEngine.Object.Equals(unit, _target))
                    continue;
                if (!judgeIsCountTarget(unit))
                    continue;
                float targetX = _target.transform.position.x;
                float unitX = unit.transform.position.x;
                if (_target.IsLeftDir)
                    if (targetX > unitX)
                        return false;
                else
                    if (targetX < unitX)
                        return false;
            }
            return true;
        }

        // RVA: 0x291ABDC Offset: 0x291ABDC VA: 0x291ABDC
        private bool judgeIsCountTarget(UnitCtrl _unit)
        {
            return _unit.Hp > 0 && !_unit.IsStealth;
        }

        // RVA: 0x291AC8C Offset: 0x291AC8C VA: 0x291AC8C
        public double GetDamageCutValue(DamageData.eDamageType _damageType)
        {
            var result = 0.0;
            if (enable)
            {
                if (_damageType == DamageData.eDamageType.ATK && (damageCutType == eDamageCutType.ATK || damageCutType == eDamageCutType.ALL))
                {
                    result = damageCutValue;
                }
                else if (_damageType == DamageData.eDamageType.MGC && (damageCutType == eDamageCutType.MGC || damageCutType == eDamageCutType.ALL))
                {
                    result = damageCutValue;
                }
            }
            return result;
        }

        // RVA: 0x2919DA4 Offset: 0x2919DA4 VA: 0x2919DA4
        public void Disable()
        {
            enable = false;
            if (iconType != eStateIconType.NONE)
            {
                owner.OnChangeState?.Invoke(owner, iconType, false);
            }
            // ignore effect below
        }

        // RVA: 0x291A95C Offset: 0x291A95C VA: 0x291A95C
        private void playActionEffect(UnitCtrl _source, Skill _skill, NormalSkillEffect _actionEffect)
        {
            // ignore effect
        }

    }

}