namespace Spine.Unity
{
	public class SpineBone : SpineAttributeBase
	{
		public SpineBone(string startsWith = "", string dataField = "", bool includeNone = true)
		{
			base.startsWith = startsWith;
			base.dataField = dataField;
			base.includeNone = includeNone;
		}

		public static Bone GetBone(string boneName, SkeletonRenderer renderer)
		{
			if (renderer.skeleton != null)
			{
				return renderer.skeleton.FindBone(boneName);
			}
			return null;
		}

		public static BoneData GetBoneData(string boneName, SkeletonDataAsset skeletonDataAsset)
		{
			return skeletonDataAsset.GetSkeletonData(quiet: true).FindBone(boneName);
		}
	}
}
