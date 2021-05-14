using System.Collections;

namespace Spine.Unity
{
	public class WaitForSpineEvent : IEnumerator
	{
		private EventData m_TargetEvent;

		private string m_EventName;

		private AnimationState m_AnimationState;

		private bool m_WasFired;

		private bool m_unsubscribeAfterFiring;

		public bool WillUnsubscribeAfterFiring
		{
			get
			{
				return m_unsubscribeAfterFiring;
			}
			set
			{
				m_unsubscribeAfterFiring = value;
			}
		}

		object IEnumerator.Current => null;

		private void Subscribe(AnimationState state, EventData eventDataReference, bool unsubscribe)
		{
			if (state == null)
			{
				m_WasFired = true;
				return;
			}
			if (eventDataReference == null)
			{
				m_WasFired = true;
				return;
			}
			m_AnimationState = state;
			m_TargetEvent = eventDataReference;
			state.Event += HandleAnimationStateEvent;
			m_unsubscribeAfterFiring = unsubscribe;
		}

		private void SubscribeByName(AnimationState state, string eventName, bool unsubscribe)
		{
			if (state == null)
			{
				m_WasFired = true;
				return;
			}
			if (string.IsNullOrEmpty(eventName))
			{
				m_WasFired = true;
				return;
			}
			m_AnimationState = state;
			m_EventName = eventName;
			state.Event += HandleAnimationStateEventByName;
			m_unsubscribeAfterFiring = unsubscribe;
		}

		public WaitForSpineEvent(AnimationState state, EventData eventDataReference, bool unsubscribeAfterFiring = true)
		{
			Subscribe(state, eventDataReference, unsubscribeAfterFiring);
		}

		public WaitForSpineEvent(SkeletonAnimation skeletonAnimation, EventData eventDataReference, bool unsubscribeAfterFiring = true)
		{
			Subscribe(skeletonAnimation.state, eventDataReference, unsubscribeAfterFiring);
		}

		public WaitForSpineEvent(AnimationState state, string eventName, bool unsubscribeAfterFiring = true)
		{
			SubscribeByName(state, eventName, unsubscribeAfterFiring);
		}

		public WaitForSpineEvent(SkeletonAnimation skeletonAnimation, string eventName, bool unsubscribeAfterFiring = true)
		{
			SubscribeByName(skeletonAnimation.state, eventName, unsubscribeAfterFiring);
		}

		private void HandleAnimationStateEventByName(TrackEntry trackEntry, Event e)
		{
			m_WasFired |= e.Data.Name == m_EventName;
			if (m_WasFired && m_unsubscribeAfterFiring)
			{
				m_AnimationState.Event -= HandleAnimationStateEventByName;
			}
		}

		private void HandleAnimationStateEvent(TrackEntry trackEntry, Event e)
		{
			m_WasFired |= e.Data == m_TargetEvent;
			if (m_WasFired && m_unsubscribeAfterFiring)
			{
				m_AnimationState.Event -= HandleAnimationStateEvent;
			}
		}

		public WaitForSpineEvent NowWaitFor(AnimationState state, EventData eventDataReference, bool unsubscribeAfterFiring = true)
		{
			((IEnumerator)this).Reset();
			Clear(state);
			Subscribe(state, eventDataReference, unsubscribeAfterFiring);
			return this;
		}

		public WaitForSpineEvent NowWaitFor(AnimationState state, string eventName, bool unsubscribeAfterFiring = true)
		{
			((IEnumerator)this).Reset();
			Clear(state);
			SubscribeByName(state, eventName, unsubscribeAfterFiring);
			return this;
		}

		private void Clear(AnimationState state)
		{
			state.Event -= HandleAnimationStateEvent;
			state.Event -= HandleAnimationStateEventByName;
		}

		bool IEnumerator.MoveNext()
		{
			if (m_WasFired)
			{
				((IEnumerator)this).Reset();
				return false;
			}
			return true;
		}

		void IEnumerator.Reset()
		{
			m_WasFired = false;
		}
	}
}
