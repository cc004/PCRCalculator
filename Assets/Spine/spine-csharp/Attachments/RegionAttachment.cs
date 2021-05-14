namespace Spine
{
	public class RegionAttachment : Attachment
	{
		public const int BLX = 0;

		public const int BLY = 1;

		public const int ULX = 2;

		public const int ULY = 3;

		public const int URX = 4;

		public const int URY = 5;

		public const int BRX = 6;

		public const int BRY = 7;

		public const int X1 = 0;

		public const int Y1 = 1;

		public const int X2 = 2;

		public const int Y2 = 3;

		public const int X3 = 4;

		public const int Y3 = 5;

		public const int X4 = 6;

		public const int Y4 = 7;

		internal float x;

		internal float y;

		internal float rotation;

		internal float scaleX = 1f;

		internal float scaleY = 1f;

		internal float width;

		internal float height;

		internal float regionOffsetX;

		internal float regionOffsetY;

		internal float regionWidth;

		internal float regionHeight;

		internal float regionOriginalWidth;

		internal float regionOriginalHeight;

		internal float[] offset = new float[8];

		internal float[] uvs = new float[8];

		internal float r = 1f;

		internal float g = 1f;

		internal float b = 1f;

		internal float a = 1f;

		public object RendererObject;

		public float X
		{
			get
			{
				return x;
			}
			set
			{
				x = value;
			}
		}

		public float Y
		{
			get
			{
				return y;
			}
			set
			{
				y = value;
			}
		}

		public float Rotation
		{
			get
			{
				return rotation;
			}
			set
			{
				rotation = value;
			}
		}

		public float ScaleX
		{
			get
			{
				return scaleX;
			}
			set
			{
				scaleX = value;
			}
		}

		public float ScaleY
		{
			get
			{
				return scaleY;
			}
			set
			{
				scaleY = value;
			}
		}

		public float Width
		{
			get
			{
				return width;
			}
			set
			{
				width = value;
			}
		}

		public float Height
		{
			get
			{
				return height;
			}
			set
			{
				height = value;
			}
		}

		public float R
		{
			get
			{
				return r;
			}
			set
			{
				r = value;
			}
		}

		public float G
		{
			get
			{
				return g;
			}
			set
			{
				g = value;
			}
		}

		public float B
		{
			get
			{
				return b;
			}
			set
			{
				b = value;
			}
		}

		public float A
		{
			get
			{
				return a;
			}
			set
			{
				a = value;
			}
		}

		public string Path
		{
			get;
			set;
		}

		public float RegionOffsetX
		{
			get
			{
				return regionOffsetX;
			}
			set
			{
				regionOffsetX = value;
			}
		}

		public float RegionOffsetY
		{
			get
			{
				return regionOffsetY;
			}
			set
			{
				regionOffsetY = value;
			}
		}

		public float RegionWidth
		{
			get
			{
				return regionWidth;
			}
			set
			{
				regionWidth = value;
			}
		}

		public float RegionHeight
		{
			get
			{
				return regionHeight;
			}
			set
			{
				regionHeight = value;
			}
		}

		public float RegionOriginalWidth
		{
			get
			{
				return regionOriginalWidth;
			}
			set
			{
				regionOriginalWidth = value;
			}
		}

		public float RegionOriginalHeight
		{
			get
			{
				return regionOriginalHeight;
			}
			set
			{
				regionOriginalHeight = value;
			}
		}

		public float[] Offset => offset;

		public float[] UVs => uvs;

		public RegionAttachment(string name)
			: base(name)
		{
		}

		public void UpdateOffset()
		{
			float num = width;
			float num2 = height;
			float num3 = scaleX;
			float num4 = scaleY;
			float num5 = num / regionOriginalWidth * num3;
			float num6 = num2 / regionOriginalHeight * num4;
			float num7 = (0f - num) / 2f * num3 + regionOffsetX * num5;
			float num8 = (0f - num2) / 2f * num4 + regionOffsetY * num6;
			float num9 = num7 + regionWidth * num5;
			float num10 = num8 + regionHeight * num6;
			float degrees = rotation;
			float num11 = MathUtils.CosDeg(degrees);
			float num12 = MathUtils.SinDeg(degrees);
			float num13 = x;
			float num14 = y;
			float num15 = num7 * num11 + num13;
			float num16 = num7 * num12;
			float num17 = num8 * num11 + num14;
			float num18 = num8 * num12;
			float num19 = num9 * num11 + num13;
			float num20 = num9 * num12;
			float num21 = num10 * num11 + num14;
			float num22 = num10 * num12;
			float[] array = offset;
			array[0] = num15 - num18;
			array[1] = num17 + num16;
			array[2] = num15 - num22;
			array[3] = num21 + num16;
			array[4] = num19 - num22;
			array[5] = num21 + num20;
			array[6] = num19 - num18;
			array[7] = num17 + num20;
		}

		public void SetUVs(float u, float v, float u2, float v2, bool rotate)
		{
			float[] array = uvs;
			if (rotate)
			{
				array[4] = u;
				array[5] = v2;
				array[6] = u;
				array[7] = v;
				array[0] = u2;
				array[1] = v;
				array[2] = u2;
				array[3] = v2;
			}
			else
			{
				array[2] = u;
				array[3] = v2;
				array[4] = u;
				array[5] = v;
				array[6] = u2;
				array[7] = v;
				array[0] = u2;
				array[1] = v2;
			}
		}

		public void ComputeWorldVertices(Bone bone, float[] worldVertices, int offset, int stride = 2)
		{
			float[] array = this.offset;
			float worldX = bone.worldX;
			float worldY = bone.worldY;
			float num = bone.a;
			float num2 = bone.b;
			float c = bone.c;
			float d = bone.d;
			float num3 = array[6];
			float num4 = array[7];
			worldVertices[offset] = num3 * num + num4 * num2 + worldX;
			worldVertices[offset + 1] = num3 * c + num4 * d + worldY;
			offset += stride;
			num3 = array[0];
			num4 = array[1];
			worldVertices[offset] = num3 * num + num4 * num2 + worldX;
			worldVertices[offset + 1] = num3 * c + num4 * d + worldY;
			offset += stride;
			num3 = array[2];
			num4 = array[3];
			worldVertices[offset] = num3 * num + num4 * num2 + worldX;
			worldVertices[offset + 1] = num3 * c + num4 * d + worldY;
			offset += stride;
			num3 = array[4];
			num4 = array[5];
			worldVertices[offset] = num3 * num + num4 * num2 + worldX;
			worldVertices[offset + 1] = num3 * c + num4 * d + worldY;
		}
	}
}
