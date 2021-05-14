using System;

namespace Spine
{
	public class DeformTimeline : CurveTimeline
	{
		private static float[] zeros = new float[64];

		private int slotIndex;

		internal float[] frames;

		internal float[][] frameVertices;

		private VertexAttachment attachment;

		private int propertyId;

		public int SlotIndex
		{
			get
			{
				return slotIndex;
			}
			set
			{
				slotIndex = value;
			}
		}

		public float[] Frames
		{
			get
			{
				return frames;
			}
			set
			{
				frames = value;
			}
		}

		public float[][] Vertices
		{
			get
			{
				return frameVertices;
			}
			set
			{
				frameVertices = value;
			}
		}

		public VertexAttachment Attachment
		{
			get
			{
				return attachment;
			}
			set
			{
				attachment = value;
			}
		}

		public override int PropertyId => propertyId;

		public DeformTimeline(int frameCount, int _slotIndex, VertexAttachment _attachment)
			: base(frameCount)
		{
			frames = new float[frameCount];
			frameVertices = new float[frameCount][];
			slotIndex = _slotIndex;
			attachment = _attachment;
			propertyId = 100663296 + attachment.id + slotIndex;
		}

		public void SetFrame(int frameIndex, float time, float[] vertices)
		{
			frames[frameIndex] = time;
			frameVertices[frameIndex] = vertices;
		}

		public override void Apply(Skeleton skeleton, float lastTime, float time, ExposedList<Event> firedEvents, float alpha, MixPose pose, MixDirection direction)
		{
			Slot slot = skeleton.slots.Items[slotIndex];
			VertexAttachment vertexAttachment = slot.attachment as VertexAttachment;
			if (vertexAttachment == null || !vertexAttachment.ApplyDeform(attachment))
			{
				return;
			}
			ExposedList<float> attachmentVertices = slot.attachmentVertices;
			float[][] array = frameVertices;
			int num = array[0].Length;
			if (attachmentVertices.Capacity < num)
			{
				attachmentVertices.Capacity = num;
			}
			attachmentVertices.Count = num;
			float[] items = attachmentVertices.Items;
			float[] array2 = frames;
			if (time < array2[0])
			{
				switch (pose)
				{
				case MixPose.Setup:
				{
					float[] array3;
					if (vertexAttachment.bones == null)
					{
						array3 = vertexAttachment.vertices;
					}
					else
					{
						array3 = zeros;
						if (array3.Length < num)
						{
							array3 = (zeros = new float[num]);
						}
					}
					Array.Copy(array3, 0, items, 0, num);
					break;
				}
				case MixPose.Current:
					if (alpha == 1f)
					{
						break;
					}
					if (vertexAttachment.bones == null)
					{
						float[] vertices = vertexAttachment.vertices;
						for (int i = 0; i < num; i++)
						{
							items[i] += (vertices[i] - items[i]) * alpha;
						}
					}
					else
					{
						alpha = 1f - alpha;
						for (int j = 0; j < num; j++)
						{
							items[j] *= alpha;
						}
					}
					break;
				}
				return;
			}
			if (time >= array2[array2.Length - 1])
			{
				float[] array4 = array[array2.Length - 1];
				if (alpha == 1f)
				{
					Array.Copy(array4, 0, items, 0, num);
				}
				else if (pose == MixPose.Setup)
				{
					if (vertexAttachment.bones == null)
					{
						float[] vertices2 = vertexAttachment.vertices;
						for (int k = 0; k < num; k++)
						{
							float num2 = vertices2[k];
							items[k] = num2 + (array4[k] - num2) * alpha;
						}
					}
					else
					{
						for (int l = 0; l < num; l++)
						{
							items[l] = array4[l] * alpha;
						}
					}
				}
				else
				{
					for (int m = 0; m < num; m++)
					{
						items[m] += (array4[m] - items[m]) * alpha;
					}
				}
				return;
			}
			int num3 = Animation.BinarySearch(array2, time);
			float[] array5 = array[num3 - 1];
			float[] array6 = array[num3];
			float num4 = array2[num3];
			float curvePercent = GetCurvePercent(num3 - 1, 1f - (time - num4) / (array2[num3 - 1] - num4));
			if (alpha == 1f)
			{
				for (int n = 0; n < num; n++)
				{
					float num5 = array5[n];
					items[n] = num5 + (array6[n] - num5) * curvePercent;
				}
			}
			else if (pose == MixPose.Setup)
			{
				if (vertexAttachment.bones == null)
				{
					float[] vertices3 = vertexAttachment.vertices;
					for (int num6 = 0; num6 < num; num6++)
					{
						float num7 = array5[num6];
						float num8 = vertices3[num6];
						items[num6] = num8 + (num7 + (array6[num6] - num7) * curvePercent - num8) * alpha;
					}
				}
				else
				{
					for (int num9 = 0; num9 < num; num9++)
					{
						float num10 = array5[num9];
						items[num9] = (num10 + (array6[num9] - num10) * curvePercent) * alpha;
					}
				}
			}
			else
			{
				for (int num11 = 0; num11 < num; num11++)
				{
					float num12 = array5[num11];
					items[num11] += (num12 + (array6[num11] - num12) * curvePercent - items[num11]) * alpha;
				}
			}
		}
	}
}
