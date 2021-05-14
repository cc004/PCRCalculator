namespace Spine.Unity
{
	public class SpineSkin : SpineAttributeBase
	{
		public SpineSkin(string startsWith = "", string dataField = "", bool includeNone = true)
		{
			base.startsWith = startsWith;
			base.dataField = dataField;
			base.includeNone = includeNone;
		}
	}
}
