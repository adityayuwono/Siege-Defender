using Scripts.Components;
using Scripts.ViewModels;

namespace Scripts.Views
{
	public class PlayerHitboxView : ElementView
	{
		private readonly PlayerHitbox _viewModel;

		public PlayerHitboxView(PlayerHitbox viewModel, ObjectView parent)
			: base(viewModel, parent)
		{
			_viewModel = viewModel;
		}

		protected override void OnShow()
		{
			base.OnShow();

			GameObject.AddComponent<TriggerController>().Collision += _viewModel.CollideWithTarget;
		}
	}
}