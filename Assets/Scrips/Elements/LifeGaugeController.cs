// Decompiled with JetBrains decompiler
// Type: Elements.LifeGaugeController
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using Elements.Battle;
using System.Collections;
using UnityEngine;

namespace Elements
{
    public class LifeGaugeController : MonoBehaviour, ISingletonField
    {
        /*[SerializeField]
        private UISlider mainSlider;
        [SerializeField]
        private UISlider subSlider;
        [SerializeField]
        private UISprite mainGaugeSprite;
        [SerializeField]
        private UIPanel panel;
        [SerializeField]
        private UISprite skillNameBalloon;
        [SerializeField]
        private CustomUILabel skillNameLabel;
        private static Yggdrasil<LifeGaugeController> staticSingletonTree = (Yggdrasil<LifeGaugeController>) null;
        private static FMHLMNGHNJG staticBattleManager = (FMHLMNGHNJG) null;
        private bool isGaugeVisibleAlways;
        private static readonly string RED_SPRITE_NAME = "battle_gauge_bar_enemy";
        private const float RESET_TIME = 0.05f;
        private const float DURATION = 0.2f;
        private const float END_DURATION = 0.2f;
        private const float BALLOON_DURATION = 1f;
        private const int EDGE_SIZE = 38;
        private const int STRING_SIZE = 18;
        private static readonly Vector3 SKILL_NAME_BALLOON_SCALE = new Vector3(1.25f, 1.2f, 1f);
        private static readonly Vector3 SKILL_NAME_BALLOON_POSITION = new Vector3(0.0f, 105f);
        private static readonly Vector3 STATE_ICON_SCALE = new Vector3(0.85f, 0.85f, 1f);
        private static readonly Vector3 STATE_ICON_POSITION = new Vector3(0.0f, 180f);

        private FMHLMNGHNJG battleManager => LifeGaugeController.staticBattleManager;

        private UnitCtrl owner { get; set; }

        private float toValue { get; set; }

        private bool isMoving { get; set; }

        private bool skillBalloonVisible { get; set; }

        private float damagedTime { get; set; }

        private AbromalStateIconController abnormalIconGameObject { get; set; }

        private int iconCount { get; set; }

        private void OnDestroy()
        {
          this.mainSlider = (UISlider) null;
          this.subSlider = (UISlider) null;
          this.mainGaugeSprite = (UISprite) null;
          this.panel = (UIPanel) null;
          this.skillNameBalloon = (UISprite) null;
          this.skillNameLabel = (CustomUILabel) null;
          this.owner = (UnitCtrl) null;
          this.abnormalIconGameObject = (AbromalStateIconController) null;
        }

        public static void StaticRelease()
        {
          LifeGaugeController.staticSingletonTree = (Yggdrasil<LifeGaugeController>) null;
          LifeGaugeController.staticBattleManager = (FMHLMNGHNJG) null;
        }*/

        public void Initialize(
          bool _isOther,
          float _height,
          UnitCtrl _unit,
          bool _isGaugeVisibleAlways = false)
        {
            /*if (LifeGaugeController.staticSingletonTree == null)
            {
              LifeGaugeController.staticSingletonTree = this.CreateSingletonTree<LifeGaugeController>();
              LifeGaugeController.staticBattleManager = (FMHLMNGHNJG) LifeGaugeController.staticSingletonTree.Get<BattleManager>();
            }
            ResourceManager instance = ManagerSingleton<ResourceManager>.Instance;
            this.transform.parent = _unit.transform.TargetTransform;
            this.transform.ResetLocal();
            this.gameObject.SetActive(false);
            this.subSlider.gameObject.SetActive(false);
            this.mainSlider.gameObject.SetActive(false);
            this.skillNameBalloon.gameObject.SetActive(false);
            this.owner = _unit;
            this.isGaugeVisibleAlways = _isGaugeVisibleAlways;
            if (this.battleManager.IsDefenceReplayMode)
            {
              Vector3 localScale = this.transform.localScale;
              localScale.x = -localScale.x;
              this.transform.localScale = localScale;
            }
            if (_isOther == !this.battleManager.IsDefenceReplayMode)
              this.mainGaugeSprite.spriteName = LifeGaugeController.RED_SPRITE_NAME;
            this.skillNameBalloon.transform.localScale = LifeGaugeController.SKILL_NAME_BALLOON_SCALE;
            this.skillNameBalloon.transform.localPosition = LifeGaugeController.SKILL_NAME_BALLOON_POSITION;
            this.panel.sortingOrder = 11000;
            this.battleManager.StartCoroutine(this.followTarget(_unit));
            this.abnormalIconGameObject = instance.LoadImmediately(eResourceId.ABNORMAL_STATEICON).GetComponent<AbromalStateIconController>();
            this.abnormalIconGameObject.Initialize(3, true);
            this.abnormalIconGameObject.transform.parent = this.transform;
            this.abnormalIconGameObject.transform.localPosition = LifeGaugeController.STATE_ICON_POSITION;
            this.abnormalIconGameObject.transform.localScale = LifeGaugeController.STATE_ICON_SCALE;*/
        }

        /*private IEnumerator followTarget(UnitCtrl _unit)
        {
          LifeGaugeController lifeGaugeController = this;
          while (true)
          {
            yield return (object) null;
            if (!((Object) _unit == (Object) null))
              lifeGaugeController.transform.position = BattleUnitBaseSpineController.BoneWorldToGlobalPosConsiderRotate(_unit.StateBone, _unit.RotateCenter, _unit.BottomTransform.lossyScale);
            else
              break;
          }
        }*/

        public void SetSortOrderFront() { }// => this.panel.sortingOrder = 22500;

        public void SetSortOrderBack() { }// => this.panel.sortingOrder = 11000;

    public void SetNormalizedLifeAmount(float value, bool _isInitialize = false, bool _isTowerTimeUp = false)
    {
      /*this.damagedTime = Time.time;
      this.mainSlider.value = value;
      if (_isTowerTimeUp)
      {
        this.gameObject.SetActive(true);
        this.skillNameBalloon.SetActive(false);
        this.subSlider.gameObject.SetActive(true);
        this.mainSlider.gameObject.SetActive(true);
        this.StartCoroutine(this.updateLateSlider());
      }
      else
      {
        if (_isInitialize && !this.isGaugeVisibleAlways || this.isMoving)
          return;
        if (!this.gameObject.activeSelf)
          this.gameObject.SetActive(true);
        this.subSlider.gameObject.SetActive(true);
        this.mainSlider.gameObject.SetActive(true);
        this.isMoving = true;
        this.battleManager.AppendCoroutine(this.updateLateSlider(), ePauseType.VISUAL, this.owner);
      }*/
    }

    public void IndicateSkillName(string _skillName)
    {
     /* if (string.IsNullOrEmpty(_skillName) || this.owner.IsFront && this.battleManager.BlackOutUnitList.Count != 0)
        return;
      if (!this.gameObject.activeSelf)
        this.gameObject.SetActive(true);
      this.skillNameLabel.SetText(eTextId.NUM, (object) _skillName);
      this.skillBalloonVisible = true;
      this.skillNameBalloon.gameObject.SetActive(true);
      this.skillNameBalloon.width = _skillName.Length * 18 + 38;
      this.battleManager.AppendCoroutine(this.updateSkillBalloon(_skillName), ePauseType.IGNORE_BLACK_OUT, (UnitCtrl) null);*/
    }

    /*private IEnumerator updateSkillBalloon(string _skillName)
    {
      LifeGaugeController lifeGaugeController = this;
      float timer = 1f;
      while (!(_skillName != lifeGaugeController.skillNameLabel.text))
      {
        timer -= lifeGaugeController.battleManager.DeltaTime_60fps;
        if ((double) timer < 0.0 || lifeGaugeController.owner.IsFront && lifeGaugeController.battleManager.BlackOutUnitList.Count != 0)
        {
          lifeGaugeController.skillNameBalloon.gameObject.SetActive(false);
          if (lifeGaugeController.isMoving || lifeGaugeController.skillBalloonVisible || lifeGaugeController.iconCount != 0)
            break;
          lifeGaugeController.gameObject.SetActive(false);
          break;
        }
        yield return (object) null;
      }
    }*/

    /*private IEnumerator updateLateSlider()
    {
      LifeGaugeController lifeGaugeController = this;
      while ((double) Time.time <= (double) lifeGaugeController.damagedTime + 0.0500000007450581)
        yield return (object) null;
      float oldDamageTime = lifeGaugeController.damagedTime + 0.05f;
      lifeGaugeController.toValue = lifeGaugeController.mainSlider.value;
      while (((double) oldDamageTime + 0.200000002980232 - (double) Time.time) / (double) Time.deltaTime > 1.0)
      {
        lifeGaugeController.subSlider.value += Mathf.Max((float) (((double) lifeGaugeController.toValue - (double) lifeGaugeController.subSlider.value) / ((double) oldDamageTime + 0.200000002980232 - (double) Time.time)) * Time.deltaTime, lifeGaugeController.toValue - lifeGaugeController.subSlider.value);
        yield return (object) null;
      }
      lifeGaugeController.subSlider.value = lifeGaugeController.toValue;
      lifeGaugeController.isMoving = false;
      float time = 0.0f;
      if (!lifeGaugeController.isGaugeVisibleAlways)
      {
        while (true)
        {
          time += Time.deltaTime;
          if ((double) time <= 0.200000002980232)
            yield return (object) null;
          else
            break;
        }
        lifeGaugeController.mainSlider.gameObject.SetActive(false);
        lifeGaugeController.subSlider.gameObject.SetActive(false);
        if (!lifeGaugeController.isMoving && !lifeGaugeController.skillBalloonVisible && lifeGaugeController.iconCount == 0)
          lifeGaugeController.gameObject.SetActive(false);
      }
    }*/

    public void AddIcon(eStateIconType _state)
    {
      /*if (_state == eStateIconType.NONE)
        return;
      this.gameObject.SetActive(true);
      ++this.iconCount;
      this.abnormalIconGameObject.AddIcon(_state);*/
    }

    public void SetIconNum(eStateIconType _state, int _num)
    {
      /*if (_state == eStateIconType.NONE)
        return;
      this.abnormalIconGameObject.SetStateNum(_state, _num);*/
    }

    public void DeleteIcon(eStateIconType _state)
    {
      /*if (_state == eStateIconType.NONE)
        return;
      if (!this.isMoving && !this.skillBalloonVisible && this.iconCount == 0)
        this.gameObject.SetActive(false);
      --this.iconCount;
      this.abnormalIconGameObject.DeleteIcon(_state);*/
    }

    public void SetVisibleAlways()
    {
     /*this.isGaugeVisibleAlways = true;
      this.mainSlider.gameObject.SetActive(true);
      this.subSlider.gameObject.SetActive(true);*/
    }
  }
}
