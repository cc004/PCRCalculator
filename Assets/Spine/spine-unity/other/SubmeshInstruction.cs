using UnityEngine;

namespace Spine.Unity
{
	public struct SubmeshInstruction
	{
		public Skeleton skeleton;

		public int startSlot;

		public int endSlot;

		public Material material;

		public bool forceSeparate;

		public int preActiveClippingSlotSource;

		public int rawTriangleCount;

		public int rawVertexCount;

		public int rawFirstVertexIndex;

		public bool hasClipping;

		public int SlotCount => endSlot - startSlot;
	}
}
