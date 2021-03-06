using System;
using UnityEngine;
using Object = Scripts.ViewModels.Object;

namespace Scripts.Components
{
	public class TriggerController : MonoBehaviour
	{
		public Action<Object> Collision;

		private void OnTriggerEnter(Collider target)
		{
			var viewModelController = target.gameObject.GetComponent<ViewModelController>();
			if (viewModelController != null)
			{
				var viewModel = viewModelController.GetViewModel();
				Collision(viewModel);
			}
		}
	}
}