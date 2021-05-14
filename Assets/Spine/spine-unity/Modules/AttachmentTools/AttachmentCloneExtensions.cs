using System;
using UnityEngine;

namespace Spine.Unity.Modules.AttachmentTools
{
	public static class AttachmentCloneExtensions
	{
		public static Attachment GetClone(this Attachment o, bool cloneMeshesAsLinked)
		{
			RegionAttachment regionAttachment = o as RegionAttachment;
			if (regionAttachment != null)
			{
				return regionAttachment.GetClone();
			}
			MeshAttachment meshAttachment = o as MeshAttachment;
			if (meshAttachment != null)
			{
				if (!cloneMeshesAsLinked)
				{
					return meshAttachment.GetClone();
				}
				return meshAttachment.GetLinkedClone();
			}
			BoundingBoxAttachment boundingBoxAttachment = o as BoundingBoxAttachment;
			if (boundingBoxAttachment != null)
			{
				return boundingBoxAttachment.GetClone();
			}
			PathAttachment pathAttachment = o as PathAttachment;
			if (pathAttachment != null)
			{
				return pathAttachment.GetClone();
			}
			PointAttachment pointAttachment = o as PointAttachment;
			if (pointAttachment != null)
			{
				return pointAttachment.GetClone();
			}
			return (o as ClippingAttachment)?.GetClone();
		}

		public static RegionAttachment GetClone(this RegionAttachment o)
		{
			return new RegionAttachment(o.Name + "clone")
			{
				x = o.x,
				y = o.y,
				rotation = o.rotation,
				scaleX = o.scaleX,
				scaleY = o.scaleY,
				width = o.width,
				height = o.height,
				r = o.r,
				g = o.g,
				b = o.b,
				a = o.a,
				Path = o.Path,
				RendererObject = o.RendererObject,
				regionOffsetX = o.regionOffsetX,
				regionOffsetY = o.regionOffsetY,
				regionWidth = o.regionWidth,
				regionHeight = o.regionHeight,
				regionOriginalWidth = o.regionOriginalWidth,
				regionOriginalHeight = o.regionOriginalHeight,
				uvs = (o.uvs.Clone() as float[]),
				offset = (o.offset.Clone() as float[])
			};
		}

		public static ClippingAttachment GetClone(this ClippingAttachment o)
		{
			ClippingAttachment clippingAttachment = new ClippingAttachment(o.Name)
			{
				endSlot = o.endSlot
			};
			CloneVertexAttachment(o, clippingAttachment);
			return clippingAttachment;
		}

		public static PointAttachment GetClone(this PointAttachment o)
		{
			return new PointAttachment(o.Name)
			{
				rotation = o.rotation,
				x = o.x,
				y = o.y
			};
		}

		public static BoundingBoxAttachment GetClone(this BoundingBoxAttachment o)
		{
			BoundingBoxAttachment boundingBoxAttachment = new BoundingBoxAttachment(o.Name);
			CloneVertexAttachment(o, boundingBoxAttachment);
			return boundingBoxAttachment;
		}

		public static MeshAttachment GetLinkedClone(this MeshAttachment o, bool inheritDeform = true)
		{
			return o.GetLinkedMesh(o.Name, o.RendererObject as AtlasRegion, inheritDeform);
		}

		public static MeshAttachment GetClone(this MeshAttachment o)
		{
			MeshAttachment meshAttachment = new MeshAttachment(o.Name)
			{
				r = o.r,
				g = o.g,
				b = o.b,
				a = o.a,
				inheritDeform = o.inheritDeform,
				Path = o.Path,
				RendererObject = o.RendererObject,
				regionOffsetX = o.regionOffsetX,
				regionOffsetY = o.regionOffsetY,
				regionWidth = o.regionWidth,
				regionHeight = o.regionHeight,
				regionOriginalWidth = o.regionOriginalWidth,
				regionOriginalHeight = o.regionOriginalHeight,
				RegionU = o.RegionU,
				RegionV = o.RegionV,
				RegionU2 = o.RegionU2,
				RegionV2 = o.RegionV2,
				RegionRotate = o.RegionRotate,
				uvs = (o.uvs.Clone() as float[])
			};
			if (o.ParentMesh != null)
			{
				meshAttachment.ParentMesh = o.ParentMesh;
			}
			else
			{
				CloneVertexAttachment(o, meshAttachment);
				meshAttachment.regionUVs = o.regionUVs.Clone() as float[];
				meshAttachment.triangles = o.triangles.Clone() as int[];
				meshAttachment.hulllength = o.hulllength;
				meshAttachment.Edges = ((o.Edges == null) ? null : (o.Edges.Clone() as int[]));
				meshAttachment.Width = o.Width;
				meshAttachment.Height = o.Height;
			}
			return meshAttachment;
		}

		public static PathAttachment GetClone(this PathAttachment o)
		{
			PathAttachment pathAttachment = new PathAttachment(o.Name)
			{
				lengths = (o.lengths.Clone() as float[]),
				closed = o.closed,
				constantSpeed = o.constantSpeed
			};
			CloneVertexAttachment(o, pathAttachment);
			return pathAttachment;
		}

		private static void CloneVertexAttachment(VertexAttachment src, VertexAttachment dest)
		{
			dest.worldVerticesLength = src.worldVerticesLength;
			if (src.bones != null)
			{
				dest.bones = src.bones.Clone() as int[];
			}
			if (src.vertices != null)
			{
				dest.vertices = src.vertices.Clone() as float[];
			}
		}

		public static MeshAttachment GetLinkedMesh(this MeshAttachment o, string newLinkedMeshName, AtlasRegion region, bool inheritDeform = true)
		{
			if (region == null)
			{
				throw new ArgumentNullException("region");
			}
			if (o.ParentMesh != null)
			{
				o = o.ParentMesh;
			}
			MeshAttachment meshAttachment = new MeshAttachment(newLinkedMeshName);
			meshAttachment.SetRegion(region, updateUVs: false);
			meshAttachment.Path = newLinkedMeshName;
			meshAttachment.r = 1f;
			meshAttachment.g = 1f;
			meshAttachment.b = 1f;
			meshAttachment.a = 1f;
			meshAttachment.inheritDeform = inheritDeform;
			meshAttachment.ParentMesh = o;
			meshAttachment.UpdateUVs();
			return meshAttachment;
		}

		public static MeshAttachment GetLinkedMesh(this MeshAttachment o, Sprite sprite, Shader shader, bool inheritDeform = true, Material materialPropertySource = null)
		{
			Material material = new Material(shader);
			if (materialPropertySource != null)
			{
				material.CopyPropertiesFromMaterial(materialPropertySource);
				material.shaderKeywords = materialPropertySource.shaderKeywords;
			}
			return o.GetLinkedMesh(sprite.name, sprite.ToAtlasRegion(), inheritDeform);
		}

		public static MeshAttachment GetLinkedMesh(this MeshAttachment o, Sprite sprite, Material materialPropertySource, bool inheritDeform = true)
		{
			return o.GetLinkedMesh(sprite, materialPropertySource.shader, inheritDeform, materialPropertySource);
		}

		public static Attachment GetRemappedClone(this Attachment o, Sprite sprite, Material sourceMaterial, bool premultiplyAlpha = true, bool cloneMeshAsLinked = true, bool useOriginalRegionSize = false)
		{
			AtlasRegion atlasRegion = (premultiplyAlpha ? sprite.ToAtlasRegionPMAClone(sourceMaterial) : sprite.ToAtlasRegion());
			return o.GetRemappedClone(atlasRegion, cloneMeshAsLinked, useOriginalRegionSize, 1f / sprite.pixelsPerUnit);
		}

		public static Attachment GetRemappedClone(this Attachment o, AtlasRegion atlasRegion, bool cloneMeshAsLinked = true, bool useOriginalRegionSize = false, float scale = 0.01f)
		{
			RegionAttachment regionAttachment = o as RegionAttachment;
			if (regionAttachment != null)
			{
				RegionAttachment clone = regionAttachment.GetClone();
				clone.SetRegion(atlasRegion, updateOffset: false);
				if (!useOriginalRegionSize)
				{
					clone.width = (float)atlasRegion.width * scale;
					clone.height = (float)atlasRegion.height * scale;
				}
				clone.UpdateOffset();
				return clone;
			}
			MeshAttachment meshAttachment = o as MeshAttachment;
			if (meshAttachment != null)
			{
				MeshAttachment obj = (cloneMeshAsLinked ? meshAttachment.GetLinkedClone(cloneMeshAsLinked) : meshAttachment.GetClone());
				obj.SetRegion(atlasRegion);
				return obj;
			}
			return o.GetClone(cloneMeshesAsLinked: true);
		}
	}
}
