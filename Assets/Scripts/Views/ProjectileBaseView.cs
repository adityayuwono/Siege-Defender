using Scripts.Components;
using Scripts.ViewModels;
using UnityEngine;

namespace Scripts.Views
{
    public class ProjectileBaseView : RigidbodyView
    {
        private readonly ProjectileBaseViewModel _viewModel;
        protected ProjectileBaseView(ProjectileBaseViewModel viewModel, ObjectView parent) : base(viewModel, parent)
        {
            _viewModel = viewModel;
        }



        private CollisionController _collisionController;
        protected override void OnLoad()
        {
            base.OnLoad();

            _collisionController = GameObject.AddComponent<CollisionController>();
            _collisionController.OnCollision += _viewModel.CollideWithTarget;
        }

        protected override void OnShow()
        {
            base.OnShow();

            _collisionController.enabled = true;

            Transform.localPosition = Vector3.zero;
            Transform.parent = null;
        }

        protected override void OnHide()
        {
            _collisionController.enabled = false;

            base.OnHide();
        }

        protected override void OnDeath()
        {
            Transform.parent = null;

            base.OnDeath();
        }

        protected override void OnDestroy()
        {
            _collisionController.OnCollision -= _viewModel.CollideWithTarget;

            base.OnDestroy();
        }
    }
}
