using System;
using System.Collections.Generic;
using System.Linq;
using PCRCaculator.Guild;
using static Elements.UnitCtrl;

namespace Elements.Battle
{
    public sealed class SemanticUbSequenceItem
    {
        public int SequenceIndex { get; set; }
        public int Lane { get; set; }
        public int Gene { get; set; }
    }

    public sealed class SemanticUbConversionSnapshot
    {
        public List<List<int>> SemanticLanes { get; set; }
        public List<SemanticUbSequenceItem> SemanticSequence { get; set; }
    }

    public enum eSemanEvent
    {
        ATTACK = 0,
        DAMAGE = 1,
        BUFF = 2,
        DEBUFF = 3,
        ABNORMAL = 4,
        SEAL = 5,
        TP_GAIN = 6,
        UB = 7,
        SKILL = 8, // include attack
        IDLE = 9,
    }

    public class SemanUbManager
    {
        private List<UnitCtrl> players = new List<UnitCtrl>();

        private Dictionary<ActionState, int> State2Id = new Dictionary<ActionState, int>
        {
            // { ActionState.ATK, 0 },
            { ActionState.SKILL_1, 1 },
            // { ActionState.SKILL, 8 },
            // { ActionState.ATK, 8 },
            { ActionState.IDLE, 9 },
        };
        public enum Action
        {
            NO_UB = -1,
            COUNTDOWN = 0,
            WAIT_TP_FULL = 1,
            WAIT_UNIT_ACTION = 2,
            WAIT_UNIT_EXEC = 3, // ignore what skill, exclude ub
            WAIT_EXEC = 4, // ignore who skill, exclude ub
            WAIT_UB = 5, // ignore who, only ub
            COUNTDOWN_EXCLUDE_IDLE = 6, // countdown but exclude idle
            WAIT_UNIT_EVENT = 7,
            WAIT_UNIT_ANY_EVENT = 8, // wait for any event from specific unit
            WAIT_EVENT = 9, // wait for any event from any unit
        }

        public class UbRecord
        {
            public bool skillReady
            {
                get
                {
                    return unitId < parent.players.Count && parent.players[unitId].IsSkillReady;
                }
            }
            private bool canProcess
            {
                get
                {
                    return skillReady;
                }
            }
            public bool IsIdle
            {
                get
                {
                    return unitId < parent.players.Count && parent.players[unitId].CurrentState == ActionState.IDLE;
                }
            }
            public int delay = 0;
            public int no_idle_delay = 0;
            public int unitId = 0;
            public List<UbStatus> history = new List<UbStatus>();
            private SemanUbManager parent;

            public UbRecord(SemanUbManager parent, int unitId)
            {
                this.parent = parent;
                this.unitId = unitId;
            }

            // public void OnTpChangeCallBack(float energy)
            // {
            //     if (energy >= 1000)
            //     {
            //         skillReady = true;
            //     }
            //     else
            //     {
            //         skillReady = false;
            //     }
            // }

            public void OnEventCallback(int unitPos, int eventId)
            {
                if (!canProcess) return;
                int frame = BattleHeaderController.CurrentFrameCount;
                bool duplicate = false;
                if (eventId != (int)eSemanEvent.UB)
                {
                    for (int index = history.Count - 1; index >= 0 && history[index].frame == frame; index--)
                    {
                        var item = history[index];
                        if (item.waitUnit == unitPos &&
                            item.skillId == eventId &&
                            item.action == Action.WAIT_UNIT_EVENT)
                        {
                            duplicate = true;
                            break;
                        }
                    }
                }
                if (!duplicate)
                {
                    history.Add(new UbStatus(parent)
                    {
                        waitUnit = unitPos,
                        skillId = eventId,
                        action = Action.WAIT_UNIT_EVENT,
                        frame = frame,
                        recordDelayBase = delay,
                        hasRecordDelayBase = true
                    });
                }
            }

            public void OnChangeStateCallBack(int unitPos, ActionState state)
            {
                if (!canProcess) return;
                if (parent.State2Id.ContainsKey(state))
                {
                    var stateId = parent.State2Id[state];
                    var frame = BattleHeaderController.CurrentFrameCount;
                    // if (history.Count == 0 || history.Last().frame < frame || history.Count(h => h.action != Action.WAIT_UNIT_EVENT && h.frame == frame && h.waitUnit == unitPos && h.skillId == stateId) == 0)
                    if (true) // for now consider IDLE and UB, no need to check repeat
                    {
                        history.Add(new UbStatus(parent)
                        {
                            waitUnit = unitPos,
                            skillId = stateId,
                            action = Action.WAIT_UNIT_ACTION,
                            frame = frame,
                            recordDelayBase = delay,
                            hasRecordDelayBase = true
                        });
                    }
                }
            }

            public void OnSkillCallBack(int unitPos, int skillId)
            {
                if (!canProcess) return;
                int frame = BattleHeaderController.CurrentFrameCount;
                bool duplicate = false;
                for (int index = history.Count - 1; index >= 0 && history[index].frame == frame; index--)
                {
                    var item = history[index];
                    if (item.action != Action.WAIT_UNIT_EVENT &&
                        item.waitUnit == unitPos &&
                        item.skillId == skillId)
                    {
                        duplicate = true;
                        break;
                    }
                }
                if (!duplicate)
                {
                    history.Add(new UbStatus(parent)
                    {
                        waitUnit = unitPos,
                        skillId = skillId,
                        action = Action.WAIT_UNIT_ACTION,
                        frame = frame,
                        recordDelayBase = delay,
                        hasRecordDelayBase = true
                    });
                }
            }
            public void ProcessFrame()
            {
                if (canProcess)
                {
                    delay++;
                    if (!IsIdle)
                    {
                        no_idle_delay++;
                    }
                }
            }

            public int GetUbNumber(
                int unitId,
                int semanSkillType,
                int semanUbType)
            {
                bool useTypedEvent = semanSkillType >= 5;
                var relevantHistory = history
                    .Where(h => useTypedEvent
                        ? h.action == Action.WAIT_UNIT_EVENT
                        : h.action != Action.WAIT_UNIT_EVENT)
                    .ToList();
                int ubSkillId = useTypedEvent ? (int)eSemanEvent.UB : 1;

                if (relevantHistory.Count == 0 ||
                    (delay == 0 && relevantHistory.All(h => h.skillId != ubSkillId)))
                {
                    return delay;
                }

                int HistoricalDelay(UbStatus status) => status.hasRecordDelayBase
                    ? delay - status.recordDelayBase
                    : status.delay;
                int lastDelay = HistoricalDelay(relevantHistory.Last());
                var latestEvents = relevantHistory
                    .Where(h => HistoricalDelay(h) == lastDelay)
                    .ToList();
                var conditions = new List<Func<UbStatus, bool>>
                {
                    h => h.skillId == ubSkillId,
                    h => h.waitUnit == unitId && h.skillId == 9,
                    h => h.waitUnit == unitId,
                    h => true,
                };
                var last = conditions
                    .Select(condition => latestEvents.LastOrDefault(condition))
                    .First(status => status != null);

                if (useTypedEvent)
                {
                    if (semanSkillType == 5)
                    {
                        int trigger = relevantHistory.Count(h =>
                            h.waitUnit == last.waitUnit &&
                            h.skillId == last.skillId) - 1;
                        if (trigger >= 100 || lastDelay >= 10)
                        {
                            return delay;
                        }
                        return 100000 + last.waitUnit * 10000 + last.skillId * 1000 + trigger * 10 + lastDelay;
                    }
                    if (semanSkillType == 6)
                    {
                        int nonUbTrigger = relevantHistory
                            .Where(h =>
                                h.waitUnit == last.waitUnit &&
                                h.skillId != (int)eSemanEvent.UB)
                            .Select(h => h.frame)
                            .ToHashSet()
                            .Count;
                        int ubTrigger = relevantHistory.Count(h =>
                            h.waitUnit == last.waitUnit &&
                            h.skillId == (int)eSemanEvent.UB);
                        int trigger = nonUbTrigger + ubTrigger - 1;

                        if (trigger >= 100 || lastDelay >= 100)
                        {
                            return delay;
                        }
                        return 200000 + last.waitUnit * 10000 + trigger * 100 + lastDelay;
                    }
                    if (semanSkillType == 7)
                    {
                        int nonUbTrigger = relevantHistory
                            .Where(h => h.skillId != (int)eSemanEvent.UB)
                            .Select(h => h.frame)
                            .ToHashSet()
                            .Count;
                        int ubTrigger = relevantHistory.Count(h =>
                            h.skillId == (int)eSemanEvent.UB);
                        int trigger = nonUbTrigger + ubTrigger - 1;

                        if (trigger >= 1000 || lastDelay >= 100)
                        {
                            return delay;
                        }
                        return 300000 + trigger * 100 + lastDelay;
                    }
                    return delay;
                }

                if (last.skillId == 9 ||
                    last.skillId == 1 && semanUbType == 0 ||
                    last.skillId != 1 && semanSkillType == 1)
                {
                    int trigger = relevantHistory.Count(h =>
                        h.waitUnit == last.waitUnit &&
                        h.skillId == last.skillId) - 1;
                    if (trigger >= 10 || lastDelay >= 10)
                    {
                        return delay;
                    }
                    return 10000 + last.waitUnit * 1000 + last.skillId * 100 + trigger * 10 + lastDelay;
                }
                if (last.skillId == 1 && semanUbType == 1)
                {
                    int trigger = relevantHistory
                        .Where(h => h.skillId == last.skillId)
                        .Select(h => h.frame)
                        .ToHashSet()
                        .Count - 1;
                    if (trigger >= 100 || lastDelay >= 100)
                    {
                        return delay;
                    }
                    return 40000 + trigger * 100 + lastDelay;
                }
                if (semanSkillType == 2)
                {
                    int trigger = relevantHistory.Count(h =>
                        h.waitUnit == last.waitUnit &&
                        h.skillId != 1 &&
                        h.skillId != 9 &&
                        h.action == Action.WAIT_UNIT_ACTION) - 1;
                    if (trigger >= 100 || lastDelay >= 10)
                    {
                        return delay;
                    }
                    return 20000 + last.waitUnit * 1000 + trigger * 10 + lastDelay;
                }
                if (semanSkillType == 3)
                {
                    int trigger = relevantHistory
                        .Where(h =>
                            h.skillId != 1 &&
                            h.skillId != 9 &&
                            h.action == Action.WAIT_UNIT_ACTION)
                        .Select(h => h.frame)
                        .ToHashSet()
                        .Count - 1;
                    if (trigger >= 100 || lastDelay >= 100)
                    {
                        return delay;
                    }
                    return 30000 + trigger * 100 + lastDelay;
                }
                if (semanSkillType == 4 && !IsIdle)
                {
                    return 50000 + no_idle_delay;
                }
                return delay;
            }

            public void Clear()
            {
                delay = 0;
                no_idle_delay = 0;
                history.Clear();
            }

        }

        public class UbStatus
        {
            public SemanUbManager parent;

            public UbStatus(SemanUbManager parent)
            {
                this.parent = parent;
            }

            public Action action { get; set; }
            public bool skillReady {
                get
                {
                    return unitId < parent.players.Count && parent.players[unitId].IsSkillReady;
                }
            }
            private bool canProcess
            {
                get
                {
                    return skillReady && parent.IsSequenceTurn(unitId);
                }
            }

            public bool IsIdle
            {
                get
                {
                    return unitId < parent.players.Count && parent.players[unitId].CurrentState == ActionState.IDLE;
                }
            }
            public int unitId { get; set; }
            public int waitUnit { get; set; }
            public int skillId { get; set; } // 0: attack, 1: ub, 2: skill1, 3: skill2, ..., 9: auto
            public int trigger { get; set; }
            public int delay { get; set; }
            public int frame { get; set; }
            public int lastUpdateFrame { get; set; }
            // Recording-only baseline used to derive the historical delay in O(1).
            public int recordDelayBase { get; set; }
            internal bool hasRecordDelayBase;
            public int geneIndex { get; set; } = -1;
            public int geneValue { get; set; }
            public Action initialAction { get; set; }
            public int initialWaitUnit { get; set; }
            public int initialSkillId { get; set; }
            public int initialTrigger { get; set; }
            public int initialDelay { get; set; }
            public int anchorSatisfiedFrame { get; set; } = -1;
            private bool observeAnchor;
            private Action observedAction;
            private int observedTrigger;
            private int observedDelay;
            private int observedLastUpdateFrame;

            internal void EnableAnchorObservation()
            {
                observeAnchor = true;
                observedAction = action;
                observedTrigger = trigger;
                observedDelay = delay;
                observedLastUpdateFrame = lastUpdateFrame;
            }

            private bool IsObservedAnchorSatisfied()
            {
                return observeAnchor &&
                       (observedAction == Action.COUNTDOWN ||
                        observedAction == Action.COUNTDOWN_EXCLUDE_IDLE && !IsIdle) &&
                       observedDelay <= 0;
            }

            private void CaptureObservedAnchor()
            {
                if (anchorSatisfiedFrame < 0 && IsObservedAnchorSatisfied())
                {
                    anchorSatisfiedFrame = BattleHeaderController.CurrentFrameCount;
                }
            }

            private void ObserveEvent(int unitPos, int eventId)
            {
                if (!observeAnchor) return;
                if ((observedLastUpdateFrame < BattleHeaderController.CurrentFrameCount ||
                     eventId == (int)eSemanEvent.UB) &&
                    (observedAction == Action.WAIT_UNIT_EVENT && waitUnit == unitPos && skillId == eventId ||
                     observedAction == Action.WAIT_UNIT_ANY_EVENT && waitUnit == unitPos ||
                     observedAction == Action.WAIT_EVENT))
                {
                    observedTrigger--;
                    if (eventId != (int)eSemanEvent.UB)
                        observedLastUpdateFrame = BattleHeaderController.CurrentFrameCount;
                    if (observedTrigger < 0)
                        observedAction = Action.COUNTDOWN;
                    CaptureObservedAnchor();
                }
            }

            private void ObserveSkill(int unitPos, int observedSkillId)
            {
                if (!observeAnchor) return;
                if (observedLastUpdateFrame < BattleHeaderController.CurrentFrameCount &&
                    (observedAction == Action.WAIT_EXEC ||
                     waitUnit == unitPos &&
                     (observedAction == Action.WAIT_UNIT_EXEC ||
                      observedAction == Action.WAIT_UNIT_ACTION && skillId == observedSkillId)))
                {
                    observedTrigger--;
                    observedLastUpdateFrame = BattleHeaderController.CurrentFrameCount;
                    if (observedTrigger < 0)
                        observedAction = Action.COUNTDOWN;
                    CaptureObservedAnchor();
                }
            }

            private void ObserveState(int unitPos, ActionState state)
            {
                if (!observeAnchor) return;
                if (observedAction == Action.WAIT_UNIT_ACTION && waitUnit == unitPos &&
                    parent.State2Id.ContainsKey(state) && skillId == parent.State2Id[state] ||
                    state == ActionState.SKILL_1 && observedAction == Action.WAIT_UB)
                {
                    observedTrigger--;
                    if (observedTrigger < 0)
                        observedAction = Action.COUNTDOWN;
                    CaptureObservedAnchor();
                }
            }

            // public void OnTpChangeCallBack(float energy)
            // {
            //     if (energy >= 1000)
            //     {
            //         skillReady = true;
            //     }
            //     else
            //     {
            //         skillReady = false;
            //     }
            //     if (action == Action.WAIT_TP_FULL && skillReady)
            //     {
            //         action = Action.COUNTDOWN;
            //     }
            // }

            public void OnEventCallback(int unitPos, int eventId)
            {
                ObserveEvent(unitPos, eventId);
                if (!canProcess) return;
                if (
                    (lastUpdateFrame < BattleHeaderController.CurrentFrameCount || eventId == (int)eSemanEvent.UB) &&
                (action == Action.WAIT_UNIT_EVENT && waitUnit == unitPos && skillId == eventId ||
                (action == Action.WAIT_UNIT_ANY_EVENT && waitUnit == unitPos) ||
                (action == Action.WAIT_EVENT))
                )
                {
                    trigger--;
                    if (eventId != (int)eSemanEvent.UB)
                        lastUpdateFrame = BattleHeaderController.CurrentFrameCount;
                    if (trigger < 0)
                    {
                        action = Action.COUNTDOWN;
                    }
                }
            }

            public void OnSkillCallBack(int unitPos, int skillId)
            {
                ObserveSkill(unitPos, skillId);
                if (!canProcess) return;
                if (lastUpdateFrame < BattleHeaderController.CurrentFrameCount && (
                    action == Action.WAIT_EXEC || (waitUnit == unitPos &&
                    (action == Action.WAIT_UNIT_EXEC || (action == Action.WAIT_UNIT_ACTION && this.skillId == skillId))
                    )))
                {
                    trigger--;
                    lastUpdateFrame = BattleHeaderController.CurrentFrameCount;
                    if (trigger < 0)
                    {
                        action = Action.COUNTDOWN;
                    }
                }
            }

            public void ProcessFrame()
            {
                if (observeAnchor &&
                    (observedAction == Action.COUNTDOWN ||
                     observedAction == Action.COUNTDOWN_EXCLUDE_IDLE && !IsIdle))
                {
                    observedDelay--;
                    CaptureObservedAnchor();
                }
                if (canProcess && (action == Action.COUNTDOWN || action == Action.COUNTDOWN_EXCLUDE_IDLE && !IsIdle))
                    delay--;
            }

            public bool IsUbExec()
            {
                return canProcess && (action == Action.COUNTDOWN || action == Action.COUNTDOWN_EXCLUDE_IDLE && !IsIdle) && delay <= 0;
            }

            public bool IsSemanticAnchorSatisfied()
            {
                return IsObservedAnchorSatisfied();
            }

            public void OnChangeStateCallBack(int unitPos, ActionState state)
            {
                ObserveState(unitPos, state);
                if (!canProcess) return;
                if ((action == Action.WAIT_UNIT_ACTION && waitUnit == unitPos && parent.State2Id.ContainsKey(state) && skillId == parent.State2Id[state]) ||
                    (state == ActionState.SKILL_1 && action == Action.WAIT_UB)
                    // (state == ActionState.SKILL_1 && (action == Action.WAIT_UB || action == Action.WAIT_EXEC))
                )
                {
                    trigger--;
                    if (trigger < 0)
                    {
                        action = Action.COUNTDOWN;
                    }
                }
            }
        }

        private UbStatus GetUbStatusCore(int unitId, int num)
        {
            int op = num / 10000;
            if (num < 0)
            {
                return new UbStatus(this)
                {
                    action = Action.NO_UB,
                    unitId = unitId,
                    delay = 0
                };
            }
            else if (op == 0)
            {
                return new UbStatus(this)
                {
                    action = Action.COUNTDOWN,
                    unitId = unitId,
                    trigger = 0,
                    delay = num % 10000
                };
            }
            else if (op == 1)
            {
                int unitPos = num / 1000 % 10;
                int skillId = num / 100 % 10;
                int trigger = num / 10 % 10;
                int delay = num % 10;
                return new UbStatus(this)
                {
                    action = Action.WAIT_UNIT_ACTION,
                    unitId = unitId,
                    waitUnit = unitPos,
                    skillId = skillId,
                    trigger = trigger,
                    delay = delay
                };
            }
            else if (op == 2)
            {
                int unitPos = num / 1000 % 10;
                int trigger = num / 10 % 100;
                int delay = num % 10;
                return new UbStatus(this)
                {
                    action = Action.WAIT_UNIT_EXEC,
                    unitId = unitId,
                    waitUnit = unitPos,
                    trigger = trigger,
                    delay = delay
                };
            }
            else if (op == 3)
            {
                int trigger = num / 100 % 100;
                int delay = num % 100;
                return new UbStatus(this)
                {
                    action = Action.WAIT_EXEC,
                    unitId = unitId,
                    trigger = trigger,
                    delay = delay
                };
            }
            else if (op == 4)
            {
                int trigger = num / 100 % 100;
                int delay = num % 100;
                return new UbStatus(this)
                {
                    action = Action.WAIT_UB,
                    unitId = unitId,
                    trigger = trigger,
                    delay = delay
                };
            }
            else if (op == 5)
            {
                int delay = num % 10000;
                return new UbStatus(this)
                {
                    action = Action.COUNTDOWN_EXCLUDE_IDLE,
                    unitId = unitId,
                    trigger = 0,
                    delay = delay
                };
            }
            else if (op >= 30)
            // 3 bbb cc
            {
                int trigger = num / 100 % 1000;
                int delay = num % 100;
                return new UbStatus(this)
                {
                    action = Action.WAIT_EVENT,
                    unitId = unitId,
                    trigger = trigger,
                    delay = delay
                };
            }
            else if (op >= 20)
            // 2a bb cc
            {
                int unitPos = num / 10000 % 10;
                int trigger = num / 100 % 100;
                int delay = num % 100;
                return new UbStatus(this)
                {
                    action = Action.WAIT_UNIT_ANY_EVENT,
                    unitId = unitId,
                    waitUnit = unitPos,
                    trigger = trigger,
                    delay = delay
                };
            }
            else if (op >= 10)
            // 1a b cc d
            {
                int unitPos = num / 10000 % 10;
                int eventId = num / 1000 % 10;
                int trigger = num / 10 % 100;
                int delay = num % 10;
                return new UbStatus(this)
                {
                    action = Action.WAIT_UNIT_EVENT,
                    unitId = unitId,
                    waitUnit = unitPos,
                    skillId = eventId,
                    trigger = trigger,
                    delay = delay
                };
            }
            else
            {
                throw new ArgumentException($"Invalid operation code: {op}");
            }
        }

        public UbStatus GetUbStatus(int unitId, int num, int geneIndex = -1)
        {
            var status = GetUbStatusCore(unitId, num);
            status.geneIndex = geneIndex;
            status.geneValue = num;
            status.initialAction = status.action;
            status.initialWaitUnit = status.waitUnit;
            status.initialSkillId = status.skillId;
            status.initialTrigger = status.trigger;
            status.initialDelay = status.delay;
            return status;
        }

        private Queue<UbStatus>[] queues;
        public List<int> UBNums { get; private set; } = CreateUbCounts();

        private List<UbRecord> auto2seman = new List<UbRecord>();
        private List<List<int>> auto2semanAll = CreateEmptyLanes();
        private List<int> auto2semanSequence = new List<int>();
        private Dictionary<int, List<List<int>>> teacherProfiles = new Dictionary<int, List<List<int>>>();
        private bool executionEnabled = false;
        private bool recordingEnabled = false;
        private bool auto2semanSequenceMode = false;
        private bool conversionRecordingMode = false;
        private int conversionSemanSkillType = 6;
        private int conversionSemanUbType = 0;
        private List<int> ubSequence = new List<int>();
        private int ubSequenceIndex = 0;
        private readonly HashSet<UnitCtrl> subscribedUnits = new HashSet<UnitCtrl>();

        private bool IsActive => executionEnabled || recordingEnabled;

        private static List<int> CreateUbCounts() => new List<int> { 0, 0, 0, 0, 0 };

        private static List<List<int>> CreateEmptyLanes() => Enumerable.Range(0, 5)
            .Select(_ => new List<int>())
            .ToList();

        private void ResetRecordingState(bool recordSequence)
        {
            teacherProfiles = new[] { 5, 6, 7 }.ToDictionary(profile => profile, profile => CreateEmptyLanes());
            recordingEnabled = true;
            auto2semanSequenceMode = recordSequence;
            auto2seman = Enumerable.Range(0, 5)
                .Select(unitId => new UbRecord(this, unitId))
                .ToList();
            auto2semanAll = CreateEmptyLanes();
            auto2semanSequence = new List<int>();
        }

        public void Disable()
        {
            teacherProfiles.Clear();
            executionEnabled = false;
            recordingEnabled = false;
            subscribedUnits.Clear();
            players = new List<UnitCtrl>();
            queues = null;
            UBNums = CreateUbCounts();
            auto2seman = new List<UbRecord>();
            auto2semanAll = CreateEmptyLanes();
            auto2semanSequenceMode = false;
            conversionRecordingMode = false;
            conversionSemanSkillType = 6;
            conversionSemanUbType = 0;
            auto2semanSequence = new List<int>();
            ubSequence = new List<int>();
            ubSequenceIndex = 0;
        }

        public void SetUbExec(
            List<List<int>> ublists,
            List<int> sequence = null,
            bool recordSequence = false)
        {
            ubSequence = NormalizeSequence(sequence);
            ubSequenceIndex = 0;
            UBNums = CreateUbCounts();
            if (ublists == null || ublists.Count == 0)
            {
                executionEnabled = false;
                queues = null;
                conversionRecordingMode = false;
                conversionSemanSkillType = 6;
                conversionSemanUbType = 0;
                ResetRecordingState(recordSequence);
                return;
            }
            executionEnabled = true;
            if (recordingEnabled && !conversionRecordingMode)
            {
                teacherProfiles.Clear();
                recordingEnabled = false;
                auto2semanSequenceMode = false;
                auto2seman = new List<UbRecord>();
                auto2semanAll = CreateEmptyLanes();
                auto2semanSequence = new List<int>();
            }
            queues = ublists.Select((ublist, id) => new Queue<UbStatus>(
                ublist.Select((num, geneIndex) => GetUbStatus(id, num, geneIndex)))).ToArray();
        }

        public void BeginConversionRecording(
            bool recordSequence,
            int semanSkillType,
            int semanUbType)
        {
            ResetRecordingState(recordSequence);
            conversionRecordingMode = true;
            conversionSemanSkillType = semanSkillType;
            conversionSemanUbType = semanUbType;
        }

        private List<int> NormalizeSequence(List<int> sequence)
        {
            return sequence?.Where(pos => pos >= 0 && pos <= 4).ToList() ?? new List<int>();
        }

        private bool IsSequenceTurn(int unitId)
        {
            return ubSequence == null ||
                ubSequence.Count == 0 ||
                ubSequenceIndex >= ubSequence.Count ||
                ubSequence[ubSequenceIndex] == unitId;
        }

        public bool IsUbExec(int pos)
        {
            if (!executionEnabled) return false;
            if (pos < 0 || pos > 4 || pos >= queues.Length) return false;
            if (queues[pos].Count > 0)
            {
                var pending = queues[pos].Peek();
                if (pending.anchorSatisfiedFrame < 0 && pending.IsSemanticAnchorSatisfied())
                {
                    pending.anchorSatisfiedFrame = BattleHeaderController.CurrentFrameCount;
                }
            }
            if (!IsSequenceTurn(pos)) return false;
            if (queues[pos].Count == 0 || queues[pos].Peek().IsUbExec())
            {
                return true;
            }
            return false;
        }

        // public void OnTpChangeCallBack(int pos, float energy)
        // {
        //     if (recordingEnabled)
        //     {
        //         auto2seman[pos].OnTpChangeCallBack(energy);
        //         return;
        //     }

        //     if (queues[pos].Count > 0)
        //     {
        //         queues[pos].Peek().OnTpChangeCallBack(energy);
        //     }
        // }

        public void OnEventCallBack(int unitPos, eSemanEvent eventId)
        {
            if (!IsActive) return;
            if (recordingEnabled)
            {
                foreach (var ub in auto2seman)
                {
                    ub.OnEventCallback(unitPos, (int)eventId);
                }
            }
            if (executionEnabled)
            {
                foreach (var queue in queues)
                {
                    if (queue.Count > 0)
                    {
                        queue.Peek().OnEventCallback(unitPos, (int)eventId);
                    }
                }
            }
        }

        public void OnSkillEffectCallBack(int unitPos, int skill)
        {
            if (!IsActive) return;
            if (skill == 1) // ignore ub
                return;
            if (recordingEnabled)
            {
                foreach (var ub in auto2seman)
                {
                    ub.OnSkillCallBack(unitPos, skill);
                }
            }
            if (executionEnabled)
            {
                foreach (var queue in queues)
                {
                    if (queue.Count > 0)
                    {
                        queue.Peek().OnSkillCallBack(unitPos, skill);
                    }
                }
            }
        }

        public void OnChangeStateCallBack(int unitPos, ActionState state)
        {
            if (!IsActive) return;
            if (recordingEnabled)
            {
                foreach (var ub in auto2seman)
                {
                    ub.OnChangeStateCallBack(unitPos, state);
                }
            }
            if (executionEnabled)
            {
                foreach (var queue in queues)
                {
                    if (queue.Count > 0)
                    {
                        queue.Peek().OnChangeStateCallBack(unitPos, state);
                    }
                }
            }
        }

        public void ProcessFrame()
        {
            if (!IsActive) return;
            if (recordingEnabled)
            {
                for(int i = 0; i < auto2seman.Count; i++)
                {
                    auto2seman[i].ProcessFrame();
                }
            }
            if (executionEnabled)
            {
                for(int i = 0; i < queues.Length; i++)
                {
                    if (queues[i].Count > 0)
                    {
                        var pending = queues[i].Peek();
                        pending.ProcessFrame();
                        if (pending.anchorSatisfiedFrame < 0 && pending.IsSemanticAnchorSatisfied())
                        {
                            pending.anchorSatisfiedFrame = BattleHeaderController.CurrentFrameCount;
                        }
                    }
                }
            }
        }

        public void UbExecCallback(int pos)
        {
            if (!IsActive) return;
            if (pos < 0 || pos > 4) return;
            if (recordingEnabled)
            {
                int semanticUb;
                if (conversionRecordingMode)
                {
                    semanticUb = auto2seman[pos].GetUbNumber(
                        pos,
                        conversionSemanSkillType,
                        conversionSemanUbType);
                }
                else
                {
#if UNITY_5_3_OR_NEWER
                    int semanSkillType = GuildManager.Instance.SemanSkillToggles.FindIndex(
                        5,
                        toggle => toggle.isOn);
                    if (semanSkillType < 0)
                    {
                        semanSkillType = GuildManager.Instance.SemanSkillToggles.FindIndex(
                            toggle => toggle.isOn);
                    }
                    int semanUbType = GuildManager.Instance.SemanUBToggles.FindIndex(
                        toggle => toggle.isOn);
                    semanticUb = auto2seman[pos].GetUbNumber(
                        pos,
                        semanSkillType,
                        semanUbType);
#else
                    semanticUb = 0;
#endif
                }
                foreach (var profile in teacherProfiles.Keys)
                    teacherProfiles[profile][pos].Add(auto2seman[pos].GetUbNumber(pos, profile, 0));
                auto2semanAll[pos].Add(semanticUb);
                if (auto2semanSequenceMode)
                {
                    auto2semanSequence.Add(pos);
                }
                if (auto2semanSequenceMode)
                {
                    foreach (var record in auto2seman)
                    {
                        record.Clear();
                    }
                }
                else
                {
                    auto2seman[pos].Clear();
                }
            }
            if (executionEnabled && pos < queues.Length && queues[pos].Count > 0)
            {
                queues[pos].Dequeue();
            }
            if (executionEnabled &&
                ubSequence != null &&
                ubSequenceIndex < ubSequence.Count &&
                ubSequence[ubSequenceIndex] == pos)
            {
                ubSequenceIndex++;
            }
            UBNums[pos]++;
        }

        public int skill2Id(int skillId)
        {
            if (skillId == 1)
            {
                return 0;
            }
            int kind = skillId / 100 % 10;
            int id = skillId % 10;
            if (kind == 0)
            {
                return id;
            }
            else if (kind == 1) // sp skill
            {
                return id + 1;
            }
            else
            {
                return skillId % 10;
            }
        }

        public void SetUnitCallBack(List<UnitCtrl> players, UnitCtrl boss)
        {
            SetUnitCallBack(players, boss == null ? Enumerable.Empty<UnitCtrl>() : new[] { boss });
        }

        public void SetUnitCallBack(List<UnitCtrl> players, IEnumerable<UnitCtrl> bosses)
        {
            if (!IsActive) return;
            this.players = players ?? new List<UnitCtrl>();
            for (int i = 0; i < this.players.Count; i++)
            {
                AddUnitCallBack(this.players[i], i);
            }
            foreach (var boss in bosses ?? Enumerable.Empty<UnitCtrl>())
            {
                ObserveEnemyUnit(boss);
            }
        }

        public void ObserveEnemyUnit(UnitCtrl unit)
        {
            if (!IsActive || unit == null || !unit.IsOther) return;
            AddUnitCallBack(unit, 5);
        }

        internal void AddUnitCallBack(UnitCtrl unit, int unitPos)
        {
            if (!IsActive || unit == null || !subscribedUnits.Add(unit)) return;
            // unit.SemanOnTPChanged += (unitId, energy) => OnTpChangeCallBack(unitPos, energy);
            unit.SemanOnStartAction += (unitId, skill) => OnSkillEffectCallBack(unitPos, skill2Id(skill));
            unit.SemanOnExecAction += (unitId, skill) => OnSkillEffectCallBack(unitPos, skill2Id(skill));
            unit.SemanOnChangeState += (unitId, state) => OnChangeStateCallBack(unitPos, state);
            unit.SemanOnEventCallback += (unitId, buffDebuff) => OnEventCallBack(unitPos, buffDebuff);
        }

        public SemanticUbConversionSnapshot CaptureConversionSnapshot(bool includeSequence)
        {
            var lanes = BuildAuto2SemanOutput(auto2semanAll);
            List<SemanticUbSequenceItem> sequence = null;
            if (includeSequence)
            {
                if (!auto2semanSequenceMode)
                {
                    throw new InvalidOperationException(
                        "Semantic sequence output was not enabled for this battle.");
                }

                var laneOrdinals = new int[5];
                sequence = new List<SemanticUbSequenceItem>(auto2semanSequence.Count);
                for (int sequenceIndex = 0; sequenceIndex < auto2semanSequence.Count; sequenceIndex++)
                {
                    int lane = auto2semanSequence[sequenceIndex];
                    if (lane < 0 || lane >= auto2semanAll.Count ||
                        laneOrdinals[lane] >= auto2semanAll[lane].Count)
                    {
                        throw new InvalidOperationException(
                            "Semantic sequence and lane recordings are inconsistent.");
                    }
                    sequence.Add(new SemanticUbSequenceItem
                    {
                        SequenceIndex = sequenceIndex,
                        Lane = lane,
                        Gene = auto2semanAll[lane][laneOrdinals[lane]++],
                    });
                }
                for (int lane = 0; lane < laneOrdinals.Length; lane++)
                {
                    if (laneOrdinals[lane] != auto2semanAll[lane].Count)
                    {
                        throw new InvalidOperationException(
                            "Semantic sequence does not cover every recorded player UB.");
                    }
                }
            }

            return new SemanticUbConversionSnapshot
            {
                SemanticLanes = lanes,
                SemanticSequence = sequence,
            };
        }

        // Preserve the existing local-server teacher API without importing server observation DTOs.
        public List<List<int>> Auto2Seman() => BuildAuto2SemanOutput(auto2semanAll);
        public List<int> Auto2SemanSequence() => new List<int>(auto2semanSequence);
        public List<List<int>> Auto2Seman(int profile) => BuildAuto2SemanOutput(
            teacherProfiles.TryGetValue(profile, out var lanes) ? lanes : null);

        private List<List<int>> BuildAuto2SemanOutput(List<List<int>> source)
        {
            var ret = new List<List<int>>(5);
            for (int lane = 0; lane < 5; lane++)
            {
                var laneSource = source?.ElementAtOrDefault(lane) ?? Enumerable.Empty<int>();
                var ub = new List<int>();
                foreach (var gene in laneSource)
                {
                    ub.Add(gene);
                }
                ub.Add(-1);
                ret.Add(ub);
            }
            return ret;
        }

        private List<int> BuildSequenceLaneOutput(
            IReadOnlyList<SemanticUbSequenceItem> sequence)
        {
            if (sequence == null)
                return null;
            var lanes = new List<int>(sequence.Count);
            foreach (var item in sequence)
            {
                lanes.Add(item.Lane);
            }
            return lanes;
        }

    }
}
