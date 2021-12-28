// Decompiled with JetBrains decompiler
// Type: Elements.UbChangeSpeedAction
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using Cute;
using Elements.Battle;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Elements
{
  public class UbChangeSpeedAction : ActionParameter
  {
    private static readonly Dictionary<UbChangeSpeedAction.eChangeSpeedType, UnitCtrl.eAbnormalState> abnormalStateDic = new Dictionary<UbChangeSpeedAction.eChangeSpeedType, UnitCtrl.eAbnormalState>((IEqualityComparer<UbChangeSpeedAction.eChangeSpeedType>) new UbChangeSpeedAction.eChangeSpeedType_DictComparer())
    {
      {
        UbChangeSpeedAction.eChangeSpeedType.SLOW,
        UnitCtrl.eAbnormalState.SLOW
      },
      {
        UbChangeSpeedAction.eChangeSpeedType.HASTE,
        UnitCtrl.eAbnormalState.HASTE
      },
      {
        UbChangeSpeedAction.eChangeSpeedType.PARALYSIS,
        UnitCtrl.eAbnormalState.PARALYSIS
      },
      {
        UbChangeSpeedAction.eChangeSpeedType.FREEZE,
        UnitCtrl.eAbnormalState.FREEZE
      },
      {
        UbChangeSpeedAction.eChangeSpeedType.CHAINED,
        UnitCtrl.eAbnormalState.CHAINED
      },
      {
        UbChangeSpeedAction.eChangeSpeedType.SLEEP,
        UnitCtrl.eAbnormalState.SLEEP
      },
      {
        UbChangeSpeedAction.eChangeSpeedType.STUN,
        UnitCtrl.eAbnormalState.STUN
      },
      {
        UbChangeSpeedAction.eChangeSpeedType.STONE,
        UnitCtrl.eAbnormalState.STONE
      },
      {
        UbChangeSpeedAction.eChangeSpeedType.FAINT,
        UnitCtrl.eAbnormalState.FAINT
      }
    };

    private Dictionary<UbChangeSpeedAction.eChangeSpeedType, GameObject> effectDictionary { get; set; }

    public override void Initialize()
    {
      base.Initialize();
      this.effectDictionary = new Dictionary<UbChangeSpeedAction.eChangeSpeedType, GameObject>((IEqualityComparer<UbChangeSpeedAction.eChangeSpeedType>) new UbChangeSpeedAction.eChangeSpeedType_DictComparer());
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
      this.AppendIsAlreadyExeced(_target.Owner, _num);
            double pp = BattleUtil.GetDodgeByLevelDiff(_skill.Level, _target.GetLevel());

            if ((double) BattleManager.Random(0.0f, 1f, new PCRCaculator.Guild.RandomData(_source, _target.Owner, ActionId, 16, (float)pp)) < (double) BattleUtil.GetDodgeByLevelDiff(_skill.Level, _target.GetLevel()))
      {
        //SkillEffectCtrl effect = this.battleEffectPool.GetEffect(this.effectDictionary[UbChangeSpeedAction.eChangeSpeedType.STUN]);
        //effect.InitializeSort();
        //effect.SortTarget = _target.Owner;
        //effect.SetSortOrderBack();
        //effect.transform.position = _target.GetBottomTransformPosition();
        //this.battleManager.StartCoroutine(effect.TrackTarget(_target, Vector3.zero, followY: false, bone: _target.GetCenterBone()));
        //this.battleManager.StartCoroutine(effect.TrackTargetSort(_target.Owner));
        UbAbnormalData ubAbnormalData = new UbAbnormalData()
        {
          AbnormalState = UbChangeSpeedAction.abnormalStateDic[(UbChangeSpeedAction.eChangeSpeedType) this.ActionDetail1],
          EffectTime = _valueDictionary[eValueNumber.VALUE_3],
          Value = _valueDictionary[eValueNumber.VALUE_1],
          Timer = _valueDictionary[eValueNumber.VALUE_5],
          //Effect = effect
        };
        ubAbnormalData.StartTimer(_target.Owner);
        _target.Owner.UbAbnormalDataList.Add(ubAbnormalData);
        _target.Owner.OnChangeState.Call<UnitCtrl, eStateIconType, bool>(_target.Owner, eStateIconType.UB_DISABLE, true);
                _target.Owner.MyOnChangeAbnormalState?.Invoke(_target.Owner, eStateIconType.UB_DISABLE, true, _valueDictionary[eValueNumber.VALUE_5],
                    "???");

            }
            else
      {
        ActionExecedData actionExecedData = this.AlreadyExecedData[_target.Owner][_num];
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
      this.Value[eValueNumber.VALUE_1] = (float) ((double) this.MasterData.action_value_1 + (double) this.MasterData.action_value_2 * (double) _level);
      this.Value[eValueNumber.VALUE_3] = (float) ((double) this.MasterData.action_value_3 + (double) this.MasterData.action_value_4 * (double) _level);
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

    private class eChangeSpeedType_DictComparer : IEqualityComparer<UbChangeSpeedAction.eChangeSpeedType>
    {
      public bool Equals(
        UbChangeSpeedAction.eChangeSpeedType _x,
        UbChangeSpeedAction.eChangeSpeedType _y) => _x == _y;

      public int GetHashCode(UbChangeSpeedAction.eChangeSpeedType _obj) => (int) _obj;
    }
  }
}
