namespace Spine.Unity
{
	public class SpineTransformConstraint : SpineAttributeBase
	{
		public SpineTransformConstraint(string startsWith = "", string dataField = "", bool includeNone = true)
		{
			base.startsWith = startsWith;
			base.dataField = dataField;
			base.includeNone = includeNone;
		}
	}
}
