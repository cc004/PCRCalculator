namespace Spine
{
	public class AttachmentTimeline : Timeline
	{
		private int slotIndex;

		internal float[] frames;

		private string[] attachmentNames;

		protected int propertyId;

		public int SlotIndex
		{
			get
			{
				return slotIndex;
			}
			set
			{
				slotIndex = value;
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

		public string[] AttachmentNames
		{
			get
			{
				return attachmentNames;
			}
			set
			{
				attachmentNames = value;
			}
		}

		public int FrameCount => frames.Length;

		public int PropertyId => propertyId;

		public AttachmentTimeline(int frameCount, int _slotIndex)
		{
			frames = new float[frameCount];
			attachmentNames = new string[frameCount];
			slotIndex = _slotIndex;
			propertyId = 67108864 + slotIndex;
		}

		public void SetFrame(int frameIndex, float time, string attachmentName)
		{
			frames[frameIndex] = time;
			attachmentNames[frameIndex] = attachmentName;
		}

		public void Apply(Skeleton skeleton, float lastTime, float time, ExposedList<Event> firedEvents, float alpha, MixPose pose, MixDirection direction)
		{
			Slot slot = skeleton.slots.Items[slotIndex];
			if (direction == MixDirection.Out && pose == MixPose.Setup)
			{
				string attachmentName = slot.data.attachmentName;
				slot.Attachment = ((attachmentName == null) ? null : skeleton.GetAttachment(slotIndex, attachmentName));
				return;
			}
			float[] array = frames;
			if (time < array[0])
			{
				if (pose == MixPose.Setup)
				{
					string attachmentName = slot.data.attachmentName;
					slot.Attachment = ((attachmentName == null) ? null : skeleton.GetAttachment(slotIndex, attachmentName));
				}
			}
			else
			{
				int num = ((!(time >= array[array.Length - 1])) ? (Animation.BinarySearch(array, time, 1) - 1) : (array.Length - 1));
				string attachmentName = attachmentNames[num];
				slot.Attachment = ((attachmentName == null) ? null : skeleton.GetAttachment(slotIndex, attachmentName));
			}
		}
	}
}
