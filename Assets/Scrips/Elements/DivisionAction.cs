// Decompiled with JetBrains decompiler
// Type: Elements.DivisionAction
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using Elements.Battle;
using UnityEngine;

namespace Elements
{
  public class DivisionAction : ActionParameter
  {
    private List<DivisionData> divisionDataList = new List<DivisionData>();
    private List<UnitCtrl> divisionList = new List<UnitCtrl>();
    private const int POSITION_DIGID = 10;
    private const int UNDO_MOTION_NUM = 1;
    public const int UNDO_ERROR_MOTION_NUM = 2;

    public override void Initialize(UnitCtrl _ownerUnitCtrl)
    {
      Initialize();
      Action<int, float, eUnitRespawnPos> action = (_id, _position, _respawnPos) =>
      {
          if (_id == 0)
              return;
          //Singleton<BattleUnitLoader>.Instance.AddLoadResource(UnitUtility.GetSummonResourceId(UnitUtility.GetUnitResourceId(_id)), (Action<GameObject>) (_loadedObject => this.divisionDataList.Add(new DivisionAction.DivisionData()
          //{
          //  Prefab = _loadedObject,
          //  DivisionId = _id,
          //  PositionX = _position,
          //  RespawnPos = _respawnPos
          //})));
      };
      action(ActionDetail1, Value[eValueNumber.VALUE_3] / 10f, (eUnitRespawnPos) (Math.Abs(Value[eValueNumber.VALUE_3]) % 10.0));
      action(ActionDetail1, Value[eValueNumber.VALUE_4] / 10f, (eUnitRespawnPos) (Math.Abs(Value[eValueNumber.VALUE_4]) % 10.0));
      action(ActionDetail1, Value[eValueNumber.VALUE_5] / 10f, (eUnitRespawnPos) (Math.Abs(Value[eValueNumber.VALUE_5]) % 10.0));
     // ManagerSingleton<ResourceManager>.Instance.StartLoad();
    }

    public override void ExecAction(
      UnitCtrl _source,
      BasePartsData _target,
      int _num,
      UnitActionController _sourceActionController,
      Skill _skill,
      float _startTime,
      Dictionary<int, bool> _enabledChildAction,
      Dictionary<eValueNumber, FloatWithEx> _valueDictionary)
    {
      base.ExecAction(_source, _target, _num, _sourceActionController, _skill, _startTime, _enabledChildAction, _valueDictionary);
      int defeatCount = 0;
      _source.OnStartErrorUndoDivision = _isTimeover =>
      {
          if (_isTimeover)
          {
              BattleSpineController currentSpineCtrl = _source.GetCurrentSpineCtrl();
              currentSpineCtrl.PlayAnime(currentSpineCtrl.ConvertAnimeIdToAnimeName(_skill.AnimId, _skill.SkillNum, 2), false);
          }
          else
              _source.PlayAnimeNoOverlap(_skill.AnimId, _skill.SkillNum, 2);
          _sourceActionController.CreateNormalPrefabWithTargetMotion(_skill, 2, false, _isTimeover);
      };
      for (int index = 0; index < divisionDataList.Count; ++index)
      {
        DivisionData divisionData = divisionDataList[index];
        _source.IsDivisionSourceForDamage = true;
        _source.IsDivisionSourceForDie = true;
        UnitCtrl unit = battleManager.Summon(new SummonData
        {
          SummonId = divisionData.DivisionId,
          TargetPosition = _target.GetLocalPosition() + new Vector3((_source.IsLeftDir ? -1f : 1f) * divisionData.PositionX, 0.0f),
          Owner = _source,
          Skill = _skill,
          Prefab = divisionData.Prefab,
          SummonSide = SummonAction.eSummonSide.OURS,
          SummonType = SummonAction.eSummonType.DIVISION,
          MoveSpeed = 0.0f,
          FromPosition = _source.transform.localPosition + new Vector3(_source.IsLeftDir ? -1f : 1f, 0.0f, 0.0f),
          RespawnPos = BattleUtil.SearchRespawnPos(divisionData.RespawnPos, _source.IsOther ? battleManager.EnemyList : battleManager.UnitList)
        });
        unit.SummonSource = _source;
        unit.MaxHp = Mathf.CeilToInt((long) _source.Hp / (float) divisionDataList.Count);
        unit.SetCurrentHp(unit.MaxHp);
        bool defeat = false;
        unit.OnDamageForDivision = (_byAttack, _damage, _critical) =>
        {
            _source.SetDamage(new DamageData
            {
                Damage = (long) (int) _damage,
                IsDivisionDamage = true,
                ActionType = eActionType.DIVISION
            }, false, 0, _hasEffect: false);
            if (((double) _valueDictionary[eValueNumber.VALUE_1] != 3.0 || (long) unit.Hp > 0L ? 0 : (!defeat ? 1 : 0)) == 0)
                return;
            defeat = true;
            ++defeatCount;
            if (defeatCount != (double) _valueDictionary[eValueNumber.VALUE_2])
                return;
            unDoDivision(_source, _sourceActionController, _skill);
        };
        divisionList.Add(unit);
      }
      _source.DisappearForDivision(false, false);
      _source.AppendCoroutine(waitMotionEnd(_source, _sourceActionController, _skill), ePauseType.SYSTEM);
      eReleaseType eReleaseType = (eReleaseType)(float)_valueDictionary[eValueNumber.VALUE_1];
      if (eReleaseType != eReleaseType.TIME)
      {
        int num = (int) (eReleaseType - 2);
      }
      else
        _sourceActionController.AppendCoroutine(waitTime(_valueDictionary[eValueNumber.VALUE_2], _source, _sourceActionController, _skill), ePauseType.SYSTEM);
    }

    private IEnumerator waitMotionEnd(
      UnitCtrl _source,
      UnitActionController _sourceActionController,
      Skill _skill)
    {
      while ((isActionCancel(_source, _sourceActionController, _skill) ? 1 : (!_source.IsDivisionSourceForDamage ? 1 : 0)) == 0)
      {
        if (!_source.GetCurrentSpineCtrl().IsPlayAnimeBattle)
        {
          _source.gameObject.SetActive(false);
          break;
        }
        yield return null;
      }
    }

    private IEnumerator waitTime(
      float _startTime,
      UnitCtrl _source,
      UnitActionController _sourceActionController,
      Skill _skill)
    {
      DivisionAction divisionAction = this;
      while (true)
      {
        _startTime -= divisionAction.battleManager.DeltaTime_60fps;
        if (_startTime >= 0.0)
          yield return null;
        else
          break;
      }
      divisionAction.unDoDivision(_source, _sourceActionController, _skill);
    }

    private void unDoDivision(
      UnitCtrl _source,
      UnitActionController _sourceActionController,
      Skill _skill)
    {
      (_source.IsOther ? battleManager.EnemyList : battleManager.UnitList).Add(_source);
      BattleManager.Instance.shouldUpdateSkillTarget = true;
            _source.PlayAnimeNoOverlap(_skill.AnimId, _skill.SkillNum, 1);
      _sourceActionController.CreateNormalPrefabWithTargetMotion(_skill, 1, false);
      _source.AppendCoroutine(updateUndoMotion(_source, _sourceActionController, _skill), ePauseType.SYSTEM, _source);
      _source.gameObject.SetActive(true);
      _source.IsDivisionSourceForDamage = false;
      for (int index = 0; index < divisionList.Count; ++index)
        divisionList[index].DisappearForDivision(true, false);
    }

    private IEnumerator updateUndoMotion(
      UnitCtrl _source,
      UnitActionController _sourceActionController,
      Skill _skill)
    {
      while (!isActionCancel(_source, _sourceActionController, _skill))
      {
          if (!_source.UnitSpineCtrl.IsPlayAnimeBattle)
        {
          _source.IdleOnly = false;
          _source.SetState(UnitCtrl.ActionState.IDLE);
          _source.IsDivisionSourceForDie = false;
          yield break;
        }

          yield return null;
      }
      _source.IsDivisionSourceForDie = false;
    }

    private bool isActionCancel(
      UnitCtrl _source,
      UnitActionController _sourceActionController,
      Skill _skill)
    {
      if ((_source.IsUnableActionState() ? 1 : (_source.IsCancelActionState(_skill.SkillId == _source.UnionBurstSkillId) ? 1 : 0)) == 0)
        return false;
      _source.CancelByAwake = false;
      _source.CancelByConvert = false;
      _source.CancelByToad = false;
      _sourceActionController.CancelAction(_skill.SkillId);
      return true;
    }

    private class DivisionData
    {
      public int DivisionId { get; set; }

      public GameObject Prefab { get; set; }

      public float PositionX { get; set; }

      public eUnitRespawnPos RespawnPos { get; set; }
    }

    private enum eReleaseType
    {
      TIME = 1,
      HP = 2,
      DEFEAT = 3,
    }
  }
}
