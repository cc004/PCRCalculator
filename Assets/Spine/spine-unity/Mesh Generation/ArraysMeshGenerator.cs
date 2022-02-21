using UnityEngine;

namespace Spine.Unity.MeshGeneration
{
	public class ArraysMeshGenerator
	{
		public class SubmeshTriangleBuffer
		{
			public int[] triangles;

			public SubmeshTriangleBuffer(int triangleCount)
			{
				triangles = new int[triangleCount];
			}
		}

		protected bool premultiplyVertexColors = true;

		protected float[] attachmentVertexBuffer = new float[8];

		protected Vector3[] meshVertices;

		protected Color32[] meshColors32;

		protected Vector2[] meshUVs;

		protected bool generateNormals;

		private Vector3[] meshNormals;

		public bool PremultiplyVertexColors
		{
			get
			{
				return premultiplyVertexColors;
			}
			set
			{
				premultiplyVertexColors = value;
			}
		}

		public bool GenerateNormals
		{
			get
			{
				return generateNormals;
			}
			set
			{
				generateNormals = value;
			}
		}

		public void TryAddNormalsTo(Mesh mesh, int targetVertexCount)
		{
			if (!generateNormals)
			{
				return;
			}
			if (meshNormals == null || targetVertexCount > meshNormals.Length)
			{
				meshNormals = new Vector3[targetVertexCount];
				Vector3 vector = new Vector3(0f, 0f, -1f);
				Vector3[] array = meshNormals;
				for (int i = 0; i < targetVertexCount; i++)
				{
					array[i] = vector;
				}
			}
			mesh.normals = meshNormals;
		}

		public static bool EnsureSize(int targetVertexCount, ref Vector3[] vertices, ref Vector2[] uvs, ref Color32[] colors)
		{
			Vector3[] array = vertices;
			bool flag = array == null || targetVertexCount > array.Length;
			if (flag)
			{
				vertices = new Vector3[targetVertexCount];
				colors = new Color32[targetVertexCount];
				uvs = new Vector2[targetVertexCount];
			}
			else
			{
				Vector3 zero = Vector3.zero;
				int i = targetVertexCount;
				for (int num = array.Length; i < num; i++)
				{
					array[i] = zero;
				}
			}
			return flag;
		}

		public static bool EnsureTriangleBuffersSize(ExposedList<SubmeshTriangleBuffer> submeshBuffers, int targetSubmeshCount, SubmeshInstruction[] instructionItems)
		{
			bool flag = submeshBuffers.Count < targetSubmeshCount;
			if (flag)
			{
				submeshBuffers.GrowIfNeeded(targetSubmeshCount - submeshBuffers.Count);
				int num = submeshBuffers.Count;
				while (submeshBuffers.Count < targetSubmeshCount)
				{
					submeshBuffers.Add(new SubmeshTriangleBuffer(instructionItems[num].rawTriangleCount));
					num++;
				}
			}
			return flag;
		}

		public static void FillVerts(Skeleton skeleton, int startSlot, int endSlot, float zSpacing, bool pmaColors, Vector3[] verts, Vector2[] uvs, Color32[] colors, ref int vertexIndex, ref float[] tempVertBuffer, ref Vector3 boundsMin, ref Vector3 boundsMax)
		{
			Color32 color = default(Color32);
			Slot[] items = skeleton.DrawOrder.Items;
			float num = skeleton.a * 255f;
			float r = skeleton.r;
			float g = skeleton.g;
			float b = skeleton.b;
			int num2 = vertexIndex;
			float[] array = tempVertBuffer;
			Vector3 vector = boundsMin;
			Vector3 vector2 = boundsMax;
			for (int i = startSlot; i < endSlot; i++)
			{
				Slot slot = items[i];
				Attachment attachment = slot.attachment;
				float z = i * zSpacing;
				RegionAttachment regionAttachment = attachment as RegionAttachment;
				if (regionAttachment != null)
				{
					regionAttachment.ComputeWorldVertices(slot.bone, array, 0);
					float num3 = array[0];
					float num4 = array[1];
					float num5 = array[2];
					float num6 = array[3];
					float num7 = array[4];
					float num8 = array[5];
					float num9 = array[6];
					float num10 = array[7];
					verts[num2].x = num3;
					verts[num2].y = num4;
					verts[num2].z = z;
					verts[num2 + 1].x = num9;
					verts[num2 + 1].y = num10;
					verts[num2 + 1].z = z;
					verts[num2 + 2].x = num5;
					verts[num2 + 2].y = num6;
					verts[num2 + 2].z = z;
					verts[num2 + 3].x = num7;
					verts[num2 + 3].y = num8;
					verts[num2 + 3].z = z;
					if (pmaColors)
					{
						color.a = (byte)(num * slot.a * regionAttachment.a);
						color.r = (byte)(r * slot.r * regionAttachment.r * color.a);
						color.g = (byte)(g * slot.g * regionAttachment.g * color.a);
						color.b = (byte)(b * slot.b * regionAttachment.b * color.a);
						if (slot.data.blendMode == BlendMode.Additive)
						{
							color.a = 0;
						}
					}
					else
					{
						color.a = (byte)(num * slot.a * regionAttachment.a);
						color.r = (byte)(r * slot.r * regionAttachment.r * 255f);
						color.g = (byte)(g * slot.g * regionAttachment.g * 255f);
						color.b = (byte)(b * slot.b * regionAttachment.b * 255f);
					}
					colors[num2] = color;
					colors[num2 + 1] = color;
					colors[num2 + 2] = color;
					colors[num2 + 3] = color;
					float[] uvs2 = regionAttachment.uvs;
					uvs[num2].x = uvs2[0];
					uvs[num2].y = uvs2[1];
					uvs[num2 + 1].x = uvs2[6];
					uvs[num2 + 1].y = uvs2[7];
					uvs[num2 + 2].x = uvs2[2];
					uvs[num2 + 2].y = uvs2[3];
					uvs[num2 + 3].x = uvs2[4];
					uvs[num2 + 3].y = uvs2[5];
					if (num3 < vector.x)
					{
						vector.x = num3;
					}
					else if (num3 > vector2.x)
					{
						vector2.x = num3;
					}
					if (num5 < vector.x)
					{
						vector.x = num5;
					}
					else if (num5 > vector2.x)
					{
						vector2.x = num5;
					}
					if (num7 < vector.x)
					{
						vector.x = num7;
					}
					else if (num7 > vector2.x)
					{
						vector2.x = num7;
					}
					if (num9 < vector.x)
					{
						vector.x = num9;
					}
					else if (num9 > vector2.x)
					{
						vector2.x = num9;
					}
					if (num4 < vector.y)
					{
						vector.y = num4;
					}
					else if (num4 > vector2.y)
					{
						vector2.y = num4;
					}
					if (num6 < vector.y)
					{
						vector.y = num6;
					}
					else if (num6 > vector2.y)
					{
						vector2.y = num6;
					}
					if (num8 < vector.y)
					{
						vector.y = num8;
					}
					else if (num8 > vector2.y)
					{
						vector2.y = num8;
					}
					if (num10 < vector.y)
					{
						vector.y = num10;
					}
					else if (num10 > vector2.y)
					{
						vector2.y = num10;
					}
					num2 += 4;
					continue;
				}
				MeshAttachment meshAttachment = attachment as MeshAttachment;
				if (meshAttachment == null)
				{
					continue;
				}
				int num11 = meshAttachment.vertices.Length;
				if (array.Length < num11)
				{
					array = new float[num11];
				}
				meshAttachment.ComputeWorldVertices(slot, array);
				if (pmaColors)
				{
					color.a = (byte)(num * slot.a * meshAttachment.a);
					color.r = (byte)(r * slot.r * meshAttachment.r * color.a);
					color.g = (byte)(g * slot.g * meshAttachment.g * color.a);
					color.b = (byte)(b * slot.b * meshAttachment.b * color.a);
					if (slot.data.blendMode == BlendMode.Additive)
					{
						color.a = 0;
					}
				}
				else
				{
					color.a = (byte)(num * slot.a * meshAttachment.a);
					color.r = (byte)(r * slot.r * meshAttachment.r * 255f);
					color.g = (byte)(g * slot.g * meshAttachment.g * 255f);
					color.b = (byte)(b * slot.b * meshAttachment.b * 255f);
				}
				float[] uvs3 = meshAttachment.uvs;
				for (int j = 0; j < num11; j += 2)
				{
					float num12 = array[j];
					float num13 = array[j + 1];
					verts[num2].x = num12;
					verts[num2].y = num13;
					verts[num2].z = z;
					colors[num2] = color;
					uvs[num2].x = uvs3[j];
					uvs[num2].y = uvs3[j + 1];
					if (num12 < vector.x)
					{
						vector.x = num12;
					}
					else if (num12 > vector2.x)
					{
						vector2.x = num12;
					}
					if (num13 < vector.y)
					{
						vector.y = num13;
					}
					else if (num13 > vector2.y)
					{
						vector2.y = num13;
					}
					num2++;
				}
			}
			vertexIndex = num2;
			tempVertBuffer = array;
			boundsMin = vector;
			boundsMax = vector2;
		}

		public static void FillTriangles(Skeleton skeleton, int triangleCount, int firstVertex, int startSlot, int endSlot, ref int[] triangleBuffer, bool isLastSubmesh)
		{
			int num = triangleBuffer.Length;
			int[] array = triangleBuffer;
			if (isLastSubmesh)
			{
				if (num > triangleCount)
				{
					for (int i = triangleCount; i < num; i++)
					{
						array[i] = 0;
					}
				}
				else if (num < triangleCount)
				{
					array = (triangleBuffer = new int[triangleCount]);
				}
			}
			else if (num != triangleCount)
			{
				array = (triangleBuffer = new int[triangleCount]);
			}
			int num2 = 0;
			int num3 = firstVertex;
			Slot[] items = skeleton.drawOrder.Items;
			for (int j = startSlot; j < endSlot; j++)
			{
				Attachment attachment = items[j].attachment;
				if (attachment is RegionAttachment)
				{
					array[num2] = num3;
					array[num2 + 1] = num3 + 2;
					array[num2 + 2] = num3 + 1;
					array[num2 + 3] = num3 + 2;
					array[num2 + 4] = num3 + 3;
					array[num2 + 5] = num3 + 1;
					num2 += 6;
					num3 += 4;
					continue;
				}
				int[] array2 = new int[0];
				int num4 = 0;
				MeshAttachment meshAttachment = attachment as MeshAttachment;
				if (meshAttachment != null)
				{
					num4 = meshAttachment.vertices.Length >> 1;
					array2 = meshAttachment.triangles;
				}
				int num5 = 0;
				int num6 = array2.Length;
				while (num5 < num6)
				{
					array[num2] = num3 + array2[num5];
					num5++;
					num2++;
				}
				num3 += num4;
			}
		}

		public static Bounds ToBounds(Vector3 boundsMin, Vector3 boundsMax)
		{
			Vector3 vector = boundsMax - boundsMin;
			return new Bounds(boundsMin + vector * 0.5f, vector);
		}
	}
}
