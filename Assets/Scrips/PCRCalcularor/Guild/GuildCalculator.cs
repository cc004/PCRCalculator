using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using Elements;
using Elements.Battle;
using ExcelDataReader;
using ExcelHelper;
using Newtonsoft0.Json;
using OfficeOpenXml;
using PCRCaculator.Battle;
using UnityEngine;
using UnityEngine.UI;
using BattleDefine = Elements.BattleDefine;
using Random = UnityEngine.Random;

namespace PCRCaculator.Guild
{
    public class ProbEvent
    {
        public string unit;
        public int frame = BattleHeaderController.CurrentFrameCount;
        public string description;
        public Func<int, bool> predict;
        public bool enabled = true;
        public bool isProb = false;
    }
    public class GuildCalculator : MonoBehaviour
    {
        public static GuildCalculator Instance;
        public const float deltaXforChat = 1 / 5400.0f;
        public Dictionary<int, List<UnitStateChangeData>> allUnitStateChangeDic = new Dictionary<int, List<UnitStateChangeData>>();
        public Dictionary<int, List<UnitAbnormalStateChangeData>> allUnitAbnormalStateDic = new Dictionary<int, List<UnitAbnormalStateChangeData>>();
        public Dictionary<int, List<ValueChangeData>> allUnitHPDic = new Dictionary<int, List<ValueChangeData>>();
        public Dictionary<int, List<ValueChangeData>> allUnitTPDic = new Dictionary<int, List<ValueChangeData>>();
        public Dictionary<int, List<UnitSkillExecData>> allUnitSkillExecDic = new Dictionary<int, List<UnitSkillExecData>>();
        public Dictionary<int, List<ValueChangeData>> playerUnitDamageDic = new Dictionary<int, List<ValueChangeData>>();
        public List<ValueChangeData> bossDefChangeDic = new List<ValueChangeData>();
        public List<ValueChangeData> bossMgcDefChangeDic = new List<ValueChangeData>();
        public List<RandomData> AllRandomDataList = new List<RandomData>();
        public GameObject guildPageUI;
        public GameObject skillDetailPrefab;

        public GuildChatPannel chatPannel;

        public Text CurrentBossText;
        public Text totalDamageText;
        public Text currentSeedText;
        public ScrollRect skillScrollRect;
        public List<Toggle> ModeToggles;
        public GameObject skillGroupPrefab;
        public InputField basedmg;
        public RectTransform skillGroupParent;
        public Vector3 skillGroupBasePos;
        public Vector3 skillGroupAddPos;
        public List<Color> skillGroupColors;
        public GameObject UBTimeEditorPrefab;

        private readonly Dictionary<int, UnitStateChangeData> allUnitLastStateDic = new Dictionary<int, UnitStateChangeData>();
        private readonly List<int> playerIds = new List<int>();
        private int bossId;
        private List<UnitCtrl> players;
        private UnitCtrl boss;
        private readonly Dictionary<int, GuildSkillGroupPrefab> skillGroupPrefabDic = new Dictionary<int, GuildSkillGroupPrefab>();
        private long totalDamage;
        private long totalDamageCriEX;
        private FloatWithEx totalDamageExcept = 0;
        private bool isFinishCalc;
        private int backTime;

        public List<int> PlayerIds => playerIds;
        public int BossId => bossId;

        private void Awake()
        {
            Instance = this;
        }
        private void OnDestroy()
        {
            Instance = null;
        }

        public void Initialize(List<UnitCtrl> players, UnitCtrl boss)
        {
            this.players = players;
            this.boss = boss;
            if (MyGameCtrl.Instance.tempData.isGuildBattle && !MainManager.Instance.GuildBattleData.SettingData.calcSpineAnimation)
            {
                guildPageUI.SetActive(true);
            }
            else
            {
                guildPageUI.SetActive(false);
            }

            ReflashTotalDamage(false, 0, false, 0, 0);
            CurrentBossText.text = MainManager.Instance.GuildBattleData.SettingData.GetCurrentBossDes();
            currentSeedText.text = "" + MyGameCtrl.Instance.CurrentSeedForSave;
            int idx = 0;
            bossId = boss.UnitId;
            AddSkillGroupPrefab(boss.UnitId, boss, 0);
            boss.MyOnDamage += AppendGetDamage;
            boss.MyOnBaseValueChanged += AppendChangeBaseValue;
            foreach (UnitCtrl unitCtrl in players)
            {
                idx++;
                AddSkillGroupPrefab(unitCtrl.UnitId, unitCtrl, idx);
                PlayerIds.Add(unitCtrl.UnitId);
            }
            playerUnitDamageDic.Add(0, new List<ValueChangeData> { new ValueChangeData(0, 0) });
            AppendChangeBaseValue(bossId, 1, boss.Def, 0, "初始化");
            AppendChangeBaseValue(bossId, 2, boss.MagicDef, 0, "初始化");
            OnToggleSwitched(0);
            BattleManager.OnCallRandom = AppendCallRandom;
        }
        private void AddSkillGroupPrefab(int a, UnitCtrl b, int c, bool create = true)
        {
            bool isPlayer = b.UnitId <= 200000;
            if (create)
            {
                allUnitStateChangeDic.Add(a, new List<UnitStateChangeData>());
                allUnitAbnormalStateDic.Add(a, new List<UnitAbnormalStateChangeData>());
                allUnitHPDic.Add(a, new List<ValueChangeData> { new ValueChangeData(0, 1, (int)b.MaxHp, "初始化") });
                allUnitTPDic.Add(a, new List<ValueChangeData> { new ValueChangeData(0, 0) });
                allUnitLastStateDic.Add(a, new UnitStateChangeData(0, UnitCtrl.ActionState.GAME_START, UnitCtrl.ActionState.GAME_START));
                allUnitSkillExecDic.Add(a, new List<UnitSkillExecData>());
            }
            b.MyOnChangeState += AppendChangeState;
            if (b.UnitId >= 300000 && b.UnitId <= 400000)
            {
                b.MyOnDamage2 += ReflashTotalDamage;
            }
            else if (isPlayer)
            {
                playerUnitDamageDic.Add(b.UnitId, new List<ValueChangeData> { new ValueChangeData(0, 0) });
            }
            b.MyOnAbnormalStateChange += AppendChangeAbnormalState;
            b.MyOnLifeChanged += AppendChangeHP;
            b.MyOnTPChanged += AppendChangeTP;
            b.MyOnStartAction += AppendStartSkill;
            b.MyOnExecAction += AppendExecAction;
            if (create)
            {
                AddSkillGroups(a, c, c, b.UnitName);
            }

            //AddSkillGroups(a, c, c,isPlayer?b.unitData.GetNicName():b.UnitName);

        }

        private int id;
        public void AppendChangeState(int unitid, UnitCtrl.ActionState actionState, int frameCount, string describe, UnitCtrl ctrl)
        {
            try
            {
                if (unitid >= 500000 && allUnitStateChangeDic.ContainsKey(unitid)) { return; }
                if (allUnitLastStateDic[unitid].changStateTo != actionState)
                {
                    if (actionState == UnitCtrl.ActionState.IDLE)
                    {
                        ctrl.critPoint = new UnitCtrl.CritPoint
                        {
                            description = "IDLE",
                            description2 = "AUTO",
                            priority = UnitCtrl.eCritPointPriority.ExecAction
                        };
                    }
                    allUnitStateChangeDic[unitid].Add(
                        new UnitStateChangeData
                        {
                            id = ++id,
                            changStateFrom = allUnitLastStateDic[unitid].changStateTo,
                            changStateTo = actionState,
                            currentFrameCount = frameCount,
                            realFrameCount = BattleManager.Instance.FrameCount,
                            operation = actionState == UnitCtrl.ActionState.SKILL_1 ? ctrl.GetCurrentOp() : null
                        });
                    //skillGroupPrefabDic[unitid].AddButtons(allUnitLastStateDic[unitid].currentFrameCount, frameCount, (int)actionState);
                    Action action = null;
                    int startFrame = allUnitLastStateDic[unitid].currentFrameCount;
                    int oldState = (int)allUnitLastStateDic[unitid].changStateTo;
                    if (oldState >= 1 && oldState <= 3)
                    {
                        UnitSkillExecData skillExecData = allUnitSkillExecDic[unitid].FindLast(a => a.startTime == startFrame);
                        if (skillExecData != null)
                        {
                            skillExecData.endTime = frameCount;
                            action = () => { OpenSkillDetailPannel(skillExecData); };
                        }
                    }
                    skillGroupPrefabDic[unitid].AddButtons(startFrame, frameCount, oldState, action);

                    skillScrollRect.horizontalNormalizedPosition = frameCount * deltaXforChat;
                    allUnitLastStateDic[unitid] = new UnitStateChangeData(frameCount,
                        allUnitLastStateDic[unitid].changStateTo, actionState, describe);
                }
            }
            catch (Exception e)
            {
                Debug.LogError("添加角色按钮时出错！" + e.Message);
            }
        }
        public void AppendChangeAbnormalState(int unitid, UnitAbnormalStateChangeData abnormalData, int frameCount)
        {

            UnitAbnormalStateChangeData changeData;
            if (abnormalData.isBuff)
            {
                /*
                if (abnormalData.BUFF_Type)
                {
                    return;
                }*/

                changeData = allUnitAbnormalStateDic[unitid].Find(a =>
                {
                    return a.BUFF_Type == abnormalData.BUFF_Type &&
                    a.MainValue == abnormalData.MainValue;
                });
            }
            else
            {
                if (abnormalData.CurrentAbnormalState == UnitCtrl.eAbnormalState.NONE)
                {
                    return;
                }

                changeData = allUnitAbnormalStateDic[unitid].FindLast(
                    a => a.CurrentAbnormalState == abnormalData.CurrentAbnormalState);/*||
                    a.CurrentAbnormalState== UnitCtrl.eAbnormalState.SLOW &&
                    abnormalData.CurrentAbnormalState == UnitCtrl.eAbnormalState.HASTE||
                    a.CurrentAbnormalState == UnitCtrl.eAbnormalState.HASTE &&
                    abnormalData.CurrentAbnormalState == UnitCtrl.eAbnormalState.SLOW);*/
            }
            if (abnormalData.enable)
            {
                abnormalData.startFrameCount = frameCount;
                abnormalData.isFinish = false;
                allUnitAbnormalStateDic[unitid].Add(abnormalData);
            }
            else
            {
                if (changeData == null)
                {
                    //Debug.LogError("(" + frameCount + "）角色：" + unitid + "的状态" + abnormalData.SkillName + "-" + abnormalData.ActionId + "发生错误！");
                    return;
                }
                changeData.endFrameCount = frameCount;
                changeData.isFinish = true;
                skillGroupPrefabDic[unitid].AddAbnormalStateButtons(changeData);
                allUnitAbnormalStateDic[unitid].Remove(changeData);
            }
        }
        public void AppendChangeHP(int unitid, float currentHP, int hp, int damage, int frame, string describe, UnitCtrl source)
        {
            if (unitid >= 400000 && !allUnitHPDic.ContainsKey(unitid)) { return; }
            ValueChangeData valueData = new ValueChangeData(frame * deltaXforChat, currentHP, hp, describe)
            {
                damage = damage,
                id = id,
                source = source?.UnitId ?? unitid
            };
            allUnitHPDic[unitid].Add(valueData);
            if (!GuildManager.StaticsettingData.calcSpineAnimation)
                skillGroupPrefabDic[unitid].ReflashHPChat(allUnitHPDic[unitid]);
        }
        public void AppendChangeTP(int unitid, float currentTP, int frame, string describe)
        {
            if (unitid >= 400000 && !allUnitTPDic.ContainsKey(unitid)) { return; }
            allUnitTPDic[unitid].Add(new ValueChangeData(frame * deltaXforChat, currentTP, 0, describe));
            if (!GuildManager.StaticsettingData.calcSpineAnimation)
                skillGroupPrefabDic[unitid].ReflashTPChat(allUnitTPDic[unitid]);
        }
        public void AppendStartSkill(int unitid, UnitSkillExecData unitSkillExecData)
        {
            if (unitid >= 400000 && !allUnitSkillExecDic.ContainsKey(unitid)) { return; }
            allUnitSkillExecDic[unitid].Add(unitSkillExecData);
        }
        public void AppendExecAction(int unitid, int skillid, UnitActionExecData actionExecData)
        {
            if (unitid >= 400000 && !allUnitSkillExecDic.ContainsKey(unitid)) { return; }
            UnitSkillExecData skillData = allUnitSkillExecDic[unitid].FindLast(a =>
                a.startTime <= actionExecData.execTime && a.skillID == skillid);
            if (skillData != null)
            {
                UnitActionExecData unitAction = skillData.actionExecDatas.Find(a => a.actionID == actionExecData.actionID);
                if (unitAction != null && unitAction.CanCombine(actionExecData))
                {
                    if (!unitAction.targetNames.Contains(actionExecData.targetNames[0]))
                    {
                        unitAction.targetNames.Add(actionExecData.targetNames[0]);
                    }
                    else
                    {
                        unitAction.describe += "/" + actionExecData.describe;
                    }
                }
                else
                {
                    skillData.actionExecDatas.Add(actionExecData);
                }
            }
        }
        public void AppendGetDamage(int unitid, int sourceUnitid, float damage, int currentFrame)
        {
            if (!playerUnitDamageDic.ContainsKey(sourceUnitid))
            {
                return;
            }

            if (unitid == bossId)
            {
                playerUnitDamageDic[sourceUnitid].Add(new ValueChangeData(currentFrame / 5400.0f, damage));
                playerUnitDamageDic[0].Add(new ValueChangeData(currentFrame / 5400.0f, damage));
            }
        }
        public void AppendChangeBaseValue(int unitid, int valueType, float value, int currentFrame, string describe)
        {
            if (unitid == bossId)
            {
                if (value < 0)
                {
                    value = 0;
                }
                switch (valueType)
                {
                    case 1:
                        bossDefChangeDic.Add(new ValueChangeData(currentFrame * deltaXforChat, value, 0, describe));
                        break;
                    case 2:
                        bossMgcDefChangeDic.Add(new ValueChangeData(currentFrame * deltaXforChat, value, 0, describe));
                        break;
                }
            }
        }
        public void AppendCallRandom(RandomData data)
        {
            data.id = AllRandomDataList.Count;
            data.frame = BattleHeaderController.CurrentFrameCount;
            data.currentSeed = Random.seed;
            AllRandomDataList.Add(data);
        }
        public void OnToggleSwitched(int toggleId)
        {
            if (ModeToggles[toggleId].isOn)
            {
                foreach (GuildSkillGroupPrefab sk in skillGroupPrefabDic.Values)
                {
                    sk.SwitchPage(toggleId);
                }
                ResizePrefabs(toggleId == 1);
            }
        }
        private void AddSkillGroups(int unitid, int idx, int colorIdx, string Name)
        {
            GameObject a = Instantiate(skillGroupPrefab);
            a.transform.SetParent(skillGroupParent, false);
            a.transform.localPosition = skillGroupBasePos + idx * skillGroupAddPos;
            GuildSkillGroupPrefab guildSkill = a.GetComponent<GuildSkillGroupPrefab>();
            if (colorIdx >= skillGroupColors.Count)
            {
                colorIdx = skillGroupColors.Count - 1;
            }

            guildSkill.Initialize(Name, skillGroupColors[colorIdx]);
            skillGroupPrefabDic.Add(unitid, guildSkill);
        }
        public void ReflashTotalDamage(bool byAttack, float value, bool critical, float criticalEXdamage, FloatWithEx exvalue)
        {
            totalDamage += (long)value;
            totalDamageExcept += exvalue;
            if (critical)
            {
                totalDamageCriEX += (long)criticalEXdamage;
            }
        }
        public void AddSummonUnit(UnitCtrl unitCtrl)
        {
            if (playerIds.Contains(unitCtrl.UnitId))
            {
                AddSkillGroupPrefab(unitCtrl.UnitId, unitCtrl, playerIds.Count, false);
                return;
            }
            int idx = playerIds.Count + 1;
            AddSkillGroupPrefab(unitCtrl.UnitId, unitCtrl, idx);
            PlayerIds.Add(unitCtrl.UnitId);
        }
        public void OnBattleFinished(int result, int currentFrame)
        {
            Action<List<UnitCtrl>> action = a =>
            {
                foreach (UnitCtrl unitCtrl in a)
                {
                    int unitid = unitCtrl.UnitId;
                    AppendChangeState(unitid, UnitCtrl.ActionState.GAME_START, 5400, "时间耗尽", unitCtrl);
                    AppendChangeHP(unitid, unitCtrl.Hp / unitCtrl.MaxHp, 0, (int)unitCtrl.Hp, 5400, "时间耗尽", unitCtrl);
                    AppendChangeTP(unitid, (float)unitCtrl.Energy / BattleDefine.SKILL_ENERGY_MAX, 5400, "时间耗尽");
                    foreach (UnitAbnormalStateChangeData changeData in allUnitAbnormalStateDic[unitid])
                    {
                        changeData.endFrameCount = 5400;
                        changeData.isFinish = true;
                        skillGroupPrefabDic[unitid].AddAbnormalStateButtons(changeData);
                    }
                }
            };
            action(MyGameCtrl.Instance.playerUnitCtrl);
            action(MyGameCtrl.Instance.enemyUnitCtrl);
            
            foreach (var pair in skillGroupPrefabDic)
            {
                pair.Value.ReflashHPChat(allUnitHPDic[pair.Key]);
                pair.Value.ReflashTPChat(allUnitTPDic[pair.Key]);
            }

            AppendChangeBaseValue(bossId, 1, boss.Def, 5400, "时间耗尽");
            AppendChangeBaseValue(bossId, 2, boss.MagicDef, 5400, "时间耗尽");
            skillScrollRect.horizontalNormalizedPosition = 1;
            guildPageUI.SetActive(true);
            isFinishCalc = true;
            chatPannel.Init();
            OnToggleSwitched(0);
            if (currentFrame < 5400)
            {
                backTime = Mathf.CeilToInt((MyGameCtrl.Instance.tempData.SettingData.limitTime * 60 - currentFrame) / 60.0f) + 20;
                ReflashTotalDamage(true, 0, false, 0, 0);
            }

            var n = GuildManager.StaticsettingData.n1;
            expectedDamage = (int)totalDamageExcept.Expected(n);
            string damageStr =
                $"{totalDamage}({(totalDamage - totalDamageCriEX)}+<color=#FFEC00>{totalDamageCriEX}</color>)[<color=#56A0FF>{expectedDamage}({totalDamageExcept.Expect})±{(int)totalDamageExcept.Stddev}</color>]";
            if (backTime > 0)
            {
                detail = $"返{backTime}s-{boss.Hp.Probability(x => x <= 0f, 1000):P0}";
                damageStr += detail;
            }
            else detail = string.Empty;
            totalDamageText.text = damageStr;
            basedmg.text = totalDamageCriEX.ToString();
        }

        public int expectedDamage;
        public string detail;
        private void ResizePrefabs(bool change)
        {
            int idx = 0;
            float localPosX = skillGroupBasePos.x;
            float localPosY = skillGroupAddPos.y;
            foreach (GuildSkillGroupPrefab prefab in skillGroupPrefabDic.Values)
            {
                //a.transform.localPosition = skillGroupBasePos + idx * skillGroupAddPos;
                float localPosY_change = prefab.Resize(change) + 3;
                prefab.gameObject.transform.localPosition = new Vector3(localPosX, localPosY, 0);
                localPosY -= localPosY_change;
                idx++;
            }
            float totalHight = Mathf.Abs(localPosY - 3);
            skillGroupParent.sizeDelta = new Vector2(skillGroupParent.sizeDelta.x, totalHight);
        }
        public void OpenSkillDetailPannel(UnitSkillExecData data)
        {
            GameObject a = Instantiate(skillDetailPrefab);
            a.transform.SetParent(BaseBackManager.Instance.latestUIback.transform, false);
            a.GetComponent<GuildSkillDetailPannel>().Setdetails(data);
        }
        public void OpenChatPannel()
        {
            if (isFinishCalc)
            {
                chatPannel.gameObject.SetActive(true);
            }
        }
        public void SaveFileToExcel()
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                MainManager.Instance.WindowConfigMessage("手机端导出可能失败，建议使用电脑端导出！\n如要尝试请按确定继续", CallExcelHelper);
            }
            else
            {
                CallExcelHelper();
            }
        }

        private void CallExcelHelper()
        {
            string fileName = CreateExcelName();

            try
            {

                GuildTimelineData timelineData = new GuildTimelineData(MyGameCtrl.Instance.tempData.SettingData.GetCurrentPlayerGroup(), MyGameCtrl.Instance.CurrentSeedForSave, allUnitStateChangeDic,
                    allUnitAbnormalStateDic, allUnitHPDic, allUnitTPDic, allUnitSkillExecDic, playerUnitDamageDic, bossDefChangeDic, bossMgcDefChangeDic, AllRandomDataList)
                {
                    UBExecTime = CreateUBExecTimeData(),
                    exceptDamage = Mathf.RoundToInt(expectedDamage / 10000),
                    backDamage = Mathf.RoundToInt((totalDamage - totalDamageCriEX) / 10000),
                    charImages = BattleUIManager.Instance.GetCharactersImage(),
                    //string fileName = CreateExcelName();
                    timeLineName = CreateExcelName(),
                    uBDetails = CreateUBDetailList(),
                    detail = detail,
                };
                ExcelHelper.ExcelHelper.OutputGuildTimeLine(timelineData, fileName);
                MainManager.Instance.WindowConfigMessage("成功！", null);
            }
#if UNITY_EDITOR
            catch (DuplicateWaitObjectException a)
#else
            catch (System.Exception a)
#endif
            {
                MainManager.Instance.WindowConfigMessage("发生错误：" + a.Message, null);
            }

        }
        public List<List<float>> CreateUBExecTimeData()
        {
            /*
            List<List<float>> UBTimes = new List<List<float>>();
            for(int i = 0; i < 5; i++)
            {
                List<float> ubline = new List<float>();
                if (i < PlayerIds.Count)
                {
                    int playerid = PlayerIds[i];
                    foreach(var data in allUnitStateChangeDic[playerid])
                    {
                        if(data.changStateTo == UnitCtrl.ActionState.SKILL_1)
                        {
                            ubline.Add(data.currentFrameCount);
                        }
                    }
                }
                UBTimes.Add(ubline);
            }
            return UBTimes;*/
            return PlayerIds.Take(5).Append(bossId).SelectMany((id, i) => allUnitStateChangeDic[id]
                    .Where(state => state.changStateTo == UnitCtrl.ActionState.SKILL_1)
                    .Select(s => (state: s, pos: i)))
                .GroupBy(s => s.state.currentFrameCount).SelectMany(g => g.OrderBy(t => t.state.id)
                    .Select((t, i) => (t, normalized: t.state.currentFrameCount + i * 0.01f)))
                .GroupBy(s => s.t.pos).Select(g =>
                    (g.Key, list: g.OrderBy(t => t.normalized).Select(t => t.normalized).ToList()))
                .Aggregate(new List<List<float>>
                {
                    new List<float>(), new List<float>(), new List<float>(), new List<float>(), new List<float>(), new List<float>()
                }, (result, tuple) =>
                {
                    result[tuple.Key] = tuple.list;
                    return result;
                });
        }
        public List<UBDetail> CreateUBDetailList()
        {
            List<UBDetail> uBDetails = new List<UBDetail>();
            //List<List<float>> UBTimes = CreateUBExecTimeData();
            for (int i = 0; i < 5; i++)
            {
                if (i < PlayerIds.Count)
                {
                    foreach (UnitStateChangeData tm in allUnitStateChangeDic[PlayerIds[i]])
                    {
                        if (tm.changStateTo != UnitCtrl.ActionState.SKILL_1)
                        {
                            continue;
                        }

                        UnitData unitData = players[i].unitData;
                        UBDetail detail = new UBDetail
                        {
                            unitData = unitData,
                            sorting = tm.id,
                            UBTime = tm.currentFrameCount,
                            description = tm.operation ?? string.Empty
                        };
                        ValueChangeData changeData = allUnitHPDic[bossId].Find(
                            a => /* Mathf.RoundToInt(a.xValue * 5400) == tm.currentFrameCount && */
                                 a.id == tm.id && a.source == unitData.unitId);
                        if (changeData != null)
                        {
                            detail.Damage = changeData.damage;
                            detail.Critical = changeData.describe.Contains("暴击");
                        }
                        else
                        {
                            detail.Damage = 0;
                        }

                        uBDetails.Add(detail);
                    }
                }
            }
            foreach (UnitStateChangeData data in allUnitStateChangeDic[bossId])
            {
                if (data.changStateTo == UnitCtrl.ActionState.SKILL_1)
                {
                    UBDetail detail = new UBDetail
                    {
                        isBossUB = true,
                        sorting = data.id,
                        UBTime = data.currentFrameCount
                    };
                    uBDetails.Add(detail);
                }
            }
            return uBDetails.OrderBy(d => d.sorting).ToList();
        }
        private string CreateExcelName()
        {
            string fileName = CurrentBossText.text + "-";
            for (int i = 0; i < 5; i++)
            {
                if (playerIds.Count > i)
                {
                    int player = playerIds[i];
                    fileName += MainManager.Instance.GetUnitNickName(player);
                }
            }
            fileName += "-" + Mathf.RoundToInt(expectedDamage / 10000) + "w";
            return fileName;
        }
        public void ReplaceUBTime()
        {
            //MainManager.Instance.WindowConfigMessage("是否将预设阵容的UB时间改为当前的UB时间？", ReplaceUBTime_0, null);
            GameObject a = Instantiate(UBTimeEditorPrefab);
            a.transform.SetParent(BaseBackManager.Instance.latestUIback.transform, false);
            a.GetComponent<UBTimeEditor>().Init(MyGameCtrl.Instance.tempData.SettingData.GetCurrentPlayerGroup());
        }
        /*private void ReplaceUBTime_0()
        {
            MyGameCtrl.Instance.tempData.SettingData.ReplaceUBTimeData(CreateUBExecTimeData());
            GuildManager.SaveSettingData(MyGameCtrl.Instance.tempData.SettingData);
            MainManager.Instance.WindowConfigMessage("成功！", null, null);
        }*/
        public OnceResultData GetOnceResultData(int id)
        {
            List<string> errorList = new List<string>();
            List<List<float>> ubExecTime = CreateUBExecTimeData();
            for (int i = 0; i < 5; i++)
            {
                if (players.Count > i)
                {
                    if (MyGameCtrl.Instance.tempData.UBExecTimeList[i].Count != ubExecTime[i].Count)
                    {
                        errorList.Add(players[i].UnitName + "的某个UB释放失败！");
                    }
                }
            }

            var currentFrame = BattleHeaderController.CurrentFrameCount;
            return new OnceResultData
            {
                id = id,
                exceptDamage = (long)totalDamageExcept.Expect,
                criticalEX = totalDamageCriEX,
                currentDamage = totalDamage,
                randomSeed = MyGameCtrl.Instance.CurrentSeedForSave,
                backTime = currentFrame < 5400 ? (Mathf.CeilToInt((MyGameCtrl.Instance.tempData.SettingData.limitTime * 60 - currentFrame) / 60.0f) + 20) : 0,
                warnings = errorList
            };
        }
        public static List<ValueChangeData> CreateLineChatData(List<ValueChangeData> data, float deltaXforChat = 1 / 5400.0f)
        {
            List<ValueChangeData> newData = new List<ValueChangeData>();
            if (data.Count >= 1)
            {
                newData.Add(data[0]);
            }
            int delta = 1;
            if (data.Count >= 200)
            {
                delta = Mathf.Max(1, Mathf.RoundToInt(data.Count / (float)200));
            }
            for (int i = 1; i < data.Count; i += delta)
            {
                if (data[i].xValue > data[i - 1].xValue + deltaXforChat)
                {
                    newData.Add(new ValueChangeData(data[i].xValue - deltaXforChat, data[i - 1].yValue));
                }
                newData.Add(data[i]);
            }
            return newData;
        }
        public static List<ValueChangeData> CreateTotalChatData(List<ValueChangeData> data)
        {
            List<ValueChangeData> data0 = new List<ValueChangeData>();
            float add = 0;
            foreach (ValueChangeData value in data)
            {
                add += value.yValue;
                data0.Add(new ValueChangeData(value.xValue, add));
            }
            return CreateLineChatData(data0);
        }
        public static List<ValueChangeData> CreateLineChatData2(List<ValueChangeData> data, float deltaX = 1 / 1080.0f, float deltaXforChat = 1 / 5400.0f)
        {
            List<ValueChangeData> newData = new List<ValueChangeData>();
            if (data.Count >= 1)
            {
                newData.Add(data[0]);
            }
            for (int i = 1; i < data.Count; i++)
            {
                if (data[i].xValue > data[i - 1].xValue + deltaXforChat)
                {
                    newData.Add(new ValueChangeData(data[i].xValue - deltaXforChat, data[i - 1].yValue));
                }

                if (i == data.Count - 1 || (data[i].xValue < data[i + 1].xValue + deltaX + deltaXforChat))
                {
                    newData.Add(new ValueChangeData(data[i].xValue + deltaX, data[i].yValue));
                    newData.Add(new ValueChangeData(data[i].xValue + deltaX + deltaXforChat, 0));
                }
                newData.Add(data[i]);
            }
            return newData;
        }
        public static List<ValueChangeData> NormalizeLineChatData(List<ValueChangeData> data, float Max)
        {
            List<ValueChangeData> data0 = new List<ValueChangeData>();
            foreach (ValueChangeData value in data)
            {
                data0.Add(new ValueChangeData(value.xValue, Mathf.Max(0, value.yValue / Max)));
            }
            return data0;
        }



        private readonly Dictionary<string, string> templateSettings = new Dictionary<string, string>();
        public readonly List<ProbEvent> dmglist = new List<ProbEvent>();
        public readonly List<(int frame, FloatWithEx value)> bossValues = new List<(int frame, FloatWithEx value)>();

        private static bool CanKill(long maxhp, params float[] dmgs)
        {
            Array.Sort(dmgs);
            var remaining = maxhp - dmgs.Skip(1).Sum();
            return  dmgs[0] >= 30 / 7f * remaining;
        }

        public void SaveDieProb()
        {
            try
            {
                var ofn = new OpenFileName();
                ofn.structSize = Marshal.SizeOf(ofn);
                ofn.filter = "Text Files(*.txt)\0*.txt\0";
                ofn.file = new string(new char[256]);
                ofn.maxFile = ofn.file.Length;
                ofn.fileTitle = new string(new char[64]);
                ofn.maxFileTitle = ofn.fileTitle.Length;
                ofn.title = "选择保存路径";
                ofn.defExt = "txt";
                ofn.file = CreateExcelName();
                //注意 一下项目不一定要全选 但是0x00000008项不要缺少
                ofn.flags = 0x00080000 | 0x00001000 | 0x00000800 | 0x00000200 | 0x00000008;//OFN_EXPLORER|OFN_FILEMUSTEXIST|OFN_PATHMUSTEXIST| OFN_ALLOWMULTISELECT|OFN_NOCHANGEDIR

                if (!DllTest.GetSaveFileName(ofn))
                {
                    return;
                }
                var fileName = ofn.file.Replace("\\", "/");
                const string dmginfo = "标伤到达率";
                const string dmginfo2 = "标伤到达率(未乱轴)";
                const string totaldie = "总乱轴（上限）";
                //const string totaldie2 = "总乱轴（下限，不计入穿盾、break）";

                const string mb1 = "1+满补";
                const string mb2 = "2+满补";
                const string d1 = "1刀";
                const string d2 = "2刀";

                var dname = new Dictionary<string, int>();
                var dname2 = new Dictionary<string, int>();
                var dskill = new Dictionary<string, Dictionary<string, int>>();
                var dmg = new List<float>();
                foreach (var name in dmglist.Select(n => n.unit).Distinct())
                {
                    dname2.Add(name, 0);
                    dname.Add(name, 0);
                }
                dname.Add(dmginfo, 0);
                dname.Add(dmginfo2, 0);
                dname.Add(totaldie, 0);
                //dname.Add(totaldie2, 0);
                foreach (var skill in dmglist.Select(n => (n.unit, n.description)).Distinct())
                {
                    if (!dskill.ContainsKey(skill.unit)) dskill.Add(skill.unit, new Dictionary<string, int>());
                    dskill[skill.unit].Add(skill.description, 0);
                }
                var sname = new HashSet<string>();
                var sname2 = new HashSet<string>();
                var sskill = new HashSet<(string, string)>();
                var rnd = new System.Random();
                var val = float.Parse(basedmg.text);

                Func<int, bool> action;
                if (val > 0) action = hash => totalDamageExcept.Emulate(hash) >= val;
                else
                {
                    var lastHp = bossValues.Last(f => (GuildManager.Instance.SettingData.limitTime - f.frame / 60 >= -val)).value;
                    action = hash => lastHp.Emulate(hash) <= 0;
                }

                var dmglist2 = dmglist.Where(d => d.enabled).ToArray();
                for (int i = 0; i < GuildManager.StaticsettingData.n2; ++i)
                {
                    var flag = false;
                    var hash = rnd.Next();
                    dmg.Add(totalDamageExcept.Emulate(hash));
                    foreach (var evt in dmglist2)
                    {
                        if (evt.predict(hash))
                        {
                            if (!sname.Contains(evt.unit))
                            {
                                if (!evt.isProb)
                                {
                                    flag = true;
                                    sname2.Add(evt.unit);
                                }
                                sname.Add(evt.unit);
                                sskill.Add((evt.unit, evt.description));
                            }
                        }
                    }

                    if (sname.Count > 0)
                    {
                        ++dname[totaldie];
                        //if (flag) ++dname[totaldie2];
                        if (action(hash)) ++dname[dmginfo];
                    }
                    else if (action(hash))
                    {
                        ++dname[dmginfo];
                        ++dname[dmginfo2];
                    }

                    foreach (var name in sname) ++dname[name];
                    foreach (var name in sname2) ++dname2[name];
                    foreach ((string name, string source) in sskill) ++dskill[name][source];
                    sname.Clear();
                    sname2.Clear();
                    sskill.Clear();
                }

                var maxhp = boss.MaxHp;
                dname.Add(mb1, 0); dname.Add(mb2, 0); dname.Add(d2, 0); dname.Add(d1, 0);
                for (int i = 0; i < GuildManager.StaticsettingData.n2; ++i)
                {
                    var a = dmg[i];
                    var b = dmg[(i + 1) % GuildManager.StaticsettingData.n2];
                    var c = dmg[(i + 2) % GuildManager.StaticsettingData.n2];
                    if (CanKill(maxhp, a, b, c))
                    {
                        ++dname[mb2];
                        if (a + b > maxhp)
                        {
                            ++dname[d2];
                            if (CanKill(maxhp, a, b))
                            {
                                ++dname[mb1];
                                if (a > maxhp) ++dname[d1];
                            }
                        }
                    }
                }
                if (File.Exists(fileName)) File.Delete(fileName);
                using (var sw = new StreamWriter(File.OpenWrite(fileName)))
                {
                    foreach (var name in dname.OrderByDescending(p => p.Value).Select(p => p.Key))
                    {
                        var real = dname2.ContainsKey(name)
                            ? $"({(float) dname2[name] / GuildManager.StaticsettingData.n2:P1})"
                            : string.Empty;
                        sw.WriteLine($"{name}-{(float)dname[name] / GuildManager.StaticsettingData.n2:P1}" + real);
                        if (!dskill.ContainsKey(name)) continue;
                        foreach (var pair in dskill[name].OrderByDescending(p => p.Value).Where(p => p.Value > 0))
                            sw.WriteLine($"\t{pair.Key}-{(float)pair.Value / GuildManager.StaticsettingData.n2:P1}");
                    }
                }
                MainManager.Instance.WindowMessage("成功！");
            }
            catch (Exception e)
            {
                MainManager.Instance.WindowMessage(e.ToString());
            }
        }

        public void SaveFileToTemplate()
        {
            try
            {
                SaveFileToTemplateInternal();
            }
            catch (IOException)
            {
                MainManager.Instance.WindowMessage("excel被占用");
            }
        }

        private const float hparam = 1.75f;
        private const float hparam2 = 1.3125f;
        public void SaveFileToTemplateInternal()
        {

            OpenFileName ofn = new OpenFileName();

            ofn.file = MainManager.GetSaveDataPath() + "/Templates/模板4 星美版2.xlsx";
            templateSettings.Clear();
            using (IExcelDataReader reader =
                   ExcelReaderFactory.CreateReader(File.Open(ofn.file, FileMode.Open)))
            {
                DataSet ds = reader.AsDataSet();
                DataTable wsh = ds.Tables["设置"];
                for (int i = 1; i < wsh.Rows.Count; i++)
                {
                    if (wsh.Rows[i][0] is string cell1 && !string.IsNullOrEmpty(cell1))
                    {
                        var str = wsh.Rows[i][1]?.ToString();
                        if (string.IsNullOrEmpty(str)) str = null;
                        templateSettings.Add(cell1, str);
                    }
                    else
                    {
                        break;
                    }
                }
            }
            ExcelPackage src = new ExcelPackage(new FileInfo(ofn.file));

            GuildTimelineData timelineData = new GuildTimelineData(MyGameCtrl.Instance.tempData.SettingData.GetCurrentPlayerGroup(), MyGameCtrl.Instance.CurrentSeedForSave, allUnitStateChangeDic,
                allUnitAbnormalStateDic, allUnitHPDic, allUnitTPDic, allUnitSkillExecDic, playerUnitDamageDic, bossDefChangeDic, bossMgcDefChangeDic, AllRandomDataList)
            {
                UBExecTime = CreateUBExecTimeData(),
                exceptDamage = Mathf.RoundToInt(expectedDamage / 10000),
                backDamage = Mathf.RoundToInt((totalDamage - totalDamageCriEX) / 10000),
                charImages = BattleUIManager.Instance.GetCharactersImage(),
                //string fileName = CreateExcelName();
                timeLineName = CreateExcelName(),
                uBDetails = CreateUBDetailList(),
                detail = detail,
            };

            string fileName = CreateExcelName();
            ofn = new OpenFileName();
            ofn.structSize = Marshal.SizeOf(ofn);
            ofn.filter = "Excel Files(*.xlsx)\0*.xlsx\0";
            ofn.file = new string(new char[256]);
            ofn.maxFile = ofn.file.Length;
            ofn.fileTitle = new string(new char[64]);
            ofn.maxFileTitle = ofn.fileTitle.Length;
            ofn.title = "选择保存路径";
            ofn.defExt = "xlsx";
            ofn.file = fileName;
            //注意 一下项目不一定要全选 但是0x00000008项不要缺少
            ofn.flags = 0x00080000 | 0x00001000 | 0x00000800 | 0x00000200 | 0x00000008;//OFN_EXPLORER|OFN_FILEMUSTEXIST|OFN_PATHMUSTEXIST| OFN_ALLOWMULTISELECT|OFN_NOCHANGEDIR

            if (!DllTest.GetSaveFileName(ofn))
            {
                return;
            }

            fileName = ofn.file.Replace("\\", "/");

            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }

            ExcelPackage tgt = new ExcelPackage(new FileInfo(fileName));
            tgt.Workbook.Worksheets.Add("轴", src.Workbook.Worksheets["轴"]);

            ExcelWorksheet sheet = tgt.Workbook.Worksheets["轴"];

            if (templateSettings["轴编号坐标"] != null)
            {
                sheet.Cells[templateSettings["轴编号坐标"]].Value =
                    MainManager.Instance.GuildBattleData.SettingData.GetCurrentBossDes();
            }
            if (templateSettings["轴标题坐标"] != null)
            {
                sheet.Cells[templateSettings["轴标题坐标"]].Value = CreateExcelName();
            }
            if (templateSettings["轴作者坐标"] != null)
            {
                sheet.Cells[templateSettings["轴作者坐标"]].Value = GuildManager.Instance.SettingData.author;
            }
            if (templateSettings["BOSS名称坐标"] != null)
            {
                sheet.Cells[templateSettings["BOSS名称坐标"]].Value = MyGameCtrl.Instance.tempData.guildEnemy.detailData.unit_name;
            }
            
            if (templateSettings["BOSS头像"] != null)
            {
                var arr = templateSettings["BOSS头像"].Split(',').Select(int.Parse).ToArray();
                sheet.InsertImage(timelineData.charImages[5], arr[0], arr[1], false, arr[2], arr[3]);
            }
            string text = "";
            for (int i = 0; i < 5; i++)
            {
                if (i >= timelineData.playerGroupData.playerData.playrCharacters.Count) break;
                var unit = timelineData.playerGroupData.playerData.playrCharacters[i];
                if (templateSettings[$"角色{(i + 1)}名称坐标"] != null)
                {
                    sheet.Cells[templateSettings[$"角色{(i + 1)}名称坐标"]].Value = MainManager.Instance.GetUnitNickName(unit.unitId);
                }

                //等级
                string str = templateSettings[$"角色{(i + 1)}等级坐标"];
                if (str != null)
                {
                    text = sheet.Cells[str].Value == null ? "" : $"{sheet.Cells[str].Value} ";
                    sheet.Cells[str].Value = text + string.Format(templateSettings["等级文本"], unit.level);
                }

                //星级
                str = templateSettings[$"角色{(i + 1)}星级坐标"];
                if (str != null)
                {
                    text = sheet.Cells[str].Value == null ? "" : $"{sheet.Cells[str].Value} ";
                    sheet.Cells[str].Value = text + string.Format(templateSettings["星级文本"], unit.rarity);
                }

                str = templateSettings[$"角色{(i + 1)}Rank坐标"];
                if (str != null)
                {
                    text = sheet.Cells[str].Value == null ? "" : $"{sheet.Cells[str].Value} ";
                    sheet.Cells[str].Value = text + string.Format(templateSettings["Rank文本"],
                        $"{unit.rank}-{unit.equipLevel.Count(l => l != -1)}");
                }

                //专武等级
                str = templateSettings[$"角色{(i + 1)}专武坐标"];
                if (str != null)
                {

                    text = sheet.Cells[str].Value == null ? "" : $"{sheet.Cells[str].Value} ";
                    if (unit.uniqueEqLv == 0)
                    {
                        if (templateSettings["无专武文本"] != null)
                        {
                            sheet.Cells[str].Value = text + templateSettings["无专武文本"];
                        }
                    }
                    else
                    {
                        sheet.Cells[str].Value = text + string.Format(templateSettings["专武文本"], unit.uniqueEqLv);
                    }

                }

                if (templateSettings[$"角色{(i + 1)}头像"] != null)
                {
                    var arr = templateSettings[$"角色{(i + 1)}头像"].Split(',').Select(int.Parse).ToArray();
                    sheet.InsertImage(timelineData.charImages[i], arr[0], arr[1], false, arr[2], arr[3]);
                }
            }
            
            int rowId = int.Parse(templateSettings["轴起始行"]);
            var last = -1;
            foreach (var detail in timelineData.uBDetails)
            {
                if (templateSettings["帧数列"] != null)
                {
                    sheet.Cells[rowId, int.Parse(templateSettings["帧数列"])].Value = detail.UBTime;
                }

                if (last != detail.UBTime / 60)
                {
                    sheet.Cells[rowId, int.Parse(templateSettings["时间列"])].Value =
                        new DateTime(2000, 1, 1, 0, 0, 0)
                            .Add(TimeSpan.FromSeconds(GuildManager.Instance.SettingData.limitTime - detail.UBTime / 60))
                            .ToString(string.IsNullOrEmpty(GuildManager.Instance.SettingData.format) ? "m:ss" : GuildManager.Instance.SettingData.format);
                    last = detail.UBTime / 60;
                }
                if (detail.isBossUB)
                {
                    sheet.SelectedRange[$"A{rowId}:AAA{rowId}"].Clear();
                    sheet.Cells[$"A{templateSettings["BOOSUB行"]}:AAA{templateSettings["BOOSUB行"]}"]
                        .Copy(sheet.Cells[$"A{rowId}"]);
                }
                else
                {
                    sheet.Cells[rowId, int.Parse(templateSettings["角色列"])].Value = detail.unitData.GetUnitName();
                    sheet.MySetValue(rowId, int.Parse(templateSettings["伤害列"]), detail.Damage,
                        fontColor: detail.Critical ? new[] { 255, 0, 0 } : new[] { 0, 0, 0 });
                }
                ++rowId;
            }
            sheet.Cells[$"A{(int.Parse(templateSettings["轴结尾行"]) - 2)}:AAA{templateSettings["轴结尾行"]}"]
                .Copy(sheet.Cells[$"A{rowId}"]);
            //wsh2.Cells[rowId, int.Parse(setting["帧数列"])].Value = 5400;
            //wsh2.Cells[rowId, 3].Value = GetTimeText(5400);
            rowId += 3;
            sheet.Cells[$"A{rowId}:AAA999"].Clear();

            tgt.Save();
            MainManager.Instance.WindowMessage("保存完成");
        }
    }
    public struct UnitStateChangeData
    {
        public int id;
        public int currentFrameCount, realFrameCount;
        public UnitCtrl.ActionState changStateFrom;
        public UnitCtrl.ActionState changStateTo;
        public string describe;
        [JsonIgnore]
        public string operation;

        public UnitStateChangeData(int currentFrameCount, UnitCtrl.ActionState changStateFrom, UnitCtrl.ActionState changStateTo, string describe = "")
        {
            this.currentFrameCount = currentFrameCount;
            this.realFrameCount = 0;
            this.changStateFrom = changStateFrom;
            this.changStateTo = changStateTo;
            this.describe = describe;
            operation = null;
            id = 0;
        }
        public string GetMainDescribe()
        {
            return "切换到" + GuildSkillGroupPrefab.stateNames[(int)changStateTo] + "状态";
        }
    }
    public class UnitAbnormalStateChangeData
    {
        public bool isFinish;
        public bool enable;
        public int startFrameCount;
        public int endFrameCount;
        public UnitCtrl.eAbnormalState CurrentAbnormalState;
        public FloatWithEx MainValue;
        public float Time;
        public float Duration;
        public int ActionId;
        public float SubValue;

        public float EnergyReduceRate;
        public bool IsEnergyReduceMode;

        public bool IsDamageRelease;
        public bool IsReleasedByDamage;
        public int AbsorberValue;

        public string SkillName;
        public string SourceName;
        //public Skill Skill;
        //public UnitCtrl Source;
        public bool isBuff;
        public UnitCtrl.BuffParamKind BUFF_Type;
        public string GetDescription()
        {
            if (isBuff)
            {
                return ((UnitCtrl.BuffParamKind)BUFF_Type).GetDescription() + " " + MainValue;
            }

            return CurrentAbnormalState.GetDescription();
        }
        public void ShowDetail()
        {
            float total = (endFrameCount - startFrameCount) / 60.0f;
            string detail = "开始时间：" + startFrameCount + "\n结束时间：" + endFrameCount + "\n持续时间：" + total + "\n效果：" + GetDescription();
            MainManager.Instance.WindowConfigMessage(detail, null);
        }
    }

    public class ValueChangeData
    {
        public float xValue { get; set; }
        public float yValue { get; set; }
        public int hp;
        public int damage;
        public string describe;
        public int id;
        public int source;
        public ValueChangeData() { }
        public ValueChangeData(float x, float y)
        {
            xValue = x;
            yValue = y;
        }

        public ValueChangeData(float x, float y, int hp, string describe)
        {
            xValue = x;
            yValue = y;
            this.hp = hp;
            this.describe = describe;
        }
    }
    public class UnitSkillExecData
    {
        public enum SkillState
        {
            [Description("正常释放")]
            NORMAL = 0,
            [Description("被取消")]
            CANCEL = 1
        }
        public int unitid;
        public string UnitName;
        public int skillID;
        public SkillState skillState;//0-正常，1-被取消
        public string skillName;
        public int startTime;
        public int endTime;
        [JsonIgnore]
        public FloatWithEx energy;
        public List<UnitActionExecData> actionExecDatas = new List<UnitActionExecData>();

        public string GetDescribeA()
        {
            return "释放技能" + skillName;
        }
    }
    public class UnitActionExecData
    {
        public enum ActionState
        {
            [Description("正常释放")]
            NORMAL = 0,
            [Description("未命中")]
            MISS = 1,
            [Description("被取消")]
            CANCEL_BY_SKILL = 2,
            [Description("被条件分支取消")]
            CANCEL_BY_IFFORALL = 3,
            [Description("被系统取消")]
            CANCEL_BY_COVENT = 4
        }
        public int unitid;
        public int actionID;
        public string actionType;
        public List<string> targetNames;
        public int execTime;
        public ActionState result;//0-正常触发，1-MISS，2-被取消，3-被排他条件分支取消
        public string describe;
        public List<int> damageList = new List<int>();

        public bool CanCombine(UnitActionExecData data)
        {
            if (unitid == data.unitid && actionID == data.actionID && actionType == data.actionType && execTime == data.execTime && describe == data.describe)
            {
                return true;
            }
            return false;
        }
        public string GetDescribe()
        {
            string names = "";
            for (int i = 0; i < targetNames.Count; i++)
            {
                names += targetNames[i];
                if (i < targetNames.Count - 1)
                {
                    names += "、";
                }
            }
            return "目标：" + names + "," + describe;
        }
    }
    /// <summary>
    /// 存档类，保存到excel用
    /// </summary>
    public class GuildTimelineData
    {
        //public GuildSettingData currentSettingData;
        public int currentRandomSeed;
        public GuildPlayerGroupData playerGroupData;
        [JsonIgnore]
        public Dictionary<int, List<UnitStateChangeData>> allUnitStateChangeDic = new Dictionary<int, List<UnitStateChangeData>>();
        [JsonIgnore]
        public Dictionary<int, List<UnitAbnormalStateChangeData>> allUnitAbnormalStateDic = new Dictionary<int, List<UnitAbnormalStateChangeData>>();
        [JsonIgnore]
        public Dictionary<int, List<ValueChangeData>> allUnitHPDic = new Dictionary<int, List<ValueChangeData>>();
        [JsonIgnore]
        public Dictionary<int, List<ValueChangeData>> allUnitTPDic = new Dictionary<int, List<ValueChangeData>>();
        [JsonIgnore]
        public Dictionary<int, List<UnitSkillExecData>> allUnitSkillExecDic = new Dictionary<int, List<UnitSkillExecData>>();
        [JsonIgnore]
        public Dictionary<int, List<ValueChangeData>> playerUnitDamageDic = new Dictionary<int, List<ValueChangeData>>();
        [JsonIgnore]
        public List<ValueChangeData> bossDefChangeDic = new List<ValueChangeData>();
        [JsonIgnore]
        public List<ValueChangeData> bossMgcDefChangeDic = new List<ValueChangeData>();
        //[JsonIgnore]
        public List<List<float>> UBExecTime = new List<List<float>>();
        [JsonIgnore]
        public List<RandomData> AllRandomList = new List<RandomData>();
        public int exceptDamage;
        public int backDamage;
        [JsonIgnore]
        public List<byte[]> charImages;
        public string timeLineName;
        public string detail;
        [JsonIgnore]
        public List<UBDetail> uBDetails;
        public GuildTimelineData()
        {

        }
        public GuildTimelineData(GuildPlayerGroupData playerGroupData, int seed, Dictionary<int, List<UnitStateChangeData>> allUnitStateChangeDic, Dictionary<int, List<UnitAbnormalStateChangeData>> allUnitAbnormalStateDic, Dictionary<int, List<ValueChangeData>> allUnitHPDic, Dictionary<int, List<ValueChangeData>> allUnitTPDic, Dictionary<int, List<UnitSkillExecData>> allUnitSkillExecDic, Dictionary<int, List<ValueChangeData>> playerUnitDamageDic, List<ValueChangeData> bossDefChangeDic, List<ValueChangeData> bossMgcDefChangeDic, List<RandomData> allRandomList)
        {
            //this.currentSettingData = currentSettingData;
            currentRandomSeed = seed;
            this.playerGroupData = playerGroupData;
            this.allUnitStateChangeDic = allUnitStateChangeDic;
            this.allUnitAbnormalStateDic = allUnitAbnormalStateDic;
            this.allUnitHPDic = allUnitHPDic;
            this.allUnitTPDic = allUnitTPDic;
            this.allUnitSkillExecDic = allUnitSkillExecDic;
            this.playerUnitDamageDic = playerUnitDamageDic;
            this.bossDefChangeDic = bossDefChangeDic;
            this.bossMgcDefChangeDic = bossMgcDefChangeDic;
            AllRandomList = allRandomList;
        }
    }
    public class UBDetail
    {
        [JsonIgnore] public float sorting;
        public bool isBossUB;
        public UnitData unitData;
        public int UBTime;
        public int Damage;
        public bool Critical;
        public string description;
    }
    public class RandomData
    {
        public int id;//第几次调用
        public int frame;
        public int ownerID;
        public int targetID;
        public int actionID;
        //20-恐惧判定
        //19-次数黑暗判定
        //18-清BUFF判定??
        //17-触发器判定,大于则不触发
        //16-不能放UB
        //15-DOT
        //14-沉默
        //13-禁疗，小于则中
        //12-条件分支判定
        //11-减疗，小于则中
        //10-技能随机选择目标判定 (此时target为上限)
        //9-抗性判定，大于等于无效，小于则抵抗，actionid为actiontype
        //8-随机特效判定，actionid为skillid
        //7-魅惑判定,小于则中
        //6-减速加速判定,小于等于则命中,加速必中无视判定
        //5-致盲判定，小于等于则命中
        //4-恐惧判定，小于等于则命中
        //3-黑暗判定，小于则中黑暗
        //2-MISS判定，小于则闪避
        //1-对数盾暴击判定，小于等于则判定暴击（未触发对数盾则无效！）
        //0-暴击判定，小于等于则判定暴击
        public int type;
        public int currentSeed;
        public int randomResult;
        public float targetResult;
        public float criticalDamageRate;
        public RandomData() { }
        public RandomData(UnitCtrl source, UnitCtrl target, int actionid, int type, float targetResult, float criDamage = 0)
        {
            ownerID = source == null ? 0 : source.UnitId;
            targetID = target == null ? 0 : target.UnitId;
            actionID = actionid;
            this.type = type;
            this.targetResult = targetResult;
            criticalDamageRate = criDamage;
        }
        public string GetDescribe()
        {
            string sourceName = MainManager.Instance.GetUnitNickName(ownerID);
            string targetName = MainManager.Instance.GetUnitNickName(targetID);

            switch (type)
            {
                case 0:
                    return sourceName + "对" + targetName + "的暴击判定" + (randomResult <= targetResult * 1000 ? "暴击" : "没暴击");
                case 1:
                    return sourceName + "对" + targetName + "的对数盾暴击判定" + (randomResult <= targetResult * 1000 ? "暴击" : "没暴击");
                case 2:
                    return sourceName + "对" + targetName + "的闪避判定" + (randomResult < targetResult * 1000 ? "MISS" : "命中");
                case 9:
                    return sourceName + "对" + targetName + "的抗性判定" + (randomResult < targetResult * 1000 ? "抵抗" : "不抵抗");
                case 10:
                    return sourceName + "的技能随机选择判定，目标" + randomResult;
                case 15:
                    return sourceName + "对" + targetName + "的DOT判定" + (randomResult < targetResult * 1000 ? "MISS" : "命中");
                default:
                    return sourceName + "对" + targetName + "的其他判定";
            }
        }
        public bool JudgeColored()
        {
            if (actionID >= 100000000)
            {
                int skill = (actionID % 1000) / 100;
                if (skill == 1 && type == 0)
                {
                    return true;
                }
            }
            return false;
        }

    }
}