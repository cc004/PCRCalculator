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
        public DoubleWithEx oppositeActionProb;

        public string sortinfo;

        //private static Yggdrasil<ActionParameter> staticSingletonTree;
#if ENABLE_THREADING
        private BattleManager staticBattleManager;
        private BattleEffectPoolInterface staticBattleEffectPool;
        private BattleLogIntreface staticBattleLog;
#else
        private static BattleManager staticBattleManager;
        private static BattleEffectPoolInterface staticBattleEffectPool;
        private static BattleLogIntreface staticBattleLog;
#endif
        public eActionType ActionType;
        public OnActionEndDelegate OnActionEnd;
        public bool IsSearchAndSorted;
        public bool IsAlwaysChargeEnegry;
        public double EnergyChargeMultiple = 1f;
        //protected List<SkillEffectCtrl> changePatternCurrentSkillEffect = new List<SkillEffectCtrl>();

        protected BattleManager battleManager => staticBattleManager;

        protected BattleEffectPoolInterface battleEffectPool => staticBattleEffectPool;

        protected BattleLogIntreface battleLog => staticBattleLog;

        public eTargetAssignment TargetAssignment { get; set; }

        public PriorityPattern TargetSort { get; set; }

        public int TargetNth { get; set; }

        public int TargetNum { get; set; }

        public float TargetWidth { get; set; }

        public bool TriggerCall { get; set; }


        public DirectionType Direction { get; set; }

        public float Position { get; set; }

        public int ActionDetail1 { get; set; }

        public int ActionDetail2 { get; set; }

        public int ActionDetail3 { get; set; }

        public int ActionId { get; set; }

        public List<BasePartsData> TargetList { get; set; }

        public ValueDictionary Value { get; set; }

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

        public UnitCtrl.eAbnormalState EnvironmentTargetAbnormalState { get; set; }

        public bool IsEnvironmentAbnormalState { get; set; }

        public Action OnDefeatEnemy { get; set; }

        public Action OnInitWhenNoTarget { get; set; }

        public bool CancelByIfForAll { get; set; }

        public bool NoDelayExecByTriggerOnly { get; set; }

        public Dictionary<BasePartsData, long> IdOffsetDictionary { get; set; }

        public int ContinuousTargetCount { get; set; }

        public GameObject SummonPrefab { get; set; }

        public AnimationCurve KnockAnimationCurve { get; set; }

        public AnimationCurve KnockDownAnimationCurve { get; set; }

        public Dictionary<BasePartsData, bool> HitOnceDic { get; set; }

        public Dictionary<BasePartsData, ProbEvent> HitOnceProbDic { get; set; } =
            new Dictionary<BasePartsData, ProbEvent>();

        public Dictionary<UnitCtrl, Dictionary<int, ActionExecedData>> AlreadyExecedData { get; set; }

        public bool SkipAppendIsAlreadyExeced { get; set; }

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

        public Dictionary<BasePartsData, LongWithEx> TotalDamageDictionary { get; set; } =
            new Dictionary<BasePartsData, LongWithEx>();

        public ValueDictionary AdditionalValue { get; set; }

        public ValueDictionary MultipleValue { get; set; }

        public ValueDictionary DivideValue { get; set; }

        public MasterSkillAction.SkillAction MasterData { get; set; }

        public eEffectType EffectType { get; set; }

        public bool IgnoreDecoy { get; set; }
        public bool IsExEquipSkillAction { get; set; }
        public List<UnitCtrl> ExecedMultiBossList { get; set; } = new List<UnitCtrl>();
        public Dictionary<UnitCtrl, double> AllSealDamageCutExecValueDic { get; set; } = new Dictionary<UnitCtrl, double>();
        public double AllSealDamageCutValueForCountZero { get; set; } = -1.0;
        public bool AlreadyTriggerCause { get; set; }
        public bool IgnoreTriggerCausedByTrigger { get; set; }


        public BasePartsData ProtectedParts { get; set; }

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
            ValueDictionary _valueDictionary,
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
            ValueDictionary _valueDictionary)

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
            AllSealDamageCutExecValueDic.Clear();
            AlreadyTriggerCause = false;
            ExecedMultiBossList.Clear();
            AllSealDamageCutValueForCountZero = -1.0;
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

        // RVA: 0x27C95C0 Offset: 0x27C95C0 VA: 0x27C95C0 Slot: 12
        public virtual DoubleWithEx GetActionBaseValue(UnitCtrl _source, eValueNumber _type)
        {
            DoubleWithEx val;
            Value.TryGetValue(_type, out val);
            return val;
        }

        public delegate void OnDamageHitDelegate(LongWithEx damage);

        public delegate void OnActionEndDelegate();

        public ProbEvent relatedEvent;

        public ValueDictionary oppositeValue = new ValueDictionary();

        public ValueDictionary PrepareActionValue(UnitCtrl _owner)
        {

            ValueDictionary additionalValue = AdditionalValue;
            ValueDictionary multipleValue = MultipleValue;
            ValueDictionary divideValue = DivideValue;
            Func<ActionParameter, eValueNumber, DoubleWithEx> func = (_action, _type) =>
            {
                DoubleWithEx num1 = 0.0;
                if (additionalValue != null && additionalValue.TryGetValue(_type, out var value))
                    num1 = value;
                DoubleWithEx num2 = 1.0;
                if (multipleValue != null && multipleValue.TryGetValue(_type, out var value1))
                    num2 = value1;
                DoubleWithEx num3 = 1;
                if (divideValue != null && divideValue.TryGetValue(_type, out var value2))
                    if ((double) value2 != 0.0)
                        num3 = value2;
                DoubleWithEx num4 = 0.0;
                // _action.Value.TryGetValue(_type, out num4);
                num4 = _action.GetActionBaseValue(_owner, _type);
                if ((object) oppositeActionProb == null)
                    return (num1 + num4) * num2 / num3;
                var result = (num1 + num4) * num2 / num3;
                var opp = oppositeValue.TryGetValue(_type, out var val) ? val : 0;
                return result * (1 - oppositeActionProb) + opp * oppositeActionProb;
            };
            ValueDictionary _valueDictionary = new ValueDictionary();
            _valueDictionary.Add(eValueNumber.VALUE_1, func(this, eValueNumber.VALUE_1));
            _valueDictionary.Add(eValueNumber.VALUE_2, func(this, eValueNumber.VALUE_2));
            _valueDictionary.Add(eValueNumber.VALUE_3, func(this, eValueNumber.VALUE_3));
            _valueDictionary.Add(eValueNumber.VALUE_4, func(this, eValueNumber.VALUE_4));
            _valueDictionary.Add(eValueNumber.VALUE_5, func(this, eValueNumber.VALUE_5));
            _valueDictionary.Add(eValueNumber.VALUE_6, func(this, eValueNumber.VALUE_6));
            _valueDictionary.Add(eValueNumber.VALUE_7, func(this, eValueNumber.VALUE_7));
            return _valueDictionary;
        }

        private int _level;

        public string GetSkillDetail(UnitCtrl owner)
        {
#if UNITY_5_3_OR_NEWER
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
#else
            return string.Empty;
#endif
        }
    }
}
