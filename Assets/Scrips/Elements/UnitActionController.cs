// Decompiled with JetBrains decompiler
// Type: Elements.UnitActionController
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cute;
using Elements.Battle;
using PCRCaculator;
using PCRCaculator.Battle;
using PCRCaculator.Guild;
using Spine;
using UnityEngine;

namespace Elements
{
    public partial class UnitActionController : MonoBehaviour, ISingletonField
    {
        new public FixedTransformMonoBehavior.FixedTransform transform;
        public ActionParameterOnPrefabDetail AttackDetail;
        public bool UseDefaultDelay = true;
        public Skill Attack;
        public List<Skill> UnionBurstList;
        public List<Skill> MainSkillList;
        public List<Skill> SpecialSkillList;
        public List<Skill> SpecialSkillEvolutionList;
        public List<Skill> UnionBurstEvolutionList;
        public List<Skill> MainSkillEvolutionList;
        public List<Skill> SubUnionBurstList;
        public Skill Annihilation;
        //private static Yggdrasil<UnitActionController> staticSingletonTree;
        private static BattleManagerForActionController staticBattleManager;

        private IBattleCameraEffectForUnitActionController battleCameraEffect;
        //private static BMIOELFOCPE staticBattleCameraEffect;
        private static BattleEffectPoolInterface staticBattleEffectPool;
        //private static JFJNEHKINFI staticBattleTimeScale;
        public const long ACTION_ID_OFFSET = 100;
        private const int ACTION_ID_OFFSET_FOR_CHARA_ID = 10000;
        private const int DEFAULT_MOTION_NUMBER = 0;
        private const int LOOP_MOTION_NUMBER = 1;
        private const int FIRST_TARGET_INDEX = 0;

        public UnitCtrl Owner { get; set; }

        public bool Skill1IsChargeTime { set; get; }

        public bool Skill1Charging { set; get; }
        public bool StopModeChangeEndEffectCalled
        {
            get;
            set;
        }

        public bool DisableUBByModeChange { set; get; }

        public bool ModeChanging { get; set; }

        public bool MoveEnd { get; set; }

        public Dictionary<int, Skill> skillDictionary { get; private set; }

        public bool ContinuousActionEndDone { get; set; }

        private bool isUnionBurstOnlyOwner { get; set; }

        private bool updateBranchMotionRunning { get; set; }

        private BattleManagerForActionController battleManager => staticBattleManager;

        //private JFJNEHKINFI battleTimeScale => UnitstaticBattleTimeScale;
        public bool GetIsSkillPrincessForm(int _skillId) => skillDictionary[_skillId].IsPrincessForm;

        //private BMIOELFOCPE battleCameraEffect => UnitstaticBattleCameraEffect;

        private BattleEffectPoolInterface battleEffectPool => staticBattleEffectPool;

        public List<PrincessSkillMovieData> GetPrincessFormMovieData(int _skillId) => skillDictionary[_skillId].PrincessSkillMovieDataList;

        public static void StaticRelease()
        {
            //UnitstaticSingletonTree = (Yggdrasil<UnitActionController>) null;
            staticBattleManager = null;
            //UnitstaticBattleCameraEffect = (BMIOELFOCPE) null;
            staticBattleEffectPool = null;
            //UnitstaticBattleTimeScale = (JFJNEHKINFI) null;
        }

        private void OnDestroy()
        {
            AttackDetail = null;
            Attack = null;
            UnionBurstList = null;
            MainSkillList = null;
            SpecialSkillList = null;
            SpecialSkillEvolutionList = null;
            Owner = null;
            skillDictionary = null;
        }

        public void Initialize(
          UnitCtrl _owner,
          UnitParameter _unitParameter,
          bool _initializeAttackOnly = false,
          UnitCtrl _seOwner = null)
        {
            /*if (UnitstaticSingletonTree == null)
            {
              UnitstaticSingletonTree = this.CreateSingletonTree<UnitActionController>();
              UnitstaticBattleManager = (GHPNJFDPICH) UnitstaticSingletonTree.Get<BattleManager>();
              UnitstaticBattleCameraEffect = (BMIOELFOCPE) UnitstaticSingletonTree.Get<CMMLKFHCEPD>();
              UnitstaticBattleEffectPool = (BattleEffectPoolInterface) UnitstaticSingletonTree.Get<BattleEffectPool>();
              UnitstaticBattleTimeScale = (JFJNEHKINFI) UnitstaticSingletonTree.Get<BattleSpeedManager>();
            }*/

            staticBattleManager = BattleManager.Instance;
            staticBattleEffectPool = BattleManager.Instance.battleEffectPool;
            battleCameraEffect = BattleManager.Instance.battleCameraEffect;
            Owner = _owner;
            transform = _owner.transform;
            //MyGameCtrl.ResetSkillEffects(this);
            skillDictionary = new Dictionary<int, Skill>();
            Attack.AnimId = eSpineCharacterAnimeId.ATTACK;
            Attack.WeaponType = _initializeAttackOnly ? _seOwner.WeaponSeType : Owner.WeaponSeType;
            Attack.skillAreaWidth = Owner.SearchAreaSize;
            Attack.SkillId = 1;
            Attack.SkillName = "普攻";
            AttackAction attackAction = new AttackAction();
            attackAction.Initialize();
            attackAction.TargetAssignment = eTargetAssignment.OTHER_SIDE;
            attackAction.TargetSort = PriorityPattern.NEAR;
            attackAction.TargetNth = 0;
            attackAction.TargetNum = 1;
            attackAction.TargetWidth = Owner.SearchAreaSize;
            attackAction.Direction = DirectionType.FRONT;
            attackAction.ActionType = eActionType.ATTACK;
            attackAction.Value = new Dictionary<eValueNumber, FloatWithEx>(new eValueNumber_DictComparer())
      {
        {
          eValueNumber.VALUE_1,
          0.0f
        },
        {
          eValueNumber.VALUE_3,
          1f
        }
      };
            attackAction.ActionDetail1 = _owner.AtkType;
            float num = 0.0f;
            if (UseDefaultDelay && BattleDefine.WEAPON_HIT_DELAY_DIC.TryGetValue(_initializeAttackOnly ? _seOwner.WeaponMotionType : Owner.WeaponMotionType, out num))
            {
                attackAction.ActionExecTimeList = new List<ActionExecTime>
                {
          new ActionExecTime
          {
            Weight = 1f,
            DamageNumType = eDamageEffectType.NORMAL
          }
        };
                attackAction.ExecTime = new float[1] { num };
                attackAction.ActionWeightSum = 1f;
            }
            else
            {
                ActionParameterOnPrefabDetail attackDetail = AttackDetail;
                attackDetail.ExecTime = new List<ActionExecTime>(attackDetail.ExecTimeForPrefab);
                int index1 = 0;
                for (int count = attackDetail.ExecTimeCombo.Count; index1 < count; ++index1)
                {
                    ActionExecTimeCombo actionExecTimeCombo = attackDetail.ExecTimeCombo[index1];
                    for (int index2 = 0; index2 < actionExecTimeCombo.Count; ++index2)
                    {
                        switch (actionExecTimeCombo.InterporationType)
                        {
                            case eComboInterporationType.LINEAR:
                                attackDetail.ExecTime.Add(new ActionExecTime
                                {
                                    Time = actionExecTimeCombo.StartTime + index2 * actionExecTimeCombo.OffsetTime,
                                    DamageNumType = eDamageEffectType.COMBO
                                });
                                break;
                            case eComboInterporationType.CURVE:
                                attackDetail.ExecTime.Add(new ActionExecTime
                                {
                                    Time = actionExecTimeCombo.StartTime + actionExecTimeCombo.Curve.Evaluate(index2 / (float)actionExecTimeCombo.Count) * actionExecTimeCombo.Count * actionExecTimeCombo.OffsetTime,
                                    DamageNumType = eDamageEffectType.COMBO
                                });
                                break;
                        }
                    }
                }
                attackDetail.ExecTime.Sort((a, b) => a.Time.CompareTo(b.Time));
                attackAction.ActionExecTimeList = attackDetail.ExecTime;
                attackAction.ExecTime = new float[attackDetail.ExecTime.Count];
                for (int index2 = 0; index2 < attackDetail.ExecTime.Count; ++index2)
                {
                    attackAction.ActionWeightSum += attackDetail.ExecTime[index2].Weight;
                    attackAction.ExecTime[index2] = attackDetail.ExecTime[index2].Time;
                }
            }
            attackAction.ActionChildrenIndexes = new List<int>();
            attackAction.ActionId = UnitUtility.GetCharaId(_owner.UnitId) * 10000;
            attackAction.TargetList = new List<BasePartsData>();
            Attack.ActionParameters = new List<ActionParameter>();
            Attack.ActionParameters.Add(attackAction);
            skillDictionary.Add(1, Attack);
            dependActionSolve(Attack);
            execActionOnStart(Attack);
            if (_initializeAttackOnly)
                return;
            MasterUnitSkillData.UnitSkillData skillData = _unitParameter.SkillData;
            if (UnionBurstList.Count != 0)
            {
                List<int> unionBurstIds = skillData.UnionBurstIds;
                skillDictionary.Add(unionBurstIds[0], UnionBurstList[0]);
                isUnionBurstOnlyOwner = (uint)unionBurstIds[0] > 0U;
                UnionBurstList[0].SkillNum = 0;
            }
            if (UnionBurstEvolutionList.Count != 0)
            {
                skillDictionary.Add(skillData.UnionBurstEvolutionIds[0], UnionBurstEvolutionList[0]);
                UnionBurstEvolutionList[0].SkillNum = 0;
            }
            List<int> mainSkillIds = skillData.MainSkillIds;
            int index3 = 0;
            for (int count = mainSkillIds.Count; index3 < count; ++index3)
            {
                if (mainSkillIds[index3] != 0)
                {
                    if (MainSkillList.Count > index3)
                    {
                        MainSkillList[index3].SkillNum = index3 + 1;
                        skillDictionary.Add(mainSkillIds[index3], MainSkillList[index3]);
                    }
                    else if (index3 == MainSkillList.Count)
                    {
                        skillDictionary.Add(mainSkillIds[index3], Attack);

                        Debug.LogError("角色" + Owner.UnitId + "的技能" + mainSkillIds[index3] + "错误！");
                    }
                }
            }
            List<int> skillEvolutionIds1 = skillData.MainSkillEvolutionIds;
            for (int index1 = 0; index1 < skillEvolutionIds1.Count; ++index1)
            {
                if (skillEvolutionIds1[index1] != 0 && index1 < MainSkillEvolutionList.Count)
                {
                    MainSkillEvolutionList[index1].SkillNum = index1 + 1;
                    skillDictionary.Add(skillEvolutionIds1[index1], MainSkillEvolutionList[index1]);
                }
            }
            List<int> spSkillIds = skillData.SpSkillIds;
            int index4 = 0;
            for (int count = spSkillIds.Count; index4 < count && spSkillIds[index4] != 0; ++index4)
            {
                SpecialSkillList[index4].SkillNum = index4 + 1;
                skillDictionary.Add(spSkillIds[index4], SpecialSkillList[index4]);
                Owner.SkillLevels[spSkillIds[index4]] = Owner.Level;
            }
            List<int> skillEvolutionIds2 = skillData.SpSkillEvolutionIds;
            for (int index1 = 0; index1 < skillEvolutionIds2.Count; ++index1)
            {
                int key = skillEvolutionIds2[index1];
                if (key != 0)
                {
                    SpecialSkillEvolutionList[index1].SkillNum = index1 + 1;
                    skillDictionary.Add(key, SpecialSkillEvolutionList[index1]);
                    Owner.SkillLevels[key] = Owner.Level;
                }
                else
                    break;
            }
            List<int> subUnionBurstIds = skillData.SubUnionBurstIds;
            for (int num3 = 0; num3 < subUnionBurstIds.Count; num3++)
            {
                int num4 = subUnionBurstIds[num3];
                if (num4 == 0)
                {
                    break;
                }
                SubUnionBurstList[num3].SkillNum = num3;
                skillDictionary.Add(num4, SubUnionBurstList[num3]);
                Owner.SkillLevels[num4] = Owner.Level;
            }
            Dictionary<int, int> dictionary = new Dictionary<int, int>();
            foreach (KeyValuePair<int, Skill> skill in skillDictionary)
            {
                Skill _skill = skill.Value;
                int key = skill.Key;
                switch (key)
                {
                    case 0:
                    case 1:
                        continue;
                    default:
                        int lv = Owner.SkillLevels[key];
                        if (lv != 0)
                        {
                            SkillData data = MainManager.Instance.SkillDataDic[key];
                            MasterSkillData.SkillData _skillParameter = new MasterSkillData.SkillData(data);
                            try
                            {
                                setSkillParameter(_skill, _skillParameter);
                                _skill.SetLevel(lv);
                                List<MasterUnitSkillDataRf.UnitSkillDataRf> list = MainManager.Instance.MasterUnitSkillDataRf.GetListWithSkillId(key).OrderBy((Func<MasterUnitSkillDataRf.UnitSkillDataRf, int>)((MasterUnitSkillDataRf.UnitSkillDataRf e) => e.min_lv)).ToList();
                                for (int num6 = 0; num6 < list.Count; num6++)
                                {
                                    MasterUnitSkillDataRf.UnitSkillDataRf unitSkillDataRf = list[num6];
                                    if (lv < (int)unitSkillDataRf.min_lv)
                                    {
                                        if (num6 != 0)
                                        {
                                            dictionary.Add(_skill.SkillId, num6 - 1);
                                        }
                                        break;
                                    }
                                    if (list.Count == num6 + 1)
                                    {
                                        dictionary.Add(_skill.SkillId, num6);
                                    }
                                }

                            }
                            catch
                            {
                                MainManager.Instance.WindowMessage($"技能: {_skill.SkillName}({_skill.SkillId})含有超前动作，已被重置为普攻");
                                Owner.SkillLevels[key] = 0;
                            }
                        }
                        continue;
                }
            }
            if (MainManager.Instance.Enemy_ignore_skill_rf.Contains(Owner.UnitId))//ManagerSingleton<MasterDataManager>.Instance.masterEnemyIgnoreSkillRf.Get(Owner.UnitId) == null)
            {
                foreach (KeyValuePair<int, int> item2 in dictionary)
                {
                    skillDictionary[item2.Key] = skillDictionary[item2.Key].OverrideSkillList[item2.Value];
                }
            }
            if (UnionBurstList.Count != 0)
                setCutInSkipTimeForPrincessForm(skillData.UnionBurstIds[0]);
            if (UnionBurstEvolutionList.Count == 0)
                return;
            setCutInSkipTimeForPrincessForm(skillData.UnionBurstEvolutionIds[0]);
        }

        private void setSkillParameter(Skill _skill, MasterSkillData.SkillData _skillParameter,int _parentSkillId = -1)
        {
            Dictionary<eActionType, int> actionCounter = new Dictionary<eActionType, int>(new eActionType_DictComparer());
            _skill.ActionParameters = new List<ActionParameter>();
            if (_skillParameter == null)
                return;
            if (_skillParameter.SkillId == Owner.UnionBurstSkillId)
            {
                //eUbResponceVoiceType responceVoiceType = eUbResponceVoiceType.APPLOUD;
                //if (!SkillDefine.UbResponceVoiceDictionary.TryGetValue((int) _skillParameter.icon_type, out responceVoiceType))
                //  responceVoiceType = eUbResponceVoiceType.APPLOUD;
                //this.Owner.UbResponceVoiceType = responceVoiceType;
            }
            if (_parentSkillId != -1)
            {
                _skill.SkillId = _parentSkillId;
            }
            else
            {
                _skill.SkillId = _skillParameter.SkillId;
            }
            _skill.skillAreaWidth = _skillParameter.skill_area_width == 0 ? Owner.SearchAreaSize : _skillParameter.skill_area_width;
            for (int index = 0; index < _skillParameter.ActionDataList.Count; ++index)
            {
                MasterSkillAction.SkillAction actionData = _skillParameter.ActionDataList[index];
                createActionValue(_skillParameter, actionData, _skill, actionCounter);
            }
            _skill.CastTime = (float)(double)_skillParameter.skill_cast_time;
            _skill.UnionBurstCoolDownTime = (float)(double)_skillParameter.boss_ub_cool_time;
            _skill.SkillName = _skillParameter.Name;
            //add scripts
            if (MainManager.Instance.SkillNameAndDescribe_cn.TryGetValue(_skill.SkillId, out string[] names))
            {
                _skill.SkillName = names[0];
            }
            switch (_skill.SkillMotionType)
            {
                case SkillMotionType.DEFAULT:
                    _skill.AnimId = Owner.animeIdDictionary[_skill.SkillId];
                    break;
                case SkillMotionType.AWAKE:
                    _skill.AnimId = eSpineCharacterAnimeId.AWAKE;
                    break;
                case SkillMotionType.ATTACK:
                    _skill.AnimId = eSpineCharacterAnimeId.ATTACK;
                    break;
                case SkillMotionType.EVOLUTION:
                    _skill.AnimId = eSpineCharacterAnimeId.SKILL_EVOLUTION;
                    break;
                case SkillMotionType.SP_EVOLUTION:
                    _skill.AnimId = eSpineCharacterAnimeId.SPECIAL_SKILL_EVOLUTION;
                    break;
            }
            dependActionSolve(_skill);
            initializeAction(_skill);
            /*foreach (NormalSkillEffect skillEffect in _skill.SkillEffects)
            {
                if (!((UnityEngine.Object)skillEffect.PrefabLeft == (UnityEngine.Object)null) && !((UnityEngine.Object)skillEffect.Prefab == (UnityEngine.Object)null) && (bool)(UnityEngine.Object)skillEffect.PrefabLeft.GetComponentInChildren<MeshRenderer>())
                {
                    SkillEffectCtrl left = this.battleEffectPool.GetEffect(skillEffect.PrefabLeft);
                    left.SetTimeToDie(true);
                    left.IsPlaying = false;
                    //left.transform.parent = ExceptNGUIRoot.Transform;
                    left.transform.SetParent(ExceptNGUIRoot.Transform,false);
                    this.Timer((System.Action)(() => left.gameObject.SetActive(false)));
                    SkillEffectCtrl right = this.battleEffectPool.GetEffect(skillEffect.Prefab);
                    right.SetTimeToDie(true);
                    right.IsPlaying = false;
                    //right.transform.parent = ExceptNGUIRoot.Transform;
                    right.transform.SetParent(ExceptNGUIRoot.Transform,false);
                    this.Timer((System.Action)(() => right.gameObject.SetActive(false)));
                }
            }*/
            List<MasterUnitSkillDataRf.UnitSkillDataRf> listWithSkillId = MainManager.Instance.MasterUnitSkillDataRf.GetListWithSkillId(_skillParameter.SkillId);

            for (int j = 0; j < _skill.OverrideSkillList.Count; j++)
            {
                if (listWithSkillId.Count > j)
                {
                    _skill.OverrideSkillList[j].SkillNum = _skill.SkillNum;
                    SkillData data = MainManager.Instance.SkillDataDic[listWithSkillId[j].rf_skill_id];
                    MasterSkillData.SkillData temp0 = new MasterSkillData.SkillData(data);

                    setSkillParameter(_skill.OverrideSkillList[j], temp0, _skillParameter.SkillId);
                    _skill.OverrideSkillList[j].SetLevel(Owner.SkillLevels[_skillParameter.SkillId]);
                }
            }
        }

        private void dependActionSolve(Skill skill)
        {
            for (int index1 = 0; index1 < skill.ActionParameters.Count; ++index1)
            {
                if (skill.ActionParameters[index1].DepenedActionId != 0)
                {
                    int index2 = 0;
                    for (int count = skill.ActionParameters.Count; index2 < count; ++index2)
                    {
                        ActionParameter actionParameter = skill.ActionParameters[index2];
                        if (skill.ActionParameters[index1].DepenedActionId == actionParameter.ActionId)
                            actionParameter.ActionChildrenIndexes.Add(index1);
                    }
                }
            }
            skill.HasParentIndexes = new List<int>();
            int index3 = 0;
            for (int count1 = skill.ActionParameters.Count; index3 < count1; ++index3)
            {
                ActionParameter actionParameter = skill.ActionParameters[index3];
                skill.HasParentIndexes = skill.HasParentIndexes.Union(actionParameter.ActionChildrenIndexes).ToList();
                if (actionParameter.ActionType == eActionType.REFLEXIVE)
                {
                    int index1 = 0;
                    for (int count2 = actionParameter.ActionChildrenIndexes.Count; index1 < count2; ++index1)
                        skill.ActionParameters[actionParameter.ActionChildrenIndexes[index1]].ReferencedByReflection = true;
                }
            }
            UpdateEffectRunTimeData(skill, skill.SkillEffects);
        }

        public void UpdateEffectRunTimeData(
          Skill _skill,
          List<NormalSkillEffect> _skillList,
          bool _resetflag = true)
        {
            if (_resetflag)
            {
                int index = 0;
                for (int count = _skill.ActionParameters.Count; index < count; ++index)
                    _skill.ActionParameters[index].ReferencedByEffect = false;
            }
            int index1 = 0;
            for (int count = _skillList.Count; index1 < count; ++index1)
            {
                NormalSkillEffect effect = _skillList[index1];
                effect.TargetAction = _skill.ActionParameters.Find(e => e.ActionId == effect.TargetActionId || (e.ActionId - 1000) == effect.TargetActionId);
                if (effect.TargetAction == null)
                    effect.TargetAction = _skill.ActionParameters[0];
                UpdateEffectRunTimeData(_skill, effect.FireArmEndEffects, false);
                switch (effect.EffectBehavior)
                {
                    case eEffectBehavior.FIREARM:
                    case eEffectBehavior.SERIES:
                    case eEffectBehavior.SERIES_FIREARM:
                        if (_skill.SkillId == 1)
                        {
                            effect.FireAction = _skill.ActionParameters[0];
                            effect.FireAction.ReferencedByEffect = true;
                            break;
                        }
                        if (effect.FireActionId != -1)
                        {
                            effect.FireAction = _skill.ActionParameters.Find(e => e.ActionId == effect.FireActionId || (e.ActionId - 1000) == effect.FireActionId);
                            if (effect.FireAction != null)
                                effect.FireAction.ReferencedByEffect = true;
                        }
                        break;
                }
            }
        }

        private void execActionOnStart(Skill _skill)
        {
            for (int index = 0; index < _skill.ActionParameters.Count; ++index)
            {
                ActionParameter actionParameter = _skill.ActionParameters[index];
                if (actionParameter is AttackActionBase)
                    _skill.HasAttack = true;
                actionParameter.ExecActionOnStart(_skill, Owner, this);
            }
            for (int index = 0; index < _skill.SkillEffects.Count; ++index)
            {
                _skill.SkillEffects[index].AlreadyFireArmExecedData = new Dictionary<UnitCtrl, bool>();
                _skill.SkillEffects[index].AlreadyFireArmExecedKeys = new List<UnitCtrl>();
            }
            _skill.DamagedPartsList = new List<BasePartsData>();
        }

        private void execActionOnWaveStart(Skill _skill)
        {
            for (int index = 0; index < _skill.ActionParameters.Count; ++index)
                _skill.ActionParameters[index].ExecActionOnWaveStart(_skill, Owner, this);
        }

        private void initializeAction(Skill _skill)
        {
            for (int index = 0; index < _skill.ActionParameters.Count; ++index)
                _skill.ActionParameters[index].Initialize(Owner);
        }

        private void createActionValue(
          MasterSkillData.SkillData skillParameter,
          MasterSkillAction.SkillAction actionParam,
          Skill skill,
          Dictionary<eActionType, int> actionCounter)
        {
            ActionParameter actionParameter = Activator.CreateInstance(SkillDefine.SkillActionTypeDictionary[(eActionType)(byte)actionParam.action_type]) as ActionParameter;
            actionParameter.TargetSort = (PriorityPattern)(int)actionParam.target_type;
            actionParameter.TargetNth = actionParam.target_number;
            actionParameter.TargetNum = actionParam.target_count;
            actionParameter.TargetWidth = actionParam.target_range <= 0 ? Owner.SearchAreaSize : actionParam.target_range;
            actionParameter.ActionType = (eActionType)(byte)actionParam.action_type;
            actionParameter.ActionDetail1 = actionParam.action_detail_1;
            actionParameter.ActionDetail2 = actionParam.action_detail_2;
            actionParameter.ActionDetail3 = actionParam.action_detail_3;
            actionParameter.Value = new Dictionary<eValueNumber, FloatWithEx>(new eValueNumber_DictComparer());
            actionParameter.Value.Add(eValueNumber.VALUE_1, (float)(double)actionParam.action_value_1);
            actionParameter.Value.Add(eValueNumber.VALUE_2, (float)(double)actionParam.action_value_2);
            actionParameter.Value.Add(eValueNumber.VALUE_3, (float)(double)actionParam.action_value_3);
            actionParameter.Value.Add(eValueNumber.VALUE_4, (float)(double)actionParam.action_value_4);
            actionParameter.Value.Add(eValueNumber.VALUE_5, (float)(double)actionParam.action_value_5);
            actionParameter.Value.Add(eValueNumber.VALUE_6, (float)(double)actionParam.action_value_6);
            actionParameter.Value.Add(eValueNumber.VALUE_7, (float)(double)actionParam.action_value_7);
            actionParameter.MasterData = actionParam;
            int index1 = 0;
            if (actionCounter.ContainsKey(actionParameter.ActionType))
            {
                index1 = actionCounter[actionParameter.ActionType];
                actionCounter[actionParameter.ActionType]++;
            }
            else
                actionCounter.Add(actionParameter.ActionType, 1);
            ActionParameterOnPrefab parameterOnPrefab = skill.ActionParametersOnPrefab.Find(e => e.ActionType == actionParameter.ActionType);
            if (parameterOnPrefab == null)
            {
                parameterOnPrefab = new ActionParameterOnPrefab
                {
                    ActionType = actionParameter.ActionType
                };
                skill.ActionParametersOnPrefab.Add(parameterOnPrefab);
            }
            ActionParameterOnPrefabDetail parameterOnPrefabDetail;
            if (index1 >= parameterOnPrefab.Details.Count)
            {
                parameterOnPrefabDetail = new ActionParameterOnPrefabDetail
                {
                    ExecTime = new List<ActionExecTime>()
                };
                parameterOnPrefab.Details.Add(parameterOnPrefabDetail);
            }
            else
            {
                parameterOnPrefabDetail = parameterOnPrefab.Details[index1];
                parameterOnPrefabDetail.ExecTime = new List<ActionExecTime>(parameterOnPrefabDetail.ExecTimeForPrefab);
                int index2 = 0;
                for (int count = parameterOnPrefabDetail.ExecTimeCombo.Count; index2 < count; ++index2)
                {
                    ActionExecTimeCombo actionExecTimeCombo = parameterOnPrefabDetail.ExecTimeCombo[index2];
                    for (int index3 = 0; index3 < actionExecTimeCombo.Count; ++index3)
                    {
                        switch (actionExecTimeCombo.InterporationType)
                        {
                            case eComboInterporationType.LINEAR:
                                parameterOnPrefabDetail.ExecTime.Add(new ActionExecTime
                                {
                                    Time = actionExecTimeCombo.StartTime + index3 * actionExecTimeCombo.OffsetTime,
                                    DamageNumType = eDamageEffectType.COMBO
                                });
                                break;
                            case eComboInterporationType.CURVE:
                                parameterOnPrefabDetail.ExecTime.Add(new ActionExecTime
                                {
                                    Time = actionExecTimeCombo.StartTime + actionExecTimeCombo.Curve.Evaluate(index3 / (float)actionExecTimeCombo.Count) * actionExecTimeCombo.Count * actionExecTimeCombo.OffsetTime,
                                    DamageNumType = eDamageEffectType.COMBO
                                });
                                break;
                        }
                    }
                }
                parameterOnPrefabDetail.ExecTime.Sort((a, b) => a.Time.CompareTo(b.Time));
            }
            actionParameter.ActionExecTimeList = parameterOnPrefabDetail.ExecTime;
            actionParameter.ExecTime = new float[parameterOnPrefabDetail.ExecTime.Count];
            actionParameter.KnockAnimationCurve = parameterOnPrefab.KnockAnimationCurve;
            actionParameter.KnockDownAnimationCurve = parameterOnPrefab.KnockDownAnimationCurve;
            actionParameter.EffectType = parameterOnPrefab.EffectType;
            actionParameter.ActionEffectList = parameterOnPrefabDetail.ActionEffectList;
            int index4 = 0;
            for (int count = parameterOnPrefabDetail.ExecTime.Count; index4 < count; ++index4)
            {
                actionParameter.ActionWeightSum += parameterOnPrefabDetail.ExecTime[index4].Weight;
                actionParameter.ExecTime[index4] = parameterOnPrefabDetail.ExecTime[index4].Time;
            }

            if (MainManager.Instance.execTimePatch.TryGetValue(actionParam.action_id, out var time))
                actionParameter.ExecTime = time;

            actionParameter.DepenedActionId = actionParam.DependActionId;
            actionParameter.ActionId = actionParam.action_id;
            actionParameter.TargetList = new List<BasePartsData>();
            actionParameter.ActionChildrenIndexes = new List<int>();
            actionParameter.TargetAssignment = (eTargetAssignment)(int)actionParam.target_assignment;
            actionParameter.Direction = (DirectionType)(int)actionParam.target_area;
            skill.ActionParameters.Add(actionParameter);
            skill.SkillId = skillParameter.skill_id;
        }

        public bool StartAction(int skillId)
        {
            if (!skillDictionary.ContainsKey(skillId))
            {
                MainManager.Instance.WindowConfigMessage("角色" + Owner.UnitId + "的技能" + skillId + "错误！", null);
                return false;
            }
            Skill skill = skillDictionary[skillId];
            if (skill.UnionBurstCoolDownTime > 0f)
            {
                Owner.UnionBurstCoolDownTime = skill.UnionBurstCoolDownTime;
            }
            skill.DefeatEnemyCount = 0;
            skill.DefeatByThisSkill = false;
            skill.AlreadyAddAttackSelfSeal = false;
            skill.AlreadyExexActionByHit = false;
            skill.LifeSteal = 0;
            skill.Cancel = false;
            skill.EffectBranchId = 0;
            skill.LoopEffectAlreadyDone = false;
            ContinuousActionEndDone = false;
            skill.OwnerReturnPosition = transform.localPosition;
            skill.CountBlind = false;
            skill.AbsorberValue = battleManager.KIHOGJBONDH;
            if (skill.HasAttack && skill.IsLifeStealEnabled)
            {
                for (int index = Owner.LifeStealQueueList.Count - 1; index >= 0; --index)
                {
                    skill.LifeSteal += Owner.LifeStealQueueList[index].Dequeue();
                    if (Owner.LifeStealQueueList[index].Count == 0)
                    {
                        Owner.LifeStealQueueList.RemoveAt(index);
                        Owner.OnChangeState.Call(Owner, eStateIconType.BUFF_ADD_LIFE_STEAL, false);
                        Owner.MyOnChangeAbnormalState?.Invoke(Owner, eStateIconType.BUFF_ADD_LIFE_STEAL, false, 90, "1次");

                    }
                }
            }
            bool flag1 = true;
            List<int> hasParentIndexes = skill.HasParentIndexes;
            for (int index1 = 0; index1 < skill.ActionParameters.Count; ++index1)
            {
                ActionParameter actionParameter1 = skill.ActionParameters[index1];
                actionParameter1.IdOffsetDictionary = new Dictionary<BasePartsData, long>();
                actionParameter1.CancelByIfForAll = false;
                actionParameter1.AdditionalValue = null;
                actionParameter1.MultipleValue = null;
                actionParameter1.DivideValue = null;
                if (!actionParameter1.ReferencedByReflection)
                {
                    if (!actionParameter1.IsSearchAndSorted)
                        searchAndSortTarget(skill, actionParameter1, transform.position);
                    actionParameter1.IsSearchAndSorted = false;
                    if (actionParameter1.ActionType == eActionType.REFLEXIVE)
                    {
                        Vector3 _basePosition = new Vector3();
                        bool _considerBodyWidth = true;
                        float num = 0.0f;
                        switch (actionParameter1.ActionDetail1)
                        {
                            case 1:
                                if (actionParameter1.TargetList.Count != 0)
                                {
                                    _basePosition = actionParameter1.TargetList[0].GetPosition();
                                    break;
                                }
                                continue;
                            case 2:
                                _basePosition = transform.position + new Vector3((float)((Owner.IsLeftDir ? -1.0 : 1.0) * (double)actionParameter1.Value[eValueNumber.VALUE_1] / 540.0), 0.0f);
                                break;
                            case 3:
                                _basePosition = transform.position;
                                num = (Owner.IsLeftDir ? -1f : 1f) * actionParameter1.Value[eValueNumber.VALUE_1];
                                break;
                            case 4:
                                if (actionParameter1.TargetList.Count != 0)
                                {
                                    _basePosition = actionParameter1.TargetList[0].GetPosition();
                                    _considerBodyWidth = false;
                                    break;
                                }
                                continue;
                        }
                        int index2 = 0;
                        for (int count = actionParameter1.ActionChildrenIndexes.Count; index2 < count; ++index2)
                        {
                            ActionParameter actionParameter2 = skill.ActionParameters[actionParameter1.ActionChildrenIndexes[index2]];
                            actionParameter2.Position = num;
                            searchAndSortTarget(skill, actionParameter2, _basePosition, _considerBodyWidth: _considerBodyWidth);
                        }
                    }
                    if (actionParameter1.TargetList.Count != 0)
                        flag1 = false;
                }
            }
            bool flag2 = false;
            if (skill.BlackOutTime > 0.0)
            {
                for (int index1 = 0; index1 < skill.ActionParameters.Count; ++index1)
                {
                    ActionParameter actionParameter = skill.ActionParameters[index1];
                    if (!hasParentIndexes.Contains(index1) || actionParameter.ReferencedByReflection)
                    {
                        int index2 = 0;
                        for (int count = actionParameter.TargetList.Count; index2 < count && (index2 < actionParameter.TargetNum || actionParameter.TargetNum == 0 || index2 == 0); ++index2)
                        {
                            UnitCtrl owner = actionParameter.TargetList[index2].Owner;
                            if (actionParameter.TargetAssignment == eTargetAssignment.OTHER_SIDE)
                                owner.IsScaleChangeTarget = true;
                            battleManager.AddBlackOutTarget(Owner, owner, actionParameter.TargetList[index2]);
                            if (Owner != owner && skill.DisableOtherCharaOnStart)
                                owner.GetCurrentSpineCtrl().CurColor = new Color(1f, 1f, 1f, 0.0f);
                        }
                    }
                }
                battleManager.StartChangeScale(skill, skill.CutInSkipTime);
            }
            if (skillId == Owner.UnionBurstSkillId)
            {
                battleManager.SetSkillExeScreen(Owner, skill.BlackOutTime, skill.BlackoutColor, skill.BlackoutEndWithMotion || skill.IsPrincessForm);
                //this.battleTimeScale.StopSlowEffect();
                //if (skill.SlowEffect.Enable)
                //this.battleTimeScale.StartSlowEffect(skill.SlowEffect, this.Owner, skill.CutInSkipTime, false);
                if (Owner.IsOther == battleManager.IsDefenceReplayMode)
                    flag2 = true;
            }
            bool flag3 = flag2 || Owner.IsBoss;
            //if (flag3)
            //SingletonMonoBehaviour<BattleHeaderController>.Instance.SkillWindow.Indicate(skill.SkillName, this.Owner.IsBoss);
            BattleSpineController currentSpineCtrl = Owner.GetCurrentSpineCtrl();
            if (skill.ForcePlayNoTarget)
                flag1 = false;
            // fix that boss silense it self causing attack action not working
            if (flag1 || this.Owner.IsAbnormalState(UnitCtrl.eAbnormalState.SILENCE) && !this.Owner.AttackWhenSilence && !Owner.IsBoss)
            if (flag1 || this.Owner.IsAbnormalState(UnitCtrl.eAbnormalState.SILENCE) && !this.Owner.AttackWhenSilence)
            if (flag1 || Owner.IsAbnormalState(UnitCtrl.eAbnormalState.SILENCE) && !Owner.AttackWhenSilence)
            {
                if (Owner.IsBoss && !Owner.IsConfusionOrConvert() && (!skill.IsPrincessForm && currentSpineCtrl.IsAnimation(skill.AnimId, skill.SkillNum, _index3: Owner.MotionPrefix)))
                {
                    Owner.PlayAnime(skill.AnimId, skill.SkillNum, _index3: Owner.MotionPrefix, _isLoop: false);
                    CreateNormalPrefabWithTargetMotion(skill, 0, true);
                }
                return false;
            }
            if (!flag3)
                Owner.IndicateSkillName(skill.SkillName);
            for (int index = 0; index < skill.ShakeEffects.Count; ++index)
            {
              if (skill.ShakeEffects[index].TargetMotion == 0)
                this.AppendCoroutine(this.StartShakeWithDelay(skill.ShakeEffects[index], skill, true), ePauseType.VISUAL, (double) skill.BlackOutTime > 0.0 ? this.Owner : (UnitCtrl) null);
            }
            //this.startBlurEffects(skill, true);
            if (skill.AnimId != eSpineCharacterAnimeId.NONE && !skill.IsPrincessForm && currentSpineCtrl.IsAnimation(skill.AnimId, skill.SkillNum, _index3: Owner.MotionPrefix))
            {
                if (Owner.IsCutInSkip)
                {
                    Owner.IsCutInSkip = false;
                    Owner.RestartPlayAnimeCoroutine(skill.CutInSkipTime, skill.AnimId, skill.SkillNum, Owner.MotionPrefix);
                }
                else
                    Owner.PlayAnime(skill.AnimId, skill.SkillNum, _index3: Owner.MotionPrefix, _isLoop: false, _startTime: skill.CutInSkipTime);
            }
            skill.ReadySkill();
            skill.AweValue = Owner.CalcAweValue(skillId == Owner.UnionBurstSkillId, skillId == 1);
            bool effectFlag = false;
            for (int index = 0; index < skill.ActionParameters.Count; ++index)
            {
                ActionParameter actionParameter = skill.ActionParameters[index];
                actionParameter.ReadyAction(Owner, this, skill);
                //if ((!hasParentIndexes.Contains(index) || actionParameter.ReferencedByReflection) && !actionParameter.ReferencedByEffect)
                //    this.ExecUnitActionWithDelay(actionParameter, skill, true, true);
                if (!hasParentIndexes.Contains(index) || actionParameter.ReferencedByReflection)
                {
                    if (!actionParameter.ReferencedByEffect)
                    {
                        ExecUnitActionWithDelay(actionParameter, skill, true, true);
                    }
                    else
                    {
                        if (UseSkillEffect)
                        {
                            effectFlag = true;
                            //CalcDelayByPhysice2(actionParameter, skill, 0, true);
                            //this.ExecUnitActionWithDelay(actionParameter, skill, true, true);
                        }
                        else
                        {
                            CalcDelayByPhysice(actionParameter, skill, 0, true);
                            ExecUnitActionWithDelay(actionParameter, skill, true, true);
                        }
                    }
                }

            }
            foreach (var unit in battleManager.BlackoutUnitTargetList)
                unit.DisableSortOrderFrontOnBlackoutTarget = false;
            if(UseSkillEffect && effectFlag)
                CreateNormalPrefabWithTargetMotion2(skill, 0, true);
            //if (skill.ZoomEffect.Enable)
            // this.battleCameraEffect.StartZoomEffect(skill.ZoomEffect, this.Owner, skill.CutInSkipTime, false, false, skill.SkillId != this.Owner.UnionBurstSkillId);
            Owner.StartChangeSortOrder(skill.ChangeSortDatas, skill.CutInSkipTime);
            //added Scripts
            UnitSkillExecData skillExecData = new UnitSkillExecData();
            skillExecData.skillID = skillId;
            skillExecData.skillName = skill.SkillName;
            skillExecData.skillState = UnitSkillExecData.SkillState.NORMAL;
            skillExecData.startTime = BattleHeaderController.CurrentFrameCount;
            skillExecData.unitid = Owner.UnitId;
            skillExecData.UnitName = Owner.UnitName;
            if (skillId == Owner.UnionBurstSkillId)
            {
                skillExecData.energy = Owner.lastEnergyBeforeUB;
                var e = Owner.lastEnergyBeforeUB;
                GuildCalculator.Instance.dmglist.Add(new ProbEvent
                {
                    unit = Owner.UnitNameEx,
                    predict = hash => e.Emulate(hash) < 1000f,
                    exp = hash => $"{e.ToExpression(hash)} < 1000",
                    description = $"({BattleHeaderController.CurrentFrameCount}){Owner.UnitNameEx}的UB没开出"
                });
            }
            else
            {
                skillExecData.energy = Owner.Energy;
            }
            Owner.MyOnStartAction?.Invoke(Owner.UnitId, skillExecData);
            Owner.AppendStartSkill(skillId);
            //end added Scripts
            Owner.lastCritPoint = new UnitCtrl.CritPoint
            {
                description = $"{skill.SkillName}唱名",
                description2 = $"{skill.SkillName}唱名",
                priority = UnitCtrl.eCritPointPriority.StartSkill,
            };
            return true;
        }
        private void searchAndSortTarget(
          Skill _skill,
          ActionParameter _action,
          Vector3 _basePosition,
          bool _quiet = false,
          bool _considerBodyWidth = true)
        {
            searchTargetUnit(_action, _basePosition, _skill, _considerBodyWidth);
            sortTargetListByTargetPattern(_action, Owner.BottomTransform, _action.Value[eValueNumber.VALUE_1], _quiet, _skill);
            bool flag = false;
            for (int index = 0; index < _action.TargetList.Count - _action.TargetNth; ++index)
            {
                flag = true;
                _action.TargetList[index] = _action.TargetList[_action.TargetNth + index];
            }
            if (flag)
            {
                int count = _action.TargetList.Count;
                for (int index = count - 1; index >= count - _action.TargetNth; --index)
                    _action.TargetList.RemoveAt(index);
            }
            if (!flag && _action.TargetNth != 0 && _action.TargetList.Count != 0)
            {
                BasePartsData target = _action.TargetList[_action.TargetList.Count - 1];
                _action.TargetList.Clear();
                _action.TargetList.Add(target);
            }
            BasePartsData basePartsData = null;
            if (_action.TargetAssignment == eTargetAssignment.OTHER_SIDE && _action.TargetNum == 1 && !_action.IgnoreDecoy)
            {
                if (Owner.IsOther)
                {
                    if (Owner.IsConfusionOrConvert())
                    {
                        if (battleManager.DecoyEnemy != null)
                            basePartsData = battleManager.DecoyEnemy.GetFirstParts(_basePos: Owner.transform.position.x);
                    }
                    else if (battleManager.DecoyUnit != null)
                        basePartsData = battleManager.DecoyUnit.GetFirstParts(_basePos: Owner.transform.position.x);
                }
                else if (Owner.IsConfusionOrConvert())
                {
                    if (battleManager.DecoyUnit != null)
                        basePartsData = battleManager.DecoyUnit.GetFirstParts(_basePos: Owner.transform.position.x);
                }
                else if (battleManager.DecoyEnemy != null)
                    basePartsData = battleManager.DecoyEnemy.GetFirstParts(_basePos: Owner.transform.position.x);
                //2022/09/05改
                if (basePartsData != null && !basePartsData.Owner.IsDead && !basePartsData.Owner.IsAbnormalState(UnitCtrl.eAbnormalState.SPY) && isInActionTargetArea(_skill, _action, _basePosition, _considerBodyWidth, basePartsData))
                {
                    //if ((double)Mathf.Abs(basePartsData.GetLocalPosition().x - this.transform.localPosition.x) <= (double)_action.TargetWidth + (double)basePartsData.GetBodyWidth() / 2.0 + (double)this.Owner.BodyWidth / 2.0)
                    //if (Mathf.Abs(basePartsData.GetLocalPosition().x - transform.localPosition.x) <= _action.TargetWidth + basePartsData.GetBodyWidth() / 2.0 + Owner.BodyWidth / 2.0)
                    //{
                        _action.TargetList.Clear();
                        _action.TargetList.Add(basePartsData);
                    //}
                }
            }
            _action.IsSearchAndSorted = true;
        }

        private void setCutInSkipTimeForPrincessForm(int _skillId)
        {
            Skill skill = skillDictionary[_skillId];
            if (!skill.IsPrincessForm)
                return;
            skill.CutInSkipTime = 0.0f;
        }

        public IEnumerator StartActionWithOutCutIn(
          UnitCtrl _unit,
          int _skillId,
          Action _callback)
        {
            //UnitActionController actionController = this;
            //battleTimeScale.StopSlowEffect();
            Skill skill = skillDictionary[_skillId];
            BattleSpineController unitSpineController = Owner.GetCurrentSpineCtrl();
            TrackEntry entry = unitSpineController.state.GetCurrent(0);
            float deltaTimeAccumulated = 0.0f;
            //BattleHeaderController battleHeaderController = SingletonMonoBehaviour<BattleHeaderController>.Instance;
            battleManager.SetSkillExeScreenActive(_unit, Color.black);
            Owner.SetSortOrderFront();
            AddBlackoutTarget(_skillId);
            if (skill.IsPrincessForm)
            {
                yield return null;
                _callback.Call();
            }
            else
            {
                Owner.PlayAnime(skill.AnimId, skill.SkillNum, _index3: Owner.MotionPrefix, _isLoop: false);
                for (int index = 0; index < skill.SkillEffects.Count; ++index)
                {
                    NormalSkillEffect skillEffect = skill.SkillEffects[index];
                    if (skillEffect.TargetMotionIndex == 0)
                    {
                        skill.Cancel = false;
                        //StartCoroutine(updateCoroutineWithOutCutIn(createNormalPrefabWithDelay(skillEffect, skill, _skipCutIn: true)));
                    }
                }
                //if (skill.ZoomEffect.Enable)
                //  battleCameraEffect.StartZoomEffect(skill.ZoomEffect, Owner, 0.0f, true, false, false);
                if (skill.SkillId == Owner.UnionBurstSkillId)
                    Owner.StartChangeSortOrder(skill.ChangeSortDatas, 0.0f);
                while (!battleManager.CoroutineManager.VisualPause)
                    yield return null;
                this.Timer(() => battleManager.SetSkillExeScreenActive(_unit, skill.BlackoutColor));
                Owner.GetCurrentSpineCtrl().state.TimeScale = 1f;
                Owner.IsCutInSkip = true;
                entry = unitSpineController.state.GetCurrent(0);
                while (true)
                {
                    if (!BattleHeaderController.Instance.IsPaused)
                        deltaTimeAccumulated += Time.deltaTime;
                    for (; deltaTimeAccumulated > 0.0; deltaTimeAccumulated -= battleManager.DeltaTime_60fps)
                    {
                        if (!battleManager.BlackoutUnitTargetList.Contains(Owner))
                        {
                            unitSpineController.RealUpdate();
                            unitSpineController.RealLateUpdate();
                        }
                        foreach (var unit in battleManager.BlackoutUnitTargetList)
                        {
                            var spine = unit.GetCurrentSpineCtrl();
                            spine.RealUpdate(); spine.RealLateUpdate();
                        }
                        foreach (var ctrl in Owner.EffectSpineControllerList)
                        {
                            ctrl.RealUpdate();
                            ctrl.RealLateUpdate();
                        }
                    }
                    if (entry.TrackTime < (double)skill.CutInSkipTime)//TrackTime为修改过的
                        yield return null;
                    else
                        break;
                }
                Owner.EffectSpineControllerList.Clear();
                _callback.Call();
            }
        }
        /*
        public IEnumerator StartAnnihilationSkillAnimation(int _annihilationId)
        {
            //UnitActionController actionController = this;
            Owner.Pause = false;
            //battleTimeScale.StopSlowEffect();
            Skill annihilation = Annihilation;
            BattleSpineController unitSpineController = Owner.GetCurrentSpineCtrl();
            //BattleHeaderController instance = SingletonMonoBehaviour<BattleHeaderController>.Instance;
            unitSpineController.AnimationName = unitSpineController.ConvertAnimeIdToAnimeName(eSpineCharacterAnimeId.ANNIHILATION, _index3: Owner.MotionPrefix);
            for (int index = 0; index < annihilation.SkillEffects.Count; ++index)
            {
                NormalSkillEffect skillEffect = annihilation.SkillEffects[index];
                if (skillEffect.TargetMotionIndex == 0)
                {
                    annihilation.Cancel = false;
                    //StartCoroutine(updateCoroutineWithOutCutIn(createNormalPrefabWithDelay(skillEffect, annihilation)));
                }
            }
            battleCameraEffect.ClearShake();
            if (annihilation.ZoomEffect.Enable)
            {
              ++Owner.UbCounter;
              battleCameraEffect.StartZoomEffect(annihilation.ZoomEffect, Owner, 0.0f, true, true);
            }
            foreach (ShakeEffect shakeEffect in annihilation.ShakeEffects)
              Owner.StartCoroutine(updateCoroutineWithOutCutIn(StartShakeWithDelay(shakeEffect, annihilation)));
            //if (annihilation.SlowEffect.Enable)
            //battleTimeScale.StartSlowEffect(annihilation.SlowEffect, Owner, 0.0f, true);
            if (annihilation.BlackOutTime > 0.0)
            {
                battleManager.SetForegroundEnable(false);
                StartCoroutine(updateCoroutineWithOutCutIn(foregroundActiveWithDelay(annihilation.BlackOutTime)));
            }
            Owner.GetCurrentSpineCtrl().state.TimeScale = 1f;
            while (_annihilationId == Owner.AnnihilationId)
            {
                for (float num = 0.0f + Time.deltaTime; num > 0.0; num -= battleManager.DeltaTime_60fps)
                {
                    battleCameraEffect.UpdateShake();
                    if (!unitSpineController.IsPlayAnime)
                    {
                        unitSpineController.AnimationName = unitSpineController.ConvertAnimeIdToAnimeName(eSpineCharacterAnimeId.IDLE);
                        unitSpineController.loop = true;
                    }
                }
                yield return null;
            }
        }*/

        private IEnumerator foregroundActiveWithDelay(float _time)
        {
            yield return new WaitForSeconds(_time);
            battleManager.SetForegroundEnable(true);
        }

        public void AddBlackoutTarget(int _skillId)
        {
            Skill skill = skillDictionary[_skillId];
            int index1 = 0;
            for (int count1 = skill.ActionParameters.Count; index1 < count1; ++index1)
            {
                ActionParameter actionParameter1 = skill.ActionParameters[index1];
                if (!actionParameter1.ReferencedByReflection)
                {
                    searchAndSortTarget(skill, actionParameter1, transform.position, true);
                    for (int index2 = 0; index2 < actionParameter1.TargetNum; ++index2)
                    {
                        if (actionParameter1.TargetList.Count > index2)
                        {
                            UnitCtrl owner1 = actionParameter1.TargetList[index2].Owner;
                            if (actionParameter1.TargetAssignment == eTargetAssignment.OTHER_SIDE)
                                owner1.IsScaleChangeTarget = true;
                            battleManager.AddBlackOutTarget(Owner, owner1, actionParameter1.TargetList[index2]);
                            if (actionParameter1.ActionType == eActionType.REFLEXIVE)
                            {
                                Vector3 _basePosition = new Vector3();
                                bool _considerBodyWidth = true;
                                switch (actionParameter1.ActionDetail1)
                                {
                                    case 1:
                                        _basePosition = actionParameter1.TargetList[0].GetPosition();
                                        break;
                                    case 2:
                                        _basePosition = transform.position + new Vector3((float)((Owner.IsLeftDir ? -1.0 : 1.0) * (double)actionParameter1.Value[eValueNumber.VALUE_1] / 540.0), 0.0f);
                                        break;
                                    case 3:
                                        _basePosition = transform.position;
                                        break;
                                    case 4:
                                        _basePosition = actionParameter1.TargetList[0].GetPosition();
                                        _considerBodyWidth = false;
                                        break;
                                }
                                int index3 = 0;
                                for (int count2 = actionParameter1.ActionChildrenIndexes.Count; index3 < count2; ++index3)
                                {
                                    ActionParameter actionParameter2 = skill.ActionParameters[actionParameter1.ActionChildrenIndexes[index3]];
                                    searchAndSortTarget(skill, actionParameter2, _basePosition, _considerBodyWidth: _considerBodyWidth);
                                    for (int index4 = 0; index4 < actionParameter2.TargetNum && index4 != actionParameter2.TargetList.Count; ++index4)
                                    {
                                        UnitCtrl owner2 = actionParameter2.TargetList[index4].Owner;
                                        if (actionParameter1.TargetAssignment == eTargetAssignment.OTHER_SIDE)
                                            owner2.IsScaleChangeTarget = true;
                                        battleManager.AddBlackOutTarget(Owner, owner2, actionParameter2.TargetList[index4]);
                                    }
                                }
                            }
                        }
                    }
                    battleManager.StartChangeScale(skill, 0.0f);
                }
            }
        }

        public void SearchTargetByAction(int _skillId)
        {
            Skill skill = skillDictionary[_skillId];
            int index1 = 0;
            for (int count1 = skill.ActionParameters.Count; index1 < count1; ++index1)
            {
                ActionParameter actionParameter1 = skill.ActionParameters[index1];
                if (!actionParameter1.ReferencedByReflection)
                {
                    searchAndSortTarget(skill, actionParameter1, transform.position, true);
                    actionParameter1.IsSearchAndSorted = false;
                    for (int index2 = 0; index2 < actionParameter1.TargetNum; ++index2)
                    {
                        if (actionParameter1.TargetList.Count > index2 && actionParameter1.ActionType == eActionType.REFLEXIVE)
                        {
                            Vector3 _basePosition = new Vector3();
                            bool _considerBodyWidth = true;
                            switch (actionParameter1.ActionDetail1)
                            {
                                case 1:
                                    _basePosition = actionParameter1.TargetList[0].GetPosition();
                                    break;
                                case 2:
                                    _basePosition = transform.position + new Vector3((float)((Owner.IsLeftDir ? -1.0 : 1.0) * (double)actionParameter1.Value[eValueNumber.VALUE_1] / 540.0), 0.0f);
                                    break;
                                case 3:
                                    _basePosition = transform.position;
                                    break;
                                case 4:
                                    _basePosition = actionParameter1.TargetList[0].GetPosition();
                                    _considerBodyWidth = false;
                                    break;
                            }
                            int index3 = 0;
                            for (int count2 = actionParameter1.ActionChildrenIndexes.Count; index3 < count2; ++index3)
                            {
                                ActionParameter actionParameter2 = skill.ActionParameters[actionParameter1.ActionChildrenIndexes[index3]];
                                searchAndSortTarget(skill, actionParameter2, _basePosition, _considerBodyWidth: _considerBodyWidth);
                                actionParameter2.IsSearchAndSorted = false;
                            }
                        }
                    }
                }
            }
        }

        private IEnumerator updateCoroutineWithOutCutIn(IEnumerator _coroutine)
        {
            float deltaTimeAccumulated = 0.0f;
        label_2:
            yield return null;
            //if (!SingletonMonoBehaviour<BattleHeaderController>.Instance.IsPaused)
            deltaTimeAccumulated += Time.deltaTime;
            for (; deltaTimeAccumulated > 0.0; deltaTimeAccumulated -= battleManager.DeltaTime_60fps)
            {
                if (!_coroutine.MoveNext())
                    yield break;
            }
            goto label_2;
        }

        public void CreateNormalPrefabWithTargetMotion(
          Skill _skill,
          int _targetmotion,
          bool _first,
          bool _useStartCoroutine = false,
          bool _modechangeEndEffect = false)
        {
            CreateNormalPrefabWithTargetMotion2(_skill, _targetmotion, _first, _useStartCoroutine, _modechangeEndEffect);
            return;
            int index = 0;
            for (int count = _skill.SkillEffects.Count; index < count; ++index)
            {
                NormalSkillEffect skillEffect = _skill.SkillEffects[index];
                if (skillEffect.TargetMotionIndex == _targetmotion)
                {
                    bool flag = skillEffect.EffectBehavior == eEffectBehavior.NORMAL || skillEffect.EffectBehavior == eEffectBehavior.SKILL_AREA_RANDOM || skillEffect.EffectBehavior == eEffectBehavior.TARGET_CENTER || skillEffect.EffectBehavior == eEffectBehavior.WORLD_POS_CENTER;
                    if (_useStartCoroutine)
                    {
                        battleManager.StartCoroutine(createNormalPrefabWithDelay(skillEffect, _skill, _first));
                    }
                    else
                    {
                        ePauseType pauseType = flag ? ePauseType.VISUAL : ePauseType.SYSTEM;
                        UnitCtrl unit = _skill.BlackOutTime > 0.0 ? Owner : null;
                        AppendCoroutine(createNormalPrefabWithDelay(skillEffect, _skill, _first, _isFirearmEndEffect: _modechangeEndEffect, _modeChangeEndEffect: _modechangeEndEffect), pauseType, unit);
                    }
                }
            }
        }

        public void ExecUnitActionWithDelay(
          ActionParameter _action,
          Skill _skill,
          bool _first,
          bool _boneCount,
          bool _ignoreCancel = false)
        {
            if (_action == null|| _action.TargetList == null)
                return;
            if (_action.TargetList.Count == 0)
            {
                initWhenNoTarget(_action, _skill);
            }
            if (_action.ActionType == eActionType.CONTINUOUS_ATTACK)
            {
                //if (this.Owner.GetCurrentSpineCtrl().IsAnimation(_skill.AnimId, _skill.SkillNum, 1))
                //  this.AppendCoroutine((_action as ContinuousAttackAction).UpdateMotionRoopForContinuousDamage(_skill, this.Owner, this), ePauseType.SYSTEM, (double) _skill.BlackOutTime > 0.0 ? this.Owner : (UnitCtrl) null);
                // _action.ContinuousTargetCount = 0;
            }
            for (int index = 0; index < _action.TargetList.Count && (index < _action.TargetNum || _action.TargetNum == 0 || index == 0); ++index)
            {
                BasePartsData target = _action.TargetList[index];
                AppendCoroutine(ExecActionWithDelayAndTarget(_action, _skill, target, 0.0f, _first, _boneCount, _ignoreCancel), ePauseType.SYSTEM, _skill.BlackOutTime > 0.0 ? Owner : null);
            }
        }

        public void ExecUnitActionNoDelay(ActionParameter _action, Skill _skill)
        {
            if (_action.TargetList.Count == 0)
            {
                initWhenNoTarget(_action, _skill);
            }
            for (int index = 0; index < _action.TargetList.Count && (index < _action.TargetNum || _action.TargetNum == 0 || index == 0); ++index)
            {
                BasePartsData target = _action.TargetList[index];
                ExecAction(_action, _skill, target, 0, 0.0f);
            }
        }

        public IEnumerator ExecActionWithDelayAndTarget(
          ActionParameter _action,
          Skill _skill,
          BasePartsData _target,
          float _starttime,
          bool _first = false,
          bool _boneCount = true,
          bool _ignoreCancel = false)
        {
            // temporary fix for log barrier skill
            if (_action is LogBarrierAction)
                _action.ExecTime = new[] { 0f };
            for (int _num = 0; _num < _action.ExecTime.Length; ++_num)
            {
                if (_boneCount)
                    _action.AppendTargetNum(_target.Owner, _num);
            }
            float time = _first ? _starttime + _skill.CutInSkipTime : _starttime;
            long[] ids = new long[_action.ExecTime.Length];
            for (int i = 0; i < _action.ExecTime.Length; ++i)
            {
                ids[i] = 0L;
                long int64 = Convert.ToInt64(_action.ActionId);
                while (battleManager.PBCLBKCKHAI.Contains(int64 * 100L + ids[i]))
                    ++ids[i];
                long actionIndivisualId = int64 * 100L + ids[i];
                _target.Owner.ActionsTargetOnMe.Add(actionIndivisualId);
                battleManager.PBCLBKCKHAI.Add(actionIndivisualId);
                float waitTime = _action.ExecTime[i];
                while (true)
                {
                    time += battleManager.DeltaTime_60fps;
                    if (_ignoreCancel || !_skill.Cancel && !_action.CancelByIfForAll)
                    {
                        if (waitTime > (double)time && (_action.ActionType != eActionType.TRIGER || _action.ActionDetail1 != 4))
                            yield return null;
                        else
                            goto label_17;
                    }
                    else
                        break;
                }
                if (_action.OnActionEnd != null)
                    _action.OnActionEnd();
                _target.Owner.ActionsTargetOnMe.Remove(actionIndivisualId);
                battleManager.CallbackActionEnd(actionIndivisualId);
                break;
            label_17:
                ExecAction(_action, _skill, _target, i, time);
                if ((_action.ActionType != eActionType.CONTINUOUS_ATTACK || _action.ActionDetail1 != 3) && (_action.ActionType != eActionType.MODE_CHANGE || _action.ActionDetail1 == 3))
                {
                    _target.Owner.ActionsTargetOnMe.Remove(actionIndivisualId);
                    battleManager.CallbackActionEnd(actionIndivisualId);
                }
                else if (_action.IdOffsetDictionary.ContainsKey(_target))
                    _action.IdOffsetDictionary[_target] = ids[i];
                else
                    _action.IdOffsetDictionary.Add(_target, ids[i]);
                if (i == _action.ExecTime.Length - 1 && _action.OnActionEnd != null && (_action.ActionType != eActionType.CONTINUOUS_ATTACK && _action.ActionType != eActionType.MOVE))
                    _action.OnActionEnd();
            }
        }

        public void ExecAction(
          ActionParameter action,
          Skill skill,
          BasePartsData target,
          int num,
          float starttime)
        {
            Dictionary<eValueNumber, FloatWithEx> additionalValue = action.AdditionalValue;
            Dictionary<eValueNumber, FloatWithEx> multipleValue = action.MultipleValue;
            Dictionary<eValueNumber, FloatWithEx> divideValue = action.DivideValue;
            Dictionary<int, bool> dictionary = new Dictionary<int, bool>();
            Func<ActionParameter, eValueNumber, FloatWithEx> func = (_action, _type) =>
            {
                FloatWithEx num1 = 0.0f;
                if (additionalValue != null && additionalValue.ContainsKey(_type))
                    num1 = additionalValue[_type];
                FloatWithEx num2 = 1f;
                if (multipleValue != null && multipleValue.ContainsKey(_type))
                    num2 = multipleValue[_type];
                FloatWithEx num3 = 1f;
                if (divideValue != null && divideValue.ContainsKey(_type) && (double)divideValue[_type] != 0.0)
                    num3 = divideValue[_type];
                FloatWithEx num4 = 0.0f;
                _action.Value.TryGetValue(_type, out num4);
                return (num1 + num4) * num2 / num3;
            };
            Dictionary<eValueNumber, FloatWithEx> _valueDictionary = new Dictionary<eValueNumber, FloatWithEx>(new eValueNumber_DictComparer())
      {
        {
          eValueNumber.VALUE_1,
          func(action, eValueNumber.VALUE_1)
        },
        {
          eValueNumber.VALUE_2,
          func(action, eValueNumber.VALUE_2)
        },
        {
          eValueNumber.VALUE_3,
          func(action, eValueNumber.VALUE_3)
        },
        {
          eValueNumber.VALUE_4,
          func(action, eValueNumber.VALUE_4)
        },
        {
          eValueNumber.VALUE_5,
          func(action, eValueNumber.VALUE_5)
        },
        {
          eValueNumber.VALUE_6,
          func(action, eValueNumber.VALUE_6)
        },
        {
          eValueNumber.VALUE_7,
          func(action, eValueNumber.VALUE_7)
        }
      };
            bool _last = action.JudgeLastAndNotExeced(target.Owner, num);
            bool _targetOneParts = action.AlreadyExecedData[target.Owner][num].TargetPartsNumber == 1;
            /*if (!target.ResistStatus(action.ActionType, action.ActionDetail1, this.Owner, _last, _targetOneParts, target) && action.JudgeIsAlreadyExeced(target.Owner, num))
            {
                int index = skill.ActionParameters.FindIndex(a => a == action) + 1;
                int index2 = skill.ActionParameters.Count;
                Owner.UIManager.LogMessage("执行技能" + skill.SkillName + "(" + index + "/" + index2 + ")" + ",目标" + target.Owner.UnitName, PCRCaculator.Battle.eLogMessageType.EXEC_ACTION, this.Owner);
                action.ExecAction(this.Owner, target, num, this, skill, starttime, dictionary, _valueDictionary);
            }*/
            //change start
            bool isResisted = target.ResistStatus(action.ActionType, action.ActionDetail1, Owner, _last, _targetOneParts, target);
            bool exec = !isResisted && action.JudgeIsAlreadyExeced(target.Owner, num);
            UnitActionExecData actionExecData = new UnitActionExecData();
            actionExecData.execTime = BattleHeaderController.CurrentFrameCount;
            actionExecData.describe = action.sortinfo;//"执行失败，原因：" + (isResisted?"<color=#FF0000>被抵抗</color>": "<color=#FF0000>已经执行过了</color>");
            actionExecData.actionID = action.ActionId;
            actionExecData.unitid = Owner.UnitId;
            actionExecData.actionType = action.ActionType.GetDescription();
            actionExecData.targetNames = new List<string> { target.Owner.UnitName };
            if (exec)
            {
                int index = skill.ActionParameters.FindIndex(a => a == action) + 1;
                int index2 = skill.ActionParameters.Count;
                Owner.UIManager.LogMessage("执行技能" + skill.SkillName + "(" + index + "/" + index2 + ")" + ",目标" + target.Owner.UnitName, eLogMessageType.EXEC_ACTION, Owner);

                action.PreExecAction();
                action.ExecAction(Owner, target, num, this, skill, starttime, dictionary, 
                    _valueDictionary,a=> 
                { 
                    actionExecData.describe += a;
                    //if (b > 0)
                    //    actionExecData.damageList.Add(b);
                });
            }
            else
            {
                actionExecData.describe = "执行失败，原因：" + (isResisted?"<color=#FF0000>被抵抗</color>": "<color=#FF0000>已经执行过了</color>");
            }
            actionExecData.result = isResisted ? UnitActionExecData.ActionState.MISS : UnitActionExecData.ActionState.NORMAL;
            Owner.MyOnExecAction?.Invoke(Owner.UnitId, skill.SkillId, actionExecData);
            var a = skill.ActionParameters.Count(ap => ap.ActionType == action.ActionType);
            var b = skill.ActionParameters.Where(ap => ap.ActionType == action.ActionType).TakeWhile(ap => ap != action).Count();
            Owner.lastCritPoint = new UnitCtrl.CritPoint
            {
                description = $"{skill.SkillName}的{(a == 1 ? "" : $"第{b}个")}{action.ActionType.GetDescription()}执行",
                description2 = (skill.SkillId == 1 ? Owner.UnitNameEx : string.Empty) + (string.IsNullOrEmpty(skill.SkillName) ? skill.SkillId.ToString() : skill.SkillName),
                priority = UnitCtrl.eCritPointPriority.ExecAction
            };
            //change end
            if (action.ActionType == eActionType.REFLEXIVE)
                return;
            if (!action.HitOnceDic.ContainsKey(target))
            {
                ExecChildrenAction(action, skill, target, num, starttime, dictionary);
            }
            else
            {
                if (num != action.ExecTime.Length - 1)
                    return;
                if (action.HitOnceDic[target])
                {
                    ExecChildrenAction(action, skill, target, num, starttime, dictionary);
                }
                else
                {
                    for (int index = 0; index < action.ActionChildrenIndexes.Count; ++index)
                    {
                        ActionParameter actionParameter = skill.ActionParameters[action.ActionChildrenIndexes[index]];
                        if (actionParameter.OnActionEnd != null)
                            actionParameter.OnActionEnd();
                    }
                }
            }
        }

        public void ExecChildrenAction(
          ActionParameter action,
          Skill skill,
          BasePartsData target,
          int num,
          float starttime,
          Dictionary<int, bool> enabledChildAction)
        {
            int index = 0;
            for (int count = action.ActionChildrenIndexes.Count; index < count; ++index)
            {
                //int actionChildrenIndex = action.ActionChildrenIndexes[index];
                //if ((skill.ActionParameters[actionChildrenIndex].ActionType != eActionType.MODE_CHANGE || skill.ActionParameters[actionChildrenIndex].ActionDetail1 != 3) && (!enabledChildAction.ContainsKey(skill.ActionParameters[actionChildrenIndex].ActionId) || enabledChildAction[skill.ActionParameters[actionChildrenIndex].ActionId]) && (action.ActionType != eActionType.ATTACK || num == action.ExecTime.Length - 1))
                //    AppendCoroutine(ExecActionWithDelayAndTarget(skill.ActionParameters[actionChildrenIndex], skill, target, starttime), ePauseType.SYSTEM, skill.BlackOutTime > 0.0 ? Owner : null);
                ActionParameter actionParameter = skill.ActionParameters[action.ActionChildrenIndexes[index]];
                if ((actionParameter.ActionType == eActionType.MODE_CHANGE && actionParameter.ActionDetail1 == 3) || (enabledChildAction.ContainsKey(actionParameter.ActionId) && !enabledChildAction[actionParameter.ActionId]) || (action.ActionType == eActionType.ATTACK && num != action.ExecTime.Length - 1))
                {
                    continue;
                }
                if (actionParameter.TargetSort != PriorityPattern.PARENT_TARGET_PARTS)
                {
                    AppendCoroutine(ExecActionWithDelayAndTarget(actionParameter, skill, target, starttime), ePauseType.SYSTEM, (skill.BlackOutTime > 0f) ? Owner : null);
                }
                else
                {
                    if (!target.Owner.IsPartsBoss || actionParameter.ExecedMultiBossList.Contains(target.Owner))
                    {
                        continue;
                    }
                    foreach (PartsData item in target.Owner.BossPartsListForBattle)
                    {
                        if (item.GetTargetable())
                        {
                            AppendCoroutine(ExecActionWithDelayAndTarget(actionParameter, skill, item, starttime), ePauseType.SYSTEM, (skill.BlackOutTime > 0f) ? Owner : null);
                        }
                    }
                    actionParameter.ExecedMultiBossList.Add(target.Owner);
                }
            }
        }
        private void initWhenNoTarget(ActionParameter _action, Skill _skill)
        {
            _action.OnInitWhenNoTarget.Call();
            Queue<int> queue = new Queue<int>(_action.ActionChildrenIndexes);
            while (queue.Count > 0)
            {
                ActionParameter actionParameter = _skill.ActionParameters[queue.Dequeue()];
                actionParameter.OnInitWhenNoTarget.Call();
                for (int i = 0; i < actionParameter.ActionChildrenIndexes.Count; i++)
                {
                    queue.Enqueue(actionParameter.ActionChildrenIndexes[i]);
                }
            }
        }

        private IEnumerator createNormalPrefabWithDelay(
          NormalSkillEffect _skilleffect,
          Skill _skill,
          bool _first = false,
          bool _skipCutIn = false,
          bool _isFirearmEndEffect = false,
          bool _modeChangeEndEffect = false)
        {
            float time = _first ? _skill.CutInSkipTime : 0.0f;
            string key = Owner.IsLeftDir || Owner.IsForceLeftDirOrPartsBoss ? _skilleffect.PrefabLeft.name : _skilleffect.Prefab.name;
            if (battleManager.DAIFDPFABCM.ContainsKey(key))
            {
              GameObject gameObject = battleManager.DAIFDPFABCM[key];
              if (gameObject == null)
              {
                yield break;
              }

              battleManager.DAIFDPFABCM.Remove(key);
              Destroy(gameObject);
            }
            else if (_skilleffect.IsReaction)
              battleManager.DAIFDPFABCM.Add(key, null);
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
                        if (UseSkillEffect)
                        {
                            switch (_skilleffect.EffectTarget)
                            {
                                case eEffectTarget.OWNER:
                                    createNormalEffectPrefab(_skilleffect, _skill, Owner.GetFirstParts(true), _firearmEndTarget, true, startTime, _skipCutIn, execTimeIndex, _modeChangeEndEffect);
                                    continue;
                                case eEffectTarget.ALL_TARGET:
                                    switch (_skilleffect.EffectBehavior)
                                    {
                                        case eEffectBehavior.SERIES:
                                        case eEffectBehavior.SERIES_FIREARM:
                                            createNormalEffectPrefab(_skilleffect, _skill, _skilleffect.TargetAction.TargetList[execTimeIndex], _firearmEndTarget, true, startTime, _skipCutIn, execTimeIndex);
                                            continue;
                                        default:
                                            int index1 = 0;
                                            for (int count = _skilleffect.TargetAction.TargetList.Count; index1 < count && index1 < _skilleffect.TargetAction.TargetNum; ++index1)
                                                createNormalEffectPrefab(_skilleffect, _skill, _skilleffect.TargetAction.TargetList[index1], _firearmEndTarget, index1 == 0, startTime, _skipCutIn, execTimeIndex);
                                            continue;
                                    }
                                case eEffectTarget.FORWARD_TARGET:
                                case eEffectTarget.BACK_TARGET:
                                    bool flag2 = _skilleffect.EffectTarget == eEffectTarget.FORWARD_TARGET == !Owner.IsOther;
                                    List<BasePartsData> basePartsDataList2 = new List<BasePartsData>(_skilleffect.TargetAction.TargetList.GetRange(0, Mathf.Min(_skilleffect.TargetAction.TargetNum, _skilleffect.TargetAction.TargetList.Count)));
                                    basePartsDataList2.Sort((a, b) => a.GetPosition().x.CompareTo(b.GetPosition().x));
                                    if (basePartsDataList2.Count != 0)
                                    {
                                        createNormalEffectPrefab(_skilleffect, _skill, basePartsDataList2[flag2 ? 0 : basePartsDataList2.Count - 1], _firearmEndTarget, true, startTime, _skipCutIn, execTimeIndex);
                                    }
                                    continue;
                                case eEffectTarget.ALL_OTHER:
                                    List<UnitCtrl> unitCtrlList1 = Owner.IsOther ? battleManager.UnitList : battleManager.EnemyList;
                                    for (int index2 = 0; index2 < unitCtrlList1.Count; ++index2)
                                    {
                                        if ((long)unitCtrlList1[index2].Hp != 0L)
                                            createNormalEffectPrefab(_skilleffect, _skill, unitCtrlList1[index2].GetFirstParts(true), _firearmEndTarget, true, startTime, _skipCutIn, execTimeIndex);
                                    }
                                    continue;
                                case eEffectTarget.ALL_UNIT_EXCEPT_OWNER:
                                    List<UnitCtrl> unitCtrlList2 = Owner.IsOther ? battleManager.EnemyList : battleManager.UnitList;
                                    for (int index2 = 0; index2 < unitCtrlList2.Count; ++index2)
                                    {
                                        if (unitCtrlList2[index2] != Owner && !unitCtrlList2[index2].IsDead)
                                            createNormalEffectPrefab(_skilleffect, _skill, unitCtrlList2[index2].GetFirstParts(true), _firearmEndTarget, true, startTime, _skipCutIn, execTimeIndex);
                                    }
                                    continue;
                                default:
                                    continue;
                            }
                        }
                    }
                }
            }
        }

        private IEnumerator createNormalPrefabWithDelayAndTarget(
          NormalSkillEffect _skilleffect,
          Skill _skill,
          float _delay,
          BasePartsData _target,
          bool _first)
        {
          float time = _first ? _skill.CutInSkipTime : 0.0f;
          if (_delay >= (double) time)
          {
            while (true)
            {
              time += battleManager.DeltaTime_60fps;
              if (!_skill.Cancel)
              {
                if (_delay > (double) time)
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
              createNormalEffectPrefab(_skilleffect, _skill, Owner.GetFirstParts(true), null, false, 0.0f, false);
            else
              createNormalEffectPrefab(_skilleffect, _skill, _target, null, false, 0.0f, false);
          }
        }

        private void createNormalEffectPrefab(
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
          if (_skillEffect.EffectBehavior == eEffectBehavior.SERIES_FIREARM && !(_skillEffect.FireAction is AttackAction) && (!(_skillEffect.FireAction is RatioDamageAction) && !(_skillEffect.FireAction is UpperLimitAttackAction)) && (!(_skillEffect.FireAction is HealAction) && _skillEffect.AppendAndJudgeAlreadyExeced(_firearmEndTarget.Owner)) || (_skillEffect.TargetBranchId != 0 && _skillEffect.TargetBranchId != _skill.EffectBranchId || _skillEffect.TargetMotionIndex == 1 && _skill.LoopEffectAlreadyDone) || (_modeChangeEndEffect && StopModeChangeEndEffectCalled))
            return;
          GameObject prefab1 = null;
          SkillEffectCtrl prefab2 = createPrefab(_skillEffect, _skill, _target, ref prefab1);
          prefab2.InitializeSort();
          prefab2.SetStartTime(_starttime);
          //prefab2.PlaySe(this.Owner.SoundUnitId, this.Owner.IsLeftDir);
          prefab2.SetPossitionAppearanceType(_skillEffect, _target, Owner, _skill);
          prefab2.ExecAppendCoroutine(_skill.BlackOutTime > 0.0 ? Owner : null, prefab2 is AwakeUbNoneStopEffect);
          prefab2.SetTimeToDieByStartHp(Owner.StartHpPercent);
          if (_modeChangeEndEffect)
            Owner.ModeChangeEndEffectList.Add(prefab2);
          if (_skipCutIn)
            prefab2.OnAwakeWhenSkipCutIn();
          switch (_skillEffect.EffectBehavior)
          {
            case eEffectBehavior.FIREARM:
            case eEffectBehavior.SERIES_FIREARM:
              FirearmCtrl firearmCtrl = prefab2 as FirearmCtrl;
              List<ActionParameter> _actions = new List<ActionParameter>();
              if (_skillEffect.FireActionId != -1 & actionStart)
                _actions.Add(_skillEffect.FireAction);
              firearmCtrl.Initialize(_firearmEndTarget, _actions, _skill, actionStart ? _skillEffect.FireArmEndEffects : new List<NormalSkillEffect>(), Owner, _skillEffect.Height, _skill.BlackOutTime > 0.0, _skillEffect.IsAbsoluteFireArm, transform.position + (Owner.IsLeftDir ? -1f : 1f) * new Vector3(_skillEffect.AbsoluteFireDistance, 0.0f), _skillEffect.ShakeEffects, _skillEffect.FireArmEndTargetBone);
              firearmCtrl.OnHitAction = _skillEffect.EffectBehavior != eEffectBehavior.FIREARM ? fctrl =>
              { for (int index1 = 0; index1 < fctrl.ShakeEffects.Count; ++index1)
                {
                  if (fctrl.ShakeEffects[index1].TargetMotion == 0)
                    this.AppendCoroutine(this.StartShakeWithDelay(fctrl.ShakeEffects[index1], fctrl.Skill), ePauseType.VISUAL, (double) fctrl.Skill.BlackOutTime > 0.0 ? this.Owner : (UnitCtrl) null);
                }
                  if (_skillEffect.FireActionId != -1)
                      AppendCoroutine(ExecActionWithDelayAndTarget(_skillEffect.FireAction, _skill, _firearmEndTarget, 0.0f), ePauseType.SYSTEM, _skill.BlackOutTime > 0.0 ? Owner : null);
                  int index3 = 0;
                  for (int count = _skillEffect.FireArmEndEffects.Count; index3 < count; ++index3)
                      AppendCoroutine(createNormalPrefabWithDelayAndTarget(_skillEffect.FireArmEndEffects[index3], _skill, 0.0f, _firearmEndTarget, false), ePauseType.VISUAL, _skill.BlackOutTime > 0.0 ? Owner : null);
              } : new Action<FirearmCtrl>(onFirearmHit);
              break;
            case eEffectBehavior.WORLD_POS_CENTER:
              switch (_skillEffect.TrackDimension)
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
              break;
            case eEffectBehavior.TARGET_CENTER:
              Vector3 vector3_1 = new Vector3();
              int index2 = 0;
              for (int count = _skillEffect.TargetAction.TargetList.Count; index2 < count; ++index2)
                vector3_1 += _skillEffect.TargetAction.TargetList[index2].GetPosition() / count;
              prefab2.transform.position = vector3_1;
              break;
            case eEffectBehavior.SKILL_AREA_RANDOM:
              Vector3 vector3_2 = new Vector3();
              int attackSide = GetAttackSide(_skillEffect.TargetAction.Direction, Owner);
              float num1 = attackSide < 1 ? -_skillEffect.TargetAction.TargetWidth : 0.0f;
              float num2 = attackSide > -1 ? _skillEffect.TargetAction.TargetWidth : 0.0f;
              float num3 = _skillEffect.TargetAction.ActionType == eActionType.REFLEXIVE ? _skillEffect.TargetAction.TargetList[0].GetPosition().x : transform.position.x;
              float num4 = num1 + num3;
              float num5 = num2 + num3;
              if (num4 < -(double) BattleDefine.BATTLE_FIELD_SIZE)
                num4 = -BattleDefine.BATTLE_FIELD_SIZE;
              if (num5 > (double) BattleDefine.BATTLE_FIELD_SIZE)
                num5 = BattleDefine.BATTLE_FIELD_SIZE;
              vector3_2.x = (float) ((num4 + (num5 - (double) num4) * BattleManager.Random(0.0f, 1f,
                  new RandomData(Owner,_target.Owner,_skill.SkillId,8,0))) / 540.0);
              vector3_2.y += (float) (_skillEffect.CenterY + _skillEffect.DeltaY * (double) BattleManager.Random(-1f, 1f,
                  new RandomData(Owner, _target.Owner, _skill.SkillId, 8, 0)) + 9.25925922393799);
              prefab2.transform.position = vector3_2;
              break;
            case eEffectBehavior.SERIES:
              if (_skillEffect.FireActionId != -1)
              {
                AppendCoroutine(ExecActionWithDelayAndTarget(_skillEffect.FireAction, _skill, _target, 0.0f), ePauseType.SYSTEM, _skill.BlackOutTime > 0.0 ? Owner : null);
              }
              break;
          }
          prefab2.RestartTween();
        }

        private SkillEffectCtrl createPrefab(
          NormalSkillEffect skillEffect,
          Skill skill,
          BasePartsData target,
          ref GameObject prefab)
        {
          prefab = Owner.IsLeftDir || Owner.IsForceLeftDirOrPartsBoss ? skillEffect.PrefabLeft : skillEffect.Prefab;
          string name = prefab.name;
          SkillEffectCtrl effect = battleEffectPool.GetEffect(prefab,skillEffect.PrefabData, Owner);
          effect.transform.parent = ExceptNGUIRoot.Transform;
          skill.EffectObjs.Add(effect);
          if (skillEffect.TargetMotionIndex == 1)
          {
            skill.LoopEffectObjs.Add(effect);
            effect.OnEffectEnd += obj => skill.LoopEffectObjs.Remove(obj);
          }
          if (effect.IsRepeat && !(effect is FirearmCtrl))
          {
            target.Owner.RepeatEffectList.Add(effect);
            effect.OnEffectEnd += obj => target.Owner.RepeatEffectList.Remove(obj);
          }
          effect.OnEffectEnd += destroyedEffect => skill.EffectObjs.Remove(destroyedEffect);
          if (skillEffect.IsReaction)
            battleManager.DAIFDPFABCM[name] = effect.gameObject;
          return effect;
        }

        private void onFirearmHit(FirearmCtrl firearmCtrl)
        {
          if (firearmCtrl == null || firearmCtrl.FireTarget == null || firearmCtrl.Skill == null)
            return;
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
            AppendCoroutine(createNormalPrefabWithDelay(firearmCtrl.SkillHitEffects[index2], firearmCtrl.Skill, _isFirearmEndEffect: true), ePauseType.VISUAL, firearmCtrl.Skill.BlackOutTime > 0.0 ? Owner : null);
        }

        private void searchTargetUnit(
          ActionParameter actionParameter,
          Vector3 basePosition,
          Skill skill,
          bool _considerBodyWidth)
        {
            float x = Owner.lossyx;
            //float x = transform.parent.lossyScale.x;

            actionParameter.TargetList.Clear();
            bool isOther = Owner.IsOther;
            List<UnitCtrl> unitCtrlList1 = !Owner.IsConfusionOrConvert() ? (isOther ? battleManager.UnitList : battleManager.EnemyList) : (!isOther ? battleManager.UnitList : battleManager.EnemyList);
            List<UnitCtrl> unitCtrlList2 = isOther ? battleManager.EnemyList : battleManager.UnitList;
            List<UnitCtrl> unitCtrlList3 = null;
            switch (actionParameter.TargetAssignment)
            {
                case eTargetAssignment.OTHER_SIDE:
                    unitCtrlList3 = unitCtrlList1;
                    break;
                case eTargetAssignment.OLD_OWNER_SIDE:
                case eTargetAssignment.OWNER_SITE:
                    unitCtrlList3 = unitCtrlList2;
                    break;
                case eTargetAssignment.ALL:
                    unitCtrlList3 = new List<UnitCtrl>(battleManager.EnemyList);
                    unitCtrlList3.AddRange(battleManager.UnitList);
                    break;
            }
            switch (actionParameter.TargetSort)
            {
                case PriorityPattern.SUMMON:
                case PriorityPattern.ALL_SUMMON_RANDOM:
                    unitCtrlList3 = unitCtrlList3.FindAll(e => e.IsSummonOrPhantom);
                    break;
                case PriorityPattern.ATK_PHYSICS:
                    unitCtrlList3 = unitCtrlList3.FindAll(e => e.AtkType == 1);
                    break;
                case PriorityPattern.ATK_MAGIC:
                    unitCtrlList3 = unitCtrlList3.FindAll(e => e.AtkType == 2);
                    break;
                case PriorityPattern.OWN_SUMMON_RANDOM:
                    unitCtrlList3 = Owner.SummonUnitDictionary.Values.ToList().FindAll(e => !e.IdleOnly && !e.IsDead);
                    break;
                case PriorityPattern.NEAR_MY_TEAM_WITHOUT_OWNER:
                    unitCtrlList3 = unitCtrlList3.FindAll(e => e != Owner);
                    break;
            }
            float num = actionParameter.TargetWidth;
            if (actionParameter.TargetSort == PriorityPattern.BACK || actionParameter.TargetSort == PriorityPattern.FORWARD)
            {
                int index1 = 0;
                for (int count = unitCtrlList3.Count; index1 < count; ++index1)
                {
                    UnitCtrl _unitCtrl = unitCtrlList3[index1];
                    if (judgeIsTarget(_unitCtrl, actionParameter))
                    {
                        if (_unitCtrl.IsPartsBoss)
                        {
                            for (int index2 = 0; index2 < _unitCtrl.BossPartsListForBattle.Count; ++index2)
                            {
                                PartsData partsData = _unitCtrl.BossPartsListForBattle[index2];
                                if (partsData.GetTargetable())
                                    actionParameter.TargetList.Add(partsData);
                            }
                        }
                        else
                            actionParameter.TargetList.Add(_unitCtrl.GetFirstParts());
                    }
                }
            }
            else
            {
                int attackSide = GetAttackSide(actionParameter.Direction, Owner);
                if (num <= 0.0)
                    num = skill.SkillId == 1 ? Owner.SearchAreaSize : skill.skillAreaWidth;
                float _start = attackSide < 1 ? -num : 0.0f;
                float _end = attackSide > -1 ? num : 0.0f;
                BasePartsData basePartsData1 = null;
                int index1 = 0;
                for (int count = unitCtrlList3.Count; index1 < count; ++index1)
                {
                    UnitCtrl _unitCtrl = unitCtrlList3[index1];
                    if (!_unitCtrl.IsPartsBoss)
                    {
                        if (judgeIsInTargetArea(actionParameter, basePosition, _considerBodyWidth, x, _start, _end, _unitCtrl.GetFirstParts()))
                        {
                            if (judgeIsTarget(_unitCtrl, actionParameter))
                                actionParameter.TargetList.Add(_unitCtrl.GetFirstParts());
                            else if ((long)_unitCtrl.Hp == 0L)
                                basePartsData1 = _unitCtrl.GetFirstParts();
                        }
                    }
                    else
                    {
                        bool flag = actionParameter.TargetSort == PriorityPattern.HP_ASC || actionParameter.TargetSort == PriorityPattern.HP_DEC || (actionParameter.TargetSort == PriorityPattern.ENERGY_ASC || actionParameter.TargetSort == PriorityPattern.ENERGY_DEC) || actionParameter.TargetSort == PriorityPattern.ENERGY_REDUCING;
                        BasePartsData basePartsData2 = null;
                        for (int index2 = 0; index2 < _unitCtrl.BossPartsListForBattle.Count; ++index2)
                        {
                            PartsData partsData = _unitCtrl.BossPartsListForBattle[index2];
                            if (judgeIsInTargetArea(actionParameter, basePosition, _considerBodyWidth, x, _start, _end, partsData) && judgeIsTarget(_unitCtrl, actionParameter))
                            {
                                if (flag)
                                {
                                    if (basePartsData2 == null)
                                    {
                                        basePartsData2 = partsData;
                                    }
                                    else
                                    {
                                        float _a = Mathf.Abs(basePartsData2.GetPosition().x - Owner.transform.position.x);
                                        float _b = Mathf.Abs(partsData.GetPosition().x - Owner.transform.position.x);
                                        if (_a > (double)_b || BattleUtil.Approximately(_a, _b))
                                            basePartsData2 = partsData;
                                    }
                                }
                                else
                                    actionParameter.TargetList.Add(partsData);
                            }
                        }
                        if (basePartsData2 != null)
                            actionParameter.TargetList.Add(basePartsData2);
                    }
                }
                if (actionParameter.TargetSort != PriorityPattern.NEAR || actionParameter.TargetList.Count != 0 || (basePartsData1 == null || Owner.IsConfusionOrConvert()))
                    return;
                actionParameter.TargetList.Add(basePartsData1);
            }
        }
        private bool isInActionTargetArea(Skill _skill, ActionParameter _action, Vector3 _basePosition, bool _considerBodyWidth, BasePartsData _checkUnitCtrl)
        {
            int attackSide = GetAttackSide(_action.Direction, Owner);
            float num = _action.TargetWidth;
            if (num <= 0f)
            {
                num = ((_skill.SkillId == 1) ? Owner.SearchAreaSize : _skill.skillAreaWidth);
            }
            float start = ((attackSide != 1) ? (0f - num) : 0f);
            float end = ((attackSide != -1) ? num : 0f);
            return judgeIsInTargetArea(_action, _basePosition, _considerBodyWidth, transform.parent.lossyScale.x, start, end, _checkUnitCtrl);
        }
        private bool judgeIsInTargetArea(
          ActionParameter _actionParameter,
          Vector3 _basePosition,
          bool _considerBodyWidth,
          float _parentLossyScale,
          float _start,
          float _end,
          BasePartsData _unitCtrl)
        {
            /* if (!_unitCtrl.GetTargetable())
                 return false;
             float _a = (float)(_unitCtrl.GetPosition().x / (double)_parentLossyScale - _basePosition.x / (double)_parentLossyScale);
             float bodyWidth = _unitCtrl.GetBodyWidth();
             if (_considerBodyWidth)            
                 bodyWidth += Owner.BodyWidth;

             float _b1 = _start - bodyWidth * 0.5f;
             float _b2 = _end + bodyWidth * 0.5f;
             return _a >= (double)_b1 && _a <= (double)_b2 || (BattleUtil.Approximately(_a, _b1) || BattleUtil.Approximately(_a, _b2)) || _actionParameter.Direction == DirectionType.ALL;
         */
            if (!_unitCtrl.GetTargetable())
            {
                return false;
            }
            float num = _unitCtrl.GetPosition().x / _parentLossyScale - _basePosition.x / _parentLossyScale;
            float num2 = _unitCtrl.GetBodyWidth();
            if (_considerBodyWidth)
            {
                num2 += Owner.BodyWidth;
            }
            float num3 = _start - num2 * 0.5f;
            float num4 = _end + num2 * 0.5f;
            if ((!(num >= num3) || !(num <= num4)) && !BattleUtil.Approximately(num, num3) && !BattleUtil.Approximately(num, num4))
            {
                return _actionParameter.Direction == DirectionType.ALL;
            }
            return true;
        }

        /*private bool judgeIsTarget(UnitCtrl _unitCtrl, ActionParameter _actionParameter)
        {
            if (_unitCtrl.IsStealth || _unitCtrl.IsPhantom && _actionParameter.TargetSort != PriorityPattern.OWN_SUMMON_RANDOM || (_unitCtrl.IsDead || (long)_unitCtrl.Hp <= 0L) && !_unitCtrl.HasUnDeadTime)
                return false;
            return _actionParameter.TargetAssignment != eTargetAssignment.OTHER_SIDE || Owner != _unitCtrl;
        }*/
        private bool judgeIsTarget(UnitCtrl _unitCtrl, ActionParameter _actionParameter)
        {
            if (!_unitCtrl.IsStealth && (!_unitCtrl.IsPhantom || _actionParameter.TargetSort == PriorityPattern.OWN_SUMMON_RANDOM) && ((!_unitCtrl.IsDead && (long)_unitCtrl.Hp > 0) || _unitCtrl.HasUnDeadTime) && (_actionParameter.TargetAssignment != eTargetAssignment.OTHER_SIDE || Owner != _unitCtrl))
            {
                if (_actionParameter.TargetAssignment == eTargetAssignment.OTHER_SIDE && _actionParameter.TargetNum == 1)
                {
                    return !_unitCtrl.IsAbnormalState(UnitCtrl.eAbnormalState.SPY);
                }
                return true;
            }
            return false;
        }

        public static int GetAttackSide(DirectionType _direction, UnitCtrl _owner) => _direction != DirectionType.FRONT ? 0 : (!_owner.IsLeftDir ? 1 : -1);

        private void sortTargetListByTargetPattern(
          ActionParameter _actionParameter,
          Transform _baseTransform,
          float _value,
          bool _quiet,
          Skill skill)
        {
            Func<BasePartsData, string> selector;
            switch (_actionParameter.TargetSort)
            {
                case PriorityPattern.HP_ASC:
                case PriorityPattern.HP_DEC:
                case PriorityPattern.HP_ASC_NEAR:
                case PriorityPattern.HP_DEC_NEAR:
                    selector = parts => $"{parts.Owner.UnitName}({(float)parts.Owner.Hp / parts.Owner.MaxHp:P2})";
                    break;
                case PriorityPattern.ENERGY_ASC:
                case PriorityPattern.ENERGY_DEC:
                case PriorityPattern.ENERGY_REDUCING:
                case PriorityPattern.ENERGY_ASC_NEAR:
                case PriorityPattern.ENERGY_DEC_NEAR:
                case PriorityPattern.ENERGY_DEC_NEAR_MAX_FORWARD:
                    selector = parts => $"{parts.Owner.UnitName}({(float)parts.Owner.Energy / UnitDefine.MAX_ENERGY:P2})";
                    break;
                case PriorityPattern.ATK_ASC:
                case PriorityPattern.ATK_DEC:
                case PriorityPattern.ATK_ASC_NEAR:
                case PriorityPattern.ATK_DEC_NEAR:
                    selector = parts => $"{parts.Owner.UnitName}({parts.GetAtkZero()})";
                    break;
                case PriorityPattern.MAGIC_STR_ASC:
                case PriorityPattern.MAGIC_STR_DEC:
                case PriorityPattern.MAGIC_STR_ASC_NEAR:
                case PriorityPattern.MAGIC_STR_DEC_NEAR:
                    selector = parts => $"{parts.Owner.UnitName}({parts.GetMagicStrZero()})";
                    break;
                case PriorityPattern.PHYSICS_OR_MAGIC_HIGH_ATTACK_DEC:
                case PriorityPattern.PHYSICS_OR_MAGIC_HIGH_ATTACK_ASC:
                    selector = parts => $"{parts.Owner.UnitName}({parts.GetAtkZero()}|{parts.GetMagicStrZero()})";
                    break;
                default:
                    selector = parts => $"{parts.Owner.UnitName}";
                    break;
            }
            void QuickSort(List<BasePartsData> _array, Func<BasePartsData, BasePartsData, int> _compare)
            {
                _actionParameter.sortinfo = "";
                var str = $"对{skill.SkillName}({_actionParameter.ActionId % 100 + 1}/{skill.ActionParameters.Count})进行排序：\n" +
                    $"{string.Join("\n", _array.Select(p => p == null ? "null" : selector(p)).ToArray())}";
                Owner.UIManager.LogMessage(str, eLogMessageType.EXEC_ACTION, Owner);
                _actionParameter.sortinfo += str + "\n";
                if (_array.Count == 0)
                    return;
                UnitCtrl.FunctionalComparer<BasePartsData> instance = UnitCtrl.FunctionalComparer<BasePartsData>.Instance;
                instance.SetComparer(_compare);
                UnitCtrl.quickSortImpl(_array, 0, _array.Count - 1, instance);
                str = $"{Owner.UnitName}对{skill.SkillName}({_actionParameter.ActionId % 100 + 1}/{skill.ActionParameters.Count})排序结果\n" +
                    $"{string.Join("\n", _array.Select(p => p == null ? "null" : selector(p)).ToArray())}";
                Owner.UIManager.LogMessage(str, eLogMessageType.EXEC_ACTION, Owner);
                _actionParameter.sortinfo += str + "\n";
            }

            List<BasePartsData> targetList = _actionParameter.TargetList;
            var lst = targetList.Select(part => new BasePartsDataEx
            {
                Energy = part.Owner.Energy,
                Hp = part.Owner.Hp,
                MaxHp = part.Owner.MaxHp,
                GetMagicStrZeroEx = part.GetMagicStrZeroEx(),
                GetAtkZeroEx = part.GetAtkZeroEx(),
                UnitName = part.Owner.UnitNameEx,
                hash = part.GetHashCode()
            }).ToList();

            void AddProbEvent(Func<BasePartsDataEx, BasePartsDataEx, int, int> _compare,
                Func<BasePartsDataEx, FloatWithEx> getter)
            {
                if (_actionParameter.TargetNum != 1) return;
                var nth = _actionParameter.TargetNth;
                var tgt = targetList[Math.Min(lst.Count - 1, nth)];
                var tgth = tgt.GetHashCode();
                var evt = new ProbEvent
                {
                    unit = Owner.UnitNameEx,
                    predict = hash =>
                    {
                        var lst2 = new List<BasePartsDataEx>(lst);
                        var instance = UnitCtrl.FunctionalComparer<BasePartsDataEx>.Instance;
                        instance.SetComparer((a, b) => _compare(a, b, hash));
                        UnitCtrl.quickSortImpl(lst2, 0, lst2.Count - 1, instance);
                        return lst2[Math.Min(lst2.Count - 1, nth)].hash != tgth;
                    },
                    exp = hash => $"\n qsort = {nth}:\n{string.Join("\n", lst.Select(u => getter(u).ToExpression(hash)))}",
                    enabled = false,
                    description =
                        $"({BattleHeaderController.CurrentFrameCount}){Owner.UnitNameEx}的{(Owner.CurrentSkillId == 1 ? "普攻" : $"{skillDictionary[Owner.CurrentSkillId].SkillName}技能({Owner.CurrentSkillId})")}打歪(原定目标: {tgt.Owner.UnitNameEx})"
                };
                _actionParameter.relatedEvent = evt;
                GuildCalculator.Instance.dmglist.Add(evt);
            }

            switch (_actionParameter.TargetSort)
            {
                case PriorityPattern.RANDOM:
                case PriorityPattern.RANDOM_ONCE:
                case PriorityPattern.ALL_SUMMON_RANDOM:
                case PriorityPattern.OWN_SUMMON_RANDOM:
                    int count = targetList.Count;
                    while (count > 1)
                    {
                        int index = BattleManager.Random(0, count,
                                                    new RandomData(null,null,_actionParameter.ActionId,10,count));
                        --count;
                        BasePartsData basePartsData = targetList[index];
                        targetList[index] = targetList[count];
                        targetList[count] = basePartsData;
                    }
                    break;
                case PriorityPattern.NEAR:
                case PriorityPattern.NEAR_MY_TEAM_WITHOUT_OWNER:
                    Owner.BaseX = _baseTransform.position.x;
                    targetList.Sort(Owner.CompareDistanceAsc);
                    break;
                case PriorityPattern.FAR:
                    Owner.BaseX = _baseTransform.position.x;
                    targetList.Sort(Owner.CompareDistanceDec);
                    break;
                case PriorityPattern.HP_ASC:
                    QuickSort(targetList, UnitCtrl.CompareLifeAsc);
                    AddProbEvent(UnitCtrl.CompareLifeAsc, u => u.Hp / u.MaxHp);
                    break;
                case PriorityPattern.HP_DEC:
                    QuickSort(targetList, UnitCtrl.CompareLifeDec);
                    AddProbEvent(UnitCtrl.CompareLifeDec, u => u.Hp / u.MaxHp);
                    break;
                case PriorityPattern.OWNER:
                    targetList.Clear();
                    targetList.Add(Owner.GetFirstParts(_basePos: (-BattleDefine.BATTLE_FIELD_SIZE)));
                    break;
                case PriorityPattern.FORWARD:
                case PriorityPattern.BACK:
                    bool _isForwardPattern = _actionParameter.TargetSort == PriorityPattern.FORWARD;
                    sortTargetPosition(targetList, _isForwardPattern, Owner.CompareLeft, Owner.CompareRight);
                    break;
                case PriorityPattern.ABSOLUTE_POSITION:
                    targetList.Clear();
                    targetList.Add(Owner.GetFirstParts(true));
                    break;
                case PriorityPattern.ENERGY_DEC:
                    QuickSort(targetList, UnitCtrl.CompareEnergyDec);
                    AddProbEvent(UnitCtrl.CompareEnergyDec, u => u.Energy);
                    break;
                case PriorityPattern.ENERGY_ASC:
                case PriorityPattern.ENERGY_REDUCING:
                    QuickSort(targetList, UnitCtrl.CompareEnergyAsc);
                    AddProbEvent(UnitCtrl.CompareEnergyAsc, u => u.Energy);
                    break;
                case PriorityPattern.ATK_DEC:
                    QuickSort(targetList, UnitCtrl.CompareAtkDec);
                    AddProbEvent(UnitCtrl.CompareAtkDec, u => u.GetAtkZeroEx);
                    break;
                case PriorityPattern.ATK_ASC:
                    QuickSort(targetList, UnitCtrl.CompareAtkAsc);
                    AddProbEvent(UnitCtrl.CompareAtkAsc, u => u.GetAtkZeroEx);
                    break;
                case PriorityPattern.MAGIC_STR_DEC:
                    QuickSort(targetList, UnitCtrl.CompareMagicStrDec);
                    AddProbEvent(UnitCtrl.CompareMagicStrDec, u => u.GetMagicStrZeroEx);
                    break;
                case PriorityPattern.MAGIC_STR_ASC:
                    QuickSort(targetList, UnitCtrl.CompareMagicStrAsc);
                    AddProbEvent(UnitCtrl.CompareMagicStrAsc, u => u.GetMagicStrZeroEx);
                    break;
                case PriorityPattern.BOSS:
                    Owner.BaseX = _baseTransform.position.x;
                    targetList.Sort(Owner.CompareBoss);
                    break;
                case PriorityPattern.HP_ASC_NEAR:
                    Owner.BaseX = _baseTransform.position.x;
                    targetList.Sort(Owner.CompareLifeAscNear);
                    break;
                case PriorityPattern.HP_DEC_NEAR:
                    Owner.BaseX = _baseTransform.position.x;
                    targetList.Sort(Owner.CompareLifeDecNear);
                    break;
                case PriorityPattern.ENERGY_DEC_NEAR:                
                    Owner.BaseX = _baseTransform.position.x;
                    targetList.Sort(Owner.CompareEnergyDecNear);
                    break;
                case PriorityPattern.ENERGY_DEC_NEAR_MAX_FORWARD:
                    Owner.BaseX = _baseTransform.position.x;
                    targetList.Sort(Owner.CompareEnergyDecNearForward);
                    break;
                case PriorityPattern.ENERGY_ASC_NEAR:
                    Owner.BaseX = _baseTransform.position.x;
                    targetList.Sort(Owner.CompareEnergyAscNear);
                    break;
                case PriorityPattern.ATK_DEC_NEAR:
                    Owner.BaseX = _baseTransform.position.x;
                    targetList.Sort(Owner.CompareAtkDecNear);
                    break;
                case PriorityPattern.ATK_ASC_NEAR:
                    Owner.BaseX = _baseTransform.position.x;
                    targetList.Sort(Owner.CompareAtkAscNear);
                    break;
                case PriorityPattern.MAGIC_STR_DEC_NEAR:
                    Owner.BaseX = _baseTransform.position.x;
                    targetList.Sort(Owner.CompareMagicStrDecNear);
                    break;
                case PriorityPattern.MAGIC_STR_ASC_NEAR:
                    Owner.BaseX = _baseTransform.position.x;
                    targetList.Sort(Owner.CompareMagicStrAscNear);
                    break;
                case PriorityPattern.SHADOW:
                    Owner.BaseX = _baseTransform.position.x;
                    targetList.Sort(Owner.CompareShadow);
                    break;
                case PriorityPattern.HP_VALUE_DEC_NEAR_FORWARD:
                    Owner.BaseX = _baseTransform.position.x;
                    sortTargetPosition(targetList, true, Owner.CompareLifeValueDecSameLeft, Owner.CompareLifeValueDecSameRight);
                    break;
                case PriorityPattern.HP_VALUE_ASC_NEAR_FORWARD:
                    Owner.BaseX = _baseTransform.position.x;
                    sortTargetPosition(targetList, true, Owner.CompareLifeValueAscSameLeft, Owner.CompareLifeValueAscSameRight);
                    break;
                case PriorityPattern.PHYSICS_OR_MAGIC_HIGH_ATTACK_DEC:
                    Owner.BaseX = _baseTransform.position.x;
                    targetList.Sort(Owner.CompareHigherAtkOrMagicStrDecSameNear);
                    break;
                case PriorityPattern.PHYSICS_OR_MAGIC_HIGH_ATTACK_ASC:
                    Owner.BaseX = _baseTransform.position.x;
                    targetList.Sort(Owner.CompareHigherAtkOrMagicStrAscSameNear);
                    break;
                default:
                    targetList.Sort(Owner.CompareDistanceAsc);
                    break;
            }
            switch (_actionParameter.TargetSort)
            {
                case PriorityPattern.ENERGY_DEC:
                case PriorityPattern.ENERGY_DEC_NEAR:
                    List<BasePartsData> basePartsDataList1 = new List<BasePartsData>();
                    List<BasePartsData> basePartsDataList2 = new List<BasePartsData>();
                    for (int index = 0; index < targetList.Count; ++index)
                    {
                        if ((double)targetList[index].Owner.Energy == 1000.0)
                            basePartsDataList1.Add(targetList[index]);
                        else
                            basePartsDataList2.Add(targetList[index]);
                    }
                    basePartsDataList2.AddRange(basePartsDataList1);
                    _actionParameter.TargetList = basePartsDataList2;
                    break;
                case PriorityPattern.ENERGY_REDUCING:
                    List<BasePartsData> basePartsDataList3 = new List<BasePartsData>();
                    List<BasePartsData> basePartsDataList4 = new List<BasePartsData>();
                    for (int index = 0; index < targetList.Count; ++index)
                    {
                        if (targetList[index].Owner.GetIsReduceEnergy())
                            basePartsDataList3.Add(targetList[index]);
                        else
                            basePartsDataList4.Add(targetList[index]);
                    }
                    basePartsDataList3.AddRange(basePartsDataList4);
                    _actionParameter.TargetList = basePartsDataList3;
                    break;
            }
        }

        private void sortTargetPosition(
          List<BasePartsData> _targetList,
          bool _isForwardPattern,
          Comparison<BasePartsData> _forwardComparison,
          Comparison<BasePartsData> _backComparison)
        {
            if (Owner.IsOther)
                _isForwardPattern = !_isForwardPattern;
            if (Owner.IsConfusionOrConvert())
                _isForwardPattern = !_isForwardPattern;
            if (_isForwardPattern)
                _targetList.Sort(_forwardComparison);
            else
                _targetList.Sort(_backComparison);
        }

        public void CancelAction(int skillId)
        {
            Skill skill = skillDictionary[skillId];
            for (int index = skill.EffectObjs.Count - 1; index >= 0; --index)
            {
              if (skill.EffectObjs[index] != null && !(skill.EffectObjs[index] is FirearmCtrl) && !skill.EffectObjs[index].IsRepeat)
              {
                skill.EffectObjs[index].SetTimeToDie(true);
                skill.EffectObjs[index].OnEffectEnd(skill.EffectObjs[index]);
              }
            }
            skill.Cancel = true;
        }

        public IEnumerator StartShakeWithDelay(
          ShakeEffect _shake,
          Skill _skill,
          bool _first = false)
        {
          if (!_first || (double) _shake.StartTime >= (double) _skill.CutInSkipTime)
          {
            float time = _first ? _skill.CutInSkipTime : 0.0f;
            while (true)
            {
              time += this.Owner.DeltaTimeForPause;
              if (!_skill.Cancel)
              {
                if ((double) time <= (double) _shake.StartTime)
                  yield return (object) null;
                else
                  goto label_6;
              }
              else
                break;
            }
            yield break;
    label_6:
            this.battleCameraEffect.StartShake(_shake, _skill, this.Owner);
          }
        }

        /*private void startBlurEffects(Skill _skill, bool _first = false)
        {
          int index = 0;
          for (int count = _skill.BlurEffects.Count; index < count; ++index)
            this.AppendCoroutine(this.startBlurWithDelay(_skill.BlurEffects[index], _skill, _first), ePauseType.VISUAL, (double) _skill.BlackOutTime > 0.0 ? this.Owner : (UnitCtrl) null);
        }*/

        /*private IEnumerator startBlurWithDelay(
          BlurEffect.BlurEffectData _blurData,
          Skill _skill,
          bool _first)
        {
          if (!_first || (double) _blurData.StartTime >= (double) _skill.CutInSkipTime)
          {
            float time = _first ? _skill.CutInSkipTime : 0.0f;
            while (true)
            {
              time += this.Owner.DeltaTimeForPause;
              if (!_skill.Cancel)
              {
                if ((double) time <= (double) _blurData.StartTime)
                  yield return (object) null;
                else
                  goto label_6;
              }
              else
                break;
            }
            yield break;
    label_6:
            this.battleCameraEffect.StartBlurEffect(_blurData);
          }
        }*/

        public void AppendCoroutine(IEnumerator cr, ePauseType pauseType, UnitCtrl unit = null) => battleManager.AppendCoroutine(cr, pauseType, unit);

        public bool HasNextAnime(int skillId)
        {
            //Skill skill = skillDictionary[skillId];
            //if (skill.IsPrincessForm)
            //    return true;
            //return (skill.AnimId == eSpineCharacterAnimeId.SKILL || skill.AnimId == eSpineCharacterAnimeId.SKILL_EVOLUTION || skill.AnimId == eSpineCharacterAnimeId.SPECIAL_SKILL_EVOLUTION) && Owner.GetCurrentSpineCtrl().IsAnimation(skill.AnimId, skill.SkillNum, 1);
            Skill skill = skillDictionary[skillId];
            if (skill.IsPrincessForm)
            {
                return true;
            }
            if (skill.AnimId == eSpineCharacterAnimeId.SKILL || skill.AnimId == eSpineCharacterAnimeId.SKILL_EVOLUTION || skill.AnimId == eSpineCharacterAnimeId.SPECIAL_SKILL_EVOLUTION || skill.AnimId == eSpineCharacterAnimeId.SUB_UNION_BURST)
            {
                return Owner.GetCurrentSpineCtrl().IsAnimation(skill.AnimId, skill.SkillNum, 1);
            }
            return false;
        }

        public bool IsLoopMotionPlaying(int _skillId)
        {
            Skill skill = skillDictionary[_skillId];
            return Owner.GetCurrentSpineCtrl().AnimationName == Owner.GetCurrentSpineCtrl().ConvertAnimeIdToAnimeName(skill.AnimId, skill.SkillNum, 1);
        }

        public SkillMotionType GetSkillMotionType(int _skillId) => skillDictionary[_skillId].SkillMotionType;

        public int GetSkillNum(int _skillId) => skillDictionary[_skillId].SkillNum;
        public eSpineCharacterAnimeId GetAnimeId(int _skillId)
        {
            return skillDictionary[_skillId].AnimId;
        }

        public bool IsModeChange(int _skillId) => skillDictionary[_skillId].IsModeChange;
        public void SetBonusId(int _skillId, int _bonusId)
        {
            skillDictionary[_skillId].BonusId = _bonusId;
        }

        public void ExecActionOnStart()
        {
            foreach (KeyValuePair<int, Skill> skill in skillDictionary)
                execActionOnStart(skill.Value);
        }

        public void ExecActionOnWaveStart()
        {
            foreach (KeyValuePair<int, Skill> skill in skillDictionary)
                execActionOnWaveStart(skill.Value);
        }

        public IEnumerator UpdateBranchMotion(ActionParameter _action, Skill _skill)
        {
            if (_skill.AnimId != eSpineCharacterAnimeId.NONE && !updateBranchMotionRunning)
            {
                updateBranchMotionRunning = true;
                BattleSpineController unitSpineController = Owner.GetCurrentSpineCtrl();
                if (!unitSpineController.IsAnimation(_skill.AnimId, _skill.SkillNum, 1))
                {
                    updateBranchMotionRunning = false;
                }
                else
                {
                    while (!actionCancelAndGetCancel(_skill.SkillId))
                    {
                        if (unitSpineController.IsPlayAnimeBattle)
                            yield return null;
                        else if (_skill.EffectBranchId == 0)
                        {
                            updateBranchMotionRunning = false;
                            yield break;
                        }
                        else
                        {
                            Owner.PlayAnime(_skill.AnimId, _skill.SkillNum, _skill.EffectBranchId, _isLoop: false);
                            if (_skill.SkillId == Owner.UnionBurstSkillId)
                                Owner.UnionBurstAnimeEndForIfAction = true;
                            while (!actionCancelAndGetCancel(_skill.SkillId))
                            {
                                if (!unitSpineController.IsPlayAnimeBattle)
                                {
                                    Owner.SetState(UnitCtrl.ActionState.IDLE);
                                    updateBranchMotionRunning = false;
                                    yield break;
                                }

                                yield return null;
                            }
                            updateBranchMotionRunning = false;
                            yield break;
                        }
                    }
                    updateBranchMotionRunning = false;
                }
            }
        }

        private bool actionCancelAndGetCancel(int _skillId)
        {
            if (!Owner.IsUnableActionState() && !Owner.IsCancelActionState(_skillId == Owner.UnionBurstSkillId))
                return false;
            Owner.CancelByAwake = false;
            Owner.CancelByConvert = false;
            CancelAction(_skillId);
            return true;
        }
        public bool GetForceCutinOff(int _skillId)
        {
            return skillDictionary[_skillId].ForceCutinOff;
        }

        private enum eRefrexiveType
        {
            REFLEXIVE_NORMAL = 1,
            REFLEXIVE_ABSOLUTE_SEARCH = 2,
            REFLEXIVE_ABSOLUTE_POSITION = 3,
            REFLEXIVE_NORMAL_NOT_CONSIDER_BODY_WIDTH = 4,
        }
    }
}
