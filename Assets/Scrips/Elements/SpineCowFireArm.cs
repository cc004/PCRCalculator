// Decompiled with JetBrains decompiler
// Type: Elements.SpineCowFireArm
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using Elements.Battle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elements
{
  public class SpineCowFireArm : FirearmCtrl
  {
    private const int DEFAULT_OFFSET = 200;
    public GameObject WalkEffectPrefab;
    public GameObject ReturnWalkEffectPrefab;
    public GameObject AttackEffectPrefab;
    [SerializeField]
    private int skillId;
    [SerializeField]
    private int index;
    [SerializeField]
    private ShakeEffect shake;
    [SerializeField]
    private bool isLeftDir;
    private const float ATTACK_EFFECT_DELAY = 0.3f;
    //private static DAGLMEOBLAA staticBattleEffectPool;
    //private static KHEAEOIEDJN staticBattleCameraEffect;
    private static bool isStaticInit;

    private BattleSpineController unitSpineController { get; set; }

    private SkillEffectCtrl effectSmoke { get; set; }

    private bool turn { get; set; }

    private bool ownerIsLeftDir { get; set; }

    protected override bool getStopFlag() => false;

    //protected DAGLMEOBLAA battleEffectPool => SpineCowFireArm.staticBattleEffectPool;

    //protected KHEAEOIEDJN battleCameraEffect => SpineCowFireArm.staticBattleCameraEffect;

    public new static void StaticRelease()
    {
      SpineCowFireArm.isStaticInit = false;
      //SpineCowFireArm.staticBattleCameraEffect = (KHEAEOIEDJN) null;
      //SpineCowFireArm.staticBattleEffectPool = (DAGLMEOBLAA) null;
    }

    /*protected override void Awake()
    {
      base.Awake();
      BattleSpineController.LoadCreateImmidiateBySkinId(eSpineType.SKILL_EFFECT, this.skillId, this.skillId, this.index, this.transform, (System.Action<BattleSpineController>) (unit =>
      {
        this.unitSpineController = unit;
        this.unitSpineController.Depth = 200;
      }));
      if (SpineCowFireArm.isStaticInit)
        return;
      SpineCowFireArm.staticBattleCameraEffect = (KHEAEOIEDJN) this.singletonTree.Get<CMMLKFHCEPD>();
      SpineCowFireArm.staticBattleEffectPool = (DAGLMEOBLAA) this.singletonTree.Get<PPODNPPHKCJ>();
      SpineCowFireArm.isStaticInit = true;
    }*/

    protected override void onDestroy()
    {
      this.unitSpineController = (BattleSpineController) null;
      this.WalkEffectPrefab = (GameObject) null;
      this.ReturnWalkEffectPrefab = (GameObject) null;
      this.AttackEffectPrefab = (GameObject) null;
      this.effectSmoke = (SkillEffectCtrl) null;
      this.shake = (ShakeEffect) null;
      base.onDestroy();
    }

    protected override void setInitialPosition() => this.initialPosistion = new Vector3((float) ((this.isLeftDir ? -1.0 : 1.0) * -2500.0 / 540.0), this.FireTarget.GetPosition().y);

    /*public override void Initialize(
      BasePartsData _target,
      List<ActionParameter> _actions,
      Skill _skill,
      List<NormalSkillEffect> _skillEffect,
      UnitCtrl _owner,
      float _height,
      bool _hasBlackOutTime,
      bool _isAbsolute,
      Vector3 _targetPosition,
      List<ShakeEffect> _shakes,
      eTargetBone _targetBone)
    {
      base.Initialize(_target, _actions, _skill, _skillEffect, _owner, _height, _hasBlackOutTime, _isAbsolute, _targetPosition, _shakes, _targetBone);
      this.unitSpineController.Owner = _owner;
      this.unitSpineController.PlayAnimeNoOverlap(eSpineCharacterAnimeId.SUMMON_SKILL_EFFECT_WALK, this.skillId);
      this.onCowHit = new System.Action(this.stopAndTurn);
      Vector3 position = this.transform.position;
      position.y = this.FireTarget.GetPosition().y;
      this.FireTarget.Owner.OnIsFrontFalse += (System.Action) (() => this.unitSpineController.Depth = this.FireTarget.Owner.GetSortOrderConsiderBlackout() + 200);
      position.x = (float) ((this.isLeftDir ? -1.0 : 1.0) * -2500.0 / 540.0);
      this.unitSpineController.Depth = this.FireTarget.Owner.GetSortOrderConsiderBlackout() + 200;
      this.transform.position = position;
      this.transform.localScale = new Vector3((this.isLeftDir ? -1f : 1f) * Mathf.Abs(this.transform.localScale.x), this.transform.localScale.y, this.transform.localScale.z);
      this.effectSmoke = this.battleEffectPool.GetEffect(this.isLeftDir ? this.ReturnWalkEffectPrefab : this.WalkEffectPrefab);
      this.effectSmoke.SortTarget = this.FireTarget.Owner;
      this.effectSmoke.transform.position = this.transform.position;
      this.effectSmoke.transform.parent = ExceptNGUIRoot.Transform;
      this.effectSmoke.GetComponent<TrackingObject>().SetTarget(this.transform);
      this.ownerIsLeftDir = _owner.IsLeftDir;
      this.effectSmoke.GetComponent<SkillEffectCtrl>().PlaySe(_owner.SoundUnitId, this.ownerIsLeftDir);
      this.effectSmoke.ExecAppendCoroutine(this.owner);
      this.shake.ShakeLoopEnd = false;
      this.battleCameraEffect.StartShake(this.shake, this.Skill, _owner);
    }*/

    /*private void stopAndTurn()
    {
      this.shake.ShakeLoopEnd = true;
      this.OnHitAction = (System.Action<FirearmCtrl>) null;
      this.effectSmoke.SetTimeToDie(true);
      this.speed = Vector3.zero;
      this.unitSpineController.PlayAnimeNoOverlap(eSpineCharacterAnimeId.SUMMON_SKILL_EFFECT_ATTACK, this.skillId, false);
      ManagerSingleton<SoundManager>.Instance.PlaySeByOuterSource(this.SeSource, _cueSheetName: UnitCtrl.ConvertToSkillCueSheetName(this.owner.SoundUnitId), _cueName: this.unitSpineController.ConvertAnimeIdToAnimeName(eSpineCharacterAnimeId.SUMMON_SKILL_EFFECT_ATTACK, this.skillId));
      this.AppendCoroutine(this.updateAttack(), ePauseType.SYSTEM, this.owner);
      this.turn = true;
    }*/

    /*private IEnumerator updateAttack()
    {
      SpineCowFireArm spineCowFireArm = this;
      bool effectCreated = false;
      float time = 0.0f;
      while (true)
      {
        time += spineCowFireArm.battleManager.DeltaTime_60fps;
        if (spineCowFireArm.unitSpineController.IsPlayAnimeBattle)
        {
          if ((double) time > 0.300000011920929 && !effectCreated)
          {
            effectCreated = true;
            SkillEffectCtrl effect = spineCowFireArm.battleEffectPool.GetEffect(spineCowFireArm.AttackEffectPrefab);
            effect.transform.parent = ExceptNGUIRoot.Transform;
            effect.transform.position = spineCowFireArm.FireTarget.GetBottomTransformPosition() + spineCowFireArm.FireTarget.GetFixedCenterPos();
            effect.InitializeSort();
            effect.PlaySe(spineCowFireArm.owner.SoundUnitId, spineCowFireArm.owner.IsLeftDir);
            effect.SortTarget = spineCowFireArm.FireTarget.Owner;
            effect.ExecAppendCoroutine(spineCowFireArm.owner);
            if (spineCowFireArm.FireTarget.Owner.IsFront)
              effect.SetSortOrderFront();
            else
              effect.SetSortOrderBack();
          }
          yield return (object) null;
        }
        else
          break;
      }
      spineCowFireArm.transform.localScale = new Vector3(-spineCowFireArm.transform.localScale.x, spineCowFireArm.transform.localScale.y, spineCowFireArm.transform.localScale.z);
      spineCowFireArm.unitSpineController.PlayAnimeNoOverlap(eSpineCharacterAnimeId.SUMMON_SKILL_EFFECT_WALK, spineCowFireArm.skillId);
      spineCowFireArm.speed = new Vector3(-spineCowFireArm.MoveRate, 0.0f, 0.0f);
      spineCowFireArm.shake.ShakeLoopEnd = false;
      spineCowFireArm.battleCameraEffect.StartShake(spineCowFireArm.shake, spineCowFireArm.Skill, spineCowFireArm.owner);
      spineCowFireArm.effectSmoke = spineCowFireArm.battleEffectPool.GetEffect(spineCowFireArm.isLeftDir ? spineCowFireArm.WalkEffectPrefab : spineCowFireArm.ReturnWalkEffectPrefab);
      spineCowFireArm.effectSmoke.SortTarget = spineCowFireArm.FireTarget.Owner;
      spineCowFireArm.effectSmoke.transform.position = spineCowFireArm.transform.position;
      spineCowFireArm.effectSmoke.transform.parent = ExceptNGUIRoot.Transform;
      spineCowFireArm.effectSmoke.ExecAppendCoroutine(spineCowFireArm.owner);
      spineCowFireArm.effectSmoke.GetComponent<TrackingObject>().SetTarget(spineCowFireArm.transform);
      spineCowFireArm.effectSmoke.GetComponent<SkillEffectCtrl>().PlaySe(spineCowFireArm.owner.SoundUnitId, spineCowFireArm.ownerIsLeftDir);
    }*/

    /*public override bool _Update()
    {
      if ((this.isLeftDir ? ((double) this.transform.position.x > 4.62962961196899 ? 1 : 0) : ((double) this.transform.position.x < -4.62962961196899 ? 1 : 0)) != 0)
      {
        this.shake.ShakeLoopEnd = true;
        this.timeToDie = true;
        this.effectSmoke.SetTimeToDie(true);
      }
      int num = base._Update() ? 1 : 0;
      if (!this.FireTarget.Owner.IsDead)
        return num != 0;
      if (this.turn)
        return num != 0;
      this.effectSmoke.SetTimeToDie(true);
      this.turn = true;
      this.transform.localScale = new Vector3((this.isLeftDir ? 1f : -1f) * this.transform.localScale.x, this.transform.localScale.y, this.transform.localScale.z);
      this.speed = new Vector3(-this.MoveRate, 0.0f, 0.0f);
      return num != 0;
    }*/

    public override void Pause() => this.unitSpineController.Pause();

    public override void Resume() => this.unitSpineController.Resume();
  }
}
