using System.Collections;
using Scripts.Components;
using Scripts.ViewModels.Weapons;
using UnityEngine;

namespace Scripts.Views.Weapons
{
	public class ProjectileBaseView : RigidbodyView
	{
		private readonly ProjectileBase _viewModel;

		private CollisionController _collisionController;

		protected ProjectileBaseView(ProjectileBase viewModel, ShooterView parent) : base(viewModel, parent)
		{
			_viewModel = viewModel;
		}

		protected override void OnLoad()
		{
			base.OnLoad();

			_collisionController = GameObject.AddComponent<CollisionController>();
		}

		protected override void OnShow()
		{
			base.OnShow();

			_collisionController.OnCollision += _viewModel.CollideWithTarget;
			_collisionController.enabled = true;
			Transform.parent = null;
		}

		protected override void OnHide(string reason)
		{
			_viewModel.Root.StartCoroutine(DelayedHiding(reason));
		}

		protected override void OnDestroy()
		{
			_collisionController = null;

			base.OnDestroy();
		}

		private IEnumerator DelayedHiding(string reason)
		{
			yield return new WaitForSeconds(_viewModel.HideDelay);
			HideProjectile(reason);
		}

		protected virtual void HideProjectile(string reason)
		{
			_collisionController.OnCollision -= _viewModel.CollideWithTarget;
			_collisionController.enabled = false;

			base.OnHide(reason);
		}

		protected override void OnDeath(string reason)
		{
			Transform.parent = null;

			base.OnDeath(reason);
		}
	}
}