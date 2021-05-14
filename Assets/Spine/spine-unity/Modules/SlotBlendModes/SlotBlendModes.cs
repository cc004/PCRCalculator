using System.Collections.Generic;
using UnityEngine;

namespace Spine.Unity.Modules
{
	[DisallowMultipleComponent]
	public class SlotBlendModes : MonoBehaviour
	{
		public struct MaterialTexturePair
		{
			public Texture2D texture2D;

			public Material material;
		}

		private static Dictionary<MaterialTexturePair, Material> materialTable;

		public Material multiplyMaterialSource;

		public Material screenMaterialSource;

		private Texture2D texture;

		internal static Dictionary<MaterialTexturePair, Material> MaterialTable
		{
			get
			{
				if (materialTable == null)
				{
					materialTable = new Dictionary<MaterialTexturePair, Material>();
				}
				return materialTable;
			}
		}

		public bool Applied
		{
			get;
			private set;
		}

		internal static Material GetMaterialFor(Material materialSource, Texture2D texture)
		{
			if (materialSource == null || texture == null)
			{
				return null;
			}
			Dictionary<MaterialTexturePair, Material> dictionary = MaterialTable;
			MaterialTexturePair materialTexturePair = default(MaterialTexturePair);
			materialTexturePair.material = materialSource;
			materialTexturePair.texture2D = texture;
			MaterialTexturePair key = materialTexturePair;
			if (!dictionary.TryGetValue(key, out var value))
			{
				value = new Material(materialSource);
				value.name = "(Clone)" + texture.name + "-" + materialSource.name;
				value.mainTexture = texture;
				dictionary[key] = value;
			}
			return value;
		}

		private void Start()
		{
			if (!Applied)
			{
				Apply();
			}
		}

		private void OnDestroy()
		{
			if (Applied)
			{
				Remove();
			}
		}

		public void Apply()
		{
			GetTexture();
			if (texture == null)
			{
				return;
			}
			SkeletonRenderer component = GetComponent<SkeletonRenderer>();
			if (component == null)
			{
				return;
			}
			Dictionary<Slot, Material> customSlotMaterials = component.CustomSlotMaterials;
			foreach (Slot slot in component.Skeleton.Slots)
			{
				switch (slot.data.blendMode)
				{
				case BlendMode.Multiply:
					if (multiplyMaterialSource != null)
					{
						customSlotMaterials[slot] = GetMaterialFor(multiplyMaterialSource, texture);
					}
					break;
				case BlendMode.Screen:
					if (screenMaterialSource != null)
					{
						customSlotMaterials[slot] = GetMaterialFor(screenMaterialSource, texture);
					}
					break;
				}
			}
			Applied = true;
			component.LateUpdate();
		}

		public void Remove()
		{
			GetTexture();
			if (texture == null)
			{
				return;
			}
			SkeletonRenderer component = GetComponent<SkeletonRenderer>();
			if (component == null)
			{
				return;
			}
			Dictionary<Slot, Material> customSlotMaterials = component.CustomSlotMaterials;
			foreach (Slot slot in component.Skeleton.Slots)
			{
				Material value = null;
				switch (slot.data.blendMode)
				{
				case BlendMode.Multiply:
					if (customSlotMaterials.TryGetValue(slot, out value) && (object)value == GetMaterialFor(multiplyMaterialSource, texture))
					{
						customSlotMaterials.Remove(slot);
					}
					break;
				case BlendMode.Screen:
					if (customSlotMaterials.TryGetValue(slot, out value) && (object)value == GetMaterialFor(screenMaterialSource, texture))
					{
						customSlotMaterials.Remove(slot);
					}
					break;
				}
			}
			Applied = false;
			if (component.valid)
			{
				component.LateUpdate();
			}
		}

		public void GetTexture()
		{
			if (!(texture == null))
			{
				return;
			}
			SkeletonRenderer component = GetComponent<SkeletonRenderer>();
			if (component == null)
			{
				return;
			}
			SkeletonDataAsset skeletonDataAsset = component.skeletonDataAsset;
			if (skeletonDataAsset == null)
			{
				return;
			}
			AtlasAsset atlasAsset = skeletonDataAsset.atlasAssets[0];
			if (!(atlasAsset == null))
			{
				Material material = atlasAsset.materials[0];
				if (!(material == null))
				{
					texture = material.mainTexture as Texture2D;
				}
			}
		}
	}
}
