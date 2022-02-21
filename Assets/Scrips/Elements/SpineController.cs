// Decompiled with JetBrains decompiler
// Type: Elements.SpineController
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using Spine;
using Spine.Unity;
using UnityEngine;
using AnimationState = Spine.AnimationState;
using Event = Spine.Event;

namespace Elements
{
    public class SpineController : SkeletonAnimation
    {
        private Skin DefaultSkin;
        private Dictionary<Skin.AttachmentKeyTuple, Attachment> defaultAttachmentDic = new Dictionary<Skin.AttachmentKeyTuple, Attachment>();
        //protected SpineManager spineManager;
        protected AnimationState.TrackEntryDelegate startEndDelegate;
        protected AnimationState.TrackEntryDelegate startStartDelegate;
        protected AnimationState.TrackEntryEventDelegate eventDelegate;
        //private ColorShader colorShader = new ColorShader();
        private Shader defaultShader;
        private Material[] materialArray;
        protected bool isSyncUpdate;

        public string DefaultSkinName => DefaultSkin == null ? "" : DefaultSkin.Name;

        public bool IsPlayAnime { get; protected set; }

        public string AnimeName { get; protected set; }

        /*public int Depth
        {
          get => this.colorShader.Depth;
          set => this.colorShader.Depth = value;
        }*/

        public Color CurColor;
        /*{
          get => this.colorShader.CurColor;
          set => this.colorShader.CurColor = value;
        }*/

        /*public Color CurColorOffset
        {
          get => this.colorShader.CurColorOffset;
          set => this.colorShader.CurColorOffset = value;
        }*/

        //public bool IsFadeProccess => this.colorShader.IsFadeProccess;

        public override void OnDestroy()
        {
          //this.colorShader.Destroy();
          base.OnDestroy();
          if (materialArray == null)
            return;
          for (int index = 0; index < materialArray.Length; ++index)
            Destroy(materialArray[index]);
          materialArray = null;
        }

         public override void Update()
         {
           if (isSyncUpdate)
             return;
           base.Update();
         }

        /*public static SpineController LoadCreateImmidiate(
          eSpineType _spineType,
          int _trgIdx0,
          int _trgIdx1,
          Transform _transform,
          Action<SpineController> _callback = null)
        {
          SpineResourceSet _resourceSpineSet = SpineResourceLoader.LoadImmediately(_spineType, 0, _trgIdx0, _trgIdx1, _transform);
          SpineController component = _resourceSpineSet.Controller.GetComponent<SpineController>();
          component.Create(_resourceSpineSet);
          _callback.Call<SpineController>(component);
          return component;
        }*/

        public virtual void SyncUpdate(float _time) => Update(_time);

        public void SetUseSyncUpdate(bool _isSyncUpdate) => isSyncUpdate = _isSyncUpdate;

        public virtual void Create(SkeletonDataAsset _createSkeletonDataAsset)
        {
            //this.spineManager = ManagerSingleton<SpineManager>.Instance;
            IsPlayAnime = false;
            AnimeName = "";
            //this.ChangeSkeletonDataAsset(_createSkeletonDataAsset);
            //this.colorShader.Init(this.GetComponent<Renderer>());
            //this.defaultShader = _createSkeletonDataAsset.atlasAssets[0].materials[0].shader;
            //this.Depth = 0;
        }

        public virtual void Create(SpineResourceSet _resourceSpineSet)
        {
            float animationScale = _resourceSpineSet.SpineResourceInfo.animationScale;
            /*if ((UnityEngine.Object)_resourceSpineSet.Animation != (UnityEngine.Object)null)
            {
                _resourceSpineSet.Skelton.CreateSkeltonCysp(_resourceSpineSet.Animation, animationScale, false);
                //Debug.LogError("动画为空！");
            }
            else
                _resourceSpineSet.Skelton.scale = animationScale;*/
            Create(_resourceSpineSet.Skelton);
        }

        //public Dictionary<string, Spine.Animation> GetAnimations() => this.skeleton.data.animations;

        /*private void ChangeSkeletonDataAsset(SkeletonDataAsset _createSkeletonDataAsset)
        {
          this.DestroyCallBack();
          this.skeletonDataAsset = _createSkeletonDataAsset;
          this.gameObject.SetActive(true);
          this.EntryCallBack();
          this.DefaultSkin = this.SkeletonDataAsset.GetSkeletonData(true).DefaultSkin;
          if (this.DefaultSkin == null)
            return;
          this.defaultAttachmentDic = this.DefaultSkin.CopyAttachment();
        }*/

        //public void SetColor(string _propertyName, Color _color) => this.colorShader.SetColor(_propertyName, _color);

        //public void ChangeDefaultShader() => this.ChangeShader(this.defaultShader);

        //public void ChangeShader(Shader _shader) => this.curShader = _shader;

        //public void ChangeSkin(string _skinName) => this.skeleton.SetSkin(_skinName, false);

        /*public bool ChangeSkinWithCheck(eSpineSkinId _skinId)
        {
          string skinName = SpineDefine.GetSkinName(_skinId);
          Skin currentSkin = this.GetCurrentSkin();
          if (currentSkin != null && !(currentSkin.Name != skinName))
            return false;
          this.ChangeSkin(skinName);
          return true;
        }*/

        /*public void ChangeDefaultAttachment()
        {
          this.DefaultSkin.SetAttachmentAll(this.defaultAttachmentDic);
          this.Skeleton.SetSkin(this.DefaultSkin);
        }*/

        public bool FindSkinData(string _skinName) => skeleton.Data.FindSkin(_skinName) != null;

        public Skin GetCurrentSkin() => skeleton.Skin;

        public virtual void PlayAnime(string _playAnimeName, bool _playLoop = true)
        {
            loop = _playLoop;
            AnimationName = _playAnimeName;
        }

        public virtual void PlayAnime(int _trackIndex, string _playAnimeName, bool _playLoop = true) => state.SetAnimation(_trackIndex, _playAnimeName, _playLoop);

        public void PlayAnimeNoOverlap(string _playAnimeName, bool _playLoop = true)
        {
            if (AnimeName == _playAnimeName)
                return;
            PlayAnime(_playAnimeName, _playLoop);
        }

        public void EntryAnime(string _animeName, bool _loop, float _delay = 0.0f) => state.AddAnimation(0, _animeName, true, _delay);

        public void StopAnime(int _trackIndex)
        {
            IsPlayAnime = false;
            state.ClearTrack(_trackIndex);
        }

        public void StopAnimeAll()
        {
            IsPlayAnime = false;
            state.ClearTracks();
        }

        public bool IsAnimation(string _animeName) => skeleton.data.IsAnimation(_animeName);

        private void SetAnimeEventDelegate(AnimationState.TrackEntryEventDelegate _eDelegate)
        {
            if (eventDelegate != null)
                state.Event -= eventDelegate;
            eventDelegate = _eDelegate;
            state.Event += eventDelegate;
        }

        public void SetAnimeEventDelegate(Action<Event> _callBack) => SetAnimeEventDelegate((_state, _event) => _callBack(_event));

        protected virtual void EntryCallBack()
        {
            startEndDelegate = _animationState =>
            {
                if (!(_animationState.Animation.Name == AnimationName))
                    return;
                IsPlayAnime = false;
            };
            state.Complete += startEndDelegate;
            startStartDelegate = _animationState =>
            {
                IsPlayAnime = true;
                AnimeName = AnimationName;
            };
            state.Start += startStartDelegate;
        }

        private void DestroyCallBack()
        {
            if (startEndDelegate != null)
            {
                state.Complete -= startEndDelegate;
                startEndDelegate = null;
            }
            if (startStartDelegate != null)
            {
                state.Start -= startStartDelegate;
                startStartDelegate = null;
            }
            if (eventDelegate == null)
                return;
            state.Event -= eventDelegate;
            eventDelegate = null;
        }

        //public void FadeIn(float _timeMax, Action _onFinish = null) => this.colorShader.FadeIn((MonoBehaviour) this, _timeMax, _onFinish);

        //public void FadeOut(float _timeMax, Action _onFinish = null) => this.colorShader.FadeOut((MonoBehaviour) this, _timeMax, _onFinish);

        public void AddMixAnimation(string _fromAnime, string _toAnime, float _duration) => skeletonDataAsset.GetAnimationStateData().SetMix(_fromAnime, _toAnime, _duration);
    }
}
