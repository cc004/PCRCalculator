namespace Spine
{
	public class ScaleTimeline : TranslateTimeline
	{
		public ScaleTimeline(int frameCount, int _boneIndex)
			: base(frameCount, _boneIndex)
		{
			propertyId = 33554432 + _boneIndex;
		}

		public override void Apply(Skeleton skeleton, float lastTime, float time, ExposedList<Event> firedEvents, float alpha, MixPose pose, MixDirection direction)
		{
			Bone bone = skeleton.bones.Items[base.BoneIndex];
			float[] frames = base.frames;
			if (time < frames[0])
			{
				switch (pose)
				{
				case MixPose.Setup:
					bone.scaleX = bone.data.scaleX;
					bone.scaleY = bone.data.scaleY;
					break;
				case MixPose.Current:
					bone.scaleX += (bone.data.scaleX - bone.scaleX) * alpha;
					bone.scaleY += (bone.data.scaleY - bone.scaleY) * alpha;
					break;
				}
				return;
			}
			float num;
			float num2;
			if (time >= frames[frames.Length - 3])
			{
				num = frames[frames.Length + -2] * bone.data.scaleX;
				num2 = frames[frames.Length + -1] * bone.data.scaleY;
			}
			else
			{
				int num3 = Animation.BinarySearch(frames, time, 3);
				num = frames[num3 + -2];
				num2 = frames[num3 + -1];
				float num4 = frames[num3];
				float curvePercent = GetCurvePercent(num3 / 3 - 1, 1f - (time - num4) / (frames[num3 + -3] - num4));
				num = (num + (frames[num3 + 1] - num) * curvePercent) * bone.data.scaleX;
				num2 = (num2 + (frames[num3 + 2] - num2) * curvePercent) * bone.data.scaleY;
			}
			if (alpha == 1f)
			{
				bone.scaleX = num;
				bone.scaleY = num2;
				return;
			}
			float num5;
			float num6;
			if (pose == MixPose.Setup)
			{
				num5 = bone.data.scaleX;
				num6 = bone.data.scaleY;
			}
			else
			{
				num5 = bone.scaleX;
				num6 = bone.scaleY;
			}
			if (direction == MixDirection.Out)
			{
				num = ((num >= 0f) ? num : (0f - num)) * (float)((num5 >= 0f) ? 1 : (-1));
				num2 = ((num2 >= 0f) ? num2 : (0f - num2)) * (float)((num6 >= 0f) ? 1 : (-1));
			}
			else
			{
				num5 = ((num5 >= 0f) ? num5 : (0f - num5)) * (float)((num >= 0f) ? 1 : (-1));
				num6 = ((num6 >= 0f) ? num6 : (0f - num6)) * (float)((num2 >= 0f) ? 1 : (-1));
			}
			bone.scaleX = num5 + (num - num5) * alpha;
			bone.scaleY = num6 + (num2 - num6) * alpha;
		}
	}
}
