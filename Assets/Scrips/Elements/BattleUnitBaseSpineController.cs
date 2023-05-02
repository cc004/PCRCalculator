// Decompiled with JetBrains decompiler
// Type: Elements.BattleUnitBaseSpineController
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using PCRCaculator;
using Spine;
using System.Collections.Generic;
using UnityEngine;

namespace Elements
{
    public class BattleUnitBaseSpineController : SpineController
    {
        protected int skinId;
        protected eSpineType spineType;

        public int UnitId { get; private set; }

        public int MotionType { get; private set; }

        protected bool isPause { get; set; }

        public bool IsPause => isPause;

        public override void Create(SpineResourceSet _resourceSpineSet)
        {
            UnitId = _resourceSpineSet.UnitId;
            skinId = _resourceSpineSet.SkinId;
            spineType = _resourceSpineSet.SpineResourceInfo.spineType;
            //this.MotionType = !ManagerSingleton<MasterDataManager>.Instance.masterUnitData.ContainsKey(_resourceSpineSet.UnitId) ? 0 : (int) ManagerSingleton<MasterDataManager>.Instance.masterUnitData[_resourceSpineSet.UnitId].MotionType;
            MotionType = MainManager.Instance.GetMotionType(UnitId, skinId);
            float animationScale = _resourceSpineSet.SpineResourceInfo.animationScale;
            //if ((Object)_resourceSpineSet.Animation != (Object)null)
            //_resourceSpineSet.Skelton.CreateSkeltonCysp(_resourceSpineSet.Animation, animationScale, false);
            //    Debug.LogError(UnitId + "的动画为空！");
            //else
            //_resourceSpineSet.Skelton.scale = animationScale;
            Create(_resourceSpineSet.Skelton);
        }

        public void Destroy() => Destroy(gameObject);

        private Dictionary<long, string> animeNameCache = new Dictionary<long, string>();
        public string ConvertAnimeIdToAnimeName(
          eSpineCharacterAnimeId _animeId,
          int _index1 = -1,
          int _index2 = -1,
          int _index3 = -1)
        {
            var key = ((long)(ushort)_animeId << 48) | ((long)(ushort)_index1 << 32) | ((long)(ushort)_index2 << 16) | ((ushort)_index3);
            if (animeNameCache.TryGetValue(key, out var val)) return val;
            var str = SpineDefine.GetAnimeName(_animeId, UnitId, MotionType, _index1, _index2, _index3);
            animeNameCache.Add(key, str);
            return str;
        }

        public bool IsAnimation(
          eSpineCharacterAnimeId _animeId,
          int _index1 = -1,
          int _index2 = -1,
          int _index3 = -1) => IsAnimation(ConvertAnimeIdToAnimeName(_animeId, _index1, _index2, _index3));

        public void Pause()
        {
            isPause = true;
            state.TimeScale = 0.0f;
        }

        public void Resume()
        {
            isPause = false;
            state.TimeScale = 1f;
        }

        public void SetTimeScale(float _timeScale) => state.TimeScale = _timeScale;

        /*public void LoadAnimationIDImmediately(eSpineBinaryAnimationId _binaryAnimationId)
        {
            int fromUnitIdWeaponId = this.getResourceIndexFromUnitIdWeaponId(_binaryAnimationId);
            this.spineManager.LoadAnimationImmediately(_binaryAnimationId, fromUnitIdWeaponId, this.skeletonDataAsset);
        }*/

        //public void LoadAnimationIDImmediately(eSpineBinaryAnimationId _binaryAnimationId, int _index) => this.spineManager.LoadAnimationImmediately(_binaryAnimationId, _index, this.skeletonDataAsset);

        /*public void RemoveAnimationIDImmediately(eSpineBinaryAnimationId _binaryAnimationId)
        {
            int fromUnitIdWeaponId = this.getResourceIndexFromUnitIdWeaponId(_binaryAnimationId);
            this.spineManager.RemoveAnimationImmediately(_binaryAnimationId, fromUnitIdWeaponId, this.skeletonDataAsset);
        }*/

        //public void RemoveAnimationIDImmediately(eSpineBinaryAnimationId _binaryAnimationId, int _index) => this.spineManager.RemoveAnimationImmediately(_binaryAnimationId, _index, this.skeletonDataAsset);

        /*public void UnloadAnimationResourceByIDImmediately(eSpineBinaryAnimationId _binaryAnimationId)
        {
            int fromUnitIdWeaponId = this.getResourceIndexFromUnitIdWeaponId(_binaryAnimationId);
            this.spineManager.UnloadAnimationResourceImmediately(_binaryAnimationId, (long)fromUnitIdWeaponId);
        }
        */
        private int getResourceIndexFromUnitIdWeaponId(eSpineBinaryAnimationId _binaryAnimationId) => 0;//SpineManager.SelectResourceIndexFromUnitIdWeaponId(_binaryAnimationId, this.UnitId, this.MotionType);

        //public void UnloadSkeletonResource(bool _isUnloadTypeNoneOnly = false) => SpineResourceLoader.UnloadResource(this.spineType, this.skinId, _isUnloadTypeNoneOnly);

        public void ChangeShader(eShader _shader) { }//this.ChangeShader(ShaderDefine.GetShader(_shader));

    //public void ChangeSkin(eSpineSkinId _spineSkinId) => this.ChangeSkin(SpineDefine.GetSkinName(_spineSkinId));

    public static Vector3 BoneWorldToGlobalPosConsiderRotate(
      Bone _bone,
      Transform _position,
      Vector3 _lossyScale,
      bool _callFromBossDialog = false) => _callFromBossDialog ? BoneWorldToGlobalPos(_bone, _position.position, _lossyScale) : _position.position + Quaternion.Euler(_position.eulerAngles) * (Vector3.Scale(new Vector3(_bone.worldX, _bone.worldY, 0.0f), _lossyScale) - Vector3.Scale(_position.localPosition, _position.lossyScale));

    public static Vector3 BoneWorldToGlobalPos(
      Bone _bone,
      Vector3 _position,
      Vector3 _lossyScale) => _position + Vector3.Scale(new Vector3(_bone.worldX, _bone.worldY, 0.0f), _lossyScale);

    public static Vector3 BoneWorldToLocalPos(Bone _bone, Transform _parent) => _parent.localPosition + Vector3.Scale(new Vector3(_bone.worldX, _bone.worldY, 0.0f), _parent.localScale);

    protected override void EntryCallBack()
    {
      startEndDelegate = _animationState => IsPlayAnime = false;
      state.Complete += startEndDelegate;
      startStartDelegate = _animationState =>
      {
          IsPlayAnime = true;
          AnimeName = AnimationName;
      };
      state.Start += startStartDelegate;
    }
  }
}
