using System;
using System.Collections.Generic;
using System.Linq;
using PCRCaculator;
using PCRCaculator.SQL;

namespace Elements
{
    public class ConnectRankBonusParameter
    {
        public int MaxBonusLevel { get; set; }
        public long PrimaryValue { get; set; }
        public long SecondaryValue { get; set; }
    }

    public class ConnectRankTempData
    {
        public static readonly ConnectRankTempData Instance = new ConnectRankTempData();

        private readonly Dictionary<(int rank, int attackType, int roleId), IReadOnlyDictionary<eParamType, long>> statusCache = new();

        public void ClearCache()
        {
            statusCache.Clear();
        }

        public IReadOnlyDictionary<eParamType, long> CreateRankStatusDictionary(int roleId, int rank, int attackType)
        {
            rank = Math.Max(0, rank);
            var key = (rank, attackType, roleId);
            if (statusCache.TryGetValue(key, out var cached))
                return cached;

            if (rank == 0)
            {
                var zeroDictionary = CreateRankStatusDictionary(roleId, 1, attackType)
                    .ToDictionary(pair => pair.Key, pair => 0L);
                statusCache[key] = zeroDictionary;
                return zeroDictionary;
            }

            var result = new Dictionary<eParamType, long>();
            var manager = MainManager.Instance;
            if (manager == null ||
                manager.ConnectRankStatusDic == null ||
                !manager.ConnectRankStatusDic.TryGetValue(roleId, out var statusList))
            {
                statusCache[key] = result;
                return result;
            }

            foreach (var status in statusList.OrderBy(x => x.rank_from))
            {
                if (status.rank_from > rank)
                    break;

                int endRank = Math.Min(rank, status.rank_to);
                int effectCount = endRank - status.rank_from + 1;
                if (effectCount <= 0)
                    continue;

                foreach (var parameter in status.Parameters)
                {
                    if (!TryConvertParamType(parameter.paramType, attackType, out var paramType))
                        continue;

                    long value = (long)effectCount * parameter.value;
                    if (result.TryGetValue(paramType, out var current))
                        result[paramType] = current + value;
                    else
                        result[paramType] = value;
                }
            }

            statusCache[key] = result;
            return result;
        }

        public IReadOnlyDictionary<eParamType, (long before, long after)> CreateRankStatusDiffDictionary(
            int roleId,
            int fromRank,
            int toRank,
            int attackType)
        {
            var before = CreateRankStatusDictionary(roleId, fromRank, attackType);
            var after = CreateRankStatusDictionary(roleId, toRank, attackType);
            return before.Keys
                .Union(after.Keys)
                .Select(paramType =>
                {
                    before.TryGetValue(paramType, out var beforeValue);
                    after.TryGetValue(paramType, out var afterValue);
                    return (paramType, beforeValue, afterValue);
                })
                .Where(x => x.afterValue > x.beforeValue)
                .ToDictionary(x => x.paramType, x => (x.beforeValue, x.afterValue));
        }

        public int GetConnectRankBonusLevel(int connectRank)
        {
            if (connectRank <= 0)
                return 0;

            var chart = MainManager.Instance?.ConnectRankChartDic;
            return chart != null && chart.TryGetValue(connectRank, out var row) ? row.bonus_level : 0;
        }

        public IReadOnlyDictionary<int, ConnectRankBonusParameter> CreateRankBonusDictionary(int targetBonusLevel)
        {
            var result = new Dictionary<int, ConnectRankBonusParameter>();
            var bonusList = MainManager.Instance?.ConnectRankBonusList;
            if (targetBonusLevel <= 0 || bonusList == null)
                return result;

            foreach (var bonus in bonusList.Where(x => x.bonus_level <= targetBonusLevel).OrderBy(x => x.bonus_level))
            {
                if (!result.TryGetValue(bonus.bonus_type, out var parameter))
                {
                    parameter = new ConnectRankBonusParameter();
                    result[bonus.bonus_type] = parameter;
                }

                parameter.MaxBonusLevel = Math.Max(parameter.MaxBonusLevel, bonus.bonus_level);
                parameter.PrimaryValue += bonus.value_1;
                parameter.SecondaryValue += bonus.value_2;
            }

            return result;
        }

        public IReadOnlyDictionary<int, ConnectRankBonusParameter> CreateRankBonusDictionaryForUnit(
            int unitId,
            int targetBonusLevel)
        {
            var bonusDictionary = CreateRankBonusDictionary(targetBonusLevel);
            if (IsExUniqueEquipment1Released(unitId))
                return bonusDictionary;

            return bonusDictionary
                .Where(pair => pair.Key != 4)
                .ToDictionary(pair => pair.Key, pair => pair.Value);
        }

        public int GetDefaultConnectRankLimit()
        {
            var limitCommon = MainManager.Instance?.ConnectRankLimitCommonDic;
            if (limitCommon != null && limitCommon.Count > 0)
                return limitCommon.Keys.Max();

            var chart = MainManager.Instance?.ConnectRankChartDic;
            return chart != null && chart.Count > 0 ? chart.Keys.Max() : 0;
        }

        public IReadOnlyDictionary<eParamType, long> CreateScaledRankStatusDictionary(
            int roleId,
            int rank,
            int attackType,
            int systemId)
        {
            var statusDictionary = CreateRankStatusDictionary(roleId, rank, attackType);
            var coefficient = 100;
            var boostDic = MainManager.Instance?.ConnectRankBoostDic;
            if (boostDic != null && boostDic.TryGetValue(systemId, out var boost))
                coefficient += boost.coefficient;

            if (coefficient == 100)
                return statusDictionary;

            return statusDictionary.ToDictionary(pair => pair.Key, pair => pair.Value * coefficient / 100);
        }

        private static bool TryConvertParamType(int connectRankParamType, int attackType, out eParamType paramType)
        {
            switch (connectRankParamType)
            {
                case 1:
                    paramType = eParamType.HP;
                    return true;
                case 2:
                    paramType = attackType == 2 ? eParamType.MAGIC_ATK : eParamType.ATK;
                    return true;
                case 3:
                    paramType = eParamType.DEF;
                    return true;
                case 4:
                    paramType = eParamType.MAGIC_DEF;
                    return true;
                case 5:
                    paramType = eParamType.ADDITIONAL_WHOLE_DAMAGE_UP_PERMYRIAD;
                    return true;
                case 6:
                    paramType = eParamType.ADDITIONAL_WHOLE_DAMAGE_CUT_PERMYRIAD;
                    return true;
                default:
                    paramType = eParamType.NONE;
                    return false;
            }
        }

        private static bool IsExUniqueEquipment1Released(int unitId)
        {
            var unitExEquips = MainManager.Instance?.UnitExEquips;
            if (unitExEquips == null || !unitExEquips.TryGetValue(unitId, out var slots))
                return false;

            return slots != null &&
                   slots.Length > 0 &&
                   slots[0] != null &&
                   slots[0].Count > 0;
        }
    }
}
