using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Elements;
using Elements.Battle;
using ExcelDataReader;
using ExcelHelper;
using Newtonsoft.Json;
using OfficeOpenXml;
using PCRCaculator.Battle;
using SFB;
using UnityEngine;
using UnityEngine.Networking.Types;
using UnityEngine.UI;
using XCharts.Runtime;
using static UnityEngine.GraphicsBuffer;
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
        public Func<int, string> exp;
        public bool enabled = true;
        public bool isProb = false;
    }
    public class GuildCalculator : MonoBehaviour
    {
        public static GuildCalculator Instance;
        // public const float deltaXforChat = 1 / 5400.0f;
        public Dictionary<int, List<UnitStateChangeData>> allUnitStateChangeDic;
        public Dictionary<int, List<UnitAbnormalStateChangeData>> allUnitAbnormalStateDic;
        public Dictionary<int, List<ValueChangeData>> allUnitHPDic;
        public Dictionary<int, List<ValueChangeData>> allUnitTPDic;
        public Dictionary<int, List<UnitSkillExecData>> allUnitSkillExecDic;
        public Dictionary<int, List<ValueChangeData>> playerUnitDamageDic;
        public List<ValueChangeData> bossDefChangeDic;
        public List<ValueChangeData> bossMgcDefChangeDic;
        public List<RandomData> AllRandomDataList;
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
        public LineChart lineChart;

        private readonly Dictionary<int, UnitStateChangeData> allUnitLastStateDic = new Dictionary<int, UnitStateChangeData>();
        private readonly List<int> playerIds = new List<int>();
        private int bossId;
        private List<UnitCtrl> players;
        private UnitCtrl boss;
        private readonly Dictionary<int, GuildSkillGroupPrefab> skillGroupPrefabDic = new Dictionary<int, GuildSkillGroupPrefab>();
        private long totalDamage;
        private long totalDamageCriEX;
        private FloatWithEx totalDamageExcept = 0;
        public bool isFinishCalc;
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

        public long GetTotalDamage()
        {
            return totalDamage;
        }

        public long GetTotalDamageExpect()
        {
            return (long)totalDamageExcept.Expect;
        }

        public void Initialize(List<UnitCtrl> players, UnitCtrl boss)
        {
            foreach (GuildSkillGroupPrefab guildSkillGroupPrefab in skillGroupPrefabDic.Values)
            {
                Destroy(guildSkillGroupPrefab.gameObject);
            }
            skillScrollRect.horizontalNormalizedPosition = 0;
            skillGroupPrefabDic.Clear();
            isFinishCalc = false;
            totalDamage = 0;
            totalDamageCriEX = 0;
            totalDamageExcept = 0;
            allUnitLastStateDic.Clear();
            playerIds.Clear();
            allUnitStateChangeDic = new Dictionary<int, List<UnitStateChangeData>>();
            allUnitAbnormalStateDic = new Dictionary<int, List<UnitAbnormalStateChangeData>>();
            allUnitHPDic = new Dictionary<int, List<ValueChangeData>>();
            allUnitTPDic = new Dictionary<int, List<ValueChangeData>>();
            allUnitSkillExecDic = new Dictionary<int, List<UnitSkillExecData>>();
            playerUnitDamageDic = new Dictionary<int, List<ValueChangeData>>();
            bossDefChangeDic = new List<ValueChangeData>();
            bossMgcDefChangeDic = new List<ValueChangeData>();
            AllRandomDataList = new List<RandomData>();
            skillGroupList = new List<int>();
            skillGroupOrder = new Dictionary<int, int>();
            unit_state_id = new Dictionary<int, int>();
            hpCache = new List<ValueChangeData>();
            tpCache = new List<ValueChangeData>();
            lastHp = new List<ValueChangeData>();
            lastTp = new List<ValueChangeData>();
            templateSettings.Clear();
            dmglist.Clear();
            bossValues.Clear();

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

            RefreshTotalDamage(false, 0, false, 0, 0);
            CurrentBossText.text = MainManager.Instance.GuildBattleData.SettingData.GetCurrentBossDes();
            currentSeedText.text = "随机数种子：" + MyGameCtrl.Instance.CurrentSeedForSave;
            int idx = 0;
            bossId = boss.UnitId;
            AddSkillGroupPrefab(boss.UnitId, boss, 0);
            boss.NoSkipOnDamage += AppendGetDamage;
            boss.NoSkipOnBaseValueChanged += AppendChangeBaseValue;
            foreach (UnitCtrl unitCtrl in players)
            {
                idx++;
                AddSkillGroupPrefab(unitCtrl.UnitId, unitCtrl, idx);
                PlayerIds.Add(unitCtrl.UnitId);
            }
            playerUnitDamageDic.Add(0, new List<ValueChangeData> { new ValueChangeData(0, 0) });
            foreach (var (part, def, mdef) in boss.IsPartsBoss ? boss.BossPartsListForBattle.Select(x => (x.Index, x.Def, x.MagicDef)) : 
                         new[] {(0, boss.Def, boss.MagicDef)})
            {
                AppendChangeBaseValue(bossId + part * 10, 1, def, 0, "初始化");
                AppendChangeBaseValue(bossId + part * 10, 2, mdef, 0, "初始化");
            }

            lineChart.transform.SetParent(skillGroupParent);

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
                allUnitHPDic.Add(a, new List<ValueChangeData> { new ValueChangeData(0, b.Hp, (int)b.Hp, "初始化"){target = a} });
                allUnitTPDic.Add(a, new List<ValueChangeData> { new ValueChangeData(0, 0){describe = "初始化", target = a} });
                allUnitLastStateDic.Add(a, new UnitStateChangeData(0, UnitCtrl.ActionState.GAME_START, UnitCtrl.ActionState.GAME_START));
                allUnitSkillExecDic.Add(a, new List<UnitSkillExecData>());
            }
            b.NoSkipOnChangeState += AppendChangeState;
            if (b.UnitId >= 300000 && b.UnitId <= 400000)
            {
                b.NoSkipOnDamage2 += RefreshTotalDamage;
            }
            else if (isPlayer)
            {
                playerUnitDamageDic.Add(b.UnitId, new List<ValueChangeData> { new ValueChangeData(0, 0) });
            }
            b.NoSkipOnAbnormalStateChange += AppendChangeAbnormalState;
            b.NoSkipOnLifeChanged += AppendChangeHP;
            b.NoSkipOnTPChanged += AppendChangeTP;
            b.NoSkipOnStartAction += AppendStartSkill;
            b.NoSkipOnExecAction += AppendExecAction;
            if (create)
            {
                AddSkillGroups(a, c, c, b.UnitName);
            }

            //AddSkillGroups(a, c, c,isPlayer?b.unitData.GetNicName():b.UnitName);

        }

    private int id;
    private Dictionary<int, int> unit_state_id;
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
                    //skillGroupPrefabDic[unitid].AddButtons(allUnitLastStateDic[unitid].currentFrameCount, frameCount, (int)actionState);
                    Action action = null;
                    int startFrame = allUnitLastStateDic[unitid].currentFrameCount;
                    int oldState = (int)allUnitLastStateDic[unitid].changStateTo;
                    int variant = 0;
                    if (oldState >= 1 && oldState <= 3)
                    {
                        UnitSkillExecData skillExecData = allUnitSkillExecDic[unitid].FindLast(a => a.startTime == startFrame);
                        if (skillExecData != null)
                        {
                            skillExecData.endTime = frameCount;
                            action = () => { OpenSkillDetailPannel(skillExecData); };
                            if (skillExecData.skillID < 2000000)
                                variant = skillExecData.skillID % 1000;
                            else
                                variant = skillExecData.skillID % 10;
                        }
                    }
                    var newData = new UnitStateChangeData
                    {
                        id = ++id,
                        changStateFrom = allUnitLastStateDic[unitid].changStateTo,
                        changStateTo = actionState,
                        currentFrameCount = frameCount,
                        realFrameCount = BattleManager.Instance.FrameCount,
                        operation = actionState == UnitCtrl.ActionState.SKILL_1 ? ctrl.GetCurrentOp() : null,
                        variant = variant
                    };
                    allUnitStateChangeDic[unitid].Add(newData);
                    unit_state_id[unitid]=newData.id;
                    var trans = skillGroupPrefabDic[unitid].AddButtons(startFrame, frameCount, oldState, action, variant);

                    var pos = skillGroupParent.transform.InverseTransformPoint(trans.position);

                    skillScrollRect.horizontalNormalizedPosition = 
                        Math.Max(skillScrollRect.horizontalNormalizedPosition, pos.x / 4200f - 0.06f); 
                                                            // width of scroll rect; foresee
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
            var sourceId = source?.UnitId ?? unitid;
            ValueChangeData valueData = new ValueChangeData(frame, currentHP, hp, describe)
            {
                damage = damage,
                id = unit_state_id.ContainsKey(sourceId) ? unit_state_id[sourceId] : 0,
                source= sourceId,
                target = unitid,
            };
            allUnitHPDic[unitid].Add(valueData);
            if (!GuildManager.StaticsettingData.calcSpineAnimation && currentToggleId == 2)
            {
                AppendHpDrawData(valueData);
                // skillGroupPrefabDic[unitid].RefreshHPChat(allUnitHPDic[unitid]);
            }
        }
        public void AppendChangeTP(int unitid, float currentTP, int frame, string describe)
        {
            if (unitid >= 400000 && !allUnitTPDic.ContainsKey(unitid)) { return; }

            var t = new ValueChangeData(frame, currentTP, 0, describe)
            {
                target = unitid
            };
            allUnitTPDic[unitid].Add(t);
            if (!GuildManager.StaticsettingData.calcSpineAnimation && currentToggleId == 3)
            {
                AppendTpDrawData(t);
                // skillGroupPrefabDic[unitid].RefreshTPChat(allUnitTPDic[unitid]);
            }
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
                playerUnitDamageDic[sourceUnitid].Add(new ValueChangeData(currentFrame, damage));
                playerUnitDamageDic[0].Add(new ValueChangeData(currentFrame, damage));
            }
        }
        public void AppendChangeBaseValue(int unitid, int valueType, float value, int currentFrame, string describe)
        {
            if (BattleManager.Instance.skipping) return;
            if (unitid - unitid / 10 % 10 * 10 == bossId)
            {
                if (value < 0)
                {
                    value = 0;
                }
                switch (valueType)
                {
                    case 1:
                        bossDefChangeDic.Add(new ValueChangeData(currentFrame, value, 0, describe)
                        {
                            target = unitid / 10 % 10
                        });
                        break;
                    case 2:
                        bossMgcDefChangeDic.Add(new ValueChangeData(currentFrame, value, 0, describe)
                        {
                            target = unitid / 10 % 10
                        });
                        break;
                }
            }
        }
        public void AppendCallRandom(RandomData data)
        {
            data.frame = BattleHeaderController.CurrentFrameCount;
            data.currentSeed = Random.seed;
            AllRandomDataList.Add(data);
        }

        private int currentToggleId;

        public void OnToggleSwitched(int toggleId)
        {
            currentToggleId = toggleId;
            foreach (var toggle in ModeToggles)
            {
                // 获取子对象 Label 的 Text 组件
                Text label = toggle.GetComponentInChildren<Text>();
                // 如果是当前 toggleId，则设置为白色
                if (toggle.isOn)
                {
                    label.color = Color.white;
                }
                else
                {
                  // 否则设置为默认颜色 #323232
                    label.color = new Color32(0x32, 0x32, 0x32, 0xFF);
                }
            }
            if (ModeToggles[toggleId].isOn)
            {
                foreach (GuildSkillGroupPrefab sk in skillGroupPrefabDic.Values)
                {
                    sk.SwitchPage(toggleId);
                }
                ResizePrefabs(toggleId == 1);

                lineChart.SetActive(toggleId > 1);

                RefreshLineChart();
            }
        }

        private int hpXPos;
        private int tpXPos;
        private void RefreshLineChart()
        {
            switch (currentToggleId)
            {
                // hp
                case 2:
                    hpXPos = DrawData(skillGroupList.Select(x => allUnitHPDic[x]).ToArray(),
                        GetHpDescription,
                        skillGroupList.Select(x => (float)allUnitHPDic[x].First().hp).ToArray(),
                        ref lastHp);
                    hpCache.Clear();
                    hpCacheFrame = hpXPos;
                    break;
                // tp
                case 3:
                    tpXPos = DrawData(skillGroupList.Select(x => allUnitTPDic[x]).ToArray(),
                        GetTpDescription,
                        skillGroupList.Select(_ => (float) UnitDefine.MAX_ENERGY).ToArray(),
                        ref lastTp);
                    tpCache.Clear();
                    tpCacheFrame = tpXPos;
                    break;
            }
        }

        private static string GetTpDescription(ValueChangeData x)
        {
            return $"{(int) (x.yValue * 1000) / 1000f:F3} {MainManager.Instance.GetUnitNickName(x.target)}\n" + $"来源：{MainManager.Instance.GetUnitNickName(x.source)}\n" + $"([{x.xValue}] {x.describe})";
        }

        private static string GetHpDescription(ValueChangeData x)
        {
            return $"{x.yValue}/{x.hp} {MainManager.Instance.GetUnitNickName(x.target)}\n" + $"来源：{MainManager.Instance.GetUnitNickName(x.source)}\n" + $"[{x.xValue}] {x.describe}";
        }

        private int hpCacheFrame = -1;
        private List<ValueChangeData> hpCache;
        private int tpCacheFrame = -1;
        private List<ValueChangeData> tpCache;

        private List<ValueChangeData> lastHp;
        private List<ValueChangeData> lastTp;

        private void AppendHpDrawData(ValueChangeData x)
        {
            if (hpCacheFrame < x.xValue)
            {
                for (++hpXPos; hpXPos <= hpCacheFrame; ++hpXPos)
                {
                    if (hpXPos == hpCacheFrame)
                    {
                        foreach (var data in hpCache)
                            lastHp[skillGroupOrder[data.target]] = data;
                    }
                    lineChart.AddXAxisData(hpXPos.ToString());
                    for (var i = 0; i < lastHp.Count; ++i)
                    {
                        lastHp[i] ??= ValueChangeData.Default;
                        lineChart.AddData(i, hpXPos, Mathf.Clamp01(lastHp[i].yValue / lastHp[i].hp) * PREFAB_SCALE - i + 100, GetHpDescription(lastHp[i]));
                    }
                }

                hpXPos = hpCacheFrame;
                hpCacheFrame = x.xValue;
                hpCache.Clear();
            }
            hpCache.Add(x);
        }

        private void AppendTpDrawData(ValueChangeData x)
        {
            if (tpCacheFrame < x.xValue)
            {
                for (++tpXPos; tpXPos <= tpCacheFrame; ++tpXPos)
                {
                    if (tpXPos == tpCacheFrame)
                    {
                        foreach (var data in tpCache)
                            lastTp[skillGroupOrder[data.target]] = data;
                    }
                    lineChart.AddXAxisData(tpXPos.ToString());
                    for (var i = 0; i < lastTp.Count; ++i)
                    {
                        lastTp[i] ??= ValueChangeData.Default;
                        lineChart.AddData(i, tpXPos, Mathf.Clamp01(lastTp[i].yValue / UnitDefine.MAX_ENERGY) * PREFAB_SCALE - i + 100, GetTpDescription(lastTp[i]));
                    }
                }

                tpXPos = tpCacheFrame;
                tpCacheFrame = x.xValue;
                tpCache.Clear();
            }
            tpCache.Add(x);
        }

        private void AddLine()
        {
            var serie = lineChart.AddSerie<Line>();
            serie.lineType = LineType.StepEnd;
            serie.lineStyle.width = .5f;
            
            var serieColor = SerieHelper.GetLineColor(serie, null, lineChart.theme, -1, SerieState.Normal);
            
            serie.areaStyle = new AreaStyle
            {
                show = true,
                opacity = .6f,
                color = serieColor
            };

            serie.areaZero = 100 - serie.index;
        }

        private const float PREFAB_SCALE = 35f / 38;
        
        private int DrawData(List<ValueChangeData>[] data, Func<ValueChangeData, string> descriptionFunc,
            float[] serieMaxValue, ref List<ValueChangeData> last)
        {
            lineChart.RemoveData();
            
            if (data.Length == 0) return -1;

            var xpos = Enumerable.Range(0, data.SelectMany(x => x).Max(x => x.xValue)).ToArray();

            var nowx = new int[data.Length];

            last = Enumerable.Range(0, data.Length).Select(_ => default(ValueChangeData)).ToList();

            foreach (var _ in serieMaxValue)
            {
                AddLine();
            }

            foreach (var x in xpos)
            {
                lineChart.AddXAxisData(x.ToString());
                for (var i = 0; i < data.Length; ++i)
                {
                    while (nowx[i] + 1 < data[i].Count && data[i][nowx[i] + 1].xValue <= x)
                        ++nowx[i];
                    var t = data[i][nowx[i]];
                    last[i] = t;
                    lineChart.AddData(i, x, Mathf.Clamp01(t.yValue / serieMaxValue[i]) * PREFAB_SCALE  - i + 100, descriptionFunc(t));
                }
            }

            var axis = lineChart.GetChartComponent<YAxis>();
            axis.max = PREFAB_SCALE + 100; // 少一个interval
            axis.min = 101 - data.Length;
            
            return xpos.Length == 0 ? -1 : xpos.Max();
        }

        private RectTransform headAnchor, tailAnchor;
        private List<int> skillGroupList;
        private Dictionary<int, int> skillGroupOrder;

        private void AddSkillGroups(int unitid, int idx, int colorIdx, string Name)
        {
            GameObject a = Instantiate(skillGroupPrefab);
            a.transform.SetParent(skillGroupParent, false);
            a.transform.localPosition = skillGroupBasePos + idx * skillGroupAddPos;

            var source = a.GetComponent<RectTransform>();

            if (skillGroupPrefabDic.Count == 0)
                headAnchor = source;
            tailAnchor = source;

            
            GuildSkillGroupPrefab guildSkill = a.GetComponent<GuildSkillGroupPrefab>();
            /*
            if (colorIdx >= skillGroupColors.Count)
            {
                colorIdx = skillGroupColors.Count - 1;
            }
            */
            var color = lineChart.theme.GetColor(colorIdx);
            color = Color.Lerp(color, Color.white, .7f);

            guildSkill.Initialize(Name, color/*skillGroupColors[colorIdx]*/);
            skillGroupPrefabDic.Add(unitid, guildSkill);

            skillGroupList.Add(unitid);
            var order = skillGroupOrder.Count;
            skillGroupOrder.Add(unitid, order);

            if (!GuildManager.StaticsettingData.calcSpineAnimation)
            {
                if (currentToggleId == 2)
                {
                    AddLine();
                    for (int i = 0; i <= hpXPos; ++i)
                        lineChart.AddData(order, i, 0);
                    lastHp.Add(ValueChangeData.Default);

                    var axis = lineChart.GetChartComponent<YAxis>();
                    axis.max = PREFAB_SCALE + 100; // 少一个interval
                    axis.min = 101 - lastHp.Count;
                    
                }
                else if (currentToggleId == 3)
                {
                    AddLine();
                    for (int i = 0; i <= tpXPos; ++i)
                        lineChart.AddData(order, i, 0);
                    lastTp.Add(ValueChangeData.Default);

                    var axis = lineChart.GetChartComponent<YAxis>();
                    axis.max = PREFAB_SCALE + 100; // 少一个interval
                    axis.min = 101 - lastTp.Count;
                    
                }

            }
        }
        
        public void RefreshTotalDamage(bool byAttack, float value, bool critical, float criticalEXdamage, FloatWithEx exvalue)
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
                AppendChangeHP(unitid, unitCtrl.Hp, (int)unitCtrl.MaxHp, (int)unitCtrl.Hp, 5400, "时间耗尽", unitCtrl);
                AppendChangeTP(unitid, (float)unitCtrl.Energy, 5400, "时间耗尽");
                if (allUnitAbnormalStateDic.TryGetValue(unitid, out var value))
                {
                  foreach (UnitAbnormalStateChangeData changeData in allUnitAbnormalStateDic[unitid])
                  {
                    changeData.endFrameCount = 5400;
                    changeData.isFinish = true;
                    skillGroupPrefabDic[unitid].AddAbnormalStateButtons(changeData);
                  }
                }
                else
                {
                  Debug.Log($"Key '{unitid}' not found in the dictionary.");
                }
              }
            };

            if (!BattleManager.Instance.skipping)
            {
                action(MyGameCtrl.Instance.playerUnitCtrl);
                action(MyGameCtrl.Instance.enemyUnitCtrl);
            }
            /*
            foreach (var pair in skillGroupPrefabDic)
            {
                pair.Value.RefreshHPChat(allUnitHPDic[pair.Key]);
                pair.Value.RefreshTPChat(allUnitTPDic[pair.Key]);
            }
            */
            foreach (var (part, def, mdef) in boss.IsPartsBoss ? boss.BossPartsListForBattle.Select(x => (x.Index, x.Def, x.MagicDef)) :
                         new[] { (0, boss.Def, boss.MagicDef) })
            {
                AppendChangeBaseValue(bossId + part * 10, 1, def, 5400, "时间耗尽");
                AppendChangeBaseValue(bossId + part * 10, 2, mdef, 5400, "时间耗尽");
            }

            skillScrollRect.horizontalNormalizedPosition = 1;
            guildPageUI.SetActive(true);
            isFinishCalc = true;
            chatPannel.Init();
            OnToggleSwitched(0);
            if (currentFrame < 5400)
            {
                backTime = Mathf.CeilToInt((MyGameCtrl.Instance.tempData.SettingData.limitTime * 60 - currentFrame) / 60.0f) + 20;
                RefreshTotalDamage(true, 0, false, 0, 0);
            }

            var n = GuildManager.StaticsettingData.n1;
            expectedDamage = (int)totalDamageExcept.Expected(n);
            string damageStr =
                $"{totalDamage}({(totalDamage - totalDamageCriEX)}+<color=#FFEC00>{totalDamageCriEX}</color>)\n<color=#56A0FF>{expectedDamage}({totalDamageExcept.Expect})±{(int)totalDamageExcept.Stddev}</color>";
            if (MyGameCtrl.Instance.tempData.SettingData.limitTime != 90)
            {
                detail = $" 尾刀{MyGameCtrl.Instance.tempData.SettingData.limitTime}s";
            }
            else if (backTime > 0)
            {
                detail = $" 返{backTime}s-{boss.Hp.Probability(x => x <= 0f):P0}";
            }
            else
            { 
                detail = string.Empty;
            }
            damageStr += detail;
            damageStr += $" 随机数种子：{MyGameCtrl.Instance.CurrentSeedForSave}";
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

            var target = lineChart.GetComponent<RectTransform>();

            const int TEXT_WIDTH = 80;

            target.localPosition = headAnchor.localPosition + TEXT_WIDTH * Vector3.right;
            target.sizeDelta = -(Vector2)(tailAnchor.localPosition - headAnchor.localPosition) + tailAnchor.sizeDelta 
                               + TEXT_WIDTH * Vector2.left;

            target.SetAsLastSibling();
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
                 chatPannel.Refresh();
                chatPannel.gameObject.SetActive(true);
            }
        }
        public void SaveFileToExcel()
        {
#if PLATFORM_ANDROID
            MainManager.Instance.WindowConfigMessage("手机端导出可能失败，建议使用电脑端导出！\n如要尝试请按确定继续", CallExcelHelper);
#else
            CallExcelHelper();
#endif
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
                Debug.LogError(a.ToString());
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
                    .Select((t, i) => (t, normalized: t.state.currentFrameCount + i * 0.001f)))
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
                            sorting = tm.realFrameCount,
                            UBTime = tm.currentFrameCount,
                            description = tm.operation ?? string.Empty
                        };
                        if (allUnitHPDic.ContainsKey(bossId) && allUnitHPDic[bossId] != null)
                        {
                            ValueChangeData changeData = allUnitHPDic[bossId].Find(
                            a => /* Mathf.RoundToInt(a.xValue * 5400) == tm.currentFrameCount && */
                                a.id == tm.id && a.source == unitData.unitId);
                            // if (changeData == null)
                            // {
                            //     // 再次查找满足 a.source == unitData.unitId && a.xValue == tm.currentFrameCount 的元素
                            //     changeData = allUnitHPDic[bossId].Find(
                            //         a => a.source == unitData.unitId && a.xValue == tm.currentFrameCount);
                            // }
                            if (changeData != null)
                            {
                                detail.Damage = changeData.damage;
                                detail.Critical = changeData.describe.Contains("暴击");
                                int totalDamage = 0;
                                foreach (var data in allUnitHPDic[bossId])
                                {
                                    if (data.id == changeData.id)
                                    {
                                      totalDamage += data.damage;
                                      detail.totalDamage = totalDamage;
                                    }
                                }
                            }
                            else
                            {
                                detail.Damage = 0;
                            }
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
                        sorting = data.realFrameCount,
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
            if (MyGameCtrl.Instance.tempData.SettingData.limitTime != 90)
            {
                fileName += $"-尾刀{MyGameCtrl.Instance.tempData.SettingData.limitTime}s";
            }
            else if (backTime > 0)
            {
                fileName += $"-返{backTime}s-{boss.Hp.Probability(x => x <= 0f):P0}";
            }
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
                if (ubExecTime.Count > i && MyGameCtrl.Instance.tempData.UBExecTimeList.Count > i)
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
            return data;/*
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
            return newData;*/
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
            return data;/*
            List<ValueChangeData> newData = new List<ValueChangeData>();
            if (data.Count >= 1)
            {
                newData.Add(data[0]);
            }
            for (int i = 1; i < data.Count; i++)
            {
                if (data[i].xValue > data[i - 1].xValue + deltaXforChat)
                {
                    newData.Add(new ValueChangeData(data[i].xValue - 1, data[i - 1].yValue));
                }
                
                if (i == data.Count - 1 || (data[i].xValue < data[i + 1].xValue + 3 + 1))
                {
                    newData.Add(new ValueChangeData(data[i].xValue + 3, data[i].yValue));
                    newData.Add(new ValueChangeData(data[i].xValue + 3 + 1, 0));
                }
                newData.Add(data[i]);
            }
            return newData;*/
        }
        public static List<ValueChangeData> NormalizeLineChatData(List<ValueChangeData> data, float Max)
        {
            return data;
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
                var fileName = CreateExcelName();
#if PLATFORM_ANDROID
#else
                var ststrr = StandaloneFileBrowser.SaveFilePanel(
                    "保存Excel", string.Empty, fileName, "txt");
                if (!string.IsNullOrEmpty(ststrr))
                {
                    fileName = ststrr;
                    fileName = fileName.Replace("\\", "/");
                }
                else return;
                
#endif
                const string dmginfo = "标伤到达率";
                const string dmginfo2 = "标伤到达率(未乱轴)";
                const string totaldie = "总乱轴（上限）";
                const string totaldie2 = "总乱轴（下限，不计入穿盾、break）";

                const string mb1 = "1+满补";
                const string mb2 = "2+满补";
                const string d1 = "1刀";
                const string d2 = "2刀";

                var dname = new Dictionary<string, int>();
                var dname2 = new Dictionary<string, int>();
                var dskill = new Dictionary<string, Dictionary<string, int>>();
                var seedset = new Dictionary<string, int>();
                var dmg = new List<float>();
                var probs = new Dictionary<string, ProbEvent>();
                foreach (var name in dmglist.Select(n => n.unit).Distinct())
                {
                    dname2.Add(name, 0);
                    dname.Add(name, 0);
                }
                dname.Add(dmginfo, 0);
                dname.Add(dmginfo2, 0);
                dname.Add(totaldie, 0);
                dname.Add(totaldie2, 0);
                
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
                    var seed = rnd.Next();
                    FloatWithEx.SetState(seed);
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
                                probs[evt.description] = evt;
                            }
                        }
                    }

                    if (sname.Count > 0)
                    {
                        ++dname[totaldie];
                        if (flag) ++dname[totaldie2];
                        if (action(hash)) ++dname[dmginfo];
                    }
                    else if (action(hash))
                    {
                        ++dname[dmginfo];
                        ++dname[dmginfo2];
                    }

                    foreach (var name in sname) ++dname[name];
                    foreach (var name in sname2) ++dname2[name];
                    foreach ((string name, string source) in sskill)
                    {
                        ++dskill[name][source];
                        seedset[source] = seed;
                    }
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
#if PLATFORM_ANDROID
                var stream = new MemoryStream();
#else
                if (File.Exists(fileName)) File.Delete(fileName);
                var stream = File.OpenWrite(fileName);
#endif
                using (var sw = new StreamWriter(stream))
                {
                    foreach (var name in dname.OrderByDescending(p => p.Value).Select(p => p.Key))
                    {
                        var real = dname2.ContainsKey(name)
                            ? $"({(float)dname2[name] / GuildManager.StaticsettingData.n2:P1})"
                            : string.Empty;
                        sw.WriteLine($"{name}-{(float)dname[name] / GuildManager.StaticsettingData.n2:P1}" + real);
                        if (!dskill.ContainsKey(name)) continue;
                        foreach (var pair in dskill[name].OrderByDescending(p => p.Value).Where(p => p.Value > 0))
                        {
                            var prob = probs[pair.Key];
                            var exp = string.Empty;
                            if (prob.exp != null && ExcelHelper.ExcelHelper.AsmExportEnabled)
                            {
                                exp = prob.exp(rnd.Next());
                            }
                            sw.WriteLine("\t{0}-{1:P1}(seed={2})\n{3}", pair.Key, (float) pair.Value / GuildManager.StaticsettingData.n2, seedset[pair.Key], exp);
                        }
                    }

                    if (ExcelHelper.ExcelHelper.AsmExportEnabled)
                    {
                        sw.WriteLine(totalDamageExcept.ToExpression(rnd.Next()));

                        if (BattleManager.Instance.scriptMgr != null)
                        {
                            foreach (var data in BattleManager.Instance.scriptMgr.additionalData)
                                sw.WriteLine($"{data.comment}: {data.data?.ToExpression(rnd.Next())}");
                        }
                    }
                }
#if PLATFORM_ANDROID
                    BaseBackManager.Instance.ShowText(Encoding.UTF8.GetString(stream.ToArray()));
#else
                MainManager.Instance.WindowMessage("成功！");
#endif
                stream.Dispose();
            }
            catch (Exception e)
            {
                MainManager.Instance.WindowMessage(e.ToString());
                throw;
            }
        }

        public void SaveFileToTemplate()
        {
            if(Application.platform == RuntimePlatform.Android)
            {
                MainManager.Instance.WindowConfigMessage("Android端暂时不支持！",null);
                return;
            }
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
            var str2 = StandaloneFileBrowser.OpenFilePanel(
                "打开Excel", Path.Combine(MainManager.GetSaveDataPath(), "Templates"),
                "xlsx", false);
            string file = null;
            if (str2.Length > 0)
            {
                file = str2[0];
                file = file.Replace("\\", "/");
            }
            
            templateSettings.Clear();
            using (IExcelDataReader reader =
                   ExcelReaderFactory.CreateReader(File.Open(file, FileMode.Open)))
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
            ExcelPackage src = new ExcelPackage(new FileInfo(file));

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


            var ststrr = StandaloneFileBrowser.SaveFilePanel(
                "保存Excel", string.Empty, fileName, "xlsx");
            if (!string.IsNullOrEmpty(ststrr))
            {
                fileName = ststrr;
                fileName = fileName.Replace("\\", "/");
            }
            else return;
            
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
                sheet.Cells[templateSettings["BOSS名称坐标"]].Value = MyGameCtrl.Instance.tempData.guildEnemy[0].detailData.unit_name;
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
        public int id, variant;
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
            id = variant = 0;
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

    public interface IValue
    {
        public float xValue { get; }
        public float yValue { get; }
    }

    public class ValueChangeData : IValue
    {
        public int xValue { get; set; }
        public float yValue { get; set; }
        float IValue.xValue => xValue / XScale;
        float IValue.yValue => yValue / YScale;

        public int hp;
        public int damage;
        public int expectedDamage;
        public string describe;
        public int id;
        public int source;
        public int target;
        public int realFrameCount;
        private const float XScale = 5400f;
        private float YScale => (hp == 0) ? UnitDefine.MAX_ENERGY : hp;
        
        public ValueChangeData() { }
        public ValueChangeData(int x, float y)
        {
            xValue = x;
            yValue = y;
            realFrameCount = BattleManager.Instance?.FrameCount ?? 0;
        }

        public static readonly ValueChangeData Default = new ValueChangeData(-1, 0, 1, string.Empty);
        public ValueChangeData(int x, float y, int hp, string describe)
        {
            xValue = x;
            yValue = y;
            this.hp = hp;
            this.describe = describe;
            realFrameCount = BattleManager.Instance?.FrameCount ?? 0;
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
        public int totalDamage;
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