using System;

namespace Spine.Unity
{
	public class SpineAttachment : SpineAttributeBase
	{
		public struct Hierarchy
		{
			public string skin;

			public string slot;

			public string name;

			public Hierarchy(string fullPath)
			{
				string[] array = fullPath.Split(new char[1]
				{
					'/'
				}, StringSplitOptions.RemoveEmptyEntries);
				if (array.Length == 0)
				{
					skin = "";
					slot = "";
					name = "";
					return;
				}
				if (array.Length < 2)
				{
					throw new Exception("Cannot generate Attachment Hierarchy from string! Not enough components! [" + fullPath + "]");
				}
				skin = array[0];
				slot = array[1];
				name = "";
				for (int i = 2; i < array.Length; i++)
				{
					name += array[i];
				}
			}
		}

		public bool returnAttachmentPath;

		public bool currentSkinOnly;

		public bool placeholdersOnly;

		public string skinField = "";

		public string slotField = "";

		public SpineAttachment(bool currentSkinOnly = true, bool returnAttachmentPath = false, bool placeholdersOnly = false, string slotField = "", string dataField = "", string skinField = "", bool includeNone = true)
		{
			this.currentSkinOnly = currentSkinOnly;
			this.returnAttachmentPath = returnAttachmentPath;
			this.placeholdersOnly = placeholdersOnly;
			this.slotField = slotField;
			this.dataField = dataField;
			this.skinField = skinField;
			this.includeNone = includeNone;
		}

		public static Hierarchy GetHierarchy(string fullPath)
		{
			return new Hierarchy(fullPath);
		}

		public static Attachment GetAttachment(string attachmentPath, SkeletonData skeletonData)
		{
			Hierarchy hierarchy = GetHierarchy(attachmentPath);
			if (!string.IsNullOrEmpty(hierarchy.name))
			{
				return skeletonData.FindSkin(hierarchy.skin).GetAttachment(skeletonData.FindSlotIndex(hierarchy.slot), hierarchy.name);
			}
			return null;
		}

		public static Attachment GetAttachment(string attachmentPath, SkeletonDataAsset skeletonDataAsset)
		{
			return GetAttachment(attachmentPath, skeletonDataAsset.GetSkeletonData(quiet: true));
		}
	}
}
