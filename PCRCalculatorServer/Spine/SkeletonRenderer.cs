using System;
using System.Collections.Generic;
using UnityEngine;

namespace Spine.Unity
{
	public class SkeletonRenderer : MonoBehaviour
	{
		public delegate void SkeletonRendererDelegate(SkeletonRenderer skeletonRenderer);

		public SkeletonDataAsset skeletonDataAsset;

		public string initialSkinName;

		public bool initialFlipX;

		public bool initialFlipY;

		public string[] separatorSlotNames = new string[0];

		[NonSerialized]
		public readonly List<Slot> separatorSlots = new List<Slot>();

		public float zSpacing;

		public bool useClipping = true;

		public bool immutableTriangles;

		public bool pmaVertexColors = true;

		public bool clearStateOnDisable;

		public bool tintBlack;

		public bool singleSubmesh;

		public bool calculateTangents;

		public bool logErrors;

		public bool disableRenderingOnOverride = true;

		[NonSerialized]
		private readonly Dictionary<Material, Material> customMaterialOverride = new Dictionary<Material, Material>();

		[NonSerialized]
		private readonly Dictionary<Slot, Material> customSlotMaterials = new Dictionary<Slot, Material>();

		private Material[] copyMaterials;

		protected Shader curShader;

		[NonSerialized]
		public bool valid;

		[NonSerialized]
		public Skeleton skeleton;

		public SkeletonDataAsset SkeletonDataAsset => skeletonDataAsset;

		public Dictionary<Material, Material> CustomMaterialOverride => customMaterialOverride;

		public Dictionary<Slot, Material> CustomSlotMaterials => customSlotMaterials;

		public Skeleton Skeleton
		{
			get
			{
				Initialize(overwrite: false);
				return skeleton;
			}
		}

		public event SkeletonRendererDelegate OnRebuild;
		public static T NewSpineGameObject<T>(SkeletonDataAsset skeletonDataAsset) where T : SkeletonRenderer
		{
			return AddSpineComponent<T>(new GameObject("New Spine GameObject"), skeletonDataAsset);
		}

		public static T AddSpineComponent<T>(GameObject gameObject, SkeletonDataAsset skeletonDataAsset) where T : SkeletonRenderer
		{
			T val = gameObject.AddComponent<T>();
			if (skeletonDataAsset != null)
			{
				val.skeletonDataAsset = skeletonDataAsset;
				val.Initialize(overwrite: false);
			}
			return val;
		}

		public virtual void Awake()
		{
			Initialize(overwrite: false);
		}

		private void OnDisable()
		{
			if (clearStateOnDisable && valid)
			{
				ClearState();
			}
		}

		public virtual void OnDestroy()
		{
			valid = false;
		}

		public virtual void ClearState()
		{
			if (skeleton != null)
			{
				skeleton.SetToSetupPose();
			}
		}

		public virtual void Initialize(bool overwrite)
		{
			if (valid && !overwrite)
			{
				return;
			}
			skeleton = null;
			valid = false;
			if (!skeletonDataAsset)
			{
				_ = logErrors;
				return;
			}
			SkeletonData skeletonData = skeletonDataAsset.GetSkeletonData(quiet: false);
			if (skeletonData != null)
			{
				valid = true;
				skeleton = new Skeleton(skeletonData)
				{
					flipX = initialFlipX,
					flipY = initialFlipY
				};
				if (!string.IsNullOrEmpty(initialSkinName) && !string.Equals(initialSkinName, "default", StringComparison.Ordinal))
				{
					skeleton.SetSkin(initialSkinName);
				}
				separatorSlots.Clear();
				for (int i = 0; i < separatorSlotNames.Length; i++)
				{
					separatorSlots.Add(skeleton.FindSlot(separatorSlotNames[i]));
				}
				LateUpdate();
				if (OnRebuild != null)
				{
					OnRebuild(this);
				}
			}
		}
		public virtual void LateUpdate()
		{
		}
	}
}
