using System;
using System.Collections.Generic;

namespace Spine
{
	public static class SkeletonExtensions
	{
		public static bool IsWeighted(this VertexAttachment va)
		{
			if (va.bones != null)
			{
				return va.bones.Length != 0;
			}
			return false;
		}

		public static bool InheritsRotation(this TransformMode mode)
		{
			return ((ulong)mode & 1uL) == 0;
		}

		public static bool InheritsScale(this TransformMode mode)
		{
			return ((ulong)mode & 2uL) == 0;
		}

		internal static void SetPropertyToSetupPose(this Skeleton skeleton, int propertyID)
		{
			int num = propertyID >> 24;
			TimelineType timelineType = (TimelineType)num;
			int num2 = propertyID - (num << 24);
			switch (timelineType)
			{
			case TimelineType.Rotate:
			{
				Bone obj9 = skeleton.bones.Items[num2];
				obj9.rotation = obj9.data.rotation;
				break;
			}
			case TimelineType.Translate:
			{
				Bone obj8 = skeleton.bones.Items[num2];
				obj8.x = obj8.data.x;
				obj8.y = obj8.data.y;
				break;
			}
			case TimelineType.Scale:
			{
				Bone obj7 = skeleton.bones.Items[num2];
				obj7.scaleX = obj7.data.scaleX;
				obj7.scaleY = obj7.data.scaleY;
				break;
			}
			case TimelineType.Shear:
			{
				Bone obj6 = skeleton.bones.Items[num2];
				obj6.shearX = obj6.data.shearX;
				obj6.shearY = obj6.data.shearY;
				break;
			}
			case TimelineType.Attachment:
				skeleton.SetSlotAttachmentToSetupPose(num2);
				break;
			case TimelineType.Color:
				skeleton.slots.Items[num2].SetColorToSetupPose();
				break;
			case TimelineType.TwoColor:
				skeleton.slots.Items[num2].SetColorToSetupPose();
				break;
			case TimelineType.Deform:
				skeleton.slots.Items[num2].attachmentVertices.Clear();
				break;
			case TimelineType.DrawOrder:
				skeleton.SetDrawOrderToSetupPose();
				break;
			case TimelineType.IkConstraint:
			{
				IkConstraint obj5 = skeleton.ikConstraints.Items[num2];
				obj5.mix = obj5.data.mix;
				obj5.bendDirection = obj5.data.bendDirection;
				break;
			}
			case TimelineType.TransformConstraint:
			{
				TransformConstraint obj4 = skeleton.transformConstraints.Items[num2];
				TransformConstraintData data = obj4.data;
				obj4.rotateMix = data.rotateMix;
				obj4.translateMix = data.translateMix;
				obj4.scaleMix = data.scaleMix;
				obj4.shearMix = data.shearMix;
				break;
			}
			case TimelineType.PathConstraintPosition:
			{
				PathConstraint obj3 = skeleton.pathConstraints.Items[num2];
				obj3.position = obj3.data.position;
				break;
			}
			case TimelineType.PathConstraintSpacing:
			{
				PathConstraint obj2 = skeleton.pathConstraints.Items[num2];
				obj2.spacing = obj2.data.spacing;
				break;
			}
			case TimelineType.PathConstraintMix:
			{
				PathConstraint obj = skeleton.pathConstraints.Items[num2];
				obj.rotateMix = obj.data.rotateMix;
				obj.translateMix = obj.data.translateMix;
				break;
			}
			case TimelineType.Event:
				break;
			}
		}

		public static void SetDrawOrderToSetupPose(this Skeleton skeleton)
		{
			Slot[] items = skeleton.slots.Items;
			int count = skeleton.slots.Count;
			ExposedList<Slot> drawOrder = skeleton.drawOrder;
			drawOrder.Clear(clearArray: false);
			drawOrder.GrowIfNeeded(count);
			Array.Copy(items, drawOrder.Items, count);
		}

		public static void SetColorToSetupPose(this Slot slot)
		{
			slot.r = slot.data.r;
			slot.g = slot.data.g;
			slot.b = slot.data.b;
			slot.a = slot.data.a;
			slot.r2 = slot.data.r2;
			slot.g2 = slot.data.g2;
			slot.b2 = slot.data.b2;
		}

		public static void SetAttachmentToSetupPose(this Slot slot)
		{
			SlotData data = slot.data;
			slot.Attachment = slot.bone.skeleton.GetAttachment(data.name, data.attachmentName);
		}

		public static void SetSlotAttachmentToSetupPose(this Skeleton skeleton, int slotIndex)
		{
			Slot slot = skeleton.slots.Items[slotIndex];
			string attachmentName = slot.data.attachmentName;
			if (string.IsNullOrEmpty(attachmentName))
			{
				slot.Attachment = null;
			}
			else
			{
				Attachment attachment2 = (slot.Attachment = skeleton.GetAttachment(slotIndex, attachmentName));
			}
		}

		public static void PoseWithAnimation(this Skeleton skeleton, string animationName, float time, bool loop = false)
		{
			skeleton.data.FindAnimation(animationName)?.Apply(skeleton, 0f, time, loop, null, 1f, MixPose.Setup, MixDirection.In);
		}

		public static void PoseSkeleton(this Animation animation, Skeleton skeleton, float time, bool loop = false)
		{
			animation.Apply(skeleton, 0f, time, loop, null, 1f, MixPose.Setup, MixDirection.In);
		}

		public static void SetKeyedItemsToSetupPose(this Animation animation, Skeleton skeleton)
		{
			animation.Apply(skeleton, 0f, 0f, loop: false, null, 0f, MixPose.Setup, MixDirection.Out);
		}

		public static void FindNamesForSlot(this Skin skin, string slotName, SkeletonData skeletonData, List<string> results)
		{
			int slotIndex = skeletonData.FindSlotIndex(slotName);
			skin.FindNamesForSlot(slotIndex, results);
		}

		public static void FindAttachmentsForSlot(this Skin skin, string slotName, SkeletonData skeletonData, List<Attachment> results)
		{
			int slotIndex = skeletonData.FindSlotIndex(slotName);
			skin.FindAttachmentsForSlot(slotIndex, results);
		}
	}
}
