using System;
using System.Collections.Generic;
using Cute;

namespace Elements
{
    public class GutsData
    {
        private readonly Dictionary<int, LongWithEx> healValue = new Dictionary<int, LongWithEx>();
        private readonly NormalSkillEffect execEffectData;
        private readonly Skill skill;
        private readonly UnitCtrl target;
        private readonly UnitCtrl source;
        private LongWithEx healValueForInfinity;
        private SealData sealData;

        public eStateIconType StateIconType { get; private set; }
        public UnitCtrl.eInhibitHealType InhibitHealType { get; private set; } = UnitCtrl.eInhibitHealType.NO_EFFECT;
        public float LifeTime { get; private set; }
        public bool IsInfinityCount { get; private set; }
        public bool IsEnable => sealData.GetCurrentCount() > 0;

        public GutsData(
            eStateIconType stateIconType,
            UnitCtrl.eInhibitHealType inhibitHealType,
            LongWithEx healValue,
            float limitTime,
            int remainCount,
            int remainMaxCount,
            SkillEffectCtrl effect,
            NormalSkillEffect execEffectData,
            Skill skill,
            UnitCtrl target,
            UnitCtrl source)
        {
            StateIconType = stateIconType;
            InhibitHealType = inhibitHealType;
            LifeTime = limitTime;
            IsInfinityCount = remainCount == 0;
            this.execEffectData = execEffectData;
            this.skill = skill;
            this.target = target;
            this.source = source;

            int addCount = remainCount <= 1 ? 1 : remainCount;
            sealData = new SealData
            {
                Effect = effect,
                Max = IsInfinityCount ? 1 : Math.Max(remainMaxCount, 1),
                DisplayCount = !IsInfinityCount,
                OnAddAction = (sealId, zeroToOne) => AddHealData(sealId, healValue)
            };

            target.OnChangeState.Call(target, StateIconType, true);
            target.SealDictionary[StateIconType] = sealData;
            sealData.AddSeal(LifeTime, target, StateIconType, addCount);
            sealData.OnAddAction = null;
            sealData.OnRemoveAction = RemoveHealData;
        }

        public void AddRemainCount(float limitTime, LongWithEx healValue, int count)
        {
            sealData.OnAddAction = (sealId, zeroToOne) => AddHealData(sealId, healValue);
            if (sealData.GetCurrentCount() == 0)
            {
                target.OnChangeState.Call(target, StateIconType, true);
            }

            sealData.AddSeal(limitTime, target, StateIconType, IsInfinityCount ? 1 : count);
            sealData.OnAddAction = null;
        }

        public void Exec()
        {
            PlayExecEffect();
            if (IsInfinityCount)
            {
                target.SetRecovery(healValueForInfinity, 0, InhibitHealType, target, _isRevival: true);
                return;
            }

            sealData.OnRemoveAction = ExecByRemovedSeal;
            sealData.RemoveSeal(1, true);
            sealData.OnRemoveAction = RemoveHealData;
        }

        private void AddHealData(int sealId, LongWithEx value)
        {
            if (IsInfinityCount)
            {
                healValueForInfinity = value;
                return;
            }

            healValue[sealId] = value;
        }

        private void ExecByRemovedSeal(int sealId, bool oneToZero)
        {
            if (healValue.TryGetValue(sealId, out LongWithEx value))
            {
                target.SetRecovery(value, 0, InhibitHealType, target, _isRevival: true);
                healValue.Remove(sealId);
            }
        }

        private void RemoveHealData(int sealId, bool oneToZero)
        {
            healValue.Remove(sealId);
        }

        private void PlayExecEffect()
        {
            // Visual effect creation is omitted in the shared simulator path, matching nearby action implementations.
            _ = execEffectData;
            _ = skill;
            _ = source;
        }
    }
}
