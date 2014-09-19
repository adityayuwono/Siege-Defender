using Scripts.Helpers;
using Scripts.ViewModels;
using UnityEngine;

namespace Scripts.Views
{
    public class EnemyBaseView : RigidbodyView
    {
        private readonly EnemyBase _viewModel;
        private readonly EnemyManagerView _parent;

        public EnemyBaseView(EnemyBase viewModel, EnemyManagerView parent) : base(viewModel, parent)
        {
            _viewModel = viewModel;
            _parent = parent;

            _viewModel.AnimationId.OnChange += Animation_OnChange;
        }

        

        private Animator _animator;
        
        protected override void OnLoad()
        {
            base.OnLoad();

            _viewModel.DoAttach += AttachProjectileToSelf;

            _projectileRooTransform = Transform.FindChildRecursivelyBreadthFirst("ProjectileRoot");
            _animator = GameObject.GetComponent<Animator>();
        }
        protected override void OnShow()
        {
            base.OnShow();

            _animator.SetBool("IsDead", false);
            
            Transform.localEulerAngles = new Vector3(0, 180f + Random.Range(-_viewModel.Rotation, _viewModel.Rotation), 0);

            BalistaContext.Instance.IntervalRunner.SubscribeToInterval(Walk,0f);
        }

        protected override void SetPosition()
        {
            Transform.position = _parent.GetRandomSpawnPoint();
        }

        private void Walk()
        {
            Transform.localPosition += Transform.forward * Time.deltaTime * _viewModel.Speed;
        }

        protected override void OnHide(string reason)
        {
            BalistaContext.Instance.IntervalRunner.UnsubscribeFromInterval(Walk);

            _animator.SetBool("IsDead", true);

            base.OnHide(reason);
        }

        protected override void OnDestroy()
        {
            _viewModel.DoAttach -= AttachProjectileToSelf;
            _animator = null;
            BalistaContext.Instance.IntervalRunner.UnsubscribeFromInterval(Walk);

            base.OnDestroy();
        }

        private void Animation_OnChange()
        {
            _animator.SetBool(_viewModel.AnimationId.GetValue(), true);
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