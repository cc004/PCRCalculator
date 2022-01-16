// Decompiled with JetBrains decompiler
// Type: Elements.ActionParameter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using Elements.Battle;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Elements
{
  public class ActionParameter : ISingletonField
  {
        public string sortinfo;
    //private static Yggdrasil<ActionParameter> staticSingletonTree;
    private static BattleManager staticBattleManager;
    private static BattleEffectPoolInterface staticBattleEffectPool;
    private static BattleLogIntreface staticBattleLog;
    public eActionType ActionType;
    public ActionParameter.OnActionEndDelegate OnActionEnd;
    public bool IsSearchAndSorted;
    public float EnergyChargeMultiple = 1f;
    //protected List<SkillEffectCtrl> changePatternCurrentSkillEffect = new List<SkillEffectCtrl>();

    protected BattleManager battleManager => ActionParameter.staticBattleManager;

    protected BattleEffectPoolInterface battleEffectPool => ActionParameter.staticBattleEffectPool;

    protected BattleLogIntreface battleLog => ActionParameter.staticBattleLog;

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

    public ActionParameter.OnDamageHitDelegate OnDamageHit { get; set; }

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

    public Dictionary<UnitCtrl, Dictionary<int, ActionExecedData>> AlreadyExecedData { get; set; }

    public Dictionary<UnitCtrl, List<int>> AlreadyExecedKeys { get; set; }

    public List<BasePartsData> HitOnceKeyList { get; set; }

    public Dictionary<BasePartsData, List<CriticalData>> CriticalDataDictionary { get; set; } = new Dictionary<BasePartsData, List<CriticalData>>();

    public Dictionary<BasePartsData, FloatWithEx> TotalDamageDictionary { get; set; } = new Dictionary<BasePartsData, FloatWithEx>();

    //FIXME: no expectation for multiple or divide value for the expression is not linear
    public Dictionary<eValueNumber, FloatWithEx> AdditionalValue { get; set; }

    public Dictionary<eValueNumber, FloatWithEx> MultipleValue { get; set; }

    public Dictionary<eValueNumber, FloatWithEx> DivideValue { get; set; }

    public MasterSkillAction.SkillAction MasterData { get; set; }

    public eEffectType EffectType { get; set; }

    public bool IgnoreDecoy { get; set; }

    public static void StaticRelease()
    {
      //ActionParameter.staticSingletonTree = (Yggdrasil<ActionParameter>) null;
      ActionParameter.staticBattleManager = null;
      ActionParameter.staticBattleEffectPool = (BattleEffectPoolInterface) null;
      ActionParameter.staticBattleLog = (BattleLogIntreface) null;
    }

    public ActionParameter()
    {
            //if (ActionParameter.staticSingletonTree != null)
            //  return;
            //ActionParameter.staticSingletonTree = this.CreateSingletonTree<ActionParameter>();
            ActionParameter.staticBattleManager = BattleManager.Instance;
      ActionParameter.staticBattleEffectPool = staticBattleManager.battleEffectPool;
      ActionParameter.staticBattleLog = staticBattleManager;
    }

    public virtual void Initialize()
    {
      this.HitOnceDic = new Dictionary<BasePartsData, bool>();
      this.HitOnceKeyList = new List<BasePartsData>();
      this.AlreadyExecedData = new Dictionary<UnitCtrl, Dictionary<int, ActionExecedData>>();
      this.AlreadyExecedKeys = new Dictionary<UnitCtrl, List<int>>();
    }

    public virtual void Initialize(UnitCtrl _ownerUnitCtrl) => this.Initialize();

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
            ExecAction(_source, _target, _num, _sourceActionController, _skill, _starttime, _enabledChildAction, _valueDictionary);
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
    }

    public virtual void ReadyAction(
      UnitCtrl _source,
      UnitActionController _sourceActionController,
      Skill _skill) => this.ResetHitData();

    public void ResetHitData()
    {
      for (int index = 0; index < this.HitOnceKeyList.Count; ++index)
        this.HitOnceDic[this.HitOnceKeyList[index]] = true;
      Dictionary<UnitCtrl, Dictionary<int, ActionExecedData>>.Enumerator enumerator = this.AlreadyExecedData.GetEnumerator();
      while (enumerator.MoveNext())
      {
        for (int index = 0; index < this.AlreadyExecedKeys[enumerator.Current.Key].Count; ++index)
        {
          ActionExecedData actionExecedData = this.AlreadyExecedData[enumerator.Current.Key][this.AlreadyExecedKeys[enumerator.Current.Key][index]];
          actionExecedData.AlreadyExeced = false;
          actionExecedData.ExecedPartsNumber = 0;
          actionExecedData.TargetPartsNumber = 0;
        }
      }
    }

    public AbnormalStateEffectPrefabData CreateAbnormalEffectData()
    {
      AbnormalStateEffectPrefabData effectPrefabData = (AbnormalStateEffectPrefabData) null;
      if (this.ActionEffectList.Count > 0)
        effectPrefabData = new AbnormalStateEffectPrefabData()
        {
          IsHead = this.ActionEffectList[0].TargetBone == eTargetBone.HEAD,
          LeftEffectPrefab = this.ActionEffectList[0].PrefabLeft,
          RightEffectPrefab = this.ActionEffectList[0].Prefab
        };
      return effectPrefabData;
    }

    public bool JudgeIsAlreadyExeced(UnitCtrl _target, int _num) => !this.AlreadyExecedData[_target][_num].AlreadyExeced;

    public void AppendTargetNum(UnitCtrl _target, int _num)
    {
      if (!this.AlreadyExecedData.ContainsKey(_target))
      {
        this.AlreadyExecedData.Add(_target, new Dictionary<int, ActionExecedData>()
        {
          {
            _num,
            new ActionExecedData() { TargetPartsNumber = 1 }
          }
        });
        this.AlreadyExecedKeys.Add(_target, new List<int>()
        {
          _num
        });
      }
      else if (!this.AlreadyExecedData[_target].ContainsKey(_num))
      {
        this.AlreadyExecedData[_target].Add(_num, new ActionExecedData()
        {
          TargetPartsNumber = 1
        });
        this.AlreadyExecedKeys[_target].Add(_num);
      }
      else
        ++this.AlreadyExecedData[_target][_num].TargetPartsNumber;
    }

    public void AppendIsAlreadyExeced(UnitCtrl _target, int _num) => this.AlreadyExecedData[_target][_num].AlreadyExeced = true;

    public bool JudgeLastAndNotExeced(UnitCtrl _target, int _num)
    {
      ActionExecedData actionExecedData = this.AlreadyExecedData[_target][_num];
      ++actionExecedData.ExecedPartsNumber;
      return !actionExecedData.AlreadyExeced && actionExecedData.ExecedPartsNumber == actionExecedData.TargetPartsNumber;
    }

    public delegate void OnDamageHitDelegate(float damage);

    public delegate void OnActionEndDelegate();
  }
}
