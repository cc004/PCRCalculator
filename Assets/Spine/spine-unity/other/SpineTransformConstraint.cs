namespace Spine.Unity
{
	public class SpineTransformConstraint : SpineAttributeBase
	{
		public SpineTransformConstraint(string startsWith = "", string dataField = "", bool includeNone = true)
		{
			this.startsWith = startsWith;
			this.dataField = dataField;
			this.includeNone = includeNone;
		}
	}
}
