using UnityEngine;

namespace Scripts.Components
{
	public class GizmoHelper : MonoBehaviour
	{
		public enum GizmoType
		{
			Box,
			Sphere
		}

		public Color Color = new Color(0, 1f, 0, 0.25f);
		public bool OverrideCollider = false;
		public float Radius;
		public Vector3 Size;

		public GizmoType Type = GizmoType.Box;

		private void OnDrawGizmos()
		{
			Gizmos.color = Color;
			var collider = GetComponent<Collider>();
			if (collider == null || OverrideCollider)
			{
				if (Type == GizmoType.Sphere)
					Gizmos.DrawSphere(transform.position, Radius);
				else if (Type == GizmoType.Box)
					Gizmos.DrawCube(transform.position, Size);
			}
			else if (collider != null)
			{
				if (collider is BoxCollider)
				{
					var boxCollider = collider as BoxCollider;
					Gizmos.DrawCube(transform.position + boxCollider.center, boxCollider.size);
				}
				else if (collider is SphereCollider)
				{
					var sphereCollider = collider as SphereCollider;
					Gizmos.DrawSphere(transform.position + sphereCollider.center, sphereCollider.radius);
				}
			}
		}
	}
}