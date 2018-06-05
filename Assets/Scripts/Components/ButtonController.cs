using System;
using UnityEngine;

namespace Scripts.Components
{
	public class ButtonController : MonoBehaviour
	{
		public Action OnClicked;

		public void OnClick()
		{
			// Yes, it's this simple
			OnClicked();
		}
	}
}