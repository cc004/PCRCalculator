using System.Collections;

namespace Spine.Unity
{
	public class WaitForSpineAnimationComplete : IEnumerator
	{
		private bool m_WasFired;

		object IEnumerator.Current => null;

		public WaitForSpineAnimationComplete(TrackEntry trackEntry)
		{
			SafeSubscribe(trackEntry);
		}

		private void HandleComplete(TrackEntry trackEntry)
		{
			m_WasFired = true;
		}

		private void SafeSubscribe(TrackEntry trackEntry)
		{
			if (trackEntry == null)
			{
				m_WasFired = true;
			}
			else
			{
				trackEntry.Complete += HandleComplete;
			}
		}

		public WaitForSpineAnimationComplete NowWaitFor(TrackEntry trackEntry)
		{
			SafeSubscribe(trackEntry);
			return this;
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
