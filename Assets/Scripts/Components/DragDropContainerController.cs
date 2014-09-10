using System;
using Scripts.ViewModels;
using UnityEngine;

namespace Scripts.Components
{
    public class DragDropContainerController : UIDragDropContainer
    {
        public Action<ObjectViewModel> OnDropped;

        public void OnDrop(GameObject droppedGameObject)
        {
            var viewModelController = droppedGameObject.GetComponent<ViewModelController>();
            if (viewModelController != null)
            {
                OnDropped(viewModelController.GetViewModel());
                droppedGameObject.transform.localPosition = Vector3.zero;
            }
        }
    }
}
