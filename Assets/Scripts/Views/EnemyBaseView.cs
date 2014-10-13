using Scripts.ViewModels;
using UnityEngine;

namespace Scripts.Views
{
    public class EnemyBaseView : LivingObjectView
    {
        private readonly EnemyBase _viewModel;

        public EnemyBaseView(EnemyBase viewModel, ObjectView parent) : base(viewModel, parent)
        {
            _viewModel = viewModel;
        }

        private Animator _animator;
        
        protected override void OnLoad()
        {
            base.OnLoad();

            _viewModel.AnimationId.OnChange += Animation_OnChange;

            _animator = GetAnimator();
        }

        protected override void OnShow()
        {
            base.OnShow();

            // TODO: Find more elegant solution, if possible
            // Make the enemy face the player
            var randomRotation = 180f + (Random.value*Random.Range(-1, 2)*_viewModel.Rotation);
            Transform.localEulerAngles = new Vector3(0, randomRotation, 0);

            // TODO: This is checking for both Legacy and Mecanim
            if (_animator != null)
            {
                _animator.SetBool("IsDead", false);
                BalistaContext.Instance.IntervalRunner.SubscribeToInterval(Walk, 0f);
            }
            else
            {
                var animation = GameObject.GetComponent<Animation>();
                if (animation != null)
                {
                    animation.Play("Spawn");
                    BalistaContext.Instance.IntervalRunner.SubscribeToInterval(StartWalkAnimationSubscription, 1f, false);
                }
            }
        }

        private void StartWalkAnimationSubscription()
        {
            BalistaContext.Instance.IntervalRunner.UnsubscribeFromInterval(StartWalkAnimationSubscription);
            var animation = GameObject.GetComponent<Animation>();
            animation.Play("Walk");
            BalistaContext.Instance.IntervalRunner.SubscribeToInterval(Walk, 0f);
        }

        protected override void SetPosition()
        {
            if (_viewModel.EnemyManager != null)
            {
                var enemyManagerView = _viewModel.Root.GetView<EnemyManagerView>(_viewModel.EnemyManager);
                Transform.position = enemyManagerView.GetRandomSpawnPoint();
            }
        }

        protected virtual Animator GetAnimator()
        {
            return GameObject.GetComponent<Animator>();
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
            else
            {
                var animation = GameObject.GetComponent<Animation>();
                if (animation != null)
                    animation.Play("Death");
            }

            base.OnHide(reason);
        }

        protected override void OnDestroy()
        {
            // We may still be subscribed to this
            BalistaContext.Instance.IntervalRunner.UnsubscribeFromInterval(StartWalkAnimationSubscription);

            _viewModel.AnimationId.OnChange -= Animation_OnChange;
            _animator = null;
            BalistaContext.Instance.IntervalRunner.UnsubscribeFromInterval(Walk);

            base.OnDestroy();
        }

        private string _lastAnimationValue;
        private void Animation_OnChange()
        {
            OnAnimationChanged();
        }

        protected virtual void OnAnimationChanged()
        {
            if (!string.IsNullOrEmpty(_lastAnimationValue))
                _animator.SetBool(_lastAnimationValue, false);
            _animator.SetBool(_viewModel.AnimationId.GetValue(), true);
            _lastAnimationValue = _viewModel.AnimationId.GetValue();
        }
    }
}