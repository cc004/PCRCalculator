﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using Excel;
using ExcelDataReader;
using System.Data;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Newtonsoft0.Json;
using PCRCaculator;
using PCRCaculator.Guild;
using System.Text.RegularExpressions;
using System.Drawing;

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
            ExcelHelper.ReadExcelNicName();
        }

    }
#endif

    public static class ExcelHelper// : MonoBehaviour
    {
        public static Dictionary<int, PCRCaculator.UnitSkillTimeData> DataDic = new Dictionary<int, PCRCaculator.UnitSkillTimeData>();
        public static GuildTimelineData TimelineData;
        public static List<System.Drawing.Color> stateColors;
        const float DEFAULT_DPI = 96;
        public static void OutputGuildTimeLine(GuildTimelineData timelineData,string defaultName = "")
        {
            TimelineData = timelineData;
            AddStateColors();
            SaveExcel(2,defaultName);
        }
        private static void AddStateColors()
        {
            stateColors = new List<System.Drawing.Color>();
            stateColors.Add(System.Drawing.Color.FromArgb(178,178,178));
            stateColors.Add(System.Drawing.Color.FromArgb(255,134,134));
            stateColors.Add(System.Drawing.Color.FromArgb(255,173,95));
            stateColors.Add(System.Drawing.Color.FromArgb(151,168,255));
            stateColors.Add(System.Drawing.Color.FromArgb(190,190,190));
            stateColors.Add(System.Drawing.Color.FromArgb(185,120,100));
            stateColors.Add(System.Drawing.Color.FromArgb(135,135,135));
            stateColors.Add(System.Drawing.Color.FromArgb(172,255,167));
            stateColors.Add(System.Drawing.Color.FromArgb(215, 213, 255));

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
                        PCRCaculator.UnitSkillTimeData skillTimeData = JsonConvert.DeserializeObject<PCRCaculator.UnitSkillTimeData>(jsonStr);
                        DataDic.Add(skillTimeData.unitId, skillTimeData);
                    }
                }
            }
        }

        public static bool ReadExcelTimeLineData(out GuildTimelineData guildTimelineData,SystemWindowMessage.configDelegate failedAction = null)
        {
            if(Application.platform == RuntimePlatform.Android)
            {
                guildTimelineData = new GuildTimelineData();
                failedAction?.Invoke();
                return false;
            }

            OpenFileName ofn = new OpenFileName();

            ofn.structSize = Marshal.SizeOf(ofn);

            //ofn.filter = "All Files\0*.*\0\0";
            //ofn.filter = "Image Files(*.jpg;*.png)\0*.jpg;*.png\0";
            //ofn.filter = "Txt Files(*.txt)\0*.txt\0";

            //ofn.filter = "Word Files(*.docx)\0*.docx\0";
            //ofn.filter = "Word Files(*.doc)\0*.doc\0";
            //ofn.filter = "Word Files(*.doc:*.docx)\0*.doc:*.docx\0";

            //ofn.filter = "Excel Files(*.xls)\0*.xls\0";
            ofn.filter = "Excel Files(*.xlsx)\0*.xlsx\0";  //指定打开格式
                                                           //ofn.filter = "Excel Files(*.xls:*.xlsx)\0*.xls:*.xlsx\0";
                                                           //ofn.filter = "Excel Files(*.xlsx:*.xls)\0*.xlsx:*.xls\0";

            ofn.file = new string(new char[256]);

            ofn.maxFile = ofn.file.Length;

            ofn.fileTitle = new string(new char[64]);

            ofn.maxFileTitle = ofn.fileTitle.Length;

            ofn.initialDir = UnityEngine.Application.dataPath;//默认路径

            ofn.title = "打开Excel";

            ofn.defExt = "xlsx";

            //注意 一下项目不一定要全选 但是0x00000008项不要缺少
            ofn.flags = 0x00080000 | 0x00001000 | 0x00000800 | 0x00000200 | 0x00000008;//OFN_EXPLORER|OFN_FILEMUSTEXIST|OFN_PATHMUSTEXIST| OFN_ALLOWMULTISELECT|OFN_NOCHANGEDIR


            //打开windows框
            if (DllTest.GetOpenFileName(ofn))
            {
                //TODO

                //把文件路径格式替换一下
                ofn.file = ofn.file.Replace("\\", "/");

                //string url = ofn.file;
                //WWW www = new WWW(url);

                ////写到临时文件
                //while (!www.isDone)
                //{
                //    if (File.Exists(Application.dataPath + "/temp.xlsx"))
                //    {
                //        File.Delete(Application.dataPath + "/temp.xlsx");
                //    }

                //    byte[] fileBytes = www.bytes;

                //    FileStream fs = new FileStream(Application.dataPath + "/temp.xlsx", FileMode.Create);
                //    fs.Write(fileBytes, 0, fileBytes.Length);
                //    fs.Flush();
                //    //每次读取文件后都要记得关闭文件
                //    fs.Close();
                //}
                //FileStream stream = File.Open(ofn.file, FileMode.Open, FileAccess.Read, FileShare.Read);

                // CreateOpenXmlReader用于读取Excel2007版本及以上的文件

                //IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);

                //DataSet result = ExcelReaderFactory.CreateOpenXmlReader(stream).AsDataSet();

                //excelReader.Close();

                //Debug.Log(ofn.file);
                //FileInfo info = new FileInfo(Application.dataPath + "/temp.xlsx");
                //FileInfo info = new FileInfo(ofn.file);
                FileStream stream = null;
                try
                {
                     stream = File.Open(ofn.file,FileMode.Open, FileAccess.Read,FileShare.Read);

                }
                catch (IOException e)
                {
                    MainManager.Instance.WindowConfigMessage("读取错误，请保证excel文件没有被其他程序占用！", null, null);
                    guildTimelineData = new GuildTimelineData();
                    return false;
                }
                //IExcelDataReader excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
                using (ExcelDataReader.IExcelDataReader excelReader = ExcelDataReader.ExcelReaderFactory.CreateReader(stream))
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
                    var result = excelReader.AsDataSet();
                    if (result != null)
                    {
                        DataTable dataTable = result.Tables["savePage"];
                        if (dataTable != null)
                        {
                            int i = dataTable.Rows.Count;
                            int j = dataTable.Columns.Count;
                            string dataStr = dataTable.Rows[1][0].ToString();
                            if (dataStr != null && dataStr != "")
                            {
                                try
                                {
                                    string jsonStr = MainManager.DecryptDES(dataStr);
                                    guildTimelineData = JsonConvert.DeserializeObject<GuildTimelineData>(jsonStr);
                                    return true;
                                }
                                catch (System.Exception e)
                                {
                                    MainManager.Instance.WindowConfigMessage("发生错误：" + e.Message + "\n请手动输入存档数据！", failedAction, null);
                                }
                            }
                            else
                            {
                                MainManager.Instance.WindowConfigMessage("没有数据！\n请手动输入存档数据！", failedAction, null);
                            }
                        }
                        else
                        {
                            MainManager.Instance.WindowConfigMessage("未找到savePage页！\n请手动输入存档数据！", failedAction, null);
                        }
                    }
                    else
                    {
                        MainManager.Instance.WindowConfigMessage("文件读取失败！\n请手动输入存档数据！", failedAction, null);
                    }
                }
            }
            else
            {
                MainManager.Instance.WindowConfigMessage("文件读取失败！\n请手动输入存档数据！", failedAction, null);
            }
            guildTimelineData = new GuildTimelineData();
            return false;

        }


        public static void SaveExcel(int type,string defaultName = "")
        {
            bool isSuccess = true;
            string filePath = "";
            if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer)
            {
                OpenFileName ofn = new OpenFileName();

                ofn.structSize = Marshal.SizeOf(ofn);

                //ofn.filter = "All Files\0*.*\0\0";
                //ofn.filter = "Image Files(*.jpg;*.png)\0*.jpg;*.png\0";
                //ofn.filter = "Txt Files(*.txt)\0*.txt\0";

                //ofn.filter = "Word Files(*.docx)\0*.docx\0";
                //ofn.filter = "Word Files(*.doc)\0*.doc\0";
                //ofn.filter = "Word Files(*.doc:*.docx)\0*.doc:*.docx\0";

                //ofn.filter = "Excel Files(*.xls)\0*.xls\0";
                ofn.filter = "Excel Files(*.xlsx)\0*.xlsx\0";  //指定打开格式
                                                               //ofn.filter = "Excel Files(*.xls:*.xlsx)\0*.xls:*.xlsx\0";
                                                               //ofn.filter = "Excel Files(*.xlsx:*.xls)\0*.xlsx:*.xls\0";

                ofn.file = new string(new char[256]);

                ofn.maxFile = ofn.file.Length;

                ofn.fileTitle = new string(new char[64]);

                ofn.maxFileTitle = ofn.fileTitle.Length;

                //ofn.fileTitle = "B1狼狗智克yls200w";

                ofn.initialDir = UnityEngine.Application.dataPath;//默认路径

                ofn.title = "选择保存路径";

                ofn.defExt = "xlsx";
                ofn.file = defaultName;
                //注意 一下项目不一定要全选 但是0x00000008项不要缺少
                ofn.flags = 0x00080000 | 0x00001000 | 0x00000800 | 0x00000200 | 0x00000008;//OFN_EXPLORER|OFN_FILEMUSTEXIST|OFN_PATHMUSTEXIST| OFN_ALLOWMULTISELECT|OFN_NOCHANGEDIR

                isSuccess = DllTest.GetSaveFileName(ofn);
                filePath = ofn.file.Replace("\\", "/");

            }
            else if(Application.platform == RuntimePlatform.Android)
            {
                string name = (defaultName == "" ? "ExportTimeLine" : defaultName);
                filePath = Application.persistentDataPath + "/" + name + ".xlsx";
            }
            //打开windows框
            if (isSuccess)
            {
                //TODO

                //把文件路径格式替换一下
                //ofn.file = ofn.file.Replace("\\", "/");
                //Debug.Log(ofn.file);

                FileInfo newFile = new FileInfo(filePath);
                if (newFile.Exists)
                {
                    newFile.Delete();  // ensures we create a new workbook
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
                if (Application.platform == RuntimePlatform.Android)
                {
                    MainManager.Instance.WindowConfigMessage("EXCEL保存路径为：" + filePath + "\n请自行前往查看！", null, null);
                }



            }
            
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
                Dictionary<int, UnitRarityData> unitRarityDic = MainManager.Instance.UnitRarityDic;
                Dictionary<int, string> unitName_cn = MainManager.Instance.UnitName_cn;
                Dictionary<int, string[]> skillNameAndDescribe_cn = MainManager.Instance.SkillNameAndDescribe_cn;
                Dictionary<int, SkillData> skillDataDic = MainManager.Instance.SkillDataDic;
                string[] execStr = new string[] { "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
                int i = 2;
                foreach (PCRCaculator.UnitSkillTimeData timeData in DataDic.Values)
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
        private static void CreateGuildTimeLineExcel(FileInfo newFile)
        {
            using (ExcelPackage package = new ExcelPackage(newFile))
            {
                AddedPlayerData playerDatas = TimelineData.playerGroupData.playerData;
                ExcelWorksheet worksheet0 = package.Workbook.Worksheets.Add("轴模板");
                int[] backColotInt_1 = new int[3] { 253, 233, 217 };
                int[] backColotInt_2 = new int[3] { 250, 191, 143 };
                int[] lineColor = new int[3] { 151, 71, 6 };
                worksheet0.Cells[1, 2, 2, 9].Merge = true;
                worksheet0.MySetValue(1, 2, TimelineData.timeLineName, 16, blod: true, backColor: backColotInt_1);
                worksheet0.MySetValue(3, 2,
                    MainManager.Instance.GuildBattleData.SettingData.GetCurrentBossDes(),12, blod: true, fontColor:new int[3] { 226,107,10}, backColor: backColotInt_1);
                worksheet0.MySetValue(4, 2, TimelineData.exceptDamage + "w", blod: true, fontColor: new int[3] { 26, 36, 242 }, backColor: backColotInt_1);
                worksheet0.MySetValue(5, 2, TimelineData.backDamage + "w", blod: true, backColor: backColotInt_1);
                worksheet0.MySetValue(6, 2, "等级", blod: true, backColor: backColotInt_2);
                worksheet0.MySetValue(7, 2, "RANK", blod: true, backColor: backColotInt_2);
                worksheet0.MySetValue(8, 2, "星级", blod: true, backColor: backColotInt_2);
                int count = 0;
                foreach (UnitData unitData in playerDatas.playrCharacters)
                {
                    worksheet0.Cells[3, 7-count, 5, 7-count].Merge = true;
                    worksheet0.MySetValue(3, 7 - count, unitData.GetUnitName(), backColor: backColotInt_1);
                    worksheet0.InsertImage(TimelineData.charImages[count], 3, 7 - count, false,1,3);
                    worksheet0.MySetValue(6, 7 - count, unitData.GetLevelDescribe(), backColor: backColotInt_1);
                    worksheet0.MySetValue(7, 7 - count, unitData.GetRankDescribe(), backColor: backColotInt_1);
                    worksheet0.MySetValue(8, 7 - count, unitData.rarity , backColor: backColotInt_1);
                    count++;
                }
                worksheet0.Cells[3, 8, 8, 9].Merge = true;
                worksheet0.InsertImage(TimelineData.charImages[5], 3, 8, false,1.8f,5);
                worksheet0.MySetValue(9, 1, "帧数");
                worksheet0.MySetValue(9, 2, "秒数", blod: true, backColor: backColotInt_2);
                worksheet0.MySetValue(9, 3, "角色", blod: true, backColor: backColotInt_2);
                worksheet0.MySetValue(9, 4, "操作", blod: true, backColor: backColotInt_2);
                worksheet0.MySetValue(9, 5, "伤害", blod: true, backColor: backColotInt_2);
                worksheet0.Cells[9, 6, 9, 9].Merge = true;
                worksheet0.MySetValue(9, 6, "说明", blod: true, backColor: backColotInt_2);

                int currentLineNum = 10;

                List<UBDetail> UBList = TimelineData.uBDetails;
                UBList.Sort((a, b) => a.UBTime - b.UBTime);
                foreach(var a in UBList)
                {
                    worksheet0.MySetValue(currentLineNum, 1, a.UBTime);
                    if (a.isBossUB)
                    {
                        worksheet0.Cells[currentLineNum, 2, currentLineNum, 9].Merge = true;
                        worksheet0.MySetValue(currentLineNum, 2, "BOSS  UB",backColor:backColotInt_1);
                    }
                    else
                    {
                        worksheet0.Cells[currentLineNum, 6, currentLineNum, 9].Merge = true;
                        int second = (90 - a.UBTime / 60);
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
                        worksheet0.MySetValue(currentLineNum, 3, a.unitData.GetNicName());
                        worksheet0.MySetValue(currentLineNum, 5, a.Damage , fontColor: a.Critical ? new int[3] { 255, 0, 0 } : new int[3] { 0, 0, 0 });
                    }
                    currentLineNum++;
                }
                worksheet0.Cells[currentLineNum, 2, currentLineNum, 9].Merge = true;
                worksheet0.MySetValue(currentLineNum, 2, "TIME UP", backColor: backColotInt_1);
                using (ExcelRange r = worksheet0.Cells[1, 2, currentLineNum, 9])
                {
                    r.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    r.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    r.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    r.Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    System.Drawing.Color color0 = System.Drawing.Color.FromArgb(lineColor[0], lineColor[1], lineColor[2]);
                    r.Style.Border.Top.Color.SetColor(color0) ;
                    r.Style.Border.Bottom.Color.SetColor(color0);
                    r.Style.Border.Left.Color.SetColor(color0);
                    r.Style.Border.Right.Color.SetColor(color0);
                }



                // 添加一个sheet
                ExcelWorksheet worksheet1 = package.Workbook.Worksheets.Add("基础数据");
                string[] HeadNames = new string[18]
                {
                    "角色ID","角色名字","角色等级","角色星级","角色好感度","角色Rank",
                    "装备等级(左上)", "装备等级(右上)", "装备等级(左中)","装备等级(右中)","装备等级(左下)","装备等级(右下)",
                "UB技能等级","技能1等级","技能2等级","EX技能等级","专武等级","高级设置"};
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
                    worksheet1.Cells[lineNum, 5].Value = unitData.love;
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
                    if (unitData.playLoveDic != null)
                    {
                        worksheet1.Cells[lineNum, 18].Value = JsonConvert.SerializeObject(unitData.playLoveDic);
                    }
                    lineNum++;
                }
                lineNum++;
                worksheet1.Cells[lineNum, 1, lineNum, HeadNames.Length].Merge = true;
                worksheet1.Cells[lineNum,1].Style.Font.Size = 16; 
                worksheet1.Cells[lineNum,1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet1.Cells[lineNum,1].Value = "BOSS详情";
                lineNum++;
                EnemyData enemyData = Elements.MyGameCtrl.Instance.tempData.guildEnemy;
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
                            worksheet1.Cells[lineNum, 1 + i].Value = (90.0f- UBexecTimeList[i][count0]/60.0f);
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
                    foreach(var exec in unit.Value)
                    {
                        //int length = Mathf.RoundToInt((exec.currentFrameCount - lastFrameCount) / 60.0f);
                        int endLoc = Mathf.RoundToInt(exec.currentFrameCount / 30.0f)+3;
                        int length = endLoc - currentLoc;
                        if (length < 1 && exec.changStateFrom == Elements.UnitCtrl.ActionState.SKILL_1)
                        {
                            worksheet2.Cells[lineNum - 1, currentLoc].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            worksheet2.Cells[lineNum - 1, currentLoc].Style.Fill.PatternType = ExcelFillStyle.DarkGray;
                            worksheet2.Cells[lineNum - 1, currentLoc].Style.Fill.BackgroundColor.SetColor(stateColors[(int)exec.changStateFrom]);
                            worksheet2.Cells[lineNum - 1, currentLoc].Value = exec.changStateFrom.GetDescription();
                            lastFrameCount = exec.currentFrameCount;
                        }
                        else
                        {
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
                                worksheet2.Cells[lineNum, currentLoc].Value = exec.changStateFrom.GetDescription();
                                worksheet2.Cells[lineNum + 1, currentLoc].Value = lastFrameCount;
                                worksheet2.Cells[lineNum + 1, currentLoc].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                lastFrameCount = exec.currentFrameCount;
                                currentLoc += length + 1;
                            }
                        }
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
                    worksheet3.Cells[lineNum, 2, lineNum, 2 + charLong0 - 1].Merge = true;
                    worksheet3.Cells[lineNum, 2].Value = enemyData.detailData.unit_name;
                    worksheet3.Cells[lineNum, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    if (detailType < 4||detailType==6)
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            if (i < playerDatas.playrCharacters.Count)
                            {
                                worksheet3.Cells[lineNum, 2 + charLong0 * (i + 1), lineNum, 2 + charLong0 * (i + 2) - 1].Merge = true;
                                worksheet3.Cells[lineNum, 2 + charLong0 * (i + 1)].Value = playerDatas.playrCharacters[i].GetUnitName();
                                worksheet3.Cells[lineNum, 2 + charLong0 * (i + 1)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            }
                        }
                    }
                    lineNum++;
                    timedata = CreateTimeLineDataA(detailType);
                    int currentFream0 = 0;
                    foreach (var timeData in timedata)
                    {
                        int pos = 2 + charLong0 * timeData.idx;
                        //if (timeData.frame != currentFream0)
                        //{
                            lineNum++;
                            currentFream0 = timeData.frame;
                            worksheet3.Cells[lineNum, 1].Value = timeData.frame;
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
                            timeLines.Add(new TimeLineDataA(Mathf.RoundToInt(data1.xValue * 5400), data0.Key, idx,"", data1.describe,valueA:data1.hp));
                        }
                        idx++;
                    }
                    break;
                case 3:
                    foreach (var data0 in TimelineData.allUnitTPDic)
                    {
                        foreach (var data1 in data0.Value)
                        {
                            timeLines.Add(new TimeLineDataA(Mathf.RoundToInt(data1.xValue * 5400), data0.Key, idx, "", data1.describe,valueB:data1.yValue*1000));
                        }
                        idx++;
                    }
                    break;
                case 4:
                    foreach(var data0 in TimelineData.bossDefChangeDic)
                    {
                        timeLines.Add(new TimeLineDataA(Mathf.RoundToInt(data0.xValue * 5400), 0, idx, "", data0.describe, valueB: data0.yValue));
                    }
                    break;
                case 5:
                    foreach (var data0 in TimelineData.bossMgcDefChangeDic)
                    {
                        timeLines.Add(new TimeLineDataA(Mathf.RoundToInt(data0.xValue * 5400), 0, idx, "", data0.describe, valueB: data0.yValue));
                    }
                    break;
                case 6:
                    idx = 1;
                    foreach(var data0 in TimelineData.playerUnitDamageDic)
                    {
                        foreach(var data1 in data0.Value)
                        {
                            timeLines.Add(new TimeLineDataA(Mathf.RoundToInt(data1.xValue * 5400), data0.Key, idx, "", "",valueA:(int)data1.yValue));
                        }
                        idx++;
                    }
                    break;

            }
            timeLines.Sort((a, b) => a.frame - b.frame);
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
                    worksheet.Cells[x, y].Style.Font.Color.SetColor(System.Drawing.Color.FromArgb(199,203,0));
                }
                if (describe.Contains("MISS"))
                {
                    worksheet.Cells[x, y].Style.Font.Color.SetColor(System.Drawing.Color.FromArgb(255, 0, 0));
                }

            }

        }
        public static void ReadExcelNicName()
        {
            OpenFileName ofn = new OpenFileName();
            ofn.structSize = Marshal.SizeOf(ofn);
            ofn.filter = "Excel Files(*.xlsx)\0*.xlsx\0";  //指定打开格式
            ofn.file = new string(new char[256]);
            ofn.maxFile = ofn.file.Length;
            ofn.fileTitle = new string(new char[64]);
            ofn.maxFileTitle = ofn.fileTitle.Length;
            ofn.initialDir = UnityEngine.Application.dataPath;//默认路径
            ofn.title = "打开Excel";
            ofn.defExt = "xlsx";
            ofn.flags = 0x00080000 | 0x00001000 | 0x00000800 | 0x00000200 | 0x00000008;//OFN_EXPLORER|OFN_FILEMUSTEXIST|OFN_PATHMUSTEXIST| OFN_ALLOWMULTISELECT|OFN_NOCHANGEDIR
            //打开windows框
            if (DllTest.GetOpenFileName(ofn))
            {
                ofn.file = ofn.file.Replace("\\", "/");
                FileInfo info = new FileInfo(ofn.file);
                FileStream stream = info.Open(FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                ExcelDataReader.IExcelDataReader excelReader = ExcelDataReader.ExcelReaderFactory.CreateOpenXmlReader(stream);
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
        }
        private static void MySetValue(this ExcelWorksheet worksheet,int posx,int posy,string value,
            int size=11,bool centre = true,bool blod = false,int[] fontColor=null,int[] backColor=null)
        {
            worksheet.Cells[posx,posy].Style.Font.Size = size; //字体大小
            if(blod)
                worksheet.Cells[posx, posy].Style.Font.Bold = true; //字体大小
            if (fontColor != null && fontColor.Length >= 3)
                worksheet.Cells[posx, posy].Style.Font.Color.SetColor(System.Drawing.Color.FromArgb(fontColor[0], fontColor[1], fontColor[2]));
            if (centre)
            {
                worksheet.Cells[posx, posy].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center; //对其方式
                worksheet.Cells[posx, posy].Style.VerticalAlignment = ExcelVerticalAlignment.Center; //对其方式

            }
            worksheet.Cells[posx, posy].Value = value; //显示
            if (backColor != null&&backColor.Length>=3)
            {
                worksheet.Cells[posx, posy].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Cells[posx, posy].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(backColor[0], backColor[1], backColor[2]));
            }
        }
        private static void MySetValue(this ExcelWorksheet worksheet, int posx, int posy, int value,
    int size = 11, bool centre = true, bool blod = false, int[] fontColor = null, int[] backColor = null)
        {
            worksheet.Cells[posx, posy].Style.Font.Size = size; //字体大小
            if (blod)
                worksheet.Cells[posx, posy].Style.Font.Bold = true; //字体大小
            if (fontColor != null && fontColor.Length >= 3)
                worksheet.Cells[posx, posy].Style.Font.Color.SetColor(System.Drawing.Color.FromArgb(fontColor[0], fontColor[1], fontColor[2]));
            if (centre)
            {
                worksheet.Cells[posx, posy].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center; //对其方式
                worksheet.Cells[posx, posy].Style.VerticalAlignment = ExcelVerticalAlignment.Center; //对其方式

            }
            worksheet.Cells[posx, posy].Value = value; //显示
            if (backColor != null && backColor.Length >= 3)
            {
                worksheet.Cells[posx, posy].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Cells[posx, posy].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(backColor[0], backColor[1], backColor[2]));
            }
        }

        public static void InsertImage(this ExcelWorksheet worksheet, byte[] imageBytes, int rowNum, int columnNum, bool autofit,float widthCount,float hightCount)
        {
            using (var image = System.Drawing.Image.FromStream(new MemoryStream(imageBytes)))
            {
                var picture = worksheet.Drawings.AddPicture(rowNum+"0"+columnNum, image);
                var cell = worksheet.Cells[rowNum, columnNum];
                int cellColumnWidthInPix = Mathf.RoundToInt(GetWidthInPixels(cell)*widthCount);
                int cellRowHeightInPix = GetHeightInPixels(cell);
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
                picture.SetSize(cellColumnWidthInPix, cellColumnWidthInPix);
                picture.SetPosition(rowNum-1 , 0, columnNum-1 , 0);
                

            }
        }
        /// <summary>
        /// 获取自适应调整后的图片尺寸
        /// </summary>
        /// <param name="image"></param>
        /// <param name="cellColumnWidthInPix"></param>
        /// <param name="cellRowHeightInPix"></param>
        /// <returns>item1:调整后的图片宽度; item2:调整后的图片高度</returns>
        private static Tuple<int, int> GetAdjustImageSize(System.Drawing.Image image, int cellColumnWidthInPix, int cellRowHeightInPix)
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
        }
        /// <summary>
        /// 获取单元格的宽度(像素)
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        private static int GetWidthInPixels(ExcelRange cell)
        {
            double columnWidth = cell.Worksheet.Column(cell.Start.Column).Width;
            System.Drawing.Font font = new System.Drawing.Font(cell.Style.Font.Name, cell.Style.Font.Size, System.Drawing.FontStyle.Regular);
            double pxBaseline = Math.Round(MeasureString("1234567890", font) / 10);
            return (int)(columnWidth * pxBaseline);
        }
        /// <summary>
        /// 获取单元格的高度(像素)
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        private static int GetHeightInPixels(ExcelRange cell)
        {
            double rowHeight = cell.Worksheet.Row(cell.Start.Row).Height;
            using (System.Drawing.Graphics graphics = System.Drawing.Graphics.FromHwnd(IntPtr.Zero))
            {
                float dpiY = graphics.DpiY;
                return (int)(rowHeight * (1.0 / DEFAULT_DPI) * dpiY);
            }
        }
        /// <summary>
        /// MeasureString
        /// </summary>
        /// <param name="s"></param>
        /// <param name="font"></param>
        /// <returns></returns>
        private static float MeasureString(string s, System.Drawing.Font font)
        {
            using (var g = System.Drawing.Graphics.FromHwnd(IntPtr.Zero))
            {
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                return g.MeasureString(s, font, int.MaxValue, StringFormat.GenericTypographic).Width;
            }
        }
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

        public TimeLineDataA(int frame, int unitid,int idx, string describeA, string describeB,int valueA = 0,float valueB = 0)
        {
            this.frame = frame;
            this.unitid = unitid;
            this.idx = idx;
            this.describeA = describeA;
            this.describeB = describeB;
            this.valueA = valueA;
            this.valueB = valueB;
        }
    }
}