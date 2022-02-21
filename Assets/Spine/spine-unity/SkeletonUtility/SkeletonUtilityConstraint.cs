using UnityEngine;

namespace Spine.Unity
{
	[ExecuteInEditMode]
	[RequireComponent(typeof(SkeletonUtilityBone))]
	public abstract class SkeletonUtilityConstraint : MonoBehaviour
	{
		protected SkeletonUtilityBone utilBone;

		protected SkeletonUtility skeletonUtility;

		protected virtual void OnEnable()
		{
			utilBone = GetComponent<SkeletonUtilityBone>();
			skeletonUtility = transform.GetComponentInParent<SkeletonUtility>();
			skeletonUtility.RegisterConstraint(this);
		}

		protected virtual void OnDisable()
		{
			skeletonUtility.UnregisterConstraint(this);
		}

		public abstract void DoUpdate();
	}
}
