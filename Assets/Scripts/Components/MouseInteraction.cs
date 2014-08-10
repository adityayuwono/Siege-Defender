using UnityEngine;

namespace Scripts.Components
{
    public class MouseInteraction : BaseController
    {
        private void Update()
        {
            if (Input.touches.Length > 0)
            {
                foreach (var touch in Input.touches)
                {
                    ProcessTouchOrMouse(touch.position);
                }
            }
            else
            {
                ProcessTouchOrMouse(Input.mousePosition);
            }
        }

        protected virtual void ProcessTouchOrMouse(Vector2 inputPosition)
        {
            
        }
    }
}
