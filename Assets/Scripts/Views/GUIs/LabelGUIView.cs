using Scripts.ViewModels.GUIs;

namespace Scripts.Views.GUIs
{
	public class LabelGUIView : ObjectView
	{
		private readonly LabelGUI _viewModel;

		private UILabel _uiLabel;

		public LabelGUIView(LabelGUI viewModel, ObjectView parent) : base(viewModel, parent)
		{
			_viewModel = viewModel;
		}

		protected override void OnLoad()
		{
			base.OnLoad();

			_uiLabel = GameObject.GetComponent<UILabel>();
			_viewModel.Text.OnChange += Text_OnChange;
			Text_OnChange();
		}

		private void Text_OnChange()
		{
			_uiLabel.color = _viewModel.Color;
			_uiLabel.text = _viewModel.Text.GetValue();
		}
	}
}