using Scripts.Helpers;
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
		private Rect _clickCheckArea;
		private Shooter _shooterView;

		protected override void OnSetup()
		{
			base.OnSetup();

			_shooterView = ViewModel as Shooter;
		}

		protected override void OnChange()
		{
			base.OnChange();

			_clickCheckArea = TextureScreenArea;
			// Reverse the height, because Mouse position is 0 when at bottom
			_clickCheckArea.y = Screen.height - _clickCheckArea.y - TextureScreenArea.height;
		}

		private void Update()
		{
#if !UNITY_EDITOR
// If Android
                if (Input.touches.Any(touch => _clickCheckArea.Contains(touch.position)))
                    _shooterView.StartShooting();
                else
                    _shooterView.StopShooting();
#else
			// If Mouse, for testing purposes only
			if (Input.GetMouseButton(0) && _clickCheckArea.ContainsIfThisIsACircle(Input.mousePosition))
				_shooterView.StartShooting();
			else
				_shooterView.StopShooting();
#endif
		}
	}
}