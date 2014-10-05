using UnityEngine;

namespace Scripts.Components
{
    public class GizmoHelper : MonoBehaviour
    {
        public bool OverrideCollider = false;

        public Color Color = new Color(0,1f,0,0.25f);
        public float Radius;
        public Vector3 Size; 

        public GizmoType Type = GizmoType.Box;
        public enum GizmoType
        {
            Box,
            Sphere
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color;

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
                else if (collider is  SphereCollider)
                {
                    var sphereCollider = collider as SphereCollider;
                    Gizmos.DrawSphere(transform.position + sphereCollider.center, sphereCollider.radius);
                }
            }
        }
    }
}
