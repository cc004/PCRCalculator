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
    public class ProtectionData 
    {
        private readonly List<eActionType> NoneEffectActionList; 
        public float Timer; 

        public eStateIconType IconType { get; set; }
        public bool Enable { get; set; }
        public int Counter { get; set; }
        public BasePartsData ProtectionParts { get; set; }
        public UnitCtrl Target { get; set; }
        public GameObject EffectPrefab { get; set; }
        public GameObject EffectPrefabLeft { get; set; }
        public int EffectMaxCount { get; set; }
        public float EffectDuration { get; set; }
        private int effectCounter { get; set; }
        public bool HasCountLimit { get => Counter != -1; }

        public void StartTimer() 
        { 
            var enumerator = updateTimer();
            if (Target != null && enumerator != null)
            {
                Target.AppendCoroutine(enumerator, ePauseType.SYSTEM);
            }
        }

        public void DisplayIcon() 
        { 
            if (Target == null)
                return;

            Target.OnChangeState?.Invoke(Target, IconType, true);

            if (HasCountLimit)
            {
                Target.OnChangeStateNum?.Invoke(Target, IconType, Counter);
            }
        }

        private IEnumerator updateTimer() 
        { 
            Enable = true;
            while (Enable && Timer > 0f)
            {
                Timer -= BattleManager.Instance.DeltaTime_60fps;
                yield return null;
            }
            Enable = false;
            Disable();
        }

        public void Disable() 
        { 
            Timer = 0f;
            Enable = false;
            if (Target != null)
            {
                Target.OnChangeState?.Invoke(Target, IconType, false);
            }
        }

        public void DisableIfOwnerDead() 
        { 
            if (!Enable)
                return;

            if (ProtectionParts == null)
                return;

            var owner = ProtectionParts.Owner;
            if (owner == null)
                return;

            if (owner.IdleOnly || owner.Hp <= 0)
            {
                Disable();
            }
        }

        public bool TryExec() 
        { 
            if (!Enable)
                return false;

            if (ProtectionParts == null)
                return false;

            var owner = ProtectionParts.Owner;
            if (owner == null)
                return false;

            if (owner.IsUnableActionState())
                return false;

            if (owner.IsConfusionOrConvert())
                return false;

            if (owner.ToadDatas != null && owner.ToadDatas.Count > 0)
                return false;

            if (HasCountLimit)
            {
                Counter--;
                if (Counter == 0)
                {
                    Disable();
                }
                else
                {
                    if (Target != null)
                    {
                        Target.OnChangeStateNum?.Invoke(Target, IconType, Counter);
                    }
                }
            }
            return true;

        }

        // public void StartEffect(UnitCtrl _target, IBattleEffectPool _pool, IBattleManagerForUnitActionController _battleManager, eActionType _actionType) 
        // { 
            // ignore efect
        // }

        // public IEnumerator UpdateEffect(float _deltaTime) 
        // { 
            // ignore efect
        // }

    }

}