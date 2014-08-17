using System;
using Scripts.ViewModels;
using UnityEngine;

namespace Scripts.Components
{
    public class CollisionController : MonoBehaviour
    {
        public Action<ObjectViewModel> OnCollision;

        private void OnCollisionEnter(Collision collisionInfo)
        {
            if (OnCollision != null)
            {
                var viewModelController = collisionInfo.gameObject.GetComponent<ViewModelController>();
                if (viewModelController != null)
                {
                    var viewModel = viewModelController.GetViewModel();
                    OnCollision(viewModel);
                }
            }
        }
    }
}
