using System;
using Scripts.ViewModels;
using UnityEngine;
using Object = Scripts.ViewModels.Object;

namespace Scripts.Components.Debugs
{
    public class TriggerController : MonoBehaviour
    {
        public Action<Object> OnCollision; 
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
