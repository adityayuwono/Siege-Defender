using UnityEngine;

namespace Scripts.Components
{
	/// <summary>
	///     Controls Camera according to tilt and orientation of the device
	/// </summary>
	public class AccelerometerController : BaseController
	{
		// We need to keep track of the rotation on our own in Unity3d
		private float _xRotationAngle;
		private float _yRotationAngle;

		private void Update()
		{
			var xClamped = -Input.acceleration.z / 2f;
			var xAngle = /*27.5f+*/ (xClamped * 30f) - 7.5f;

			// Between 90 to the left, and 90 to the right
			var yClamped = Input.acceleration.x * 90f;
			_yRotationAngle += (yClamped - _yRotationAngle) * Time.deltaTime;

			_xRotationAngle += (xAngle - _xRotationAngle) * Time.deltaTime;
			transform.eulerAngles = new Vector3(_xRotationAngle, _yRotationAngle, transform.eulerAngles.z);
		}

		private void OnGUI()
		{
			GUI.Label(new Rect(0, 35, 100, 100), Input.acceleration + "\n" + transform.eulerAngles);
		}
	}
}