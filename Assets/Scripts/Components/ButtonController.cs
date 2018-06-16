using System;
using UnityEngine;

namespace Scripts.Components
{
	public class ButtonController : MonoBehaviour
	{
		public Action OnClicked;

		private float _timeMouseDown;

		public void OnMouseDown()
		{
			_timeMouseDown = Time.time;
		}

		public void OnMouseUp()
		{
			if (Time.time - _timeMouseDown < 1)
			{
				OnClicked();
			}
		}
	}
}