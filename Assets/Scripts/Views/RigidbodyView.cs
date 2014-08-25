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

        protected override void OnHide()
        {
            if (_collider != null)
                _collider.enabled = false;

            base.OnHide();
        }

        protected override void OnDestroy()
        {
            _rigidbody = null;
            _collider = null;

            base.OnDestroy();
        }



        protected void AddRelativeForce(Vector3 direction)
        {
            _rigidbody.isKinematic = false;
            _rigidbody.AddRelativeForce(direction, ForceMode.Impulse);
            _rigidbody.AddRelativeTorque(Vector3.right*1.5f, ForceMode.Impulse);
        }

        protected void Freeze()
        {
            _rigidbody.constraints = RigidbodyConstraints.FreezeAll;
            _rigidbody.isKinematic = true;
        }
    }
}
