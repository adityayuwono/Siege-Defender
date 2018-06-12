using Scripts.ViewModels.GUIs;
using UnityEngine;

namespace Scripts.Views.GUIs
{
	public class StaticGUIView : ObjectView
	{
		private readonly StaticGUI _viewModel;
		public StaticGUIView(StaticGUI viewModel, ObjectView parent) : base(viewModel, parent)
		{
			_viewModel = viewModel;
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
	}
}
