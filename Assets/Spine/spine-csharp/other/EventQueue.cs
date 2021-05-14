using System;

namespace Spine
{
	internal class EventQueue
	{
		private class EventQueueEntry
		{
			public EventType type;

			public TrackEntry entry;

			public Event e;

			public EventQueueEntry(EventType eventType, TrackEntry trackEntry, Event e = null)
			{
				type = eventType;
				entry = trackEntry;
				this.e = e;
			}

			public void SetData(EventType _eventType, TrackEntry _trackEntry, Event _event = null)
			{
				type = _eventType;
				entry = _trackEntry;
				e = _event;
			}
		}

		private enum EventType
		{
			NULL,
			Start,
			Interrupt,
			End,
			Dispose,
			Complete,
			Event
		}

		private readonly ExposedList<EventQueueEntry> eventQueueEntries = new ExposedList<EventQueueEntry>();

		internal bool drainDisabled;

		private readonly AnimationState state;

		private readonly Pool<TrackEntry> trackEntryPool;

		private const int EVENT_QUEUE_ENTRY_POOL = 32;

		private EventQueueEntry[] eventQueueEntryPool = new EventQueueEntry[32];

		internal event Action AnimationsChanged;

		internal EventQueue(AnimationState state, Action HandleAnimationsChanged, Pool<TrackEntry> trackEntryPool)
		{
			this.state = state;
			AnimationsChanged += HandleAnimationsChanged;
			this.trackEntryPool = trackEntryPool;
			initEventQueueExtryPool();
		}

		internal void Start(TrackEntry entry)
		{
			eventQueueEntries.Add(eventQueueExtryGet(EventType.Start, entry));
			if (this.AnimationsChanged != null)
			{
				this.AnimationsChanged();
			}
		}

		internal void Interrupt(TrackEntry entry)
		{
			eventQueueEntries.Add(eventQueueExtryGet(EventType.Interrupt, entry));
		}

		internal void End(TrackEntry entry)
		{
			eventQueueEntries.Add(eventQueueExtryGet(EventType.End, entry));
			if (this.AnimationsChanged != null)
			{
				this.AnimationsChanged();
			}
		}

		internal void Dispose(TrackEntry entry)
		{
			eventQueueEntries.Add(eventQueueExtryGet(EventType.Dispose, entry));
		}

		internal void Complete(TrackEntry entry)
		{
			eventQueueEntries.Add(eventQueueExtryGet(EventType.Complete, entry));
		}

		internal void Event(TrackEntry entry, Event e)
		{
			eventQueueEntries.Add(eventQueueExtryGet(EventType.Event, entry, e));
		}

		internal void Drain()
		{
			if (drainDisabled)
			{
				return;
			}
			drainDisabled = true;
			ExposedList<EventQueueEntry> exposedList = eventQueueEntries;
			AnimationState animationState = state;
			for (int i = 0; i < exposedList.Count; i++)
			{
				EventQueueEntry eventQueueEntry = exposedList.Items[i];
				TrackEntry entry = eventQueueEntry.entry;
				switch (eventQueueEntry.type)
				{
				case EventType.Start:
					entry.OnStart();
					animationState.OnStart(entry);
					break;
				case EventType.Interrupt:
					entry.OnInterrupt();
					animationState.OnInterrupt(entry);
					break;
				case EventType.End:
					entry.OnEnd();
					animationState.OnEnd(entry);
					goto case EventType.Dispose;
				case EventType.Dispose:
					entry.OnDispose();
					animationState.OnDispose(entry);
					trackEntryPool.Free(entry);
					break;
				case EventType.Complete:
					entry.OnComplete();
					animationState.OnComplete(entry);
					break;
				case EventType.Event:
					entry.OnEvent(eventQueueEntry.e);
					animationState.OnEvent(entry, eventQueueEntry.e);
					break;
				}
			}
			eventQueueEntriesClear();
			drainDisabled = false;
		}

		internal void Clear()
		{
			eventQueueEntriesClear();
		}

		private void initEventQueueExtryPool()
		{
			for (int i = 0; i < eventQueueEntryPool.Length; i++)
			{
				eventQueueEntryPool[i] = new EventQueueEntry(EventType.NULL, null);
			}
		}

		private EventQueueEntry eventQueueExtryGet(EventType _eventType, TrackEntry _trackEntry, Event _event = null)
		{
			for (int i = 0; i < eventQueueEntryPool.Length; i++)
			{
				EventQueueEntry eventQueueEntry = eventQueueEntryPool[i];
				if (eventQueueEntry.type == EventType.NULL)
				{
					eventQueueEntry.SetData(_eventType, _trackEntry, _event);
					return eventQueueEntry;
				}
			}
			return new EventQueueEntry(_eventType, _trackEntry, _event);
		}

		private void eventQueueExtryRelease(EventQueueEntry _enentQueueEntry)
		{
			_enentQueueEntry.type = EventType.NULL;
		}

		private void eventQueueEntriesClear()
		{
			for (int i = 0; i < eventQueueEntries.Count; i++)
			{
				EventQueueEntry enentQueueEntry = eventQueueEntries.Items[i];
				eventQueueExtryRelease(enentQueueEntry);
			}
			eventQueueEntries.Clear();
		}
	}
}
