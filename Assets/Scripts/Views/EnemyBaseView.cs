using System.Collections;
using Scripts.Helpers;
using Scripts.ViewModels;
using UnityEngine;

namespace Scripts.Views
{
    public class EnemyBaseView : RigidbodyView
    {
        private readonly EnemyBaseViewModel _viewModel;
        private readonly EnemyManagerView _parent;

        public EnemyBaseView(EnemyBaseViewModel viewModel, EnemyManagerView parent) : base(viewModel, parent)
        {
            _viewModel = viewModel;
            _parent = parent;

            _viewModel.DoAttach += AttachProjectileToSelf;
        }

        private Animator Animator;
        protected override void OnShow()
        {
            base.OnShow();

            GameObject.transform.position = _parent.GetRandomSpawnPoint();
            _projectileRooTransform = Transform.FindChildRecursivelyBreadthFirst("ProjectileRoot");

            Animator = GameObject.GetComponent<Animator>();

            _viewModel.Root.StartCoroutine(Walking());
        }

        private bool _isDead;
        private IEnumerator Walking()
        {
            while (!_isDead)
            {
                Transform.localPosition += Vector3.back * Time.deltaTime * _viewModel.Speed;
                yield return null;
            }
        }

        private Transform _projectileRooTransform;
        private void AttachProjectileToSelf(ProjectileBaseViewModel projectile)
        {
            var projectileView = _viewModel.Root.GetView<ProjectileView>(projectile.Id);
            if (projectileView != null)
            {
                var projectileTransform = projectileView.Transform;
                projectileTransform.parent = _projectileRooTransform;
                projectileTransform.localPosition = Vector3.zero;
            }
        }

        protected override void OnDestroyGameObject()
        {
            Collider.enabled = false;
            _isDead = true;
            Animator.SetBool("IsDead", true);
            base.OnDestroyGameObject();
        }
    }
}