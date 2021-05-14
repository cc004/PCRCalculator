namespace Spine
{
	public class ShearTimeline : TranslateTimeline
	{
		public ShearTimeline(int frameCount, int _boneIndex)
			: base(frameCount, _boneIndex)
		{
			propertyId = 50331648 + _boneIndex;
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
					bone.shearX = bone.data.shearX;
					bone.shearY = bone.data.shearY;
					break;
				case MixPose.Current:
					bone.shearX += (bone.data.shearX - bone.shearX) * alpha;
					bone.shearY += (bone.data.shearY - bone.shearY) * alpha;
					break;
				}
				return;
			}
			float num;
			float num2;
			if (time >= frames[frames.Length - 3])
			{
				num = frames[frames.Length + -2];
				num2 = frames[frames.Length + -1];
			}
			else
			{
				int num3 = Animation.BinarySearch(frames, time, 3);
				num = frames[num3 + -2];
				num2 = frames[num3 + -1];
				float num4 = frames[num3];
				float curvePercent = GetCurvePercent(num3 / 3 - 1, 1f - (time - num4) / (frames[num3 + -3] - num4));
				num += (frames[num3 + 1] - num) * curvePercent;
				num2 += (frames[num3 + 2] - num2) * curvePercent;
			}
			if (pose == MixPose.Setup)
			{
				bone.shearX = bone.data.shearX + num * alpha;
				bone.shearY = bone.data.shearY + num2 * alpha;
			}
			else
			{
				bone.shearX += (bone.data.shearX + num - bone.shearX) * alpha;
				bone.shearY += (bone.data.shearY + num2 - bone.shearY) * alpha;
			}
		}
	}
}
