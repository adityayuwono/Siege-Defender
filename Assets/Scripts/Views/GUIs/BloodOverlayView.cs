using Scripts.ViewModels.GUIs;
using UnityEngine;

namespace Scripts.Views.GUIs
{
	public class BloodOverlayView : PercentageView
	{
		private Color _defaultColor = new Color(1,1,1,0);
		public BloodOverlayView(BloodOverlay viewModel, ObjectView parent)
			: base(viewModel, parent)
		{
		}

		protected override void OnLoad()
		{
			base.OnLoad();

			Image.color = _defaultColor;
		}

		protected override void UpdateValueDisplay(float value, float maxValue)
		{
			var color = Image.color;
			color.a += 0.3f;
			Image.color = color;
			Image.CrossFadeAlpha(1f, 0f, false);
			Image.CrossFadeAlpha(0f, 3f, false);
		}
	}
}
