using System;
using UnityEngine;

namespace Spine.Unity
{
	[AttributeUsage(AttributeTargets.Field)]
	public abstract class SpineAttributeBase : PropertyAttribute
	{
		public string dataField = "";

		public string startsWith = "";

		public bool includeNone = true;
	}
}
