using UnityEngine;
using UnityEngine.UI;

namespace Spine.Unity
{
	[ExecuteInEditMode]
	[RequireComponent(typeof(CanvasRenderer), typeof(RectTransform))]
	[DisallowMultipleComponent]
	[AddComponentMenu("Spine/SkeletonGraphic (Unity UI Canvas)")]
	public class SkeletonGraphic : MaskableGraphic, ISkeletonComponent, IAnimationStateComponent, ISkeletonAnimation
	{
		public SkeletonDataAsset skeletonDataAsset;

		[SpineSkin("", "skeletonDataAsset")]
		public string initialSkinName = "default";

		public bool initialFlipX;

		public bool initialFlipY;

		[SpineAnimation("", "skeletonDataAsset")]
		public string startingAnimation;

		public bool startingLoop;

		public float timeScale = 1f;

		public bool freeze;

		public bool unscaledTime;

		private Texture overrideTexture;

		protected Skeleton skeleton;

		protected AnimationState state;

		[SerializeField]
		protected MeshGenerator meshGenerator = new MeshGenerator();

		private DoubleBuffered<MeshRendererBuffers.SmartMesh> meshBuffers;

		private SkeletonRendererInstruction currentInstructions = new SkeletonRendererInstruction();

		public SkeletonDataAsset SkeletonDataAsset => skeletonDataAsset;

		public Texture OverrideTexture
		{
			get
			{
				return overrideTexture;
			}
			set
			{
				overrideTexture = value;
				canvasRenderer.SetTexture(mainTexture);
			}
		}

		public override Texture mainTexture
		{
			get
			{
				if (overrideTexture != null)
				{
					return overrideTexture;
				}
				if (!(skeletonDataAsset == null))
				{
					return skeletonDataAsset.atlasAssets[0].materials[0].mainTexture;
				}
				return null;
			}
		}

		public Skeleton Skeleton => skeleton;

		public SkeletonData SkeletonData
		{
			get
			{
				if (skeleton != null)
				{
					return skeleton.data;
				}
				return null;
			}
		}

		public bool IsValid => skeleton != null;

		public AnimationState AnimationState => state;

		public MeshGenerator MeshGenerator => meshGenerator;

		public event UpdateBonesDelegate UpdateLocal;

		public event UpdateBonesDelegate UpdateWorld;

		public event UpdateBonesDelegate UpdateComplete;

		public event MeshGeneratorDelegate OnPostProcessVertices;

		public static SkeletonGraphic NewSkeletonGraphicGameObject(SkeletonDataAsset skeletonDataAsset, Transform parent)
		{
			SkeletonGraphic skeletonGraphic = AddSkeletonGraphicComponent(new GameObject("New Spine GameObject"), skeletonDataAsset);
			if (parent != null)
			{
				skeletonGraphic.transform.SetParent(parent, worldPositionStays: false);
			}
			return skeletonGraphic;
		}

		public static SkeletonGraphic AddSkeletonGraphicComponent(GameObject gameObject, SkeletonDataAsset skeletonDataAsset)
		{
			SkeletonGraphic skeletonGraphic = gameObject.AddComponent<SkeletonGraphic>();
			if (skeletonDataAsset != null)
			{
				skeletonGraphic.skeletonDataAsset = skeletonDataAsset;
				skeletonGraphic.Initialize(overwrite: false);
			}
			return skeletonGraphic;
		}

		protected override void Awake()
		{
			base.Awake();
			if (!IsValid)
			{
				Initialize(overwrite: false);
				Rebuild(CanvasUpdate.PreRender);
			}
		}

		public override void Rebuild(CanvasUpdate update)
		{
			base.Rebuild(update);
			if (!canvasRenderer.cull && update == CanvasUpdate.PreRender)
			{
				UpdateMesh();
			}
		}

		public virtual void Update()
		{
			if (!freeze)
			{
				Update(unscaledTime ? Time.unscaledDeltaTime : Time.deltaTime);
			}
		}

		public virtual void Update(float deltaTime)
		{
			if (IsValid)
			{
				deltaTime *= timeScale;
				skeleton.Update(deltaTime);
				state.Update(deltaTime);
				state.Apply(skeleton);
				if (UpdateLocal != null)
				{
					UpdateLocal(this);
				}
				skeleton.UpdateWorldTransform();
				if (UpdateWorld != null)
				{
					UpdateWorld(this);
					skeleton.UpdateWorldTransform();
				}
				if (UpdateComplete != null)
				{
					UpdateComplete(this);
				}
			}
		}

		public void LateUpdate()
		{
			if (!freeze)
			{
				UpdateMesh();
			}
		}

		public Mesh GetLastMesh()
		{
			return meshBuffers.GetCurrent().mesh;
		}

		public void Clear()
		{
			skeleton = null;
			canvasRenderer.Clear();
		}

		public void Initialize(bool overwrite)
		{
			if ((IsValid && !overwrite) || skeletonDataAsset == null)
			{
				return;
			}
			SkeletonData skeletonData = skeletonDataAsset.GetSkeletonData(quiet: false);
			if (skeletonData == null || skeletonDataAsset.atlasAssets.Length == 0 || skeletonDataAsset.atlasAssets[0].materials.Length == 0)
			{
				return;
			}
			state = new AnimationState(skeletonDataAsset.GetAnimationStateData());
			if (state == null)
			{
				Clear();
				return;
			}
			skeleton = new Skeleton(skeletonData)
			{
				flipX = initialFlipX,
				flipY = initialFlipY
			};
			meshBuffers = new DoubleBuffered<MeshRendererBuffers.SmartMesh>();
			canvasRenderer.SetTexture(mainTexture);
			if (!string.IsNullOrEmpty(initialSkinName))
			{
				skeleton.SetSkin(initialSkinName);
			}
			if (!string.IsNullOrEmpty(startingAnimation))
			{
				state.SetAnimation(0, startingAnimation, startingLoop);
				Update(0f);
			}
		}

		public void UpdateMesh()
		{
			if (IsValid)
			{
				skeleton.SetColor(color);
				MeshRendererBuffers.SmartMesh next = meshBuffers.GetNext();
				SkeletonRendererInstruction skeletonRendererInstruction = currentInstructions;
				MeshGenerator.GenerateSingleSubmeshInstruction(skeletonRendererInstruction, skeleton, material);
				bool flag = SkeletonRendererInstruction.GeometryNotEqual(skeletonRendererInstruction, next.instructionUsed);
				meshGenerator.Begin();
				if (skeletonRendererInstruction.hasActiveClipping)
				{
					meshGenerator.AddSubmesh(skeletonRendererInstruction.submeshInstructions.Items[0], flag);
				}
				else
				{
					meshGenerator.BuildMeshWithArrays(skeletonRendererInstruction, flag);
				}
				if (canvas != null)
				{
					meshGenerator.ScaleVertexData(canvas.referencePixelsPerUnit);
				}
				if (OnPostProcessVertices != null)
				{
					OnPostProcessVertices(meshGenerator.Buffers);
				}
				Mesh mesh = next.mesh;
				meshGenerator.FillVertexData(mesh);
				if (flag)
				{
					meshGenerator.FillTrianglesSingle(mesh);
				}
				canvasRenderer.SetMesh(mesh);
				next.instructionUsed.Set(skeletonRendererInstruction);
			}
		}
	}
}
