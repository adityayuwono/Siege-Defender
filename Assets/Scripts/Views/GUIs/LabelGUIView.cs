using Scripts.ViewModels.GUIs;
using UnityEngine;

namespace Scripts.Views.GUIs
{
	public class LabelGUIView : ObjectView
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

		protected override void SetPosition()
		{
			if (!_viewModel.IsStatic)
			{
				base.SetPosition();
			}
		}

		protected override Transform GetParent()
		{
			if (!_viewModel.IsStatic)
			{
				return base.GetParent();
			}
			return null;
		}

		private void Text_OnChange()
		{
			_text.color = _viewModel.Color;
			if (_viewModel.Size != 0)
			{
				_text.fontSize = _viewModel.Size;
			}
			_text.text = _viewModel.Text.GetValue();
		}
	}
}