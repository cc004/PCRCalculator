namespace Spine.Unity
{
	public class SpineSlot : SpineAttributeBase
	{
		public bool containsBoundingBoxes;

		public SpineSlot(string startsWith = "", string dataField = "", bool containsBoundingBoxes = false, bool includeNone = true)
		{
			this.startsWith = startsWith;
			this.dataField = dataField;
			this.containsBoundingBoxes = containsBoundingBoxes;
			this.includeNone = includeNone;
		}
	}
}
