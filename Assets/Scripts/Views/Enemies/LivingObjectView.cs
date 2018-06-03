using System.Collections.Generic;
using Scripts.Helpers;
using Scripts.ViewModels;
using Scripts.ViewModels.Enemies;
using Scripts.ViewModels.Weapons;
using Scripts.Views.Weapons;
using UnityEngine;

namespace Scripts.Views
{
    public class LivingObjectView : RigidbodyView
    {
        private static readonly System.Random ProjectileRootIndexRandomizer = new System.Random();

        private readonly LivingObject _viewModel;
	    private readonly List<Transform> _projectileRooTransform = new List<Transform>();

        public LivingObjectView(LivingObject viewModel, ObjectView parent) : base(viewModel, parent)
        {
            _viewModel = viewModel;
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            _viewModel.DoAttach += AttachProjectileToSelf;
            var projectileRoot = Transform.FindChildRecursivelyBreadthFirst("ProjectileRoot");
            if (projectileRoot == null)
            {
            }
            else
            {
                if (projectileRoot.childCount > 0)
                {
                    for (int i = 0; i < projectileRoot.childCount; i++)
                    {
                        var rootChild = projectileRoot.GetChild(i);
                        _projectileRooTransform.Add(rootChild);
                    }
                }
                _projectileRooTransform.Add(projectileRoot);
            }
        }

        protected override void OnShow()
        {
            base.OnShow();

            _viewModel.SpecialEffect.OnChange += SpecialEffect_OnChange;
        }

        protected override void OnHide(string reason)
        {
            _viewModel.SpecialEffect.OnChange -= SpecialEffect_OnChange;

            base.OnHide(reason);
        }

        protected override void OnDestroy()
        {
            _viewModel.DoAttach -= AttachProjectileToSelf;

            base.OnDestroy();
        }

        private void SpecialEffect_OnChange()
        {
            _viewModel.Root.SpecialEffectManager.DisplaySpecialEffect(_viewModel.SpecialEffect.GetValue(), _viewModel);
        }

        private void AttachProjectileToSelf(ProjectileBase projectile)
        {
            var projectileView = _viewModel.Root.GetView<ProjectileView>(projectile);
            var projectileTransform = projectileView.Transform;

            if (_projectileRooTransform.Count > 0)
            {
                projectileTransform.parent = _projectileRooTransform[ProjectileRootIndexRandomizer.Next(_projectileRooTransform.Count)];
                projectileTransform.localPosition = Vector3.zero;
            }
            else
            {
                projectileTransform.parent = Transform;
            }
        }
    }
}
