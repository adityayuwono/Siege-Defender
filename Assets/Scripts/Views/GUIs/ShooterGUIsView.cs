using Scripts.Helpers;
using Scripts.ViewModels.GUIs;
using UnityEngine;

namespace Scripts.Views.GUIs
{
    public class ShooterGUIsView : BaseGUIView
    {
        private readonly ShooterGUIs _viewModel;

        public ShooterGUIsView(ShooterGUIs viewModel, ObjectView parent) : base(viewModel, parent)
        {
            _viewModel = viewModel;
        }
    }

    public class ShooterGUIView : BaseGUIView
    {
        private readonly ShooterGUI _viewModel;
        private readonly ObjectView _parent;

        public ShooterGUIView(ShooterGUI viewModel, ObjectView parent) : base(viewModel, parent)
        {
            _viewModel = viewModel;
            _parent = parent;
        }

        private GameObject AimingGameObject;
        
        protected override void OnLoad()
        {
            base.OnLoad();

            AimingGameObject = _parent.Transform.FindChildRecursivelyBreadthFirst(_viewModel.AimingAssetId).gameObject;

            var targetView = _viewModel.Root.GetView<TargetView>(_viewModel.Shooter.Target);
            targetView.SetupController(AimingGameObject.GetComponent<UITexture>());

            var sourceView = _viewModel.Root.GetView<ShooterView>(_viewModel.Shooter);
            sourceView.SetupController(GameObject.GetComponent<UITexture>());
        }
    }
}
