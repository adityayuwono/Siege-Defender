using System.Collections;
using UnityEngine;

namespace Scripts.Components.SpecialEvents
{
    public class BaseSpecialEventController : MonoBehaviour
    {
        public void StartSpecialEvent(EngineBase engine)
        {
            engine.StartCoroutine(StartSpecialEvent());
        }

        protected virtual IEnumerator StartSpecialEvent()
        {
            yield return null;
        }
    }
}
