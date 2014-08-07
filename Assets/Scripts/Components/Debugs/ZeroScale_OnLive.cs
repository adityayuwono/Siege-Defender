using UnityEngine;

namespace Scripts.Components.Debugs
{
    public class ZeroScale_OnLive : MonoBehaviour
    {
        private void Start()
        {
#if !UNITY_EDITOR
            transform.localScale = Vector3.zero;
#endif
        }
    }
}
