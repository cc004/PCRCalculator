namespace Spine.Unity
{
	public class SpineEvent : SpineAttributeBase
	{
		public SpineEvent(string startsWith = "", string dataField = "", bool includeNone = true)
		{
			base.startsWith = startsWith;
			base.dataField = dataField;
			base.includeNone = includeNone;
		}
	}
}
