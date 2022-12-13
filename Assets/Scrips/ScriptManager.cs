using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elements;
using Elements.Battle;
using PCRCaculator;
using PCRCaculator.Guild;
using UnityEngine;

namespace Assets.Scrips
{
    public class ScriptManager
    {
        private BattleManager mgr;
        private Func<int, (int, bool)>[] compiled;
        private Dictionary<string, double> variables = new Dictionary<string, double>();
        private HashSet<int> press = new HashSet<int>();
        private Dictionary<int, int> pressing = new Dictionary<int, int>();
        private List<int> pc = new List<int>();
        private static StreamWriter logger = new StreamWriter(
            new FileStream("logger.txt", FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
        {
            AutoFlush = true
        };
        private Dictionary<int, UnitCtrl> unitlist = new Dictionary<int, UnitCtrl>();

        public ScriptManager(BattleManager mgr)
        {
            this.mgr = mgr;
        }

        private static int Parse(double id)
        {
            if (GuildManager.EnemyDataDic.TryGetValue((int)id, out var data))
                return data.unit_id;
            return (int)id;
        }
        public void ParseScript()
        {
            variables.Clear();
            pressing.Clear();
            press.Clear();
            pc.Clear();
            pc.Add(0);
            variables["seed"] = MyGameCtrl.Instance.CurrentSeedForSave;
            variables["limit_time"] = MyGameCtrl.Instance.tempData.SettingData.limitTime;

            string script = null;

            var pdict = GuildManager.Instance.SettingData.GetCurrentPlayerData().playrCharacters.ToDictionary(
                unit => unit.unitId,
                unit => unit);
            pdict.Add(mgr.BossUnit.enemyId, null);

            foreach (var file in Directory.EnumerateFiles(".").Where(f => f.EndsWith(".asm")))
            {
                var scripts = File.ReadAllText(file).Split('~');

                for (int i = 0; i < scripts.Length; ++i)
                {
                    logger.WriteLine($"judging {file}.{i}");

                    try
                    {

                        var lines = scripts[i].Split('\n', ';');

                        var valid = new HashSet<int>();
                        var all = new HashSet<int>();

                        foreach (var line in lines)
                        {
                            if (!line.StartsWith("//require")) continue;
                            var s1 = line.Substring(9).Trim().Split(':');
                            var unit = int.Parse(s1[0]);
                            all.Add(unit);
                            if (pdict.TryGetValue(unit, out var val))
                            {
                                var flag = true;
                                var cond = s1.Length == 1 ? new string[0] : s1[1].Trim().Split(' ');
                                foreach (var c in cond)
                                {
                                    if (string.IsNullOrEmpty(c)) continue;
                                    var x = 0;
                                    var field = c.Substring(0, 2);
                                    if (field == "st")
                                    {
                                        x = val.rarity;
                                    }
                                    else if (field == "rk")
                                        x = (int)val.rank;
                                    else if (field == "e1")
                                        x = val.equipLevel[0];
                                    else if (field == "e2")
                                        x = val.equipLevel[1];
                                    else if (field == "e3")
                                        x = val.equipLevel[2];
                                    else if (field == "e4")
                                        x = val.equipLevel[3];
                                    else if (field == "e5")
                                        x = val.equipLevel[4];
                                    else if (field == "e6")
                                        x = val.equipLevel[5];
                                    else if (field == "s1")
                                        x = val.skillLevel[1];
                                    else if (field == "s2")
                                        x = val.skillLevel[2];
                                    else if (field == "ex")
                                        x = val.skillLevel[3];
                                    else if (field == "ub")
                                        x = val.skillLevel[0];
                                    else if (field == "lv")
                                        x = val.level;

                                    var y = int.Parse(c.Substring(3));
                                    switch (c[2])
                                    {
                                        case '>':
                                            {
                                                if (x <= y) flag = false;
                                                break;
                                            }
                                        case '<':
                                            {
                                                if (x >= y) flag = false;
                                                break;
                                            }
                                        case '=':
                                            {
                                                if (x != y) flag = false;
                                                break;
                                            }
                                    }

                                    if (!flag)
                                    {
                                        logger.WriteLine($"assumption {c} for {unit} failed");
                                        break;
                                    }

                                }

                                if (flag)
                                {
                                    valid.Add(unit);
                                }
                            }
                            else
                            {
                                logger.WriteLine($"unit {unit} not exists");
                            }
                        }

                        if (valid.Count != all.Count) continue;
                        script = scripts[i];
                        goto break2;
                    }
                    catch (Exception e)
                    {
                        logger.WriteLine($"error judge {file}.{i}:{e}");
                    }
                }
            }
            break2:
            if (script == null)
            {
                compiled = Array.Empty<Func<int, (int, bool)>>();
                return;
            }

            var ls = script.Split('\n', ';');
            compiled = new Func<int, (int, bool)>[ls.Length];
            var ii = 0;
            var labels = new Dictionary<string, int>();
            foreach (var cmd in ls)
            {
                ++ii;
                var l = cmd.Split('/')[0].Trim();
                if (l.EndsWith(":"))
                {
                    var name = l.Substring(0, l.Length - 1);
                    logger.WriteLine($"registering label: {name} to {ii}");
                    labels.Add(name, ii);
                    compiled[ii - 1] = pc => (pc, true);
                }
            }

            ii = 0;
            foreach (var cmd in ls)
            {
                ++ii;
                var l = cmd.Split('/')[0].Trim();
                if (l.EndsWith(":")) continue;
                var c1 = l.Split(' ');
                var op = c1[0];
                var arg = c1.Length == 1 ? Array.Empty<string>() : c1[1].Split(',');
                var argv = new Func<double>[arg.Length];
                for (var i = 0; i < argv.Length; i++)
                {
                    arg[i] = arg[i].Trim();
                    if (double.TryParse(arg[i], out var val4))
                        argv[i] = () => val4;
                    else
                    {
                        var name = arg[i];
                        argv[i] = () => variables.TryGetValue(name, out var val) ? val : 0;
                    }
                }

                Func<int, (int, bool)> result = null;
                switch (op)
                {
                    case "j":
                    {
                        var next = labels[arg[0]];
                        result = _ =>
                        {
                            logger.WriteLine($"jumping to {next}.");
                            return (next, true);
                        };
                        break;
                    }
                    case "jc":
                    {
                        var next = labels[arg[0]];
                        var a = argv[1];
                        result = pc =>
                        {
                            if (a() != 0)
                            {
                                logger.WriteLine($"jumping to {next}.");
                                return (next, true);
                            }

                            return (pc + 1, true);
                        };
                        break;
                    }
                    case "mov":
                    {
                        var a = argv[0];
                        var b = arg[1];
                        result = pc =>
                        {
                            variables[b] = a();
                            return (pc + 1, true);
                        };
                        break;
                    }
                    case "div":
                    {
                        var a = argv[0];
                        var b = argv[1];
                        var c = arg[2];
                        result = pc =>
                        {
                            variables[c] = a() / b();
                            return (pc + 1, true);
                        };
                        break;
                    }
                    case "mul":
                    {
                        var a = argv[0];
                        var b = argv[1];
                        var c = arg[2];
                        result = pc =>
                        {
                            variables[c] = a() * b();
                            return (pc + 1, true);
                        };
                        break;
                    }
                    case "add":
                    {
                        var a = argv[0];
                        var b = argv[1];
                        var c = arg[2];
                        result = pc =>
                        {
                            variables[c] = a() + b();
                            return (pc + 1, true);
                        };
                        break;
                    }
                    case "sub":
                    {
                        var a = argv[0];
                        var b = argv[1];
                        var c = arg[2];
                        result = pc =>
                        {
                            variables[c] = a() - b();
                            return (pc + 1, true);
                        };
                        break;
                    }
                    case "lt":
                    {
                        var a = argv[0];
                        var b = argv[1];
                        var c = arg[2];
                        result = pc =>
                        {
                            variables[c] = a() < b() ? 1 : 0;
                            return (pc + 1, true);
                        };
                        break;
                    }
                    case "gt":
                    {
                        var a = argv[0];
                        var b = argv[1];
                        var c = arg[2];
                        result = pc =>
                        {
                            variables[c] = a() > b() ? 1 : 0;
                            return (pc + 1, true);
                        };
                        break;
                    }
                    case "le":
                    {
                        var a = argv[0];
                        var b = argv[1];
                        var c = arg[2];
                        result = pc =>
                        {
                            variables[c] = a() <= b() ? 1 : 0;
                            return (pc + 1, true);
                        };
                        break;
                    }
                    case "ge":
                    {
                        var a = argv[0];
                        var b = argv[1];
                        var c = arg[2];
                        result = pc =>
                        {
                            variables[c] = a() >= b() ? 1 : 0;
                            return (pc + 1, true);
                        };
                        break;
                    }
                    case "eq":
                    {
                        var a = argv[0];
                        var b = argv[1];
                        var c = arg[2];
                        result = pc =>
                        {
                            variables[c] = a() == b() ? 1 : 0;
                            return (pc + 1, true);
                        };
                        break;
                    }
                    case "ne":
                    {
                        var a = argv[0];
                        var b = argv[1];
                        var c = arg[2];
                        result = pc =>
                        {
                            variables[c] = a() != b() ? 1 : 0;
                            return (pc + 1, true);
                        };
                        break;
                    }
                    case "in_tp":
                    {
                        var a = argv[0];
                        var b = arg[1];
                        result = pc =>
                        {
                            if (unitlist.TryGetValue((int) a(), out var val))
                                variables[b] = val.energy;
                            return (pc + 1, true);
                        };
                        break;
                    }
                    case "in_hp":
                    {
                        var a = argv[0];
                        var b = arg[1];
                        result = pc =>
                        {
                            if (unitlist.TryGetValue((int) Parse(a()), out var val))
                                variables[b] = val.Hp;
                            return (pc + 1, true);
                        };
                        break;
                    }
                    case "in_maxhp":
                    {
                        var a = argv[0];
                        var b = arg[1];
                        result = pc =>
                        {
                            if (unitlist.TryGetValue((int)Parse(a()), out var val))
                                variables[b] = val.MaxHp;
                            return (pc + 1, true);
                        };
                        break;
                    }
                    case "in_atk":
                    {
                        var a = argv[0];
                        var b = arg[1];
                        result = pc =>
                        {
                            if (unitlist.TryGetValue((int)Parse(a()), out var val))
                                variables[b] = val.Atk;
                            return (pc + 1, true);
                        };
                        break;
                    }
                    case "in_magicstr":
                    {
                        var a = argv[0];
                        var b = arg[1];
                        result = pc =>
                        {
                            if (unitlist.TryGetValue((int)Parse(a()), out var val))
                                variables[b] = val.MagicStr;
                            return (pc + 1, true);
                        };
                        break;
                    }
                    case "in_def":
                    {
                        var a = argv[0];
                        var b = arg[1];
                        result = pc =>
                        {
                            if (unitlist.TryGetValue((int)Parse(a()), out var val))
                                variables[b] = val.Def;
                            return (pc + 1, true);
                        };
                        break;
                    }
                    case "in_magicdef":
                    {
                        var a = argv[0];
                        var b = arg[1];
                        result = pc =>
                        {
                            if (unitlist.TryGetValue((int)Parse(a()), out var val))
                                variables[b] = val.MagicDef;
                            return (pc + 1, true);
                        };
                        break;
                    }
                    case "in_state":
                    {
                        var a = argv[0];
                        var b = arg[1];
                        result = pc =>
                        {
                            if (unitlist.TryGetValue((int)Parse(a()), out var val))
                                variables[b] = (int) val.CurrentState;
                            return (pc + 1, true);
                        };
                        break;
                    }
                    case "in_skill":
                    {
                        var a = argv[0];
                        var b = arg[1];
                        result = pc =>
                        {
                            if (unitlist.TryGetValue((int)Parse(a()), out var val))
                                variables[b] = val.CurrentSkillId;
                            return (pc + 1, true);
                        };
                        break;
                    }
                    case "in_break":
                    {
                        var a = argv[0];
                        var b = arg[1];
                        result = pc =>
                        {
                            variables[b] = mgr.BossUnit.BossPartsListForBattle[(int) a()]
                                .IsBreak
                                ? 1
                                : 0;
                            return (pc + 1, true);
                        };
                        break;
                    }
                    case "waitl":
                    {
                        var a = argv[0];
                        result = pc =>
                        {
                            if (variables["cur_lframe"] < a()) return (pc, false);
                            logger.WriteLine($"waitl {a()} done.");
                            return (pc + 1, true);
                        };
                        break;
                    }
                    case "wait":
                    {
                        var a = argv[0];
                        result = pc =>
                        {
                            if (variables["cur_frame"] < a()) return (pc, false);
                            logger.WriteLine($"wait {a()} done.");
                            return (pc + 1, true);
                        };
                        break;
                    }
                    case "yield":
                    {
                        result = pc => (pc + 1, false);
                        break;
                    }
                    case "press":
                    {
                        var a = argv[0];
                        var b = argv[1];
                        result = pc =>
                        {
                            logger.WriteLine($"pressing {a()} for {b()}.");
                            pressing[(int) a()] = (int) (b() + variables["cur_lframe"]);
                            return (pc + 1, true);
                        };
                        break;
                    }
                    case "presson":
                    {
                        var a = argv[0];
                        result = pc =>
                        {
                            logger.WriteLine($"enable pressing {a()}.");
                            pressing[(int) a()] = 9999;
                            return (pc + 1, true);
                        };
                        break;
                    }
                    case "pressoff":
                    {
                        var a = argv[0];
                        result = pc =>
                        {
                            logger.WriteLine($"disable pressing {a()}.");
                            pressing.Remove((int) a());
                            return (pc + 1, true);
                        };
                        break;
                    }
                    case "pressub":
                    {
                        var a = argv[0];
                        result = pc =>
                        {
                            logger.WriteLine($"pressing ub {a()}.");
                            press.Add((int) a());
                            return (pc + 1, true);
                        };
                        break;
                    }
                    case "pause":
                    {
                        result = pc =>
                        {
                            logger.WriteLine($"pause button clicked.");
                            BattleHeaderController.Instance.OnClickPauseButton();
                            return (pc + 1, true);
                        };
                        break;
                    }
                    case "log":
                    {
                        result = pc =>
                        {
                            var sb = new StringBuilder();
                            sb.Append("value of ");
                            for (int i = 0; i < argv.Length; i++)
                            {
                                sb.Append($"{arg[i]} = {argv[i]()}");
                                if (i != argv.Length - 1) sb.Append(",");
                            }

                            logger.WriteLine(sb.ToString());
                            return (pc + 1, true);
                        };
                        break;
                    }
                    case "start":
                    {
                        var next = labels[arg[0]];
                        result = pc =>
                        {
                            logger.WriteLine($"starting new thread at {next}");
                            this.pc.Add(next);
                            return (pc + 1, true);
                        };
                        break;
                    }
                    case "eval":
                    {
                        var a = arg[1];
                        result = pc =>
                        {

                            var x = (uint) variables["seed"];
                            var y = 1812433253 * x + 1;
                            var z = 1812433253 * y + 1;
                            var w = 1812433253 * z + 1;
                            var rlist = new List<int>();

                            var splits = arg[0].Split('|');
                            var n = splits.Length;
                            var value = new float[n];
                            for (var i = 0; i < n; ++i)
                            {
                                var exp = splits[i].Split(':');
                                switch (exp[0])
                                {
                                    case "pure":
                                        value[i] = float.Parse(exp[1]);
                                        break;
                                    case "add":
                                        value[i] = value[int.Parse(exp[1])] + value[int.Parse(exp[2])];
                                        break;
                                    case "sub":
                                        value[i] = value[int.Parse(exp[1])] - value[int.Parse(exp[2])];
                                        break;
                                    case "mul":
                                        value[i] = value[int.Parse(exp[1])] * value[int.Parse(exp[2])];
                                        break;
                                    case "div":
                                        value[i] = value[int.Parse(exp[1])] / value[int.Parse(exp[2])];
                                        break;
                                    case "abs":
                                        value[i] = Mathf.Abs(value[int.Parse(exp[1])]);
                                        break;
                                    case "floor":
                                        value[i] = Mathf.FloorToInt(value[int.Parse(exp[1])]);
                                        break;
                                    case "ceil":
                                        value[i] = Mathf.CeilToInt(value[int.Parse(exp[1])]);
                                        break;
                                    case "max":
                                    {
                                        var a = float.Parse(exp[1]);
                                        var b = value[int.Parse(exp[2])];
                                        value[i] = Mathf.Max(a, b);
                                        break;
                                    }
                                    case "min":
                                    {
                                        var a = float.Parse(exp[1]);
                                        var b = value[int.Parse(exp[2])];
                                        value[i] = Mathf.Min(a, b);
                                        break;
                                    }
                                    case "barrier":
                                    {
                                        var main = float.Parse(exp[1]);
                                        var sub = float.Parse(exp[2]);
                                        var total = value[int.Parse(exp[3])];
                                        if (total > sub)
                                            value[i] = (Mathf.Log(((total - sub) / main + 1.0f)) * main + sub);
                                        else value[i] = total;
                                        break;
                                    }
                                    case "rnd":
                                    {
                                        var id = int.Parse(exp[1]);
                                        var bound = int.Parse(exp[2]);
                                        while (rlist.Count <= id)
                                        {
                                            uint t = x, s = w;
                                            x = y;
                                            y = z;
                                            z = w;
                                            t ^= t << 11;
                                            t ^= t >> 8;
                                            w = t ^ s ^ (s >> 19);
                                            rlist.Add((int) (w % 1000));
                                        }

                                        value[i] = rlist[id] <= bound ? 1 : 0;
                                        break;
                                    }
                                    default:
                                        logger.WriteLine($"eval: unrecognized operator: {exp[0]}");
                                        break;
                                }
                            }

                            variables[a] = value[n - 1];
                            return (pc + 1, true);
                        };
                        break;
                    }
                    case "end":
                        result = pc =>
                        {
                            logger.WriteLine($"thread terminated at {pc}");
                            return (-1, true);
                        };
                        break;
                    default:
                        logger.WriteLine($"unrecognized command: {op}");
                        result = pc => (pc + 1, true);
                        break;
                }

                compiled[ii - 1] = result;
            }

        }

        public void Update()
        {
            unitlist.Clear();
            foreach (var unit in mgr.UnitList)
                unitlist[unit.UnitId] = unit;
            foreach (var unit in mgr.EnemyList)
                unitlist[unit.UnitId] = unit;
            variables["cur_frame"] = mgr.FrameCount;
            variables["cur_lframe"] = BattleHeaderController.CurrentFrameCount;
            var n = pc.Count;
            var removing = new List<int>();
            for (var i = 0; i < n; ++i)
            {
                try
                {
                    for (;;)
                    {
                        if (pc[i] >= compiled.Length || pc[i] == -1)
                        {
                            logger.WriteLine($"thread {i} terminating for meeting eof.");
                            removing.Add(i);
                            break;
                        }
                        var (next, cont) = compiled[pc[i]](pc[i]);
                        pc[i] = next;
                        if (!cont) break;
                    }
                }
                catch (Exception e)
                {
                    logger.WriteLine($"thread terminating for exception:{e}");
                    removing.Add(i);
                }
            }

            for (var i = removing.Count - 1; i >= 0; --i)
                pc.RemoveAt(removing[i]);
        }

        public bool IsPressed(int unit)
        {
            return pressing.TryGetValue(unit, out var end) && end >= BattleHeaderController.CurrentFrameCount
                   || press.Contains(unit);
        }

        public void UbExecCallback(int unit)
        {
            press.Remove(unit);
        }

    }
}
