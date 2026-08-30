using System;
using System.Collections.Generic;
using System.Linq;
using PCRCaculator.Guild;
using static Elements.UnitCtrl;

namespace Elements.Battle
{
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
        private List<UnitCtrl> players;

        private Dictionary<ActionState, int> State2Id = new Dictionary<ActionState, int>
        {
            // { ActionState.ATK, 0 },
            { ActionState.SKILL_1, 1 },
            // { ActionState.SKILL, 8 },
            // { ActionState.ATK, 8 },
            { ActionState.IDLE, 9 },
        };
        private bool ubMod;
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
                if (history.Count == 0 || history.Last().frame < frame || eventId == (int)eSemanEvent.UB || history.Count(h => h.frame == frame && h.waitUnit == unitPos && h.skillId == eventId && h.action == Action.WAIT_UNIT_EVENT) == 0)
                {
                    history.Add(new UbStatus(parent)
                    {
                        waitUnit = unitPos,
                        skillId = eventId,
                        action = Action.WAIT_UNIT_EVENT,
                        frame = frame
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
                            frame = frame
                        });
                    }
                }
            }

            public void OnSkillCallBack(int unitPos, int skillId)
            {
                if (!canProcess) return;
                int frame = BattleHeaderController.CurrentFrameCount;
                if (history.Count == 0 || history.Last().frame < frame || history.Count(h => h.action != Action.WAIT_UNIT_EVENT && h.frame == frame && h.waitUnit == unitPos && h.skillId == skillId) == 0)
                {
                    history.Add(new UbStatus(parent)
                    {
                        waitUnit = unitPos,
                        skillId = skillId,
                        action = Action.WAIT_UNIT_ACTION,
                        frame = frame
                    });
                }
            }
            public void ProcessFrame()
            {
                if (canProcess)
                {
                    foreach (var ub in history)
                    {
                        ub.delay++;
                    }
                    delay++;
                    if (!IsIdle)
                    {
                        no_idle_delay++;
                    }
                }
            }

            public int GetUbNumber(int unitId)
            {
#if !UNITY_5_3_OR_NEWER
                return 0;
#endif
                // any of 5 6 7 on
                bool use_fifth = GuildManager.Instance.SemanSkillToggles.Where((t, i) => i >= 5).Any(t => t.isOn);

                var t_history = history.Where(h => use_fifth && h.action == Action.WAIT_UNIT_EVENT || !use_fifth && h.action != Action.WAIT_UNIT_EVENT).ToList();

                if (t_history.Count == 0 ||
                (delay == 0 && t_history.Count(h => !use_fifth && h.skillId == 1 || use_fifth && h.skillId == (int)eSemanEvent.UB) == 0)) // set and not after other ub
                {
                    return delay;
                }

                int frame = BattleHeaderController.CurrentFrameCount;

                int lastDelay = t_history.Last().delay;
                var lasts = t_history.Where(h => h.delay == lastDelay).ToList();
                UbStatus last = null;

                var conditions = new List<Func<UbStatus, bool>>
                {
                    // ub
                    h => !use_fifth && h.skillId == 1 || use_fifth && h.skillId == (int)eSemanEvent.UB,
                    // self auto
                    h => !use_fifth && h.waitUnit == unitId && h.skillId == 9 || use_fifth && h.waitUnit == unitId && h.skillId == (int)eSemanEvent.IDLE,
                    // self skill
                    h => h.waitUnit == unitId, 
                    // anyway
                    h => true,
                };

                foreach (var condition in conditions)
                {
                    var info = lasts.Where(condition).ToList();
                    if (info.Count > 0)
                    {
                        last = info.Last();
                        break;
                    }
                }

                if (use_fifth)
                {
                    if (GuildManager.Instance.SemanSkillToggles[5].isOn)
                    {
                        int trigger = t_history.Count(h => h.waitUnit == last.waitUnit && h.skillId == last.skillId && h.action == Action.WAIT_UNIT_EVENT) - 1;
                        if (trigger >= 100 || lastDelay >= 10)
                        {
                            // UnityEngine.Debug.LogError($"{unitId} trigger or delay too large: {trigger} {lastDelay}, using {delay} instead, maybe wrong!");
                            return delay;
                        }
                        return 100000 + last.waitUnit * 10000 + last.skillId * 1000 + trigger * 10 + lastDelay;
                    }
                    else if (GuildManager.Instance.SemanSkillToggles[6].isOn)
                    {
                        int no_ub_trigger = t_history.Where(h => h.waitUnit == last.waitUnit && h.action == Action.WAIT_UNIT_EVENT && h.skillId != (int)eSemanEvent.UB).Select(x => x.frame).ToHashSet().Count();
                        int ub_trigger = t_history.Count(h => h.waitUnit == last.waitUnit && h.action == Action.WAIT_UNIT_EVENT && h.skillId == (int)eSemanEvent.UB);
                        int trigger = no_ub_trigger + ub_trigger - 1;

                        if (trigger >= 100 || lastDelay >= 100)
                        {
                            // UnityEngine.Debug.LogError($"{unitId} trigger or delay too large: {trigger} {lastDelay}, using {delay} instead, maybe wrong!");
                            return delay;
                        }
                        return 200000 + last.waitUnit * 10000 + trigger * 100 + lastDelay;
                    }
                    else if (GuildManager.Instance.SemanSkillToggles[7].isOn)
                    {
                        int no_ub_trigger = t_history.Where(h => h.action == Action.WAIT_UNIT_EVENT && h.skillId != (int)eSemanEvent.UB).Select(x => x.frame).ToHashSet().Count();
                        int ub_trigger = t_history.Count(h => h.action == Action.WAIT_UNIT_EVENT && h.skillId == (int)eSemanEvent.UB);
                        int trigger = no_ub_trigger + ub_trigger - 1;

                        if (trigger >= 1000 || lastDelay >= 100)
                        {
                            // UnityEngine.Debug.LogError($"{unitId} trigger or delay too large: {trigger} {lastDelay}, using {delay} instead, maybe wrong!");
                            return delay;
                        }
                        return 300000 + trigger * 100 + lastDelay;
                    }
                    else
                    {
                        return delay;
                    }
                }
                else
                {
                    if (last.skillId == 9 || // auto
                        last.skillId == 1 && GuildManager.Instance.SemanUBToggles[0].isOn ||
                        last.skillId != 1 && GuildManager.Instance.SemanSkillToggles[1].isOn)
                    {
                        int trigger = t_history.Count(h => h.waitUnit == last.waitUnit && h.skillId == last.skillId) - 1;
                        if (trigger >= 10 || lastDelay >= 10)
                        {
                            // UnityEngine.Debug.LogError($"{unitId} trigger or delay too large: {trigger} {lastDelay}, using {delay} instead, maybe wrong!");
                            return delay;
                        }
                        return 10000 + last.waitUnit * 1000 + last.skillId * 100 + trigger * 10 + lastDelay;
                    }
                    else if (last.skillId == 1 && GuildManager.Instance.SemanUBToggles[1].isOn)
                    {
                        int trigger = t_history.Where(h => h.skillId == last.skillId).Select(x => x.frame).ToHashSet().Count() - 1;
                        if (trigger >= 100 || lastDelay >= 100)
                        {
                            // UnityEngine.Debug.LogError($"{unitId} trigger or delay too large: {trigger} {lastDelay}, using {delay} instead, maybe wrong!");
                            return delay;
                        }
                        return 40000 + trigger * 100 + lastDelay;
                    }
                    else // skill
                    {
                        if (GuildManager.Instance.SemanSkillToggles[0].isOn)
                        {
                            return delay;
                        }
                        else if (GuildManager.Instance.SemanSkillToggles[2].isOn)
                        {
                            int trigger = t_history.Count(h => h.waitUnit == last.waitUnit && h.skillId != 1 && h.skillId != 9 && h.action == Action.WAIT_UNIT_ACTION) - 1;
                            if (trigger >= 100 || lastDelay >= 10)
                            {
                                // UnityEngine.Debug.LogError($"{unitId} trigger or delay too large: {trigger} {lastDelay}, using {delay} instead, maybe wrong!");
                                return delay;
                            }
                            return 20000 + last.waitUnit * 1000 + trigger * 10 + lastDelay;
                        }
                        else if (GuildManager.Instance.SemanSkillToggles[3].isOn)
                        {
                            int trigger = history.Where(h => h.skillId != 1 && h.skillId != 9 && h.action == Action.WAIT_UNIT_ACTION).Select(x => x.frame).ToHashSet().Count() - 1;
                            if (trigger >= 100 || lastDelay >= 100)
                            {
                                // UnityEngine.Debug.LogError($"{unitId} trigger or delay too large: {trigger} {lastDelay}, using {delay} instead, maybe wrong!");
                                return delay;
                            }
                            return 30000 + trigger * 100 + lastDelay;
                        }
                        else if (GuildManager.Instance.SemanSkillToggles[4].isOn)
                        {
                            if (IsIdle)
                            {
                                return delay;
                            }
                            return 50000 + no_idle_delay;
                        }
                        else
                        {
                            return delay;
                        }
                    }

                }

            }

            public int GetTeacherUbNumber(int unitId, int semanSkillToggleIndex)
            {
                var t_history = history.Where(h => h.action == Action.WAIT_UNIT_EVENT).ToList();

                if (t_history.Count == 0 ||
                (delay == 0 && t_history.Count(h => h.skillId == (int)eSemanEvent.UB) == 0))
                {
                    return delay;
                }

                int lastDelay = t_history.Last().delay;
                var lasts = t_history.Where(h => h.delay == lastDelay).ToList();
                UbStatus last = null;

                var conditions = new List<Func<UbStatus, bool>>
                {
                    h => h.skillId == (int)eSemanEvent.UB,
                    h => h.waitUnit == unitId && h.skillId == (int)eSemanEvent.IDLE,
                    h => h.waitUnit == unitId,
                    h => true,
                };

                foreach (var condition in conditions)
                {
                    var info = lasts.Where(condition).ToList();
                    if (info.Count > 0)
                    {
                        last = info.Last();
                        break;
                    }
                }

                if (last == null)
                {
                    return delay;
                }

                if (semanSkillToggleIndex == 5)
                {
                    int trigger = t_history.Count(h => h.waitUnit == last.waitUnit && h.skillId == last.skillId && h.action == Action.WAIT_UNIT_EVENT) - 1;
                    if (trigger >= 100 || lastDelay >= 10)
                    {
                        return delay;
                    }
                    return 100000 + last.waitUnit * 10000 + last.skillId * 1000 + trigger * 10 + lastDelay;
                }
                else if (semanSkillToggleIndex == 6)
                {
                    int no_ub_trigger = t_history.Where(h => h.waitUnit == last.waitUnit && h.action == Action.WAIT_UNIT_EVENT && h.skillId != (int)eSemanEvent.UB).Select(x => x.frame).ToHashSet().Count();
                    int ub_trigger = t_history.Count(h => h.waitUnit == last.waitUnit && h.action == Action.WAIT_UNIT_EVENT && h.skillId == (int)eSemanEvent.UB);
                    int trigger = no_ub_trigger + ub_trigger - 1;

                    if (trigger >= 100 || lastDelay >= 100)
                    {
                        return delay;
                    }
                    return 200000 + last.waitUnit * 10000 + trigger * 100 + lastDelay;
                }
                else if (semanSkillToggleIndex == 7)
                {
                    int no_ub_trigger = t_history.Where(h => h.action == Action.WAIT_UNIT_EVENT && h.skillId != (int)eSemanEvent.UB).Select(x => x.frame).ToHashSet().Count();
                    int ub_trigger = t_history.Count(h => h.action == Action.WAIT_UNIT_EVENT && h.skillId == (int)eSemanEvent.UB);
                    int trigger = no_ub_trigger + ub_trigger - 1;

                    if (trigger >= 1000 || lastDelay >= 100)
                    {
                        return delay;
                    }
                    return 300000 + trigger * 100 + lastDelay;
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
                if (canProcess && (action == Action.COUNTDOWN || action == Action.COUNTDOWN_EXCLUDE_IDLE && !IsIdle))
                    delay--;
            }

            public bool IsUbExec()
            {
                return canProcess && (action == Action.COUNTDOWN || action == Action.COUNTDOWN_EXCLUDE_IDLE && !IsIdle) && delay <= 0;
            }

            public void OnChangeStateCallBack(int unitPos, ActionState state)
            {
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

        public UbStatus GetUbStatus(int unitId, int num) // xxxxx
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

        private Queue<UbStatus>[] queues;
        public List<int> UBNums { get; private set; }

        private List<UbRecord> auto2seman;
        private List<List<int>> auto2semanAll;
        private List<int> auto2semanSequence;
        private Dictionary<int, List<List<int>>> auto2semanTeacherProfiles;
        private static readonly int[] AutoSemanTeacherProfileIndexes = { 5, 6, 7 };
        private bool enable = false;
        private bool auto2semanSequenceMode = false;
        private List<int> ubSequence = new List<int>();
        private int ubSequenceIndex = 0;

        public void Disable()
        {
            enable = false;
            auto2semanSequenceMode = false;
            auto2semanSequence = null;
            ubSequence = new List<int>();
            ubSequenceIndex = 0;
        }

        public void SetUbExec(List<List<int>> ublists, List<int> sequence = null, bool recordSequence = false)
        {
            enable = true;
            ubMod = false;
            auto2semanSequenceMode = recordSequence;
            ubSequence = NormalizeSequence(sequence);
            ubSequenceIndex = 0;
            UBNums = new List<int> { 0, 0, 0, 0, 0 };
            if (ublists == null || ublists.Count == 0)
            {
                auto2seman = new List<UbRecord>() {
                    new UbRecord(this, unitId: 0),
                    new UbRecord(this, unitId: 1),
                    new UbRecord(this, unitId: 2),
                    new UbRecord(this, unitId: 3),
                    new UbRecord(this, unitId: 4),
                };
                auto2semanAll = new List<List<int>>() { new List<int>(), new List<int>(), new List<int>(), new List<int>(), new List<int>() };
                auto2semanSequence = new List<int>();
                auto2semanTeacherProfiles = AutoSemanTeacherProfileIndexes.ToDictionary(
                    profile => profile,
                    profile => new List<List<int>>() { new List<int>(), new List<int>(), new List<int>(), new List<int>(), new List<int>() });
                return;
            }
            ubMod = true;
            auto2semanSequence = null;
            auto2semanTeacherProfiles = null;
            queues = ublists.Select((ublist, id) => new Queue<UbStatus>(ublist.Select(num => GetUbStatus(id, num)))).ToArray();
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
            if (!enable) return false;
            if (!ubMod) return false;
            if (pos < 0 || pos > 4) return false;
            if (!IsSequenceTurn(pos)) return false;
            if (queues[pos].Count == 0 || queues[pos].Peek().IsUbExec())
            {
                return true;
            }
            return false;
        }

        // public void OnTpChangeCallBack(int pos, float energy)
        // {
        //     if (!ubMod)
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
            if (!enable) return;
            if (!ubMod)
            {
                foreach (var ub in auto2seman)
                {
                    ub.OnEventCallback(unitPos, (int)eventId);
                }
                return;
            }
            foreach (var queue in queues)
            {
                if (queue.Count > 0)
                {
                    queue.Peek().OnEventCallback(unitPos, (int)eventId);
                }
            }
        }

        public void OnSkillEffectCallBack(int unitPos, int skill)
        {
            if (!enable) return;
            if (skill == 1) // ignore ub
                return;
            if (!ubMod)
            {
                foreach (var ub in auto2seman)
                {
                    ub.OnSkillCallBack(unitPos, skill);
                }
                return;
            }
            foreach (var queue in queues)
            {
                if (queue.Count > 0)
                {
                    queue.Peek().OnSkillCallBack(unitPos, skill);
                }
            }
        }

        public void OnChangeStateCallBack(int unitPos, ActionState state)
        {
            if (!enable) return;
            if (!ubMod)
            {
                foreach (var ub in auto2seman)
                {
                    ub.OnChangeStateCallBack(unitPos, state);
                }
                return;
            }
            foreach (var queue in queues)
            {
                if (queue.Count > 0)
                {
                    queue.Peek().OnChangeStateCallBack(unitPos, state);
                }
            }
        }

        public void ProcessFrame()
        {
            if (!enable) return;
            if (!ubMod)
            {
                for(int i = 0; i < auto2seman.Count; i++)
                {
                    auto2seman[i].ProcessFrame();
                }
                return;
            }
            for(int i = 0; i < queues.Length; i++)
            {
                if (queues[i].Count > 0)
                {
                    queues[i].Peek().ProcessFrame();
                }
            }
        }

        public void UbExecCallback(int pos)
        {
            if (!enable) return;
            if (!ubMod)
            {
                if (pos >= 0 && pos <= 4) // no boss
                {
                    auto2semanAll[pos].Add(auto2seman[pos].GetUbNumber(pos));
                    if (auto2semanSequenceMode)
                    {
                        auto2semanSequence.Add(pos);
                    }
                    foreach (int profile in AutoSemanTeacherProfileIndexes)
                    {
                        if (auto2semanTeacherProfiles != null && auto2semanTeacherProfiles.ContainsKey(profile))
                        {
                            auto2semanTeacherProfiles[profile][pos].Add(auto2seman[pos].GetTeacherUbNumber(pos, profile));
                        }
                    }
                    UBNums[pos]++;
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
                return;
            }
            if (pos < 0 || pos > 4) return;
            if (queues[pos].Count > 0)
                queues[pos].Dequeue();
            UBNums[pos]++;
            if (ubSequence != null && ubSequenceIndex < ubSequence.Count && ubSequence[ubSequenceIndex] == pos)
            {
                ubSequenceIndex++;
            }
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
            if (!enable) return;
            this.players = players;
            for (int i = 0; i < players.Count; i++)
            {
                int localIndex = i;
                // players[localIndex].SemanOnTPChanged += (unitId, energy) => OnTpChangeCallBack(localIndex, energy);
                players[localIndex].SemanOnStartAction += (unitId, skill) => OnSkillEffectCallBack(localIndex, skill2Id(skill));
                players[localIndex].SemanOnExecAction += (unitId, skill) => OnSkillEffectCallBack(localIndex, skill2Id(skill));
                players[localIndex].SemanOnChangeState += (unitId, state) => OnChangeStateCallBack(localIndex, state);
                players[localIndex].SemanOnEventCallback += (unitId, buffDebuff) => OnEventCallBack(localIndex, buffDebuff);
            }
            boss.SemanOnStartAction += (unitId, skill) => OnSkillEffectCallBack(5, skill2Id(skill));
            boss.SemanOnExecAction += (unitId, skill) => OnSkillEffectCallBack(5, skill2Id(skill));
            boss.SemanOnChangeState += (unitId, state) => OnChangeStateCallBack(5, state);
            boss.SemanOnEventCallback += (unitId, buffDebuff) => OnEventCallBack(5, buffDebuff);
        }

        public List<List<int>> Auto2Seman()
        {
            return BuildAuto2SemanOutput(auto2semanAll);
        }

        public List<int> Auto2SemanSequence()
        {
            return auto2semanSequence?.ToList() ?? new List<int>();
        }

        public List<List<int>> Auto2Seman(int semanSkillToggleIndex)
        {
            if (auto2semanTeacherProfiles != null && auto2semanTeacherProfiles.ContainsKey(semanSkillToggleIndex))
            {
                return BuildAuto2SemanOutput(auto2semanTeacherProfiles[semanSkillToggleIndex]);
            }
            return BuildAuto2SemanOutput(null);
        }

        private List<List<int>> BuildAuto2SemanOutput(List<List<int>> source)
        {
            var ret = new List<List<int>>();
            var lanes = source ?? new List<List<int>>() { new List<int>(), new List<int>(), new List<int>(), new List<int>(), new List<int>() };
            foreach (var seman in lanes)
            {
                var ub = new List<int>(seman)
                {
                    -1
                };
                ret.Add(ub);
            }
            return ret;
        }

    }
}
