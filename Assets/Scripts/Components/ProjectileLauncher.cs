using UnityEngine;

namespace Scripts.Components
{
	public class ProjectileLauncher : MonoBehaviour
	{
		private float _duration = 0.03f;
		private Rigidbody _rigidbody;
		private float _strength;

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

		private void FixedUpdate()
		{
			if (_duration > 0)
			{
				_duration -= Time.deltaTime;
				_rigidbody.AddRelativeForce(Vector3.forward * _strength / 5f, ForceMode.Impulse);
			}
		}
	}
}