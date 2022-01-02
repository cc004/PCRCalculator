// Decompiled with JetBrains decompiler
// Type: Elements.UnitActionController
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using Cute;
using Elements.Battle;
using Spine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Elements
{
    public static class MathfPlus
    {
        public static FloatWithEx Max(float f, FloatWithEx x) =>
            new FloatWithEx { ex = Mathf.Max(x.ex, f), value = Mathf.Max(x.value, f)};

        public static FloatWithEx Max(FloatWithEx x, float f) => Max(f, x);
        public static FloatWithEx Log(FloatWithEx x) => new FloatWithEx { ex = Mathf.Log(x.ex), value = Mathf.Log(x.value) };
    }
    public struct FloatWithEx
    {
        public float value;
        public float ex;

        public FloatWithEx Floor()
        {
            return new FloatWithEx
            {
                ex = ex - 0.5f, value = (long) value
            };
        }
        public override string ToString()
        {
            return (value == ex) ? value.ToString() : $"{(int)value}[{(int)ex}]";
        }

        public static FloatWithEx operator *(FloatWithEx a, FloatWithEx b)
        {
            return new FloatWithEx { value = a.value * b.value, ex = a.ex * b.ex };
        }

        public static FloatWithEx operator *(FloatWithEx a, float b)
        {
            return new FloatWithEx { value = a.value * b, ex = a.ex * b };
        }

        public static FloatWithEx operator +(FloatWithEx a, FloatWithEx b)
        {
            return new FloatWithEx { value = a.value + b.value, ex = a.ex + b.ex };
        }

        public static FloatWithEx operator +(FloatWithEx a, float b)
        {
            return new FloatWithEx { value = a.value + b, ex = a.ex + b };
        }

        public static FloatWithEx operator -(FloatWithEx a, FloatWithEx b)
        {
            return new FloatWithEx { value = a.value - b.value, ex = a.ex - b.ex };
        }

        public static FloatWithEx operator -(FloatWithEx a, float b)
        {
            return new FloatWithEx { value = a.value - b, ex = a.ex - b };
        }

        public static FloatWithEx operator /(FloatWithEx a, FloatWithEx b)
        {
            return new FloatWithEx { value = a.value / b.value, ex = a.ex / b.ex };
        }

        public static FloatWithEx operator /(FloatWithEx a, float b)
        {
            return new FloatWithEx { value = a.value / b, ex = a.ex / b };
        }

        public static implicit operator float(FloatWithEx self) => self.value;
        public static implicit operator FloatWithEx(float x) => new FloatWithEx { value = x, ex = x};
        
    }

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
        public Skill Annihilation;
        //private static Yggdrasil<UnitActionController> staticSingletonTree;
        private static BattleManagerForActionController staticBattleManager;
        //private static BMIOELFOCPE staticBattleCameraEffect;
        private static BattleEffectPoolInterface staticBattleEffectPool;
        //private static JFJNEHKINFI staticBattleTimeScale;
        public const long ACTION_ID_OFFSET = 100;
        private const int ACTION_ID_OFFSET_FOR_CHARA_ID = 10000;
        private const int DEFAULT_MOTION_NUMBER = 0;
        private const int LOOP_MOTION_NUMBER = 1;
        private const int FIRST_TARGET_INDEX = 0;

        public bool GetIsSkillPrincessForm(int _skillId) => this.skillDictionary[_skillId].IsPrincessForm;

        public List<PrincessSkillMovieData> GetPrincessFormMovieData(int _skillId) => this.skillDictionary[_skillId].PrincessSkillMovieDataList;

        public UnitCtrl Owner { get; set; }

        public bool Skill1IsChargeTime { set; get; }

        public bool Skill1Charging { set; get; }

        public bool DisableUBByModeChange { set; get; }

        public bool ModeChanging { get; set; }

        public bool MoveEnd { get; set; }

        public Dictionary<int, Skill> skillDictionary { get; private set; }

        public bool ContinuousActionEndDone { get; set; }

        private bool isUnionBurstOnlyOwner { get; set; }

        private bool updateBranchMotionRunning { get; set; }

        private BattleManagerForActionController battleManager => UnitActionController.staticBattleManager;

        //private BMIOELFOCPE battleCameraEffect => UnitActionController.staticBattleCameraEffect;

        private BattleEffectPoolInterface battleEffectPool => UnitActionController.staticBattleEffectPool;

        //private JFJNEHKINFI battleTimeScale => UnitActionController.staticBattleTimeScale;

        public static void StaticRelease()
        {
            //UnitActionController.staticSingletonTree = (Yggdrasil<UnitActionController>) null;
            UnitActionController.staticBattleManager = (BattleManagerForActionController)null;
            //UnitActionController.staticBattleCameraEffect = (BMIOELFOCPE) null;
            UnitActionController.staticBattleEffectPool = (BattleEffectPoolInterface)null;
            //UnitActionController.staticBattleTimeScale = (JFJNEHKINFI) null;
        }

        private void OnDestroy()
        {
            this.AttackDetail = (ActionParameterOnPrefabDetail)null;
            this.Attack = (Skill)null;
            this.UnionBurstList = (List<Skill>)null;
            this.MainSkillList = (List<Skill>)null;
            this.SpecialSkillList = (List<Skill>)null;
            this.SpecialSkillEvolutionList = (List<Skill>)null;
            this.Owner = (UnitCtrl)null;
            this.skillDictionary = (Dictionary<int, Skill>)null;
        }

        public void Initialize(
          UnitCtrl _owner,
          UnitParameter _unitParameter,
          bool _initializeAttackOnly = false,
          UnitCtrl _seOwner = null)
        {
            /*if (UnitActionController.staticSingletonTree == null)
            {
              UnitActionController.staticSingletonTree = this.CreateSingletonTree<UnitActionController>();
              UnitActionController.staticBattleManager = (GHPNJFDPICH) UnitActionController.staticSingletonTree.Get<BattleManager>();
              UnitActionController.staticBattleCameraEffect = (BMIOELFOCPE) UnitActionController.staticSingletonTree.Get<CMMLKFHCEPD>();
              UnitActionController.staticBattleEffectPool = (BattleEffectPoolInterface) UnitActionController.staticSingletonTree.Get<BattleEffectPool>();
              UnitActionController.staticBattleTimeScale = (JFJNEHKINFI) UnitActionController.staticSingletonTree.Get<BattleSpeedManager>();
            }*/

            staticBattleManager = BattleManager.Instance;
            staticBattleEffectPool = BattleManager.Instance.battleEffectPool;

            this.Owner = _owner;
            this.transform = _owner.transform;
            MyGameCtrl.ResetSkillEffects(this);
            this.skillDictionary = new Dictionary<int, Skill>();
            this.Attack.AnimId = eSpineCharacterAnimeId.ATTACK;
            this.Attack.WeaponType = _initializeAttackOnly ? _seOwner.WeaponSeType : this.Owner.WeaponSeType;
            this.Attack.skillAreaWidth = this.Owner.SearchAreaSize;
            this.Attack.SkillId = 1;
            this.Attack.SkillName = "普攻";
            AttackAction attackAction = new AttackAction();
            attackAction.Initialize();
            attackAction.TargetAssignment = eTargetAssignment.OTHER_SIDE;
            attackAction.TargetSort = PriorityPattern.NEAR;
            attackAction.TargetNth = 0;
            attackAction.TargetNum = 1;
            attackAction.TargetWidth = this.Owner.SearchAreaSize;
            attackAction.Direction = DirectionType.FRONT;
            attackAction.ActionType = eActionType.ATTACK;
            attackAction.Value = new Dictionary<eValueNumber, FloatWithEx>((IEqualityComparer<eValueNumber>)new eValueNumber_DictComparer())
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
            if (this.UseDefaultDelay && BattleDefine.WEAPON_HIT_DELAY_DIC.TryGetValue(_initializeAttackOnly ? _seOwner.WeaponMotionType : this.Owner.WeaponMotionType, out num))
            {
                attackAction.ActionExecTimeList = new List<ActionExecTime>()
        {
          new ActionExecTime()
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
                ActionParameterOnPrefabDetail attackDetail = this.AttackDetail;
                attackDetail.ExecTime = new List<ActionExecTime>((IEnumerable<ActionExecTime>)attackDetail.ExecTimeForPrefab);
                int index1 = 0;
                for (int count = attackDetail.ExecTimeCombo.Count; index1 < count; ++index1)
                {
                    ActionExecTimeCombo actionExecTimeCombo = attackDetail.ExecTimeCombo[index1];
                    for (int index2 = 0; index2 < actionExecTimeCombo.Count; ++index2)
                    {
                        switch (actionExecTimeCombo.InterporationType)
                        {
                            case eComboInterporationType.LINEAR:
                                attackDetail.ExecTime.Add(new ActionExecTime()
                                {
                                    Time = actionExecTimeCombo.StartTime + (float)index2 * actionExecTimeCombo.OffsetTime,
                                    DamageNumType = eDamageEffectType.COMBO
                                });
                                break;
                            case eComboInterporationType.CURVE:
                                attackDetail.ExecTime.Add(new ActionExecTime()
                                {
                                    Time = actionExecTimeCombo.StartTime + actionExecTimeCombo.Curve.Evaluate((float)index2 / (float)actionExecTimeCombo.Count) * (float)actionExecTimeCombo.Count * actionExecTimeCombo.OffsetTime,
                                    DamageNumType = eDamageEffectType.COMBO
                                });
                                break;
                        }
                    }
                }
                attackDetail.ExecTime.Sort((Comparison<ActionExecTime>)((a, b) => a.Time.CompareTo(b.Time)));
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
            this.Attack.ActionParameters = new List<ActionParameter>();
            this.Attack.ActionParameters.Add((ActionParameter)attackAction);
            this.skillDictionary.Add(1, this.Attack);
            this.dependActionSolve(this.Attack);
            this.execActionOnStart(this.Attack);
            if (_initializeAttackOnly)
                return;
            MasterUnitSkillData.UnitSkillData skillData = _unitParameter.SkillData;
            if (this.UnionBurstList.Count != 0)
            {
                List<int> unionBurstIds = skillData.UnionBurstIds;
                this.skillDictionary.Add(unionBurstIds[0], this.UnionBurstList[0]);
                this.isUnionBurstOnlyOwner = (uint)unionBurstIds[0] > 0U;
                this.UnionBurstList[0].SkillNum = 0;
            }
            if (this.UnionBurstEvolutionList.Count != 0)
            {
                this.skillDictionary.Add(skillData.UnionBurstEvolutionIds[0], this.UnionBurstEvolutionList[0]);
                this.UnionBurstEvolutionList[0].SkillNum = 0;
            }
            List<int> mainSkillIds = skillData.MainSkillIds;
            int index3 = 0;
            for (int count = mainSkillIds.Count; index3 < count; ++index3)
            {
                if (mainSkillIds[index3] != 0)
                {
                    if (MainSkillList.Count > index3)
                    {
                        this.MainSkillList[index3].SkillNum = index3 + 1;
                        this.skillDictionary.Add(mainSkillIds[index3], this.MainSkillList[index3]);
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
                    this.MainSkillEvolutionList[index1].SkillNum = index1 + 1;
                    this.skillDictionary.Add(skillEvolutionIds1[index1], this.MainSkillEvolutionList[index1]);
                }
            }
            List<int> spSkillIds = skillData.SpSkillIds;
            int index4 = 0;
            for (int count = spSkillIds.Count; index4 < count && spSkillIds[index4] != 0; ++index4)
            {
                this.SpecialSkillList[index4].SkillNum = index4 + 1;
                this.skillDictionary.Add(spSkillIds[index4], this.SpecialSkillList[index4]);
                this.Owner.SkillLevels[spSkillIds[index4]] = (int)this.Owner.Level;
            }
            List<int> skillEvolutionIds2 = skillData.SpSkillEvolutionIds;
            for (int index1 = 0; index1 < skillEvolutionIds2.Count; ++index1)
            {
                int key = skillEvolutionIds2[index1];
                if (key != 0)
                {
                    this.SpecialSkillEvolutionList[index1].SkillNum = index1 + 1;
                    this.skillDictionary.Add(key, this.SpecialSkillEvolutionList[index1]);
                    this.Owner.SkillLevels[key] = (int)this.Owner.Level;
                }
                else
                    break;
            }
            foreach (KeyValuePair<int, Skill> skill in this.skillDictionary)
            {
                Skill _skill = skill.Value;
                int key = skill.Key;
                switch (key)
                {
                    case 0:
                    case 1:
                        continue;
                    default:
                        if (this.Owner.SkillLevels[key] != 0)
                        {
                            PCRCaculator.SkillData data = PCRCaculator.MainManager.Instance.SkillDataDic[key];
                            MasterSkillData.SkillData _skillParameter = new MasterSkillData.SkillData(data);
                            this.setSkillParameter(_skill, _skillParameter);
                            _skill.SetLevel(this.Owner.SkillLevels[key]);
                            continue;
                        }
                        continue;
                }
            }
            if (this.UnionBurstList.Count != 0)
                this.setCutInSkipTimeForPrincessForm(skillData.UnionBurstIds[0]);
            if (this.UnionBurstEvolutionList.Count == 0)
                return;
            this.setCutInSkipTimeForPrincessForm(skillData.UnionBurstEvolutionIds[0]);
        }

        private void setSkillParameter(Skill _skill, MasterSkillData.SkillData _skillParameter)
        {
            Dictionary<eActionType, int> actionCounter = new Dictionary<eActionType, int>((IEqualityComparer<eActionType>)new eActionType_DictComparer());
            _skill.ActionParameters = new List<ActionParameter>();
            if (_skillParameter == null)
                return;
            if (_skillParameter.SkillId == this.Owner.UnionBurstSkillId)
            {
                //eUbResponceVoiceType responceVoiceType = eUbResponceVoiceType.APPLOUD;
                //if (!SkillDefine.UbResponceVoiceDictionary.TryGetValue((int) _skillParameter.icon_type, out responceVoiceType))
                //  responceVoiceType = eUbResponceVoiceType.APPLOUD;
                //this.Owner.UbResponceVoiceType = responceVoiceType;
            }
            _skill.SkillId = _skillParameter.SkillId;
            _skill.skillAreaWidth = (int)_skillParameter.skill_area_width == 0 ? this.Owner.SearchAreaSize : (float)(int)_skillParameter.skill_area_width;
            for (int index = 0; index < _skillParameter.ActionDataList.Count; ++index)
            {
                MasterSkillAction.SkillAction actionData = _skillParameter.ActionDataList[index];
                this.createActionValue(_skillParameter, actionData, _skill, actionCounter);
            }
            _skill.CastTime = (float)(double)_skillParameter.skill_cast_time;
            _skill.SkillName = _skillParameter.Name;
            //add scripts
            if (PCRCaculator.MainManager.Instance.SkillNameAndDescribe_cn.TryGetValue(_skill.SkillId, out string[] names))
            {
                _skill.SkillName = names[0];
            }
            switch (_skill.SkillMotionType)
            {
                case SkillMotionType.DEFAULT:
                    _skill.AnimId = this.Owner.animeIdDictionary[_skill.SkillId];
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
            this.dependActionSolve(_skill);
            this.initializeAction(_skill);
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
                skill.HasParentIndexes = skill.HasParentIndexes.Union<int>((IEnumerable<int>)actionParameter.ActionChildrenIndexes).ToList<int>();
                if (actionParameter.ActionType == eActionType.REFLEXIVE)
                {
                    int index1 = 0;
                    for (int count2 = actionParameter.ActionChildrenIndexes.Count; index1 < count2; ++index1)
                        skill.ActionParameters[actionParameter.ActionChildrenIndexes[index1]].ReferencedByReflection = true;
                }
            }
            this.UpdateEffectRunTimeData(skill, skill.SkillEffects);
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
                effect.TargetAction = _skill.ActionParameters.Find((Predicate<ActionParameter>)(e => e.ActionId == effect.TargetActionId || (e.ActionId - 1000) == effect.TargetActionId));
                if (effect.TargetAction == null)
                    effect.TargetAction = _skill.ActionParameters[0];
                this.UpdateEffectRunTimeData(_skill, effect.FireArmEndEffects, false);
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
                            effect.FireAction = _skill.ActionParameters.Find((Predicate<ActionParameter>)(e => e.ActionId == effect.FireActionId || (e.ActionId - 1000) == effect.FireActionId));
                            if (effect.FireAction != null)
                                effect.FireAction.ReferencedByEffect = true;
                            break;
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
                actionParameter.ExecActionOnStart(_skill, this.Owner, this);
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
                _skill.ActionParameters[index].ExecActionOnWaveStart(_skill, this.Owner, this);
        }

        private void initializeAction(Skill _skill)
        {
            for (int index = 0; index < _skill.ActionParameters.Count; ++index)
                _skill.ActionParameters[index].Initialize(this.Owner);
        }

        private void createActionValue(
          MasterSkillData.SkillData skillParameter,
          MasterSkillAction.SkillAction actionParam,
          Skill skill,
          Dictionary<eActionType, int> actionCounter)
        {
            ActionParameter actionParameter = Activator.CreateInstance(SkillDefine.SkillActionTypeDictionary[(eActionType)(byte)actionParam.action_type]) as ActionParameter;
            actionParameter.TargetSort = (PriorityPattern)(int)actionParam.target_type;
            actionParameter.TargetNth = (int)actionParam.target_number;
            actionParameter.TargetNum = (int)actionParam.target_count;
            actionParameter.TargetWidth = (int)actionParam.target_range <= 0 ? this.Owner.SearchAreaSize : (float)(int)actionParam.target_range;
            actionParameter.ActionType = (eActionType)(byte)actionParam.action_type;
            actionParameter.ActionDetail1 = (int)actionParam.action_detail_1;
            actionParameter.ActionDetail2 = (int)actionParam.action_detail_2;
            actionParameter.ActionDetail3 = (int)actionParam.action_detail_3;
            actionParameter.Value = new Dictionary<eValueNumber, FloatWithEx>((IEqualityComparer<eValueNumber>)new eValueNumber_DictComparer());
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
            ActionParameterOnPrefab parameterOnPrefab = skill.ActionParametersOnPrefab.Find((Predicate<ActionParameterOnPrefab>)(e => e.ActionType == actionParameter.ActionType));
            if (parameterOnPrefab == null)
            {
                parameterOnPrefab = new ActionParameterOnPrefab()
                {
                    ActionType = actionParameter.ActionType
                };
                skill.ActionParametersOnPrefab.Add(parameterOnPrefab);
            }
            ActionParameterOnPrefabDetail parameterOnPrefabDetail;
            if (index1 >= parameterOnPrefab.Details.Count)
            {
                parameterOnPrefabDetail = new ActionParameterOnPrefabDetail()
                {
                    ExecTime = new List<ActionExecTime>()
                };
                parameterOnPrefab.Details.Add(parameterOnPrefabDetail);
            }
            else
            {
                parameterOnPrefabDetail = parameterOnPrefab.Details[index1];
                parameterOnPrefabDetail.ExecTime = new List<ActionExecTime>((IEnumerable<ActionExecTime>)parameterOnPrefabDetail.ExecTimeForPrefab);
                int index2 = 0;
                for (int count = parameterOnPrefabDetail.ExecTimeCombo.Count; index2 < count; ++index2)
                {
                    ActionExecTimeCombo actionExecTimeCombo = parameterOnPrefabDetail.ExecTimeCombo[index2];
                    for (int index3 = 0; index3 < actionExecTimeCombo.Count; ++index3)
                    {
                        switch (actionExecTimeCombo.InterporationType)
                        {
                            case eComboInterporationType.LINEAR:
                                parameterOnPrefabDetail.ExecTime.Add(new ActionExecTime()
                                {
                                    Time = actionExecTimeCombo.StartTime + (float)index3 * actionExecTimeCombo.OffsetTime,
                                    DamageNumType = eDamageEffectType.COMBO
                                });
                                break;
                            case eComboInterporationType.CURVE:
                                parameterOnPrefabDetail.ExecTime.Add(new ActionExecTime()
                                {
                                    Time = actionExecTimeCombo.StartTime + actionExecTimeCombo.Curve.Evaluate((float)index3 / (float)actionExecTimeCombo.Count) * (float)actionExecTimeCombo.Count * actionExecTimeCombo.OffsetTime,
                                    DamageNumType = eDamageEffectType.COMBO
                                });
                                break;
                        }
                    }
                }
                parameterOnPrefabDetail.ExecTime.Sort((Comparison<ActionExecTime>)((a, b) => a.Time.CompareTo(b.Time)));
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
            actionParameter.DepenedActionId = actionParam.DependActionId;
            actionParameter.ActionId = (int)actionParam.action_id;
            actionParameter.TargetList = new List<BasePartsData>();
            actionParameter.ActionChildrenIndexes = new List<int>();
            actionParameter.TargetAssignment = (eTargetAssignment)(int)actionParam.target_assignment;
            actionParameter.Direction = (DirectionType)(int)actionParam.target_area;
            skill.ActionParameters.Add(actionParameter);
            skill.SkillId = (int)skillParameter.skill_id;
        }

        public bool StartAction(int skillId)
        {
            if (!skillDictionary.ContainsKey(skillId))
            {
                PCRCaculator.MainManager.Instance.WindowConfigMessage("角色" + Owner.UnitId + "的技能" + skillId + "错误！", null);
                return false;
            }
            Skill skill = this.skillDictionary[skillId];
            skill.DefeatEnemyCount = 0;
            skill.DefeatByThisSkill = false;
            skill.AlreadyAddAttackSelfSeal = false;
            skill.AlreadyExexActionByHit = false;
            skill.LifeSteal = 0;
            skill.Cancel = false;
            skill.EffectBranchId = 0;
            skill.LoopEffectAlreadyDone = false;
            this.ContinuousActionEndDone = false;
            skill.OwnerReturnPosition = this.transform.localPosition;
            skill.CountBlind = false;
            skill.AbsorberValue = this.battleManager.KIHOGJBONDH;
            if (skill.HasAttack && skill.IsLifeStealEnabled)
            {
                for (int index = this.Owner.LifeStealQueueList.Count - 1; index >= 0; --index)
                {
                    skill.LifeSteal += this.Owner.LifeStealQueueList[index].Dequeue();
                    if (this.Owner.LifeStealQueueList[index].Count == 0)
                    {
                        this.Owner.LifeStealQueueList.RemoveAt(index);
                        this.Owner.OnChangeState.Call<UnitCtrl, eStateIconType, bool>(this.Owner, eStateIconType.BUFF_ADD_LIFE_STEAL, false);
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
                actionParameter1.AdditionalValue = (Dictionary<eValueNumber, FloatWithEx>)null;
                actionParameter1.MultipleValue = (Dictionary<eValueNumber, FloatWithEx>)null;
                actionParameter1.DivideValue = (Dictionary<eValueNumber, FloatWithEx>)null;
                if (!actionParameter1.ReferencedByReflection)
                {
                    if (!actionParameter1.IsSearchAndSorted)
                        this.searchAndSortTarget(skill, actionParameter1, this.transform.position);
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
                                _basePosition = this.transform.position + new Vector3((float)((this.Owner.IsLeftDir ? -1.0 : 1.0) * (double)actionParameter1.Value[eValueNumber.VALUE_1] / 540.0), 0.0f);
                                break;
                            case 3:
                                _basePosition = this.transform.position;
                                num = (this.Owner.IsLeftDir ? -1f : 1f) * actionParameter1.Value[eValueNumber.VALUE_1];
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
                            this.searchAndSortTarget(skill, actionParameter2, _basePosition, _considerBodyWidth: _considerBodyWidth);
                        }
                    }
                    if (actionParameter1.TargetList.Count != 0)
                        flag1 = false;
                }
            }
            bool flag2 = false;
            if ((double)skill.BlackOutTime > 0.0)
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
                            this.battleManager.AddBlackOutTarget(this.Owner, owner, actionParameter.TargetList[index2]);
                            if ((UnityEngine.Object)this.Owner != (UnityEngine.Object)owner && skill.DisableOtherCharaOnStart)
                                owner.GetCurrentSpineCtrl().CurColor = new Color(1f, 1f, 1f, 0.0f);
                        }
                    }
                }
                this.battleManager.StartChangeScale(skill, skill.CutInSkipTime);
            }
            if (skillId == this.Owner.UnionBurstSkillId)
            {
                this.battleManager.SetSkillExeScreen(this.Owner, skill.BlackOutTime, skill.BlackoutColor, skill.BlackoutEndWithMotion || skill.IsPrincessForm);
                //this.battleTimeScale.StopSlowEffect();
                //if (skill.SlowEffect.Enable)
                //this.battleTimeScale.StartSlowEffect(skill.SlowEffect, this.Owner, skill.CutInSkipTime, false);
                if (this.Owner.IsOther == this.battleManager.IsDefenceReplayMode)
                    flag2 = true;
            }
            bool flag3 = flag2 || this.Owner.IsBoss;
            //if (flag3)
            //SingletonMonoBehaviour<BattleHeaderController>.Instance.SkillWindow.Indicate(skill.SkillName, this.Owner.IsBoss);
            BattleSpineController currentSpineCtrl = this.Owner.GetCurrentSpineCtrl();
            if (skill.ForcePlayNoTarget)
                flag1 = false;
            if (flag1 || this.Owner.IsAbnormalState(UnitCtrl.eAbnormalState.SILENCE) && !this.Owner.AttackWhenSilence)
            {
                if (this.Owner.IsBoss && !this.Owner.IsConfusionOrConvert() && (!skill.IsPrincessForm && currentSpineCtrl.IsAnimation(skill.AnimId, skill.SkillNum, _index3: this.Owner.MotionPrefix)))
                {
                    this.Owner.PlayAnime(skill.AnimId, skill.SkillNum, _index3: this.Owner.MotionPrefix, _isLoop: false);
                    this.CreateNormalPrefabWithTargetMotion(skill, 0, true);
                }
                return false;
            }
            if (!flag3)
                this.Owner.IndicateSkillName(skill.SkillName);
            /*for (int index = 0; index < skill.ShakeEffects.Count; ++index)
            {
              if (skill.ShakeEffects[index].TargetMotion == 0)
                this.AppendCoroutine(this.StartShakeWithDelay(skill.ShakeEffects[index], skill, true), ePauseType.VISUAL, (double) skill.BlackOutTime > 0.0 ? this.Owner : (UnitCtrl) null);
            }*/
            //this.startBlurEffects(skill, true);
            if (skill.AnimId != eSpineCharacterAnimeId.NONE && !skill.IsPrincessForm && currentSpineCtrl.IsAnimation(skill.AnimId, skill.SkillNum, _index3: this.Owner.MotionPrefix))
            {
                if (this.Owner.IsCutInSkip)
                {
                    this.Owner.IsCutInSkip = false;
                    this.Owner.RestartPlayAnimeCoroutine(skill.CutInSkipTime, skill.AnimId, skill.SkillNum, this.Owner.MotionPrefix);
                }
                else
                    this.Owner.PlayAnime(skill.AnimId, skill.SkillNum, _index3: this.Owner.MotionPrefix, _isLoop: false, _startTime: skill.CutInSkipTime);
            }
            skill.ReadySkill();
            skill.AweValue = this.Owner.CalcAweValue(skillId == this.Owner.UnionBurstSkillId, skillId == 1);
            bool effectFlag = false;
            for (int index = 0; index < skill.ActionParameters.Count; ++index)
            {
                ActionParameter actionParameter = skill.ActionParameters[index];
                actionParameter.ReadyAction(this.Owner, this, skill);
                //if ((!hasParentIndexes.Contains(index) || actionParameter.ReferencedByReflection) && !actionParameter.ReferencedByEffect)
                //    this.ExecUnitActionWithDelay(actionParameter, skill, true, true);
                if (!hasParentIndexes.Contains(index) || actionParameter.ReferencedByReflection)
                {
                    if (!actionParameter.ReferencedByEffect)
                    {
                        this.ExecUnitActionWithDelay(actionParameter, skill, true, true);
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
                            this.ExecUnitActionWithDelay(actionParameter, skill, true, true);
                        }
                    }
                }

            }
            for (int index = 0; index < this.battleManager.BlackoutUnitTargetList.Count; ++index)
                this.battleManager.BlackoutUnitTargetList[index].DisableSortOrderFrontOnBlackoutTarget = false;
            if(UseSkillEffect && effectFlag)
                this.CreateNormalPrefabWithTargetMotion2(skill, 0, true);
            //if (skill.ZoomEffect.Enable)
            // this.battleCameraEffect.StartZoomEffect(skill.ZoomEffect, this.Owner, skill.CutInSkipTime, false, false, skill.SkillId != this.Owner.UnionBurstSkillId);
            this.Owner.StartChangeSortOrder(skill.ChangeSortDatas, skill.CutInSkipTime);
            //added Scripts
            PCRCaculator.Guild.UnitSkillExecData skillExecData = new PCRCaculator.Guild.UnitSkillExecData();
            skillExecData.skillID = skillId;
            skillExecData.skillName = skill.SkillName;
            skillExecData.skillState = PCRCaculator.Guild.UnitSkillExecData.SkillState.NORMAL;
            skillExecData.startTime = BattleHeaderController.CurrentFrameCount;
            skillExecData.unitid = Owner.UnitId;
            skillExecData.UnitName = Owner.UnitName;
            Owner.MyOnStartAction?.Invoke(Owner.UnitId, skillExecData);
            Owner.AppendStartSkill(skillId);
            //end added Scripts
            return true;
        }
        private void searchAndSortTarget(
          Skill _skill,
          ActionParameter _action,
          Vector3 _basePosition,
          bool _quiet = false,
          bool _considerBodyWidth = true)
        {
            this.searchTargetUnit(_action, _basePosition, _skill, _considerBodyWidth);
            this.sortTargetListByTargetPattern(_action, this.Owner.BottomTransform, _action.Value[eValueNumber.VALUE_1], _quiet);
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
            BasePartsData basePartsData = (BasePartsData)null;
            if (_action.TargetAssignment == eTargetAssignment.OTHER_SIDE && _action.TargetNum == 1 && !_action.IgnoreDecoy)
            {
                if (this.Owner.IsOther)
                {
                    if (this.Owner.IsConfusionOrConvert())
                    {
                        if ((UnityEngine.Object)this.battleManager.DecoyEnemy != (UnityEngine.Object)null)
                            basePartsData = this.battleManager.DecoyEnemy.GetFirstParts(_basePos: this.Owner.transform.position.x);
                    }
                    else if ((UnityEngine.Object)this.battleManager.DecoyUnit != (UnityEngine.Object)null)
                        basePartsData = this.battleManager.DecoyUnit.GetFirstParts(_basePos: this.Owner.transform.position.x);
                }
                else if (this.Owner.IsConfusionOrConvert())
                {
                    if ((UnityEngine.Object)this.battleManager.DecoyUnit != (UnityEngine.Object)null)
                        basePartsData = this.battleManager.DecoyUnit.GetFirstParts(_basePos: this.Owner.transform.position.x);
                }
                else if ((UnityEngine.Object)this.battleManager.DecoyEnemy != (UnityEngine.Object)null)
                    basePartsData = this.battleManager.DecoyEnemy.GetFirstParts(_basePos: this.Owner.transform.position.x);
                if (basePartsData != null && !basePartsData.Owner.IsDead)
                {
                    //if ((double)Mathf.Abs(basePartsData.GetLocalPosition().x - this.transform.localPosition.x) <= (double)_action.TargetWidth + (double)basePartsData.GetBodyWidth() / 2.0 + (double)this.Owner.BodyWidth / 2.0)
                    if ((double)Mathf.Abs(basePartsData.GetLocalPosition().x - this.transform.localPosition.x) <= (double)_action.TargetWidth + (double)basePartsData.GetBodyWidth() / 4.0 + (double)this.Owner.BodyWidth / 4.0)
                    {
                        _action.TargetList.Clear();
                        _action.TargetList.Add(basePartsData);
                    }
                }
            }
            _action.IsSearchAndSorted = true;
        }

        private void setCutInSkipTimeForPrincessForm(int _skillId)
        {
            Skill skill = this.skillDictionary[_skillId];
            if (!skill.IsPrincessForm)
                return;
            skill.CutInSkipTime = 0.0f;
        }

        public IEnumerator StartActionWithOutCutIn(
          UnitCtrl _unit,
          int _skillId,
          System.Action _callback)
        {
            UnitActionController actionController = this;
            //actionController.battleTimeScale.StopSlowEffect();
            Skill skill = actionController.skillDictionary[_skillId];
            BattleSpineController unitSpineController = actionController.Owner.GetCurrentSpineCtrl();
            TrackEntry entry = unitSpineController.state.GetCurrent(0);
            float deltaTimeAccumulated = 0.0f;
            //BattleHeaderController battleHeaderController = SingletonMonoBehaviour<BattleHeaderController>.Instance;
            actionController.battleManager.SetSkillExeScreenActive(_unit, Color.black);
            actionController.Owner.SetSortOrderFront();
            actionController.AddBlackoutTarget(_skillId);
            if (skill.IsPrincessForm)
            {
                yield return (object)null;
                _callback.Call();
            }
            else
            {
                actionController.Owner.PlayAnime(skill.AnimId, skill.SkillNum, _index3: actionController.Owner.MotionPrefix, _isLoop: false);
                for (int index = 0; index < skill.SkillEffects.Count; ++index)
                {
                    NormalSkillEffect skillEffect = skill.SkillEffects[index];
                    if (skillEffect.TargetMotionIndex == 0)
                    {
                        skill.Cancel = false;
                        //actionController.StartCoroutine(actionController.updateCoroutineWithOutCutIn(actionController.createNormalPrefabWithDelay(skillEffect, skill, _skipCutIn: true)));
                    }
                }
                //if (skill.ZoomEffect.Enable)
                //  actionController.battleCameraEffect.StartZoomEffect(skill.ZoomEffect, actionController.Owner, 0.0f, true, false, false);
                if (skill.SkillId == actionController.Owner.UnionBurstSkillId)
                    actionController.Owner.StartChangeSortOrder(skill.ChangeSortDatas, 0.0f);
                while (!actionController.battleManager.CoroutineManager.VisualPause)
                    yield return (object)null;
                actionController.Timer((System.Action)(() => this.battleManager.SetSkillExeScreenActive(_unit, skill.BlackoutColor)));
                actionController.Owner.GetCurrentSpineCtrl().state.TimeScale = 1f;
                actionController.Owner.IsCutInSkip = true;
                entry = unitSpineController.state.GetCurrent(0);
                while (true)
                {
                    if (!BattleHeaderController.Instance.IsPaused)
                        deltaTimeAccumulated += Time.deltaTime;
                    for (; (double)deltaTimeAccumulated > 0.0; deltaTimeAccumulated -= actionController.battleManager.DeltaTime_60fps)
                    {
                        if (!actionController.battleManager.BlackoutUnitTargetList.Contains(actionController.Owner))
                        {
                            unitSpineController.RealUpdate();
                            unitSpineController.RealLateUpdate();
                        }
                        actionController.battleManager.BlackoutUnitTargetList.ForEach((System.Action<UnitCtrl>)(_it => _it.GetCurrentSpineCtrl().RealUpdate()));
                        actionController.battleManager.BlackoutUnitTargetList.ForEach((System.Action<UnitCtrl>)(_it => _it.GetCurrentSpineCtrl().RealLateUpdate()));
                        actionController.Owner.EffectSpineControllerList.ForEach((System.Action<BattleSpineController>)(_fx => _fx.RealUpdate()));
                        actionController.Owner.EffectSpineControllerList.ForEach((System.Action<BattleSpineController>)(_fx => _fx.RealLateUpdate()));
                    }
                    if ((double)entry.TrackTime < (double)skill.CutInSkipTime)//TrackTime为修改过的
                        yield return (object)null;
                    else
                        break;
                }
                actionController.Owner.EffectSpineControllerList.Clear();
                _callback.Call();
            }
        }

        public IEnumerator StartAnnihilationSkillAnimation(int _annihilationId)
        {
            UnitActionController actionController = this;
            actionController.Owner.Pause = false;
            //actionController.battleTimeScale.StopSlowEffect();
            Skill annihilation = actionController.Annihilation;
            BattleSpineController unitSpineController = actionController.Owner.GetCurrentSpineCtrl();
            //BattleHeaderController instance = SingletonMonoBehaviour<BattleHeaderController>.Instance;
            unitSpineController.AnimationName = unitSpineController.ConvertAnimeIdToAnimeName(eSpineCharacterAnimeId.ANNIHILATION, _index3: actionController.Owner.MotionPrefix);
            for (int index = 0; index < annihilation.SkillEffects.Count; ++index)
            {
                NormalSkillEffect skillEffect = annihilation.SkillEffects[index];
                if (skillEffect.TargetMotionIndex == 0)
                {
                    annihilation.Cancel = false;
                    //actionController.StartCoroutine(actionController.updateCoroutineWithOutCutIn(actionController.createNormalPrefabWithDelay(skillEffect, annihilation)));
                }
            }
            //actionController.battleCameraEffect.ClearShake();
            /*if (annihilation.ZoomEffect.Enable)
            {
              ++actionController.Owner.UbCounter;
              actionController.battleCameraEffect.StartZoomEffect(annihilation.ZoomEffect, actionController.Owner, 0.0f, true, true);
            }*/
            //foreach (ShakeEffect shakeEffect in annihilation.ShakeEffects)
            //  actionController.Owner.StartCoroutine(actionController.updateCoroutineWithOutCutIn(actionController.StartShakeWithDelay(shakeEffect, annihilation)));
            //if (annihilation.SlowEffect.Enable)
            //actionController.battleTimeScale.StartSlowEffect(annihilation.SlowEffect, actionController.Owner, 0.0f, true);
            if ((double)annihilation.BlackOutTime > 0.0)
            {
                actionController.battleManager.SetForegroundEnable(false);
                actionController.StartCoroutine(actionController.updateCoroutineWithOutCutIn(actionController.foregroundActiveWithDelay(annihilation.BlackOutTime)));
            }
            actionController.Owner.GetCurrentSpineCtrl().state.TimeScale = 1f;
            while (_annihilationId == actionController.Owner.AnnihilationId)
            {
                for (float num = 0.0f + Time.deltaTime; (double)num > 0.0; num -= actionController.battleManager.DeltaTime_60fps)
                {
                    //actionController.battleCameraEffect.UpdateShake();
                    if (!unitSpineController.IsPlayAnime)
                    {
                        unitSpineController.AnimationName = unitSpineController.ConvertAnimeIdToAnimeName(eSpineCharacterAnimeId.IDLE);
                        unitSpineController.loop = true;
                    }
                }
                yield return (object)null;
            }
        }

        private IEnumerator foregroundActiveWithDelay(float _time)
        {
            yield return (object)new WaitForSeconds(_time);
            this.battleManager.SetForegroundEnable(true);
        }

        public void AddBlackoutTarget(int _skillId)
        {
            Skill skill = this.skillDictionary[_skillId];
            int index1 = 0;
            for (int count1 = skill.ActionParameters.Count; index1 < count1; ++index1)
            {
                ActionParameter actionParameter1 = skill.ActionParameters[index1];
                if (!actionParameter1.ReferencedByReflection)
                {
                    this.searchAndSortTarget(skill, actionParameter1, this.transform.position, true);
                    for (int index2 = 0; index2 < actionParameter1.TargetNum; ++index2)
                    {
                        if (actionParameter1.TargetList.Count > index2)
                        {
                            UnitCtrl owner1 = actionParameter1.TargetList[index2].Owner;
                            if (actionParameter1.TargetAssignment == eTargetAssignment.OTHER_SIDE)
                                owner1.IsScaleChangeTarget = true;
                            this.battleManager.AddBlackOutTarget(this.Owner, owner1, actionParameter1.TargetList[index2]);
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
                                        _basePosition = this.transform.position + new Vector3((float)((this.Owner.IsLeftDir ? -1.0 : 1.0) * (double)actionParameter1.Value[eValueNumber.VALUE_1] / 540.0), 0.0f);
                                        break;
                                    case 3:
                                        _basePosition = this.transform.position;
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
                                    this.searchAndSortTarget(skill, actionParameter2, _basePosition, _considerBodyWidth: _considerBodyWidth);
                                    for (int index4 = 0; index4 < actionParameter2.TargetNum && index4 != actionParameter2.TargetList.Count; ++index4)
                                    {
                                        UnitCtrl owner2 = actionParameter2.TargetList[index4].Owner;
                                        if (actionParameter1.TargetAssignment == eTargetAssignment.OTHER_SIDE)
                                            owner2.IsScaleChangeTarget = true;
                                        this.battleManager.AddBlackOutTarget(this.Owner, owner2, actionParameter2.TargetList[index4]);
                                    }
                                }
                            }
                        }
                    }
                    this.battleManager.StartChangeScale(skill, 0.0f);
                }
            }
        }

        public void SearchTargetByAction(int _skillId)
        {
            Skill skill = this.skillDictionary[_skillId];
            int index1 = 0;
            for (int count1 = skill.ActionParameters.Count; index1 < count1; ++index1)
            {
                ActionParameter actionParameter1 = skill.ActionParameters[index1];
                if (!actionParameter1.ReferencedByReflection)
                {
                    this.searchAndSortTarget(skill, actionParameter1, this.transform.position, true);
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
                                    _basePosition = this.transform.position + new Vector3((float)((this.Owner.IsLeftDir ? -1.0 : 1.0) * (double)actionParameter1.Value[eValueNumber.VALUE_1] / 540.0), 0.0f);
                                    break;
                                case 3:
                                    _basePosition = this.transform.position;
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
                                this.searchAndSortTarget(skill, actionParameter2, _basePosition, _considerBodyWidth: _considerBodyWidth);
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
            yield return (object)null;
            //if (!SingletonMonoBehaviour<BattleHeaderController>.Instance.IsPaused)
            deltaTimeAccumulated += Time.deltaTime;
            for (; (double)deltaTimeAccumulated > 0.0; deltaTimeAccumulated -= this.battleManager.DeltaTime_60fps)
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
                        this.battleManager.StartCoroutine(this.createNormalPrefabWithDelay(skillEffect, _skill, _first));
                    }
                    else
                    {
                        ePauseType pauseType = flag ? ePauseType.VISUAL : ePauseType.SYSTEM;
                        UnitCtrl unit = (double)_skill.BlackOutTime > 0.0 ? this.Owner : (UnitCtrl)null;
                        this.AppendCoroutine(this.createNormalPrefabWithDelay(skillEffect, _skill, _first, _isFirearmEndEffect: _modechangeEndEffect, _modeChangeEndEffect: _modechangeEndEffect), pauseType, unit);
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
                _action.OnInitWhenNoTarget.Call();
            if (_action.ActionType == eActionType.CONTINUOUS_ATTACK)
            {
                //if (this.Owner.GetCurrentSpineCtrl().IsAnimation(_skill.AnimId, _skill.SkillNum, 1))
                //  this.AppendCoroutine((_action as ContinuousAttackAction).UpdateMotionRoopForContinuousDamage(_skill, this.Owner, this), ePauseType.SYSTEM, (double) _skill.BlackOutTime > 0.0 ? this.Owner : (UnitCtrl) null);
                // _action.ContinuousTargetCount = 0;
            }
            for (int index = 0; index < _action.TargetList.Count && (index < _action.TargetNum || _action.TargetNum == 0 || index == 0); ++index)
            {
                BasePartsData target = _action.TargetList[index];
                this.AppendCoroutine(this.ExecActionWithDelayAndTarget(_action, _skill, target, 0.0f, _first, _boneCount, _ignoreCancel), ePauseType.SYSTEM, (double)_skill.BlackOutTime > 0.0 ? this.Owner : (UnitCtrl)null);
            }
        }

        public void ExecUnitActionNoDelay(ActionParameter _action, Skill _skill)
        {
            if (_action.TargetList.Count == 0)
                _action.OnInitWhenNoTarget.Call();
            for (int index = 0; index < _action.TargetList.Count && (index < _action.TargetNum || _action.TargetNum == 0 || index == 0); ++index)
            {
                BasePartsData target = _action.TargetList[index];
                this.ExecAction(_action, _skill, target, 0, 0.0f);
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
                while (this.battleManager.PBCLBKCKHAI.Contains(int64 * 100L + ids[i]))
                    ++ids[i];
                long actionIndivisualId = int64 * 100L + ids[i];
                _target.Owner.ActionsTargetOnMe.Add(actionIndivisualId);
                this.battleManager.PBCLBKCKHAI.Add(actionIndivisualId);
                float waitTime = _action.ExecTime[i];
                while (true)
                {
                    time += this.battleManager.DeltaTime_60fps;
                    if (_ignoreCancel || !_skill.Cancel && !_action.CancelByIfForAll)
                    {
                        if ((double)waitTime > (double)time && (_action.ActionType != eActionType.TRIGER || _action.ActionDetail1 != 4))
                            yield return (object)null;
                        else
                            goto label_17;
                    }
                    else
                        break;
                }
                if (_action.OnActionEnd != null)
                    _action.OnActionEnd();
                _target.Owner.ActionsTargetOnMe.Remove(actionIndivisualId);
                this.battleManager.CallbackActionEnd(actionIndivisualId);
                break;
            label_17:
                this.ExecAction(_action, _skill, _target, i, time);
                if ((_action.ActionType != eActionType.CONTINUOUS_ATTACK || _action.ActionDetail1 != 3) && (_action.ActionType != eActionType.MODE_CHANGE || _action.ActionDetail1 == 3))
                {
                    _target.Owner.ActionsTargetOnMe.Remove(actionIndivisualId);
                    this.battleManager.CallbackActionEnd(actionIndivisualId);
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
            Func<ActionParameter, eValueNumber, FloatWithEx> func = (Func<ActionParameter, eValueNumber, FloatWithEx>)((_action, _type) =>
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
           });
            Dictionary<eValueNumber, FloatWithEx> _valueDictionary = new Dictionary<eValueNumber, FloatWithEx>((IEqualityComparer<eValueNumber>)new eValueNumber_DictComparer())
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
            bool isResisted = target.ResistStatus(action.ActionType, action.ActionDetail1, this.Owner, _last, _targetOneParts, target);
            bool exec = !isResisted && action.JudgeIsAlreadyExeced(target.Owner, num);
            PCRCaculator.Guild.UnitActionExecData actionExecData = new PCRCaculator.Guild.UnitActionExecData();
            actionExecData.execTime = BattleHeaderController.CurrentFrameCount;
            actionExecData.describe = "";//"执行失败，原因：" + (isResisted?"<color=#FF0000>被抵抗</color>": "<color=#FF0000>已经执行过了</color>");
            actionExecData.actionID = action.ActionId;
            actionExecData.unitid = Owner.UnitId;
            actionExecData.actionType = action.ActionType.GetDescription();
            actionExecData.targetNames = new List<string> { target.Owner.UnitName };
            if (exec)
            {
                int index = skill.ActionParameters.FindIndex(a => a == action) + 1;
                int index2 = skill.ActionParameters.Count;
                Owner.UIManager.LogMessage("执行技能" + skill.SkillName + "(" + index + "/" + index2 + ")" + ",目标" + target.Owner.UnitName, PCRCaculator.Battle.eLogMessageType.EXEC_ACTION, this.Owner);
                action.ExecAction(this.Owner, target, num, this, skill, starttime, dictionary, 
                    _valueDictionary,(a)=> 
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
            actionExecData.result = isResisted ? PCRCaculator.Guild.UnitActionExecData.ActionState.MISS : PCRCaculator.Guild.UnitActionExecData.ActionState.NORMAL;
            Owner.MyOnExecAction?.Invoke(Owner.UnitId, skill.SkillId, actionExecData);
            //change end
            if (action.ActionType == eActionType.REFLEXIVE)
                return;
            if (!action.HitOnceDic.ContainsKey(target))
            {
                this.ExecChildrenAction(action, skill, target, num, starttime, dictionary);
            }
            else
            {
                if (num != action.ExecTime.Length - 1)
                    return;
                if (action.HitOnceDic[target])
                {
                    this.ExecChildrenAction(action, skill, target, num, starttime, dictionary);
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
                int actionChildrenIndex = action.ActionChildrenIndexes[index];
                if ((skill.ActionParameters[actionChildrenIndex].ActionType != eActionType.MODE_CHANGE || skill.ActionParameters[actionChildrenIndex].ActionDetail1 != 3) && (!enabledChildAction.ContainsKey(skill.ActionParameters[actionChildrenIndex].ActionId) || enabledChildAction[skill.ActionParameters[actionChildrenIndex].ActionId]) && (action.ActionType != eActionType.ATTACK || num == action.ExecTime.Length - 1))
                    this.AppendCoroutine(this.ExecActionWithDelayAndTarget(skill.ActionParameters[actionChildrenIndex], skill, target, starttime), ePauseType.SYSTEM, (double)skill.BlackOutTime > 0.0 ? this.Owner : (UnitCtrl)null);
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
            string key = this.Owner.IsLeftDir || this.Owner.IsForceLeftDirOrPartsBoss ? _skilleffect.PrefabLeft.name : _skilleffect.Prefab.name;
            if (this.battleManager.DAIFDPFABCM.ContainsKey(key))
            {
              GameObject gameObject = this.battleManager.DAIFDPFABCM[key];
              if ((UnityEngine.Object) gameObject == (UnityEngine.Object) null)
              {
                yield break;
              }
              else
              {
                this.battleManager.DAIFDPFABCM.Remove(key);
                UnityEngine.Object.Destroy((UnityEngine.Object) gameObject);
              }
            }
            else if (_skilleffect.IsReaction)
              this.battleManager.DAIFDPFABCM.Add(key, (GameObject) null);
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
                if (_skill.AnimId != eSpineCharacterAnimeId.ATTACK || !this.UseDefaultDelay || (_skilleffect.EffectBehavior != eEffectBehavior.FIREARM || !BattleDefine.WEAPON_EFFECT_DELAY_DIC.TryGetValue(this.Owner.WeaponMotionType, out waitTime)))
                    waitTime = execTime.Length - 1 >= execTimeIndex ? execTime[execTimeIndex] : (execTime.Length < 2 ? execTime[0] : (execTime[execTime.Length - 1] - execTime[execTime.Length - 2]) * (float)(execTimeIndex - execTime.Length + 1) + execTime[execTime.Length - 1]);
                if (!execed[execTimeIndex])
                {
                    float startTime = 0.0f;
                    if (!_skipCutIn || (double)waitTime <= (double)_skill.CutInSkipTime && !BattleUtil.Approximately(waitTime, _skill.CutInSkipTime))
                    {
                        if (!_skipCutIn && (double)waitTime < (double)_skill.CutInSkipTime && !_isFirearmEndEffect)
                        {
                            if (_skilleffect.PlayWithCutIn && this.Owner.PlayCutInFlag && this.Owner.MovieId != 0)
                                startTime = _skill.CutInSkipTime - waitTime;
                            else
                                continue;
                        }
                        do
                        {
                            yield return (object)null;
                            time += this.battleManager.DeltaTime_60fps;
                            if (_skill.Cancel || !this.Owner.gameObject.activeSelf || _modeChangeEndEffect && this.Owner.IsUnableActionState())
                                yield break;
                        }
                        while ((double)waitTime > (double)time);
                        execed[execTimeIndex] = true;
                        BasePartsData _firearmEndTarget = (BasePartsData)null;
                        if (_skilleffect.EffectBehavior == eEffectBehavior.FIREARM || _skilleffect.EffectBehavior == eEffectBehavior.SERIES_FIREARM)
                        {
                            switch (_skilleffect.FireArmEndTarget)
                            {
                                case eEffectTarget.OWNER:
                                    _firearmEndTarget = this.Owner.GetFirstParts(true);
                                    break;
                                case eEffectTarget.ALL_TARGET:
                                    _firearmEndTarget = _skilleffect.TargetAction.TargetList[execTimeIndex];
                                    break;
                                case eEffectTarget.FORWARD_TARGET:
                                case eEffectTarget.BACK_TARGET:
                                    bool flag1 = _skilleffect.FireArmEndTarget == eEffectTarget.FORWARD_TARGET == !this.Owner.IsOther;
                                    List<BasePartsData> basePartsDataList1 = new List<BasePartsData>((IEnumerable<BasePartsData>)_skilleffect.TargetAction.TargetList.GetRange(0, Mathf.Min(_skilleffect.TargetAction.TargetNum, _skilleffect.TargetAction.TargetList.Count)));
                                    basePartsDataList1.Sort((Comparison<BasePartsData>)((a, b) => a.GetPosition().x.CompareTo(b.GetPosition().x)));
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
                                    basePartsDataList2.Sort((Comparison<BasePartsData>)((a, b) => a.GetPosition().x.CompareTo(b.GetPosition().x)));
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
          if ((double) _delay >= (double) time)
          {
            while (true)
            {
              time += this.battleManager.DeltaTime_60fps;
              if (!_skill.Cancel)
              {
                if ((double) _delay > (double) time)
                  yield return (object) null;
                else
                  goto label_6;
              }
              else
                break;
            }
            yield break;
    label_6:
            if (_skilleffect.EffectTarget == eEffectTarget.OWNER)
              this.createNormalEffectPrefab(_skilleffect, _skill, this.Owner.GetFirstParts(true), (BasePartsData) null, false, 0.0f, false);
            else
              this.createNormalEffectPrefab(_skilleffect, _skill, _target, (BasePartsData) null, false, 0.0f, false);
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
          if (_skillEffect.EffectBehavior == eEffectBehavior.SERIES_FIREARM && !(_skillEffect.FireAction is AttackAction) && (!(_skillEffect.FireAction is RatioDamageAction) && !(_skillEffect.FireAction is UpperLimitAttackAction)) && (!(_skillEffect.FireAction is HealAction) && _skillEffect.AppendAndJudgeAlreadyExeced(_firearmEndTarget.Owner)) || (_skillEffect.TargetBranchId != 0 && _skillEffect.TargetBranchId != _skill.EffectBranchId || _skillEffect.TargetMotionIndex == 1 && _skill.LoopEffectAlreadyDone))
            return;
          GameObject prefab1 = (GameObject) null;
          SkillEffectCtrl prefab2 = this.createPrefab(_skillEffect, _skill, _target, ref prefab1);
          prefab2.InitializeSort();
          prefab2.SetStartTime(_starttime);
          //prefab2.PlaySe(this.Owner.SoundUnitId, this.Owner.IsLeftDir);
          prefab2.SetPossitionAppearanceType(_skillEffect, _target, this.Owner, _skill);
          prefab2.ExecAppendCoroutine((double) _skill.BlackOutTime > 0.0 ? this.Owner : (UnitCtrl) null, prefab2 is AwakeUbNoneStopEffect);
          prefab2.SetTimeToDieByStartHp(this.Owner.StartHpPercent);
          if (_modeChangeEndEffect)
            this.Owner.ModeChangeEndEffectList.Add(prefab2);
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
              firearmCtrl.Initialize(_firearmEndTarget, _actions, _skill, actionStart ? _skillEffect.FireArmEndEffects : new List<NormalSkillEffect>(), this.Owner, _skillEffect.Height, (double) _skill.BlackOutTime > 0.0, _skillEffect.IsAbsoluteFireArm, this.transform.position + (this.Owner.IsLeftDir ? -1f : 1f) * new Vector3(_skillEffect.AbsoluteFireDistance, 0.0f), _skillEffect.ShakeEffects, _skillEffect.FireArmEndTargetBone);
              firearmCtrl.OnHitAction = _skillEffect.EffectBehavior != eEffectBehavior.FIREARM ? (System.Action<FirearmCtrl>) (fctrl =>
              {
                /*for (int index1 = 0; index1 < fctrl.ShakeEffects.Count; ++index1)
                {
                  if (fctrl.ShakeEffects[index1].TargetMotion == 0)
                    this.AppendCoroutine(this.StartShakeWithDelay(fctrl.ShakeEffects[index1], fctrl.Skill), ePauseType.VISUAL, (double) fctrl.Skill.BlackOutTime > 0.0 ? this.Owner : (UnitCtrl) null);
                }*/
                if (_skillEffect.FireActionId != -1)
                  this.AppendCoroutine(this.ExecActionWithDelayAndTarget(_skillEffect.FireAction, _skill, _firearmEndTarget, 0.0f), ePauseType.SYSTEM, (double) _skill.BlackOutTime > 0.0 ? this.Owner : (UnitCtrl) null);
                int index3 = 0;
                for (int count = _skillEffect.FireArmEndEffects.Count; index3 < count; ++index3)
                  this.AppendCoroutine(this.createNormalPrefabWithDelayAndTarget(_skillEffect.FireArmEndEffects[index3], _skill, 0.0f, _firearmEndTarget, false), ePauseType.VISUAL, (double) _skill.BlackOutTime > 0.0 ? this.Owner : (UnitCtrl) null);
              }) : new System.Action<FirearmCtrl>(this.onFirearmHit);
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
                vector3_1 += _skillEffect.TargetAction.TargetList[index2].GetPosition() / (float) count;
              prefab2.transform.position = vector3_1;
              break;
            case eEffectBehavior.SKILL_AREA_RANDOM:
              Vector3 vector3_2 = new Vector3();
              int attackSide = UnitActionController.GetAttackSide(_skillEffect.TargetAction.Direction, this.Owner);
              float num1 = attackSide < 1 ? -_skillEffect.TargetAction.TargetWidth : 0.0f;
              float num2 = attackSide > -1 ? _skillEffect.TargetAction.TargetWidth : 0.0f;
              float num3 = _skillEffect.TargetAction.ActionType == eActionType.REFLEXIVE ? _skillEffect.TargetAction.TargetList[0].GetPosition().x : this.transform.position.x;
              float num4 = num1 + num3;
              float num5 = num2 + num3;
              if ((double) num4 < -(double) BattleDefine.BATTLE_FIELD_SIZE)
                num4 = -BattleDefine.BATTLE_FIELD_SIZE;
              if ((double) num5 > (double) BattleDefine.BATTLE_FIELD_SIZE)
                num5 = BattleDefine.BATTLE_FIELD_SIZE;
              vector3_2.x = (float) (((double) num4 + ((double) num5 - (double) num4) * (double) BattleManager.Random(0.0f, 1f,
                  new PCRCaculator.Guild.RandomData(Owner,_target.Owner,_skill.SkillId,8,0))) / 540.0);
              vector3_2.y += (float) ((double) _skillEffect.CenterY + (double) _skillEffect.DeltaY * (double) BattleManager.Random(-1f, 1f,
                  new PCRCaculator.Guild.RandomData(Owner, _target.Owner, _skill.SkillId, 8, 0)) + 9.25925922393799);
              prefab2.transform.position = vector3_2;
              break;
            case eEffectBehavior.SERIES:
              if (_skillEffect.FireActionId != -1)
              {
                this.AppendCoroutine(this.ExecActionWithDelayAndTarget(_skillEffect.FireAction, _skill, _target, 0.0f), ePauseType.SYSTEM, (double) _skill.BlackOutTime > 0.0 ? this.Owner : (UnitCtrl) null);
                break;
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
          prefab = this.Owner.IsLeftDir || this.Owner.IsForceLeftDirOrPartsBoss ? skillEffect.PrefabLeft : skillEffect.Prefab;
          string name = prefab.name;
          SkillEffectCtrl effect = this.battleEffectPool.GetEffect(prefab,skillEffect.PrefabData, this.Owner);
          effect.transform.parent = ExceptNGUIRoot.Transform;
          skill.EffectObjs.Add(effect);
          if (skillEffect.TargetMotionIndex == 1)
          {
            skill.LoopEffectObjs.Add(effect);
            effect.OnEffectEnd += (System.Action<SkillEffectCtrl>) (obj => skill.LoopEffectObjs.Remove(obj));
          }
          if (effect.IsRepeat && !(effect is FirearmCtrl))
          {
            target.Owner.RepeatEffectList.Add(effect);
            effect.OnEffectEnd += (System.Action<SkillEffectCtrl>) (obj => target.Owner.RepeatEffectList.Remove(obj));
          }
          effect.OnEffectEnd += (System.Action<SkillEffectCtrl>) (destroyedEffect => skill.EffectObjs.Remove(destroyedEffect));
          if (skillEffect.IsReaction)
            this.battleManager.DAIFDPFABCM[name] = effect.gameObject;
          return effect;
        }

        private void onFirearmHit(FirearmCtrl firearmCtrl)
        {
          if ((UnityEngine.Object) firearmCtrl == (UnityEngine.Object) null || firearmCtrl.FireTarget == null || firearmCtrl.Skill == null)
            return;
          /*for (int index = 0; index < firearmCtrl.ShakeEffects.Count; ++index)
          {
            if (firearmCtrl.ShakeEffects[index].TargetMotion == 0)
              this.AppendCoroutine(this.StartShakeWithDelay(firearmCtrl.ShakeEffects[index], firearmCtrl.Skill), ePauseType.VISUAL, (double) firearmCtrl.Skill.BlackOutTime > 0.0 ? this.Owner : (UnitCtrl) null);
          }*/
          int index1 = 0;
          for (int count = firearmCtrl.EndActions.Count; index1 < count; ++index1)
            this.ExecUnitActionWithDelay(firearmCtrl.EndActions[index1], firearmCtrl.Skill, false, true);
          int index2 = 0;
          for (int count = firearmCtrl.SkillHitEffects.Count; index2 < count; ++index2)
            this.AppendCoroutine(this.createNormalPrefabWithDelay(firearmCtrl.SkillHitEffects[index2], firearmCtrl.Skill, _isFirearmEndEffect: true), ePauseType.VISUAL, (double) firearmCtrl.Skill.BlackOutTime > 0.0 ? this.Owner : (UnitCtrl) null);
        }

        private void searchTargetUnit(
          ActionParameter actionParameter,
          Vector3 basePosition,
          Skill skill,
          bool _considerBodyWidth)
        {
            float x = this.transform.parent.lossyScale.x;
            actionParameter.TargetList.Clear();
            bool isOther = this.Owner.IsOther;
            List<UnitCtrl> unitCtrlList1 = !this.Owner.IsConfusionOrConvert() ? (isOther ? this.battleManager.UnitList : this.battleManager.EnemyList) : (!isOther ? this.battleManager.UnitList : this.battleManager.EnemyList);
            List<UnitCtrl> unitCtrlList2 = isOther ? this.battleManager.EnemyList : this.battleManager.UnitList;
            List<UnitCtrl> unitCtrlList3 = actionParameter.TargetAssignment == eTargetAssignment.OTHER_SIDE ? unitCtrlList1 : unitCtrlList2;
            switch (actionParameter.TargetSort)
            {
                case PriorityPattern.SUMMON:
                case PriorityPattern.ALL_SUMMON_RANDOM:
                    unitCtrlList3 = unitCtrlList3.FindAll((Predicate<UnitCtrl>)(e => e.IsSummonOrPhantom));
                    break;
                case PriorityPattern.ATK_PHYSICS:
                    unitCtrlList3 = unitCtrlList3.FindAll((Predicate<UnitCtrl>)(e => e.AtkType == 1));
                    break;
                case PriorityPattern.ATK_MAGIC:
                    unitCtrlList3 = unitCtrlList3.FindAll((Predicate<UnitCtrl>)(e => e.AtkType == 2));
                    break;
                case PriorityPattern.OWN_SUMMON_RANDOM:
                    unitCtrlList3 = this.Owner.SummonUnitDictionary.Values.ToList<UnitCtrl>().FindAll((Predicate<UnitCtrl>)(e => !e.IdleOnly && !e.IsDead));
                    break;
                case PriorityPattern.NEAR_MY_TEAM_WITHOUT_OWNER:
                    unitCtrlList3 = unitCtrlList3.FindAll((Predicate<UnitCtrl>)(e => (UnityEngine.Object)e != (UnityEngine.Object)this.Owner));
                    break;
            }
            float num = actionParameter.TargetWidth;
            if (actionParameter.TargetSort == PriorityPattern.BACK || actionParameter.TargetSort == PriorityPattern.FORWARD)
            {
                int index1 = 0;
                for (int count = unitCtrlList3.Count; index1 < count; ++index1)
                {
                    UnitCtrl _unitCtrl = unitCtrlList3[index1];
                    if (this.judgeIsTarget(_unitCtrl, actionParameter))
                    {
                        if (_unitCtrl.IsPartsBoss)
                        {
                            for (int index2 = 0; index2 < _unitCtrl.BossPartsListForBattle.Count; ++index2)
                            {
                                PartsData partsData = _unitCtrl.BossPartsListForBattle[index2];
                                if (partsData.GetTargetable())
                                    actionParameter.TargetList.Add((BasePartsData)partsData);
                            }
                        }
                        else
                            actionParameter.TargetList.Add(_unitCtrl.GetFirstParts());
                    }
                }
            }
            else
            {
                int attackSide = UnitActionController.GetAttackSide(actionParameter.Direction, this.Owner);
                if ((double)num <= 0.0)
                    num = skill.SkillId == 1 ? this.Owner.SearchAreaSize : skill.skillAreaWidth;
                float _start = attackSide < 1 ? -num : 0.0f;
                float _end = attackSide > -1 ? num : 0.0f;
                BasePartsData basePartsData1 = (BasePartsData)null;
                int index1 = 0;
                for (int count = unitCtrlList3.Count; index1 < count; ++index1)
                {
                    UnitCtrl _unitCtrl = unitCtrlList3[index1];
                    if (!_unitCtrl.IsPartsBoss)
                    {
                        if (this.judgeIsInTargetArea(actionParameter, basePosition, _considerBodyWidth, x, _start, _end, _unitCtrl.GetFirstParts()))
                        {
                            if (this.judgeIsTarget(_unitCtrl, actionParameter))
                                actionParameter.TargetList.Add(_unitCtrl.GetFirstParts());
                            else if ((long)_unitCtrl.Hp == 0L)
                                basePartsData1 = _unitCtrl.GetFirstParts();
                        }
                    }
                    else
                    {
                        bool flag = actionParameter.TargetSort == PriorityPattern.HP_ASC || actionParameter.TargetSort == PriorityPattern.HP_DEC || (actionParameter.TargetSort == PriorityPattern.ENERGY_ASC || actionParameter.TargetSort == PriorityPattern.ENERGY_DEC) || actionParameter.TargetSort == PriorityPattern.ENERGY_REDUCING;
                        BasePartsData basePartsData2 = (BasePartsData)null;
                        for (int index2 = 0; index2 < _unitCtrl.BossPartsListForBattle.Count; ++index2)
                        {
                            PartsData partsData = _unitCtrl.BossPartsListForBattle[index2];
                            if (this.judgeIsInTargetArea(actionParameter, basePosition, _considerBodyWidth, x, _start, _end, (BasePartsData)partsData) && this.judgeIsTarget(_unitCtrl, actionParameter))
                            {
                                if (flag)
                                {
                                    if (basePartsData2 == null)
                                    {
                                        basePartsData2 = (BasePartsData)partsData;
                                    }
                                    else
                                    {
                                        float _a = Mathf.Abs(basePartsData2.GetPosition().x - this.Owner.transform.position.x);
                                        float _b = Mathf.Abs(partsData.GetPosition().x - this.Owner.transform.position.x);
                                        if ((double)_a > (double)_b || BattleUtil.Approximately(_a, _b))
                                            basePartsData2 = (BasePartsData)partsData;
                                    }
                                }
                                else
                                    actionParameter.TargetList.Add((BasePartsData)partsData);
                            }
                        }
                        if (basePartsData2 != null)
                            actionParameter.TargetList.Add(basePartsData2);
                    }
                }
                if (actionParameter.TargetSort != PriorityPattern.NEAR || actionParameter.TargetList.Count != 0 || (basePartsData1 == null || this.Owner.IsConfusionOrConvert()))
                    return;
                actionParameter.TargetList.Add(basePartsData1);
            }
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
            if (!_unitCtrl.GetTargetable())
                return false;
            float _a = (float)((double)_unitCtrl.GetPosition().x / (double)_parentLossyScale - (double)_basePosition.x / (double)_parentLossyScale);
            float bodyWidth = _unitCtrl.GetBodyWidth();
            if (_considerBodyWidth)            
                bodyWidth += this.Owner.BodyWidth;
            //add 
            /*if (_considerBodyWidth)
            {
                bodyWidth += Mathf.Min(this.Owner.BodyWidth,bodyWidth);

            }*/

            //end
            float _b1 = _start - bodyWidth * 0.5f;
            float _b2 = _end + bodyWidth * 0.5f;
            return (double)_a >= (double)_b1 && (double)_a <= (double)_b2 || (BattleUtil.Approximately(_a, _b1) || BattleUtil.Approximately(_a, _b2)) || _actionParameter.Direction == DirectionType.ALL;
        }

        private bool judgeIsTarget(UnitCtrl _unitCtrl, ActionParameter _actionParameter)
        {
            if (_unitCtrl.IsStealth || _unitCtrl.IsPhantom && _actionParameter.TargetSort != PriorityPattern.OWN_SUMMON_RANDOM || (_unitCtrl.IsDead || (long)_unitCtrl.Hp <= 0L) && !_unitCtrl.HasUnDeadTime)
                return false;
            return _actionParameter.TargetAssignment != eTargetAssignment.OTHER_SIDE || (UnityEngine.Object)this.Owner != (UnityEngine.Object)_unitCtrl;
        }

        public static int GetAttackSide(DirectionType _direction, UnitCtrl _owner) => _direction != DirectionType.FRONT ? 0 : (!_owner.IsLeftDir ? 1 : -1);

        private void sortTargetListByTargetPattern(
          ActionParameter _actionParameter,
          Transform _baseTransform,
          float _value,
          bool _quiet)
        {
            List<BasePartsData> targetList = _actionParameter.TargetList;
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
                                                    new PCRCaculator.Guild.RandomData(null,null,_actionParameter.ActionId,10,count));
                        --count;
                        BasePartsData basePartsData = targetList[index];
                        targetList[index] = targetList[count];
                        targetList[count] = basePartsData;
                    }
                    break;
                case PriorityPattern.NEAR:
                case PriorityPattern.NEAR_MY_TEAM_WITHOUT_OWNER:
                    this.Owner.BaseX = _baseTransform.position.x;
                    targetList.Sort(new Comparison<BasePartsData>(this.Owner.CompareDistanceAsc));
                    break;
                case PriorityPattern.FAR:
                    this.Owner.BaseX = _baseTransform.position.x;
                    targetList.Sort(new Comparison<BasePartsData>(this.Owner.CompareDistanceDec));
                    break;
                case PriorityPattern.HP_ASC:
                    UnitCtrl.QuickSort<BasePartsData>(targetList, new Func<BasePartsData, BasePartsData, int>(UnitCtrl.CompareLifeAsc));
                    break;
                case PriorityPattern.HP_DEC:
                    UnitCtrl.QuickSort<BasePartsData>(targetList, new Func<BasePartsData, BasePartsData, int>(UnitCtrl.CompareLifeDec));
                    break;
                case PriorityPattern.OWNER:
                    targetList.Clear();
                    targetList.Add(this.Owner.GetFirstParts(_basePos: (-BattleDefine.BATTLE_FIELD_SIZE)));
                    break;
                case PriorityPattern.FORWARD:
                case PriorityPattern.BACK:
                    bool _isForwardPattern = _actionParameter.TargetSort == PriorityPattern.FORWARD;
                    this.sortTargetPosition(targetList, _isForwardPattern, new Comparison<BasePartsData>(this.Owner.CompareLeft), new Comparison<BasePartsData>(this.Owner.CompareRight));
                    break;
                case PriorityPattern.ABSOLUTE_POSITION:
                    targetList.Clear();
                    targetList.Add(this.Owner.GetFirstParts(true));
                    break;
                case PriorityPattern.ENERGY_DEC:
                    UnitCtrl.QuickSort<BasePartsData>(targetList, new Func<BasePartsData, BasePartsData, int>(UnitCtrl.CompareEnergyDec));
                    break;
                case PriorityPattern.ENERGY_ASC:
                case PriorityPattern.ENERGY_REDUCING:
                    UnitCtrl.QuickSort<BasePartsData>(targetList, new Func<BasePartsData, BasePartsData, int>(UnitCtrl.CompareEnergyAsc));
                    break;
                case PriorityPattern.ATK_DEC:
                    UnitCtrl.QuickSort<BasePartsData>(targetList, new Func<BasePartsData, BasePartsData, int>(UnitCtrl.CompareAtkDec));
                    break;
                case PriorityPattern.ATK_ASC:
                    UnitCtrl.QuickSort<BasePartsData>(targetList, new Func<BasePartsData, BasePartsData, int>(UnitCtrl.CompareAtkAsc));
                    break;
                case PriorityPattern.MAGIC_STR_DEC:
                    UnitCtrl.QuickSort<BasePartsData>(targetList, new Func<BasePartsData, BasePartsData, int>(UnitCtrl.CompareMagicStrDec));
                    break;
                case PriorityPattern.MAGIC_STR_ASC:
                    UnitCtrl.QuickSort<BasePartsData>(targetList, new Func<BasePartsData, BasePartsData, int>(UnitCtrl.CompareMagicStrAsc));
                    break;
                case PriorityPattern.BOSS:
                    this.Owner.BaseX = _baseTransform.position.x;
                    targetList.Sort(new Comparison<BasePartsData>(this.Owner.CompareBoss));
                    break;
                case PriorityPattern.HP_ASC_NEAR:
                    this.Owner.BaseX = _baseTransform.position.x;
                    targetList.Sort(new Comparison<BasePartsData>(this.Owner.CompareLifeAscNear));
                    break;
                case PriorityPattern.HP_DEC_NEAR:
                    this.Owner.BaseX = _baseTransform.position.x;
                    targetList.Sort(new Comparison<BasePartsData>(this.Owner.CompareLifeDecNear));
                    break;
                case PriorityPattern.ENERGY_DEC_NEAR:
                case PriorityPattern.ENERGY_DEC_NEAR_MAX:
                    this.Owner.BaseX = _baseTransform.position.x;
                    targetList.Sort(new Comparison<BasePartsData>(this.Owner.CompareEnergyDecNear));
                    break;
                case PriorityPattern.ENERGY_ASC_NEAR:
                    this.Owner.BaseX = _baseTransform.position.x;
                    targetList.Sort(new Comparison<BasePartsData>(this.Owner.CompareEnergyAscNear));
                    break;
                case PriorityPattern.ATK_DEC_NEAR:
                    this.Owner.BaseX = _baseTransform.position.x;
                    targetList.Sort(new Comparison<BasePartsData>(this.Owner.CompareAtkDecNear));
                    break;
                case PriorityPattern.ATK_ASC_NEAR:
                    this.Owner.BaseX = _baseTransform.position.x;
                    targetList.Sort(new Comparison<BasePartsData>(this.Owner.CompareAtkAscNear));
                    break;
                case PriorityPattern.MAGIC_STR_DEC_NEAR:
                    this.Owner.BaseX = _baseTransform.position.x;
                    targetList.Sort(new Comparison<BasePartsData>(this.Owner.CompareMagicStrDecNear));
                    break;
                case PriorityPattern.MAGIC_STR_ASC_NEAR:
                    this.Owner.BaseX = _baseTransform.position.x;
                    targetList.Sort(new Comparison<BasePartsData>(this.Owner.CompareMagicStrAscNear));
                    break;
                case PriorityPattern.SHADOW:
                    this.Owner.BaseX = _baseTransform.position.x;
                    targetList.Sort(new Comparison<BasePartsData>(this.Owner.CompareShadow));
                    break;
                case PriorityPattern.HP_VALUE_DEC_NEAR_FORWARD:
                    this.Owner.BaseX = _baseTransform.position.x;
                    this.sortTargetPosition(targetList, true, new Comparison<BasePartsData>(this.Owner.CompareLifeValueDecSameLeft), new Comparison<BasePartsData>(this.Owner.CompareLifeValueDecSameRight));
                    break;
                case PriorityPattern.HP_VALUE_ASC_NEAR_FORWARD:
                    this.Owner.BaseX = _baseTransform.position.x;
                    this.sortTargetPosition(targetList, true, new Comparison<BasePartsData>(this.Owner.CompareLifeValueAscSameLeft), new Comparison<BasePartsData>(this.Owner.CompareLifeValueAscSameRight));
                    break;
                default:
                    targetList.Sort(new Comparison<BasePartsData>(this.Owner.CompareDistanceAsc));
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
                    basePartsDataList2.AddRange((IEnumerable<BasePartsData>)basePartsDataList1);
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
                    basePartsDataList3.AddRange((IEnumerable<BasePartsData>)basePartsDataList4);
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
            if (this.Owner.IsOther)
                _isForwardPattern = !_isForwardPattern;
            if (this.Owner.IsConfusionOrConvert())
                _isForwardPattern = !_isForwardPattern;
            if (_isForwardPattern)
                _targetList.Sort(_forwardComparison);
            else
                _targetList.Sort(_backComparison);
        }

        public void CancelAction(int skillId)
        {
            Skill skill = this.skillDictionary[skillId];
            for (int index = skill.EffectObjs.Count - 1; index >= 0; --index)
            {
              if ((UnityEngine.Object) skill.EffectObjs[index] != (UnityEngine.Object) null && !(skill.EffectObjs[index] is FirearmCtrl) && !skill.EffectObjs[index].IsRepeat)
              {
                skill.EffectObjs[index].SetTimeToDie(true);
                skill.EffectObjs[index].OnEffectEnd(skill.EffectObjs[index]);
              }
            }
            skill.Cancel = true;
        }

        /*public IEnumerator StartShakeWithDelay(
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
        }*/

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

        public void AppendCoroutine(IEnumerator cr, ePauseType pauseType, UnitCtrl unit = null) => this.battleManager.AppendCoroutine(cr, pauseType, unit);

        public bool HasNextAnime(int skillId)
        {
            Skill skill = this.skillDictionary[skillId];
            if (skill.IsPrincessForm)
                return true;
            return (skill.AnimId == eSpineCharacterAnimeId.SKILL || skill.AnimId == eSpineCharacterAnimeId.SKILL_EVOLUTION || skill.AnimId == eSpineCharacterAnimeId.SPECIAL_SKILL_EVOLUTION) && this.Owner.GetCurrentSpineCtrl().IsAnimation(skill.AnimId, skill.SkillNum, 1);
        }

        public bool IsLoopMotionPlaying(int _skillId)
        {
            Skill skill = this.skillDictionary[_skillId];
            return this.Owner.GetCurrentSpineCtrl().AnimationName == this.Owner.GetCurrentSpineCtrl().ConvertAnimeIdToAnimeName(skill.AnimId, skill.SkillNum, 1);
        }

        public SkillMotionType GetSkillMotionType(int _skillId) => this.skillDictionary[_skillId].SkillMotionType;

        public int GetSkillNum(int _skillId) => this.skillDictionary[_skillId].SkillNum;

        public bool IsModeChange(int _skillId) => this.skillDictionary[_skillId].IsModeChange;

        public void ExecActionOnStart()
        {
            foreach (KeyValuePair<int, Skill> skill in this.skillDictionary)
                this.execActionOnStart(skill.Value);
        }

        public void ExecActionOnWaveStart()
        {
            foreach (KeyValuePair<int, Skill> skill in this.skillDictionary)
                this.execActionOnWaveStart(skill.Value);
        }

        public IEnumerator UpdateBranchMotion(ActionParameter _action, Skill _skill)
        {
            if (_skill.AnimId != eSpineCharacterAnimeId.NONE && !this.updateBranchMotionRunning)
            {
                this.updateBranchMotionRunning = true;
                BattleSpineController unitSpineController = this.Owner.GetCurrentSpineCtrl();
                if (!unitSpineController.IsAnimation(_skill.AnimId, _skill.SkillNum, 1))
                {
                    this.updateBranchMotionRunning = false;
                }
                else
                {
                    while (!this.actionCancelAndGetCancel(_skill.SkillId))
                    {
                        if (unitSpineController.IsPlayAnimeBattle)
                            yield return (object)null;
                        else if (_skill.EffectBranchId == 0)
                        {
                            this.updateBranchMotionRunning = false;
                            yield break;
                        }
                        else
                        {
                            this.Owner.PlayAnime(_skill.AnimId, _skill.SkillNum, _skill.EffectBranchId, _isLoop: false);
                            if (_skill.SkillId == this.Owner.UnionBurstSkillId)
                                this.Owner.UnionBurstAnimeEndForIfAction = true;
                            while (!this.actionCancelAndGetCancel(_skill.SkillId))
                            {
                                if (!unitSpineController.IsPlayAnimeBattle)
                                {
                                    this.Owner.SetState(UnitCtrl.ActionState.IDLE);
                                    this.updateBranchMotionRunning = false;
                                    yield break;
                                }
                                else
                                    yield return (object)null;
                            }
                            this.updateBranchMotionRunning = false;
                            yield break;
                        }
                    }
                    this.updateBranchMotionRunning = false;
                }
            }
        }

        private bool actionCancelAndGetCancel(int _skillId)
        {
            if (!this.Owner.IsUnableActionState() && !this.Owner.IsCancelActionState(_skillId == this.Owner.UnionBurstSkillId))
                return false;
            this.Owner.CancelByAwake = false;
            this.Owner.CancelByConvert = false;
            this.CancelAction(_skillId);
            return true;
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
