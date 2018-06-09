using Scripts.Components;
using Scripts.ViewModels;
using UnityEngine.UI;

namespace Scripts.Views
{
	public class TargetView : ObjectView
	{
		private readonly Target _viewModel;

		public TargetView(Target viewModel, ObjectView parent) : base(viewModel, parent)
		{
			_viewModel = viewModel;
		}

		public void SetupController(Image image)
		{
			var followMouseController = GameObject.AddComponent<AimingController>();
			followMouseController.MainTexture = image;
			followMouseController.Setup(_viewModel);
		}
	}
}