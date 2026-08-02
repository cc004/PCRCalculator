using System;
using System.Collections.Generic;
using System.Linq;
using PCRCaculator;
using PCRCaculator.SQL;

namespace Elements
{
    public class LabyrinthRelicTempData
    {
#if ENABLE_THREADING
        [ThreadStatic]
        private static LabyrinthRelicTempData instance;

        public static LabyrinthRelicTempData Instance => instance ??= new LabyrinthRelicTempData();
#else
        public static readonly LabyrinthRelicTempData Instance = new LabyrinthRelicTempData();
#endif

        private const int PassiveEffectType = 14;
        private const int BattleSkillEffectType = 1;

        public IReadOnlyDictionary<eParamType, long> CreatePassiveParameterDictionary(
            IEnumerable<int> relicIds,
            IEnumerable<PCRCaculator.UnitData> ownUnits,
            int alphaRupee)
        {
            var result = new Dictionary<eParamType, long>(new eParamType_DictComparer());
            var manager = MainManager.Instance;
            if (manager == null || relicIds == null)
                return result;

            var relicDictionary = manager.LabyrinthRelicDic;
            var effectGroupDictionary = manager.LabyrinthRelicEffectGroupDic;
            var effectDictionary = manager.LabyrinthRelicEffectDic;
            if (relicDictionary == null || effectGroupDictionary == null || effectDictionary == null)
                return result;

            var normalizedRelicIds = relicIds.Where(id => id > 0).ToList();
            if (normalizedRelicIds.Count == 0)
                return result;

            var ownUnitList = (ownUnits ?? Enumerable.Empty<PCRCaculator.UnitData>())
                .Where(unit => unit != null)
                .ToList();
            var relicMarkCounts = CreateRelicMarkCounts(normalizedRelicIds, relicDictionary);

            foreach (var relicId in normalizedRelicIds)
            {
                if (!relicDictionary.TryGetValue(relicId, out var relic) ||
                    !effectGroupDictionary.TryGetValue(relic.effect_group_id, out var effectGroup))
                {
                    continue;
                }

                foreach (var effectId in effectGroup.EffectIds)
                {
                    if (!effectDictionary.TryGetValue(effectId, out var effect) ||
                        effect.effect_type != PassiveEffectType ||
                        !TryConvertParamType(effect.effect_value_1, out var paramType))
                    {
                        continue;
                    }

                    long enhanceValue = CalcEnhanceValue(effect, ownUnitList, relicMarkCounts, alphaRupee);
                    if (result.TryGetValue(paramType, out var currentValue))
                        result[paramType] = currentValue + enhanceValue;
                    else
                        result[paramType] = enhanceValue;
                }
            }

            return result;
        }

        public IReadOnlyList<LabyrinthRelicBattleSkillSpec> CreateBattleSkillSpecs(IEnumerable<int> relicIds)
        {
            var result = new List<LabyrinthRelicBattleSkillSpec>();
            var manager = MainManager.Instance;
            if (manager == null || relicIds == null)
                return result;

            var relicDictionary = manager.LabyrinthRelicDic;
            var effectGroupDictionary = manager.LabyrinthRelicEffectGroupDic;
            var effectDictionary = manager.LabyrinthRelicEffectDic;
            var markEffectDictionary = manager.LabyrinthRelicMarkEffectDic;
            var relicSkillExecTimeDictionary = manager.RelicSkillExecTimeDic;
            if (relicDictionary == null ||
                effectGroupDictionary == null ||
                effectDictionary == null ||
                relicSkillExecTimeDictionary == null)
            {
                return result;
            }

            var normalizedRelicIds = relicIds.Where(id => id > 0).ToList();
            if (normalizedRelicIds.Count == 0)
                return result;

            foreach (var relicId in normalizedRelicIds)
            {
                if (!relicDictionary.TryGetValue(relicId, out var relic))
                    continue;

                AddBattleSkillSpecs(
                    result,
                    relic.effect_group_id,
                    effectGroupDictionary,
                    effectDictionary,
                    relicSkillExecTimeDictionary);
            }

            if (markEffectDictionary == null)
                return result;

            var relicMarkCounts = CreateRelicMarkCounts(normalizedRelicIds, relicDictionary);
            foreach (var pair in markEffectDictionary.OrderBy(x => x.Key))
            {
                if (!relicMarkCounts.TryGetValue(pair.Key, out int markCount))
                    continue;

                foreach (var markEffect in pair.Value.OrderBy(x => x.trigger_count))
                {
                    if (markCount < markEffect.trigger_count)
                        continue;

                    AddBattleSkillSpecs(
                        result,
                        markEffect.effect_group_id,
                        effectGroupDictionary,
                        effectDictionary,
                        relicSkillExecTimeDictionary);
                }
            }

            return result;
        }

        private static void AddBattleSkillSpecs(
            ICollection<LabyrinthRelicBattleSkillSpec> result,
            int effectGroupId,
            IReadOnlyDictionary<int, labyrinth_relic_effect_group> effectGroupDictionary,
            IReadOnlyDictionary<int, labyrinth_relic_effect> effectDictionary,
            IReadOnlyDictionary<int, List<relic_skill_exec_time>> relicSkillExecTimeDictionary)
        {
            if (!effectGroupDictionary.TryGetValue(effectGroupId, out var effectGroup))
                return;

            foreach (var effectId in effectGroup.EffectIds)
            {
                if (!effectDictionary.TryGetValue(effectId, out var effect) ||
                    effect.effect_type != BattleSkillEffectType ||
                    effect.effect_value_1 <= 0)
                {
                    continue;
                }

                if (!relicSkillExecTimeDictionary.TryGetValue(effect.effect_value_1, out var execTimes) ||
                    execTimes == null ||
                    execTimes.Count == 0)
                {
                    continue;
                }

                result.Add(new LabyrinthRelicBattleSkillSpec(
                    effectGroupId,
                    effect.effect_id,
                    effect.effect_value_1,
                    effect.effect_value_2,
                    execTimes.Select(x => (float)x.exec_time).ToList()));
            }
        }

        private static Dictionary<int, int> CreateRelicMarkCounts(
            IEnumerable<int> relicIds,
            IReadOnlyDictionary<int, labyrinth_relic> relicDictionary)
        {
            var result = new Dictionary<int, int>();
            foreach (var relicId in relicIds)
            {
                if (!relicDictionary.TryGetValue(relicId, out var relic) ||
                    relic.relic_mark_id <= 0 ||
                    relic.relic_mark_count == 0)
                {
                    continue;
                }

                result[relic.relic_mark_id] = result.GetValueOrDefault(relic.relic_mark_id, 0) + relic.relic_mark_count;
            }

            return result;
        }

        private static long CalcEnhanceValue(
            labyrinth_relic_effect effect,
            IReadOnlyList<PCRCaculator.UnitData> ownUnits,
            IReadOnlyDictionary<int, int> relicMarkCounts,
            int alphaRupee)
        {
            long baseValue = effect.effect_value_2;
            int additiveType = effect.effect_value_3;
            if (additiveType == 0)
                return baseValue;

            long targetValue = GetTargetValueForAdditiveType(additiveType, ownUnits, relicMarkCounts, alphaRupee);
            long cappedValue = Math.Min(targetValue, effect.effect_value_4);
            return baseValue * cappedValue;
        }

        private static long GetTargetValueForAdditiveType(
            int additiveType,
            IReadOnlyList<PCRCaculator.UnitData> ownUnits,
            IReadOnlyDictionary<int, int> relicMarkCounts,
            int alphaRupee)
        {
            if (additiveType == 1)
                return ownUnits.Count;

            if (additiveType >= 10 && additiveType <= 19)
            {
                int roleId = additiveType - 10;
                return ownUnits.Count(unit => GetUnitRoleId(unit.unitId) == roleId);
            }

            if (additiveType >= 20 && additiveType <= 29)
            {
                int talentId = additiveType % 10;
                return ownUnits.Count(unit => HasTalent(unit.unitId, talentId));
            }

            if (additiveType == 30)
                return alphaRupee;

            if (additiveType > 99)
            {
                int relicMarkId = additiveType - 100;
                return relicMarkCounts.TryGetValue(relicMarkId, out var count) ? count : 0;
            }

            return 0;
        }

        private static int GetUnitRoleId(int unitId)
        {
            var manager = MainManager.Instance;
            if (manager == null)
                return 0;

            if (manager.UnitRoleDataDic != null &&
                manager.UnitRoleDataDic.TryGetValue(unitId, out var roleData))
            {
                return roleData.unit_role_id;
            }

            if (manager.UnitRarityDic != null &&
                manager.UnitRarityDic.TryGetValue(unitId, out var rarityData))
            {
                return rarityData.detailData?.unit_role_id ?? 0;
            }

            return 0;
        }

        private static bool HasTalent(int unitId, int talentId)
        {
            if (talentId <= 0)
                return false;

            var talents = MainManager.Instance?.GetTalentIds(unitId);
            return talents != null && talents.Contains(talentId);
        }

        private static bool TryConvertParamType(int value, out eParamType paramType)
        {
            if (!Enum.IsDefined(typeof(eParamType), value))
            {
                paramType = eParamType.NONE;
                return false;
            }

            paramType = (eParamType)value;
            return paramType != eParamType.INVALID_VALUE &&
                   paramType != eParamType.NONE &&
                   paramType != eParamType.BASE_PARAMETER_NUM;
        }
    }

    public sealed class LabyrinthRelicBattleSkillSpec
    {
        public int EffectGroupId { get; }
        public int EffectId { get; }
        public int SkillId { get; }
        public int SkillLevel { get; }
        public IReadOnlyList<float> ExecTimes { get; }

        public LabyrinthRelicBattleSkillSpec(
            int effectGroupId,
            int effectId,
            int skillId,
            int skillLevel,
            IReadOnlyList<float> execTimes)
        {
            EffectGroupId = effectGroupId;
            EffectId = effectId;
            SkillId = skillId;
            SkillLevel = skillLevel;
            ExecTimes = execTimes ?? Array.Empty<float>();
        }
    }

    public sealed class LabyrinthRelicBattleSkillData
    {
        private readonly UnitCtrl unitController;
        private readonly Queue<(float execTime, Skill skill)> sortedSkillQueue;
        private Queue<(float execTime, Skill skill)> execSkillQueue;

        private LabyrinthRelicBattleSkillData(
            UnitCtrl unitController,
            IEnumerable<(float execTime, Skill skill)> sortedSkills)
        {
            this.unitController = unitController;
            sortedSkillQueue = new Queue<(float execTime, Skill skill)>(sortedSkills);
            execSkillQueue = new Queue<(float execTime, Skill skill)>(sortedSkillQueue);
        }

        public int ExecSkillRemainCount => execSkillQueue.Count;

        public static bool TryCreate(
            UnitCtrl unitController,
            LabyrinthRelicBattleSkillSpec spec,
            out LabyrinthRelicBattleSkillData skillData)
        {
            skillData = null;
            if (unitController?.unitActionController == null || spec == null || spec.ExecTimes.Count == 0)
                return false;

            var skill = unitController.unitActionController.CreateLabyrinthRelicSkill(
                spec.SkillId,
                spec.SkillLevel);
            if (skill == null)
                return false;

            var sortedSkills = spec.ExecTimes
                .Select(execTime => (execTime, skill))
                .OrderBy(x => x.execTime)
                .ToList();
            skillData = new LabyrinthRelicBattleSkillData(unitController, sortedSkills);
            return true;
        }

        public void TryExec(float passedTime)
        {
            while (execSkillQueue.Count > 0 &&
                   BattleUtil.LessThanOrApproximately(execSkillQueue.Peek().execTime, passedTime))
            {
                var skillInfo = execSkillQueue.Dequeue();
                unitController.unitActionController.StartAction(skillInfo.skill.SkillId, skillInfo.skill);
            }
        }

        public void ResetExecStatus()
        {
            execSkillQueue.Clear();
            execSkillQueue = new Queue<(float execTime, Skill skill)>(sortedSkillQueue);
        }
    }

    public sealed class LabyrinthRelicBattleSkillExecutor
    {
        private readonly List<LabyrinthRelicBattleSkillData> relicSkillList = new List<LabyrinthRelicBattleSkillData>();
        private List<LabyrinthRelicBattleSkillData> execSkillList = new List<LabyrinthRelicBattleSkillData>();

        public void Clear()
        {
            relicSkillList.Clear();
            execSkillList.Clear();
        }

        public void Initialize(UnitCtrl unitController, IReadOnlyList<LabyrinthRelicBattleSkillSpec> specs)
        {
            Clear();
            if (unitController == null || specs == null || specs.Count == 0)
                return;

            foreach (var spec in specs)
            {
                if (LabyrinthRelicBattleSkillData.TryCreate(unitController, spec, out var skillData))
                    relicSkillList.Add(skillData);
            }

            ResetExecedFlag();
        }

        public void TryExec(float passedTime)
        {
            for (int i = execSkillList.Count - 1; i >= 0; i--)
            {
                var skillData = execSkillList[i];
                skillData.TryExec(passedTime);
                if (skillData.ExecSkillRemainCount == 0)
                    execSkillList.RemoveAt(i);
            }
        }

        private void ResetExecedFlag()
        {
            execSkillList = new List<LabyrinthRelicBattleSkillData>(relicSkillList);
            foreach (var skillData in execSkillList)
                skillData.ResetExecStatus();
        }
    }

    public class LabyrinthEnhanceTempData
    {
#if ENABLE_THREADING
        [ThreadStatic]
        private static LabyrinthEnhanceTempData instance;

        public static LabyrinthEnhanceTempData Instance => instance ??= new LabyrinthEnhanceTempData();
#else
        public static readonly LabyrinthEnhanceTempData Instance = new LabyrinthEnhanceTempData();
#endif

        private const int ParameterEffectType = 1;
        private const int TalentBonusEffectType = 2;

        public IReadOnlyDictionary<eParamType, long> CreateBuffParameterDictionary(IEnumerable<int> enhancedNodeIds)
        {
            var result = new Dictionary<eParamType, long>(new eParamType_DictComparer());
            var manager = MainManager.Instance;
            if (manager == null || enhancedNodeIds == null)
                return result;

            var nodeDictionary = manager.LabyrinthEnhanceNodeDic;
            var effectDictionary = manager.LabyrinthEnhanceDataDic;
            if (nodeDictionary == null || effectDictionary == null)
                return result;

            foreach (int nodeId in enhancedNodeIds.Where(id => id > 0))
            {
                if (!nodeDictionary.TryGetValue(nodeId, out var node) ||
                    !effectDictionary.TryGetValue(node.effect_id, out var effect))
                {
                    continue;
                }

                eParamType paramType;
                long enhanceValue;
                if (effect.effect_type == ParameterEffectType)
                {
                    if (!TryConvertParamType(effect.effect_value_1, out paramType))
                        continue;

                    enhanceValue = effect.effect_value_2;
                }
                else if (effect.effect_type == TalentBonusEffectType)
                {
                    paramType = eParamType.TALENT_BONUS;
                    enhanceValue = effect.effect_value_1;
                }
                else
                {
                    continue;
                }

                if (result.TryGetValue(paramType, out var currentValue))
                    result[paramType] = currentValue + enhanceValue;
                else
                    result[paramType] = enhanceValue;
            }

            return result;
        }

        private static bool TryConvertParamType(int value, out eParamType paramType)
        {
            if (!Enum.IsDefined(typeof(eParamType), value))
            {
                paramType = eParamType.NONE;
                return false;
            }

            paramType = (eParamType)value;
            return paramType != eParamType.INVALID_VALUE &&
                   paramType != eParamType.NONE &&
                   paramType != eParamType.BASE_PARAMETER_NUM;
        }
    }
}
