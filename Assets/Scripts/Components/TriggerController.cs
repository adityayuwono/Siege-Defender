using System;
using Scripts.ViewModels;
using UnityEngine;

namespace Scripts.Components.Debugs
{
    public class TriggerController : MonoBehaviour
    {
        public Action<ObjectViewModel> OnCollision; 
        private void OnTriggerEnter(Collider target)
        {
            var viewModelController = target.gameObject.GetComponent<ViewModelController>();
            if (viewModelController != null)
            {
                var viewModel = viewModelController.GetViewModel();
                OnCollision(viewModel);
            }
        }
    }
}
