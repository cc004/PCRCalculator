namespace Spine.Unity
{
	public class SpineSkin : SpineAttributeBase
	{
		public SpineSkin(string startsWith = "", string dataField = "", bool includeNone = true)
		{
			this.startsWith = startsWith;
			this.dataField = dataField;
			this.includeNone = includeNone;
		}
	}
}
