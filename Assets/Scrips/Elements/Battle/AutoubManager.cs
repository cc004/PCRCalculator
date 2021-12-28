using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Elements.Battle
{
    public class AutoubManager
    {
        private bool enabled;
        private int count;

        private class UbStatus
        {
            public UbStatus depending;
            public bool pressed;
            public int frame;
            public float origin;
        }

        private Queue<UbStatus>[] queues;
        
        public void SetUbExec(List<List<float>> ublists, int count)
        {
            if (ublists == null)
            {
                enabled = false;
                return;
            }

            this.enabled = true;
            this.count = count;
            queues = ublists.SelectMany((l, i) => l.Select(t => (time: t, pos: i)))
                .GroupBy(g => (int) (g.time)).SelectMany(g => g.OrderBy(t => t.time).Aggregate(new List<(UbStatus sta, int pos)>(),
                    (result, tuple) =>
                    {
                        var last = result.LastOrDefault(r => r.sta.origin < tuple.time);
                        result.Add((new UbStatus
                        {
                            depending = last.sta,
                            frame = g.Key,
                            origin = tuple.time
                        }, tuple.pos));
                        return result;
                    })).GroupBy(t => t.pos).OrderBy(g => g.Key)
                .Select(g => (g.Key, new Queue<UbStatus>(g.Select(t => t.sta).OrderBy(t => t.frame)))).Aggregate(new Queue<UbStatus>[]
                {
                    new Queue<UbStatus>(), new Queue<UbStatus>(), new Queue<UbStatus>(), new Queue<UbStatus>(), new Queue<UbStatus>()
                }, (arr, tuple) =>
                {
                    arr[tuple.Key] = tuple.Item2;
                    return arr;
                });
        }

        public bool IsUbExec(int pos)
        {
            if (!enabled) return false;
            if (pos < 0 || pos > 4) return false;
            if (queues[pos].Count > 0)
            {
                UbStatus next = null;
                var cnt = BattleHeaderController.CurrentFrameCount;
                while (queues[pos].Count > 0)
                {
                    next = queues[pos].Peek();
                    if (next.frame + count < cnt)
                    {
                        next = null;
                        queues[pos].Dequeue();
                        continue;
                    }

                    break;
                }

                if (next == null) return false;

                if (next.depending?.pressed ?? true)
                {
                    if (next.frame <= cnt)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public void UbExecCallback(int pos)
        {
            if (!enabled) return;
            if (pos < 0 || pos > 4) return;
            if (queues[pos].Count > 0)
                queues[pos].Dequeue().pressed = true;
        }
    }
}