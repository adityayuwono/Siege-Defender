using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Scripts.Components.UI
{
	public class DragObject : MonoBehaviour
	{
		public bool IsDragMode { get; set; }

		private readonly List<DragDropContainerController> _dragDropContainerControllers = new List<DragDropContainerController>();

		private void OnMouseDown()
		{
			IsDragMode = true;
		}

		private void OnMouseUp()
		{
			IsDragMode = false;
			var dragDropContainerController = _dragDropContainerControllers.FirstOrDefault();
			if (dragDropContainerController != null)
			{
				dragDropContainerController.OnDrop(gameObject);
			}
			else
			{
				var parentTable = transform.parent.GetComponent<Table>();
				if (parentTable != null)
				{
					parentTable.Reposition();
				}
			}
		}

		private void OnTriggerEnter2D(Collider2D coll)
		{
			var dragDropContainerController = coll.GetComponent<DragDropContainerController>();

			if (!_dragDropContainerControllers.Contains(dragDropContainerController))
			{
				_dragDropContainerControllers.Add(dragDropContainerController);
			}
		}

		private void OnTriggerExit2D(Collider2D coll)
		{
			var dragDropContainerController = coll.GetComponent<DragDropContainerController>();

			if (_dragDropContainerControllers.Contains(dragDropContainerController))
			{
				_dragDropContainerControllers.Remove(dragDropContainerController);
			}
		}

		private void Update()
		{
			if (IsDragMode)
			{
				UpdateCardPositionForDragging();
			}
		}

		private void UpdateCardPositionForDragging()
		{
			var screenPoint = Input.mousePosition;
			screenPoint.y -= 10;

			var dragPosition = Camera.main.ScreenToWorldPoint(screenPoint);
			transform.position = new Vector3(dragPosition.x, dragPosition.y, transform.position.z);
		}
	}
}
