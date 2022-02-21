namespace Spine.Unity
{
	public class SpinePathConstraint : SpineAttributeBase
	{
		public SpinePathConstraint(string startsWith = "", string dataField = "", bool includeNone = true)
		{
			this.startsWith = startsWith;
			this.dataField = dataField;
			this.includeNone = includeNone;
		}
	}
}
