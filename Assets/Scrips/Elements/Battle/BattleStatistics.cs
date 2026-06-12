using System;
using System.Collections.Generic;
using System.Linq;
using ActionParameterSerializer;
using PCRCaculator;
using PCRCaculator.Guild;

namespace Elements.Battle
{
    public enum BattleStatisticsLogLevel
    {
        None = 0,
        Core = 1 << 0,
        UBTime = 1 << 1,
        BuffFlow = 1 << 2,
        BossDef = 1 << 3,
        SkillExecMin = 1 << 4,
        HitTrace = 1 << 5,
        SkillReadyTrace = 1 << 6,
        AutoSemanTeacherTrace = 1 << 7,
        TpChangeTrace = 1 << 8,
        Simple = Core | UBTime,
        Full = Core | UBTime,
    }

    public class UnitStateChangeDataForTolerance
    {
        public int currentFrameCount;
        public int realFrameCount;
        public UnitCtrl.ActionState changStateTo;
    }

    public class BattleStatistics
    {
        private const int MaxTrackedFrames = 5400;

        public Dictionary<int, List<int>> PlayerUbToleranceFrames { get; set; } = new Dictionary<int, List<int>>();
        public Dictionary<int, List<UnitStateChangeDataForTolerance>> AllUnitStateChangeDic { get; set; } = new Dictionary<int, List<UnitStateChangeDataForTolerance>>();
        public List<BuffDebuffEventData> BuffDebuffEvents { get; } = new List<BuffDebuffEventData>();
        public List<BossDefEventData> BossDefEvents { get; } = new List<BossDefEventData>();
        public List<BossDefEventData> BossMagicDefEvents { get; } = new List<BossDefEventData>();
        public List<SkillExecEventData> SkillExecEvents { get; } = new List<SkillExecEventData>();
        public List<HitTraceEventData> HitTraceEvents { get; } = new List<HitTraceEventData>();
        public List<TpChangeEventData> TpChangeEvents { get; } = new List<TpChangeEventData>();
#if !ENABLE_THREADING
        public Dictionary<int, Dictionary<int, bool>> IsSkillReady
        {
            get => BattleManager.Instance.IsSkillReady;
        }
#endif

        private Dictionary<long, int> ActiveBuffDebuffEventIndexByToken { get; } = new Dictionary<long, int>();
        private Dictionary<int, int> ActiveSkillExecEventIndexByUnitId { get; } = new Dictionary<int, int>();

        public BattleStatisticsLogLevel LogLevel { get; set; } = BattleStatisticsLogLevel.Full;

        public static BattleStatisticsLogLevel NormalizeLogLevel(int rawCalcStats)
        {
            if (rawCalcStats <= 0)
            {
                return BattleStatisticsLogLevel.None;
            }

            if (rawCalcStats == 1 || rawCalcStats == 2)
            {
                return BattleStatisticsLogLevel.Full;
            }

            var flags = (BattleStatisticsLogLevel)rawCalcStats;
            if (!flags.HasFlag(BattleStatisticsLogLevel.Core))
            {
                flags |= BattleStatisticsLogLevel.Core;
            }

            return flags;
        }

        public bool NeedsCoreStats() => LogLevel.HasFlag(BattleStatisticsLogLevel.Core);
        public bool NeedsUbTime() => LogLevel.HasFlag(BattleStatisticsLogLevel.UBTime);
        public bool NeedsBuffFlow() => LogLevel.HasFlag(BattleStatisticsLogLevel.BuffFlow);
        public bool NeedsBossDef() => LogLevel.HasFlag(BattleStatisticsLogLevel.BossDef);
        public bool NeedsSkillExecMin() => LogLevel.HasFlag(BattleStatisticsLogLevel.SkillExecMin);
        public bool NeedsHitTrace() => LogLevel.HasFlag(BattleStatisticsLogLevel.HitTrace);
        public bool NeedsSkillReadyTrace() => LogLevel.HasFlag(BattleStatisticsLogLevel.SkillReadyTrace);
        public bool NeedsAutoSemanTeacherTrace() => LogLevel.HasFlag(BattleStatisticsLogLevel.AutoSemanTeacherTrace);
        public bool NeedsTpChangeTrace() => LogLevel.HasFlag(BattleStatisticsLogLevel.TpChangeTrace);

        public long TotalMagicBuff { get; private set; }
        public long TotalPhysicsBuff { get; private set; }
        public long TotalMagicDebuff { get; private set; }
        public long TotalPhysicsDebuff { get; private set; }
        public Dictionary<int, long> PlayerTpGainedDic { get; private set; } = new Dictionary<int, long>();
        public Dictionary<int, long> PlayerDamageDic { get; set; } = new Dictionary<int, long>();
        public Dictionary<int, long> PlayerNonUbDamageDic { get; set; } = new Dictionary<int, long>();
        
        /// 物理伤害期望
        public double ScorePhysicsExpectation { get; private set; }

        /// 魔法伤害期望
        public double ScoreMagicExpectation { get; private set; }

        // 修改参数类型为 double
        public void AccumulateFrameStats(double phyScore, double magScore)
        {
            if (!NeedsCoreStats()) return;
            ScorePhysicsExpectation += phyScore;
            ScoreMagicExpectation += magScore;
        }

        public void AddMagicBuff(long value) { if (NeedsCoreStats()) TotalMagicBuff += value; }
        public void AddPhysicsBuff(long value) { if (NeedsCoreStats()) TotalPhysicsBuff += value; }
        public void AddMagicDebuff(long value) { if (NeedsCoreStats()) TotalMagicDebuff += value; }
        public void AddPhysicsDebuff(long value) { if (NeedsCoreStats()) TotalPhysicsDebuff += value; }

        public void AddPlayerTp(int unitId, long value)
        {
            if (!NeedsCoreStats()) return;

            if (!PlayerTpGainedDic.ContainsKey(unitId))
            {
                PlayerTpGainedDic.Add(unitId, 0);
            }
            PlayerTpGainedDic[unitId] += value;
        }

        public void AddPlayerDamage(int unitId, long damage)
        {
            if (!NeedsCoreStats()) return;

            if (!PlayerDamageDic.ContainsKey(unitId))
            {
                PlayerDamageDic.Add(unitId, 0);
            }
            PlayerDamageDic[unitId] += damage;
        }

        public void AddPlayerNonUbDamage(int unitId, long damage)
        {
            if (!NeedsCoreStats()) return;

            if (!PlayerNonUbDamageDic.ContainsKey(unitId))
            {
                PlayerNonUbDamageDic.Add(unitId, 0);
            }
            PlayerNonUbDamageDic[unitId] += damage;
        }

        public void RecordStateChange(UnitCtrl unit, UnitCtrl.ActionState nextState, BattleManager battleManager, int frame, int realFrame)
        {
            if (!NeedsUbTime() || unit == null || battleManager == null)
            {
                return;
            }

            if (nextState != UnitCtrl.ActionState.SKILL_1 || unit.IsOther)
            {
                if (!NeedsUbTime())
                {
                    return;
                }
            }

            var myStateList = GetOrCreateStateHistory(unit.UnitId);
            myStateList.Add(new UnitStateChangeDataForTolerance
            {
                currentFrameCount = frame,
                realFrameCount = realFrame,
                changStateTo = nextState
            });

            if (!NeedsUbTime() || nextState != UnitCtrl.ActionState.SKILL_1 || unit.IsOther)
            {
                return;
            }

            int finalTolerance = -1;
            if (ShouldRecordPlayerUbTolerance(unit.UnitId, myStateList, frame))
            {
                int unitIndex = battleManager.UnitList.IndexOf(unit);
                if (unitIndex != -1)
                {
                    finalTolerance = CalculatePlayerUbTolerance(battleManager, unitIndex, frame);
                }
            }

            AddPlayerUbTolerance(unit.UnitId, finalTolerance);
        }

        private List<UnitStateChangeDataForTolerance> GetOrCreateStateHistory(int unitId)
        {
            if (!AllUnitStateChangeDic.TryGetValue(unitId, out var stateHistory))
            {
                stateHistory = new List<UnitStateChangeDataForTolerance>();
                AllUnitStateChangeDic[unitId] = stateHistory;
            }

            return stateHistory;
        }

        private bool ShouldRecordPlayerUbTolerance(int unitId, List<UnitStateChangeDataForTolerance> stateHistory, int frame)
        {
            if (stateHistory.Count < 2)
            {
                return false;
            }

            var previousState = stateHistory[stateHistory.Count - 2];
            if (previousState.changStateTo != UnitCtrl.ActionState.IDLE)
            {
                return false;
            }

            int lastTpReadyFrame = GetLastTpReadyFrame(unitId, frame - 1);
            return frame == Math.Max(previousState.currentFrameCount, lastTpReadyFrame);
        }

        private int CalculatePlayerUbTolerance(BattleManager battleManager, int currentUnitIndex, int frame)
        {
            if (IsFakeAuto(battleManager, currentUnitIndex))
            {
                return MaxTrackedFrames;
            }

            int? tolerance = null;
            for (int i = 0; i < battleManager.UnitList.Count; ++i)
            {
                var candidate = battleManager.UnitList[i];
                if (i != currentUnitIndex && !candidate.IsSkillReady)
                {
                    continue;
                }

                if (TryGetToleranceAgainstUnit(
                        battleManager,
                        currentUnitIndex,
                        i,
                        candidate,
                        frame,
                        out int candidateTolerance))
                {
                    tolerance = tolerance.HasValue
                        ? Math.Min(tolerance.Value, candidateTolerance)
                        : candidateTolerance;
                }
            }

            return tolerance ?? -1;
        }

        private bool IsFakeAuto(BattleManager battleManager, int currentUnitIndex)
        {
            for (int i = 0; i < currentUnitIndex; ++i)
            {
                var unit = battleManager.UnitList[i];
                if (unit.IsSkillReady && unit.CurrentState == UnitCtrl.ActionState.IDLE)
                {
                    return true;
                }
            }

            return false;
        }

        private bool TryGetToleranceAgainstUnit(
            BattleManager battleManager,
            int currentUnitIndex,
            int candidateIndex,
            UnitCtrl candidate,
            int frame,
            out int tolerance)
        {
            if (candidateIndex != currentUnitIndex &&
                candidate.CurrentState == UnitCtrl.ActionState.IDLE &&
                frame > 0 &&
                WasSkillReadyAt(candidate.UnitId, frame - 1))
            {
                tolerance = 0;
                return true;
            }

            if (!TryGetIdleWindow(candidate.UnitId, candidateIndex == currentUnitIndex, frame, out int leftFrame, out var nextState))
            {
                tolerance = 0;
                return false;
            }

            if (!WasSkillReadyInRange(candidate.UnitId, leftFrame, nextState.currentFrameCount))
            {
                tolerance = 0;
                return false;
            }

            tolerance = battleManager.FrameCount - nextState.realFrameCount;
            return true;
        }

        private bool TryGetIdleWindow(
            int unitId,
            bool isCurrentUnit,
            int frame,
            out int leftFrame,
            out UnitStateChangeDataForTolerance nextState)
        {
            leftFrame = 0;
            nextState = default;

            if (!AllUnitStateChangeDic.TryGetValue(unitId, out var stateHistory))
            {
                return false;
            }

            for (int i = stateHistory.Count - 1; i >= 0; --i)
            {
                var state = stateHistory[i];
                bool isRelevantIdle = state.changStateTo == UnitCtrl.ActionState.IDLE &&
                                      (!isCurrentUnit || state.currentFrameCount < frame);
                if (!isRelevantIdle)
                {
                    continue;
                }

                if (i + 1 >= stateHistory.Count)
                {
                    return false;
                }

                leftFrame = state.currentFrameCount;
                nextState = stateHistory[i + 1];
                return true;
            }

            return false;
        }

        private void AddPlayerUbTolerance(int unitId, int tolerance)
        {
            if (!PlayerUbToleranceFrames.TryGetValue(unitId, out var toleranceFrames))
            {
                toleranceFrames = new List<int>();
                PlayerUbToleranceFrames[unitId] = toleranceFrames;
            }

            toleranceFrames.Add(tolerance);
        }

        private int GetLastTpReadyFrame(int unitId, int frame)
        {
            int lastFrame = Math.Min(frame, MaxTrackedFrames - 1);
            for (int k = lastFrame; k >= 0; --k)
            {
                if (!WasSkillReadyAt(unitId, k))
                {
                    return k + 1;
                }
            }

            return 0;
        }

        private bool WasSkillReadyAt(int unitId, int frame)
        {
#if ENABLE_THREADING
            var frames = BattleManager.Instance.IsSkillReadyFrames;
            var slotMap = BattleManager.Instance._unitIdToSlot;
            if (frames == null || slotMap == null) return false;
            if (frame < 0 || frame >= MaxTrackedFrames || frame >= frames.Length) return false;
            if (!slotMap.TryGetValue(unitId, out int slot)) return false;
            if (frames[frame] == null || slot < 0 || slot >= frames[frame].Length) return false;
            return frames[frame][slot];
#else
            return frame >= 0 &&
                   frame < MaxTrackedFrames &&
                   IsSkillReady.TryGetValue(frame, out var readyByUnit) &&
                   readyByUnit.TryGetValue(unitId, out var isReady) &&
                   isReady;
#endif
        }

        private bool WasSkillReadyInRange(int unitId, int leftFrame, int rightFrame)
        {
#if ENABLE_THREADING
            var frames = BattleManager.Instance.IsSkillReadyFrames;
            var slotMap = BattleManager.Instance._unitIdToSlot;
            if (frames == null || slotMap == null) return false;
            if (!slotMap.TryGetValue(unitId, out int slot)) return false;

            int startFrame = Math.Max(0, leftFrame);
            int endFrame = Math.Min(Math.Min(rightFrame, MaxTrackedFrames), frames.Length);
            for (int k = startFrame; k < endFrame; k++)
            {
                if (frames[k] != null &&
                    slot >= 0 &&
                    slot < frames[k].Length &&
                    frames[k][slot])
                {
                    return true;
                }
            }

            return false;
#else
            int startFrame = Math.Max(0, leftFrame);
            int endFrame = Math.Min(rightFrame, MaxTrackedFrames);
            for (int k = startFrame; k < endFrame; k++)
            {
                if (WasSkillReadyAt(unitId, k))
                {
                    return true;
                }
            }

            return false;
#endif
        }

        public void RecordBuffDebuffStart(long token, UnitCtrl target, UnitCtrl source, UnitCtrl.BuffParamKind kind, bool isBuff, long value, int skillId, int actionId, string describe)
        {
            if (!NeedsBuffFlow() || target == null)
            {
                return;
            }

            ActiveBuffDebuffEventIndexByToken[token] = BuffDebuffEvents.Count;
            BuffDebuffEvents.Add(new BuffDebuffEventData
            {
                TargetUnitId = target.UnitId,
                SourceUnitId = source?.UnitId ?? 0,
                BuffParamKind = kind,
                BuffParamKindValue = (int)kind,
                BuffParamKindName = kind.ToString(),
                BuffParamKindDescription = kind.GetDescription(),
                IsBuff = isBuff,
                Value = value,
                StartFrame = BattleHeaderController.CurrentFrameCount,
                EndFrame = -1,
                IsActiveAtBattleEnd = true,
                SkillId = skillId,
                ActionId = actionId,
                Describe = describe
            });
        }

        public void RecordBuffDebuffEnd(long token)
        {
            if (!NeedsBuffFlow())
            {
                return;
            }

            if (!ActiveBuffDebuffEventIndexByToken.TryGetValue(token, out var index))
            {
                return;
            }

            BuffDebuffEvents[index].EndFrame = BattleHeaderController.CurrentFrameCount;
            BuffDebuffEvents[index].IsActiveAtBattleEnd = false;
            ActiveBuffDebuffEventIndexByToken.Remove(token);
        }

        public void RecordBossDefEvent(UnitCtrl target, UnitCtrl source, UnitCtrl.BuffParamKind kind, int skillId, int actionId, string describe)
        {
            if (!NeedsBossDef() || target == null || !target.IsOther || target.IsSummonOrPhantom)
            {
                return;
            }

            var eventData = new BossDefEventData
            {
                TargetUnitId = target.UnitId,
                Frame = BattleHeaderController.CurrentFrameCount,
                Value = kind == UnitCtrl.BuffParamKind.DEF ? target.DefZero : target.MagicDefZero,
                SourceUnitId = source?.UnitId ?? 0,
                SkillId = skillId,
                ActionId = actionId,
                Describe = describe
            };

            if (kind == UnitCtrl.BuffParamKind.DEF)
            {
                BossDefEvents.Add(eventData);
            }
            else if (kind == UnitCtrl.BuffParamKind.MAGIC_DEF)
            {
                BossMagicDefEvents.Add(eventData);
            }
        }

        public void RecordSkillExecStart(UnitCtrl unit, int skillId, string skillName, int actionId)
        {
            if (!NeedsSkillExecMin() || unit == null)
            {
                return;
            }

            RecordSkillExecEnd(unit.UnitId, BattleHeaderController.CurrentFrameCount);
            ActiveSkillExecEventIndexByUnitId[unit.UnitId] = SkillExecEvents.Count;
            SkillExecEvents.Add(new SkillExecEventData
            {
                UnitId = unit.UnitId,
                SkillId = skillId,
                SkillName = ResolveSkillName(unit, skillId, skillName),
                ActionId = actionId,
                StartFrame = BattleHeaderController.CurrentFrameCount,
                EndFrame = -1
            });
        }

        public void RecordSkillExecEnd(int unitId, int frame)
        {
            if (!NeedsSkillExecMin())
            {
                return;
            }

            if (!ActiveSkillExecEventIndexByUnitId.TryGetValue(unitId, out var index))
            {
                return;
            }

            SkillExecEvents[index].EndFrame = frame;
            ActiveSkillExecEventIndexByUnitId.Remove(unitId);
        }

        public void RecordHitTrace(
            UnitCtrl target,
            UnitCtrl source,
            int skillId,
            string skillName,
            int actionId,
            long damage,
            DamageData.eDamageType damageType,
            bool isCritical,
            long remainingHp,
            int targetPartsIndex,
            eActionType actionType,
            bool isSlipDamage)
        {
            if (!NeedsHitTrace() || target == null || damage <= 0)
            {
                return;
            }

            HitTraceEvents.Add(new HitTraceEventData
            {
                Frame = BattleHeaderController.CurrentFrameCount,
                SourceUnitId = source?.UnitId ?? 0,
                TargetUnitId = target.UnitId,
                SkillId = skillId,
                SkillName = ResolveSkillName(source, skillId, skillName),
                ActionId = actionId,
                Damage = damage,
                DamageType = damageType,
                DamageTypeValue = (int)damageType,
                DamageTypeName = damageType.ToString(),
                IsCritical = isCritical,
                RemainingHp = remainingHp,
                TargetPartsIndex = targetPartsIndex,
                ActionType = actionType,
                ActionTypeValue = (int)actionType,
                ActionTypeName = actionType.ToString(),
                IsSlipDamage = isSlipDamage
            });
        }

        public void RecordTpChange(
            UnitCtrl target,
            UnitCtrl source,
            eSetEnergyType setEnergyType,
            double energyBefore,
            double requestedEnergy,
            double energyAfter)
        {
            if (!NeedsTpChangeTrace() ||
                target == null ||
                target.IsOther ||
                target.IsSummonOrPhantom)
            {
                return;
            }

            double deltaBeforeCap = requestedEnergy - energyBefore;
            double appliedDelta = energyAfter - energyBefore;
            bool isUbConsume = setEnergyType == eSetEnergyType.BY_USE_SKILL;

            if (!isUbConsume && appliedDelta <= 0)
            {
                return;
            }

            if (!isUbConsume &&
                (setEnergyType == eSetEnergyType.INITIALIZE ||
                 setEnergyType == eSetEnergyType.BY_STORY_TIME_LINE ||
                 setEnergyType == eSetEnergyType.BY_MODE_CHANGE))
            {
                return;
            }

            int skillId = ResolveTpChangeSkillId(target, source, setEnergyType);
            UnitCtrl skillOwner = isUbConsume ? target : source;
            double energyCap = UnitDefine.MAX_ENERGY;
            bool isReadyBefore = energyBefore >= energyCap;
            bool isReadyAfter = energyAfter >= energyCap;

            TpChangeEvents.Add(new TpChangeEventData
            {
                Frame = BattleHeaderController.CurrentFrameCount,
                TargetUnitId = target.UnitId,
                TargetLaneIndex = ResolvePlayerLaneIndex(target),
                SourceUnitId = source?.UnitId ?? 0,
                SourceLaneIndex = ResolvePlayerLaneIndex(source),
                SetEnergyTypeValue = (int)setEnergyType,
                SetEnergyTypeName = setEnergyType.ToString(),
                SkillId = skillId,
                SkillName = ResolveSkillName(skillOwner, skillId, string.Empty),
                ActionId = 0,
                EnergyBefore = energyBefore,
                EnergyAfter = energyAfter,
                EnergyCap = energyCap,
                DeltaBeforeCap = deltaBeforeCap,
                AppliedDelta = appliedDelta,
                OverflowDelta = deltaBeforeCap - appliedDelta,
                IsIncrease = deltaBeforeCap > 0,
                IsReadyBefore = isReadyBefore,
                IsReadyAfter = isReadyAfter,
                CrossedReady = !isReadyBefore && isReadyAfter
            });
        }

        private int ResolveTpChangeSkillId(UnitCtrl target, UnitCtrl source, eSetEnergyType setEnergyType)
        {
            if (setEnergyType == eSetEnergyType.BY_USE_SKILL)
            {
                return target?.UnionBurstSkillId ?? 0;
            }

            return source?.CurrentSkillId ?? 0;
        }

        private int ResolvePlayerLaneIndex(UnitCtrl unit)
        {
            if (unit == null)
            {
                return -1;
            }

            var playerList = MyGameCtrl.Instance?.tempData?.playerList;
            if (playerList == null)
            {
                return -1;
            }

            for (int index = 0; index < playerList.Count; index++)
            {
                if (playerList[index].unitId == unit.UnitId)
                {
                    return index;
                }
            }

            return -1;
        }

        private string ResolveSkillName(UnitCtrl unit, int skillId, string skillName)
        {
            if (!string.IsNullOrWhiteSpace(skillName))
            {
                return skillName;
            }

            if (skillId == 1)
            {
                return "普攻";
            }

            if (unit?.unitActionController?.skillDictionary != null &&
                unit.unitActionController.skillDictionary.TryGetValue(skillId, out var skill) &&
                !string.IsNullOrWhiteSpace(skill?.SkillName))
            {
                return skill.SkillName;
            }

            var mainManager = MainManager.Instance;
            if (mainManager?.SkillNameAndDescribeCn != null &&
                mainManager.SkillNameAndDescribeCn.TryGetValue(skillId, out var localizedNames) &&
                localizedNames != null &&
                localizedNames.Length > 0 &&
                !string.IsNullOrWhiteSpace(localizedNames[0]))
            {
                return localizedNames[0];
            }

            return skillId > 0 ? $"skill_{skillId}" : string.Empty;
        }

        public void Reset()
        {
            TotalMagicBuff = 0;
            TotalPhysicsBuff = 0;
            TotalMagicDebuff = 0;
            TotalPhysicsDebuff = 0;
            ScorePhysicsExpectation = 0;
            ScoreMagicExpectation = 0;
            PlayerDamageDic.Clear();
            PlayerNonUbDamageDic.Clear();
            PlayerTpGainedDic.Clear();
            PlayerUbToleranceFrames.Clear();
            AllUnitStateChangeDic.Clear();
            BuffDebuffEvents.Clear();
            BossDefEvents.Clear();
            BossMagicDefEvents.Clear();
            SkillExecEvents.Clear();
            HitTraceEvents.Clear();
            TpChangeEvents.Clear();
            ActiveBuffDebuffEventIndexByToken.Clear();
            ActiveSkillExecEventIndexByUnitId.Clear();
        }

        public BattleStatsData ToBattleStatsData()
        {
            var playerList = MyGameCtrl.Instance.tempData.playerList;
            var statsData = new BattleStatsData
            {
                TotalDamage = GuildCalculator.Instance.GetTotalDamage(),
                TotalMagicBuff = TotalMagicBuff,
                TotalPhysicsBuff = TotalPhysicsBuff,
                TotalMagicDebuff = TotalMagicDebuff,
                TotalPhysicsDebuff = TotalPhysicsDebuff,
                ScorePhysicsExpectation = ScorePhysicsExpectation,
                ScoreMagicExpectation = ScoreMagicExpectation,
                PlayerTpGained = playerList.Select(p => PlayerTpGainedDic.ContainsKey(p.unitId) ? PlayerTpGainedDic[p.unitId] : 0).ToList(),
                PlayerDamageList = playerList.Select(p => PlayerDamageDic.ContainsKey(p.unitId) ? PlayerDamageDic[p.unitId] : 0).ToList(),
                PlayerNonUbDamageList = playerList.Select(p => PlayerNonUbDamageDic.ContainsKey(p.unitId) ? PlayerNonUbDamageDic[p.unitId] : 0).ToList(),
                SkillReadyCrossingFrames = BuildSkillReadyCrossingFrames(playerList),
                AutoSemanUBExecTimeList = new List<List<int>>(),
                AutoSemanTeacherPayloads = new List<AutoSemanTeacherPayloadData>(),
                BuffDebuffEvents = NeedsBuffFlow() ? BuffDebuffEvents.ToList() : new List<BuffDebuffEventData>(),
                BossDefEvents = NeedsBossDef() ? BossDefEvents.ToList() : new List<BossDefEventData>(),
                BossMagicDefEvents = NeedsBossDef() ? BossMagicDefEvents.ToList() : new List<BossDefEventData>(),
                SkillExecEvents = NeedsSkillExecMin() ? SkillExecEvents.ToList() : new List<SkillExecEventData>(),
                HitTraceEvents = NeedsHitTrace() ? HitTraceEvents.ToList() : new List<HitTraceEventData>(),
                TpChangeEvents = NeedsTpChangeTrace() ? TpChangeEvents.ToList() : new List<TpChangeEventData>()
            };

            statsData.UBTime = playerList
                .Select((p, i) => (player: p, pos: i))
                .SelectMany(playerWithIndex =>
                {
                    if (!AllUnitStateChangeDic.TryGetValue(playerWithIndex.player.unitId, out var stateHistory))
                    {
                        return Enumerable.Empty<(UnitStateChangeDataForTolerance state, int pos)>();
                    }

                    return stateHistory
                        .Where(state => state.changStateTo == UnitCtrl.ActionState.SKILL_1)
                        .Select(state => (state, playerWithIndex.pos));
                })
                .GroupBy(s => s.state.currentFrameCount).SelectMany(g => g.OrderBy(t => t.state.realFrameCount)
                    .Select((t, i) => (t, normalized: t.state.currentFrameCount + i * 0.001f)))
                .GroupBy(s => s.t.pos).Select(g =>
                    (g.Key, list: g.OrderBy(t => t.normalized).Select(t => t.normalized).ToList()))
                .Aggregate(new List<List<float>>(Enumerable.Repeat(new List<float>(), Math.Max(0, playerList.Count()))
                    .Select(x => new List<float>())), (result, tuple) =>
                {
                    int index = tuple.Key;
                    while (result.Count <= index)
                    {
                        result.Add(new List<float>());
                    }
                    result[index] = tuple.list;
                    return result;
                });

            if (!NeedsUbTime())
            {
                statsData.UBToleranceFrames = playerList.Select(_ => new List<int>()).ToList();
                statsData.UBTime = playerList.Select(_ => new List<float>()).ToList();
                AttachAutoSemanTeacherPayloads(statsData);
                return statsData;
            }

            statsData.UBToleranceFrames = playerList.Select(p =>
            {
                int unitId = p.unitId;
                if (PlayerUbToleranceFrames.ContainsKey(unitId))
                    return PlayerUbToleranceFrames[unitId];
                return new List<int>();
            }).ToList();

            AttachAutoSemanTeacherPayloads(statsData);

            return statsData;
        }

        private void AttachAutoSemanTeacherPayloads(BattleStatsData statsData)
        {
            if (!NeedsAutoSemanTeacherTrace())
            {
                return;
            }

            var payloads = BuildAutoSemanTeacherPayloads(statsData);
            statsData.AutoSemanTeacherPayloads = payloads;
            statsData.AutoSemanUBExecTimeList = payloads
                .FirstOrDefault(p => p.SemanSkillToggleIndex == 5)?.TeacherGenesByLane
                ?? new List<List<int>>();
        }

        private List<AutoSemanTeacherPayloadData> BuildAutoSemanTeacherPayloads(BattleStatsData statsData)
        {
            var semanManager = BattleManager.Instance.semanubmanager;
            var semanUbNum = semanManager.UBNums?.ToList() ?? new List<int>();
            return new List<AutoSemanTeacherPayloadData>
            {
                BuildAutoSemanTeacherPayload("typed_event", 5, statsData, semanUbNum),
                BuildAutoSemanTeacherPayload("unit_any_event", 6, statsData, semanUbNum),
                BuildAutoSemanTeacherPayload("global_any_event", 7, statsData, semanUbNum),
            };
        }

        private AutoSemanTeacherPayloadData BuildAutoSemanTeacherPayload(
            string toggleProfile,
            int semanSkillToggleIndex,
            BattleStatsData statsData,
            List<int> semanUbNum)
        {
            var teacherGenes = BattleManager.Instance.semanubmanager.Auto2Seman(semanSkillToggleIndex);
            return new AutoSemanTeacherPayloadData
            {
                ToggleProfile = toggleProfile,
                SemanSkillToggleIndex = semanSkillToggleIndex,
                TeacherGenesByLane = CloneNestedIntLists(teacherGenes),
                UBTime = CloneNestedFloatLists(statsData.UBTime),
                SemanUBNum = semanUbNum.ToList(),
                CalcStats = (int)LogLevel,
                Seed = MyGameCtrl.Instance.CurrentSeedForSave,
            };
        }

        private static List<List<int>> CloneNestedIntLists(List<List<int>> values)
        {
            return (values ?? new List<List<int>>())
                .Select(lane => new List<int>(lane ?? new List<int>()))
                .ToList();
        }

        private static List<List<float>> CloneNestedFloatLists(List<List<float>> values)
        {
            return (values ?? new List<List<float>>())
                .Select(lane => new List<float>(lane ?? new List<float>()))
                .ToList();
        }

        private List<List<int>> BuildSkillReadyCrossingFrames(List<PCRCaculator.UnitData> playerList)
        {
            if (!NeedsSkillReadyTrace())
            {
                return playerList.Select(_ => new List<int>()).ToList();
            }

            return playerList
                .Select(player => BuildSkillReadyCrossingFrames(player.unitId))
                .ToList();
        }

        private List<int> BuildSkillReadyCrossingFrames(int unitId)
        {
            var crossingFrames = new List<int>();
            bool hasPrevious = false;
            bool previousReady = false;

#if ENABLE_THREADING
            var frames = BattleManager.Instance.IsSkillReadyFrames;
            if (frames == null)
            {
                return crossingFrames;
            }

            int endFrame = Math.Min(MaxTrackedFrames, frames.Length);
            for (int frame = 0; frame < endFrame; frame++)
            {
                bool isReady = WasSkillReadyAt(unitId, frame);
                if (isReady && (!hasPrevious || !previousReady))
                {
                    crossingFrames.Add(frame);
                }

                hasPrevious = true;
                previousReady = isReady;
            }
#else
            foreach (int frame in IsSkillReady.Keys
                         .Where(frame => frame >= 0 && frame < MaxTrackedFrames)
                         .OrderBy(frame => frame))
            {
                bool isReady = WasSkillReadyAt(unitId, frame);
                if (isReady && (!hasPrevious || !previousReady))
                {
                    crossingFrames.Add(frame);
                }

                hasPrevious = true;
                previousReady = isReady;
            }
#endif

            return crossingFrames;
        }
    }
}
