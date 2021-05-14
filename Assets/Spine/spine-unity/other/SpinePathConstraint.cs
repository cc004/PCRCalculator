namespace Spine.Unity
{
	public class SpinePathConstraint : SpineAttributeBase
	{
		public SpinePathConstraint(string startsWith = "", string dataField = "", bool includeNone = true)
		{
			base.startsWith = startsWith;
			base.dataField = dataField;
			base.includeNone = includeNone;
		}
	}
}
