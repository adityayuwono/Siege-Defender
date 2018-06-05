using UnityEngine;

namespace Scripts.Components
{
	public class RotationController : MonoBehaviour
	{
		public Vector3 AxisMultiplier;

		private void Update()
		{
			transform.Rotate(AxisMultiplier);
		}
	}
}