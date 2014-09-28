using System.Collections.Generic;
using Scripts.Helpers;
using Scripts.ViewModels;
using Scripts.ViewModels.Enemies;
using UnityEngine;

namespace Scripts.Views
{
    public class LivingObjectView : RigidbodyView
    {
        private static readonly System.Random ProjectileRootIndexRandomizer = new System.Random();

        private readonly LivingObject _viewModel;


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

        protected override void OnDestroy()
        {
            _viewModel.DoAttach -= AttachProjectileToSelf;

            base.OnDestroy();
        }

        private readonly List<Transform> _projectileRooTransform = new List<Transform>();
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
