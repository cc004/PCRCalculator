using Cute;
using UnityEngine;

namespace Spine.Unity.Modules
{
	[RequireComponent(typeof(SkeletonUtilityBone))]
	[ExecuteInEditMode]
	public class SkeletonUtilityGroundConstraint : SkeletonUtilityConstraint
	{
		[Tooltip("LayerMask for what objects to raycast against")]
		public LayerMask groundMask;

		[Tooltip("Use 2D")]
		public bool use2D;

		[Tooltip("Uses SphereCast for 3D mode and CircleCast for 2D mode")]
		public bool useRadius;

		[Tooltip("The Radius")]
		public float castRadius = 0.1f;

		[Tooltip("How high above the target bone to begin casting from")]
		public float castDistance = 5f;

		[Tooltip("X-Axis adjustment")]
		public float castOffset;

		[Tooltip("Y-Axis adjustment")]
		public float groundOffset;

		[Tooltip("How fast the target IK position adjusts to the ground.  Use smaller values to prevent snapping")]
		public float adjustSpeed = 5f;

		private Vector3 rayOrigin;

		private Vector3 rayDir = new Vector3(0f, -1f, 0f);

		private float hitY;

		private float lastHitY;

		protected override void OnEnable()
		{
			base.OnEnable();
			lastHitY = base.transform.position.y;
		}

		public override void DoUpdate()
		{
			rayOrigin = base.transform.position + new Vector3(castOffset, castDistance, 0f);
			hitY = float.MinValue;
			if (use2D)
			{
				RaycastHit2D raycastHit2D = ((!useRadius) ? Physics2D.Raycast(rayOrigin, rayDir, castDistance + groundOffset, groundMask) : Physics2D.CircleCast(rayOrigin, castRadius, rayDir, castDistance + groundOffset, groundMask));
				if (raycastHit2D.collider != null)
				{
					hitY = raycastHit2D.point.y + groundOffset;
					if (Application01.BNDCGKDLADN)
					{
						hitY = Mathf.MoveTowards(lastHitY, hitY, adjustSpeed * Time.deltaTime);
					}
				}
				else if (Application01.BNDCGKDLADN)
				{
					hitY = Mathf.MoveTowards(lastHitY, base.transform.position.y, adjustSpeed * Time.deltaTime);
				}
			}
			else
			{
				bool flag = false;
				if ((!useRadius) ? Physics.Raycast(rayOrigin, rayDir, out var hitInfo, castDistance + groundOffset, groundMask) : Physics.SphereCast(rayOrigin, castRadius, rayDir, out hitInfo, castDistance + groundOffset, groundMask))
				{
					hitY = hitInfo.point.y + groundOffset;
					if (Application01.BNDCGKDLADN)
					{
						hitY = Mathf.MoveTowards(lastHitY, hitY, adjustSpeed * Time.deltaTime);
					}
				}
				else if (Application01.BNDCGKDLADN)
				{
					hitY = Mathf.MoveTowards(lastHitY, base.transform.position.y, adjustSpeed * Time.deltaTime);
				}
			}
			Vector3 position = base.transform.position;
			position.y = Mathf.Clamp(position.y, Mathf.Min(lastHitY, hitY), float.MaxValue);
			base.transform.position = position;
			utilBone.bone.X = base.transform.localPosition.x;
			utilBone.bone.Y = base.transform.localPosition.y;
			lastHitY = hitY;
		}

		private void OnDrawGizmos()
		{
			Vector3 vector = rayOrigin + rayDir * Mathf.Min(castDistance, rayOrigin.y - hitY);
			Vector3 to = rayOrigin + rayDir * castDistance;
			Gizmos.DrawLine(rayOrigin, vector);
			if (useRadius)
			{
				Gizmos.DrawLine(new Vector3(vector.x - castRadius, vector.y - groundOffset, vector.z), new Vector3(vector.x + castRadius, vector.y - groundOffset, vector.z));
				Gizmos.DrawLine(new Vector3(to.x - castRadius, to.y, to.z), new Vector3(to.x + castRadius, to.y, to.z));
			}
			Gizmos.color = Color.red;
			Gizmos.DrawLine(vector, to);
		}
	}
}
