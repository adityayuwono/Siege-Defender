using Scripts.Components;
using UnityEngine;
using Object = Scripts.ViewModels.Object;

namespace Scripts.Views
{
	public class RigidbodyView : ObjectView
	{
		private Collider _collider;

		private Rigidbody _rigidbody;

		protected RigidbodyView(Object viewModel, ObjectView parent) : base(viewModel, parent)
		{
		}

		protected override void OnLoad()
		{
			base.OnLoad();

			_collider = GameObject.GetComponent<Collider>();
			if (_collider == null) _collider = GameObject.GetComponentInChildren<Collider>();

			_rigidbody = GameObject.GetComponent<Rigidbody>();
			if (_rigidbody == null) _rigidbody = GameObject.GetComponentInChildren<Rigidbody>();
			if (_rigidbody == null)
			{
				_rigidbody = GameObject.AddComponent<Rigidbody>();
				_rigidbody.isKinematic = true;
			}

			// Recalculate center of mass
			var centerOfMass = Transform.Find("CenterOfMass");
			if (centerOfMass != null) _rigidbody.centerOfMass = centerOfMass.localPosition;
		}

		protected override void OnShow()
		{
			base.OnShow();

			if (_collider != null) _collider.enabled = true;
		}

		protected override void OnHide(string reason)
		{
			if (_collider != null) _collider.enabled = false;

			base.OnHide(reason);
		}

		protected override void OnDestroy()
		{
			_rigidbody = null;
			_collider = null;

			base.OnDestroy();
		}

		protected virtual void AddRelativeForce(float strength, ForceMode forceMode = ForceMode.Impulse)
		{
			GameObject.AddMissingComponent<ProjectileLauncher>().Reset(strength);
		}

		/// <summary>
		///     FREEZE!!!
		/// </summary>
		protected void Freeze(bool isKinematic = false)
		{
			_rigidbody.constraints = RigidbodyConstraints.FreezeAll;
			_rigidbody.isKinematic = isKinematic;
		}
	}
}