using UnityEngine;

namespace Scripts.Components
{
	public class AlwaysFaceMainCamera : MonoBehaviour
	{
		private Transform Target;
 
		private void  Update ()
		{
			var wantedPos = Camera.main.WorldToViewportPoint(Target.position);
			transform.position = wantedPos;
		}
	}
}
