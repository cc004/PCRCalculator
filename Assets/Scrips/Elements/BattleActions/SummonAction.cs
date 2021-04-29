// Decompiled with JetBrains decompiler
// Type: Elements.SummonAction
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using Elements.Battle;
using Spine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elements
{
  public class SummonAction : ActionParameter
  {
    private Attachment targetAttachment;
    private Attachment appliedAttachment;
    private bool attachmentChanged;
    private const int UNUSE_RESPAWNPOS_NUM = 99;
    private const int CONSIDER_EQUIP_NUM = 3;

    public override void Initialize(UnitCtrl _ownerUnitCtrl)
    {
      bool _isWaveEnemy = (UnityEngine.Object) _ownerUnitCtrl != (UnityEngine.Object) null && _ownerUnitCtrl.IsOther;
      this.Initialize();
      //Singleton<BattleUnitLoader>.Instance.AddLoadResource(UnitUtility.GetSummonResourceId(UnitUtility.GetUnitResourceId(this.ActionDetail2)), (Action<GameObject>) (_loadedObject => this.SummonPrefab = _loadedObject), _isWaveEnemy);
      //ManagerSingleton<ResourceManager>.Instance.StartLoad();
    }

    public override void ExecAction(
      UnitCtrl _source,
      BasePartsData _target,
      int _num,
      UnitActionController _sourceActionController,
      Skill _skill,
      float _starttime,
      Dictionary<int, bool> _enabledChildAction,
      Dictionary<eValueNumber, float> _valueDictionary)
    {
      base.ExecAction(_source, _target, _num, _sourceActionController, _skill, _starttime, _enabledChildAction, _valueDictionary);
      eUnitRespawnPos _basePos = (eUnitRespawnPos) Mathf.Min(9, Mathf.Max(0, (int) (_source.RespawnPos + (int) _valueDictionary[eValueNumber.VALUE_2])));
      UnitCtrl unitCtrl = this.battleManager.Summon(new SummonData()
      {
        SummonId = this.ActionDetail2,
        TargetPosition = _target.GetLocalPosition() + new Vector3((_source.IsLeftDir || _source.IsForceLeftDir ? -1f : 1f) * _valueDictionary[eValueNumber.VALUE_7], 0.0f),
        Owner = _source,
        Skill = _skill,
        //Prefab = this.SummonPrefab,
        SummonSide = (SummonAction.eSummonSide) this.ActionDetail3,
        SummonType = (SummonAction.eSummonType) _valueDictionary[eValueNumber.VALUE_5],
        MoveSpeed = _valueDictionary[eValueNumber.VALUE_1],
        FromPosition = _source.transform.localPosition + new Vector3((_source.IsLeftDir || _source.IsForceLeftDir ? -1f : 1f) * _valueDictionary[eValueNumber.VALUE_6], 0.0f, 0.0f),
        UseRespawnPos = (double) _valueDictionary[eValueNumber.VALUE_2] != 99.0,
        RespawnPos = BattleUtil.SearchRespawnPos(_basePos, _source.IsOther ? this.battleManager.EnemyList : this.battleManager.UnitList),
        ConsiderEquipmentAndBonus = this.ActionDetail1 == 3
      });
      int actionId = this.ActionId;
      if (!_source.SummonUnitDictionary.ContainsKey(actionId))
      {
        _source.SummonUnitDictionary.Add(actionId, unitCtrl);
      }
      else
      {
        UnitCtrl summonUnit = _source.SummonUnitDictionary[actionId];
        if (!summonUnit.IdleOnly && !summonUnit.IsDead)
        {
          summonUnit.IdleOnly = true;
          summonUnit.CureAllAbnormalState();
        }
        _source.SummonUnitDictionary[actionId] = unitCtrl;
      }
      unitCtrl.SummonSource = _source;
      UnitCtrl summonUnit1 = _source.SummonUnitDictionary[actionId];
      if (_source.IdleOnly || this.battleManager.GameState != eBattleGameState.PLAY)
      {
        summonUnit1.IdleOnly = true;
        if (this.battleManager.UnitList.Contains(summonUnit1))
          this.battleManager.UnitList.Remove(summonUnit1);
      }
      if (_source.SummonTargetAttachmentName.IsNullOrEmpty() || this.appliedAttachment != null)
        return;
      int index1 = _source.UnitSpineCtrl.skeleton.slots.FindIndex((Predicate<Spine.Slot>) (e => e.data.Name == _source.SummonTargetAttachmentName));
      this.targetAttachment = _source.UnitSpineCtrl.skeleton.GetAttachment(index1, _source.SummonTargetAttachmentName);
      int index2 = _source.UnitSpineCtrl.skeleton.slots.FindIndex((Predicate<Spine.Slot>) (e => e.data.Name == _source.SummonAppliedAttachmentName));
      this.appliedAttachment = _source.UnitSpineCtrl.skeleton.data.defaultSkin.GetAttachment(index2, _source.SummonAppliedAttachmentName);
      _source.StartCoroutine(this.updateAttachmentChange(_source, index1));
    }

    private IEnumerator updateAttachmentChange(UnitCtrl _source, int _targetIndex)
    {
      SummonAction summonAction = this;
      while (true)
      {
        bool flag;
        do
        {
          yield return (object) null;
          flag = (_source.IsOther ? summonAction.battleManager.EnemyList : summonAction.battleManager.UnitList).Contains(_source.SummonUnitDictionary[summonAction.ActionId]);
          if (!summonAction.attachmentChanged & flag)
          {
            summonAction.attachmentChanged = true;
            (_source.UnitSpineCtrl.skeleton.skin == null ? _source.UnitSpineCtrl.skeleton.data.defaultSkin : _source.UnitSpineCtrl.skeleton.skin).AddAttachment(_targetIndex, _source.SummonTargetAttachmentName, summonAction.appliedAttachment);
            _source.UnitSpineCtrl.skeleton.slots.Items[_targetIndex].attachment = summonAction.appliedAttachment;
          }
        }
        while (!summonAction.attachmentChanged || flag);
        summonAction.attachmentChanged = false;
        (_source.UnitSpineCtrl.skeleton.skin == null ? _source.UnitSpineCtrl.skeleton.data.defaultSkin : _source.UnitSpineCtrl.skeleton.skin).AddAttachment(_targetIndex, _source.SummonTargetAttachmentName, summonAction.targetAttachment);
        _source.UnitSpineCtrl.skeleton.slots.Items[_targetIndex].attachment = summonAction.targetAttachment;
      }
    }

    public enum eSummonSide
    {
      OURS = 1,
      OTHER = 2,
    }

    public enum eSummonType
    {
      NONE = 0,
      SUMMON = 1,
      PHANTOM = 2,
      DIVISION = 1001, // 0x000003E9
    }

    public enum eMoveType
    {
      NORMAL = 1,
      LINEAR = 2,
    }
  }
}
