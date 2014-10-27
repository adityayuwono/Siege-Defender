using System;
using UnityEngine;
using Object = Scripts.ViewModels.Object;

namespace Scripts.Components
{
    public class DragDropContainerController : UIDragDropContainer
    {
        public event Action<Object> OnDropped;

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
