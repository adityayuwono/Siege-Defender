using Scripts.ViewModels.GUIs;
using UnityEngine;

namespace Scripts.Views.GUIs
{
	public class LabelView : StaticView
	{
		private readonly Label _viewModel;

		private UnityEngine.UI.Text _text;

		public LabelView(Label viewModel, ObjectView parent)
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
			if (_viewModel.Color != Color.clear)
			{
				_text.color = _viewModel.Color;
			}
			if (_viewModel.Size != 0)
			{
				_text.fontSize = _viewModel.Size;
			}
			_text.text = _viewModel.Text.GetValue();
		}
	}
}