﻿using Scripts.Components;
using Scripts.ViewModels;

namespace Scripts.Views
{
    public class ProjectileBaseView : RigidbodyView
    {
        private readonly ProjectileBaseViewModel _viewModel;
        protected readonly ShooterView ParentShooter;
        protected ProjectileBaseView(ProjectileBaseViewModel viewModel, ShooterView parent) : base(viewModel, parent)
        {
            _viewModel = viewModel;
            ParentShooter = parent;
        }



        private CollisionController _collisionController;
        protected override void OnLoad()
        {
            base.OnLoad();

            _collisionController = GameObject.AddComponent<CollisionController>();
        }

        protected override void OnShow()
        {
            base.OnShow();

            _collisionController.OnCollision += _viewModel.CollideWithTarget;
            _collisionController.enabled = true;
            Transform.parent = null;
        }

        protected override void OnHide(string reason)
        {
            _collisionController.OnCollision -= _viewModel.CollideWithTarget;
            _collisionController.enabled = false;

            base.OnHide(reason);
        }

        protected override void OnDeath(string reason)
        {
            Transform.parent = null;

            base.OnDeath(reason);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
        }
    }
}