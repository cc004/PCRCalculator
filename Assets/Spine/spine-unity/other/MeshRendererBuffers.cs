using System;
using UnityEngine;

namespace Spine.Unity
{
	public class MeshRendererBuffers : IDisposable
	{
		public class SmartMesh : IDisposable
		{
			public Mesh mesh = SpineMesh.NewMesh();

			public SkeletonRendererInstruction instructionUsed = new SkeletonRendererInstruction();

			public void Dispose()
			{
				if (mesh != null)
				{
					UnityEngine.Object.Destroy(mesh);
				}
				mesh = null;
			}
		}

		private DoubleBuffered<SmartMesh> doubleBufferedMesh;

		internal readonly ExposedList<Material> submeshMaterials = new ExposedList<Material>();

		internal Material[] sharedMaterials = new Material[0];

		public void Initialize()
		{
			doubleBufferedMesh = new DoubleBuffered<SmartMesh>();
		}

		public Material[] GetUpdatedShaderdMaterialsArray()
		{
			if (submeshMaterials.Count == sharedMaterials.Length)
			{
				submeshMaterials.CopyTo(sharedMaterials);
			}
			else
			{
				sharedMaterials = submeshMaterials.ToArray();
			}
			return sharedMaterials;
		}

		public bool MaterialsChangedInLastUpdate()
		{
			int count = submeshMaterials.Count;
			Material[] array = sharedMaterials;
			if (count != array.Length)
			{
				return true;
			}
			Material[] items = submeshMaterials.Items;
			for (int i = 0; i < count; i++)
			{
				if ((object)items[i] != array[i])
				{
					return true;
				}
			}
			return false;
		}

		public void UpdateSharedMaterials(ExposedList<SubmeshInstruction> instructions)
		{
			int count = instructions.Count;
			if (count > submeshMaterials.Items.Length)
			{
				Array.Resize(ref submeshMaterials.Items, count);
			}
			submeshMaterials.Count = count;
			Material[] items = submeshMaterials.Items;
			SubmeshInstruction[] items2 = instructions.Items;
			for (int i = 0; i < count; i++)
			{
				items[i] = items2[i].material;
			}
		}

		public SmartMesh GetNextMesh()
		{
			return doubleBufferedMesh.GetNext();
		}

		public void Clear()
		{
			sharedMaterials = new Material[0];
			submeshMaterials.Clear();
		}

		public void Dispose()
		{
			if (doubleBufferedMesh != null)
			{
				doubleBufferedMesh.GetNext().Dispose();
				doubleBufferedMesh.GetNext().Dispose();
				doubleBufferedMesh = null;
			}
		}
	}
}
