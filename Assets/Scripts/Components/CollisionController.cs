using System;
using Scripts.ViewModels;
using UnityEngine;

namespace Scripts.Components
{
    public class CollisionController : MonoBehaviour
    {
        public Action<ObjectViewModel, Vector3> OnCollision;

        private void OnCollisionEnter(Collision collisionInfo)
        {
            //Debug.LogError(gameObject.name+":"+collisionInfo.gameObject.name);
            if (OnCollision != null)
            {
                var viewModelController = collisionInfo.gameObject.GetComponent<ViewModelController>();
                if (viewModelController != null)
                {
                    var viewModel = viewModelController.GetViewModel();
                    OnCollision(viewModel, transform.position);
                }
            }
        }
    }
}
