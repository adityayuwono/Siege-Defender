using Scripts.ViewModels.GUIs;
using UnityEngine.UI;

namespace Scripts.Views.GUIs
{
	public class ProgressBarGUIView : ValueDisplayGUIView
	{
		private readonly ProgressBarGUI _viewModel;

		private Image _image;

		public ProgressBarGUIView(ProgressBarGUI viewModel, ObjectView parent)
			: base(viewModel, parent)
		{
			_viewModel = viewModel;
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