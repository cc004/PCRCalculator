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
        // private static SingletonTree<EnvironmentData> staticSingletonTree;
        private static BattleManager staticBattleManager;
        // private static Battle.IBattleEffectPool staticBattleEffectPool;

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
        public BattleManager battleManager => staticBattleManager;

        public void SetTimerCallback(Action<BasePartsData> callback)
        {
            _timerCallback = callback;
        }

        public void ResetTimer()
        {
            timer = 0.0f;
        }

        public static void StaticRelease()
        {
            // staticSingletonTree = null;
            staticBattleManager = null;
            // staticBattleEffectPool = null;
        }

        public EnvironmentData()
        {
            TargetActions = new List<ActionParameter>();
            staticBattleManager = BattleManager.Instance;
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

            manager.AppendCoroutine(this.UpdateEnvironment(), ePauseType.SYSTEM, SourceUnit);
        }

        public IEnumerator UpdateEnvironment()
        {
            // —— 第一步：销毁残留特效（如果有）并等待一帧 —— 
            var bm = battleManager;
            // if (bm != null && !bm.IsPaused && SkillEffect != null)
            //     UnityEngine.Object.Destroy(SkillEffect);
            yield return null;

            // —— 第二步：第一次恢复后，遍历 EnemyUnitList 和 UnitList —— 
            if (!WaitingAnotherEnvironmentEnd)
            {
                foreach (var ctrl in EnemyUnitList)
                {
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
            foreach (var ctrl in UnitList)
            {
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

            // —— 第三步：进入循环，每帧累积时间并检查结束条件 —— 
            while (true)
            {
                // 终止条件
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

                // 等待一帧
                yield return null;

                timer += battleManager.DeltaTime_60fps;
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
            BattleUIManager.Instance.SetEnvironmentUI(this.EnvironmentType, false);
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

                // 原 LODWORD(current[5].klass)
                int actionTypeValue = (int)current.ActionType;
                // 原 HIDWORD(current[2].monitor)
                int detail1 = current.ActionDetail1;

                // 跳过加入 TargetList 的条件
                bool skipAdd = actionTypeValue == 8
                            && (detail1 - 1) <= 1
                            && owner.OverwriteSpeedDataCount > 0;

                if (!hasAddedToTargetList && !skipAdd)
                {
                    TargetList.Add(parts);
                }

                // 召唤/幻影时先尝试附加目标编号
                if (owner.IsSummonOrPhantom)
                    current.AppendTargetNum(owner, 0);

                // 确保至少调用一次 AppendTargetNum
                if (!current.IsAppendTargetNum(owner))
                    current.AppendTargetNum(owner, 0);

                // Buff/Debuff 动作分支 (原 actionTypeValue == 10)
                if (actionTypeValue == 10) // TODO fix
                {
                    // int depth = current.GetTypeHierarchyDepth();
                    // var lastType = current.GetTypeAtDepth(depth - 1);
                    // if (depth >= BuffDebuffActionTypeInfo.Depth
                    //     && lastType == BuffDebuffActionTypeInfo)
                    // {
                        // LODWORD(current[26].klass) 已经超了定义的最后一个属性了
                        // current.SetTargetEnvironmentId(this.EnvironmentId);
                    // }
                }

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
                        bool typeMatch = (sortCode == 20 && owner.AtkType == 1)
                                    || (sortCode == 21 && owner.AtkType == 2);
                        if (!typeMatch)
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

            var coroutineList = parts.Owner?.buffDebuffCoroutineList;
            if (coroutineList != null)
            {
                foreach (var data in coroutineList)
                {
                    // 原 HIDWORD(current[1].monitor)
                    if (data.EnvironmentId == this.environmentId)
                    {
                        // 原 LOBYTE(current[1].monitor) = 0
                        data.Enable = false;
                    }
                }
            }
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
            BattleUIManager.Instance.SetEnvironmentUI(this.EnvironmentType, true);
        }

        public void UpdateEnvironmentCallback(BasePartsData unit)
        {
            if (unit == null)
                return;

            var owner = unit.Owner;
            if (owner == null)
                return;

            if (owner.RevivalFlagForEnvironment || EnvironmentType != 0)
            {
                if (TargetList != null && TargetList.Contains(unit)){
                    SourceActionController.ExecAction(
                        TargetActions.Find(p => p.TargetList.Contains(unit)),
                        Skill,
                        unit,
                        0,
                        0f
                    );
                    return;
                }
            }

            bool isPhantom = owner.IsSummonOrPhantom;

            if (!isPhantom)
            {
                if (owner.RevivalFlagForEnvironment)
                {
                    owner.RevivalFlagForEnvironment = false;
                    ResetHitDataForAllActions();
                    if (TargetList != null && TargetList.Contains(unit))
                        SourceActionController.ExecAction(
                            TargetActions.Find(p => p.TargetList.Contains(unit)),
                            Skill,
                            unit,
                            0,
                            0f
                        );
                }
                else
                {
                    OnEnter(unit);
                    // TargetList.Add(unit); // TODO fix real logic
                }
                return;
            }

            if (owner.RevivalFlagForEnvironment)
            {
                owner.RevivalFlagForEnvironment = false;
                ResetHitDataForAllActions();
                if (TargetList != null && TargetList.Contains(unit))
                    SourceActionController.ExecAction(
                        TargetActions.Find(p => p.TargetList.Contains(unit)),
                        Skill,
                        unit,
                        0,
                        0f
                    );
            }
            else if (owner.IdleOnly)
            {
                if (TargetList != null && TargetList.Contains(unit))
                    SourceActionController.ExecAction(
                        TargetActions.Find(p => p.TargetList.Contains(unit)),
                        Skill,
                        unit,
                        0,
                        0f
                    );
            }
            else
            {
                // TargetList.Add(unit); // TODO fix real logic
                OnEnter(unit);
            }
        }

        private void ResetHitDataForAllActions()
        {
            if (TargetActions == null) return;
            foreach (var action in TargetActions)
                action.ResetHitData();
        }

    }

}