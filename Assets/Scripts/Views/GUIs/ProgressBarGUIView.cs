using Scripts.ViewModels.GUIs;
using UnityEngine.UI;

namespace Scripts.Views.GUIs
{
	public class ProgressBarGUIView : ValueDisplayGUIView
	{
		private Image _image;

		public ProgressBarGUIView(ProgressBarGUI viewModel, ObjectView parent)
			: base(viewModel, parent)
		{
		}

		protected override void OnLoad()
		{
			base.OnLoad();

			_image = GameObject.GetComponent<Image>();
		}

		protected override void UpdateValueDisplay(float value, float maxValue)
		{
			_image.fillAmount = value / maxValue;
		}
	}
}