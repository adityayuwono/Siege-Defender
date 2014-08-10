using UnityEngine;

namespace Scripts.Components
{
    public class RigidbodyController : BaseController
    {
        protected Rigidbody Rigidbody;

        protected override void OnSetup()
        {
            if (rigidbody == null)
            {
                Rigidbody = gameObject.AddComponent<Rigidbody>();
                Rigidbody.isKinematic = true;
            }
            else
                Rigidbody = rigidbody;

            var centerOfMass = transform.FindChild("CenterOfMass");
            if (centerOfMass != null)
                rigidbody.centerOfMass = centerOfMass.localPosition;
        }
    }
}
