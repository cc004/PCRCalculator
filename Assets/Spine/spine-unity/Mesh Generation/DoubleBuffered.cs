namespace Spine.Unity
{
	public class DoubleBuffered<T> where T : new()
	{
		private readonly T a = new T();

		private readonly T b = new T();

		private bool usingA;

		public T GetCurrent()
		{
			if (!usingA)
			{
				return b;
			}
			return a;
		}

		public T GetNext()
		{
			usingA = !usingA;
			if (!usingA)
			{
				return b;
			}
			return a;
		}
	}
}
