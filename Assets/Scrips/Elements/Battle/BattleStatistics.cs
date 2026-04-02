using System;
using System.Collections.Generic;
using System.Linq;
using PCRCaculator.Guild;

namespace Elements.Battle
{
    public enum BattleStatisticsLogLevel
    {
        None = 0,
        Simple = 1,
        Full = 2,
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
        public Dictionary<int, Dictionary<int, bool>> IsSkillReady
        {
            get => BattleManager.Instance.IsSkillReady;
        }

        public BattleStatisticsLogLevel LogLevel { get; set; } = BattleStatisticsLogLevel.Full;

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
            if (LogLevel < BattleStatisticsLogLevel.Simple) return;
            ScorePhysicsExpectation += phyScore;
            ScoreMagicExpectation += magScore;
        }

        public void AddMagicBuff(long value) { if (LogLevel >= BattleStatisticsLogLevel.Simple) TotalMagicBuff += value; }
        public void AddPhysicsBuff(long value) { if (LogLevel >= BattleStatisticsLogLevel.Simple) TotalPhysicsBuff += value; }
        public void AddMagicDebuff(long value) { if (LogLevel >= BattleStatisticsLogLevel.Simple) TotalMagicDebuff += value; }
        public void AddPhysicsDebuff(long value) { if (LogLevel >= BattleStatisticsLogLevel.Simple) TotalPhysicsDebuff += value; }

        public void AddPlayerTp(int unitId, long value)
        {
            if (LogLevel < BattleStatisticsLogLevel.Simple) return;

            if (!PlayerTpGainedDic.ContainsKey(unitId))
            {
                PlayerTpGainedDic.Add(unitId, 0);
            }
            PlayerTpGainedDic[unitId] += value;
        }

        public void AddPlayerDamage(int unitId, long damage)
        {
            if (LogLevel < BattleStatisticsLogLevel.Simple) return;

            if (!PlayerDamageDic.ContainsKey(unitId))
            {
                PlayerDamageDic.Add(unitId, 0);
            }
            PlayerDamageDic[unitId] += damage;
        }

        public void AddPlayerNonUbDamage(int unitId, long damage)
        {
            if (LogLevel < BattleStatisticsLogLevel.Simple) return;

            if (!PlayerNonUbDamageDic.ContainsKey(unitId))
            {
                PlayerNonUbDamageDic.Add(unitId, 0);
            }
            PlayerNonUbDamageDic[unitId] += damage;
        }

        public void RecordStateChange(UnitCtrl unit, UnitCtrl.ActionState nextState, BattleManager battleManager, int frame, int realFrame)
        {
            if (LogLevel < BattleStatisticsLogLevel.Full || unit == null || battleManager == null)
            {
                return;
            }

            var myStateList = GetOrCreateStateHistory(unit.UnitId);
            myStateList.Add(new UnitStateChangeDataForTolerance
            {
                currentFrameCount = frame,
                realFrameCount = realFrame,
                changStateTo = nextState
            });

            if (nextState != UnitCtrl.ActionState.SKILL_1 || unit.IsOther)
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
            return frame >= 0 &&
                   frame < MaxTrackedFrames &&
                   IsSkillReady.TryGetValue(frame, out var readyByUnit) &&
                   readyByUnit.TryGetValue(unitId, out var isReady) &&
                   isReady;
        }

        private bool WasSkillReadyInRange(int unitId, int leftFrame, int rightFrame)
        {
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
        }

        public BattleStatsData ToBattleStatsData()
        {
            var ubToleranceFrames = MyGameCtrl.Instance.tempData.playerList.Select(p =>
            {
                int unitId = p.unitId;
                if (PlayerUbToleranceFrames.ContainsKey(unitId))
                    return PlayerUbToleranceFrames[unitId];
                return new List<int>();
            }).ToList();

            return new BattleStatsData
            {
                TotalDamage = GuildCalculator.Instance.GetTotalDamage(),
                TotalMagicBuff = TotalMagicBuff,
                TotalPhysicsBuff = TotalPhysicsBuff,
                TotalMagicDebuff = TotalMagicDebuff,
                TotalPhysicsDebuff = TotalPhysicsDebuff,
                ScorePhysicsExpectation = ScorePhysicsExpectation,
                ScoreMagicExpectation = ScoreMagicExpectation,
                PlayerTpGained = MyGameCtrl.Instance.tempData.playerList.Select(p => PlayerTpGainedDic.ContainsKey(p.unitId) ? PlayerTpGainedDic[p.unitId] : 0).ToList(),
                PlayerDamageList = MyGameCtrl.Instance.tempData.playerList.Select(p => PlayerDamageDic.ContainsKey(p.unitId) ? PlayerDamageDic[p.unitId] : 0).ToList(),
                PlayerNonUbDamageList = MyGameCtrl.Instance.tempData.playerList.Select(p => PlayerNonUbDamageDic.ContainsKey(p.unitId) ? PlayerNonUbDamageDic[p.unitId] : 0).ToList(),
                UBToleranceFrames = ubToleranceFrames
            };
        }
    }
}
