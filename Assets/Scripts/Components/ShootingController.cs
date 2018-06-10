using Scripts.ViewModels;
using UnityEngine;
// ReSharper disable RedundantUsingDirective
using System.Linq; // This is used when iterating inputs, but it's only in Android
// ReSharper restore RedundantUsingDirective

namespace Scripts.Components
{
	/// <summary>
	///     Drag to aim, stay to shoot repeatedly
	/// </summary>
	public class ShootingController : BaseTexturedController
	{
		private Shooter _shooterView;

		protected override void OnSetup()
		{
			base.OnSetup();

			_shooterView = ViewModel as Shooter;
		}

		private void Update()
		{
			var rectTransform = MainTexture.GetComponent<RectTransform>();
#if !UNITY_EDITOR
// If Android
			if (Input.touches.Any(touch => RectTransformUtility.RectangleContainsScreenPoint(rectTransform, touch.position, Camera.main)))
			{
				_shooterView.StartShooting();
			}
			else
			{
				_shooterView.StopShooting();
			}
#else
			var isRectangleContainsMouse = RectTransformUtility.RectangleContainsScreenPoint(rectTransform, Input.mousePosition, Camera.main);
			// If Mouse, for testing purposes only
			if (Input.GetMouseButton(0) && isRectangleContainsMouse)
			{
				_shooterView.StartShooting();
			}
			else
			{
				_shooterView.StopShooting();
			}
#endif
		}
	}
}