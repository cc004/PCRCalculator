namespace Spine.Unity
{
	public class SkeletonRendererInstruction
	{
		public bool immutableTriangles;

		public readonly ExposedList<SubmeshInstruction> submeshInstructions = new ExposedList<SubmeshInstruction>();

		public bool hasActiveClipping;

		public int rawVertexCount = -1;

		public readonly ExposedList<Attachment> attachments = new ExposedList<Attachment>();

		public void Clear()
		{
			attachments.Clear(clearArray: false);
			rawVertexCount = -1;
			hasActiveClipping = false;
			submeshInstructions.Clear(clearArray: false);
		}

		public void SetWithSubset(ExposedList<SubmeshInstruction> instructions, int startSubmesh, int endSubmesh)
		{
			int num = 0;
			ExposedList<SubmeshInstruction> exposedList = submeshInstructions;
			exposedList.Clear(clearArray: false);
			int num2 = endSubmesh - startSubmesh;
			exposedList.Resize(num2);
			SubmeshInstruction[] items = exposedList.Items;
			SubmeshInstruction[] items2 = instructions.Items;
			for (int i = 0; i < num2; i++)
			{
				SubmeshInstruction submeshInstruction = (items[i] = items2[startSubmesh + i]);
				hasActiveClipping = submeshInstruction.hasClipping;
				items[i].rawFirstVertexIndex = num;
				num += submeshInstruction.rawVertexCount;
			}
			rawVertexCount = num;
			int startSlot = items2[startSubmesh].startSlot;
			int endSlot = items2[endSubmesh - 1].endSlot;
			attachments.Clear(clearArray: false);
			int num3 = endSlot - startSlot;
			attachments.Resize(num3);
			Attachment[] items3 = attachments.Items;
			Slot[] items4 = items2[0].skeleton.drawOrder.Items;
			for (int j = 0; j < num3; j++)
			{
				items3[j] = items4[startSlot + j].attachment;
			}
		}

		public void Set(SkeletonRendererInstruction other)
		{
			immutableTriangles = other.immutableTriangles;
			hasActiveClipping = other.hasActiveClipping;
			rawVertexCount = other.rawVertexCount;
			attachments.Clear(clearArray: false);
			attachments.GrowIfNeeded(other.attachments.Capacity);
			attachments.Count = other.attachments.Count;
			other.attachments.CopyTo(attachments.Items);
			submeshInstructions.Clear(clearArray: false);
			submeshInstructions.GrowIfNeeded(other.submeshInstructions.Capacity);
			submeshInstructions.Count = other.submeshInstructions.Count;
			other.submeshInstructions.CopyTo(submeshInstructions.Items);
		}

		public static bool GeometryNotEqual(SkeletonRendererInstruction a, SkeletonRendererInstruction b)
		{
			if (a.hasActiveClipping || b.hasActiveClipping)
			{
				return true;
			}
			if (a.rawVertexCount != b.rawVertexCount)
			{
				return true;
			}
			if (a.immutableTriangles != b.immutableTriangles)
			{
				return true;
			}
			int count = b.attachments.Count;
			if (a.attachments.Count != count)
			{
				return true;
			}
			int count2 = a.submeshInstructions.Count;
			int count3 = b.submeshInstructions.Count;
			if (count2 != count3)
			{
				return true;
			}
			SubmeshInstruction[] items = a.submeshInstructions.Items;
			SubmeshInstruction[] items2 = b.submeshInstructions.Items;
			Attachment[] items3 = a.attachments.Items;
			Attachment[] items4 = b.attachments.Items;
			for (int i = 0; i < count; i++)
			{
				if (items3[i] != items4[i])
				{
					return true;
				}
			}
			for (int j = 0; j < count3; j++)
			{
				SubmeshInstruction submeshInstruction = items[j];
				SubmeshInstruction submeshInstruction2 = items2[j];
				if (submeshInstruction.rawVertexCount != submeshInstruction2.rawVertexCount || submeshInstruction.startSlot != submeshInstruction2.startSlot || submeshInstruction.endSlot != submeshInstruction2.endSlot || submeshInstruction.rawTriangleCount != submeshInstruction2.rawTriangleCount || submeshInstruction.rawFirstVertexIndex != submeshInstruction2.rawFirstVertexIndex)
				{
					return true;
				}
			}
			return false;
		}
	}
}
