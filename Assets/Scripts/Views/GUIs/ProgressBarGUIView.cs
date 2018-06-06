using Scripts.ViewModels.GUIs;

namespace Scripts.Views.GUIs
{
	public class ProgressBarGUIView : ValueDisplayGUIView
	{
		private readonly ProgressBarGUI _viewModel;

		private UIProgressBar _uiProgressBar;

		public ProgressBarGUIView(ProgressBarGUI viewModel, ObjectView parent)
			: base(viewModel, parent)
		{
			_viewModel = viewModel;
		}

		protected override void OnLoad()
		{
			base.OnLoad();

			_uiProgressBar = GameObject.GetComponent<UIProgressBar>();
		}

		protected override void UpdateValueDisplay(float value, float maxValue)
		{
			_uiProgressBar.value = value / maxValue;
		}
	}
}