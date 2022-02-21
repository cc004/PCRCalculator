using System;
using System.Collections.Generic;

namespace Spine
{
	public class TrackEntry : Pool<TrackEntry>.IPoolable
	{
		internal Animation animation;

		internal TrackEntry next;

		internal TrackEntry mixingFrom;

		internal int trackIndex;

		internal bool loop;

		internal float eventThreshold;

		internal float attachmentThreshold;

		internal float drawOrderThreshold;

		internal float animationStart;

		internal float animationEnd;

		internal float animationLast;

		internal float nextAnimationLast;

		internal float delay;

		internal float trackTime;

		internal float trackLast;

		internal float nextTrackLast;

		internal float trackEnd;

		internal float timeScale = 1f;

		internal float alpha;

		internal float mixTime;

		internal float mixDuration;

		internal float interruptAlpha;

		internal float totalAlpha;

		internal readonly ExposedList<int> timelineData = new ExposedList<int>();

		internal readonly ExposedList<TrackEntry> timelineDipMix = new ExposedList<TrackEntry>();

		internal readonly ExposedList<float> timelinesRotation = new ExposedList<float>();

		public int TrackIndex => trackIndex;

		public Animation Animation => animation;

		public bool Loop
		{
			get
			{
				return loop;
			}
			set
			{
				loop = value;
			}
		}

		public float Delay
		{
			get
			{
				return delay;
			}
			set
			{
				delay = value;
			}
		}

		public float TrackTime
		{
			get
			{
				return trackTime;
			}
			set
			{
				trackTime = value;
			}
		}

		public float Time
		{
			get
			{
				return trackTime;
			}
			set
			{
				trackTime = value;
			}
		}

		public float time
		{
			get
			{
				return trackTime;
			}
			set
			{
				trackTime = value;
			}
		}

		public float lastTime
		{
			get
			{
				return animationLast;
			}
			set
			{
				animationLast = value;
			}
		}

		public float EndTime
		{
			get
			{
				return animationEnd;
			}
			set
			{
				animationEnd = value;
			}
		}

		public float endTime
		{
			get
			{
				return animationEnd;
			}
			set
			{
				animationEnd = value;
			}
		}

		public float TrackEnd
		{
			get
			{
				return trackEnd;
			}
			set
			{
				trackEnd = value;
			}
		}

		public float AnimationStart
		{
			get
			{
				return animationStart;
			}
			set
			{
				animationStart = value;
			}
		}

		public float AnimationEnd
		{
			get
			{
				return animationEnd;
			}
			set
			{
				animationEnd = value;
			}
		}

		public float AnimationLast
		{
			get
			{
				return animationLast;
			}
			set
			{
				animationLast = value;
				nextAnimationLast = value;
			}
		}

		public float AnimationTime
		{
			get
			{
				if (loop)
				{
					float num = animationEnd - animationStart;
					if (num == 0f)
					{
						return animationStart;
					}
					return trackTime % num + animationStart;
				}
				return Math.Min(trackTime + animationStart, animationEnd);
			}
		}

		public float TimeScale
		{
			get
			{
				return timeScale;
			}
			set
			{
				timeScale = value;
			}
		}

		public float Alpha
		{
			get
			{
				return alpha;
			}
			set
			{
				alpha = value;
			}
		}

		public float EventThreshold
		{
			get
			{
				return eventThreshold;
			}
			set
			{
				eventThreshold = value;
			}
		}

		public float AttachmentThreshold
		{
			get
			{
				return attachmentThreshold;
			}
			set
			{
				attachmentThreshold = value;
			}
		}

		public float DrawOrderThreshold
		{
			get
			{
				return drawOrderThreshold;
			}
			set
			{
				drawOrderThreshold = value;
			}
		}

		public TrackEntry Next => next;

		public bool IsComplete => trackTime >= animationEnd - animationStart;

		public float MixTime
		{
			get
			{
				return mixTime;
			}
			set
			{
				mixTime = value;
			}
		}

		public float MixDuration
		{
			get
			{
				return mixDuration;
			}
			set
			{
				mixDuration = value;
			}
		}

		public TrackEntry MixingFrom => mixingFrom;

		public event AnimationState.TrackEntryDelegate Start;

		public event AnimationState.TrackEntryDelegate Interrupt;

		public event AnimationState.TrackEntryDelegate End;

		public event AnimationState.TrackEntryDelegate Dispose;

		public event AnimationState.TrackEntryDelegate Complete;

		public event AnimationState.TrackEntryEventDelegate Event;

		public void Reset()
		{
			next = null;
			mixingFrom = null;
			animation = null;
			timelineData.Clear();
			timelineDipMix.Clear();
			timelinesRotation.Clear();
			Start = null;
			Interrupt = null;
			End = null;
			Dispose = null;
			Complete = null;
			Event = null;
		}

		internal TrackEntry SetTimelineData(TrackEntry to, ExposedList<TrackEntry> mixingToArray, HashSet<int> propertyIDs)
		{
			if (to != null)
			{
				mixingToArray.Add(to);
			}
			TrackEntry result = ((mixingFrom != null) ? mixingFrom.SetTimelineData(this, mixingToArray, propertyIDs) : this);
			if (to != null)
			{
				mixingToArray.Pop();
			}
			TrackEntry[] items = mixingToArray.Items;
			int num = mixingToArray.Count - 1;
			Timeline[] items2 = animation.timelines.Items;
			int count = animation.timelines.Count;
			int[] items3 = timelineData.Resize(count).Items;
			timelineDipMix.Clear();
			TrackEntry[] items4 = timelineDipMix.Resize(count).Items;
			for (int i = 0; i < count; i++)
			{
				int propertyId = items2[i].PropertyId;
				if (!propertyIDs.Add(propertyId))
				{
					items3[i] = 0;
					continue;
				}
				if (to == null || !to.HasTimeline(propertyId))
				{
					items3[i] = 1;
					continue;
				}
				int num2 = num;
				while (true)
				{
					if (num2 >= 0)
					{
						TrackEntry trackEntry = items[num2];
						if (trackEntry.HasTimeline(propertyId))
						{
							num2--;
							continue;
						}
						if (trackEntry.mixDuration > 0f)
						{
							items3[i] = 3;
							items4[i] = trackEntry;
							break;
						}
					}
					items3[i] = 2;
					break;
				}
			}
			return result;
		}

		private bool HasTimeline(int id)
		{
			Timeline[] items = animation.timelines.Items;
			int i = 0;
			for (int count = animation.timelines.Count; i < count; i++)
			{
				if (items[i].PropertyId == id)
				{
					return true;
				}
			}
			return false;
		}

		internal void OnStart()
		{
			if (Start != null)
			{
				Start(this);
			}
		}

		internal void OnInterrupt()
		{
			if (Interrupt != null)
			{
				Interrupt(this);
			}
		}

		internal void OnEnd()
		{
			if (End != null)
			{
				End(this);
			}
		}

		internal void OnDispose()
		{
			if (Dispose != null)
			{
				Dispose(this);
			}
		}

		internal void OnComplete()
		{
			if (Complete != null)
			{
				Complete(this);
			}
		}

		internal void OnEvent(Event e)
		{
			if (Event != null)
			{
				Event(this, e);
			}
		}

		public void ResetRotationDirections()
		{
			timelinesRotation.Clear();
		}

		public override string ToString()
		{
			if (animation != null)
			{
				return animation.name;
			}
			return "<none>";
		}
	}
}
