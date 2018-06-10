using Scripts.ViewModels.GUIs;

namespace Scripts.Views.GUIs
{
	public class LabelGUIView : ElementView
	{
		private readonly LabelGUI _viewModel;

		private UnityEngine.UI.Text _text;

		public LabelGUIView(LabelGUI viewModel, ObjectView parent)
			: base(viewModel, parent)
		{
			_viewModel = viewModel;
		}

		protected override void OnLoad()
		{
			base.OnLoad();

			_text = GameObject.GetComponent<UnityEngine.UI.Text>();
			_viewModel.Text.OnChange += Text_OnChange;
			Text_OnChange();
		}

		private void Text_OnChange()
		{
			_text.color = _viewModel.Color;
			_text.fontSize = _viewModel.Size;
			_text.text = _viewModel.Text.GetValue();
		}
	}
}