﻿using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Elements.Battle;
using Newtonsoft.Json;
using PCRCaculator;
using Spine;
using Spine.Unity;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Elements
{
    public partial class UnitActionController
    {

        void filterTargetByFlight(ActionParameter actionParameter)
        {
            if (actionParameter == null)
                return;

            if ((int)actionParameter.TargetSort != 47 &&
                ((int)actionParameter.Direction < 4 || (int)actionParameter.Direction > 6) &&
                (int)actionParameter.TargetAssignment == 1 &&
                actionParameter.TargetNum != 1)
            {
                var targetList = actionParameter.TargetList;

                if (actionParameter.ReferencedByReflection)
                {
                    if (actionParameter.TargetNth < 1)
                    {
                        bool isFlightTarget = actionParameter.IsFlightStateTargetByReflection;

                        targetList?.RemoveAll(target =>
                        {
                            if (target?.Owner == null)
                                throw new NullReferenceException("target.Owner is null");

                            return isFlightTarget != target.Owner.IsAbnormalState(UnitCtrl.eAbnormalState.FLIGHT);
                        });
                    }
                    else
                    {
                        targetList?.RemoveAll(target =>
                        {
                            if (target?.Owner == null)
                                throw new NullReferenceException("target.Owner is null");

                            return target.Owner.IsAbnormalState(UnitCtrl.eAbnormalState.FLIGHT);
                        });
                    }
                }
                else
                {
                    bool hasNonFlying = targetList?.Any(target =>
                    {
                        if (target?.Owner == null)
                            throw new NullReferenceException("target.Owner is null");

                        return !target.Owner.IsAbnormalState(UnitCtrl.eAbnormalState.FLIGHT);
                    }) ?? false;

                    if (hasNonFlying)
                    {
                        targetList?.RemoveAll(target =>
                        {
                            if (target?.Owner == null)
                                throw new NullReferenceException("target.Owner is null");

                            return target.Owner.IsAbnormalState(UnitCtrl.eAbnormalState.FLIGHT);
                        });
                    }
                }
            }
        }
        
        private Dictionary<string, List<FirearmCtrlData>> unitFirearmDatas = new Dictionary<string, List<FirearmCtrlData>>();
        // public bool UseSkillEffect = false;
        public void LoadActionControllerData(int unitid,bool isABunit=true)
        {
            unitFirearmDatas = MainManager.Instance.FirearmData.GetData(unitid);
            if (!isABunit)
                MainManager.Instance.WindowConfigMessage("错误！角色" + unitid + "的数据读取失败！", null);
            /*
            if (isABunit)
                return;
            string json_0 = "";
            int datatype = 0;
            if (json_0 == "")
            {
                /*TextAsset text_0 = Resources.Load<TextAsset>("unitPrefabDatas/UNIT_" + unitid);
                if (text_0 != null && text_0.text != "")
                {
                    json_0 = text_0.text;
                }*//*
                string filePath3 = Application.streamingAssetsPath + "/Datas/unitPrefabDatas/UNIT_" + unitid + ".json";
                if (File.Exists(filePath3))
                {
                    StreamReader sr = new StreamReader(filePath3);
                    string jsonStr = sr.ReadToEnd();
                    sr.Close();
                    if (jsonStr != "")
                    {
                        json_0 = jsonStr;
                    }
                }
            }
            if (json_0 != "")
            {
                if (datatype == 0)
                {
                    UnitPrefabData prefabData = JsonConvert.DeserializeObject<UnitPrefabData>(json_0);
                    UseDefaultDelay = prefabData.UnitActionControllerData.UseDefaultDelay;
                    AttackDetail = prefabData.UnitActionControllerData.AttackDetail;
                    Attack = prefabData.UnitActionControllerData.Attack;
                    UnionBurstList = prefabData.UnitActionControllerData.UnionBurstList;
                    UnionBurstEvolutionList = prefabData.UnitActionControllerData.UnionBurstEvolutionList;
                    MainSkillList = prefabData.UnitActionControllerData.MainSkillList;
                    MainSkillEvolutionList = prefabData.UnitActionControllerData.MainSkillEvolutionList;
                    SpecialSkillList = prefabData.UnitActionControllerData.SpecialSkillList;
                    SpecialSkillEvolutionList = prefabData.UnitActionControllerData.SpecialSkillEvolutionList;
                    unitFirearmDatas = prefabData.unitFirearmDatas;
                }
            }
            else
            {
            }
            */
        }

        private static Dictionary<int, FirearmCtrlData> cache = new Dictionary<int, FirearmCtrlData>();

        private static void SetPrefabDataBySkillid(int skillid, FirearmCtrlData data)
        {
            cache[skillid] = data;
        }

        private FirearmCtrlData GetPrefabDataBySkillid(int skillid)
        {
            if (cache.TryGetValue(skillid, out var val)) return val;
            string key = "attack";
            try
            {
                if (skillid == Owner.MainSkillIdList[0] || skillid - 10 == Owner.MainSkillIdList[0])
                {
                    key = "skill1";
                    if (Owner.UnitId <= 200000 && Owner.unitData.uniqueEqLv > 0)
                        key = "skill1+";
                }
                else if (skillid == Owner.MainSkillIdList[1])
                {
                    key = "skill2";
                    if (Owner.UnitId <= 200000 && Owner.unitData.uniqueEq2Lv > 0)
                        key = "skill2+";
                }
                else if (skillid == Owner.UnionBurstSkillId || skillid - 10 == Owner.UnionBurstSkillId)
                {
                    key = "skill0";
                    if (Owner.UnitId <= 200000 && Owner.unitData.rarity == 6)
                        key = "skill0+";
                }
                else if (skillid == Owner.SpecialSkillIdList[0])
                    key = "special0";
                else if (skillid == Owner.SpecialSkillIdList[1])
                    key = "special1";
                else if (skillid == Owner.SpecialSkillIdList[2])
                    key = "special2";
                return unitFirearmDatas[key][0];
            }
            catch (Exception e)
            {
                //BattleUIManager.Instance.LogMessage(Owner.UnitName + "的技能特效数据丢失！", eLogMessageType.ERROR, Owner.IsOther);
#if UNITY_EDITOR
                string Msg = "加载角色" + Owner.UnitName + "的技能" + skillid + "的弹道数据时发生错误：" + e.Message;
                Debug.LogError(Msg);
#endif
                throw;
                //MainManager.Instance.WindowConfigMessage(Msg, null);
                //return new FirearmCtrlData();
            }
        }
        private void CalcDelayByPhysice(ActionParameter action, Skill _skill, int targetmotion, bool first = false, bool _skipCutIn = false,
          bool _isFirearmEndEffect = false,
          bool _modeChangeEndEffect = false)
        {
            for (int i = 0; i < _skill.SkillEffects.Count; i++)
            {
                NormalSkillEffect _skilleffect = _skill.SkillEffects[i];
                float time = first ? _skill.CutInSkipTime : 0.0f;
                int generateEffectCount = 0;
                float[] execTime = _skilleffect.ExecTime;
                switch (_skilleffect.EffectBehavior)
                {
                    case eEffectBehavior.NORMAL:
                    case eEffectBehavior.FIREARM:
                    case eEffectBehavior.WORLD_POS_CENTER:
                    case eEffectBehavior.TARGET_CENTER:
                    case eEffectBehavior.SKILL_AREA_RANDOM:
                        generateEffectCount = execTime.Length;
                        break;
                    case eEffectBehavior.SERIES:
                    case eEffectBehavior.SERIES_FIREARM:
                        generateEffectCount = _skilleffect.TargetAction.TargetList.Count;
                        break;
                }
                bool[] execed = new bool[generateEffectCount];
                for (int execTimeIndex = 0; execTimeIndex < generateEffectCount; ++execTimeIndex)
                {
                    float waitTime = 0.0f;
                    if (_skill.AnimId != eSpineCharacterAnimeId.ATTACK || !UseDefaultDelay || (_skilleffect.EffectBehavior != eEffectBehavior.FIREARM || !BattleDefine.WEAPON_EFFECT_DELAY_DIC.TryGetValue(Owner.WeaponMotionType, out waitTime)))
                        waitTime = execTime.Length - 1 >= execTimeIndex ? execTime[execTimeIndex] : (execTime.Length < 2 ? execTime[0] : (execTime[execTime.Length - 1] - execTime[execTime.Length - 2]) * (execTimeIndex - execTime.Length + 1) + execTime[execTime.Length - 1]);
                    if (!execed[execTimeIndex])
                    {
                        float startTime = 0.0f;
                        if (!_skipCutIn || waitTime <= (double)_skill.CutInSkipTime && !BattleUtil.Approximately(waitTime, _skill.CutInSkipTime))
                        {
                            if (!_skipCutIn && waitTime < (double)_skill.CutInSkipTime && !_isFirearmEndEffect)
                            {
                                if (_skilleffect.PlayWithCutIn && Owner.PlayCutInFlag && Owner.MovieId != 0)
                                    startTime = _skill.CutInSkipTime - waitTime;
                                else
                                    continue;
                            }
                            /*do
                            {
                                yield return (object)null;
                                time += this.battleManager.DeltaTime_60fps;
                                if (_skill.Cancel || !this.Owner.gameObject.activeSelf || _modeChangeEndEffect && this.Owner.IsUnableActionState())
                                    yield break;
                            }
                            while ((double)waitTime > (double)time);*/
                            time += Mathf.CeilToInt(waitTime / battleManager.DeltaTime_60fps) * battleManager.DeltaTime_60fps;
                            execed[execTimeIndex] = true;
                            BasePartsData _firearmEndTarget = null;
                            if (_skilleffect.EffectBehavior == eEffectBehavior.FIREARM || _skilleffect.EffectBehavior == eEffectBehavior.SERIES_FIREARM)
                            {
                                switch (_skilleffect.FireArmEndTarget)
                                {
                                    case eEffectTarget.OWNER:
                                        _firearmEndTarget = Owner.GetFirstParts(true);
                                        break;
                                    case eEffectTarget.ALL_TARGET:
                                        _firearmEndTarget = _skilleffect.TargetAction.TargetList[execTimeIndex];
                                        break;
                                    case eEffectTarget.FORWARD_TARGET:
                                    case eEffectTarget.BACK_TARGET:
                                        bool flag1 = _skilleffect.FireArmEndTarget == eEffectTarget.FORWARD_TARGET == !Owner.IsOther;
                                        List<BasePartsData> basePartsDataList1 = new List<BasePartsData>(_skilleffect.TargetAction.TargetList.GetRange(0, Mathf.Min(_skilleffect.TargetAction.TargetNum, _skilleffect.TargetAction.TargetList.Count)));
                                        basePartsDataList1.Sort(((a, b) => a.GetPosition().x.CompareTo(b.GetPosition().x)));
                                        if (basePartsDataList1.Count == 0)
                                        {
                                            //yield break;
                                            return;
                                        }
                                        else
                                        {
                                            _firearmEndTarget = basePartsDataList1[flag1 ? 0 : basePartsDataList1.Count - 1];
                                            break;
                                        }
                                }
                            }
                            switch (_skilleffect.EffectBehavior)
                            {
                                case eEffectBehavior.NORMAL:
                                case eEffectBehavior.SERIES:
                                case eEffectBehavior.SKILL_AREA_RANDOM:
                                    continue;
                            }

                            if (_skilleffect.TargetActionId == action.ActionId ||
                            action.ActionId % 1000 == 0 && _skilleffect.TargetAction == action ||
                            _skilleffect.FireActionId == action.ActionId)//!MyGameCtrl.Instance.ignoreEffects)
                            {
                                /*switch (_skilleffect.EffectTarget)
                                {
                                    case eEffectTarget.OWNER:
                                        this.createNormalEffectPrefab(_skilleffect, _skill, this.Owner.GetFirstParts(true), _firearmEndTarget, true, startTime, _skipCutIn, execTimeIndex, _modeChangeEndEffect);
                                        continue;
                                    case eEffectTarget.ALL_TARGET:
                                        switch (_skilleffect.EffectBehavior)
                                        {
                                            case eEffectBehavior.SERIES:
                                            case eEffectBehavior.SERIES_FIREARM:
                                                this.createNormalEffectPrefab(_skilleffect, _skill, _skilleffect.TargetAction.TargetList[execTimeIndex], _firearmEndTarget, true, startTime, _skipCutIn, execTimeIndex);
                                                continue;
                                            default:
                                                int index1 = 0;
                                                for (int count = _skilleffect.TargetAction.TargetList.Count; index1 < count && index1 < _skilleffect.TargetAction.TargetNum; ++index1)
                                                    this.createNormalEffectPrefab(_skilleffect, _skill, _skilleffect.TargetAction.TargetList[index1], _firearmEndTarget, index1 == 0, startTime, _skipCutIn, execTimeIndex);
                                                continue;
                                        }
                                    case eEffectTarget.FORWARD_TARGET:
                                    case eEffectTarget.BACK_TARGET:
                                        bool flag2 = _skilleffect.EffectTarget == eEffectTarget.FORWARD_TARGET == !this.Owner.IsOther;
                                        List<BasePartsData> basePartsDataList2 = new List<BasePartsData>((IEnumerable<BasePartsData>)_skilleffect.TargetAction.TargetList.GetRange(0, Mathf.Min(_skilleffect.TargetAction.TargetNum, _skilleffect.TargetAction.TargetList.Count)));
                                        basePartsDataList2.Sort(((a, b) => a.GetPosition().x.CompareTo(b.GetPosition().x)));
                                        if (basePartsDataList2.Count != 0)
                                        {
                                            this.createNormalEffectPrefab(_skilleffect, _skill, basePartsDataList2[flag2 ? 0 : basePartsDataList2.Count - 1], _firearmEndTarget, true, startTime, _skipCutIn, execTimeIndex);
                                            continue;
                                        }
                                        continue;
                                    case eEffectTarget.ALL_OTHER:
                                        List<UnitCtrl> unitCtrlList1 = this.Owner.IsOther ? this.battleManager.UnitList : this.battleManager.EnemyList;
                                        for (int index2 = 0; index2 < unitCtrlList1.Count; ++index2)
                                        {
                                            if ((long)unitCtrlList1[index2].Hp != 0L)
                                                this.createNormalEffectPrefab(_skilleffect, _skill, unitCtrlList1[index2].GetFirstParts(true), _firearmEndTarget, true, startTime, _skipCutIn, execTimeIndex);
                                        }
                                        continue;
                                    case eEffectTarget.ALL_UNIT_EXCEPT_OWNER:
                                        List<UnitCtrl> unitCtrlList2 = this.Owner.IsOther ? this.battleManager.EnemyList : this.battleManager.UnitList;
                                        for (int index2 = 0; index2 < unitCtrlList2.Count; ++index2)
                                        {
                                            if ((UnityEngine.Object)unitCtrlList2[index2] != (UnityEngine.Object)this.Owner && !unitCtrlList2[index2].IsDead)
                                                this.createNormalEffectPrefab(_skilleffect, _skill, unitCtrlList2[index2].GetFirstParts(true), _firearmEndTarget, true, startTime, _skipCutIn, execTimeIndex);
                                        }
                                        continue;
                                    default:
                                        continue;
                                }*/
                                CalcFirearmDelay(_skill, waitTime);
                            }

                        }
                    }
                }
            }
            action.ReferencedByEffect = false;

        }
        private void CalcFirearmDelay(Skill _skill, float waitTime)
        {
            foreach (ActionParameter action in _skill.ActionParameters)
            {
                if (action.ReferencedByEffect)
                {
                    FirearmCtrlData firearmCtrlData = GetPrefabDataBySkillid(_skill.SkillId);
                    if (firearmCtrlData.ignoreFirearm)
                    {
                        action.ExecTime = new float[1] { firearmCtrlData.fixedExecTime };
                    }
                    else
                    {
                        float MoveRate = firearmCtrlData.MoveRate;
                        float delay = firearmCtrlData.MoveType == (int)eMoveTypes.LINEAR ? 0.2f / MoveRate : firearmCtrlData.HitDelay;
                        float speedDelay = 1.0f / MoveRate;
                        float allDelay = waitTime + speedDelay + delay;
                        if (action.ExecTime.Length == 0)
                        {
                            action.ExecTime = new[] { allDelay };
                            action.ActionExecTimeList = new List<ActionExecTime>();
                            action.ActionExecTimeList.Add(new ActionExecTime { Time = allDelay, Weight = 1 });
                            action.ActionWeightSum = 1;
                        }
                        else
                        {
                            action.ExecTime[0] += allDelay;
                        }
                    }
                    action.ReferencedByEffect = false;
                }
            }
        }
        [ContextMenu("生成json文件")]
        public void SaveDataToJson()
        {
            UnitPrefabData unitPrefabData = new UnitPrefabData();
            unitPrefabData.UnitActionControllerData = GetUnitActionControllerData();

            unitPrefabData.unitFirearmDatas = GetFirearmDatas();

            string fileName = gameObject.name.Remove(11);
            string filePath = Application.dataPath + "/Resources/unitPrefabDatas/" + fileName + ".json";
            string saveJsonStr = JsonConvert.SerializeObject(unitPrefabData);
            StreamWriter sw = new StreamWriter(filePath);
            sw.Write(saveJsonStr);
            sw.Close();
            Debug.Log("成功！" + filePath);
        }
        public UnitSkillEffectData CreateUnitSkillEffectData()
        {
            UnitSkillEffectData data = new UnitSkillEffectData();
            List<string> namelist = new List<string>();
            Action<List<Skill>> action = a =>
            {
                foreach (var skill in a)
                {
                    foreach (var effect in skill.SkillEffects)
                    {
                        FindFxskInSkill(effect, namelist);
                    }
                }
            };
            foreach (var effect in Attack.SkillEffects)
            {
                FindFxskInSkill(effect, namelist);
            }
            action(UnionBurstList);
            action(UnionBurstEvolutionList);
            action(MainSkillList);
            action(MainSkillEvolutionList);
            action(SpecialSkillList);
            action(SpecialSkillEvolutionList);
            data.requireFsxkList = namelist;
            return data;

        }
        private void FindFxskInSkill(NormalSkillEffect effect, List<string> result)
        {
            if (effect != null)
            {
                if (effect.Prefab != null)
                {
                    try
                    {
                        if (effect.Prefab.name.Length >= 10)
                        {
                            string name_8 = effect.Prefab.name.Substring(0, 9);
                            if (name_8.Contains("fxsk") && !result.Contains(name_8))
                                result.Add(name_8);
                        }
                    }
                    catch (MissingReferenceException e)
                    {
                        Debug.LogError("角色PREFAB丢失！");
                    }
                }
                if (effect.FireArmEndEffects.Count > 0)
                {
                    foreach (var eff in effect.FireArmEndEffects)
                    {
                        FindFxskInSkill(eff, result);
                    }
                }
            }
        }
        public Dictionary<string, List<FirearmCtrlData>> GetFirearmDatas()
        {
            Dictionary<string, List<FirearmCtrlData>> dic = new Dictionary<string, List<FirearmCtrlData>>();
            if (Attack != null)
                dic.Add("attack", GetFirearmCtrlDatasFromSkill(Attack));
            if (UnionBurstList.Count > 0)
                dic.Add("skill0", GetFirearmCtrlDatasFromSkill(UnionBurstList[0]));
            if (UnionBurstEvolutionList.Count > 0)
                dic.Add("skill0+", GetFirearmCtrlDatasFromSkill(UnionBurstEvolutionList[0]));
            if (MainSkillList.Count > 0)
            {
                for (int i = 0; i < MainSkillList.Count; i++)
                {
                    dic.Add("skill" + (i + 1), GetFirearmCtrlDatasFromSkill(MainSkillList[i]));
                }
                /*dic.Add("skill1", GetFirearmCtrlDatasFromSkill(MainSkillList[0]));
                if (MainSkillList.Count > 1)
                {
                    dic.Add("skill2", GetFirearmCtrlDatasFromSkill(MainSkillList[1]));
                }*/
            }
            if (MainSkillEvolutionList.Count > 0)
            {
                dic.Add("skill1+", GetFirearmCtrlDatasFromSkill(MainSkillEvolutionList[0]));
                if (MainSkillEvolutionList.Count > 1)
                    dic.Add("skill2+", GetFirearmCtrlDatasFromSkill(MainSkillEvolutionList[1]));
            }
            for (int i = 0; i < SpecialSkillList.Count; i++)
            {
                dic.Add("special" + i, GetFirearmCtrlDatasFromSkill(SpecialSkillList[i]));
            }
            for (int i = 0; i < SpecialSkillEvolutionList.Count; i++)
            {
                dic.Add("special" + i + "+", GetFirearmCtrlDatasFromSkill(SpecialSkillEvolutionList[i]));
            }
            return dic;
        }
        public UnitActionControllerData2 GetUnitActionControllerData()
        {
            return new UnitActionControllerData2(AttackDetail, UseDefaultDelay, Attack, UnionBurstList, MainSkillList, SpecialSkillList, SpecialSkillEvolutionList, UnionBurstEvolutionList, MainSkillEvolutionList, Annihilation);
        }

        private List<FirearmCtrlData> GetFirearmCtrlDatasFromSkill(Skill skill)
        {
            List<FirearmCtrlData> list = new List<FirearmCtrlData>();
            /*foreach (NormalSkillEffect skillEffect in skill.SkillEffects)
            {
                if (skillEffect.EffectBehavior == eEffectBehavior.FIREARM || skillEffect.EffectBehavior == eEffectBehavior.SERIES_FIREARM)
                {
                    if (skillEffect.Prefab != null)
                    {
                        list.Add(skillEffect.Prefab.GetComponent<FirearmCtrl>().GetPrefabData());
                    }
                }
            }*/
            foreach (NormalSkillEffect skillEffect in skill.SkillEffects)
            {
                GetFirearmCtrlDatas_0(skillEffect, list);
            }
            return list;
        }
        private void GetFirearmCtrlDatas_0(NormalSkillEffect skillEffect, List<FirearmCtrlData> list)
        {
            if (skillEffect.EffectBehavior == eEffectBehavior.FIREARM || skillEffect.EffectBehavior == eEffectBehavior.SERIES_FIREARM)
            {
                if (skillEffect.Prefab != null)
                {
                    list.Add(skillEffect.Prefab.GetComponent<FirearmCtrl>().GetPrefabData());
                }
            }
            if (skillEffect.FireArmEndEffects.Count > 0)
            {
                foreach (var endEffect in skillEffect.FireArmEndEffects)
                    GetFirearmCtrlDatas_0(endEffect, list);
            }
        }
        private void CalcDelayByPhysice(ActionParameter action, Skill skill, int targetmotion, bool first)
        {
            for (int i = 0; i < skill.SkillEffects.Count; i++)
            {
                NormalSkillEffect _skilleffect = skill.SkillEffects[i];
                float time = first ? skill.CutInSkipTime : 0.0f;
                int generateEffectCount = 0;
                float[] execTime = _skilleffect.ExecTime;
                switch (_skilleffect.EffectBehavior)
                {
                    case eEffectBehavior.NORMAL:
                    case eEffectBehavior.FIREARM:
                    case eEffectBehavior.WORLD_POS_CENTER:
                    case eEffectBehavior.TARGET_CENTER:
                    case eEffectBehavior.SKILL_AREA_RANDOM:
                        generateEffectCount = execTime.Length;
                        break;
                    case eEffectBehavior.SERIES:
                    case eEffectBehavior.SERIES_FIREARM:
                        generateEffectCount = _skilleffect.TargetAction.TargetList.Count;
                        break;
                }
                bool[] execed = new bool[generateEffectCount];

                if (_skilleffect.TargetActionId == action.ActionId ||
                    action.ActionId % 1000 == 0 && _skilleffect.TargetAction == action ||
                    _skilleffect.FireActionId == action.ActionId)
                {
                    switch (_skilleffect.EffectBehavior)
                    {
                        case eEffectBehavior.NORMAL:
                        case eEffectBehavior.SERIES:
                        case eEffectBehavior.SKILL_AREA_RANDOM:
                            continue;
                    }
                    if (_skilleffect.TargetMotionIndex == targetmotion)
                    {
                        if (action.TargetList.Count <= 0)
                        {
                            continue;
                        }
                        float waitTime_5_7 = 0;
                        float[] execTime_5_4 = _skilleffect.ExecTime;
                        bool k = true;
                        if (skill.AnimId != eSpineCharacterAnimeId.ATTACK || !UseDefaultDelay || _skilleffect.EffectBehavior == eEffectBehavior.FIREARM)
                        {
                            k = false;
                        }
                        if (k)
                        {
                            if (!BattleDefine.WEAPON_EFFECT_DELAY_DIC.TryGetValue(Owner.WeaponMotionType, out waitTime_5_7))
                            {
                                //简化
                                waitTime_5_7 = execTime_5_4[0];
                            }
                        }
                        else
                        {
                            waitTime_5_7 = execTime_5_4[0];
                        }
                        //Elements.FirearmCtrlData firearmCtrlData = skillEffect.PrefabData;
                        FirearmCtrlData firearmCtrlData;
                        try
                        {
                            firearmCtrlData = _skilleffect.PrefabData;
                        }
                        catch
                        {
                            firearmCtrlData = GetPrefabDataBySkillid(skill.SkillId);
                        }

                        if (firearmCtrlData.ignoreFirearm)
                        {
                            action.ExecTime = new float[1] { firearmCtrlData.fixedExecTime };
                        }
                        else
                        {
                            float MoveRate = firearmCtrlData.MoveRate;
                            float delay = firearmCtrlData.MoveType == (int)eMoveTypes.LINEAR ? 0.2f / MoveRate : firearmCtrlData.HitDelay;
                            float speedDelay = 0;
                            switch ((eMoveTypes)(int)firearmCtrlData.MoveType)
                            {
                                case eMoveTypes.LINEAR:
                                case eMoveTypes.HORIZONTAL:
                                    speedDelay = 1.0f / MoveRate;
                                    break;
                                case eMoveTypes.PARABORIC:
                                case eMoveTypes.PARABORIC_ROTATE:
                                    speedDelay = firearmCtrlData.duration;
                                    break;
                            }

                            float allDelay = waitTime_5_7 + speedDelay + delay + skill.CutInSkipTime;
                            if (action.ExecTime.Length == 0)
                            {
                                action.ExecTime = new[] { allDelay };
                                action.ActionExecTimeList = new List<ActionExecTime>();
                                action.ActionExecTimeList.Add(new ActionExecTime { Time = allDelay, Weight = 1 });
                                action.ActionWeightSum = 1;
                            }
                            else
                            {
                                action.ExecTime[0] += allDelay;
                            }
                        }
                    }
                }

            }
            action.ReferencedByEffect = false;

        }
        private void CalcDelayByPhysice2(ActionParameter action, Skill skill, int targetmotion, bool first)
        {
            CreateNormalPrefabWithTargetMotion2(skill, targetmotion, first);
        }
        public void CreateNormalPrefabWithTargetMotion2(
  Skill _skill,
  int _targetmotion,
  bool _first,
  bool _useStartCoroutine = false,
  bool _modechangeEndEffect = false)
        {
            //Debug.Log("创建特效");
            int index = 0;
            for (int count = _skill.SkillEffects.Count; index < count; ++index)
            {
                NormalSkillEffect skillEffect = _skill.SkillEffects[index];
                if (skillEffect.TargetMotionIndex == _targetmotion)
                {
                    bool flag = skillEffect.EffectBehavior == eEffectBehavior.NORMAL || skillEffect.EffectBehavior == eEffectBehavior.SKILL_AREA_RANDOM || skillEffect.EffectBehavior == eEffectBehavior.TARGET_CENTER || skillEffect.EffectBehavior == eEffectBehavior.WORLD_POS_CENTER;
                    if (_useStartCoroutine)
                    {
                        battleManager.StartCoroutine(createNormalPrefabWithDelay2(skillEffect, _skill, _first));
                        //Debug.Log("创建特效携程" + skillEffect.EffectBehavior.GetDescription()+index);
                    }
                    else
                    {
                        ePauseType pauseType = flag ? ePauseType.VISUAL : ePauseType.SYSTEM;
                        UnitCtrl unit = _skill.BlackOutTime > 0.0 ? Owner : null;
                        AppendCoroutine(createNormalPrefabWithDelay2(skillEffect, _skill, _first, _isFirearmEndEffect: _modechangeEndEffect, _modeChangeEndEffect: _modechangeEndEffect), pauseType, unit);
                        //Debug.Log("创建特效携程" + skillEffect.EffectBehavior.GetDescription() + index);

                    }
                }
            }
        }
        private IEnumerator createNormalPrefabWithDelay2(
  NormalSkillEffect _skilleffect,
  Skill _skill,
  bool _first = false,
  bool _skipCutIn = false,
  bool _isFirearmEndEffect = false,
  bool _modeChangeEndEffect = false)
        {
            //Debug.Log("创建特效携程"+ _skilleffect.EffectBehavior.GetDescription());
            float time = _first ? _skill.CutInSkipTime : 0.0f;
            /*string key = this.Owner.IsLeftDir || this.Owner.IsForceLeftDirOrPartsBoss ? _skilleffect.PrefabLeft.name : _skilleffect.Prefab.name;
            if (this.battleManager.DAIFDPFABCM.ContainsKey(key))
            {
                GameObject gameObject = this.battleManager.DAIFDPFABCM[key];
                if ((UnityEngine.Object)gameObject == (UnityEngine.Object)null)
                {
                    yield break;
                }
                else
                {
                    this.battleManager.DAIFDPFABCM.Remove(key);
                    UnityEngine.Object.Destroy((UnityEngine.Object)gameObject);
                }
            }
            else if (_skilleffect.IsReaction)
                this.battleManager.DAIFDPFABCM.Add(key, (GameObject)null);*/
            int generateEffectCount = 0;
            float[] execTime = _skilleffect.ExecTime;
            switch (_skilleffect.EffectBehavior)
            {
                case eEffectBehavior.NORMAL:
                case eEffectBehavior.FIREARM:
                case eEffectBehavior.WORLD_POS_CENTER:
                case eEffectBehavior.TARGET_CENTER:
                case eEffectBehavior.SKILL_AREA_RANDOM:
                    generateEffectCount = execTime.Length;
                    break;
                case eEffectBehavior.SERIES:
                case eEffectBehavior.SERIES_FIREARM:
                    generateEffectCount = _skilleffect.TargetAction.TargetList.Count;
                    break;
            }
            bool[] execed = new bool[generateEffectCount];
            for (int execTimeIndex = 0; execTimeIndex < generateEffectCount; ++execTimeIndex)
            {
                float waitTime = 0.0f;
                if (_skill.AnimId != eSpineCharacterAnimeId.ATTACK || !UseDefaultDelay || (_skilleffect.EffectBehavior != eEffectBehavior.FIREARM || !BattleDefine.WEAPON_EFFECT_DELAY_DIC.TryGetValue(Owner.WeaponMotionType, out waitTime)))
                    waitTime = execTime.Length - 1 >= execTimeIndex ? execTime[execTimeIndex] : (execTime.Length < 2 ? execTime[0] : (execTime[execTime.Length - 1] - execTime[execTime.Length - 2]) * (execTimeIndex - execTime.Length + 1) + execTime[execTime.Length - 1]);
                if (!execed[execTimeIndex])
                {
                    float startTime = 0.0f;
                    if (!_skipCutIn || waitTime <= (double)_skill.CutInSkipTime && !BattleUtil.Approximately(waitTime, _skill.CutInSkipTime))
                    {
                        if (!_skipCutIn && waitTime < (double)_skill.CutInSkipTime && !_isFirearmEndEffect)
                        {
                            if (_skilleffect.PlayWithCutIn && Owner.PlayCutInFlag && Owner.MovieId != 0)
                                startTime = _skill.CutInSkipTime - waitTime;
                            else
                                continue;
                        }
                        do
                        {
                            yield return null;
                            time += battleManager.DeltaTime_60fps;
                            if (_skill.Cancel || !Owner.gameObject.activeSelf || _modeChangeEndEffect && Owner.IsUnableActionState())
                                yield break;
                        }
                        while (waitTime > (double)time);
                        execed[execTimeIndex] = true;
                        BasePartsData _firearmEndTarget = null;
                        if (_skilleffect.EffectBehavior == eEffectBehavior.FIREARM || _skilleffect.EffectBehavior == eEffectBehavior.SERIES_FIREARM)
                        {
                            switch (_skilleffect.FireArmEndTarget)
                            {
                                case eEffectTarget.OWNER:
                                    _firearmEndTarget = Owner.GetFirstParts(true);
                                    break;
                                case eEffectTarget.ALL_TARGET:
                                    _firearmEndTarget = _skilleffect.TargetAction.TargetList[execTimeIndex];
                                    break;
                                case eEffectTarget.FORWARD_TARGET:
                                case eEffectTarget.BACK_TARGET:
                                    bool flag1 = _skilleffect.FireArmEndTarget == eEffectTarget.FORWARD_TARGET == !Owner.IsOther;
                                    List<BasePartsData> basePartsDataList1 = new List<BasePartsData>(_skilleffect.TargetAction.TargetList.GetRange(0, Mathf.Min(_skilleffect.TargetAction.TargetNum, _skilleffect.TargetAction.TargetList.Count)));
                                    basePartsDataList1.Sort((a, b) => a.GetPosition().x.CompareTo(b.GetPosition().x));
                                    if (basePartsDataList1.Count == 0)
                                    {
                                        yield break;
                                    }
                                    else
                                    {
                                        _firearmEndTarget = basePartsDataList1[flag1 ? 0 : basePartsDataList1.Count - 1];
                                        break;
                                    }
                            }
                        }
                        switch (_skilleffect.EffectTarget)
                        {
                            case eEffectTarget.OWNER:
                                createNormalEffectPrefab2(_skilleffect, _skill, Owner.GetFirstParts(true), _firearmEndTarget, true, startTime, _skipCutIn, execTimeIndex, _modeChangeEndEffect);
                                continue;
                            case eEffectTarget.ALL_TARGET:
                                switch (_skilleffect.EffectBehavior)
                                {
                                    case eEffectBehavior.SERIES:
                                    case eEffectBehavior.SERIES_FIREARM:
                                        createNormalEffectPrefab2(_skilleffect, _skill, _skilleffect.TargetAction.TargetList[execTimeIndex], _firearmEndTarget, true, startTime, _skipCutIn, execTimeIndex);
                                        continue;
                                    default:
                                        int index1 = 0;
                                        for (int count = _skilleffect.TargetAction.TargetList.Count; index1 < count && index1 < _skilleffect.TargetAction.TargetNum; ++index1)
                                            createNormalEffectPrefab2(_skilleffect, _skill, _skilleffect.TargetAction.TargetList[index1], _firearmEndTarget, index1 == 0, startTime, _skipCutIn, execTimeIndex);
                                        continue;
                                }
                            case eEffectTarget.FORWARD_TARGET:
                            case eEffectTarget.BACK_TARGET:
                                bool flag2 = _skilleffect.EffectTarget == eEffectTarget.FORWARD_TARGET == !Owner.IsOther;
                                List<BasePartsData> basePartsDataList2 = new List<BasePartsData>(_skilleffect.TargetAction.TargetList.GetRange(0, Mathf.Min(_skilleffect.TargetAction.TargetNum, _skilleffect.TargetAction.TargetList.Count)));
                                basePartsDataList2.Sort((a, b) => a.GetPosition().x.CompareTo(b.GetPosition().x));
                                if (basePartsDataList2.Count != 0)
                                {
                                    createNormalEffectPrefab2(_skilleffect, _skill, basePartsDataList2[flag2 ? 0 : basePartsDataList2.Count - 1], _firearmEndTarget, true, startTime, _skipCutIn, execTimeIndex);
                                }
                                continue;
                            case eEffectTarget.ALL_OTHER:
                                List<UnitCtrl> unitCtrlList1 = Owner.IsOther ? battleManager.UnitList : battleManager.EnemyList;
                                for (int index2 = 0; index2 < unitCtrlList1.Count; ++index2)
                                {
                                    if ((long)unitCtrlList1[index2].Hp != 0L)
                                        createNormalEffectPrefab2(_skilleffect, _skill, unitCtrlList1[index2].GetFirstParts(true), _firearmEndTarget, true, startTime, _skipCutIn, execTimeIndex);
                                }
                                continue;
                            case eEffectTarget.ALL_UNIT_EXCEPT_OWNER:
                                List<UnitCtrl> unitCtrlList2 = Owner.IsOther ? battleManager.EnemyList : battleManager.UnitList;
                                for (int index2 = 0; index2 < unitCtrlList2.Count; ++index2)
                                {
                                    if (unitCtrlList2[index2] != Owner && !unitCtrlList2[index2].IsDead)
                                        createNormalEffectPrefab2(_skilleffect, _skill, unitCtrlList2[index2].GetFirstParts(true), _firearmEndTarget, true, startTime, _skipCutIn, execTimeIndex);
                                }
                                continue;
                            default:
                                continue;
                        }
                    }
                }
            }
        }

        private void createNormalEffectPrefab2(
  NormalSkillEffect _skillEffect,
  Skill _skill,
  BasePartsData _target,
  BasePartsData _firearmEndTarget,
  bool actionStart,
  float _starttime,
  bool _skipCutIn,
  int execTimeIndex = 0,
  bool _modeChangeEndEffect = false)
        {
            if (_skillEffect.EffectBehavior == eEffectBehavior.SERIES_FIREARM && !(_skillEffect.FireAction is AttackAction) && (!(_skillEffect.FireAction is RatioDamageAction) && !(_skillEffect.FireAction is UpperLimitAttackAction)) && (!(_skillEffect.FireAction is HealAction) && _skillEffect.AppendAndJudgeAlreadyExeced(_firearmEndTarget.Owner)) || (_skillEffect.TargetBranchId != 0 && _skillEffect.TargetBranchId != _skill.EffectBranchId || _skillEffect.TargetMotionIndex == 1 && _skill.LoopEffectAlreadyDone))
                return;
            GameObject prefab1 = null;
            //SkillEffectCtrl prefab2 = this.createPrefab(_skillEffect, _skill, _target, ref prefab1);
            switch (_skillEffect.EffectBehavior)
            {
                case eEffectBehavior.FIREARM:
                case eEffectBehavior.SERIES_FIREARM:
                    var prefab = Owner.IsLeftDir || Owner.IsForceLeftDirOrPartsBoss ? _skillEffect.PrefabLeft : _skillEffect.Prefab;
                    var firearmCtrl = battleManager.GetFirearmCtrl2FromPool(Owner, prefab == null ? "null" : prefab.name);
                    firearmCtrl.SetPossitionAppearanceType(_skillEffect, _target, Owner, _skill);
                    try
                    {
                        firearmCtrl.data = prefab
                            .GetComponent<FirearmCtrl>().GetPrefabData();
                        firearmCtrl.position += firearmCtrl.data.initialPosition;
                        SetPrefabDataBySkillid(_skill.SkillId, firearmCtrl.data);
                    }
                    catch (Exception e)
                    {
                        Debug.LogError($"警告：弹道{_skill.SkillId}加载失败，使用旧版弹道可能会导致偏差");
                        firearmCtrl.data = GetPrefabDataBySkillid(_skill.SkillId);
                    }
                    List<ActionParameter> _actions = new List<ActionParameter>();
                    if (_skillEffect.FireActionId != -1 & actionStart)
                        _actions.Add(_skillEffect.FireAction);
                    firearmCtrl.Initialize(_firearmEndTarget, _actions, _skill, actionStart ? _skillEffect.FireArmEndEffects : new List<NormalSkillEffect>(), Owner, _skillEffect.Height, _skill.BlackOutTime > 0.0, _skillEffect.IsAbsoluteFireArm, transform.position + (Owner.IsLeftDir ? -1f : 1f) * new Vector3(_skillEffect.AbsoluteFireDistance, 0.0f), _skillEffect.ShakeEffects, _skillEffect.FireArmEndTargetBone);
                    firearmCtrl.OnHitAction = _skillEffect.EffectBehavior != eEffectBehavior.FIREARM ? fctrl =>
                    {
                        for (int index1 = 0; index1 < fctrl.ShakeEffects.Count; ++index1)
                        {
                          if (fctrl.ShakeEffects[index1].TargetMotion == 0)
                            this.AppendCoroutine(this.StartShakeWithDelay(fctrl.ShakeEffects[index1], fctrl.Skill), ePauseType.VISUAL, (double) fctrl.Skill.BlackOutTime > 0.0 ? this.Owner : (UnitCtrl) null);
                        }
                        if (_skillEffect.FireActionId != -1)
                            AppendCoroutine(ExecActionWithDelayAndTarget(_skillEffect.FireAction, _skill, _firearmEndTarget, 0.0f), ePauseType.SYSTEM, _skill.BlackOutTime > 0.0 ? Owner : null);
                        int index3 = 0;
                        for (int count = _skillEffect.FireArmEndEffects.Count; index3 < count; ++index3)
                            AppendCoroutine(createNormalPrefabWithDelayAndTarget2(_skillEffect.FireArmEndEffects[index3], _skill, 0.0f, _firearmEndTarget, false), ePauseType.VISUAL, _skill.BlackOutTime > 0.0 ? Owner : null);
                    } : new Action<FirearmCtrl2>(onFirearmHit2);
                    //Debug.Log("生成弹道");
                    break;
                case eEffectBehavior.WORLD_POS_CENTER:
                    /*switch (_skillEffect.TrackDimension)
                    {
                        case eTrackDimension.X:
                            prefab2.SetWorldPosY(9.259259f);
                            break;
                        case eTrackDimension.Y:
                            prefab2.SetWorldPosX(0.0f);
                            break;
                        case eTrackDimension.NONE:
                            prefab2.transform.position = new Vector3(0.0f, 9.259259f, 0.0f);
                            break;
                    }
                    break;*/
                case eEffectBehavior.TARGET_CENTER:
                    /*Vector3 vector3_1 = new Vector3();
                    int index2 = 0;
                    for (int count = _skillEffect.TargetAction.TargetList.Count; index2 < count; ++index2)
                        vector3_1 += _skillEffect.TargetAction.TargetList[index2].GetPosition() / (float)count;
                    prefab2.transform.position = vector3_1;
                    break;*/
                case eEffectBehavior.SKILL_AREA_RANDOM:
                    break;
                /*Vector3 vector3_2 = new Vector3();
                int attackSide = UnitActionController.GetAttackSide(_skillEffect.TargetAction.Direction, this.Owner);
                float num1 = attackSide < 1 ? -_skillEffect.TargetAction.TargetWidth : 0.0f;
                float num2 = attackSide > -1 ? _skillEffect.TargetAction.TargetWidth : 0.0f;
                float num3 = _skillEffect.TargetAction.ActionType == eActionType.REFLEXIVE ? _skillEffect.TargetAction.TargetList[0].GetPosition().x : this.transform.position.x;
                float num4 = num1 + num3;
                float num5 = num2 + num3;
                if ((double)num4 < -(double)BattleDefine.BATTLE_FIELD_SIZE)
                    num4 = -BattleDefine.BATTLE_FIELD_SIZE;
                if ((double)num5 > (double)BattleDefine.BATTLE_FIELD_SIZE)
                    num5 = BattleDefine.BATTLE_FIELD_SIZE;
                vector3_2.x = (float)(((double)num4 + ((double)num5 - (double)num4) * (double)BattleManager.Random(0.0f, 1f)) / 540.0);
                vector3_2.y += (float)((double)_skillEffect.CenterY + (double)_skillEffect.DeltaY * (double)BattleManager.Random(-1f, 1f) + 9.25925922393799);
                prefab2.transform.position = vector3_2;
                break;*/
                case eEffectBehavior.SERIES:
                    if (_skillEffect.FireActionId != -1)
                    {
                        AppendCoroutine(ExecActionWithDelayAndTarget(_skillEffect.FireAction, _skill, _target, 0.0f), ePauseType.SYSTEM, _skill.BlackOutTime > 0.0 ? Owner : null);
                    }
                    break;
            }
            //prefab2.RestartTween();
        }
        private IEnumerator createNormalPrefabWithDelayAndTarget2(
  NormalSkillEffect _skilleffect,
  Skill _skill,
  float _delay,
  BasePartsData _target,
  bool _first)
        {
            float time = _first ? _skill.CutInSkipTime : 0.0f;
            if (_delay >= (double)time)
            {
                while (true)
                {
                    time += battleManager.DeltaTime_60fps;
                    if (!_skill.Cancel)
                    {
                        if (_delay > (double)time)
                            yield return null;
                        else
                            goto label_6;
                    }
                    else
                        break;
                }
                yield break;
            label_6:
                if (_skilleffect.EffectTarget == eEffectTarget.OWNER)
                    createNormalEffectPrefab2(_skilleffect, _skill, Owner.GetFirstParts(true), null, false, 0.0f, false);
                else
                    createNormalEffectPrefab2(_skilleffect, _skill, _target, null, false, 0.0f, false);
            }
        }
        private void onFirearmHit2(FirearmCtrl2 firearmCtrl)
        {
            if (firearmCtrl == null || firearmCtrl.FireTarget == null || firearmCtrl.Skill == null)
                return;
            //Debug.Log("触发技能");
            for (int index = 0; index < firearmCtrl.ShakeEffects.Count; ++index)
            {
              if (firearmCtrl.ShakeEffects[index].TargetMotion == 0)
                this.AppendCoroutine(this.StartShakeWithDelay(firearmCtrl.ShakeEffects[index], firearmCtrl.Skill), ePauseType.VISUAL, (double) firearmCtrl.Skill.BlackOutTime > 0.0 ? this.Owner : (UnitCtrl) null);
            }
            int index1 = 0;
            for (int count = firearmCtrl.EndActions.Count; index1 < count; ++index1)
                ExecUnitActionWithDelay(firearmCtrl.EndActions[index1], firearmCtrl.Skill, false, true);
            int index2 = 0;
            for (int count = firearmCtrl.SkillHitEffects.Count; index2 < count; ++index2)
                AppendCoroutine(createNormalPrefabWithDelay2(firearmCtrl.SkillHitEffects[index2], firearmCtrl.Skill, _isFirearmEndEffect: true), ePauseType.VISUAL, firearmCtrl.Skill.BlackOutTime > 0.0 ? Owner : null);
        }

    }
    public class FirearmCtrl2 : IUpdate
    {
        protected UnitCtrl source;
        protected bool timeToDie;
        protected bool isPause;
        protected Dictionary<Renderer, int> particleRendererDictionary;

        public UnitCtrl SortTarget { get; set; }

        public bool IsPlaying { get; set; } = true;

        public bool _Update()
        {
            if (timeToDie)
            {
                // Debug.Log($"[{BattleHeaderController.CurrentFrameCount}] firearm set to die @{GetHashCode():x}.");
                IsPlaying = false;
                obj.SetActive(false);
                return false;
            }
            return true;
        }

        public virtual void SetPossitionAppearanceType(
  NormalSkillEffect skillEffect,
  BasePartsData target,
  UnitCtrl _owner,
  Skill skill)
        {
            source = _owner;
            Vector3 vector3 = new Vector3(0.0f, 9.259259f, 0.0f);
            Bone bone = null;
            switch (skillEffect.EffectTarget)
            {
                case eEffectTarget.OWNER:
                case eEffectTarget.ALL_TARGET:
                case eEffectTarget.FORWARD_TARGET:
                case eEffectTarget.BACK_TARGET:
                case eEffectTarget.ALL_OTHER:
                case eEffectTarget.ALL_UNIT_EXCEPT_OWNER:
                    SortTarget = target.Owner;
                    switch (skillEffect.TargetBone)
                    {
                        case eTargetBone.BOTTOM:
                            vector3 = target.GetBottomTransformPosition();
                            break;
                        case eTargetBone.HEAD:
                            bone = target.GetStateBone();
                            vector3 = BattleUnitBaseSpineController.BoneWorldToGlobalPosConsiderRotate(target.GetStateBone(), target.Owner.RotateCenter, target.GetBottomLossyScale());
                            break;
                        case eTargetBone.CENTER:
                            bone = target.GetCenterBone();
                            vector3 = BattleUnitBaseSpineController.BoneWorldToGlobalPosConsiderRotate(target.GetCenterBone(), target.Owner.RotateCenter, target.GetBottomLossyScale());
                            break;
                        case eTargetBone.FIXED_CENETER:
                            vector3 = target.GetBottomTransformPosition() + target.GetFixedCenterPos();
                            break;
                        case eTargetBone.ANY_BONE:
                            bone = (SortTarget.MotionPrefix == -1 ? SortTarget.UnitSpineCtrl : (SkeletonRenderer)SortTarget.UnitSpineCtrlModeChange).skeleton.FindBone(skillEffect.TargetBoneName);
                            skillEffect.TrackType = eTrackType.BONE;
                            skillEffect.TrackDimension = eTrackDimension.XY;
                            vector3 = BattleUnitBaseSpineController.BoneWorldToGlobalPosConsiderRotate(bone, SortTarget.RotateCenter, SortTarget.BottomTransform.lossyScale);
                            break;
                    }
                    break;
            }
            if (skillEffect.EffectBehavior == eEffectBehavior.SERIES_FIREARM || skillEffect.EffectBehavior == eEffectBehavior.FIREARM && skillEffect.TargetBone != eTargetBone.BOTTOM)
            {
                vector3 = target.GetBottomTransformPosition() + target.GetFixedCenterPos();
            }
            position += vector3;
        }

        public bool activeSelf = true;

        public FirearmCtrlData data;
        private GameObject obj;
        private Vector2 _position = Vector2.zero;
        public Vector3 position
        {
            get => _position;
            set
            {
                _position = value;
                obj.transform.position = _position;
            }
        }
        public bool IsAbsolute { get; set; }

        public bool InFlag { get; set; }

        public BasePartsData FireTarget { get; protected set; }

        public List<NormalSkillEffect> SkillHitEffects { set; get; }

        public List<ActionParameter> EndActions { set; get; }

        public Skill Skill { set; get; }

        public Action<FirearmCtrl2> OnHitAction { get; set; }

        public Vector3 TargetPos { get; set; }

        public List<ShakeEffect> ShakeEffects { get; set; }

        private bool stopFlag { get; set; }

        protected Vector3 initialPosistion { get; set; }

        protected Vector3 speed { get; set; }

        public UnitCtrl owner { get; set; }

        protected BattleManager battleManager => BattleManager.Instance;

        public string name;
        public FirearmCtrl2(string name)
        {
            this.name = name;
            obj = Object.Instantiate(MyGameCtrl.Instance.firearmPrefab);
            // Debug.Log($"[{BattleHeaderController.CurrentFrameCount}] firearm constructing {name}@{GetHashCode():x}.");
        }

        public void Destory()
        {
            Object.Destroy(obj);
        }

        ~FirearmCtrl2()
        {
            try
            {
                Object.Destroy(obj);
            }
            catch
            {

            }
        }

        public void ResetParam()
        {
            IsPlaying = true;
            activeSelf = true;
            stopFlag = false;
            timeToDie = false;
            obj.transform.localPosition = MyGameCtrl.Instance.firearmPrefab.transform.localPosition;
            this._position = obj.transform.position;
            obj.SetActive(true);
        }

        public void Initialize(
  BasePartsData _target,
  List<ActionParameter> _actions,
  Skill _skill,
  List<NormalSkillEffect> _skillEffect,
  UnitCtrl _owner,
  float _height,
  bool _hasBlackOutTime,
  bool _isAbsolute,
  Vector3 _targetPosition,
  List<ShakeEffect> _shakes,
  eTargetBone _targetBone)
        {
            battleManager.AddEffectToUpdateList(this);

            if (!battleManager.skipping && BattleManager.enableFirearmBug)
                Debug.Log($"[{BattleHeaderController.CurrentFrameCount}] firearm intiializing {name}@{GetHashCode():x}.");

            ShakeEffects = _shakes;
            IsAbsolute = _isAbsolute;
            Skill = _skill;
            if (_isAbsolute)
            {
                TargetPos = _targetPosition;
            }
            else
            {
                switch (_targetBone)
                {
                    case eTargetBone.BOTTOM:
                        TargetPos = _target.GetBottomTransformPosition();
                        break;
                    case eTargetBone.HEAD:
                        TargetPos = getHeadBonePos(_target);
                        break;
                    case eTargetBone.CENTER:
                    case eTargetBone.FIXED_CENETER:
                        TargetPos = _target.GetBottomTransformPosition() + _target.GetFixedCenterPos();
                        break;
                }
            }
            FireTarget = _target;
            //_target.Owner.FirearmCtrlsOnMe.Add(this);
            EndActions = _actions;
            SkillHitEffects = _skillEffect;
            owner = _owner;
            setInitialPosition();
            initMoveType(_height, _owner);
            battleManager.AppendCoroutine(updatePosition(Vector3.Distance(TargetPos, initialPosistion) + 1f), ePauseType.SYSTEM, _hasBlackOutTime ? _owner : null);
            //this.battleManager.AppendEffect((SkillEffectCtrl)this, _hasBlackOutTime ? _owner : (UnitCtrl)null, false);
        }
        protected virtual Vector3 getHeadBonePos(BasePartsData _target) => _target.GetBottomTransformPosition() + _target.GetFixedCenterPos();
        protected virtual void setInitialPosition() => initialPosistion = position;
        private void initMoveType(float _height, UnitCtrl _owner)
        {
            switch ((eMoveTypes)data.MoveType)
            {
                case eMoveTypes.LINEAR:
                    Vector3 toDirection = TargetPos - position;
                    toDirection.z = 0;
                    toDirection.Normalize();
                    //this.speed = this.MoveRate * toDirection;
                    speed = data.MoveRate * toDirection;
                    //this.transform.rotation = Quaternion.FromToRotation((UnityEngine.Object)_owner == (UnityEngine.Object)null || !_owner.IsLeftDir ? Vector3.right : Vector3.left, toDirection);
                    break;
                case eMoveTypes.NONE:
                case eMoveTypes.HORIZONTAL:
                    speed = new Vector3(data.MoveRate, 0.0f, 0.0f);
                    break;
                case eMoveTypes.PARABORIC:
                case eMoveTypes.PARABORIC_ROTATE:
                    float durationTime = data.duration / 2f;
                    easingUpY = new CustomEasing(CustomEasing.eType.outCubic, position.y, position.y + _height, durationTime);
                    easingDownY = new CustomEasing(CustomEasing.eType.inQuad, position.y + _height, TargetPos.y, durationTime);
                    easingX = new CustomEasing(CustomEasing.eType.linear, position.x, TargetPos.x, data.duration);
                    break;
            }
            if ((eMoveTypes)data.MoveType != eMoveTypes.PARABORIC_ROTATE)
                return;
            easingUpRotate = new CustomEasing(CustomEasing.eType.inQuad, data.startRotate, 0.0f, data.duration / 2f);
            easingDownRotate = new CustomEasing(CustomEasing.eType.linear, 0.0f, data.endRotate, data.duration / 2f);
        }

        private Vector3 GetParaboricPosition(float _currentTime, float _deltaTime)
        {
            Vector3 position = this.position;
            if (easingX.IsMoving)
            {
                position = new Vector3(easingX.GetCurVal(_deltaTime, true), (_currentTime >= data.duration / 2f) ? easingDownY.GetCurVal(_deltaTime, true) : easingUpY.GetCurVal(_deltaTime, true), this.position.z);
            }
            return position;
        }

        private CustomEasing easingX;
        private CustomEasing easingUpY;
        private CustomEasing easingDownY;
        private CustomEasing easingUpRotate;
        private CustomEasing easingDownRotate;

        public bool updatePositionRunning = false;

        private IEnumerator updatePosition(float _lifeDistance)
        {
            updatePositionRunning = true;
            float currentTime = 0.0f;
            bool hitFlag = false;
            float hitTimer = 0.0f;
            while (IsPlaying)
            {
                // Debug.Log($"[{BattleHeaderController.CurrentFrameCount}] firearm updating position {name}@{GetHashCode():x}.");
                if (!this.activeSelf)
                {
                    yield return null;
                    continue;
                }
                float deltaTime = battleManager.DeltaTime_60fps;
                if (hitFlag)
                {
                    hitTimer += deltaTime;
                    float _b = ((eMoveTypes)data.MoveType) == eMoveTypes.LINEAR ? 0.2f / data.MoveRate : data.HitDelay;
                    if (hitTimer > (double)_b || BattleUtil.Approximately(hitTimer, _b))
                    {
                        if (!battleManager.skipping && BattleManager.enableFirearmBug)
                            Debug.Log($"[{BattleHeaderController.CurrentFrameCount}] firearm hit target and stopping {name}@{GetHashCode():x}.");
                        hitTimer = 0.0f;
                        hitFlag = false;
                        stopFlag = true;
                        if (OnHitAction != null)
                        {
                            //FireTarget.Owner.FirearmCtrlsOnMe.Remove(data);
                            OnHitAction(this);
                        }
                        //onCowHit.Call();
                        //onCowHit = (Action)null;
                    }
                }

                if (getStopFlag())
                {
                    activeSelf = false;
                    //data.FireTarget.Owner.FirearmCtrlsOnMe.Remove(data);
                    timeToDie = true;
                }
                else
                {
                    Vector3 b = position;
                    switch ((eMoveTypes)data.MoveType)
                    {
                        case eMoveTypes.LINEAR:
                        case eMoveTypes.HORIZONTAL:
                            //case eMoveTypes.PARABORIC:
                            //case eMoveTypes.PARABORIC_ROTATE:

                            //b += new Vector3(speed.x * deltaTime,0,0);
                            b += speed * deltaTime;
                            break;
                        case eMoveTypes.PARABORIC:
                        case eMoveTypes.PARABORIC_ROTATE:
                            currentTime += deltaTime;
                            b = GetParaboricPosition(currentTime, deltaTime);
                            if (currentTime > (double)data.duration)
                            {
                                hitFlag = true;
                            }
                            break;
                    }
                    position = b;
                    var moveType = (eMoveTypes) data.MoveType;
                    if ((moveType == eMoveTypes.LINEAR || moveType == eMoveTypes.HORIZONTAL) && Vector3.Distance(this.initialPosistion, b) > _lifeDistance)
                    {
                        // this.FireTarget.Owner.FirearmCtrlsOnMe.Remove(this);
                        // Debug.Log($"[{BattleHeaderController.CurrentFrameCount}] firearm hit set to die due to out of distance {name}@{GetHashCode():x}.");
                        this.timeToDie = true;
                    }
                }
                if (!IsAbsolute)
                    hitFlag = collisionDetection(hitFlag, currentTime);
                yield return null;

            }
            updatePositionRunning = false;
            // Debug.Log($"[{BattleHeaderController.CurrentFrameCount}] firearm update position exit @{GetHashCode():x}.");
        }
        protected virtual bool getStopFlag() => stopFlag;
        private bool collisionDetection(bool _hitFlag, float _currentTime)
        {
            if ((((eMoveTypes)data.MoveType) == eMoveTypes.PARABORIC || (eMoveTypes)data.MoveType == eMoveTypes.PARABORIC_ROTATE) && _currentTime < data.duration * 0.5 || (_hitFlag || InFlag))
                return _hitFlag;
            float num1 = position.x + data.ColliderBoxCentre[0];
            float num2 = data.ColliderBoxSize[0] * 0.5f;
            double num3 = FireTarget.GetColliderCenter().x + (double)FireTarget.GetPosition().x;
            float num4 = FireTarget.GetColliderSize().x * 0.5f;
            float _b1 = num1 - num2;
            float _b2 = num1 + num2;
            float _a1 = (float)num3 - num4;
            float _a2 = (float)num3 + num4;
            //Debug.Log(BattleHeaderController.CurrentFrameCount + "-弹道检测：" + num1 + "--" + num2 + "--" + num3+"--"+num4);
            //if ((double)_a1 >= (double)_b2 && !BattleUtil.Approximately(_a1, _b2) || (double)_a2 <= (double)_b1 && !BattleUtil.Approximately(_a2, _b1))
            //   return _hitFlag;
            if (speed.x > 0)
            {
                if (_a1 >= (double)_b2 && !BattleUtil.Approximately(_a1, _b2))
                    return _hitFlag;
            }
            else
            {
                if (_a2 <= (double)_b1 && !BattleUtil.Approximately(_a2, _b1))
                    return _hitFlag;
            }
            if ((eMoveTypes)data.MoveType != eMoveTypes.PARABORIC && (eMoveTypes)data.MoveType != eMoveTypes.PARABORIC_ROTATE)
                return true;
            InFlag = true;
            return _hitFlag;
        }


    }
}