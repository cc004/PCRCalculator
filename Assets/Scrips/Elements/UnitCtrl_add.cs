﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using PCRCaculator;
using PCRCaculator.Battle;
using PCRCaculator.Guild;
using UnityEngine;

namespace Elements
{
    public partial class UnitCtrl
    {

        // Token: 0x020006C2 RID: 1730
        public enum eOverwriteAbnormalState
        {
            // Token: 0x0400282F RID: 10287
            NONE,
            // Token: 0x04002830 RID: 10288
            SLOW,
            // Token: 0x04002831 RID: 10289
            HASTE
        }

        internal CharacterPageButton unitUI;
        private CharacterBuffUIController unitBuffUI;
        private BattleUIManager uIManager;
        public PCRCaculator.UnitData unitData;
        public UnitParameter unitParameter;
        public string UnitName = "", UnitNameEx = "";
        public int posIdx;//0-4
        public Action<int,ActionState, int,string, UnitCtrl> NoSkipOnChangeState;
        public Action<int,ActionState> SemanOnChangeState;
        public Action<int, UnitAbnormalStateChangeData, int> NoSkipOnAbnormalStateChange;
        public Action<int,float,int, int,int,string, UnitCtrl> NoSkipOnLifeChanged;
        public Action<int,float, int,string> NoSkipOnTPChanged;
        public Action<int,float> SemanOnTPChanged;
        public Action<int, UnitSkillExecData> NoSkipOnStartAction;
        public Action<int, int> SemanOnStartAction;
        public Action<int, int, UnitActionExecData> NoSkipOnExecAction;
        public Action<int, int> SemanOnExecAction;
        public Action<int, int, float, int> NoSkipOnDamage;
        public Action<bool, float, bool, float, FloatWithEx> NoSkipOnDamage2;
        public Action<int, int, float, int,string> NoSkipOnBaseValueChanged;
        public Action<UnitCtrl, eStateIconType, bool,float,string> NoSkipOnChangeAbnormalState;
        public CharacterPageButton button;
        public Action<int, int, float, float> NoSkipOnChangeSkillID;
        public Action<float> NoSkipOnSkillCD;
        public Action<float> NoSkipOnUsingSkill;
        public Dictionary<int, int> MySkillExecDic = new Dictionary<int, int>();

        public BattleUIManager UIManager { get => BattleUIManager.Instance; }
        private const float BOSS_DELTA_FIX = -1f;

        private GuildPlayerGroupData.LogBarrierType useLogBarrier;
        private static GuildPlayerGroupData group => MyGameCtrl.Instance.tempData.SettingData.GetCurrentPlayerGroup();

        public void SetUI(CharacterPageButton ui, CharacterBuffUIController buffui)
        {
            uIManager = BattleUIManager.Instance;
            unitUI = ui;
            unitUI.SetButton(this);
            unitBuffUI = buffui;
            ui.SetHP(LifeAmount);
            ui.SetMaxHP(MaxLifeAmount);
            ui.SetTP(EnegryAmount);
            //this.unitBuffUI.SetLeftDir(this.IsOther);
            OnLifeAmmountChange += ui.SetHP;
            OnMaxLifeAmmountChange += ui.SetMaxHP;
        }
        public void SetUIForSuommon()
        {
            uIManager = BattleUIManager.Instance;
        }
        [ContextMenu("生成unitCtrl.json文件")]
        public void SaveUnitCtrlData()
        {
            //Debug.Log(gameObject.name.Substring(5, 6));
            int unitId = UnitId;//int.Parse(gameObject.name.Substring(5, 6));
            var jsonObject = new UnitCtrlData(unitId, ShowTitleDelay, UnitAppearDelay, BossAppearDelay, BattleCameraSize, Scale, BossDeltaX, BossDeltaY, AllUnitCenter,
                BossBodyWidthOffset, SummonTargetAttachmentName, SummonAppliedAttachmentName, IsGameStartDepthBack, BossSortIsBack, DisableFlash, IsForceLeftDir
                ,BossPartsList,UseTargetCursorOver);
            string fileName = gameObject.name.Remove(11);
            string filePath = Application.dataPath + "/Resources/unitCtrlData/" + fileName + ".json";
            //string filePath = Application.streamingAssetsPath + "/Datas/unitCtrlData" + fileName + ".json";
            string saveJsonStr = JsonConvert.SerializeObject(jsonObject);
            StreamWriter sw = new StreamWriter(filePath);
            sw.Write(saveJsonStr);
            sw.Close();
            Debug.Log("成功！" + filePath);
        }
        public void SetUnitCtrl(UnitCtrlData data)
        {
            ShowTitleDelay = data.ShowTitleDelay;
            UnitAppearDelay = data.UnitAppearDelay;
            BossAppearDelay = data.BossAppearDelay;
            BattleCameraSize = data.BattleCameraSize;
            Scale = data.Scale/50;
            BossDeltaX = data.BossDeltaX*BOSS_DELTA_FIX;
            BossDeltaY = data.BossDeltaY;
            AllUnitCenter = data.AllUnitCenter;
            BossBodyWidthOffset = data.BossBodyWidthOffset;
            SummonTargetAttachmentName = data.SummonTargetAttachmentName;
            SummonAppliedAttachmentName = data.SummonAppliedAttachmentName;
            IsGameStartDepthBack = data.IsGameStartDepthBack;
            BossSortIsBack = data.BossSortIsBack;
            DisableFlash = data.DisableFlash;
            IsForceLeftDir = data.IsForceLeftDir;
            BossPartsList = data.BossPartsList;
            UseTargetCursorOver = data.UseTargetCursorOver;
        }
        
        private void CallBackAbnormalStateChanged(AbnormalStateCategoryData data)
        {
            UnitAbnormalStateChangeData stateChangeData = new UnitAbnormalStateChangeData();
            stateChangeData.AbsorberValue = data.AbsorberValue;
            stateChangeData.ActionId = data.ActionId;
            stateChangeData.CurrentAbnormalState = data.CurrentAbnormalState;
            stateChangeData.Duration = data.Duration;
            stateChangeData.enable = data.enable;
            stateChangeData.EnergyReduceRate = data.EnergyReduceRate;
            stateChangeData.IsDamageRelease = data.IsDamageRelease;
            stateChangeData.IsEnergyReduceMode = data.IsEnergyReduceMode;
            stateChangeData.IsReleasedByDamage = data.IsReleasedByDamage;
            stateChangeData.MainValue = data.MainValue;
            stateChangeData.SubValue = data.SubValue;
            stateChangeData.SkillName = data.Skill?.SkillName ?? string.Empty;
            stateChangeData.SourceName = data.Source?.UnitName ?? string.Empty;
            NoSkipOnAbnormalStateChange?.Invoke(UnitId, stateChangeData, BattleHeaderController.CurrentFrameCount);
        }
        
        /// <summary>
        /// 从skillid判断是不是UB、技能12、atk
        /// </summary>
        /// <param name="skillid"></param>
        /// <param name="targetSkill">0-UB,1-技能1，2-技能2，3-普攻</param>
        /// <returns></returns>
        public bool JudgeIsTargetSkill(int skillid,int targetSkill)
        {
            if(skillid == UnionBurstSkillId && targetSkill == 0)
            {
                return true;
            }
            if (MainSkillIdList != null && MainSkillIdList.Count > 0)
                if (skillid == (MainSkill1Evolved ? MainSkillEvolutionIdList : MainSkillIdList)[0] && targetSkill == 1 )
                {
                    return true;
                }
            if (MainSkillIdList != null && MainSkillIdList.Count > 1)
                if (skillid == MainSkillIdList[1] && targetSkill == 2)
                {
                    return true;
                }
            if(skillid == 1 && targetSkill == 3)
            {
                return true;
            }
            return false;
        }
        /*
        public void SetUBExecTime(List<float> times,int tryCount)
        {
            for(int i = 0; i < times.Count; i++)
            {
                if (times[i] > 91)
                {
                    times[i] = 90 - times[i] / 60.0f;
                }
            }
            AppendCoroutine(UpdateUBExecTime(times, tryCount), ePauseType.SYSTEM);
        }
        public IEnumerator UpdateUBExecTime(List<float> times,int tryCount)
        {
            int idx = 0;
            int count = tryCount;
            if (times.Count <= 0)
            {
                yield break;
            }
            while (true)
            {
                if (battleManager.BattleLeftTime > times[idx])
                {
                    yield return null;
                    continue;
                }
                IsUbExecTrying = true;
                count--;
                if (count <= 0)
                {
                    idx++;
                    count = tryCount;
                    if (idx >= times.Count)
                    {
                        yield break;
                    }
                }
                yield return null;
            }
        }*/
        private void SetSkill(List<int> idList, List<SkillLevelInfo> skillLevelInfoList, eSpineCharacterAnimeId animeId, bool isSpecial, UnitParameter _data)
        {
            int index = 0;
            for (int count = idList.Count; index < count; ++index)
            {
                int skillId = idList[index];
                if (skillId != 0)
                {
                    //this.voiceTypeDictionary.Add(skillId, new UnitCtrl.VoiceTypeAndSkillNumber()
                    //{
                    //    VoiceType = isSpecial ? SoundManager.eVoiceType.SP_SKILL : SoundManager.eVoiceType.SKILL,
                    //    SkillNumber = index + 1
                    //});
                    if (MainManager.Instance.SkillDataDic.ContainsKey(skillId))
                    {


                        SkillData data = MainManager.Instance.SkillDataDic[skillId];
                        MasterSkillData.SkillData skillData = new MasterSkillData.SkillData(data);
                        if (MyGameCtrl.Instance.tempData.isGuildBattle && MyGameCtrl.Instance.tempData.SettingData.bossSkillCastTimeDic.TryGetValue(skillId, out float cast_time))
                        {
                            skillData.ChangeSkillCastTime(cast_time);
                        }
                        if (!SkillAreaWidthList.ContainsKey(skillId))
                        {
                            if (skillData.skill_area_width < 1)
                                SkillAreaWidthList.Add(skillId, (int)_data.MasterData.SearchAreaWidth);
                            else
                                SkillAreaWidthList.Add(skillId, (int)skillData.skill_area_width);
                        }

                        SkillLevelInfo skillLevelInfo = skillLevelInfoList.Find(e => e.SkillId == skillId);
                        if (!SkillLevels.ContainsKey(skillId))
                        {
                            SkillLevels.Add(skillId, skillLevelInfo == null ? 0 : (int)skillLevelInfo.SkillLevel);
                        }
                        if (!SkillUseCount.ContainsKey(skillId))
                            SkillUseCount.Add(skillId, 0);
                        if (!castTimeDictionary.ContainsKey(skillId))
                        {
                            castTimeDictionary.Add(skillId, (float)(double)skillData.skill_cast_time);
                        }
                        if (!animeIdDictionary.ContainsKey(skillId))
                        {
                            animeIdDictionary.Add(skillId, animeId);
                        }
                    }
                }
            }
        }
        public void AppendStartSkill(int skillid)
        {
            if (MySkillExecDic.ContainsKey(skillid))
            {
                MySkillExecDic[skillid]++;
            }
            else
            {
                MySkillExecDic.Add(skillid, 1);
            }
        }
        private IEnumerator TPRecovery()
        {
            int count = 0;
            while (count<5400)
            {
                count++;
                yield return null;
                if (count >= 60)
                {
                    count = 0;
                    Energy += 80f;
                }
            }
        }

        public enum eCritPointPriority
        {
            FullEnergy,
            StartSkill,
            ExecAction,
        }

        public class CritPoint
        {
            public eCritPointPriority priority;
            public readonly int frame;
            public string description;
            public string description2;

            public CritPoint()
            {
                frame = BattleHeaderController.CurrentFrameCount;
            }

            public string ToString(int frame)
            {
                if ((priority == eCritPointPriority.FullEnergy || 
                     priority == eCritPointPriority.ExecAction && description2 == "AUTO") && frame - this.frame <= 1)
                    return description2;
                if (frame <= this.frame + 2)
                    return $"极限押{description2}({frame - this.frame})";
                if (frame <= this.frame + 5)
                    return $"速押{description2}({frame - this.frame})";
                if (frame <= this.frame + 8)
                    return $"慢押{description2}({frame - this.frame})";
                return $"{description}后{frame - this.frame}帧";
            }
        }

        private static CritPoint globalCritPoint;
        public CritPoint critPoint;
        public bool lastCanReleaseSkill = false, skillReleased = false;
        public int enemyId;

        public CritPoint lastCritPoint
        {
            get => critPoint;
            set
            {
                if (critPoint == null || value.priority <= critPoint.priority || value.frame >= 10 + critPoint.frame)
                    critPoint = value;
                if (IsSummonOrPhantom) return;
                globalCritPoint = value;
                globalCritPoint.description = UnitNameEx + globalCritPoint.description;
            }
        }
        public string GetCurrentOp()
        {
            var frame = BattleHeaderController.CurrentFrameCount;
            var res = string.Empty;
            res = new[] { critPoint, globalCritPoint }.Where(c => c?.frame >= frame - 8).Distinct()
                .OrderByDescending(c => c.frame).FirstOrDefault()?.ToString(frame);
            if (!string.IsNullOrEmpty(res)) return res;
            return critPoint?.ToString(BattleHeaderController.CurrentFrameCount) + "    " + globalCritPoint?.ToString(BattleHeaderController.CurrentFrameCount);
        }
    }
}