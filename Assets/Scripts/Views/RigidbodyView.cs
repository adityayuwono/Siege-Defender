using Scripts.ViewModels;
using UnityEngine;

namespace Scripts.Views
{
    public class RigidbodyView : ObjectView
    {
        protected RigidbodyView(ObjectViewModel viewModel, ObjectView parent) : base(viewModel, parent)
        {
        }

        private Rigidbody _rigidbody;
        private Collider _collider;
        


        protected override void OnLoad()
        {
            base.OnLoad();

            _collider = GameObject.GetComponent<Collider>();
            _rigidbody = GameObject.GetComponent<Rigidbody>();

            if (_rigidbody == null)
            {
                _rigidbody = GameObject.AddComponent<Rigidbody>();
                _rigidbody.isKinematic = true;
            }

            var centerOfMass = Transform.FindChild("CenterOfMass");
            if (centerOfMass != null)
                _rigidbody.centerOfMass = centerOfMass.localPosition;
        }

        protected override void OnShow()
        {   
            base.OnShow();

            if (_collider != null)
                _collider.enabled = true;
        }

        protected override void OnHide(string reason)
        {
            if (_collider != null)
                _collider.enabled = false;

            base.OnHide(reason);
        }

        protected override void OnDestroy()
        {
            _rigidbody = null;
            _collider = null;

            base.OnDestroy();
        }

        protected virtual void AddRelativeForce(Vector3 direction, ForceMode forceMode = ForceMode.Impulse)
        {
            // Reset parameters to makes sure we have a fresh RigidBody
            _rigidbody.isKinematic = false;
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;

            _rigidbody.AddRelativeForce(direction, forceMode);
            _rigidbody.AddRelativeTorque(Vector3.right * 1.5f, forceMode);
        }

        protected void Freeze(bool isKinematic = false)
        {
            _rigidbody.constraints = RigidbodyConstraints.FreezeAll;
            _rigidbody.isKinematic = isKinematic;
        }
    }
}
