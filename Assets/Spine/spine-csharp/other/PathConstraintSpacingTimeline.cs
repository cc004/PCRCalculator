namespace Spine
{
	public class PathConstraintSpacingTimeline : PathConstraintPositionTimeline
	{
		public override int PropertyId => propertyId;

		public PathConstraintSpacingTimeline(int frameCount, int _pathConstraintIndex)
			: base(frameCount, _pathConstraintIndex)
		{
			pathConstraintIndex = _pathConstraintIndex;
			propertyId = 201326592 + pathConstraintIndex;
		}

		public override void Apply(Skeleton skeleton, float lastTime, float time, ExposedList<Event> firedEvents, float alpha, MixPose pose, MixDirection direction)
		{
			PathConstraint pathConstraint = skeleton.pathConstraints.Items[pathConstraintIndex];
			float[] frames = this.frames;
			if (time < frames[0])
			{
				switch (pose)
				{
				case MixPose.Setup:
					pathConstraint.spacing = pathConstraint.data.spacing;
					break;
				case MixPose.Current:
					pathConstraint.spacing += (pathConstraint.data.spacing - pathConstraint.spacing) * alpha;
					break;
				}
				return;
			}
			float num;
			if (time >= frames[frames.Length - 2])
			{
				num = frames[frames.Length + -1];
			}
			else
			{
				int num2 = Animation.BinarySearch(frames, time, 2);
				num = frames[num2 + -1];
				float num3 = frames[num2];
				float curvePercent = GetCurvePercent(num2 / 2 - 1, 1f - (time - num3) / (frames[num2 + -2] - num3));
				num += (frames[num2 + 1] - num) * curvePercent;
			}
			if (pose == MixPose.Setup)
			{
				pathConstraint.spacing = pathConstraint.data.spacing + (num - pathConstraint.data.spacing) * alpha;
			}
			else
			{
				pathConstraint.spacing += (num - pathConstraint.spacing) * alpha;
			}
		}
	}
}
