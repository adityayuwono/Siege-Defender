using UnityEngine;
using Object = Scripts.ViewModels.Object;

namespace Scripts.Components
{
	public class DragDropContainerController : MonoBehaviour
	{
		public event System.Action<Object> OnDropped;

		public void OnDrop(GameObject droppedGameObject)
		{
			var viewModelController = droppedGameObject.GetComponent<ViewModelController>();
			if (viewModelController != null)
			{
				OnDropped(viewModelController.GetViewModel());
			}
		}
	}
}