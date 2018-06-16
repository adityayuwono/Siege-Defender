﻿using Scripts.ViewModels;
using Scripts.ViewModels.Items;
using UnityEngine;

namespace Scripts.Components.UI
{
	public class DragObject : MonoBehaviour
	{
		public bool IsDragMode { get; set; }

		private DragDropContainerController _dragDropContainerController;

		private Item Item
		{
			get { return (Item)GetComponent<ViewModelController>().ViewModel; }
		}

		private void OnMouseDown()
		{
			IsDragMode = true;
		}

		private void OnMouseUp()
		{
			IsDragMode = false;
			if (_dragDropContainerController != null)
			{
				_dragDropContainerController.OnDrop(gameObject);
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
			_dragDropContainerController = coll.GetComponent<DragDropContainerController>();
		}

		private void OnTriggerExit2D(Collider2D coll)
		{
			_dragDropContainerController = null;
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
