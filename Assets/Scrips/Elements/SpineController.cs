// Decompiled with JetBrains decompiler
// Type: Elements.SpineController
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using Cute;
using Spine;
using Spine.Unity;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Elements
{
    public class SpineController : SkeletonAnimation
    {
        private Skin DefaultSkin;
        private Dictionary<Skin.AttachmentKeyTuple, Attachment> defaultAttachmentDic = new Dictionary<Skin.AttachmentKeyTuple, Attachment>();
        //protected SpineManager spineManager;
        protected Spine.AnimationState.TrackEntryDelegate startEndDelegate;
        protected Spine.AnimationState.TrackEntryDelegate startStartDelegate;
        protected Spine.AnimationState.TrackEntryEventDelegate eventDelegate;
        //private ColorShader colorShader = new ColorShader();
        private Shader defaultShader;
        private Material[] materialArray;
        protected bool isSyncUpdate;

        public string DefaultSkinName => this.DefaultSkin == null ? "" : this.DefaultSkin.Name;

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
          if (this.materialArray == null)
            return;
          for (int index = 0; index < this.materialArray.Length; ++index)
            UnityEngine.Object.Destroy((UnityEngine.Object) this.materialArray[index]);
          this.materialArray = (Material[]) null;
        }

         public override void Update()
         {
           if (this.isSyncUpdate)
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

        public virtual void SyncUpdate(float _time) => this.Update(_time);

        public void SetUseSyncUpdate(bool _isSyncUpdate) => this.isSyncUpdate = _isSyncUpdate;

        public virtual void Create(SkeletonDataAsset _createSkeletonDataAsset)
        {
            //this.spineManager = ManagerSingleton<SpineManager>.Instance;
            this.IsPlayAnime = false;
            this.AnimeName = "";
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
            this.Create(_resourceSpineSet.Skelton);
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

        public bool FindSkinData(string _skinName) => this.skeleton.Data.FindSkin(_skinName) != null;

        public Skin GetCurrentSkin() => this.skeleton.Skin;

        public virtual void PlayAnime(string _playAnimeName, bool _playLoop = true)
        {
            this.loop = _playLoop;
            this.AnimationName = _playAnimeName;
        }

        public virtual void PlayAnime(int _trackIndex, string _playAnimeName, bool _playLoop = true) => this.state.SetAnimation(_trackIndex, _playAnimeName, _playLoop);

        public void PlayAnimeNoOverlap(string _playAnimeName, bool _playLoop = true)
        {
            if (this.AnimeName == _playAnimeName)
                return;
            this.PlayAnime(_playAnimeName, _playLoop);
        }

        public void EntryAnime(string _animeName, bool _loop, float _delay = 0.0f) => this.state.AddAnimation(0, _animeName, true, _delay);

        public void StopAnime(int _trackIndex)
        {
            this.IsPlayAnime = false;
            this.state.ClearTrack(_trackIndex);
        }

        public void StopAnimeAll()
        {
            this.IsPlayAnime = false;
            this.state.ClearTracks();
        }

        public bool IsAnimation(string _animeName) => this.skeleton.data.IsAnimation(_animeName);

        private void SetAnimeEventDelegate(Spine.AnimationState.TrackEntryEventDelegate _eDelegate)
        {
            if (this.eventDelegate != null)
                this.state.Event -= this.eventDelegate;
            this.eventDelegate = _eDelegate;
            this.state.Event += this.eventDelegate;
        }

        public void SetAnimeEventDelegate(Action<Spine.Event> _callBack) => this.SetAnimeEventDelegate((Spine.AnimationState.TrackEntryEventDelegate)((_state, _event) => _callBack(_event)));

        protected virtual void EntryCallBack()
        {
            this.startEndDelegate = (Spine.AnimationState.TrackEntryDelegate)(_animationState =>
           {
               if (!(_animationState.Animation.Name == this.AnimationName))
                   return;
               this.IsPlayAnime = false;
           });
            this.state.Complete += this.startEndDelegate;
            this.startStartDelegate = (Spine.AnimationState.TrackEntryDelegate)(_animationState =>
           {
               this.IsPlayAnime = true;
               this.AnimeName = this.AnimationName;
           });
            this.state.Start += this.startStartDelegate;
        }

        private void DestroyCallBack()
        {
            if (this.startEndDelegate != null)
            {
                this.state.Complete -= this.startEndDelegate;
                this.startEndDelegate = (Spine.AnimationState.TrackEntryDelegate)null;
            }
            if (this.startStartDelegate != null)
            {
                this.state.Start -= this.startStartDelegate;
                this.startStartDelegate = (Spine.AnimationState.TrackEntryDelegate)null;
            }
            if (this.eventDelegate == null)
                return;
            this.state.Event -= this.eventDelegate;
            this.eventDelegate = (Spine.AnimationState.TrackEntryEventDelegate)null;
        }

        //public void FadeIn(float _timeMax, Action _onFinish = null) => this.colorShader.FadeIn((MonoBehaviour) this, _timeMax, _onFinish);

        //public void FadeOut(float _timeMax, Action _onFinish = null) => this.colorShader.FadeOut((MonoBehaviour) this, _timeMax, _onFinish);

        public void AddMixAnimation(string _fromAnime, string _toAnime, float _duration) => this.skeletonDataAsset.GetAnimationStateData().SetMix(_fromAnime, _toAnime, _duration);
    }
}
