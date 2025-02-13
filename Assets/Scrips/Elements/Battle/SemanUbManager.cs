using System;
using System.Collections.Generic;
using System.Linq;
using static Elements.UnitCtrl;

namespace Elements.Battle
{
    public class SemanUbManager
    {
        static private List<UnitCtrl> players;

        static private Dictionary<ActionState, int> State2Id = new Dictionary<ActionState, int>
        {
            // { ActionState.ATK, 0 },
            { ActionState.SKILL_1, 1 },
            { ActionState.IDLE, 9 },
        };
        private bool ubMod;
        public enum Action
        {
            COUNTDOWN = 0,
            WAIT_TP_FULL = 1,
            WAIT_UNIT_ACTION = 2,
            NO_UB = 4,
        }

        public class UbRecord
        {
            public bool skillReady { 
                get
                {
                    return unitId < SemanUbManager.players.Count && SemanUbManager.players[unitId].IsSkillReady;
                }
            }
            public int delay = 0;
            public int unitId = 0;
            public List<UbStatus> history = new List<UbStatus>();

            public UbRecord(int unitId)
            {
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
            public void OnChangeStateCallBack(int unitPos, ActionState state)
            {
                if (!skillReady) return;
                if (SemanUbManager.State2Id.ContainsKey(state))
                {
                    var stateId = SemanUbManager.State2Id[state];
                    var frame = BattleHeaderController.CurrentFrameCount;
                    if (history.Count == 0 || history.Last().frame < frame || history.Last().waitUnit != unitPos || history.Last().skillId != stateId)
                    {
                        history.Add(new UbStatus
                        {
                            waitUnit = unitPos,
                            skillId = stateId,
                            frame = frame
                        });
                    }
                }
            }
            
            public void OnSkillCallBack(int unitPos, int skillId)
            {
                if (!skillReady) return;
                int frame = BattleHeaderController.CurrentFrameCount;
                if (history.Count == 0 || history.Last().frame < frame || history.Last().waitUnit != unitPos || history.Last().skillId != skillId)
                {
                    history.Add(new UbStatus
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
                if (skillReady)
                {
                    foreach (var ub in history)
                    {
                        ub.delay++;
                    }
                    delay++;
                }
            }

            public int GetUbNumber(int unitId)
            {
                int frame = BattleHeaderController.CurrentFrameCount;
                if (history.Count == 0 || 
                (delay == 0 && history.Count(h => h.skillId == 1) == 0)) // set and not after other ub
                {
                    return delay;
                }
                // if (history.Last().delay > 0)
                // {
                //     var zibun = history.Where(h => h.waitUnit == unitId).ToList();
                //     UbStatus lastone = null;
                //     if (zibun.Count > 0)
                //     {
                //         lastone = zibun.Last();
                //     }
                //     else
                //     {
                //         lastone = history.Last();
                //     }
                //     int triggers = history.Count(h => h.waitUnit == lastone.waitUnit && h.skillId == lastone.skillId) - 1;
                //     int delays = lastone.delay;
                //     if (triggers >= 10 || delays >= 10)
                //     {
                //         UnityEngine.Debug.LogError($"{unitId} trigger or delay too large: {triggers}, {delays}, using {delay} instead, maybe wrong!");
                //         return delay;
                //     }
                //     return 10000 + lastone.waitUnit * 1000 + lastone.skillId * 100 + triggers * 10 + delays;
                // }
                int lastDelay = history.Last().delay;
                var lasts = history.Where(h => h.delay == lastDelay).ToList();
                UbStatus last = null;

                var conditions = new List<Func<UbStatus, bool>>
                {
                    // ub
                    h => h.skillId == 1, 
                    // self auto
                    h => h.waitUnit == unitId && h.skillId == 9,
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

                int trigger = history.Count(h => h.waitUnit == last.waitUnit && h.skillId == last.skillId) - 1;
                if (trigger >= 10 || lastDelay >= 10)
                {
                    UnityEngine.Debug.LogError($"{unitId} trigger or delay too large: {trigger} {lastDelay}, using {delay} instead, maybe wrong!");
                    return delay;
                }
                return 10000 + last.waitUnit * 1000 + last.skillId * 100 + trigger * 10 + lastDelay;
            }

            public void Clear()
            {
                delay = 0;
                history.Clear();
            }

        }

        public class UbStatus
        {
            public Action action { get; set; }
            public bool skillReady { 
                get
                {
                    return unitId < SemanUbManager.players.Count && SemanUbManager.players[unitId].IsSkillReady;
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
            public void OnSkillCallBack(int unitPos, int skillId)
            {
                if (!skillReady) return;
                if (action == Action.WAIT_UNIT_ACTION && waitUnit == unitPos && this.skillId == skillId && lastUpdateFrame < BattleHeaderController.CurrentFrameCount)
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
                if (skillReady && action == Action.COUNTDOWN)
                    delay--;
            }

            public bool IsUbExec()
            {
                return skillReady && action == Action.COUNTDOWN && delay <= 0;
            }

            public void OnChangeStateCallBack(int unitPos, ActionState state)
            {
                if (!skillReady) return;
                if (action == Action.WAIT_UNIT_ACTION && waitUnit == unitPos && State2Id.ContainsKey(state) && skillId == State2Id[state])
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
            if (op == 0)
            {
                return new UbStatus
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
                return new UbStatus
                {
                    action = Action.WAIT_UNIT_ACTION,
                    unitId = unitId,
                    waitUnit = unitPos,
                    skillId = skillId,
                    trigger = trigger,
                    delay = delay
                };
            }
            else // >= 20000
            {
                return new UbStatus
                {
                    action = Action.NO_UB,
                    unitId = unitId,
                    delay = 0
                };
            }
        }

        private Queue<UbStatus>[] queues;
        public List<int> UBNums { get; private set; }

        private List<UbRecord> auto2seman;
        private List<List<int>> auto2semanAll;
        private bool enable = false;

        public void Disable()
        {
            enable = false;
        }

        public void SetUbExec(List<List<int>> ublists)
        {
            enable = true;
            ubMod = false;
            if (ublists == null || ublists.Count == 0)
            {
                auto2seman = new List<UbRecord>() {
                    new UbRecord(unitId: 0),
                    new UbRecord(unitId: 1),
                    new UbRecord(unitId: 2),
                    new UbRecord(unitId: 3),
                    new UbRecord(unitId: 4),
                };
                auto2semanAll = new List<List<int>>() { new List<int>(), new List<int>(), new List<int>(), new List<int>(), new List<int>() };
                return;
            }
            ubMod = true;
            UBNums = new List<int> { 0, 0, 0, 0, 0 };
            queues = ublists.Select((ublist, id) => new Queue<UbStatus>(ublist.Select(num => GetUbStatus(id, num)))).ToArray();
        }

        public bool IsUbExec(int pos)
        {
            if (!enable) return false;
            if (!ubMod) return false;
            if (pos < 0 || pos > 4) return false;
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
                    auto2seman[pos].Clear();
                }
                return;
            }
            if (pos < 0 || pos > 4) return;
            if (queues[pos].Count > 0)
                queues[pos].Dequeue();
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
            if (!enable) return;
            SemanUbManager.players = players;
            for (int i = 0; i < players.Count; i++)
            {
                int localIndex = i;
                // players[localIndex].SemanOnTPChanged += (unitId, energy) => OnTpChangeCallBack(localIndex, energy);
                players[localIndex].SemanOnStartAction += (unitId, skill) => OnSkillEffectCallBack(localIndex, skill2Id(skill));
                players[localIndex].SemanOnExecAction += (unitId, skill) => OnSkillEffectCallBack(localIndex, skill2Id(skill));
                players[localIndex].SemanOnChangeState += (unitId, state) => OnChangeStateCallBack(localIndex, state);
            }
            boss.SemanOnStartAction += (unitId, skill) => OnSkillEffectCallBack(5, skill2Id(skill));
            boss.SemanOnExecAction += (unitId, skill) => OnSkillEffectCallBack(5, skill2Id(skill));
            boss.SemanOnChangeState += (unitId, state) => OnChangeStateCallBack(5, state);
        }

        public List<List<int>> Auto2Seman()
        {
            var ret = new List<List<int>>();
            foreach (var seman in auto2semanAll)
            {
                var ub = new List<int>(seman)
                {
                    30000
                };
                ret.Add(ub);
            }
            return ret;
        }

    }
}