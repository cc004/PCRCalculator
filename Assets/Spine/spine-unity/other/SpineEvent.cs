namespace Spine.Unity
{
	public class SpineEvent : SpineAttributeBase
	{
		public SpineEvent(string startsWith = "", string dataField = "", bool includeNone = true)
		{
			this.startsWith = startsWith;
			this.dataField = dataField;
			this.includeNone = includeNone;
		}
	}
}
