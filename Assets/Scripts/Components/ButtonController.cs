using System;
using UnityEngine;
// ReSharper disable once RedundantUsingDirective
using UnityEngine.EventSystems;

namespace Scripts.Components
{
	public class ButtonController : MonoBehaviour
	{
		public Action OnClicked;

		public void OnMouseUp()
		{
#if !UNITY_EDITOR
			if (Input.touches.Length == 1)
			{
				if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
				{
#endif
					OnClicked();
#if !UNITY_EDITOR
				}
			}
#endif
		}
	}
}