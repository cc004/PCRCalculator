using System;
using UnityEngine;

namespace Spine
{
	public class IkConstraint : IConstraint, IUpdatable
	{
		internal IkConstraintData data;

		internal ExposedList<Bone> bones = new ExposedList<Bone>();

		internal Bone target;

		internal float mix;

		internal int bendDirection;

		public IkConstraintData Data => data;

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

		public int BendDirection
		{
			get
			{
				return bendDirection;
			}
			set
			{
				bendDirection = value;
			}
		}

		public float Mix
		{
			get
			{
				return mix;
			}
			set
			{
				mix = value;
			}
		}

		public IkConstraint(IkConstraintData data, Skeleton skeleton)
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
			mix = data.mix;
			bendDirection = data.bendDirection;
			bones = new ExposedList<Bone>(data.bones.Count);
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
			Bone bone = target;
			ExposedList<Bone> exposedList = bones;
			switch (exposedList.Count)
			{
			case 1:
				Apply(exposedList.Items[0], bone.worldX, bone.worldY, mix);
				break;
			case 2:
				Apply(exposedList.Items[0], exposedList.Items[1], bone.worldX, bone.worldY, bendDirection, mix);
				break;
			}
		}

		public override string ToString()
		{
			return data.name;
		}

		public static void Apply(Bone bone, float targetX, float targetY, float alpha)
		{
			if (!bone.appliedValid)
			{
				bone.UpdateAppliedTransform();
			}
			Bone parent = bone.parent;
			float num = 1f / (parent.a * parent.d - parent.b * parent.c);
			float num2 = targetX - parent.worldX;
			float num3 = targetY - parent.worldY;
			float num4 = (num2 * parent.d - num3 * parent.b) * num - bone.ax;
			float num5 = (float)Math.Atan2((num3 * parent.a - num2 * parent.c) * num - bone.ay, num4) * (180f / (float)Math.PI) - bone.ashearX - bone.arotation;
			if (bone.ascaleX < 0f)
			{
				num5 += 180f;
			}
			if (num5 > 180f)
			{
				num5 -= 360f;
			}
			else if (num5 < -180f)
			{
				num5 += 360f;
			}
			bone.UpdateWorldTransform(bone.ax, bone.ay, bone.arotation + num5 * alpha, bone.ascaleX, bone.ascaleY, bone.ashearX, bone.ashearY);
		}

		public static void Apply(Bone parent, Bone child, float targetX, float targetY, int bendDir, float alpha)
		{
			if (alpha == 0f)
			{
				child.UpdateWorldTransform();
				return;
			}
			if (!parent.appliedValid)
			{
				parent.UpdateAppliedTransform();
			}
			if (!child.appliedValid)
			{
				child.UpdateAppliedTransform();
			}
			float ax = parent.ax;
			float ay = parent.ay;
			float num = parent.ascaleX;
			float num2 = parent.ascaleY;
			float num3 = child.ascaleX;
			int num4;
			int num5;
			if (num < 0f)
			{
				num = 0f - num;
				num4 = 180;
				num5 = -1;
			}
			else
			{
				num4 = 0;
				num5 = 1;
			}
			if (num2 < 0f)
			{
				num2 = 0f - num2;
				num5 = -num5;
			}
			int num6;
			if (num3 < 0f)
			{
				num3 = 0f - num3;
				num6 = 180;
			}
			else
			{
				num6 = 0;
			}
			float ax2 = child.ax;
			float a = parent.a;
			float b = parent.b;
			float c = parent.c;
			float d = parent.d;
			bool num7 = Math.Abs(num - num2) <= 0.0001f;
			float num8;
			float num9;
			float num10;
			if (!num7)
			{
				num8 = 0f;
				num9 = a * ax2 + parent.worldX;
				num10 = c * ax2 + parent.worldY;
			}
			else
			{
				num8 = child.ay;
				num9 = a * ax2 + b * num8 + parent.worldX;
				num10 = c * ax2 + d * num8 + parent.worldY;
			}
			Bone parent2 = parent.parent;
			a = parent2.a;
			b = parent2.b;
			c = parent2.c;
			d = parent2.d;
			float num11 = 1f / (a * d - b * c);
			float num12 = targetX - parent2.worldX;
			float num13 = targetY - parent2.worldY;
			float num14 = (num12 * d - num13 * b) * num11 - ax;
			float num15 = (num13 * a - num12 * c) * num11 - ay;
			num12 = num9 - parent2.worldX;
			num13 = num10 - parent2.worldY;
			float num16 = (num12 * d - num13 * b) * num11 - ax;
			float num17 = (num13 * a - num12 * c) * num11 - ay;
			float num18 = Mathf.Sqrt(num16 * num16 + num17 * num17);
			float num19 = child.data.length * num3;
			float num22;
			float num21;
			if (num7)
			{
				num19 *= num;
				float num20 = (num14 * num14 + num15 * num15 - num18 * num18 - num19 * num19) / (2f * num18 * num19);
				if (num20 < -1f)
				{
					num20 = -1f;
				}
				else if (num20 > 1f)
				{
					num20 = 1f;
				}
				num21 = (float)Math.Acos(num20) * (float)bendDir;
				a = num18 + num19 * num20;
				b = num19 * (float)Math.Sin(num21);
				num22 = (float)Math.Atan2(num15 * a - num14 * b, num14 * a + num15 * b);
			}
			else
			{
				a = num * num19;
				b = num2 * num19;
				float num23 = a * a;
				float num24 = b * b;
				float num25 = num14 * num14 + num15 * num15;
				float num26 = (float)Math.Atan2(num15, num14);
				c = num24 * num18 * num18 + num23 * num25 - num23 * num24;
				float num27 = -2f * num24 * num18;
				float num28 = num24 - num23;
				d = num27 * num27 - 4f * num28 * c;
				if (d >= 0f)
				{
					float num29 = Mathf.Sqrt(d);
					if (num27 < 0f)
					{
						num29 = 0f - num29;
					}
					num29 = (0f - (num27 + num29)) / 2f;
					float num30 = num29 / num28;
					float num31 = c / num29;
					float num32 = ((Math.Abs(num30) < Math.Abs(num31)) ? num30 : num31);
					if (num32 * num32 <= num25)
					{
						num13 = Mathf.Sqrt(num25 - num32 * num32) * (float)bendDir;
						num22 = num26 - (float)Math.Atan2(num13, num32);
						num21 = (float)Math.Atan2(num13 / num2, (num32 - num18) / num);
						goto IL_04af;
					}
				}
				float num33 = (float)Math.PI;
				float num34 = num18 - a;
				float num35 = num34 * num34;
				float num36 = 0f;
				float num37 = 0f;
				float num38 = num18 + a;
				float num39 = num38 * num38;
				float num40 = 0f;
				c = (0f - a) * num18 / (num23 - num24);
				if (c >= -1f && c <= 1f)
				{
					c = (float)Math.Acos(c);
					num12 = a * (float)Math.Cos(c) + num18;
					num13 = b * (float)Math.Sin(c);
					d = num12 * num12 + num13 * num13;
					if (d < num35)
					{
						num33 = c;
						num35 = d;
						num34 = num12;
						num36 = num13;
					}
					if (d > num39)
					{
						num37 = c;
						num39 = d;
						num38 = num12;
						num40 = num13;
					}
				}
				if (num25 <= (num35 + num39) / 2f)
				{
					num22 = num26 - (float)Math.Atan2(num36 * (float)bendDir, num34);
					num21 = num33 * (float)bendDir;
				}
				else
				{
					num22 = num26 - (float)Math.Atan2(num40 * (float)bendDir, num38);
					num21 = num37 * (float)bendDir;
				}
			}
			goto IL_04af;
			IL_04af:
			float num41 = (float)Math.Atan2(num8, ax2) * (float)num5;
			float arotation = parent.arotation;
			num22 = (num22 - num41) * (180f / (float)Math.PI) + (float)num4 - arotation;
			if (num22 > 180f)
			{
				num22 -= 360f;
			}
			else if (num22 < -180f)
			{
				num22 += 360f;
			}
			parent.UpdateWorldTransform(ax, ay, arotation + num22 * alpha, parent.scaleX, parent.ascaleY, 0f, 0f);
			arotation = child.arotation;
			num21 = ((num21 + num41) * (180f / (float)Math.PI) - child.ashearX) * (float)num5 + (float)num6 - arotation;
			if (num21 > 180f)
			{
				num21 -= 360f;
			}
			else if (num21 < -180f)
			{
				num21 += 360f;
			}
			child.UpdateWorldTransform(ax2, num8, arotation + num21 * alpha, child.ascaleX, child.ascaleY, child.ashearX, child.ashearY);
		}
	}
}
