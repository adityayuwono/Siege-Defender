using System;
using System.Collections;
using Scripts.Interfaces;
using UnityEngine;

namespace Scripts.Components.SpecialEvents
{
	public class BaseSpecialEventController : MonoBehaviour
	{
		public event Action OnEventStart;
		public event Action OnEventFinished;

		public void StartSpecialEvent(IRoot engine)
		{
			engine.StartCoroutine(EnumerateSpecialEvent());
		}

		protected virtual IEnumerator EnumerateSpecialEvent()
		{
			yield return null;
		}

		protected void InvokeEventStart()
		{
			if (OnEventStart != null)
			{
				OnEventStart();
			}
		}

		protected void InvokeEventFinished()
		{
			if (OnEventFinished != null)
			{
				OnEventFinished();
			}
		}
	}
}