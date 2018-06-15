using Scripts.ViewModels.GUIs;
using UnityEngine.UI;

namespace Scripts.Views.GUIs
{
	public class ProgressBarView : PercentageView
	{
		private Image _image;

		public ProgressBarView(ProgressBar viewModel, ObjectView parent)
			: base(viewModel, parent)
		{
		}

		protected override void UpdateValueDisplay(float value, float maxValue)
		{
			Image.fillAmount = value / maxValue;
		}
	}
}