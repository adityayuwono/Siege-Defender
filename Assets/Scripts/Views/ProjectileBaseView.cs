using Scripts.Components;
using Scripts.ViewModels;
using Scripts.Views;
using UnityEngine;

namespace Assets.Scripts.Views
{
    public class ProjectileBaseView : RigidbodyView
    {
        private readonly ProjectileBaseViewModel _viewModel;
        protected ProjectileBaseView(ProjectileBaseViewModel viewModel, ObjectView parent) : base(viewModel, parent)
        {
            _viewModel = viewModel;
        }

        private CollisionController _collisionController;
        protected override void OnShow()
        {
            base.OnShow();

            _collisionController = GameObject.AddComponent<CollisionController>();
            _collisionController.OnCollision += _viewModel.CollideWithTarget;

            Transform.localPosition = Vector3.zero;
            Transform.parent = null;
        }

        protected override void OnHide()
        {
            Object.Destroy(_collisionController);
            _collisionController.OnCollision -= _viewModel.CollideWithTarget;

            base.OnHide();
        }
    }
}
