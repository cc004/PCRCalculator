namespace Spine.Unity
{
	public class SpineIkConstraint : SpineAttributeBase
	{
		public SpineIkConstraint(string startsWith = "", string dataField = "", bool includeNone = true)
		{
			this.startsWith = startsWith;
			this.dataField = dataField;
			this.includeNone = includeNone;
		}
	}
}
