using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Elements.Battle;
using PCRCaculator.Battle;
namespace Elements
{
    public class EnvironmentData : ISingletonField
    {
        public Skill Skill { get; set; }
        public UnitCtrl SourceUnit { get; set; }
        public int EnvironmentType { get; set; }
        public List<ActionParameter> TargetActions { get; set; }
        public bool stopCalled { get; set; }
        public SkillEffectCtrl skillEffect { get; set; }
        public bool Enable { get; set; }
        public float LimitTime { get; set; }
        public UnitActionController SourceActionController { get; set; }
        public List<UnitCtrl> EnemyUnitList { get; set; }
        public List<UnitCtrl> UnitList { get; set; }
        public GameObject UniqueEffectPrefab { get; set; }
        public bool WaitingAnotherEnvironmentEnd { get; set; }
        public List<BasePartsData> TargetList { get; set; }
        public float timer { get; set; }
        public int environmentId; // 0x6C

        private Action<BasePartsData> _timerCallback;

        // public Battle.IBattleEffectPool battleEffectPool => staticBattleEffectPool;
        public BattleManager battleManager => BattleManager.Instance;

        public void SetTimerCallback(Action<BasePartsData> callback)
        {
            _timerCallback = callback;
        }

        public void ResetTimer()
        {
            timer = 0.0f;
        }

        public EnvironmentData()
        {
            TargetActions = new List<ActionParameter>();
        }

        public void StartEnvironment()
        {
            int envType = this.EnvironmentType;

            var manager = this.battleManager;

            var dict = manager.EnvironmentCounterDic;
            if (!dict.ContainsKey(envType))
            {
                dict[envType] = 0;
            }

            if (dict.TryGetValue(envType, out int count) && count > 0)
            {
                this.WaitingAnotherEnvironmentEnd = true;
            }

            dict[envType] = dict[envType] + 1;

            if (!this.WaitingAnotherEnvironmentEnd)
            {
                InitializeSkillEffect();
            }

            Enable = true;

            environmentId = manager.CurrentEnvironmentId;
            manager.CurrentEnvironmentId++;

            manager.AppendCoroutine(this.UpdateEnvironment(), ePauseType.SYSTEM, SourceUnit);
        }

        public IEnumerator UpdateEnvironment()
        {
            while (true)
            {
                if (stopCalled)
                    yield break;

                if (!WaitingAnotherEnvironmentEnd)
                {
                    UpdateEnvironmentUnits(EnemyUnitList);
                    UpdateEnvironmentUnits(UnitList);
                }

                if (SourceUnit == null
                    || SourceUnit.IsDead
                    || SourceUnit.Hp < 1
                    || timer > LimitTime)
                {
                    stop();
                    // if (SourceUnit != null && SourceUnit.IsPlayerUnit)
                    //     BattleHeaderController.Instance.DisappearEnvironmentIcon(true);
                    yield break;
                }

                yield return null;

                timer += BattleManager.DeltaTime_60fps;
            }
        }

        private void UpdateEnvironmentUnits(List<UnitCtrl> units)
        {
            if (units == null)
                return;

            foreach (var ctrl in units)
            {
                if (ctrl == null)
                    continue;

                if (ctrl.IsPartsBoss)
                {
                    foreach (var part in ctrl.BossPartsListForBattle)
                        UpdateEnvironmentCallback(part);
                }
                else
                {
                    UpdateEnvironmentCallback(ctrl.GetFirstParts());
                }
            }
        }

        public void StopEnvironmentImmediately()
        {
            this.stopCalled = true;
            stop();
        }

        void stop()
        {
            if (battleManager != null)
            {
                var dict = battleManager.EnvironmentCounterDic;
                var environmentCount = dict[EnvironmentType];
                dict[EnvironmentType] = environmentCount - 1;

                if (environmentCount == 1)
                {
                    if (SourceUnit != null)
                    {
                        bool isPlayerUnit = SourceUnit.IsPlayerUnit;
                        battleManager.SetEnvironmentResume(!isPlayerUnit);
                    }
                }
            }

            // if (Skill != null && Skill.BonusId != 0)
            // {
            //     if (battleManager != null)
            //     {
            //         battleManager.DeleteBonusIcon(Skill.BonusId);
            //     }
            // }

            if (TargetList != null)
            {
                for (int i = TargetList.Count - 1; i >= 0; i--)
                {
                    var target = TargetList[i];
                    OnExit(target);
                }
            }

            Enable = false;

            // if (skillEffect != null)
            // {
            //     skillEffect.SetTimeToDie(true);
            // }
            // SetEnvironmentUI();
#if UNITY_5_3_OR_NEWER
            BattleUIManager.Instance.SetEnvironmentUI(this.EnvironmentType, true, SourceUnit.IsPlayerUnit);
#endif
        }

        public void OnEnter(BasePartsData parts)
        {
            if (parts == null || TargetActions == null)
                return;

            bool hasAddedToTargetList = false;

            foreach (var current in TargetActions)
            {
                if (current == null)
                    continue;

                var owner = parts.Owner;
                if (owner == null)
                    continue;

                // 原 HIDWORD(current[1].klass)
                int sortCode = (int)current.TargetSort;
                if ((sortCode == 20 && owner.AtkType != 1)
                || (sortCode == 21 && owner.AtkType != 2))
                    continue;

                if (current.IsChangeSpeedAction() && owner.OverwriteSpeedDataCount > 0)
                    continue;

                if (!hasAddedToTargetList)
                {
                    TargetList.Add(parts);
                }

                // 召唤/幻影时先尝试附加目标编号
                if (owner.IsSummonOrPhantom)
                    current.AppendTargetNum(owner, 0);

                // 确保至少调用一次 AppendTargetNum
                if (!current.IsAppendTargetNum(owner))
                    current.AppendTargetNum(owner, 0);

                if (current is BuffDebuffAction buffDebuffAction)
                    buffDebuffAction.EnvironmentId = environmentId;
                else if (current is ExtendSlipDamageAction extendSlipDamageAction)
                    extendSlipDamageAction.EnvironmentId = environmentId;

                // 执行动作
                if (SourceActionController != null)
                {
                    hasAddedToTargetList = true;
                    SourceActionController.ExecAction(
                        current,
                        Skill,
                        parts,
                        0,
                        0f
                    );
                }
            }
        }

        public void OnExit(BasePartsData parts)
        {
            if (parts == null)
                return;

            TargetList?.Remove(parts);

            if (TargetActions != null)
            {
                var owner = parts.Owner;
                if (owner != null)
                {
                    foreach (var current in TargetActions)
                    {
                        if (current == null)
                            continue;

                        // 原 HIDWORD(current[1].klass)
                        int sortCode = (int)current.TargetSort;
                        if ((sortCode == 20 && owner.AtkType != 1)
                            || (sortCode == 21 && owner.AtkType != 2))
                            continue;

                        // 原 LODWORD(current[10].monitor)
                        var envState = current.EnvironmentTargetAbnormalState;
                        // 原 LODWORD(current[3].monitor)
                        int actionId = current.ActionId;

                        // 调用禁用方法：Elements_UnitCtrl__DisableAbnormalStateById
                        owner.DisableAbnormalStateById(
                            envState,
                            actionId,
                            false
                        );
                    }
                }
            }

            parts.Owner?.DisableEnvironmentCoroutine(environmentId);
        }

        public void InitializeSkillEffect()
        {
            // var effectPool = this.battleEffectPool;
            // if (effectPool == null) return;

            // var prefab = this.UniqueEffectPrefab;

            // var skillEffect = effectPool.Instantiate(prefab, false) as Elements_SkillEffectCtrl;
            // this.skillEffect = skillEffect;

            // if (skillEffect == null) return;

            // var transform = skillEffect.transform;
            // if (transform == null) return;


            // var battleManager = this.battleManager;
            // if (battleManager == null) return;

            // float heightOffset = battleManager.GetFieldFloat(4);  

            // Vector3 pos = new Vector3(0f, (heightOffset + 5000f) / 540f, 0f);
            // transform.localPosition = pos;

            // skillEffect.InvokeInternalNameCall();
            // skillEffect.InvokeInternalInterfaceCall();

#if UNITY_5_3_OR_NEWER
                BattleUIManager.Instance.SetEnvironmentUI(this.EnvironmentType, true, SourceUnit.IsPlayerUnit);
#endif
        }

        public void UpdateEnvironmentCallback(BasePartsData unit)
        {
            if (unit == null)
                return;

            var owner = unit.Owner;
            if (owner == null)
                return;

            bool contains = TargetList != null && TargetList.Contains(unit);

            if (owner.IsStealth || owner.IsDead)
            {
                if (contains)
                    OnExit(unit);
                return;
            }

            bool canEnter = !owner.IsSummonOrPhantom || !owner.IdleOnly;

            if (owner.RevivalFlagForEnvironment)
            {
                owner.RevivalFlagForEnvironment = false;
                ResetHitDataForAllActions();
            }
            else if (canEnter)
            {
                if (!contains)
                    OnEnter(unit);
                return;
            }

            if (contains)
                OnExit(unit);
        }

        private void ResetHitDataForAllActions()
        {
            if (TargetActions == null) return;
            foreach (var action in TargetActions)
                action.ResetHitData();
        }

    }

}
