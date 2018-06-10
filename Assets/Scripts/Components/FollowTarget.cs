using UnityEngine;

namespace Scripts.Components
{
	public class FollowTarget : MonoBehaviour
	{
		public Transform Target;
 
		private void  Update ()
		{
			if (Target != null)
			{
				transform.position = Target.position;
			}
		}
	}
}
