using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Elements;
using Elements.SpineFormat;

namespace Spine
{
	public class SkeletonBinary
	{
		internal class Vertices
		{
			public int[] bones;

			public float[] vertices;
		}

		public const int BONE_ROTATE = 0;

		public const int BONE_TRANSLATE = 1;

		public const int BONE_SCALE = 2;

		public const int BONE_SHEAR = 3;

		public const int SLOT_ATTACHMENT = 0;

		public const int SLOT_COLOR = 1;

		public const int SLOT_TWO_COLOR = 2;

		public const int PATH_POSITION = 0;

		public const int PATH_SPACING = 1;

		public const int PATH_MIX = 2;

		public const int CURVE_LINEAR = 0;

		public const int CURVE_STEPPED = 1;

		public const int CURVE_BEZIER = 2;

		private AttachmentLoader attachmentLoader;

		private byte[] buffer = new byte[32];

		private List<SkeletonJson.LinkedMesh> linkedMeshes = new List<SkeletonJson.LinkedMesh>();

		public static readonly TransformMode[] TransformModeValues = new TransformMode[5]
		{
			TransformMode.Normal,
			TransformMode.OnlyTranslation,
			TransformMode.NoRotationOrReflection,
			TransformMode.NoScale,
			TransformMode.NoScaleOrReflection
		};

		public float Scale
		{
			get;
			set;
		}

		public SkeletonBinary(params Atlas[] atlasArray)
			: this(new AtlasAttachmentLoader(atlasArray))
		{
		}

		public SkeletonBinary(AttachmentLoader attachmentLoader)
		{
			if (attachmentLoader == null)
			{
				throw new ArgumentNullException("attachmentLoader");
			}
			this.attachmentLoader = attachmentLoader;
			Scale = 1f;
		}

		public SkeletonData ReadSkeletonData(string path)
		{
			using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
			{
				byte[] data = new byte[fileStream.Length];
				fileStream.Read(data, 0, (int)fileStream.Length);
				SimpleMemoryStream input = new SimpleMemoryStream(data);
				SkeletonData skeletonData = ReadSkeletonData(input);
				skeletonData.name = Path.GetFileNameWithoutExtension(path);
				return skeletonData;
			}
		}

		public static string GetVersionString(SimpleMemoryStream input)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			try
			{
				int num = ReadVarint(input, optimizePositive: true);
				if (num > 1)
				{
					input.Position += num - 1;
				}
				num = ReadVarint(input, optimizePositive: true);
				if (num > 1)
				{
					num--;
					byte[] bytes = new byte[num];
					ReadFully(input, bytes, 0, num);
					return Encoding.UTF8.GetString(bytes, 0, num);
				}
				throw new ArgumentException("SimpleMemoryStream does not contain a valid binary Skeleton Data.", "input");
			}
			catch (Exception arg)
			{
				throw new ArgumentException("SimpleMemoryStream does not contain a valid binary Skeleton Data.\n" + arg, "input");
			}
		}

		public SkeletonData ReadSkeletonData(SimpleMemoryStream input)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			float scale = Scale;
			SkeletonData skeletonData = new SkeletonData();
			skeletonData.hash = ReadString(input);
			if (skeletonData.hash.Length == 0)
			{
				skeletonData.hash = null;
			}
			skeletonData.version = ReadString(input);
			if (skeletonData.version.Length == 0)
			{
				skeletonData.version = null;
			}
			skeletonData.width = ReadFloat(input);
			skeletonData.height = ReadFloat(input);
			bool flag = ReadBoolean(input);
			if (flag)
			{
				skeletonData.fps = ReadFloat(input);
				skeletonData.imagesPath = ReadString(input);
				if (skeletonData.imagesPath.Length == 0)
				{
					skeletonData.imagesPath = null;
				}
			}
			int i = 0;
			for (int num = ReadVarint(input, optimizePositive: true); i < num; i++)
			{
				string name = ReadString(input);
				BoneData parent = ((i == 0) ? null : skeletonData.bones.Items[ReadVarint(input, optimizePositive: true)]);
				BoneData boneData = new BoneData(i, name, parent);
				boneData.rotation = ReadFloat(input);
				boneData.x = ReadFloat(input) * scale;
				boneData.y = ReadFloat(input) * scale;
				boneData.scaleX = ReadFloat(input);
				boneData.scaleY = ReadFloat(input);
				boneData.shearX = ReadFloat(input);
				boneData.shearY = ReadFloat(input);
				boneData.length = ReadFloat(input) * scale;
				boneData.transformMode = TransformModeValues[ReadVarint(input, optimizePositive: true)];
				if (flag)
				{
					ReadInt(input);
				}
				skeletonData.bones.Add(boneData);
			}
			int j = 0;
			for (int num2 = ReadVarint(input, optimizePositive: true); j < num2; j++)
			{
				string name2 = ReadString(input);
				BoneData boneData2 = skeletonData.bones.Items[ReadVarint(input, optimizePositive: true)];
				SlotData slotData = new SlotData(j, name2, boneData2);
				int num3 = ReadInt(input);
				slotData.r = (float)((num3 & 0xFF000000u) >> 24) / 255f;
				slotData.g = (float)((num3 & 0xFF0000) >> 16) / 255f;
				slotData.b = (float)((num3 & 0xFF00) >> 8) / 255f;
				slotData.a = (float)(num3 & 0xFF) / 255f;
				int num4 = ReadInt(input);
				if (num4 != -1)
				{
					slotData.hasSecondColor = true;
					slotData.r2 = (float)((num4 & 0xFF0000) >> 16) / 255f;
					slotData.g2 = (float)((num4 & 0xFF00) >> 8) / 255f;
					slotData.b2 = (float)(num4 & 0xFF) / 255f;
				}
				slotData.attachmentName = ReadString(input);
				slotData.blendMode = (BlendMode)ReadVarint(input, optimizePositive: true);
				skeletonData.slots.Add(slotData);
			}
			int k = 0;
			for (int num5 = ReadVarint(input, optimizePositive: true); k < num5; k++)
			{
				IkConstraintData ikConstraintData = new IkConstraintData(ReadString(input));
				ikConstraintData.order = ReadVarint(input, optimizePositive: true);
				int l = 0;
				for (int num6 = ReadVarint(input, optimizePositive: true); l < num6; l++)
				{
					ikConstraintData.bones.Add(skeletonData.bones.Items[ReadVarint(input, optimizePositive: true)]);
				}
				ikConstraintData.target = skeletonData.bones.Items[ReadVarint(input, optimizePositive: true)];
				ikConstraintData.mix = ReadFloat(input);
				ikConstraintData.bendDirection = ReadSByte(input);
				skeletonData.ikConstraints.Add(ikConstraintData);
			}
			int m = 0;
			for (int num7 = ReadVarint(input, optimizePositive: true); m < num7; m++)
			{
				TransformConstraintData transformConstraintData = new TransformConstraintData(ReadString(input));
				transformConstraintData.order = ReadVarint(input, optimizePositive: true);
				int n = 0;
				for (int num8 = ReadVarint(input, optimizePositive: true); n < num8; n++)
				{
					transformConstraintData.bones.Add(skeletonData.bones.Items[ReadVarint(input, optimizePositive: true)]);
				}
				transformConstraintData.target = skeletonData.bones.Items[ReadVarint(input, optimizePositive: true)];
				transformConstraintData.local = ReadBoolean(input);
				transformConstraintData.relative = ReadBoolean(input);
				transformConstraintData.offsetRotation = ReadFloat(input);
				transformConstraintData.offsetX = ReadFloat(input) * scale;
				transformConstraintData.offsetY = ReadFloat(input) * scale;
				transformConstraintData.offsetScaleX = ReadFloat(input);
				transformConstraintData.offsetScaleY = ReadFloat(input);
				transformConstraintData.offsetShearY = ReadFloat(input);
				transformConstraintData.rotateMix = ReadFloat(input);
				transformConstraintData.translateMix = ReadFloat(input);
				transformConstraintData.scaleMix = ReadFloat(input);
				transformConstraintData.shearMix = ReadFloat(input);
				skeletonData.transformConstraints.Add(transformConstraintData);
			}
			int num9 = 0;
			for (int num10 = ReadVarint(input, optimizePositive: true); num9 < num10; num9++)
			{
				PathConstraintData pathConstraintData = new PathConstraintData(ReadString(input));
				pathConstraintData.order = ReadVarint(input, optimizePositive: true);
				int num11 = 0;
				for (int num12 = ReadVarint(input, optimizePositive: true); num11 < num12; num11++)
				{
					pathConstraintData.bones.Add(skeletonData.bones.Items[ReadVarint(input, optimizePositive: true)]);
				}
				pathConstraintData.target = skeletonData.slots.Items[ReadVarint(input, optimizePositive: true)];
				pathConstraintData.positionMode = (PositionMode)Enum.GetValues(typeof(PositionMode)).GetValue(ReadVarint(input, optimizePositive: true));
				pathConstraintData.spacingMode = (SpacingMode)Enum.GetValues(typeof(SpacingMode)).GetValue(ReadVarint(input, optimizePositive: true));
				pathConstraintData.rotateMode = (RotateMode)Enum.GetValues(typeof(RotateMode)).GetValue(ReadVarint(input, optimizePositive: true));
				pathConstraintData.offsetRotation = ReadFloat(input);
				pathConstraintData.position = ReadFloat(input);
				if (pathConstraintData.positionMode == PositionMode.Fixed)
				{
					pathConstraintData.position *= scale;
				}
				pathConstraintData.spacing = ReadFloat(input);
				if (pathConstraintData.spacingMode == SpacingMode.Length || pathConstraintData.spacingMode == SpacingMode.Fixed)
				{
					pathConstraintData.spacing *= scale;
				}
				pathConstraintData.rotateMix = ReadFloat(input);
				pathConstraintData.translateMix = ReadFloat(input);
				skeletonData.pathConstraints.Add(pathConstraintData);
			}
			Skin skin = ReadSkin(input, skeletonData, "default", flag);
			if (skin != null)
			{
				skeletonData.defaultSkin = skin;
				skeletonData.skins.Add(skin);
			}
			int num13 = 0;
			for (int num14 = ReadVarint(input, optimizePositive: true); num13 < num14; num13++)
			{
				skeletonData.skins.Add(ReadSkin(input, skeletonData, ReadString(input), flag));
			}
			int num15 = 0;
			for (int count = linkedMeshes.Count; num15 < count; num15++)
			{
				SkeletonJson.LinkedMesh linkedMesh = linkedMeshes[num15];
				Attachment attachment = (((linkedMesh.skin == null) ? skeletonData.DefaultSkin : skeletonData.FindSkin(linkedMesh.skin)) ?? throw new Exception("Skin not found: " + linkedMesh.skin)).GetAttachment(linkedMesh.slotIndex, linkedMesh.parent);
				if (attachment == null)
				{
					throw new Exception("Parent mesh not found: " + linkedMesh.parent);
				}
				linkedMesh.mesh.ParentMesh = (MeshAttachment)attachment;
				linkedMesh.mesh.UpdateUVs();
			}
			linkedMeshes.Clear();
			int num16 = 0;
			for (int num17 = ReadVarint(input, optimizePositive: true); num16 < num17; num16++)
			{
				EventData eventData = new EventData(ReadString(input));
				eventData.Int = ReadVarint(input, optimizePositive: false);
				eventData.Float = ReadFloat(input);
				eventData.String = ReadString(input);
				skeletonData.events.Add(eventData);
			}
			int num18 = 0;
			for (int num19 = ReadVarint(input, optimizePositive: true); num18 < num19; num18++)
			{
				ReadAnimation(ReadString(input), input, skeletonData, skeletonData.animations);
			}
			skeletonData.bones.TrimExcess();
			skeletonData.slots.TrimExcess();
			skeletonData.skins.TrimExcess();
			skeletonData.events.TrimExcess();
			skeletonData.ikConstraints.TrimExcess();
			skeletonData.pathConstraints.TrimExcess();
			return skeletonData;
		}

		public SkeletonData ReadSkeletonDevisionCyspSkeleton(SimpleMemoryStream input)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			byte[] array = new byte[32];
			input.Read(array, 0, 32);
			BinaryHeader binaryHeader = default(BinaryHeader);
			binaryHeader.Init(array);
			if (!binaryHeader.CheckTag())
			{
				return null;
			}
			if (!binaryHeader.CheckVersion())
			{
				return null;
			}
			if (binaryHeader.bodyCount < 1)
			{
				return null;
			}
			input.Seek(binaryHeader.bodyCount * 32, SeekOrigin.Current);
			float scale = Scale;
			SkeletonData skeletonData = new SkeletonData();
			skeletonData.hash = ReadString(input);
			if (skeletonData.hash.Length == 0)
			{
				skeletonData.hash = null;
			}
			skeletonData.version = ReadString(input);
			if (skeletonData.version.Length == 0)
			{
				skeletonData.version = null;
			}
			skeletonData.width = ReadFloat(input);
			skeletonData.height = ReadFloat(input);
			bool flag = ReadBoolean(input);
			if (flag)
			{
				skeletonData.fps = ReadFloat(input);
				skeletonData.imagesPath = ReadString(input);
				if (skeletonData.imagesPath.Length == 0)
				{
					skeletonData.imagesPath = null;
				}
			}
			int i = 0;
			for (int num = ReadVarint(input, optimizePositive: true); i < num; i++)
			{
				string name = ReadString(input);
				BoneData parent = ((i == 0) ? null : skeletonData.bones.Items[ReadVarint(input, optimizePositive: true)]);
				BoneData boneData = new BoneData(i, name, parent);
				boneData.rotation = ReadFloat(input);
				boneData.x = ReadFloat(input) * scale;
				boneData.y = ReadFloat(input) * scale;
				boneData.scaleX = ReadFloat(input);
				boneData.scaleY = ReadFloat(input);
				boneData.shearX = ReadFloat(input);
				boneData.shearY = ReadFloat(input);
				boneData.length = ReadFloat(input) * scale;
				boneData.transformMode = TransformModeValues[ReadVarint(input, optimizePositive: true)];
				if (flag)
				{
					ReadInt(input);
				}
				skeletonData.bones.Add(boneData);
			}
			int j = 0;
			for (int num2 = ReadVarint(input, optimizePositive: true); j < num2; j++)
			{
				string name2 = ReadString(input);
				BoneData boneData2 = skeletonData.bones.Items[ReadVarint(input, optimizePositive: true)];
				SlotData slotData = new SlotData(j, name2, boneData2);
				int num3 = ReadInt(input);
				slotData.r = (float)((num3 & 0xFF000000u) >> 24) / 255f;
				slotData.g = (float)((num3 & 0xFF0000) >> 16) / 255f;
				slotData.b = (float)((num3 & 0xFF00) >> 8) / 255f;
				slotData.a = (float)(num3 & 0xFF) / 255f;
				int num4 = ReadInt(input);
				if (num4 != -1)
				{
					slotData.hasSecondColor = true;
					slotData.r2 = (float)((num4 & 0xFF0000) >> 16) / 255f;
					slotData.g2 = (float)((num4 & 0xFF00) >> 8) / 255f;
					slotData.b2 = (float)(num4 & 0xFF) / 255f;
				}
				slotData.attachmentName = ReadString(input);
				slotData.blendMode = (BlendMode)ReadVarint(input, optimizePositive: true);
				skeletonData.slots.Add(slotData);
			}
			int k = 0;
			for (int num5 = ReadVarint(input, optimizePositive: true); k < num5; k++)
			{
				IkConstraintData ikConstraintData = new IkConstraintData(ReadString(input));
				ikConstraintData.order = ReadVarint(input, optimizePositive: true);
				int l = 0;
				for (int num6 = ReadVarint(input, optimizePositive: true); l < num6; l++)
				{
					ikConstraintData.bones.Add(skeletonData.bones.Items[ReadVarint(input, optimizePositive: true)]);
				}
				ikConstraintData.target = skeletonData.bones.Items[ReadVarint(input, optimizePositive: true)];
				ikConstraintData.mix = ReadFloat(input);
				ikConstraintData.bendDirection = ReadSByte(input);
				skeletonData.ikConstraints.Add(ikConstraintData);
			}
			int m = 0;
			for (int num7 = ReadVarint(input, optimizePositive: true); m < num7; m++)
			{
				TransformConstraintData transformConstraintData = new TransformConstraintData(ReadString(input));
				transformConstraintData.order = ReadVarint(input, optimizePositive: true);
				int n = 0;
				for (int num8 = ReadVarint(input, optimizePositive: true); n < num8; n++)
				{
					transformConstraintData.bones.Add(skeletonData.bones.Items[ReadVarint(input, optimizePositive: true)]);
				}
				transformConstraintData.target = skeletonData.bones.Items[ReadVarint(input, optimizePositive: true)];
				transformConstraintData.local = ReadBoolean(input);
				transformConstraintData.relative = ReadBoolean(input);
				transformConstraintData.offsetRotation = ReadFloat(input);
				transformConstraintData.offsetX = ReadFloat(input) * scale;
				transformConstraintData.offsetY = ReadFloat(input) * scale;
				transformConstraintData.offsetScaleX = ReadFloat(input);
				transformConstraintData.offsetScaleY = ReadFloat(input);
				transformConstraintData.offsetShearY = ReadFloat(input);
				transformConstraintData.rotateMix = ReadFloat(input);
				transformConstraintData.translateMix = ReadFloat(input);
				transformConstraintData.scaleMix = ReadFloat(input);
				transformConstraintData.shearMix = ReadFloat(input);
				skeletonData.transformConstraints.Add(transformConstraintData);
			}
			int num9 = 0;
			for (int num10 = ReadVarint(input, optimizePositive: true); num9 < num10; num9++)
			{
				PathConstraintData pathConstraintData = new PathConstraintData(ReadString(input));
				pathConstraintData.order = ReadVarint(input, optimizePositive: true);
				int num11 = 0;
				for (int num12 = ReadVarint(input, optimizePositive: true); num11 < num12; num11++)
				{
					pathConstraintData.bones.Add(skeletonData.bones.Items[ReadVarint(input, optimizePositive: true)]);
				}
				pathConstraintData.target = skeletonData.slots.Items[ReadVarint(input, optimizePositive: true)];
				pathConstraintData.positionMode = (PositionMode)Enum.GetValues(typeof(PositionMode)).GetValue(ReadVarint(input, optimizePositive: true));
				pathConstraintData.spacingMode = (SpacingMode)Enum.GetValues(typeof(SpacingMode)).GetValue(ReadVarint(input, optimizePositive: true));
				pathConstraintData.rotateMode = (RotateMode)Enum.GetValues(typeof(RotateMode)).GetValue(ReadVarint(input, optimizePositive: true));
				pathConstraintData.offsetRotation = ReadFloat(input);
				pathConstraintData.position = ReadFloat(input);
				if (pathConstraintData.positionMode == PositionMode.Fixed)
				{
					pathConstraintData.position *= scale;
				}
				pathConstraintData.spacing = ReadFloat(input);
				if (pathConstraintData.spacingMode == SpacingMode.Length || pathConstraintData.spacingMode == SpacingMode.Fixed)
				{
					pathConstraintData.spacing *= scale;
				}
				pathConstraintData.rotateMix = ReadFloat(input);
				pathConstraintData.translateMix = ReadFloat(input);
				skeletonData.pathConstraints.Add(pathConstraintData);
			}
			Skin skin = ReadSkin(input, skeletonData, "default", flag);
			if (skin != null)
			{
				skeletonData.defaultSkin = skin;
				skeletonData.skins.Add(skin);
			}
			int num13 = 0;
			for (int num14 = ReadVarint(input, optimizePositive: true); num13 < num14; num13++)
			{
				skeletonData.skins.Add(ReadSkin(input, skeletonData, ReadString(input), flag));
			}
			int num15 = 0;
			for (int count = linkedMeshes.Count; num15 < count; num15++)
			{
				SkeletonJson.LinkedMesh linkedMesh = linkedMeshes[num15];
				Attachment attachment = (((linkedMesh.skin == null) ? skeletonData.DefaultSkin : skeletonData.FindSkin(linkedMesh.skin)) ?? throw new Exception("Skin not found: " + linkedMesh.skin)).GetAttachment(linkedMesh.slotIndex, linkedMesh.parent);
				if (attachment == null)
				{
					throw new Exception("Parent mesh not found: " + linkedMesh.parent);
				}
				linkedMesh.mesh.ParentMesh = (MeshAttachment)attachment;
				linkedMesh.mesh.UpdateUVs();
			}
			linkedMeshes.Clear();
			int num16 = 0;
			for (int num17 = ReadVarint(input, optimizePositive: true); num16 < num17; num16++)
			{
				EventData eventData = new EventData(ReadString(input));
				eventData.Int = ReadVarint(input, optimizePositive: false);
				eventData.Float = ReadFloat(input);
				eventData.String = ReadString(input);
				skeletonData.events.Add(eventData);
			}
			Dictionary<string, Animation> animations = skeletonData.animations;
			int num18 = 1;
			for (int bodyCount = binaryHeader.bodyCount; num18 < bodyCount; num18++)
			{
				ReadAnimation(ReadString(input), input, skeletonData, animations);
			}
			skeletonData.bones.TrimExcess();
			skeletonData.slots.TrimExcess();
			skeletonData.skins.TrimExcess();
			skeletonData.events.TrimExcess();
			skeletonData.ikConstraints.TrimExcess();
			skeletonData.pathConstraints.TrimExcess();
			return skeletonData;
		}

		public List<string> ReadSkeletonDevisionCyspAnimation(SimpleMemoryStream input, SkeletonData skeletonData)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input cannot be null.");
			}
			byte[] array = new byte[32];
			input.Read(array, 0, 32);
			BinaryHeader binaryHeader = default(BinaryHeader);
			binaryHeader.Init(array);
			if (!binaryHeader.CheckTag())
			{
				return null;
			}
			if (!binaryHeader.CheckVersion())
			{
				return null;
			}
			if (binaryHeader.bodyCount <= 0)
			{
				return null;
			}
			List<string> list = null;
			input.Seek(binaryHeader.bodyCount * 32, SeekOrigin.Current);
			Dictionary<string, Animation> animations = skeletonData.animations;
			list = new List<string>(binaryHeader.bodyCount);
			int i = 0;
			for (int bodyCount = binaryHeader.bodyCount; i < bodyCount; i++)
			{
				string text = ReadString(input);
				ReadAnimation(text, input, skeletonData, animations);
				list.Add(text);
			}
			return list;
		}

		private Skin ReadSkin(SimpleMemoryStream input, SkeletonData skeletonData, string skinName, bool nonessential)
		{
			int num = ReadVarint(input, optimizePositive: true);
			if (num == 0)
			{
				return null;
			}
			Skin skin = new Skin(skinName);
			for (int i = 0; i < num; i++)
			{
				int slotIndex = ReadVarint(input, optimizePositive: true);
				int j = 0;
				for (int num2 = ReadVarint(input, optimizePositive: true); j < num2; j++)
				{
					string text = ReadString(input);
					Attachment attachment = ReadAttachment(input, skeletonData, skin, slotIndex, text, nonessential);
					if (attachment != null)
					{
						skin.AddAttachment(slotIndex, text, attachment);
					}
				}
			}
			return skin;
		}

		private Attachment ReadAttachment(SimpleMemoryStream input, SkeletonData skeletonData, Skin skin, int slotIndex, string attachmentName, bool nonessential)
		{
			float scale = Scale;
			string text = ReadString(input);
			if (text == null)
			{
				text = attachmentName;
			}
			switch (input.ReadByte())
			{
			case 0:
			{
				string text2 = ReadString(input);
				float rotation = ReadFloat(input);
				float num3 = ReadFloat(input);
				float num4 = ReadFloat(input);
				float scaleX = ReadFloat(input);
				float scaleY = ReadFloat(input);
				float num5 = ReadFloat(input);
				float num6 = ReadFloat(input);
				int num7 = ReadInt(input);
				if (text2 == null)
				{
					text2 = text;
				}
				RegionAttachment regionAttachment = attachmentLoader.NewRegionAttachment(skin, text, text2);
				if (regionAttachment == null)
				{
					return null;
				}
				regionAttachment.Path = text2;
				regionAttachment.x = num3 * scale;
				regionAttachment.y = num4 * scale;
				regionAttachment.scaleX = scaleX;
				regionAttachment.scaleY = scaleY;
				regionAttachment.rotation = rotation;
				regionAttachment.width = num5 * scale;
				regionAttachment.height = num6 * scale;
				regionAttachment.r = (float)((num7 & 0xFF000000u) >> 24) / 255f;
				regionAttachment.g = (float)((num7 & 0xFF0000) >> 16) / 255f;
				regionAttachment.b = (float)((num7 & 0xFF00) >> 8) / 255f;
				regionAttachment.a = (float)(num7 & 0xFF) / 255f;
				regionAttachment.UpdateOffset();
				return regionAttachment;
			}
			case 1:
			{
				int num20 = ReadVarint(input, optimizePositive: true);
				Vertices vertices4 = ReadVertices(input, num20);
				if (nonessential)
				{
					ReadInt(input);
				}
				BoundingBoxAttachment boundingBoxAttachment = attachmentLoader.NewBoundingBoxAttachment(skin, text);
				if (boundingBoxAttachment == null)
				{
					return null;
				}
				boundingBoxAttachment.worldVerticesLength = num20 << 1;
				boundingBoxAttachment.vertices = vertices4.vertices;
				boundingBoxAttachment.bones = vertices4.bones;
				return boundingBoxAttachment;
			}
			case 2:
			{
				string text3 = ReadString(input);
				int num8 = ReadInt(input);
				int num9 = ReadVarint(input, optimizePositive: true);
				float[] regionUVs = ReadFloatArray(input, num9 << 1, 1f);
				int[] triangles = ReadShortArray(input);
				Vertices vertices2 = ReadVertices(input, num9);
				int num10 = ReadVarint(input, optimizePositive: true);
				int[] edges = null;
				float num11 = 0f;
				float num12 = 0f;
				if (nonessential)
				{
					edges = ReadShortArray(input);
					num11 = ReadFloat(input);
					num12 = ReadFloat(input);
				}
				if (text3 == null)
				{
					text3 = text;
				}
				MeshAttachment meshAttachment = attachmentLoader.NewMeshAttachment(skin, text, text3);
				if (meshAttachment == null)
				{
					return null;
				}
				meshAttachment.Path = text3;
				meshAttachment.r = (float)((num8 & 0xFF000000u) >> 24) / 255f;
				meshAttachment.g = (float)((num8 & 0xFF0000) >> 16) / 255f;
				meshAttachment.b = (float)((num8 & 0xFF00) >> 8) / 255f;
				meshAttachment.a = (float)(num8 & 0xFF) / 255f;
				meshAttachment.bones = vertices2.bones;
				meshAttachment.vertices = vertices2.vertices;
				meshAttachment.WorldVerticesLength = num9 << 1;
				meshAttachment.triangles = triangles;
				meshAttachment.regionUVs = regionUVs;
				meshAttachment.UpdateUVs();
				meshAttachment.HullLength = num10 << 1;
				if (nonessential)
				{
					meshAttachment.Edges = edges;
					meshAttachment.Width = num11 * scale;
					meshAttachment.Height = num12 * scale;
				}
				return meshAttachment;
			}
			case 3:
			{
				string text4 = ReadString(input);
				int num13 = ReadInt(input);
				string skin2 = ReadString(input);
				string parent = ReadString(input);
				bool inheritDeform = ReadBoolean(input);
				float num14 = 0f;
				float num15 = 0f;
				if (nonessential)
				{
					num14 = ReadFloat(input);
					num15 = ReadFloat(input);
				}
				if (text4 == null)
				{
					text4 = text;
				}
				MeshAttachment meshAttachment2 = attachmentLoader.NewMeshAttachment(skin, text, text4);
				if (meshAttachment2 == null)
				{
					return null;
				}
				meshAttachment2.Path = text4;
				meshAttachment2.r = (float)((num13 & 0xFF000000u) >> 24) / 255f;
				meshAttachment2.g = (float)((num13 & 0xFF0000) >> 16) / 255f;
				meshAttachment2.b = (float)((num13 & 0xFF00) >> 8) / 255f;
				meshAttachment2.a = (float)(num13 & 0xFF) / 255f;
				meshAttachment2.inheritDeform = inheritDeform;
				if (nonessential)
				{
					meshAttachment2.Width = num14 * scale;
					meshAttachment2.Height = num15 * scale;
				}
				linkedMeshes.Add(new SkeletonJson.LinkedMesh(meshAttachment2, skin2, slotIndex, parent));
				return meshAttachment2;
			}
			case 4:
			{
				bool closed = ReadBoolean(input);
				bool constantSpeed = ReadBoolean(input);
				int num16 = ReadVarint(input, optimizePositive: true);
				Vertices vertices3 = ReadVertices(input, num16);
				float[] array = new float[num16 / 3];
				int i = 0;
				for (int num17 = array.Length; i < num17; i++)
				{
					array[i] = ReadFloat(input) * scale;
				}
				if (nonessential)
				{
					ReadInt(input);
				}
				PathAttachment pathAttachment = attachmentLoader.NewPathAttachment(skin, text);
				if (pathAttachment == null)
				{
					return null;
				}
				pathAttachment.closed = closed;
				pathAttachment.constantSpeed = constantSpeed;
				pathAttachment.worldVerticesLength = num16 << 1;
				pathAttachment.vertices = vertices3.vertices;
				pathAttachment.bones = vertices3.bones;
				pathAttachment.lengths = array;
				return pathAttachment;
			}
			case 5:
			{
				float rotation2 = ReadFloat(input);
				float num18 = ReadFloat(input);
				float num19 = ReadFloat(input);
				if (nonessential)
				{
					ReadInt(input);
				}
				PointAttachment pointAttachment = attachmentLoader.NewPointAttachment(skin, text);
				if (pointAttachment == null)
				{
					return null;
				}
				pointAttachment.x = num18 * scale;
				pointAttachment.y = num19 * scale;
				pointAttachment.rotation = rotation2;
				return pointAttachment;
			}
			case 6:
			{
				int num = ReadVarint(input, optimizePositive: true);
				int num2 = ReadVarint(input, optimizePositive: true);
				Vertices vertices = ReadVertices(input, num2);
				if (nonessential)
				{
					ReadInt(input);
				}
				ClippingAttachment clippingAttachment = attachmentLoader.NewClippingAttachment(skin, text);
				if (clippingAttachment == null)
				{
					return null;
				}
				clippingAttachment.EndSlot = skeletonData.slots.Items[num];
				clippingAttachment.worldVerticesLength = num2 << 1;
				clippingAttachment.vertices = vertices.vertices;
				clippingAttachment.bones = vertices.bones;
				return clippingAttachment;
			}
			default:
				return null;
			}
		}

		private Vertices ReadVertices(SimpleMemoryStream input, int vertexCount)
		{
			float scale = Scale;
			int num = vertexCount << 1;
			Vertices vertices = new Vertices();
			if (!ReadBoolean(input))
			{
				vertices.vertices = ReadFloatArray(input, num, scale);
				return vertices;
			}
			ExposedList<float> exposedList = new ExposedList<float>(num * 3 * 3);
			ExposedList<int> exposedList2 = new ExposedList<int>(num * 3);
			for (int i = 0; i < vertexCount; i++)
			{
				int num2 = ReadVarint(input, optimizePositive: true);
				exposedList2.Add(num2);
				for (int j = 0; j < num2; j++)
				{
					exposedList2.Add(ReadVarint(input, optimizePositive: true));
					exposedList.Add(ReadFloat(input) * scale);
					exposedList.Add(ReadFloat(input) * scale);
					exposedList.Add(ReadFloat(input));
				}
			}
			vertices.vertices = exposedList.ToArray();
			vertices.bones = exposedList2.ToArray();
			return vertices;
		}

		private float[] ReadFloatArray(SimpleMemoryStream input, int n, float scale)
		{
			float[] array = new float[n];
			if (scale == 1f)
			{
				for (int i = 0; i < n; i++)
				{
					array[i] = ReadFloat(input);
				}
			}
			else
			{
				for (int j = 0; j < n; j++)
				{
					array[j] = ReadFloat(input) * scale;
				}
			}
			return array;
		}

		private int[] ReadShortArray(SimpleMemoryStream input)
		{
			int num = ReadVarint(input, optimizePositive: true);
			int[] array = new int[num];
			for (int i = 0; i < num; i++)
			{
				array[i] = (input.ReadByte() << 8) | input.ReadByte();
			}
			return array;
		}

		private void ReadAnimation(string name, SimpleMemoryStream input, SkeletonData skeletonData, Dictionary<string, Animation> animationTable)
		{
			ExposedList<Timeline> exposedList = new ExposedList<Timeline>();
			float scale = Scale;
			float num = 0f;
			int i = 0;
			for (int num2 = ReadVarint(input, optimizePositive: true); i < num2; i++)
			{
				int slotIndex = ReadVarint(input, optimizePositive: true);
				int j = 0;
				for (int num3 = ReadVarint(input, optimizePositive: true); j < num3; j++)
				{
					int num4 = input.ReadByte();
					int num5 = ReadVarint(input, optimizePositive: true);
					switch (num4)
					{
					case 0:
					{
						AttachmentTimeline attachmentTimeline = new AttachmentTimeline(num5, slotIndex);
						for (int m = 0; m < num5; m++)
						{
							attachmentTimeline.SetFrame(m, ReadFloat(input), ReadString(input));
						}
						exposedList.Add(attachmentTimeline);
						num = Math.Max(num, attachmentTimeline.frames[num5 - 1]);
						break;
					}
					case 1:
					{
						ColorTimeline colorTimeline = new ColorTimeline(num5, slotIndex);
						for (int l = 0; l < num5; l++)
						{
							float time2 = ReadFloat(input);
							int num8 = ReadInt(input);
							float r3 = (float)((num8 & 0xFF000000u) >> 24) / 255f;
							float g3 = (float)((num8 & 0xFF0000) >> 16) / 255f;
							float b3 = (float)((num8 & 0xFF00) >> 8) / 255f;
							float a2 = (float)(num8 & 0xFF) / 255f;
							colorTimeline.SetFrame(l, time2, r3, g3, b3, a2);
							if (l < num5 - 1)
							{
								ReadCurve(input, l, colorTimeline);
							}
						}
						exposedList.Add(colorTimeline);
						num = Math.Max(num, colorTimeline.frames[(colorTimeline.FrameCount - 1) * 5]);
						break;
					}
					case 2:
					{
						TwoColorTimeline twoColorTimeline = new TwoColorTimeline(num5, slotIndex);
						for (int k = 0; k < num5; k++)
						{
							float time = ReadFloat(input);
							int num6 = ReadInt(input);
							float r = (float)((num6 & 0xFF000000u) >> 24) / 255f;
							float g = (float)((num6 & 0xFF0000) >> 16) / 255f;
							float b = (float)((num6 & 0xFF00) >> 8) / 255f;
							float a = (float)(num6 & 0xFF) / 255f;
							int num7 = ReadInt(input);
							float r2 = (float)((num7 & 0xFF0000) >> 16) / 255f;
							float g2 = (float)((num7 & 0xFF00) >> 8) / 255f;
							float b2 = (float)(num7 & 0xFF) / 255f;
							twoColorTimeline.SetFrame(k, time, r, g, b, a, r2, g2, b2);
							if (k < num5 - 1)
							{
								ReadCurve(input, k, twoColorTimeline);
							}
						}
						exposedList.Add(twoColorTimeline);
						num = Math.Max(num, twoColorTimeline.frames[(twoColorTimeline.FrameCount - 1) * 8]);
						break;
					}
					}
				}
			}
			int n = 0;
			for (int num9 = ReadVarint(input, optimizePositive: true); n < num9; n++)
			{
				int boneIndex = ReadVarint(input, optimizePositive: true);
				int num10 = 0;
				for (int num11 = ReadVarint(input, optimizePositive: true); num10 < num11; num10++)
				{
					int num12 = input.ReadByte();
					int num13 = ReadVarint(input, optimizePositive: true);
					switch (num12)
					{
					case 0:
					{
						RotateTimeline rotateTimeline = new RotateTimeline(num13, boneIndex);
						for (int num16 = 0; num16 < num13; num16++)
						{
							rotateTimeline.SetFrame(num16, ReadFloat(input), ReadFloat(input));
							if (num16 < num13 - 1)
							{
								ReadCurve(input, num16, rotateTimeline);
							}
						}
						exposedList.Add(rotateTimeline);
						num = Math.Max(num, rotateTimeline.frames[(num13 - 1) * 2]);
						break;
					}
					case 1:
					case 2:
					case 3:
					{
						float num14 = 1f;
						TranslateTimeline translateTimeline;
						switch (num12)
						{
						case 2:
							translateTimeline = new ScaleTimeline(num13, boneIndex);
							break;
						case 3:
							translateTimeline = new ShearTimeline(num13, boneIndex);
							break;
						default:
							translateTimeline = new TranslateTimeline(num13, boneIndex);
							num14 = scale;
							break;
						}
						for (int num15 = 0; num15 < num13; num15++)
						{
							translateTimeline.SetFrame(num15, ReadFloat(input), ReadFloat(input) * num14, ReadFloat(input) * num14);
							if (num15 < num13 - 1)
							{
								ReadCurve(input, num15, translateTimeline);
							}
						}
						exposedList.Add(translateTimeline);
						num = Math.Max(num, translateTimeline.frames[(num13 - 1) * 3]);
						break;
					}
					}
				}
			}
			int num17 = 0;
			for (int num18 = ReadVarint(input, optimizePositive: true); num17 < num18; num17++)
			{
				int ikConstraintIndex = ReadVarint(input, optimizePositive: true);
				int num19 = ReadVarint(input, optimizePositive: true);
				IkConstraintTimeline ikConstraintTimeline = new IkConstraintTimeline(num19, ikConstraintIndex);
				for (int num20 = 0; num20 < num19; num20++)
				{
					ikConstraintTimeline.SetFrame(num20, ReadFloat(input), ReadFloat(input), ReadSByte(input));
					if (num20 < num19 - 1)
					{
						ReadCurve(input, num20, ikConstraintTimeline);
					}
				}
				exposedList.Add(ikConstraintTimeline);
				num = Math.Max(num, ikConstraintTimeline.frames[(num19 - 1) * 3]);
			}
			int num21 = 0;
			for (int num22 = ReadVarint(input, optimizePositive: true); num21 < num22; num21++)
			{
				int transformConstraintIndex = ReadVarint(input, optimizePositive: true);
				int num23 = ReadVarint(input, optimizePositive: true);
				TransformConstraintTimeline transformConstraintTimeline = new TransformConstraintTimeline(num23, transformConstraintIndex);
				for (int num24 = 0; num24 < num23; num24++)
				{
					transformConstraintTimeline.SetFrame(num24, ReadFloat(input), ReadFloat(input), ReadFloat(input), ReadFloat(input), ReadFloat(input));
					if (num24 < num23 - 1)
					{
						ReadCurve(input, num24, transformConstraintTimeline);
					}
				}
				exposedList.Add(transformConstraintTimeline);
				num = Math.Max(num, transformConstraintTimeline.frames[(num23 - 1) * 5]);
			}
			int num25 = 0;
			for (int num26 = ReadVarint(input, optimizePositive: true); num25 < num26; num25++)
			{
				int num27 = ReadVarint(input, optimizePositive: true);
				PathConstraintData pathConstraintData = skeletonData.pathConstraints.Items[num27];
				int num28 = 0;
				for (int num29 = ReadVarint(input, optimizePositive: true); num28 < num29; num28++)
				{
					int num30 = ReadSByte(input);
					int num31 = ReadVarint(input, optimizePositive: true);
					switch (num30)
					{
					case 0:
					case 1:
					{
						float num33 = 1f;
						PathConstraintPositionTimeline pathConstraintPositionTimeline;
						if (num30 == 1)
						{
							pathConstraintPositionTimeline = new PathConstraintSpacingTimeline(num31, num27);
							if (pathConstraintData.spacingMode == SpacingMode.Length || pathConstraintData.spacingMode == SpacingMode.Fixed)
							{
								num33 = scale;
							}
						}
						else
						{
							pathConstraintPositionTimeline = new PathConstraintPositionTimeline(num31, num27);
							if (pathConstraintData.positionMode == PositionMode.Fixed)
							{
								num33 = scale;
							}
						}
						for (int num34 = 0; num34 < num31; num34++)
						{
							pathConstraintPositionTimeline.SetFrame(num34, ReadFloat(input), ReadFloat(input) * num33);
							if (num34 < num31 - 1)
							{
								ReadCurve(input, num34, pathConstraintPositionTimeline);
							}
						}
						exposedList.Add(pathConstraintPositionTimeline);
						num = Math.Max(num, pathConstraintPositionTimeline.frames[(num31 - 1) * 2]);
						break;
					}
					case 2:
					{
						PathConstraintMixTimeline pathConstraintMixTimeline = new PathConstraintMixTimeline(num31, num27);
						for (int num32 = 0; num32 < num31; num32++)
						{
							pathConstraintMixTimeline.SetFrame(num32, ReadFloat(input), ReadFloat(input), ReadFloat(input));
							if (num32 < num31 - 1)
							{
								ReadCurve(input, num32, pathConstraintMixTimeline);
							}
						}
						exposedList.Add(pathConstraintMixTimeline);
						num = Math.Max(num, pathConstraintMixTimeline.frames[(num31 - 1) * 3]);
						break;
					}
					}
				}
			}
			int num35 = 0;
			for (int num36 = ReadVarint(input, optimizePositive: true); num35 < num36; num35++)
			{
				Skin skin = skeletonData.skins.Items[ReadVarint(input, optimizePositive: true)];
				int num37 = 0;
				for (int num38 = ReadVarint(input, optimizePositive: true); num37 < num38; num37++)
				{
					int slotIndex2 = ReadVarint(input, optimizePositive: true);
					int num39 = 0;
					for (int num40 = ReadVarint(input, optimizePositive: true); num39 < num40; num39++)
					{
						VertexAttachment vertexAttachment = (VertexAttachment)skin.GetAttachment(slotIndex2, ReadString(input));
						bool flag = vertexAttachment.bones != null;
						float[] vertices = vertexAttachment.vertices;
						int num41 = (flag ? (vertices.Length / 3 * 2) : vertices.Length);
						int num42 = ReadVarint(input, optimizePositive: true);
						DeformTimeline deformTimeline = new DeformTimeline(num42, slotIndex2, vertexAttachment);
						for (int num43 = 0; num43 < num42; num43++)
						{
							float time3 = ReadFloat(input);
							int num44 = ReadVarint(input, optimizePositive: true);
							float[] array;
							if (num44 == 0)
							{
								array = (flag ? new float[num41] : vertices);
							}
							else
							{
								array = new float[num41];
								int num45 = ReadVarint(input, optimizePositive: true);
								num44 += num45;
								if (scale == 1f)
								{
									for (int num46 = num45; num46 < num44; num46++)
									{
										array[num46] = ReadFloat(input);
									}
								}
								else
								{
									for (int num47 = num45; num47 < num44; num47++)
									{
										array[num47] = ReadFloat(input) * scale;
									}
								}
								if (!flag)
								{
									int num48 = 0;
									for (int num49 = array.Length; num48 < num49; num48++)
									{
										array[num48] += vertices[num48];
									}
								}
							}
							deformTimeline.SetFrame(num43, time3, array);
							if (num43 < num42 - 1)
							{
								ReadCurve(input, num43, deformTimeline);
							}
						}
						exposedList.Add(deformTimeline);
						num = Math.Max(num, deformTimeline.frames[num42 - 1]);
					}
				}
			}
			int num50 = ReadVarint(input, optimizePositive: true);
			if (num50 > 0)
			{
				DrawOrderTimeline drawOrderTimeline = new DrawOrderTimeline(num50);
				int count = skeletonData.slots.Count;
				for (int num51 = 0; num51 < num50; num51++)
				{
					float time4 = ReadFloat(input);
					int num52 = ReadVarint(input, optimizePositive: true);
					int[] array2 = new int[count];
					for (int num53 = count - 1; num53 >= 0; num53--)
					{
						array2[num53] = -1;
					}
					int[] array3 = new int[count - num52];
					int num54 = 0;
					int num55 = 0;
					for (int num56 = 0; num56 < num52; num56++)
					{
						int num57 = ReadVarint(input, optimizePositive: true);
						while (num54 != num57)
						{
							array3[num55++] = num54++;
						}
						array2[num54 + ReadVarint(input, optimizePositive: true)] = num54++;
					}
					while (num54 < count)
					{
						array3[num55++] = num54++;
					}
					for (int num58 = count - 1; num58 >= 0; num58--)
					{
						if (array2[num58] == -1)
						{
							array2[num58] = array3[--num55];
						}
					}
					drawOrderTimeline.SetFrame(num51, time4, array2);
				}
				exposedList.Add(drawOrderTimeline);
				num = Math.Max(num, drawOrderTimeline.frames[num50 - 1]);
			}
			int num59 = ReadVarint(input, optimizePositive: true);
			if (num59 > 0)
			{
				EventTimeline eventTimeline = new EventTimeline(num59);
				for (int num60 = 0; num60 < num59; num60++)
				{
					float time5 = ReadFloat(input);
					EventData eventData = skeletonData.events.Items[ReadVarint(input, optimizePositive: true)];
					Event @event = new Event(time5, eventData);
					@event.Int = ReadVarint(input, optimizePositive: false);
					@event.Float = ReadFloat(input);
					@event.String = (ReadBoolean(input) ? ReadString(input) : eventData.String);
					eventTimeline.SetFrame(num60, @event);
				}
				exposedList.Add(eventTimeline);
				num = Math.Max(num, eventTimeline.frames[num59 - 1]);
			}
			exposedList.TrimExcess();
			if (!animationTable.ContainsKey(name))
			{
				animationTable.Add(name, new Animation(name, exposedList, num));
			}
		}

		private void ReadCurve(SimpleMemoryStream input, int frameIndex, CurveTimeline timeline)
		{
			switch (input.ReadByte())
			{
			case 1:
				timeline.SetStepped(frameIndex);
				break;
			case 2:
				timeline.SetCurve(frameIndex, ReadFloat(input), ReadFloat(input), ReadFloat(input), ReadFloat(input));
				break;
			}
		}

		private static sbyte ReadSByte(SimpleMemoryStream input)
		{
			return (sbyte)input.ReadByte();
		}

		private static bool ReadBoolean(SimpleMemoryStream input)
		{
			return input.ReadByte() != 0;
		}

		private float ReadFloat(SimpleMemoryStream input)
		{
			buffer[3] = (byte)input.ReadByte();
			buffer[2] = (byte)input.ReadByte();
			buffer[1] = (byte)input.ReadByte();
			buffer[0] = (byte)input.ReadByte();
			return BitConverter.ToSingle(buffer, 0);
		}

		private static int ReadInt(SimpleMemoryStream input)
		{
			return (input.ReadByte() << 24) + (input.ReadByte() << 16) + (input.ReadByte() << 8) + input.ReadByte();
		}

		private static int ReadVarint(SimpleMemoryStream input, bool optimizePositive)
		{
			int num = input.ReadByte();
			int num2 = num & 0x7F;
			if (((uint)num & 0x80u) != 0)
			{
				num = input.ReadByte();
				num2 |= (num & 0x7F) << 7;
				if (((uint)num & 0x80u) != 0)
				{
					num = input.ReadByte();
					num2 |= (num & 0x7F) << 14;
					if (((uint)num & 0x80u) != 0)
					{
						num = input.ReadByte();
						num2 |= (num & 0x7F) << 21;
						if (((uint)num & 0x80u) != 0)
						{
							num2 |= (input.ReadByte() & 0x7F) << 28;
						}
					}
				}
			}
			if (!optimizePositive)
			{
				return (num2 >> 1) ^ -(num2 & 1);
			}
			return num2;
		}

		private string ReadString(SimpleMemoryStream input)
		{
			int num = ReadVarint(input, optimizePositive: true);
			switch (num)
			{
			case 0:
				return null;
			case 1:
				return "";
			default:
			{
				num--;
				byte[] array = buffer;
				if (array.Length < num)
				{
					array = new byte[num];
				}
				ReadFully(input, array, 0, num);
				return Encoding.UTF8.GetString(array, 0, num);
			}
			}
		}

		private static void ReadFully(SimpleMemoryStream input, byte[] buffer, int offset, int length)
		{
			while (length > 0)
			{
				int num = input.Read(buffer, offset, length);
				offset += num;
				length -= num;
			}
		}
	}
}
