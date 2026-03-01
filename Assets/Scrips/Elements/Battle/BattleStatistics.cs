using System.Collections.Generic;
using System.Linq;
using PCRCaculator.Guild;

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
        public Dictionary<int, long> PlayerDamageDic { get; set; } = new Dictionary<int, long>();
        
        /// 物理伤害期望
        public double ScorePhysicsExpectation { get; private set; }

        /// 魔法伤害期望
        public double ScoreMagicExpectation { get; private set; }

        // 修改参数类型为 double
        public void AccumulateFrameStats(double phyScore, double magScore)
        {
            if (!IsEnabled) return;
            ScorePhysicsExpectation += phyScore;
            ScoreMagicExpectation += magScore;
        }

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

        public void AddPlayerDamage(int unitId, long damage)
        {
            if (!IsEnabled) return;

            if (!PlayerDamageDic.ContainsKey(unitId))
            {
                PlayerDamageDic.Add(unitId, 0);
            }
            PlayerDamageDic[unitId] += damage;
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
            PlayerTpGainedDic.Clear();
        }

        public BattleStatsData ToBattleStatsData()
        {
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
                PlayerDamageList = MyGameCtrl.Instance.tempData.playerList.Select(p => PlayerDamageDic.ContainsKey(p.unitId) ? PlayerDamageDic[p.unitId] : 0).ToList()
            };
        }
    }
}
