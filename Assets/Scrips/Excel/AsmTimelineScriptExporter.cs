using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Elements;
using Elements.Battle;
using Newtonsoft.Json;
using PCRCaculator;
using PCRCaculator.Guild;
using UnitData = PCRCaculator.UnitData;

namespace ExcelHelper
{
    public sealed class AsmTimelineUnitExportData
    {
        public AsmTimelineUnitExportData(UnitData unit, string name, string detail)
        {
            Unit = unit ?? throw new ArgumentNullException(nameof(unit));
            Name = name ?? string.Empty;
            Detail = detail ?? string.Empty;
        }

        public UnitData Unit { get; }
        public string Name { get; }
        public string Detail { get; }
    }

    public sealed class AsmTimelineScriptContext
    {
        public AsmTimelineScriptContext(
            GuildTimelineData timelineData,
            int bossId,
            IEnumerable<int> bossUnitIds,
            IReadOnlyList<AsmTimelineUnitExportData> players,
            string talentSkillDescription,
            string teamSkillDescription)
        {
            TimelineData = timelineData ?? throw new ArgumentNullException(nameof(timelineData));
            if (bossId <= 0)
                throw new ArgumentOutOfRangeException(nameof(bossId), "Boss id must be positive.");
            if (bossUnitIds == null)
                throw new ArgumentNullException(nameof(bossUnitIds));
            BossId = bossId;
            BossUnitIds = bossUnitIds.Distinct().OrderBy(unitId => unitId).ToArray();
            if (BossUnitIds.Count == 0)
                throw new ArgumentException("At least one battle boss unit id is required.", nameof(bossUnitIds));
            Players = players ?? throw new ArgumentNullException(nameof(players));
            TalentSkillDescription = talentSkillDescription ?? string.Empty;
            TeamSkillDescription = teamSkillDescription ?? string.Empty;
        }

        public GuildTimelineData TimelineData { get; }
        public int BossId { get; }
        public IReadOnlyCollection<int> BossUnitIds { get; }
        public IReadOnlyList<AsmTimelineUnitExportData> Players { get; }
        public string TalentSkillDescription { get; }
        public string TeamSkillDescription { get; }
    }

    /// <summary>
    /// Formats the legacy PCR ASM timeline script. This type only creates text: it does not
    /// authorize export, inspect marker files, or write to the filesystem.
    /// </summary>
    public static class AsmTimelineScriptExporter
    {
        public const string Format = "pcr-asm.v1";

        private static readonly (string Name, Func<UnitData, object> Value)[] Conditions =
        {
            ("st", c => c.rarity),
            ("rk", c => c.rank),
            ("e1", c => int.TryParse(c.equipLevel[0].ToString(), out var val) ? val : -1),
            ("e2", c => int.TryParse(c.equipLevel[1].ToString(), out var val) ? val : -1),
            ("e3", c => int.TryParse(c.equipLevel[2].ToString(), out var val) ? val : -1),
            ("e4", c => int.TryParse(c.equipLevel[3].ToString(), out var val) ? val : -1),
            ("e5", c => int.TryParse(c.equipLevel[4].ToString(), out var val) ? val : -1),
            ("e6", c => int.TryParse(c.equipLevel[5].ToString(), out var val) ? val : -1),
            ("ub", c => c.skillLevel[0]),
            ("s1", c => c.skillLevel[1]),
            ("s2", c => c.skillLevel[2]),
            ("ex", c => c.skillLevel[3]),
            ("lv", c => c.level)
        };

        public static AsmTimelineScriptContext CreateContext(
            GuildTimelineData timelineData,
            int bossId,
            IEnumerable<int> bossUnitIds,
            IEnumerable<UnitData> playerUnits,
            string talentSkillDescription,
            string teamSkillDescription)
        {
            if (playerUnits == null)
                throw new ArgumentNullException(nameof(playerUnits));

            var players = playerUnits
                .Select(unit => new AsmTimelineUnitExportData(unit, unit.GetNicName(), UnitDetail(unit)))
                .ToList();
            return new AsmTimelineScriptContext(
                timelineData,
                bossId,
                bossUnitIds,
                players,
                talentSkillDescription,
                teamSkillDescription);
        }

        public static string CreateContent(AsmTimelineScriptContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            GuildTimelineData timelineData = context.TimelineData;
            var cdict = context.Players.ToDictionary(
                player => player.Unit.unitId,
                player => player);
            var bossUnitIds = new HashSet<int>(context.BossUnitIds);
            const string enemy = "boss";
            var src = new StringBuilder();
            var messageLines = new List<string>();
            void AppendSourceLine(string line)
            {
                src.AppendLine(line);
            }
            void AppendMessageLine(string line)
            {
                messageLines.Add(line);
            }
            var allStateChanges = timelineData.allUnitStateChangeDic ??
                                  new Dictionary<int, List<UnitStateChangeData>>();
            var allHpChanges = timelineData.allUnitHPDic ??
                               new Dictionary<int, List<ValueChangeData>>();

            const string header = "log seed\n";
            AppendSourceLine(header);

            foreach (var pair in cdict)
            {
                string cond = string.Join(" ", Conditions.Select(
                    condition => $"{condition.Name}={condition.Value(pair.Value.Unit)}"));
                AppendSourceLine($"//require {pair.Key}:{cond}");
            }

            AppendSourceLine($"//require {context.BossId}:");

            foreach (var pair in cdict)
            {
                int unitId = pair.Value.Unit.unitId;
                string name = pair.Value.Name;
                switch (unitId)
                {
                    case 105701:
                        unitId = 170301;
                        break;
                    case 106101:
                        name = "矛衣未";
                        break;
                    case 114701:
                        name = "春511";
                        break;
                    case 125001:
                        name = "解511";
                        break;
                }
                AppendSourceLine($"mov {unitId},{name}");
            }

            // Keep the legacy ASM comment payload and ordering byte-for-byte compatible.
            AppendMessageLine($"boss: {context.BossId}");
            var playerDetailLines = cdict
                .Select(pair => $"{pair.Value.Detail}|{pair.Value.Name}")
                .ToList();
            if (playerDetailLines.Count == 0)
                AppendMessageLine(string.Empty);
            else
                foreach (var line in playerDetailLines) AppendMessageLine(line);
            AppendMessageLine($"属性技能：{context.TalentSkillDescription}");
            AppendMessageLine($"大师技能：{context.TeamSkillDescription}");
            AppendMessageLine("帧轴：");

            const int limit = 60 * 90;
            foreach (var (unit, data) in allStateChanges
                         .Where(pair => pair.Value != null)
                         .SelectMany(pair =>
                             pair.Value.Select(item => (pair.Key, item)))
                         .Where(item => item.item.changStateTo == UnitCtrl.ActionState.SKILL_1)
                         .OrderBy(item => item.item.realFrameCount))
            {
                int unitId = unit;
                if (cdict.TryGetValue(unitId, out var player))
                {
                    string name = unitId switch
                    {
                        106101 => "矛衣未",
                        114701 => "春511",
                        125001 => "解511",
                        _ => player.Name,
                    };
                    AppendMessageLine($"{ToTime(limit - data.currentFrameCount, limit)}:{data.realFrameCount})\t{name}");
                    AppendSourceLine($"wait {data.realFrameCount}; pressub {name}; // lframe {data.currentFrameCount}");
                }
                else if (bossUnitIds.Contains(unitId))
                {
                    AppendSourceLine("//bossub");
                    AppendMessageLine($"{ToTime(limit - data.currentFrameCount, limit)}:{data.realFrameCount})\t{enemy}");
                }
                else
                {
                    AppendSourceLine($"//autoub {unitId}");
                    AppendMessageLine($"{ToTime(limit - data.currentFrameCount, limit)}:{data.realFrameCount})\tautoub {unitId}");
                }
            }

            var lastDamage = FindLastDamage(allHpChanges);
            if (lastDamage.RealFrame > 0 && lastDamage.LogicFrame == 5399)
            {
                AppendSourceLine($"wait {lastDamage.RealFrame}; pause;");
            }

            var firstStateChanges = allStateChanges.Values
                .FirstOrDefault(changes => changes != null && changes.Count > 0);
            int dieFrame = firstStateChanges?.LastOrDefault().realFrameCount ?? 0;
            AppendMessageLine($"最后一帧：{dieFrame}");
            foreach (var line in messageLines)
                AppendSourceLine($"//{line}");
            AppendSourceLine("//");
            return src.ToString();
        }

        private static string UnitDetail(UnitData info)
        {
            string skillLevels = string.Join(",", new[]
            {
                info.skillLevel[0], info.skillLevel[1], info.skillLevel[2], info.skillLevel[3]
            });
            string unitsLove = JsonConvert.SerializeObject(info.playLoveDic);
            return $"{info.unitId}|{info.level}|{info.rarity}|{info.love2}|{(int)info.rank}|{string.Concat(info.equipLevel.Select(level => level.ToString()[0]))}|{skillLevels}|{info.uniqueEqLv}|{info.uniqueEq2Lv}|{info.GetExEquipDescribe()}|{info.TalentLevelDescribe()}|{UnitRoleMasteryDetail(info)}|{unitsLove}";
        }

        private static string UnitRoleMasteryDetail(UnitData info)
        {
            if (!MainManager.Instance.UnitRoleDataDic.ContainsKey(info.unitId))
                return "无";

            return info.RoleMasteryDescribe()
                .Replace("\r\n", "/")
                .Replace('\n', '/')
                .Replace('\r', '/');
        }

        private static string ToTime(long time, int limit)
        {
            return $"{time / 3600}:{(time / 60 % 60):D2}:{(time % 60):D2} ({limit - time:D4}";
        }

        private static (int LogicFrame, int RealFrame) FindLastDamage(
            IReadOnlyDictionary<int, List<ValueChangeData>> allHpChanges)
        {
            int bestLogic = 0;
            int bestReal = 0;

            foreach (var list in allHpChanges.Values)
            {
                if (list == null)
                    continue;
                foreach (var data in list)
                {
                    if (data.damage <= 0 || data.xValue >= 5400)
                        continue;

                    if (data.xValue > bestLogic ||
                        (data.xValue == bestLogic && data.realFrameCount > bestReal))
                    {
                        bestLogic = data.xValue;
                        bestReal = data.realFrameCount;
                    }
                }
            }

            return (bestLogic, bestReal);
        }
    }
}
