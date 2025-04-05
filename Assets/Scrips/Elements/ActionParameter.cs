// Decompiled with JetBrains decompiler
// Type: Elements.ActionParameter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using ActionParameterSerializer;
using Elements.Battle;
using PCRCaculator.Guild;
using UnityEngine;

namespace Elements
{
    public class ActionParameter : ISingletonField
    {
        public List<NormalSkillEffect> ActionSubEffectList { get; set; }
        public ActionParameter oppositeAction;

        /// <summary>
        /// 1 when the if for all action turns to the other action
        /// null means no other action
        /// should always 'is null or is {.value = 0}' when execAction called
        /// </summary>
        public FloatWithEx oppositeActionProb;

        public string sortinfo;

        //private static Yggdrasil<ActionParameter> staticSingletonTree;
        private static BattleManager staticBattleManager;
        private static BattleEffectPoolInterface staticBattleEffectPool;
        private static BattleLogIntreface staticBattleLog;
        public eActionType ActionType;
        public OnActionEndDelegate OnActionEnd;
        public bool IsSearchAndSorted;
        public bool IsAlwaysChargeEnegry;
        public float EnergyChargeMultiple = 1f;
        //protected List<SkillEffectCtrl> changePatternCurrentSkillEffect = new List<SkillEffectCtrl>();

        protected BattleManager battleManager => staticBattleManager;

        protected BattleEffectPoolInterface battleEffectPool => staticBattleEffectPool;

        protected BattleLogIntreface battleLog => staticBattleLog;

        public eTargetAssignment TargetAssignment { get; set; }

        public PriorityPattern TargetSort { get; set; }

        public int TargetNth { get; set; }

        public int TargetNum { get; set; }

        public float TargetWidth { get; set; }

        public DirectionType Direction { get; set; }

        public float Position { get; set; }

        public int ActionDetail1 { get; set; }

        public int ActionDetail2 { get; set; }

        public int ActionDetail3 { get; set; }

        public int ActionId { get; set; }

        public List<BasePartsData> TargetList { get; set; }

        public Dictionary<eValueNumber, FloatWithEx> Value { get; set; }

        public float[] ExecTime { get; set; }

        public List<ActionExecTime> ActionExecTimeList { get; set; }

        public float ActionWeightSum { get; set; }

        public int DepenedActionId { set; get; }

        public bool ReferencedByEffect { get; set; }

        public bool ReferencedByReflection { get; set; }

        public List<int> ActionChildrenIndexes { get; set; }

        public OnDamageHitDelegate OnDamageHit { get; set; }
        public bool IsFlightStateTargetByReflection { get; set; }

        public List<NormalSkillEffect> ActionEffectList { get; set; }

        public AbnormalStateFieldAction AbnormalStateFieldAction { get; set; }

        public Action OnDefeatEnemy { get; set; }

        public Action OnInitWhenNoTarget { get; set; }

        public bool CancelByIfForAll { get; set; }

        public Dictionary<BasePartsData, long> IdOffsetDictionary { get; set; }

        public int ContinuousTargetCount { get; set; }

        public GameObject SummonPrefab { get; set; }

        public AnimationCurve KnockAnimationCurve { get; set; }

        public AnimationCurve KnockDownAnimationCurve { get; set; }

        public Dictionary<BasePartsData, bool> HitOnceDic { get; set; }

        public Dictionary<BasePartsData, ProbEvent> HitOnceProbDic { get; set; } =
            new Dictionary<BasePartsData, ProbEvent>();

        public Dictionary<UnitCtrl, Dictionary<int, ActionExecedData>> AlreadyExecedData { get; set; }

        public Dictionary<UnitCtrl, List<int>> AlreadyExecedKeys { get; set; }

        public List<BasePartsData> HitOnceKeyList { get; set; }

        public Dictionary<BasePartsData, List<CriticalData>> CriticalDataDictionary { get; set; } =
            new Dictionary<BasePartsData, List<CriticalData>>();

        public Dictionary<eStateIconType, List<UnitCtrl>> UsedChargeEnergyByReceiveDamage { get; set; } =
            new Dictionary<eStateIconType, List<UnitCtrl>>();


        public Dictionary<BasePartsData, long> LimitDamageDictionaryAtk { get; set; } =
            new Dictionary<BasePartsData, long>();


        public Dictionary<BasePartsData, long> LimitDamageDictionaryMgc { get; set; } =
            new Dictionary<BasePartsData, long>();

        public Dictionary<BasePartsData, FloatWithEx> TotalDamageDictionary { get; set; } =
            new Dictionary<BasePartsData, FloatWithEx>();

        public Dictionary<eValueNumber, FloatWithEx> AdditionalValue { get; set; }

        public Dictionary<eValueNumber, FloatWithEx> MultipleValue { get; set; }

        public Dictionary<eValueNumber, FloatWithEx> DivideValue { get; set; }

        public MasterSkillAction.SkillAction MasterData { get; set; }

        public eEffectType EffectType { get; set; }

        public bool IgnoreDecoy { get; set; }
        public List<UnitCtrl> ExecedMultiBossList { get; set; } = new List<UnitCtrl>();

        public static void StaticRelease()
        {
            //ActionParameter.staticSingletonTree = (Yggdrasil<ActionParameter>) null;
            staticBattleManager = null;
            staticBattleEffectPool = null;
            staticBattleLog = null;
        }

        public ActionParameter()
        {
            //if (ActionParameter.staticSingletonTree != null)
            //  return;
            //ActionParameter.staticSingletonTree = this.CreateSingletonTree<ActionParameter>();
            staticBattleManager = BattleManager.Instance;
            staticBattleEffectPool = staticBattleManager.battleEffectPool;
            staticBattleLog = BattleLogIntreface.Instance;
        }

        public virtual void Initialize()
        {
            HitOnceDic = new Dictionary<BasePartsData, bool>();
            HitOnceKeyList = new List<BasePartsData>();
            AlreadyExecedData = new Dictionary<UnitCtrl, Dictionary<int, ActionExecedData>>();
            AlreadyExecedKeys = new Dictionary<UnitCtrl, List<int>>();
        }

        public virtual void Initialize(UnitCtrl _ownerUnitCtrl) => Initialize();

        public virtual void ExecActionOnStart(
            Skill _skill,
            UnitCtrl _source,
            UnitActionController _sourceActionController)
        {
        }

        public virtual void ExecActionOnWaveStart(
            Skill _skill,
            UnitCtrl _source,
            UnitActionController _sourceActionController)
        {
        }

        public void PreExecAction()
        {
            if (relatedEvent != null)
            {
                relatedEvent.enabled = true;
                relatedEvent = null;
                relatedEvent = null;
            }
        }

        public virtual void ExecAction(
            UnitCtrl _source,
            BasePartsData _target,
            int _num,
            UnitActionController _sourceActionController,
            Skill _skill,
            float _starttime,
            Dictionary<int, bool> _enabledChildAction,
            Dictionary<eValueNumber, FloatWithEx> _valueDictionary,
            Action<string> callBack = null)
        {
            ExecAction(_source, _target, _num, _sourceActionController, _skill, _starttime, _enabledChildAction,
                _valueDictionary);
            //add scripts
            string describe = "技能描述鸽了！";
            callBack(describe);
            //end add
        }

        public virtual void ExecAction(
            UnitCtrl _source,
            BasePartsData _target,
            int _num,
            UnitActionController _sourceActionController,
            Skill _skill,
            float _starttime,
            Dictionary<int, bool> _enabledChildAction,
            Dictionary<eValueNumber, FloatWithEx> _valueDictionary)

        {
        }


        public virtual void SetLevel(float _level)
        {
            this._level = (int)_level;
        }

        public virtual void ReadyAction(
            UnitCtrl _source,
            UnitActionController _sourceActionController,
            Skill _skill)
        {
            ExecedMultiBossList.Clear();
            ResetHitData();
        }

        public void ResetHitData()
        {
            for (int index = 0; index < HitOnceKeyList.Count; ++index)
                HitOnceDic[HitOnceKeyList[index]] = true;
            Dictionary<UnitCtrl, Dictionary<int, ActionExecedData>>.Enumerator enumerator =
                AlreadyExecedData.GetEnumerator();
            while (enumerator.MoveNext())
            {
                for (int index = 0; index < AlreadyExecedKeys[enumerator.Current.Key].Count; ++index)
                {
                    ActionExecedData actionExecedData =
                        AlreadyExecedData[enumerator.Current.Key][AlreadyExecedKeys[enumerator.Current.Key][index]];
                    actionExecedData.AlreadyExeced = false;
                    actionExecedData.ExecedPartsNumber = 0;
                    actionExecedData.TargetPartsNumber = 0;
                }
            }
        }

        public AbnormalStateEffectPrefabData CreateAbnormalEffectData()
        {
            AbnormalStateEffectPrefabData effectPrefabData = null;
            if (ActionEffectList.Count > 0)
                effectPrefabData = new AbnormalStateEffectPrefabData
                {
                    IsHead = ActionEffectList[0].TargetBone == eTargetBone.HEAD,
                    LeftEffectPrefab = ActionEffectList[0].PrefabLeft,
                    RightEffectPrefab = ActionEffectList[0].Prefab
                };
            return effectPrefabData;
        }

        public bool JudgeIsAlreadyExeced(UnitCtrl _target, int _num) => !AlreadyExecedData[_target][_num].AlreadyExeced;

        public bool IsAppendTargetNum(UnitCtrl _target)
        {
            return AlreadyExecedData.ContainsKey(_target);
        }

        public void AppendTargetNum(UnitCtrl _target, int _num)
        {
            if (!AlreadyExecedData.ContainsKey(_target))
            {
                AlreadyExecedData.Add(_target, new Dictionary<int, ActionExecedData>
                {
                    {
                        _num,
                        new ActionExecedData {TargetPartsNumber = 1}
                    }
                });
                AlreadyExecedKeys.Add(_target, new List<int>
                {
                    _num
                });
            }
            else if (!AlreadyExecedData[_target].ContainsKey(_num))
            {
                AlreadyExecedData[_target].Add(_num, new ActionExecedData
                {
                    TargetPartsNumber = 1
                });
                AlreadyExecedKeys[_target].Add(_num);
            }
            else
                ++AlreadyExecedData[_target][_num].TargetPartsNumber;
        }

        public void AppendIsAlreadyExeced(UnitCtrl _target, int _num) =>
            AlreadyExecedData[_target][_num].AlreadyExeced = true;

        public bool JudgeLastAndNotExeced(UnitCtrl _target, int _num)
        {
            ActionExecedData actionExecedData = AlreadyExecedData[_target][_num];
            ++actionExecedData.ExecedPartsNumber;
            return !actionExecedData.AlreadyExeced &&
                   actionExecedData.ExecedPartsNumber == actionExecedData.TargetPartsNumber;
        }

        public delegate void OnDamageHitDelegate(FloatWithEx damage);

        public delegate void OnActionEndDelegate();

        public ProbEvent relatedEvent;

        public Dictionary<eValueNumber, FloatWithEx> oppositeValue = new Dictionary<eValueNumber, FloatWithEx>();

        public Dictionary<eValueNumber, FloatWithEx> PrepareActionValue()
        {

            Dictionary<eValueNumber, FloatWithEx> additionalValue = AdditionalValue;
            Dictionary<eValueNumber, FloatWithEx> multipleValue = MultipleValue;
            Dictionary<eValueNumber, FloatWithEx> divideValue = DivideValue;
            Func<ActionParameter, eValueNumber, FloatWithEx> func = (_action, _type) =>
            {
                FloatWithEx num1 = 0.0f;
                if (additionalValue != null && additionalValue.ContainsKey(_type))
                    num1 = additionalValue[_type];
                FloatWithEx num2 = 1f;
                if (multipleValue != null && multipleValue.ContainsKey(_type))
                    num2 = multipleValue[_type];
                FloatWithEx num3 = 1f;
                if (divideValue != null && divideValue.ContainsKey(_type) && (double) divideValue[_type] != 0.0)
                    num3 = divideValue[_type];
                FloatWithEx num4 = 0.0f;
                _action.Value.TryGetValue(_type, out num4);
                if ((object) oppositeActionProb == null)
                    return (num1 + num4) * num2 / num3;
                var result = (num1 + num4) * num2 / num3;
                var opp = oppositeValue.TryGetValue(_type, out var val) ? val : 0;
                return result * (1 - oppositeActionProb) + opp * oppositeActionProb;
            };
            Dictionary<eValueNumber, FloatWithEx> _valueDictionary =
                new Dictionary<eValueNumber, FloatWithEx>(new eValueNumber_DictComparer())
                {
                    {
                        eValueNumber.VALUE_1,
                        func(this, eValueNumber.VALUE_1)
                    },
                    {
                        eValueNumber.VALUE_2,
                        func(this, eValueNumber.VALUE_2)
                    },
                    {
                        eValueNumber.VALUE_3,
                        func(this, eValueNumber.VALUE_3)
                    },
                    {
                        eValueNumber.VALUE_4,
                        func(this, eValueNumber.VALUE_4)
                    },
                    {
                        eValueNumber.VALUE_5,
                        func(this, eValueNumber.VALUE_5)
                    },
                    {
                        eValueNumber.VALUE_6,
                        func(this, eValueNumber.VALUE_6)
                    },
                    {
                        eValueNumber.VALUE_7,
                        func(this, eValueNumber.VALUE_7)
                    }
                };
            return _valueDictionary;
        }

        private int _level;

        public string GetSkillDetail(UnitCtrl owner)
        {
            if (MasterData == null) return null;
            var param = ActionParameterSerializer.Actions.ActionParameter.type(MasterData.action_type);
            param.init(owner.IsOther, MasterData.action_id, DepenedActionId, MasterData.class_id,  MasterData.action_type, MasterData.action_detail_1,
                MasterData.action_detail_2, MasterData.action_detail_3, MasterData.action_value_1, MasterData.action_value_2, MasterData.action_value_3,
                MasterData.action_value_4, MasterData.action_value_5, MasterData.action_value_6, MasterData.action_value_7,
                MasterData.target_assignment, MasterData.target_area, MasterData.target_range, MasterData.target_type, MasterData.target_number, MasterData.target_count, 
                null, new List<SkillAction>());
            UserSettings.expression = UserSettings.EXPRESSION_VALUE;
            return param.localizedDetail(_level, new Property()
            {
                atk = owner.Atk, magicStr = owner.MagicStr
            });
        }
    }
}
