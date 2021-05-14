namespace Spine.Unity
{
	public class SpineIkConstraint : SpineAttributeBase
	{
		public SpineIkConstraint(string startsWith = "", string dataField = "", bool includeNone = true)
		{
			base.startsWith = startsWith;
			base.dataField = dataField;
			base.includeNone = includeNone;
		}
	}
}
