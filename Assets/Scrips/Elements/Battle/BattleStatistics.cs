using System.Collections.Generic;

namespace Elements.Battle
{
    public class BattleStatistics
    {
        public bool IsEnabled { get; set; } = true;

        public long TotalMagicBuff { get; private set; }
        public long TotalPhysicsBuff { get; private set; }
        public long TotalMagicDebuff { get; private set; }
        public long TotalPhysicsDebuff { get; private set; }
        public Dictionary<int, long> PlayerTpGainedDic { get; private set; } = new Dictionary<int, long>();

        public void AddMagicBuff(long value) { if (IsEnabled) TotalMagicBuff += value; }
        public void AddPhysicsBuff(long value) { if (IsEnabled) TotalPhysicsBuff += value; }
        public void AddMagicDebuff(long value) { if (IsEnabled) TotalMagicDebuff += value; }
        public void AddPhysicsDebuff(long value) { if (IsEnabled) TotalPhysicsDebuff += value; }

        public void AddPlayerTp(int unitId, long value)
        {
            if (!IsEnabled) return;

            if (!PlayerTpGainedDic.ContainsKey(unitId))
            {
                PlayerTpGainedDic.Add(unitId, 0);
            }
            PlayerTpGainedDic[unitId] += value;
        }

        public void Reset()
        {
            TotalMagicBuff = 0;
            TotalPhysicsBuff = 0;
            TotalMagicDebuff = 0;
            TotalPhysicsDebuff = 0;
            PlayerTpGainedDic.Clear();
        }
    }
}
