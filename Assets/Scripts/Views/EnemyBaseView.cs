using Scripts.ViewModels;
using UnityEngine;

namespace Scripts.Views
{
    public class EnemyBaseView : LivingObjectView
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

            _animator = GetAnimator();
        }

        protected override void OnShow()
        {
            base.OnShow();

            if (_animator != null)
                _animator.SetBool("IsDead", false);
            
            // Make the enemy face the player
            Transform.localEulerAngles = new Vector3(0, 180f + (Random.Range(-1, 1)*_viewModel.Rotation), 0);

            BalistaContext.Instance.IntervalRunner.SubscribeToInterval(Walk,0f);
        }

        protected override void SetPosition()
        {
            Transform.position = _parent.GetRandomSpawnPoint();
        }

        protected virtual Animator GetAnimator()
        {
            return GameObject.GetComponent<Animator>();
        }

        protected void Animate_SetBool(string name, bool value)
        {
            _animator.SetBool(name, value);
        }

        private void Walk()
        {
            Transform.localPosition += Transform.forward * Time.deltaTime * _viewModel.Speed;
        }

        protected override void OnHide(string reason)
        {
            BalistaContext.Instance.IntervalRunner.UnsubscribeFromInterval(Walk);

            // Start the death animation, if any
            if (_animator != null)
                _animator.SetBool("IsDead", true);

            base.OnHide(reason);
        }

        protected override void OnDestroy()
        {
            _animator = null;
            BalistaContext.Instance.IntervalRunner.UnsubscribeFromInterval(Walk);

            base.OnDestroy();
        }

        private string _lastAnimationValue;
        private void Animation_OnChange()
        {
            if (!string.IsNullOrEmpty(_lastAnimationValue))
                _animator.SetBool(_lastAnimationValue, false);
            _animator.SetBool(_viewModel.AnimationId.GetValue(), true);
            _lastAnimationValue = _viewModel.AnimationId.GetValue();
        }
    }
}