using System;
using UnityEngine;

namespace Spine
{
	public class TransformConstraint : IConstraint, IUpdatable
	{
		internal TransformConstraintData data;

		internal ExposedList<Bone> bones;

		internal Bone target;

		internal float rotateMix;

		internal float translateMix;

		internal float scaleMix;

		internal float shearMix;

		public TransformConstraintData Data => data;

		public int Order => data.order;

		public ExposedList<Bone> Bones => bones;

		public Bone Target
		{
			get
			{
				return target;
			}
			set
			{
				target = value;
			}
		}

		public float RotateMix
		{
			get
			{
				return rotateMix;
			}
			set
			{
				rotateMix = value;
			}
		}

		public float TranslateMix
		{
			get
			{
				return translateMix;
			}
			set
			{
				translateMix = value;
			}
		}

		public float ScaleMix
		{
			get
			{
				return scaleMix;
			}
			set
			{
				scaleMix = value;
			}
		}

		public float ShearMix
		{
			get
			{
				return shearMix;
			}
			set
			{
				shearMix = value;
			}
		}

		public TransformConstraint(TransformConstraintData data, Skeleton skeleton)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data", "data cannot be null.");
			}
			if (skeleton == null)
			{
				throw new ArgumentNullException("skeleton", "skeleton cannot be null.");
			}
			this.data = data;
			rotateMix = data.rotateMix;
			translateMix = data.translateMix;
			scaleMix = data.scaleMix;
			shearMix = data.shearMix;
			bones = new ExposedList<Bone>();
			foreach (BoneData bone in data.bones)
			{
				bones.Add(skeleton.FindBone(bone.name));
			}
			target = skeleton.FindBone(data.target.name);
		}

		public void Apply()
		{
			Update();
		}

		public void Update()
		{
			if (data.local)
			{
				if (data.relative)
				{
					ApplyRelativeLocal();
				}
				else
				{
					ApplyAbsoluteLocal();
				}
			}
			else if (data.relative)
			{
				ApplyRelativeWorld();
			}
			else
			{
				ApplyAbsoluteWorld();
			}
		}

		private void ApplyAbsoluteWorld()
		{
			float num = rotateMix;
			float num2 = translateMix;
			float num3 = scaleMix;
			float num4 = shearMix;
			Bone bone = target;
			float a = bone.a;
			float b = bone.b;
			float c = bone.c;
			float d = bone.d;
			float num5 = ((a * d - b * c > 0f) ? ((float)Math.PI / 180f) : (-(float)Math.PI / 180f));
			float num6 = data.offsetRotation * num5;
			float num7 = data.offsetShearY * num5;
			ExposedList<Bone> exposedList = bones;
			int i = 0;
			for (int count = exposedList.Count; i < count; i++)
			{
				Bone bone2 = exposedList.Items[i];
				bool flag = false;
				if (num != 0f)
				{
					float a2 = bone2.a;
					float b2 = bone2.b;
					float c2 = bone2.c;
					float d2 = bone2.d;
					float num8 = MathUtils.Atan2(c, a) - MathUtils.Atan2(c2, a2) + num6;
					if (num8 > (float)Math.PI)
					{
						num8 -= (float)Math.PI * 2f;
					}
					else if (num8 < -(float)Math.PI)
					{
						num8 += (float)Math.PI * 2f;
					}
					num8 *= num;
					float num9 = MathUtils.Cos(num8);
					float num10 = MathUtils.Sin(num8);
					bone2.a = num9 * a2 - num10 * c2;
					bone2.b = num9 * b2 - num10 * d2;
					bone2.c = num10 * a2 + num9 * c2;
					bone2.d = num10 * b2 + num9 * d2;
					flag = true;
				}
				if (num2 != 0f)
				{
					bone.LocalToWorld(data.offsetX, data.offsetY, out var worldX, out var worldY);
					bone2.worldX += (worldX - bone2.worldX) * num2;
					bone2.worldY += (worldY - bone2.worldY) * num2;
					flag = true;
				}
				if (num3 > 0f)
				{
					float num11 = Mathf.Sqrt(bone2.a * bone2.a + bone2.c * bone2.c);
					if (num11 > 1E-05f)
					{
						num11 = (num11 + (Mathf.Sqrt(a * a + c * c) - num11 + data.offsetScaleX) * num3) / num11;
					}
					bone2.a *= num11;
					bone2.c *= num11;
					num11 = Mathf.Sqrt(bone2.b * bone2.b + bone2.d * bone2.d);
					if (num11 > 1E-05f)
					{
						num11 = (num11 + (Mathf.Sqrt(b * b + d * d) - num11 + data.offsetScaleY) * num3) / num11;
					}
					bone2.b *= num11;
					bone2.d *= num11;
					flag = true;
				}
				if (num4 > 0f)
				{
					float b3 = bone2.b;
					float d3 = bone2.d;
					float num12 = MathUtils.Atan2(d3, b3);
					float num13 = MathUtils.Atan2(d, b) - MathUtils.Atan2(c, a) - (num12 - MathUtils.Atan2(bone2.c, bone2.a));
					if (num13 > (float)Math.PI)
					{
						num13 -= (float)Math.PI * 2f;
					}
					else if (num13 < -(float)Math.PI)
					{
						num13 += (float)Math.PI * 2f;
					}
					num13 = num12 + (num13 + num7) * num4;
					float num14 = Mathf.Sqrt(b3 * b3 + d3 * d3);
					bone2.b = MathUtils.Cos(num13) * num14;
					bone2.d = MathUtils.Sin(num13) * num14;
					flag = true;
				}
				if (flag)
				{
					bone2.appliedValid = false;
				}
			}
		}

		private void ApplyRelativeWorld()
		{
			float num = rotateMix;
			float num2 = translateMix;
			float num3 = scaleMix;
			float num4 = shearMix;
			Bone bone = target;
			float a = bone.a;
			float b = bone.b;
			float c = bone.c;
			float d = bone.d;
			float num5 = ((a * d - b * c > 0f) ? ((float)Math.PI / 180f) : (-(float)Math.PI / 180f));
			float num6 = data.offsetRotation * num5;
			float num7 = data.offsetShearY * num5;
			ExposedList<Bone> exposedList = bones;
			int i = 0;
			for (int count = exposedList.Count; i < count; i++)
			{
				Bone bone2 = exposedList.Items[i];
				bool flag = false;
				if (num != 0f)
				{
					float a2 = bone2.a;
					float b2 = bone2.b;
					float c2 = bone2.c;
					float d2 = bone2.d;
					float num8 = MathUtils.Atan2(c, a) + num6;
					if (num8 > (float)Math.PI)
					{
						num8 -= (float)Math.PI * 2f;
					}
					else if (num8 < -(float)Math.PI)
					{
						num8 += (float)Math.PI * 2f;
					}
					num8 *= num;
					float num9 = MathUtils.Cos(num8);
					float num10 = MathUtils.Sin(num8);
					bone2.a = num9 * a2 - num10 * c2;
					bone2.b = num9 * b2 - num10 * d2;
					bone2.c = num10 * a2 + num9 * c2;
					bone2.d = num10 * b2 + num9 * d2;
					flag = true;
				}
				if (num2 != 0f)
				{
					bone.LocalToWorld(data.offsetX, data.offsetY, out var worldX, out var worldY);
					bone2.worldX += worldX * num2;
					bone2.worldY += worldY * num2;
					flag = true;
				}
				if (num3 > 0f)
				{
					float num11 = (Mathf.Sqrt(a * a + c * c) - 1f + data.offsetScaleX) * num3 + 1f;
					bone2.a *= num11;
					bone2.c *= num11;
					num11 = (Mathf.Sqrt(b * b + d * d) - 1f + data.offsetScaleY) * num3 + 1f;
					bone2.b *= num11;
					bone2.d *= num11;
					flag = true;
				}
				if (num4 > 0f)
				{
					float num12 = MathUtils.Atan2(d, b) - MathUtils.Atan2(c, a);
					if (num12 > (float)Math.PI)
					{
						num12 -= (float)Math.PI * 2f;
					}
					else if (num12 < -(float)Math.PI)
					{
						num12 += (float)Math.PI * 2f;
					}
					float b3 = bone2.b;
					float d3 = bone2.d;
					num12 = MathUtils.Atan2(d3, b3) + (num12 - (float)Math.PI / 2f + num7) * num4;
					float num13 = Mathf.Sqrt(b3 * b3 + d3 * d3);
					bone2.b = MathUtils.Cos(num12) * num13;
					bone2.d = MathUtils.Sin(num12) * num13;
					flag = true;
				}
				if (flag)
				{
					bone2.appliedValid = false;
				}
			}
		}

		private void ApplyAbsoluteLocal()
		{
			float num = rotateMix;
			float num2 = translateMix;
			float num3 = scaleMix;
			float num4 = shearMix;
			Bone bone = target;
			if (!bone.appliedValid)
			{
				bone.UpdateAppliedTransform();
			}
			Bone[] items = bones.Items;
			int i = 0;
			for (int count = bones.Count; i < count; i++)
			{
				Bone bone2 = items[i];
				if (!bone2.appliedValid)
				{
					bone2.UpdateAppliedTransform();
				}
				float num5 = bone2.arotation;
				if (num != 0f)
				{
					float num6 = bone.arotation - num5 + data.offsetRotation;
					num6 -= (float)((16384 - (int)(16384.499999999996 - (double)(num6 / 360f))) * 360);
					num5 += num6 * num;
				}
				float num7 = bone2.ax;
				float num8 = bone2.ay;
				if (num2 != 0f)
				{
					num7 += (bone.ax - num7 + data.offsetX) * num2;
					num8 += (bone.ay - num8 + data.offsetY) * num2;
				}
				float num9 = bone2.ascaleX;
				float num10 = bone2.ascaleY;
				if (num3 > 0f)
				{
					if (num9 > 1E-05f)
					{
						num9 = (num9 + (bone.ascaleX - num9 + data.offsetScaleX) * num3) / num9;
					}
					if (num10 > 1E-05f)
					{
						num10 = (num10 + (bone.ascaleY - num10 + data.offsetScaleY) * num3) / num10;
					}
				}
				float ashearY = bone2.ashearY;
				if (num4 > 0f)
				{
					float num11 = bone.ashearY - ashearY + data.offsetShearY;
					num11 -= (float)((16384 - (int)(16384.499999999996 - (double)(num11 / 360f))) * 360);
					bone2.shearY += num11 * num4;
				}
				bone2.UpdateWorldTransform(num7, num8, num5, num9, num10, bone2.ashearX, ashearY);
			}
		}

		private void ApplyRelativeLocal()
		{
			float num = rotateMix;
			float num2 = translateMix;
			float num3 = scaleMix;
			float num4 = shearMix;
			Bone bone = target;
			if (!bone.appliedValid)
			{
				bone.UpdateAppliedTransform();
			}
			Bone[] items = bones.Items;
			int i = 0;
			for (int count = bones.Count; i < count; i++)
			{
				Bone bone2 = items[i];
				if (!bone2.appliedValid)
				{
					bone2.UpdateAppliedTransform();
				}
				float num5 = bone2.arotation;
				if (num != 0f)
				{
					num5 += (bone.arotation + data.offsetRotation) * num;
				}
				float num6 = bone2.ax;
				float num7 = bone2.ay;
				if (num2 != 0f)
				{
					num6 += (bone.ax + data.offsetX) * num2;
					num7 += (bone.ay + data.offsetY) * num2;
				}
				float num8 = bone2.ascaleX;
				float num9 = bone2.ascaleY;
				if (num3 > 0f)
				{
					if (num8 > 1E-05f)
					{
						num8 *= (bone.ascaleX - 1f + data.offsetScaleX) * num3 + 1f;
					}
					if (num9 > 1E-05f)
					{
						num9 *= (bone.ascaleY - 1f + data.offsetScaleY) * num3 + 1f;
					}
				}
				float num10 = bone2.ashearY;
				if (num4 > 0f)
				{
					num10 += (bone.ashearY + data.offsetShearY) * num4;
				}
				bone2.UpdateWorldTransform(num6, num7, num5, num8, num9, bone2.ashearX, num10);
			}
		}

		public override string ToString()
		{
			return data.name;
		}
	}
}
