using Scripts.ViewModels;
using UnityEngine;

namespace Scripts.Views.Enemies
{
	public class StaticEnemyView : LivingObjectView
	{
		private Animation _animation;

		public StaticEnemyView(StaticEnemy viewModel, ObjectView parent)
			: base(viewModel, parent)
		{
		}

		protected override void OnLoad()
		{
			base.OnLoad();

			_animation = GameObject.GetComponent<Animation>();
		}

		protected override void OnShow()
		{
			base.OnShow();

			var randomRotation = 180f + Random.value * Random.Range(-1, 2);
			Transform.localEulerAngles = new Vector3(0, randomRotation, 0);

			_animation.Play("Spawn");
		}

		protected override void OnHide(string reason)
		{
			_animation.Play("Death");

			base.OnHide(reason);
		}
	}
}