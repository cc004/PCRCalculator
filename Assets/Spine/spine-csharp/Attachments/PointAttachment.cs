using System;

namespace Spine
{
	public class PointAttachment : Attachment
	{
		internal float x;

		internal float y;

		internal float rotation;

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

		public PointAttachment(string name)
			: base(name)
		{
		}

		public void ComputeWorldPosition(Bone bone, out float ox, out float oy)
		{
			bone.LocalToWorld(x, y, out ox, out oy);
		}

		public float ComputeWorldRotation(Bone bone)
		{
			float num = MathUtils.CosDeg(rotation);
			float num2 = MathUtils.SinDeg(rotation);
			return MathUtils.Atan2(x: num * bone.a + num2 * bone.b, y: num * bone.c + num2 * bone.d) * (180f / (float)Math.PI);
		}
	}
}
