﻿using System.Collections;
using System.Collections.Generic;
using Scripts.ViewModels;
using UnityEngine;

namespace Scripts.Components
{
    public class EnemyBaseController : ProjectileTargetController
    {
        private EnemyBaseViewModel _viewModel;
        private Animator _animator;
        public Transform ProjectileRooTransform;

        protected override void OnSetup()
        {
            base.OnSetup();

            _viewModel = ViewModel as EnemyBaseViewModel;
            _animator = GetComponent<Animator>();

            ProjectileRooTransform = transform.FindChild("ProjectileRoot");

            StartCoroutine(Walking());
        }



        private IEnumerator Walking()
        {
            while (!IsDead)
            {
                transform.localPosition += Vector3.back * Time.deltaTime * _viewModel.Speed;
                yield return null;
            }
        }
        


        protected override void OnKilled()
        {
            collider.enabled = false;
            _animator.SetBool("IsDead", true);

            foreach (var projectileController in _projectiles)
                projectileController.OnDeath();

            StartCoroutine(DelayedDeath(2f));
        }

        private readonly List<ProjectileController> _projectiles = new List<ProjectileController>();
        public void AttachProjectile(ProjectileController projectile)
        {
            if (ProjectileRooTransform != null)
            {
                var projectileTransform = projectile.transform;
                projectileTransform.parent = ProjectileRooTransform;
                projectileTransform.localPosition = Vector3.zero;
            }
            _projectiles.Add(projectile);
        }
    }
}
