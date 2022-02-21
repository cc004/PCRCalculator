namespace Spine.Unity
{
	public class SpineAnimation : SpineAttributeBase
	{
		public SpineAnimation(string startsWith = "", string dataField = "", bool includeNone = true)
		{
			this.startsWith = startsWith;
			this.dataField = dataField;
			this.includeNone = includeNone;
		}
	}
}
