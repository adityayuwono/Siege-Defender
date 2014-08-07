using System.Collections;
using System.Collections.Generic;
using Scripts.ViewModels;
using UnityEngine;

namespace Scripts.Components
{
    public class EnemyBaseController : ProjectileTargetController
    {
        public string Id
        {
            get { return View.Id; }
        }

        private EnemyBaseViewModel _viewModel;
        private Animator _animator;


        protected override void OnSetup()
        {
            base.OnSetup();

            _viewModel = View as EnemyBaseViewModel;
            _animator = GetComponent<Animator>();

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

            foreach (var projectile in _projectiles)
            {
                Destroy(projectile.gameObject);
            }

            StartCoroutine(DelayedDeath(1f));
        }

        private readonly List<Transform> _projectiles = new List<Transform>();
        public void AttachProjectile(Transform projectile)
        {
            projectile.parent = transform;
            projectile.localPosition = Vector3.up;
            _projectiles.Add(projectile);
        }
    }
}
