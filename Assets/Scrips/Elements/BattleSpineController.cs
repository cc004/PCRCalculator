// Decompiled with JetBrains decompiler
// Type: Elements.BattleSpineController
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using Cute;
using Elements.Battle;
using Spine;
using Spine.Unity;
using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

namespace Elements
{
    public class BattleSpineController : BattleUnitBaseSpineController, ISingletonField
    {
        private int startedCoroutineId;
        private bool isStone;
        private bool isPauseAction;
        private const eShader stoneShader = eShader.VERTEX_COLOR_SEPARETE_GRAY_SCALE;
        private const eShader pauseActionShader = eShader.VERTEX_COLOR_SEPARETE_GRAY_SCALE;
        private bool isIndependentBattleSync;
        //private static Yggdrasil<BattleSpineController> staticSingletonTree;
        private static BattleManager staticBattleManager;

        public UnitCtrl Owner { get; set; }

        public bool IsStopState { get; set; }

        public bool IsPlayAnimeBattle { get; set; }

        public float StopStateTime { get; set; }

        private Action stopStateEvent { get; set; }

        public float DropTreasureBoxTime { get; set; }

        public bool IsConstantVelocity { get; set; }

        private BattleManager battleManager => BattleSpineController.staticBattleManager;

        public bool IsIndependentBattleSync
        {
            get => this.isIndependentBattleSync;
            set => this.isIndependentBattleSync = value;
        }

        public bool IsColorStone
        {
            get => this.isStone;
            set
            {
                this.isStone = value;
                if (this.isStone)
                    this.ChangeShader(eShader.VERTEX_COLOR_SEPARETE_GRAY_SCALE);
                else { }
                //this.ChangeDefaultShader();
            }
        }

        public bool IsColorPauseAction
        {
            get => this.isPauseAction;
            set
            {
                this.isPauseAction = value;
                if (this.isPauseAction)
                    this.ChangeShader(eShader.VERTEX_COLOR_SEPARETE_GRAY_SCALE);
                else { }
                //this.ChangeDefaultShader();
            }
        }

        public bool HasSpecialSleepAnimatilon(int _motionPrefix) => this.IsAnimation(eSpineCharacterAnimeId.SLEEP, _motionPrefix, 0);

        public bool CheckPlaySpecialSleepAnimeExceptRelease(int _motionPrefix) => this.HasSpecialSleepAnimatilon(_motionPrefix) && (this.AnimationName == this.ConvertAnimeIdToAnimeName(eSpineCharacterAnimeId.SLEEP, _motionPrefix, 0) || this.AnimationName == this.ConvertAnimeIdToAnimeName(eSpineCharacterAnimeId.SLEEP, _motionPrefix, 1));

        public static void StaticRelease()
        {
            //BattleSpineController.staticSingletonTree = (Yggdrasil<BattleSpineController>) null;
            //BattleSpineController.staticBattleManager = (FMOLPKDBOPO) null;
        }

        public static void LoadCreate(
          eSpineType _spineType,
          int _unitId,
          int _rarity,
          Transform _transform,
          Action<BattleSpineController> _callback = null,
          int _enemyColor = 0)
        {
            UnityEngine.Debug.LogError("不要使用这个函数加载骨骼！");
            /*int skinId = UnitUtility.GetSkinId(_spineType, _unitId, _rarity);
            SpineResourceLoader.Load(_spineType, skinId, skinId, _enemyColor, _transform, (Action<SpineResourceSet>) (_resourceSpineSet =>
            {
              BattleSpineController component = _resourceSpineSet.Controller.GetComponent<BattleSpineController>();
              component.Create(_resourceSpineSet);
              _callback.Call<BattleSpineController>(component);
            }));*/
        }
        public static BattleSpineController LoadNewSkeletonAnimationGameObject(SkeletonDataAsset skeletonDataAsset)
        {
            return SkeletonAnimation.NewSpineGameObject<BattleSpineController>(skeletonDataAsset);
        }
        public static BattleSpineController LoadAddedSkeletonAnimation(SkeletonDataAsset skeletonDataAsset,GameObject obj)
        {
            return AddSpineComponent<BattleSpineController>(obj, skeletonDataAsset);
        }

        /*public static BattleSpineController LoadCreateImmidiateBySkinId(
          eSpineType _spineType,
          int _skinId,
          int _trgIdx0,
          int _trgIdx1,
          Transform _transform,
          Action<BattleSpineController> _callback = null)
        {
          SpineResourceSet _resourceSpineSet = SpineResourceLoader.LoadImmediately(_spineType, _skinId, _trgIdx0, _trgIdx1, _transform);
          BattleSpineController component = _resourceSpineSet.Controller.GetComponent<BattleSpineController>();
          component.Create(_resourceSpineSet);
          _callback.Call<BattleSpineController>(component);
          return component;
        }*/

        /*public static BattleSpineController LoadCreateImmidiate(
          eSpineType _spineType,
          int _unitId,
          int _rarity,
          Transform _transform,
          Action<BattleSpineController> _callback = null)
        {
          int skinId = UnitUtility.GetSkinId(_spineType, _unitId, _rarity);
          return BattleSpineController.LoadCreateImmidiateBySkinId(_spineType, skinId, skinId, 0, _transform, (Action<BattleSpineController>) (_loadedObject => _callback.Call<BattleSpineController>(_loadedObject)));
        }*/

        [Conditional("UNITY_EDITOR")]
        private static void checkSpineType(eSpineType _spineType)
        {
        }

        public override void Create(SpineResourceSet _resourceSpineSet)
        {
          base.Create(_resourceSpineSet);
            /*if (BattleSpineController.staticSingletonTree == null)
            {
              BattleSpineController.staticSingletonTree = this.CreateSingletonTree<BattleSpineController>();
              BattleSpineController.staticBattleManager = (FMOLPKDBOPO) BattleSpineController.staticSingletonTree.Get<BattleManager>();
            }*/
            staticBattleManager = BattleManager.Instance;
          this.battleManager.AddUnitSpineControllerList(this);
          this.IsStopState = false;
        }

        /*public override void OnDestroy()
        {
          if (this.battleManager != null)
            this.battleManager.RemoveUnitSpineControllerList(this);
          base.OnDestroy();
        }*/

         public override void Update()
         {
         }

        public void UpdateIndependentBattleSync()
        {
            if (!this.IsIndependentBattleSync)
                return;
            this.RealUpdate();
        }

        public void LateUpdateIndependentBattleSync()
        {
            if (!this.IsIndependentBattleSync)
                return;
            this.RealLateUpdate();
        }

        public void RealUpdate()
        {
            if (!this.isActiveAndEnabled)
                return;
            if (this.battleManager != null)
                this.Update(this.IsConstantVelocity ? this.battleManager.DeltaTime_60fps / Time.timeScale : this.battleManager.DeltaTime_60fps);
            else 
             base.Update();
        }

        public override void LateUpdate()
        {
        }

        public void RealLateUpdate()
        {
            if (!this.isActiveAndEnabled)
                return;
            base.LateUpdate();
        }

        public void PlayAnime(eSpineCharacterAnimeId _animeId, bool _playLoop = true) => this.PlayAnime(this.ConvertAnimeIdToAnimeName(_animeId), _playLoop, 0.0f, false);

        public void PlayAnime(eSpineCharacterAnimeId _animeId, int _index1, bool _playLoop = true) => this.PlayAnime(this.ConvertAnimeIdToAnimeName(_animeId, _index1), _playLoop, 0.0f, false);

        public void PlayAnime(
          string _playAnimeName,
          bool _playLoop,
          float _startTime = 0.0f,
          bool _ignoreBlackout = false)
        {
            base.PlayAnime(_playAnimeName, _playLoop);
            this.IsPlayAnimeBattle = true;
            float duration = this.state.Data.skeletonData.FindAnimation(_playAnimeName).Duration;
            if (_playLoop || !((UnityEngine.Object)this.Owner == (UnityEngine.Object)null) && this.Owner.PlayingNoCutinMotion)
                return;
            this.setUpdateAnime(_startTime, duration, _ignoreBlackout);
        }

        public void RestartPlayAnimeCoroutine(
          float _startTime,
          eSpineCharacterAnimeId _animeId,
          int _index,
          int _prefix)
        {
            float duration = this.state.Data.skeletonData.FindAnimation(this.ConvertAnimeIdToAnimeName(_animeId, _index, _index3: _prefix)).Duration;
            this.IsPlayAnimeBattle = true;
            this.setUpdateAnime(_startTime, duration);
        }

        public override void PlayAnime(string _playAnimeName, bool _playLoop = true) => this.PlayAnime(_playAnimeName, _playLoop, 0.0f, false);

        public void PlayAnimeNoOverlap(eSpineCharacterAnimeId _animeId, int _index1, bool _playLoop = true) => this.PlayAnimeNoOverlap(this.ConvertAnimeIdToAnimeName(_animeId, _index1), _playLoop);

        public void PlayAnimeNoOverlap(
          eSpineCharacterAnimeId _animeId,
          int _index1,
          int _ndex2,
          int _index3,
          bool _playLoop = true) => this.PlayAnimeNoOverlap(this.ConvertAnimeIdToAnimeName(_animeId, _index1, _ndex2, _index3), _playLoop);

        public void SetAnimeEventDelegateForBattle(Action _callBack, int _motionPrefix = -1)
        {
            Timeline timeline = this.state.Data.skeletonData.FindAnimation(this.ConvertAnimeIdToAnimeName(eSpineCharacterAnimeId.DAMEGE, _motionPrefix)).timelines.Find((Predicate<Timeline>)(e => e is EventTimeline));
            this.stopStateEvent = _callBack;
            if (timeline == null)
                return;
            this.StopStateTime = (timeline as EventTimeline).frames[0];
        }

        public void SetDropTreasureBoxTime()
        {
            Spine.Animation animation = this.state.Data.skeletonData.FindAnimation(this.ConvertAnimeIdToAnimeName(eSpineCharacterAnimeId.DIE));
            if (animation == null)
            {
                this.DropTreasureBoxTime = 0.2f;
            }
            else
            {
                Timeline timeline = animation.timelines.Find((Predicate<Timeline>)(e => e is EventTimeline));
                if (timeline != null)
                    this.DropTreasureBoxTime = (timeline as EventTimeline).frames[0];
                else
                    this.DropTreasureBoxTime = 0.2f;
            }
        }

        public void PlayAnime(
          eSpineCharacterAnimeId _animeId,
          int _index1,
          int _index2,
          int _index3,
          bool _playLoop = true,
          float _startTime = 0.0f,
          bool _ignoreBlackout = false) => this.PlayAnime(this.ConvertAnimeIdToAnimeName(_animeId, _index1, _index2, _index3), _playLoop, _startTime, _ignoreBlackout);

        private void setUpdateAnime(float _startTime, float _endTime, bool _ignoreBlackout = false)
        {
            if (_ignoreBlackout)
                this.battleManager.AppendCoroutine(this.updateAnime(_startTime, _endTime, ++this.startedCoroutineId), ePauseType.IGNORE_BLACK_OUT, (UnitCtrl)null);
            else
                this.battleManager.AppendCoroutine(this.updateAnime(_startTime, _endTime, ++this.startedCoroutineId), ePauseType.SYSTEM, this.Owner);
        }

        private IEnumerator updateAnime(float _startTime, float _endTime, int _thisId)
        {
            BattleSpineController battleSpineController = this;
            float deltaTime = battleSpineController.battleManager != null ? battleSpineController.battleManager.DeltaTime_60fps : 0.03333334f;
            float time = _startTime;
            bool callDamageEvent = (UnityEngine.Object)battleSpineController.Owner != (UnityEngine.Object)null && battleSpineController.AnimationName == battleSpineController.ConvertAnimeIdToAnimeName(eSpineCharacterAnimeId.DAMEGE, battleSpineController.Owner.MotionPrefix);
            while (battleSpineController.startedCoroutineId == _thisId)
            {
                time += !battleSpineController.isPause ? deltaTime : 0.0f;
                if (callDamageEvent && (double)time > (double)battleSpineController.StopStateTime)
                {
                    battleSpineController.stopStateEvent.Call();
                    callDamageEvent = false;
                }
                if ((double)time > (double)_endTime || BattleUtil.Approximately(time, _endTime))
                {
                    battleSpineController.IsPlayAnimeBattle = false;
                    battleSpineController.IsStopState = false;
                    break;
                }
                yield return (object)null;
            }
        }
    }
}
