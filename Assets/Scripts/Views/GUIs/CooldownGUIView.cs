using Scripts.ViewModels.GUIs;
using UnityEngine.UI;

namespace Scripts.Views.GUIs
{
	public class CooldownGUIView : ValueDisplayGUIView
	{
		private readonly CooldownGUI _viewModel;

		private Image _image;

		public CooldownGUIView(CooldownGUI viewModel, ObjectView parent)
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