using System.Collections.Generic;
using System.Linq;

namespace Elements.Battle
{
    public class AutoubManager
    {
        private const int PlayerLaneCount = 5;
        private const int AutomaticLaneIndex = PlayerLaneCount;
        private const int LaneCount = PlayerLaneCount + 1;
        private const float SameFrameOrderStep = 0.001f;

        private bool enabled;
        private int count;
        private List<List<float>> recordedUbTime = CreateEmptyLanes();
        private int recordedFrame = -1;
        private int recordedSameFrameOrdinal;

        private class UbStatus
        {
            public UbStatus depending;
            public bool pressed;
            public int frame;
            public int lane;
            public float origin;
        }

        private Queue<UbStatus>[] queues;

        public void SetUbExec(List<List<float>> ublists, int count)
        {
            enabled = false;
            queues = null;
            ResetRecording();
            if (ublists == null || ublists.Count == 0)
            {
                return;
            }

            ublists = NormalizeLanes(ublists);
            enabled = true;
            this.count = count;
            var statuses = ublists.SelectMany((lane, laneIndex) =>
                    lane.Select(time => (time, lane: laneIndex)))
                .GroupBy(entry => GetFrame(entry.time))
                .SelectMany(group => group.OrderBy(entry => entry.time).ThenBy(entry => entry.lane)
                    .Aggregate(new List<UbStatus>(),
                    (result, tuple) =>
                    {
                        var last = result.LastOrDefault(status => status.origin < tuple.time);
                        result.Add(new UbStatus
                        {
                            depending = last,
                            frame = group.Key,
                            lane = tuple.lane,
                            origin = tuple.time,
                        });
                        return result;
                    }))
                .ToList();
            queues = statuses.GroupBy(status => status.lane).OrderBy(group => group.Key)
                .Select(group => (group.Key, new Queue<UbStatus>(group.OrderBy(status => status.origin))))
                .Aggregate(Enumerable.Range(0, LaneCount)
                    .Select(_ => new Queue<UbStatus>()).ToArray(), (result, tuple) =>
                {
                    result[tuple.Key] = tuple.Item2;
                    return result;
                });
        }

        public bool IsUbExec(int pos)
        {
            if (!enabled) return false;
            if (pos < 0 || pos >= PlayerLaneCount) return false;
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
            UbExecCallback(null, pos);
        }

        public void UbExecCallback(UnitCtrl unit, int pos)
        {
            RecordUbResult(unit, pos);
            if (!enabled) return;

            int lane = ResolveLane(unit, pos);
            if (lane >= 0 && queues[lane].Count > 0)
            {
                queues[lane].Dequeue().pressed = true;
            }
        }

        public List<List<float>> GetRecordedUbTime()
        {
            return recordedUbTime.Select(lane => lane.ToList()).ToList();
        }

        private void ResetRecording()
        {
            recordedUbTime = CreateEmptyLanes();
            recordedFrame = -1;
            recordedSameFrameOrdinal = 0;
        }

        private void RecordUbResult(UnitCtrl unit, int pos)
        {
            int lane = ResolveLane(unit, pos);
            if (lane < 0) return;

            int frame = BattleHeaderController.CurrentFrameCount;
            if (frame == recordedFrame)
            {
                recordedSameFrameOrdinal++;
            }
            else
            {
                recordedFrame = frame;
                recordedSameFrameOrdinal = 0;
            }
            recordedUbTime[lane].Add(Encode(frame, recordedSameFrameOrdinal));
        }

        private static int ResolveLane(UnitCtrl unit, int unitListPosition)
        {
            if (unit != null)
            {
                if (unit.IsOther || unit.IsSummonOrPhantom)
                {
                    return AutomaticLaneIndex;
                }
                return ResolvePlayerLane(unitListPosition);
            }

            return unitListPosition == -1
                ? AutomaticLaneIndex
                : ResolvePlayerLane(unitListPosition);
        }

        private static int ResolvePlayerLane(int unitListPosition)
        {
            return unitListPosition >= 0 && unitListPosition < PlayerLaneCount
                ? unitListPosition
                : -1;
        }

        private static int GetFrame(float encodedTime)
        {
            return (int)encodedTime;
        }

        private static float Encode(int frame, int sameFrameOrdinal)
        {
            return frame + sameFrameOrdinal * SameFrameOrderStep;
        }

        private static List<List<float>> CreateEmptyLanes()
        {
            return Enumerable.Range(0, LaneCount)
                .Select(_ => new List<float>())
                .ToList();
        }

        private static List<List<float>> NormalizeLanes(IReadOnlyList<List<float>> lanes)
        {
            var result = CreateEmptyLanes();
            for (int sourceIndex = 0; sourceIndex < lanes.Count; sourceIndex++)
            {
                var source = lanes[sourceIndex];
                if (source == null || source.Count == 0)
                {
                    continue;
                }

                int targetIndex = sourceIndex < PlayerLaneCount
                    ? sourceIndex
                    : AutomaticLaneIndex;
                result[targetIndex].AddRange(source);
            }

            foreach (var lane in result)
            {
                lane.Sort();
            }
            return result;
        }
    }
}
