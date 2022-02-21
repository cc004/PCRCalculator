using System;
using UnityEngine;

namespace Spine.Unity
{
	[AddComponentMenu("Spine/UI/BoneFollowerGraphic")]
	[ExecuteInEditMode]
	[DisallowMultipleComponent]
	public class BoneFollowerGraphic : MonoBehaviour
	{
		public SkeletonGraphic skeletonGraphic;

		public bool initializeOnAwake = true;

		[SerializeField]
		[SpineBone("", "skeletonGraphic")]
		public string boneName;

		public bool followBoneRotation = true;

		[Tooltip("Follows the skeleton's flip state by controlling this Transform's local scale.")]
		public bool followSkeletonFlip = true;

		[Tooltip("Follows the target bone's local scale. BoneFollower cannot inherit world/skewed scale because of UnityEngine.Transform property limitations.")]
		public bool followLocalScale;

		public bool followZPosition = true;

		[NonSerialized]
		public Bone bone;

		private Transform skeletonTransform;

		private bool skeletonTransformIsParent;

		[NonSerialized]
		public bool valid;

		public SkeletonGraphic SkeletonGraphic
		{
			get
			{
				return skeletonGraphic;
			}
			set
			{
				skeletonGraphic = value;
				Initialize();
			}
		}

		public bool SetBone(string name)
		{
			bone = skeletonGraphic.Skeleton.FindBone(name);
			if (bone == null)
			{
				return false;
			}
			boneName = name;
			return true;
		}

		public void Awake()
		{
			if (initializeOnAwake)
			{
				Initialize();
			}
		}

		public void Initialize()
		{
			bone = null;
			valid = skeletonGraphic != null && skeletonGraphic.IsValid;
			if (valid)
			{
				skeletonTransform = skeletonGraphic.transform;
				skeletonTransformIsParent = (object)skeletonTransform == transform.parent;
				if (!string.IsNullOrEmpty(boneName))
				{
					bone = skeletonGraphic.Skeleton.FindBone(boneName);
				}
			}
		}

		public void LateUpdate()
		{
			if (!valid)
			{
				Initialize();
				return;
			}
			if (bone == null)
			{
				if (string.IsNullOrEmpty(boneName))
				{
					return;
				}
				bone = skeletonGraphic.Skeleton.FindBone(boneName);
				if (!SetBone(boneName))
				{
					return;
				}
			}
			RectTransform rectTransform = transform as RectTransform;
			if (rectTransform == null)
			{
				return;
			}
			float referencePixelsPerUnit = skeletonGraphic.canvas.referencePixelsPerUnit;
			if (skeletonTransformIsParent)
			{
				rectTransform.localPosition = new Vector3(bone.worldX * referencePixelsPerUnit, bone.worldY * referencePixelsPerUnit, followZPosition ? 0f : rectTransform.localPosition.z);
				if (followBoneRotation)
				{
					rectTransform.localRotation = bone.GetQuaternion();
				}
			}
			else
			{
				Vector3 position = skeletonTransform.TransformPoint(new Vector3(bone.worldX * referencePixelsPerUnit, bone.worldY * referencePixelsPerUnit, 0f));
				if (!followZPosition)
				{
					position.z = rectTransform.position.z;
				}
				float num = bone.WorldRotationX;
				Transform parent = rectTransform.parent;
				if (parent != null)
				{
					Matrix4x4 localToWorldMatrix = parent.localToWorldMatrix;
					if (localToWorldMatrix.m00 * localToWorldMatrix.m11 - localToWorldMatrix.m01 * localToWorldMatrix.m10 < 0f)
					{
						num = 0f - num;
					}
				}
				if (followBoneRotation)
				{
					Vector3 eulerAngles = skeletonTransform.rotation.eulerAngles;
					rectTransform.SetPositionAndRotation(position, Quaternion.Euler(eulerAngles.x, eulerAngles.y, skeletonTransform.rotation.eulerAngles.z + num));
				}
				else
				{
					rectTransform.position = position;
				}
			}
			Vector3 localScale = (followLocalScale ? new Vector3(bone.scaleX, bone.scaleY, 1f) : new Vector3(1f, 1f, 1f));
			if (followSkeletonFlip)
			{
				localScale.y *= ((bone.skeleton.flipX ^ bone.skeleton.flipY) ? (-1f) : 1f);
			}
			rectTransform.localScale = localScale;
		}
	}
}
