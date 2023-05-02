// Decompiled with JetBrains decompiler
// Type: Elements.BattleSpineController
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Diagnostics;
using Cute;
using Elements.Battle;
using Spine;
using Spine.Unity;
using UnityEngine;
using Animation = Spine.Animation;
using Debug = UnityEngine.Debug;

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
        public bool quickNotActive = true;

        public UnitCtrl Owner { get; set; }

        public bool IsStopState { get; set; }

        public bool IsPlayAnimeBattle { get; set; }

        public float StopStateTime { get; set; }

        private Action stopStateEvent { get; set; }

        public float DropTreasureBoxTime { get; set; }

        public bool IsConstantVelocity { get; set; }

        private BattleManager battleManager => staticBattleManager;

        public bool IsIndependentBattleSync
        {
            get => isIndependentBattleSync;
            set => isIndependentBattleSync = value;
        }

        public bool IsColorStone
        {
            get => isStone;
            set
            {
                isStone = value;
                if (isStone)
                    ChangeShader(eShader.VERTEX_COLOR_SEPARETE_GRAY_SCALE);
                //this.ChangeDefaultShader();
            }
        }

        public bool IsColorPauseAction
        {
            get => isPauseAction;
            set
            {
                isPauseAction = value;
                if (isPauseAction)
                    ChangeShader(eShader.VERTEX_COLOR_SEPARETE_GRAY_SCALE);
                //this.ChangeDefaultShader();
            }
        }

        public bool HasSpecialSleepAnimatilon(int _motionPrefix) => IsAnimation(eSpineCharacterAnimeId.SLEEP, _motionPrefix, 0);

        public bool CheckPlaySpecialSleepAnimeExceptRelease(int _motionPrefix) => HasSpecialSleepAnimatilon(_motionPrefix) && (AnimationName == ConvertAnimeIdToAnimeName(eSpineCharacterAnimeId.SLEEP, _motionPrefix, 0) || AnimationName == ConvertAnimeIdToAnimeName(eSpineCharacterAnimeId.SLEEP, _motionPrefix, 1));

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
            Debug.LogError("不要使用这个函数加载骨骼！");
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
            return NewSpineGameObject<BattleSpineController>(skeletonDataAsset);
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
          battleManager.AddUnitSpineControllerList(this);
          IsStopState = false;
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
            if (!IsIndependentBattleSync)
                return;
            RealUpdate();
        }

        public void LateUpdateIndependentBattleSync()
        {
            if (!IsIndependentBattleSync)
                return;
            RealLateUpdate();
        }

        public void RealUpdate()
        {
            if (!isActiveAndEnabled)
                return;
            if (battleManager != null)
                Update(IsConstantVelocity ? battleManager.DeltaTime_60fps / Time.timeScale : battleManager.DeltaTime_60fps);
            else 
             base.Update();
        }

        public override void LateUpdate()
        {
        }

        public void RealLateUpdate()
        {
            if (!isActiveAndEnabled)
                return;
            base.LateUpdate();
        }

        public void PlayAnime(eSpineCharacterAnimeId _animeId, bool _playLoop = true) => PlayAnime(ConvertAnimeIdToAnimeName(_animeId), _playLoop);

        public void PlayAnime(eSpineCharacterAnimeId _animeId, int _index1, bool _playLoop = true) => PlayAnime(ConvertAnimeIdToAnimeName(_animeId, _index1), _playLoop);

        public void PlayAnime(
          string _playAnimeName,
          bool _playLoop,
          float _startTime = 0.0f,
          bool _ignoreBlackout = false)
        {
            base.PlayAnime(_playAnimeName, _playLoop);
            IsPlayAnimeBattle = true;
            float duration = state.Data.skeletonData.FindAnimation(_playAnimeName).Duration;
            if (_playLoop || !(Owner == null) && Owner.PlayingNoCutinMotion)
                return;
            setUpdateAnime(_startTime, duration, _ignoreBlackout);
        }

        public void RestartPlayAnimeCoroutine(
          float _startTime,
          eSpineCharacterAnimeId _animeId,
          int _index,
          int _prefix)
        {
            float duration = state.Data.skeletonData.FindAnimation(ConvertAnimeIdToAnimeName(_animeId, _index, _index3: _prefix)).Duration;
            IsPlayAnimeBattle = true;
            setUpdateAnime(_startTime, duration);
        }

        public override void PlayAnime(string _playAnimeName, bool _playLoop = true) => PlayAnime(_playAnimeName, _playLoop);

        public void PlayAnimeNoOverlap(eSpineCharacterAnimeId _animeId, int _index1, bool _playLoop = true) => PlayAnimeNoOverlap(ConvertAnimeIdToAnimeName(_animeId, _index1), _playLoop);

        public void PlayAnimeNoOverlap(
          eSpineCharacterAnimeId _animeId,
          int _index1,
          int _ndex2,
          int _index3,
          bool _playLoop = true) => PlayAnimeNoOverlap(ConvertAnimeIdToAnimeName(_animeId, _index1, _ndex2, _index3), _playLoop);

        public void SetAnimeEventDelegateForBattle(Action _callBack, int _motionPrefix = -1)
        {
            Timeline timeline = state.Data.skeletonData.FindAnimation(ConvertAnimeIdToAnimeName(eSpineCharacterAnimeId.DAMEGE, _motionPrefix)).timelines.Find(e => e is EventTimeline);
            stopStateEvent = _callBack;
            if (timeline == null)
                return;
            StopStateTime = (timeline as EventTimeline).frames[0];
        }

        public void SetDropTreasureBoxTime()
        {
            Animation animation = state.Data.skeletonData.FindAnimation(ConvertAnimeIdToAnimeName(eSpineCharacterAnimeId.DIE));
            if (animation == null)
            {
                DropTreasureBoxTime = 0.2f;
            }
            else
            {
                Timeline timeline = animation.timelines.Find(e => e is EventTimeline);
                if (timeline != null)
                    DropTreasureBoxTime = (timeline as EventTimeline).frames[0];
                else
                    DropTreasureBoxTime = 0.2f;
            }
        }

        public void PlayAnime(
          eSpineCharacterAnimeId _animeId,
          int _index1,
          int _index2,
          int _index3,
          bool _playLoop = true,
          float _startTime = 0.0f,
          bool _ignoreBlackout = false) => PlayAnime(ConvertAnimeIdToAnimeName(_animeId, _index1, _index2, _index3), _playLoop, _startTime, _ignoreBlackout);

        private void setUpdateAnime(float _startTime, float _endTime, bool _ignoreBlackout = false)
        {
            if (_ignoreBlackout)
                battleManager.AppendCoroutine(updateAnime(_startTime, _endTime, ++startedCoroutineId), ePauseType.IGNORE_BLACK_OUT);
            else
                battleManager.AppendCoroutine(updateAnime(_startTime, _endTime, ++startedCoroutineId), ePauseType.SYSTEM, Owner);
        }

        private IEnumerator updateAnime(float _startTime, float _endTime, int _thisId)
        {
            BattleSpineController battleSpineController = this;
            float deltaTime = battleSpineController.battleManager != null ? battleSpineController.battleManager.DeltaTime_60fps : 0.03333334f;
            float time = _startTime;
            bool callDamageEvent = battleSpineController.Owner != null && battleSpineController.AnimationName == battleSpineController.ConvertAnimeIdToAnimeName(eSpineCharacterAnimeId.DAMEGE, battleSpineController.Owner.MotionPrefix);
            while (battleSpineController.startedCoroutineId == _thisId)
            {
                time += !battleSpineController.isPause ? deltaTime : 0.0f;
                if (callDamageEvent && time > (double)battleSpineController.StopStateTime)
                {
                    battleSpineController.stopStateEvent.Call();
                    callDamageEvent = false;
                }
                if (time > (double)_endTime || BattleUtil.Approximately(time, _endTime))
                {
                    battleSpineController.IsPlayAnimeBattle = false;
                    battleSpineController.IsStopState = false;
                    break;
                }
                yield return null;
            }
        }
    }
}
