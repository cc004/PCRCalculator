using System;
using System.Collections.Generic;
using UnityEngine;

namespace Spine.Unity
{
	[Serializable]
	public class MeshGenerator
	{
		[Serializable]
		public struct Settings
		{
			public bool useClipping;

			[Space]
			[Range(-0.1f, 0f)]
			public float zSpacing;

			[Space]
			[Header("Vertex Data")]
			public bool pmaVertexColors;

			public bool tintBlack;

			public bool calculateTangents;

			public bool addNormals;

			public bool immutableTriangles;

			public static Settings Default
			{
				get
				{
					Settings result = default(Settings);
					result.pmaVertexColors = true;
					result.zSpacing = 0f;
					result.useClipping = true;
					result.tintBlack = false;
					result.calculateTangents = false;
					result.addNormals = false;
					result.immutableTriangles = false;
					return result;
				}
			}
		}

		public Settings settings = Settings.Default;

		private const float BoundsMinDefault = float.PositiveInfinity;

		private const float BoundsMaxDefault = float.NegativeInfinity;

		[NonSerialized]
		private readonly ExposedList<Vector3> vertexBuffer = new ExposedList<Vector3>(4);

		[NonSerialized]
		private readonly ExposedList<Vector2> uvBuffer = new ExposedList<Vector2>(4);

		[NonSerialized]
		private readonly ExposedList<Color32> colorBuffer = new ExposedList<Color32>(4);

		[NonSerialized]
		private readonly ExposedList<ExposedList<int>> submeshes = new ExposedList<ExposedList<int>>
		{
			new ExposedList<int>(6)
		};

		[NonSerialized]
		private Vector2 meshBoundsMin;

		[NonSerialized]
		private Vector2 meshBoundsMax;

		[NonSerialized]
		private float meshBoundsThickness;

		[NonSerialized]
		private int submeshIndex;

		[NonSerialized]
		private SkeletonClipping clipper = new SkeletonClipping();

		[NonSerialized]
		private float[] tempVerts = new float[8];

		[NonSerialized]
		private int[] regionTriangles = new int[6]
		{
			0,
			1,
			2,
			2,
			3,
			0
		};

		[NonSerialized]
		private Vector3[] normals;

		[NonSerialized]
		private Vector4[] tangents;

		[NonSerialized]
		private Vector2[] tempTanBuffer;

		[NonSerialized]
		private ExposedList<Vector2> uv2;

		[NonSerialized]
		private ExposedList<Vector2> uv3;

		public int VertexCount => vertexBuffer.Count;

		public MeshGeneratorBuffers Buffers
		{
			get
			{
				MeshGeneratorBuffers result = default(MeshGeneratorBuffers);
				result.vertexCount = VertexCount;
				result.vertexBuffer = vertexBuffer.Items;
				result.uvBuffer = uvBuffer.Items;
				result.colorBuffer = colorBuffer.Items;
				result.meshGenerator = this;
				return result;
			}
		}

		public static void GenerateSingleSubmeshInstruction(SkeletonRendererInstruction instructionOutput, Skeleton skeleton, Material material)
		{
			ExposedList<Slot> drawOrder = skeleton.drawOrder;
			int count = drawOrder.Count;
			instructionOutput.Clear();
			ExposedList<SubmeshInstruction> submeshInstructions = instructionOutput.submeshInstructions;
			submeshInstructions.Resize(1);
			instructionOutput.attachments.Resize(count);
			Attachment[] items = instructionOutput.attachments.Items;
			int num = 0;
			SubmeshInstruction submeshInstruction = new SubmeshInstruction
			{
				skeleton = skeleton,
				preActiveClippingSlotSource = -1,
				startSlot = 0,
				rawFirstVertexIndex = 0,
				material = material,
				forceSeparate = false,
				endSlot = count
			};
			bool hasActiveClipping = false;
			Slot[] items2 = drawOrder.Items;
			for (int i = 0; i < count; i++)
			{
				Attachment attachment = (items[i] = items2[i].attachment);
				int num2;
				int num3;
				if (attachment is RegionAttachment)
				{
					num2 = 4;
					num3 = 6;
				}
				else
				{
					MeshAttachment meshAttachment = attachment as MeshAttachment;
					if (meshAttachment != null)
					{
						num2 = meshAttachment.worldVerticesLength >> 1;
						num3 = meshAttachment.triangles.Length;
					}
					else
					{
						if (attachment is ClippingAttachment)
						{
							submeshInstruction.hasClipping = true;
							hasActiveClipping = true;
						}
						num2 = 0;
						num3 = 0;
					}
				}
				submeshInstruction.rawTriangleCount += num3;
				submeshInstruction.rawVertexCount += num2;
				num += num2;
			}
			instructionOutput.hasActiveClipping = hasActiveClipping;
			instructionOutput.rawVertexCount = num;
			submeshInstructions.Items[0] = submeshInstruction;
		}

		public static void GenerateSkeletonRendererInstruction(SkeletonRendererInstruction instructionOutput, Skeleton skeleton, Dictionary<Slot, Material> customSlotMaterials, List<Slot> separatorSlots, bool generateMeshOverride, bool immutableTriangles = false)
		{
			ExposedList<Slot> drawOrder = skeleton.drawOrder;
			int count = drawOrder.Count;
			instructionOutput.Clear();
			ExposedList<SubmeshInstruction> submeshInstructions = instructionOutput.submeshInstructions;
			instructionOutput.attachments.Resize(count);
			Attachment[] items = instructionOutput.attachments.Items;
			int num = 0;
			bool hasActiveClipping = false;
			SubmeshInstruction submeshInstruction = new SubmeshInstruction
			{
				skeleton = skeleton,
				preActiveClippingSlotSource = -1
			};
			bool flag = customSlotMaterials != null && customSlotMaterials.Count > 0;
			int num2 = separatorSlots?.Count ?? 0;
			bool flag2 = num2 > 0;
			int num3 = -1;
			int preActiveClippingSlotSource = -1;
			SlotData slotData = null;
			int num4 = 0;
			Slot[] items2 = drawOrder.Items;
			for (int i = 0; i < count; i++)
			{
				Slot slot = items2[i];
				Attachment attachment = (items[i] = slot.attachment);
				int num5 = 0;
				int num6 = 0;
				object obj = null;
				bool flag3 = false;
				RegionAttachment regionAttachment = attachment as RegionAttachment;
				if (regionAttachment != null)
				{
					obj = regionAttachment.RendererObject;
					num5 = 4;
					num6 = 6;
				}
				else
				{
					MeshAttachment meshAttachment = attachment as MeshAttachment;
					if (meshAttachment != null)
					{
						obj = meshAttachment.RendererObject;
						num5 = meshAttachment.worldVerticesLength >> 1;
						num6 = meshAttachment.triangles.Length;
					}
					else
					{
						ClippingAttachment clippingAttachment = attachment as ClippingAttachment;
						if (clippingAttachment != null)
						{
							slotData = clippingAttachment.endSlot;
							num3 = i;
							submeshInstruction.hasClipping = true;
							hasActiveClipping = true;
						}
						flag3 = true;
					}
				}
				if (slotData != null && slot.data == slotData)
				{
					slotData = null;
					num3 = -1;
				}
				if (flag2)
				{
					submeshInstruction.forceSeparate = false;
					for (int j = 0; j < num2; j++)
					{
						if (slot == separatorSlots[j])
						{
							submeshInstruction.forceSeparate = true;
							break;
						}
					}
				}
				if (flag3)
				{
					if (submeshInstruction.forceSeparate && generateMeshOverride)
					{
						submeshInstruction.endSlot = i;
						submeshInstruction.preActiveClippingSlotSource = preActiveClippingSlotSource;
						submeshInstructions.Resize(num4 + 1);
						submeshInstructions.Items[num4] = submeshInstruction;
						num4++;
						submeshInstruction.startSlot = i;
						preActiveClippingSlotSource = num3;
						submeshInstruction.rawTriangleCount = 0;
						submeshInstruction.rawVertexCount = 0;
						submeshInstruction.rawFirstVertexIndex = num;
						submeshInstruction.hasClipping = num3 >= 0;
					}
					continue;
				}
				Material value;
				if (flag)
				{
					if (!customSlotMaterials.TryGetValue(slot, out value))
					{
						value = (Material)((AtlasRegion)obj).page.rendererObject;
					}
				}
				else
				{
					value = (Material)((AtlasRegion)obj).page.rendererObject;
				}
				if (submeshInstruction.forceSeparate || (submeshInstruction.rawVertexCount > 0 && (object)submeshInstruction.material != value))
				{
					submeshInstruction.endSlot = i;
					submeshInstruction.preActiveClippingSlotSource = preActiveClippingSlotSource;
					submeshInstructions.Resize(num4 + 1);
					submeshInstructions.Items[num4] = submeshInstruction;
					num4++;
					submeshInstruction.startSlot = i;
					preActiveClippingSlotSource = num3;
					submeshInstruction.rawTriangleCount = 0;
					submeshInstruction.rawVertexCount = 0;
					submeshInstruction.rawFirstVertexIndex = num;
					submeshInstruction.hasClipping = num3 >= 0;
				}
				submeshInstruction.material = value;
				submeshInstruction.rawTriangleCount += num6;
				submeshInstruction.rawVertexCount += num5;
				submeshInstruction.rawFirstVertexIndex = num;
				num += num5;
			}
			if (submeshInstruction.rawVertexCount > 0)
			{
				submeshInstruction.endSlot = count;
				submeshInstruction.preActiveClippingSlotSource = preActiveClippingSlotSource;
				submeshInstruction.forceSeparate = false;
				submeshInstructions.Resize(num4 + 1);
				submeshInstructions.Items[num4] = submeshInstruction;
			}
			instructionOutput.hasActiveClipping = hasActiveClipping;
			instructionOutput.rawVertexCount = num;
			instructionOutput.immutableTriangles = immutableTriangles;
		}

		public static void TryReplaceMaterials(ExposedList<SubmeshInstruction> workingSubmeshInstructions, Dictionary<Material, Material> customMaterialOverride)
		{
			SubmeshInstruction[] items = workingSubmeshInstructions.Items;
			for (int i = 0; i < workingSubmeshInstructions.Count; i++)
			{
				Material material = items[i].material;
				if (customMaterialOverride.TryGetValue(material, out var value))
				{
					items[i].material = value;
				}
			}
		}

		public void Begin()
		{
			vertexBuffer.Clear(clearArray: false);
			colorBuffer.Clear(clearArray: false);
			uvBuffer.Clear(clearArray: false);
			clipper.ClipEnd();
			meshBoundsMin.x = float.PositiveInfinity;
			meshBoundsMin.y = float.PositiveInfinity;
			meshBoundsMax.x = float.NegativeInfinity;
			meshBoundsMax.y = float.NegativeInfinity;
			meshBoundsThickness = 0f;
			submeshes.Count = 1;
			submeshes.Items[0].Clear(clearArray: false);
			submeshIndex = 0;
		}

		public void AddSubmesh(SubmeshInstruction instruction, bool updateTriangles = true)
		{
			Settings obj = settings;
			if (submeshes.Count - 1 < submeshIndex)
			{
				submeshes.Resize(submeshIndex + 1);
				if (submeshes.Items[submeshIndex] == null)
				{
					submeshes.Items[submeshIndex] = new ExposedList<int>();
				}
			}
			ExposedList<int> exposedList = submeshes.Items[submeshIndex];
			exposedList.Clear(clearArray: false);
			Skeleton skeleton = instruction.skeleton;
			Slot[] items = skeleton.drawOrder.Items;
			Color32 color = default(Color32);
			float num = skeleton.a * 255f;
			float r = skeleton.r;
			float g = skeleton.g;
			float b = skeleton.b;
			Vector2 vector = meshBoundsMin;
			Vector2 vector2 = meshBoundsMax;
			float zSpacing = obj.zSpacing;
			bool pmaVertexColors = obj.pmaVertexColors;
			bool tintBlack = obj.tintBlack;
			bool flag = obj.useClipping && instruction.hasClipping;
			if (flag && instruction.preActiveClippingSlotSource >= 0)
			{
				Slot slot = items[instruction.preActiveClippingSlotSource];
				clipper.ClipStart(slot, slot.attachment as ClippingAttachment);
			}
			for (int i = instruction.startSlot; i < instruction.endSlot; i++)
			{
				Slot slot2 = items[i];
				Attachment attachment = slot2.attachment;
				float z = zSpacing * (float)i;
				float[] array = tempVerts;
				Color color2 = default(Color);
				RegionAttachment regionAttachment = attachment as RegionAttachment;
				float[] array2;
				int[] array3;
				int num2;
				int num3;
				if (regionAttachment != null)
				{
					regionAttachment.ComputeWorldVertices(slot2.bone, array, 0);
					array2 = regionAttachment.uvs;
					array3 = regionTriangles;
					color2.r = regionAttachment.r;
					color2.g = regionAttachment.g;
					color2.b = regionAttachment.b;
					color2.a = regionAttachment.a;
					num2 = 4;
					num3 = 6;
				}
				else
				{
					MeshAttachment meshAttachment = attachment as MeshAttachment;
					if (meshAttachment == null)
					{
						if (flag)
						{
							ClippingAttachment clippingAttachment = attachment as ClippingAttachment;
							if (clippingAttachment != null)
							{
								clipper.ClipStart(slot2, clippingAttachment);
							}
						}
						continue;
					}
					int worldVerticesLength = meshAttachment.worldVerticesLength;
					if (array.Length < worldVerticesLength)
					{
						array = (tempVerts = new float[worldVerticesLength]);
					}
					meshAttachment.ComputeWorldVertices(slot2, 0, worldVerticesLength, array, 0);
					array2 = meshAttachment.uvs;
					array3 = meshAttachment.triangles;
					color2.r = meshAttachment.r;
					color2.g = meshAttachment.g;
					color2.b = meshAttachment.b;
					color2.a = meshAttachment.a;
					num2 = worldVerticesLength >> 1;
					num3 = meshAttachment.triangles.Length;
				}
				if (pmaVertexColors)
				{
					color.a = (byte)(num * slot2.a * color2.a);
					color.r = (byte)(r * slot2.r * color2.r * (float)(int)color.a);
					color.g = (byte)(g * slot2.g * color2.g * (float)(int)color.a);
					color.b = (byte)(b * slot2.b * color2.b * (float)(int)color.a);
					if (slot2.data.blendMode == BlendMode.Additive)
					{
						color.a = 0;
					}
				}
				else
				{
					color.a = (byte)(num * slot2.a * color2.a);
					color.r = (byte)(r * slot2.r * color2.r * 255f);
					color.g = (byte)(g * slot2.g * color2.g * 255f);
					color.b = (byte)(b * slot2.b * color2.b * 255f);
				}
				if (flag && clipper.IsClipping())
				{
					clipper.ClipTriangles(array, num2 << 1, array3, num3, array2);
					array = clipper.clippedVertices.Items;
					num2 = clipper.clippedVertices.Count >> 1;
					array3 = clipper.clippedTriangles.Items;
					num3 = clipper.clippedTriangles.Count;
					array2 = clipper.clippedUVs.Items;
				}
				if (num2 != 0 && num3 != 0)
				{
					if (tintBlack)
					{
						AddAttachmentTintBlack(slot2.r2, slot2.g2, slot2.b2, num2);
					}
					int count = vertexBuffer.Count;
					int num4 = count + num2;
					if (num4 > vertexBuffer.Items.Length)
					{
						Array.Resize(ref vertexBuffer.Items, num4);
						Array.Resize(ref uvBuffer.Items, num4);
						Array.Resize(ref colorBuffer.Items, num4);
					}
					vertexBuffer.Count = (uvBuffer.Count = (colorBuffer.Count = num4));
					Vector3[] items2 = vertexBuffer.Items;
					Vector2[] items3 = uvBuffer.Items;
					Color32[] items4 = colorBuffer.Items;
					if (count == 0)
					{
						for (int j = 0; j < num2; j++)
						{
							int num5 = count + j;
							int num6 = j << 1;
							float num7 = array[num6];
							float num8 = array[num6 + 1];
							items2[num5].x = num7;
							items2[num5].y = num8;
							items2[num5].z = z;
							items3[num5].x = array2[num6];
							items3[num5].y = array2[num6 + 1];
							items4[num5] = color;
							if (num7 < vector.x)
							{
								vector.x = num7;
							}
							if (num7 > vector2.x)
							{
								vector2.x = num7;
							}
							if (num8 < vector.y)
							{
								vector.y = num8;
							}
							if (num8 > vector2.y)
							{
								vector2.y = num8;
							}
						}
					}
					else
					{
						for (int k = 0; k < num2; k++)
						{
							int num9 = count + k;
							int num10 = k << 1;
							float num11 = array[num10];
							float num12 = array[num10 + 1];
							items2[num9].x = num11;
							items2[num9].y = num12;
							items2[num9].z = z;
							items3[num9].x = array2[num10];
							items3[num9].y = array2[num10 + 1];
							items4[num9] = color;
							if (num11 < vector.x)
							{
								vector.x = num11;
							}
							else if (num11 > vector2.x)
							{
								vector2.x = num11;
							}
							if (num12 < vector.y)
							{
								vector.y = num12;
							}
							else if (num12 > vector2.y)
							{
								vector2.y = num12;
							}
						}
					}
					if (updateTriangles)
					{
						int count2 = exposedList.Count;
						int num13 = count2 + num3;
						if (num13 > exposedList.Items.Length)
						{
							Array.Resize(ref exposedList.Items, num13);
						}
						exposedList.Count = num13;
						int[] items5 = exposedList.Items;
						for (int l = 0; l < num3; l++)
						{
							items5[count2 + l] = array3[l] + count;
						}
					}
				}
				clipper.ClipEnd(slot2);
			}
			clipper.ClipEnd();
			meshBoundsMin = vector;
			meshBoundsMax = vector2;
			meshBoundsThickness = (float)instruction.endSlot * zSpacing;
			int[] items6 = exposedList.Items;
			int m = exposedList.Count;
			for (int num14 = items6.Length; m < num14; m++)
			{
				items6[m] = 0;
			}
			submeshIndex++;
		}

		public void BuildMesh(SkeletonRendererInstruction instruction, bool updateTriangles)
		{
			SubmeshInstruction[] items = instruction.submeshInstructions.Items;
			int i = 0;
			for (int count = instruction.submeshInstructions.Count; i < count; i++)
			{
				AddSubmesh(items[i], updateTriangles);
			}
		}

		public void BuildMeshWithArrays(SkeletonRendererInstruction instruction, bool updateTriangles)
		{
			Settings settings = this.settings;
			int rawVertexCount = instruction.rawVertexCount;
			if (rawVertexCount > vertexBuffer.Items.Length)
			{
				Array.Resize(ref vertexBuffer.Items, rawVertexCount);
				Array.Resize(ref uvBuffer.Items, rawVertexCount);
				Array.Resize(ref colorBuffer.Items, rawVertexCount);
			}
			vertexBuffer.Count = (uvBuffer.Count = (colorBuffer.Count = rawVertexCount));
			Color32 color = default(Color32);
			int num = 0;
			float[] array = tempVerts;
			Vector3 v = meshBoundsMin;
			Vector3 v2 = meshBoundsMax;
			Vector3[] items = vertexBuffer.Items;
			Vector2[] items2 = uvBuffer.Items;
			Color32[] items3 = colorBuffer.Items;
			int num2 = 0;
			int i = 0;
			Vector2 vector = default(Vector2);
			Vector2 vector2 = default(Vector2);
			for (int count = instruction.submeshInstructions.Count; i < count; i++)
			{
				SubmeshInstruction submeshInstruction = instruction.submeshInstructions.Items[i];
				Skeleton skeleton = submeshInstruction.skeleton;
				Slot[] items4 = skeleton.drawOrder.Items;
				float num3 = skeleton.a * 255f;
				float r = skeleton.r;
				float g = skeleton.g;
				float b = skeleton.b;
				int endSlot = submeshInstruction.endSlot;
				int startSlot = submeshInstruction.startSlot;
				num2 = endSlot;
				if (settings.tintBlack)
				{
					int num4 = num;
					vector.y = 1f;
					if (uv2 == null)
					{
						uv2 = new ExposedList<Vector2>();
						uv3 = new ExposedList<Vector2>();
					}
					if (rawVertexCount > uv2.Items.Length)
					{
						Array.Resize(ref uv2.Items, rawVertexCount);
						Array.Resize(ref uv3.Items, rawVertexCount);
					}
					uv2.Count = (uv3.Count = rawVertexCount);
					Vector2[] items5 = uv2.Items;
					Vector2[] items6 = uv3.Items;
					for (int j = startSlot; j < endSlot; j++)
					{
						Slot slot = items4[j];
						Attachment attachment = slot.attachment;
						vector2.x = slot.r2;
						vector2.y = slot.g2;
						vector.x = slot.b2;
						if (attachment is RegionAttachment)
						{
							items5[num4] = vector2;
							items5[num4 + 1] = vector2;
							items5[num4 + 2] = vector2;
							items5[num4 + 3] = vector2;
							items6[num4] = vector;
							items6[num4 + 1] = vector;
							items6[num4 + 2] = vector;
							items6[num4 + 3] = vector;
							num4 += 4;
							continue;
						}
						MeshAttachment meshAttachment = attachment as MeshAttachment;
						if (meshAttachment != null)
						{
							int worldVerticesLength = meshAttachment.worldVerticesLength;
							for (int k = 0; k < worldVerticesLength; k += 2)
							{
								items5[num4] = vector2;
								items6[num4] = vector;
								num4++;
							}
						}
					}
				}
				for (int l = startSlot; l < endSlot; l++)
				{
					Slot slot2 = items4[l];
					Attachment attachment2 = slot2.attachment;
					float z = (float)l * settings.zSpacing;
					RegionAttachment regionAttachment = attachment2 as RegionAttachment;
					if (regionAttachment != null)
					{
						regionAttachment.ComputeWorldVertices(slot2.bone, array, 0);
						float num5 = array[0];
						float num6 = array[1];
						float num7 = array[2];
						float num8 = array[3];
						float num9 = array[4];
						float num10 = array[5];
						float num11 = array[6];
						float num12 = array[7];
						items[num].x = num5;
						items[num].y = num6;
						items[num].z = z;
						items[num + 1].x = num11;
						items[num + 1].y = num12;
						items[num + 1].z = z;
						items[num + 2].x = num7;
						items[num + 2].y = num8;
						items[num + 2].z = z;
						items[num + 3].x = num9;
						items[num + 3].y = num10;
						items[num + 3].z = z;
						if (settings.pmaVertexColors)
						{
							color.a = (byte)(num3 * slot2.a * regionAttachment.a);
							color.r = (byte)(r * slot2.r * regionAttachment.r * (float)(int)color.a);
							color.g = (byte)(g * slot2.g * regionAttachment.g * (float)(int)color.a);
							color.b = (byte)(b * slot2.b * regionAttachment.b * (float)(int)color.a);
							if (slot2.data.blendMode == BlendMode.Additive)
							{
								color.a = 0;
							}
						}
						else
						{
							color.a = (byte)(num3 * slot2.a * regionAttachment.a);
							color.r = (byte)(r * slot2.r * regionAttachment.r * 255f);
							color.g = (byte)(g * slot2.g * regionAttachment.g * 255f);
							color.b = (byte)(b * slot2.b * regionAttachment.b * 255f);
						}
						items3[num] = color;
						items3[num + 1] = color;
						items3[num + 2] = color;
						items3[num + 3] = color;
						float[] uvs = regionAttachment.uvs;
						items2[num].x = uvs[0];
						items2[num].y = uvs[1];
						items2[num + 1].x = uvs[6];
						items2[num + 1].y = uvs[7];
						items2[num + 2].x = uvs[2];
						items2[num + 2].y = uvs[3];
						items2[num + 3].x = uvs[4];
						items2[num + 3].y = uvs[5];
						if (num5 < v.x)
						{
							v.x = num5;
						}
						if (num5 > v2.x)
						{
							v2.x = num5;
						}
						if (num7 < v.x)
						{
							v.x = num7;
						}
						else if (num7 > v2.x)
						{
							v2.x = num7;
						}
						if (num9 < v.x)
						{
							v.x = num9;
						}
						else if (num9 > v2.x)
						{
							v2.x = num9;
						}
						if (num11 < v.x)
						{
							v.x = num11;
						}
						else if (num11 > v2.x)
						{
							v2.x = num11;
						}
						if (num6 < v.y)
						{
							v.y = num6;
						}
						if (num6 > v2.y)
						{
							v2.y = num6;
						}
						if (num8 < v.y)
						{
							v.y = num8;
						}
						else if (num8 > v2.y)
						{
							v2.y = num8;
						}
						if (num10 < v.y)
						{
							v.y = num10;
						}
						else if (num10 > v2.y)
						{
							v2.y = num10;
						}
						if (num12 < v.y)
						{
							v.y = num12;
						}
						else if (num12 > v2.y)
						{
							v2.y = num12;
						}
						num += 4;
						continue;
					}
					MeshAttachment meshAttachment2 = attachment2 as MeshAttachment;
					if (meshAttachment2 == null)
					{
						continue;
					}
					int worldVerticesLength2 = meshAttachment2.worldVerticesLength;
					if (array.Length < worldVerticesLength2)
					{
						array = (tempVerts = new float[worldVerticesLength2]);
					}
					meshAttachment2.ComputeWorldVertices(slot2, array);
					if (settings.pmaVertexColors)
					{
						color.a = (byte)(num3 * slot2.a * meshAttachment2.a);
						color.r = (byte)(r * slot2.r * meshAttachment2.r * (float)(int)color.a);
						color.g = (byte)(g * slot2.g * meshAttachment2.g * (float)(int)color.a);
						color.b = (byte)(b * slot2.b * meshAttachment2.b * (float)(int)color.a);
						if (slot2.data.blendMode == BlendMode.Additive)
						{
							color.a = 0;
						}
					}
					else
					{
						color.a = (byte)(num3 * slot2.a * meshAttachment2.a);
						color.r = (byte)(r * slot2.r * meshAttachment2.r * 255f);
						color.g = (byte)(g * slot2.g * meshAttachment2.g * 255f);
						color.b = (byte)(b * slot2.b * meshAttachment2.b * 255f);
					}
					float[] uvs2 = meshAttachment2.uvs;
					if (num == 0)
					{
						float num13 = array[0];
						float num14 = array[1];
						if (num13 < v.x)
						{
							v.x = num13;
						}
						if (num13 > v2.x)
						{
							v2.x = num13;
						}
						if (num14 < v.y)
						{
							v.y = num14;
						}
						if (num14 > v2.y)
						{
							v2.y = num14;
						}
					}
					for (int m = 0; m < worldVerticesLength2; m += 2)
					{
						float num15 = array[m];
						float num16 = array[m + 1];
						items[num].x = num15;
						items[num].y = num16;
						items[num].z = z;
						items3[num] = color;
						items2[num].x = uvs2[m];
						items2[num].y = uvs2[m + 1];
						if (num15 < v.x)
						{
							v.x = num15;
						}
						else if (num15 > v2.x)
						{
							v2.x = num15;
						}
						if (num16 < v.y)
						{
							v.y = num16;
						}
						else if (num16 > v2.y)
						{
							v2.y = num16;
						}
						num++;
					}
				}
			}
			meshBoundsMin = v;
			meshBoundsMax = v2;
			meshBoundsThickness = (float)num2 * settings.zSpacing;
			if (!updateTriangles)
			{
				return;
			}
			int count2 = instruction.submeshInstructions.Count;
			if (submeshes.Count < count2)
			{
				submeshes.Resize(count2);
				int n = 0;
				for (int num17 = count2; n < num17; n++)
				{
					ExposedList<int> exposedList = submeshes.Items[n];
					if (exposedList == null)
					{
						submeshes.Items[n] = new ExposedList<int>();
					}
					else
					{
						exposedList.Clear(clearArray: false);
					}
				}
			}
			SubmeshInstruction[] items7 = instruction.submeshInstructions.Items;
			int num18 = 0;
			for (int num19 = 0; num19 < count2; num19++)
			{
				SubmeshInstruction submeshInstruction2 = items7[num19];
				ExposedList<int> exposedList2 = submeshes.Items[num19];
				int rawTriangleCount = submeshInstruction2.rawTriangleCount;
				if (rawTriangleCount > exposedList2.Items.Length)
				{
					Array.Resize(ref exposedList2.Items, rawTriangleCount);
				}
				else if (rawTriangleCount < exposedList2.Items.Length)
				{
					int[] items8 = exposedList2.Items;
					int num20 = rawTriangleCount;
					for (int num21 = items8.Length; num20 < num21; num20++)
					{
						items8[num20] = 0;
					}
				}
				exposedList2.Count = rawTriangleCount;
				int[] items9 = exposedList2.Items;
				int num22 = 0;
				Slot[] items10 = submeshInstruction2.skeleton.drawOrder.Items;
				int num23 = submeshInstruction2.startSlot;
				for (int endSlot2 = submeshInstruction2.endSlot; num23 < endSlot2; num23++)
				{
					Attachment attachment3 = items10[num23].attachment;
					if (attachment3 is RegionAttachment)
					{
						items9[num22] = num18;
						items9[num22 + 1] = num18 + 2;
						items9[num22 + 2] = num18 + 1;
						items9[num22 + 3] = num18 + 2;
						items9[num22 + 4] = num18 + 3;
						items9[num22 + 5] = num18 + 1;
						num22 += 6;
						num18 += 4;
						continue;
					}
					MeshAttachment meshAttachment3 = attachment3 as MeshAttachment;
					if (meshAttachment3 != null)
					{
						int[] triangles = meshAttachment3.triangles;
						int num24 = 0;
						int num25 = triangles.Length;
						while (num24 < num25)
						{
							items9[num22] = num18 + triangles[num24];
							num24++;
							num22++;
						}
						num18 += meshAttachment3.worldVerticesLength >> 1;
					}
				}
			}
		}

		public void ScaleVertexData(float scale)
		{
			Vector3[] items = vertexBuffer.Items;
			int i = 0;
			for (int count = vertexBuffer.Count; i < count; i++)
			{
				items[i] *= scale;
			}
			meshBoundsMin *= scale;
			meshBoundsMax *= scale;
			meshBoundsThickness *= scale;
		}

		private void AddAttachmentTintBlack(float r2, float g2, float b2, int vertexCount)
		{
			Vector2 vector = new Vector2(r2, g2);
			Vector2 vector2 = new Vector2(b2, 1f);
			int count = vertexBuffer.Count;
			int num = count + vertexCount;
			if (uv2 == null)
			{
				uv2 = new ExposedList<Vector2>();
				uv3 = new ExposedList<Vector2>();
			}
			if (num > uv2.Items.Length)
			{
				Array.Resize(ref uv2.Items, num);
				Array.Resize(ref uv3.Items, num);
			}
			uv2.Count = (uv3.Count = num);
			Vector2[] items = uv2.Items;
			Vector2[] items2 = uv3.Items;
			for (int i = 0; i < vertexCount; i++)
			{
				items[count + i] = vector;
				items2[count + i] = vector2;
			}
		}

		public void FillVertexData(Mesh mesh)
		{
			Vector3[] items = vertexBuffer.Items;
			Vector2[] items2 = uvBuffer.Items;
			Color32[] items3 = colorBuffer.Items;
			ExposedList<int>[] items4 = submeshes.Items;
			int count = submeshes.Count;
			int count2 = vertexBuffer.Count;
			int num = vertexBuffer.Items.Length;
			Vector3 zero = Vector3.zero;
			for (int i = count2; i < num; i++)
			{
				items[i] = zero;
			}
			mesh.vertices = items;
			mesh.uv = items2;
			mesh.colors32 = items3;
			if (float.IsInfinity(meshBoundsMin.x))
			{
				mesh.bounds = default(Bounds);
			}
			else
			{
				Vector2 b = (meshBoundsMax - meshBoundsMin) * 0.5f;
				mesh.bounds = new Bounds
				{
					center = meshBoundsMin + b,
					extents = new Vector3(b.x, b.y, meshBoundsThickness * 0.5f)
				};
			}
			int count3 = vertexBuffer.Count;
			if (settings.addNormals)
			{
				int num2 = 0;
				if (normals == null)
				{
					normals = new Vector3[count3];
				}
				else
				{
					num2 = normals.Length;
				}
				if (num2 < count3)
				{
					Array.Resize(ref normals, count3);
					Vector3[] array = normals;
					for (int j = num2; j < count3; j++)
					{
						array[j] = Vector3.back;
					}
				}
				mesh.normals = normals;
			}
			if (settings.tintBlack)
			{
				mesh.uv2 = uv2.Items;
				mesh.uv3 = uv3.Items;
			}
			if (settings.calculateTangents)
			{
				SolveTangents2DEnsureSize(ref tangents, ref tempTanBuffer, count3);
				for (int k = 0; k < count; k++)
				{
					int[] items5 = items4[k].Items;
					int count4 = items4[k].Count;
					SolveTangents2DTriangles(tempTanBuffer, items5, count4, items, items2, count3);
				}
				SolveTangents2DBuffer(tangents, tempTanBuffer, count3);
				mesh.tangents = tangents;
			}
		}

		public void FillTriangles(Mesh mesh)
		{
			int count = submeshes.Count;
			ExposedList<int>[] items = submeshes.Items;
			mesh.subMeshCount = count;
			for (int i = 0; i < count; i++)
			{
				mesh.SetTriangles(items[i].Items, i, calculateBounds: false);
			}
		}

		public void FillTrianglesSingle(Mesh mesh)
		{
			mesh.SetTriangles(submeshes.Items[0].Items, 0, calculateBounds: false);
		}

		public void TrimExcess()
		{
			vertexBuffer.TrimExcess();
			uvBuffer.TrimExcess();
			colorBuffer.TrimExcess();
			if (uv2 != null)
			{
				uv2.TrimExcess();
			}
			if (uv3 != null)
			{
				uv3.TrimExcess();
			}
			int count = vertexBuffer.Count;
			if (normals != null)
			{
				Array.Resize(ref normals, count);
			}
			if (tangents != null)
			{
				Array.Resize(ref tangents, count);
			}
		}

		internal static void SolveTangents2DEnsureSize(ref Vector4[] tangentBuffer, ref Vector2[] tempTanBuffer, int vertexCount)
		{
			if (tangentBuffer == null || tangentBuffer.Length < vertexCount)
			{
				tangentBuffer = new Vector4[vertexCount];
			}
			if (tempTanBuffer == null || tempTanBuffer.Length < vertexCount * 2)
			{
				tempTanBuffer = new Vector2[vertexCount * 2];
			}
		}

		internal static void SolveTangents2DTriangles(Vector2[] tempTanBuffer, int[] triangles, int triangleCount, Vector3[] vertices, Vector2[] uvs, int vertexCount)
		{
			Vector2 vector7 = default(Vector2);
			Vector2 vector8 = default(Vector2);
			for (int i = 0; i < triangleCount; i += 3)
			{
				int num = triangles[i];
				int num2 = triangles[i + 1];
				int num3 = triangles[i + 2];
				Vector3 vector = vertices[num];
				Vector3 vector2 = vertices[num2];
				Vector3 vector3 = vertices[num3];
				Vector2 vector4 = uvs[num];
				Vector2 vector5 = uvs[num2];
				Vector2 vector6 = uvs[num3];
				float num4 = vector2.x - vector.x;
				float num5 = vector3.x - vector.x;
				float num6 = vector2.y - vector.y;
				float num7 = vector3.y - vector.y;
				float num8 = vector5.x - vector4.x;
				float num9 = vector6.x - vector4.x;
				float num10 = vector5.y - vector4.y;
				float num11 = vector6.y - vector4.y;
				float num12 = num8 * num11 - num9 * num10;
				float num13 = ((num12 == 0f) ? 0f : (1f / num12));
				vector7.x = (num11 * num4 - num10 * num5) * num13;
				vector7.y = (num11 * num6 - num10 * num7) * num13;
				tempTanBuffer[num] = (tempTanBuffer[num2] = (tempTanBuffer[num3] = vector7));
				vector8.x = (num8 * num5 - num9 * num4) * num13;
				vector8.y = (num8 * num7 - num9 * num6) * num13;
				tempTanBuffer[vertexCount + num] = (tempTanBuffer[vertexCount + num2] = (tempTanBuffer[vertexCount + num3] = vector8));
			}
		}

		internal static void SolveTangents2DBuffer(Vector4[] tangents, Vector2[] tempTanBuffer, int vertexCount)
		{
			Vector4 vector = default(Vector4);
			vector.z = 0f;
			for (int i = 0; i < vertexCount; i++)
			{
				Vector2 vector2 = tempTanBuffer[i];
				float num = Mathf.Sqrt(vector2.x * vector2.x + vector2.y * vector2.y);
				if ((double)num > 1E-05)
				{
					float num2 = 1f / num;
					vector2.x *= num2;
					vector2.y *= num2;
				}
				Vector2 vector3 = tempTanBuffer[vertexCount + i];
				vector.x = vector2.x;
				vector.y = vector2.y;
				vector.w = ((vector2.y * vector3.x > vector2.x * vector3.y) ? 1 : (-1));
				tangents[i] = vector;
			}
		}
	}
}
