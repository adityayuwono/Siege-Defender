using System;
using UnityEngine;

namespace Scripts.Components
{
	public class ButtonController : MonoBehaviour
	{
		public Action OnClicked;

		public void OnMouseUp()
		{
#if !UNITY_EDITOR
			if (Input.touches.Length == 1)
#endif
			{
				OnClicked();
			}
		}
	}
}