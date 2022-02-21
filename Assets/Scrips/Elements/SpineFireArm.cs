// Decompiled with JetBrains decompiler
// Type: Elements.SpineFireArm
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

namespace Elements
{
  public class SpineFireArm : FirearmCtrl
  {
    [SerializeField]
    private eSortOrder sortOrder;
    private const int OFFSET = 1;
    [SerializeField]
    private eSortOrderType sortOrderType = eSortOrderType.BACK_OF_FX;
    private const int DEFAULT_OFFSET = 200;
    public List<GameObject> WalkEffectPrefab;
    public List<GameObject> WalkEffectPrefabLeft;
    [SerializeField]
    private float moveDuration;
    [SerializeField]
    private float sortChangeTime;
    [SerializeField]
    private int skillId;
    [SerializeField]
    private int index;
    [SerializeField]
    private bool isLeftDir;
    [SerializeField]
    private Vector3 targetPosition;
    //private static DAGLMEOBLAA staticBattleEffectPool;
    private static bool isStaticInit;

    private BattleSpineController unitSpineController { get; set; }

    private List<SkillEffectCtrl> effectSmokeList { get; set; }

    protected override bool getStopFlag() => false;

    //protected DAGLMEOBLAA battleEffectPool => SpineFireArm.staticBattleEffectPool;

    /*public new static void StaticRelease()
    {
      SpineFireArm.isStaticInit = false;
      SpineFireArm.staticBattleEffectPool = (DAGLMEOBLAA) null;
    }

    protected override void Awake()
    {
      base.Awake();
      BattleSpineController.LoadCreateImmidiateBySkinId(eSpineType.SKILL_EFFECT, this.skillId, this.skillId, this.index, this.transform, (System.Action<BattleSpineController>) (unit =>
      {
        this.unitSpineController = unit;
        this.unitSpineController.Depth = 200;
      }));
      if (SpineFireArm.isStaticInit)
        return;
      SpineFireArm.staticBattleEffectPool = (DAGLMEOBLAA) this.singletonTree.Get<PPODNPPHKCJ>();
      SpineFireArm.isStaticInit = true;
    }

    protected override void onDestroy()
    {
      this.unitSpineController = (BattleSpineController) null;
      this.WalkEffectPrefab = (List<GameObject>) null;
      this.WalkEffectPrefabLeft = (List<GameObject>) null;
      this.effectSmokeList = (List<SkillEffectCtrl>) null;
      base.onDestroy();
    }

    public override void Initialize(
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
      this.FireTarget = _target;
      this.owner = _owner;
      this.Skill = _skill;
      this.unitSpineController.Owner = _owner;
      this.unitSpineController.PlayAnimeNoOverlap(eSpineCharacterAnimeId.SUMMON_SKILL_EFFECT, this.skillId, false);
      int num = this.sortOrderType == SpineFireArm.eSortOrderType.FRONT_OF_FX ? 1 : -1;
      switch (this.sortOrder)
      {
        case SpineFireArm.eSortOrder.FOREMOST:
          this.unitSpineController.Depth = 11000 + num;
          break;
        case SpineFireArm.eSortOrder.FRONT:
          this.unitSpineController.Depth = BattleDefine.GetUnitSortOrder(this.owner) + 300 + num;
          break;
        case SpineFireArm.eSortOrder.BACK:
          this.unitSpineController.Depth = BattleDefine.GetUnitSortOrder(this.owner) - 300 + num;
          break;
        case SpineFireArm.eSortOrder.TOP_BACK:
          this.unitSpineController.Depth = 690 + num;
          break;
      }
      if (this.owner.IsFront)
        this.unitSpineController.Depth += 11500;
      this.transform.position = this.owner.BottomTransform.transform.position;
      this.transform.localScale = new Vector3((this.isLeftDir ? -1f : 1f) * Mathf.Abs(this.transform.localScale.x), this.transform.localScale.y, this.transform.localScale.z);
      this.battleManager.AppendCoroutine(this.updateAppear(), ePauseType.SYSTEM, (double) this.Skill.BlackOutTime > 0.0 ? this.owner : (UnitCtrl) null);
    }

    private IEnumerator updateAppear()
    {
      while (this.unitSpineController.IsPlayAnimeBattle)
        yield return (object) null;
      this.startMove();
    }

    private void startMove()
    {
      this.unitSpineController.PlayAnimeNoOverlap(eSpineCharacterAnimeId.SUMMON_SKILL_EFFECT_WALK, this.skillId);
      List<GameObject> gameObjectList = this.isLeftDir ? this.WalkEffectPrefabLeft : this.WalkEffectPrefab;
      this.effectSmokeList = new List<SkillEffectCtrl>();
      for (int index = 0; index < gameObjectList.Count; ++index)
      {
        SkillEffectCtrl effect = this.battleEffectPool.GetEffect(gameObjectList[index]);
        effect.SortTarget = this.owner;
        effect.InitializeSort();
        effect.transform.position = this.transform.position;
        effect.transform.parent = ExceptNGUIRoot.Transform;
        effect.GetComponent<TrackingObject>().SetTarget(this.transform);
        effect.GetComponent<SkillEffectCtrl>().PlaySe(this.owner.SoundUnitId, this.owner.IsLeftDir);
        effect.ExecAppendCoroutine(this.owner);
        this.effectSmokeList.Add(effect);
      }
      this.battleManager.AppendCoroutine(this.updatePosition(), ePauseType.SYSTEM, (double) this.Skill.BlackOutTime > 0.0 ? this.owner : (UnitCtrl) null);
    }

    private IEnumerator updatePosition()
    {
      SpineFireArm spineFireArm = this;
      float time = spineFireArm.moveDuration;
      bool sortChanged = false;
      while (true)
      {
        spineFireArm.transform.position = Vector3.Lerp(spineFireArm.FireTarget.GetBottomTransformPosition() + spineFireArm.targetPosition, spineFireArm.owner.BottomTransform.position, time / spineFireArm.moveDuration);
        time -= spineFireArm.battleManager.DeltaTime_60fps;
        if ((double) spineFireArm.moveDuration - (double) time > (double) spineFireArm.sortChangeTime && !sortChanged)
        {
          sortChanged = true;
          spineFireArm.unitSpineController.Depth = spineFireArm.FireTarget.Owner.SortOrder + 200;
          for (int index = 0; index < spineFireArm.effectSmokeList.Count; ++index)
          {
            SkillEffectCtrl effectSmoke = spineFireArm.effectSmokeList[index];
            effectSmoke.SortTarget = spineFireArm.FireTarget.Owner;
            if (spineFireArm.FireTarget.Owner.IsFront)
              effectSmoke.SetSortOrderFront();
            else
              effectSmoke.SetSortOrderBack();
          }
        }
        if ((double) time >= 0.0 && !BattleUtil.Approximately(time, 0.0f))
          yield return (object) null;
        else
          break;
      }
      spineFireArm.startAttack();
    }

    private void startAttack()
    {
      this.OnHitAction = (System.Action<FirearmCtrl>) null;
      this.effectSmokeSetTimeToDie();
      this.speed = Vector3.zero;
      this.unitSpineController.PlayAnimeNoOverlap(eSpineCharacterAnimeId.SUMMON_SKILL_EFFECT_ATTACK, this.skillId, false);
      ManagerSingleton<SoundManager>.Instance.PlaySeByOuterSource(this.SeSource, _cueSheetName: UnitCtrl.ConvertToSkillCueSheetName(this.owner.SoundUnitId), _cueName: this.unitSpineController.ConvertAnimeIdToAnimeName(eSpineCharacterAnimeId.SUMMON_SKILL_EFFECT_ATTACK, this.skillId));
      this.AppendCoroutine(this.updateAttack(), ePauseType.SYSTEM, this.owner);
    }

    private void effectSmokeSetTimeToDie()
    {
      for (int index = 0; index < this.effectSmokeList.Count; ++index)
      {
        if (this.effectSmokeList[index].IsRepeat)
          this.effectSmokeList[index].SetTimeToDie(true);
      }
    }

    private IEnumerator updateAttack()
    {
      SpineFireArm spineFireArm = this;
      while (spineFireArm.unitSpineController.IsPlayAnimeBattle)
        yield return (object) null;
      spineFireArm.SetTimeToDie(true);
    }

    public override bool _Update()
    {
      if ((this.isLeftDir ? ((double) this.transform.position.x > 4.62962961196899 ? 1 : 0) : ((double) this.transform.position.x < -4.62962961196899 ? 1 : 0)) != 0)
      {
        this.timeToDie = true;
        this.effectSmokeSetTimeToDie();
      }
      int num = base._Update() ? 1 : 0;
      if (!this.FireTarget.Owner.IsDead)
        return num != 0;
      this.effectSmokeSetTimeToDie();
      return num != 0;
    }

    public override void Pause() => this.unitSpineController.Pause();

    public override void Resume() => this.unitSpineController.Resume();

    public override void SetSortOrderBack()
    {
      base.SetSortOrderBack();
      if (!((UnityEngine.Object) this.unitSpineController != (UnityEngine.Object) null))
        return;
      this.setSortOrderBackImpl();
    }

    private void setSortOrderBackImpl()
    {
      int num = this.sortOrderType == SpineFireArm.eSortOrderType.FRONT_OF_FX ? 1 : -1;
      switch (this.sortOrder)
      {
        case SpineFireArm.eSortOrder.FOREMOST:
          this.unitSpineController.Depth = 11000 + num;
          break;
        case SpineFireArm.eSortOrder.FRONT:
          this.unitSpineController.Depth = BattleDefine.GetUnitSortOrder(this.SortTarget) + 300 + num;
          break;
        case SpineFireArm.eSortOrder.BACK:
          this.unitSpineController.Depth = BattleDefine.GetUnitSortOrder(this.SortTarget) - 300 + num;
          break;
        case SpineFireArm.eSortOrder.TOP_BACK:
          this.unitSpineController.Depth = 690 + num;
          break;
      }
    }*/

    private enum eSortOrder
    {
      DEFAULT,
      FOREMOST,
      FRONT,
      BACK,
      TOP_BACK,
    }

    private enum eSortOrderType
    {
      FRONT_OF_FX,
      BACK_OF_FX,
    }
  }
}
