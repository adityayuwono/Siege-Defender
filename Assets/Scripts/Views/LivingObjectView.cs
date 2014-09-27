using Scripts.Helpers;
using Scripts.ViewModels;
using Scripts.ViewModels.Enemies;
using UnityEngine;

namespace Scripts.Views
{
    public class LivingObjectView : RigidbodyView
    {
        private readonly LivingObject _viewModel;

        public LivingObjectView(LivingObject viewModel, ObjectView parent) : base(viewModel, parent)
        {
            _viewModel = viewModel;
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            _viewModel.DoAttach += AttachProjectileToSelf;
            _projectileRooTransform = Transform.FindChildRecursivelyBreadthFirst("ProjectileRoot");
        }

        protected override void OnDestroy()
        {
            _viewModel.DoAttach -= AttachProjectileToSelf;

            base.OnDestroy();
        }

        private Transform _projectileRooTransform;
        private void AttachProjectileToSelf(ProjectileBase projectile)
        {
            var projectileView = _viewModel.Root.GetView<ProjectileView>(projectile);
            var projectileTransform = projectileView.Transform;
            projectileTransform.parent = _projectileRooTransform;
            projectileTransform.localPosition = Vector3.zero;
        }
    }
}
