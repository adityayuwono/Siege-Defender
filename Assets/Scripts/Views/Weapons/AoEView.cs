using Scripts.ViewModels.Weapons;
using UnityEngine;

namespace Scripts.Views.Weapons
{
	public class AoEView : ProjectileBaseView
	{
		private readonly AoE _viewModel;

		public AoEView(AoE viewModel, ShooterView parent)
			: base(viewModel, parent)
		{
			_viewModel = viewModel;
		}

		protected override void OnLoad()
		{
			base.OnLoad();

			Freeze();
		}

		protected override void OnShow()
		{
			base.OnShow();

			Transform.localScale = Vector3.one * _viewModel.Radius / 2f;
			Transform.parent = null;
			Transform.position = _viewModel.Position;
		}
	}
}