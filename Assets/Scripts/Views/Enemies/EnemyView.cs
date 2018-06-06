﻿using Scripts.Components;
using Scripts.Helpers;
using Scripts.ViewModels;
using UnityEngine;

namespace Scripts.Views.Enemies
{
	public class EnemyView : LivingObjectView
	{
		private readonly Enemy _viewModel;
		private GameObject _character;
		private string _lastAnimationValue;

		private Transform _targetTransform;

		public EnemyView(Enemy viewModel, ObjectView parent) : base(viewModel, parent)
		{
			_viewModel = viewModel;
		}

		protected GameObject CharacterRoot
		{
			get
			{
				if (_character == null)
				{
					var characterRoot = Transform.Find(Values.Defaults.BossCharacterRootName);
					if (characterRoot != null)
					{
						CharacterTransform = characterRoot;
						_character = characterRoot.gameObject;
						_character.AddComponent<ViewModelController>().ViewModel = _viewModel;
					}
				}

				return _character;
			}
		}

		protected Transform CharacterTransform { get; private set; }

		protected Animator Animator { get; private set; }

		protected override void OnLoad()
		{
			base.OnLoad();

			Animator = GetAnimator();

			LoadTarget();
		}

		protected virtual void LoadTarget()
		{
			var targetTransform = _viewModel.Root.GetView<ObjectView>(_viewModel.Target).Transform;
			_targetTransform = targetTransform;
		}

		protected override void OnShow()
		{
			base.OnShow();

			_viewModel.AnimationId.OnChange += Animation_OnChange;

			AdjustRotation();
			StartWalking();

			_viewModel.OnSpawn();
		}

		protected virtual void AdjustRotation()
		{
			// make the enemy look directly at the player
			Transform.LookAt(_targetTransform);

			// Need to reset x rotation to make sure we are walking straight
			var currentRotation = Transform.localEulerAngles;
			currentRotation.x = 0;
			Transform.localEulerAngles = currentRotation;
		}

		protected virtual void StartWalking()
		{
			Animator.Play("Spawn");
			_viewModel.Root.Context.IntervalRunner.SubscribeToInterval(StartWalkAnimationSubscription, 1f, false);
		}

		private void StartWalkAnimationSubscription()
		{
			_viewModel.OnWalk();
			_viewModel.Root.Context.IntervalRunner.UnsubscribeFromInterval(StartWalkAnimationSubscription);
			_viewModel.AnimationId.SetValue("Walk");

			var targetMovement = _targetTransform.position;
			targetMovement.x += Random.Range(-3f, 3f);

			var distance = Vector3.Distance(Transform.position, targetMovement);
			var duration = distance / _viewModel.Speed;

			iTween.MoveTo(GameObject, iTween.Hash("position", targetMovement, "time", duration, "easetype", "linear"));
			iTween.RotateTo(GameObject, iTween.Hash("y", 180d, "time", duration, "easetype", "linear"));

			_viewModel.Root.Context.IntervalRunner.SubscribeToInterval(StartAttackAnimation, duration, false);
		}

		private void StartAttackAnimation()
		{
			_viewModel.Root.Context.IntervalRunner.UnsubscribeFromInterval(StartAttackAnimation);
			_viewModel.Root.Context.IntervalRunner.SubscribeToInterval(AttackAnimation, _viewModel.AttackSpeed);
		}

		private void AttackAnimation()
		{
			_viewModel.AnimationId.SetValue("Attack");
			_viewModel.OnAttack();
		}

		private Animator GetAnimator()
		{
			return CharacterRoot.GetComponent<Animator>();
		}

		protected void Walk()
		{
			Transform.localPosition += Transform.forward * Time.deltaTime * _viewModel.Speed;
		}

		protected override void OnHide(string reason)
		{
			// We may still be subscribed to these
			iTween.Stop(GameObject);
			iTween.Stop(CharacterRoot);
			UnsubscribeIntervals();

			_viewModel.AnimationId.SetValue("Death");
			_viewModel.AnimationId.OnChange -= Animation_OnChange;

			base.OnHide(reason);
		}

		protected override void OnDestroy()
		{
			// We may still be subscribed to this
			UnsubscribeIntervals();

			base.OnDestroy();
		}

		private void UnsubscribeIntervals()
		{
			_viewModel.Root.Context.IntervalRunner.UnsubscribeFromInterval(StartWalkAnimationSubscription);
			_viewModel.Root.Context.IntervalRunner.UnsubscribeFromInterval(Walk);
			_viewModel.Root.Context.IntervalRunner.UnsubscribeFromInterval(StartAttackAnimation);
			_viewModel.Root.Context.IntervalRunner.UnsubscribeFromInterval(AttackAnimation);
		}

		protected void Animation_OnChange()
		{
			if (!string.IsNullOrEmpty(_lastAnimationValue)) Animator.SetBool(_lastAnimationValue, false);
			Animator.SetBool(_viewModel.AnimationId.GetValue(), true);
			_lastAnimationValue = _viewModel.AnimationId.GetValue();
		}
	}
}