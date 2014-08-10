using UnityEngine;

namespace Scripts.Components
{
    /// <summary>
    /// Controls Camera according to tilt and orientation of the device
    /// </summary>
    public class AccelerometerController : BaseController
    {
        // We need to keep track of the rotation on our own in Unity3d
        private float _yRotationAngle = 0f;

        private void Update()
        {
            var xClamped = -Input.acceleration.z/2f;
            var xAngle = 35f + (xClamped*30f);

            // Between 90 to the left, and 90 to the right
            var yClamped = Input.acceleration.x*90f;
            _yRotationAngle += (yClamped - _yRotationAngle)*Time.deltaTime;
            
            transform.eulerAngles += (new Vector3(xAngle, 0, 0) - transform.eulerAngles) * Time.deltaTime;
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, _yRotationAngle, transform.eulerAngles.z);
        }
    }
}
