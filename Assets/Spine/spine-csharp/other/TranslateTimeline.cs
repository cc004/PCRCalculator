namespace Spine
{
	public class TranslateTimeline : CurveTimeline
	{
		public const int ENTRIES = 3;

		protected const int PREV_TIME = -3;

		protected const int PREV_X = -2;

		protected const int PREV_Y = -1;

		protected const int X = 1;

		protected const int Y = 2;

		private int boneIndex;

		internal float[] frames;

		protected int propertyId;

		public int BoneIndex
		{
			get
			{
				return boneIndex;
			}
			set
			{
				boneIndex = value;
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

		public sealed override int PropertyId => propertyId;

		public TranslateTimeline(int frameCount, int _boneIndex)
			: base(frameCount)
		{
			frames = new float[frameCount * 3];
			boneIndex = _boneIndex;
			propertyId = 16777216 + boneIndex;
		}

		public void SetFrame(int frameIndex, float time, float x, float y)
		{
			frameIndex *= 3;
			frames[frameIndex] = time;
			frames[frameIndex + 1] = x;
			frames[frameIndex + 2] = y;
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
					bone.x = bone.data.x;
					bone.y = bone.data.y;
					break;
				case MixPose.Current:
					bone.x += (bone.data.x - bone.x) * alpha;
					bone.y += (bone.data.y - bone.y) * alpha;
					break;
				}
				return;
			}
			float num;
			float num2;
			if (time >= array[array.Length - 3])
			{
				num = array[array.Length + -2];
				num2 = array[array.Length + -1];
			}
			else
			{
				int num3 = Animation.BinarySearch(array, time, 3);
				num = array[num3 + -2];
				num2 = array[num3 + -1];
				float num4 = array[num3];
				float curvePercent = GetCurvePercent(num3 / 3 - 1, 1f - (time - num4) / (array[num3 + -3] - num4));
				num += (array[num3 + 1] - num) * curvePercent;
				num2 += (array[num3 + 2] - num2) * curvePercent;
			}
			if (pose == MixPose.Setup)
			{
				bone.x = bone.data.x + num * alpha;
				bone.y = bone.data.y + num2 * alpha;
			}
			else
			{
				bone.x += (bone.data.x + num - bone.x) * alpha;
				bone.y += (bone.data.y + num2 - bone.y) * alpha;
			}
		}
	}
}
