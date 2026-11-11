using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using Elements;
using PCRCaculator.Guild;

namespace ExcelHelper
{
    public static class AutoTimelineLegacyScriptExporter
    {
        private const int AutoCloseDelayFrames = 5;
        private const int TimeUpPauseOffsetFrames = 60;
        private const int MinSpeedTapIntervalFrames = 10;
        private const int PrincessFormSpeedUpDelayFrames = 70;
        private const int X2SpeedUpAfterUbFrames = 10;
        private const int X4SpeedUpAfterUbFrames = 20;
        private const int X4SecondSpeedTapDelayFrames = 10;
        private const int X2SlowdownBeforeUbFrames = 30;
        private const int X4SlowdownBeforeUbFrames = 60;
        private static readonly string[] SpeedModes = { "x1", "x2", "x4" };

        public static void WriteScriptSet(GuildTimelineData timelineData, string filePath)
        {
            foreach (string speedMode in SpeedModes)
            {
                File.WriteAllText(
                    filePath + "." + speedMode + ".py",
                    CreateScript(timelineData, speedMode),
                    new UTF8Encoding(false));
            }
        }

        public static string CreateScript(GuildTimelineData timelineData, string speedMode)
        {
            if (!SpeedModes.Contains(speedMode))
                throw new ArgumentException($"Unsupported speed mode: {speedMode}", nameof(speedMode));
            if (timelineData == null)
                throw new ArgumentNullException(nameof(timelineData));

            List<string> actors = PlayerNames(timelineData);
            List<ScriptLine> body = BuildBodyLines(timelineData);
            if (speedMode != "x1")
                body.AddRange(BuildSpeedLines(body, speedMode));

            List<string> lines = BuildHeaderLines(actors, speedMode);
            lines.AddRange(body.OrderBy(line => line.Frame).ThenBy(line => line.Order).Select(line => line.Text));
            return string.Join("\n", lines).TrimEnd() + "\n";
        }

        private static List<string> PlayerNames(GuildTimelineData timelineData)
        {
            return timelineData.playerGroupData.playerData.playrCharacters
                .Take(5)
                .Select(unit => unit.GetNicName())
                .ToList();
        }

        private static List<string> BuildHeaderLines(List<string> actors, string speedMode)
        {
            string posNames = PythonList(new[] { "暂停", "SET", "AUTO", "SPEED" }.Concat(actors));
            string dnplayerX = DecimalList(new[] { 0.94, 0.95, 0.95, 0.95 }.Concat(
                new[] { 0.74, 0.62, 0.50, 0.38, 0.26 }.Take(actors.Count)));
            string dnplayerY = DecimalList(new[] { 0.05, 0.64, 0.76, 0.90 }.Concat(
                Enumerable.Repeat(0.80, actors.Count)));
            string mumuX = DecimalList(new[] { 0.95, 0.36, 0.24, 0.10 }.Concat(
                new[] { 0.20, 0.20, 0.20, 0.20, 0.20 }.Take(actors.Count)));

            var lines = new List<string>
            {
                "from autotimeline import *",
                "import sys",
                "sys.path.append('.')",
                "",
                "import ctypes, os",
                "from ctypes import wintypes",
                "",
                $"pos_name = {posNames}",
                $"pos_dnplayer = ({dnplayerX},{dnplayerY})",
                $"pos_mumu     = ({mumuX},pos_dnplayer[0])",
                "GetModuleFileNameExA = ctypes.windll.psapi.GetModuleFileNameExA",
                "",
                "def _get_module_path(h):",
                "    buf = ctypes.create_string_buffer(1024)",
                "    GetModuleFileNameExA(wintypes.HANDLE(h), None, buf, 1024)",
                "    return buf.value.decode(errors=\"ignore\")",
                "",
                "def detect(h):",
                "    path = _get_module_path(h)",
                "    n = os.path.basename(path).lower()",
                "    p = path.lower()",
                "    if \"mumu\" in p: return \"MUMU\", pos_mumu",
                "    if \"ldplayer14\" in p or \"dnplayer\" in n or \"ld9box\" in n or \"ldbox\" in n: return \"雷电\", pos_dnplayer",
                "    raise RuntimeError(\"不支持的模拟器: \" + (n or path))",
                "",
                "handle_name, (pos_x, pos_y) = detect(Program.hwnd)",
                "print(\"当前模拟器\" + handle_name)",
                "print(\"minitouch 连接中\")",
                "minitouch.connect(\"127.0.0.1\", 1111)",
                "max_x = minitouch.getMaxX()",
                "max_y = minitouch.getMaxY()",
                "for i, name in enumerate(pos_name):",
                "    print(name + \" 定位中\")",
                "    minitouch.setPos(name, int(max_x * pos_x[i]), int(max_y * pos_y[i]))",
                "",
                "print(\"解除暂停，塔塔开!\")",
                "autopcr.setOffset(2, 0); # offset calibration",
                "",
            };

            if (speedMode != "x1")
            {
                lines.Add("# 如不需要加速，请使用 x1 脚本。");
                lines.Add("def touchSpeed():");
                lines.Add("    minitouch.press(\"SPEED\")");
                lines.Add("");
            }

            return lines;
        }

        private static List<ScriptLine> BuildBodyLines(GuildTimelineData timelineData)
        {
            var players = timelineData.playerGroupData.playerData.playrCharacters
                .ToDictionary(
                    unit => unit.unitId,
                    unit => new
                    {
                        Name = unit.GetNicName(),
                        IsPrincessForm = IsPrincessForm(unit.unitId)
                    });

            var lines = new List<ScriptLine>();
            int order = 0;
            // Script frames use realFrameCount (waitFrame), not currentFrameCount (lframe).
            foreach (var pair in timelineData.allUnitStateChangeDic
                         .SelectMany(pair => pair.Value.Select(data => (unitId: pair.Key, data)))
                         .Where(item => item.data.changStateTo == UnitCtrl.ActionState.SKILL_1)
                         .OrderBy(item => item.data.realFrameCount)
                         .ThenBy(item => item.data.currentFrameCount))
            {
                int unitId = pair.unitId;
                UnitStateChangeData data = pair.data;
                if (unitId <= 200000)
                {
                    bool playerFound = players.TryGetValue(unitId, out var player);
                    string actor = playerFound ? player.Name : unitId.ToString();
                    int speedUpDelayFrames = playerFound && player.IsPrincessForm
                        ? PrincessFormSpeedUpDelayFrames
                        : 0;
                    if (data.operation == "连点")
                    {
                        lines.Add(new ScriptLine(
                            data.realFrameCount,
                            order++,
                            $"autopcr.waitFrame({data.realFrameCount}); minitouch.press(\"{EscapePython(actor)}\") #连点 lframe {data.currentFrameCount}",
                            speedUpDelayFrames));
                    }
                    else if (IsAutoOperation(data.operation))
                    {
                        lines.Add(new ScriptLine(
                            data.realFrameCount,
                            order++,
                            $"autopcr.waitFrame({data.realFrameCount}); minitouch.press(\"AUTO\") #开AUTO lframe {data.currentFrameCount}",
                            speedUpDelayFrames));
                        lines.Add(new ScriptLine(
                            data.realFrameCount + AutoCloseDelayFrames,
                            order++,
                            $"autopcr.waitFrame({data.realFrameCount + AutoCloseDelayFrames}); minitouch.press(\"AUTO\") #关AUTO lframe {data.currentFrameCount}",
                            null));
                    }
                    else
                    {
                        lines.Add(new ScriptLine(
                            data.realFrameCount,
                            order++,
                            $"autopcr.waitFrame({data.realFrameCount}); minitouch.press(\"{EscapePython(actor)}\") # lframe {data.currentFrameCount}",
                            speedUpDelayFrames));
                    }
                }
                else
                {
                    lines.Add(new ScriptLine(data.realFrameCount, order++, "# bossub", null));
                }
            }

            int dieFrame = LastRealFrame(timelineData);
            if (dieFrame > 0)
            {
                lines.Add(new ScriptLine(
                    dieFrame - TimeUpPauseOffsetFrames,
                    order++,
                    $"autopcr.waitFrame({dieFrame} - {TimeUpPauseOffsetFrames}); minitouch.press(\"暂停\") # time-up pause",
                    null));
            }

            return lines;
        }

        private static bool IsAutoOperation(string operation)
        {
            return operation == "AUTO" ||
                   operation?.StartsWith("AUTO(", StringComparison.Ordinal) == true;
        }

        private static List<ScriptLine> BuildSpeedLines(List<ScriptLine> bodyLines, string speedMode)
        {
            List<ScriptLine> ubTaps = bodyLines
                .Where(line => line.PostUbSpeedUpDelayFrames.HasValue)
                .OrderBy(line => line.Frame)
                .ThenBy(line => line.Order)
                .ToList();
            var speedLines = new List<ScriptLine>();
            if (ubTaps.Count == 0)
                return speedLines;

            int slowdownOffset = speedMode == "x4"
                ? X4SlowdownBeforeUbFrames
                : X2SlowdownBeforeUbFrames;
            int order = -100000;

            TryAddSpeedWindow(speedLines, speedMode, 300, ubTaps[0].Frame - slowdownOffset, ref order);
            for (int index = 1; index < ubTaps.Count; index++)
            {
                int defaultSpeedUpDelay =
                    speedMode == "x4"
                        ? X4SpeedUpAfterUbFrames
                        : X2SpeedUpAfterUbFrames;
                int start = ubTaps[index - 1].Frame + Math.Max(
                    defaultSpeedUpDelay,
                    ubTaps[index - 1].PostUbSpeedUpDelayFrames.Value);
                int end = ubTaps[index].Frame - slowdownOffset;
                TryAddSpeedWindow(speedLines, speedMode, start, end, ref order);
            }

            return speedLines;
        }

        private static void TryAddSpeedWindow(
            List<ScriptLine> speedLines,
            string speedMode,
            int startFrame,
            int slowdownFrame,
            ref int order)
        {
            int lastSpeedUpFrame = startFrame;
            if (speedMode == "x4")
                lastSpeedUpFrame += X4SecondSpeedTapDelayFrames;
            if (slowdownFrame - lastSpeedUpFrame < MinSpeedTapIntervalFrames)
                return;

            speedLines.Add(new ScriptLine(startFrame, order++, SpeedLine(startFrame, "加速"), null));
            if (speedMode == "x4")
                speedLines.Add(new ScriptLine(lastSpeedUpFrame, order++, SpeedLine(lastSpeedUpFrame, "加速*4"), null));
            speedLines.Add(new ScriptLine(slowdownFrame, order++, SpeedLine(slowdownFrame, "减速"), null));
        }

        private static bool IsPrincessForm(int unitId)
        {
            if (MyGameCtrl.Instance == null ||
                !MyGameCtrl.Instance.playerUnitCtrlDic.TryGetValue(unitId, out UnitCtrl unitCtrl) ||
                unitCtrl.unitActionController == null ||
                unitCtrl.UnionBurstSkillId == 0)
                return false;

            return unitCtrl.unitActionController.GetIsSkillPrincessForm(unitCtrl.UnionBurstSkillId);
        }

        private static string SpeedLine(int frame, string label)
        {
            return $"autopcr.waitFrame({frame}); touchSpeed() #{label}";
        }

        private static int LastRealFrame(GuildTimelineData timelineData)
        {
            return timelineData.allUnitStateChangeDic
                .SelectMany(pair => pair.Value)
                .Select(data => data.realFrameCount)
                .DefaultIfEmpty(0)
                .Max();
        }

        private static string PythonList(IEnumerable<string> values)
        {
            return "[" + string.Join(",", values.Select(value => $"\"{EscapePython(value)}\"")) + "]";
        }

        private static string DecimalList(IEnumerable<double> values)
        {
            return "[" + string.Join(
                ",",
                values.Select(value => value.ToString("0.00", CultureInfo.InvariantCulture))) + "]";
        }

        private static string EscapePython(string value)
        {
            return value
                .Replace("\\", "\\\\")
                .Replace("\"", "\\\"")
                .Replace("\r", "\\r")
                .Replace("\n", "\\n");
        }

        private sealed class ScriptLine
        {
            public ScriptLine(int frame, int order, string text, int? postUbSpeedUpDelayFrames)
            {
                Frame = frame;
                Order = order;
                Text = text;
                PostUbSpeedUpDelayFrames = postUbSpeedUpDelayFrames;
            }

            public int Frame { get; }
            public int Order { get; }
            public string Text { get; }
            public int? PostUbSpeedUpDelayFrames { get; }
        }
    }
}
