// Decompiled with JetBrains decompiler
// Type: Elements.SummonAction
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System.Collections;
using System.Collections.Generic;
using Elements.Battle;
using Spine;
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
      bool _isWaveEnemy = _ownerUnitCtrl != null && _ownerUnitCtrl.IsOther;
      Initialize();
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
      Dictionary<eValueNumber, FloatWithEx> _valueDictionary,
      System.Action<string> action)
    {
      base.ExecAction(_source, _target, _num, _sourceActionController, _skill, _starttime, _enabledChildAction, _valueDictionary);
      eUnitRespawnPos _basePos = (eUnitRespawnPos) Mathf.Min(9, Mathf.Max(0, (int) (_source.RespawnPos + (int) _valueDictionary[eValueNumber.VALUE_2])));
      UnitCtrl unitCtrl = battleManager.Summon(new SummonData
      {
        SummonId = ActionDetail2,
        TargetPosition = _target.GetLocalPosition() + new Vector3((_source.IsLeftDir || _source.IsForceLeftDir ? -1f : 1f) * _valueDictionary[eValueNumber.VALUE_7], 0.0f),
        Owner = _source,
        Skill = _skill,
        //Prefab = this.SummonPrefab,
        SummonSide = (eSummonSide) ActionDetail3,
        SummonType = (eSummonType) (float)_valueDictionary[eValueNumber.VALUE_5],
        MoveSpeed = _valueDictionary[eValueNumber.VALUE_1],
        FromPosition = _source.transform.localPosition + new Vector3((_source.IsLeftDir || _source.IsForceLeftDir ? -1f : 1f) * _valueDictionary[eValueNumber.VALUE_6], 0.0f, 0.0f),
        UseRespawnPos = (double) _valueDictionary[eValueNumber.VALUE_2] != 99.0,
        RespawnPos = BattleUtil.SearchRespawnPos(_basePos, _source.IsOther ? battleManager.EnemyList : battleManager.UnitList),
        ConsiderEquipmentAndBonus = ActionDetail1 == 3
      });
      int actionId = ActionId;
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
      if (_source.IdleOnly || battleManager.GameState != eBattleGameState.PLAY)
      {
        summonUnit1.IdleOnly = true;
        if (battleManager.UnitList.Contains(summonUnit1))
          battleManager.UnitList.Remove(summonUnit1);
      }
            action($"召唤{unitCtrl.UnitNameEx}{ActionDetail2}");
      if (_source.SummonTargetAttachmentName.IsNullOrEmpty() || appliedAttachment != null)
        return;
      int index1 = _source.UnitSpineCtrl.skeleton.slots.FindIndex(e => e.data.Name == _source.SummonTargetAttachmentName);
      targetAttachment = _source.UnitSpineCtrl.skeleton.GetAttachment(index1, _source.SummonTargetAttachmentName);
      int index2 = _source.UnitSpineCtrl.skeleton.slots.FindIndex(e => e.data.Name == _source.SummonAppliedAttachmentName);
      appliedAttachment = _source.UnitSpineCtrl.skeleton.data.defaultSkin.GetAttachment(index2, _source.SummonAppliedAttachmentName);
      _source.StartCoroutine(updateAttachmentChange(_source, index1));
    }

    private IEnumerator updateAttachmentChange(UnitCtrl _source, int _targetIndex)
    {
      SummonAction summonAction = this;
      while (true)
      {
        bool flag;
        do
        {
          yield return null;
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
