using System;
using System.Collections;
using UnityEngine;

namespace Scripts.Components.SpecialEvents
{
    public class BaseSpecialEventController : MonoBehaviour
    {
        public event Action OnEventStart;
        public event Action OnEventFinished;

        public void StartSpecialEvent(EngineBase engine)
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
                OnEventStart();
        }

        protected void InvokeEventFinished()
        {
            if (OnEventFinished != null)
                OnEventFinished();
        }
    }
}
