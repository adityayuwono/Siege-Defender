using UnityEngine;

namespace Scripts.Components
{
	public class ProjectileLauncher : MonoBehaviour
	{
		private Rigidbody _rigidbody;
		private float _duration = 0.1f;
		private float _strength;
		private void FixedUpdate()
		{
			if (_duration > 0)
			{
				_duration -= Time.deltaTime;
				// Reset parameters to makes sure we have a fresh RigidBody
				_rigidbody.AddRelativeForce(Vector3.forward * _strength/5f, ForceMode.Impulse);
			}
		}

		public void Reset(float strength)
		{
			_rigidbody = GetComponent<Rigidbody>();
			_rigidbody.isKinematic = false;
			_rigidbody.velocity = Vector3.zero;
			_rigidbody.angularVelocity = Vector3.zero;
			;
			_strength = strength;
			_duration = 0.1f;
		}
	}
}
