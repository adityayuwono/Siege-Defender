using System;
using System.Collections;
using UnityEngine;

namespace Scripts.Components
{
	public class ButtonController : MonoBehaviour
	{
		public Action OnClicked;

		public IEnumerator OnMouseUpAsButton()
		{
			yield return null;
			OnClicked();
		}
	}
}