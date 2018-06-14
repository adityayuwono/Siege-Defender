using Scripts.Extensions;
using Scripts.ViewModels.GUIs;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Views.GUIs
{
	public class ShooterView : BaseGUIView
	{
		private readonly ObjectView _parent;
		private readonly ShooterGUI _viewModel;

		private GameObject _aimingGameObject;

		public ShooterView(ShooterGUI viewModel, ObjectView parent)
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
			targetView.SetupController(_aimingGameObject.GetComponent<Image>());

			var sourceView = _viewModel.Root.GetView<Views.ShooterView>(_viewModel.Shooter);
			sourceView.SetupController(GameObject.GetComponent<Image>());
		}
	}
}