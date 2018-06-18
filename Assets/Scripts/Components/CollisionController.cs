using System;
using UnityEngine;
using Object = Scripts.ViewModels.Object;

namespace Scripts.Components
{
	public class CollisionController : MonoBehaviour
	{
		public event Action<Object, Vector3, Vector3> OnCollision;

		private void OnCollisionEnter(Collision collisionInfo)
		{
			if (OnCollision != null)
			{
				var viewModelController = collisionInfo.gameObject.GetComponent<ViewModelController>();
				if (viewModelController != null)
				{
					var viewModel = viewModelController.GetViewModel();
					OnCollision(viewModel, transform.position, collisionInfo.contacts[0].point);
				}
			}
		}

		private void OnTriggerEnter(Collider target)
		{
			var collisionPosition = target.transform.position;
			if (OnCollision != null)
			{
				var viewModelController = target.gameObject.GetComponent<ViewModelController>();

				if (viewModelController != null)
				{
					if (collisionPosition.y < 0)
					{
						collisionPosition.y *= -1;
					}

					var viewModel = viewModelController.GetViewModel();
					OnCollision(viewModel, transform.position, collisionPosition);
				}
			}
		}
	}
}