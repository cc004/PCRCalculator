﻿// Decompiled with JetBrains decompiler
// Type: Elements.SkillEffectCtrl
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using Elements.Battle;
using Spine;
using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elements
{
    [ExecuteAlways]
    //[RequireComponent(typeof (CriAtomSource))]
    public class SkillEffectCtrl : MonoBehaviour, ISingletonField
    {
        /*public static readonly eSE[] BattleCommonSeArray = new eSE[2]
        {
          eSE.NONE,
          eSE.BTL_FIREARM_ARROW_1
        };*/
        [SerializeField]
        protected ParticleSystem particle;
        //[SerializeField]
        //public TweenPosition[] TweenerList = new TweenPosition[0];
        //[SerializeField]
        //public TweenRotation[] TweenRotList = new TweenRotation[0];
        [SerializeField]
        public Animator[] AnimatorList = new Animator[0];
        //[SerializeField]
        //protected CriAtomSource skillSeSource;
        [SerializeField]
        protected bool isRepeat;
        protected UnitCtrl source;
        [SerializeField]
        private bool isCommon;
        [SerializeField]
        private eBattleSkillSeType seType;
        [SerializeField]
        private List<SkillEffectCtrl.RarityUpTextureChangeDataSet> textureChangeParticles;
        [SerializeField]
        private List<SkillEffectCtrl.DefenceModeTextureChange> defenceModeTextureSetting;
        protected bool timeToDie;
        protected bool isPause;
        //private SoundManager soundManager = ManagerSingleton<SoundManager>.Instance;
        protected Dictionary<Renderer, int> particleRendererDictionary;
        private bool isFront;
        private static readonly string LAYER_NAME = "Default";
        private const int SORT_ORDER_BOUNDARY = 1000;
        //private static Yggdrasil<SkillEffectCtrl> staticSingletonTree = (Yggdrasil<SkillEffectCtrl>) null;
        private static BattleManager staticBattleManager;// = BattleManager.Instance;

        public bool IsAura { get; set; }

        public bool IsCommon
        {
            get => this.isCommon;
            set => this.isCommon = value;
        }

        public System.Action<SkillEffectCtrl> OnEffectEnd { set; get; }

        public UnitCtrl SortTarget { get; set; }

        public UnitCtrl Target { get; set; }

        public bool IsRepeat
        {
            get => this.isRepeat;
            set => this.isRepeat = value;
        }

        public bool IsPlaying { get; set; }

        protected ParticleSystem[] particles { get; set; }

        private Dictionary<ParticleSystem, float> particleStartDelayDictionary { get; set; }

        private float resumeTime { get; set; }

        //protected Yggdrasil<SkillEffectCtrl> singletonTree => SkillEffectCtrl.staticSingletonTree;

        protected BattleManager battleManager => SkillEffectCtrl.staticBattleManager;

        // public static void StaticInitSingletonTree() => SkillEffectCtrl.staticSingletonTree = (Yggdrasil<SkillEffectCtrl>) null;

        public static void StaticRelease()
        {
            //SkillEffectCtrl.staticSingletonTree = (Yggdrasil<SkillEffectCtrl>) null;
            SkillEffectCtrl.staticBattleManager = null;
        }

        private void Awake() => this.awakeImpl();

        protected void awakeImpl()
        {
            //if (SkillEffectCtrl.staticSingletonTree != null)
            //  return;
            //SkillEffectCtrl.staticSingletonTree = this.CreateSingletonTree<SkillEffectCtrl>();
            //SkillEffectCtrl.staticBattleManager = (HJEGJNLKOML) SkillEffectCtrl.staticSingletonTree.Get<BattleManager>();
            //if (staticBattleManager == null)
            //{
                staticBattleManager = BattleManager.Instance;
            //}
        }

        private void OnDestroy()
        {
            if (this.battleManager != null && this.battleManager.BOEDFIHEEOL)
                this.battleManager.RemoveEffectToUpdateList(this);
            this.particle = (ParticleSystem)null;
            //this.TweenerList = (TweenPosition[])null;
            //this.TweenRotList = (TweenRotation[])null;
            //this.skillSeSource = (CriAtomSource)null;
            this.source = (UnitCtrl)null;
            this.textureChangeParticles = (List<SkillEffectCtrl.RarityUpTextureChangeDataSet>)null;
            this.OnEffectEnd = (System.Action<SkillEffectCtrl>)null;
            this.SortTarget = (UnitCtrl)null;
            this.Target = (UnitCtrl)null;
            //this.soundManager = (SoundManager)null;
            this.particleRendererDictionary = (Dictionary<Renderer, int>)null;
            this.onDestroy();
        }

        protected virtual void onDestroy()
        {
        }

        private void setLayerName(string value)
        {
            if (this.particleRendererDictionary == null)
                return;
            foreach (KeyValuePair<Renderer, int> particleRenderer in this.particleRendererDictionary)
                particleRenderer.Key.sortingLayerName = value;
        }

        //public void SetLayerUI() => this.transform.ChangeChildObjectLayer(LayerDefine.LAYER_UI);

        public virtual void InitializeSort()
        {
            this.battleManager.AddEffectToUpdateList(this);
            this.setLayerName(SkillEffectCtrl.LAYER_NAME);
            this.particleRendererDictionaryInitialize();
        }

        public void InitializeSortForWithOutBattle()
        {
            this.setLayerName(SkillEffectCtrl.LAYER_NAME);
            this.particleRendererDictionaryInitialize();
        }

        public virtual void ExecAppendCoroutine(UnitCtrl _unit = null, bool _isAbnormal = false)
        {
            if (!this.isRepeat)
                this.AppendCoroutine(this.UpdateTimer(), ePauseType.VISUAL, _unit);
            else
                this.AppendCoroutine(this.UpdateTimerRepeat(), ePauseType.VISUAL);
            this.battleManager.AppendEffect(this, _unit, _isAbnormal);
        }

        private void particleRendererDictionaryInitialize()
        {
            if (this.particleRendererDictionary != null)
                return;
            this.particleRendererDictionary = new Dictionary<Renderer, int>();
            this.particles = this.gameObject.GetComponentsInChildren<ParticleSystem>(true);
            this.particleStartDelayDictionary = new Dictionary<ParticleSystem, float>();
            for (int index = 0; index < this.particles.Length; ++index)
            {
                Renderer component = this.particles[index].GetComponent<Renderer>();
                component.sortingOrder = Mathf.Min(5000, component.sortingOrder);
                this.particleRendererDictionary.Add(component, component.sortingOrder);
                this.particleStartDelayDictionary.Add(this.particles[index], this.particles[index].main.startDelayMultiplier);
            }
        }

        public virtual void SetPossitionAppearanceType(
          NormalSkillEffect skillEffect,
          BasePartsData target,
          UnitCtrl _owner,
          Skill skill)
        {
            this.source = _owner;
            Vector3 vector3 = new Vector3(0.0f, 9.259259f, 0.0f);
            Bone bone = (Bone)null;
            switch (skillEffect.EffectTarget)
            {
                case eEffectTarget.OWNER:
                case eEffectTarget.ALL_TARGET:
                case eEffectTarget.FORWARD_TARGET:
                case eEffectTarget.BACK_TARGET:
                case eEffectTarget.ALL_OTHER:
                case eEffectTarget.ALL_UNIT_EXCEPT_OWNER:
                    this.SortTarget = target.Owner;
                    switch (skillEffect.TargetBone)
                    {
                        case eTargetBone.BOTTOM:
                            vector3 = target.GetBottomTransformPosition();
                            break;
                        case eTargetBone.HEAD:
                            bone = target.GetStateBone();
                            vector3 = BattleUnitBaseSpineController.BoneWorldToGlobalPosConsiderRotate(target.GetStateBone(), target.Owner.RotateCenter, target.GetBottomLossyScale());
                            break;
                        case eTargetBone.CENTER:
                            bone = target.GetCenterBone();
                            vector3 = BattleUnitBaseSpineController.BoneWorldToGlobalPosConsiderRotate(target.GetCenterBone(), target.Owner.RotateCenter, target.GetBottomLossyScale());
                            break;
                        case eTargetBone.FIXED_CENETER:
                            vector3 = target.GetBottomTransformPosition() + target.GetFixedCenterPos();
                            break;
                        case eTargetBone.ANY_BONE:
                            bone = (this.SortTarget.MotionPrefix == -1 ? (SkeletonRenderer)this.SortTarget.UnitSpineCtrl : (SkeletonRenderer)this.SortTarget.UnitSpineCtrlModeChange).skeleton.FindBone(skillEffect.TargetBoneName);
                            skillEffect.TrackType = eTrackType.BONE;
                            skillEffect.TrackDimension = eTrackDimension.XY;
                            vector3 = BattleUnitBaseSpineController.BoneWorldToGlobalPosConsiderRotate(bone, this.SortTarget.RotateCenter, this.SortTarget.BottomTransform.lossyScale);
                            break;
                    }
                    break;
            }
            if (skillEffect.EffectBehavior == eEffectBehavior.SERIES_FIREARM || skillEffect.EffectBehavior == eEffectBehavior.FIREARM && skillEffect.TargetBone != eTargetBone.BOTTOM && (UnityEngine.Object)this.particle != (UnityEngine.Object)null)
            {
                this.particle.transform.position += vector3 - target.GetBottomTransformPosition() - target.GetFixedCenterPos();
                vector3 = target.GetBottomTransformPosition() + target.GetFixedCenterPos();
            }
            this.transform.position += vector3;
            switch (skillEffect.EffectBehavior)
            {
                case eEffectBehavior.FIREARM:
                case eEffectBehavior.SERIES_FIREARM:
                    skillEffect.TrackType = eTrackType.NONE;
                    break;
            }
            Vector3 absolutePosition = this.transform.position - target.GetBottomTransformPosition();
            switch (skillEffect.TrackType)
            {
                case eTrackType.BONE:
                    switch (skillEffect.TrackDimension)
                    {
                        case eTrackDimension.XY:
                            this.battleManager.StartCoroutine(this.TrackTarget(target, absolutePosition, bone: bone, _trackRotation: skillEffect.TrackRotation));
                            return;
                        case eTrackDimension.X:
                            this.battleManager.StartCoroutine(this.TrackTarget(target, absolutePosition, followY: false, bone: bone));
                            return;
                        case eTrackDimension.Y:
                            this.battleManager.StartCoroutine(this.TrackTarget(target, absolutePosition, false, bone: bone));
                            return;
                        default:
                            return;
                    }
                case eTrackType.BOTTOM:
                    switch (skillEffect.TrackDimension)
                    {
                        case eTrackDimension.XY:
                            this.battleManager.StartCoroutine(this.TrackTarget(target, absolutePosition));
                            return;
                        case eTrackDimension.X:
                            this.battleManager.StartCoroutine(this.TrackTarget(target, absolutePosition, followY: false));
                            return;
                        case eTrackDimension.Y:
                            this.battleManager.StartCoroutine(this.TrackTarget(target, absolutePosition, false));
                            return;
                        default:
                            return;
                    }
            }
        }

        public IEnumerator TrackTarget(
          BasePartsData trans,
          Vector3 absolutePosition,
          bool followX = true,
          bool followY = true,
          Bone bone = null,
          bool _trackRotation = false)
        {
            SkillEffectCtrl skillEffectCtrl = this;
            while (!((UnityEngine.Object)skillEffectCtrl == (UnityEngine.Object)null) && trans != null && (!skillEffectCtrl.timeToDie && !((UnityEngine.Object)trans.Owner == (UnityEngine.Object)null)))
            {
                if (followX & followY)
                    skillEffectCtrl.transform.position = bone == null ? trans.GetBottomTransformPosition() + absolutePosition : BattleUnitBaseSpineController.BoneWorldToGlobalPosConsiderRotate(bone, trans.Owner.RotateCenter, trans.Owner.BottomTransform.lossyScale);
                else if (!followX)
                {
                    Vector3 position = skillEffectCtrl.transform.position;
                    position.y = bone == null ? trans.GetBottomTransformPosition().y + absolutePosition.y : BattleUnitBaseSpineController.BoneWorldToGlobalPosConsiderRotate(bone, trans.Owner.RotateCenter, trans.Owner.BottomTransform.lossyScale).y;
                    skillEffectCtrl.transform.position = position;
                }
                else if (!followY)
                {
                    Vector3 position = skillEffectCtrl.transform.position;
                    position.x = bone == null ? trans.GetBottomTransformPosition().x + absolutePosition.x : BattleUnitBaseSpineController.BoneWorldToGlobalPosConsiderRotate(bone, trans.Owner.RotateCenter, trans.Owner.BottomTransform.lossyScale).x;
                    skillEffectCtrl.transform.position = position;
                }
                if (_trackRotation && bone != null)
                {
                    float num = bone.skeleton.flipX ^ bone.skeleton.flipY ^ (double)trans.Owner.BottomTransform.localScale.x < 0.0 ? -1f : 1f;
                    Vector3 eulerAngles = skillEffectCtrl.transform.localRotation.eulerAngles;
                    skillEffectCtrl.transform.localRotation = Quaternion.Euler(eulerAngles.x, eulerAngles.y, bone.WorldRotationX * num);
                }
                yield return (object)null;
            }
        }

        public IEnumerator TrackTarget(
          BattleUnitBaseSpineController _spine,
          Vector3 _absolutePosition,
          bool _followX = true,
          bool _followY = true,
          Bone _bone = null,
          bool _trackRotation = false,
          Transform _traskScale = null,
          float _coefficient = 1f,
          bool _callFromBossDialog = false)
        {
            SkillEffectCtrl skillEffectCtrl = this;
            Vector3 baseLocalScale = skillEffectCtrl.transform.localScale;
            while (!((UnityEngine.Object)skillEffectCtrl == (UnityEngine.Object)null) && !((UnityEngine.Object)_spine == (UnityEngine.Object)null) && !skillEffectCtrl.timeToDie)
            {
                if (_followX & _followY)
                    skillEffectCtrl.transform.position = _bone == null ? _spine.transform.position + _absolutePosition : BattleUnitBaseSpineController.BoneWorldToGlobalPosConsiderRotate(_bone, _spine.transform, _spine.transform.lossyScale, _callFromBossDialog);
                else if (!_followX)
                {
                    Vector3 position = skillEffectCtrl.transform.position;
                    position.y = _bone == null ? _spine.transform.position.y + _absolutePosition.y : BattleUnitBaseSpineController.BoneWorldToGlobalPosConsiderRotate(_bone, _spine.transform, _spine.transform.lossyScale, _callFromBossDialog).y;
                    skillEffectCtrl.transform.position = position;
                }
                else if (!_followY)
                {
                    Vector3 position = skillEffectCtrl.transform.position;
                    position.x = _bone == null ? _spine.transform.position.x + _absolutePosition.x : BattleUnitBaseSpineController.BoneWorldToGlobalPosConsiderRotate(_bone, _spine.transform, _spine.transform.lossyScale, _callFromBossDialog).x;
                    skillEffectCtrl.transform.position = position;
                }
                if (_trackRotation && _bone != null)
                {
                    float num = _bone.skeleton.flipX ^ _bone.skeleton.flipY ^ (double)_spine.transform.localScale.x < 0.0 ? -1f : 1f;
                    Vector3 eulerAngles = skillEffectCtrl.transform.localRotation.eulerAngles;
                    skillEffectCtrl.transform.localRotation = Quaternion.Euler(eulerAngles.x, eulerAngles.y, _bone.WorldRotationX * num);
                }
                if ((UnityEngine.Object)_traskScale != (UnityEngine.Object)null)
                    skillEffectCtrl.transform.localScale = Vector3.Scale(baseLocalScale * _coefficient, _traskScale.localScale);
                yield return (object)null;
            }
        }

        public IEnumerator TrackTargetSort(UnitCtrl unit)
        {
            SkillEffectCtrl skillEffectCtrl = this;
            bool startDepthBack = unit.IsDepthBack;
            while (!((UnityEngine.Object)skillEffectCtrl == (UnityEngine.Object)null) && !((UnityEngine.Object)unit == (UnityEngine.Object)null) && !skillEffectCtrl.timeToDie)
            {
                if (startDepthBack && !unit.IsDepthBack)
                {
                    skillEffectCtrl.SetSortOrderFront();
                    startDepthBack = false;
                }
                if (unit.IsFront && !skillEffectCtrl.isFront)
                    skillEffectCtrl.SetSortOrderFront();
                else if (!unit.IsFront && skillEffectCtrl.isFront)
                    skillEffectCtrl.SetSortOrderBack();
                yield return (object)null;
            }
        }

        private IEnumerator UpdateTimer()
        {
            SkillEffectCtrl skillEffectCtrl = this;
            float time = 0.0f;
            do
            {
                do
                {
                    do
                    {
                        yield return (object)null;
                        if ((UnityEngine.Object)skillEffectCtrl == (UnityEngine.Object)null)
                            yield break;
                    }
                    while (skillEffectCtrl.isPause);
                    if ((UnityEngine.Object)skillEffectCtrl.Target != (UnityEngine.Object)null && skillEffectCtrl.Target.IsDead)
                    {
                        skillEffectCtrl.OnEffectEnd?.Invoke(skillEffectCtrl);
                        if (!(skillEffectCtrl is FirearmCtrl))
                            skillEffectCtrl.timeToDie = true;
                    }
                    time += skillEffectCtrl.battleManager.DeltaTime_60fps;
                }
                while ((double)time < (double)skillEffectCtrl.particle.main.duration);
                skillEffectCtrl.OnEffectEnd?.Invoke(skillEffectCtrl);
            }
            while (skillEffectCtrl is FirearmCtrl);
            skillEffectCtrl.timeToDie = true;
        }

        private IEnumerator UpdateTimerRepeat()
        {
            SkillEffectCtrl skillEffectCtrl = this;
            while (true)
            {
                do
                {
                    do
                    {
                        yield return (object)null;
                    }
                    while (skillEffectCtrl.isPause || !((UnityEngine.Object)skillEffectCtrl.Target != (UnityEngine.Object)null) || !skillEffectCtrl.Target.IsDead);
                    skillEffectCtrl.OnEffectEnd?.Invoke(skillEffectCtrl);
                }
                while (skillEffectCtrl is FirearmCtrl);
                skillEffectCtrl.timeToDie = true;
            }
        }

        public virtual bool _Update()
        {
            if (this.timeToDie)
            {
                //this.skillSeSource.Stop();
                this.IsPlaying = false;
                this.gameObject.SetActive(false);
            }
            return !this.timeToDie;
        }

        public virtual void Pause()
        {
            if ((UnityEngine.Object)this.particle != (UnityEngine.Object)null)
            {
                this.particle.Pause(true);
                this.resumeTime = this.particle.time;
                //this.pauseSe(true);
            }
            //for (int index = 0; index < this.TweenerList.Length; ++index)
            //    this.TweenerList[index].enabled = false;
            //for (int index = 0; index < this.TweenRotList.Length; ++index)
            //    this.TweenRotList[index].enabled = false;
            //for (int index = 0; index < this.AnimatorList.Length; ++index)
            //    this.AnimatorList[index].enabled = false;
            this.isPause = true;
        }

        protected virtual void resetStartDelay()
        {
            for (int index = 0; index < this.particles.Length; ++index)
            {
                ParticleSystem.MainModule main = this.particles[index].main;
                float num = this.particleStartDelayDictionary != null ? this.particleStartDelayDictionary[this.particles[index]] : main.startDelayMultiplier;
                if ((double)num > (double)this.resumeTime)
                    main.startDelayMultiplier = num - this.resumeTime;
            }
        }

        public virtual void Resume()
        {
            if ((UnityEngine.Object)this.particle != (UnityEngine.Object)null && this.particle.isPaused && ((UnityEngine.Object)this.gameObject != (UnityEngine.Object)null && this.gameObject.activeSelf))
            {
                this.particle.Play();
                this.resetStartDelay();
                //this.pauseSe(false);
            }
            //for (int index = 0; index < this.TweenerList.Length; ++index)
            //    this.TweenerList[index].enabled = true;
            //for (int index = 0; index < this.TweenRotList.Length; ++index)
            //    this.TweenRotList[index].enabled = true;
            //for (int index = 0; index < this.AnimatorList.Length; ++index)
            //    this.AnimatorList[index].enabled = true;
            this.isPause = false;
        }

        /*protected virtual void initTweener(UITweener _tweener)
        {
            _tweener.ResetToBeginning();
            _tweener.PlayForward();
        }*/

        public virtual void SetSortOrderBack()
        {
            this.isFront = false;
            this.particleRendererDictionaryInitialize();
            foreach (KeyValuePair<Renderer, int> particleRenderer in this.particleRendererDictionary)
                particleRenderer.Key.sortingOrder = particleRenderer.Value >= -1000 ? (particleRenderer.Value > 0 ? (particleRenderer.Value >= 1000 ? 11000 : BattleDefine.GetUnitSortOrder(this.SortTarget) + 300) : BattleDefine.GetUnitSortOrder(this.SortTarget) - 300) : 690;
        }

        public virtual void SetSortOrderFront()
        {
            this.isFront = true;
            this.particleRendererDictionaryInitialize();
            foreach (KeyValuePair<Renderer, int> particleRenderer in this.particleRendererDictionary)
            {
                Renderer key = particleRenderer.Key;
                key.sortingOrder = particleRenderer.Value >= -1000 ? (particleRenderer.Value > 0 ? (particleRenderer.Value >= 1000 ? 11000 : BattleDefine.GetUnitSortOrder(this.SortTarget) + 300) : BattleDefine.GetUnitSortOrder(this.SortTarget) - 300) : 690;
                key.sortingOrder += 11500;
            }
        }

        public void SetSortOrderStatus(int offset)
        {
            this.particleRendererDictionaryInitialize();
            foreach (KeyValuePair<Renderer, int> particleRenderer in this.particleRendererDictionary)
                particleRenderer.Key.sortingOrder = particleRenderer.Value >= -1000 ? (particleRenderer.Value >= 0 ? (particleRenderer.Value >= 1000 ? 11000 : BattleDefine.GetUnitSortOrder(this.SortTarget) + 350 + offset) : BattleDefine.GetUnitSortOrder(this.SortTarget) - 300) : 690;
        }

        public void SetSortOrderHit(int _offset)
        {
            this.particleRendererDictionaryInitialize();
            foreach (KeyValuePair<Renderer, int> particleRenderer in this.particleRendererDictionary)
                particleRenderer.Key.sortingOrder = particleRenderer.Value >= -1000 ? (particleRenderer.Value >= 0 ? (particleRenderer.Value >= 1000 ? 11000 : 11000 + _offset) : BattleDefine.GetUnitSortOrder(this.SortTarget) - 300) : 690;
        }

        public void SetSortOrderForSummon(int _sortOrder)
        {
            this.particleRendererDictionaryInitialize();
            foreach (KeyValuePair<Renderer, int> particleRenderer in this.particleRendererDictionary)
                particleRenderer.Key.sortingOrder = particleRenderer.Value >= -1000 ? (particleRenderer.Value > 0 ? (particleRenderer.Value >= 1000 ? 11000 : _sortOrder + 300) : _sortOrder - 300) : 690;
        }

        protected void AppendCoroutine(IEnumerator cr, ePauseType pauseType, UnitCtrl unit = null) => this.battleManager.AppendCoroutine(cr, pauseType, unit);

        /*public void PlaySe(int soundUnitId, bool isEnemySide)
        {
            if (this.seType == eBattleSkillSeType.NAME)
            {
                string _cueSheetName = this.isCommon ? SoundManager.SE_BATTLE_CUESHEET : UnitCtrl.ConvertToSkillCueSheetName(soundUnitId);
                string _cueName = this.gameObject.name;
                if (_cueName.Contains("fxsk_"))
                    _cueName = _cueName.Substring("fxsk_0000_CUT_".Length);
                int length1 = _cueName.LastIndexOf("(");
                if (length1 >= 0)
                    _cueName = _cueName.Substring(0, length1);
                int length2 = _cueName.Length - 2;
                if (_cueName.LastIndexOf("_l") == length2 || _cueName.LastIndexOf("_r") == length2)
                    _cueName = _cueName.Substring(0, length2);
                UnitCtrl.EnchantSeDirectionToSeSource(this.skillSeSource, isEnemySide);
                this.soundManager.PlaySeByOuterSource(this.skillSeSource, _cueSheetName: _cueSheetName, _cueName: _cueName);
            }
            else
                this.PlaySe(SkillEffectCtrl.BattleCommonSeArray[(int)this.seType], isEnemySide);
        }*/

        /*public void PlaySe(eSE se, bool isEnemySide)
        {
            UnitCtrl.EnchantSeDirectionToSeSource(this.skillSeSource, isEnemySide);
            this.soundManager.PlaySeByOuterSource(this.skillSeSource, se);
        }*/

        /*private void pauseSe(bool isPause)
        {
            if ((UnityEngine.Object)this.skillSeSource == (UnityEngine.Object)null)
                return;
            this.skillSeSource.Pause(isPause);
        }*/

        /*public CriAtomSource SeSource
        {
            get => this.skillSeSource;
            set => this.skillSeSource = value;
        }*/

        public virtual void ResetParameter(GameObject _prefab, int _skinID = 0, bool _isShadow = false)
        {
            if (_skinID != 0)
            {
                for (int index = 0; index < this.textureChangeParticles.Count; ++index)
                    this.textureChangeParticles[index].ParticleSystemRenderer.material.mainTexture = this.textureChangeParticles[index].TextureAndSkinIdList.Find((Predicate<SkillEffectCtrl.RarityUpTextureChangeDataSet.TextureAndSkinId>)(e => e.SkinId == _skinID)).Texture;
            }
            /*if (this.battleManager.IsDefenceReplayMode)
            {
                for (int index = 0; index < this.defenceModeTextureSetting.Count; ++index)
                {
                    SkillEffectCtrl.DefenceModeTextureChange modeTextureChange = this.defenceModeTextureSetting[index];
                    modeTextureChange.Target.material.SetTexture(modeTextureChange.TargetParameterName, modeTextureChange.Texture);
                    if ((UnityEngine.Object)modeTextureChange.Target.trailMaterial != (UnityEngine.Object)null)
                        modeTextureChange.Target.trailMaterial.SetTexture(modeTextureChange.TargetParameterName, modeTextureChange.Texture);
                    if (modeTextureChange.UsePosition)
                        modeTextureChange.Target.transform.SetLocalPosX(modeTextureChange.PositionX);
                }
                DefenceModeEffectChange component = this.GetComponent<DefenceModeEffectChange>();
                if ((UnityEngine.Object)component != (UnityEngine.Object)null)
                    component.Apply();
            }*/
            this.timeToDie = false;
            this.IsPlaying = true;
            this.OnEffectEnd = (System.Action<SkillEffectCtrl>)null;
            //this.pauseSe(false);
            this.transform.localPosition = _prefab.transform.localPosition;
            if ((UnityEngine.Object)this.particle != (UnityEngine.Object)null)
            {
                this.particle.transform.localPosition = _prefab.transform.GetChild(0).localPosition;
                this.particle.transform.localEulerAngles = _prefab.transform.GetChild(0).localEulerAngles;
                this.particle.Simulate(0.0f, true, true, true);
                this.particle.Play(true);
            }
            if (this.particleStartDelayDictionary == null)
                return;
            //foreach (KeyValuePair<ParticleSystem, float> particleStartDelay in this.particleStartDelayDictionary)
                //particleStartDelay.Key.main.startDelayMultiplier = particleStartDelay.Value;
        }

        public void RestartTween()
        {
            /*int index1 = 0;
            for (int length = this.TweenerList.Length; index1 < length; ++index1)
                this.initTweener((UITweener)this.TweenerList[index1]);
            int index2 = 0;
            for (int length = this.TweenRotList.Length; index2 < length; ++index2)
            {
                this.TweenRotList[index2].ResetToBeginning();
                this.TweenRotList[index2].PlayForward();
            }*/
        }

        public void SetTimeToDie(bool value) => this.timeToDie = value;

        /*public IEnumerator TrackTargetSortForSummon(UnitCtrl _unitCtrl)
        {
            int currentDepth = 0;
            while (!((UnityEngine.Object)_unitCtrl == (UnityEngine.Object)null) && !((UnityEngine.Object)_unitCtrl.GetCurrentSpineCtrl() == (UnityEngine.Object)null))
            {
                if (currentDepth != _unitCtrl.GetCurrentSpineCtrl().Depth)
                {
                    currentDepth = _unitCtrl.GetCurrentSpineCtrl().Depth;
                    this.SetSortOrderForSummon(currentDepth);
                }
                yield return (object)null;
                if (!this.IsPlaying)
                    break;
            }
        }*/
        public virtual void SetStartTime(float _starttime)
        {
            for (int index = 0; index < this.AnimatorList.Length; ++index)
            {
                Animator animator = this.AnimatorList[index];
                AnimatorClipInfo[] animatorClipInfo = animator.GetCurrentAnimatorClipInfo(0);
                if (animatorClipInfo.Length != 0)
                {
                    AnimationClip clip = animatorClipInfo[0].clip;
                    AnimatorStateInfo animatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);
                    animator.Play(animatorStateInfo.fullPathHash, -1, Mathf.Min(1f, _starttime / clip.length));
                }
            }
            if (!((UnityEngine.Object)this.particle != (UnityEngine.Object)null))
                return;
            foreach (KeyValuePair<ParticleSystem, float> particleStartDelay in this.particleStartDelayDictionary)
            {
                ParticleSystem key = particleStartDelay.Key;
                ParticleSystem.MainModule main = key.main;
                if ((double)particleStartDelay.Value > (double)_starttime)
                {
                    main.startDelayMultiplier = particleStartDelay.Value - _starttime;
                    key.Simulate(0.0f, true, true, true);
                }
                else
                {
                    main.startDelayMultiplier = 0.0f;
                    key.Simulate(_starttime - particleStartDelay.Value, true, true, true);
                }
            }
            this.particle.Play(true);
        }

        public virtual void OnAwakeWhenSkipCutIn()
        {
        }

        public virtual void SetTimeToDieByStartHp(float _hpPercent)
        {
        }

        [Serializable]
        private class RarityUpTextureChangeDataSet
        {
            public ParticleSystemRenderer ParticleSystemRenderer;
            public List<SkillEffectCtrl.RarityUpTextureChangeDataSet.TextureAndSkinId> TextureAndSkinIdList;

            [Serializable]
            public class TextureAndSkinId
            {
                public Texture Texture;
                public int SkinId;
            }
        }

        [Serializable]
        private class DefenceModeTextureChange
        {
            public Texture Texture;
            public ParticleSystemRenderer Target;
            public string TargetParameterName = "";
            public bool UsePosition;
            public float PositionX;
        }
    }
}