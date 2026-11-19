using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Elements;
using PCRCaculator.Guild;
using Newtonsoft.Json;
using UnitData = PCRCaculator.UnitData; // Elements 也有同名类，这里明确指向配队数据

namespace ExcelHelper
{
    /// <summary>
    /// 通用版 autotimeline 脚本导出器。
    ///   1. Python 头部：simulator 探测、minitouch 坐标、touchSpeed（按 speed 全局变量生效）
    ///   2. 主体：setOffset 校准 → 初始加速 → 逐条操作（普通点击 / 连点 / AUTO 开合 / 公主形态减速）→ 暂停收尾
    ///   3. 尾部三引号注释块：角色练度详细配置 + 轴参考表
    /// 本类型只负责生成文本，不写盘、不检查导出标记文件。
    /// </summary>
    public static class AutoTimelineLegacyScriptExporter
    {
        public const string Format = "autotimeline-generic.v1";

        /// <summary>网站上 #offset 的默认值（容错窗口，单位：帧）。</summary>
        public const int DefaultAutoOffsetFrames = 30;
        public const int MinAutoOffsetFrames = 3;
        public const int MaxAutoOffsetFrames = 120;

        private const int AutoOpenCloseDelayFrames = 10;
        private const int DebugCheckDelayFrames = 3;
        private const int FirstSpeedUpFrame = 100;
        private const int SecondSpeedUpFrame = 105;
        private const int X2SlowdownBeforeFrames = 30;
        private const int X4SlowdownBeforeFrames = 60;
        private const int X2SpeedUpAfterFrames = 10;
        private const int X4SpeedUpAfterFrames = 20;
        private const int X4SecondSpeedUpDelayFrames = 5;
        private const int PrincessSlowdownFrames = 10;
        private const int PrincessSkipSpeedUpGapFrames = 150;
        private const int PrincessFormCloseGapFrames = 20;
        private const int PrincessFormEarlyTapFrames = 10;
        private const int DirectPressToleranceFrames = 3;
        private const int BattleFrameLimit = 5400;
        private const int LastPauseClampFrame = BattleFrameLimit - 5;

        private static readonly string[] SpeedModes = { "x1", "x2", "x4" };
        private static readonly Regex ToleranceRegex =
            new Regex(@"容错\s*(\d+)\s*帧", RegexOptions.Compiled);

        public static void WriteScriptSet(
            GuildTimelineData timelineData,
            string filePath,
            string talentSkillNodeDescription = null,
            string teamSkillNodeDescription = null,
            bool debug = false,
            int autoOffsetFrames = DefaultAutoOffsetFrames,
            IEnumerable<string> speedModes = null)
        {
            foreach (string speedMode in NormalizeSpeedModes(speedModes))
            {
                File.WriteAllText(
                    filePath + "." + speedMode + ".py",
                    CreateScript(
                        timelineData,
                        speedMode,
                        talentSkillNodeDescription,
                        teamSkillNodeDescription,
                        debug,
                        autoOffsetFrames),
                    new UTF8Encoding(false));
            }
        }

        public static string CreateScript(
            GuildTimelineData timelineData,
            string speedMode,
            string talentSkillNodeDescription = null,
            string teamSkillNodeDescription = null,
            bool debug = false,
            int autoOffsetFrames = DefaultAutoOffsetFrames)
        {
            if (!SpeedModes.Contains(speedMode))
                throw new ArgumentException($"Unsupported speed mode: {speedMode}", nameof(speedMode));
            if (timelineData == null)
                throw new ArgumentNullException(nameof(timelineData));

            List<UnitData> players = timelineData.playerGroupData.playerData.playrCharacters
                .Take(5)
                .ToList();
            List<AxisAction> actions = BuildActions(timelineData, players);

            var sb = new StringBuilder();
            // 第一行是提示，之后才是 speed 开关与调试开关。
            AppendLine(sb, "print('可右键脚本选择打开方式->记事本，查看阵容详细配置或修改脚本。\\n')");
            if (speedMode != "x1")
                AppendLine(sb, "speed = \"不需要加速删除这行\"");
            if (debug)
                AppendLine(sb, "print('当前脚本为调试模式，会自动检测UB帧数是否正确，如不需要请不要勾选。')");
            AppendLine(sb, string.Empty);

            sb.Append(BuildHeader(players, speedMode));
            sb.Append(BuildBody(actions, speedMode, debug, autoOffsetFrames));
            sb.Append(BuildDetailBlock(timelineData, players, actions, talentSkillNodeDescription, teamSkillNodeDescription));

            return sb.ToString().TrimEnd() + "\n";
        }

        private static IEnumerable<string> NormalizeSpeedModes(IEnumerable<string> speedModes)
        {
            if (speedModes == null)
                return SpeedModes;

            var result = new List<string>();
            foreach (string mode in speedModes)
            {
                if (mode != null && SpeedModes.Contains(mode) && !result.Contains(mode))
                    result.Add(mode);
            }

            return result.Count == 0 ? (IEnumerable<string>)SpeedModes : result;
        }

        // ------------------------------------------------------------------
        // 轴数据
        // ------------------------------------------------------------------

        private sealed class AxisAction
        {
            public int UnitId;
            public string Name;
            public string Operation;
            public int RealFrame;
            public int LogicFrame;
            public bool IsBoss;
            public bool IsAuto;
            public bool IsClick;
            public bool IsPrincess;
            public bool IsLast;

            public bool IsManual => !IsAuto && !IsClick;
        }

        private static List<AxisAction> BuildActions(GuildTimelineData timelineData, List<UnitData> players)
        {
            var playerLookup = new Dictionary<int, UnitData>();
            foreach (UnitData unit in players)
            {
                if (!playerLookup.ContainsKey(unit.unitId))
                    playerLookup.Add(unit.unitId, unit);
            }

            var actions = new List<AxisAction>();
            var stateChanges = timelineData.allUnitStateChangeDic ??
                               new Dictionary<int, List<UnitStateChangeData>>();

            foreach (var pair in stateChanges
                         .SelectMany(entry => entry.Value.Select(data => (unitId: entry.Key, data)))
                         .Where(item => item.data.changStateTo == UnitCtrl.ActionState.SKILL_1)
                         .OrderBy(item => item.data.realFrameCount)
                         .ThenBy(item => item.data.currentFrameCount))
            {
                bool isBoss = pair.unitId > 200000;
                string name;
                bool isPrincess = false;
                if (isBoss)
                {
                    name = GetBossName(timelineData);
                }
                else if (playerLookup.TryGetValue(pair.unitId, out UnitData unit))
                {
                    name = unit.GetNicName();
                    isPrincess = IsPrincessForm(pair.unitId);
                }
                else
                {
                    name = pair.unitId.ToString(CultureInfo.InvariantCulture);
                }

                string operation = pair.data.operation ?? string.Empty;
                actions.Add(new AxisAction
                {
                    UnitId = pair.unitId,
                    Name = name,
                    Operation = operation,
                    RealFrame = pair.data.realFrameCount,
                    LogicFrame = pair.data.currentFrameCount,
                    IsBoss = isBoss,
                    IsAuto = IsAutoOperation(operation),
                    IsClick = operation.Trim() == "连点",
                    IsPrincess = isPrincess
                });
            }

            int lastRealFrame = actions.Count == 0 ? 0 : actions.Max(action => action.RealFrame);
            int lastLogicFrame = actions.Count == 0 ? 0 : actions.Max(action => action.LogicFrame);
            // 暂停帧 = 导出表格「角色伤害统计」最后一行的渲染帧（所有伤害记录里最大的 realFrameCount）。
            // 若表格最后一帧比最后一个 UB 还早（例如最后一发是零伤害 UB），仍取较晚的那个，避免脚本提前结束。
            int pauseRealFrame = GetDamageTableLastRealFrame(timelineData);
            if (pauseRealFrame < lastRealFrame)
                pauseRealFrame = lastRealFrame;
            if (pauseRealFrame > 0)
            {
                actions.Add(new AxisAction
                {
                    Name = "LAST",
                    Operation = "暂停",
                    RealFrame = pauseRealFrame,
                    LogicFrame = lastLogicFrame,
                    IsLast = true
                });
            }

            return actions;
        }

        private static bool IsAutoOperation(string operation)
        {
            if (string.IsNullOrEmpty(operation))
                return false;
            string text = operation.Trim();
            return text == "AUTO" || text.StartsWith("AUTO(", StringComparison.Ordinal);
        }

        // ------------------------------------------------------------------
        // Python 头部
        // ------------------------------------------------------------------

        private static string BuildHeader(List<UnitData> players, string speedMode)
        {
            List<string> names = players.Select(unit => unit.GetNicName()).ToList();
            while (names.Count < 5)
                names.Add("角色");

            // pos_name 按队伍位置 1~5 的顺序排（与「角色练度」里的顺序一致），
            // 角色横坐标因此从左到右递增，和名字一一对应。
            List<string> ordered = names;

            string posNames = PythonList(new[] { "暂停", "SET", "AUTO", "SPEED" }.Concat(ordered));
            string dnplayerX = DecimalList(new[] { 0.94, 0.95, 0.95, 0.95 }
                .Concat(new[] { 0.74, 0.62, 0.50, 0.38, 0.26 }.Take(ordered.Count)));
            string dnplayerY = DecimalList(new[] { 0.05, 0.64, 0.76, 0.90 }
                .Concat(Enumerable.Repeat(0.80, ordered.Count)));
            string mumuX = DecimalList(new[] { 0.95, 0.36, 0.24, 0.10 }
                .Concat(new[] { 0.20, 0.20, 0.20, 0.20, 0.20 }.Take(ordered.Count)));

            var sb = new StringBuilder();
            AppendLine(sb, "speedCheck = \"speed\" in globals()");
            AppendLine(sb, "if speedCheck:");
            AppendLine(sb, "    print(\"当前脚本为倍速，不需要请手动删除脚本第一行\\n\")");
            AppendLine(sb, string.Empty);
            AppendLine(sb, "from autotimeline import *");
            AppendLine(sb, "import sys");
            AppendLine(sb, "sys.path.append('.')");
            AppendLine(sb, string.Empty);
            AppendLine(sb, "import ctypes, os");
            AppendLine(sb, "from ctypes import wintypes");
            AppendLine(sb, string.Empty);
            AppendLine(sb, $"pos_name = {posNames}");
            AppendLine(sb, $"pos_dnplayer = ([{dnplayerX}],[{dnplayerY}])");
            AppendLine(sb, $"pos_mumu     = ([{mumuX}],pos_dnplayer[0])");
            AppendLine(sb, "GetModuleFileNameExA = ctypes.windll.psapi.GetModuleFileNameExA");
            AppendLine(sb, string.Empty);
            AppendLine(sb, "def _get_name(h):");
            AppendLine(sb, "    \"\"\"获取内存句柄对应的模拟器进程名\"\"\"");
            AppendLine(sb, "    buf = ctypes.create_string_buffer(1024)");
            AppendLine(sb, "    GetModuleFileNameExA(wintypes.HANDLE(h), None, buf, 1024)");
            AppendLine(sb, "    return os.path.basename(buf.value.decode(errors=\"ignore\"))");
            AppendLine(sb, string.Empty);
            AppendLine(sb, "def detect(h):");
            AppendLine(sb, "    \"\"\"根据模拟器进程名判断模拟器\"\"\"");
            AppendLine(sb, "    n = _get_name(h).lower()");
            AppendLine(sb, "    if \"mumu\" in n: return \"MUMU\", pos_mumu");
            AppendLine(sb, "    if \"ld9box\" in n or \"ldbox\" in n or \"dnplayer\" in n or \"ldplayer\" in n: return \"雷电\", pos_dnplayer");
            AppendLine(sb, "    raise RuntimeError(\"不支持的模拟器: \" + n)");
            AppendLine(sb, string.Empty);
            AppendLine(sb, "def touchSpeed():");
            AppendLine(sb, "    if speedCheck:");
            AppendLine(sb, "        minitouch.press(\"SPEED\")");
            AppendLine(sb, string.Empty);
            AppendLine(sb, "handle_name, (pos_x, pos_y) = detect(Program.hwnd)");
            AppendLine(sb, "print(\"当前模拟器\" + handle_name)");
            AppendLine(sb, string.Empty);
            AppendLine(sb, "print('minitouch 连接中')");
            AppendLine(sb, "minitouch.connect('127.0.0.1', 1111)");
            AppendLine(sb, "max_x = minitouch.getMaxX()");
            AppendLine(sb, "max_y = minitouch.getMaxY()");
            AppendLine(sb, "for i, name in enumerate(pos_name):");
            AppendLine(sb, "    print(name + ' 定位中')");
            AppendLine(sb, "    minitouch.setPos(name, int(max_x * pos_x[i]), int(max_y * pos_y[i]))");
            AppendLine(sb, string.Empty);
            return sb.ToString();
        }

        // ------------------------------------------------------------------
        // 主体（加速 / set / auto 状态机）
        // ------------------------------------------------------------------

        private static string BuildBody(List<AxisAction> actions, string speedMode, bool debug, int autoOffsetFrames)
        {
            bool canSpeed = speedMode != "x1";
            bool isX4 = speedMode == "x4";
            int offset = Math.Max(MinAutoOffsetFrames, Math.Min(MaxAutoOffsetFrames, autoOffsetFrames));
            var sb = new StringBuilder();
            bool speed = false;

            if (debug)
            {
                AppendLine(sb, "def debugFrame(frame, name):");
                AppendLine(sb, "    # autopcr.getlFrame 算的有问题，这里自己计算");
                AppendLine(sb, "    currentFrame = int((90.0 - autopcr.getTime()) * 60)");
                AppendLine(sb, "    if frame != currentFrame:");
                AppendLine(sb, "        print(name + ' [ERROR]检测到当前帧数应为' + str(frame) + ', 实际帧数为' + str(currentFrame))");
                AppendLine(sb, "        autopcr.waitOneFrame(); minitouch.press(\"暂停\")");
                AppendLine(sb, "    else:");
                AppendLine(sb, "        print(name + ' 当前帧数应为' + str(frame) + ', 实际帧数确为' + str(currentFrame))");
                AppendLine(sb, string.Empty);
            }

            AppendLine(sb, "print('解除暂停，塔塔开!')");
            AppendLine(sb, string.Empty);
            AppendLine(sb, "autopcr.setOffset(2, 0); # offset calibration");
            AppendLine(sb, "autopcr.waitFrame(autopcr.getFrame() + 50);");
            AppendLine(sb, string.Empty);

            if (canSpeed)
            {
                AppendLine(sb, $"autopcr.waitFrame({FirstSpeedUpFrame}); touchSpeed() #加速");
                if (isX4)
                    AppendLine(sb, $"autopcr.waitFrame({SecondSpeedUpFrame}); touchSpeed() #加速*4");
                AppendLine(sb, string.Empty);
                speed = true;
            }

            void MaybeSlowDown(int realFrame, bool nextIsPrincess, bool nextNextIsManual)
            {
                if (!speed || !canSpeed || !nextIsPrincess || !nextNextIsManual)
                    return;
                AppendLine(sb, SpeedLine($"{realFrame} - {PrincessSlowdownFrames}", "减速"));
                speed = false;
            }

            void MaybeSpeedUp(int realFrame, bool lastIsPrincess, bool nextIsPrincess, bool nextIsManual,
                int nextLogicFrame, int nextNextLogicFrame)
            {
                if (speed || !canSpeed || lastIsPrincess)
                    return;
                if (nextIsPrincess && !nextIsManual && nextNextLogicFrame - nextLogicFrame < PrincessSkipSpeedUpGapFrames)
                    return;
                AppendLine(sb, SpeedLine($"{realFrame} + {(isX4 ? X4SpeedUpAfterFrames : X2SpeedUpAfterFrames)}", "加速"));
                if (isX4)
                    AppendLine(sb, SpeedLine($"{realFrame} + {X4SpeedUpAfterFrames + X4SecondSpeedUpDelayFrames}", "加速*4"));
                speed = true;
            }

            void AppendDebug(AxisAction action)
            {
                if (!debug)
                    return;
                AppendLine(sb, $"autopcr.waitFrame({action.RealFrame} + {DebugCheckDelayFrames}); debugFrame({action.LogicFrame}, '{EscapePython(action.Name)}') # debug {action.LogicFrame}");
            }

            for (int index = 0; index < actions.Count; index++)
            {
                AxisAction action = actions[index];
                AxisAction next = index + 1 < actions.Count ? actions[index + 1] : null;
                AxisAction nextNext = index + 2 < actions.Count ? actions[index + 2] : null;
                AxisAction previous = index > 0 ? actions[index - 1] : null;

                bool previousPrincessClose = previous != null && previous.IsPrincess &&
                                             action.LogicFrame - previous.LogicFrame < PrincessFormCloseGapFrames;
                bool nextPrincessThenManual = next != null && next.IsPrincess &&
                                              nextNext != null && nextNext.IsManual;

                if (action.IsLast)
                {
                    if (speed)
                        AppendLine(sb, SpeedLine($"{action.RealFrame} - {(isX4 ? X4SlowdownBeforeFrames : X2SlowdownBeforeFrames)}", "减速"));
                    // 暂停帧已在 BuildActions 里取成「角色伤害统计」最后一行的渲染帧，直接写数字。
                    AppendLine(sb, $"autopcr.waitFrame({action.RealFrame.ToString(CultureInfo.InvariantCulture)}); minitouch.press(\"暂停\") #暂停");
                    continue;
                }

                if (action.IsBoss)
                {
                    AppendLine(sb, $"# {action.Name} lframe {action.LogicFrame} rFrame {action.RealFrame} //time {TimeText(action.LogicFrame)}");
                    if (!speed)
                    {
                        MaybeSpeedUp(action.RealFrame, previous != null && previous.IsPrincess,
                            next != null && next.IsPrincess, next != null && next.IsManual,
                            next?.LogicFrame ?? 0, nextNext?.LogicFrame ?? 0);
                    }
                    continue;
                }

                if (action.IsAuto)
                {
                    int tolerance = ParseTolerance(action.Operation, offset);
                    int earlyTapFrames = tolerance > offset ? offset : tolerance - 1;
                    if (earlyTapFrames < DirectPressToleranceFrames)
                    {
                        // 容错太小，直接点角色按钮比开 AUTO 更稳。
                        if (speed)
                        {
                            AppendLine(sb, SpeedLine($"{action.RealFrame} - {(isX4 ? X4SlowdownBeforeFrames : X2SlowdownBeforeFrames)}", "减速"));
                            speed = false;
                        }
                        AppendLine(sb, PressLine($"{action.RealFrame}", action.Name, action.Operation, action.LogicFrame, action.RealFrame));
                        AppendDebug(action);
                        MaybeSpeedUp(action.RealFrame, previous != null && previous.IsPrincess,
                            next != null && next.IsPrincess, next != null && next.IsManual,
                            next?.LogicFrame ?? 0, nextNext?.LogicFrame ?? 0);
                    }
                    else
                    {
                        string openFrame = $"{action.RealFrame} - {(previousPrincessClose ? PrincessFormEarlyTapFrames : earlyTapFrames)}";
                        AppendLine(sb, $"autopcr.waitFrame({openFrame}); minitouch.press(\"AUTO\") #AUTO开");
                        AppendLine(sb, $"# {action.Operation} {action.Name} lframe {action.LogicFrame} rFrame {action.RealFrame} //time {TimeText(action.LogicFrame)}");
                        AppendDebug(action);
                        AppendLine(sb, $"autopcr.waitFrame({action.RealFrame} + {AutoOpenCloseDelayFrames}); minitouch.press(\"AUTO\") #AUTO关");
                        if (!speed)
                        {
                            MaybeSpeedUp(action.RealFrame, previous != null && previous.IsPrincess,
                                next != null && next.IsPrincess, next != null && next.IsManual,
                                next?.LogicFrame ?? 0, nextNext?.LogicFrame ?? 0);
                        }
                        else
                        {
                            MaybeSlowDown(action.RealFrame, next != null && next.IsPrincess, nextNext != null && nextNext.IsManual);
                        }
                    }
                    continue;
                }

                if (action.IsClick)
                {
                    string clickFrame = $"{action.RealFrame} - {(previousPrincessClose ? PrincessFormEarlyTapFrames : offset)}";
                    AppendLine(sb, PressLine(clickFrame, action.Name, action.Operation, action.LogicFrame, action.RealFrame));
                    AppendDebug(action);
                    if (!speed)
                    {
                        MaybeSpeedUp(action.RealFrame, previous != null && previous.IsPrincess,
                            next != null && next.IsPrincess, next != null && next.IsManual,
                            next?.LogicFrame ?? 0, nextNext?.LogicFrame ?? 0);
                    }
                    else
                    {
                        MaybeSlowDown(action.RealFrame, next != null && next.IsPrincess, nextNext != null && nextNext.IsManual);
                    }
                    continue;
                }

                if (speed)
                {
                    AppendLine(sb, SpeedLine($"{action.RealFrame} - {(isX4 ? X4SlowdownBeforeFrames : X2SlowdownBeforeFrames)}", "减速"));
                    speed = false;
                }
                AppendLine(sb, PressLine($"{action.RealFrame}", action.Name, action.Operation, action.LogicFrame, action.RealFrame));
                AppendDebug(action);
                MaybeSpeedUp(action.RealFrame, previous != null && previous.IsPrincess,
                    next != null && next.IsPrincess, next != null && next.IsManual,
                    next?.LogicFrame ?? 0, nextNext?.LogicFrame ?? 0);
            }

            return sb.ToString();
        }

        private static int ParseTolerance(string operation, int fallback)
        {
            if (string.IsNullOrEmpty(operation))
                return fallback;
            Match match = ToleranceRegex.Match(operation);
            if (!match.Success)
                return fallback;
            return int.TryParse(match.Groups[1].Value, NumberStyles.Integer, CultureInfo.InvariantCulture, out int value)
                ? value
                : fallback;
        }

        private static string SpeedLine(string frameExpression, string label)
        {
            return $"autopcr.waitFrame({frameExpression}); touchSpeed() #{label}";
        }

        private static string PressLine(string frameExpression, string name, string operation, int logicFrame, int realFrame)
        {
            string comment = string.IsNullOrEmpty(operation) ? string.Empty : operation + " ";
            return $"autopcr.waitFrame({frameExpression}); minitouch.press(\"{EscapePython(name)}\") #{comment}lframe {logicFrame} rFrame {realFrame} //time {TimeText(logicFrame)}";
        }

        private static string TimeText(int logicFrame)
        {
            // 剩余帧数按 60 帧/秒折成时间：满 60 秒进位到分钟，秒保留两位小数（1:20.88 这种）。
            double remainingSeconds = (BattleFrameLimit - logicFrame) / 60.0;
            if (remainingSeconds < 0)
                remainingSeconds = 0;
            int minutes = (int)(remainingSeconds / 60.0);
            double seconds = Math.Round(remainingSeconds - minutes * 60.0, 2);
            if (seconds >= 60.0)
            {
                minutes++;
                seconds -= 60.0;
            }
            return string.Format(CultureInfo.InvariantCulture, "{0}:{1:00.00}", minutes, seconds);
        }

        // ------------------------------------------------------------------
        // 尾部：角色练度详细配置 + 轴参考
        // ------------------------------------------------------------------

        private static string BuildDetailBlock(
            GuildTimelineData timelineData,
            List<UnitData> players,
            List<AxisAction> actions,
            string talentSkillNodeDescription,
            string teamSkillNodeDescription)
        {
            var sb = new StringBuilder();
            // 正文之后空两行再开三引号块，块内先空两行再写标题。
            AppendLine(sb, string.Empty);
            AppendLine(sb, string.Empty);
            AppendLine(sb, "'''");
            AppendLine(sb, string.Empty);
            AppendLine(sb, string.Empty);
            AppendLine(sb, "角色练度");
            AppendLine(sb, string.Empty);

            string teamCharge = TeamChargeLine(players);
            if (!string.IsNullOrEmpty(teamCharge))
            {
                AppendLine(sb, teamCharge);
                AppendLine(sb, string.Empty);
            }

            foreach (UnitData unit in players)
            {
                AppendLine(sb, RoleHeadLine(unit, talentSkillNodeDescription, teamSkillNodeDescription));
                AppendLine(sb, RoleMasteryLine(unit));
                AppendLine(sb, RoleRankLine(unit));
                AppendLine(sb, RoleSkillLine(unit));
                AppendLine(sb, RoleUniqueEqLine(unit));
                AppendLine(sb, $"连结RANK:{unit.connectRank}");
                AppendLine(sb, RoleExEquipLine(unit, 0, "EX武器"));
                AppendLine(sb, RoleExEquipLine(unit, 1, "EX防具"));
                AppendLine(sb, RoleExEquipLine(unit, 2, "EX首饰"));
                AppendLine(sb, string.Empty);
            }

            AppendLine(sb, "轴参考");
            AppendLine(sb, string.Empty);
            foreach (string line in BuildAxisTable(actions))
                AppendLine(sb, line);
            AppendLine(sb, string.Empty);

            foreach (string line in BuildMzqBlock(timelineData))
                AppendLine(sb, line);

            AppendLine(sb, string.Empty);
            AppendLine(sb, "'''");
            return sb.ToString();
        }

        private static string RoleHeadLine(UnitData unit, string talent, string team)
        {
            return $"角色:{unit.GetNicName()} Lv{unit.level} ★{unit.rarity}  | " +
                   $"属性等级:{SafeDescribe(unit.TalentLevelDescribe)}  | " +
                   $"属性技能:{Sanitize(talent)}  | " +
                   $"大师技能:{Sanitize(team)}  ";
        }

        private static string RoleMasteryLine(UnitData unit)
        {
            string mastery = SafeDescribe(unit.RoleMasteryDescribe, "0-0/0-0/0-0/0-0")
                .Replace("\r", string.Empty)
                .Replace("\n", "/");
            string roleName = RoleTypeName(GetRoleId(unit));
            string label = string.IsNullOrEmpty(roleName) ? "职能精通" : "职能精通-" + roleName;
            return $"{label} 左上/右上/左下/右下:{mastery}  | 好感度:{unit.love2.ToString(CultureInfo.InvariantCulture)}";
        }

        private static string RoleRankLine(UnitData unit)
        {
            return $"Rank:{SafeDescribe(unit.GetRankDescribe)}  | " +
                   $"左上:{EquipText(unit, 0)}  | 右上:{EquipText(unit, 1)}  | " +
                   $"左中:{EquipText(unit, 2)}  | 右中:{EquipText(unit, 3)}  | " +
                   $"左下:{EquipText(unit, 4)}  | 右下:{EquipText(unit, 5)}";
        }

        private static string RoleSkillLine(UnitData unit)
        {
            string levels = unit.skillLevel == null
                ? "0/0/0/0"
                : string.Join("/", unit.skillLevel.Select(level => level.ToString(CultureInfo.InvariantCulture)));
            return $"技能UB/1/2/EX:{levels}  ";
        }

        private static string RoleUniqueEqLine(UnitData unit)
        {
            return $"专武1/专武2:{SafeDescribe(unit.GetUniqueEqsDescribe)}";
        }

        private static string RoleExEquipLine(UnitData unit, int index, string title)
        {
            int level = unit.exEquipLevel != null && index < unit.exEquipLevel.Length
                ? unit.exEquipLevel[index]
                : 0;
            string name = SafeDescribe(() => unit.GetExEquipName(index));
            string subs = ExEquipSubsText(unit, index);
            // 有副属性时不再展示等级，只留副属性。
            string body = subs.Length > 0
                ? "  副属性:" + subs
                : "  等级:" + level.ToString(CultureInfo.InvariantCulture);
            string tail = index < 2 ? "  " : string.Empty;
            if (unit.exEquip != null && index < unit.exEquip.Length && unit.exEquip[index] > 0 && unit.ExceedRank(index) > 0)
                body += $"/星{unit.ExceedRank(index)}";
            return $"{title}:{name}{body}{tail}";
        }

        private static string SafeDescribe(Func<string> describe, string fallback = "无")
        {
            try
            {
                string value = describe();
                return string.IsNullOrEmpty(value) ? fallback : Sanitize(value);
            }
            catch (Exception)
            {
                return fallback;
            }
        }

        private static string EquipText(UnitData unit, int slot)
        {
            if (unit.equipLevel == null || slot >= unit.equipLevel.Length)
                return "未装备";
            int level = unit.equipLevel[slot];
            return level < 0 ? "未装备" : level.ToString(CultureInfo.InvariantCulture);
        }

        // ------------------------------------------------------------------
        // 队伍级配置 / 职能类型名 / EX 副属性（与角色详情面板同一套数据）
        // ------------------------------------------------------------------
        private static readonly string[] RoleTypeNames =
        {
            "攻击型", "破防型", "增益型", "减益型", "强化型", "治疗型", "坦克型", "干扰型"
        };

        private static string TeamChargeLine(List<UnitData> players)
        {
            // 优先：队伍里有「强化型」角色时，用该角色职能精通「右下」（第 4 格）的值，形如 5.1
            string fromEnhanceUnit = TryGetEnhanceUnitCharge(players);
            if (!string.IsNullOrEmpty(fromEnhanceUnit))
                return fromEnhanceUnit;

            try
            {
                PCRCaculator.MainManager manager = PCRCaculator.MainManager.Instance;
                GuildManager guild = GuildManager.Instance;
                if (manager == null || guild == null || guild.SettingData == null)
                    return string.Empty;

                GuildPlayerGroupData group = guild.SettingData.GetCurrentPlayerGroup();
                if (group == null || group.roleMastery == null || group.roleMastery.Count == 0)
                    return string.Empty;

                foreach (var pair in group.roleMastery)
                {
                    string slotName = SlotNameOf(manager, pair.Key);
                    if (string.IsNullOrEmpty(slotName) || slotName.IndexOf("技能值充能", StringComparison.Ordinal) < 0)
                        continue;

                    string typeName = ExtractBracket(slotName);
                    if (typeName.Length == 0)
                        typeName = "强化型";
                    string ability = ExtractAbility(slotName);
                    if (ability.Length == 0)
                        ability = "技能值充能";

                    var value = pair.Value;
                    return typeName + "-" + ability + " " +
                           value.Item1.ToString(CultureInfo.InvariantCulture) + "." +
                           value.Item2.ToString(CultureInfo.InvariantCulture);
                }
            }
            catch (Exception)
            {
                // 队伍没配置职能精通时整行省略，不影响导出
            }

            return string.Empty;
        }

        // 「角色伤害统计」表最后一行的渲染帧（表按 realFrameCount 升序，故取最大值）
        private static int GetDamageTableLastRealFrame(GuildTimelineData timelineData)
        {
            try
            {
                var dic = timelineData == null ? null : timelineData.playerUnitDamageDic;
                if (dic == null)
                    return 0;

                int last = 0;
                foreach (var pair in dic)
                {
                    if (pair.Value == null)
                        continue;
                    foreach (var data in pair.Value)
                    {
                        if (data != null && data.realFrameCount > last)
                            last = data.realFrameCount;
                    }
                }

                return last;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        // 队伍里强化型角色右下（第 4 格）职能精通的「等级.强化」值，如 5.1
        private static string TryGetEnhanceUnitCharge(List<UnitData> players)
        {
            try
            {
                if (players == null)
                    return string.Empty;

                foreach (UnitData unit in players)
                {
                    if (unit == null)
                        continue;
                    if (!string.Equals(RoleTypeName(GetRoleId(unit)), "强化型", StringComparison.Ordinal))
                        continue;

                    string mastery = SafeDescribe(unit.RoleMasteryDescribe, string.Empty);
                    if (string.IsNullOrEmpty(mastery))
                        return string.Empty;

                    string[] cells = mastery.Split('/');
                    string bottomRight = cells[cells.Length - 1].Trim();
                    if (bottomRight.Length == 0 || bottomRight == "0-0")
                        return string.Empty;

                    return "强化型-技能值充能 " + bottomRight.Replace('-', '.');
                }
            }
            catch (Exception)
            {
            }

            return string.Empty;
        }

        private static string SlotNameOf(PCRCaculator.MainManager manager, int masteryId)
        {
            try
            {
                if (manager.UnitRoleSlotDataDic != null)
                {
                    foreach (var pair in manager.UnitRoleSlotDataDic)
                    {
                        var slot = pair.Value;
                        if (slot != null && slot.mastery_id == masteryId && !string.IsNullOrEmpty(slot.name))
                            return slot.name;
                    }
                }

                if (manager.UnitRoleMasterySlotEnhanceDataDic != null &&
                    manager.UnitRoleMasterySlotEnhanceDataDic.TryGetValue(masteryId, out var enhance) &&
                    enhance != null && enhance.slot_level_info != null)
                {
                    foreach (var pair in enhance.slot_level_info)
                    {
                        string name = pair.Value?.name;
                        if (!string.IsNullOrEmpty(name))
                            return name;
                    }
                }
            }
            catch (Exception)
            {
            }

            return string.Empty;
        }

        // 槽位名字形如「【减益型】生命值提升Lv5」，类型名在方括号里；表 unit_role_type 没有被加载，
        // 所以这里从已加载的 unit_role_mastery_slot_data.name 里取，取不到再回落到内置表。
        private static string ExtractBracket(string name)
        {
            if (string.IsNullOrEmpty(name))
                return string.Empty;
            int start = name.IndexOf('【');
            int end = name.IndexOf('】');
            return start >= 0 && end > start + 1
                ? name.Substring(start + 1, end - start - 1)
                : string.Empty;
        }

        private static string ExtractAbility(string name)
        {
            if (string.IsNullOrEmpty(name))
                return string.Empty;
            int end = name.IndexOf('】');
            string rest = end >= 0 ? name.Substring(end + 1) : name;
            rest = rest.Trim();
            int lv = rest.IndexOf("Lv", StringComparison.OrdinalIgnoreCase);
            if (lv >= 0)
                rest = rest.Substring(0, lv);
            return rest.Trim();
        }

        private static int GetRoleId(UnitData unit)
        {
            try
            {
                PCRCaculator.MainManager manager = PCRCaculator.MainManager.Instance;
                if (unit == null || manager == null || manager.UnitRoleDataDic == null)
                    return 0;
                return manager.UnitRoleDataDic.TryGetValue(unit.unitId, out var roleData) && roleData != null
                    ? roleData.unit_role_id
                    : 0;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        private static string RoleTypeName(int roleId)
        {
            try
            {
                PCRCaculator.MainManager manager = PCRCaculator.MainManager.Instance;
                if (manager != null && manager.UnitRoleSlotDataDic != null && manager.UnitRoleMasteryIdDic != null)
                {
                    foreach (var pair in manager.UnitRoleSlotDataDic)
                    {
                        var slot = pair.Value;
                        if (slot == null || string.IsNullOrEmpty(slot.name))
                            continue;
                        if (!manager.UnitRoleMasteryIdDic.TryGetValue(slot.mastery_id, out var mastery) || mastery == null)
                            continue;
                        if (mastery.unit_role_id != roleId)
                            continue;
                        string typeName = ExtractBracket(slot.name);
                        if (typeName.Length > 0)
                            return typeName;
                    }
                }
            }
            catch (Exception)
            {
            }

            return roleId >= 1 && roleId <= RoleTypeNames.Length ? RoleTypeNames[roleId - 1] : string.Empty;
        }

        // 与角色详情面板（EquipmentPageIcon.SetExEquipmentIcon）同一逻辑：
        // 稀有度 >= 5 的 EX 装备才有副属性，exEquipSubs 是成对的 (属性下标, 属性档位)，同名属性累加后格式化。
        private static string ExEquipSubsText(UnitData unit, int slot)
        {
            try
            {
                if (unit == null || unit.exEquip == null || slot < 0 || slot >= unit.exEquip.Length)
                    return string.Empty;

                int equipmentId = unit.exEquip[slot];
                if (equipmentId <= 0)
                    return string.Empty;

                int exRarity = (equipmentId / 100) % 10;
                if (exRarity < 5)
                    return string.Empty;

                if (unit.exEquipSubs == null || !unit.exEquipSubs.TryGetValue(slot, out int[] subs) ||
                    subs == null || subs.Length < 2)
                    return string.Empty;

                PCRCaculator.MainManager manager = PCRCaculator.MainManager.Instance;
                if (manager == null || manager.UnitExEquips == null ||
                    !manager.UnitExEquips.TryGetValue(unit.unitId, out var perUnit) ||
                    perUnit == null || perUnit.Length <= slot || perUnit[slot] == null ||
                    !perUnit[slot].TryGetValue(equipmentId, out var exEquipData) ||
                    exEquipData == null || exEquipData.status == null)
                    return string.Empty;

                List<eParamType> statusKeys = exEquipData.status.Keys.ToList();
                var merged = new Dictionary<eParamType, int>();
                var order = new List<eParamType>();
                for (int i = 0; i + 1 < subs.Length; i += 2)
                {
                    int statusIndex = subs[i];
                    int statusLevel = subs[i + 1];
                    if (statusIndex < 0 || statusIndex >= statusKeys.Count)
                        continue;
                    eParamType paramType = statusKeys[statusIndex];
                    int[] values = exEquipData.status[paramType];
                    if (values == null || statusLevel < 0 || statusLevel >= values.Length)
                        continue;
                    if (!merged.ContainsKey(paramType))
                    {
                        merged[paramType] = 0;
                        order.Add(paramType);
                    }
                    merged[paramType] += values[statusLevel];
                }

                var parts = new List<string>();
                foreach (eParamType paramType in order)
                {
                    int totalValue = merged[paramType];
                    double processed = totalValue;
                    bool percentage = false;
                    if (manager.ParamTypeDic != null &&
                        manager.ParamTypeDic.TryGetValue((int)paramType, out var config) && config != null)
                    {
                        if (config.ratio > 0)
                            processed = (double)totalValue / config.ratio;
                        percentage = config.is_percentage == 1;
                    }
                    string formatted = percentage
                        ? (processed * 100).ToString("0.#", CultureInfo.InvariantCulture) + "%"
                        : processed.ToString("F0", CultureInfo.InvariantCulture);
                    parts.Add(paramType.GetDescription() + "+" + formatted);
                }

                return string.Join("、", parts);
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        private static IEnumerable<string> BuildMzqBlock(GuildTimelineData timelineData)
        {
            yield return "mzq导入用 这个py可以直接导入mzq";
            yield return string.Empty;
            yield return BuildMzqPayload(timelineData);
            yield return string.Empty;
            yield return "mzq导入用结束";
        }

        private static string BuildMzqPayload(GuildTimelineData timelineData)
        {
            try
            {
                string json = JsonConvert.SerializeObject(timelineData);
                return PCRCaculator.MainManager.EncryptDES(json);
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        private static string GetBossName(GuildTimelineData timelineData)
        {
            if (timelineData != null && timelineData.uBDetails != null)
            {
                foreach (var detail in timelineData.uBDetails)
                {
                    if (detail != null && detail.isBossUB && detail.enemyData != null &&
                        !string.IsNullOrEmpty(detail.enemyData.name))
                        return detail.enemyData.name;
                }
            }
            return "boss";
        }

        private static IEnumerable<string> BuildAxisTable(List<AxisAction> actions)
        {
            var rows = actions
                .Where(action => !action.IsLast)
                .Select(action => new[]
                {
                    action.LogicFrame.ToString(CultureInfo.InvariantCulture),
                    action.IsBoss ? string.Empty : TimeText(action.LogicFrame),
                    action.Name,
                    action.IsBoss ? string.Empty : action.Operation
                })
                .ToList();

            var headers = new[] { "帧数", "秒数", "角色", "操作" };
            var widths = new int[3];
            for (int column = 0; column < 3; column++)
            {
                widths[column] = headers[column].Length;
                foreach (string[] row in rows)
                    widths[column] = Math.Max(widths[column], row[column].Length);
            }

            string header = string.Join("\t", new[]
            {
                headers[0].PadRight(widths[0]),
                headers[1].PadRight(widths[1]),
                headers[2].PadRight(widths[2]),
                headers[3]
            });
            yield return header;

            foreach (string[] row in rows)
            {
                yield return string.Join("\t", new[]
                {
                    row[0].PadRight(widths[0]),
                    row[1].PadRight(widths[1]),
                    row[2].PadRight(widths[2]),
                    row[3]
                });
            }
        }

        private static string AlignRow(IEnumerable<(string Title, string Value)> cells)
        {
            return string.Join("  ", cells.Select(cell => $"{cell.Title}:{cell.Value}"));
        }

        // ------------------------------------------------------------------
        // 工具
        // ------------------------------------------------------------------

        private static bool IsPrincessForm(int unitId)
        {
            if (MyGameCtrl.Instance == null ||
                !MyGameCtrl.Instance.playerUnitCtrlDic.TryGetValue(unitId, out UnitCtrl unitCtrl) ||
                unitCtrl.unitActionController == null ||
                unitCtrl.UnionBurstSkillId == 0)
                return false;

            return unitCtrl.unitActionController.GetIsSkillPrincessForm(unitCtrl.UnionBurstSkillId);
        }

        private static string Sanitize(string value)
        {
            if (string.IsNullOrEmpty(value))
                return "无";
            return value
                .Replace("\r\n", "/")
                .Replace('\n', '/')
                .Replace('\r', '/')
                .Replace("'''", "'\\''");
        }

        private static string PythonList(IEnumerable<string> values)
        {
            return "[" + string.Join(",", values.Select(value => $"\"{EscapePython(value)}\"")) + "]";
        }

        private static string DecimalList(IEnumerable<double> values)
        {
            return string.Join(
                ",",
                values.Select(value => value.ToString("0.00", CultureInfo.InvariantCulture)));
        }

        private static string EscapePython(string value)
        {
            if (value == null)
                return string.Empty;
            return value
                .Replace("\\", "\\\\")
                .Replace("\"", "\\\"")
                .Replace("\r", "\\r")
                .Replace("\n", "\\n");
        }

        private static void AppendLine(StringBuilder sb, string line)
        {
            sb.Append(line).Append('\n');
        }
    }
}
