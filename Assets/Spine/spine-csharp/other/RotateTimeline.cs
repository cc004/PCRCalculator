namespace Spine
{
	public class RotateTimeline : CurveTimeline
	{
		public const int ENTRIES = 2;

		internal const int PREV_TIME = -2;

		internal const int PREV_ROTATION = -1;

		internal const int ROTATION = 1;

		private int boneIndex;

		internal float[] frames;

		private int propertyId;

		public int BoneIndex => boneIndex;

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

		public override int PropertyId => propertyId;

		public RotateTimeline(int frameCount, int _boneIndex)
			: base(frameCount)
		{
			frames = new float[frameCount << 1];
			boneIndex = _boneIndex;
			propertyId = boneIndex;
		}

		public void SetFrame(int frameIndex, float time, float degrees)
		{
			frameIndex <<= 1;
			frames[frameIndex] = time;
			frames[frameIndex + 1] = degrees;
		}

		public override void Apply(Skeleton skeleton, float lastTime, float time, ExposedList<Event> firedEvents, float alpha, MixPose pose, MixDirection direction)
		{
			Bone bone = skeleton.bones.Items[boneIndex];
			float[] array = frames;
			if (time < array[0])
			{
				switch (pose)
				{
				case MixPose.Setup:
					bone.rotation = bone.data.rotation;
					break;
				case MixPose.Current:
				{
					float num = bone.data.rotation - bone.rotation;
					num -= (16384 - (int)(16384.499999999996 - num / 360f)) * 360;
					bone.rotation += num * alpha;
					break;
				}
				}
				return;
			}
			if (time >= array[array.Length - 2])
			{
				if (pose == MixPose.Setup)
				{
					bone.rotation = bone.data.rotation + array[array.Length + -1] * alpha;
					return;
				}
				float num2 = bone.data.rotation + array[array.Length + -1] - bone.rotation;
				num2 -= (16384 - (int)(16384.499999999996 - num2 / 360f)) * 360;
				bone.rotation += num2 * alpha;
				return;
			}
			int num3 = Animation.BinarySearch(array, time, 2);
			float num4 = array[num3 + -1];
			float num5 = array[num3];
			float curvePercent = GetCurvePercent((num3 >> 1) - 1, 1f - (time - num5) / (array[num3 + -2] - num5));
			float num6 = array[num3 + 1] - num4;
			num6 -= (16384 - (int)(16384.499999999996 - num6 / 360f)) * 360;
			num6 = num4 + num6 * curvePercent;
			if (pose == MixPose.Setup)
			{
				num6 -= (16384 - (int)(16384.499999999996 - num6 / 360f)) * 360;
				bone.rotation = bone.data.rotation + num6 * alpha;
			}
			else
			{
				num6 = bone.data.rotation + num6 - bone.rotation;
				num6 -= (16384 - (int)(16384.499999999996 - num6 / 360f)) * 360;
				bone.rotation += num6 * alpha;
			}
		}
	}
}
