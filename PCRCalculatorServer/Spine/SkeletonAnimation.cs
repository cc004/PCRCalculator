using UnityEngine;

namespace Spine.Unity
{
	[AddComponentMenu("Spine/SkeletonAnimation")]
	public class SkeletonAnimation : SkeletonRenderer, ISkeletonAnimation
	{
		public AnimationState state;

		[SerializeField]
		[SpineAnimation]
		private string _animationName;

		public bool loop;

		public float timeScale = 1f;

		public AnimationState AnimationState => state;

		public string AnimationName
		{
			get
			{
				if (!valid)
				{
					return null;
				}
				return state.GetCurrent(0)?.Animation.Name;
			}
			set
			{
				_animationName = value;
				if (valid)
				{
					if (string.IsNullOrEmpty(value))
					{
						state.ClearTrack(0);
						return;
					}
					state.SetAnimation(0, value, loop);
					Update();
				}
			}
		}

		protected event UpdateBonesDelegate _UpdateLocal;

		protected event UpdateBonesDelegate _UpdateWorld;

		protected event UpdateBonesDelegate _UpdateComplete;

		public event UpdateBonesDelegate UpdateLocal
		{
			add
			{
				_UpdateLocal += value;
			}
			remove
			{
				_UpdateLocal -= value;
			}
		}

		public event UpdateBonesDelegate UpdateWorld
		{
			add
			{
				_UpdateWorld += value;
			}
			remove
			{
				_UpdateWorld -= value;
			}
		}

		public event UpdateBonesDelegate UpdateComplete
		{
			add
			{
				_UpdateComplete += value;
			}
			remove
			{
				_UpdateComplete -= value;
			}
		}

		public static SkeletonAnimation AddToGameObject(GameObject gameObject, SkeletonDataAsset skeletonDataAsset)
		{
			return AddSpineComponent<SkeletonAnimation>(gameObject, skeletonDataAsset);
		}

		public static SkeletonAnimation NewSkeletonAnimationGameObject(SkeletonDataAsset skeletonDataAsset)
		{
			return NewSpineGameObject<SkeletonAnimation>(skeletonDataAsset);
		}

		public override void ClearState()
		{
			base.ClearState();
			if (state != null)
			{
				state.ClearTracks();
			}
		}

		public override void Initialize(bool overwrite)
		{
			if (valid && !overwrite)
			{
				return;
			}
			base.Initialize(overwrite);
			if (valid)
			{
				state = new AnimationState(skeletonDataAsset.GetAnimationStateData());
				if (!string.IsNullOrEmpty(_animationName))
				{
					state.SetAnimation(0, _animationName, loop);
					Update(0f);
				}
			}
		}

		public virtual void Update()
		{
			Update(Time.deltaTime);
		}

        private bool vistualUpdateQueued;
        private bool lateUpdateQueued;

		public void VisualUpdate(bool force_update = false, bool suppress_events = false, bool force_late_update = false)
        {
#if UNITY_5_3_OR_NEWER
			if (valid && (vistualUpdateQueued || force_update))
			{
				bool prevSuppressEvents = false;
				if (state != null)
				{
					prevSuppressEvents = state.SuppressEvents;
					state.SuppressEvents = suppress_events;
				}
				state.Apply(skeleton);
				if (state != null)
				{
					state.SuppressEvents = prevSuppressEvents;
				}
                if (_UpdateLocal != null)
                {
                    _UpdateLocal(this);
                }
                skeleton.UpdateWorldTransform();
                if (_UpdateWorld != null)
                {
                    _UpdateWorld(this);
                    skeleton.UpdateWorldTransform();
                }
                if (_UpdateComplete != null)
                {
                    _UpdateComplete(this);
                }

                vistualUpdateQueued = false;
            }

            if (valid && (lateUpdateQueued || force_late_update))
            {
                base.LateUpdate();
                lateUpdateQueued = false;
			}
#endif
        }

        public override void LateUpdate()
        {
            lateUpdateQueued = true;
        }

        public void Update(float deltaTime)
		{
			if (valid)
			{
				deltaTime *= timeScale;
				skeleton.Update(deltaTime);
				state.Update(deltaTime);
                vistualUpdateQueued = true;
            }
		}
	}
}
