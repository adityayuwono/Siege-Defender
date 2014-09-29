using System;
using System.Collections;
using UnityEngine;

namespace Scripts.Components.SpecialEvents
{
    public class BaseSpecialEventController : MonoBehaviour
    {
        public Action OnSpecialEventFinished;

        public void StartSpecialEvent(EngineBase engine)
        {
            engine.StartCoroutine(EnumerateSpecialEvent());
        }

        protected virtual IEnumerator EnumerateSpecialEvent()
        {
            yield return null;
        }
    }
}
