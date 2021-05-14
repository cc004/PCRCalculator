namespace Spine
{
	public class IkConstraintTimeline : CurveTimeline
	{
		public const int ENTRIES = 3;

		private const int PREV_TIME = -3;

		private const int PREV_MIX = -2;

		private const int PREV_BEND_DIRECTION = -1;

		private const int MIX = 1;

		private const int BEND_DIRECTION = 2;

		private int ikConstraintIndex;

		internal float[] frames;

		private int propertyId;

		public int IkConstraintIndex
		{
			get
			{
				return ikConstraintIndex;
			}
			set
			{
				ikConstraintIndex = value;
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

		public override int PropertyId => propertyId;

		public IkConstraintTimeline(int frameCount, int _ikConstraintIndex)
			: base(frameCount)
		{
			frames = new float[frameCount * 3];
			ikConstraintIndex = _ikConstraintIndex;
			propertyId = 150994944 + ikConstraintIndex;
		}

		public void SetFrame(int frameIndex, float time, float mix, int bendDirection)
		{
			frameIndex *= 3;
			frames[frameIndex] = time;
			frames[frameIndex + 1] = mix;
			frames[frameIndex + 2] = bendDirection;
		}

		public override void Apply(Skeleton skeleton, float lastTime, float time, ExposedList<Event> firedEvents, float alpha, MixPose pose, MixDirection direction)
		{
			IkConstraint ikConstraint = skeleton.ikConstraints.Items[ikConstraintIndex];
			float[] array = frames;
			if (time < array[0])
			{
				switch (pose)
				{
				case MixPose.Setup:
					ikConstraint.mix = ikConstraint.data.mix;
					ikConstraint.bendDirection = ikConstraint.data.bendDirection;
					break;
				case MixPose.Current:
					ikConstraint.mix += (ikConstraint.data.mix - ikConstraint.mix) * alpha;
					ikConstraint.bendDirection = ikConstraint.data.bendDirection;
					break;
				}
				return;
			}
			if (time >= array[array.Length - 3])
			{
				if (pose == MixPose.Setup)
				{
					ikConstraint.mix = ikConstraint.data.mix + (array[array.Length + -2] - ikConstraint.data.mix) * alpha;
					ikConstraint.bendDirection = ((direction == MixDirection.Out) ? ikConstraint.data.bendDirection : ((int)array[array.Length + -1]));
					return;
				}
				ikConstraint.mix += (array[array.Length + -2] - ikConstraint.mix) * alpha;
				if (direction == MixDirection.In)
				{
					ikConstraint.bendDirection = (int)array[array.Length + -1];
				}
				return;
			}
			int num = Animation.BinarySearch(array, time, 3);
			float num2 = array[num + -2];
			float num3 = array[num];
			float curvePercent = GetCurvePercent(num / 3 - 1, 1f - (time - num3) / (array[num + -3] - num3));
			if (pose == MixPose.Setup)
			{
				ikConstraint.mix = ikConstraint.data.mix + (num2 + (array[num + 1] - num2) * curvePercent - ikConstraint.data.mix) * alpha;
				ikConstraint.bendDirection = ((direction == MixDirection.Out) ? ikConstraint.data.bendDirection : ((int)array[num + -1]));
				return;
			}
			ikConstraint.mix += (num2 + (array[num + 1] - num2) * curvePercent - ikConstraint.mix) * alpha;
			if (direction == MixDirection.In)
			{
				ikConstraint.bendDirection = (int)array[num + -1];
			}
		}
	}
}
