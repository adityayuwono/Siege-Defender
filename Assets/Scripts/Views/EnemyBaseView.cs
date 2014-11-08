using System;
using Scripts.Helpers;
using Scripts.ViewModels;
using UnityEngine;
using Random = UnityEngine.Random;

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
        private Animation _animation;
        
        protected override void OnLoad()
        {
            base.OnLoad();

            _animator = GetAnimator();

            if (_viewModel.Target != null)
            {
                var targetTransform = _viewModel.Root.GetView<ObjectView>(_viewModel.Target).Transform;
                _targetTransform = targetTransform;
            }
        }

        protected override void OnShow()
        {
            base.OnShow();

            if (_targetTransform != null)
            {
                // make the enemy look directly at the player
                Transform.LookAt(_targetTransform);

                // Need to reset x rotation to make sure we are walking straight
                var currentRotation = Transform.localEulerAngles;
                currentRotation.x = 0;
                Transform.localEulerAngles = currentRotation;
            }
            else
            {
                var randomRotation = 180f + (Random.value*Random.Range(-1, 2)*_viewModel.Rotation);
                Transform.localEulerAngles = new Vector3(0, randomRotation, 0);
            }

            // TODO: This is checking for both Legacy and Mecanim, it should not
            // Mecanim is only used in bosses
            if (_animator != null)
            {
                _viewModel.AnimationId.OnChange += Animation_OnChange;
                _animator.SetBool("IsDead", false);
                BalistaContext.Instance.IntervalRunner.SubscribeToInterval(Walk);
            }
            else
            {
                _animation = GameObject.GetComponent<Animation>();
                if (_animation != null)
                {
                    _animation.Play("Spawn");
                    BalistaContext.Instance.IntervalRunner.SubscribeToInterval(StartWalkAnimationSubscription, 1f, false);
                }
            }

            _viewModel.OnSpawn();
        }

        private Transform _targetTransform;

        private void StartWalkAnimationSubscription()
        {
            _viewModel.OnWalk();

            BalistaContext.Instance.IntervalRunner.UnsubscribeFromInterval(StartWalkAnimationSubscription);
            _animation.CrossFade("Walk");
            if (_viewModel.Speed > 0)
            {
                if (_targetTransform != null)
                {
                    var distance = Vector3.Distance(Transform.position, _targetTransform.position);
                    var duration = distance/_viewModel.Speed;

                    var targetMovement = _targetTransform.position;
                    targetMovement.x += Random.Range(-3f, 3f);

                    iTween.MoveTo(GameObject, iTween.Hash("position", targetMovement, "time", duration, "easetype", "linear"));
                    iTween.RotateTo(GameObject, iTween.Hash("y", 180d, "time", duration, "easetype", "linear"));

                    BalistaContext.Instance.IntervalRunner.SubscribeToInterval(StartAttackAnimation, duration, false);
                }
                else
                {
                    BalistaContext.Instance.IntervalRunner.SubscribeToInterval(Walk);
                }
            }
        }

        private void StartAttackAnimation()
        {
            BalistaContext.Instance.IntervalRunner.UnsubscribeFromInterval(StartAttackAnimation);
            BalistaContext.Instance.IntervalRunner.SubscribeToInterval(AttackAnimation, _viewModel.AttackSpeed);
        }

        private void AttackAnimation()
        {
            if (_animation == null)
                throw new EngineException(this, string.Format("Failed to find Animation in {0}", Id));

            _animation.Play("Attack");
            _viewModel.OnAttack();
        }

        protected virtual Animator GetAnimator()
        {
            var animator = GameObject.GetComponent<Animator>();
            return animator;
        }
        
        private void Walk()
        {
            Transform.localPosition += Transform.forward * Time.deltaTime * _viewModel.Speed;
        }

        protected override void OnHide(string reason)
        {
            // We may still be subscribed to these
            iTween.Stop(GameObject);
            UnsubscribeEverything();

            // Start the death animation, if any
            if (_animator != null)
                _animator.SetBool("IsDead", true);
            else
            {
                var animation = GameObject.GetComponent<Animation>();
                if (animation != null)
                    animation.CrossFade("Death");
            }

            base.OnHide(reason);
        }

        protected override void OnDestroy()
        {
            // We may still be subscribed to this
            UnsubscribeEverything();
            _viewModel.AnimationId.OnChange -= Animation_OnChange;

            base.OnDestroy();
        }

        private void UnsubscribeEverything()
        {
            BalistaContext.Instance.IntervalRunner.UnsubscribeFromInterval(StartWalkAnimationSubscription);
            BalistaContext.Instance.IntervalRunner.UnsubscribeFromInterval(Walk);
            BalistaContext.Instance.IntervalRunner.UnsubscribeFromInterval(StartAttackAnimation);
            BalistaContext.Instance.IntervalRunner.UnsubscribeFromInterval(AttackAnimation);
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