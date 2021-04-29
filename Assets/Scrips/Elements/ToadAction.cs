// Decompiled with JetBrains decompiler
// Type: Elements.ToadAction
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using Cute;
using Spine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elements
{
  public class ToadAction : ActionParameter
  {
    private SystemIdDefine.eWeaponMotionType targetMotionType;
    private SystemIdDefine.eWeaponSeType targetSeType;

    public override void Initialize()
    {
      base.Initialize();
      //int actionDetail1 = this.ActionDetail1;
      //Singleton<BattleUnitLoader>.Instance.AddLoadResource(actionDetail1, (Action<GameObject>) (_loadedObject => this.SummonPrefab = _loadedObject));
      //MasterUnitData.UnitData unitData = ManagerSingleton<MasterDataManager>.Instance.masterUnitData.Get(actionDetail1);
      //this.targetSeType = (SystemIdDefine.eWeaponSeType) (int) unitData.SeType;
      //this.targetMotionType = (SystemIdDefine.eWeaponMotionType) (int) unitData.MotionType;
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
            /*if (_target.Owner.IsDead)
              return;
            if (_target.Owner.ToadDatas.Count > 0)
            {
              ToadData toadData = _target.Owner.ToadDatas[0];
              toadData.DisableByNextToad = true;
              toadData.BattleSpineController.gameObject.SetActive(false);
              _target.Owner.ToadDatas.RemoveAt(0);
              _target.Owner.OnChangeState.Call<UnitCtrl, eStateIconType, bool>(_target.Owner, toadData.StateIconType, false);
              if (_target.Owner.IsFront)
                _target.Owner.SetSortOrderFront();
              else
                _target.Owner.SetSortOrderBack();
              _target.Owner.GetCurrentSpineCtrl().SetActive(true);
            }
            if (_target.Owner.IsAbnormalState(UnitCtrl.eAbnormalState.PAUSE_ACTION))
              _target.Owner.GetCurrentSpineCtrl().IsColorPauseAction = false;
            UnitCtrl unitCtrl = UnityEngine.Object.Instantiate<GameObject>(this.SummonPrefab).GetComponent<UnitCtrl>();
            //unitCtrl.transform.parent = ExceptNGUIRoot.Transform;
            unitCtrl.WeaponSeType = this.targetSeType;
            unitCtrl.WeaponMotionType = this.targetMotionType;
            UnitActionController unitactioncontroller = unitCtrl.GetComponent<UnitActionController>();
            unitactioncontroller.Initialize(_target.Owner, _target.Owner.UnitParameter, true, unitCtrl);
            BattleSpineController.LoadCreateImmidiate(eSpineType.SD_NORMAL_BOUND, this.ActionDetail1, 0, _target.Owner.transform.TargetTransform, (Action<BattleSpineController>) (_obj =>
            {
              _obj.transform.localScale = new Vector3(unitCtrl.Scale, Mathf.Abs(unitCtrl.Scale), 1f);
              _obj.Owner = _source;
              _obj.LoadAnimationIDImmediately(eSpineBinaryAnimationId.COMMON_BATTLE);
              _obj.LoadAnimationIDImmediately(eSpineBinaryAnimationId.BATTLE);
              _obj.SetAnimeEventDelegateForBattle((Action) (() => _obj.IsStopState = true));
              _target.Owner.GetCurrentSpineCtrl().SetActive(false);
              ToadData _toadData = new ToadData()
              {
                StateIconType = (eStateIconType) _valueDictionary[eValueNumber.VALUE_4],
                BattleSpineController = _obj,
                LeftDirScale = new Vector3(-unitCtrl.Scale, Mathf.Abs(unitCtrl.Scale), 1f),
                RightDirScale = new Vector3(unitCtrl.Scale, Mathf.Abs(unitCtrl.Scale), 1f),
                UnitActionController = unitactioncontroller,
                Enable = true,
                StateBone = _obj.skeleton.FindBone("State"),
                          ReleaseByHeal = this.ActionDetail2 == 1

              };
              _target.Owner.ToadDatas.Add(_toadData);
              _target.Owner.OnChangeState.Call<UnitCtrl, eStateIconType, bool>(_target.Owner, _toadData.StateIconType, true);
              if ((long) _target.Owner.Hp > 0L)
              {
                if (!_target.Owner.IsUnableActionState() && _target.Owner.CurrentState != UnitCtrl.ActionState.DAMAGE)
                  _target.Owner.SetState(UnitCtrl.ActionState.IDLE);
                if (_target.Owner.IsAbnormalState(UnitCtrl.eAbnormalState.PAUSE_ACTION))
                {
                  BattleSpineController currentSpineCtrl = _target.Owner.GetCurrentSpineCtrl();
                  currentSpineCtrl.IsColorPauseAction = true;
                  currentSpineCtrl.Pause();
                }
              }
              _target.Owner.AppendCoroutine(this.updateToad(_toadData, _target, _skill, _valueDictionary), ePauseType.SYSTEM, _target.Owner);
              if (_target.Owner.IsFront)
                _target.Owner.SetSortOrderFront();
              else
                _target.Owner.SetSortOrderBack();
              _target.Owner.SetLeftDirection(_target.Owner.IsLeftDir);
            }));
            _target.Owner.CancelByToad = true;*/
        }

        private IEnumerator updateToad(
      ToadData _toadData,
      BasePartsData _target,
      Skill _skill,
      Dictionary<eValueNumber, float> _valueDictionary)
    {
      ToadAction toadAction = this;
      while ((double) _toadData.Timer <= (double) _valueDictionary[eValueNumber.VALUE_1] || _target.Owner.CurrentState != UnitCtrl.ActionState.IDLE)
      {
        _toadData.Timer += _target.Owner.DeltaTimeForPause;
        if (_toadData.DisableByNextToad)
          yield break;
        else if (_toadData.Enable && (!_target.Owner.IdleOnly || (long) _target.Owner.Hp <= 0L))
          yield return (object) null;
        else
          break;
      }
      if (!_toadData.DisableByNextToad)
      {
        /*foreach (NormalSkillEffect actionEffect in toadAction.ActionEffectList)
        {
          GameObject MDOJNMEMHLN = _target.Owner.IsLeftDir ? actionEffect.PrefabLeft : actionEffect.Prefab;
          SkillEffectCtrl effect = toadAction.battleEffectPool.GetEffect(MDOJNMEMHLN);
          effect.transform.parent = ExceptNGUIRoot.Transform;
          effect.SortTarget = _target.Owner;
          effect.InitializeSort();
          effect.SetPossitionAppearanceType(actionEffect, _target, _target.Owner, _skill);
          effect.ExecAppendCoroutine();
        }*/
        _toadData.Timer = 0.0f;
        _target.Owner.CancelByToad = true;
        _target.Owner.ToadRelease = true;
        while ((double) _toadData.Timer < (double) _valueDictionary[eValueNumber.VALUE_3])
        {
          if (_toadData.DisableByNextToad)
          {
            yield break;
          }
          else
          {
            _toadData.Timer += _target.Owner.DeltaTimeForPause;
            yield return (object) null;
          }
        }
        if (!_toadData.DisableByNextToad)
        {
          _target.Owner.PlayAnime(eSpineCharacterAnimeId.DIE, _isLoop: false);
          BattleSpineController spineCtrl = _target.Owner.GetCurrentSpineCtrl();
          if (_target.Owner.IsUnableActionState())
            spineCtrl.Resume();
          //_target.Owner.PlayDieEffect();
          while (spineCtrl.IsPlayAnimeBattle)
          {
            if (_toadData.DisableByNextToad)
              yield break;
            else
              yield return (object) null;
          }
          if (!_toadData.DisableByNextToad)
          {
            _target.Owner.ToadRelease = false;
            _target.Owner.ToadDatas.Remove(_toadData);
            _target.Owner.OnChangeState.Call<UnitCtrl, eStateIconType, bool>(_target.Owner, _toadData.StateIconType, false);
            _toadData.BattleSpineController.gameObject.SetActive(false);
            Color curColor = _toadData.BattleSpineController.CurColor;
            BattleSpineController currentSpineCtrl = _target.Owner.GetCurrentSpineCtrl();
            currentSpineCtrl.CurColor = curColor;
            _target.Owner.PlayAnime(eSpineCharacterAnimeId.IDLE, _target.Owner.MotionPrefix);
            if (_target.Owner.ToadReleaseDamage)
            {
              _target.Owner.ToadReleaseDamage = false;
              _target.Owner.SetState(UnitCtrl.ActionState.DAMAGE);
            }
            if (_target.Owner.IsUnableActionState())
            {
              currentSpineCtrl.PlayAnime(eSpineCharacterAnimeId.DAMEGE);
              TrackEntry current = currentSpineCtrl.state.GetCurrent(0);
              if (current != null)
              {
                                current.lastTime = currentSpineCtrl.StopStateTime;
                                current.time = currentSpineCtrl.StopStateTime;
                                //current.animationLast = currentSpineCtrl.StopStateTime;
                                //current.animationStart = currentSpineCtrl.StopStateTime; 
                            }
              currentSpineCtrl.Pause();
            }
            currentSpineCtrl.SetActive(true);
            if (_target.Owner.IsFront)
              _target.Owner.SetSortOrderFront();
            else
              _target.Owner.SetSortOrderBack();
            if (_target.Owner.IdleOnly)
            {
              if (_target.Owner.CurrentState == UnitCtrl.ActionState.IDLE)
                _target.Owner.CurrentState = UnitCtrl.ActionState.WALK;
              _target.Owner.SetState(UnitCtrl.ActionState.IDLE);
            }
            if (_target.Owner.DieInToad)
              _target.Owner.SetState(UnitCtrl.ActionState.DIE);
          }
        }
      }
    }

    public override void SetLevel(float _level) => this.Value[eValueNumber.VALUE_1] = (float) ((double) this.MasterData.action_value_1 + (double) this.MasterData.action_value_2 * (double) _level);
  }
}
