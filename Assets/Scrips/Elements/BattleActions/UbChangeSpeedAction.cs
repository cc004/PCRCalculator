// Decompiled with JetBrains decompiler
// Type: Elements.UbChangeSpeedAction
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System.Collections.Generic;
using Cute;
using Elements.Battle;
using PCRCaculator.Guild;
using UnityEngine;

namespace Elements
{
  public class UbChangeSpeedAction : ActionParameter
  {
    private static readonly Dictionary<eChangeSpeedType, UnitCtrl.eAbnormalState> abnormalStateDic = new Dictionary<eChangeSpeedType, UnitCtrl.eAbnormalState>(new eChangeSpeedType_DictComparer())
    {
      {
        eChangeSpeedType.SLOW,
        UnitCtrl.eAbnormalState.SLOW
      },
      {
        eChangeSpeedType.HASTE,
        UnitCtrl.eAbnormalState.HASTE
      },
      {
        eChangeSpeedType.PARALYSIS,
        UnitCtrl.eAbnormalState.PARALYSIS
      },
      {
        eChangeSpeedType.FREEZE,
        UnitCtrl.eAbnormalState.FREEZE
      },
      {
        eChangeSpeedType.CHAINED,
        UnitCtrl.eAbnormalState.CHAINED
      },
      {
        eChangeSpeedType.SLEEP,
        UnitCtrl.eAbnormalState.SLEEP
      },
      {
        eChangeSpeedType.STUN,
        UnitCtrl.eAbnormalState.STUN
      },
      {
        eChangeSpeedType.STONE,
        UnitCtrl.eAbnormalState.STONE
      },
      {
        eChangeSpeedType.FAINT,
        UnitCtrl.eAbnormalState.FAINT
      }
    };

    private Dictionary<eChangeSpeedType, GameObject> effectDictionary { get; set; }

    public override void Initialize()
    {
      base.Initialize();
      effectDictionary = new Dictionary<eChangeSpeedType, GameObject>(new eChangeSpeedType_DictComparer());
     // ManagerSingleton<ResourceManager>.Instance.AddLoadResourceId(eResourceId.FX_UB_ABNORMAL_STUN, (Action<object>) (resourceObject => this.effectDictionary.Add(UbChangeSpeedAction.eChangeSpeedType.STUN, resourceObject as GameObject)));
    }

    public override void ExecAction(
      UnitCtrl _source,
      BasePartsData _target,
      int _num,
      UnitActionController _sourceActionController,
      Skill _skill,
      float _starttime,
      Dictionary<int, bool> _enabledChildAction,
      Dictionary<eValueNumber, FloatWithEx> _valueDictionary)
    {
      AppendIsAlreadyExeced(_target.Owner, _num);
            double pp = BattleUtil.GetDodgeByLevelDiff(_skill.Level, _target.GetLevel());

            if (BattleManager.Random(0.0f, 1f, new RandomData(_source, _target.Owner, ActionId, 16, (float)pp)) < (double) BattleUtil.GetDodgeByLevelDiff(_skill.Level, _target.GetLevel()))
      {
        //SkillEffectCtrl effect = this.battleEffectPool.GetEffect(this.effectDictionary[UbChangeSpeedAction.eChangeSpeedType.STUN]);
        //effect.InitializeSort();
        //effect.SortTarget = _target.Owner;
        //effect.SetSortOrderBack();
        //effect.transform.position = _target.GetBottomTransformPosition();
        //this.battleManager.StartCoroutine(effect.TrackTarget(_target, Vector3.zero, followY: false, bone: _target.GetCenterBone()));
        //this.battleManager.StartCoroutine(effect.TrackTargetSort(_target.Owner));
        UbAbnormalData ubAbnormalData = new UbAbnormalData
        {
          AbnormalState = abnormalStateDic[(eChangeSpeedType) ActionDetail1],
            Source = _source,
            EffectTime = _valueDictionary[eValueNumber.VALUE_3],
          Value = _valueDictionary[eValueNumber.VALUE_1],
          Timer = _valueDictionary[eValueNumber.VALUE_5],
          //Effect = effect
        };
        ubAbnormalData.StartTimer(_target.Owner);
        _target.Owner.UbAbnormalDataList.Add(ubAbnormalData);
        _target.Owner.OnChangeState.Call(_target.Owner, eStateIconType.UB_DISABLE, true);
                _target.Owner.NoSkipOnChangeAbnormalState?.Invoke(_target.Owner, eStateIconType.UB_DISABLE, true, _valueDictionary[eValueNumber.VALUE_5],
                    "???");

            }
            else
      {
        ActionExecedData actionExecedData = AlreadyExecedData[_target.Owner][_num];
        if (actionExecedData.ExecedPartsNumber != actionExecedData.TargetPartsNumber)
          return;
        if (actionExecedData.TargetPartsNumber == 1)
          _target.Owner.SetMissAtk(_source, eMissLogType.DODGE_CHANGE_SPEED, _parts: _target);
        else
          _target.Owner.SetMissAtk(_source, eMissLogType.DODGE_CHANGE_SPEED);
      }
    }

    public override void SetLevel(float _level)
    {
      base.SetLevel(_level);
      Value[eValueNumber.VALUE_1] = (float) (MasterData.action_value_1 + MasterData.action_value_2 * _level);
      Value[eValueNumber.VALUE_3] = (float) (MasterData.action_value_3 + MasterData.action_value_4 * _level);
    }

    private enum eChangeSpeedType
    {
      SLOW = 1,
      HASTE = 2,
      PARALYSIS = 3,
      FREEZE = 4,
      CHAINED = 5,
      SLEEP = 6,
      STUN = 7,
      STONE = 8,
      FAINT = 9,
    }

    private class eChangeSpeedType_DictComparer : IEqualityComparer<eChangeSpeedType>
    {
      public bool Equals(
        eChangeSpeedType _x,
        eChangeSpeedType _y) => _x == _y;

      public int GetHashCode(eChangeSpeedType _obj) => (int) _obj;
    }
  }
}
