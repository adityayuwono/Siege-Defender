﻿using System.Collections;
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
            _viewModel.OnDamaged += DisplayDamage;
        }

        


        private Animator _animator;
        
        protected override void OnLoad()
        {
            base.OnLoad();
            _projectileRooTransform = Transform.FindChildRecursivelyBreadthFirst("ProjectileRoot");
            _animator = GameObject.GetComponent<Animator>();
        }
        protected override void OnShow()
        {
            base.OnShow();

            _isDead = false;
            _animator.SetBool("IsDead", false);

            Transform.position = _parent.GetRandomSpawnPoint();
            Transform.localEulerAngles = new Vector3(0, 180f + Random.Range(-_viewModel.Rotation, _viewModel.Rotation), 0);
            _viewModel.Root.StartCoroutine(Walking());
        }

        protected override void OnHide(string reason)
        {
            _isDead = true;
            _animator.SetBool("IsDead", true);

            base.OnHide(reason);
        }



        private void DisplayDamage(float damage)
        {
            // TODO: Display Damage
            var damageToDisplay = Mathf.RoundToInt(damage);
        }

        private bool _isDead;
        private IEnumerator Walking()
        {
            while (!_isDead)
            {
                Transform.localPosition += Transform.forward * Time.deltaTime * _viewModel.Speed;
                yield return null;
            }
        }

        private Transform _projectileRooTransform;
        private void AttachProjectileToSelf(ProjectileBaseViewModel projectile)
        {
            var projectileView = _viewModel.Root.GetView<ProjectileView>(projectile.Id);
            var projectileTransform = projectileView.Transform;
            projectileTransform.parent = _projectileRooTransform;
            projectileTransform.localPosition = Vector3.zero;
        }
    }
}