using Scripts.ViewModels;
using UnityEngine;

namespace Scripts.Views
{
    public class RigidbodyView : ObjectView
    {
        protected RigidbodyView(ObjectViewModel viewModel, ObjectView parent) : base(viewModel, parent)
        {
        }

        protected Rigidbody Rigidbody;
        protected Collider Collider;
        protected override void OnShow()
        {
            base.OnShow();

            Collider = GameObject.GetComponent<Collider>();
            Rigidbody = GameObject.GetComponent<Rigidbody>();
            
            if (Rigidbody == null)
            {
                Rigidbody = GameObject.AddComponent<Rigidbody>();
                Rigidbody.isKinematic = true;
            }

            var centerOfMass = Transform.FindChild("CenterOfMass");
            if (centerOfMass != null)
                Rigidbody.centerOfMass = centerOfMass.localPosition;
        }
    }
}
