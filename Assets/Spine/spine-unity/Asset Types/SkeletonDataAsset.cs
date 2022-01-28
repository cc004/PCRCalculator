using System;
using System.Collections.Generic;
using System.IO;
using Elements;
using UnityEngine;

namespace Spine.Unity
{
	public class SkeletonDataAsset : ScriptableObject
	{
		public AtlasAsset[] atlasAssets = new AtlasAsset[0];

		public float scale = 0.01f;

		public TextAsset skeletonJSON;

		[SpineAnimation("", "", false)]
		public string[] fromAnimation = new string[0];

		[SpineAnimation("", "", false)]
		public string[] toAnimation = new string[0];

		public float[] duration = new float[0];

		public float defaultMix;

		public RuntimeAnimatorController controller;

		private SkeletonData skeletonData;

		private AnimationStateData stateData;

		private Atlas[] atlasArr;

		private AtlasAttachmentLoader attachmentLoader;

		private float skeletonDataScale = 1f;

		public bool IsLoaded => skeletonData != null;
		private bool useOwnBytes = false;
		private byte[] skelBytes;

		private void Reset()
		{
			Clear();
		}

		public static SkeletonDataAsset CreateRuntimeInstance(TextAsset skeletonDataFile, AtlasAsset atlasAsset, bool initialize, float scale = 0.01f)
		{
			return CreateRuntimeInstance(skeletonDataFile, new AtlasAsset[1]
			{
				atlasAsset
			}, initialize, scale);
		}

		public static SkeletonDataAsset CreateRuntimeInstance(TextAsset skeletonDataFile, AtlasAsset[] atlasAssets, bool initialize, float scale = 0.01f)
		{
			SkeletonDataAsset skeletonDataAsset = ScriptableObject.CreateInstance<SkeletonDataAsset>();
			skeletonDataAsset.Clear();
			skeletonDataAsset.skeletonJSON = skeletonDataFile;
			skeletonDataAsset.atlasAssets = atlasAssets;
			skeletonDataAsset.scale = scale;
			if (initialize)
			{
				skeletonDataAsset.GetSkeletonData(quiet: true);
			}
			return skeletonDataAsset;
		}
		public static SkeletonDataAsset CreateRuntimeInstance(byte[] skeletonDataBytes, string name, AtlasAsset atlasAsset, bool initialize, float scale = 0.01f)
		{
			SkeletonDataAsset skeletonDataAsset = ScriptableObject.CreateInstance<SkeletonDataAsset>();
			skeletonDataAsset.Clear();
			skeletonDataAsset.skeletonJSON = new TextAsset("default");
			skeletonDataAsset.skeletonJSON.name = name;
			skeletonDataAsset.useOwnBytes = true;
			skeletonDataAsset.skelBytes = skeletonDataBytes;
			skeletonDataAsset.atlasAssets = new AtlasAsset[] { atlasAsset };
			skeletonDataAsset.scale = scale;

			if (initialize)
				skeletonDataAsset.GetSkeletonData(true);

			return skeletonDataAsset;
		}
		public void Clear()
		{
			skeletonData = null;
			stateData = null;
			atlasArr = null;
			attachmentLoader = null;
			skeletonDataScale = 1f;
		}

		public SkeletonData GetSkeletonData(bool quiet)
		{
			if (skeletonData != null)
			{
				return skeletonData;
			}
			AttachmentLoader attachmentLoader = new AtlasAttachmentLoader(GetAtlasArray());
			float num = scale;
			bool flag = skeletonJSON.name.ToLower().Contains(".skel");
			SkeletonData sd;
			try
			{
				if (useOwnBytes)
				{
					sd = SkeletonDataAsset.ReadSkeletonData(skelBytes, attachmentLoader, skeletonDataScale);
				}
				else
				{
					sd = ((!flag) ? ReadSkeletonData(skeletonJSON.text, attachmentLoader, num) : ReadSkeletonData(skeletonJSON.bytes, attachmentLoader, num));
				}
			}
			catch (Exception e)
			{
				return null;
			}
			InitializeWithData(sd);
			return skeletonData;
		}

		internal void InitializeWithData(SkeletonData sd)
		{
			skeletonData = sd;
			stateData = new AnimationStateData(skeletonData);
			FillStateData();
		}

		internal Atlas[] GetAtlasArray()
		{
			List<Atlas> list = new List<Atlas>(atlasAssets.Length);
			for (int i = 0; i < atlasAssets.Length; i++)
			{
				AtlasAsset atlasAsset = atlasAssets[i];
				if (!(atlasAsset == null))
				{
					Atlas atlas = atlasAsset.GetAtlas();
					if (atlas != null)
					{
						list.Add(atlas);
					}
				}
			}
			return list.ToArray();
		}

		internal static SkeletonData ReadSkeletonData(byte[] bytes, AttachmentLoader attachmentLoader, float scale)
		{
			SimpleMemoryStream input = new SimpleMemoryStream(bytes);
			return new SkeletonBinary(attachmentLoader)
			{
				Scale = scale
			}.ReadSkeletonData(input);
		}

		internal static SkeletonData ReadSkeletonData(string text, AttachmentLoader attachmentLoader, float scale)
		{
			StringReader reader = new StringReader(text);
			return new SkeletonJson(attachmentLoader)
			{
				Scale = scale
			}.ReadSkeletonData(reader);
		}

		public void FillStateData()
		{
			if (stateData == null)
			{
				return;
			}
			stateData.defaultMix = defaultMix;
			int i = 0;
			for (int num = fromAnimation.Length; i < num; i++)
			{
				if (fromAnimation[i].Length != 0 && toAnimation[i].Length != 0)
				{
					stateData.SetMix(fromAnimation[i], toAnimation[i], duration[i]);
				}
			}
		}

		public AnimationStateData GetAnimationStateData()
		{
			if (stateData != null)
			{
				return stateData;
			}
			GetSkeletonData(quiet: false);
			return stateData;
		}

		private bool CreateAtlas(bool quiet)
		{
			if (atlasArr != null)
			{
				return true;
			}
			if (atlasAssets == null)
			{
				atlasAssets = new AtlasAsset[0];
				Reset();
				return false;
			}
			if (skeletonJSON == null)
			{
				Reset();
				return false;
			}
			if (atlasAssets.Length == 0)
			{
				Reset();
				return false;
			}
			atlasArr = new Atlas[atlasAssets.Length];
			for (int i = 0; i < atlasAssets.Length; i++)
			{
				if (atlasAssets[i] == null)
				{
					Reset();
					return false;
				}
				atlasArr[i] = atlasAssets[i].GetAtlas();
				if (atlasArr[i] == null)
				{
					Reset();
					return false;
				}
			}
			return true;
		}

		private bool CreateAttachmentLoader()
		{
			if (attachmentLoader != null)
			{
				return true;
			}
			attachmentLoader = new AtlasAttachmentLoader(atlasArr);
			skeletonDataScale = scale;
			return true;
		}

		public SkeletonData CreateSkeltonCysp(TextAsset skelton, float scale, bool quiet)
		{
			if (skeletonData != null)
			{
				return skeletonData;
			}
			skeletonJSON = skelton;
			if (!CreateAtlas(quiet))
			{
				return null;
			}
			this.scale = scale;
			if (!CreateAttachmentLoader())
			{
				return null;
			}
			try
			{
				SimpleMemoryStream input = new SimpleMemoryStream(skelton.bytes);
				SkeletonBinary skeletonBinary = new SkeletonBinary(attachmentLoader);
				skeletonBinary.Scale = skeletonDataScale;
				skeletonData = skeletonBinary.ReadSkeletonDevisionCyspSkeleton(input);
			}
			catch (Exception)
			{
				return null;
			}
			stateData = new AnimationStateData(skeletonData);
			FillStateData();
			return null;
		}

		public List<string> AddAnimationCysp(TextAsset animationAsset, bool quiet = false)
		{
			SkeletonData skeletonData = GetSkeletonData(quiet: false);
			if (!CreateAtlas(quiet))
			{
				return null;
			}
			if (!CreateAttachmentLoader())
			{
				return null;
			}
			List<string> list = null;
			try
			{
				SimpleMemoryStream input = new SimpleMemoryStream(animationAsset.bytes);
				list = new SkeletonBinary(attachmentLoader)
				{
					Scale = skeletonDataScale
				}.ReadSkeletonDevisionCyspAnimation(input, skeletonData);
			}
			catch (Exception)
			{
				return null;
			}
			FillStateData();
			return list;
		}

		public void RemoveAnimation(List<string> animationNameList)
		{
			SkeletonData skeletonData = GetSkeletonData(quiet: false);
			for (int i = 0; i < animationNameList.Count; i++)
			{
				skeletonData.animations.Remove(animationNameList[i]);
			}
		}

		public void AddAnimations(TextAsset _skeletonJSON)
		{
			if (CreateAtlas(quiet: false) && CreateAttachmentLoader())
			{
				StringReader reader = new StringReader(_skeletonJSON.text);
				SkeletonJson skeletonJson = new SkeletonJson(attachmentLoader);
				skeletonJson.Scale = skeletonDataScale;
				skeletonJson.AddAnimation(reader, skeletonData);
			}
		}
	}
}
