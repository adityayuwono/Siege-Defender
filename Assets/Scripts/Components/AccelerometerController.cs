using UnityEngine;

namespace Scripts.Components
{
    /// <summary>
    /// Controls Camera according to tilt and orientation of the device
    /// </summary>
    public class AccelerometerController : BaseController
    {
        private float _yRotationAngle = 0f;
        private void Update()
        {
            var xClamped = -Input.acceleration.z/2f;
            var xAngle = 45 + (xClamped*10f);

            var yClamped = Input.acceleration.x*75f;
            _yRotationAngle += (yClamped - _yRotationAngle)*Time.deltaTime;
            
            transform.eulerAngles += (new Vector3(xAngle, 0, 0) - transform.eulerAngles) * Time.deltaTime;
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, _yRotationAngle, transform.eulerAngles.z);
        }
    }
}
