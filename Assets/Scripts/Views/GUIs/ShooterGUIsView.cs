using Scripts.Helpers;
using Scripts.ViewModels.GUIs;
using UnityEngine;

namespace Scripts.Views.GUIs
{
	public class ShooterGUIView : BaseGUIView
	{
		private readonly ObjectView _parent;
		private readonly ShooterGUI _viewModel;

		private GameObject _aimingGameObject;

		public ShooterGUIView(ShooterGUI viewModel, ObjectView parent)
			: base(viewModel, parent)
		{
			_viewModel = viewModel;
			_parent = parent;
		}

		protected override void OnLoad()
		{
			base.OnLoad();

			_aimingGameObject = _parent.Transform.FindChildRecursivelyBreadthFirst(_viewModel.AimingAssetId).gameObject;

			var targetView = _viewModel.Root.GetView<TargetView>(_viewModel.Shooter.Target);
			targetView.SetupController(_aimingGameObject.GetComponent<UITexture>());

			var sourceView = _viewModel.Root.GetView<ShooterView>(_viewModel.Shooter);
			sourceView.SetupController(GameObject.GetComponent<UITexture>());
		}
	}
}