using Scripts.Helpers;
using Scripts.ViewModels.GUIs;
using UnityEngine;

namespace Scripts.Views.GUIs
{
    public class ShooterGUIView : BaseGUIView
    {
        private readonly ShooterGUI _viewModel;
        private readonly ObjectView _parent;

        public ShooterGUIView(ShooterGUI viewModel, ObjectView parent) : base(viewModel, parent)
        {
            _viewModel = viewModel;
            _parent = parent;
        }

        private GameObject _aimingGameObject;
        
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
