namespace Spine.Unity
{
	public class SpineAnimation : SpineAttributeBase
	{
		public SpineAnimation(string startsWith = "", string dataField = "", bool includeNone = true)
		{
			base.startsWith = startsWith;
			base.dataField = dataField;
			base.includeNone = includeNone;
		}
	}
}
