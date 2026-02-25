using System;
using System.Collections;
using System.Collections.Generic;
using Cute;
using Elements;
using UnityEngine;

namespace Elements
{

	public class CircleEffectChild : MonoBehaviour
	{
		private const float PI_DEG = 180f;

		public List<Renderer> Renderers;

		public float StartPitch;

		[SerializeField]
		private GameObject execEffect;

		public float CurrentPitch { get; set; }

		public bool IsFront { get; set; }

		public CircleEffectController CircleEffectController { get; set; }

		private List<Renderer> initialExecRenderer { get; set; }

		private void OnDestroy()
		{
			Renderers = null;
			CircleEffectController = null;
			onDestroy();
		}

		protected virtual void onDestroy()
		{
		}

		public virtual void Initialize(CircleEffectController _circleEffectController, UnitCtrl _target)
		{
			CircleEffectController = _circleEffectController;
			CurrentPitch = StartPitch;
			ParticleSystem[] componentsInChildren = execEffect.GetComponentsInChildren<ParticleSystem>();
			initialExecRenderer = new List<Renderer>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				initialExecRenderer.Add(((Component)componentsInChildren[i]).GetComponent<Renderer>());
			}
		}

		public void UpdatePosition(float _deltaAngle, int _sortOrder)
		{
			//IL_009a: Unknown result type (might be due to invalid IL or missing references)
			//IL_009f: Unknown result type (might be due to invalid IL or missing references)
			//IL_00af: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
			//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
			//IL_0102: Unknown result type (might be due to invalid IL or missing references)
			CurrentPitch += _deltaAngle;
			if (CurrentPitch < 0f)
			{
				CurrentPitch += 360f;
			}
			CurrentPitch %= 360f;
			if (CurrentPitch < 180f && !IsFront)
			{
				IsFront = true;
				SetSortOder(_sortOrder - 1);
			}
			if (CurrentPitch > 180f && IsFront)
			{
				IsFront = false;
				SetSortOder(_sortOrder + 1);
			}
			((Component)this).transform.localPosition = Quaternion.AngleAxis(CircleEffectController.Roll, Vector3.left) * Quaternion.AngleAxis(CircleEffectController.Yaw, Vector3.forward) * new Vector3(CircleEffectController.Radius * Mathf.Cos((float)Math.PI / 180f * CurrentPitch), 0f, CircleEffectController.Radius * Mathf.Sin((float)Math.PI / 180f * CurrentPitch));
		}

		protected virtual void SetSortOder(int _sortOrder)
		{
			int i = 0;
			for (int count = Renderers.Count; i < count; i++)
			{
				Renderers[i].sortingOrder = _sortOrder;
			}
		}

		public void UpdateSortOrder(int _sortOrder)
		{
			if (CurrentPitch <= 180f)
			{
				IsFront = true;
				SetSortOder(_sortOrder - 1);
			}
			if (CurrentPitch > 180f)
			{
				IsFront = false;
				SetSortOder(_sortOrder + 1);
			}
		}

		public virtual void StartEffect()
		{
			((Component)this).gameObject.SetActive(true);
			execEffect.SetActive(false);
			execEffect.SetActive(true);
		}

		public virtual void EndEffect(Action _callback)
		{
			((Component)this).gameObject.SetActive(false);
			_callback.Call();
		}

		public virtual void Pause()
		{
		}

		public virtual void Resume()
		{
		}

		public void SetExecEffectDisable()
		{
			// if ((Object)(object)execEffect != (Object)null)
			// {
			// 	for (int i = 0; i < initialExecRenderer.Count; i++)
			// 	{
			// 		initialExecRenderer[i].sortingOrder = -11500;
			// 	}
			// }
		}
	}


	public class CircleEffectController : SkillEffectCtrl
	{
		[SerializeField]
		private float duration = 10f;

		public bool FollowXY = true;

		public List<CircleEffectChild> Children;

		public float Radius = 0.5f;

		public float Roll = 10f;

		public float Yaw;

		[SerializeField]
		private float angularVelocity = 180f;

		protected override void onDestroy()
		{
			Children = null;
			base.onDestroy();
		}

		public override void ResetParameter(GameObject _prefab, int _skinId, bool _isShadow)
		{
			base.ResetParameter(_prefab);
			int i = 0;
			for (int count = Children.Count; i < count; i++)
			{
				((MonoBehaviour)(object)Children[i]).SetActive(_isActive: true);
			}
		}

		public void SetChildrenInactive()
		{
			int i = 0;
			for (int count = Children.Count; i < count; i++)
			{
				((MonoBehaviour)(object)Children[i]).SetActive(_isActive: false);
			}
		}

		public void AddEffect(int number)
		{
			int num = 0;
			for (int i = 0; i < Children.Count; i++)
			{
				if (!((Component)Children[i]).gameObject.activeSelf)
				{
					Children[i].StartEffect();
					num++;
					if (num == number)
					{
						break;
					}
				}
			}
		}

		public override void ExecAppendCoroutine(UnitCtrl _unit = null, bool _isAbnormal = false)
		{
			// AppendCoroutine(UpdateTimer(), ePauseType.VISUAL, _unit);
			// int i = 0;
			// for (int count = Children.Count; i < count; i++)
			// {
			// 	Children[i].Initialize(this, base.SortTarget);
			// }
			// base.ExecAppendCoroutine(_unit);
			// base.SortTarget.CircleEffectList.Add(this);
		}

		public IEnumerator UpdateTimer()
		{
            return null;
			// float time = 0f;
			// while (true)
			// {
			// 	if (!base.IsPlaying)
			// 	{
			// 		yield break;
			// 	}
			// 	time += base.battleManager.FNHFJLAENPF;
			// 	int i = 0;
			// 	for (int count = Children.Count; i < count; i++)
			// 	{
			// 		Children[i].UpdatePosition(base.battleManager.FNHFJLAENPF * angularVelocity, base.SortTarget.SortOrder);
			// 	}
			// 	if (time > duration)
			// 	{
			// 		break;
			// 	}
			// 	yield return null;
			// }
			// // base.SortTarget.CircleEffectList.Remove(this);
			// timeToDie = true;
		}

		public override void SetSortOrderBack()
		{
			setSortOrder();
		}

		private void setSortOrder()
		{
			int i = 0;
			for (int count = Children.Count; i < count; i++)
			{
				Children[i].UpdateSortOrder(base.SortTarget.SortOrder);
			}
		}

		public override void SetSortOrderFront()
		{
			setSortOrder();
			int i = 0;
			for (int count = Children.Count; i < count; i++)
			{
				Children[i].SetExecEffectDisable();
			}
		}

		public void DisableTop(bool isLeftDir, Action _callback)
		{
			//IL_0047: Unknown result type (might be due to invalid IL or missing references)
			//IL_0062: Unknown result type (might be due to invalid IL or missing references)
			CircleEffectChild circleEffectChild = null;
			bool flag = true;
			int i = 0;
			for (int count = Children.Count; i < count; i++)
			{
				if (((Component)Children[i]).gameObject.activeSelf)
				{
					if (flag)
					{
						circleEffectChild = Children[i];
						flag = false;
					}
					else if (isLeftDir == ((Component)circleEffectChild).transform.localPosition.x > ((Component)Children[i]).transform.localPosition.x)
					{
						circleEffectChild = Children[i];
					}
				}
			}
			if ((System.Object)(object)circleEffectChild != (System.Object)null)
			{
				circleEffectChild.EndEffect(_callback);
			}
		}

		public override void Pause()
		{
			base.Pause();
			for (int i = 0; i < Children.Count; i++)
			{
				Children[i].Pause();
			}
		}

		public override void Resume()
		{
			base.Resume();
			for (int i = 0; i < Children.Count; i++)
			{
				Children[i].Resume();
			}
		}

		protected override void resetStartDelay()
		{
		}
	}

}