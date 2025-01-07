using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Text.RegularExpressions;
using Elements;
using Elements.Battle;
using ExcelDataReader;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OfficeOpenXml;
using OfficeOpenXml.Drawing;
using OfficeOpenXml.Style;
using PCRCaculator;
using PCRCaculator.Guild;
using SFB;
using TMPro;
using UnityEngine;
using Application = UnityEngine.Application;
using Color = System.Drawing.Color;
using UnitData = PCRCaculator.UnitData;

namespace ExcelHelper
{
    public class ImportClass
    {
        public string nickname;
        public string studentid;
        public string password;


        public ImportClass(string nickname, string studentid, string password)
        {
            this.nickname = nickname;
            this.studentid = studentid;
            this.password = password;
        }
    }
#if UNITY_EDITOR
    public class OutPutExcelEditor
    {
        [UnityEditor.MenuItem("PCRTools/生成Excel")]
        public static void OutPutExcel()
        {
            ExcelHelper.LoadDic();
            ExcelHelper.SaveExcel(1);
        }
        [UnityEditor.MenuItem("PCRTools/从Excel生成别名json")]
        public static void CreateNicName()
        {
           // ExcelHelper.ReadExcelNicName();
        }

    }
#endif

    public static class ExcelHelper// : MonoBehaviour
    {
        public static Dictionary<int, UnitSkillTimeData> DataDic = new Dictionary<int, UnitSkillTimeData>();
        public static GuildTimelineData TimelineData;
        public static List<Color> stateColors;
        const float DEFAULT_DPI = 96;
        public static void OutputGuildTimeLine(GuildTimelineData timelineData,string defaultName = "")
        {
            TimelineData = timelineData;
            AddStateColors();
            SaveExcel(2,defaultName);
        }
        private static void AddStateColors()
        {
            stateColors = new List<Color>();
            stateColors.Add(Color.FromArgb(178,178,178));
            stateColors.Add(Color.FromArgb(255,134,134));
            stateColors.Add(Color.FromArgb(255,173,95));
            stateColors.Add(Color.FromArgb(151,168,255));
            stateColors.Add(Color.FromArgb(190,190,190));
            stateColors.Add(Color.FromArgb(185,120,100));
            stateColors.Add(Color.FromArgb(135,135,135));
            stateColors.Add(Color.FromArgb(172,255,167));
            stateColors.Add(Color.FromArgb(215, 213, 255));

        }
        public static void LoadDic()
        {
            for (int unitId = 100101; unitId < 110001; unitId += 100)
            {
                string filePath = Application.dataPath + "/unitSkillTime/" + unitId + ".json";
                if (File.Exists(filePath))
                {
                    StreamReader sr = new StreamReader(filePath);
                    string jsonStr = sr.ReadToEnd();
                    sr.Close();
                    if (jsonStr != "")
                    {
                        UnitSkillTimeData skillTimeData = JsonConvert.DeserializeObject<UnitSkillTimeData>(jsonStr);
                        DataDic.Add(skillTimeData.unitId, skillTimeData);
                    }
                }
            }
        }
        public static string GetExcelPath()
        {
#if PLATFORM_ANDROID
            return AndroidTool.GetExcelPath();
#else
            var str = StandaloneFileBrowser.OpenFilePanel(
                "打开Excel", string.Empty, new ExtensionFilter[]
                {
                    new ExtensionFilter("", "xlsx"),
                    new ExtensionFilter("", "json"),
                }, false);
            if (str.Length > 0)
            {
                var file = str[0];
                file = file.Replace("\\", "/");
                return file;
            }
            throw new Exception("用户取消操作");
#endif
        }
        
        private static bool ReadJsonTimeLineData(string path, out GuildTimelineData guildTimelineData, SystemWindowMessage.configDelegate failedAction)
        {
            try
            {
                // 可能是一个box json
                var json = JObject.Parse(File.ReadAllText(path));
                if (json.ContainsKey("data_headers")) json = (JObject) json["data"];

                MainManager.Instance.PlayerSetting.playerLevel = (int) json["user_info"]["team_level"];

                var lovedict = json["read_story_ids"].ToObject<int[]>()
                    .Select(x =>
                        MainManager.Instance.unitStoryLoveDic.TryGetValue(x, out var y)
                            ? (x / 1000 * 100 + 1, y)
                            : (0, 0))
                    .GroupBy(p => p.Item1)
                    .ToDictionary(g => g.Key, g => g.Select(p => p.Item2).Max());

                foreach (var chara in json["unit_list"])
                {
                    var data = MainManager.Instance.unitDataDic[(int) chara["id"]];
                    data.rarity = (int)chara["unit_rarity"];
                    data.level = (int)chara["unit_level"];
                    data.rank = (int)chara["promotion_level"];

                    data.equipLevel = chara["equip_slot"]
                        .Select(e => (int) e["is_slot"] == 1 ? (int) e["enhancement_level"] : -1).ToArray();
                    data.uniqueEqLv = chara["unique_equip_slot"].SingleOrDefault()?.Value<int>("enhancement_level") ?? 0;
                    data.skillLevel = new[]
                    {
                        ((JArray) chara["union_burst"]).Count > 0 ? chara["union_burst"][0] : null,
                        ((JArray) chara["main_skill"]).Count > 0 ? chara["main_skill"][0] : null,
                        ((JArray) chara["main_skill"]).Count > 1 ? chara["main_skill"][1] : null,
                        ((JArray) chara["ex_skill"]).Count > 0 ? chara["ex_skill"][0] : null,
                    }.Select(token => token?.Value<int>("skill_level") ?? 0).ToArray();

                    foreach (var key in data.playLoveDic.Keys.ToArray())
                        if (lovedict.TryGetValue(key, out var val))
                            data.playLoveDic[key] = val;
                }

                guildTimelineData = null;
                return false;
            }
            catch (Exception e)
            {
            }

            try
            {
                var json = JToken.Parse(File.ReadAllText(path))["data"];
                var turn = (int) json["battle_log"]["data"]["lap_num"];
                var ids = Enumerable.Range(1, 5).Select(i => (int)json["battle_log"][$"unit_id_{i}"])
                    .OrderBy(t => MainManager.Instance.UnitRarityDic[t].detailData.searchAreaWidth)
                    .ThenBy(t => t).Append((int)json["battle_log"][$"unit_id_{6}"]).ToArray();
                var idinv = ids.Select((t, i) => (t, i)).ToDictionary(k => k.t, k => k.i);
                double frame = 0;
                var lovedict = json["battle_log"]["data"]["related_love"]
                    .ToObject<List<Dictionary<int, int>>>().Select(d => d.Single()).Select(p => (p.Key, p.Value))
                    .Concat(json["battle_log"]["data"]["units"].Values().Select(u => ((int) u["unit_id"], (int) u["love"])))
                    .Distinct().ToDictionary(u => u.Item1, u => u.Item2);
                guildTimelineData = new GuildTimelineData
                {
                    currentRandomSeed = (int)json["battle_log"]["seed"],
                    playerGroupData = new GuildPlayerGroupData
                    {
                        playerData = new AddedPlayerData
                        {
                            playrCharacters = json["battle_log"]["data"]["units"].Values().Select(t =>
                            {
                                var unit = new UnitData(id: (int) t["unit_id"])
                                {
                                    equipLevel = new int[]
                                    {
                                        (int) t["equip1"],
                                        (int) t["equip2"],
                                        (int) t["equip3"],
                                        (int) t["equip4"],
                                        (int) t["equip5"],
                                        (int) t["equip6"],
                                    },
                                    level = (int) t["level"],
                                    skillLevel = new int[]
                                    {
                                        (int) t["ub"],
                                        (int) t["main1"],
                                        (int) t["main2"],
                                        (int) t["ex"],
                                    },
                                    rarity = (int) t["battle_rarity"] == 0
                                        ? (int) t["rarity"]
                                        : (int) t["battle_rarity"],
                                    rank = (int) t["rank"],
                                    uniqueEqLv = (int) t["unique"]
                                };
                                foreach (var key in unit.playLoveDic.Keys.ToArray())
                                {
                                    unit.playLoveDic[key] = lovedict.TryGetValue(key, out var val) ? val : 0;
                                }

                                return unit;
                            }).OrderBy(t => idinv[t.unitId]).ToList()
                        },
                        currentGuildMonth = (int) json["battle_log"]["data"]["clan_battle_id"],
                        currentGuildEnemyNum = (int) json["battle_log"]["data"]["order_num"] - 1,
                        currentTurn = (turn > 3 ? 1 : 0) + (turn > 10 ? 1 : 0) + (turn > 35 ? 1 : 0) +
                                      (turn > 45 ? 1 : 0) +
                                      1,
                    },
                    UBExecTime = json["timeline"].Select(j => new
                            {unit = idinv[(int) j["unit_id"]], frame = (int) j["press"]})
                        .Select(t =>
                        {
                            if ((int) frame == t.frame)
                            {
                                frame += 0.01;
                                return (unit: t.unit, frame: frame);
                            }
                            else
                            {
                                frame = t.frame;
                                return (unit: t.unit, frame: t.frame);
                            }
                        })
                        .Aggregate(
                            new List<List<float>>()
                            {
                                new List<float>(), new List<float>(), new List<float>(), new List<float>(),
                                new List<float>(), new List<float>(),
                            }, (seed, t) =>
                            {
                                seed[t.unit].Add((float) t.frame);
                                return seed;
                            })
                };
                return true;
            }
            catch (Exception e)
            {
                MainManager.Instance.WindowConfigMessage(e.ToString(), null);
                guildTimelineData = null;
                return false;
            }
        }
        
        public static bool ReadExcelTimeLineData(out GuildTimelineData guildTimelineData,SystemWindowMessage.configDelegate failedAction = null,string path0=null)
        {
            string path = string.IsNullOrEmpty(path0) ? GetExcelPath() : path0;

            //打开windows框
            if (!string.IsNullOrEmpty(path))
            {
                if (path.EndsWith(".json"))
                {
                    return ReadJsonTimeLineData(path, out guildTimelineData, failedAction);
                }
                
                FileStream stream = null;
                try
                {
                     stream = File.Open(path,FileMode.Open, FileAccess.Read,FileShare.Read);

                }
                catch (IOException e)
                {
                    MainManager.Instance.WindowConfigMessage("读取错误，请保证excel文件没有被其他程序占用！", null);
                    guildTimelineData = new GuildTimelineData();
                    return false;
                }
                //IExcelDataReader excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
                using (IExcelDataReader excelReader = ExcelReaderFactory.CreateReader(stream))
                {
                    //var result = excelReader.AsDataSet();
                    //IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                    //excelReader.Initialize(stream);
                    /*do
                    {
                        // sheet name
                        Debug.Log(excelReader.Name);
                        while (excelReader.Read())
                        {
                            for (int i = 0; i < excelReader.FieldCount; i++)
                            {
                                string value = excelReader.IsDBNull(i) ? "" : excelReader.GetString(i);
                                Debug.Log(value);
                            }
                        }
                    } while (excelReader.NextResult());*/
                    do
                    {
                        if (excelReader.Name == "savePage") break;
                    } while (excelReader.NextResult());

                    excelReader.Read();
                    excelReader.Read();
                    string dataStr = excelReader.GetString(0);


                    if (dataStr != null && dataStr != "")
                    {
                        try
                        {
                            string jsonStr = MainManager.DecryptDES(dataStr);
                            guildTimelineData = JsonConvert.DeserializeObject<GuildTimelineData>(jsonStr);
                            return true;
                        }
                        catch (Exception e)
                        {
                            MainManager.Instance.WindowConfigMessage("发生错误：" + e.Message + "\n请手动输入存档数据！", failedAction);
                        }
                    }
                    else
                    {
                        MainManager.Instance.WindowConfigMessage("没有数据！\n请手动输入存档数据！", failedAction);
                    }
                    /*
                    if (result != null)
                    {
                        DataTable dataTable = result.Tables["savePage"];
                        if (dataTable != null)
                        {
                            int i = dataTable.Rows.Count;
                            int j = dataTable.Columns.Count;
                            string dataStr = dataTable.Rows[1][0].ToString();
                        }
                        else
                        {
                            MainManager.Instance.WindowConfigMessage("未找到savePage页！\n请手动输入存档数据！", failedAction);
                        }
                    }
                    else
                    {
                        MainManager.Instance.WindowConfigMessage("文件读取失败！\n请手动输入存档数据！", failedAction);
                    }*/
                }
            }
            else
            {
                MainManager.Instance.WindowConfigMessage("文件读取失败！\n请手动输入存档数据！", failedAction);
            }
            guildTimelineData = new GuildTimelineData();
            return false;
        }

        public static bool AsmExportEnabled =>
            Application.isEditor || File.Exists("patch_asm_exportaabbabab");
        public static bool AsmExportEnabled2 =>
            Application.isEditor || File.Exists("patch_lerist");
        public static void SaveExcel(int type,string defaultName = "")
        {
            bool isSuccess = true;
            string filePath = "";
#if PLATFORM_ANDROID

            string name = (defaultName == "" ? "ExportTimeLine" : defaultName);
            filePath = Application.persistentDataPath + "/" + name + ".xlsx";
#else


            var ststrr = StandaloneFileBrowser.SaveFilePanel(
                "保存Excel", string.Empty, defaultName, "xlsx");
            if (!string.IsNullOrEmpty(ststrr))
            {
                filePath = ststrr;
                filePath = filePath.Replace("\\", "/");
            }
            else return;

#endif
            //打开windows框
            //TODO

            //把文件路径格式替换一下
            //ofn.file = ofn.file.Replace("\\", "/");
            //Debug.Log(ofn.file);

            FileInfo newFile = new FileInfo(filePath);
            if (newFile.Exists)
            {
                newFile.Delete(); // ensures we create a new workbook
                newFile = new FileInfo(filePath);
            }

            switch (type)
            {
                case 1:
                    CreateUnitExecTimeExcel(newFile);
                    break;
                case 2:
                    CreateGuildTimeLineExcel(newFile);
                    break;

            }
            
#if PLATFORM_ANDROID
                MainManager.Instance.WindowConfigMessage("EXCEL保存路径为：" + filePath + "\n是否打开？",()=> AndroidTool.ShowExcelFile(filePath));
#endif

            // Script Export

            //if (File.Exists("patch_asm_exportaabbabab"))
            //    File.WriteAllText(filePath + ".asm", CreateAsmString());
                if (AsmExportEnabled)
                File.WriteAllText(filePath + ".asm", CreateAsmString());
        }

        private static (string, Func<UnitData, object>)[] conditions =
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

        private static string UnitDetail(UnitData info)
        {
            var lstring = string.Join(",", new[]
            {
                info.skillLevel[0], info.skillLevel[1], info.skillLevel[2], info.skillLevel[3]
            });
            string unitsLove = JsonConvert.SerializeObject(info.playLoveDic);
            return $"{info.unitId}|{info.level}|{info.rarity}|{info.love2}|{(int)info.rank}|{string.Concat(info.equipLevel.Select(l => l.ToString()[0]))}|{lstring}|{info.uniqueEqLv}|000|{unitsLove}";
        }

        private static string ToTime(long time, int limit)
        {
            return $"{time / 3600}:{(time / 60 % 60):D2}:{(time % 60):D2} ({limit - time:D4}";
        }

        private static string CreateAsmString()
        {
            int seed = TimelineData.currentRandomSeed;
            var units = TimelineData.playerGroupData.playerData.playrCharacters;
            var cdict = units.ToDictionary(u => u.unitId, u => new { name = u.GetNicName(), unit = u });
            var enemy = "boss";
            var msg = new StringBuilder();
            var src = new StringBuilder();

            const string header = "log seed\n";
            src.AppendLine(header);

            foreach (var pair in cdict)
            {
                var cond = string.Join(" ", conditions.Select(t => $"{t.Item1}={t.Item2(pair.Value.unit)}"));

                src.AppendLine($"//require {pair.Key}:{cond}");
            }

            src.AppendLine($"//require {TimelineData.playerGroupData.selectedEnemyIDs[0]}:");

            foreach (var tuple in cdict)
            {
                int unitId = tuple.Value.unit.unitId;
                string name = tuple.Value.name;
                switch (unitId)
                {
                    case 105701:
                        unitId = 170301;
                        break;
                    case 106101:
                        name = "矛衣未";
                        break;
                    case 104701:
                        name = "春511";
                        break;
                    case 125001:
                        name = "解511";
                        break;
                }
                src.AppendLine($"mov {unitId},{name}");
      }

            //var damage = dmgs.Where(pair => pair.Key <= 999999).Sum(pair => pair.Value);
            //msg.AppendLine($"对\"{enemy}\"（{seed}:{enemy}-{PCRBattle.Instance.enemyUnitid}）造成{damage}伤害：");
            msg.AppendLine(string.Join("\n", cdict.Select(c => $"{UnitDetail(c.Value.unit)}|{c.Value.name}")));
            msg.AppendLine($"boss: {TimelineData.playerGroupData.selectedEnemyIDs[0]}");
            msg.AppendLine("帧轴：");

            var skippingFrame = 0;
            cdict.Add(TimelineData.playerGroupData.selectedEnemyIDs[0], new { name = string.Empty, unit = (UnitData)null });

            var limit = 60 * 90;// __instance.GetMiliLimitTime() / 1000;

            foreach (var (unit, data) in TimelineData.allUnitStateChangeDic.SelectMany(pair =>
                             pair.Value.Select(dat => (pair.Key, dat)))
                         .Where(t => t.dat.changStateTo == UnitCtrl.ActionState.SKILL_1)
                         .OrderBy(t => t.dat.realFrameCount))
            {
                var frame = data.currentFrameCount;
                var unit_id = unit;
                if (unit_id <= 200000)
                {
                    var name = cdict[unit].name;
                    msg.AppendLine($"{ToTime(limit - data.currentFrameCount, limit)}:{data.realFrameCount})\t{name}");
                    src.AppendLine($"wait {data.realFrameCount}; pressub {name}; // lframe {data.currentFrameCount}");
                }
                else
                {
                    src.AppendLine($"//bossub");
                    msg.AppendLine($"{ToTime(limit - data.currentFrameCount, limit)}:{data.realFrameCount})\tboss");
                }
            }

            src.AppendLine(string.Join("\n", msg.ToString().Split('\n').Select(m => $"//{m}")));

            return src.ToString();
        }

        private static string CreatePyString()
        {
            var units = TimelineData.playerGroupData.playerData.playrCharacters;
            var cdict = units.ToDictionary(u => u.unitId, u => new { name = u.GetNicName(), unit = u });
            var enemy = "boss";
            var msg = new StringBuilder();
            var src = new StringBuilder();
            
            src.AppendLine("from autotimeline import *\nimport sys\nsys.path.append('.')\n");

            foreach (var tuple in cdict)
            {
                src.AppendLine($"print(\"calibrate for {tuple.Value.name}\");\nautopcr.calibrate(\"{tuple.Value.name}\")");
            }

            src.AppendLine("autopcr.setOffset(2, 0); # offset calibration");

            msg.AppendLine(string.Join("\n", cdict.Select(c => $"{UnitDetail(c.Value.unit)}|{c.Value.name}")));
            msg.AppendLine($"boss: {TimelineData.playerGroupData.selectedEnemyIDs[0]}");
            msg.AppendLine("帧轴：");

            var skippingFrame = 0;
            cdict.Add(TimelineData.playerGroupData.selectedEnemyIDs[0], new { name = string.Empty, unit = (UnitData)null });

            var limit = 60 * 90;// __instance.GetMiliLimitTime() / 1000;

            foreach (var (unit, data) in TimelineData.allUnitStateChangeDic.SelectMany(pair =>
                             pair.Value.Select(dat => (pair.Key, dat)))
                         .Where(t => t.dat.changStateTo == UnitCtrl.ActionState.SKILL_1)
                         .OrderBy(t => t.dat.realFrameCount))
            {
                var frame = data.currentFrameCount;
                var unit_id = unit;
                if (unit_id <= 200000)
                {
                    var name = cdict[unit].name;
                    msg.AppendLine($"{ToTime(limit - data.currentFrameCount, limit)}:{data.realFrameCount})\t{name}");
                    src.AppendLine($"autopcr.waitFrame({data.realFrameCount}); autopcr.multipress(\"{name}\", 5) # lframe {data.currentFrameCount}");
                }
                else
                {
                    src.AppendLine("# bossub");
                    msg.AppendLine($"{ToTime(limit - data.currentFrameCount, limit)}:{data.realFrameCount})\tboss");
                }
            }

            src.AppendLine(string.Join("\n", msg.ToString().Split('\n').Select(m => $"# {m}")));

            return src.ToString();
        }

        private static void CreateUnitExecTimeExcel(FileInfo newFile)
        {
            using (ExcelPackage package = new ExcelPackage(newFile))
            {
                // 添加一个sheet
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("技能时间");

                //添加一点测试数据

                worksheet.Cells["A1"].Value = "角色ID";
                worksheet.Cells["B1"].Value = "角色名字";
                worksheet.Cells["C1"].Value = "技能ID";
                worksheet.Cells["D1"].Value = "技能名字";
                worksheet.Cells["E1"].Value = "技能片段ID";
                worksheet.Cells["F1"].Value = "使用默认延时";
                worksheet.Cells["G1"].Value = "技能动画持续时间";
                worksheet.Cells["H1"].Value = "有依赖技能";
                worksheet.Cells["I1"].Value = "通过特效触发";
                worksheet.Cells["J1"].Value = "特效触发延时";
                worksheet.Cells["K1"].Value = "特效触发速度";
                worksheet.Cells["M1"].Value = "特效触发后延时";
                worksheet.Cells["N1"].Value = "触发时间1";
                worksheet.Cells["O1"].Value = "触发时间2";
                worksheet.Cells["P1"].Value = "触发时间3";
                worksheet.Cells["Q1"].Value = "触发时间4";
                worksheet.Cells["R1"].Value = "触发时间5";
                worksheet.Cells["S1"].Value = "触发时间6";
                worksheet.Cells["T1"].Value = "触发时间7";
                worksheet.Cells["U1"].Value = "触发时间8";
                worksheet.Cells["V1"].Value = "触发时间9";
                worksheet.Cells["W1"].Value = "触发时间10";
                worksheet.Cells["X1"].Value = "触发时间11";
                worksheet.Cells["Y1"].Value = "触发时间12";
                worksheet.Cells["Z1"].Value = "触发时间13";
                var unitRarityDic = MainManager.Instance.UnitRarityDic;
                var unitName_cn = MainManager.Instance.UnitName_cn;
                var skillNameAndDescribe_cn = MainManager.Instance.SkillNameAndDescribe_cn;
                var skillDataDic = MainManager.Instance.SkillDataDic;
                string[] execStr = { "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
                int i = 2;
                foreach (UnitSkillTimeData timeData in DataDic.Values)
                {
                    worksheet.Cells["A" + i].Value = timeData.unitId;
                    int id = timeData.unitId;
                    worksheet.Cells["B" + i].Value = unitRarityDic[id].unitName;
                    worksheet.Cells["F" + i].Value = timeData.useDefaultDelay ? "true" : "false";
                    if (unitName_cn.TryGetValue(id, out string namestr))
                    {
                        worksheet.Cells["B" + i].Value = namestr;
                    }
                    int[] skillids = new int[4] { 1, unitRarityDic[id].skillData.UB, unitRarityDic[id].skillData.skill_1, unitRarityDic[id].skillData.skill_2 };
                    for (int k = 0; k < 4; k++)
                    {
                        worksheet.Cells["C" + i].Value = skillids[k].ToString();
                        Dictionary<int, float[]> execDic = timeData.actionExecTime_UB;
                        switch (k)
                        {
                            case 0:
                                worksheet.Cells["G" + i].Value = timeData.spineTime_Attack;
                                break;
                            case 1:
                                worksheet.Cells["G" + i].Value = timeData.spineTime_UB;
                                break;
                            case 2:
                                worksheet.Cells["G" + i].Value = timeData.spineTime_MainSkill1;
                                execDic = timeData.actionExecTime_MainSkill1;
                                break;
                            case 3:
                                worksheet.Cells["G" + i].Value = timeData.spineTime_MainSkill2;
                                execDic = timeData.actionExecTime_MainSkill2;
                                break;
                        }
                        if (skillids[k] != 1)
                        {
                            worksheet.Cells["D" + i].Value = skillDataDic[skillids[k]].name;
                            if (skillNameAndDescribe_cn.TryGetValue(skillids[k], out string[] sr))
                            {
                                worksheet.Cells["D" + i].Value = sr[0];
                            }
                            int[] actionids = skillDataDic[skillids[k]].skillactions;
                            for (int m = 0; m < actionids.Length; m++)
                            {
                                if (actionids[m] > 0)
                                {


                                    worksheet.Cells["E" + i].Value = actionids[m];
                                    try
                                    {
                                        if (timeData.skillExecDependParentaction[skillids[k]][actionids[m]])
                                            worksheet.Cells["H" + i].Value = "true";

                                    }
                                    catch
                                    {
                                        worksheet.Cells["H" + i].Value = "ERROR!";
                                    }
                                    bool effectable = timeData.skillExecDependEffect[skillids[k]][actionids[m]];
                                    worksheet.Cells["I" + i].Value = effectable ? "true" : null;
                                    if (effectable)
                                    {
                                        if (timeData.actionEffectDic.TryGetValue(actionids[m], out List<EffectData> value))
                                        {
                                            worksheet.Cells["J" + i].Value = value[0].execTime[0];
                                            worksheet.Cells["K" + i].Value = value[0].moveRate;
                                            worksheet.Cells["M" + i].Value = value[0].hitDelay;
                                        }
                                    }
                                    float[] exTimes = execDic[actionids[m]];
                                    for (int j = 0; j < exTimes.Length; j++)
                                    {
                                        if (j < 13)
                                        {
                                            worksheet.Cells[execStr[j] + i].Value = exTimes[j];
                                        }
                                    }
                                    i++;
                                }
                            }
                        }
                        else
                        {
                            worksheet.Cells["D" + i].Value = "普攻";
                            worksheet.Cells["E" + i].Value = "0";
                            worksheet.Cells["H" + i].Value = timeData.skillExecDependParentaction[1][0] ? "true" : null;
                            bool effectable = timeData.skillExecDependEffect[1][0];
                            if (effectable)
                            {
                                if (timeData.actionEffectDic.TryGetValue(0, out List<EffectData> value))
                                {
                                    worksheet.Cells["J" + i].Value = value[0].execTime[0];
                                    worksheet.Cells["K" + i].Value = value[0].moveRate;
                                    worksheet.Cells["M" + i].Value = value[0].hitDelay;
                                }
                            }
                            worksheet.Cells["N" + i].Value = timeData.actionExecTime_Attack[1][0];
                            i++;
                        }
                    }
                }

                package.Save();
            }
        }

        private static (int lframe, int frame, string comment)[] ParseSetStatus()
        {
            const int N = 5;
            var statusChanging = new List<(int frame, int lframe, BattleManager.eSetStatus old, BattleManager.eSetStatus @new)>[N];
            var status = new BattleManager.eSetStatus[N];
            var result = new List<(int lframe, int frame, string comment)>();


            for (var i = 0; i < N; ++i)
            {
                statusChanging[i] = new List<(int, int, BattleManager.eSetStatus, BattleManager.eSetStatus)>();
                /*
                {
                    (-1, -1, BattleManager.eSetStatus.MUST_NOT, BattleManager.eSetStatus.MUST_NOT)
                };*/
                status[i] = BattleManager.eSetStatus.MUST_NOT;
            }

            var frame = 0;
            
            foreach (var (lframe, t) in BattleManager.Instance.setStatus)
            {
                for (var i = 0; i < N; ++i)
                {
                    if (status[i] != t[i])
                    {
                        statusChanging[i].Add((frame, lframe, status[i], t[i]));
                        status[i] = t[i];
                    }
                }

                ++frame;
            }

            var pos = new int[N];
            var statusChanging2 = new List<(int, BattleManager.eSetStatus[])>();

            for (var i = 0; i < N; ++i)
            {
                status[i] = BattleManager.eSetStatus.MUST_NOT;
                pos[i] = 0;
            }

            for (;;)
            {
                int minFrame = 9999, minPos = -1;
                for (var i = 0; i < N; ++i)
                {
                    if (pos[i] >= statusChanging[i].Count) continue;
                    
                    var t = statusChanging[i][pos[i]];
                    if (minFrame <= t.frame) continue;
                    
                    minFrame = statusChanging[i][pos[i]].frame;
                    minPos = i;
                }

                if (minPos == -1) break;
                var mint = statusChanging[minPos][pos[minPos]++];

                if (mint.@new == BattleManager.eSetStatus.MAY || mint.@new == status[minPos])
                    continue;
                
                var newStatus = new BattleManager.eSetStatus[N];

                for (var i = 0; i < N; ++i)
                {
                    newStatus[i] = status[i];

                    if (i == minPos)
                    {
                        newStatus[i] = mint.@new;
                        continue;
                    }

                    if (pos[i] >= statusChanging[i].Count) continue;
                    
                    var t = statusChanging[i][pos[i]];
                    if (t.old == BattleManager.eSetStatus.MAY)
                        newStatus[i] = t.@new;
                    else
                        newStatus[i] = t.old;
                }
                
                if (!newStatus.SequenceEqual(status))
                {
                    result.Add((mint.lframe, mint.frame,
                            string.Concat(newStatus.Reverse().Select(x => x switch
                            {
                                BattleManager.eSetStatus.MUST_NOT => 'x',
                                BattleManager.eSetStatus.MUST => 'o',
                                _ => throw new InvalidOperationException()
                            }))));
                    status = newStatus;
                }
            }

            return result.ToArray();
        }
        private static void CreateGuildTimeLineExcel(FileInfo newFile)
        {
            var setData = ParseSetStatus();
            
            using (ExcelPackage package = new ExcelPackage(newFile))
            {
                AddedPlayerData playerDatas = TimelineData.playerGroupData.playerData;
                ExcelWorksheet worksheet0 = package.Workbook.Worksheets.Add("轴模板");
                int[] backColotInt_1 = new int[3] { 253, 233, 217 };
                int[] backColotInt_2 = new int[3] { 250, 191, 143 };
                int[] lineColor = new int[3] { 151, 71, 6 };
                worksheet0.Cells[1, 12, 1, 13].Merge = true;
                worksheet0.Cells[1, 12].Value = "v" + Application.version;
                worksheet0.Cells[1, 14].Value = GuildManager.StaticsettingData.GetCurrentPlayerGroup().useLogBarrierNew
                    .GetDescription();
                worksheet0.Cells[1, 15].Value = "TPlmt" + MainManager.Instance.PlayerSetting.maxTPUpValue;
                worksheet0.Cells[2, 12, 2, 13].Merge = true;
                worksheet0.Cells[2, 12].Value = "角色数据版本";
                worksheet0.Cells[2, 14, 2, 15].Merge = true;
                worksheet0.Cells[2, 14].Value = MainManager.Instance.Version.CharacterVersionJP.ToString();
                worksheet0.Cells[3, 12, 3, 13].Merge = true;
                worksheet0.Cells[3, 12].Value = "Boss底层版本";
                worksheet0.Cells[3, 14, 3, 15].Merge = true;
                worksheet0.Cells[3, 14].Value = MainManager.Instance.Version.BossVersionJP.ToString();
                worksheet0.Cells[4, 12, 4, 13].Merge = true;
                worksheet0.Cells[4, 12].Value = "Boss数据版本";
                worksheet0.Cells[4, 14, 4, 15].Merge = true;
                worksheet0.Cells[4, 14].Value = MainManager.Instance.Version.BossVersionCN.ToString();
                worksheet0.Cells[5, 12, 5, 13].Merge = true;
                worksheet0.Cells[5, 12].Value = "是否为QA版本";
                worksheet0.Cells[5, 14, 5, 15].Merge = true;
                worksheet0.Cells[5, 14].Value = MainManager.Instance.Version.useQA? "‌✔" : "";
                worksheet0.Cells[6, 12, 6, 13].Merge = true;
                worksheet0.Cells[6, 12].Value = "使用日服Boss数据";
                worksheet0.Cells[6, 14, 6, 15].Merge = true;
                worksheet0.Cells[6, 14].Value = MainManager.Instance.Version.useJP? "‌✔" : "";
                worksheet0.Cells[7, 12, 7, 13].Merge = true;
                worksheet0.Cells[7, 12].Value = "使用最新Boss数据";
                worksheet0.Cells[7, 14, 7, 15].Merge = true;
                worksheet0.Cells[7, 14].Value = MainManager.Instance.Version.newAB ? "‌✔" : "";
                worksheet0.Cells[1, 2, 2, 2].Merge = true;
                worksheet0.MySetValue(1, 2,
                    MainManager.Instance.GuildBattleData.SettingData.GetCurrentBossDes(), 16, blod: true, backColor: backColotInt_1);
                worksheet0.Cells[1, 3, 2, 10].Merge = true;
                TimelineData.timeLineName= TimelineData.timeLineName.Substring(2, 1) == "#" ? TimelineData.timeLineName[4..] : TimelineData.timeLineName[3..];
                worksheet0.MySetValue(1, 3, TimelineData.timeLineName, 16, blod: true, backColor: backColotInt_1);
                worksheet0.Cells[1, 3, 2, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet0.Cells[3, 2, 5, 2].Merge = true;
                worksheet0.MySetValue(3, 2, TimelineData.exceptDamage + "w", 14, blod: true, fontColor: new int[3] { 26, 36, 242 }, backColor: backColotInt_1);
                worksheet0.InsertImage(TimelineData.charImages.Last(), 2, 1, false, 1, 3);
                worksheet0.MySetValue(6, 2, "等级", blod: true, backColor: backColotInt_2);
                worksheet0.MySetValue(7, 2, "RANK", blod: true, backColor: backColotInt_2);
                worksheet0.MySetValue(8, 2, "专武", blod: true, backColor: backColotInt_2);
                worksheet0.MySetValue(9, 2, "星级", blod: true, backColor: backColotInt_2);
                worksheet0.MySetValue(10, 2, "武/防/饰", blod: true, backColor: backColotInt_2);
                int count = 0;
                foreach (UnitData unitData in playerDatas.playrCharacters)
                {
                    worksheet0.Cells[3, 7-count, 5, 7-count].Merge = true;
                    worksheet0.MySetValue(3, 7 - count, unitData.GetUnitName(), backColor: backColotInt_1);
                    worksheet0.InsertImage(TimelineData.charImages[count], 2, 6 - count , false,1,3);
                    worksheet0.MySetValue(6, 7 - count, unitData.GetLevelDescribe(), backColor: backColotInt_1);
                    worksheet0.MySetValue(7, 7 - count, unitData.GetRankDescribe(), backColor: backColotInt_1);
                    worksheet0.MySetValue(8, 7 - count, unitData.GetUniqueEqLvDescribe(), backColor: backColotInt_1);
                    worksheet0.MySetValue(9, 7 - count, unitData.rarity , backColor: backColotInt_1);
                    var result = new List<string>();
                    for (int i = 0; i < unitData.exEquip.Length; i++)
                    {
                        int currentEquip = unitData.exEquip[i];
                        int currentLevel = unitData.exEquipLevel[i];
                        string exEquipStr = currentEquip.ToString();
                        string value = currentEquip == 0 ? "-" :
                            (exEquipStr.Length >= 3 && exEquipStr[exEquipStr.Length - 3] == '3' ? $"G{currentLevel}" :
                            (exEquipStr.Length >= 3 && exEquipStr[exEquipStr.Length - 3] == '2' ? $"S{currentLevel}" : "-"));
                        result.Add(value);
                    }
                    worksheet0.MySetValue(10, 7 - count, string.Join("/", result), backColor: backColotInt_1);
                    count++;
                }
                worksheet0.Cells[3, 8, 3, 10].Merge = true;
                worksheet0.MySetValue(3, 8, "备注", blod: true, backColor: backColotInt_2);
                worksheet0.Cells[4, 8, 10, 10].Merge = true;
                worksheet0.Cells[4, 8, 10, 10].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                // worksheet0.InsertImage(TimelineData.charImages.Last(), 2, 7, false,3,8);//boss头像
                worksheet0.MySetValue(11, 1, "帧数");
                worksheet0.MySetValue(11, 2, "秒数", blod: true, backColor: backColotInt_2);
                worksheet0.MySetValue(11, 3, "角色", blod: true, backColor: backColotInt_2);
                worksheet0.Cells[11, 4, 11, 6].Merge = true;
                worksheet0.MySetValue(11, 4, "操作", blod: true, backColor: backColotInt_2);
                worksheet0.MySetValue(11, 7, "伤害", blod: true, backColor: backColotInt_2);
                worksheet0.MySetValue(11, 8, "ub总伤", blod: true, backColor: backColotInt_2);
                worksheet0.Cells[11, 9, 11, 10].Merge = true;
                worksheet0.MySetValue(11, 9, "说明", blod: true, backColor: backColotInt_2);

                int currentLineNum = 12;

                List<UBDetail> UBList = TimelineData.uBDetails;
                //UBList.Sort((a, b) => a.UBTime - b.UBTime);
                var dmglist = GuildCalculator.Instance.dmglist
                    .Where(p => p.enabled).ToArray();

                var dmgcnt = new Dictionary<ProbEvent, int>();
                var rnd = new System.Random();
                var seedDict = new Dictionary<ProbEvent, int>();

                for (int i = 0; i < GuildManager.StaticsettingData.n2; ++i)
                {
                    var seed = rnd.Next();
                    FloatWithEx.SetState(seed);

                    var hash = rnd.Next();

                    foreach (var evt in dmglist)
                    {
                        if (evt.predict(hash))
                        {
                            if (!dmgcnt.ContainsKey(evt)) dmgcnt[evt] = 0;
                            ++dmgcnt[evt];
                            seedDict[evt] = seed;
                            break;
                        }
                    }
                }

                {
                    var curline = 0;
                    foreach (var (time, content) in dmgcnt
                                 .OrderBy(p => p.Key.frame)
                                 .Select(p => (p.Key.frame, new string[]
                                 {
                                     p.Key.frame.ToString(),
                                     $"{p.Value / (float) GuildManager.StaticsettingData.n2:P2}",
                                     $"seed={seedDict[p.Key]}",
                                     p.Key.unit,
                                     p.Key.description,
                                 })))
                    {
                        while (curline < UBList.Count && UBList[curline].UBTime < time) ++curline;
                        foreach (var (cont, i) in content.Select((cont, i) => (cont, i)))
                            worksheet0.MySetValue(11 + curline, 14 + i, cont, centre:false);
                        ++curline;
                    }

                }

                {
                    var curline = 0;
                    foreach (var (lframe, content) in setData
                                 .OrderBy(p => p.frame)
                                 .Select(p => (p.lframe, new string[]
                                 {
                                     p.lframe.ToString(),
                                     p.comment
                                 })))
                    {
                        while (curline < UBList.Count && UBList[curline].UBTime < lframe) ++curline;
                        foreach (var (cont, i) in content.Select((cont, i) => (cont, i)))
                            worksheet0.MySetValue(12 + curline, 12 + i, cont, centre:false);
                        ++curline;
                    }

                }
                foreach(var a in UBList)
                {
                    worksheet0.MySetValue(currentLineNum, 1, a.UBTime);
                    int second = (MyGameCtrl.Instance.tempData.SettingData.limitTime - a.UBTime / 60);
                    if (second >= 60)
                    {
                        second -= 60;
                        if (second >= 10)
                            worksheet0.MySetValue(currentLineNum, 2, "1:" + second);
                        else
                            worksheet0.MySetValue(currentLineNum, 2, "1:0" + second);
                    }
                    else
                        worksheet0.MySetValue(currentLineNum, 2, second);
                    if (a.isBossUB)
                    {
                        worksheet0.Cells[currentLineNum, 3, currentLineNum, 10].Merge = true;
                        worksheet0.MySetValue(currentLineNum, 3, "BOSS  UB",backColor:backColotInt_1);
                    }
                    else
                    {
                        worksheet0.Cells[currentLineNum, 9, currentLineNum, 10].Merge = true;
                        worksheet0.MySetValue(currentLineNum, 3, a.unitData.GetNicName());
                        worksheet0.Cells[currentLineNum, 4, currentLineNum, 6].Merge = true;
                        worksheet0.MySetValue(currentLineNum, 4, a.description);
                        worksheet0.Cells[currentLineNum, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        worksheet0.MySetValue(currentLineNum, 7, a.Damage , fontColor: a.Critical ? new int[3] { 255, 0, 0 } : new int[3] { 0, 0, 0 }, blod: a.Critical);
                        worksheet0.MySetValue(currentLineNum, 8, a.totalDamage, fontColor:  new int[3] { 102, 0, 204 }, blod:true);
          }
          currentLineNum++;
                }
                worksheet0.Cells[currentLineNum, 2, currentLineNum, 10].Merge = true;
                worksheet0.MySetValue(currentLineNum, 2, "TIME UP", backColor: backColotInt_1);
                using (ExcelRange r = worksheet0.Cells[1, 2, currentLineNum, 10])
                {
                    r.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    r.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    r.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    r.Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    Color color0 = Color.FromArgb(lineColor[0], lineColor[1], lineColor[2]);
                    r.Style.Border.Top.Color.SetColor(color0) ;
                    r.Style.Border.Bottom.Color.SetColor(color0);
                    r.Style.Border.Left.Color.SetColor(color0);
                    r.Style.Border.Right.Color.SetColor(color0);
                }



                // 添加一个sheet
                ExcelWorksheet worksheet1 = package.Workbook.Worksheets.Add("基础数据");
                string[] HeadNames = new string[24]
                {
                    "角色ID","角色名字","角色等级","角色星级","角色好感度","角色Rank",
                    "装备等级(左上)", "装备等级(右上)", "装备等级(左中)","装备等级(右中)","装备等级(左下)","装备等级(右下)",
                "UB技能等级","技能1等级","技能2等级","EX技能等级","专武等级","EX武器","EX武器等级","EX防具","EX防具等级","EX首饰","EX首饰等级","高级设置"};
                worksheet1.Cells[1, 1, 1, HeadNames.Length].Merge = true;//合并单元格(1行1列到1行6列)
                worksheet1.Cells["A1"].Style.Font.Size = 16; //字体大小
                worksheet1.Cells["A1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center; //对其方式
                worksheet1.Cells["A1"].Value = "角色基础参数"; //显示
                int lineNum = 2;
                for(int i = 0; i < HeadNames.Length; i++)
                {
                    worksheet1.Cells[lineNum, i + 1].Value = HeadNames[i];
                }
                lineNum++;
                foreach (UnitData unitData in playerDatas.playrCharacters)
                {
                    worksheet1.Cells[lineNum, 1].Value = unitData.unitId;
                    worksheet1.Cells[lineNum, 2].Value = unitData.GetUnitName();
                    worksheet1.Cells[lineNum, 3].Value = unitData.level;
                    worksheet1.Cells[lineNum, 4].Value = unitData.rarity;
                    worksheet1.Cells[lineNum, 5].Value = unitData.playLoveDic[unitData.unitId];//unitData.love;
                    worksheet1.Cells[lineNum, 6].Value = unitData.rank;
                    for(int i = 0; i < unitData.equipLevel.Length; i++)
                    {
                        if(unitData.equipLevel[i] == -1)
                            worksheet1.Cells[lineNum, 7 + i].Value = "未装备";
                        else
                            worksheet1.Cells[lineNum, 7 + i].Value = unitData.equipLevel[i];
                    }
                    for(int i = 0; i < unitData.skillLevel.Length; i++)
                    {
                        worksheet1.Cells[lineNum, 13 + i].Value = unitData.skillLevel[i];
                    }
                    worksheet1.Cells[lineNum, 17].Value = unitData.uniqueEqLv;
                    for (int i = 0; i < 3; i++)
                    {
                        worksheet1.Cells[lineNum, 18 + i * 2].Value = unitData.exEquip[i];
                        worksheet1.Cells[lineNum, 18 + i * 2 + 1].Value = unitData.exEquipLevel[i];
                    }
                    if (unitData.playLoveDic != null)
                    {
                        worksheet1.Cells[lineNum, 24].Value = JsonConvert.SerializeObject(unitData.playLoveDic);
                    }
                    lineNum++;
                }
                lineNum++;
                worksheet1.Cells[lineNum, 1, lineNum, HeadNames.Length].Merge = true;
                worksheet1.Cells[lineNum,1].Style.Font.Size = 16; 
                worksheet1.Cells[lineNum,1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet1.Cells[lineNum,1].Value = "BOSS详情";
                lineNum++;
                EnemyData enemyData = MyGameCtrl.Instance.tempData.guildEnemy[0];
                worksheet1.Cells[lineNum, 1].Value = enemyData.unit_id;
                worksheet1.Cells[lineNum, 2].Value = enemyData.detailData.unit_name;
                string str = "";
                switch (TimelineData.playerGroupData.currentTurn)
                {
                    case 1:
                        str = "一周目";
                        break;
                    case 2:
                        str = "二周目";
                        break;
                    case 3:
                        str = "三周目";
                        break;
                    case 4:
                        str = "四周目";
                        break;
                }
                worksheet1.Cells[lineNum, 3].Value = str;
                //worksheet1.Cells[lineNum, 4].Value = TimelineData.playerGroupData.isViolent ? "狂暴" : "普通";
                worksheet1.Cells[lineNum, 4].Value = "普通";
                worksheet1.Cells[lineNum, 6].Value = "随机数种子：";
                worksheet1.Cells[lineNum, 7].Value = TimelineData.currentRandomSeed;

                lineNum++;
                lineNum++;
                worksheet1.Cells[lineNum, 1, lineNum ,5].Merge = true;
                worksheet1.Cells[lineNum, 1].Style.Font.Size = 16;
                worksheet1.Cells[lineNum, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet1.Cells[lineNum, 1].Value = "角色UB时间";
                lineNum++;
                for(int i = 0; i < playerDatas.playrCharacters.Count; i++)
                {
                    worksheet1.Cells[lineNum, 1 + i].Value = playerDatas.playrCharacters[i].GetUnitName();
                }
                lineNum++;
                
                bool flag = true;
                int count0 = 0;
                List<List<float>> UBexecTimeList = TimelineData.UBExecTime;
                while (flag)
                {
                    flag = false;
                    for (int i = 0; i < playerDatas.playrCharacters.Count; i++)
                    {
                        if(UBexecTimeList[i]!=null && count0 < UBexecTimeList[i].Count)
                        {
                            worksheet1.Cells[lineNum, 1 + i].Value = (MyGameCtrl.Instance.tempData.SettingData.limitTime - UBexecTimeList[i][count0]/60.0f);
                            flag = true;
                        }
                    }
                    count0++;
                    lineNum++;
                }
                ExcelWorksheet worksheet2 = package.Workbook.Worksheets.Add("技能循环");
                lineNum = 1;
                worksheet2.Cells[lineNum, 1, lineNum, 20].Merge = true;
                worksheet2.Cells[lineNum, 1].Style.Font.Size = 16;
                worksheet2.Cells[lineNum, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet2.Cells[lineNum, 1].Value = "角色技能循环";
                lineNum++;
                foreach (var unit in TimelineData.allUnitStateChangeDic)
                {
                    lineNum++;
                    worksheet2.Cells[lineNum-1, 1, lineNum+1, 2].Merge = true;
                    if (unit.Key >= 300000)
                        worksheet2.Cells[lineNum-1, 1].Value = enemyData.detailData.unit_name;
                    else
                    {
                        var a00 = playerDatas.playrCharacters.Find(a => a.unitId == unit.Key);
                        string str00 = a00 == null ? "???" : a00.GetUnitName();
                        worksheet2.Cells[lineNum - 1, 1].Value = str00;
                    }
                    worksheet2.Cells[lineNum-1, 1].Style.Font.Size = 16;
                    worksheet2.Cells[lineNum-1, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet2.Cells[lineNum - 1, 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    int currentLoc = 3;
                    int lastFrameCount = 0;
                    // int lastVariant = 0;
                    foreach(var exec in unit.Value)
                    {
                        //int length = Mathf.RoundToInt((exec.currentFrameCount - lastFrameCount) / 60.0f);
                        int endLoc = Mathf.RoundToInt(exec.currentFrameCount / 30.0f)+3;
                        int length = endLoc - currentLoc;
                        if (length < 1 && exec.changStateFrom == UnitCtrl.ActionState.SKILL_1)
                        {
                            worksheet2.Cells[lineNum - 1, currentLoc].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            worksheet2.Cells[lineNum - 1, currentLoc].Style.Fill.PatternType = ExcelFillStyle.DarkGray;
                            worksheet2.Cells[lineNum - 1, currentLoc].Style.Fill.BackgroundColor.SetColor(stateColors[(int)exec.changStateFrom]);
                            worksheet2.Cells[lineNum - 1, currentLoc].Value = exec.changStateFrom.GetDescription();
                            lastFrameCount = exec.currentFrameCount;
                        }
                        else
                        {
                            var variantString = string.Empty;
                            if (exec.variant != 0)
                            {
                                if (exec.variant > 100)
                                    variantString = $"_SP{exec.variant % 10}";
                                else if (exec.variant > 10)
                                    variantString = $"_{(exec.variant - 1) % 10}+";
                                else
                                    variantString = $"_{exec.variant - 1}";
                            }
                            if (length < 1 && exec.currentFrameCount != lastFrameCount)
                            {
                                length = 1;
                            }
                            if (length >= 1)
                            {
                                worksheet2.Cells[lineNum, currentLoc, lineNum, currentLoc + length].Merge = true;
                                worksheet2.Cells[lineNum, currentLoc].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                worksheet2.Cells[lineNum, currentLoc].Style.Fill.PatternType = ExcelFillStyle.DarkGray;
                                worksheet2.Cells[lineNum, currentLoc].Style.Fill.BackgroundColor.SetColor(stateColors[(int)exec.changStateFrom]);
                                worksheet2.Cells[lineNum, currentLoc].Value = exec.changStateFrom.GetDescription() + variantString;
                                worksheet2.Cells[lineNum + 1, currentLoc].Value = lastFrameCount;
                                worksheet2.Cells[lineNum + 1, currentLoc].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                lastFrameCount = exec.currentFrameCount;
                                currentLoc += length + 1;
                            }
                        }

                        // lastVariant = exec.variant;
                    }
                    lineNum++;
                    lineNum++;
                }
                lineNum++;
                worksheet2.Cells[lineNum, 1, lineNum, 12].Merge = true;
                worksheet2.Cells[lineNum, 1].Style.Font.Size = 16;
                worksheet2.Cells[lineNum, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet2.Cells[lineNum, 1].Value = "角色技能循环详情";
                lineNum++;
                int charLong = 5;
                int detailAlong = 2;
                int detailBlong = 3;
                worksheet2.Cells[lineNum, 2, lineNum, 2+ charLong-1].Merge = true;
                worksheet2.Cells[lineNum, 2].Value = enemyData.detailData.unit_name;
                worksheet2.Cells[lineNum, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                for(int i = 0; i < 5; i++)
                {
                    if (i < playerDatas.playrCharacters.Count)
                    {
                        worksheet2.Cells[lineNum, 2+charLong*(i+1), lineNum, 2 + charLong * (i + 2)-1].Merge = true;
                        worksheet2.Cells[lineNum, 2 + charLong * (i + 1)].Value = playerDatas.playrCharacters[i].GetUnitName();
                        worksheet2.Cells[lineNum, 2 + charLong * (i + 1)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    }
                }
                lineNum++;
                List<TimeLineDataA> timedata = CreateTimeLineDataA(1);
                //Regex regex = new Regex(@"<[^>]*>",RegexOptions.IgnoreCase);
                //string regex = 
                foreach(var timeData in timedata)
                {
                    int pos = 2 + charLong * timeData.idx;
                    worksheet2.Cells[lineNum, 1].Value = timeData.frame;
                    //worksheet2.Cells[lineNum, pos].Value = timeData.describeA;//.Replace(/<[^>] +>/ g, "");
                    AddStringWithJudge(worksheet2, lineNum, pos, timeData.describeA);
                    /*if (timeData.describeA != null)
                    {
                        worksheet2.Cells[lineNum, pos].Value = regex.Replace(timeData.describeA, "");
                        if (timeData.describeA.Contains("暴击"))
                        {
                            worksheet2.Cells[lineNum, pos].Style.Font.Color.SetColor(System.Drawing.Color.Yellow);
                        }

                    }*/
                    worksheet2.Cells[lineNum, pos, lineNum, pos + detailAlong-1].Merge = true;
                    AddStringWithJudge(worksheet2, lineNum, pos + detailAlong, timeData.describeB);
                    //worksheet2.Cells[lineNum, pos + detailAlong].RichText.Add(timeData.describeB);
                    /*if (timeData.describeB != null)
                    {
                        worksheet2.Cells[lineNum, pos + detailAlong].Value = regex.Replace(timeData.describeB,"");
                        if (timeData.describeB.Contains("暴击"))
                        {
                            worksheet2.Cells[lineNum, pos + detailAlong].Style.Font.Color.SetColor(System.Drawing.Color.Yellow);
                        }
                    }*/
                    worksheet2.Cells[lineNum, pos + detailAlong, lineNum, pos + detailAlong + detailBlong - 1].Merge = true;

                    lineNum++;
                }
                void MyAction0(string headName,string head2,int detailType,int charLong0 = 3,int detailAlong0 = 1,int detailBlong0 = 2)
                {
                    ExcelWorksheet worksheet3 = package.Workbook.Worksheets.Add(headName);
                    lineNum = 1;
                    worksheet3.Cells[lineNum, 1, lineNum, 20].Merge = true;
                    worksheet3.Cells[lineNum, 1].Style.Font.Size = 16;
                    worksheet3.Cells[lineNum, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet3.Cells[lineNum, 1].Value = head2;
                    lineNum++;
                    //charLong = 3;
                    //detailAlong = 1;
                    //detailBlong = 2;
                    worksheet3.Cells[lineNum, 1].Value = "逻辑帧";
                    worksheet3.Cells[lineNum, 2].Value = "渲染帧";
                    worksheet3.Cells[lineNum, 3, lineNum, 3 + charLong0 - 1].Merge = true;
                    worksheet3.Cells[lineNum, 3].Value = enemyData.detailData.unit_name;
                    worksheet3.Cells[lineNum, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    if (detailType < 4||detailType==6)
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            if (i < playerDatas.playrCharacters.Count)
                            {
                                worksheet3.Cells[lineNum, 3 + charLong0 * (i + 1), lineNum, 3 + charLong0 * (i + 2) - 1].Merge = true;
                                worksheet3.Cells[lineNum, 3 + charLong0 * (i + 1)].Value = playerDatas.playrCharacters[i].GetUnitName();
                                worksheet3.Cells[lineNum, 3 + charLong0 * (i + 1)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            }
                        }
                    }
                    lineNum++;
                    timedata = CreateTimeLineDataA(detailType);
                    int currentFream0 = 0;
                    foreach (var timeData in timedata)
                    {
                        int pos = 3 + charLong0 * timeData.idx;
                        //if (timeData.frame != currentFream0)
                        //{
                            lineNum++;
                            currentFream0 = timeData.frame;
                            worksheet3.Cells[lineNum, 1].Value = timeData.frame;
                            worksheet3.Cells[lineNum, 2].Value = timeData.realFrameCount;
                        //}
                        //worksheet2.Cells[lineNum, pos].Value = timeData.describeA;//.Replace(/<[^>] +>/ g, "");
                        worksheet3.Cells[lineNum, pos].Value = (detailType==2||detailType==6)?timeData.valueA:timeData.valueB;
                        if (detailAlong0 > 1)
                            worksheet3.Cells[lineNum, pos, lineNum, pos + detailAlong0 - 1].Merge = true;
                        //worksheet2.Cells[lineNum, pos + detailAlong].RichText.Add(timeData.describeB);
                        if (detailBlong0 > 0)
                        {
                            AddStringWithJudge(worksheet3, lineNum, pos + detailAlong0, timeData.describeB);
                            //if (timeData.describeB != null)
                            //    worksheet3.Cells[lineNum, pos + detailAlong0].Value = regex.Replace(timeData.describeB, "");
                            if (detailBlong0 > 1)
                                worksheet3.Cells[lineNum, pos + detailAlong0, lineNum, pos + detailAlong0 + detailBlong0 - 1].Merge = true;
                        }
                    }
                    lineNum++;
                }
                MyAction0("HP变化", "角色HP", 2);
                MyAction0("TP变化", "角色TP", 3);
                MyAction0("Boss防御", "Boss防御变化", 4);
                MyAction0("Boss魔防", "Boss魔防变化", 5);
                MyAction0("角色伤害统计", "角色伤害统计", 6,1,1,0);
                ExcelWorksheet worksheet4 = package.Workbook.Worksheets.Add("savePage");
                lineNum = 1;
                worksheet4.Cells[lineNum, 1, lineNum, 20].Merge = true;
                worksheet4.Cells[lineNum, 1].Style.Font.Size = 16;
                worksheet4.Cells[lineNum, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet4.Cells[lineNum, 1].Value = "存档数据页，请勿修改此页面的任何数据，否则无法导入！";
                lineNum++;
                string TimeLineDataStr = JsonConvert.SerializeObject(TimelineData);
                string hideData = MainManager.EncryptDES(TimeLineDataStr);
                worksheet4.Cells[lineNum, 1, lineNum, 20].Merge = true;
                worksheet4.Cells[lineNum, 1].Style.Font.Size = 12;
                worksheet4.Cells[lineNum, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet4.Cells[lineNum, 1].Value = hideData;

                ExcelWorksheet worksheet5 = package.Workbook.Worksheets.Add("随机判定");
                lineNum = 1;
                worksheet5.Cells[lineNum, 1, lineNum, 20].Merge = true;
                worksheet5.Cells[lineNum, 1].Style.Font.Size = 16;
                worksheet5.Cells[lineNum, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet5.Cells[lineNum, 1].Value = "随机判定页面";
                lineNum++;
                worksheet5.Cells[lineNum, 1].Value = "id";
                worksheet5.Cells[lineNum, 2].Value = "帧数";
                worksheet5.Cells[lineNum, 3].Value = "源";
                worksheet5.Cells[lineNum, 4].Value = "目标";
                worksheet5.Cells[lineNum, 5].Value = "技能id";
                worksheet5.Cells[lineNum, 6].Value = "类型";
                worksheet5.Cells[lineNum, 7].Value = "结果";
                worksheet5.Cells[lineNum, 8].Value = "目标值";
                worksheet5.Cells[lineNum, 9].Value = "参数2";
                worksheet5.Cells[lineNum, 10, lineNum, 15].Merge = true;
                worksheet5.Cells[lineNum, 10].Value = "状态";
                worksheet5.Cells[lineNum, 16].Value = "seed";

                lineNum++;
                for (int i = 0; i < TimelineData.AllRandomList.Count; i++)
                {
                    var data = TimelineData.AllRandomList[i];
                    worksheet5.Cells[lineNum, 1].Value = data.id;
                    worksheet5.Cells[lineNum, 2].Value = data.frame;
                    worksheet5.Cells[lineNum, 3].Value = data.ownerID;
                    worksheet5.Cells[lineNum, 4].Value = data.targetID;
                    worksheet5.Cells[lineNum, 5].Value = data.actionID;
                    worksheet5.Cells[lineNum, 6].Value = data.type;
                    worksheet5.Cells[lineNum, 7].Value = data.randomResult/10;
                    worksheet5.Cells[lineNum, 8].Value = data.targetResult*100;
                    worksheet5.Cells[lineNum, 9].Value = data.criticalDamageRate;
                    worksheet5.Cells[lineNum, 10, lineNum, 15].Merge = true;
                    worksheet5.Cells[lineNum, 10].Value = data.GetDescribe();
                    if (data.JudgeColored())
                    {
                        worksheet5.Cells[lineNum, 1,lineNum,20].Style.Fill.PatternType = ExcelFillStyle.DarkGray;
                        worksheet5.Cells[lineNum, 1, lineNum, 20].Style.Fill.BackgroundColor.SetColor(stateColors[2]);

                    }
                    lineNum++;
                }

                package.Save();
            }
        }
        private static List<TimeLineDataA> CreateTimeLineDataA(int type)
        {
            List<TimeLineDataA> timeLines = new List<TimeLineDataA>();
            int idx = 0;
            switch (type)
            {
                case 1:
                    foreach (var data0 in TimelineData.allUnitStateChangeDic)
                    {
                        foreach (var data1 in data0.Value)
                        {
                            timeLines.Add(new TimeLineDataA(data1.currentFrameCount, data0.Key, idx, data1.GetMainDescribe(), data1.describe));
                        }
                        idx++;
                    }
                    idx = 0;
                    foreach (var data0 in TimelineData.allUnitSkillExecDic)
                    {
                        foreach (var data1 in data0.Value)
                        {
                            foreach (var data2 in data1.actionExecDatas)
                            {
                                timeLines.Add(new TimeLineDataA(data2.execTime, data0.Key, idx, data1.GetDescribeA(), data2.GetDescribe()));
                            }
                        }
                        idx++;
                    }
                    break;
                case 2:
                    foreach(var data0 in TimelineData.allUnitHPDic)
                    {
                        foreach(var data1 in data0.Value)
                        {
                            timeLines.Add(new TimeLineDataA(Mathf.RoundToInt(data1.xValue), data0.Key, idx,"", data1.describe,valueA:(int)data1.yValue, realFrameCount: data1.realFrameCount));
                        }
                        idx++;
                    }
                    break;
                case 3:
                    foreach (var data0 in TimelineData.allUnitTPDic)
                    {
                        foreach (var data1 in data0.Value)
                        {
                            timeLines.Add(new TimeLineDataA(Mathf.RoundToInt(data1.xValue), data0.Key, idx, "", data1.describe,valueB:data1.yValue, realFrameCount: data1.realFrameCount));
                        }
                        idx++;
                    }
                    break;
                case 4:
                    foreach(var data0 in TimelineData.bossDefChangeDic)
                    {
                        timeLines.Add(new TimeLineDataA(Mathf.RoundToInt(data0.xValue), 0, idx, "", data0.describe, valueB: data0.yValue, realFrameCount: data0.realFrameCount));
                    }
                    break;
                case 5:
                    foreach (var data0 in TimelineData.bossMgcDefChangeDic)
                    {
                        timeLines.Add(new TimeLineDataA(Mathf.RoundToInt(data0.xValue), 0, idx, "", data0.describe, valueB: data0.yValue, realFrameCount: data0.realFrameCount));
                    }
                    break;
                case 6:
                    idx = 1;
                    foreach(var data0 in TimelineData.playerUnitDamageDic)
                    {
                        foreach(var data1 in data0.Value)
                        {
                            timeLines.Add(new TimeLineDataA(Mathf.RoundToInt(data1.xValue), data0.Key, idx, "", "",valueA:(int)data1.yValue, realFrameCount: data1.realFrameCount));
                        }
                        idx++;
                    }
                    break;

            }
            timeLines.Sort((a, b) =>
            {
              if(type!=1)
              { 
                int frameComparison = a.realFrameCount.CompareTo(b.realFrameCount);
                return frameComparison;
              }
              else
              {
                int frameComparison = a.frame.CompareTo(b.frame);
                return frameComparison;
              }
            });
            return timeLines;
        }
        private static void AddStringWithJudge(ExcelWorksheet worksheet,int x,int y,string describe)
        {
            Regex regex = new Regex(@"<[^>]*>", RegexOptions.IgnoreCase);
            if (describe != null)
            {
                worksheet.Cells[x,y].Value = regex.Replace(describe, "");
                if (describe.Contains("暴击"))
                {
                    worksheet.Cells[x, y].Style.Font.Color.SetColor(Color.FromArgb(199,203,0));
                }
                if (describe.Contains("MISS"))
                {
                    worksheet.Cells[x, y].Style.Font.Color.SetColor(Color.FromArgb(255, 0, 0));
                }

            }

        }
/*
        public static void ReadExcelNicName()
        {
            OpenFileName ofn = new OpenFileName();
            ofn.structSize = Marshal.SizeOf(ofn);
            ofn.filter = "Excel Files(*.xlsx)\0*.xlsx\0";  //指定打开格式
            ofn.file = new string(new char[256]);
            ofn.maxFile = ofn.file.Length;
            ofn.fileTitle = new string(new char[64]);
            ofn.maxFileTitle = ofn.fileTitle.Length;
            //ofn.initialDir = UnityEngine.Application.dataPath;//默认路径
            ofn.title = "打开Excel";
            ofn.defExt = "xlsx";
            ofn.flags = 0x00080000 | 0x00001000 | 0x00000800 | 0x00000200 | 0x00000008;//OFN_EXPLORER|OFN_FILEMUSTEXIST|OFN_PATHMUSTEXIST| OFN_ALLOWMULTISELECT|OFN_NOCHANGEDIR
            //打开windows框
            if (DllTest.GetOpenFileName(ref ofn))
            {
                ofn.file = ofn.file.Replace("\\", "/");
                FileInfo info = new FileInfo(ofn.file);
                FileStream stream = info.Open(FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                DataSet result = excelReader.AsDataSet();
                stream.Close();
                DataTable nameTable = result.Tables["NameData"];
                Dictionary<int,string> unitNameMatch = new Dictionary<int, string>();
                int rows_0 = nameTable.Rows.Count;//获取行数
                for (int i = 1; i < rows_0; i++)
                {
                    if (nameTable.Rows[i][0] != null)
                    {
                        string idstr = nameTable.Rows[i][0].ToString();
                        int id = int.Parse(idstr);
                        string name = nameTable.Rows[i][1].ToString();
                        unitNameMatch.Add(id, name);
                    }
                }
                string filePath = Application.dataPath + "/Resources/Datas/UnitNickNameDic.json";

                string saveJsonStr = JsonConvert.SerializeObject(unitNameMatch);
                StreamWriter sw = new StreamWriter(filePath);
                sw.Write(saveJsonStr);
                sw.Close();
                Debug.Log("成功！" + filePath);

            }
        }*/
        public static void MySetValue(this ExcelWorksheet worksheet,int posx,int posy,object value,
            int size=11,bool centre = true,bool blod = false,int[] fontColor=null,int[] backColor=null)
        {
            worksheet.Cells[posx,posy].Style.Font.Size = size; //字体大小
            if(blod)
                worksheet.Cells[posx, posy].Style.Font.Bold = true; //字体大小
            if (fontColor != null && fontColor.Length >= 3)
                worksheet.Cells[posx, posy].Style.Font.Color.SetColor(Color.FromArgb(fontColor[0], fontColor[1], fontColor[2]));
            if (centre)
            {
                worksheet.Cells[posx, posy].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center; //对其方式
                worksheet.Cells[posx, posy].Style.VerticalAlignment = ExcelVerticalAlignment.Center; //对其方式

            }
            worksheet.Cells[posx, posy].Value = value; //显示
            if (backColor != null&&backColor.Length>=3)
            {
                worksheet.Cells[posx, posy].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Cells[posx, posy].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(backColor[0], backColor[1], backColor[2]));
            }
        }
        private static void MySetValue(this ExcelWorksheet worksheet, int posx, int posy, int value,
    int size = 11, bool centre = true, bool blod = false, int[] fontColor = null, int[] backColor = null)
        {
            worksheet.Cells[posx, posy].Style.Font.Size = size; //字体大小
            if (blod)
                worksheet.Cells[posx, posy].Style.Font.Bold = true; //字体大小
            if (fontColor != null && fontColor.Length >= 3)
                worksheet.Cells[posx, posy].Style.Font.Color.SetColor(Color.FromArgb(fontColor[0], fontColor[1], fontColor[2]));
            if (centre)
            {
                worksheet.Cells[posx, posy].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center; //对其方式
                worksheet.Cells[posx, posy].Style.VerticalAlignment = ExcelVerticalAlignment.Center; //对其方式

            }
            worksheet.Cells[posx, posy].Value = value; //显示
            if (backColor != null && backColor.Length >= 3)
            {
                worksheet.Cells[posx, posy].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Cells[posx, posy].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(backColor[0], backColor[1], backColor[2]));
            }
        }

        public static ExcelPicture InsertImage(this ExcelWorksheet worksheet, byte[] imageBytes, int rowNum, int columnNum, bool autofit, int widthCount,int hightCount)
        {
#if PLATFORM_ANDROID
            return null;
#else
            using (var image = (new MemoryStream(imageBytes)))
            {
                var picture = worksheet.Drawings.AddPicture(rowNum+"0"+columnNum, image
                    , ePictureType.Png);
                /*int adjustImageWidthInPix = cellColumnWidthInPix;
                int adjustImageHeightInPix = cellRowHeightInPix;
                if (autofit)
                {
                    //图片尺寸适应单元格
                    var adjustImageSize = GetAdjustImageSize(image, cellColumnWidthInPix, cellRowHeightInPix);
                    adjustImageWidthInPix = adjustImageSize.Item1;
                    adjustImageHeightInPix = adjustImageSize.Item2;
                }
                //设置为居中显示
                int columnOffsetPixels = (int)((cellColumnWidthInPix - adjustImageWidthInPix) / 2.0);
                int rowOffsetPixels = (int)((cellRowHeightInPix - adjustImageHeightInPix) / 2.0);
                picture.SetSize(adjustImageWidthInPix, adjustImageHeightInPix);
                picture.SetPosition(rowNum - 1, rowOffsetPixels, columnNum - 1, columnOffsetPixels);*/

                picture.ChangeCellAnchor(eEditAs.TwoCell);
                picture.From.Column = columnNum;
                picture.From.Row = rowNum;
                picture.To.Column = columnNum + widthCount;
                picture.To.Row = rowNum + hightCount;
                picture.From.RowOff = 0;
                picture.From.ColumnOff = 0;
                picture.To.RowOff = 0;
                picture.To.ColumnOff = -1;


                return picture;
            }
#endif
        }
        /*
        /// <summary>
        /// 获取自适应调整后的图片尺寸
        /// </summary>
        /// <param name="image"></param>
        /// <param name="cellColumnWidthInPix"></param>
        /// <param name="cellRowHeightInPix"></param>
        /// <returns>item1:调整后的图片宽度; item2:调整后的图片高度</returns>
        /// 
        private static Tuple<int, int> GetAdjustImageSize(Image image, int cellColumnWidthInPix, int cellRowHeightInPix)
        {
            int imageWidthInPix = image.Width;
            int imageHeightInPix = image.Height;
            //调整图片尺寸,适应单元格
            int adjustImageWidthInPix;
            int adjustImageHeightInPix;
            if (imageHeightInPix * cellColumnWidthInPix > imageWidthInPix * cellRowHeightInPix)
            {
                //图片高度固定,宽度自适应
                adjustImageHeightInPix = cellRowHeightInPix;
                double ratio = (1.0) * adjustImageHeightInPix / imageHeightInPix;
                adjustImageWidthInPix = (int)(imageWidthInPix * ratio);
            }
            else
            {
                //图片宽度固定,高度自适应
                adjustImageWidthInPix = cellColumnWidthInPix;
                double ratio = (1.0) * adjustImageWidthInPix / imageWidthInPix;
                adjustImageHeightInPix = (int)(imageHeightInPix * ratio);
            }
            return new Tuple<int, int>(adjustImageWidthInPix, adjustImageHeightInPix);
        }*/
    }
    public struct TimeLineDataA
    {
        public int frame;
        public int unitid;
        public int idx;
        public int valueA;
        public float valueB;
        public string describeA;
        public string describeB;
        public int realFrameCount;
        public TimeLineDataA(int frame, int unitid,int idx, string describeA, string describeB,int valueA = 0,float valueB = 0,int realFrameCount=0)
        {
            this.frame = frame;
            this.unitid = unitid;
            this.idx = idx;
            this.describeA = describeA;
            this.describeB = describeB;
            this.valueA = valueA;
            this.valueB = valueB;
            this.realFrameCount = realFrameCount;
        }
    }
}