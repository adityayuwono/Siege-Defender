using Scripts.ViewModels.GUIs;

namespace Scripts.Views.GUIs
{
	public class CooldownGUIView : ValueDisplayGUIView
	{
		private readonly CooldownGUI _viewModel;

		private UITexture _uiTexture;

		public CooldownGUIView(CooldownGUI viewModel, ObjectView parent) : base(viewModel, parent)
		{
			_viewModel = viewModel;
		}

		protected override void OnLoad()
		{
			base.OnLoad();

			_uiTexture = GameObject.GetComponent<UITexture>();
		}

		protected override void UpdateValueDisplay(float value, float maxValue)
		{
			_uiTexture.fillAmount = value / maxValue;
		}
	}
}